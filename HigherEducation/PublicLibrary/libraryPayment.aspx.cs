using CCA.Util;
using HigherEducation.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class libraryPayment : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        CCACrypto ccaCrypto = new CCACrypto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if SubscriptionId exists in query string
                if (Request.QueryString["id"] != null || Request.QueryString["subscriptionid"] != null)
                {
                    string subscriptionId = Request.QueryString["id"] ?? Request.QueryString["subscriptionid"];
                    pendingPaymentAgain(subscriptionId);
                }
                else
                {
                   
                }
            }

        }


        private int SavePaymentRecord(MySqlConnection conn, MySqlTransaction transaction, int bookingId, int slotId, string paymentReferenceId, decimal bookingAmount)
        {
            using (MySqlCommand cmd = new MySqlCommand("sp_SavePaymentRecord", conn, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_FullName", Session["FullName"].ToString());
                cmd.Parameters.AddWithValue("p_UserId", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("p_PaymentReferenceID", paymentReferenceId);
                cmd.Parameters.AddWithValue("p_College_id", Session["College_id"].ToString());
                cmd.Parameters.AddWithValue("p_SubscriptionId_SlotId", slotId.ToString());
                cmd.Parameters.AddWithValue("p_PaymentType", "Library");
                cmd.Parameters.AddWithValue("p_Mobile", Session["Mobile"]?.ToString() ?? "");
                cmd.Parameters.AddWithValue("p_Email", Session["Email"]?.ToString() ?? "");
                cmd.Parameters.AddWithValue("p_TotalAmount", bookingAmount);
                cmd.Parameters.AddWithValue("p_Payment_gateway", "CCAvenue"); // Replace with actual gateway name
                cmd.Parameters.AddWithValue("p_CreateUser", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("p_IPAddress", GetClientIPAddress());
                cmd.Parameters.AddWithValue("p_Remarks", "Library slot booking payment");

                // Execute and get the payment ID
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
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

        private void pendingPaymentAgain(string subscriptionId)
        {
            decimal bookingAmount = 0;
            DataTable dt = getSubscriptionDetails(subscriptionId);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Session["FullName"] = row["FullName"].ToString();
                Session["College_id"] = row["College_id"].ToString();
                Session["Mobile"] = row["Mobile"].ToString();
                Session["Email"] = row["Email"].ToString();
                Session["TotalAmount"] = row["TotalAmount"].ToString();
                bookingAmount = Convert.ToDecimal(row["TotalAmount"].ToString());
            }
            // Validate mandatory parameters


            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

            using (var conn = new MySqlConnection(connectionString))
                if (Convert.ToInt32(subscriptionId) > 0)
                {
                    if (Convert.ToInt32(subscriptionId) > 0)
                    {
                        conn.Open();
                        using (MySqlTransaction transaction = conn.BeginTransaction())
                        {
                            string paymentReferenceId = "0";
                            string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                            //Generate Transaction Number
                            paymentReferenceId = Session["Mobile"].ToString() + currentDateTime;
                            if (paymentReferenceId.Length > 30)
                            {
                                paymentReferenceId = paymentReferenceId.Substring(paymentReferenceId.Length - 30);
                            }
                            int paymentId = SavePaymentRecord(conn, transaction, Convert.ToInt32(subscriptionId), Convert.ToInt32(subscriptionId), paymentReferenceId, bookingAmount);

                            if (paymentId <= 0)
                            {
                                transaction.Rollback();
                                return;
                            }


                            // Commit transaction
                            transaction.Commit();

                            // Store IDs in session for payment gateway
                            Session["LastBookingID"] = subscriptionId;
                            Session["PaymentReferenceID"] = paymentReferenceId;
                            Session["PaymentAmount"] = Convert.ToInt32(bookingAmount);

                            // Step 3: Redirect to payment gateway
                            RedirectToPaymentGateway(Convert.ToInt32(subscriptionId), paymentReferenceId, bookingAmount);

                            return;
                        }
                    }
                }
            // Redirect to payment page with subscriptionId after entry success
            //Response.Redirect("libraryPayment.aspx?sid=" + subscriptionId);


        }

        private void RedirectToPaymentGateway(int bookingId, string paymentReferenceId, decimal amount)
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
                objFeeModule.merchant_param3 = Session["College_id"].ToString();  // ITI ID
                objFeeModule.merchant_param4 = "";  // District ID
                objFeeModule.merchant_param5 = "Library";  // Payment type



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
                //LogError("RedirectToPaymentGateway", ex);
                //ShowMessage("Error initializing payment gateway.", "danger");
            }
        }

        private DataTable getSubscriptionDetails(string subscriptionId)
        {
            DataTable dt = new DataTable();
            if (Session["UserId"] == null)
            {

                return dt;
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand("CALL getSubscriptionDetails(@p_SubscriptionId,@p_UserId);", conn))
            {
                cmd.Parameters.AddWithValue("@p_UserId", userId);
                cmd.Parameters.AddWithValue("@p_SubscriptionId", subscriptionId);
                conn.Open();
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(dt);
                if(dt.Rows.Count>0)
                
                {

                  


                    DataRow row = dt.Rows[0];
                    Session["FullName"] = row["FullName"].ToString();
                    Session["College_id"] = row["College_id"].ToString();
                    Session["Mobile"] = row["Mobile"].ToString();
                    Session["Email"] = row["Email"].ToString();
                    Session["TotalAmount"] = row["TotalAmount"].ToString();
                    



                }
                return dt;
            }
        }
    }
}