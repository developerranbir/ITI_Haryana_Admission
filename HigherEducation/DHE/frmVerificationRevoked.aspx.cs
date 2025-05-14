using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmVerificationRevoked : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType ="1";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);
            if (!Page.IsPostBack)
            {

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }


        protected void btnVerRevoked_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRollNo.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Roll No.");
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
                if (ddlUGPG.SelectedValue == "0")
                {
                    clsAlert.AlertMsg(this, "Please Select Admissions.");
                    return;
                }

                if (txtRollNo.Text != "" && txtRemarks.Text != "" && lblRegId.Text != "")
                {


                    clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                    CSR.RegId = lblRegId.Text.Trim();
                    CSR.Name = lblStudentName.Text.Trim();
                    CSR.Remarks = txtRemarks.Text.Trim();
                    CSR.IPAddress = GetIPAddress();
                    CSR.UserId = Convert.ToString(Session["UserId"]);
                    CSR.Collegeid = Convert.ToString(Session["CollegeId"]);//Not used this collegeid coz state user is used.
                    string s = "";
                    if (ddlUGPG.SelectedValue == "PG")
                    {
                        s = CSR.VerificationRevoked_PG();
                    }
                    else
                    {
                        s = CSR.VerificationRevoked();
                    }
                    if (s == "1")
                    {
                        clsAlert.AlertMsg(this, "Verification for Registration Id: " + lblRegId.Text.Trim() + " has been revoked!");
                        clear();
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Verification for Registration Id: " + lblRegId.Text.Trim() + " has not been revoked.... try again later.");
                        return;
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "There is some problem in RollNo, Remarks or Registation Id.");
                    return;

                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmVerificationRevoked";
                clsLogger.ExceptionMsg = "btnVerRevoked_Click";
                clsLogger.SaveException();
            }
        }

        public void clear()
        {
            txtRollNo.Text = "";
            txtRemarks.Text = "";
            lblStudentName.Text = "";
            lblStuFatherName.Text = "";
            lblRegId.Text = "";
            lblBoard.Text = "";
            lblPassingYear.Text = "";
            lblMobileNo.Text = "";
            lblEmailId.Text = "";
            disable();

        }
        public void disable()
        {
            dvStuName.Style.Add("display", "none");
            dvStuFatherName.Style.Add("display", "none");
            dvRegId.Style.Add("display", "none");
            dvBoard.Style.Add("display", "none");
            dvPassingYear.Style.Add("display", "none");
            dvMobileNo.Style.Add("display", "none");
            dvEmailId.Style.Add("display", "none");
            dvSection.Style.Add("display", "none");
            dvVerRevoked.Style.Add("display", "none");
        }

        public void enable()
        {
            dvStuName.Style.Add("display", "inline-block");
            dvStuFatherName.Style.Add("display", "inline-block");
            dvRegId.Style.Add("display", "inline-block");
            dvBoard.Style.Add("display", "inline-block");
            dvPassingYear.Style.Add("display", "inline-block");
            dvMobileNo.Style.Add("display", "inline-block");
            dvEmailId.Style.Add("display", "inline-block");
            dvSection.Style.Add("display", "inline-block");
            dvVerRevoked.Style.Add("display", "inline-block");
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
                txtRemarks.Text = "";
                
                string maxpage = "0";
                string verificationstatus = "";
                clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                CSR.RollNo = txtRollNo.Text.Trim();
                CSR.Collegeid = Convert.ToString(Session["CollegeId"]);

                if (ddlUGPG.SelectedValue == "PG")
                {
                    dt = CSR.GetVerifierStudentInfo_PG();
                }
                else
                {
                    dt = CSR.GetVerifierStudentInfo();
                }
                    
                if (dt.Rows.Count > 0)
                {
                    dvSection.Style.Add("display", "inline-block");
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblBoard.Text = dt.Rows[0]["Board"].ToString();
                    lblPassingYear.Text = dt.Rows[0]["PassingYear"].ToString();
                    lblMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                    lblEmailId.Text = dt.Rows[0]["EmailId"].ToString();
                    
                    enable();
                   // Check final submission not to cancel records
                     maxpage = dt.Rows[0]["maxpage"].ToString();
                    verificationstatus = dt.Rows[0]["verificationstatus"].ToString();
                    if (Convert.ToInt32(maxpage) == 7 && verificationstatus.ToLower() == "v")//max_page 7 and status v 
                    {
                        dvVerRevoked.Style.Add("display", "inline-block");
                    }
                    else
                    {
                        dvSection.Style.Add("display", "none");
                        clsAlert.AlertMsg(this, "Verification for this record cannot be revoked.");
                        dvVerRevoked.Style.Add("display", "none");
                        return;
                    }

                }
                
              
                    else
                    {
                        dvSection.Attributes.Remove("class");
                        dvSection.Style.Add("display", "none");
                        clsAlert.AlertMsg(this, "No Matching record found");
                        clear();
                        return;                 
                }
               
            }
            catch (Exception ex)
            {
                dvVerRevoked.Style.Add("display", "none");
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmVerificationRevoked";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }
    }
   
}
    