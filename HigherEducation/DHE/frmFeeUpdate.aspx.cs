using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmFeeUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            // eSessionMgmt.checkreferer();
            clsCancelStudentRegId.CheckSession();
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

            GetFeeAdmissionStatus();
        }
      
        public void GetFeeAdmissionStatus()
        {
            try
            {
                RadioButton r1 = new RadioButton();
                Label lblCollegeID = new Label();
                clsFeeUpdate CFU = new clsFeeUpdate();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                CFU.RegId = txtRegId.Text.Trim();
                CFU.Collegeid = Convert.ToString(Session["Collegeid"]);
                hdCollegeid.Value = Convert.ToString(Session["Collegeid"]);
                dt = CFU.GetFeeAdmissionDetailInfo();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    dvSectionNew.Attributes.Add("class", "cus-middle-section-new");
                    lblStudentName.Text = dt.Rows[0]["applicant_name"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["applicant_father_name"].ToString();
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblRollNo.Text = dt.Rows[0]["RollNo"].ToString();
                    lblGender.Text = dt.Rows[0]["GenderName"].ToString();
                    string mobilno = dt.Rows[0]["MobileNo"].ToString();
                    hdMobileNo.Value = dt.Rows[0]["MobileNo"].ToString();
                    string MaskedMobileNo = mobilno.Substring(0, 2) + "XXXX" + mobilno.Substring(mobilno.Length - 4);
                    lblMobileNo.Text = MaskedMobileNo;
                    lblCounselling.Text = dt.Rows[0]["Counselling"].ToString();
                    lblStudentCategory.Text = dt.Rows[0]["categoryname"].ToString();//Reservation CategoryName
                    lblSeatAllocationCategory.Text = dt.Rows[0]["SeatAllocationCategory"].ToString();
                    dvAdmissionStatus.Style.Add("display", "inline-block");
                    lbladmissionStatus.Style.Add("display", "inline-block");
                    GrdAdmissionStatus.DataSource = dt;
                    GrdAdmissionStatus.DataBind();

                    foreach (GridViewRow row in GrdAdmissionStatus.Rows)//if Record exists in Open1 table
                    {
                        

                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            r1 = (row.FindControl("RadioButton1") as RadioButton);
                            lblCollegeID.Text=(row.FindControl("lblCollegeid") as Label).Text;
                         if (lblCollegeID.Text != hdCollegeid.Value)
                            {
                                r1.Enabled = false;
                            }

                        }
                    }

                    enable();

                    

                }

                else
                {
                    clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                    CSR.RegId = txtRegId.Text.Trim();
                    dt1 = CSR.GetCancelAdmissions();
                    if (dt1.Rows.Count > 0)
                    {
                        dvSection.Attributes.Remove("class");
                        dvSectionNew.Attributes.Remove("class");
                        lbladmissionStatus.Style.Add("display", "none");
                        dvAdmissionStatus.Style.Add("display", "none");
                        GrdAdmissionStatus.DataSource = null;
                        GrdAdmissionStatus.DataBind();
                        clsAlert.AlertMsg(this, "This Student Admission has already been cancelled.");
                        clear();
                        return;
                    }
                    else
                    {
                        dvSection.Attributes.Remove("class");
                        dvSectionNew.Attributes.Remove("class");
                        lbladmissionStatus.Style.Add("display", "none");
                        dvAdmissionStatus.Style.Add("display", "none");
                        GrdAdmissionStatus.DataSource = null;
                        GrdAdmissionStatus.DataBind();
                        clsAlert.AlertMsg(this, "Student Admission not found.");
                        clear();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmFeeUpdate";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }
        }

        public void clear()
        {
            txtRegId.Text = "";
            lblStudentName.Text = "";
            lblStuFatherName.Text = "";
            lblRegId.Text = "";
            lblRollNo.Text = "";
            lblGender.Text = "";
            lblMobileNo.Text = "";
            lblSeatAllocationCategory.Text = "";
            lblStudentCategory.Text = "";
            hdMobileNo.Value = "";
            hdCombinationid.Value = "";
            hdPaymentTransId.Value = "";
            hdCollegeid.Value = "";
            txtAmount.Text = "";
            txtRemarks.Text = "";
            dvSection.Attributes.Remove("class");
            dvSectionNew.Attributes.Remove("class");
            txtturing.Text = "";
            disable();

        }
        public void disable()
        {
            dvSection.Style.Add("display", "none");
            dvSectionNew.Style.Add("display", "none");
            dvAdmissionStatus.Style.Add("display", "none");
            dvRefAddFee.Style.Add("display", "none");
            dvRemarks.Style.Add("display", "none");
            btnSubmit.Style.Add("display", "none");
            dvCaptcha.Style.Add("display", "none");
            dvAmount.Style.Add("display", "none");
        }

        public void enable()
        {
            dvSection.Style.Add("display", "inline-block");
            dvSectionNew.Style.Add("display", "inline-block");
            dvAdmissionStatus.Style.Add("display", "inline-block");
            dvRefAddFee.Style.Add("display", "inline-block");
            dvRemarks.Style.Add("display", "inline-block");
            btnSubmit.Style.Add("display", "inline-block");
            dvCaptcha.Style.Add("display", "inline-block");
            dvAmount.Style.Add("display", "inline-block");

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
            RadioButton r1 = new RadioButton();
            Label lblCollegeID = new Label();
            string flgCollege = "y";


            try
            {
                if (Page.IsValid)
                {
                    #region captcha
                    if (Convert.ToString(Session["randomStr"]) == "" || Convert.ToString(Session["randomStr"]) == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message3", "alert('Please Try Again!!!');", true);
                        txtturing.Text = String.Empty;
                        return;
                    }
                    string RNDStr = Session["randomStr"].ToString();
                    if (txtturing.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message2", "alert('Please enter captcha');", true);
                        return;
                    }
                    else
                    {
                        if (txtturing.Text.Trim() != RNDStr.Trim())
                        {
                            //alert("Please enter your code correctly!!!");
                            ScriptManager.RegisterStartupScript(this, GetType(), "Message1", "alert('Please enter captcha correctly!!!');", true);
                            txtturing.Text = string.Empty;
                            return;
                        }
                    }
                    #endregion
                    if (txtAmount.Text == "")
                            {
                                clsAlert.AlertMsg(this, "Please Enter Amount.");
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

                    foreach (GridViewRow row in GrdAdmissionStatus.Rows)//if Record exists in Open1 table
                    {

                        if (row.RowType == DataControlRowType.DataRow)
                        {

                            r1 = (row.FindControl("RadioButton1") as RadioButton);
                            lblCollegeID.Text = (row.FindControl("lblCollegeid") as Label).Text;
                            if (r1.Checked == true)
                            {
                                hdPaymentTransId.Value = (row.FindControl("lblPaymentTransId") as Label).Text;
                                
                                if (lblCollegeID.Text != hdCollegeid.Value)
                                {
                                    flgCollege = "n";
                                }
                            }


                        }
                    }
                    if (flgCollege == "n")
                    {
                        clsAlert.AlertMsg(this, "This payment does not belong to your college.");
                        return;
                    }
                    if (hdPaymentTransId.Value == "")
                    {
                        clsAlert.AlertMsg(this, "please select payment status.");
                        return;
                    }
                    
                    clsFeeUpdate CFU = new clsFeeUpdate();
                    DataTable dt = new DataTable();
                    CFU.RegId = lblRegId.Text.Trim();
                    CFU.PaymentTransid = hdPaymentTransId.Value;
                    CFU.Amount = txtAmount.Text;
                    CFU.RFAF = rbtRefundAddFee.SelectedValue;
                    CFU.Remarks = txtRemarks.Text.Trim();
                    CFU.UserId= Convert.ToString(Session["UserId"]);
                    CFU.IPAddress = GetIPAddress();

                    string s = CFU.UpdateFee();
                    if (s == "1")
                    {
                        clsAlert.AlertMsg(this, "Fee adjustment successfully.");
                        clear();
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Fee adjustment not Updated.");
                        clear();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmFeeUpdate";
                clsLogger.ExceptionMsg = "btnSubmit_Click";
                clsLogger.SaveException();

            }
        }
        }
   
}
    