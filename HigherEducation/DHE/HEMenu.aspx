<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HEMenu.aspx.cs" Inherits="HigherEducation.HigherEducations.HEMenu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HigherEducation Menu</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="User Management" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <%--    <link href="../assets/css/all.css" rel="stylesheet" />--%>
    <link href="../assets/css/stylehemenu.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>

    <%-- <link rel="stylesheet" type="text/css" href="assets/css/style.css" />--%>
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />


    <%-- <link href="style/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <link href="assets/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />--%>
    <script src="../scripts/sweetalert.min.js"></script>
    <script src="../assets/js/popper.js/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>

    <%--   <script src="assets/js/bootstrap-datetimepicker.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>--%>
    <script type="text/javascript">

        $.extend($.expr[':'], { 'containsi': function (elem, i, match, array) { return (elem.textContent || elem.innerText || '').toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0; } });


        function MenuSearch(ele) {
            $("div > ul.content-list > li").hide();
            var a = $('#txtSearch').val();
            $("div > ul.content-list > li :containsi(" + a + ")").parent().show();

            //$("div > ul.content-list > li") :contains(" + a + ")").parent().show();
        }

    </script>
    <style>
        #txtIFSC {
            text-security: disc;
        }

        #txtIFSC {
            -webkit-text-security: disc;
        }

        #txtIFSC {
            -mox-text-security: disc;
        }

        #txtBankAccount {
            text-security: disc;
        }

        #txtBankAccount {
            -webkit-text-security: disc;
        }

        #txtBankAccount {
            -moz-text-security: disc;
        }
    </style>
</head>
<body style="margin-top: 0px; left: 0px; margin-left: 0px; top: 0px">
    <form id="form1" runat="server" defaultbutton="btnGo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" class="img-fluid" style="width: 100%" />
            </div>
            <%--<div class="row d-sm-none d-md-none d-lg-none d-xl-none">
            <img src="assets/images/VCMeet-mobile.png" style="width: 100%" />
        </div>--%>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 cus-topbar">
            <div class="row">
                <div class="col-lg-8 col-md-6 col-sm-6 col-12 cus-topbar-label">
                    <asp:Label ID="lblWelcome" Font-Bold="true" runat="server"></asp:Label>
                </div>
                <div class="col-lg-4 col-md-6 col-sm-6 col-12 cus-topbar-menu" style="display: flex; justify-content: right;">
                    <ul style="padding: 0; margin: 0.4rem;">
                        <li>
                            <div class="input-group has-search">
                                <span class="fa fa-search form-control-feedback"></span>
                                <input id="txtSearch" class="form-control" placeholder="search.." />
                                <asp:Button ID="btnGo" CssClass="btn btn-primary btn-sm" runat="server" Text="Search" OnClientClick="MenuSearch(this); return false;"></asp:Button>
                            </div>
                        </li>
                        <li>

                            <asp:Button ID="btnLogout" OnClick="btnLogout_Click" class="btn btn-sm" runat="server"
                                Text="Logout"></asp:Button>
                        </li>
                    </ul>
                </div>
            </div>
        </div>


        <div class="container-fluid" style="margin-bottom: 4%;">

            <div class="row">
                <div class="col-lg-12">
                    <div class="row" style="display: flex; justify-content: right; margin-bottom: 20px;">

                        <div class="col-md-3 cus-search">
                        </div>
                    </div>
                    <div class="row">

                        <div id="dvStateReports" runat="server" class="col-lg-3 col-md-3 col-sm-6 m-b-30">
                            <div class="pricingTable">
                                <div class="pricingTable-header">
                                    <div class="price-value">
                                        <span>
                                            <img src="../assets/images/report.png" /></span>
                                    </div>
                                    <h3 class="title">State Level Reports</h3>

                                </div>

                                <!--
                                    For Admission Report Data
                                -->
                                <div id="dvAdmRptData" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aAdmRptData" runat="server"></a></li>
                                    </ul>
                                </div>

                                <!--
                                    For candidate seat allotment
                                -->
                                <div id="dvSeatAllotment_Candidate" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aSeatAllotment_Candidate" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                -->
                                <!--
                                    For Ranking
                                -->
                                <div id="dvRanking" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRanking" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                -->
                                <!--
                                    For Penalty and Challan Status
                                -->
                                <div id="dvCandidateList" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aCandidateList" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                -->

                                <div id="dvCancelStudentRegistration" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCancelStudentRegistration" runat="server"></a></li>
                                    </ul>
                                </div>

                                 <div id="dvCancelStudentAdmission" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCancelStudentAdmission" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvRestoreStudentAdmission" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aRestoreStudentAdmission" runat="server"></a></li>
                                    </ul>
                                </div>

                            </div>
                        </div>

                        <div id="dvCollegeMgmt" runat="server" class="col-lg-3 col-md-3 col-sm-6 m-b-30 cus-card-menu">
                            <div class="pricingTable">
                                <div class="pricingTable-header">
                                    <div class="price-value">
                                        <span>
                                            <img src="../assets/images/college.png" /></span>
                                    </div>
                                    <h3 class="title">Institute Management</h3>

                                </div>
                                <div id="dvAddEditCollege" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aAddEditCollege" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvEditCollege" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aEditCollege" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvViewITI" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aViewITI" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvCollegeProfile" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCollegeGlance" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvCollegeProspectus" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCollegeProspectus" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvSeatMatrix" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aSeatMatrix" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvMeritList" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aMeritList" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvPhysicalCounselling" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aPhysicalCounselling" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvPhysicalCounsellingPG" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aPhysicalCounsellingPG" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvCourseWiseStudentList" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCourseWiseStudentList" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvCancelStudentRecord" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCancelStudentRecord" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvVerificationRevoked" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aVerificationRevoked" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvCancelAdmission" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCancelAdmission" runat="server"></a></li>
                                    </ul>
                                </div>

                               
                                <div id="dvCancelAdmissionPG" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCancelAdmissionPG" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvDownloadRR" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aDownloadRR" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvDownloadRRPG" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aDownloadRRPG" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvDownloadPhoto" runat="server">
                                    <ul class="content-list">

                                        <li><%--<a href="#" id="aDownloadPhoto"  runat="server"></a>--%>
                                            <asp:LinkButton ID="lnkDownloadPhoto" Text="Download Photos & Signatures - UG" runat="server" OnClick="lnkDownloadPhoto_Click"></asp:LinkButton>

                                        </li>
                                    </ul>
                                </div>
                                <div id="dvDownloadPhotoPG" runat="server">
                                    <ul class="content-list">

                                        <li><%--<a href="#" id="aDownloadPhoto"  runat="server"></a>--%>
                                            <asp:LinkButton ID="lnkDownloadPhotoPG" Text="Download Photos & Signatures - PG" runat="server" OnClick="lnkDownloadPhotoPG_Click"></asp:LinkButton>

                                        </li>
                                    </ul>
                                </div>

                                <div id="dvFreezeUnfreeze" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aFreezeUnfreeze" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvFreezeCollege" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aFreezeCollege" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvUpdateUniv" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aUpdateUniv" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvcollageUpdate" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="acollageUpdate" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvStudentInfo" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aStudentInfo" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvShiftTrade" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aShiftTrade" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvIDCard" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="advIDCard" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvShiftUnitUpdation" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aShiftUnitUpdation" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvRestroeAdmission" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aRestroeAdmission" runat="server"></a></li>
                                    </ul>
                                </div>

                                
                                <div id="dvChangeMobileCandidate" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aChangeMobileCandidate" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvResetITIpass" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aResetITIpass" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!-- Add for seat Matrix updation -->
                                <div id="dvSeatMatrixUpdate" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aSeatMatrixUpdate" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvPaymentStatus" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aPaymentStatus" runat="server"></a></li>
                                    </ul>
                                </div>

                                <!-- For ranking Generation -->
                                <div id="dvRankingConditions" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aRankingConditions" runat="server"></a></li>
                                    </ul>
                                </div>

                                <!-- For Candidate Mater Information -->
                                <div id="dvCanMaster" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aCanMaster" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvGrantRevoke" runat="server">
                                  <ul class="content-list">
                                      <li><a href="#" id="aGrantRevoke" runat="server"></a></li>
                                  </ul>
                              </div>

                            </div>

                        </div>

                        <div id="dvCourseMgmt" class="col-lg-3 col-md-3 col-sm-6 m-b-30" runat="server">
                            <div class="pricingTable">
                                <div class="pricingTable-header">
                                    <div class="price-value">
                                        <span>
                                            <img src="../assets/images/course.png" /></span>
                                    </div>
                                    <h3 class="title">Course Management</h3>

                                </div>
                                <div id="dvAddEditCourse" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aAddEditCourse" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvAddEditSection" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aAddEditSection" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvCollegeCourse" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCollegeCourse" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvSubjectConfig" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aSubjectConfig" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvSubjectComb" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aSubjectComb" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvCollegeCoursePG" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCollegeCoursePG" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvStudentDetailsRep" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="advStudentDetails" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvInactiveTradeStudents" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aInactiveTradeStudents" runat="server"></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div id="dvFeeMgmt" runat="server" class="col-lg-3 col-md-3 col-sm-6 m-b-30">
                            <div class="pricingTable">
                                <div class="pricingTable-header">
                                    <div class="price-value">
                                        <span>
                                            <img src="../assets/images/fee.png" /></span>
                                    </div>
                                    <h3 class="title">Fee Management</h3>

                                </div>
                                <div id="dvFeeHead" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aFeeHead" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvFeeSubHead" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aFeeSubHead" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvFeeDetail" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aFeeDetail" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvFeeAdjustment" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aFeeAdjustment" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvFeeDetailPG" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aFeeDetailPG" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvPvtITIFeePaid" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aPvtITIFeePaid" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                    For Fee of Second Year of Session 2021-23 
                                    added on 16-03-2023
                                -->
                                <div id="dvSecondYearFee" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aSecondYearFee" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                -->
                            </div>
                        </div>

                        <div id="dvVerificationMgmt" runat="server" class="col-lg-3 col-md-3 col-sm-6 m-b-30">
                            <div class="pricingTable">
                                <div class="pricingTable-header">
                                    <div class="price-value">
                                        <span>
                                            <img src="../assets/images/verified-student.png" /></span>
                                    </div>
                                    <h3 class="title">Verification Management</h3>

                                </div>
                                <div id="dvStuVerification" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aStuVerification" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvPGStuVerification" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aPGStuVerification" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvStateVerifier" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aStateVerifier" runat="server"></a></li>
                                    </ul>
                                </div>

                            </div>
                        </div>

                        <div id="dvReportsMgmt" runat="server" class="col-lg-3 col-md-3 col-sm-6 m-b-30">
                            <div class="pricingTable">
                                <div class="pricingTable-header">
                                    <div class="price-value">
                                        <span>
                                            <img src="../assets/images/report.png" /></span>
                                    </div>
                                    <h3 class="title">Reports/Dashboard</h3>

                                </div>
                                <div id="dvRptCourseWiseApp" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptCourseWiseApp" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvRptObjRaised" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptObjRaised" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvRptSectionWise" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptSectionWise" runat="server"></a></li>
                                    </ul>
                                </div>


                                <div id="dvRptFeeReceipt" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptFeeReceipt" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                    For Quarterly Fee Report
                                -->
                                <div id="dvRptFeeReceiptQtr" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptFeeReceiptQtr" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                -->
                                <div id="dvRptCancellation" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptCancellation" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvRptVacantSeats" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptVacantSeats" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvRptFeeCollection" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptFeeCollection" runat="server"></a></li>
                                    </ul>

                                </div>
                                <%--added on 11-02-2021--%>
                                <div id="dvRptSubHead" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptSubHead" runat="server"></a></li>
                                    </ul>

                                </div>
                                <div id="dvRptBankTrackID" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptBankTrackID" runat="server"></a></li>
                                    </ul>

                                </div>
                                <div id="dvDashboard" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aDashboard" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvRptCollegeCourseFeesReport" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptCollegeCourseFeesReport" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvSeatAllotmentRep" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptSeatAllotment" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvVerificationRpt" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aVerificationRpt" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvTradeWiseAdmission" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aTradeWiseAdmission" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvSummaryRep" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptSummary" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvCandidateDetailsITIWise" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptCandidateDetailsITIWise" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvAPIData" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aAPIData" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvAPIDataSID" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aAPIDataSID" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                    For ERP Data
                                -->
                                <div id="dvERPData" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aERPData" runat="server"></a></li>
                                    </ul>
                                </div>

                                

                            </div>
                        </div>


                        <div id="dvSecondYearMgmt" runat="server" class="col-lg-3 col-md-3 col-sm-6 m-b-30">
                            <div class="pricingTable">
                                <div class="pricingTable-header">
                                    <div class="price-value">
                                        <span>
                                            <img src="../assets/images/report.png" /></span>
                                    </div>
                                    <h3 class="title">Old Session Student Management</h3>

                                </div>
                                <!--
                                    Old Session Reports    
                                -->
                                <div id="dvRptFeeReceiptQtr22_24" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptFeeReceiptQtr22_24" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvCandidateDetailsITIWise_2022" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aCandidateDetailsITIWise_2022" runat="server"></a></li>
                                    </ul>
                                </div>

                                <!--
                                    For Showing 2nd Year Students of 2021-2023
                                -->
                                <div id="dvAllCandidate2nd" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aAllCandidate2nd" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvCandidateDetailsITIWise2nd" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aRptCandidateDetailsITIWise2nd" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvApprovedCandidate" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aApprovedCandidate" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvCandidateLoginDetails" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aCandidateLoginDetails" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvQtrFee2021" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aQtrFee2021" runat="server"></a></li>
                                    </ul>
                                </div>
                                <!--
                                    
                                -->

                                <div id="dvResetPassStudent" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aResetPassStudent" runat="server"></a></li>
                                    </ul>
                                </div>

                            </div>
                        </div>


                        
                        <div id="dvLibWork" runat="server" class="col-lg-3 col-md-3 col-sm-6 m-b-30">
                            <div class="pricingTable">
                                <div class="pricingTable-header">
                                    <div class="price-value">
                                        <span>
                                            <img src="../assets/images/report.png" /></span>
                                    </div>
                                    <h3 class="title">ITI Libray/Workshop Reports</h3>

                                </div>

                                <div id="dvLibrary" runat="server">
                                    <ul class="content-list">
                                        <li><a href="#" id="aLibrary" runat="server"></a></li>
                                    </ul>
                                </div>

                                <div id="dvAddWorkshop" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aAddWorkshop" runat="server"></a></li>
                                    </ul>
                                </div>
                                <div id="dvWorkshop" runat="server">
                                    <ul class="content-list">

                                        <li><a href="#" id="aWorkshop" runat="server"></a></li>
                                    </ul>
                                </div>

                                

                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>

    </form>
    <!-- ======= Footer ======= -->
    <footer id="footer">

        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-12 text-center">
                    <div class="row">
                        <div class="col-lg-10 col-md-10 text-left" style="margin-top: 10px;">
                            <div class="credits">
                                <a>Site is technically designed, hosted and maintained by National Informatics Centre, Haryana</a>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2">
                            <img src="/assets/images/nic-logo.png" style="width: 100px;">
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
    <!-- End Footer -->


</body>
</html>
