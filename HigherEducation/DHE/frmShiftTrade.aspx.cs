using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BAL;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmShiftTrade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = Convert.ToString(Session["UserType"]);//"2";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            clsLoginUser.CheckSession(UserType);
            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
                return;
            }
            if (!Page.IsPostBack)
            {
              
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                txtRemarks.Text = "";

                clsShiftTrade CSR = new clsShiftTrade();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                CSR.Collegeid = Convert.ToString(Session["Collegeid"]);
                CSR.UserType = Convert.ToString(Session["UserType"]);
                dt = CSR.GetAdmissionDetailForShift();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    //lblStudentName.Text = dt.Rows[0]["CandidateName"].ToString();
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblCollege.Text = dt.Rows[0]["CollegeName"].ToString();
                    lblCourseSectionName.Text = dt.Rows[0]["courseSectionName"].ToString();
                    lblAdmissionStatus.Text= dt.Rows[0]["admissionstatus"].ToString();
                    lblPayment_transactionId.Text= dt.Rows[0]["Payment_transactionId"].ToString();
                    lblCounselling.Text= dt.Rows[0]["Counselling"].ToString();
                   
                    hdCollegeid.Value = Convert.ToString(Session["CollegeId"]);
                   
                    dvTradeSection.Style.Add("display", "inline-block");
                    dvShift.Style.Add("display", "inline-block");
                    
                    if (Convert.ToString(Session["UserType"]) == "1")// State Level User Only
                    {
                        dvITI.Style.Add("display", "block");
                        BindCollege();
                        rfddlCollege.Enabled = true;
                    }
                    else
                    {
                        dvITI.Style.Add("display", "none");
                        rfddlCollege.Enabled = false;
                    }
                    BindCourse();
                    enable();
                }

                else
                {
                    CSR.RegId = txtRegId.Text.Trim();
                    dt1 = CSR.GetCancelAdmissions();
                    if (dt1.Rows.Count > 0)
                    {
                        dvSection.Attributes.Remove("class");

                        clsAlert.AlertMsg(this, "This Student Admission has already been cancelled.");
                        clear();
                        return;
                    }
                    else
                    {
                     dvSection.Attributes.Remove("class");
                    clsAlert.AlertMsg(this, "Student Admission not found.");
                    clear();
                    return;
                      }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmShiftTrade";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }

        public void BindCourse()
        {
            try
            {

                clsShiftTrade cfp = new clsShiftTrade();
                if (Convert.ToString(Session["UserType"]) == "1")// State Level User Only
                {
                    cfp.Collegeid = ddlCollege.SelectedValue;
                }
                else
                {
                    cfp.Collegeid = hdCollegeid.Value;
                }
                DataTable dt = new DataTable();
                dt = cfp.BindCourse();
                if (dt.Rows.Count > 0)
                {
                    ddlCourseName.DataSource = dt;
                    ddlCourseName.DataTextField = "Text";
                    ddlCourseName.DataValueField = "Value";
                    ddlCourseName.DataBind();
                    ddlCourseName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select Trade--", "0"));
                    ddlCourseName.Focus();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmShiftTrade";
                clsLogger.ExceptionMsg = "BindCourse";
                clsLogger.SaveException();
            }
        }

        public void CourseSection_Bind()
        {
            try
            {

                clsShiftTrade cfp = new clsShiftTrade();
                if (Convert.ToString(Session["UserType"]) == "1")// State Level User Only
                {
                    cfp.Collegeid = ddlCollege.SelectedValue;
                }
                else
                {
                    cfp.Collegeid = hdCollegeid.Value;
                }
                cfp.Courseid = ddlCourseName.SelectedValue;
                DataTable dt = new DataTable();
                dt = cfp.CourseSection_Bind();
                if (dt.Rows.Count > 0)
                {
                    ddlSectionName.DataSource = dt;
                    ddlSectionName.DataTextField = "sectionname";
                    ddlSectionName.DataValueField = "coursesectionid";
                    ddlSectionName.DataBind();
                    ddlSectionName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select Section--", "0"));
                    ddlSectionName.Focus();
                }
                else
                {
                    ddlSectionName.DataSource = null;
                    ddlSectionName.ClearSelection();
                    ddlSectionName.Items.Clear();
                    ddlSectionName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select Section--", "0"));
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmShiftTrade";
                clsLogger.ExceptionMsg = "CourseSection_Bind";
                clsLogger.SaveException();
            }
        }
        
        public void clear()
        {
            txtRegId.Text = "";

            txtRemarks.Text = "";
            lblRegId.Text = "";
            lblCollege.Text = "";
            lblCourseSectionName.Text = "";
            lblAdmissionStatus.Text = "";
            lblPayment_transactionId.Text = "";
            lblCounselling.Text = "";
            ddlCourseName.SelectedIndex = -1;
            ddlSectionName.SelectedIndex = -1;

            dvSection.Attributes.Remove("class");
            disable();

        }
        public void disable()
        {
            dvSection.Style.Add("display", "none");
            dvShift.Style.Add("display", "none");
        }

        public void enable()
        {
            dvSection.Style.Add("display", "inline-block");
            dvShift.Style.Add("display", "inline-block");
      
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

        protected void btnShift_Click(object sender, EventArgs e)
        {
          try
            {
                if (txtRegId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Registration Id.");
                    return;
                }
                if (txtRemarks.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Remarks.");
                    return;
                }
                if (lblRegId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Registration Id not found.");
                    return;
                }
               

                if (txtRegId.Text != "" && txtRemarks.Text != "" && lblRegId.Text != "")
                {
                   
                    clsShiftTrade CSR = new clsShiftTrade();
                    CSR.RegId = lblRegId.Text.Trim();
                    CSR.ITIName = lblCollege.Text.Trim();
                    CSR.Remarks = txtRemarks.Text.Trim();
                    CSR.Courseid = ddlCourseName.SelectedValue;
                    CSR.Sectionid = ddlSectionName.SelectedValue;
                    CSR.CourseName = ddlCourseName.SelectedItem.ToString();
                    CSR.SectionName = ddlSectionName.SelectedItem.ToString();
                    CSR.Counselling = lblCounselling.Text.Trim();
                    CSR.Payment_transactionId = lblPayment_transactionId.Text.Trim();
                    CSR.IPAddress = GetIPAddress();
                    CSR.UserId = Convert.ToString(Session["UserId"]);
                    CSR.Collegeid = Session["CollegeId"].ToString();
                    if (Convert.ToString(Session["UserType"]) == "1")// State Level User Only
                    {
                        CSR.ShiftCollegeid = ddlCollege.SelectedValue;
                        CSR.ShiftCollegeName = ddlCollege.SelectedItem.ToString();
                    }
                    else
                    {
                        CSR.ShiftCollegeid = "0";
                        CSR.ShiftCollegeName = "NA";
                    }
                    CSR.UserType = Convert.ToString(Session["UserType"]);
                    string s = CSR.ShiftTrade();
                    if (s == "1")
                    {
                    
                        clsAlert.AlertMsg(this, "This Registrtaion Id " + lblRegId.Text.Trim() + " student trade section has been shifted successfully.");
                        clear();

                        return;
                    }
                    else if (s == "2")
                    {

                        clsAlert.AlertMsg(this, "This Registrtaion Id " + lblRegId.Text.Trim() + " student trade section has been shifted already.");
                        clear();

                        return;
                    }
                    else if (s == "3")
                    {
                        clsAlert.AlertMsg(this, "There is no vacant seat in this trade section.");
                        return;
                    }
                    else if (s == "4")
                    {
                        clsAlert.AlertMsg(this, "Trade shifitng is only permitted within same Council Type (SCVT to SCVT)/(NCVT to NCVT)");
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Student trade section is not shifted.try again later.");
                        return;
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "There is some problem in Remarks or Registation Id.");
                    return;

                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmShiftTrade";
                clsLogger.ExceptionMsg = "btnShift_Click";
                clsLogger.SaveException();
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
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmShiftTrade";
                clsLogger.ExceptionMsg = "BindCollege";
                clsLogger.SaveException();
            }
        }
        protected void ddlCourseName_SelectedIndexChanged(object sender, EventArgs e)
        {       
                CourseSection_Bind();
        }

        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCollege.SelectedValue.ToString() != "0")
            {
                BindCourse();
            }
            else
            {
                ddlCourseName.DataSource = null;
                ddlCourseName.DataBind();
            }
        }

    }
   
}
    