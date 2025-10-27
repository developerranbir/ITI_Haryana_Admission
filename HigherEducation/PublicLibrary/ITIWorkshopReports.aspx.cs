using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Text;
using HigherEducation.BusinessLayer;
using HigherEducation.Models;

namespace HigherEducation.PublicLibrary
{
    public partial class ITIWorkshopReports : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        private string ITI_Id = "0";
        private string ITI_Name = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Session handling - uncomment when ready
           
            if (Session["Collegeid"] != null && Session["UserName"] != null)
            {
                ITI_Id = Session["Collegeid"].ToString();
                ITI_Name = Session["UserName"].ToString();
                //litITIId.Text = ITI_Name;
                litUserName.Text = ITI_Name;
            }
            else
            {
                Response.Redirect("Login.aspx");
                return;
            }
           

            //// Temporary hardcoded values for testing
            //ITI_Id = "2";
            //ITI_Name = "GITI Ambala City";
            //litITIName.Text = ITI_Name;
            

            if (!IsPostBack)
            {
                InitializePage();
                BindWorkshopSlots();
                DisplayUserInfo();
            }
        }

        private void InitializePage()
        {
            // Set default dates
            txtReportDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtFromDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtMonth.Text = DateTime.Today.ToString("yyyy-MM");

            // Show daily report panel by default
            pnlDailyDate.Visible = true;
            pnlDateRange.Visible = false;
            pnlMonthly.Visible = false;

            // Hide result panels initially
            pnlSummary.Visible = false;
            pnlExport.Visible = false;
            pnlGrid.Visible = false;
            pnlNoData.Visible = false;
        }

        private void DisplayUserInfo()
        {
            // Set current date and time
            litCurrentDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            litCurrentTime.Text = DateTime.Now.ToString("hh:mm tt");
            litFooterDate.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
        }

        private void BindWorkshopSlots()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Get slots for the selected date (default to today)
                    DateTime selectedDate = DateTime.Parse(txtReportDate.Text);

                    using (MySqlCommand cmd = new MySqlCommand("sp_GetWorkshopSlotsByDate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_ITI_Id", ITI_Id);
                        cmd.Parameters.AddWithValue("p_SelectedDate", selectedDate);

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            ddlWorkshopSlot.DataSource = dt;
                            ddlWorkshopSlot.DataTextField = "SlotTime";
                            ddlWorkshopSlot.DataValueField = "ID";
                            ddlWorkshopSlot.DataBind();

                            // Add "All Slots" option
                            ddlWorkshopSlot.Items.Insert(0, new ListItem("All Slots", ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ITIWorkshopReports";
                clsLogger.ExceptionMsg = "BindWorkshopSlots";
                clsLogger.SaveException();
                ShowSweetAlert("Error", "Error loading workshop slots.", "error");
            }
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlReportType.SelectedValue)
            {
                case "DAILY":
                    pnlDailyDate.Visible = true;
                    pnlDateRange.Visible = false;
                    pnlMonthly.Visible = false;
                    break;
                case "RANGE":
                    pnlDailyDate.Visible = false;
                    pnlDateRange.Visible = true;
                    pnlMonthly.Visible = false;
                    break;
                case "MONTHLY":
                    pnlDailyDate.Visible = false;
                    pnlDateRange.Visible = false;
                    pnlMonthly.Visible = true;
                    break;
            }
            upFilter.Update();
        }

        protected void txtReportDate_TextChanged(object sender, EventArgs e)
        {
            // Refresh slots when date changes
            BindWorkshopSlots();
            upFilter.Update();
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (!ValidateDates())
                return;

            try
            {
                DataTable reportData = GetReportData();
                if (reportData.Rows.Count > 0)
                {
                    BindReportGrid(reportData);
                    UpdateSummary(reportData);
                    pnlExport.Visible = true;
                    pnlNoData.Visible = false;
                    pnlGrid.Visible = true;

                    ShowSweetAlert("Report Generated", $"Found {reportData.Rows.Count} bookings matching your criteria.", "success");
                }
                else
                {
                    pnlGrid.Visible = false;
                    pnlNoData.Visible = true;
                    pnlExport.Visible = false;
                    pnlSummary.Visible = false;
                    ShowSweetAlert("No Data", "No bookings found for the selected criteria.", "info");
                }

                upReports.Update();
                upSummary.Update();
                upExport.Update();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ITIWorkshopReports";
                clsLogger.ExceptionMsg = "btnGenerateReport_Click";
                clsLogger.SaveException();
                ShowSweetAlert("Error", "Error generating report. Please try again.", "error");
            }
        }

        private bool ValidateDates()
        {
            if (ddlReportType.SelectedValue == "RANGE")
            {
                if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
                {
                    ShowSweetAlert("Validation Error", "Please select both from and to dates.", "warning");
                    return false;
                }

                DateTime fromDate = DateTime.Parse(txtFromDate.Text);
                DateTime toDate = DateTime.Parse(txtToDate.Text);

                if (fromDate > toDate)
                {
                    ShowSweetAlert("Validation Error", "From date cannot be greater than to date.", "warning");
                    return false;
                }

                if ((toDate - fromDate).Days > 365)
                {
                    ShowSweetAlert("Validation Error", "Date range cannot exceed 365 days.", "warning");
                    return false;
                }
            }
            else if (ddlReportType.SelectedValue == "DAILY" && string.IsNullOrEmpty(txtReportDate.Text))
            {
                ShowSweetAlert("Validation Error", "Please select a date.", "warning");
                return false;
            }
            else if (ddlReportType.SelectedValue == "MONTHLY" && string.IsNullOrEmpty(txtMonth.Text))
            {
                ShowSweetAlert("Validation Error", "Please select a month.", "warning");
                return false;
            }

            return true;
        }

        private DataTable GetReportData()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Using stored procedure for report data
                using (MySqlCommand cmd = new MySqlCommand("sp_GetWorkshopBookingsReport", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Required parameters
                    cmd.Parameters.AddWithValue("p_ITI_Id", ITI_Id);
                    cmd.Parameters.AddWithValue("p_ReportType", ddlReportType.SelectedValue);

                    // Date parameters based on report type
                    switch (ddlReportType.SelectedValue)
                    {
                        case "DAILY":
                            cmd.Parameters.AddWithValue("p_ReportDate", DateTime.Parse(txtReportDate.Text));
                            cmd.Parameters.AddWithValue("p_FromDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_ToDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_Month", DBNull.Value);
                            break;

                        case "RANGE":
                            cmd.Parameters.AddWithValue("p_ReportDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_FromDate", DateTime.Parse(txtFromDate.Text));
                            cmd.Parameters.AddWithValue("p_ToDate", DateTime.Parse(txtToDate.Text));
                            cmd.Parameters.AddWithValue("p_Month", DBNull.Value);
                            break;

                        case "MONTHLY":
                            cmd.Parameters.AddWithValue("p_ReportDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_FromDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_ToDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_Month", txtMonth.Text);
                            break;

                        default:
                            cmd.Parameters.AddWithValue("p_ReportDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_FromDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_ToDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("p_Month", DBNull.Value);
                            break;
                    }

                    // Optional filters
                    if (!string.IsNullOrEmpty(ddlBookingStatus.SelectedValue))
                    {
                        cmd.Parameters.AddWithValue("p_BookingStatus", ddlBookingStatus.SelectedValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("p_BookingStatus", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(ddlWorkshopSlot.SelectedValue))
                    {
                        cmd.Parameters.AddWithValue("p_SlotID", Convert.ToInt32(ddlWorkshopSlot.SelectedValue));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("p_SlotID", 0);
                    }

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        protected void BindReportGrid(DataTable data)
        {
            try
            {
                

                gvBookings.DataSource = data;
                gvBookings.DataBind();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ITIWorkshopReports";
                clsLogger.ExceptionMsg = "BindReportGrid";
                clsLogger.SaveException();
                throw;
            }
        }

        private void UpdateSummary(DataTable data)
        {
            int totalBookings = data.Rows.Count;
            decimal totalAmount = 0;

            foreach (DataRow row in data.Rows)
            {
                if (row["BookingAmount"] != DBNull.Value)
                {
                    totalAmount += Convert.ToDecimal(row["BookingAmount"]);
                }
            }

            litSummaryTotal.Text = totalBookings.ToString("N0");
            litSummaryAmount.Text = totalAmount.ToString("N0");

            // Set date range text
            switch (ddlReportType.SelectedValue)
            {
                case "DAILY":
                    litSummaryDateRange.Text = DateTime.Parse(txtReportDate.Text).ToString("dd-MMM-yyyy");
                    break;
                case "RANGE":
                    litSummaryDateRange.Text = $"{DateTime.Parse(txtFromDate.Text):dd-MMM-yyyy} to {DateTime.Parse(txtToDate.Text):dd-MMM-yyyy}";
                    break;
                case "MONTHLY":
                    DateTime month = DateTime.Parse(txtMonth.Text + "-01");
                    litSummaryDateRange.Text = month.ToString("MMMM yyyy");
                    break;
            }

            pnlSummary.Visible = true;
        }

        protected void gvBookings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    // Safe data binding for status labels
                    DataRowView rowView = (DataRowView)e.Row.DataItem;

                    // Handle Booking Status
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    if (lblStatus != null)
                    {
                        string status = rowView["BookingStatus"]?.ToString() ?? "Pending";
                        lblStatus.Text = status;
                        lblStatus.CssClass = GetStatusClass(status);
                    }

                    // Handle Payment Status
                    Label lblPaymentStatus = (Label)e.Row.FindControl("lblPaymentStatus");
                    if (lblPaymentStatus != null)
                    {
                        string paymentStatus = rowView["PaymentStatus"]?.ToString() ?? "Pending";
                        lblPaymentStatus.Text = paymentStatus;
                        lblPaymentStatus.CssClass = GetPaymentStatusClass(paymentStatus);
                    }

                    // Format numeric values safely
                    if (rowView["Duration"] != null && rowView["Duration"] != DBNull.Value)
                    {
                        if (decimal.TryParse(rowView["Duration"].ToString(), out decimal duration))
                        {
                            // Duration is already formatted in BoundField
                        }
                    }

                    if (rowView["BookingAmount"] != null && rowView["BookingAmount"] != DBNull.Value)
                    {
                        if (decimal.TryParse(rowView["BookingAmount"].ToString(), out decimal amount))
                        {
                            // Amount is already formatted in BoundField
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log individual row errors
                    System.Diagnostics.Debug.WriteLine($"RowDataBound error: {ex.Message}");
                }
            }
        }

        // Update your GetStatusClass method to handle nulls
        protected string GetStatusClass(string status)
        {
            if (string.IsNullOrEmpty(status)) return "badge badge-secondary";

            switch (status.ToLower())
            {
                case "confirmed": return "badge badge-success";
                case "pending": return "badge badge-warning";
                case "cancelled": return "badge badge-danger";
                case "completed": return "badge badge-info";
                default: return "badge badge-secondary";
            }
        }

        protected string GetPaymentStatusClass(string paymentStatus)
        {
            if (string.IsNullOrEmpty(paymentStatus)) return "badge badge-secondary";

            switch (paymentStatus.ToLower())
            {
                case "paid": return "badge badge-success";
                case "pending": return "badge badge-warning";
                case "failed": return "badge badge-danger";
                case "refunded": return "badge badge-info";
                default: return "badge badge-secondary";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            InitializePage();
            BindWorkshopSlots();
            pnlGrid.Visible = false;
            pnlNoData.Visible = false;
            pnlExport.Visible = false;
            pnlSummary.Visible = false;

            upFilter.Update();
            upReports.Update();
            upSummary.Update();
            upExport.Update();

            ShowSweetAlert("Filters Reset", "All filters have been reset to default values.", "success");
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        // Export Methods
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable exportData = GetReportData();
                if (exportData.Rows.Count > 0)
                {
                    ExportToExcel(exportData);
                }
                else
                {
                    ShowSweetAlert("No Data", "No data available to export.", "warning");
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ITIWorkshopReports";
                clsLogger.ExceptionMsg = "btnExportExcel_Click";
                clsLogger.SaveException();
                ShowSweetAlert("Export Error", "Error exporting to Excel. Please try again.", "error");
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Print", "printReport();", true);
        }

        private void ExportToExcel(DataTable data)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ITI_Workshop_Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    // Create Excel file header
                    hw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                    hw.WriteLine("<table border=\"1\">");

                    // Add report header
                    hw.WriteLine("<tr><td colspan=\"10\" style=\"background-color: #2c3e50; color: white; font-size: 16px; font-weight: bold; text-align: center; padding: 10px;\">ITI WORKSHOP BOOKINGS REPORT</td></tr>");

                    // Add summary info
                    hw.WriteLine("<tr><td colspan=\"10\" style=\"background-color: #f8f9fa; padding: 8px;\">");
                    hw.WriteLine("<strong>ITI:</strong> " + ITI_Name + " | ");
                    hw.WriteLine("<strong>Report Type:</strong> " + ddlReportType.SelectedItem.Text + " | ");
                    hw.WriteLine("<strong>Date Range:</strong> " + litSummaryDateRange.Text + " | ");
                    hw.WriteLine("<strong>Total Bookings:</strong> " + litSummaryTotal.Text + " | ");
                    hw.WriteLine("<strong>Total Amount:</strong> ₹" + litSummaryAmount.Text);
                    hw.WriteLine("</td></tr>");
                    hw.WriteLine("<tr><td colspan=\"10\"></td></tr>");

                    // Add column headers
                    hw.WriteLine("<tr style=\"background-color: #3498db; color: white; font-weight: bold;\">");
                    hw.WriteLine("<th>Booking ID</th>");
                    hw.WriteLine("<th>Student Name</th>");
                    hw.WriteLine("<th>Mobile</th>");
                    hw.WriteLine("<th>Workshop Date</th>");
                    hw.WriteLine("<th>Time</th>");
                    hw.WriteLine("<th>Duration (hrs)</th>");
                    hw.WriteLine("<th>Amount</th>");
                    hw.WriteLine("<th>Status</th>");
                    hw.WriteLine("<th>Payment</th>");
                    hw.WriteLine("<th>Booked On</th>");
                    hw.WriteLine("</tr>");

                    // Add data rows
                    foreach (DataRow row in data.Rows)
                    {
                        hw.WriteLine("<tr>");
                        hw.WriteLine("<td>" + row["BookingID"] + "</td>");
                        hw.WriteLine("<td>" + row["FullName"] + "</td>");
                        hw.WriteLine("<td>" + row["MobileNumber"] + "</td>");

                        // Format date
                        if (row["WorkshopDate"] != DBNull.Value)
                        {
                            DateTime workshopDate = Convert.ToDateTime(row["WorkshopDate"]);
                            hw.WriteLine("<td>" + workshopDate.ToString("dd-MMM-yyyy") + "</td>");
                        }
                        else
                        {
                            hw.WriteLine("<td></td>");
                        }

                        // Format time
                        if (row["WorkshopTime"] != DBNull.Value)
                        {
                            try
                            {
                                TimeSpan workshopTime = (TimeSpan)row["WorkshopTime"];
                                string timeString = workshopTime.ToString(@"hh\:mm") + " " + (workshopTime.Hours >= 12 ? "PM" : "AM");
                                hw.WriteLine("<td>" + timeString + "</td>");
                            }
                            catch
                            {
                                hw.WriteLine("<td>" + row["WorkshopTime"] + "</td>");
                            }
                        }
                        else
                        {
                            hw.WriteLine("<td></td>");
                        }

                        hw.WriteLine("<td>" + row["Duration"] + "</td>");

                        // Format amount
                        if (row["BookingAmount"] != DBNull.Value)
                        {
                            decimal amount = Convert.ToDecimal(row["BookingAmount"]);
                            hw.WriteLine("<td>₹" + amount.ToString("N0") + "</td>");
                        }
                        else
                        {
                            hw.WriteLine("<td>₹0</td>");
                        }

                        hw.WriteLine("<td>" + row["BookingStatus"] + "</td>");
                        hw.WriteLine("<td>" + row["PaymentStatus"] + "</td>");

                        // Format created date
                        if (row["CreatedDate"] != DBNull.Value)
                        {
                            DateTime createdDate = Convert.ToDateTime(row["CreatedDate"]);
                            hw.WriteLine("<td>" + createdDate.ToString("dd-MMM-yyyy HH:mm") + "</td>");
                        }
                        else
                        {
                            hw.WriteLine("<td></td>");
                        }

                        hw.WriteLine("</tr>");
                    }

                    // Add footer with export info
                    hw.WriteLine("<tr><td colspan=\"10\" style=\"background-color: #f8f9fa; padding: 8px; font-size: 11px;\">");
                    hw.WriteLine("Exported on: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm") + " | ITI Workshop Management System");
                    hw.WriteLine("</td></tr>");

                    hw.WriteLine("</table>");

                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                // Handle exception if user cancels download
                if (!ex.Message.Contains("Thread was being aborted"))
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/ITIWorkshopReports";
                    clsLogger.ExceptionMsg = "ExportToExcel";
                    clsLogger.SaveException();
                    ShowSweetAlert("Export Error", "Error generating Excel file. Please try again.", "error");
                }
            }
        }

        // SweetAlert helper method
        private void ShowSweetAlert(string title, string message, string type)
        {
            hdnSweetAlertTitle.Value = title;
            hdnSweetAlertMessage.Value = message;
            hdnSweetAlertType.Value = type;

            // Register script to show alert on client side
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert",
                "setTimeout(function() { checkForAlerts(); }, 500);", true);
        }
    }
}