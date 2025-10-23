using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HigherEducation.BusinessLayer;
using System.IO;
using System.Globalization;
namespace HigherEducation.HigherEducations
{
    public partial class HEMenu : System.Web.UI.Page
    {
        private DataSet ds = new DataSet();
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            clsQUT.qutLogout();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = "0";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            //eSessionMgmt.checkreferer();
            clsLoginUser.CheckSession(UserType);

            if (!IsPostBack)
            {
                lblWelcome.Text = "Welcome User : " + Convert.ToString(Session["UserName"]) + " (" + Convert.ToString(Session["UserId"]) + ")";
            }
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserID"])))
            {
                Response.Redirect("~/DHE/frmlogin.aspx", true);
            }
            else
            {
                if (!IsPostBack)
                {

                    //Management
                    dvCourseMgmt.Visible = false;
                    dvFeeMgmt.Visible = false;
                    dvCollegeMgmt.Visible = false;
                    dvVerificationMgmt.Visible = false;
                    dvReportsMgmt.Visible = false;
                    dvSecondYearMgmt.Visible = false;   // for Second Year Candidate
                    dvStateReports.Visible = false;   // for State Level Report

                    dvSeatMatrixUpdate.Visible = false;  // for Seat Matrix Update
                    dvRankingConditions.Visible = false;
                    dvCanMaster.Visible = false;

                    //State Level
                    dvAdmRptData.Visible = false; // added  for admission Report
                    dvSeatAllotment_Candidate.Visible = false;      //Candidate Seat Allotment
                    dvRanking.Visible = false;      //Ranking Added on 30-06-2023
                    dvCandidateList.Visible = false;     // for candidate list 20-07-2023
                    dvCancelStudentRegistration.Visible = false;    // for cancel registration
                    dvCancelStudentAdmission.Visible = false;    // for cancel Admission
                    dvRestoreStudentAdmission.Visible = false;    // for restore Admission


                    //Course Link
                    dvAddEditCourse.Visible = false;
                    dvAddEditSection.Visible = false;
                    dvCollegeCourse.Visible = false;
                    dvSubjectConfig.Visible = false;
                    dvSubjectComb.Visible = false;
                    dvCollegeCoursePG.Visible = false;
                    dvInactiveTradeStudents.Visible = false;

                    //Fee Link
                    dvFeeHead.Visible = false;
                    dvFeeSubHead.Visible = false;
                    dvFeeDetail.Visible = false;
                    dvFeeDetailPG.Visible = false;
                    dvFeeAdjustment.Visible = false;
                    dvPvtITIFeePaid.Visible = false;
                    dvSecondYearFee.Visible = false; // Added on 16-03-2023

                    //College Link
                    dvCollegeProfile.Visible = false;
                    dvCollegeProspectus.Visible = false;
                    dvSeatMatrix.Visible = false;
                    dvMeritList.Visible = false;
                    dvCancelStudentRecord.Visible = false;
                    dvVerificationRevoked.Visible = false;
                    dvCancelAdmission.Visible = false;
                    dvPhysicalCounselling.Visible = false;
                    dvPhysicalCounsellingPG.Visible = false;
                    dvCourseWiseStudentList.Visible = false;
                    dvAddEditCollege.Visible = false;
                    dvEditCollege.Visible = false;
                    dvViewITI.Visible = false;
                    dvDownloadRR.Visible = false;
                    dvDownloadPhoto.Visible = false;
                    dvCancelAdmissionPG.Visible = false;
                    dvDownloadRRPG.Visible = false;
                    dvDownloadPhotoPG.Visible = false;
                    dvFreezeUnfreeze.Visible = false;
                    dvFreezeCollege.Visible = false;
                    dvUpdateUniv.Visible = false;
                    dvcollageUpdate.Visible = false;
                    //Verification Link
                    dvStuVerification.Visible = false;
                    dvStateVerifier.Visible = false;
                    dvPGStuVerification.Visible = false;
                    dvStudentInfo.Visible = false;
                    dvShiftTrade.Visible = false;
                    dvGrantRevoke.Visible = false;

                    //Reports Link
                    dvRptCourseWiseApp.Visible = false;
                    dvRptObjRaised.Visible = false;
                    dvRptSectionWise.Visible = false;
                    dvRptFeeReceipt.Visible = false;
                    dvRptFeeReceiptQtr.Visible = false; // added on 04-01-2023
                    dvERPData.Visible = false; // added on 23-01-2023
                    dvRptCancellation.Visible = false;
                    dvRptVacantSeats.Visible = false;
                    dvRptFeeCollection.Visible = false;
                    dvRptBankTrackID.Visible = false;
                    dvRptSubHead.Visible = false;// added on 11-02-2021
                    dvRptCollegeCourseFeesReport.Visible = false;
                    dvSeatAllotmentRep.Visible = false;
                    dvVerificationRpt.Visible = false;
                    dvIDCard.Visible = false;
                    //Dashboard Link
                    dvDashboard.Visible = false;
                    dvSummaryRep.Visible = false;
                    dvCandidateDetailsITIWise.Visible = false;
                    dvStudentDetailsRep.Visible = false;
                    dvAPIData.Visible = false;      //DGT API DATA
                    dvAPIDataSID.Visible = false;      //DGT SID API DATA
                    dvRestroeAdmission.Visible = false;     //Restore Candidate Admission
                    dvChangeMobileCandidate.Visible = false;    // Change Mobile Number
                    dvResetITIpass.Visible = false;    // Reset ITI Password
                    dvCandidateDetailsITIWise2nd.Visible = false;   // 2nd Year Students 2021-23
                    dvPaymentStatus.Visible = false;
                    dvTradeWiseAdmission.Visible = false;   // for Trade wise Admission Report

                    //Second Year Link
                    dvAllCandidate2nd.Visible = false;
                    dvCandidateDetailsITIWise2nd.Visible = false;
                    dvApprovedCandidate.Visible = false;
                    dvCandidateLoginDetails.Visible = false;
                    dvQtrFee2021.Visible = false;
                    dvResetPassStudent.Visible = false;    // Reset Password 2021-23

                    // For Quarter fee Report Old Session
                    dvRptFeeReceiptQtr22_24.Visible = false;
                    dvCandidateDetailsITIWise_2022.Visible = false;

                    // For ITI Library Workshop
                    dvLibWork.Visible = false;
                    dvLibrary.Visible = false;
                    dvWorkshop.Visible = false;
                    dvAddWorkshop.Visible = false;

                    clsLoginUser obj = new clsLoginUser();
                    obj.UserID = Convert.ToString(Session["UserID"]);
                    obj.ApplicationCode = "1";
                    ds = obj.GetMenuItem();
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dtMenuBar = new DataTable();
                        dtMenuBar = ds.Tables[0];
                        //Session["MainMenu"] = dtMenuBar;
                        if (dtMenuBar.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtMenuBar.Rows)
                            {
                                string ModuleID = Convert.ToString(row["ModuleID"]);
                                string ModuleName = Convert.ToString(row["ModuleName"]);
                                string ModuleURL = Convert.ToString(row["ModuleURL"]);
                                string DisplayBlockID = Convert.ToString(row["DisplayBlockID"]);
                                if (DisplayBlockID == "dvCourseMgmt")
                                {
                                    dvCourseMgmt.Visible = true;
                                }
                                if (DisplayBlockID == "dvFeeMgmt")
                                {
                                    dvFeeMgmt.Visible = true;
                                }
                                if (DisplayBlockID == "dvCollegeMgmt")
                                {
                                    dvCollegeMgmt.Visible = true;
                                }
                                if (DisplayBlockID == "dvVerificationMgmt")
                                {
                                    dvVerificationMgmt.Visible = true;
                                }
                                if (DisplayBlockID == "dvReportsMgmt")
                                {
                                    dvReportsMgmt.Visible = true;
                                }
                                if (DisplayBlockID == "dvSecondYearMgmt")
                                {
                                    dvSecondYearMgmt.Visible = true;
                                }
                                if (DisplayBlockID == "dvStateReports")
                                {
                                    dvStateReports.Visible = true;
                                }
                                if (DisplayBlockID == "dvLibWork")
                                {
                                    dvLibWork.Visible = true;
                                }
                            }
                        }
                        DataTable dtMenuChild = new DataTable();
                        dtMenuChild = ds.Tables[1];
                        //Session["SubMenu"] = dtMenuChild;
                        if (dtMenuChild.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtMenuChild.Rows)
                            {
                                string ModuleID = Convert.ToString(row["ModuleID"]);
                                string ModuleName = Convert.ToString(row["ModuleName"]);
                                string ModuleURL = Convert.ToString(row["ModuleURL"]);
                                string DisplayBlockID = Convert.ToString(row["DisplayBlockID"]);


                                //Course Management
                                if (DisplayBlockID == "dvAddEditCourse")
                                {
                                    dvAddEditCourse.Visible = true;
                                    aAddEditCourse.InnerText = ModuleName;
                                    aAddEditCourse.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvAddEditSection")
                                {
                                    dvAddEditSection.Visible = true;
                                    aAddEditSection.InnerText = ModuleName;
                                    aAddEditSection.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvCollegeCourse")
                                {
                                    dvCollegeCourse.Visible = true;
                                    aCollegeCourse.InnerText = ModuleName;
                                    aCollegeCourse.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvSubjectConfig")
                                {
                                    dvSubjectConfig.Visible = true;
                                    aSubjectConfig.InnerText = ModuleName;
                                    aSubjectConfig.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvSubjectComb")
                                {
                                    dvSubjectComb.Visible = true;
                                    aSubjectComb.InnerText = ModuleName;
                                    aSubjectComb.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvCollegeCoursePG")
                                {
                                    dvCollegeCoursePG.Visible = true;
                                    aCollegeCoursePG.InnerText = ModuleName;
                                    aCollegeCoursePG.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvStudentDetailsRep")
                                {
                                    dvStudentDetailsRep.Visible = true;
                                    advStudentDetails.InnerText = ModuleName;
                                    advStudentDetails.HRef = ModuleURL;
                                }
                                //Fee Mangement
                                if (DisplayBlockID == "dvFeeHead")
                                {
                                    dvFeeHead.Visible = true;
                                    aFeeHead.InnerText = ModuleName;
                                    aFeeHead.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvFeeSubHead")
                                {
                                    dvFeeSubHead.Visible = true;
                                    aFeeSubHead.InnerText = ModuleName;
                                    aFeeSubHead.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvFeeDetail")
                                {
                                    dvFeeDetail.Visible = true;
                                    aFeeDetail.InnerText = ModuleName;
                                    aFeeDetail.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvFeeDetailPG")
                                {
                                    dvFeeDetailPG.Visible = true;
                                    aFeeDetailPG.InnerText = ModuleName;
                                    aFeeDetailPG.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvFeeAdjustment")
                                {
                                    dvFeeAdjustment.Visible = true;
                                    aFeeAdjustment.InnerText = ModuleName;
                                    aFeeAdjustment.HRef = ModuleURL;
                                }
                                // Added on 16-03-2023
                                if (DisplayBlockID == "dvSecondYearFee")
                                {
                                    dvSecondYearFee.Visible = true;
                                    aSecondYearFee.InnerText = ModuleName;
                                    aSecondYearFee.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvPvtITIFeePaid")
                                {
                                    dvPvtITIFeePaid.Visible = true;
                                    aPvtITIFeePaid.InnerText = ModuleName;
                                    aPvtITIFeePaid.HRef = ModuleURL;
                                }
                                //College Profile
                                if (DisplayBlockID == "dvAddEditCollege")
                                {
                                    dvAddEditCollege.Visible = true;
                                    aAddEditCollege.InnerText = ModuleName;
                                    aAddEditCollege.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvViewITI")
                                {
                                    dvViewITI.Visible = true;
                                    aViewITI.InnerText = ModuleName;
                                    aViewITI.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvEditCollege")
                                {
                                    dvEditCollege.Visible = true;
                                    aEditCollege.InnerText = ModuleName;
                                    aEditCollege.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvCollegeProfile")
                                {
                                    dvCollegeProfile.Visible = true;
                                    aCollegeGlance.InnerText = ModuleName;
                                    aCollegeGlance.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvCollegeProspectus")
                                {
                                    dvCollegeProspectus.Visible = true;
                                    aCollegeProspectus.InnerText = ModuleName;
                                    aCollegeProspectus.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvSeatMatrix")
                                {
                                    dvSeatMatrix.Visible = true;
                                    aSeatMatrix.InnerText = ModuleName;
                                    aSeatMatrix.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvMeritList")
                                {
                                    dvMeritList.Visible = true;
                                    aMeritList.InnerText = ModuleName;
                                    aMeritList.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvPhysicalCounselling")
                                {
                                    dvPhysicalCounselling.Visible = true;
                                    aPhysicalCounselling.InnerText = ModuleName;
                                    aPhysicalCounselling.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvPhysicalCounsellingPG")
                                {
                                    dvPhysicalCounsellingPG.Visible = true;
                                    aPhysicalCounsellingPG.InnerText = ModuleName;
                                    aPhysicalCounsellingPG.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvCourseWiseStudentList")
                                {
                                    dvCourseWiseStudentList.Visible = true;
                                    aCourseWiseStudentList.InnerText = ModuleName;
                                    aCourseWiseStudentList.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvCancelStudentRecord")
                                {
                                    dvCancelStudentRecord.Visible = true;
                                    aCancelStudentRecord.InnerText = ModuleName;
                                    aCancelStudentRecord.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvVerificationRevoked")
                                {
                                    dvVerificationRevoked.Visible = true;
                                    aVerificationRevoked.InnerText = ModuleName;
                                    aVerificationRevoked.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvCancelAdmission")
                                {
                                    dvCancelAdmission.Visible = true;
                                    aCancelAdmission.InnerText = ModuleName;
                                    aCancelAdmission.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvDownloadRR")
                                {
                                    dvDownloadRR.Visible = true;
                                    aDownloadRR.InnerText = ModuleName;
                                    aDownloadRR.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvCancelAdmissionPG")
                                {
                                    dvCancelAdmissionPG.Visible = true;
                                    aCancelAdmissionPG.InnerText = ModuleName;
                                    aCancelAdmissionPG.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvDownloadRRPG")
                                {
                                    dvDownloadRRPG.Visible = true;
                                    aDownloadRRPG.InnerText = ModuleName;
                                    aDownloadRRPG.HRef = ModuleURL;
                                }


                                if (DisplayBlockID == "dvDownloadPhoto")
                                {
                                    dvDownloadPhoto.Visible = true;
                                    //aDownloadPhoto.InnerText = ModuleName;
                                    // aDownloadPhoto.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvDownloadPhotoPG")
                                {
                                    dvDownloadPhotoPG.Visible = true;
                                    //aDownloadPhoto.InnerText = ModuleName;
                                    // aDownloadPhoto.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvFreezeUnfreeze")
                                {
                                    dvFreezeUnfreeze.Visible = true;
                                    aFreezeUnfreeze.InnerText = ModuleName;
                                    aFreezeUnfreeze.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvFreezeCollege")
                                {
                                    dvFreezeCollege.Visible = true;
                                    aFreezeCollege.InnerText = ModuleName;
                                    aFreezeCollege.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvUpdateUniv")
                                {
                                    dvUpdateUniv.Visible = true;
                                    aUpdateUniv.InnerText = ModuleName;
                                    aUpdateUniv.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvcollageUpdate")
                                {
                                    dvcollageUpdate.Visible = true;
                                    acollageUpdate.InnerText = ModuleName;
                                    acollageUpdate.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvStudentInfo")
                                {
                                    dvStudentInfo.Visible = true;
                                    aStudentInfo.InnerText = ModuleName;
                                    aStudentInfo.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvShiftTrade")
                                {
                                    dvShiftTrade.Visible = true;
                                    aShiftTrade.InnerText = ModuleName;
                                    aShiftTrade.HRef = ModuleURL;
                                }
                                //Verification
                                if (DisplayBlockID == "dvStuVerification")
                                {
                                    dvStuVerification.Visible = true;
                                    aStuVerification.InnerText = ModuleName;
                                    aStuVerification.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvPGStuVerification")
                                {
                                    dvPGStuVerification.Visible = true;
                                    aPGStuVerification.InnerText = ModuleName;
                                    aPGStuVerification.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvStateVerifier")
                                {
                                    dvStateVerifier.Visible = true;
                                    aStateVerifier.InnerText = ModuleName;
                                    aStateVerifier.HRef = ModuleURL;
                                }

                                //Reports
                                if (DisplayBlockID == "dvRptCourseWiseApp")
                                {
                                    dvRptCourseWiseApp.Visible = true;
                                    aRptCourseWiseApp.InnerText = ModuleName;
                                    aRptCourseWiseApp.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvRptObjRaised")
                                {
                                    dvRptObjRaised.Visible = true;
                                    aRptObjRaised.InnerText = ModuleName;
                                    aRptObjRaised.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvRptSectionWise")
                                {
                                    dvRptSectionWise.Visible = true;
                                    aRptSectionWise.InnerText = ModuleName;
                                    aRptSectionWise.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvRptFeeReceipt")
                                {
                                    dvRptFeeReceipt.Visible = true;
                                    aRptFeeReceipt.InnerText = ModuleName;
                                    aRptFeeReceipt.HRef = ModuleURL;
                                }

                                // For Quarter fee Report
                                if (DisplayBlockID == "dvRptFeeReceiptQtr")
                                {
                                    dvRptFeeReceiptQtr.Visible = true;
                                    aRptFeeReceiptQtr.InnerText = ModuleName;
                                    aRptFeeReceiptQtr.HRef = ModuleURL;
                                }
                                // For ERP Data
                                if (DisplayBlockID == "dvERPData")
                                {
                                    dvERPData.Visible = true;
                                    aERPData.InnerText = ModuleName;
                                    aERPData.HRef = ModuleURL;
                                }

                                // For Admission Report Data
                                if (DisplayBlockID == "dvAdmRptData")
                                {
                                    dvAdmRptData.Visible = true;
                                    aAdmRptData.InnerText = ModuleName;
                                    aAdmRptData.HRef = ModuleURL;
                                }

                                // For Candidate Seat Allotment
                                if (DisplayBlockID == "dvSeatAllotment_Candidate")
                                {
                                    dvSeatAllotment_Candidate.Visible = true;
                                    aSeatAllotment_Candidate.InnerText = ModuleName;
                                    aSeatAllotment_Candidate.HRef = ModuleURL;
                                }

                                // For Ranking
                                if (DisplayBlockID == "dvRanking")
                                {
                                    dvRanking.Visible = true;
                                    aRanking.InnerText = ModuleName;
                                    aRanking.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvRptCancellation")
                                {
                                    dvRptCancellation.Visible = true;
                                    aRptCancellation.InnerText = ModuleName;
                                    aRptCancellation.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvRptVacantSeats")
                                {
                                    dvRptVacantSeats.Visible = true;
                                    aRptVacantSeats.InnerText = ModuleName;
                                    aRptVacantSeats.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvRptFeeCollection")
                                {
                                    dvRptFeeCollection.Visible = true;
                                    aRptFeeCollection.InnerText = ModuleName;
                                    aRptFeeCollection.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvRptSubHead") // added on 11-02-2021
                                {
                                    dvRptSubHead.Visible = true;
                                    aRptSubHead.InnerText = ModuleName;
                                    aRptSubHead.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvRptCollegeCourseFeesReport")
                                {
                                    dvRptCollegeCourseFeesReport.Visible = true;
                                    aRptCollegeCourseFeesReport.InnerText = ModuleName;
                                    aRptCollegeCourseFeesReport.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvRptBankTrackID")
                                {
                                    dvRptBankTrackID.Visible = true;
                                    aRptBankTrackID.InnerText = ModuleName;
                                    aRptBankTrackID.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvDashboard")
                                {
                                    dvDashboard.Visible = true;
                                    aDashboard.InnerText = ModuleName;
                                    aDashboard.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvSeatAllotmentRep")
                                {
                                    dvSeatAllotmentRep.Visible = true;
                                    aRptSeatAllotment.InnerText = ModuleName;
                                    aRptSeatAllotment.HRef = ModuleURL;

                                }

                                if (DisplayBlockID == "dvVerificationRpt")
                                {
                                    dvVerificationRpt.Visible = true;
                                    aVerificationRpt.InnerText = ModuleName;
                                    aVerificationRpt.HRef = ModuleURL;

                                }
                                if (DisplayBlockID == "dvSummaryRep")
                                {
                                    dvSummaryRep.Visible = true;
                                    aRptSummary.InnerText = ModuleName;
                                    aRptSummary.HRef = ModuleURL;

                                }
                                if (DisplayBlockID == "dvCandidateDetailsITIWise")
                                {
                                    dvCandidateDetailsITIWise.Visible = true;
                                    aRptCandidateDetailsITIWise.InnerText = ModuleName;
                                    aRptCandidateDetailsITIWise.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvIDCard")
                                {
                                    dvIDCard.Visible = true;
                                    advIDCard.InnerText = ModuleName;
                                    advIDCard.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvShiftUnitUpdation")
                                {
                                    dvShiftUnitUpdation.Visible = true;
                                    aShiftUnitUpdation.InnerText = ModuleName;
                                    aShiftUnitUpdation.HRef = ModuleURL;
                                }
                                // For API Data
                                if (DisplayBlockID == "dvAPIData")
                                {
                                    dvAPIData.Visible = true;
                                    aAPIData.InnerText = ModuleName;
                                    aAPIData.HRef = ModuleURL;
                                }

                                // For SID API Data
                                if (DisplayBlockID == "dvAPIDataSID")
                                {
                                    dvAPIDataSID.Visible = true;
                                    aAPIDataSID.InnerText = ModuleName;
                                    aAPIDataSID.HRef = ModuleURL;
                                }

                                // Admission Restore
                                if (DisplayBlockID == "dvRestroeAdmission")
                                {
                                    dvRestroeAdmission.Visible = true;
                                    aRestroeAdmission.InnerText = ModuleName;
                                    aRestroeAdmission.HRef = ModuleURL;
                                }

                                // Change Mobile Number Candidate
                                if (DisplayBlockID == "dvChangeMobileCandidate")
                                {
                                    dvChangeMobileCandidate.Visible = true;
                                    aChangeMobileCandidate.InnerText = ModuleName;
                                    aChangeMobileCandidate.HRef = ModuleURL;
                                }

                                // Reset ITI Password
                                if (DisplayBlockID == "dvResetITIpass")
                                {
                                    dvResetITIpass.Visible = true;
                                    aResetITIpass.InnerText = ModuleName;
                                    aResetITIpass.HRef = ModuleURL;
                                }

                                //Second Year
                                // Display 2nd Year Students of 2021-23
                                if (DisplayBlockID == "dvAllCandidate2nd")
                                {
                                    dvAllCandidate2nd.Visible = true;
                                    aAllCandidate2nd.InnerText = ModuleName;
                                    aAllCandidate2nd.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvCandidateDetailsITIWise2nd")
                                {
                                    dvCandidateDetailsITIWise2nd.Visible = true;
                                    aRptCandidateDetailsITIWise2nd.InnerText = ModuleName;
                                    aRptCandidateDetailsITIWise2nd.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvApprovedCandidate")
                                {
                                    dvApprovedCandidate.Visible = true;
                                    aApprovedCandidate.InnerText = ModuleName;
                                    aApprovedCandidate.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvCandidateLoginDetails")
                                {
                                    dvCandidateLoginDetails.Visible = true;
                                    aCandidateLoginDetails.InnerText = ModuleName;
                                    aCandidateLoginDetails.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvQtrFee2021")
                                {
                                    dvQtrFee2021.Visible = true;
                                    aQtrFee2021.InnerText = ModuleName;
                                    aQtrFee2021.HRef = ModuleURL;
                                }

                                // Reset password Candidate 2021-23
                                if (DisplayBlockID == "dvResetPassStudent")
                                {
                                    dvResetPassStudent.Visible = true;
                                    aResetPassStudent.InnerText = ModuleName;
                                    aResetPassStudent.HRef = ModuleURL;
                                }

                                // Old Session Qtr Report
                                if (DisplayBlockID == "dvRptFeeReceiptQtr22_24")
                                {
                                    dvRptFeeReceiptQtr22_24.Visible = true;
                                    aRptFeeReceiptQtr22_24.InnerText = ModuleName;
                                    aRptFeeReceiptQtr22_24.HRef = ModuleURL;
                                }
                                //  Candidate details 2022
                                if (DisplayBlockID == "dvCandidateDetailsITIWise_2022")
                                {
                                    dvCandidateDetailsITIWise_2022.Visible = true;
                                    aCandidateDetailsITIWise_2022.InnerText = ModuleName;
                                    aCandidateDetailsITIWise_2022.HRef = ModuleURL;
                                }

                                //  for Update Seat Matrix
                                if (DisplayBlockID == "dvSeatMatrixUpdate")
                                {
                                    dvSeatMatrixUpdate.Visible = true;
                                    aSeatMatrixUpdate.InnerText = ModuleName;
                                    aSeatMatrixUpdate.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvPaymentStatus")
                                {
                                    dvPaymentStatus.Visible = true;
                                    aPaymentStatus.InnerText = ModuleName;
                                    aPaymentStatus.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvCandidateList")
                                {
                                    dvCandidateList.Visible = true;
                                    aCandidateList.InnerText = ModuleName;
                                    aCandidateList.HRef = ModuleURL;
                                }

                                //  for RankingConditions
                                if (DisplayBlockID == "dvRankingConditions")
                                {
                                    dvRankingConditions.Visible = true;
                                    aRankingConditions.InnerText = ModuleName;
                                    aRankingConditions.HRef = ModuleURL;
                                }
                                if (DisplayBlockID == "dvCanMaster")
                                {
                                    dvCanMaster.Visible = true;
                                    aCanMaster.InnerText = ModuleName;
                                    aCanMaster.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvCancelStudentRegistration")
                                {
                                    dvCancelStudentRegistration.Visible = true;
                                    aCancelStudentRegistration.InnerText = ModuleName;
                                    aCancelStudentRegistration.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvCancelStudentAdmission")
                                {
                                    dvCancelStudentAdmission.Visible = true;
                                    aCancelStudentAdmission.InnerText = ModuleName;
                                    aCancelStudentAdmission.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvRestoreStudentAdmission")
                                {
                                    dvRestoreStudentAdmission.Visible = true;
                                    aRestoreStudentAdmission.InnerText = ModuleName;
                                    aRestoreStudentAdmission.HRef = ModuleURL;
                                }


                                //  State Level Reports

                                if (DisplayBlockID == "dvCancelStudentRegistration")
                                {
                                    dvCancelStudentRegistration.Visible = true;
                                    aCancelStudentRegistration.InnerText = ModuleName;
                                    aCancelStudentRegistration.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvCancelStudentAdmission")
                                {
                                    dvCancelStudentAdmission.Visible = true;
                                    aCancelStudentAdmission.InnerText = ModuleName;
                                    aCancelStudentAdmission.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvRestoreStudentAdmission")
                                {
                                    dvRestoreStudentAdmission.Visible = true;
                                    aRestoreStudentAdmission.InnerText = ModuleName;
                                    aRestoreStudentAdmission.HRef = ModuleURL;
                                }


                                if (DisplayBlockID == "dvTradeWiseAdmission")
                                {
                                    dvTradeWiseAdmission.Visible = true;
                                    aTradeWiseAdmission.InnerText = ModuleName;
                                    aTradeWiseAdmission.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvGrantRevoke")
                                {
                                    dvGrantRevoke.Visible = true;
                                    aGrantRevoke.InnerText = ModuleName;
                                    aGrantRevoke.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvInactiveTradeStudents")
                                {
                                    dvInactiveTradeStudents.Visible = true;
                                    aInactiveTradeStudents.InnerText = ModuleName;
                                    aInactiveTradeStudents.HRef = ModuleURL;
                                }

                                /* for ITI Library and Workshop */
                                if (DisplayBlockID == "dvLibrary")
                                {
                                    dvLibrary.Visible = true;
                                    aLibrary.InnerText = ModuleName;
                                    aLibrary.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvAddWorkshop")
                                {
                                    dvAddWorkshop.Visible = true;
                                    aAddWorkshop.InnerText = ModuleName;
                                    aAddWorkshop.HRef = ModuleURL;
                                }

                                if (DisplayBlockID == "dvWorkshop")
                                {
                                    dvWorkshop.Visible = true;
                                    aWorkshop.InnerText = ModuleName;
                                    aWorkshop.HRef = ModuleURL;
                                }
                            }

                        }
                    }
                    // eSessionMgmt.SetCookies();
                    //Security Check
                    eSessionMgmt.AntiFixationInit();
                    eSessionMgmt.AntiHijackInit();
                    //Security Check
                }
            }
        }

        protected void lnkDownloadPhoto_Click(object sender, EventArgs e)
        {


            try
            {
                string downloadPath = "";
                string strFileName = "";
                strFileName = Convert.ToString(Session["CollegeId"]) + ".zip";
                downloadPath = "C:\\Images\\";
                FileInfo fi = new FileInfo(downloadPath);
                string fpath1 = (fi + strFileName);//Server.MapPath("~/" + strFileName);


                Response.Clear();
                Response.ContentType = "aapplication/octet.stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"CollegeRR_" + strFileName + "\"");
                Response.TransmitFile(fpath1);
                Response.Flush();
                Response.End();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void lnkDownloadPhotoPG_Click(object sender, EventArgs e)
        {


            try
            {
                string downloadPath = "";
                string strFileName = "";
                strFileName = Convert.ToString(Session["CollegeId"]) + ".zip";
                downloadPath = "C:\\Images1\\";
                FileInfo fi = new FileInfo(downloadPath);
                string fpath1 = (fi + strFileName);//Server.MapPath("~/" + strFileName);


                Response.Clear();
                Response.ContentType = "aapplication/octet.stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=\"CollegeRR_" + strFileName + "\"");
                Response.TransmitFile(fpath1);
                Response.Flush();
                Response.End();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('" + ex.Message + "');", true);
            }
        }

    }
}