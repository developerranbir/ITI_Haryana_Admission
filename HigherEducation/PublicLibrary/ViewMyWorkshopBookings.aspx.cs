using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class ViewMyWorkshopBookings : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

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
                    ShowError($"Error loading bookings: {ex.Message}");
                }
            }
        }

        protected void gvBookings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBookings.PageIndex = e.NewPageIndex;
            BindBookings();
        }

        protected void gvBookings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int bookingId = Convert.ToInt32(e.CommandArgument);
                ViewBookingDetails(bookingId);
            }
            else if (e.CommandName == "CheckPayment")
            {
                int bookingId = Convert.ToInt32(e.CommandArgument);
                CheckPaymentStatus(bookingId);
            }
        }

        private void ViewBookingDetails(int bookingId)
        {
            Session["LastBookingID"] = bookingId;
            Response.Redirect($"WorkShopBookingConfirmation.aspx");
        }

        private void CheckPaymentStatus(int bookingId)
        {
            Session["PendingPaymentBookingID"] = bookingId;
            Response.Redirect($"PaymentStatus.aspx?BookingID=" + bookingId);
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
                default: return "secondary";
            }
        }

        private void UpdatePagerInfo()
        {
            int currentPage = gvBookings.PageIndex + 1;
            int totalPages = gvBookings.PageCount;
            int pageSize = gvBookings.PageSize;
            int startRecord = (gvBookings.PageIndex * pageSize) + 1;
            int endRecord = Math.Min((gvBookings.PageIndex + 1) * pageSize, GetTotalRecordCount());

            lblPageInfo.Text = $"Showing {startRecord} to {endRecord} of {GetTotalRecordCount()} entries (Page {currentPage} of {totalPages})";
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
    }
}