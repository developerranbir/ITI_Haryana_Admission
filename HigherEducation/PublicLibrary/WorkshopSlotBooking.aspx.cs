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

namespace HigherEducation.PublicLibrary
{
    public partial class WorkshopSlotBooking : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        private int selectedSlotId = 0;
        private decimal hourlyRate = 300; // ₹300 per hour
        private const int SEATS_PER_BOOKING = 1; // Single seat per booking

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime currentDateTime = DateTime.Now;
                litCurrentDate.Text = currentDateTime.ToString("dddd, MMMM dd, yyyy");
                litCurrentTime.Text = currentDateTime.ToString("hh:mm tt");

                BindDistricts();
                ClearMessage();
            }
            else
            {
                // Restore selected slot on postback
                if (ViewState["SelectedSlotId"] != null)
                {
                    selectedSlotId = (int)ViewState["SelectedSlotId"];
                }
            }
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
                }
                catch (Exception ex)
                {
                    LogError("BindDistricts", ex);
                    ShowMessage("Error loading districts. Please try again.", "danger");
                }
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlDistrict.SelectedValue))
            {
                BindITIsByDistrict();
                ClearSlotSelection();
            }
            else
            {
                ddlITI.Items.Clear();
                ddlITI.Items.Add(new ListItem("-- Select ITI --", ""));
                pnlSlots.Visible = false;
            }
        }

        private void BindITIsByDistrict()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("BindITIs", conn))
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
                    LogError("BindITIsByDistrict", ex);
                    ShowMessage("Error loading ITIs. Please try again.", "danger");
                }
            }
        }

        protected void ddlITI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlITI.SelectedValue))
            {
                BindAvailableSlots();
                pnlSlots.Visible = true;
                ClearSlotSelection();
            }
            else
            {
                pnlSlots.Visible = false;
                pnlAmount.Visible = false;
                pnlBookingForm.Visible = false;
            }
        }

        private void BindAvailableSlots()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_GetAvailableSlotsByITI", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_ITI_Id", ddlITI.SelectedValue);
                        cmd.Parameters.AddWithValue("p_SlotDate", DateTime.Today);

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }

                        // Filter slots where start time is greater than current time
                        DataTable filteredSlots = FilterSlotsByCurrentTime(dt);

                        if (filteredSlots.Rows.Count > 0)
                        {
                            rptSlots.DataSource = filteredSlots;
                            rptSlots.DataBind();
                            lblNoSlots.Visible = false;

                            // Restore selected slot if exists, otherwise auto-select first
                            if (ViewState["SelectedSlotId"] != null)
                            {
                                RestoreSelectedSlot();
                            }
                            else
                            {
                                SelectFirstAvailableSlot();
                            }
                        }
                        else
                        {
                            rptSlots.DataSource = null;
                            rptSlots.DataBind();
                            lblNoSlots.Text = "No available slots for today. All slots have either passed or are fully booked.";
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
                    LogError("BindAvailableSlots", ex);
                    ShowMessage("Error loading available slots. Please try again.", "danger");
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

        private void SelectFirstAvailableSlot()
        {
            bool slotSelected = false;

            foreach (RepeaterItem item in rptSlots.Items)
            {
                RadioButton rbSlot = (RadioButton)item.FindControl("rbSlot");
                HtmlGenericControl slotCard = (HtmlGenericControl)item.FindControl("slotCard");

                if (rbSlot != null && rbSlot.Enabled && slotCard != null && !slotCard.Attributes["class"].Contains("full-slot"))
                {
                    rbSlot.Checked = true;
                    rbSlot_CheckedChanged(rbSlot, new EventArgs());
                    slotSelected = true;
                    break;
                }
            }

            // If no slot was selected (all are disabled), show appropriate message
            if (!slotSelected && rptSlots.Items.Count > 0)
            {
                lblNoSlots.Text = "All available slots for today have already started. Please check back for future slots.";
                lblNoSlots.Visible = true;
            }
        }

        protected void rptSlots_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;

                // Set Slot ID
                Literal litSlotId = (Literal)e.Item.FindControl("litSlotId");
                litSlotId.Text = rowView["ID"].ToString();

                // Format Time
                Literal litTime = (Literal)e.Item.FindControl("litTime");
                if (TimeSpan.TryParse(rowView["StartTime"].ToString(), out TimeSpan startTime) &&
                    TimeSpan.TryParse(rowView["EndTime"].ToString(), out TimeSpan endTime))
                {
                    DateTime startDateTime = DateTime.Today.Add(startTime);
                    DateTime endDateTime = DateTime.Today.Add(endTime);

                    litTime.Text = $"{startDateTime:hh:mm tt} - {endDateTime:hh:mm tt}";

                    // Check if slot has already started
                    Label lblSlotStatus = (Label)e.Item.FindControl("lblSlotStatus");
                    if (lblSlotStatus != null)
                    {
                        if (startDateTime <= DateTime.Now)
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
                }

                // Format Duration
                Literal litDuration = (Literal)e.Item.FindControl("litDuration");
                if (double.TryParse(rowView["Duration"].ToString(), out double duration))
                {
                    litDuration.Text = $"{duration:0} hour{(duration > 1 ? "s" : "")}";
                }

                // Available Seats
                Literal litAvailableSeats = (Literal)e.Item.FindControl("litAvailableSeats");
                int availableSeats = Convert.ToInt32(rowView["AvailableSeats"]);
                litAvailableSeats.Text = availableSeats.ToString();

                // Calculate and display amount (for single seat)
                Literal litAmount = (Literal)e.Item.FindControl("litAmount");
                if (double.TryParse(rowView["Duration"].ToString(), out double hours))
                {
                    decimal amount = (decimal)hours * hourlyRate;
                    litAmount.Text = amount.ToString("0");
                }

                // Store additional slot information for booking
                HiddenField hfWorkshopDate = (HiddenField)e.Item.FindControl("hfWorkshopDate");
                HiddenField hfWorkshopTime = (HiddenField)e.Item.FindControl("hfWorkshopTime");
                HiddenField hfWorkshopDuration = (HiddenField)e.Item.FindControl("hfWorkshopDuration");

                if (hfWorkshopDate != null) hfWorkshopDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
                if (hfWorkshopTime != null && TimeSpan.TryParse(rowView["StartTime"].ToString(), out TimeSpan slotTime))
                {
                    hfWorkshopTime.Value = slotTime.ToString();
                }
                if (hfWorkshopDuration != null) hfWorkshopDuration.Value = rowView["Duration"].ToString();

                // Disable radio button if no seats available or slot has started
                RadioButton rbSlot = (RadioButton)e.Item.FindControl("rbSlot");
                HtmlGenericControl slotCard = (HtmlGenericControl)e.Item.FindControl("slotCard");

                if (TimeSpan.TryParse(rowView["StartTime"].ToString(), out TimeSpan slotStartTime))
                {
                    DateTime slotDateTime = DateTime.Today.Add(slotStartTime);
                    bool isSlotStarted = slotDateTime <= DateTime.Now;
                    bool hasAvailableSeats = availableSeats >= SEATS_PER_BOOKING;

                    if (!hasAvailableSeats || isSlotStarted)
                    {
                        rbSlot.Enabled = false;
                        if (slotCard != null)
                        {
                            slotCard.Attributes["class"] = "slot-card full-slot";
                            if (isSlotStarted)
                            {
                                // Add tooltip or additional info for started slots
                                slotCard.Attributes["title"] = "This slot has already started";
                            }
                        }
                    }
                    else
                    {
                        rbSlot.Enabled = true;
                        if (slotCard != null)
                        {
                            slotCard.Attributes["class"] = "slot-card";
                        }
                    }
                }
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

                if (int.TryParse(litSlotId.Text, out selectedSlotId))
                {
                    // Store the selected slot in ViewState for postback
                    ViewState["SelectedSlotId"] = selectedSlotId;

                    // Calculate amount for single seat
                    if (double.TryParse(litDuration.Text.Replace(" hours", "").Replace(" hour", ""), out double hours))
                    {
                        decimal totalAmount = (decimal)hours * hourlyRate;
                        litTotalAmount.Text = totalAmount.ToString("0");
                        litSelectedDuration.Text = litDuration.Text;

                        pnlAmount.Visible = true;
                        pnlBookingForm.Visible = true;
                    }
                }
            }
            else
            {
                pnlAmount.Visible = false;
                pnlBookingForm.Visible = false;
                ViewState["SelectedSlotId"] = null;
            }
        }
        protected void btnBookSlot_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && selectedSlotId > 0)
            {
                try
                {
                    // Check seat availability for single seat
                    if (!CheckSeatAvailability(selectedSlotId))
                    {
                        ShowMessage("Sorry, this slot is no longer available. Please select a different slot.", "danger");
                        BindAvailableSlots();
                        return;
                    }

                    // Check if user already booked this slot (prevent duplicate booking)
                    if (CheckDuplicateBooking(txtMobileNumber.Text.Trim(), selectedSlotId))
                    {
                        ShowMessage("You have already booked this workshop slot. Each user can book only one seat per slot.", "warning");
                        return;
                    }

                    // Save booking with single seat
                    if (SaveBooking(selectedSlotId))
                    {
                        ShowMessage("Workshop slot booked successfully! You have reserved 1 seat.", "success");
                        ClearForm();
                        BindAvailableSlots();
                    }
                    else
                    {
                        ShowMessage("Failed to book workshop slot. Please try again.", "danger");
                    }
                }
                catch (Exception ex)
                {
                    LogError("btnBookSlot_Click", ex);
                    ShowMessage($"Error booking slot: {ex.Message}", "danger");
                }
            }
            else
            {
                ShowMessage("Please select a workshop slot to book.", "warning");
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
                    LogError("CheckSeatAvailability", ex);
                    return false;
                }
            }
            return false;
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
                                return Convert.ToInt32(reader["IsDuplicate"]) == 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError("CheckDuplicateBooking", ex);
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
                    using (MySqlCommand cmd = new MySqlCommand("sp_SaveBooking", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Get selected slot details
                        DataRow slotDetails = GetSlotDetails(slotId);
                        if (slotDetails == null)
                        {
                            ShowMessage("Slot details not found. Please try again.", "danger");
                            return false;
                        }

                        // Calculate booking amount
                        double duration = Convert.ToDouble(slotDetails["Duration"]);
                        decimal bookingAmount = (decimal)duration * hourlyRate;

                        // Set parameters according to new table structure
                        cmd.Parameters.AddWithValue("p_SlotID", slotId);
                        cmd.Parameters.AddWithValue("p_FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("p_Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("p_MobileNumber", txtMobileNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("p_DistrictId", Convert.ToInt32(ddlDistrict.SelectedValue));
                        cmd.Parameters.AddWithValue("p_District", ddlDistrict.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("p_ITI_Id", Convert.ToInt32(ddlITI.SelectedValue));
                        cmd.Parameters.AddWithValue("p_ITI_Name", ddlITI.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("p_WorkshopDate", DateTime.Today);
                        cmd.Parameters.AddWithValue("p_WorkshopTime", TimeSpan.Parse(slotDetails["StartTime"].ToString()));
                        cmd.Parameters.AddWithValue("p_WorkshopDuration", Convert.ToInt32(duration));
                        cmd.Parameters.AddWithValue("p_BookingAmount", bookingAmount);

                        object result = cmd.ExecuteScalar();
                        return result != null && Convert.ToInt32(result) > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    LogError("SaveBooking", ex);
                    if (ex.Message.Contains("No seats available") || ex.Message.Contains("No seats available"))
                    {
                        ShowMessage("Sorry, this slot is no longer available. Please select a different slot.", "danger");
                    }
                    else if (ex.Message.Contains("already booked"))
                    {
                        ShowMessage("You have already booked this workshop slot. Each user can book only one seat per slot.", "warning");
                    }
                    else
                    {
                        throw new Exception($"Database error: {ex.Message}");
                    }
                    return false;
                }
            }
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
                    LogError("GetSlotDetails", ex);
                    return null;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtMobileNumber.Text = "";
            pnlAmount.Visible = false;
            pnlBookingForm.Visible = false;
            ClearSlotSelection();
            ViewState["SelectedSlotId"] = null;
        }

        private void ClearSlotSelection()
        {
            selectedSlotId = 0;
            ViewState["SelectedSlotId"] = null;

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

        private void ShowMessage(string message, string type)
        {
            if (!string.IsNullOrEmpty(message))
            {
                pnlMessage.CssClass = $"alert alert-{type} alert-dismissible fade show";
                lblMessage.Text = message;
                pnlMessage.Style["display"] = "block";

                string script = $@"
                    setTimeout(function() {{
                        var alertPanel = document.getElementById('{pnlMessage.ClientID}');
                        if(alertPanel) {{
                            alertPanel.style.display = 'none';
                        }}
                    }}, 5000);";

                ScriptManager.RegisterStartupScript(this, GetType(), "hideMessage", script, true);
            }
        }

        private void ClearMessage()
        {
            lblMessage.Text = "";
            pnlMessage.Style["display"] = "none";
        }

        private void LogError(string methodName, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in {methodName}: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
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

                    if (rbSlot != null && litSlotId != null && int.TryParse(litSlotId.Text, out int slotId))
                    {
                        if (slotId == selectedSlotId)
                        {
                            rbSlot.Checked = true;
                            pnlAmount.Visible = true;
                            pnlBookingForm.Visible = true;
                            break;
                        }
                    }
                }
            }
        }
    }

}