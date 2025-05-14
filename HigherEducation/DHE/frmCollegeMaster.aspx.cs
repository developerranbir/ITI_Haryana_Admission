using HigherEducation.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.DHE
{
    public partial class frmCollegeMaster : System.Web.UI.Page
    {
        public void BindCollegeType()
        {
            try
            {
                clsCollege CS = new clsCollege();
                DataTable dt = new DataTable();
                dt = CS.BindCollegeType();
                if (dt.Rows.Count > 0)
                {
                    ddlCollegeType.DataSource = dt;
                    ddlCollegeType.DataTextField = "Text";
                    ddlCollegeType.DataValueField = "Value";
                    ddlCollegeType.DataBind();
                    ddlCollegeType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                    ddlCollegeType.Focus();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmCollegeMaster";
                clsLogger.ExceptionMsg = "BindCollegeType";
                clsLogger.SaveException();
            }
        }
        public void BindDistrict()
        {
            try
            {
                clsCollege CS = new clsCollege();
                DataTable dt = new DataTable();
                dt = CS.BindDistrict();
                if (dt.Rows.Count > 0)
                {
                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "Text";
                    ddlDistrict.DataValueField = "Value";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                    ddlDistrict.Focus();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmCollegeMaster";
                clsLogger.ExceptionMsg = "BindDistrict";
                clsLogger.SaveException();
            }
        }
        public void Clear()
        {
            txtITIName.Text = "";
            ddlCollegeType.SelectedValue = "0";
            txtInstAddress.Text = "";
            ddlDistrict.SelectedValue = "0";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtWebsite.Text = "";
            ddlEduMode.SelectedValue = "0";
            txtPrinc.Text = "";
            txtPrincePhone.Text = "";
            txtNodalOff.Text = "";
            txtNodalOffPhone.Text = "";
            txtAttraction.Text = "";
            ddlAffType.SelectedValue = "0";
            txtRating.Text = "";
            txtMISCode.Text = "";
            btnSubmit.Text = "Save";
            ViewState["CollegeID"] = null;
            //txtSearch.Text= "";
        }
        public string CheckDuplicateCollege(string CollegeName)
        {
            string val = "";
            try
            {
                clsCollege CS = new clsCollege();
                DataTable dt = new DataTable();
                dt = CS.CheckDuplicateCollege(CollegeName);
                if (dt.Rows.Count > 0)
                {
                    val = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmCollegeMaster";
                clsLogger.ExceptionMsg = "CheckDuplicateCollege";
                clsLogger.SaveException();
            }
            return val;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = "1";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
            {
                Response.Redirect("~/DHE/frmlogin.aspx", true);
            }
           
            if (!Page.IsPostBack)
            {
               
                BindDistrict();
                BindCollegeType();
            }
            eSessionMgmt.SetCookie();

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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    string confirmValue = Request.Form["confirm_value"];
                    if (txtITIName.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Institute Name");
                        return;
                    }
                    if (ddlCollegeType.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select Institute Type");
                        return;
                    }
                    if (txtInstAddress.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Institute Address");
                        return;
                    }
                    if (ddlDistrict.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select District");
                        return;
                    }
                    if (txtEmail.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Email Address");
                        return;
                    }
                    if (txtMobile.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Phone/ Mobile Number");
                        return;
                    }
                    if (ddlAffType.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select NCVT/SCVT Council");
                        return;
                    }

                    clsCollege cg = new clsCollege();
                    cg.collegename = txtITIName.Text.Trim();
                    cg.CollegeType = ddlCollegeType.SelectedValue;
                    cg.collegeAdd = txtInstAddress.Text.Trim();
                    cg.distcode = ddlDistrict.SelectedValue;
                    cg.email = txtEmail.Text.Trim();
                    cg.mobile = txtMobile.Text.Trim();
                    cg.website = txtWebsite.Text.Trim();
                    cg.EduMode = ddlEduMode.SelectedValue;
                    cg.collegePrincipal = txtPrinc.Text.Trim();
                    cg.collegePrincipalPhone = txtPrincePhone.Text.Trim();
                    cg.collegeNodal = txtNodalOff.Text.Trim();
                    cg.collegeNodalPhone = txtNodalOffPhone.Text.Trim();
                    cg.collegeAttraction = txtAttraction.Text.Trim();
                    cg.collegeCounsile = ddlAffType.SelectedValue;
                    cg.institutecode = txtMISCode.Text.Trim();
                    cg.collegeRating = txtRating.Text.Trim();
                    cg.UserId = Convert.ToString(Session["UserId"]);
                    string IPAddress = GetIPAddress();
                    cg.IPAddress = IPAddress;
                    //cg.confirmValue = confirmValue;
                    if (btnSubmit.Text == "Save")
                    {
                        string a = "";
                        a = CheckDuplicateCollege(txtITIName.Text.Trim());
                        if (a != "")
                        {
                            clsAlert.AlertMsg(this, "Institue Name already Exists.");
                            return;
                        }
                        else
                        {
                            string s = cg.AddCollege();
                            if (s == "1")
                            {
                                string uId = cg.getCollegeID();
                                Clear();
                                clsAlert.AlertMsg(this, "ITI Added Successfully. User Id : "+uId+" and Password : test@123");
                                return;
                            }
                            else
                            {
                                clsAlert.AlertMsg(this, "ITI not Added.");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmCollegeMaster";
                clsLogger.ExceptionMsg = "btnSubmit_Click";
                clsLogger.SaveException();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        
    }
}