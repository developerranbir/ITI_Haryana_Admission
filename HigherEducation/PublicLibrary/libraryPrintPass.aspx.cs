using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;

namespace HigherEducation.PublicLibrary
{
    public partial class libraryPrintPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if SubscriptionId exists in query string
                if (Request.QueryString["id"] != null || Request.QueryString["subscriptionid"] != null)
                {
                    string subscriptionId = Request.QueryString["id"] ?? Request.QueryString["subscriptionid"];
                    LoadPassDetails(subscriptionId);
                }
                else
                {
                    ShowError("Invalid request. No Subscription ID provided.");
                }
            }
        }

        private void LoadPassDetails(string subscriptionId)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "sp_LoadPassDetails";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_SubscriptionId", subscriptionId);
                        cmd.Parameters.AddWithValue("@p_UserId", Session["UserId"]);
                        conn.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the pass details
                                lblSubscriptionId.Text = reader["SubscriptionId"].ToString();
                                lblPassholderName.Text = reader["UserName"].ToString();
                                lblAmountPaid.Text = string.Format("₹{0:0.00}", reader["Amount"]);
                                lblCourse.Text = reader["SubscriptionType"].ToString();
                                InstituteName.Text = reader["collegename"].ToString();

                                // Format dates
                                DateTime validUpto = Convert.ToDateTime(reader["EndDate"]);
                                lblValidUpto.Text = validUpto.ToString("dd MMMM yyyy");

                                DateTime issuedDate = Convert.ToDateTime(reader["StartDate"]);
                                lblIssuedDate.Text = issuedDate.ToString("dd MMMM yyyy");

                                // Set logo if available


                                // Set page title
                                Page.Title = "ITI Pass - " + reader["PassholderName"].ToString();

                                // Auto print after 1 second (optional)
                                ScriptManager.RegisterStartupScript(this, GetType(), "Print", "setTimeout(function() { window.print(); }, 1000);", true);
                            }
                            else
                            {
                                ShowError("Pass not found or payment is still pending. Please complete the payment first.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/libraryPrintPass";
                clsLogger.ExceptionMsg = "LoadPassDetails";
                clsLogger.SaveException();
            }
}

        private void ShowError(string errorMessage)
        {
            pnlError.Visible = true;
            lblError.Text = errorMessage;

            // Hide other elements
            lblSubscriptionId.Visible = false;
            lblPassholderName.Visible = false;
            lblAmountPaid.Visible = false;
            lblValidUpto.Visible = false;
            lblCourse.Visible = false;
            lblIssuedDate.Visible = false;
            imgLogo.Visible = false;
        }

       
    }
}

