using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;

namespace HigherEducation.HigherEducations
{
    public partial class UploadCollegeProspectus : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)

        {
            string UserType = "2";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            //  eSessionMgmt.checkreferer();
            clsLoginUser.CheckSession(UserType);
            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);

            }

            else
            {
                if (!Page.IsPostBack)
                {
                    GetCollegeProfile();
                }

                txtCollegeName.Text = Convert.ToString(Session["CollegeName"]);
              //  txtAffiliated.Text = Convert.ToString(Session["univeristyname"]);

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }

        public void GetCollegeProfile()
        {
            clsCollegeGlance cg = new clsCollegeGlance();
            cg.collegeid = Convert.ToString(Session["CollegeId"]);
            DataTable dt = new DataTable();
            dt = cg.GetCollegeProfile();
            txtCollegeName.Text = Convert.ToString(Session["CollegeName"]);

            if (dt.Rows.Count > 0)
            {
               // txtAffiliated.Text = dt.Rows[0]["univeristyname"].ToString();
             //   Session["univeristyname"] = txtAffiliated.Text;

                Image1.ImageUrl = "~/assets/images/preview.png";

                if (string.IsNullOrEmpty(dt.Rows[0]["docs"].ToString()))
                {
                    hdPros.Value = "";
                }
                else
                {
                    hdProsDoc.Value = dt.Rows[0]["docs"].ToString();
                    hdPros.Value = "p";
                    iframepdf.Src = "data:application/pdf;base64," + dt.Rows[0]["docs"].ToString();


                }

            }
            else
            {
             //   txtAffiliated.Text = "-";
                hdPros.Value = "";
            }
        }




        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string extension = "";
            int MaxPdfSizeKB = 0;
            int MaxPdfSizeBytes = 0;
            int PDFSize = 0;
            // byte[] ProspectusBytes;
            String Prospectus = "";
            try
            {
                if (Page.IsValid)
                {


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
                                    if (File_Upload.PostedFile.ContentLength > 5242880)//
                                    {

                                        MaxPdfSizeKB = 1024;
                                        MaxPdfSizeBytes = MaxPdfSizeKB * 1024;
                                        PDFSize = File_Upload.PostedFile.ContentLength;
                                        if (PDFSize > MaxPdfSizeBytes)
                                        {

                                            clsAlert.AlertMsg(this, "Prospectus size should not be greater than 5 MB.");
                                            File_Upload.Focus();
                                            return;  // text/plain
                                        }
                                    }
                                    //using (var binaryReader = new BinaryReader(File_Upload.PostedFile.InputStream))
                                    //{
                                    //  ProspectusBytes = binaryReader.ReadBytes(File_Upload.PostedFile.ContentLength);
                                    //Prospectus = Convert.ToBase64String(ProspectusBytes, 0, ProspectusBytes.Length);

                                    //  }

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

                        //MIMEType Check
                        bool returnMIMEType;
                        returnMIMEType = false;
                        byte[] FileDet = null;
                        Stream str = File_Upload.PostedFile.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        FileDet = Br.ReadBytes((Int32)str.Length);
                        string base64ImageRepresentation = Convert.ToBase64String(FileDet);
                        string substring = "JVBER";

                        if (base64ImageRepresentation.Contains(substring))
                        {
                            returnMIMEType = true;

                            Prospectus = base64ImageRepresentation;
                        }
                        if (returnMIMEType == false)
                        {
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alertmsg('Invalid File Format..!!');", true);
                            clsAlert.AlertMsg(this, "Invalid File Format. Only pdf file allowed!!");
                            Session.Add("error", "true");
                            return;
                        }
                        //MIMEType Check

                    }


                    if (Prospectus == "")
                    {
                        clsAlert.AlertMsg(this, "Invalid filename/format. Try again.");
                        lblFilename.Text = "";
                        File_Upload.Focus();
                        return;
                    }

                    txtCollegeName.Text = Convert.ToString(Session["CollegeName"]);
                //    txtAffiliated.Text = Convert.ToString(Session["univeristyname"]);
                    clsCollegeGlance cg = new clsCollegeGlance();
                    cg.collegeid = Convert.ToString(Session["CollegeId"]);
                    cg.Prospectus = Prospectus;
                    cg.UserId = Convert.ToString(Session["UserId"]);
                    string IPAddress = GetIPAddress();
                    cg.IPAddress = IPAddress;
                    string s = cg.UploadProspectus();
                    if (s == "1")
                    {
                        clsAlert.AlertMsg(this, "Prospectus Updated Successfully.");
                        GetCollegeProfile();
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Prospectus not Updated.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/UploadCollegeProspectus";
                clsLogger.ExceptionMsg = "btnSubmit_Click";
                clsLogger.SaveException();
            }


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

        public void clearall()
        {
           // txtAffiliated.Text = "";

        }
        //protected void btnPros_Click(object sender, EventArgs e)
        //{

        //    string filename = "Prospectus";
        //    byte[] pdfBytes = Convert.FromBase64String(hdPros.Value);
        //    MemoryStream ms = new MemoryStream(pdfBytes);
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "inline;filename=" + filename + ".pdf");//attachment for download ; inline for show
        //    Response.Buffer = true;
        //    ms.WriteTo(Response.OutputStream);
        //    Response.Flush();
        //    Response.SuppressContent = true;

        //}
    }


}