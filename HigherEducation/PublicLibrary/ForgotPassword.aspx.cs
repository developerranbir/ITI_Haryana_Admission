using HigherEducation.BAL;
using HigherEducation.BusinessLayer;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace HigherEducation.PublicLibrary
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        MySqlConnection msqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear previous OTP session
                Session.Remove("OTP");
                Session.Remove("OTPVerified");
                Session.Remove("OTPMobile");
                Session.Remove("ResetUserID");

                // Ensure mobile field is clear
                txtMobile.Text = "";
            }
        }

        // MD5 Encryption Method (same as SignUp)
        private string MD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // Generate Random OTP
        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        // Send OTP to Mobile
        private bool SendOTP(string mobile, string otp)
        {
            try
            {
                // Store OTP in session for verification
                Session["OTP"] = otp;
                Session["OTPMobile"] = mobile;
                Session["OTPCreatedTime"] = DateTime.Now;

                string sms_details = "Your ITI application OTP Number is " + otp;
                AgriSMS.sendSingleSMS(mobile, sms_details, "1007030482147904866");

                return true;
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ForgotPassword";
                clsLogger.ExceptionMsg = "SendOTP";
                clsLogger.SaveException();
                ShowAlert($"Failed to send OTP: {ex.Message}", "danger");
                return false;
            }
        }

        // Verify OTP
        private bool VerifyOTP(string enteredOTP)
        {
            try
            {
                string storedOTP = Session["OTP"] as string;
                DateTime otpCreatedTime = Session["OTPCreatedTime"] != null ? (DateTime)Session["OTPCreatedTime"] : DateTime.MinValue;

                // Check if OTP exists and is not expired (5 minutes)
                if (string.IsNullOrEmpty(storedOTP) || (DateTime.Now - otpCreatedTime).TotalMinutes > 5)
                {
                    ShowAlert("OTP has expired. Please request a new one.", "warning");
                    return false;
                }

                if (storedOTP == enteredOTP)
                {
                    Session["OTPVerified"] = true;
                    Session.Remove("OTP");
                    Session.Remove("OTPCreatedTime");
                    return true;
                }
                else
                {
                    ShowAlert("Invalid OTP. Please try again.", "danger");
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ForgotPassword";
                clsLogger.ExceptionMsg = "VerifyOTP";
                clsLogger.SaveException();
                ShowAlert($"OTP verification failed: {ex.Message}", "danger");
                return false;
            }
        }

        // Show Alert Message
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

            // Register script to update UI elements
            ScriptManager.RegisterStartupScript(this, GetType(), "UpdateUI", "updateUIAfterPostback();", true);
        }

        // Check if Mobile Exists and Get User ID
        private int? GetUserIdByMobile(string mobile)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = "SELECT UserId FROM LibraryUsers WHERE Mobile = @Mobile";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Mobile", mobile);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : (int?)null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ForgotPassword";
                clsLogger.ExceptionMsg = "GetUserIdByMobile";
                clsLogger.SaveException();
                return null;
            }
        }

        // Update Password in Database
        private bool UpdatePassword(int userId, string newPassword)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = "UPDATE LibraryUsers SET Password = @Password WHERE UserId = @UserId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ForgotPassword";
                clsLogger.ExceptionMsg = "UpdatePassword";
                clsLogger.SaveException();
                return false;
            }
        }

        // Send OTP Button Click
        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            string mobile = txtMobile.Text.Trim();

            if (string.IsNullOrEmpty(mobile) || mobile.Length != 10)
            {
                ShowAlert("Please enter a valid 10-digit mobile number.", "warning");
                return;
            }

            // Check if mobile exists in database
            int? userId = GetUserIdByMobile(mobile);
            if (!userId.HasValue)
            {
                ShowAlert("This mobile number is not registered. Please check the number or sign up for a new account.", "warning");
                return;
            }

            // Store user ID for password update
            Session["ResetUserID"] = userId.Value;

            string otp = GenerateOTP();

            if (SendOTP(mobile, otp))
            {
                pnlOTP.Visible = true;
                ShowAlert($"OTP has been sent to your mobile number ending with {mobile.Substring(6)}", "success");

                // Enable OTP verification
                btnVerifyOTP.Enabled = true;

                // Register script to show OTP section
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowOTPSection", "showOTPSection();", true);
            }
        }

        // Verify OTP Button Click
        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            string enteredOTP = txtOTP.Text.Trim();

            if (string.IsNullOrEmpty(enteredOTP) || enteredOTP.Length != 6)
            {
                ShowAlert("Please enter a valid 6-digit OTP.", "warning");
                return;
            }

            if (VerifyOTP(enteredOTP))
            {
                ShowAlert("Mobile number verified successfully! You can now set your new password.", "success");
                pnlOTP.Visible = false;

                // Register script to show password section
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPasswordSection", "showPasswordSection();", true);
            }
        }

        // Resend OTP Button Click
        protected void btnResendOTP_Click(object sender, EventArgs e)
        {
            string mobile = Session["OTPMobile"] as string;

            if (string.IsNullOrEmpty(mobile))
            {
                mobile = txtMobile.Text.Trim();
            }

            string otp = GenerateOTP();

            if (SendOTP(mobile, otp))
            {
                ShowAlert("New OTP has been sent to your mobile number.", "success");

                // Reset timer
                ScriptManager.RegisterStartupScript(this, GetType(), "ResetOTPTimer", "resetOTPTimer();", true);
            }
        }

        // Reset Password Button Click
        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string newPassword = txtNewPassword.Text.Trim();
                string confirmPassword = txtConfirmPassword.Text.Trim();

                // Validation
                if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
                {
                    ShowAlert("Please fill all password fields.", "warning");
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    ShowAlert("Passwords do not match.", "warning");
                    return;
                }

                if (newPassword.Length < 6)
                {
                    ShowAlert("Password must be at least 6 characters long.", "warning");
                    return;
                }

                // Check OTP verification
                if (Session["OTPVerified"] == null || !(bool)Session["OTPVerified"])
                {
                    ShowAlert("Please verify your mobile number with OTP first.", "warning");
                    return;
                }

                // Check if user ID exists in session
                if (Session["ResetUserID"] == null)
                {
                    ShowAlert("Session expired. Please start the password reset process again.", "warning");
                    return;
                }

                int userId = (int)Session["ResetUserID"];

                // Hash new password
                string hashedPassword = MD5Hash(newPassword);

                // Update password in database
                if (UpdatePassword(userId, hashedPassword))
                {
                    ShowAlert("Password reset successfully! You can now login with your new password.", "success");

                    // Clear sessions and form
                    ClearFormAndSessions();

                    // Add delayed redirect to login page (5 seconds)
                    string script = "<script>setTimeout(function(){ window.location.href = 'Login.aspx'; }, 5000);</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectToLogin", script);
                }
                else
                {
                    ShowAlert("Failed to reset password. Please try again.", "danger");
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Password reset failed: {ex.Message}", "danger");
            }
        }

        // Clear Form and Sessions
        private void ClearFormAndSessions()
        {
            txtMobile.Text = "";
            txtOTP.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";

            pnlOTP.Visible = false;

            Session.Remove("OTP");
            Session.Remove("OTPVerified");
            Session.Remove("OTPMobile");
            Session.Remove("ResetUserID");
        }

        // Execute Stored Procedure (if needed for future enhancements)
        public DataTable ExecuteStoredProcedure(string procedureName, params MySqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);

                        conn.Open();
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ForgotPassword";
                clsLogger.ExceptionMsg = "ExecuteStoredProcedure";
                clsLogger.SaveException();
                return dt;
            }
        }

        public int ExecuteNonQuerySP(string procedureName, params MySqlParameter[] parameters)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ForgotPassword";
                clsLogger.ExceptionMsg = "ExecuteNonQuerySP";
                clsLogger.SaveException();
                return 0;
            }
        }
    }
}