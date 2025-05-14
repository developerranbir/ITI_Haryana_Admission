using HigherEducation.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.DHE
{
    public partial class frmEditCollegeMaster : System.Web.UI.Page
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
                clsLogger.ExceptionPage = "HigherEducation/frmEditCollegeMaster";
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
                clsLogger.ExceptionPage = "HigherEducation/frmEditCollegeMaster";
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
            ddlExServicemen.SelectedValue = "00";
            ddlDeafDumb.SelectedValue = "00";
            ddlPPP.SelectedValue = "00";
            txtPrinc.Text = "";
            txtPrincePhone.Text = "";
            txtNodalOff.Text = "";
            txtNodalOffPhone.Text = "";
            txtAttraction.Text = "";
            ddlAffType.SelectedValue = "0";
            txtRating.Text = "";
            txtMISCode.Text = "";
            btnSubmit.Text = "Update";
            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            ddlActive.SelectedValue = "0";
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
                clsLogger.ExceptionPage = "HigherEducation/frmEditCollegeMaster";
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
                DisableAll();
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
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    int i=GetITIDetails();
                    if (i > 0)
                    { 
                        EnableAll();
                        txtID.Enabled = false;
                    }
                    else
                    {
                        Clear();
                        DisableAll();
                        clsAlert.AlertMsg(this, "Institue not found.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmEditCollegeMaster";
                clsLogger.ExceptionMsg = "btnSearch_Click";
                clsLogger.SaveException();
            }
        }

        public int GetITIDetails()
        {
            int i = 0;
            try
            {
                clsCollege cg = new clsCollege();
                if (Convert.ToString(Session["UserType"]) == "1")// State Level User Only
                {
                    string id = Regex.Replace(txtID.Text, "[dsditi]", "");
                    cg.collegeid = Convert.ToInt32(id);
                }

                DataTable dt = new DataTable();
                dt = cg.GetITIDetails();

                if (dt.Rows.Count > 0)
                {
                    txtITIName.Text = dt.Rows[0]["collegename"].ToString();
                    Session["Iname"] = dt.Rows[0]["collegename"].ToString();
                    ddlCollegeType.SelectedValue = dt.Rows[0]["collegetype"].ToString();
                    txtInstAddress.Text = dt.Rows[0]["address"].ToString();
                    ddlDistrict.SelectedValue = dt.Rows[0]["LGdDistrictcode"].ToString();
                    txtEmail.Text = dt.Rows[0]["emailid"].ToString();
                    txtMobile.Text = dt.Rows[0]["phoneno"].ToString();
                    txtWebsite.Text = dt.Rows[0]["website"].ToString();
                    ddlEduMode.SelectedValue = dt.Rows[0]["iswomen"].ToString();
                    txtPrinc.Text = dt.Rows[0]["Principal_Name"].ToString();
                    txtPrincePhone.Text = dt.Rows[0]["Principal_PhoneNo"].ToString();
                    txtNodalOff.Text = dt.Rows[0]["NodalAdmissions"].ToString();
                    txtNodalOffPhone.Text = dt.Rows[0]["NodalAdm_PhoneNo"].ToString();
                    txtAttraction.Text = dt.Rows[0]["MainAttract"].ToString();
                    ddlAffType.SelectedValue = dt.Rows[0]["Council"].ToString();
                    txtMISCode.Text= dt.Rows[0]["misncvtid"].ToString();
                    txtRating.Text= dt.Rows[0]["InstituteRating"].ToString();
                    ddlActive.SelectedValue = dt.Rows[0]["collegeid_status"].ToString();
                    ddlExServicemen.SelectedValue = dt.Rows[0]["isExserviceMan"].ToString();
                    ddlDeafDumb.SelectedValue = dt.Rows[0]["isDumbAndDeaf"].ToString();
                    ddlPPP.SelectedValue = dt.Rows[0]["isppp"].ToString();
                    ++i;
                }
                else
                {
                    //  txtAffiliated.Text = "-";

                }
                
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmEditCollegeMaster";
                clsLogger.ExceptionMsg = "btnSearch_Click";
                clsLogger.SaveException();
            }
            return i;
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
                    if (ddlExServicemen.SelectedValue == "00")
                    {
                        clsAlert.AlertMsg(this, "Please Select Is ITI for Ex-Serviceman");
                        return;
                    }
                    if (ddlDeafDumb.SelectedValue == "00")
                    {
                        clsAlert.AlertMsg(this, "Please Select Is ITI for Deaf & Dumb");
                        return;
                    }
                    if (ddlPPP.SelectedValue == "00")
                    {
                        clsAlert.AlertMsg(this, "Please Select Is ITI for Public-Private Partnerships");
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
                    if (ddlActive.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select Is Active");
                        return;
                    }

                    clsCollege cg = new clsCollege();
                    string id = Regex.Replace(txtID.Text, "[dsditi]", "");
                    cg.collegeid = Convert.ToInt32(id);
                    cg.collegename = txtITIName.Text.Trim();
                    cg.CollegeType = ddlCollegeType.SelectedValue;
                    cg.collegeAdd = txtInstAddress.Text.Trim();
                    cg.distcode = ddlDistrict.SelectedValue;
                    cg.email = txtEmail.Text.Trim();
                    cg.mobile = txtMobile.Text.Trim();
                    cg.website = txtWebsite.Text.Trim();
                    cg.EduMode = ddlEduMode.SelectedValue;
                    cg.isExserviceMan = ddlExServicemen.SelectedValue;
                    cg.isDumbAndDeaf = ddlDeafDumb.SelectedValue;
                    cg.isppp = ddlPPP.SelectedValue;
                    cg.collegePrincipal = txtPrinc.Text.Trim();
                    cg.collegePrincipalPhone = txtPrincePhone.Text.Trim();
                    cg.collegeNodal = txtNodalOff.Text.Trim();
                    cg.collegeNodalPhone = txtNodalOffPhone.Text.Trim();
                    cg.collegeAttraction = txtAttraction.Text.Trim();
                    cg.collegeCounsile = ddlAffType.SelectedValue;
                    cg.institutecode = txtMISCode.Text.Trim();
                    cg.collegeRating = txtRating.Text.Trim();
                    cg.isactive = ddlActive.SelectedValue;
                    cg.UserId = Convert.ToString(Session["UserId"]);
                    string IPAddress = GetIPAddress();
                    cg.IPAddress = IPAddress;
                    //cg.confirmValue = confirmValue;
                    if (btnSubmit.Text == "Update")
                    {
                        string a = "";
                        if (Convert.ToString(Session["Iname"]) != txtITIName.Text.Trim())
                        {
                            a = CheckDuplicateCollege(txtITIName.Text.Trim());
                        }
                        if (a != "")
                        {
                            clsAlert.AlertMsg(this, "Institue Name already Exists.");
                            return;
                        }
                        else
                        {
                            string s = cg.UpdateITIDetails();
                            if (s == "1")
                            {
                                txtID.Enabled = true;
                                Clear();
                                clsAlert.AlertMsg(this, "ITI details are updated Successfully");
                                return;
                            }
                            else
                            {
                                clsAlert.AlertMsg(this, "ITI details are not updated");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmEditCollegeMaster";
                clsLogger.ExceptionMsg = "btnSubmit_Click";
                clsLogger.SaveException();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtID.Enabled = true;
            Clear();
            DisableAll();
        }

        public void DisableAll()
        {
            txtITIName.Enabled = false;
            ddlCollegeType.Enabled = false;
            txtInstAddress.Enabled = false;
            ddlDistrict.Enabled = false;
            txtEmail.Enabled = false;
            txtMobile.Enabled = false;
            txtWebsite.Enabled = false;
            ddlEduMode.Enabled = false;
            txtPrinc.Enabled = false;
            txtPrincePhone.Enabled = false;
            txtNodalOff.Enabled = false;
            txtNodalOffPhone.Enabled = false;
            txtAttraction.Enabled = false;
            ddlAffType.Enabled = false;
            txtRating.Enabled = false;
            txtMISCode.Enabled = false;
            ddlActive.Enabled=false;
            ddlExServicemen.Enabled = false;
            ddlDeafDumb.Enabled = false;
            ddlPPP.Enabled = false;

            btnSubmit.Enabled = false;
            btnCancel.Enabled = false;
            //txtSearch.Text= "";
        }
        public void EnableAll()
        {
            txtITIName.Enabled = true;
            ddlCollegeType.Enabled = true;
            txtInstAddress.Enabled = true;
            ddlDistrict.Enabled = true;
            txtEmail.Enabled = true;
            txtMobile.Enabled = true;
            txtWebsite.Enabled = true;
            ddlEduMode.Enabled = true;
            txtPrinc.Enabled = true;
            txtPrincePhone.Enabled = true;
            txtNodalOff.Enabled = true;
            txtNodalOffPhone.Enabled = true;
            txtAttraction.Enabled = true;
            ddlAffType.Enabled = true;
            txtRating.Enabled = true;
            txtMISCode.Enabled = true;
            ddlActive.Enabled = true;
            ddlExServicemen.Enabled = true;
            ddlDeafDumb.Enabled = true;
            ddlPPP.Enabled = true;

            btnSubmit.Enabled = true;
            btnCancel.Enabled = true;

            //txtSearch.Text= "";
        }

        
    }
}