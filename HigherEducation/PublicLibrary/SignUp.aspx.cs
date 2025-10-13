using HigherEducation.BAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Razor.Tokenizer;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class SignUp : System.Web.UI.Page
    {
        MySqlConnection msqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is already logged in
                if (Session["UserId"] != null)
                {
                    Response.Redirect("Home.aspx");
                }

                // Clear previous OTP session
                Session.Remove("OTP");
                Session.Remove("OTPVerified");
                Session.Remove("OTPMobile");
            }
        }

        // MD5 Encryption Method
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

        // Send OTP to Mobile (Simulated - Integrate with SMS Gateway in production)
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
                // Log OTP for testing (remove in production)
                System.Diagnostics.Debug.WriteLine($"OTP for {mobile}: {otp}");

                return true;
            }
            catch (Exception ex)
            {
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

            // Check if mobile already exists
            if (IsMobileExists(mobile))
            {
                ShowAlert("This mobile number is already registered. Please use a different number or login.", "warning");
                return;
            }

            string otp = GenerateOTP();

            if (SendOTP(mobile, otp))
            {
                pnlOTP.Visible = true;
                ShowAlert($"OTP has been sent to your mobile number ending with {mobile.Substring(6)}", "success");

                // Enable OTP verification
                btnVerifyOTP.Enabled = true;
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
                ShowAlert("Mobile number verified successfully!", "success");
                pnlOTP.Visible = false;
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
            }
        }

        // Check if Mobile Already Exists
        private bool IsMobileExists(string mobile)
        {
            try
            {
                using (MySqlConnection conn = msqlConn)
                {
                    string query = "SELECT COUNT(*) FROM LibraryUsers WHERE Mobile = @Mobile";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Mobile", mobile);
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking mobile: {ex.Message}");
                return false;
            }
        }

        // Check if Email Already Exists
        private bool IsEmailExists(string email)
        {
            try
            {
                using (MySqlConnection conn = msqlConn)
                {
                    string query = "SELECT COUNT(*) FROM LibraryUsers WHERE Email = @Email";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking email: {ex.Message}");
                return false;
            }
        }

        // Signup Button Click
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                string fullName = txtFullName.Text.Trim();
                string mobile = txtMobile.Text.Trim();
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();
                string confirmPassword = txtConfirmPassword.Text.Trim();

                // Validation
                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(mobile) ||
                    string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowAlert("Please fill all required fields.", "warning");
                    return;
                }

                if (password != confirmPassword)
                {
                    ShowAlert("Passwords do not match.", "warning");
                    return;
                }

                if (password.Length < 6)
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

                // Check if email already exists
                if (IsEmailExists(email))
                {
                    ShowAlert("This email address is already registered. Please use a different email or login.", "warning");
                    return;
                }

                // Hash password
                string hashedPassword = MD5Hash(password);

                // Create user using stored procedure
                var parameters = new[]
                {
                    new MySqlParameter("p_Mobile", mobile),
                    new MySqlParameter("p_Email", email),
                    new MySqlParameter("p_Password", hashedPassword),
                    new MySqlParameter("p_FullName", fullName)
                };

                ExecuteStoredProcedure("sp_UserSignUp", parameters);

                ShowAlert("Registration successful! Please login with your credentials.", "success");

                // Clear form
                ClearSignupForm();

                // Switch to login tab
                ScriptManager.RegisterStartupScript(this, this.GetType(), "switchToLogin",
                    "document.getElementById('login-tab').click();", true);
            }
            catch (Exception ex)
            {
                ShowAlert($"Registration failed: {ex.Message}", "danger");
            }
        }

       
        // Clear Signup Form
        private void ClearSignupForm()
        {
            txtFullName.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtOTP.Text = "";
            pnlOTP.Visible = false;


            Session.Remove("OTP");
            Session.Remove("OTPVerified");
            Session.Remove("OTPMobile");
        }
    

    
        public DataTable ExecuteStoredProcedure(string procedureName, params MySqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = msqlConn)
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

        public int ExecuteNonQuerySP(string procedureName, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = msqlConn)
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
    }
    
}