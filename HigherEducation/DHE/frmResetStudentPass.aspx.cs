using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BAL;
using HigherEducation.BusinessLayer;
using HigherEducation.Models;
using MySqlX.XDevAPI.Relational;
//using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmResetStudentPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string UserType = Convert.ToString(Session["UserType"]);
                eDISHAutil eSessionMgmt = new eDISHAutil();

                clsLoginUser.CheckSession(UserType);

                if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
                {
                    Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
                    return;
                }
                BindSession();
                //Security Check
                eSessionMgmt.AntiFixationInit();
                eSessionMgmt.AntiHijackInit();
                //Security Check
            }
        }

        public void disable()
        {
            dvSection.Style.Add("display", "none");
            dvSave.Style.Add("display", "none");
            dvCaptcha.Style.Add("display", "none");
        }

        public void enable()
        {
            dvSection.Style.Add("display", "block");
            dvSave.Style.Add("display", "inline-block");
            dvCaptcha.Style.Add("display", "block");
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

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                //  txtRemarks.Text = "";
                if (txtRegId.Text == "" || ddlAdmissionYear.SelectedItem.Value == "0")
                {
                    clsAlert.AlertMsg(this, "Please Select Admission Year");

                    return;
                }
                clsChangeCandidateMobile CSR = new clsChangeCandidateMobile();
                DataTable dt = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                CSR.Collegeid = Convert.ToString(Session["CollegeId"]);
                CSR.sessionId = ddlAdmissionYear.SelectedValue.ToString();
                dt = CSR.GetAdmissionDetailInfoForResetPass();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    lblGender.Text = dt.Rows[0]["GenderName"].ToString();
                    lblDOB.Text = dt.Rows[0]["BirthDate"].ToString();
                    lblCollegeName.Text = dt.Rows[0]["collegename"].ToString();
                    lblSectionName.Text = dt.Rows[0]["SectionName"].ToString();
                    lblAdmissionStatus.Text = dt.Rows[0]["Challan_status"].ToString();
                    string mobilno = dt.Rows[0]["MobileNo"].ToString();
                    string MaskedMobileNo = mobilno.Substring(0, 2) + "XXXX" + mobilno.Substring(mobilno.Length - 4);
                    lblMobileNo.Text = MaskedMobileNo;

                    enable();
                }
                else
                {
                    dvSection.Attributes.Remove("class");
                    clear();
                    clsAlert.AlertMsg(this, "Student not found. Please check Registration Number and Select correct Admission Session Year");
                    return;
                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmChangeCandidateMobile";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }
        
        //reset password ranbir
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRegId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Registration Id/Roll No.");
                    return;
                }
                
               

                if (txtRegId.Text != "" )
                {
                    clsChangeCandidateMobile CSR = new clsChangeCandidateMobile();
                    CSR.RegId = lblRegId.Text.Trim();
                  
                    CSR.UserId = Convert.ToString(Session["UserId"]);
                    CSR.IPAddress = GetIPAddress();
                    CSR.sessionId = ddlAdmissionYear.SelectedValue.ToString();
                    DataTable dt = new DataTable();
                    dt = CSR.ResetPasswordStudent();
                    int result=0;
                    if (dt.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dt.Rows[0]["Result"].ToString());
                    }
                    if(result==1)
                    { 
                        clsAlert.AlertMsg(this, "Student Password Reset to test@123 Successfully.");
                        clear();
                        return;
                    }
                   
                    else
                    {
                        clsAlert.AlertMsg(this, "Student Password Not Reset.... try again later.");
                        return;
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "There is some problem in Registation Id");
                    return;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmChangeCandidateMobile";
                clsLogger.ExceptionMsg = "btnSave_Click";
                clsLogger.SaveException();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            disable();
            //ddlCatgAllotment.SelectedIndex = -1;
           // rdbtnPMS_SC.SelectedValue = "0";
            txtRegId.Text = "";
            lblRegId.Text = "";
            lblStudentName.Text = "";
            lblStuFatherName.Text = "";
            lblGender.Text = "";
            lblDOB.Text = "";
            lblCollegeName.Text = "";
            lblSectionName.Text = "";
            lblAdmissionStatus.Text = "";


        }

        public void BindSession()
        {
            ddlAdmissionYear.Items.Clear();
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            clsChangeCandidateMobile CSR = new clsChangeCandidateMobile();
            dt = CSR.getSession();
            obj.BindDDLCommon(dt, ddlAdmissionYear, "session_id", "session_year");
            ddlAdmissionYear.Items.Insert(0, new ListItem("Please Select", "0"));
        }
    }

}
