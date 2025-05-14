using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;

namespace HigherEducation.HigherEducations
{
    public partial class frmCollegeGlance : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)

        {
            string UserType = Convert.ToString(Session["UserType"]);//"2";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            clsLoginUser.CheckSession(UserType);
            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);

            }

            else
            {
                if (!Page.IsPostBack)
                {
                    if (UserType == "1")// State Level User Only
                    {
                        ddlCollege.Style.Add("display", "block");
                        txtCollegeName.Style.Add("display", "none");
                        BindCollege();
                        rfddlCollege.Enabled = true;
                    }
                    else // Rest Users
                    {
                        txtCollegeName.Style.Add("display", "block");
                        ddlCollege.Style.Add("display", "none");
                        rfddlCollege.Enabled = false;
                    }
                    BindDistrict();
                    BindCollegeType();
                    GetCollegeProfile();
                }
                txtCollegeName.Text = Convert.ToString(Session["CollegeName"]);
                txtAffiliated.Text = Convert.ToString(Session["univeristyname"]);

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }

        public void GetCollegeProfile()
        {
            clsCollegeGlance cg = new clsCollegeGlance();
            if (Convert.ToString(Session["UserType"]) == "1")// State Level User Only
            {
                cg.collegeid = ddlCollege.SelectedValue;
            }
            else
            {
                cg.collegeid = Convert.ToString(Session["CollegeId"]);
            }
            DataTable dt = new DataTable();
            dt = cg.GetCollegeProfile();
            txtCollegeName.Text = Convert.ToString(Session["CollegeName"]);

            if (dt.Rows.Count > 0)
            {
               // txtAffiliated.Text = dt.Rows[0]["univeristyname"].ToString();
               // Session["univeristyname"] = txtAffiliated.Text;
                ddlDistrict.SelectedValue = dt.Rows[0]["LGdDistrictcode"].ToString();
                txtwebsite.Text = dt.Rows[0]["website"].ToString();
                txtEmail.Text = dt.Rows[0]["emailid"].ToString();
                txtContactNo.Text = dt.Rows[0]["phoneno"].ToString();
                txtPrincipalName.Text = dt.Rows[0]["Principal_Name"].ToString();
                txtPrincipalPhoneNo.Text = dt.Rows[0]["Principal_PhoneNo"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtNodalOfficer.Text = dt.Rows[0]["NodalAdmissions"].ToString();
                txtNodalOfficerPhoneNo.Text = dt.Rows[0]["NodalAdm_PhoneNo"].ToString();
                txtCrdArtstrmName.Text = dt.Rows[0]["CordArts"].ToString();
                txtCrdArtstrmPhoneNo.Text = dt.Rows[0]["CordArts_PhoneNo"].ToString();
                txtCrdCommStrmName.Text = dt.Rows[0]["CordComm"].ToString();
                txtCrdCommStrmPhoneNo.Text = dt.Rows[0]["CordComm_PhoneNo"].ToString();
                txtCrdSciStrmName.Text = dt.Rows[0]["CordSc"].ToString();
                txtCrdSciStrmPhoneNo.Text = dt.Rows[0]["CordSC_PhoneNo"].ToString();
                txtCrdJobCourses.Text = dt.Rows[0]["CordJob"].ToString();
                txtCrdJobCoursesPhoneNo.Text = dt.Rows[0]["CordJob_PhoneNo"].ToString();
                txtCrdFeeStruct.Text = dt.Rows[0]["CordFee"].ToString();
                txtCrdFeeStructPhoneNo.Text = dt.Rows[0]["CordFee_PhoneNo"].ToString();
                txtMainAttract.Text = dt.Rows[0]["MainAttract"].ToString();
                ddlCollegeType.SelectedValue = dt.Rows[0]["collegetype"].ToString();
                ddlEduMode.SelectedValue = dt.Rows[0]["EduMode"].ToString();
                ddlPPP.Text= dt.Rows[0]["isppp"].ToString();
                ddlExServicemen.Text= dt.Rows[0]["isExserviceMan"].ToString();
                ddlDeafDumb.Text= dt.Rows[0]["isDumbAndDeaf"].ToString();
                
            }
            else
            {
              //  txtAffiliated.Text = "-";
               
            }
        }
        public void BindCollege()
        {
            try
            {
                clsCollegeGlance cg = new clsCollegeGlance();

                DataTable dt = new DataTable();
                dt = cg.GetCollege();
                if (dt.Rows.Count > 0)
                {
                    ddlCollege.DataSource = dt;
                    ddlCollege.DataTextField = "Text";
                    ddlCollege.DataValueField = "Value";
                    ddlCollege.DataBind();
                    ddlCollege.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select ITI--", "0"));
                    ddlCollege.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void BindCollegeType()
        {
            try
            {
              
                clsCollegeGlance cg = new clsCollegeGlance();
                DataTable dt = new DataTable();
                dt = cg.BindCollegeType();
                if (dt.Rows.Count > 0)
                {
                    ddlCollegeType.DataSource = dt;
                    ddlCollegeType.DataTextField = "Text";
                    ddlCollegeType.DataValueField = "Value";
                    ddlCollegeType.DataBind();
                    ddlCollegeType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select ITI Type--", "0"));
                    ddlCollegeType.Focus();

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindDistrict()
        {
            try
            {
                //clsCollegeSearch CS = new clsCollegeSearch();
                clsCollegeGlance cg = new clsCollegeGlance();
                DataTable dt = new DataTable();
                dt = cg.BindDistrict();
                if (dt.Rows.Count > 0)
                {
                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "Text";
                    ddlDistrict.DataValueField = "Value";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select District--", "0"));
                    ddlDistrict.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (Page.IsValid)
                {
                    if (ddlDistrict.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select District");
                        return;
                    }
                    if (ddlCollegeType.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select ITI Type");
                        return;
                    }
                    if (ddlExServicemen.SelectedValue == "00")
                    {
                        clsAlert.AlertMsg(this, "Please Select ExServicemen");
                        return;
                    }
                    if (ddlDeafDumb.SelectedValue == "00")
                    {
                        clsAlert.AlertMsg(this, "Please Select Deaf and Dumb");
                        return;
                    }
                    if (ddlPPP.SelectedValue == "00")
                    {
                        clsAlert.AlertMsg(this, "Please Select Public-Private Partnerships");
                        return;
                    }
                    if (ddlEduMode.SelectedValue == "00")
                    {
                        clsAlert.AlertMsg(this, "Please Select Education Mode");
                        return;
                    }
                    if (txtContactNo.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Contact No.");
                        return;
                    }

                    if (txtEmail.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Email.");
                        return;
                    }
                    if (txtPrincipalName.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Principal Name.");
                        return;
                    }
                    if (txtPrincipalPhoneNo.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Principal Phone No.");
                        return;
                    }
                    if (txtAddress.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Address.");
                        return;
                    }

                    if (txtNodalOfficer.Text == "" || txtNodalOfficerPhoneNo.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Nodal Officer and Phone No.");
                        return;
                    }

                    txtCollegeName.Text = Convert.ToString(Session["CollegeName"]);
                    
                    clsCollegeGlance cg = new clsCollegeGlance();
                    if (Convert.ToString(Session["UserType"]) == "1")// State Level User Only
                    {
                        cg.collegeid = ddlCollege.SelectedValue;
                    }
                    else
                    {
                        cg.collegeid = Convert.ToString(Session["CollegeId"]);
                    }
                    cg.ExServiceman = ddlExServicemen.SelectedValue;
                    cg.DeafDumb = ddlDeafDumb.SelectedValue;
                    cg.PPP = ddlPPP.SelectedValue;
                    cg.distcode = ddlDistrict.SelectedValue;
                    cg.website = txtwebsite.Text;
                    cg.emailid = txtEmail.Text;
                    cg.contactno = txtContactNo.Text;
                    cg.principalname = txtPrincipalName.Text;
                    cg.principal_phoneno = txtPrincipalPhoneNo.Text;
                    cg.address = txtAddress.Text;
                    cg.NodalAdmissions = txtNodalOfficer.Text;
                    cg.NodalAdmissions_PhoneNo = txtNodalOfficerPhoneNo.Text;
                    cg.CordArts = txtCrdArtstrmName.Text;
                    cg.CordArts_PhoneNo = txtCrdArtstrmPhoneNo.Text;
                    cg.CordComm = txtCrdCommStrmName.Text;
                    cg.CordComm_PhoneNo = txtCrdCommStrmPhoneNo.Text;
                    cg.CordSc = txtCrdSciStrmName.Text;
                    cg.CordSc_PhoneNo = txtCrdSciStrmPhoneNo.Text;
                    cg.CordJob = txtCrdJobCourses.Text;
                    cg.CordJob_PhoneNo = txtCrdJobCoursesPhoneNo.Text;
                    cg.CordFee = txtCrdFeeStruct.Text;
                    cg.CordFee_PhoneNo = txtCrdFeeStructPhoneNo.Text;
                    cg.MainAttract = txtMainAttract.Text;
                    cg.UserId = Convert.ToString(Session["UserId"]);
                    cg.CollegeType = ddlCollegeType.SelectedValue;
                    cg.EduMode = ddlEduMode.SelectedValue;
                    string IPAddress = GetIPAddress();
                    cg.IPAddress = IPAddress;
                    string s = cg.UpdateCollegeGlance();
                    if (s == "1")
                    {
                        clsAlert.AlertMsg(this, "Profile Updated Successfully.");
                        GetCollegeProfile();
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Profile not Updated.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmCollegeGlance";
                clsLogger.ExceptionMsg = "btnSubmit_Click";
                clsLogger.SaveException();
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

        public void clearall()
        {
            txtAffiliated.Text = "";
            ddlDistrict.SelectedIndex = -1;
            txtwebsite.Text = "";
            txtEmail.Text = "";
            txtContactNo.Text = "";
            txtPrincipalName.Text = "";
            txtPrincipalPhoneNo.Text = "";
            txtAddress.Text = "";
            txtNodalOfficer.Text = "";
            txtNodalOfficerPhoneNo.Text = "";
            txtCrdArtstrmName.Text = "";
            txtCrdArtstrmPhoneNo.Text = "";
            txtCrdCommStrmName.Text = "";
            txtCrdCommStrmPhoneNo.Text = "";
            txtCrdSciStrmName.Text = "";
            txtCrdSciStrmPhoneNo.Text = "";
            txtCrdJobCourses.Text = "";
            txtCrdJobCoursesPhoneNo.Text = "";
            txtCrdFeeStruct.Text = "";
            txtCrdFeeStructPhoneNo.Text = "";
            txtMainAttract.Text = "";
            ddlCollege.SelectedIndex = -1;
            ddlPPP.SelectedIndex = -1;
            ddlDeafDumb.SelectedIndex = -1;
            ddlExServicemen.SelectedIndex = -1;
        }

        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCollegeProfile();
        }
    }


}