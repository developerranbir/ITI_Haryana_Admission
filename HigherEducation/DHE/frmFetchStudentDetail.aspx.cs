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
    public partial class frmFetchStudentDetail : System.Web.UI.Page
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
            dvEmailId.Style.Add("display", "none");
            dvSubmitDate.Style.Add("display", "none");
            dvUnlockedDate.Style.Add("display", "none");
            dvobjectionRaised.Style.Add("display", "none");
            dvCollegeName.Style.Add("display", "none");
            dvActionHistory.Style.Add("display", "none");
            dvOpenCounselling.Style.Add("display", "none");
            dvDocument.Style.Add("display", "none");
            dvAdmissionStatus.Style.Add("display", "none");
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
            dvSubmitDate.Style.Add("display", "inline-block");
            dvUnlockedDate.Style.Add("display", "inline-block");
            dvobjectionRaised.Style.Add("display", "inline-block");
            dvCollegeName.Style.Add("display", "inline-block");
            dvActionHistory.Style.Add("display", "inline-block");
            dvOpenCounselling.Style.Add("display", "inline-block");
            dvDocument.Style.Add("display", "inline-block");
            dvAdmissionStatus.Style.Add("display", "inline-block");

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

                string maxpage = "0";
                string verificationstatus = "";
                clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                DataTable dt = new DataTable();
                CSR.RollNo = txtRegId.Text.Trim();
                if (ddlUGPG.SelectedValue == "PG")
                {
                    dt = CSR.GetStudentInfo_PG();
                }
                else
                {
                    dt = CSR.GetStudentInfo();
                }
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
                    if (string.IsNullOrEmpty(dt.Rows[0]["FinalSubmitDate"].ToString()))
                    {
                        lblSubmissionDate.Text = "-";
                    }
                    else
                    {
                        lblSubmissionDate.Text = dt.Rows[0]["FinalSubmitDate"].ToString();
                    }
                    if (string.IsNullOrEmpty(dt.Rows[0]["UnlockDateTime"].ToString()))
                    {
                        lblUnlockDate.Text = "-";
                    }
                    else
                    {
                        lblUnlockDate.Text = dt.Rows[0]["UnlockDateTime"].ToString();
                    }

                    if (string.IsNullOrEmpty(dt.Rows[0]["VerifierRemarks"].ToString()))
                    {
                        lblObjectionRaisedRemarks.Text = "-";
                    }
                    else
                    {
                        lblObjectionRaisedRemarks.Text = dt.Rows[0]["VerifierRemarks"].ToString();
                    }
                    if (string.IsNullOrEmpty(dt.Rows[0]["CollegeName"].ToString()))
                    {
                        lblCollegeName.Text = "-";
                    }
                    else
                    {
                        lblCollegeName.Text = dt.Rows[0]["CollegeName"].ToString();
                    }

                    if (string.IsNullOrEmpty(dt.Rows[0]["ApplyforCounselling"].ToString()))
                    {
                        lblOpenCouncelling.Text = "No";
                    }
                    else
                    {
                        lblOpenCouncelling.Text = "Yes";
                    }

                    if (string.IsNullOrEmpty(dt.Rows[0]["XiiDocument"].ToString()))
                    {
                        lnkShowDocument.InnerText = "-";
                    }
                    else
                    {
                        lnkShowDocument.InnerText = "Show Document";
                        AttachmentType cc = new AttachmentType();
                        cc = GetMimeType(dt.Rows[0]["XiiDocument"].ToString());
                        iframepdf.Src = "data:"+ cc._MimeType + ";base64," + dt.Rows[0]["XiiDocument"].ToString();
                    }
                    enable();
                    // GetCandidateInfo();//Result
                    GetAdmissionStatus(); //Admission Status with Counselling 1,2,3
                    GetFeePaidInfo();//Fee Paid
                }
                else
                {
                    dvSection.Attributes.Remove("class");
                    clear();
                    clsAlert.AlertMsg(this, "Student registration not found.");
                    return;
                }
                DataTable dt1 = new DataTable();
                CSR.RollNo = txtRegId.Text.Trim();
                if (ddlUGPG.SelectedValue == "PG")
                {
                    dt1 = CSR.GetActionHistory_PG();
                }
                else
                {
                    dt1 = CSR.GetActionHistory();
                }
                    
                if (dt1.Rows.Count > 0)
                {
                    dvActionHistory.Style.Add("display", "inline-block");
                    grdhistory.DataSource = dt1;
                    grdhistory.DataBind();
                }
                else
                {
                    dvActionHistory.Style.Add("display", "none");
                    grdhistory.DataSource = null;
                    grdhistory.DataBind();
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

        public void GetFeePaidInfo()
        {
            clsResultUGAdm RU = new clsResultUGAdm();
            DataTable dt = new DataTable();
            RU.RegId = txtRegId.Text.Trim();

            dt = RU.GetpaymentSuccessDetail();
            if (dt.Rows.Count > 0)
            {
                dvFeePaid.Style.Add("display", "inline-block");
                GrdFeeDetail.DataSource = dt;
                GrdFeeDetail.DataBind();
            }
            else
            {
                dvFeePaid.Style.Add("display", "none");
                GrdFeeDetail.DataSource = null;
                GrdFeeDetail.DataBind();
            }
        }
        public void GetCandidateInfo()
        {
            clsResultUGAdm RU = new clsResultUGAdm();
            string AllocationSeat;
            string flgAllocation = "0";
            DataTable dt = new DataTable();
            DataTable dtFilter = new DataTable();
            RU.RegId = txtRegId.Text.Trim();
            RU.CandidateName = lblStudentName.Text.Trim();
            RU.Counselling = "0"; //Check both councelling
            dt = RU.GetCandidateInfo();


            if (dt.Rows.Count > 0)
            {
               // lblRegId.Text = dt.Rows[0]["registrationID"].ToString();
              //  lblCandidateName.Text = dt.Rows[0]["applicant_name"].ToString();
              //  lblFatherName.Text = dt.Rows[0]["applicant_father_name"].ToString();
                string eligible = dt.Rows[0]["eligible"].ToString().ToLower();
                if (eligible != "pass")
                {
                    clsAlert.AlertMsg(this, "Sorry! you are not eligible.");
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                 //  dvNote.Style.Add("display", "none");
                    dvGrdResultInfo.Style.Add("display", "none");
                    return;

                }

                dvSection.Style.Add("display", "inline-block");
               // dvNote.Style.Add("display", "inline-block");
                dvGrdResultInfo.Style.Add("display", "inline-block");
                AllocationSeat = dt.Rows[0]["AllocationSeatStatus"].ToString();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AllocationSeat = dt.Rows[i]["AllocationSeatStatus"].ToString();
                    if (AllocationSeat == "1")
                    {
                        dtFilter = dt.AsEnumerable().Where
                          (row => row.Field<string>("AllocationSeatStatus").ToString() == "1").CopyToDataTable();
                        GrdResultInfo.DataSource = dtFilter;
                        GrdResultInfo.DataBind();
                        flgAllocation = "1";
                    }
                }

                if (flgAllocation == "0")
                {
                    clsAlert.AlertMsg(this, "Sorry! No Seat has been allocated to you.");
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                   // dvNote.Style.Add("display", "none");
                    dvGrdResultInfo.Style.Add("display", "none");
                    return;
                }

            }
            else
            {
                DataTable dtStu = new DataTable();

                RU.RegId = txtRegId.Text.Trim();
                dtStu = RU.GetStudentInfo();
                if (dtStu.Rows.Count > 0)
                {
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                    if (dtStu.Rows[0]["StudentName"].ToString().ToLower() == lblStudentName.Text.ToLower())
                    {
                        
                    }
                    else
                    {
                        //dvSection.Style.Add("display", "none");
                        dvGrdResultInfo.Style.Add("display", "none");
                        GrdResultInfo.DataSource = null;
                        GrdResultInfo.DataBind();
                        clsAlert.AlertMsg(this, "Candidate Record Not Found");
                        return;
                    }
                    //dvNote.Style.Add("display", "none");
                    dvGrdResultInfo.Style.Add("display", "none");
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                    clsAlert.AlertMsg(this, "Sorry!! No Seat has been allocated to you.");
                    return;
                }
                else
                {
                   // dvSection.Style.Add("display", "none");
                   // dvNote.Style.Add("display", "none");
                    dvGrdResultInfo.Style.Add("display", "none");
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                    clsAlert.AlertMsg(this, "Record not found");
                    return;
                }

            }
        }


        public void GetAdmissionStatus()
        {
            clsOfferingSeats COS = new clsOfferingSeats();
            DataSet dt = new DataSet();
            COS.RegId = txtRegId.Text.Trim();
            if (ddlUGPG.SelectedValue=="PG")
            {
                dt = COS.GetAdmissionStatus_PG();
            }
            else
            {
                dt = COS.GetAdmissionStatus();
            }
          
            if (dt.Tables[1].Rows.Count > 0)
            {
                dvAdmissionStatus.Style.Add("display", "inline-block");
                lbladmissionStatus.Style.Add("display", "inline-block");
                GrdAdmissionStatus.DataSource = dt.Tables[1];
                GrdAdmissionStatus.DataBind();

            }
            else
            {
                lbladmissionStatus.Style.Add("display", "none");
                dvAdmissionStatus.Style.Add("display", "none");
                GrdAdmissionStatus.DataSource = null;
                GrdAdmissionStatus.DataBind();
            }
        }


        public void clear()
        {
            disable();
            lblStudentName.Text = "";
            lblStuFatherName.Text = "";
            lblRegId.Text = "";
            lblBoard.Text = "";
            lblPassingYear.Text = "";
            lblMobileNo.Text = "";
            lblEmailId.Text = "";
            lblSubmissionDate.Text = "";
            lblUnlockDate.Text = "";
            lblUnlockDate.Text = "";
            lblObjectionRaisedRemarks.Text = "";
            lblCollegeName.Text = "";
            lblOpenCouncelling.Text = "";
            lnkShowDocument.InnerText = "";
        

            dvFeePaid.Style.Add("display", "none");
            GrdFeeDetail.DataSource = null;
            GrdFeeDetail.DataBind();

            lbladmissionStatus.Style.Add("display", "none");
            dvAdmissionStatus.Style.Add("display", "none");
            GrdAdmissionStatus.DataSource = null;
            GrdAdmissionStatus.DataBind();

            dvGrdResultInfo.Style.Add("display", "none");
            GrdResultInfo.DataSource = null;
            GrdResultInfo.DataBind();

            dvActionHistory.Style.Add("display", "none");
            grdhistory.DataSource = null;
            grdhistory.DataBind();

        }
        protected AttachmentType GetMimeType(string value)
        {
            AttachmentType returnvalue = new AttachmentType();
            if (String.IsNullOrEmpty(value))
            {
                returnvalue._FriendlyName = "Unknown";
                returnvalue._MimeType = "application/octet-stream";
                returnvalue._Extension = "";
            }

            string data = value.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    returnvalue._FriendlyName = "Photo";
                    returnvalue._MimeType = "image/png";
                    returnvalue._Extension = ".png";
                    break;
                case "/9J/4":

                    returnvalue._FriendlyName = "Photo";
                    returnvalue._MimeType = "image/jpeg";
                    returnvalue._Extension = ".jpeg";
                    break;

                case "AAAAF":

                    returnvalue._FriendlyName = "Video";
                    returnvalue._MimeType = "video/mp4";
                    returnvalue._Extension = ".mp4";
                    break;
                case "JVBER":

                    returnvalue._FriendlyName = "Document";
                    returnvalue._MimeType = "application/pdf";
                    returnvalue._Extension = ".pdf";
                    break;
                case "UESDB":

                    returnvalue._FriendlyName = "kmz";
                    returnvalue._MimeType = "application/vnd.google-earth.kmz";
                    returnvalue._Extension = ".kmz";
                    break;
                case "PD94B":
                    returnvalue._FriendlyName = "Kml";
                    returnvalue._MimeType = "application/vnd.google-earth.kml+xml";
                    returnvalue._Extension = ".kml";
                    break;
                case "QUMXM":

                    returnvalue._FriendlyName = "dwg";
                    returnvalue._MimeType = "application/acad, application/x-acad, application/autocad_dwg, image/x-dwg, application/dwg, application/x-dwg, application/x-autocad, image/vnd.dwg, drawing/dwg";
                    returnvalue._Extension = ".dwg";
                    break;
                default:

                    returnvalue._FriendlyName = "Unknown";
                    returnvalue._MimeType = string.Empty;
                    returnvalue._Extension = "";
                    break;
            }
            return returnvalue;
        }
        public class AttachmentType
        {
            string MimeType;

            public string _MimeType
            {
                get { return MimeType; }
                set { MimeType = value; }
            }
            string FriendlyName;

            public string _FriendlyName
            {
                get { return FriendlyName; }
                set { FriendlyName = value; }
            }
            string Extension;

            public string _Extension
            {
                get { return Extension; }
                set { Extension = value; }
            }
        }
    }
   
}
    