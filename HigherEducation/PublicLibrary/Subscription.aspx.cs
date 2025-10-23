using CCA.Util;
using HigherEducation.BusinessLayer;
using HigherEducation.DataAccess;
using HigherEducation.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class Subscription : System.Web.UI.Page
    {
        CCACrypto ccaCrypto = new CCACrypto();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is already logged in
            if (Session["UserId"] == null)
            {
                Response.Redirect("login.aspx");
            }
            bindSubs();
            if (!IsPostBack)
            {

                BindDistricts();
            }
        }

        private void BindDistricts()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand("CALL BindDistrict();", conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var districts = new List<KeyValuePair<int, string>>();
                        // Add default "Select District" option
                        districts.Add(new KeyValuePair<int, string>(0, "Select District"));
                        while (reader.Read())
                        {
                            districts.Add(new KeyValuePair<int, string>(
                                Convert.ToInt32(reader["value"]),
                                reader["text"].ToString()
                            ));
                        }

                        ddldistrict.DataSource = districts;
                        ddldistrict.DataTextField = "Value";
                        ddldistrict.DataValueField = "Key";
                        ddldistrict.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/Subscription";
                clsLogger.ExceptionMsg = "BindDistricts";
                clsLogger.SaveException();
            }
        }



        protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedDistrictCode = Convert.ToInt32(ddldistrict.SelectedValue);

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand("CALL BindGovtITIs (@p_districtcode);", conn))
                {
                    cmd.Parameters.AddWithValue("@p_districtcode", selectedDistrictCode);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var itis = new List<KeyValuePair<int, string>>();
                        while (reader.Read())
                        {
                            itis.Add(new KeyValuePair<int, string>(
                                Convert.ToInt32(reader["value"]),
                                reader["text"].ToString()
                            ));
                        }


                        ddlITI.DataSource = itis;
                        ddlITI.DataTextField = "Value";
                        ddlITI.DataValueField = "Key";
                        ddlITI.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/Subscription";
                clsLogger.ExceptionMsg = "ddldistrict_SelectedIndexChanged";
                clsLogger.SaveException();
            }
        }

        protected void btnPremiumPlan_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate mandatory parameters
                if (Session["UserId"] == null || ddldistrict.SelectedIndex == 0 ||
                    ddlITI.SelectedItem.Value == null ||
                    string.IsNullOrWhiteSpace(txtStartDate.Text))

                {
                    lblMessage.Text = "All fields are mandatory. Please fill in all required information.";
                    return;
                }

                int userId = Convert.ToInt32(Session["UserId"]);
                string email = Session["Email"].ToString();
                string mobile = Session["Mobile"].ToString();



                string subscriptionType = "ReadingWithIssue"; // Premium plan type
                decimal amount = 500; // Example premium plan amount
                DateTime startDate = DateTime.Parse(txtStartDate.Text);

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

                // Check for existing subscription in last 30 days
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(@"SELECT COUNT(*) FROM usersubscriptions 
                WHERE UserId = @p_UserId 
                AND ITIId = @p_ITIId 
                AND SubscriptionType = @p_SubscriptionType 
                AND StartDate >= DATE_SUB(@p_subStart, INTERVAL 30 DAY)", conn))
                {
                    cmd.Parameters.AddWithValue("@p_UserId", userId);
                    cmd.Parameters.AddWithValue("@p_ITIId", ddlITI.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                    cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        ShowAlert("You have already applied for this subscription type in the last 30 days.", "warning");
                        return;
                    }
                }

                // Check if maximum 20 students have already applied for this ITI in the last 30 days
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(@"SELECT COUNT(*) FROM usersubscriptions 
                WHERE ITIId = @p_ITIId 
                AND SubscriptionType = @p_SubscriptionType 
                AND StartDate >= DATE_SUB(@p_subStart, INTERVAL 30 DAY)", conn))
                {
                    cmd.Parameters.AddWithValue("@p_ITIId", ddlITI.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                    cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));

                    conn.Open();
                    int studentCount = Convert.ToInt32(cmd.ExecuteScalar());
                    if (studentCount >= 20)
                    {
                        ShowAlert("Maximum 20 students can apply for this ITI in a 30 day duration. Please try another ITI or date.", "warning");
                        return;
                    }
                }
                // Proceed with subscription
                string subscriptionId;
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("CALL sp_SubscribeUser(@p_UserId, @p_ITIId, @p_SubscriptionType, @p_Amount,@p_subStart); SELECT LAST_INSERT_ID() AS SubscriptionId;", conn))
                    {
                        cmd.Parameters.AddWithValue("@p_UserId", userId);
                        cmd.Parameters.AddWithValue("@p_ITIId", ddlITI.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                        cmd.Parameters.AddWithValue("@p_Amount", amount);
                        cmd.Parameters.AddWithValue("@p_subStart", DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd"));

                        subscriptionId = cmd.ExecuteScalar().ToString();
                    }
                    if (Convert.ToInt32(subscriptionId) > 0)
                    {

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
                            int paymentId = SavePaymentRecord(conn, transaction, Convert.ToInt32(subscriptionId), Convert.ToInt32(subscriptionId), paymentReferenceId, amount);

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
                            Session["PaymentAmount"] = amount;

                            // Step 3: Redirect to payment gateway
                            RedirectToPaymentGateway(Convert.ToInt32(subscriptionId), paymentReferenceId, amount);

                            return;
                        }
                    }
                    // Redirect to payment page with subscriptionId after entry success
                    //Response.Redirect("libraryPayment.aspx?sid=" + subscriptionId);
                }
                // Optionally, show confirmation (not needed if redirecting)

                bindSubs();
                ShowAlert("ReadingWithIssue Subscription Applied! Please Make payment now to print the pass", "success");
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/Subscription";
                clsLogger.ExceptionMsg = "btnPremiumPlan_Click";
                clsLogger.SaveException();
            }
        }

        protected void btnBasicPlan_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate mandatory parameters
                if (Session["UserId"] == null || ddldistrict.SelectedIndex == 0 ||
                    ddlITI.SelectedItem.Value == null ||
                    string.IsNullOrWhiteSpace(txtStartDate.Text))

                {
                    lblMessage.Text = "All fields are mandatory. Please fill in all required information.";
                    return;
                }

                int userId = Convert.ToInt32(Session["UserId"]);
                string email = Session["Email"]?.ToString() ?? string.Empty;
                string mobile = Session["Mobile"]?.ToString() ?? string.Empty;


                string subscriptionType = "ReadingOnly";
                decimal amount = 100;
                DateTime startDate = DateTime.Parse(txtStartDate.Text);

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

                // Check for existing subscription in last 30 days for this user
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(@"SELECT COUNT(*) FROM usersubscriptions 
                WHERE UserId = @p_UserId 
                AND ITIId = @p_ITIId 
               
                AND StartDate >= DATE_SUB(@p_subStart, INTERVAL 30 DAY)", conn))
                {
                    cmd.Parameters.AddWithValue("@p_UserId", userId);
                    cmd.Parameters.AddWithValue("@p_ITIId", ddlITI.SelectedItem.Value);
                    //cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                    cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        ShowAlert("You have already applied for this subscription type in the last 30 days.", "warning");
                        return;
                    }
                }

                // Check if maximum 20 students have already applied for this ITI in the last 30 days
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(@"SELECT COUNT(*) FROM usersubscriptions 
                WHERE ITIId = @p_ITIId 
                AND SubscriptionType = @p_SubscriptionType 
                AND StartDate >= DATE_SUB(@p_subStart, INTERVAL 30 DAY)", conn))
                {
                    cmd.Parameters.AddWithValue("@p_ITIId", ddlITI.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                    cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));

                    conn.Open();
                    int studentCount = Convert.ToInt32(cmd.ExecuteScalar());
                    if (studentCount >= 20)
                    {
                        ShowAlert("Maximum 20 students can apply for this ITI in a 30 day duration. Please try another ITI or date.", "warning");
                        return;
                    }
                }

                // Proceed with subscription
                string subscriptionId;
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("CALL sp_SubscribeUser(@p_UserId, @p_ITIId, @p_SubscriptionType, @p_Amount,@p_subStart); SELECT LAST_INSERT_ID() AS SubscriptionId;", conn))
                    {
                        cmd.Parameters.AddWithValue("@p_UserId", userId);
                        cmd.Parameters.AddWithValue("@p_ITIId", ddlITI.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                        cmd.Parameters.AddWithValue("@p_Amount", amount);
                        cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));
                        subscriptionId = cmd.ExecuteScalar().ToString();
                    }
                    if (Convert.ToInt32(subscriptionId) > 0)
                    {
                        if (Convert.ToInt32(subscriptionId) > 0)
                        {

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
                                int paymentId = SavePaymentRecord(conn, transaction, Convert.ToInt32(subscriptionId), Convert.ToInt32(subscriptionId), paymentReferenceId, amount);

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
                                Session["PaymentAmount"] = amount;

                                // Step 3: Redirect to payment gateway
                                RedirectToPaymentGateway(Convert.ToInt32(subscriptionId), paymentReferenceId, amount);

                                return;
                            }
                        }
                    }
                    // Redirect to payment page with subscriptionId after entry success
                    //Response.Redirect("libraryPayment.aspx?sid=" + subscriptionId);
                    bindSubs();
                    ShowAlert("ReadingOnly Subscription Applied! Please Make payment now to print the pass", "success");
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/Subscription";
                clsLogger.ExceptionMsg = "btnBasicPlan_Click";
                clsLogger.SaveException();
            }
        }


        private void bindSubs()
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    lblMessage.Text = "User not logged in.";
                    return;
                }

                int userId = Convert.ToInt32(Session["UserId"]);
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand("CALL userSubscriptionList(@p_UserId);", conn))
                {
                    cmd.Parameters.AddWithValue("@p_UserId", userId);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var dt = new System.Data.DataTable();
                        dt.Load(reader);



                        gvSubscriptions.DataSource = dt;
                        gvSubscriptions.DataBind();


                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/Subscription";
                clsLogger.ExceptionMsg = "bindSubs";
                clsLogger.SaveException();
            }
        }

        private int SavePaymentRecord(MySqlConnection conn, MySqlTransaction transaction, int bookingId, int slotId, string paymentReferenceId, decimal bookingAmount)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_SavePaymentRecord", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_FullName", Session["FullName"].ToString());
                    cmd.Parameters.AddWithValue("p_UserId", Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("p_PaymentReferenceID", paymentReferenceId);
                    cmd.Parameters.AddWithValue("p_College_id", Convert.ToInt32(ddlITI.SelectedValue));
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
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/Subscription";
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

        private void ShowAlert(string message, string type)
        {
            pnlAlert.Visible = true;
            lblAlertMessage.Text = message;

            switch (type.ToLower())
            {
                case "success":
                    pnlAlert.CssClass = "alert alert-success alert-custom";
                    break;
                case "warning":
                    pnlAlert.CssClass = "alert alert-warning alert-custom";
                    break;
                case "danger":
                    pnlAlert.CssClass = "alert alert-danger alert-custom";
                    break;
                default:
                    pnlAlert.CssClass = "alert alert-info alert-custom";
                    break;
            }
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
                objFeeModule.merchant_param3 = ddlITI.SelectedValue;  // ITI ID
                objFeeModule.merchant_param4 = ddldistrict.SelectedValue;  // District ID
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
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/Subscription";
                clsLogger.ExceptionMsg = "RedirectToPaymentGateway";
                clsLogger.SaveException();
            }
        }


    }
}