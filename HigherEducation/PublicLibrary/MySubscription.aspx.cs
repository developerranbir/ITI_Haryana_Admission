using HigherEducation.BusinessLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class MySubscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bindSubs();
        }

        private void bindSubs()
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
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
                        gvSubscriptions.DataSource = dt;
                        gvSubscriptions.DataBind();


                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/MySubscription";
                clsLogger.ExceptionMsg = "bindSubs";
                clsLogger.SaveException();
            }
        }
    }
}