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
                        Response.Redirect("Login.aspx"); // Redirect to login page
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
                        string query = @"
                        SELECT 
                            BookingID, FullName, Email, MobileNumber, District, ITI_Name,
                            WorkshopDate, WorkshopTime, WorkshopDuration, BookingAmount,
                            PaymentStatus, CreatedAt, UserId
                        FROM WorkshopBookings 
                        WHERE UserId = @UserId";

                        // Build WHERE clause based on filters
                        string whereClause = BuildWhereClause();
                        query += whereClause;
                        query += " ORDER BY CreatedAt DESC";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            // Always filter by logged-in user
                            cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                            AddParameters(cmd);

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

            private string BuildWhereClause()
            {
                string whereClause = "";

                // Search text
                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    whereClause += " AND (District LIKE @Search OR ITI_Name LIKE @Search)";
                }

                // Status filter
                if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
                {
                    whereClause += " AND BookingStatus = @Status";
                }

                return whereClause;
            }

            private void AddParameters(MySqlCommand cmd)
            {
                // Search parameter
                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@Search", $"%{txtSearch.Text.Trim()}%");
                }

                // Status parameter
                if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
                {
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                }
            }

            protected void gvBookings_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                gvBookings.PageIndex = e.NewPageIndex;
                BindBookings();
            }

            protected void gvBookings_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Add CSS class based on booking status
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    string status = rowView["BookingStatus"].ToString();
                    e.Row.CssClass = $"status-{status.ToLower()}";
                }
            }

            protected void gvBookings_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "ViewDetails")
                {
                    int bookingId = Convert.ToInt32(e.CommandArgument);
                    ViewBookingDetails(bookingId);
                }
                else if (e.CommandName == "CancelBooking")
                {
                    int bookingId = Convert.ToInt32(e.CommandArgument);
                    CancelBooking(bookingId);
                }
            }

            private void ViewBookingDetails(int bookingId)
            {
                // Redirect to booking details page
                Response.Redirect($"BookingDetails.aspx?BookingID={bookingId}");
            }

            private void CancelBooking(int bookingId)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Start transaction
                        using (MySqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // Check if booking belongs to current user
                                string checkQuery = "SELECT SlotID FROM WorkshopBookings WHERE BookingID = @BookingID AND UserId = @UserId";
                                int slotId = 0;

                                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn, transaction))
                                {
                                    checkCmd.Parameters.AddWithValue("@BookingID", bookingId);
                                    checkCmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());

                                    object result = checkCmd.ExecuteScalar();
                                    if (result == null)
                                    {
                                        ShowError("Booking not found or you don't have permission to cancel this booking.");
                                        return;
                                    }
                                    slotId = Convert.ToInt32(result);
                                }

                                // Update booking status to Cancelled
                                string updateBookingQuery = "UPDATE WorkshopBookings SET BookingStatus = 'Cancelled' WHERE BookingID = @BookingID";
                                using (MySqlCommand updateCmd = new MySqlCommand(updateBookingQuery, conn, transaction))
                                {
                                    updateCmd.Parameters.AddWithValue("@BookingID", bookingId);
                                    updateCmd.ExecuteNonQuery();
                                }

                                // Update payment status to Refunded (if paid)
                                string updatePaymentQuery = @"
                                UPDATE WorkshopPayments 
                                SET PaymentStatus = 'Refunded' 
                                WHERE BookingID = @BookingID AND PaymentStatus = 'Completed'";

                                using (MySqlCommand paymentCmd = new MySqlCommand(updatePaymentQuery, conn, transaction))
                                {
                                    paymentCmd.Parameters.AddWithValue("@BookingID", bookingId);
                                    paymentCmd.ExecuteNonQuery();
                                }

                                // Restore available seat
                                string updateSlotQuery = "UPDATE WorkshopSlots SET AvailableSeats = AvailableSeats + 1 WHERE SlotID = @SlotID";
                                using (MySqlCommand slotCmd = new MySqlCommand(updateSlotQuery, conn, transaction))
                                {
                                    slotCmd.Parameters.AddWithValue("@SlotID", slotId);
                                    slotCmd.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                ShowSuccess("Booking cancelled successfully!");
                                BindBookings();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw new Exception($"Error cancelling booking: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Error cancelling booking: {ex.Message}");
                    }
                }
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

            // Event handlers for filters
            protected void txtSearch_TextChanged(object sender, EventArgs e)
            {
                gvBookings.PageIndex = 0;
                BindBookings();
            }

            protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
            {
                gvBookings.PageIndex = 0;
                BindBookings();
            }

            // Export to Excel
            protected void btnExport_Click(object sender, EventArgs e)
            {
                ExportToExcel();
            }

            private void ExportToExcel()
            {
                try
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=MyWorkshopBookings.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";

                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    // Create a temporary GridView for export
                    GridView gvExport = new GridView();
                    gvExport.DataSource = GetDataForExport();
                    gvExport.DataBind();

                    // Apply styles
                    gvExport.HeaderStyle.BackColor = System.Drawing.Color.LightBlue;
                    gvExport.RowStyle.BorderStyle = BorderStyle.Solid;
                    gvExport.RowStyle.BorderWidth = 1;

                    gvExport.RenderControl(hw);

                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                catch (Exception ex)
                {
                    ShowError($"Error exporting to Excel: {ex.Message}");
                }
            }

            private DataTable GetDataForExport()
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                    SELECT 
                        BookingID, FullName, Email, MobileNumber, District, ITI_Name,
                        DATE_FORMAT(WorkshopDate, '%d-%m-%Y') as WorkshopDate,
                        TIME_FORMAT(WorkshopTime, '%H:%i') as WorkshopTime,
                        WorkshopDuration, BookingAmount, BookingStatus, PaymentStatus,
                        DATE_FORMAT(CreatedAt, '%d-%m-%Y %H:%i') as CreatedAt
                    FROM WorkshopBookings 
                    WHERE UserId = @UserId";

                    string whereClause = BuildWhereClause();
                    query += whereClause + " ORDER BY CreatedAt DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                        AddParameters(cmd);

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        return dt;
                    }
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
                    string query = "SELECT COUNT(*) FROM WorkshopBookings WHERE UserId = @UserId";
                    string whereClause = BuildWhereClause();
                    query += whereClause;

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                        AddParameters(cmd);
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

            private void ShowSuccess(string message)
            {
                string cleanMessage = message.Replace("'", "\\'");
                string script = $@"alert('Success: {cleanMessage}');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }
    }