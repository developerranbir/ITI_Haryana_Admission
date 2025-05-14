<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmChangeCandidateMobile.aspx.cs" Inherits="HigherEducation.HigherEducations.frmChangeCandidateMobile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Candidate Mobile Change</title>
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
        .top-heading {
            color: #223f73;
            font-weight: 800;
            margin-top: 5px;
            margin-bottom: 10px;
            text-decoration: none;
            position: relative;
            font-family: "Montserrat", sans-serif;
        }
        h4{
            font-size: 1.5rem;
        }
    </style>
    <script>
        function ConfirmOnChange() {
            var validated = Page_ClientValidate('A');
            if (validated) {

                if (confirm("Do you want to change Mobile Number of the student?") == true)
                    return true;
                else
                    return false;
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
                                    <h4 class="heading">Change Student Mobile Number</h4>
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
                                    <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnGo_Click" ValidationGroup="B" />
                                </div>
                            </div>
                            <%-- <div class="row  cus-getbtn">
                                <asp:Button ID="btnView" runat="server" CssClass="btn btn-primary" Text="View Form" OnClick="btnView_Click" Style="margin-left: 50%;" />
                            </div>
                            --%>
                        </div>

                        <div id="dvSection" runat="server" style="display: none;">
                            <div class="row">
                                <div id="dvRegId" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">Registration Id:</label><br />
                                    <asp:Label ID="lblRegId" runat="server"></asp:Label>
                                </div>
                                <div id="dvStuName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">Student Name:</label><br />
                                    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                </div>
                                <div id="dvStuFatherName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">Father Name:</label><br />
                                    <asp:Label ID="lblStuFatherName" runat="server"></asp:Label>
                                </div>

                                <div id="dvGender" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">Gender:</label><br />
                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                                </div>

                                <div id="dvDOB" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">DOB:</label><br />
                                    <asp:Label ID="lblDOB" runat="server"></asp:Label>
                                </div>
                                <div id="Div1" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                </div>
                                <div id="dvCollegeName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">ITI Name</label><br />
                                    <asp:Label ID="lblCollegeName" runat="server"></asp:Label>
                                </div>
                                <div id="dvSectionName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">Section Name</label><br />
                                    <asp:Label ID="lblSectionName" runat="server"></asp:Label>
                                </div>
                                <div id="dvAdmissionStatus" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">Admission Status</label><br />
                                    <asp:Label ID="lblAdmissionStatus" runat="server"></asp:Label>
                                </div>
                                <div id="dvMobileNo" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">MobileNo:</label><br />
                                    <asp:Label ID="lblMobileNo" runat="server"></asp:Label>
                                </div>
                                <%-- 
                    <div id="dvEmailId" runat="server" style="display:none" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">EmailId:</label><br />
                        <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                    </div>--%>
                            </div>
                        </div>
                        
                        
                        <div id="dvCaptcha" runat="server" style="padding: 0; display: none; margin: 15px;">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                                <label class="cus-select-label">New Mobile Number:</label><span style="color: red">*</span><br />
                                <asp:TextBox ID="txtMobileNo" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobileNo"
                                    CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Plese Enter Mobile Number" ValidationGroup="A"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic" ControlToValidate="txtMobileNo"
                                    CssClass="badge badge-danger" ErrorMessage="Invalid Mobile Number" ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                                <label class="cus-select-label">Remarks:</label><span style="color: red">*</span><br />
                                <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="500" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                                    CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Remarks." ValidationGroup="A"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtRemarks"
                                    CssClass="badge badge-danger" ErrorMessage="Invalid Remarks" ValidationExpression="^[A-Za-z0-9\s]{5,500}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                            </div>

                            <br />
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
                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center cus-getbtn" id="dvSave" runat="server" style="margin-bottom: 8%; display: none;">
                            
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnSave_Click"  OnClientClick="ConfirmOnChange();" ValidationGroup="A" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>

                        
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnGo" />
                    <asp:PostBackTrigger ControlID="imgRefreshCaptcha" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <%--UpdatePanel--%>
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
