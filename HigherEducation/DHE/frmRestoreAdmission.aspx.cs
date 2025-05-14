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
    public partial class frmRestoreAdmission : System.Web.UI.Page
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


        //public void CheckSeatFreeze()
        //{
        //    string checkseatfreeze = "";
        //    string flgcheckseatfreeze = "y";
        //    try
        //    {
        //        clsRestoreAdmission CSR = new clsRestoreAdmission();
        //        DataTable dt = new DataTable();
        //        CSR.Collegeid = Convert.ToString(Session["CollegeId"]);
        //        dt = CSR.checkseatfreeze();
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; ++i)
        //            {
        //                if (string.IsNullOrEmpty(dt.Rows[i]["IsFreezed"].ToString()) || dt.Rows[i]["IsFreezed"].ToString()=="n")
        //                    {
        //                    checkseatfreeze = "";
        //                    flgcheckseatfreeze = "n";
        //                    }
        //                else
        //                {
        //                    checkseatfreeze = dt.Rows[i]["IsFreezed"].ToString();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            clsAlert.AlertMsg(this, "there is no freeze seat.");
        //            return;
        //        }

        //        if (checkseatfreeze.ToLower()=="y" && flgcheckseatfreeze=="y")
        //        {
        //            btnGo.Enabled = true;
        //        }
        //        else
        //        {
        //            btnGo.Enabled = false;
        //            clsAlert.AlertMsg(this, "Please freeze your subject combination seats before offering a seat.");
        //            return;
        //        }

        //        }
        //    catch (Exception ex)
        //    {

        //        clsLogger.ExceptionError = ex.Message;
        //        clsLogger.ExceptionPage = "DHE/frmOfferingSeats";
        //        clsLogger.ExceptionMsg = "checkseatfreeze";
        //        clsLogger.SaveException();
        //    }
            
        //}

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                //  txtRemarks.Text = "";

                clsRestoreAdmission CSR = new clsRestoreAdmission();
                DataTable dt = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                CSR.Collegeid = Convert.ToString(Session["CollegeId"]);
                dt = CSR.GetCancelledAdmissionStudentInfo();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    lblStuMotherName.Text = dt.Rows[0]["MotherName"].ToString();
                    lblGender.Text = dt.Rows[0]["GenderName"].ToString();
                    lblDOB.Text = dt.Rows[0]["BirthDate"].ToString();
                    lblExamPassed.Text = dt.Rows[0]["exampassed"].ToString();
                    lblCollegeName.Text = dt.Rows[0]["collegename"].ToString();
                    lblSectionName.Text = dt.Rows[0]["SectionName"].ToString();
                    lblAdmissionStatus.Text = dt.Rows[0]["Challan_status"].ToString();
                    // Session["RegId"] = txtRegId.Text;
                    enable();
                   
                   // if (Session["CollegeType"].ToString() == "1")//Govt College
                   // {
                   //     dvPMSSCScholarShip.Style.Add("display", "none");
                   //}
                    //else
                    //{
                    //    dvPMSSCScholarShip.Style.Add("display", "inline-block");
                    //}

                    


                }
                else
                {
                    dvSection.Attributes.Remove("class");
                    clear();
                    clsAlert.AlertMsg(this, "Student Admission is not Cancelled.");
                    return;
                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmOfferingSeats";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }
        
        //Offer Seats Save
        protected void btnSave_Click(object sender, EventArgs e)
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
                    clsRestoreAdmission CSR = new clsRestoreAdmission();
                    CSR.RegId = lblRegId.Text.Trim();
                    CSR.Name = lblStudentName.Text.Trim();
                    CSR.Remarks = txtRemarks.Text.Trim();
                    CSR.docs = Document;
                    CSR.UserId = Convert.ToString(Session["UserId"]);
                    CSR.IPAddress = GetIPAddress();
                    CSR.Collegeid = Session["CollegeId"].ToString();
                    //CSR.SLCBogus = rdbtnSLC.SelectedValue;
                    string s = CSR.RestoreAdmissions();
                    if (s == "1")
                    {
                        clsAlert.AlertMsg(this, "Student Admission " + lblRegId.Text.Trim() + " has been restored successfully.");
                        clear();
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Student Admission not restored... try again later.");
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
                clsLogger.ExceptionPage = "DHE/frmRestoreAdmission";
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
            lblStuMotherName.Text = "";
            lblGender.Text = "";
            lblDOB.Text = "";
            lblExamPassed.Text = "";
            lblCollegeName.Text = "";
            lblSectionName.Text = "";
            lblAdmissionStatus.Text = "";
            txtRemarks.Text = "";


        }

    }

}
