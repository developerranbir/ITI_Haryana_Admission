using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmUpdateFamilyId : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
           //  eSessionMgmt.checkreferer();
          //  clsCancelStudentRegId.CheckSession();
            if (!Page.IsPostBack)
            {

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }


       

      
        public void disable()
        {
            dvStuName.Style.Add("display", "none");
            dvStuFatherName.Style.Add("display", "none");
            dvRegId.Style.Add("display", "none");
            dvBoard.Style.Add("display", "none");
            dvPassingYear.Style.Add("display", "none");
            dvMobileNo.Style.Add("display", "none");
            dvFamilyId.Style.Add("display", "none");
            dvFamilyIdInput.Style.Add("display", "none");
            dvUpdateFamilyId.Style.Add("display", "none");

        }

        public void enable()
        {
            dvStuName.Style.Add("display", "inline-block");
            dvStuFatherName.Style.Add("display", "inline-block");
            dvRegId.Style.Add("display", "inline-block");
            dvBoard.Style.Add("display", "inline-block");
            dvPassingYear.Style.Add("display", "inline-block");
            dvMobileNo.Style.Add("display", "inline-block");
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
                #region captcha
                if (Convert.ToString(Session["randomStr"]) == "" || Convert.ToString(Session["randomStr"]) == null)
                {
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "Message3", "alert('Please Try Again!!!');", true);
                    clsAlert.AlertMsg(this, "Please Try Again!!!");
                    txtturing.Text = String.Empty;
                    return;
                }
                string RNDStr = Session["randomStr"].ToString();
                if (txtturing.Text == "")
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Message2", "alert('Please enter captcha');", true);
                    clsAlert.AlertMsg(this, "Please enter captcha!");
                    return;
                }
                else
                {
                    if (txtturing.Text.Trim() != RNDStr.Trim())
                    {
                        //alert("Please enter your code correctly!!!");
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Message1", "alert('Please enter captcha correctly!!!');", true);
                        clsAlert.AlertMsg(this, "Please enter captcha correctly!!!");
                        txtturing.Text = string.Empty;
                        return;
                    }
                }
                #endregion
                GetFamilyIdData();


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmUpdateFamilyId";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }

        public void GetFamilyIdData()
        {
            try
            {


                clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                DataTable dt = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                CSR.Name = txtName.Text.Trim();
                CultureInfo engb = new CultureInfo("en-GB");
                DateTime BirthDate;
                BirthDate = Convert.ToDateTime(txtsdob.Text.ToString(), engb);
                CSR.BirthDate = BirthDate;
                dt = CSR.GetStudentDetailByRegId();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblBoard.Text = dt.Rows[0]["Board"].ToString();
                    lblPassingYear.Text = dt.Rows[0]["PassingYear"].ToString();
                    string mobilno = dt.Rows[0]["MobileNo"].ToString();
                    string MaskedMobileNo = mobilno.Substring(0, 2) + "XXXX" + mobilno.Substring(mobilno.Length - 4);
                    lblMobileNo.Text = MaskedMobileNo;
                    hdMobileNo.Value= dt.Rows[0]["MobileNo"].ToString();
                    hdEmailId.Value= dt.Rows[0]["EmailId"].ToString();
                    if (string.IsNullOrEmpty(dt.Rows[0]["family_Id"].ToString()))
                    {
                        lblFamilyId.Text = "-";
                        dvFamilyId.Style.Add("display", "none");
                        dvFamilyIdInput.Style.Add("display", "Inline-block");
                        dvUpdateFamilyId.Style.Add("display", "Inline-block");
                    }
                    else
                    {
                        lblFamilyId.Text = dt.Rows[0]["family_Id"].ToString();
                        dvFamilyId.Style.Add("display", "Inline-block");
                        dvFamilyIdInput.Style.Add("display", "none");
                        dvUpdateFamilyId.Style.Add("display", "none");
                        clsAlert.AlertMsg(this, "Family Id already exists.");
                    }
                    enable();

                }
                else
                {
                    dvSection.Attributes.Remove("class");
                    clsAlert.AlertMsg(this, "Student Detail not found.");
                    return;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmUpdateFamilyId";
                clsLogger.ExceptionMsg = "GetFamilyIdData";
                clsLogger.SaveException();
            }
        }
        public void clear()
        {
            txtRegId.Text = "";
            txtName.Text = "";
            txtsdob.Text = "";
            lblStudentName.Text = "";
            lblStuFatherName.Text = "";
            lblRegId.Text = "";
            lblBoard.Text = "";
            lblPassingYear.Text = "";
            lblMobileNo.Text = "";
            lblFamilyId.Text = "";
            txtFamilyId.Text = "";

            disable();
            

        }
        protected void btnUpdFamilyId_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (txtRegId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Registration Id.");
                    return;
                }

                if (txtName.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Name.");
                    return;
                }
                if (txtsdob.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Date of Birth");
                    return;
                }
                if (lblRegId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Registration Id not found.");
                    return;
                }

                if(txtFamilyId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Family Id.");
                    return;

                }

                clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                DataTable dt = new DataTable();
                CSR.RegId = lblRegId.Text.Trim();
                CSR.FamilyId = txtFamilyId.Text.Trim();
                CSR.IPAddress = GetIPAddress();
               
                string s = CSR.UpdateFamilyId();
                if (s == "1")
                {
                    clsAlert.AlertMsg(this, "Family Id updated successfully.");
                    if (hdMobileNo.Value !="")
                    {
                        SMS.SendSMS(hdMobileNo.Value, "Dear Candidate, Your Family Id " + txtFamilyId.Text.Trim() + " has been updated against your registration id " + lblRegId.Text.Trim() );
                    }
                    if (hdEmailId.Value != "")
                    {
                        SMS.SendEmail(hdEmailId.Value,"Update your family Id against Registration Id " + lblRegId.Text.Trim() , "Dear Candidate, Your Family Id " + txtFamilyId.Text.Trim() + " has been updated against your registration id " + lblRegId.Text.Trim());
                    }
                    GetFamilyIdData();
                    return;
                }
                else
                {
                    clsAlert.AlertMsg(this, "Family Id not Updated.");
                    clear();
                    return;
                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmUpdateFamilyId";
                clsLogger.ExceptionMsg = "btnUpdFamilyId_Click";
                clsLogger.SaveException();
            }
        }
        
    }
   
}
    