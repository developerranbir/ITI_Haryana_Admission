using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class Workshop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is already logged in
            if (Session["UserId"] == null)
            {
                Response.Redirect("login.aspx");
            }

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

        

        protected void ddldistrict_SelectedIndexChanged1(object sender, EventArgs e)
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
    }
}