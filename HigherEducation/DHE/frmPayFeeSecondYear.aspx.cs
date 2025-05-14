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
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmPayFeeSecondYear : System.Web.UI.Page
    {
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
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
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

                clsPayFeeSecondYear CSR = new clsPayFeeSecondYear();
                DataTable dt = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                CSR.Collegeid = Convert.ToString(Session["CollegeId"]);
                dt = CSR.GetAdmissionDetailInfo();
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
                    clsAlert.AlertMsg(this, "Student not found. Please check Registration Number");
                    return;
                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmPayFeeSecondYear";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }
        
        //Offer Seats Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRegId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Registration Id/Roll No.");
                    return;
                }
                
                if (txtMobileNo.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Mobile No.");
                    return;
                }

                if (txtRemarks.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Remarks.");
                    return;
                }

                if (txtRegId.Text != "" && txtRemarks.Text != "" && lblRegId.Text != "" && txtMobileNo.Text != "")
                {
                    clsPayFeeSecondYear CSR = new clsPayFeeSecondYear();
                    CSR.RegId = lblRegId.Text.Trim();
                    CSR.MobileNo = txtMobileNo.Text.Trim();
                    CSR.Remarks = txtRemarks.Text.Trim();
                    CSR.UserId = Convert.ToString(Session["UserId"]);
                    CSR.IPAddress = GetIPAddress();
                    DataTable dt = new DataTable();
                    dt = CSR.changeMobile();
                    int result=5;
                    if (dt.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dt.Rows[0]["Result"].ToString());
                    }
                    if(result==1)
                    { 
                        clsAlert.AlertMsg(this, "Student mobile number changed successfully.");
                        clear();
                        return;
                    }
                    else if (result == 0)
                    {
                        clsAlert.AlertMsg(this, "Student mobile number is same as previous. Please enter new Mobile number.");
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Student mobile number not changed.... try again later.");
                        return;
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "There is some problem in Remarks or Registation Id or Roll No or Mobile No.");
                    return;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmPayFeeSecondYear";
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
            txtMobileNo.Text = "";
            txtRemarks.Text = "";


        }

    }

}
