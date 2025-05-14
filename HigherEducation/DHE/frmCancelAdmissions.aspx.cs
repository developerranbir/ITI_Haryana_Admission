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
    public partial class frmCancelAdmissions : System.Web.UI.Page
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
            try
            {
                txtRemarks.Text = "";

                string maxpage = "0";
                string verificationstatus = "";
                clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                CSR.Collegeid = Convert.ToString(Session["Collegeid"]);
                dt = CSR.GetAdmissionDetailInfo();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblStudentName.Text = dt.Rows[0]["applicant_name"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["applicant_father_name"].ToString();
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblCollege.Text = dt.Rows[0]["CollegeName"].ToString();
                //    lblCourses.Text = dt.Rows[0]["courseName"].ToString();
                    lblSectionName.Text = dt.Rows[0]["SectionName"].ToString();
                   // lblSubComb.Text = dt.Rows[0]["subjectCombination"].ToString();
                    string mobilno = dt.Rows[0]["MobileNo"].ToString();
                    hdMobileNo.Value= dt.Rows[0]["MobileNo"].ToString();
                    string MaskedMobileNo = mobilno.Substring(0, 2) + "XXXX" + mobilno.Substring(mobilno.Length - 4);
                    lblMobileNo.Text = MaskedMobileNo;
                    lblEmailId.Text = dt.Rows[0]["EmailId"].ToString();
                    lblCounselling.Text= dt.Rows[0]["Counselling"].ToString();
                    dvCancel.Style.Add("display", "inline-block");
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
                clsLogger.ExceptionPage = "DHE/frmCancelStudentAdmission";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string extension = "";
            int MaxPdfSizeKB = 0;
            int MaxPdfSizeBytes = 0;
            int PDFSize = 0;
            byte[] DocumentBytes;
            String Document = "";
            try
            {
                if (txtRegId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Registration Id/Roll No.");
                    return;
                }


                if (txtRemarks.Text == "")
                {
                    clsAlert.AlertMsg(this, "Please Enter Remarks.");
                    return;
                }
                if (lblRegId.Text == "")
                {
                    clsAlert.AlertMsg(this, "Registration Id does not exists.");
                    return;
                } 
                if (rdbtnSLC.SelectedValue == "")
                {
                    clsAlert.AlertMsg(this, "Please Select SLC Bogus.");
                    return;
                }
                if (File_Upload.HasFile)
                {
                    
                    int DoubleExten = File_Upload.PostedFile.FileName.Split('.').Length - 1;
                    if (DoubleExten == 1)
                    {
                        if (File_Upload.PostedFile.ContentType == "application/pdf")
                        {

                            extension = Path.GetExtension(File_Upload.PostedFile.FileName.ToString());
                            if (extension.ToLower() == ".pdf")
                            {
                                if (File_Upload.PostedFile.ContentLength > 1048576)//
                                {

                                    MaxPdfSizeKB = 1024;
                                    MaxPdfSizeBytes = MaxPdfSizeKB * 1024;
                                    PDFSize = File_Upload.PostedFile.ContentLength;
                                    if (PDFSize > MaxPdfSizeBytes)
                                    {

                                        clsAlert.AlertMsg(this, "Document size should not be greater than 1 MB.");
                                        File_Upload.Focus();
                                        return;  // text/plain
                                    }
                                }
                                using (var binaryReader = new BinaryReader(File_Upload.PostedFile.InputStream))
                                {
                                    DocumentBytes = binaryReader.ReadBytes(File_Upload.PostedFile.ContentLength);
                                    Document = Convert.ToBase64String(DocumentBytes, 0, DocumentBytes.Length);

                                }

                            }
                        }
                        else
                        {
                            clsAlert.AlertMsg(this, "Please upload PDF file Only.");
                            return;
                        }
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Invalid filename/format.");
                        lblFilename.Text = "";
                        File_Upload.Focus();
                        return;
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "Please upload Document.");
                    return;
                }


                if (txtRegId.Text != "" && txtRemarks.Text != "" && lblRegId.Text != "")
                {
                    clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                    CSR.RegId = lblRegId.Text.Trim();
                    CSR.Name = lblStudentName.Text.Trim();
                    CSR.Remarks = txtRemarks.Text.Trim();
                    CSR.MobileNo = hdMobileNo.Value.Trim();
                    CSR.Email = lblEmailId.Text.Trim();
                    CSR.docs = Document;
                    CSR.UserId = Convert.ToString(Session["UserId"]);
                    CSR.IPAddress = GetIPAddress();
                    CSR.Collegeid = Session["CollegeId"].ToString();
                    CSR.SLCBogus = rdbtnSLC.SelectedValue;
                    string s = CSR.CancelAdmissions();
                    if (s == "1")
                    {
                        if (hdMobileNo.Value != "")
                        {
                            string smstext = string.Empty;
                            smstext = "Dear Candidate, on your request submitted to ITI, your provisional admission against Registration ID: " + lblRegId.Text + " is cancelled by the ITI authorities. Regards, SDIT Haryana.";
                            AgriSMS.sendSingleSMS(hdMobileNo.Value.Trim(), smstext, "1007126623477871798");
                        }
                        string urlSubject = "Cancellation Your Provisional Admission Registration ID " + lblRegId.Text;
                        string msg = string.Empty;
                        msg = "Dear Candidate, on your request submitted to ITI, your provisional admission against Registration ID: " + lblRegId.Text + " is cancelled by the authorities.<br/> Remarks: " + txtRemarks.Text + " <br/> Regards, SDIT Haryana.";
                        if (lblEmailId.Text != "")
                        {
                            SMS.SendEmail(lblEmailId.Text, urlSubject, msg.Trim());
                        }
                        clsAlert.AlertMsg(this, "Student Admission " + lblRegId.Text.Trim() + " has been cancelled successfully.");
                        clear();
                        
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Student Admission not cancelled..try again later.");
                        return;
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "There is some problem in Remarks or Registation Id or Roll No.");
                    return;

                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmCancelStudentAdmission";
                clsLogger.ExceptionMsg = "btnDelete_Click";
                clsLogger.SaveException();
            }
        }

        public void clear()
        {
            txtRegId.Text = "";
            txtRemarks.Text = "";
            lblStudentName.Text = "";
            lblStuFatherName.Text = "";
            lblRegId.Text = "";
            lblCollege.Text = "";
          //  lblCourses.Text = "";
            lblSectionName.Text = "";
            //lblSubComb.Text = "";
            lblMobileNo.Text = "";
            lblEmailId.Text = "";
            hdMobileNo.Value = "";
            dvSection.Attributes.Remove("class");
            disable();

        }
        public void disable()
        {
            dvStuName.Style.Add("display", "none");
            dvStuFatherName.Style.Add("display", "none");
            dvRegId.Style.Add("display", "none");
            dvCollege.Style.Add("display", "none");
          //  dvCourses.Style.Add("display", "none");
            dvSectionName.Style.Add("display", "none");
            //dvSubComb.Style.Add("display", "none");
            dvMobileNo.Style.Add("display", "none");
            dvEmail.Style.Add("display", "none");
            dvCounselling.Style.Add("display", "none");
            dvCancel.Style.Add("display", "none");
            dvNote.Style.Add("display", "none");
        }

        public void enable()
        {
            dvStuName.Style.Add("display", "inline-block");
            dvStuFatherName.Style.Add("display", "inline-block");
            dvRegId.Style.Add("display", "inline-block");
            dvCollege.Style.Add("display", "inline-block");
           // dvCourses.Style.Add("display", "inline-block");
            dvSectionName.Style.Add("display", "inline-block");
            //dvSubComb.Style.Add("display", "inline-block");
            dvMobileNo.Style.Add("display", "inline-block");
            dvEmail.Style.Add("display", "inline-block");
            dvCounselling.Style.Add("display", "inline-block");
            dvCancel.Style.Add("display", "inline-block");
            dvNote.Style.Add("display", "inline-block");
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

        
    }
   
}
    