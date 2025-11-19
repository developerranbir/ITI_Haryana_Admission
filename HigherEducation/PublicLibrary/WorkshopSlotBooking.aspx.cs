using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Threading;
using HigherEducation.Models;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using CCA.Util;
using System.Reflection;
using HigherEducation.BusinessLayer;

namespace HigherEducation.PublicLibrary
{
    public partial class WorkshopSlotBooking : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        private int selectedSlotId = 0;
        private decimal hourlyRate = 0; 
        private const int SEATS_PER_BOOKING = 1; // Single seat per booking
        private string CandidateName = "";
        CCACrypto ccaCrypto = new CCACrypto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            CandidateName = Session["FullName"].ToString();
            hourlyRate = setPriceforWorkshop();
            if (!IsPostBack)
            {
                DateTime currentDateTime = DateTime.Now;

                // Set default date to today and ensure it's not a weekend
                DateTime today = DateTime.Today;
                if (IsWeekend(today))
                {
                    // If today is weekend, find next available weekday
                    DateTime nextWeekday = GetNextAvailableWeekday(today);
                    txtSlotDate.Text = nextWeekday.ToString("yyyy-MM-dd");
                    ShowSweetAlert("Weekend Detected", $"Today is a weekend. Default date set to next available weekday: {nextWeekday:dddd, MMMM dd}.", "info");
                }
                else
                {
                    txtSlotDate.Text = today.ToString("yyyy-MM-dd");
                }
                
                BindDistricts();
            }
            else
            {
                //hourlyRate = setPriceforWorkshop();
                // Restore selected slot on postback
                if (ViewState["SelectedSlotId"] != null)
                {
                    selectedSlotId = (int)ViewState["SelectedSlotId"];
                }
            }
        }

        private decimal setPriceforWorkshop() {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("getWorkshopFee", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hourlyRate = Convert.ToDecimal(reader["workshopFee"]);
                            }
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "getWorkshopFee";
                    clsLogger.SaveException();
                    ShowSweetAlert("Error", "Error loading districts. Please try again.", "error");
                }
            }
            return hourlyRate; 
        }

        private void BindDistricts()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("BindDistrict", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            ddlDistrict.Items.Clear();
                            ddlDistrict.Items.Add(new ListItem("-- Select District --", ""));

                            while (reader.Read())
                            {
                                ddlDistrict.Items.Add(new ListItem(
                                reader["text"].ToString(),
                                reader["value"].ToString()
                                ));
                            }
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "BindDistricts";
                    clsLogger.SaveException();
                    ShowSweetAlert("Error", "Error loading districts. Please try again.", "error");
                }
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlDistrict.SelectedValue))
            {
                BindITIsByDistrict();
                ClearSlotSelection();

                // Update panels
                upDistrictITI.Update();
                upSlots.Update();
            }
            else
            {
                ddlITI.Items.Clear();
                ddlITI.Items.Add(new ListItem("-- Select ITI --", ""));
                pnlSlots.Visible = false;

                // Update panels
                upDistrictITI.Update();
                upSlots.Update();
            }
        }

        private void BindITIsByDistrict()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("BindGovtITIs", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_districtcode", Convert.ToInt32(ddlDistrict.SelectedValue));

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            ddlITI.Items.Clear();
                            ddlITI.Items.Add(new ListItem("-- Select ITI --", ""));

                            while (reader.Read())
                            {
                                ddlITI.Items.Add(new ListItem(
                                reader["text"].ToString(),
                                reader["value"].ToString()
                                ));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "BindITIsByDistrict";
                    clsLogger.SaveException();
                    ShowSweetAlert("Error", "Error loading ITIs. Please try again.", "error");
                }
            }
        }

        private void BindAvailableSlots()
        {
            if (string.IsNullOrEmpty(txtSlotDate.Text))
            {
                ShowSweetAlert("Date Required", "Please select a date first.", "warning");
                pnlSlots.Visible = false;
                pnlAmount.Visible = false;
                pnlBookingForm.Visible = false;
                upSlots.Update();
                return;
            }

            DateTime selectedDate = DateTime.Parse(txtSlotDate.Text);
            DateTime today = DateTime.Today;

            // Check if date is in the past
            if (selectedDate < today)
            {
                ShowSweetAlert("Invalid Date", "Please select today or a future date. Past dates are not allowed.", "error");
                txtSlotDate.Text = today.ToString("yyyy-MM-dd"); // Reset to today
                pnlSlots.Visible = false;
                pnlAmount.Visible = false;
                pnlBookingForm.Visible = false;
                pnlWeekendWarning.Visible = false;
                upSlots.Update();
                return;
            }

            // Don't show slots for weekends
            if (IsWeekend(selectedDate))
            {
                pnlSlots.Visible = false;
                pnlAmount.Visible = false;
                pnlBookingForm.Visible = false;
                upSlots.Update();
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_GetAvailableSlotsByITI", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_ITI_Id", ddlITI.SelectedValue);
                        cmd.Parameters.AddWithValue("p_SlotDate", selectedDate);

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        // Filter slots based on current time if selected date is today
                        DataTable filteredSlots = selectedDate == DateTime.Today ?
                            FilterSlotsByCurrentTime(dt) : dt;

                        if (filteredSlots.Rows.Count > 0)
                        {
                            rptSlots.DataSource = filteredSlots;
                            rptSlots.DataBind();
                            lblNoSlots.Visible = false;

                            // Update header text based on selected date
                            UpdateSlotHeader(selectedDate);

                            // Hide amount panel initially
                            pnlAmount.Visible = false;
                            pnlBookingForm.Visible = false;

                            // Restore selected slot if exists, otherwise don't auto-select
                            if (ViewState["SelectedSlotId"] != null)
                            {
                                RestoreSelectedSlot();
                            }
                        }
                        else
                        {
                            rptSlots.DataSource = null;
                            rptSlots.DataBind();
                            lblNoSlots.Text = selectedDate == DateTime.Today ?
                                "No available slots for today. All slots have either passed or are fully booked." :
                                $"No available slots for {selectedDate:dddd, MMMM dd}. Please try another date.";
                            lblNoSlots.Visible = true;
                            pnlAmount.Visible = false;
                            pnlBookingForm.Visible = false;
                            head.Visible = false;
                            showText.Visible = false;
                            ViewState["SelectedSlotId"] = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "BindAvailableSlots";
                    clsLogger.SaveException();
                    ShowSweetAlert("Error", "Error loading available slots. Please try again.", "error");
                    pnlAmount.Visible = false;
                    pnlBookingForm.Visible = false;
                }
            }

            // Update the slots panel
            upSlots.Update();
        }
        // ... (All other methods remain the same until the ShowMessage method)

        // Replace the old ShowMessage method with SweetAlert version
        private void ShowSweetAlert(string title, string message, string type)
        {
            hdnSweetAlertTitle.Value = title;
            hdnSweetAlertMessage.Value = message;
            hdnSweetAlertType.Value = type;

            // Register script to show SweetAlert on client side
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowSweetAlert",
                $"showSweetAlert('{title}', '{message.Replace("'", "\\'")}', '{type}');", true);
        }

        // Remove the old ShowMessage method and replace all its calls with ShowSweetAlert

        protected void btnBookSlot_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && selectedSlotId > 0)
            {
                try
                {
                    // Check seat availability
                    if (!CheckSeatAvailability(selectedSlotId))
                    {
                        ShowSweetAlert("Slot Unavailable", "Sorry, this slot is no longer available. Please select a different slot.", "error");
                        BindAvailableSlots();
                        return;
                    }

                    // Get slot details for time checking
                    DataRow slotDetails = GetSlotDetails(selectedSlotId);
                    if (slotDetails == null)
                    {
                        ShowSweetAlert("Error", "Slot details not found. Please try again.", "error");
                        return;
                    }

                    TimeSpan workshopTime = TimeSpan.Parse(slotDetails["StartTime"].ToString());
                    DateTime workshopDate = DateTime.Parse(slotDetails["SlotDate"].ToString());

                    // Check for duplicate booking
                    if (CheckDuplicateBooking(Session["Mobile"].ToString(), selectedSlotId))
                    {
                        ShowSweetAlert("Already Booked", "You have already booked this workshop slot. Each user can book only one seat per slot. Kindly click on 'View My Bookings' to check the status of your booking.", "warning");
                        return;
                    }

                    // Show confirmation dialog before proceeding to payment
                    ShowSweetAlert("Confirm Booking", "Are you sure you want to proceed with this booking?", "question");

                    // Save booking and redirect to confirmation page
                    if (SaveBooking(selectedSlotId))
                    {
                        // Redirect happens in SaveBooking method
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "btnBookSlot_Click";
                    clsLogger.SaveException();
                    ShowSweetAlert("Booking Error", $"Error booking slot: {ex.Message}", "error");
                }
            }
            else
            {
                ShowSweetAlert("Selection Required", "Please select a workshop slot to book.", "warning");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            ShowSweetAlert("Cancelled", "Slot selection cancelled.", "info");

            // Update panels
            upSlots.Update();
        }

        protected void txtSlotDate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSlotDate.Text))
            {
                DateTime selectedDate = DateTime.Parse(txtSlotDate.Text);
                DateTime today = DateTime.Today;

                // Check if date is in the past
                if (selectedDate < today)
                {
                    ShowSweetAlert("Invalid Date", "Please select today or a future date. Past dates are not allowed.", "error");
                    txtSlotDate.Text = today.ToString("yyyy-MM-dd"); // Reset to today
                    pnlSlots.Visible = false;
                    pnlAmount.Visible = false;
                    pnlBookingForm.Visible = false;
                    pnlWeekendWarning.Visible = false;

                    // Update panels
                    upDateSelection.Update();
                    upSlots.Update();
                    return;
                }

                // Check if weekend
                if (IsWeekend(selectedDate))
                {
                    pnlWeekendWarning.Visible = true;
                    pnlSlots.Visible = false;
                    pnlAmount.Visible = false;
                    pnlBookingForm.Visible = false;

                    // Update panels
                    upDateSelection.Update();
                    upSlots.Update();
                }
                else
                {
                    pnlWeekendWarning.Visible = false;
                    if (!string.IsNullOrEmpty(ddlITI.SelectedValue))
                    {
                        BindAvailableSlots();
                        pnlSlots.Visible = true;
                    }

                    // Update panels
                    upDateSelection.Update();
                    upSlots.Update();
                }
            }
            else
            {
                pnlSlots.Visible = false;
                pnlAmount.Visible = false;
                pnlBookingForm.Visible = false;
                pnlWeekendWarning.Visible = false;

                // Update panels
                upDateSelection.Update();
                upSlots.Update();
            }
        }

        protected void ddlITI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlITI.SelectedValue) && !string.IsNullOrEmpty(txtSlotDate.Text))
            {
                DateTime selectedDate = DateTime.Parse(txtSlotDate.Text);
                DateTime today = DateTime.Today;

                // Check if date is in the past
                if (selectedDate < today)
                {
                    ShowSweetAlert("Invalid Date", "Please select today or a future date. Past dates are not allowed.", "error");
                    txtSlotDate.Text = today.ToString("yyyy-MM-dd"); // Reset to today
                    pnlSlots.Visible = false;
                    pnlAmount.Visible = false;
                    pnlBookingForm.Visible = false;
                    pnlWeekendWarning.Visible = false;

                    // Update panels
                    upDateSelection.Update();
                    upSlots.Update();
                    return;
                }

                if (!IsWeekend(selectedDate))
                {
                    BindAvailableSlots();
                    pnlSlots.Visible = true;
                    ClearSlotSelection();
                }
            }
            else
            {
                pnlSlots.Visible = false;
                pnlAmount.Visible = false;
                pnlBookingForm.Visible = false;
            }

            // Update panels
            upDistrictITI.Update();
            upSlots.Update();
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        // Helper method to find next available weekday
        private DateTime GetNextAvailableWeekday(DateTime fromDate)
        {
            DateTime nextDate = fromDate;
            while (IsWeekend(nextDate))
            {
                nextDate = nextDate.AddDays(1);
            }
            return nextDate;
        }

        private void ClearSlotSelection()
        {
            selectedSlotId = 0;
            ViewState["SelectedSlotId"] = null;

            // Hide amount and booking panels
            pnlAmount.Visible = false;
            pnlBookingForm.Visible = false;

            foreach (RepeaterItem item in rptSlots.Items)
            {
                RadioButton rb = (RadioButton)item.FindControl("rbSlot");
                if (rb != null)
                {
                    rb.Checked = false;
                }

                HtmlGenericControl slotCard = (HtmlGenericControl)item.FindControl("slotCard");
                if (slotCard != null)
                {
                    // Remove selected class but keep other classes
                    string currentClass = slotCard.Attributes["class"] ?? "";
                    slotCard.Attributes["class"] = currentClass.Replace("selected", "").Trim();
                }
            }
        }

        private DataTable FilterSlotsByCurrentTime(DataTable slots)
        {
            DataTable filteredTable = slots.Clone();
            DateTime currentTime = DateTime.Now;

            foreach (DataRow row in slots.Rows)
            {
                if (TimeSpan.TryParse(row["StartTime"].ToString(), out TimeSpan startTime))
                {
                    DateTime slotDateTime = DateTime.Today.Add(startTime);

                    // Only include slots that start in the future
                    if (slotDateTime > currentTime)
                    {
                        filteredTable.ImportRow(row);
                    }
                }
            }

            return filteredTable;
        }
        private void UpdateSlotHeader(DateTime selectedDate)
        {
            string dateText = selectedDate == DateTime.Today ? "Today" : selectedDate.ToString("dddd, MMMM dd");
            head.InnerText = $"Available Workshop Slots - {dateText}";
            showText.InnerText = $"Select one slot to book for {selectedDate:MMMM dd, yyyy}. Price: ₹300 per hour. Green indicates available seats.";
        }

        private void RestoreSelectedSlot()
        {
            if (ViewState["SelectedSlotId"] != null)
            {
                selectedSlotId = (int)ViewState["SelectedSlotId"];

                // Find and check the radio button for the selected slot
                foreach (RepeaterItem item in rptSlots.Items)
                {
                    RadioButton rbSlot = (RadioButton)item.FindControl("rbSlot");
                    Literal litSlotId = (Literal)item.FindControl("litSlotId");
                    HtmlGenericControl slotCard = (HtmlGenericControl)item.FindControl("slotCard");

                    if (rbSlot != null && litSlotId != null && slotCard != null && int.TryParse(litSlotId.Text, out int slotId))
                    {
                        if (slotId == selectedSlotId && rbSlot.Enabled)
                        {
                            rbSlot.Checked = true;

                            // Add selected class
                            string currentClass = slotCard.Attributes["class"] ?? "";
                            if (!currentClass.Contains("selected"))
                            {
                                slotCard.Attributes["class"] = currentClass + " selected";
                            }

                            // Show amount panel
                            pnlAmount.Visible = true;
                            pnlBookingForm.Visible = true;

                            // Update amount display
                            Literal litDuration = (Literal)item.FindControl("litDuration");
                            if (litDuration != null && double.TryParse(litDuration.Text.Replace(" hours", "").Replace(" hour", ""), out double hours))
                            {
                                decimal totalAmount = (decimal)hours * hourlyRate;
                                litTotalAmount.Text = totalAmount.ToString("0");
                                litSelectedDuration.Text = litDuration.Text;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private bool CheckSeatAvailability(int slotId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_CheckSeatAvailability", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_SlotID", slotId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader["IsAvailable"]) == 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "CheckSeatAvailability";
                    clsLogger.SaveException();
                    return false;
                }
            }
            return false;
        }

        private DataRow GetSlotDetails(int slotId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM WorkshopSlots WHERE ID = @SlotID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SlotID", slotId);

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "GetSlotDetails";
                    clsLogger.SaveException();
                    return null;
                }
            }
        }


        private bool CheckDuplicateBooking(string mobileNumber, int slotId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_CheckDuplicateBooking", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_MobileNumber", mobileNumber);
                        cmd.Parameters.AddWithValue("p_SlotID", slotId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader["IsDuplicate"]) > 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "CheckDuplicateBooking";
                    clsLogger.SaveException();
                    // In case of error, assume no duplicate to not block legitimate bookings
                    return false;
                }
            }
            return false;
        }


        private bool SaveBooking(int slotId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Start transaction for both booking and payment record
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Get selected slot details
                            DataRow slotDetails = GetSlotDetails(slotId);
                            if (slotDetails == null)
                            {
                                ShowSweetAlert("Error", "Slot details not found. Please try again.", "danger");
                                return false;
                            }

                            // Calculate booking amount
                            double duration = Convert.ToDouble(slotDetails["Duration"]);
                            decimal bookingAmount = (decimal)duration * hourlyRate;

                            // Step 1: Save to WorkshopBookings table using stored procedure
                            int bookingId = SaveWorkshopBooking(conn, transaction, slotId, slotDetails, bookingAmount);
                            if (bookingId <= 0)
                            {
                                transaction.Rollback();
                                return false;
                            }

                            string paymentReferenceId = "0";
                            string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                            //Generate Transaction Number
                            paymentReferenceId = Session["Mobile"].ToString() + currentDateTime;
                            if (paymentReferenceId.Length > 30)
                            {
                                paymentReferenceId = paymentReferenceId.Substring(paymentReferenceId.Length - 30);
                            }

                            int paymentId = SavePaymentRecord(conn, transaction, bookingId, slotId, paymentReferenceId, bookingAmount);
                            if (paymentId <= 0)
                            {
                                transaction.Rollback();
                                return false;
                            }

                            // Commit transaction
                            transaction.Commit();

                            // Store IDs in session for payment gateway
                            Session["LastBookingID"] = bookingId;
                            Session["PaymentReferenceID"] = paymentReferenceId;
                            Session["PaymentAmount"] = bookingAmount;

                            // Step 3: Redirect to payment gateway
                            RedirectToPaymentGateway(bookingId, paymentReferenceId, bookingAmount);

                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Transaction failed: {ex.Message}");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "SaveBooking";
                    clsLogger.SaveException();
                    if (ex.Message.Contains("No seats available"))
                    {
                        ShowSweetAlert("Error", "Sorry, this slot is no longer available. Please select a different slot.", "danger");

                        ShowSweetAlert("Error", "Slot details not found. Please try again.", "danger");
                    }
                    else if (ex.Message.Contains("already booked"))
                    {
                        ShowSweetAlert("Error", "You have already booked this workshop slot. Each user can book only one seat per slot.", "warning");
                    }
                    else
                    {
                        ShowSweetAlert("Error", "Database error", "danger");
                    }
                    return false;
                }
                catch (ThreadAbortException)
                {
                    // Expected during redirect
                    return true;
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                    clsLogger.ExceptionMsg = "SaveBooking";
                    clsLogger.SaveException();
                    ShowSweetAlert("Error", "Error saving booking", "danger");
                    return false;
                }
            }
        }
        private int SaveWorkshopBooking(MySqlConnection conn, MySqlTransaction transaction, int slotId, DataRow slotDetails, decimal bookingAmount)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_SaveBooking", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters for workshop booking
                    cmd.Parameters.AddWithValue("p_SlotID", slotId);
                    cmd.Parameters.AddWithValue("p_FullName", CandidateName);
                    cmd.Parameters.AddWithValue("p_Email", Session["Email"]?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("p_MobileNumber", Session["Mobile"]?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("p_DistrictId", Convert.ToInt32(ddlDistrict.SelectedValue));
                    cmd.Parameters.AddWithValue("p_District", ddlDistrict.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("p_ITI_Id", Convert.ToInt32(ddlITI.SelectedValue));
                    cmd.Parameters.AddWithValue("p_ITI_Name", ddlITI.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("p_WorkshopType", Convert.ToInt32(slotDetails["WorkshopTypeId"]));
                    cmd.Parameters.AddWithValue("p_WorkshopDate", DateTime.Parse(slotDetails["SlotDate"].ToString()));
                    cmd.Parameters.AddWithValue("p_WorkshopTime", TimeSpan.Parse(slotDetails["StartTime"].ToString()));
                    cmd.Parameters.AddWithValue("p_WorkshopDuration", Convert.ToInt32(slotDetails["Duration"]));
                    cmd.Parameters.AddWithValue("p_BookingAmount", bookingAmount);
                    cmd.Parameters.AddWithValue("p_UserId", Session["UserId"].ToString());

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader["BookingID"]);
                        }
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                clsLogger.ExceptionMsg = "SaveWorkshopBooking";
                clsLogger.SaveException();
            }
            return 0;
        }

        private int SavePaymentRecord(MySqlConnection conn, MySqlTransaction transaction, int bookingId, int slotId, string paymentReferenceId, decimal bookingAmount)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_SavePaymentRecord", conn, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_FullName", CandidateName);
                    cmd.Parameters.AddWithValue("p_UserId", Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("p_PaymentReferenceID", paymentReferenceId);
                    cmd.Parameters.AddWithValue("p_College_id", Convert.ToInt32(ddlITI.SelectedValue));
                    cmd.Parameters.AddWithValue("p_SubscriptionId_SlotId", slotId.ToString());
                    cmd.Parameters.AddWithValue("p_PaymentType", "WORKSHOP");
                    cmd.Parameters.AddWithValue("p_Mobile", Session["Mobile"]?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("p_Email", Session["Email"]?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("p_TotalAmount", bookingAmount);
                    cmd.Parameters.AddWithValue("p_Payment_gateway", "CCAvenue");
                    cmd.Parameters.AddWithValue("p_CreateUser", Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("p_IPAddress", GetClientIPAddress());
                    cmd.Parameters.AddWithValue("p_Remarks", "Workshop slot booking payment");

                    // Execute and get the payment ID
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                clsLogger.ExceptionMsg = "SavePaymentRecord";
                clsLogger.SaveException();
            }
            return 0;
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
                objFeeModule.merchant_param1 = bookingId.ToString();
                objFeeModule.merchant_param2 = Session["UserId"]?.ToString();
                objFeeModule.merchant_param3 = ddlITI.SelectedValue;
                objFeeModule.merchant_param4 = ddlDistrict.SelectedValue;
                objFeeModule.merchant_param5 = "WORKSHOP";

                // Generate encrypted request for payment gateway
                string ccaRequest = GenerateCCARequest(objFeeModule);
                string workingKey = ConfigurationManager.AppSettings["workingKey_ITI"];
                string strAccessCode = ConfigurationManager.AppSettings["strAccessCode_ITI"];

                // Encrypt the request
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
                clsLogger.ExceptionPage = "PublicLibrary/WorkshopSlotBooking";
                clsLogger.ExceptionMsg = "RedirectToPaymentGateway";
                clsLogger.SaveException();
                ShowSweetAlert("Error", "Error initializing payment gateway.", "danger");
            }
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

        private string GetClientIPAddress()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress ?? "Unknown";
        }

        private void ClearForm()
        {
            pnlAmount.Visible = false;
            pnlBookingForm.Visible = false;
            ClearSlotSelection();
            ViewState["SelectedSlotId"] = null;
        }

        protected void cvSlotDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtSlotDate.Text))
            {
                DateTime selectedDate = DateTime.Parse(txtSlotDate.Text);
                DateTime today = DateTime.Today;

                // Check if date is in the past
                if (selectedDate < today)
                {
                    args.IsValid = false;
                    cvSlotDate.ErrorMessage = "Please select today or a future date. Past dates are not allowed.";
                    return;
                }

                // Check if weekend
                if (IsWeekend(selectedDate))
                {
                    args.IsValid = false;
                    cvSlotDate.ErrorMessage = "Please select a weekday (Monday to Friday). Weekends are not available for workshop booking.";
                    return;
                }

                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                cvSlotDate.ErrorMessage = "Please select a date.";
            }
        }

        protected void rbSlot_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            // Uncheck all other radio buttons in the repeater
            foreach (RepeaterItem item in rptSlots.Items)
            {
                RadioButton otherRb = (RadioButton)item.FindControl("rbSlot");
                if (otherRb != null && otherRb != rb)
                {
                    otherRb.Checked = false;

                    // Remove selected class from other slots
                    HtmlGenericControl otherSlotCard = (HtmlGenericControl)item.FindControl("slotCard");
                    if (otherSlotCard != null)
                    {
                        string currentClass = otherSlotCard.Attributes["class"] ?? "";
                        otherSlotCard.Attributes["class"] = currentClass.Replace("selected", "").Trim();
                    }
                }
            }

            // Ensure the clicked radio button is checked
            rb.Checked = true;

            if (rb.Checked)
            {
                RepeaterItem item = (RepeaterItem)rb.NamingContainer;
                Literal litSlotId = (Literal)item.FindControl("litSlotId");
                Literal litDuration = (Literal)item.FindControl("litDuration");
                Literal litTime = (Literal)item.FindControl("litTime");
                Literal litWorkshopType = (Literal)item.FindControl("litWorkshopType");
                Literal litEquipment = (Literal)item.FindControl("litEquipment");
                HtmlGenericControl slotCard = (HtmlGenericControl)item.FindControl("slotCard");

                if (int.TryParse(litSlotId.Text, out selectedSlotId))
                {
                    // Store the selected slot in ViewState for postback
                    ViewState["SelectedSlotId"] = selectedSlotId;

                    // Add selected class to the slot card
                    if (slotCard != null)
                    {
                        string currentClass = slotCard.Attributes["class"] ?? "";
                        if (!currentClass.Contains("selected"))
                        {
                            slotCard.Attributes["class"] = currentClass + " selected";
                        }
                    }

                    // Calculate amount for single seat
                    if (double.TryParse(litDuration.Text.Replace(" hours", "").Replace(" hour", ""), out double hours))
                    {
                        decimal totalAmount = (decimal)hours * hourlyRate;
                        litTotalAmount.Text = totalAmount.ToString("0");
                        litSelectedDuration.Text = litDuration.Text;

                        
                        // Show amount and booking form panels
                        pnlAmount.Visible = true;
                        pnlBookingForm.Visible = true;
                    }
                }
            }
            else
            {
                // Hide panels if no slot is selected
                pnlAmount.Visible = false;
                pnlBookingForm.Visible = false;
                ViewState["SelectedSlotId"] = null;
            }

            // Update the slots panel
            upSlots.Update();
        }

        protected void rptSlots_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;

                // Set Slot ID
                Literal litSlotId = (Literal)e.Item.FindControl("litSlotId");
                if (litSlotId != null)
                {
                    litSlotId.Text = rowView["ID"].ToString();
                }

                // Format Time
                Literal litTime = (Literal)e.Item.FindControl("litTime");
                if (litTime != null)
                {
                    if (TimeSpan.TryParse(rowView["StartTime"].ToString(), out TimeSpan startTime) &&
                        TimeSpan.TryParse(rowView["EndTime"].ToString(), out TimeSpan endTime))
                    {
                        DateTime startDateTime = DateTime.Today.Add(startTime);
                        DateTime endDateTime = DateTime.Today.Add(endTime);

                        litTime.Text = $"{startDateTime:hh:mm tt} - {endDateTime:hh:mm tt}";
                    }
                    else
                    {
                        litTime.Text = "Time not available";
                    }
                }

                // Slot Status (Started/Upcoming/Scheduled)
                Label lblSlotStatus = (Label)e.Item.FindControl("lblSlotStatus");
                if (lblSlotStatus != null)
                {
                    DateTime selectedDate = string.IsNullOrEmpty(txtSlotDate.Text) ?
                        DateTime.Today : DateTime.Parse(txtSlotDate.Text);

                    if (TimeSpan.TryParse(rowView["StartTime"].ToString(), out TimeSpan startTime))
                    {
                        DateTime slotDateTime = selectedDate.Add(startTime);

                        if (selectedDate == DateTime.Today)
                        {
                            if (slotDateTime <= DateTime.Now)
                            {
                                lblSlotStatus.Text = " (Started)";
                                lblSlotStatus.CssClass = "text-danger small";
                            }
                            else
                            {
                                lblSlotStatus.Text = " (Upcoming)";
                                lblSlotStatus.CssClass = "text-success small";
                            }
                        }
                        else if (selectedDate < DateTime.Today)
                        {
                            lblSlotStatus.Text = " (Past)";
                            lblSlotStatus.CssClass = "text-muted small";
                        }
                        else
                        {
                            lblSlotStatus.Text = " (Scheduled)";
                            lblSlotStatus.CssClass = "text-info small";
                        }
                    }
                }

                // Format Duration
                Literal litDuration = (Literal)e.Item.FindControl("litDuration");
                if (litDuration != null)
                {
                    if (double.TryParse(rowView["Duration"].ToString(), out double duration))
                    {
                        litDuration.Text = $"{duration:0} hour{(duration > 1 ? "s" : "")}";
                    }
                    else
                    {
                        litDuration.Text = "Duration not available";
                    }
                }

                // Available Seats
                Label lblAvailableSeats = (Label)e.Item.FindControl("lblAvailableSeats");
                if (lblAvailableSeats != null)
                {
                    int availableSeats = 0;
                    if (int.TryParse(rowView["AvailableSeats"].ToString(), out availableSeats))
                    {
                        lblAvailableSeats.Text = availableSeats.ToString();

                        // Change color based on availability
                        if (availableSeats == 0)
                        {
                            lblAvailableSeats.CssClass = "text-danger font-weight-bold";
                        }
                        else if (availableSeats <= 2) // Low availability
                        {
                            lblAvailableSeats.CssClass = "text-warning font-weight-bold";
                        }
                        else
                        {
                            lblAvailableSeats.CssClass = "text-success font-weight-bold";
                        }
                    }
                    else
                    {
                        lblAvailableSeats.Text = "0";
                        lblAvailableSeats.CssClass = "text-danger font-weight-bold";
                    }
                }

                // Calculate and display amount (for single seat)
                Literal litAmount = (Literal)e.Item.FindControl("litAmount");
                if (litAmount != null)
                {
                    if (double.TryParse(rowView["Duration"].ToString(), out double hours))
                    {
                        decimal amount = (decimal)hours * hourlyRate;
                        litAmount.Text = amount.ToString("0");
                    }
                    else
                    {
                        litAmount.Text = "0";
                    }
                }

                // Workshop Type
                Literal litWorkshopType = (Literal)e.Item.FindControl("litWorkshopType");
                if (litWorkshopType != null)
                {
                    string workshopType = rowView["WorkshopType"]?.ToString();
                    litWorkshopType.Text = !string.IsNullOrEmpty(workshopType) ? workshopType : "General Workshop";
                }

                // Equipment
                Literal litEquipment = (Literal)e.Item.FindControl("litEquipment");
                if (litEquipment != null)
                {
                    string equipment = rowView["EquipmentList"]?.ToString();
                    litEquipment.Text = !string.IsNullOrEmpty(equipment) ? equipment : "General Workshop";
                }
                

                // Disable radio button if no seats available or slot has started
                RadioButton rbSlot = (RadioButton)e.Item.FindControl("rbSlot");
                HtmlGenericControl slotCard = (HtmlGenericControl)e.Item.FindControl("slotCard");

                if (rbSlot != null && slotCard != null)
                {
                    DateTime selectedDate = string.IsNullOrEmpty(txtSlotDate.Text) ?
                        DateTime.Today : DateTime.Parse(txtSlotDate.Text);

                    bool isSlotStarted = false;
                    bool hasAvailableSeats = false;
                    int availableSeats = 0;

                    // Check available seats
                    if (int.TryParse(rowView["AvailableSeats"].ToString(), out availableSeats))
                    {
                        hasAvailableSeats = availableSeats >= SEATS_PER_BOOKING;
                    }

                    // Check if slot has started
                    if (TimeSpan.TryParse(rowView["StartTime"].ToString(), out TimeSpan slotStartTime))
                    {
                        DateTime slotDateTime = selectedDate.Add(slotStartTime);

                        if (selectedDate < DateTime.Today)
                        {
                            // Past date - all slots are unavailable
                            isSlotStarted = true;
                        }
                        else if (selectedDate == DateTime.Today)
                        {
                            // Today - check if slot time has passed
                            isSlotStarted = slotDateTime <= DateTime.Now;
                        }
                        else
                        {
                            // Future date - slots are available
                            isSlotStarted = false;
                        }
                    }

                    // Determine if slot should be enabled
                    bool shouldEnableSlot = hasAvailableSeats && !isSlotStarted;

                    if (!shouldEnableSlot)
                    {
                        rbSlot.Enabled = false;
                        slotCard.Attributes["class"] = (slotCard.Attributes["class"] ?? "") + " disabled";

                        // Add appropriate tooltip
                        if (!hasAvailableSeats)
                        {
                            slotCard.Attributes["title"] = "No seats available";
                            slotCard.Style["cursor"] = "not-allowed";
                        }
                        else if (isSlotStarted)
                        {
                            if (selectedDate < DateTime.Today)
                            {
                                slotCard.Attributes["title"] = "This slot is from a past date";
                            }
                            else
                            {
                                slotCard.Attributes["title"] = "This slot has already started";
                            }
                            slotCard.Style["cursor"] = "not-allowed";
                        }
                    }
                    else
                    {
                        rbSlot.Enabled = true;
                        string currentClass = slotCard.Attributes["class"] ?? "";
                        slotCard.Attributes["class"] = currentClass.Replace("disabled", "").Trim();
                        slotCard.Attributes["title"] = "Click to select this slot";
                        slotCard.Style["cursor"] = "pointer";
                    }

                    // Add data attributes for JavaScript
                    slotCard.Attributes["data-slot-id"] = rowView["ID"].ToString();
                    slotCard.Attributes["data-available-seats"] = availableSeats.ToString();
                    slotCard.Attributes["data-start-time"] = rowView["StartTime"].ToString();
                    slotCard.Attributes["data-duration"] = rowView["Duration"].ToString();
                }

                // Add visual indicators for slot status
                if (slotCard != null)
                {
                    DateTime selectedDate = string.IsNullOrEmpty(txtSlotDate.Text) ?
                        DateTime.Today : DateTime.Parse(txtSlotDate.Text);

                    // Remove existing status classes
                    string currentClass = slotCard.Attributes["class"] ?? "";
                    currentClass = currentClass.Replace("slot-past", "")
                                              .Replace("slot-today", "")
                                              .Replace("slot-future", "")
                                              .Replace("slot-full", "")
                                              .Replace("slot-available", "")
                                              .Trim();

                    // Add appropriate status class
                    if (selectedDate < DateTime.Today)
                    {
                        currentClass += " slot-past";
                    }
                    else if (selectedDate == DateTime.Today)
                    {
                        currentClass += " slot-today";
                    }
                    else
                    {
                        currentClass += " slot-future";
                    }

                    // Add availability class
                    int availableSeats = 0;
                    if (int.TryParse(rowView["AvailableSeats"].ToString(), out availableSeats))
                    {
                        if (availableSeats == 0)
                        {
                            currentClass += " slot-full";
                        }
                        else
                        {
                            currentClass += " slot-available";
                        }
                    }

                    slotCard.Attributes["class"] = currentClass.Trim();
                }
            }
        }
    }
}