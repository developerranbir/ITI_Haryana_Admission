<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmCancellationStudentRegistration.aspx.cs" Inherits="HigherEducation.HigherEducations.frmCancellationStudentRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cancellation Student Registration</title>
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


        function sweetAlertConfirm(btnDelete) {
            debugger;
             if (btnDelete.dataset.confirmed) {
                 // The action was already confirmed by the user, proceed with server event
                 btnDelete.dataset.confirmed = false;
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
                     confirmButtonText: 'Yes, delete it!'
                 })
                     .then(function () {
                         // Set data-confirmed attribute to indicate that the action was confirmed
                         btnDelete.dataset.confirmed = true;
                         // Trigger button click programmatically
                         btnDelete.click();
                     }).catch(function (reason) {
                         // The action was canceled by the user
                     });
             }
        }
        function ConfirmOnDelete() {
            var validated = Page_ClientValidate('A');
            if (validated) {
                
                if (confirm("Do you really want to cancel student registration?") == true)
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
        .cus-middle-section{
            margin-bottom:5rem;
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
                        <h4 class="heading">Cancellation of Student's Registration</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="Container" style="margin-top: 20px;">
                <div class="row">
                     <div class="col-lg-2 col-md-2"> 
                        <label class="cus-select-label"  style="color: #223f73; font-weight: 800">Registration Id:<span style="color: red">*</span></label>
                    </div>
                          <div class="col-lg-4 col-md-4 col-sm-4 col-12">                      
                        <asp:TextBox ID="txtRollNo" CssClass="form-control" MaxLength="30" runat="server" autocompleteoff="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRollNo"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter RegistrationId/Roll No." ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtRollNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid RegistrationId/Roll No" ValidationExpression="^[A-Za-z0-9\s]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                 </div>
                    <%-- <div class="col-lg-2 col-md-2">
                        <label class="cus-select-label" style="color: #223f73; font-weight: 800">Select Admission:</label><span style="color: red">*</span>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-12">
                        <asp:DropDownList ID="ddlUGPG" runat="server" CssClass="form-control">
                             <asp:ListItem Text="--Please Select Admission--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="UG" Value="UG"></asp:ListItem>
                         
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlUGPG"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Admission" ValidationGroup="B"></asp:RequiredFieldValidator>

                    </div>--%>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn">
                        <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click" ValidationGroup="B" />
                    </div>
               </div>
                </div>
            <div id="dvSection" runat="server" >
              <div class="row">
                  <div id="dvStuName" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Student Name:</label><br />
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
                    <div id="dvStuFatherName" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Father Name:</label><br />
                        <asp:Label ID="lblStuFatherName" runat="server"></asp:Label>
                    </div>
                    <div id="dvRegId" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Registration Id:</label><br />
                        <asp:Label ID="lblRegId" runat="server"></asp:Label>
                    </div>
                    <div id="dvBoard" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Board:</label><br />
                        <asp:Label ID="lblBoard" runat="server"></asp:Label>
                    </div>
                    <div id="dvPassingYear" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Passing Year:</label><br />
                        <asp:Label ID="lblPassingYear" runat="server"></asp:Label>
                    </div>
                    <div id="dvMobileNo" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Mobile No:</label><br />
                        <asp:Label ID="lblMobileNo" runat="server"></asp:Label>
                    </div>
                    <div id="dvEmailId" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Email Id:</label><br />
                        <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                    </div>
               
               <div id="dvCancel" class="col-lg-12 col-md-12 col-sm-12 col-12" runat="server" style="display:none;">
                   <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                        <label class="cus-select-label">Remarks:</label><span style="color: red">*</span><br />
                        <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="500" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Remarks." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtRemarks"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Remarks" ValidationExpression="^[A-Za-z0-9\s]{5,500}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-getbtn text-center" style="margin-top:15px;">
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel Student's Registration"   OnClick="btnDelete_Click" OnClientClick="return ConfirmOnDelete();" ValidationGroup="A" />
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
                    <div class="col-lg-10 col-md-10 text-left" style="margin-top:10px;">
                        <div class="credits">
                            <a>Site is technically designed, hosted and maintained by National Informatics Centre, Haryana</a>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2">
                        <img src="/assets/images/nic-logo.png" style="width:100px;" />
                    </div>
</div>
                </div>
                
            </div>
           
        </div>
    </footer>
</body>
</html>
