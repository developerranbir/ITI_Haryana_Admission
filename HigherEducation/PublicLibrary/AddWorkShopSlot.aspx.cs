using HigherEducation.BusinessLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class AddWorkShopSlot : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        private string ITI_Id = "";
        private string ITI_Name = "";

        protected void Page_Load(object sender, EventArgs e)
        {
             //Session handling - uncomment when ready
            
            if (Session["Collegeid"] != null && Session["UserName"] != null)
            {
                ITI_Id = Session["Collegeid"].ToString();
                ITI_Name = Session["UserName"].ToString();
                litITIId.Text = ITI_Name;
            }
            else
            {
                Response.Redirect("Login.aspx");
                return;
            }
            

            // Temporary hardcoded values for testing
            //ITI_Id = "2";
            //ITI_Name = "GITI Ambala City";
            //litITIId.Text = ITI_Name;

            if (!IsPostBack)
            {
                DateTime currentDateTime = DateTime.Now;
                litCurrentDate.Text = currentDateTime.ToString("dddd, MMMM dd, yyyy");
                litCurrentTime.Text = currentDateTime.ToString("hh:mm tt");

                // Set default date to today
                txtSlotDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                UpdateSelectedDateDisplay();
                BindTimeSlots();
                BindExistingSlots();

                ClearMessage();
            }
        }

        protected void txtSlotDate_TextChanged(object sender, EventArgs e)
        {
            if (IsValidDateSelected())
            {
                UpdateSelectedDateDisplay();
                BindTimeSlots();
                BindExistingSlots();
                ClearMessage();
            }
            else
            {
                ClearForm();
                gvSlots.DataSource = new DataTable();
                gvSlots.DataBind();
            }
        }

        private bool IsValidDateSelected()
        {
            if (string.IsNullOrEmpty(txtSlotDate.Text))
                return false;

            if (!DateTime.TryParse(txtSlotDate.Text, out DateTime selectedDate))
                return false;

            // Check if date is a weekday (Monday to Friday)
            return IsWeekday(selectedDate);
        }

        private bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek >= DayOfWeek.Monday && date.DayOfWeek <= DayOfWeek.Friday;
        }

        private void UpdateSelectedDateDisplay()
        {
            if (DateTime.TryParse(txtSlotDate.Text, out DateTime selectedDate))
            {
                
                // Update grid title
                litGridTitle.Text = $"Workshop Slots for {selectedDate.ToString("dddd, MMMM dd, yyyy")}";
            }
        }

        private void BindTimeSlots()
        {
            try
            {
                ddlStartTime.Items.Clear();
                ddlEndTime.Items.Clear();

                ddlStartTime.Items.Add(new ListItem("-- Select Start Time --", ""));
                ddlEndTime.Items.Add(new ListItem("-- Select End Time --", ""));

                // Check if valid date is selected
                if (!IsValidDateSelected())
                {
                    btnSave.Enabled = false;
                    return;
                }

                DateTime selectedDate = DateTime.Parse(txtSlotDate.Text);
                DateTime currentTime = DateTime.Now;

                // If selected date is today, use current time logic
                // If selected date is in future, show all time slots
                DateTime startTime = selectedDate.AddHours(9); // 9 AM
                DateTime lastStartTime = selectedDate.AddHours(16.1); // 4 PM

                if (selectedDate.Date == DateTime.Today)
                {
                    // If current time is after 4 PM for today, disable slot creation
                    if (currentTime.TimeOfDay > lastStartTime.TimeOfDay)
                    {
                        ShowMessage("Workshop slot creation for today is only available until 4:00 PM.", "warning");
                        btnSave.Enabled = false;
                        return;
                    }

                    // Adjust start time based on current time for today
                    if (currentTime.TimeOfDay > startTime.TimeOfDay)
                    {
                        startTime = GetNextAvailableTimeSlot(currentTime);
                    }
                }

                // Generate time slots from calculated start time to 4 PM
                while (startTime <= lastStartTime)
                {
                    string timeText = startTime.ToString("hh:mm tt");
                    string timeValue = startTime.ToString("HH:mm");

                    ddlStartTime.Items.Add(new ListItem(timeText, timeValue));
                    startTime = startTime.AddMinutes(15);
                }

                // Enable/disable save button based on available slots and date validity
                btnSave.Enabled = (ddlStartTime.Items.Count > 1) && IsValidDateSelected();

                if (!btnSave.Enabled && IsValidDateSelected())
                {
                    ShowMessage("No available time slots remaining for selected date. Last start time is 4:00 PM.", "warning");
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/AddWorkShopSlot";
                clsLogger.ExceptionMsg = "BindTimeSlots";
                clsLogger.SaveException();
                ShowMessage("Error loading time slots. Please try again.", "danger");
            }
        }

        private DateTime GetNextAvailableTimeSlot(DateTime currentTime)
        {
            // Round up to the next 15-minute interval
            int minutes = currentTime.Minute;
            int remainder = minutes % 15;
            int minutesToAdd = remainder == 0 ? 15 : (15 - remainder);

            DateTime nextSlot = currentTime.AddMinutes(minutesToAdd);

            // Ensure we don't go past 4 PM
            DateTime lastStartTime = DateTime.Today.AddHours(16.1);
            return nextSlot > lastStartTime ? lastStartTime : nextSlot;
        }

        protected void ddlStartTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlStartTime.SelectedValue) && IsValidDateSelected())
                {
                    DateTime selectedDate = DateTime.Parse(txtSlotDate.Text);
                    DateTime selectedStart = selectedDate.Add(TimeSpan.Parse(ddlStartTime.SelectedValue));
                    DateTime dayEnd = selectedDate.AddHours(17); // 5 PM

                    ddlEndTime.Items.Clear();
                    ddlEndTime.Items.Add(new ListItem("-- Select End Time --", ""));

                    // Only show end times that are exactly 1, 2, 3, or 4 hours after start time
                    int[] allowedDurations = { 1, 2, 3, 4 };

                    foreach (int hours in allowedDurations)
                    {
                        DateTime endTime = selectedStart.AddHours(hours);

                        // Check if the end time is within business hours
                        if (endTime <= dayEnd)
                        {
                            string timeText = endTime.ToString("hh:mm tt");
                            string timeValue = endTime.ToString("HH:mm");
                            string displayText = $"{timeText} ({hours} hour{(hours > 1 ? "s" : "")})";

                            ddlEndTime.Items.Add(new ListItem(displayText, timeValue));
                        }
                    }

                    UpdateDurationDisplay();
                }
            }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/AddWorkShopSlot";
                clsLogger.ExceptionMsg = "ddlStartTime_SelectedIndexChanged";
                clsLogger.SaveException();
                ShowMessage("Error loading end times. Please try again.", "danger");
            }
        }

        private void UpdateDurationDisplay()
        {
            if (!string.IsNullOrEmpty(ddlStartTime.SelectedValue) && !string.IsNullOrEmpty(ddlEndTime.SelectedValue))
            {
                TimeSpan start = TimeSpan.Parse(ddlStartTime.SelectedValue);
                TimeSpan end = TimeSpan.Parse(ddlEndTime.SelectedValue);
                TimeSpan duration = end - start;

                double totalHours = duration.TotalHours;
                lblDuration.Text = $"Duration: {totalHours:0} hour{(totalHours > 1 ? "s" : "")}";

                if (duration.TotalHours < 1)
                {
                    lblDuration.CssClass = "text-danger small mt-1 d-block";
                    lblDuration.Text += " - Minimum 1 hour required";
                }
                else if (duration.TotalHours > 4)
                {
                    lblDuration.CssClass = "text-danger small mt-1 d-block";
                    lblDuration.Text += " - Maximum 4 hours allowed";
                }
                else
                {
                    lblDuration.CssClass = "text-success small mt-1 d-block";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    // Validate date selection
                    if (!IsValidDateSelected())
                    {
                        ShowMessage("Please select a valid weekday (Monday to Friday).", "danger");
                        return;
                    }

                    // Validate time selection
                    if (string.IsNullOrEmpty(ddlStartTime.SelectedValue) || string.IsNullOrEmpty(ddlEndTime.SelectedValue))
                    {
                        ShowMessage("Please select both start and end times.", "danger");
                        return;
                    }

                    // Validate available seats
                    if (!int.TryParse(txtAvailableSeats.Text.Trim(), out int availableSeats) || availableSeats < 1 || availableSeats > 20)
                    {
                        ShowMessage("Available seats must be a number between 1 and 20.", "danger");
                        return;
                    }

                    DateTime selectedDate = DateTime.Parse(txtSlotDate.Text);
                    TimeSpan startTime = TimeSpan.Parse(ddlStartTime.SelectedValue);
                    TimeSpan endTime = TimeSpan.Parse(ddlEndTime.SelectedValue);
                    DateTime currentTime = DateTime.Now;

                    // Validate if selected time is in the future (only for today)
                    if (selectedDate.Date == DateTime.Today.Date && selectedDate.Add(startTime) <= currentTime)
                    {
                        ShowMessage("Please select a start time that is in the future.", "danger");
                        return;
                    }

                    // Validate business rules
                    if (!ValidateTimeSlot(selectedDate, startTime, endTime))
                    {
                        return;
                    }

                    // Save to database using stored procedure
                    if (SaveWorkshopSlotToDatabase(selectedDate, startTime, endTime, txtRemarks.Text.Trim(), availableSeats))
                    {
                        ShowMessage("Workshop slot added successfully!", "success");
                        ClearForm();
                        BindExistingSlots();
                    }
                    else
                    {
                        ShowMessage("Failed to save workshop slot. Please try again.", "danger");
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/AddWorkShopSlot";
                    clsLogger.ExceptionMsg = "btnSave_Click";
                    clsLogger.SaveException();
                    ShowMessage($"Error saving slot: {ex.Message}", "danger");
                }
            }
        }

        private bool SaveWorkshopSlotToDatabase(DateTime slotDate, TimeSpan startTime, TimeSpan endTime, string remarks, int availableSeats)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_InsertWorkshopSlot", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        TimeSpan duration = endTime - startTime;
                        double totalHours = duration.TotalHours;

                        cmd.Parameters.AddWithValue("p_ITI_Id", ITI_Id);
                        cmd.Parameters.AddWithValue("p_SlotDate", slotDate);
                        cmd.Parameters.AddWithValue("p_StartTime", startTime.ToString(@"hh\:mm\:ss"));
                        cmd.Parameters.AddWithValue("p_EndTime", endTime.ToString(@"hh\:mm\:ss"));
                        cmd.Parameters.AddWithValue("p_Duration", totalHours);
                        cmd.Parameters.AddWithValue("p_TotalSeats", availableSeats);
                        cmd.Parameters.AddWithValue("p_AvailableSeats", availableSeats);
                        cmd.Parameters.AddWithValue("p_Remarks", remarks ?? string.Empty);

                        int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                        return rowsAffected > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/AddWorkShopSlot";
                    clsLogger.ExceptionMsg = "SaveWorkshopSlotToDatabase";
                    clsLogger.SaveException();
                    throw new Exception($"Database error: {ex.Message}");
                }
            }
        }

        private bool ValidateTimeSlot(DateTime slotDate, TimeSpan startTime, TimeSpan endTime)
        {
            // Check if start time is before end time
            if (startTime >= endTime)
            {
                ShowMessage("End time must be after start time.", "danger");
                return false;
            }

            // Check if within business hours (9 AM - 5 PM)
            TimeSpan businessStart = new TimeSpan(9, 0, 0);
            TimeSpan businessEnd = new TimeSpan(17, 0, 0);

            if (startTime < businessStart || endTime > businessEnd)
            {
                ShowMessage("Workshop slots must be between 9:00 AM and 5:00 PM.", "danger");
                return false;
            }

            // Check if duration is exactly 1, 2, 3, or 4 hours
            TimeSpan duration = endTime - startTime;
            int durationHours = (int)duration.TotalHours;

            if (durationHours < 1 || durationHours > 4 || duration.TotalHours != durationHours)
            {
                ShowMessage("Workshop slot must be exactly 1, 2, 3, or 4 hours in duration.", "danger");
                return false;
            }

            // Check for overlapping slots using stored procedure
            if (CheckOverlappingSlots(slotDate, startTime, endTime))
            {
                ShowMessage("This time slot overlaps with an existing workshop slot.", "danger");
                return false;
            }

            return true;
        }

        private bool CheckOverlappingSlots(DateTime slotDate, TimeSpan newStart, TimeSpan newEnd)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_CheckOverlappingSlots", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_ITI_Id", ITI_Id);
                        cmd.Parameters.AddWithValue("p_SlotDate", slotDate);
                        cmd.Parameters.AddWithValue("p_NewStart", newStart.ToString(@"hh\:mm\:ss"));
                        cmd.Parameters.AddWithValue("p_NewEnd", newEnd.ToString(@"hh\:mm\:ss"));

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/AddWorkShopSlot";
                    clsLogger.ExceptionMsg = "CheckOverlappingSlots";
                    clsLogger.SaveException();
                    ShowMessage($"Error checking overlapping slots: {ex.Message}", "danger");
                    return true; // Return true to prevent potential conflicts
                }
            }
        }

        private void BindExistingSlots()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    if (!IsValidDateSelected())
                    {
                        gvSlots.DataSource = new DataTable();
                        gvSlots.DataBind();
                        return;
                    }

                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_GetWorkshopSlots", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_ITI_Id", ITI_Id);
                        cmd.Parameters.AddWithValue("p_SlotDate", DateTime.Parse(txtSlotDate.Text));

                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            gvSlots.DataSource = dt;
                            gvSlots.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {

                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/AddWorkShopSlot";
                    clsLogger.ExceptionMsg = "BindExistingSlots";
                    clsLogger.SaveException();
                    ShowMessage($"Error loading existing slots: {ex.Message}", "danger");
                    gvSlots.DataSource = new DataTable();
                    gvSlots.DataBind();
                }
            }
        }

        protected void gvSlots_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Get the ID from DataKeys
                if (gvSlots.DataKeys[e.RowIndex] != null)
                {
                    string idValue = gvSlots.DataKeys[e.RowIndex].Value.ToString();

                    if (int.TryParse(idValue, out int slotId))
                    {
                        if (DeleteWorkshopSlot(slotId))
                        {
                            ShowMessage("Workshop slot deleted successfully!", "success");
                            BindExistingSlots();
                        }
                        else
                        {
                            ShowMessage("Error deleting slot. Please try again.", "danger");
                        }
                    }
                    else
                    {
                        ShowMessage("Invalid slot ID.", "danger");
                    }
                }
                else
                {
                    ShowMessage("Unable to find the slot to delete.", "danger");
                }
            }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/AddWorkShopSlot";
                clsLogger.ExceptionMsg = "gvSlots_RowDeleting";
                clsLogger.SaveException();
                ShowMessage($"Error deleting slot: {ex.Message}", "danger");
            }
        }

        private bool DeleteWorkshopSlot(int slotId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_DeleteWorkshopSlot", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("p_ID", slotId);
                        cmd.Parameters.AddWithValue("p_ITI_Id", ITI_Id);

                        int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/AddWorkShopSlot";
                    clsLogger.ExceptionMsg = "DeleteWorkshopSlot";
                    clsLogger.SaveException();
                    throw new Exception($"Database error: {ex.Message}");
                }
            }
        }

        protected void gvSlots_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                // Format StartTime
                Label lblStartTime = e.Row.FindControl("lblStartTime") as Label;
                if (lblStartTime != null && rowView["StartTime"] != DBNull.Value)
                {
                    string timeString = rowView["StartTime"].ToString();
                    if (TimeSpan.TryParse(timeString, out TimeSpan timeSpan))
                    {
                        lblStartTime.Text = DateTime.Today.Add(timeSpan).ToString("hh:mm tt");
                    }
                }

                // Format EndTime
                Label lblEndTime = e.Row.FindControl("lblEndTime") as Label;
                if (lblEndTime != null && rowView["EndTime"] != DBNull.Value)
                {
                    string timeString = rowView["EndTime"].ToString();
                    if (TimeSpan.TryParse(timeString, out TimeSpan timeSpan))
                    {
                        lblEndTime.Text = DateTime.Today.Add(timeSpan).ToString("hh:mm tt");
                    }
                }

                // Format Duration
                Label lblDuration = e.Row.FindControl("lblDuration") as Label;
                if (lblDuration != null && rowView["Duration"] != DBNull.Value)
                {
                    if (double.TryParse(rowView["Duration"].ToString(), out double duration))
                    {
                        lblDuration.Text = $"{duration:0} hour{(duration > 1 ? "s" : "")}";
                    }
                }

                // Handle Delete Button Visibility and Status
                LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;

                if (rowView["AvailableSeats"] != DBNull.Value && rowView["TotalSeats"] != DBNull.Value &&
                    rowView["SlotDate"] != DBNull.Value && rowView["StartTime"] != DBNull.Value)
                {
                    int availableSeats = Convert.ToInt32(rowView["AvailableSeats"]);
                    int totalSeats = Convert.ToInt32(rowView["TotalSeats"]);
                    int bookedSeats = totalSeats - availableSeats;

                    // Get slot date and time
                    DateTime slotDate = Convert.ToDateTime(rowView["SlotDate"]);
                    TimeSpan startTime;

                    if (rowView["StartTime"] is TimeSpan)
                    {
                        startTime = (TimeSpan)rowView["StartTime"];
                    }
                    else
                    {
                        string timeString = rowView["StartTime"].ToString();
                        TimeSpan.TryParse(timeString, out startTime);
                    }

                    DateTime slotDateTime = slotDate.Add(startTime);
                    DateTime currentDateTime = DateTime.Now;

                    // Calculate if the slot has already started or is in the past
                    bool isSlotStarted = slotDateTime <= currentDateTime;
                    bool isAllSeatsAvailable = (availableSeats == totalSeats);

                    // Show delete button only if:
                    // 1. All seats are available AND
                    // 2. The slot hasn't started yet (is in the future)
                    if (lnkDelete != null)
                    {
                        lnkDelete.Visible = (isAllSeatsAvailable && !isSlotStarted);

                        // Add tooltip for disabled delete button
                        if (!lnkDelete.Visible)
                        {
                            if (isSlotStarted)
                            {
                                lnkDelete.ToolTip = "Cannot delete slot that has already started or is in the past";
                            }
                            else if (!isAllSeatsAvailable)
                            {
                                lnkDelete.ToolTip = "Cannot delete slot with booked seats";
                            }
                        }
                        else
                        {
                            lnkDelete.ToolTip = "Delete this workshop slot";
                        }
                    }

                    // Update status label with more detailed information
                    if (lblStatus != null)
                    {
                        if (isSlotStarted)
                        {
                            if (availableSeats == totalSeats)
                            {
                                lblStatus.Text = "Completed (No bookings)";
                                lblStatus.CssClass = "status-booked";
                            }
                            else if (availableSeats == 0)
                            {
                                lblStatus.Text = "Completed (Fully booked)";
                                lblStatus.CssClass = "status-booked";
                            }
                            else
                            {
                                lblStatus.Text = $"Completed ({bookedSeats} booked)";
                                lblStatus.CssClass = "status-booked";
                            }
                        }
                        else
                        {
                            if (availableSeats == totalSeats)
                            {
                                lblStatus.Text = "Available";
                                lblStatus.CssClass = "status-available";
                            }
                            else if (availableSeats == 0)
                            {
                                lblStatus.Text = "Fully booked";
                                lblStatus.CssClass = "status-booked";
                            }
                            else
                            {
                                lblStatus.Text = $"{bookedSeats} booked";
                                lblStatus.CssClass = "status-booked";
                            }
                        }

                        // Add tooltip with time information
                        string timeStatus = isSlotStarted ? "Slot has already started" : "Slot is upcoming";
                        lblStatus.ToolTip = $"{timeStatus}. {availableSeats} of {totalSeats} seats available";
                    }
                }
                else
                {
                    // Hide delete button if data is missing
                    if (lnkDelete != null)
                    {
                        lnkDelete.Visible = false;
                        lnkDelete.ToolTip = "Cannot delete - incomplete slot information";
                    }

                    // Set default status if data is missing
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "Unknown";
                        lblStatus.CssClass = "text-muted";
                        lblStatus.ToolTip = "Slot information is incomplete";
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            ddlStartTime.SelectedIndex = 0;
            ddlEndTime.SelectedIndex = 0;
            txtAvailableSeats.Text = "";
            txtRemarks.Text = "";
            lblDuration.Text = "";
            ClearMessage(); // Also clear any messages
        }

        protected void cvAvailableSeats_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(txtAvailableSeats.Text.Trim()))
            {
                args.IsValid = false;
                return;
            }

            if (!int.TryParse(txtAvailableSeats.Text.Trim(), out int seats))
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = (seats >= 1 && seats <= 20);
        }

        protected void cvSlotDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(txtSlotDate.Text.Trim()))
            {
                args.IsValid = false;
                return;
            }

            if (!DateTime.TryParse(txtSlotDate.Text, out DateTime selectedDate))
            {
                args.IsValid = false;
                return;
            }

            // Validate that date is a weekday (Monday to Friday)
            args.IsValid = IsWeekday(selectedDate);
        }

        private void ShowMessage(string message, string type)
        {
            // Only set the message if we actually have a message to show
            if (!string.IsNullOrEmpty(message))
            {
                pnlMessage.CssClass = $"alert alert-{type} alert-dismissible fade show";
                lblMessage.Text = message;
                pnlMessage.Style["display"] = "block";

                // Register script to auto-hide message
                ScriptManager.RegisterStartupScript(this, GetType(), "hideMessage",
                    "setTimeout(function() { " +
                    "var alert = document.querySelector('.alert'); " +
                    "if(alert) { " +
                    "var closeBtn = alert.querySelector('.btn-close'); " +
                    "if(closeBtn) closeBtn.click(); " +
                    "} " +
                    "}, 5000);", true);
            }
            else
            {
                // Hide the message panel if no message
                pnlMessage.Style["display"] = "none";
            }
        }

        private void ClearMessage()
        {
            lblMessage.Text = "";
            pnlMessage.Style["display"] = "none";
        }

    }
}