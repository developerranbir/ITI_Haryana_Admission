<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmOfferingSeatsPG.aspx.cs" Inherits="HigherEducation.HigherEducations.frmOfferingSeatsPG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Physical Counselling For PG</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/styleoffering.css" rel="stylesheet" />

    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>

    <script>



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

       

        
    </script>


    <script type="text/javascript">

        function RadioCheck(rb) {

            var gv = document.getElementById("<%=GrdCourseSection.ClientID%>");

            var rbs = gv.getElementsByTagName("input");



            var row = rb.parentNode.parentNode;

            for (var i = 0; i < rbs.length; i++) {

                if (rbs[i].type == "radio") {

                    if (rbs[i].checked && rbs[i] != rb) {

                        rbs[i].checked = false;

                        break;

                    }

                }

            }

        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="updone" runat="server">
                <ContentTemplate>
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-12">
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Physical Counselling For PG</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="cus-top-section" style="margin-top: 20px;">
                <div class="row">
                    <div class="col-lg-4 col-md-4">

                        <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                        <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                    </div>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn">
                        <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click" ValidationGroup="B" />
                    </div>
                </div>
                <div class="row  cus-getbtn">
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-primary" Text="View Form" OnClick="btnView_Click"  Style="margin-left: 50%;" />
                </div>
            </div>

            <div id="dvSection" runat="server" style="display: none;">
                <div class="row">
                    <div id="dvStuName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Student Name:</label><br />
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
                    <div id="dvStuFatherName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Father Name:</label><br />
                        <asp:Label ID="lblStuFatherName" runat="server"></asp:Label>
                    </div>
                    <div id="dvStuMotherName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Mother Name:</label><br />
                        <asp:Label ID="lblStuMotherName" runat="server"></asp:Label>
                    </div>

                    <div id="dvGender" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Gender:</label><br />
                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                    </div>

                    <div id="dvDOB" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">DOB:</label><br />
                        <asp:Label ID="lblDOB" runat="server"></asp:Label>
                    </div>
                    <div id="dvAadhaarNo" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Aadhaar No:</label><br />
                        <asp:Label ID="lblAadhaarNo" runat="server"></asp:Label>
                    </div>
                    <div id="dvReservCatg" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Reservation Category:</label><br />
                        <asp:Label ID="lblReservCatg" runat="server"></asp:Label>
                    </div>
                    <div id="dvCasteCatg" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Caste Category:</label><br />
                        <asp:Label ID="lblCasteCatg" runat="server"></asp:Label>
                    </div>
                    <div id="dvCaste" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Caste:</label><br />
                        <asp:Label ID="lblCaste" runat="server"></asp:Label>
                    </div>
                    <div id="dvExamPassed" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Qualifying Exam(Year):</label><br />
                        <asp:Label ID="lblExamPassed" runat="server"></asp:Label>
                    </div>
                    <div id="dvBoard" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">University/Board:</label><br />
                        <asp:Label ID="lblBoard" runat="server"></asp:Label>
                    </div>
                    <div id="dvStream" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Stream:</label><br />
                        <asp:Label ID="lblStream" runat="server"></asp:Label>
                    </div>

                    <div id="dvTopFivePercentage" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Qualifying Exam Percentage:</label><br />
                        <asp:Label ID="lblTopFivePercentage" runat="server"></asp:Label>
                    </div>
                    <div id="dvWeightage" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Weightage:</label><br />
                        <asp:Label ID="lblWeightage" runat="server"></asp:Label>
                    </div>
                    <div id="dvTotal" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Aggregate:</label><br />
                        <asp:Label ID="lblTotalPercentage" runat="server"></asp:Label>
                    </div>

                    <div id="dvFamilyId" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Family Id:</label><br />
                        <asp:Label ID="lblFamilyId" runat="server"></asp:Label>
                    </div>
                    <div id="dvTweleveHaryana" runat="server" style="display:none" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Is Qualifying Exam from Haryana:</label><br />
                        <asp:Label ID="lblTweleveHaryana" runat="server"></asp:Label>
                    </div>
                    <div id="dvDomicile" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Is Haryana Resident:</label><br />
                        <asp:Label ID="lblDomicile" runat="server"></asp:Label>
                    </div>
                    <%--   HIDE Column here--%>
                    <div id="dvPassingYear" runat="server" style="display: none" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Passing Year:</label><br />
                        <asp:Label ID="lblPassingYear" runat="server"></asp:Label>
                    </div>
                    <%-- <div id="dvMobileNo" runat="server" style="display:none" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">MobileNo:</label><br />
                        <asp:Label ID="lblMobileNo" runat="server"></asp:Label>
                    </div>
                    <div id="dvEmailId" runat="server" style="display:none" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">EmailId:</label><br />
                        <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                    </div>--%>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="row">
                     
                    <div id="dvCatgSeatAllotment" runat="server" class="col-lg-6 col-md-6 col-sm-6 col-12 m-b-10" style="display: none">
                        <label class="cus-select-label" style="color: #492A7F;font-weight: 700;font-size: 14px;">Category of Seat Allotment:</label><span style="color: red">*</span><br />
                        <label class="cus-select-label" style="color: red;font-weight: 700;font-size: 14px;">(Please choose very carefully. The college shall be solely responsible)</label>
                        <asp:DropDownList ID="ddlCatgAllotment" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Please Select Category--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="AIOC" Value="AIOC"></asp:ListItem>
                            <asp:ListItem Text="HOGC" Value="HOGC"></asp:ListItem>
                            <asp:ListItem Text="EWS-HOGC" Value="EWS"></asp:ListItem>
                            <asp:ListItem Text="SC" Value="SC"></asp:ListItem>
                            <asp:ListItem Text="DSC" Value="DSC"></asp:ListItem>
                            <asp:ListItem Text="BCA" Value="BCA"></asp:ListItem>
                            <asp:ListItem Text="BCB" Value="BCB"></asp:ListItem>
                            <asp:ListItem Text="DA" Value="DA"></asp:ListItem>
                            <asp:ListItem Text="ESM" Value="ESM"></asp:ListItem>
                            <asp:ListItem Text="FF" Value="FF"></asp:ListItem>
                            <asp:ListItem Text="Sports" Value="Sports"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCatgAllotment"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Seat Allotment" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                    <div id="dvPMSSCScholarShip" runat="server" class="col-lg-6 col-md-6 col-sm-6 col-12 m-b-10" style="display: none">
                        <label class="cus-select-label">Is the student beneficiary Under PostMatric ScholarShip Scheme:</label><span style="color: red">*</span>
                        <asp:RadioButtonList ID="rdbtnPMS_SC" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                            <asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rdbtnPMS_SC"
                            CssClass="badge badge-danger"  ErrorMessage="Please Select PostMatric ScholarShip Scheme" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div id="dvAdmissionStatus" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display: none;">
                  <asp:Label cssclass="cus-select-label" runat="server" Text="Admission Status:" style="color: #492A7F; font-weight: 800"></asp:Label>
                <asp:GridView ID="GrdAdmissionStatus" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" Style="width: 100%; margin-top: 20px; margin-bottom:10%;">
                    <Columns>
                        <asp:TemplateField HeaderText="College Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("collegename") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Section Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("courseSectionName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Subject Combination">
                            <ItemTemplate>
                                <asp:Label ID="lblSubComb" runat="server" Text='<%# Eval("SubjectCombination") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Admission Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("admissionstatus") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Transaction Id">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentTransId" runat="server" Text='<%# Eval("Payment_transactionId") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>


                    </Columns>
                    <HeaderStyle />
                </asp:GridView>


            </div>
         <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center cus-getbtn" id="dvFeeReceipt" runat="server" style="margin-bottom: 4%; display: none;">
            <%--<asp:Button ID="btnFeeReceipt" runat="server" CssClass="btn btn-success" Text="Fee Receipt"  ValidationGroup="B" />--%>
        </div>
            <div id="dvGrdCourseSection" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display: none;">
                <%-- <label class="cus-select-label" style="color: #492A7F; font-weight: 800">:</label>--%>
                <asp:GridView ID="GrdCourseSection" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" Style="width: 100%; margin-top: 20px;">
                    <Columns>
                        <asp:TemplateField HeaderText="Course Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                <asp:HiddenField ID="hdCourseid" runat="server" Value='<%# Eval("Courseid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Section Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("sectionname") %>'></asp:Label>
                                <asp:HiddenField ID="hdSectionid" runat="server" Value='<%# Eval("Sectionid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject Combination">
                            <ItemTemplate>
                                <asp:Label ID="lblSubComb" runat="server" Text='<%# Eval("SubjectCombination") %>'></asp:Label>
                                <asp:HiddenField ID="hdSubComb" runat="server" Value='<%# Eval("subjectcombinationid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:RadioButton ID="RadioButton1" runat="server" onclick="RadioCheck(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle />
                </asp:GridView>


            </div>

            <%-- GridView 2--%>
            <div id="dvGrdCourseSection2" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display: none;">

                <asp:GridView ID="GrdCourseSection2" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" Style="width: 100%; margin-top: 20px;">
                    <Columns>
                        <asp:TemplateField HeaderText="Course Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("coursename") %>'></asp:Label>
                                <asp:HiddenField ID="hdCourseid" runat="server" Value='<%# Eval("courseid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Section Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("sectionname") %>'></asp:Label>
                                <asp:HiddenField ID="hdSectionid" runat="server" Value='<%# Eval("Sectionid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject Combination">
                            <ItemTemplate>
                                <asp:Label ID="lblSubComb" runat="server" Text='<%# Eval("SubjectCombination") %>'></asp:Label>
                                <asp:HiddenField ID="hdSubComb" runat="server" Value='<%# Eval("subjectcombinationid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seat Offered">
                            <ItemTemplate>
                                <asp:Label ID="lblSeatOffered" runat="server" Text='<%# Eval("SeatOffered") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:RadioButton ID="RadioButton1" runat="server" onclick="RadioCheck(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <HeaderStyle />
                </asp:GridView>


            </div>

        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center cus-getbtn" id="dvChangeOffer" runat="server" style="margin-bottom: 4%; display: none;">
            <asp:Button ID="btnChangeOffer" runat="server" CssClass="btn btn-success" Text="Change Offer" ToolTip="If the Student wish to change the course offer same can be done using change offer" OnClick="btnChangeOffer_Click" ValidationGroup="B" />
           
        </div>
      <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center cus-getbtn" id="dvPayNow" runat="server" style="margin-bottom: 4%; display: none;">
           <asp:Button ID="btnPayNow" runat="server" CssClass="btn btn-danger" Text="Pay Now" onclick="btnPayNow_Click" ValidationGroup="B"  />
        </div>
                    
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 form-group cus-declaration" id="dvchkId" runat="server" style="display: none">
            <asp:CheckBox ID="chkId" runat="server" />
            <span style="color: black;" id="spengInfo" runat="server">The application form along with supporting documents has been physically verified and the student is found eligible as per norms. The Caste/Income/EWS/Resident certificate can also be verified by the Colleges from <a href="http://edisha.gov.in/eForms/Status" target="_blank">http://edisha.gov.in/eForms/Status</a> </span>

        </div>
         <div id="dvNote" class="col-lg-12 col-md-12 col-sm-12 col-12 cus-note" runat="server" style="display:none">
            <h6 style="color:red">Note: Offered seats are valid till today (Midnight).</h6>

        </div>

                    <div id="dvCaptcha" runat="server" style="padding: 0; display: none;margin:15px;">
                       
							<div class="row cus-captchalist">
								<div class="col-lg-3 col-md-3 col-sm-6 col-12 text-right">
									<asp:Label ID="lblCaptcha" CssClass="col-form-label" runat="server" Text="Captcha"></asp:Label><span id="spcaptcha" style="color: red">*</span>
								</div>
								<div class="col-lg-4 col-md-4 col-sm-6 col-12">
									<asp:TextBox ID="txtturing" runat="server" MaxLength="10" placeholder="Enter Captcha" CssClass="form-control"></asp:TextBox>
								</div>
								<div class="col-lg-1 col-md-1 col-sm-6 col-7 mobilecaptcha">
									<img id="imgCaptcha" runat="server" src="Turing.aspx" style="margin-top: 8px; width: 100px; height: 32px; margin-top: 5px;" alt="" />
								</div>
								<div class="col-lg-1 col-md-1 col-sm-6 col-2 login-captcha">
									<asp:ImageButton ID="imgRefreshCaptcha" ImageUrl="../assets/images/refresh.png" Style="width: 32px;" OnClientClick="Turing.aspx" AlternateText="No Image available" runat="server" />
								</div>
								<asp:RequiredFieldValidator Display="Dynamic" CssClass="badge badge-danger" ID="RequiredFieldValidator10" runat="server"
									ErrorMessage="Enter Captcha" ControlToValidate="txtturing" ValidationGroup="A"></asp:RequiredFieldValidator>
                               
							</div>
                            

						</div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center cus-getbtn" id="dvSave" runat="server" style="margin-bottom: 4%; display: none;">
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Offer Seat" OnClick="btnSave_Click" ValidationGroup="A" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnCancel_Click" />
        </div>

        <div class="btn-freeze">
            <asp:HiddenField ID="hdRefId" runat="server" />
            <asp:HiddenField ID="hdBoard_Code" runat="server" />
            <asp:HiddenField ID="hdqualification" runat="server" />
            <asp:HiddenField ID="hdGenderid" runat="server" />
            <asp:HiddenField ID="hdRollNo" runat="server" />
            <asp:HiddenField ID="hdCollegeid" runat="server" />
            <asp:HiddenField ID="hdCollegeName" runat="server" />
            <asp:HiddenField ID="hdReservationcategoryid" runat="server" />
            <asp:HiddenField ID="hdcategoryid" runat="server" />
            <asp:HiddenField ID="hdcasteid" runat="server" />
            <asp:HiddenField ID="hdmobileno" runat="server" />
            <asp:HiddenField ID="hdemailid" runat="server" />
            <asp:HiddenField ID="hdflgDelete" runat="server" />
            <asp:HiddenField ID="hdAadhaarNo" runat="server" />
            <asp:HiddenField ID="hdCourseid2" runat="server" />
            <asp:HiddenField ID="hdSectionid2" runat="server" />
            <asp:HiddenField ID="hdSubCombid2" runat="server" />

        </div>

        <div class="modal cus-modal" id="myModal1" role="dialog">
            <div class="modal-dialog  modal-lg" style="margin-top: 1%;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row" style="width: 100%;">
                            <div style="display: inline-block; width: 70%; float: left; text-align: left">
                                <h6 class="modal-title" style="font-weight: bold; padding-left: 15px;">Document</h6>
                            </div>
                            <div style="display: inline-block; width: 28%; float: left; text-align: right">
                                <button type="button" class="close" data-dismiss="modal" onclick="">&times;</button>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body" id="mydatamodel1" style="display: block;">

                        <iframe id="iframepdf" runat="server" src="" width="100%" height="300px"></iframe>
                        <div class="input-group input-group-sm mb-3">
                        </div>
                    </div>
                </div>
            </div>
        </div>
</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnGo" />
                   <asp:PostBackTrigger ControlID="imgRefreshCaptcha" />
                </Triggers>
                </asp:UpdatePanel>
        </div> <%--UpdatePanel--%>

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
                            <img src="/assets/images/nic-logo.png" style="width: 100px;" />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
</body>
</html>
