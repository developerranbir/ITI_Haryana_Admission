using CCA.Util;
using HigherEducation.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class PaymentResponse : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        CCACrypto ccaCrypto = new CCACrypto();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Hide loading panel and process payment response
                pnlLoading.Visible = false;
                ProcessPaymentResponse();
            }
        }

        private void ProcessPaymentResponse()
        {
            try
            {
                // Get encrypted response from CCAvenue
                string encResponse = Request.Form["encResp"];

                if (string.IsNullOrEmpty(encResponse))
                {
                    ShowError("No payment response received.");
                    return;
                }

                // Decrypt the response
                string workingKey = ConfigurationManager.AppSettings["workingKey_ITI"];
                string decryptedResponse = ccaCrypto.Decrypt(encResponse, workingKey);

                if (string.IsNullOrEmpty(decryptedResponse))
                {
                    ShowError("Unable to decrypt payment response.");
                    return;
                }

                // Parse the decrypted response
                NameValueCollection responseParams = HttpUtility.ParseQueryString(decryptedResponse);

                // Extract payment parameters
                string orderStatus = responseParams["order_status"];
                string trackingId = responseParams["tracking_id"];
                string bankRefNo = responseParams["bank_ref_no"];
                string orderId = responseParams["order_id"];
                string paymentMode = responseParams["payment_mode"];
                string cardName = responseParams["card_name"];
                string statusCode = responseParams["status_code"];
                string statusMessage = responseParams["status_message"];
                string amount = responseParams["amount"];
                string transDate = responseParams["trans_date"];

                // Update payment record in database
                bool updateSuccess = UpdatePaymentResponse(orderId, trackingId, bankRefNo, amount,
                    orderStatus, paymentMode, cardName, statusCode, statusMessage, transDate);

                if (updateSuccess)
                {
                    // Update workshop booking status based on payment result
                    if (orderStatus == "Success")
                    {

                        MySqlConnection conn = new MySqlConnection(connectionString);

                        conn.Open();

                        // Start transaction
                        MySqlTransaction transaction = conn.BeginTransaction();
                        // Step 1: Get the booking ID associated with this payment
                        int bookingId = GetBookingIdByPaymentReference(orderId, conn, transaction);
                        Session["LastBookingID"] = bookingId;
                        ShowSuccessPage(orderId, trackingId, amount, paymentMode, transDate);
                    }
                    else if (orderStatus == "Failure")
                    {
                        ShowFailurePage(orderId, statusMessage);
                    }
                    else if (orderStatus == "Pending" || orderStatus == "Initiated")
                    {
                        ShowPendingPage(orderId);
                    }
                    else
                    {
                        ShowFailurePage(orderId, "Unknown payment status: " + orderStatus);
                    }
                }
                else
                {
                    ShowError("Failed to update payment record.");
                }
            }
            catch (Exception ex)
            {
                LogError("ProcessPaymentResponse", ex);
                ShowError("An error occurred while processing your payment. Please contact support.");
            }
        }

        private bool UpdatePaymentResponse(string orderId, string trackingId, string bankRefNo, string amount,
     string orderStatus, string paymentMode, string cardName, string statusCode, string statusMessage, string transDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("sp_UpdatePaymentResponse", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Input parameters
                        cmd.Parameters.AddWithValue("p_PaymentReferenceID", orderId);
                        cmd.Parameters.AddWithValue("p_tracking_id", trackingId);
                        cmd.Parameters.AddWithValue("p_bank_ref_no", bankRefNo);
                        cmd.Parameters.AddWithValue("p_bankamount", decimal.TryParse(amount, out decimal amt) ? amt : 0);
                        cmd.Parameters.AddWithValue("p_order_status", orderStatus);
                        cmd.Parameters.AddWithValue("p_payment_mode", paymentMode);
                        cmd.Parameters.AddWithValue("p_card_name", cardName);
                        cmd.Parameters.AddWithValue("p_status_code", statusCode);
                        cmd.Parameters.AddWithValue("p_status_messsage", statusMessage);
                        cmd.Parameters.AddWithValue("p_trans_date", transDate);

                        // Output parameter - match the stored procedure parameter name
                        MySqlParameter rowsUpdatedParam = new MySqlParameter("@p_RowsUpdated", MySqlDbType.Int32);
                        rowsUpdatedParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(rowsUpdatedParam);

                        cmd.ExecuteNonQuery();

                        int rowsUpdated = Convert.ToInt32(rowsUpdatedParam.Value);
                        return rowsUpdated == 1;
                    }
                }
                catch (Exception ex)
                {
                    LogError("UpdatePaymentResponse", ex);
                    return false;
                }
            }
        }


        private int GetBookingIdByPaymentReference(string paymentReferenceId, MySqlConnection conn, MySqlTransaction transaction)
        {
            string query = @"
                SELECT wb.BookingID 
                FROM WorkshopBookings wb
                INNER JOIN tblworkshoplibraryfee pay ON wb.ITI_Id = pay.College_id 
                    AND wb.SlotID = CAST(pay.SubscriptionId_SlotId AS UNSIGNED)
                    AND wb.UserId = pay.UserId
                WHERE pay.PaymentReferenceID = @PaymentReferenceID and wb.BookingStatus='Confirmed'
                LIMIT 1";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@PaymentReferenceID", paymentReferenceId);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private void ShowSuccessPage(string orderId, string trackingId, string amount, string paymentMode, string transDate)
        {
            pnlSuccess.Visible = true;
            pnlFailure.Visible = false;
            pnlPending.Visible = false;

            litBookingIdSuccess.Text = Session["LastBookingID"]?.ToString() ?? "N/A";
            litTransactionId.Text = trackingId;
            litAmountPaid.Text = decimal.TryParse(amount, out decimal amt) ? amt.ToString("0.00") : "0.00";
            litPaymentMode.Text = paymentMode;
            litPaymentDate.Text = DateTime.TryParse(transDate, out DateTime dt) ? dt.ToString("dd-MM-yyyy HH:mm") : DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        }

        private void ShowFailurePage(string orderId, string failureReason)
        {
            pnlSuccess.Visible = false;
            pnlFailure.Visible = true;
            pnlPending.Visible = false;

            litFailureReason.Text = failureReason;
            Session["FailedPaymentReference"] = orderId;
        }

        private void ShowPendingPage(string orderId)
        {
            pnlSuccess.Visible = false;
            pnlFailure.Visible = false;
            pnlPending.Visible = true;

            Session["PendingPaymentReference"] = orderId;
        }

        private void ShowError(string message)
        {
            pnlSuccess.Visible = false;
            pnlFailure.Visible = true;
            pnlPending.Visible = false;

            litFailureReason.Text = message;
        }

        // Button Click Events
        protected void btnViewBooking_Click(object sender, EventArgs e)
        {
            Response.Redirect("WorkShopBookingConfirmation.aspx");
        }

        protected void btnNewBooking_Click(object sender, EventArgs e)
        {
            Response.Redirect("WorkshopSlotBooking.aspx");
        }

        protected void btnRetryPayment_Click(object sender, EventArgs e)
        {
            string failedReference = Session["FailedPaymentReference"]?.ToString();
            if (!string.IsNullOrEmpty(failedReference))
            {
                // Implement retry payment logic
                Response.Redirect("RetryPayment.aspx?Reference=" + failedReference);
            }
            else
            {
                Response.Redirect("WorkshopSlotBooking.aspx");
            }
        }

        protected void btnContactSupport_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContactSupport.aspx");
        }

        protected void btnBackToBookings_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewMyWorkshopBookings.aspx");
        }

        protected void btnCheckStatus_Click(object sender, EventArgs e)
        {
            string pendingReference = Session["PendingPaymentReference"]?.ToString();
            if (!string.IsNullOrEmpty(pendingReference))
            {
                Response.Redirect("PaymentStatus.aspx?Reference=" + pendingReference);
            }
            else
            {
                Response.Redirect("ViewMyWorkshopBookings.aspx");
            }
        }

        protected void btnPendingBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewMyWorkshopBookings.aspx");
        }

        // Utility Methods
        private void LogError(string methodName, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"ERROR in {methodName}: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

            try
            {
                string logPath = Server.MapPath("~/App_Data/Logs/PaymentErrors.txt");
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR in {methodName}: {ex.Message}{Environment.NewLine}Stack Trace: {ex.StackTrace}{Environment.NewLine}{new string('-', 80)}{Environment.NewLine}";
                System.IO.File.AppendAllText(logPath, logMessage);
            }
            catch
            {
                // Ignore file errors
            }
        }
    }
}