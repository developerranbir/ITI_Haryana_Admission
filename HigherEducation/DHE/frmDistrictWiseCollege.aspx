<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmDistrictWiseCollege.aspx.cs" Inherits="HigherEducation.HigherEducations.frmDistrictWiseCollege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylesearch.css" rel="stylesheet" />
    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet"/>
    <link href="../assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet"/>
    <link href="../assets/vendor/animatecss/animate.min.css" rel="stylesheet"/>
    <link href="../assets/vendor/remixicon/remixicon.css" rel="stylesheet"/>
    <link href="../assets/vendor/venobox/venobox.css" rel="stylesheet"/>
    <link href="../assets/vendor/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet"/>
    <link href="../assets/vendor/aos/aos.css" rel="stylesheet"/>
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <%--<script src="../assets/js/jquery-3.4.1.js"></script>--%>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/main.js"></script>
    <%--<script src="../assets/js/moment-with-locales.min.js"></script>--%>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>
    <script src="../assets/vendor/counterup/counterup.min.js"></script>
    <script src="../assets/vendor/owlcarousel/owl.carousel.min.js"></script>
    <script src="../assets/vendor/isotope-layout/isotope.pkgd.min.js"></script>
    <script src="../assets/vendor/aos/aos.js"></script>

    <script>
        function downloadPDF(pdf) {
            const linkSource = `data:application/pdf;base64,${pdf}`;
            const downloadLink = document.createElement("a");
            const fileName = "Prospectus.pdf";
            downloadLink.href = linkSource;
            downloadLink.download = fileName;
            downloadLink.click();
        }
    </script>

    <style type="text/css">
        .modalBackground {
            background-color: rgb(192,234,255);
            filter: alpha(opacity=70);
            opacity: 0.7;
            z-index: 100 !important;
        }

        .modalPopup {
            background-color: #ffffff;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            /*width: 250px;*/
            z-index: 100;
        }

        .btnPop {
            background-color: #033280;
            color: White;
            font-size: 12px;
            font-weight: bold;
            padding-left: 5px;
        }
    </style>
    <script>
        function showCollegeInfo() {
            $("#myModal").modal('show');

        }
        function showSubjectComb() {
            $("#myModal2").modal('show');

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/main-banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
                 <div class="row header">
            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-12 col-12">
                <nav class="nav-menu d-none d-lg-block">
                    <ul class="list">
                        <%--https://itiharyanaadmissions.nic.in--%>
                        <li><a href="/.."><span class="english">Home</span><span class="hindi" style="display:none;">होम</span></a></li>
                        <li class="active"><a href="frmDistrictWiseCollege.aspx"><span class="english">District Wise Institutes List</span><span class="hindi" style="display:none;">जिला वार संस्थान सूची</span></a></li>
                    <%--    
                     <!--  <li><a href="UG/DHE/frmViewVacantSeatsPG.aspx"><span class="english">View Vacant Seats-PG </span><span class="hindi" style="display:none;">खाली सीट देखें</span></a></li> --> 
	                    <li><a href="UG/DHE/frmUpdateFamilyId.aspx"><span class="english">Trade List</span><span class="hindi" style="display:none;">व्यापार सूची </span></a></li>
                        <!--   <li><a href="#" onclick="javascript: alert('Integration of Merit Module with Fee Module is in progress. Revised merit list along with fee payment option will be available shortly');"><span class="english">Merit List</span><span class="hindi" style="display:none;">योग्यता क्रम सूची</span></a></li> -->
                    <li><a href="UG/reports/meritList"><span class="english">Search Institute</span><span class="hindi" style="display:none;">संस्थान खोजें </span></a></li> 
                        <li><a href="UG/DHE/frmResultUGAdmissions.aspx"><span class="english">Trade Wise Vacant/Filled Seats</span><span class="hindi" style="display:none;">ट्रेड वार रिक्त / भरी हुई सीटें </span></a></li>
                      <!--<li><a href="UG/DHE/frmViewPaymentStatus.aspx"><span class="english">Admission Schedules </span><span class="hindi" style="display:none;">भुगतान की स्थिति देखें</span></a></li>-->
                        <!--  <li><a href="#" onclick="javascript: alert('Integration of Merit Module with Fee Module is in progress. Revised merit list along with fee payment option will be available shortly');"><span class="english">Know Your Result</span><span class="hindi" style="display:none;">अपना परिणाम जानिए</span></a></li> -->
                       <!-- <li><a href="DHE/CutOffList.aspx"><span class="english">Cut Off List</span><span class="hindi" style="display:none;">कट ऑफ लिस्ट</span></a></li>-->--%>


                    </ul>

                </nav><!-- .nav-menu -->
           
            </div>
            <div class="col-xl-4 col-lg-4 col-md-4 col-sm-12 col-12 cus-right-button text-right">
				               

                <!--<a href="UG/DHE/frmlogin.aspx" target="_blank"><span class="english"><img src="assets/images/login.png">&nbsp;Official Login</span><span class="hindi" style="display:none;">आधिकारिक लॉगिन</span></a>-->

            </div>
        </div>
          
        </div>
        <div class="container-fluid" style="background:#f6f6f6;">

            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading">
                <h4 class="heading">District Wise Govt. ITI / Pvt. ITI</h4>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 cus-left-section">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                        <label>Select District</label><br />
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlDistrict"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select District" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                        <label>Select ITI Type</label><br />
                        <asp:DropDownList ID="ddlCollegeType" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollegeType"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select College Type" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>


                   <%-- <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                         <label class="cus-select-label">Select Admission</label><%--<span style="color: red">*</span>-
                        <asp:DropDownList ID="ddlUGPG" runat="server" CssClass="form-control">
                             <asp:ListItem Text="--Please Select Admission--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="UG" Value="UG"></asp:ListItem>
                            <asp:ListItem Text="PG" Value="PG"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlUGPG"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Admission" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>--%>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-getbtn text-center">
                        <asp:Button ID="btSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btSearch_Click" ValidationGroup="A" />
                    </div>
                </div>
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-8 col-12 cus-grid-table cus-right-section">

                <div class="table-responsive-lg table-responsive-md table-responsive-sm">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" class="table table-bordered table-striped table-hover">

                        <Columns>
                            <%--<asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="3%">
                                                        <ItemTemplate>
                                                            
                                                            </ItemTemplate>

                                                    </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblsrno" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ITI Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("collegename") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ITI Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblCollegeType" runat="server" Text='<%# Eval("collegetype") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkView" CommandName="View" CssClass="btn btn-danger btn-sm" Text="View" CommandArgument='<%# Container.DataItemIndex %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CollegeId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCollegeid1" runat="server" Text='<%# Eval("collegeid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>

            </div>
                </div>
            </div>
      
            
        </div>
        <div class="modal cus-modal" id="myModal" runat="server" role="dialog">
            <div class="modal-dialog  modal-lg" style="margin-top: 1%;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row" style="width: 100%;">
                            <div class="col-lg-10 col-md-10 col-sm-8 col-8 cus-modal-heading" style="padding: 0;">
                                <h6 class="modal-title" style="font-weight: bold; padding-left: 15px;">ITI Information</h6>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-4 col-4 cus-cross">
                                <button type="button" class="close" data-dismiss="modal" onclick="">&times;</button>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body" id="mydatamodel1" style="display: block;">
                        <div class="cus-grid-table-one table-responsive">
                            <div>
                                <table class="table table-bordered table-striped table-hover">
                                    <tbody>
                                        <tr class="info">
                                            <td colspan="1">
                                                <label>ITI Name</label></td>
                                            <td id="tdCName" runat="server" colspan="3"></td>
                                        </tr>
                                        <tr class="warning">
                                            <td>
                                                <label>ITI Type</label></td>
                                            <td id="tdCType" runat="server"></td>
                                            <td>
                                                <label>District</label></td>
                                            <td id="tdDist" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Is ITI for Ex-Serviceman</label>
                                                   </td>
                                                <td id="tdExServiceman" runat="server"></td>
                                         
                                        
                                            <td>
                                                <label>Is ITI for Deaf & Dumb</label>
                                                   </td>
                                                <td id="tdDeafDumb" runat="server"></td>
                                         
                                        </tr>
                                            <tr>
                                            <td>
                                                <label>Is ITI for Public-Private Partnerships:</label>
                                                   </td>
                                                <td id="tdPPP" runat="server"></td>
                                          <td>
                                                <label>Education Mode</label></td>
                                            <td id="tdEduMode" runat="server"></td>
                                        </tr>
                                        <tr class="success">
                                            <td>
                                                <label>ITI Address</label></td>
                                            <td colspan="3" id="tdAddress" runat="server"></td>
                                           
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>ITI Contact</label></td>
                                            <td id="tdContact" runat="server"></td>
                                            <td>
                                                <label>ITI Email</label></td>
                                            <td id="tdEmail" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Name of Principal</label></td>
                                            <td id="tdPrincipalName" runat="server"></td>
                                            <td>
                                                <label>Principal Phone</label></td>
                                            <td id="tdPrincipalPhoneNo" runat="server"></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label>Nodal Officer (Online Admission)</label></td>
                                            <td id="tdNodalOfficer" runat="server"></td>
                                            <td>
                                                <label>Phone</label></td>
                                            <td id="tdNodalPhone" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Co-Ordinator (One)</label></td>
                                            <td id="tdArts" runat="server"></td>
                                            <td>
                                                <label>Phone</label></td>
                                            <td id="tdArtsPhone" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Co-Ordinator (Two)</label></td>
                                            <td id="tdComm" runat="server"></td>
                                            <td>
                                                <label>Phone</label></td>
                                            <td id="tdCommPhone" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Co-Ordinator (Three)</label></td>
                                            <td id="tdSc" runat="server"></td>
                                            <td>
                                                <label>Phone</label></td>
                                            <td id="tdScPhone" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Co-Ordinator (Four)</label></td>
                                            <td id="tdJobOrd" runat="server"></td>
                                            <td>
                                                <label>Phone</label></td>
                                            <td id="tdJobOrdPhone" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Co-Ordinator (Five)</label></td>
                                            <td id="tdFeeStruct" runat="server"></td>
                                            <td>
                                                <label>Phone</label></td>
                                            <td id="tdFeeStructPhone" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Website URL</label></td>
                                            <td colspan="3" id="tdWebsite" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center; font-weight: 800;">
                                                <label>Main Attraction of ITI</label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" id="tdMainAttract" runat="server"></td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div id="dvPros" runat="server" class="top-section-btn" style="text-align: right">
                            <button id="btnPros" class="btn btn-success" onclick="downloadPDF($('#hdPros').val()); return false;">Download Prospectus</button>


                            <asp:HiddenField ID="hdPros" runat="server" />
                        </div>
                        <div class="pop-note">
                            <h5 style="color: #F6822F; font-weight: 800; font-size: 18px;">Trades:</h5>
                            <%--<ul class="listClass">
                                
                                <li>Total seats might vary as per the university directions</li>
                                <li id="liNoteUG" runat="server">In case of BA and BSC (Medical and Non-Medical) courses. Additional subject combination fee would be charged as per College/University norms</li>
                            </ul>-->--%>
                        </div>
                        <div class="table-responsive-lg table-responsive-md table-responsive-sm cus-grid-table-one">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrno" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trade Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("coursename") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trade Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("sectionname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="Total Seats">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalSeats" runat="server" Text='<%# Eval("totalseats") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Course Fee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourseFee" runat="server" Text='<%# Eval("coursefee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Practical Fee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPracticalFee" runat="server" Text='<%# Eval("practical_fee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="coursesection Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcoursesectionId" runat="server" Text='<%# Eval("coursesectionid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="course Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcourseId" runat="server" Text='<%# Eval("courseid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>

                            <%--PG--%>

                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrno" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Course Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("coursename") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("sectionname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Seats">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalSeats" runat="server" Text='<%# Eval("totalseats") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Sport Seats">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSport" runat="server" Text='<%# Eval("sportseat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Course Fee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourseFee" runat="server" Text='<%# Eval("coursefee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="coursesection Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcoursesectionId" runat="server" Text='<%# Eval("coursesectionid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="course Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcourseId" runat="server" Text='<%# Eval("courseid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="modal cus-modal cus-inner-modal" id="myModal2" role="dialog">
            <div class="modal-dialog  modal-lg" style="margin-top: 1%;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row" style="width: 100%;">
                            <div class="col-lg-10 col-md-10 cus-modal-heading" style="padding: 0;">
                                <h6 class="modal-title" style="font-weight: bold; padding-left: 15px;">Subject Combination Information</h6>
                            </div>
                            <div class="col-lg-2 col-md-2 cus-cross" style="padding: 0;">
                                <button type="button" class="close" data-dismiss="modal" onclick="showCollegeInfo();">&times;</button>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body" id="mydatamodel2" style="display: block;">
                        <div class="cus-grid-table-two table-responsive">

                            <table class="table table-bordered table-striped table-hover">
                                <tbody>
                                    <tr class="info">
                                        <td colspan="1">
                                            <label>ITI Name</label></td>
                                        <td id="tdG3CN" runat="server"></td>
                                    </tr>
                                    <tr class="warning">
                                        <td>
                                            <label>Course</label></td>
                                        <td id="tdG3Course" runat="server"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                        <div class="table-responsive cus-grid-table-three">
                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">

                                <Columns>
                                    <%-- <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrno" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Subject Combination">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("SubjectCombination") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Seats">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalSeats" runat="server" Text='<%# Eval("totalseats") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Total Fees">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalFees" runat="server" Text='<%# Eval("totalfee") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="AIOC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblopenSeats" runat="server" Text='<%# Eval("openSeats") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HOGC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHOGC" runat="server" Text='<%# Eval("haryanaGeneral") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EWS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEWS" runat="server" Text='<%# Eval("EcoWeaker") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total HOGC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalHOGC" runat="server" Text='<%# Eval("TotalHOGCEWS") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSC" runat="server" Text='<%# Eval("SC") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DSC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDSC" runat="server" Text='<%# Eval("DSC") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total SC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalSC" runat="server" Text='<%# Eval("SCTotal") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BCA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBCA" runat="server" Text='<%# Eval("BCA") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BCB">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBCB" runat="server" Text='<%# Eval("BCB") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BCB">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalBC" runat="server" Text='<%# Eval("BCTotal") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DAG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDAG" runat="server" Text='<%# Eval("PHGen") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DABC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDABC" runat="server" Text='<%# Eval("PHBC") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DASC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDASC" runat="server" Text='<%# Eval("PHSC") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total DA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalDA" runat="server" Text='<%# Eval("DA") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMG" runat="server" Text='<%# Eval("ESMGenCatWise") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMBCA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMBCA" runat="server" Text='<%# Eval("ESMBCACatWise") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMBCB">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMBCB" runat="server" Text='<%# Eval("ESMBCBCatWise") %>' CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMSC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMSC" MaxLength="3" runat="server" Text='<%# Eval("ESMSCCatWise") %>' onblur="startCalc(this); TotalCalcESMW(this);" CssClass="form-control"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMDSC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMDSC" MaxLength="3" runat="server" Text='<%# Eval("ESMSCDCatwise") %>' onblur="startCalc(this); TotalCalcESMW(this);" CssClass="form-control"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total ESM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalESM" runat="server" Text='<%# Eval("TotalESM") %>'></asp:Label>
                                            <asp:HiddenField ID="hdTotalESM" runat="server" Value='<%# Eval("TotalESM") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
                <div class="input-group input-group-sm mb-3">
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
                    <div class="col-lg-10 col-md-10 text-left" style="margin-top:10px;">
                        <div class="credits">
                            <a>Site is technically designed, hosted and maintained by National Informatics Centre, Haryana</a>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2">
                        <img src="../assets/images/nic-logo.png" style="width:100px;"/>
                    </div>
</div>
                </div>
                
            </div>
           
        </div>
    </footer>
    <!-- End Footer -->
	
	
	<script>
          // Smooth scroll for the navigation menu and links with .scrollto classes
          var scrolltoOffset = $('#header').outerHeight() - 17;
          $(document).on('click', '.nav-menu a, .mobile-nav a, .scrollto', function (e) {
              if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
                  var target = $(this.hash);
                  if (target.length) {
                      e.preventDefault();

                      var scrollto = target.offset().top - scrolltoOffset;

                      if ($(this).attr("href") == '#header') {
                          scrollto = 0;
                      }

                      $('html, body').animate({
                          scrollTop: scrollto
                      }, 1500, 'easeInOutExpo');

                      if ($(this).parents('.nav-menu, .mobile-nav').length) {
                          $('.nav-menu .active, .mobile-nav .active').removeClass('active');
                          $(this).closest('li').addClass('active');
                      }

                      if ($('body').hasClass('mobile-nav-active')) {
                          $('body').removeClass('mobile-nav-active');
                          $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                          $('.mobile-nav-overly').fadeOut();
                      }
                      return false;
                  }
              }
          });

          // Activate smooth scroll on page load with hash links in the url
          $(document).ready(function () {
              if (window.location.hash) {
                  var initial_nav = window.location.hash;
                  if ($(initial_nav).length) {
                      var scrollto = $(initial_nav).offset().top - scrolltoOffset;
                      $('html, body').animate({
                          scrollTop: scrollto
                      }, 1500, 'easeInOutExpo');
                  }
              }
          });

          // Mobile Navigation
          if ($('.nav-menu').length) {
              var $mobile_nav = $('.nav-menu').clone().prop({
                  class: 'mobile-nav d-lg-none'
              });
              $('body').append($mobile_nav);
              $('body').prepend('<button type="button" class="mobile-nav-toggle d-lg-none"><i class="icofont-navigation-menu"></i></button>');
              $('body').append('<div class="mobile-nav-overly"></div>');

              $(document).on('click', '.mobile-nav-toggle', function (e) {
                  $('body').toggleClass('mobile-nav-active');
                  $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                  $('.mobile-nav-overly').toggle();
              });

              $(document).on('click', '.mobile-nav .drop-down > a', function (e) {
                  e.preventDefault();
                  $(this).next().slideToggle(300);
                  $(this).parent().toggleClass('active');
              });

              $(document).click(function (e) {
                  var container = $(".mobile-nav, .mobile-nav-toggle");
                  if (!container.is(e.target) && container.has(e.target).length === 0) {
                      if ($('body').hasClass('mobile-nav-active')) {
                          $('body').removeClass('mobile-nav-active');
                          $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                          $('.mobile-nav-overly').fadeOut();
                      }
                  }
              });
          } else if ($(".mobile-nav, .mobile-nav-toggle").length) {
              $(".mobile-nav, .mobile-nav-toggle").hide();
          }

          // Navigation active state on scroll
          var nav_sections = $('section');
          var main_nav = $('.nav-menu, #mobile-nav');

          $(window).on('scroll', function () {
              var cur_pos = $(this).scrollTop() + 200;

              nav_sections.each(function () {
                  var top = $(this).offset().top,
                      bottom = top + $(this).outerHeight();

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               if (cur_pos >= top && cur_pos <= bottom) {
                      if (cur_pos <= bottom) {
                          main_nav.find('li').removeClass('active');
                      }
                      main_nav.find('a[href="#' + $(this).attr('id') + '"]').parent('li').addClass('active');
                  }
                  if (cur_pos < 300) {
                      $(".nav-menu ul:first li:first").addClass('active');
                  }
              });
          });
    </script>
</body>
</html>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             