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
    public partial class frmCancellationStudentRegistration : System.Web.UI.Page
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
       
                if (!Page.IsPostBack)
            {

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }


        protected void btnDelete_Click(object sender, EventArgs e)
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

             


                if (txtRollNo.Text != "" && txtRemarks.Text != "" && lblRegId.Text != "")
                {


                    clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                    CSR.RegId = lblRegId.Text.Trim();
                    CSR.Name = lblStudentName.Text.Trim();
                    CSR.Remarks = txtRemarks.Text.Trim();
                    CSR.IPAddress = GetIPAddress();
                    CSR.UserId = Convert.ToString(Session["UserId"]);
                    string s = "";
                   
                        s = CSR.CancelStudentRegId();
                  
                    if (s == "1")
                    {
                        if (lblMobileNo.Text != "")
                        {
                            string smstext = string.Empty;
                            smstext = "Your application for admission (" + lblRegId.Text + ") is cancelled as you initially registered with wrong information. APPLY AGAIN with correct information at  https://admissions.itiharyana.gov.in to seek the admission.";
                            AgriSMS.sendSingleSMS(lblMobileNo.Text.Trim(), smstext, "1007982881372189218");
                        }
                        string urlSubject = "Cancellation of ITI Registration ID " + lblRegId.Text;
                        string msg = string.Empty;
                        msg = "Your application for admission (" + lblRegId.Text + ") is cancelled as you initially registered with wrong information." + Environment.NewLine + "Remarks: " + txtRemarks.Text + " APPLY AGAIN with correct information at  https://admissions.itiharyana.gov.in to seek the admission. Regards, SDIT Haryana.";
                        if (lblEmailId.Text != "")
                        {
                            SMS.SendEmail(lblEmailId.Text, urlSubject, msg.Trim());
                        }
                        clsAlert.AlertMsg(this, "Student Registration Id " + lblRegId.Text.Trim() + " has been cancelled successfully.");
                        clear();
                        return;
                    }
                    else if (s == "2")//Registration ID Exists on MeritList (sealtallotment2020)
                    {
                        clsAlert.AlertMsg(this, "Student Registration Id cannot be cancelled as it exists in Merit List");
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Student Registration Id not cancelled..try again later.");
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
                clsLogger.ExceptionPage = "DHE/frmCancellationStudentRegistration";
                clsLogger.ExceptionMsg = "btnDelete_Click";
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
            dvSection.Attributes.Remove("class");
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
            dvCancel.Style.Add("display", "none");
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
            dvCancel.Style.Add("display", "inline-block");
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
               
                    dt = CSR.GetStudentInfo();
              
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblBoard.Text = dt.Rows[0]["Board"].ToString();
                    lblPassingYear.Text = dt.Rows[0]["PassingYear"].ToString();
                    lblMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                    lblEmailId.Text = dt.Rows[0]["EmailId"].ToString();
                    dvCancel.Style.Add("display", "inline-block");
                    enable();
                    //Check final submission not to cancel records
                    // maxpage= dt.Rows[0]["maxpage"].ToString();
                    // verificationstatus= dt.Rows[0]["verificationstatus"].ToString();
                    //if (Convert.ToInt32(maxpage)< 7 && verificationstatus == "")
                    //{
                    //    dvCancel.Style.Add("display", "inline-block");
                    //}
                    //else
                    //{
                    //    dvCancel.Style.Add("display", "none");
                    //}

                }
                
                else
                {
                    CSR.RollNo = txtRollNo.Text.Trim();
                    dt1 = CSR.GetStudentCancelRecords();
                    if (dt1.Rows.Count > 0)
                    {
                        dvSection.Attributes.Remove("class");
                      
                        clsAlert.AlertMsg(this, "This records has already been cancelled.");
                        clear();
                        return;
                    }
                    else
                    {
                        dvSection.Attributes.Remove("class");
                        clsAlert.AlertMsg(this, "Student registration not found.");
                        clear();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmCancellationStudentRegistration";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }
    }
   
}
    