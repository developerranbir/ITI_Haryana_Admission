using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class Subscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is already logged in
            if (Session["UserId"] == null)
            {
                Response.Redirect("login.aspx");
            }
            bindSubs();
            if (!IsPostBack)
            {

                BindDistricts();
            }
        }

        private void BindDistricts()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand("CALL BindDistrict();", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var districts = new List<KeyValuePair<int, string>>();
                    // Add default "Select District" option
                    districts.Add(new KeyValuePair<int, string>(0, "Select District"));
                    while (reader.Read())
                    {
                        districts.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(reader["value"]),
                            reader["text"].ToString()
                        ));
                    }

                    ddlDistrict.DataSource = districts;
                    ddlDistrict.DataTextField = "Value";
                    ddlDistrict.DataValueField = "Key";
                    ddlDistrict.DataBind();
                }
            }
        }

        protected DropDownList ddlDistrict
        {
            get { return (DropDownList)FindControl("ddlDistrict"); }
        }

        protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedDistrictCode = Convert.ToInt32(ddlDistrict.SelectedValue);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand("CALL BindITIs(@p_districtcode);", conn))
            {
                cmd.Parameters.AddWithValue("@p_districtcode", selectedDistrictCode);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var itis = new List<KeyValuePair<int, string>>();
                    while (reader.Read())
                    {
                        itis.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(reader["value"]),
                            reader["text"].ToString()
                        ));
                    }

                    DropDownList ddlITI = (DropDownList)FindControl("ddlITI");
                    if (ddlITI != null)
                    {
                        ddlITI.DataSource = itis;
                        ddlITI.DataTextField = "Value";
                        ddlITI.DataValueField = "Key";
                        ddlITI.DataBind();
                    }
                }
            }
        }

        protected void btnPremiumPlan_Click(object sender, EventArgs e)
        {
            // Validate mandatory parameters
            if (Session["UserId"] == null ||
                string.IsNullOrWhiteSpace(Session["Email"]?.ToString()) ||
                string.IsNullOrWhiteSpace(Session["Mobile"]?.ToString()) ||
                FindControl("ddlITI") == null ||
                string.IsNullOrWhiteSpace(((DropDownList)FindControl("ddlITI")).SelectedValue) ||
                ((DropDownList)FindControl("ddlITI")).SelectedValue == "0" ||
                string.IsNullOrWhiteSpace(txtStartDate.Text))
            {
                lblMessage.Text = "All fields are mandatory. Please fill in all required information.";
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            string email = Session["Email"].ToString();
            string mobile = Session["Mobile"].ToString();

            DropDownList ddlITI = (DropDownList)FindControl("ddlITI");
            int itiId = Convert.ToInt32(ddlITI.SelectedValue);

            string subscriptionType = "ReadingWithIssue"; // Premium plan type
            decimal amount = 500; // Example premium plan amount
            DateTime startDate = DateTime.Parse(txtStartDate.Text);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

            // Check for existing subscription in last 30 days
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(@"SELECT COUNT(*) FROM usersubscriptions 
                WHERE UserId = @p_UserId 
                AND ITIId = @p_ITIId 
                AND SubscriptionType = @p_SubscriptionType 
                AND StartDate >= DATE_SUB(@p_subStart, INTERVAL 30 DAY)", conn))
            {
                cmd.Parameters.AddWithValue("@p_UserId", userId);
                cmd.Parameters.AddWithValue("@p_ITIId", itiId);
                cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    ShowAlert("You have already applied for this subscription type in the last 30 days.", "warning");
                    return;
                }
            }

            // Check if maximum 20 students have already applied for this ITI in the last 30 days
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(@"SELECT COUNT(*) FROM usersubscriptions 
                WHERE ITIId = @p_ITIId 
                AND SubscriptionType = @p_SubscriptionType 
                AND StartDate >= DATE_SUB(@p_subStart, INTERVAL 30 DAY)", conn))
            {
                cmd.Parameters.AddWithValue("@p_ITIId", itiId);
                cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));

                conn.Open();
                int studentCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (studentCount >= 20)
                {
                    ShowAlert("Maximum 20 students can apply for this ITI in a 30 day duration. Please try another ITI or date.", "warning");
                    return;
                }
            }
            // Proceed with subscription
            string subscriptionId;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("CALL sp_SubscribeUser(@p_UserId, @p_ITIId, @p_SubscriptionType, @p_Amount,@p_subStart); SELECT LAST_INSERT_ID() AS SubscriptionId;", conn))
                {
                    cmd.Parameters.AddWithValue("@p_UserId", userId);
                    cmd.Parameters.AddWithValue("@p_ITIId", itiId);
                    cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                    cmd.Parameters.AddWithValue("@p_Amount", amount);
                    cmd.Parameters.AddWithValue("@p_subStart", DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd"));

                    subscriptionId = cmd.ExecuteScalar().ToString();
                }
                // Redirect to payment page with subscriptionId after entry success
                Response.Redirect("libraryPayment.aspx?sid=" + subscriptionId);
            }
            // Optionally, show confirmation (not needed if redirecting)
            //ShowAlert("ReadingOnly Subscription Applied! Please Make payment now to print the pass", "success");
        }

        protected void btnBasicPlan_Click(object sender, EventArgs e)
        {
            // Validate mandatory parameters
            if (Session["UserId"] == null ||
                string.IsNullOrWhiteSpace(Session["Email"]?.ToString()) ||
                string.IsNullOrWhiteSpace(Session["Mobile"]?.ToString()) ||
                FindControl("ddlITI") == null ||
                string.IsNullOrWhiteSpace(((DropDownList)FindControl("ddlITI")).SelectedValue) ||
                ((DropDownList)FindControl("ddlITI")).SelectedValue == "0" ||
                string.IsNullOrWhiteSpace(txtStartDate.Text))
            {
                lblMessage.Text = "All fields are mandatory. Please fill in all required information.";
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            string email = Session["Email"]?.ToString() ?? string.Empty;
            string mobile = Session["Mobile"]?.ToString() ?? string.Empty;

            DropDownList ddlITI = (DropDownList)FindControl("ddlITI");
            int itiId = ddlITI != null ? Convert.ToInt32(ddlITI.SelectedValue) : 0;

            string subscriptionType = "ReadingOnly";
            decimal amount = 100;
            DateTime startDate = DateTime.Parse(txtStartDate.Text);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

            // Check for existing subscription in last 30 days for this user
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(@"SELECT COUNT(*) FROM usersubscriptions 
                WHERE UserId = @p_UserId 
                AND ITIId = @p_ITIId 
                AND SubscriptionType = @p_SubscriptionType 
                AND StartDate >= DATE_SUB(@p_subStart, INTERVAL 30 DAY)", conn))
            {
                cmd.Parameters.AddWithValue("@p_UserId", userId);
                cmd.Parameters.AddWithValue("@p_ITIId", itiId);
                cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    ShowAlert("You have already applied for this subscription type in the last 30 days.", "warning");
                    return;
                }
            }

            // Check if maximum 20 students have already applied for this ITI in the last 30 days
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(@"SELECT COUNT(*) FROM usersubscriptions 
                WHERE ITIId = @p_ITIId 
                AND SubscriptionType = @p_SubscriptionType 
                AND StartDate >= DATE_SUB(@p_subStart, INTERVAL 30 DAY)", conn))
            {
                cmd.Parameters.AddWithValue("@p_ITIId", itiId);
                cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));

                conn.Open();
                int studentCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (studentCount >= 20)
                {
                    ShowAlert("Maximum 20 students can apply for this ITI in a 30 day duration. Please try another ITI or date.", "warning");
                    return;
                }
            }

            // Proceed with subscription
            string subscriptionId;
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("CALL sp_SubscribeUser(@p_UserId, @p_ITIId, @p_SubscriptionType, @p_Amount,@p_subStart); SELECT LAST_INSERT_ID() AS SubscriptionId;", conn))
                {
                    cmd.Parameters.AddWithValue("@p_UserId", userId);
                    cmd.Parameters.AddWithValue("@p_ITIId", itiId);
                    cmd.Parameters.AddWithValue("@p_SubscriptionType", subscriptionType);
                    cmd.Parameters.AddWithValue("@p_Amount", amount);
                    cmd.Parameters.AddWithValue("@p_subStart", startDate.ToString("yyyy-MM-dd"));
                    subscriptionId = cmd.ExecuteScalar().ToString();
                }
                // Redirect to payment page with subscriptionId after entry success
                Response.Redirect("libraryPayment.aspx?sid=" + subscriptionId);
                        //ShowAlert("ReadingOnly Subscription Applied! Please Make payment now to print the pass", "success");
            }
        }


        private void bindSubs()
        {
            if (Session["UserId"] == null)
            {
                lblMessage.Text = "User not logged in.";
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand("CALL userSubscriptionList(@p_UserId);", conn))
            {
                cmd.Parameters.AddWithValue("@p_UserId", userId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var dt = new System.Data.DataTable();
                    dt.Load(reader);

                    GridView gvSubscriptions = (GridView)FindControl("gvSubscriptions");
                    if (gvSubscriptions != null)
                    {
                        gvSubscriptions.DataSource = dt;
                        gvSubscriptions.DataBind();
                    }
                }
            }
        }


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

        protected void gvSubscriptions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;
            string subscriptionId = e.CommandArgument.ToString();

            if (commandName == "Pending")
            {
                // Handle payment logic for the subscription
                // Example: Redirect to payment page or show payment modal
                lblAlertMessage.Text = "Redirecting to payment for Subscription ID: " + subscriptionId;
                pnlAlert.Visible = true;
            }
            else if (commandName == "Completed")
            {
                // Handle print pass logic for the subscription
                // Example: Generate and show printable pass
                lblAlertMessage.Text = "Generating pass for Subscription ID: " + subscriptionId;
                pnlAlert.Visible = true;
            }
        }

    }
}