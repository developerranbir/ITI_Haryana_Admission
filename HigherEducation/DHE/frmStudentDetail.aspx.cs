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
    public partial class frmStudentDetail : System.Web.UI.Page
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
                //  txtRemarks.Text = "";
               
                string maxpage = "0";
                string verificationstatus = "";
                clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                DataTable dt = new DataTable();
                CSR.RollNo = txtRollNo.Text.Trim();
                CSR.Name = txtName.Text.Trim();
                CultureInfo engb = new CultureInfo("en-GB");
                DateTime BirthDate;
                BirthDate = Convert.ToDateTime(txtsdob.Text.ToString(), engb);
                CSR.BirthDate = BirthDate;
                dt = CSR.GetStudentDetail();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblBoard.Text = dt.Rows[0]["Board"].ToString();
                    lblPassingYear.Text = dt.Rows[0]["PassingYear"].ToString();
                    string mobilno = dt.Rows[0]["MobileNo"].ToString();
                    string MaskedMobileNo = mobilno.Substring(0,2) + "XXXX" + mobilno.Substring(mobilno.Length - 4);
                    lblMobileNo.Text = MaskedMobileNo;
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
                clsLogger.ExceptionPage = "DHE/frmFetchStudentDetail";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }
    }
   
}
    