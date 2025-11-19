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
using System.Threading;

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
                litUserName.Text = ITI_Name;
            }
            else
            {
                Response.Redirect("~/DHE/frmlogin.aspx");
                return;
            }

            if (!IsPostBack)
            {
                InitializePage();
                BindWorkshopSlots();
                
            }
        }

        private void InitializePage()
        {
            DateTime maxDate = DateTime.Today;
            txtFromDate.Attributes.Add("max", maxDate.ToString("yyyy-MM-dd"));
            txtToDate.Attributes.Add("max", maxDate.ToString("yyyy-MM-dd"));
            // Set default dates
            txtReportDate.Attributes.Add("max", maxDate.ToString("yyyy-MM-dd"));
            txtReportDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
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
                    pnlWorkshopSlot.Visible = true;
                    break;
                case "RANGE":
                    pnlDailyDate.Visible = false;
                    pnlDateRange.Visible = true;
                    pnlMonthly.Visible = false;
                    pnlWorkshopSlot.Visible = false;
                    break;
                case "MONTHLY":
                    pnlDailyDate.Visible = false;
                    pnlDateRange.Visible = false;
                    pnlMonthly.Visible = true;
                    pnlWorkshopSlot.Visible = false;
                    break;
            }
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
                    pnlExport.Visible = true; // Show export buttons
                    pnlNoData.Visible = false;
                    pnlGrid.Visible = true;
                    pnlNoData.Visible = false;
                    ShowSweetAlert("Report Generated", $"Found {reportData.Rows.Count} bookings matching your criteria.", "success");
                }
                else
                {
                    pnlGrid.Visible = false;
                    pnlNoData.Visible = true;
                    pnlExport.Visible = false; // Hide export buttons when no data
                    pnlSummary.Visible = false;
                    ShowSweetAlert("No Data", "No bookings found for the selected criteria.", "info");
                }

                upReports.Update();
                upSummary.Update();
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
            int confirmedCount = 0;
            int pendingCount = 0;
            int totalCount = data.Rows.Count;
            decimal confirmedAmount = 0;
            decimal pendingAmount = 0;
            decimal totalAmount = 0;

            foreach (DataRow row in data.Rows)
            {
                string status = row["BookingStatus"]?.ToString() ?? "";
                decimal amount = row["BookingAmount"] != DBNull.Value ? Convert.ToDecimal(row["BookingAmount"]) : 0;

                if (status.Equals("CONFIRMED", StringComparison.OrdinalIgnoreCase))
                {
                    confirmedCount++;
                    confirmedAmount += amount;
                }
                else if (status.Equals("PENDING", StringComparison.OrdinalIgnoreCase))
                {
                    pendingCount++;
                    pendingAmount += amount;
                }

                totalAmount += amount;
            }

            // Update the literals
            litConfirmedCount.Text = confirmedCount.ToString("N0");
            litConfirmedAmount.Text = confirmedAmount.ToString("N0");
            litPendingCount.Text = pendingCount.ToString("N0");
            litPendingAmount.Text = pendingAmount.ToString("N0");
            litTotalCount.Text = totalCount.ToString("N0");
            litTotalAmount.Text = totalAmount.ToString("N0");

            // Set date range text
            switch (ddlReportType.SelectedValue)
            {
                case "DAILY":
                    litSummaryDateRange.Text = DateTime.Parse(txtReportDate.Text).ToString("dd-MMM-yyyy");
                    litReportType.Text = "Daily Report";
                    break;
                case "RANGE":
                    litSummaryDateRange.Text = $"{DateTime.Parse(txtFromDate.Text):dd-MMM-yyyy} to {DateTime.Parse(txtToDate.Text):dd-MMM-yyyy}";
                    litReportType.Text = "Date Range Report";
                    break;
                case "MONTHLY":
                    DateTime month = DateTime.Parse(txtMonth.Text + "-01");
                    litSummaryDateRange.Text = month.ToString("MMMM yyyy");
                    litReportType.Text = "Monthly Report";
                    break;
                default:
                    litSummaryDateRange.Text = "N/A";
                    litReportType.Text = "Custom Report";
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
            if (string.IsNullOrEmpty(status)) return "badge bg-secondary";

            switch (status.ToLower())
            {
                case "confirmed": return "badge bg-success";
                case "pending": return "badge bg-warning";
                case "cancelled": return "badge bg-danger";
                case "completed": return "badge bg-info";
                default: return "badge bg-secondary";
            }
        }

        protected string GetPaymentStatusClass(string paymentStatus)
        {
            if (string.IsNullOrEmpty(paymentStatus)) return "badge bg-secondary";

            switch (paymentStatus.ToLower())
            {
                case "paid": return "badge bg-success";
                case "pending": return "badge bg-warning";
                case "failed": return "badge bg-danger";
                case "refunded": return "badge bg-info";
                default: return "badge bg-secondary";
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

            ShowSweetAlert("Filters Reset", "All filters have been reset to default values.", "success");
        }


       

        // Export Methods
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable exportData = GetReportData();
                if (exportData != null && exportData.Rows.Count > 0)
                {
                    ExportToExcel(exportData);
                }
                else
                {
                    ShowSweetAlert("No Data", "No data available to export.", "warning");
                }
            }
            catch (ThreadAbortException)
            {
                // Ignore - this is normal for Response.End()
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
            try
            {
                // Register print script
                ScriptManager.RegisterStartupScript(this, GetType(), "Print", "printReport();", true);

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/ITIWorkshopReports";
                clsLogger.ExceptionMsg = "btnPrint_Click";
                clsLogger.SaveException();
                ShowSweetAlert("Print Error", "Error preparing print. Please try again.", "error");
            }
        }

        private void ExportToExcel(DataTable data)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                    $"attachment;filename=ITI_Workshop_Report_{DateTime.Now:yyyyMMdd_HHmmss}.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    // Create Excel file structure
                    hw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                    hw.WriteLine("<table border=\"1\" style=\"border-collapse:collapse; width:100%;\">");

                    // Report Header
                    hw.WriteLine("<tr>");
                    hw.WriteLine("<td colspan=\"12\" style=\"background-color: #2c3e50; color: white; font-size: 16px; font-weight: bold; text-align: center; padding: 15px;\">");
                    hw.WriteLine("ITI WORKSHOP BOOKINGS REPORT");
                    hw.WriteLine("</td>");
                    hw.WriteLine("</tr>");

                    // Summary Information
                    hw.WriteLine("<tr>");
                    hw.WriteLine("<td colspan=\"12\" style=\"background-color: #f8f9fa; padding: 10px; font-size: 12px;\">");
                    hw.WriteLine($"<strong>ITI:</strong> {ITI_Name} | ");
                    hw.WriteLine($"<strong>Report Type:</strong> {ddlReportType.SelectedItem.Text} | ");
                    hw.WriteLine($"<strong>Date Range:</strong> {litSummaryDateRange.Text} | ");
                    hw.WriteLine($"<strong>Confirmed Bookings:</strong> {litConfirmedCount.Text} (₹{litConfirmedAmount.Text}) | ");
                    hw.WriteLine($"<strong>Pending Bookings:</strong> {litPendingCount.Text} (₹{litPendingAmount.Text}) | ");
                    hw.WriteLine($"<strong>Total Bookings:</strong> {litTotalCount.Text} (₹{litTotalAmount.Text})");
                    hw.WriteLine("</td>");
                    hw.WriteLine("</tr>");

                    hw.WriteLine("<tr><td colspan=\"12\"></td></tr>");

                    // Column Headers
                    hw.WriteLine("<tr style=\"background-color: #3498db; color: white; font-weight: bold;\">");
                    hw.WriteLine("<th style=\"padding: 8px;\">Booking ID</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Student Name</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Mobile</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Email</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Workshop Date</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Start Time</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">End Time</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Duration (hrs)</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Amount</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Status</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Payment</th>");
                    hw.WriteLine("<th style=\"padding: 8px;\">Booked On</th>");
                    hw.WriteLine("</tr>");

                    // Data Rows
                    foreach (DataRow row in data.Rows)
                    {
                        hw.WriteLine("<tr>");

                        // Booking ID
                        hw.WriteLine($"<td style=\"padding: 6px;\">{row["BookingID"]}</td>");

                        // Student Name
                        hw.WriteLine($"<td style=\"padding: 6px;\">{row["FullName"]}</td>");

                        // Mobile
                        hw.WriteLine($"<td style=\"padding: 6px;\">{row["MobileNumber"]}</td>");

                        // Email
                        hw.WriteLine($"<td style=\"padding: 6px;\">{row["Email"]}</td>");

                        // Workshop Date
                        if (row["WorkshopDate"] != DBNull.Value)
                        {
                            DateTime workshopDate = Convert.ToDateTime(row["WorkshopDate"]);
                            hw.WriteLine($"<td style=\"padding: 6px;\">{workshopDate:dd-MMM-yyyy}</td>");
                        }
                        else
                        {
                            hw.WriteLine("<td style=\"padding: 6px;\"></td>");
                        }

                        // Start Time
                        hw.WriteLine($"<td style=\"padding: 6px;\">{row["StartTime"]}</td>");

                        // End Time
                        hw.WriteLine($"<td style=\"padding: 6px;\">{row["EndTime"]}</td>");

                        // Duration
                        if (row["Duration"] != DBNull.Value)
                        {
                            decimal duration = Convert.ToDecimal(row["Duration"]);
                            hw.WriteLine($"<td style=\"padding: 6px; text-align: right;\">{duration:N1}</td>");
                        }
                        else
                        {
                            hw.WriteLine("<td style=\"padding: 6px;\"></td>");
                        }

                        // Amount
                        if (row["BookingAmount"] != DBNull.Value)
                        {
                            decimal amount = Convert.ToDecimal(row["BookingAmount"]);
                            hw.WriteLine($"<td style=\"padding: 6px; text-align: right;\">₹{amount:N0}</td>");
                        }
                        else
                        {
                            hw.WriteLine("<td style=\"padding: 6px;\">₹0</td>");
                        }

                        // Status
                        string status = row["BookingStatus"]?.ToString() ?? "Pending";
                        string statusColor = status.ToLower() == "confirmed" ? "#28a745" :
                                           status.ToLower() == "pending" ? "#ffc107" : "#6c757d";
                        hw.WriteLine($"<td style=\"padding: 6px; color: {statusColor}; font-weight: bold;\">{status}</td>");

                        // Payment Status
                        string paymentStatus = row["PaymentStatus"]?.ToString() ?? "Pending";
                        string paymentColor = paymentStatus.ToLower() == "paid" ? "#28a745" :
                                            paymentStatus.ToLower() == "pending" ? "#ffc107" : "#dc3545";
                        hw.WriteLine($"<td style=\"padding: 6px; color: {paymentColor}; font-weight: bold;\">{paymentStatus}</td>");

                        // Created Date
                        if (row["CreatedDate"] != DBNull.Value)
                        {
                            DateTime createdDate = Convert.ToDateTime(row["CreatedDate"]);
                            hw.WriteLine($"<td style=\"padding: 6px;\">{createdDate:dd-MMM-yyyy HH:mm}</td>");
                        }
                        else
                        {
                            hw.WriteLine("<td style=\"padding: 6px;\"></td>");
                        }

                        hw.WriteLine("</tr>");
                    }

                    // Footer
                    hw.WriteLine("<tr>");
                    hw.WriteLine("<td colspan=\"12\" style=\"background-color: #f8f9fa; padding: 8px; font-size: 11px; border-top: 2px solid #dee2e6;\">");
                    hw.WriteLine($"<strong>Exported on:</strong> {DateTime.Now:dd-MMM-yyyy HH:mm} | ");
                    hw.WriteLine($"<strong>Total Records:</strong> {data.Rows.Count} | ");
                    hw.WriteLine("ITI Workshop Management System");
                    hw.WriteLine("</td>");
                    hw.WriteLine("</tr>");

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
                    throw;
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