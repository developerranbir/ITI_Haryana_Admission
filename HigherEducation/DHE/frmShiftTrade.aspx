<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmShiftTrade.aspx.cs" Inherits="HigherEducation.HigherEducations.frmShiftTrade" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student's Shift Trade</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylecancel.css" rel="stylesheet" />

    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>

    <script>


        function sweetAlertConfirm(btnShift) {

            if (btnShift.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnShift.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: 'Are you sure?',
                    text: "You won't be able to revert this!",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, shift it!'
                })
                    .then(function () {
                        // Set data-confirmed attribute to indicate that the action was confirmed
                        btnShift.dataset.confirmed = true;
                        // Trigger button click programmatically
                        btnShift.click();
                    }).catch(function (reason) {
                        // The action was canceled by the user
                    });
            }
        }
        function ConfirmOnShift() {
            var validated = Page_ClientValidate('A');
            if (validated) {

                if (confirm("Do you really want to shift trade of student?") == true)
                    return true;
                else
                    return false;
            }

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
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
        </div>
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-12">
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Student's Shift Trade</h4>
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
                        <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="30" runat="server" autocompleteoff="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter RegistrationId" ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid RegistrationId" ValidationExpression="^[A-Za-z0-9\s]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn">
                        <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click" ValidationGroup="B" />
                    </div>
                </div>
            </div>
            <div id="dvSection" runat="server" style="display: none;">
                <div class="row">
                    <div id="dvRegId" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Registration Id:</label><br />
                        <asp:Label ID="lblRegId" runat="server"></asp:Label>
                    </div>
                 
                    <div id="dvCollege" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">ITI Name:</label><br />
                        <asp:Label ID="lblCollege" runat="server"></asp:Label>
                    </div>

                    <div id="dvCourseSectionName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Trade Course Section:</label><br />
                        <asp:Label ID="lblCourseSectionName" runat="server"></asp:Label>
                    </div>
                    <div id="dvAdmissionStatus" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Admission Status:</label><br />
                        <asp:Label ID="lblAdmissionStatus" runat="server"></asp:Label>
                    </div>
                      <div id="dvpaymenttransid" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Payment Transaction Id:</label><br />
                        <asp:Label ID="lblPayment_transactionId" runat="server"></asp:Label>
                        
                    </div>
                   
                    <div id="dvCounselling" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Counselling Round:</label><br />
                        <asp:Label ID="lblCounselling" runat="server"></asp:Label>
                    </div>

                    <div id="dvTradeSection" class="col-lg-12 col-md-12 col-sm-12 col-12" runat="server" style="display: none;">
                        <div class="row">
                             <div id="dvITI" class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-10" runat="server" style="display: none;">
                        <label>Shift to Another ITI:</label>
                        
                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="rfddlCollege" runat="server" ControlToValidate="ddlCollege" CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select ITI" ValidationGroup="A"></asp:RequiredFieldValidator>

                          </div>
                            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                                <label>Shift to Trade Name:</label>
                                <asp:DropDownList ID="ddlCourseName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCourseName_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RFCourseName" runat="server" ControlToValidate="ddlCourseName"
                                    CssClass="badge badge-danger" Display="Dynamic"  InitialValue="0" ErrorMessage="Please Select Course Name." ValidationGroup="A"></asp:RequiredFieldValidator>
                              
                                <asp:HiddenField ID="hdCollegeid" runat="server" />
                            </div>
                             <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                                <label>Shift to Section Name:</label>
                                <asp:DropDownList ID="ddlSectionName" runat="server" CssClass="form-control"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RFSectioName" runat="server" ControlToValidate="ddlSectionName"
                                    CssClass="badge badge-danger" Display="Dynamic"  InitialValue="0" ErrorMessage="Please Select Section Name." ValidationGroup="A"></asp:RequiredFieldValidator>
                              
                            </div>
                        </div>
                    </div>

                    <div id="dvShift" class="col-lg-12 col-md-12 col-sm-12 col-12" runat="server" style="display: none;">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                                <label class="cus-select-label">Remarks:</label><span style="color: red">*</span><br />
                                <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="500" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                                    CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Remarks." ValidationGroup="A"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtRemarks"
                                    CssClass="badge badge-danger" ErrorMessage="Invalid Remarks" ValidationExpression="^[A-Za-z0-9\s]{5,500}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                            </div>


                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-getbtn text-center" style="margin-top: 15px; margin-bottom: 10%">
                                <asp:Button ID="btnShift" runat="server" CssClass="btn btn-danger btn-sm" Text="Shift Trade" OnClick="btnShift_Click" OnClientClick="ConfirmOnShift();" ValidationGroup="A" />
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
                            <img src="/assets/images/nic-logo.png" style="width: 100px;" />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
</body>
</html>
