using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using HigherEducation.BAL;
using HigherEducation.BusinessLayer;
using HigherEducation.DataAccess;
using HigherEducation.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using Ubiety.Dns.Core.Records;


namespace HigherEducation.DHE
{
    public partial class frmCourseMaster : System.Web.UI.Page
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        DataTable dtCourses = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = Convert.ToString(Session["UserType"]);
            eDISHAutil eSessionMgmt = new eDISHAutil();

            clsLoginUser.CheckSession(UserType);

            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
                return;

            }
            if (!Page.IsPostBack) { BindCourses(); Bind_Checkbox(); }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (
                    txtCourseName.Text.Trim() != "" &&
                    txtCourseName.Text != "" &&
                    ddlDuration.SelectedValue != "0" &&
                    ddltradetype.SelectedValue != "0" &&
                    ddlEduQualificationITI.SelectedValue != "0" &&
                    ddlUnitSize.SelectedValue != "0" &&
                    ddlIsWomen.SelectedValue != "0" &&
                    ddlIsSteno.SelectedValue != "0")
                {
                    if (check_course_exist() == false)
                    {
                        clsAlert.AlertMsg(this, "Course Name already exist");
                        return;
                    }
                    string Checkbox_Selected = check_checkbox_checked();

                    using (MySqlCommand cmd = new MySqlCommand("Add_Course", vconnHE))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (vconnHE.State == ConnectionState.Closed) vconnHE.Open();
                        cmd.Parameters.AddWithValue("p_name", txtCourseName.Text.Trim());
                        cmd.Parameters.AddWithValue("p_duration", ddlDuration.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("p_tradetype", ddltradetype.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("p_EduQualificationITI", ddlEduQualificationITI.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("p_UnitSize", ddlUnitSize.SelectedItem.Text.Trim());
                        cmd.Parameters.AddWithValue("p_iswomen", ddlIsWomen.SelectedItem.Value.Trim());
                        cmd.Parameters.AddWithValue("p_issteno", ddlIsSteno.SelectedItem.Value.Trim());
                        cmd.Parameters.AddWithValue("p_ipaddress", GetIPAddress());
                        cmd.Parameters.AddWithValue("p_createuser", Session["UserID"].ToString());
                        cmd.Parameters.AddWithValue("P_Checkbox_Selected", Checkbox_Selected);
                        cmd.ExecuteNonQuery();
                        //Add disablity for the course in data

                        { }
                        clsAlert.AlertMsg(this, "Course Added");
                        Clear(); BindCourses();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsAlert.AlertMsg(this, ex.Message);

            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private string check_checkbox_checked()
        {
            String DataChecked = "";
            try
            {
                for (int i = 0; i < chkdisability.Items.Count; i++)
                {
                    if (chkdisability.Items[i].Selected)
                    {
                        DataChecked = DataChecked + chkdisability.Items[i].Value.ToString() + ",";
                    }
                }
            }
            catch { }

            DataChecked = DataChecked.TrimEnd(',');
            return DataChecked;

        }

        private bool check_course_exist()
        {
            //check_coursename_exist

            DataTable courses = new DataTable();
            using (MySqlCommand cmd = new MySqlCommand("check_coursename_exist", vconnHE))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_name", txtCourseName.Text.Trim());
                if (vconnHE.State == ConnectionState.Closed) vconnHE.Open();
                using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                {
                    adp.Fill(courses);
                    if (courses.Rows.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        private void Bind_Checkbox()
        {
            //GetDisabilityMaster
            DataTable dtDisability = new DataTable();
            using (MySqlCommand cmd = new MySqlCommand("GetDisabilityMaster", vconnHE))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (vconnHE.State == ConnectionState.Closed) vconnHE.Open();
                using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                {
                    adp.Fill(dtDisability);
                    if (dtDisability.Rows.Count > 0)
                    {
                        chkdisability.DataValueField = "id";
                        chkdisability.DataTextField = "disability";
                        chkdisability.DataSource = dtDisability;
                        chkdisability.DataBind();
                    }
                }
            }
        }
        private void BindCourses()
        {
            //GetAllCourses

            using (MySqlCommand cmd = new MySqlCommand("GetAllCourses", vconnHE))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (vconnHE.State == ConnectionState.Closed) vconnHE.Open();
                using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                {
                    adp.Fill(dtCourses);
                    if (dtCourses.Rows.Count > 0)
                    {
                        GdvCourses.DataSource = dtCourses;
                        GdvCourses.DataBind();
                        GdvCourses.UseAccessibleHeader = true;
                        GdvCourses.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
        public string GetIPAddress()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }

        public void Clear()
        {
            txtCourseName.Text = "";
            ddlDuration.SelectedValue = "0";
            ddltradetype.SelectedValue = "0";
            ddlEduQualificationITI.SelectedValue = "0";
            ddlUnitSize.SelectedValue = "0";
            ddlIsWomen.SelectedValue = "0";
            ddlIsSteno.SelectedValue = "0";
            //ddlIsDual.SelectedValue = "0";


        }
    }
}