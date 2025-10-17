using CCA.Util;
using HigherEducation.BusinessLayer;
using HigherEducation.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class ViewMyWorkshopBookings : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

        CCACrypto ccaCrypto = new CCACrypto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["UserId"] == null || Session["Mobile"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                BindBookings();
            }
        }


        protected void gvBookings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBookings.PageIndex = e.NewPageIndex;
            BindBookings();
        }



        // Updated helper method to check if pay button should be shown
        public bool ShouldShowPayButton(string paymentStatus, DateTime workshopDate, string workshopTime)
        {
            if (paymentStatus.ToLower() != "pending")
                return false;

            TimeSpan time;
            if (!TimeSpan.TryParse(workshopTime, out time))
                return false;

            DateTime workshopDateTime = workshopDate.Date.Add(time);
            return workshopDateTime > DateTime.Now;
        }






        private void UpdatePagerInfo()
        {
            int currentPage = gvBookings.PageIndex + 1;
            int totalPages = gvBookings.PageCount;
            int pageSize = gvBookings.PageSize;
            int totalRecords = GetTotalRecordCount();
            int startRecord = (gvBookings.PageIndex * pageSize) + 1;
            int endRecord = Math.Min((gvBookings.PageIndex + 1) * pageSize, totalRecords);

            // More attractive page info
            lblPageInfo.Text = $"Page {currentPage} of {totalPages} • {startRecord}-{endRecord} of {totalRecords}";

            // Update total records label
            lblTotalRecords.Text = $"{totalRecords} booking{(totalRecords != 1 ? "s" : "")} found";
        }

        private int GetTotalRecordCount()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("sp_GetMyBookingsCount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_UserId", Session["UserId"].ToString());

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }


        private void ShowError(string message)
        {
            string cleanMessage = message.Replace("'", "\\'");
            string script = $@"alert('Error: {cleanMessage}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }


        // Update the RowCommand method to remove CheckPayment handling
        protected void gvBookings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int bookingId = Convert.ToInt32(e.CommandArgument);
                ViewBookingDetails(bookingId);
            }
            else if (e.CommandName == "MakePayment")
            {
                int bookingId = Convert.ToInt32(e.CommandArgument);
                MakePayment(bookingId);
            }
        }

        // Rest of the code remains the same...
        private void ViewBookingDetails(int bookingId)
        {
            Session["LastBookingID"] = bookingId;
            Response.Redirect($"WorkShopBookingConfirmation.aspx");
        }

        // Helper method for payment status badge colors
        public string GetPaymentStatusBadge(string status)
        {
            switch (status.ToLower())
            {
                case "completed": return "success";
                case "pending": return "warning";
                case "failed": return "danger";
                case "refunded": return "info";
                case "confirmed": return "success";
                default: return "secondary";
            }
        }

        // Helper method to format time for display
        public string FormatTime(object timeValue)
        {
            if (timeValue == null) return "N/A";

            if (TimeSpan.TryParse(timeValue.ToString(), out TimeSpan time))
            {
                DateTime timeDate = DateTime.Today.Add(time);
                return timeDate.ToString("hh:mm tt");
            }

            return timeValue.ToString();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindBookings();
            ShowMessage("Bookings refreshed successfully.", "success");
        }

        private void ShowMessage(string message, string type)
        {
            string cleanMessage = message.Replace("'", "\\'");
            string script = $@"alert('{cleanMessage}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }

        protected void gvBookings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find controls
                Button btnMakePayment = (Button)e.Row.FindControl("btnMakePayment");
                Button btnViewDetails = (Button)e.Row.FindControl("btnViewDetails");
                Label lblPaymentMessage = (Label)e.Row.FindControl("lblPaymentMessage");
                Label lblPaymentStatus = (Label)e.Row.FindControl("lblPaymentStatus");
                Label lblWorkshopDate = (Label)e.Row.FindControl("lblWorkshopDate");
                Label lblWorkshopTime = (Label)e.Row.FindControl("lblWorkshopTime");

                if (btnMakePayment != null && btnViewDetails != null && lblPaymentMessage != null &&
                    lblPaymentStatus != null && lblWorkshopDate != null && lblWorkshopTime != null)
                {
                    // Get payment status
                    string paymentStatus = lblPaymentStatus.Text.ToLower();

                    // Get workshop date and time
                    DateTime workshopDate;
                    TimeSpan workshopTime;

                    bool hasValidDate = DateTime.TryParse(lblWorkshopDate.Text, out workshopDate);
                    bool hasValidTime = TimeSpan.TryParse(lblWorkshopTime.Text, out workshopTime);

                    if (hasValidDate && hasValidTime)
                    {
                        // Combine date and time
                        DateTime workshopDateTime = workshopDate.Date.Add(workshopTime);
                        DateTime currentDateTime = DateTime.Now;

                        // Check if workshop date/time has passed
                        bool isWorkshopPassed = workshopDateTime <= currentDateTime;

                        // Get booking ID for seat availability check
                        int bookingId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "BookingID"));
                        int slotId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SlotID"));

                        // Check seat availability
                        bool hasAvailableSeats = CheckSeatAvailability(slotId);

                        if (paymentStatus == "pending")
                        {
                            if (isWorkshopPassed)
                            {
                                // Workshop passed and payment is pending - show only message, hide all buttons
                                btnMakePayment.Visible = false;
                                btnViewDetails.Visible = false;
                                lblPaymentMessage.Visible = true;
                                lblPaymentMessage.Text = "Payment not available - workshop has already started or passed";

                                // Add visual indicator for expired booking
                                e.Row.CssClass = e.Row.CssClass + " expired-booking";
                            }
                            else if (!hasAvailableSeats)
                            {
                                // Workshop is in future but no seats available - show message
                                btnMakePayment.Visible = false;
                                btnViewDetails.Visible = false;
                                lblPaymentMessage.Visible = true;
                                lblPaymentMessage.Text = "Payment not available - no seats available";
                                lblPaymentMessage.CssClass = "text-warning payment-message";
                            }
                            else
                            {
                                // Workshop is in future, payment is pending, and seats are available - show Pay Now and View Details buttons
                                btnMakePayment.Visible = true;
                                btnViewDetails.Visible = false;
                                lblPaymentMessage.Visible = false;
                            }
                        }
                        else
                        {
                            // Payment is not pending (completed/failed/refunded) - show only View Details button
                            btnMakePayment.Visible = false;
                            btnViewDetails.Visible = true;
                            lblPaymentMessage.Visible = false;
                        }
                    }
                    else
                    {
                        // Invalid date/time format, show only View Details button
                        btnMakePayment.Visible = false;
                        btnViewDetails.Visible = true;
                        lblPaymentMessage.Visible = false;
                    }
                }
            }
        }

        // Method to check seat availability
        private bool CheckSeatAvailability(int slotId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_CheckSeatAvailability", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_SlotID", slotId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader["IsAvailable"]) == 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkShopBookingConfirmation";
                    clsLogger.ExceptionMsg = "CheckSeatAvailability";
                    clsLogger.SaveException();
                    return false;
                }
            }
            return false;
        }


        // Update the stored procedure call to include SlotID
        private void BindBookings()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_GetMyWorkshopBookings", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_UserId", Session["UserId"].ToString());

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        gvBookings.DataSource = dt;
                        gvBookings.DataBind();

                        // Update record count
                        lblTotalRecords.Text = $"Total Bookings: {dt.Rows.Count}";
                        UpdatePagerInfo();
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkShopBookingConfirmation";
                    clsLogger.ExceptionMsg = "BindBookings";
                    clsLogger.SaveException();
                    ShowError($"Error loading bookings: {ex.Message}");
                }
            }
        }

        private void MakePayment(int bookingId)
        {
            try
            {
                // Declare variables outside the using block
                string fullName = "";
                int slotId = 0;
                int ITI_Id = 0;
                int DistrictId = 0;
                DateTime workshopDate = DateTime.MinValue;
                TimeSpan workshopTime = TimeSpan.Zero;
                string bookingStatus = "";
                string paymentStatus = "";
                decimal bookingAmount = 0;

                // First, get booking details and close the DataReader
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_GetBookingDetailsForPayment", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_BookingID", bookingId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Extract slotId and other details
                                fullName = reader["FullName"].ToString();
                                slotId = Convert.ToInt32(reader["SlotID"]);
                                ITI_Id = Convert.ToInt32(reader["ITI_Id"]);
                                DistrictId = Convert.ToInt32(reader["DistrictId"]);
                                workshopDate = Convert.ToDateTime(reader["WorkshopDate"]);
                                workshopTime = TimeSpan.Parse(reader["WorkshopTime"].ToString());
                                bookingStatus = reader["BookingStatus"].ToString();
                                paymentStatus = reader["PaymentStatus"].ToString();
                                bookingAmount = Convert.ToDecimal(reader["BookingAmount"]);
                            }
                            else
                            {
                                ShowError("Booking details not found.");
                                return;
                            }
                        } // DataReader is closed here automatically
                    }
                } // Connection is closed here

                // Validate payment conditions (outside the first connection)
                if (!ValidatePaymentConditions(slotId, bookingId, workshopDate, workshopTime, paymentStatus))
                {
                    return; // Validation failed, error already shown
                }

                // Now create a new connection for the transaction
                using (MySqlConnection transConn = new MySqlConnection(connectionString))
                {
                    transConn.Open();
                    using (MySqlTransaction transaction = transConn.BeginTransaction())
                    {
                        try
                        {
                            string paymentReferenceId = GeneratePaymentReferenceId();
                            int paymentId = SavePaymentRecord(transConn, transaction, bookingId, slotId, paymentReferenceId, bookingAmount, fullName, ITI_Id);
                            if (paymentId <= 0)
                            {
                                transaction.Rollback();
                                ShowError("Failed to save payment record.");
                                return;
                            }

                            // Commit transaction
                            transaction.Commit();

                            // Store IDs in session for payment gateway
                            Session["LastBookingID"] = bookingId;
                            Session["PaymentReferenceID"] = paymentReferenceId;
                            Session["PaymentAmount"] = bookingAmount;

                            // Redirect to payment gateway
                            RedirectToPaymentGateway(bookingId, paymentReferenceId, bookingAmount, ITI_Id, DistrictId);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ShowError($"Transaction failed: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/WorkShopBookingConfirmation";
                clsLogger.ExceptionMsg = "MakePayment";
                clsLogger.SaveException();
                ShowError($"Error processing payment: {ex.Message}");
            }
        }


        private void RedirectToPaymentGateway(int bookingId, string paymentReferenceId, decimal amount, int ITI_Id, int DistrictId)
        {
            try
            {
                // Create FeeModule object with workshop booking data
                FeeModule objFeeModule = new FeeModule();

                // Set payment parameters
                objFeeModule.RegistrationId = Session["UserId"]?.ToString() ?? "";
                objFeeModule.TotalFee = (int)amount;
                objFeeModule.amount = (int)amount;
                objFeeModule.PaymentTransactionId = paymentReferenceId;

                // Merchant configuration from Web.config
                objFeeModule.merchant_id = ConfigurationManager.AppSettings["strMerchantId_ITI"];
                objFeeModule.order_id = paymentReferenceId;
                objFeeModule.currency = "INR";
                objFeeModule.redirect_url = ConfigurationManager.AppSettings["redirect_url_Workshop"];
                objFeeModule.cancel_url = ConfigurationManager.AppSettings["cancel_url_Workshop"];
                objFeeModule.language = "EN";

                // Additional parameters for workshop booking
                objFeeModule.merchant_param1 = bookingId.ToString();  // Booking ID
                objFeeModule.merchant_param2 = Session["UserId"]?.ToString();  // User ID
                objFeeModule.merchant_param3 = Convert.ToString(ITI_Id);  // ITI ID
                objFeeModule.merchant_param4 = Convert.ToString(DistrictId);  // District ID
                objFeeModule.merchant_param5 = "WORKSHOP";  // Payment type



                // Generate encrypted request for payment gateway
                string ccaRequest = GenerateCCARequest(objFeeModule);
                string workingKey = ConfigurationManager.AppSettings["workingKey_ITI"];
                string strAccessCode = ConfigurationManager.AppSettings["strAccessCode_ITI"];

                // Encrypt the request (you need the ccaCrypto class)
                string strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);

                // Store in session or pass to payment page
                Session["strEncRequest"] = strEncRequest;
                Session["strAccessCode"] = strAccessCode;
                Session["strMerchantId"] = objFeeModule.merchant_id;

                // Redirect to payment page
                Response.Redirect("PaymentGateway.aspx", false);
                Context.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/WorkShopBookingConfirmation";
                clsLogger.ExceptionMsg = "RedirectToPaymentGateway";
                clsLogger.SaveException();
                ShowMessage("Error initializing payment gateway.", "danger");
            }
        }

        private void LogError(string methodName, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in {methodName}: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
        }

        private string GenerateCCARequest(FeeModule objFeeModule)
        {
            string ccaRequest = "";

            // Get all properties and build request string
            PropertyInfo[] properties = objFeeModule.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var propValue = property.GetValue(objFeeModule, null);

                if (propValue != null && !string.IsNullOrEmpty(propValue.ToString()))
                {
                    // Skip properties that start with underscore or are internal
                    if (!propName.StartsWith("_"))
                    {
                        ccaRequest = ccaRequest + propName + "=" + HttpUtility.UrlEncode(Convert.ToString(propValue)) + "&";
                    }
                }
            }

            // Remove the last '&' character
            if (ccaRequest.EndsWith("&"))
            {
                ccaRequest = ccaRequest.Substring(0, ccaRequest.Length - 1);
            }

            return ccaRequest;
        }


        // Comprehensive validation method
        private bool ValidatePaymentConditions(int slotId, int bookingId, DateTime workshopDate, TimeSpan workshopTime, string paymentStatus)
        {
            // Check if workshop is still in future
            DateTime workshopDateTime = workshopDate.Date.Add(workshopTime);
            if (workshopDateTime <= DateTime.Now)
            {
                ShowError("Cannot make payment for workshops that have already started or passed.");
                BindBookings(); // Refresh grid to update button states
                return false;
            }

            // Check seat availability
            if (!CheckSeatAvailability(slotId))
            {
                ShowError("Sorry, no seats are available for this workshop slot anymore.");
                BindBookings(); // Refresh grid to update button states
                return false;
            }

            // Check if payment is still pending
            if (paymentStatus.ToLower() != "pending")
            {
                ShowError("Payment has already been processed for this booking.");
                BindBookings(); // Refresh grid to update button states
                return false;
            }
            return true;
        }

        // Generate payment reference ID
        private string GeneratePaymentReferenceId()
        {
            string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
            string mobileNumber = Session["Mobile"]?.ToString() ?? "0000000000";
            string paymentReferenceId = mobileNumber + currentDateTime;

            if (paymentReferenceId.Length > 30)
            {
                paymentReferenceId = paymentReferenceId.Substring(paymentReferenceId.Length - 30);
            }

            return paymentReferenceId;
        }


        private int SavePaymentRecord(MySqlConnection conn, MySqlTransaction transaction, int bookingId, int slotId, string paymentReferenceId, decimal bookingAmount, string fullName, int ITI_Id)
        {
            try
            {

                using (MySqlCommand cmd = new MySqlCommand("sp_SavePaymentRecord", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_FullName", fullName);
                    cmd.Parameters.AddWithValue("p_UserId", Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("p_PaymentReferenceID", paymentReferenceId);
                    cmd.Parameters.AddWithValue("p_College_id", ITI_Id);
                    cmd.Parameters.AddWithValue("p_SubscriptionId_SlotId", slotId.ToString());
                    cmd.Parameters.AddWithValue("p_PaymentType", "WORKSHOP");
                    cmd.Parameters.AddWithValue("p_Mobile", Session["Mobile"]?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("p_Email", Session["Email"]?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("p_TotalAmount", bookingAmount);
                    cmd.Parameters.AddWithValue("p_Payment_gateway", "CCAvenue"); // Replace with actual gateway name
                    cmd.Parameters.AddWithValue("p_CreateUser", Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("p_IPAddress", GetClientIPAddress());
                    cmd.Parameters.AddWithValue("p_Remarks", "Workshop slot booking payment");

                    // Execute and get the payment ID
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ViewMyWorkshopBookings";
                clsLogger.ExceptionMsg = "SavePaymentRecord";
                clsLogger.SaveException();
                return 0;
            }
        }

        private string GetClientIPAddress()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress ?? "Unknown";
        }

    }
}