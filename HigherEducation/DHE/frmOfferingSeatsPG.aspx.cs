using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmOfferingSeatsPG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            clsLoginUser.CheckSession(Session["UserType"].ToString());
           
            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
                return;
            }
            if (!Page.IsPostBack)
            {
                hdflgDelete.Value = "";
                CheckSeatFreeze();
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }

        public void disable()
        {
            dvSection.Style.Add("display", "none");
            dvCatgSeatAllotment.Style.Add("display", "none");
            dvPMSSCScholarShip.Style.Add("display", "none");
            dvchkId.Style.Add("display", "none");
            dvSave.Style.Add("display", "none");
            dvNote.Style.Add("display", "none");
            dvCaptcha.Style.Add("display", "none");
            dvPayNow.Style.Add("display", "none");
        }

        public void enable()
        {
            dvSection.Style.Add("display", "inline-block");
            dvCatgSeatAllotment.Style.Add("display", "inline-block");
            // dvPMSSCScholarShip.Style.Add("display", "inline-block");
            dvchkId.Style.Add("display", "inline-block");
            dvSave.Style.Add("display", "inline-block");
            dvNote.Style.Add("display", "inline-block");
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


        public void CheckSeatFreeze()
        {
            string checkseatfreeze = "";
            string flgcheckseatfreeze = "y";
            try
            {
                clsOfferingSeatsPG CSR = new clsOfferingSeatsPG();
                DataTable dt = new DataTable();
                CSR.Collegeid = Convert.ToString(Session["CollegeId"]);
                dt = CSR.checkseatfreezePG();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["IsFreezed"].ToString()) || dt.Rows[i]["IsFreezed"].ToString()=="n")
                            {
                            checkseatfreeze = "";
                            flgcheckseatfreeze = "n";
                            }
                        else
                        {
                            checkseatfreeze = dt.Rows[i]["IsFreezed"].ToString();
                        }
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "there is no freeze seat.");
                    return;
                }

                if (checkseatfreeze.ToLower()=="y" && flgcheckseatfreeze=="y")
                {
                    btnGo.Enabled = true;
                }
                else
                {
                    btnGo.Enabled = false;
                    clsAlert.AlertMsg(this, "Please freeze your course seats before offering a seat.");
                    return;
                }

                }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmOfferingSeatsPGPG";
                clsLogger.ExceptionMsg = "checkseatfreezePG";
                clsLogger.SaveException();
            }
            
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                //  txtRemarks.Text = "";

                string maxpage = "0";
                string verificationstatus = "";
                clsOfferingSeatsPG CSR = new clsOfferingSeatsPG();
                DataTable dt = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                CSR.Collegeid = Convert.ToString(Session["CollegeId"]);
                dt = CSR.GetOfferingStudentInfoPG();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    lblStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    lblStuMotherName.Text = dt.Rows[0]["MotherName"].ToString();
                    lblGender.Text = dt.Rows[0]["GenderName"].ToString();
                    lblDOB.Text = dt.Rows[0]["BithDate"].ToString();
                    string AadhaarNo;
                    string MaskedAadhaarNo;
                    if (string.IsNullOrEmpty(dt.Rows[0]["AadhaarNo"].ToString()))

                    {
                        AadhaarNo = "";
                        MaskedAadhaarNo = "";
                        hdAadhaarNo.Value = "";
                    }
                    else
                    {
                        AadhaarNo= dt.Rows[0]["AadhaarNo"].ToString();
                        hdAadhaarNo.Value = AadhaarNo;
                        MaskedAadhaarNo = "XXXXXXXX" + AadhaarNo.Substring(AadhaarNo.Length - 4);
                    }
                    lblAadhaarNo.Text = MaskedAadhaarNo;
                    lblReservCatg.Text = dt.Rows[0]["reservationname"].ToString();
                    lblCasteCatg.Text = dt.Rows[0]["CategoryDesc"].ToString();
                    if (string.IsNullOrEmpty(dt.Rows[0]["castedesc"].ToString()))
                    {
                        lblCaste.Text = "NA";
                    }
                    else
                    {
                        lblCaste.Text = dt.Rows[0]["castedesc"].ToString();
                    }
                    lblExamPassed.Text = dt.Rows[0]["exampassed"].ToString() + " in (" + dt.Rows[0]["PassingYear"].ToString() + ")";
                    lblStream.Text = dt.Rows[0]["streamName"].ToString();
                    lblBoard.Text = dt.Rows[0]["Board"].ToString();
                    lblPassingYear.Text = dt.Rows[0]["PassingYear"].ToString();
                    lblTopFivePercentage.Text = dt.Rows[0]["topfivepercentage"].ToString();
                    lblFamilyId.Text = dt.Rows[0]["familyidfromppp"].ToString();
                    lblTweleveHaryana.Text = dt.Rows[0]["TwelveHarana"].ToString();
                    lblDomicile.Text = dt.Rows[0]["Has_Domicile"].ToString();
                    int weightage = 0;

                    if (Convert.ToInt32(dt.Rows[0]["WeightagePercentage"]) > 10)//Maximum 10
                    {
                        weightage = 10;
                    }
                    else
                    {
                        weightage = Convert.ToInt32(dt.Rows[0]["WeightagePercentage"]);
                    }

                    lblWeightage.Text = weightage.ToString();
                    Decimal topfivepercentage;
                    if (string.IsNullOrEmpty(dt.Rows[0]["topfivepercentage"].ToString()))
                    {
                        topfivepercentage = 0;
                    }
                    else
                    {
                        topfivepercentage = Convert.ToDecimal(lblTopFivePercentage.Text);
                    }
                    lblTotalPercentage.Text = Convert.ToString(weightage + topfivepercentage);
                    hdBoard_Code.Value = dt.Rows[0]["Board_Code"].ToString();
                    hdqualification.Value = dt.Rows[0]["exampassed"].ToString();
                    hdGenderid.Value = dt.Rows[0]["Genderid"].ToString();
                    hdRollNo.Value = dt.Rows[0]["RollNo"].ToString();
                    hdCollegeid.Value = Convert.ToString(Session["CollegeId"]);
                    hdCollegeName.Value = dt.Rows[0]["CollegeName"].ToString();
                    hdReservationcategoryid.Value = dt.Rows[0]["Reservationcategoryid"].ToString();
                    hdcategoryid.Value = dt.Rows[0]["categoryid"].ToString();
                    hdcasteid.Value = dt.Rows[0]["Casteid"].ToString();
                    hdmobileno.Value = dt.Rows[0]["MobileNo"].ToString();
                    hdemailid.Value = dt.Rows[0]["EmailId"].ToString();
                    Session["RegId"] = txtRegId.Text;
                    enable();

                   
                   
                    if (Session["CollegeType"].ToString() == "1")//Govt College
                    {
                        dvPMSSCScholarShip.Style.Add("display", "none");
                        RequiredFieldValidator2.Enabled = false;
                    }
                    else
                    {
                        dvPMSSCScholarShip.Style.Add("display", "none"); //changes
                        RequiredFieldValidator2.Enabled = false;

                    }
                    if (hdReservationcategoryid.Value == "1" && lblDomicile.Text.ToLower() == "yes" && Session["CollegeType"].ToString() != "1") //SC and Domicile Haryana and not govt college
                    {
                        dvPMSSCScholarShip.Style.Add("display", "inline-block");
                        rdbtnPMS_SC.ClearSelection();
                        RequiredFieldValidator2.Enabled = true;
                    }

                    GetAdmissionStatus();
                    GetSeatallotment_open1();

                    


                }
                else
                {
                    dvSection.Attributes.Remove("class");
                    clear();
                    clsAlert.AlertMsg(this, "Student registration not found.");
                    return;
                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmOfferingSeatsPG";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }
        public void GetAdmissionStatus()
        {
            clsOfferingSeatsPG COS = new clsOfferingSeatsPG();
            DataSet dt = new DataSet();
            COS.RegId = txtRegId.Text.Trim();
            dt = COS.GetAdmissionStatus_PG();
            if (dt.Tables[1].Rows.Count > 0)
            {
                dvAdmissionStatus.Style.Add("display", "inline-block");
                dvFeeReceipt.Style.Add("display", "none");
                GrdAdmissionStatus.DataSource = dt.Tables[1];
                GrdAdmissionStatus.DataBind();

            }
            else
            {
                dvAdmissionStatus.Style.Add("display", "none");
                dvFeeReceipt.Style.Add("display", "none");
                GrdAdmissionStatus.DataSource = null;
                GrdAdmissionStatus.DataBind();
            }
        }
        public void CourseSectionDetail()
        {
            clsOfferingSeatsPG COS = new clsOfferingSeatsPG();
            DataTable dt = new DataTable();
            COS.RegId = txtRegId.Text.Trim();
            COS.Collegeid = hdCollegeid.Value;
            dt = COS.GetCourseSectionDetailsPG();
            if (dt.Rows.Count > 0)
            {
                dvGrdCourseSection.Style.Add("display", "inline-block");
                GrdCourseSection.DataSource = dt;
                GrdCourseSection.DataBind();
               

            }
            else
            {
                dvGrdCourseSection.Style.Add("display", "none");
                GrdCourseSection.DataSource = dt;
                GrdCourseSection.DataBind();
            }
        }
        public void GetSeatallotment_open1()
        {
            btnSave.Style.Add("display", "inline-block");
            string Counselling_Date;
            string Counselling;
            DateTime CounsellingDate = DateTime.Now.AddDays(-1);
            DateTime currdate = DateTime.Today;
            clsOfferingSeatsPG COS = new clsOfferingSeatsPG();
            DataTable dt = new DataTable();
            COS.RegId = txtRegId.Text.Trim();
            COS.Collegeid = hdCollegeid.Value;
            dt = COS.GetSeatallotment_open1_PG();
            if (dt.Rows.Count > 0)
            {
                //Check One PAY NOW visible
                string collegetype = dt.Rows[0]["collegetype"].ToString();
                string counsellingstatus = dt.Rows[0]["Counselling_Status"].ToString();
                string challanstatus = dt.Rows[0]["Challan_Status"].ToString();

                if (string.IsNullOrEmpty(dt.Rows[0]["Counselling_Date"].ToString()))
                {
                    Counselling_Date = "";
                }
                else
                {
                    Counselling_Date = dt.Rows[0]["Counselling_Date"].ToString();
                    CultureInfo engb = new CultureInfo("en-GB");
                    CounsellingDate = Convert.ToDateTime(Counselling_Date, engb);

                    currdate = DateTime.Now.Date;

                    CultureInfo engb1 = new CultureInfo("en-GB");
                    currdate = Convert.ToDateTime(currdate, engb1);

                }
                if (string.IsNullOrEmpty(dt.Rows[0]["Counselling"].ToString()))
                {
                    Counselling = "";
                }
                else
                {
                    Counselling = dt.Rows[0]["Counselling"].ToString();
                }

                hdCourseid2.Value = dt.Rows[0]["Courseid"].ToString();
                hdSectionid2.Value = dt.Rows[0]["Sectionid"].ToString();
                hdSubCombid2.Value = dt.Rows[0]["subjectcombinationid"].ToString();


                if (challanstatus.ToLower() == "success" && Counselling == "3")//Check from seatallotment2020_open1 if status=success
                {
                    dvGrdCourseSection2.Style.Add("display", "none");
                    dvChangeOffer.Style.Add("display", "none");
                    dvCatgSeatAllotment.Style.Add("display", "none");
                    dvPMSSCScholarShip.Style.Add("display", "none");
                    dvchkId.Style.Add("display", "none");
                    dvGrdCourseSection2.Style.Add("display", "none");
                    dvGrdCourseSection.Style.Add("display", "none");
                    dvSave.Style.Add("display", "none");
                    dvNote.Style.Add("display", "none");
                    dvCaptcha.Style.Add("display", "none");
                    clsAlert.AlertMsg(this, "This Student has already taken admission.");
                    return;

                }

                if (counsellingstatus == "OS" && challanstatus.ToLower() != "success" && CounsellingDate == currdate)
                {
                    dvGrdCourseSection2.Style.Add("display", "inline-block");
                    GrdCourseSection2.DataSource = dt;
                    GrdCourseSection2.DataBind();
                    //Change offer show when more than one 

                    DataTable dt1 = new DataTable();
                    COS.RegId = txtRegId.Text.Trim();
                    COS.Collegeid = hdCollegeid.Value;
                    dt1 = COS.GetCourseSectionDetailsPG();
                    if (dt1.Rows.Count > 1)// More than one record on grid(datatable)
                    {

                        dvChangeOffer.Style.Add("display", "inline-block");
                        hdflgDelete.Value = "y";//Delete Record of this RegId,currentDate from seatallotment2020_open1 
                        dvNote.Style.Add("display", "none");
                        dvchkId.Style.Add("display", "none");
                        btnSave.Style.Add("display", "none");
                        dvCatgSeatAllotment.Style.Add("display", "none");
                        dvPMSSCScholarShip.Style.Add("display", "none");
                        dvCaptcha.Style.Add("display", "none");
                    }
                    else
                    {
                        dvChangeOffer.Style.Add("display", "none");
                        dvNote.Style.Add("display", "none");
                        dvchkId.Style.Add("display", "none");
                        btnSave.Style.Add("display", "none");
                        dvCatgSeatAllotment.Style.Add("display", "none");
                        dvPMSSCScholarShip.Style.Add("display", "none");
                        dvCaptcha.Style.Add("display", "none");
                    }
                    dvGrdCourseSection.Style.Add("display", "none");
                    GrdCourseSection.DataSource = null;
                    GrdCourseSection.DataBind();
                }

                else
                {
                    dvGrdCourseSection2.Style.Add("display", "none");
                    dvChangeOffer.Style.Add("display", "none");
                    GrdCourseSection2.DataSource = null;
                    GrdCourseSection2.DataBind();
                    CourseSectionDetail();
                    hdflgDelete.Value = "";
                    btnSave.Style.Add("display", "inline-block");

                }

                if ((collegetype == "2" || collegetype == "3") && counsellingstatus == "OS" && challanstatus.ToLower() != "success" && CounsellingDate == currdate)
                {
                    Session["Pay"] = "y";
                    dvPayNow.Style.Add("display", "Inline-block");
                }
                else
                {
                    Session["Pay"] = "";
                    dvPayNow.Style.Add("display", "none");
                }
            }
            else
            {
                dvGrdCourseSection2.Style.Add("display", "none");
                dvChangeOffer.Style.Add("display", "none");
                dvPayNow.Style.Add("display", "none");
                GrdCourseSection2.DataSource = null;
                GrdCourseSection2.DataBind();
                CourseSectionDetail();
                hdflgDelete.Value = "";

            }
        }
        //Offer Seats Save
        protected void btnSave_Click(object sender, EventArgs e)
        {


            RadioButton r1 = new RadioButton();
            int flgCourseSection = 0;
            HiddenField hdCourseid = new HiddenField();
            HiddenField hdSectionid = new HiddenField();
            HiddenField hdSubComb = new HiddenField();
            Label lblCourseName = new Label();
            Label lblSectionName = new Label();
            Label lblSubCombName = new Label();
            try
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
                r1.Checked = false;
                {
                    if (Page.IsValid)
                    {
                        if (chkId.Checked == true)
                        {
                            if (ddlCatgAllotment.SelectedValue == "0")
                            {
                                clsAlert.AlertMsg(this, "Please Select Category ALLotment");
                                return;
                            }
                            if ((rdbtnPMS_SC.SelectedValue == "0" || rdbtnPMS_SC.SelectedValue == "") && ddlCatgAllotment.SelectedValue == "SC" && ddlCatgAllotment.SelectedValue == "DSC")
                            {
                                clsAlert.AlertMsg(this, "Please Select SCScholarShip.");
                                return;
                            }
                            if ((rdbtnPMS_SC.SelectedValue == "0" || rdbtnPMS_SC.SelectedValue == "") && hdReservationcategoryid.Value == "1" && lblDomicile.Text.ToLower() == "yes" && Session["CollegeType"].ToString() != "1") //SC and Domicile Haryana and not govt college
                            {
                                clsAlert.AlertMsg(this, "Please Select SCScholarShip.");
                                return;
                            }

                            foreach (GridViewRow row in GrdCourseSection2.Rows)//if Record exists in Open1 table
                            {

                                if (row.RowType == DataControlRowType.DataRow)
                                {

                                    flgCourseSection = 1;
                                    hdCourseid.Value = (row.FindControl("hdCourseid") as HiddenField).Value;
                                    hdSectionid.Value = (row.FindControl("hdSectionid") as HiddenField).Value;
                                    hdSubComb.Value = (row.FindControl("hdSubComb") as HiddenField).Value;
                                    lblCourseName.Text = (row.FindControl("lblCourseName") as Label).Text;
                                    lblSectionName.Text = (row.FindControl("lblSectionName") as Label).Text;
                                    lblSubCombName.Text = (row.FindControl("lblSubComb") as Label).Text;



                                }
                            }
                            foreach (GridViewRow row in GrdCourseSection.Rows)
                            {

                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    r1 = (row.FindControl("RadioButton1") as RadioButton);
                                    if (r1.Checked == true)
                                    {
                                        flgCourseSection = 1;
                                        hdCourseid.Value = (row.FindControl("hdCourseid") as HiddenField).Value;
                                        hdSectionid.Value = (row.FindControl("hdSectionid") as HiddenField).Value;
                                        hdSubComb.Value = (row.FindControl("hdSubComb") as HiddenField).Value;
                                        lblCourseName.Text = (row.FindControl("lblCourseName") as Label).Text;
                                        lblSectionName.Text = (row.FindControl("lblSectionName") as Label).Text;
                                        lblSubCombName.Text = (row.FindControl("lblSubComb") as Label).Text;
                                    }


                                }
                            }

                            if (lblCaste.Text == "NA")
                            {
                                lblCaste.Text = "0";
                            }

                            //string VacantSeat = GetCategoryWiseVacantSeat(hdCourseid.Value, hdSectionid.Value);
                            //if (VacantSeat == "0")
                            //{
                            //    //clsAlert.AlertMsg(this, "There is no vacant seat in " + ddlCatgAllotment.SelectedValue + " Category");
                            //   // return;
                            //}
                            //  else
                            //  {

                            if (flgCourseSection == 1)
                            {
                                clsOfferingSeatsPG COS = new clsOfferingSeatsPG();
                                COS.RegId = txtRegId.Text;
                                COS.Name = lblStudentName.Text;
                                COS.FatherName = lblStuFatherName.Text;
                                COS.MotherName = lblStuMotherName.Text;
                                COS.Gender = hdGenderid.Value;
                                CultureInfo engb = new CultureInfo("en-GB");
                                DateTime BirthDate;
                                BirthDate = Convert.ToDateTime(lblDOB.Text, engb);
                                COS.BirthDate = BirthDate;
                                COS.AadhaarNo = hdAadhaarNo.Value;
                                COS.ReservCatgid = hdReservationcategoryid.Value;
                                COS.ReservCatgName = lblReservCatg.Text;
                                COS.Categoryid = hdcategoryid.Value;
                                COS.Category = lblCasteCatg.Text;
                                COS.Casteid = hdcasteid.Value;
                                COS.Caste = lblCaste.Text;
                                COS.BoardName = hdBoard_Code.Value;
                                COS.RollNo = hdRollNo.Value;
                                COS.Collegeid = hdCollegeid.Value;
                                COS.CollegeName = hdCollegeName.Value;
                                COS.Courseid = hdCourseid.Value;
                                COS.CourseName = lblCourseName.Text;
                                COS.Sectionid = hdSectionid.Value;
                                COS.SectionName = lblSectionName.Text;
                                COS.SubCombid = hdSubComb.Value;
                                COS.SubCombName = lblSubCombName.Text;
                                COS.PMS_SC = rdbtnPMS_SC.SelectedValue;
                                COS.SeatAllocationCategory = ddlCatgAllotment.SelectedValue;
                                COS.exampassed = hdqualification.Value;
                                COS.TotalPercentage = lblTotalPercentage.Text;
                                COS.Weightage = lblWeightage.Text;
                                COS.MobileNo = hdmobileno.Value;
                                COS.Email = hdemailid.Value;
                                COS.Domicile = lblDomicile.Text;
                                COS.IPAddress = GetIPAddress();
                                COS.flgDelete = hdflgDelete.Value;
                                COS.TwelveHaryana = lblTweleveHaryana.Text;
                                COS.UserId = Convert.ToString(Session["UserId"]);
                                string s = COS.SaveOfferSeatsPG();
                                if (s == "1")
                                {
                                    if (hdmobileno.Value != "")
                                    {
                                        string smstext = string.Empty;
                                        smstext = "You have been offered a seat in PG course in " + hdCollegeName.Value + " which is valid till midnight. Kindly make the fee payment at the earliest.";
                                        SMS.SendSMS(hdmobileno.Value.Trim(), smstext);
                                    }
                                    string urlSubject = "Offered Seat";
                                    string msg = string.Empty;
                                    msg = "You have been offered a seat in PG course in " + hdCollegeName.Value + " which is valid till midnight. Kindly make the fee payment at the earliest.";
                                    if (hdemailid.Value != "")
                                    {
                                        SMS.SendEmail(hdemailid.Value, urlSubject, msg.Trim());
                                    }
                                    clsAlert.AlertMsg(this, "Seat Offered Successfully");
                                    GetSeatallotment_open1();

                                    if (Session["Pay"] != null)
                                    {
                                        if (Session["Pay"].ToString() == "y")
                                        {

                                            //clsAlert.AlertMsg(this, "FEE PAY.");
                                            Response.Redirect("../Account/FeeModuleCollegePG");
                                        }
                                        else
                                        {
                                            clear();
                                        }
                                    }
                                    else
                                    {
                                        clear();
                                    }

                                }
                                else if (s == "2")
                                {
                                    clsAlert.AlertMsg(this, "There is no vacant seat in this Course.");
                                    return;
                                }

                                else
                                {
                                    clsAlert.AlertMsg(this, "There is some problem...");
                                    return;
                                }
                            }
                            else
                            {
                                clsAlert.AlertMsg(this, "Please select Course to be offered.");
                                return;
                            }
                            // }
                        }
                        else
                        {
                            clsAlert.AlertMsg(this, "Please Select Consent.");
                            chkId.Focus();
                            return;
                        }


                    }
                }
            }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmOfferingSeatsPG";
                clsLogger.ExceptionMsg = "btnSave_Click";
                clsLogger.SaveException();
                clear();
                clsAlert.AlertMsg(this, "There is some problem... try later.");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            disable();
            ddlCatgAllotment.SelectedIndex = -1;
            rdbtnPMS_SC.SelectedValue = "0";
            Session["Pay"] = "";
            hdBoard_Code.Value = "";
            hdqualification.Value = "";
            hdGenderid.Value = "";
            hdRollNo.Value = "";
            hdCollegeName.Value = "";
            hdCollegeid.Value = "";
            hdReservationcategoryid.Value = "";
            hdcategoryid.Value = "";
            hdcasteid.Value = "";
            hdmobileno.Value = "";
            hdemailid.Value = "";
            hdCourseid2.Value = "";
            hdSectionid2.Value = "";
            hdSubCombid2.Value = "";

            txtRegId.Text = "";
            lblStudentName.Text = "";
            lblStuFatherName.Text = "";
            lblStuMotherName.Text = "";
            lblGender.Text = "";
            lblDOB.Text = "";
            lblAadhaarNo.Text = "";
            lblReservCatg.Text = "";
            lblCasteCatg.Text = "";
            lblCaste.Text = "";
            lblExamPassed.Text = "";
            lblStream.Text = "";
            lblBoard.Text = "";
            lblPassingYear.Text = "";
            lblTopFivePercentage.Text = "";
            lblFamilyId.Text = "";
            lblTweleveHaryana.Text = "";
            lblDomicile.Text = "";
            GrdCourseSection2.DataSource = null;
            GrdCourseSection2.DataBind();
            GrdCourseSection.DataSource = null;
            GrdCourseSection.DataBind();
            dvChangeOffer.Style.Add("display", "none");

        }

        //public string GetCategoryWiseVacantSeatPG(string Courseid, string Sectionid)
        //{
        //    DataTable dt = new DataTable();
        //    string VacantSeat = "0";
        //    try
        //    {
        //        clsOfferingSeatsPG COS = new clsOfferingSeatsPG();
        //        COS.Collegeid = hdCollegeid.Value;
        //        COS.Courseid = Courseid;
        //        COS.Sectionid = Sectionid;
        //        COS.GetCategoryWiseVacantSeatPG();
        //        if (dt.Rows.Count > 0)
        //        {
        //            if (ddlCatgAllotment.SelectedValue == "AIOC")
        //            {
        //                VacantSeat = dt.Rows[0]["AIOC"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "HOGC")
        //            {
        //                VacantSeat = dt.Rows[0]["HOGC"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "EWS")
        //            {
        //                VacantSeat = dt.Rows[0]["EWS"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "SC")
        //            {
        //                VacantSeat = dt.Rows[0]["SC"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "DSC")
        //            {
        //                VacantSeat = dt.Rows[0]["DSC"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "BCA")
        //            {
        //                VacantSeat = dt.Rows[0]["BCA"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "BCB")
        //            {
        //                VacantSeat = dt.Rows[0]["BCB"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "DA")
        //            {
        //                VacantSeat = dt.Rows[0]["DA"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "ESM")
        //            {
        //                VacantSeat = dt.Rows[0]["ESM"].ToString();
        //            }
        //            else if (ddlCatgAllotment.SelectedValue == "FF")
        //            {
        //                VacantSeat = dt.Rows[0]["FF"].ToString();
        //            }
        //        }


        //    }
        //    catch (Exception)
        //    {


        //    }
        //    return VacantSeat;
        //}

        protected void btnChangeOffer_Click(object sender, EventArgs e)
        {
            RadioButton r1 = new RadioButton();
            HiddenField hdCourseid = new HiddenField();
            HiddenField hdSectionid = new HiddenField();
            HiddenField hdSubComb = new HiddenField();

            dvGrdCourseSection2.Style.Add("display", "none");
            dvChangeOffer.Style.Add("display", "none");
            dvPayNow.Style.Add("display", "none");
            GrdCourseSection2.DataSource = null;
            GrdCourseSection2.DataBind();
            CourseSectionDetail();
           
            dvNote.Style.Add("display", "inline-block");
            dvchkId.Style.Add("display", "inline-block");
            btnSave.Style.Add("display", "inline-block");
            dvCatgSeatAllotment.Style.Add("display", "inline-block");
            dvCaptcha.Style.Add("display", "block");

            if (Session["CollegeType"].ToString() == "1")//Govt College
            {
                dvPMSSCScholarShip.Style.Add("display", "none");
                RequiredFieldValidator2.Enabled = false;
            }
            else
            {
                dvPMSSCScholarShip.Style.Add("display", "none");
                RequiredFieldValidator2.Enabled = false;
            }
            if (hdReservationcategoryid.Value == "1" && lblDomicile.Text.ToLower() == "yes" && Session["CollegeType"].ToString() != "1") //SC and Domicile Haryana and not govt college
            {
                dvPMSSCScholarShip.Style.Add("display", "inline-block");
                rdbtnPMS_SC.ClearSelection();
                RequiredFieldValidator2.Enabled = true;
            }
            foreach (GridViewRow row in GrdCourseSection.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {

                    r1 = (row.FindControl("RadioButton1") as RadioButton);

                    hdCourseid.Value = (row.FindControl("hdCourseid") as HiddenField).Value;
                    hdSectionid.Value = (row.FindControl("hdSectionid") as HiddenField).Value;
                    hdSubComb.Value = (row.FindControl("hdSubComb") as HiddenField).Value;

                    if (hdCourseid.Value == hdCourseid2.Value && hdSectionid.Value == hdSectionid2.Value && hdSubComb.Value == hdSubCombid2.Value)
                    {
                        r1.Enabled = false;
                    }

                }
            }

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (txtRegId.Text != "")
            {
                EncryptionDecryption enc = new EncryptionDecryption();

                hdRefId.Value = HttpUtility.UrlEncode(enc.EncryptKey(txtRegId.Text, "dheticketabhi@mohi#2020"));
                string RegId = hdRefId.Value;
                hdRefId.Value = RegId.Replace(" ", "+");
                string url = "https://itiharyanaadmissions.nic.in/PG/Account/candidet?Regid=" + Uri.EscapeDataString(hdRefId.Value); //Prod
                                                                                                                           //Response.Redirect(url);
                string fullURL = "window.open('" + url + "', '_blank', 'height=500,width=800,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('Invalid Registration_Id...!');", true);

            }
        }

        //protected void ddlCatgAllotment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCatgAllotment.SelectedValue != "0")
        //    {


        //        if (ddlCatgAllotment.SelectedValue == "SC" || ddlCatgAllotment.SelectedValue == "SC")
        //        {
        //            dvPMSSCScholarShip.Style.Add("display", "Inline-block");

        //        }
        //        else
        //        {
        //            dvPMSSCScholarShip.Style.Add("display", "none");
        //        }
        //        // rdbtnPMS_SC.Focus();
        //        //txtCont.Focus();

        //    }
        //}

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Account/FeeModuleCollegePG");
        }
    }

}
