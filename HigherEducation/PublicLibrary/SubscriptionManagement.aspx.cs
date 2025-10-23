using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;

namespace HigherEducation.PublicLibrary
{
    public partial class SubscriptionManagement : System.Web.UI.Page
    {

        public class Subscription
        {
            public int SubscriptionId { get; set; }
            public int UserId { get; set; }
            public int ITIId { get; set; }
            public string SubscriptionType { get; set; }
            public decimal Amount { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime PaymentDate { get; set; }
            public string PaymentStatus { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set ITI ID from session (you'll need to set this during login)
                int itiId = GetITIIdFromSession();
                //lblITIId.Text = itiId.ToString();
                if (itiId == 0) 
                {

                    Response.Redirect("~/DHE/frmlogin.aspx"); 
                }
                // Load initial data
                LoadSubscriptions("active");
            }
        }






        // Subscription model class



        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubscriptions(ddlStatus.SelectedValue);
        }



        private void LoadSubscriptions(string status)
        {
            try
            {
                int itiId = GetITIIdFromSession();
                var subscriptions = GetSubscriptionsFromDatabase(itiId, status);

                gvSubscriptions.DataSource = subscriptions;
                gvSubscriptions.DataBind();
            }
            catch (Exception ex)
            {
                // Handle error - you can show an alert or log the error
                Response.Write($"<script>alert('Error loading data: {ex.Message}');</script>");
            }
        }

        private List<Subscription> GetSubscriptionsFromDatabase(int itiId, string status)
        {
            var subscriptions = new List<Subscription>();
            string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetSubscriptionsByStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_ITIId", itiId);
                    command.Parameters.AddWithValue("@p_Status", status);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subscriptions.Add(new Subscription
                            {
                                SubscriptionId = reader.GetInt32("SubscriptionId"),
                                UserId = reader.GetInt32("UserId"),
                                ITIId = reader.GetInt32("ITIId"),
                                SubscriptionType = reader.GetString("SubscriptionType"),
                                Amount = reader.GetDecimal("Amount"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                PaymentDate = reader.GetDateTime("PaymentDate"),
                                PaymentStatus = reader.GetString("PaymentStatus"),
                                UserName = reader.GetString("FullName"),
                                Email = reader.GetString("Email")
                            });
                        }
                    }
                }
            }

            return subscriptions;
        }


        private int GetITIIdFromSession()
        {
            // Get ITI ID from session - you need to set this during login
            if (Session["CollegeId"] != null)
            {
                return Convert.ToInt32(Session["CollegeId"]);
            }
            else
            {
                return 0;
            }
        }

        // Helper method for status badge styling
        public string GetStatusBadgeClass(string status)
        {
            switch (status.ToLower())
            {
                case "completed":
                    return "status-badge-completed";
                case "pending":
                    return "status-badge-pending";
                case "failed":
                    return "status-badge-failed";
                default:
                    return "bg-secondary";
            }
        }
    }
}
