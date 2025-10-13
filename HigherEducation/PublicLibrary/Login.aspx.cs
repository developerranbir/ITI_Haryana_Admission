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
    public partial class Login : System.Web.UI.Page
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
        // Login Button Click
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string loginId = txtLoginId.Text.Trim();
                string password = txtLoginPassword.Text.Trim();

                if (string.IsNullOrEmpty(loginId) || string.IsNullOrEmpty(password))
                {
                    ShowAlert("Please enter both login ID and password.", "warning");
                    return;
                }

                // Hash password
                string hashedPassword = MD5Hash(password);

                // Authenticate user using stored procedure
                var parameters = new[]
                {
                    new MySqlParameter("p_LoginId", loginId),
                    new MySqlParameter("p_Password", hashedPassword)
                };

                var result = ExecuteStoredProcedure("sp_UserLogin", parameters);

                if (result.Rows.Count > 0)
                {
                    DataRow user = result.Rows[0];

                    // Create session
                    Session["UserId"] = user["UserId"];
                    Session["FullName"] = user["FullName"];
                    Session["Mobile"] = user["Mobile"];
                    Session["Email"] = user["Email"];

                    

                    // Redirect to home page
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ShowAlert("Invalid login credentials. Please try again.", "danger");
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Login failed: {ex.Message}", "danger");
            }
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