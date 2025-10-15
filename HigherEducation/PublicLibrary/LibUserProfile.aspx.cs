using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace HigherEducation.PublicLibrary
{
    public partial class LibUserProfile : System.Web.UI.Page
    {
        MySqlConnection msqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                LoadUserProfile();
                LoadUserStatistics();
            }
        }

        // Load User Profile Information
        private void LoadUserProfile()
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserId"]);

                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = @"SELECT UserId, FullName, Mobile, Email, CreatedDate 
                                   FROM LibraryUsers 
                                   WHERE UserId = @UserId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                litUserId.Text = reader["UserId"].ToString();
                                litFullName.Text = reader["FullName"].ToString();
                                litMobile.Text = reader["Mobile"].ToString();
                                litEmail.Text = reader["Email"].ToString();
                                litUserName.Text = reader["FullName"].ToString();

                                DateTime createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                                litMemberSince.Text = createdDate.ToString("MMMM dd, yyyy");
                            }
                            else
                            {
                                ShowAlert("User profile not found.", "danger");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Error loading profile: {ex.Message}", "danger");
            }
        }

        // Load User Statistics
        private void LoadUserStatistics()
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserId"]);

                // Total Library Subscriptions
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = @"SELECT COUNT(*) as TotalSubscriptions 
                                   FROM LibrarySubscriptions 
                                   WHERE UserId = @UserId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        litTotalSubscriptions.Text = cmd.ExecuteScalar().ToString();
                    }
                }

                // Workshop Bookings Count
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = @"SELECT COUNT(*) as WorkshopBookings 
                                   FROM WorkshopBookings 
                                   WHERE UserId = @UserId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        litWorkshopBookings.Text = cmd.ExecuteScalar().ToString();
                    }
                }

                // Active Subscriptions
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = @"SELECT COUNT(*) as ActiveSubscriptions 
                                   FROM LibrarySubscriptions 
                                   WHERE UserId = @UserId AND EndDate >= CURDATE()";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        litActiveSubscriptions.Text = cmd.ExecuteScalar().ToString();
                    }
                }

                // Total Amount Spent
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = @"SELECT COALESCE(SUM(Amount), 0) as TotalSpent 
                                   FROM (
                                       SELECT Amount FROM LibrarySubscriptions WHERE UserId = @UserId AND PaymentStatus = 'Completed'
                                       UNION ALL
                                       SELECT Amount FROM WorkshopBookings WHERE UserId = @UserId AND PaymentStatus = 'Completed'
                                   ) as CombinedPayments";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        decimal totalSpent = Convert.ToDecimal(cmd.ExecuteScalar());
                        litTotalSpent.Text = $"₹{totalSpent}";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading statistics: {ex.Message}");
                // Don't show error to user for statistics, just use default values
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

        // Verify Current Password
        private bool VerifyCurrentPassword(int userId, string currentPassword)
        {
            try
            {
                string hashedCurrentPassword = MD5Hash(currentPassword);

                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = "SELECT COUNT(*) FROM LibraryUsers WHERE UserId = @UserId AND Password = @Password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Password", hashedCurrentPassword);
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Error verifying current password: {ex.Message}", "danger");
                return false;
            }
        }

        // Update Password
        private bool UpdatePassword(int userId, string newPassword)
        {
            try
            {
                string hashedNewPassword = MD5Hash(newPassword);

                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString))
                {
                    string query = "UPDATE LibraryUsers SET Password = @Password WHERE UserId = @UserId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", hashedNewPassword);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Error updating password: {ex.Message}", "danger");
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

        // Change Password Button Click
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string currentPassword = txtCurrentPassword.Text.Trim();
                string newPassword = txtNewPassword.Text.Trim();
                string confirmPassword = txtConfirmPassword.Text.Trim();

                // Validation
                if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
                {
                    ShowAlert("Please fill all password fields.", "warning");
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    ShowAlert("New password and confirm password do not match.", "warning");
                    return;
                }

                if (newPassword.Length < 6)
                {
                    ShowAlert("New password must be at least 6 characters long.", "warning");
                    return;
                }

                int userId = Convert.ToInt32(Session["UserId"]);

                // Verify current password
                if (!VerifyCurrentPassword(userId, currentPassword))
                {
                    ShowAlert("Current password is incorrect.", "danger");
                    return;
                }

                // Update password
                if (UpdatePassword(userId, newPassword))
                {
                    ShowAlert("Password changed successfully!", "success");
                    ClearPasswordFields();
                }
                else
                {
                    ShowAlert("Failed to change password. Please try again.", "danger");
                }
            }
            catch (Exception ex)
            {
                ShowAlert($"Password change failed: {ex.Message}", "danger");
            }
        }

        // Cancel Button Click
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPasswordFields();
            ShowAlert("Password change cancelled.", "info");
        }

        // Clear Password Fields
        private void ClearPasswordFields()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
    }
}