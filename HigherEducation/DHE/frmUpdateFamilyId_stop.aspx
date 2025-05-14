<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmUpdateFamilyId.aspx.cs" Inherits="HigherEducation.HigherEducations.frmUpdateFamilyId" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Detail</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/jqueryui.css" rel="stylesheet" />
    <link href="../assets/css/stylecancel.css" rel="stylesheet" />
    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet"/>
    <link href="../assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet"/>
    <link href="../assets/vendor/animatecss/animate.min.css" rel="stylesheet"/>
    <link href="../assets/vendor/remixicon/remixicon.css" rel="stylesheet"/>
    <link href="../assets/vendor/venobox/venobox.css" rel="stylesheet"/>
    <link href="../assets/vendor/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet"/>
    <link href="../assets/vendor/aos/aos.css" rel="stylesheet"/>
    <%--<script src="../assets/js/jquery-3.4.1.js"></script>--%>
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
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
    </style>
    <script>
        $(function () {
            $("#txtsdob").datepicker({
                changeMonth: true,
                 yearRange: "-50:+0",
                //yearRange: '1950:2005',
                //minDate: '01/01/1950',
                maxDate: 0,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                controlType: 'select',
                //timeFormat: 'HH:mm:ss',
                showOn: 'focus',
                showButtonPanel: true,
                closeText: 'Clear', // Text to show for "close" button
                onClose: function () {
                    var event = arguments.callee.caller.caller.arguments[0];
                    // If "Clear" gets clicked, then really clear it
                    if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                        $(this).val('');
                    }
                }
            });
        });
       // $("#txtsdob").inputmask({ "mask": "##/##/####" });
        $('#txtsdob').datepicker({ dateFormat: 'dd/mm/yy', changeYear: true, changeMonth: true });;
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
             <div class="row header">
                <div class="col-xl-10 col-lg-10 col-md-10 col-sm-12 col-12">
                    <nav class="nav-menu d-none d-lg-block">
                        <ul>
                            
                             <li ><a href="https://itiharyanaadmissions.nic.in/"><span class="english">Home</span><span class="hindi" style="display:none;">होम</span></a></li>
              <li ><a href="frmDistrictWiseCollege.aspx"><span class="english">District Wise College</span><span class="hindi" style="display:none;">जिला वार कॉलेज</span></a></li>
              <li ><a href="frmViewCollegeSeatMatrix.aspx"><span class="english">View Seat Matrix</span><span class="hindi" style="display:none;">सीट मैट्रिक्स</span></a></li>
              <li class="active"><a href="frmUpdateFamilyId.aspx"><span class="english">Update FamilyId</span><span class="hindi" style="display:none;">अपडेट परिवार आईडी</span></a></li>
              <li><a href="frmResultUGAdmissions.aspx"><span class="english">Know Your Result</span><span class="hindi" style="display:none;">अपना परिणाम जानिए</span></a></li>

                            
                            
                            <!--   <li><a href="MeritList.aspx">Merit List</a></li>
                            <li><a href="CutOffList.aspx">CutOff List</a></li> -->
                        </ul>

                    </nav>
                    <!-- .nav-menu -->
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-12">
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Update your Family Id</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="#" onclick="redirect();" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
             <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
            <div class="cus-top-section" style="margin-top: 20px;">
                <div class="row">
                    <%--<div class="col-lg-2 col-md-2"> 
                        
                    </div>--%>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-12">  
                            <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                        <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                    </div>
                    <%-- <div class="col-lg-2 col-md-2"> 
                       
                    </div>--%>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-12">    
                             <label class="cus-select-label">Name:<span style="color: red">*</span></label>
                        <asp:TextBox ID="txtName" CssClass="form-control" MaxLength="20" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Name." ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtName"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Name" ValidationExpression="^[A-Za-z\s]{2,100}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                    </div>
                     <%--<div class="col-lg-2 col-md-2"> 
                       
                    </div>--%>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-12"> 
                         <label class="cus-select-label">Date of Birth:<span style="color: red">*</span></label>
                        <asp:TextBox ID="txtsdob"  CssClass="form-control" MaxLength="20" runat="server" placeholder="dd/mm/yyyy" autocomplete="off"></asp:TextBox>
                        
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtsdob"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter DOB" ValidationGroup="B"></asp:RequiredFieldValidator>
                 
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server"
                       CssClass="badge badge-danger" ControlToValidate="txtsdob" Display="Dynamic" ErrorMessage="Invalid field" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ValidationGroup="B"></asp:RegularExpressionValidator>       

                        </div>
                        <div id="dvCaptcha" runat="server" class="col-lg-2 col-md-2 col-sm-2"  >
							<label  class="cus-select-label">Captcha</label><span id="spcaptcha" style="color: red">*</span>
								
									<asp:TextBox ID="txtturing" runat="server" MaxLength="10" placeholder="Enter Captcha" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" CssClass="badge badge-danger" ID="RequiredFieldValidator10" runat="server"
									ErrorMessage="Enter Captcha" ControlToValidate="txtturing" ValidationGroup="B"></asp:RequiredFieldValidator>
						
						</div>
								<div class="col-lg-2 col-md-2 col-sm-6 col-7 mobilecaptcha" style="margin-top: 22px;padding: 0px;">
									<img id="imgCaptcha" runat="server" src="Turing.aspx" style="margin-top: 8px; width: 100px; height: 32px; margin-top: 5px;" alt="" />
								
								
									<asp:ImageButton ID="imgRefreshCaptcha" ImageUrl="../assets/images/refresh.png" Style="width: 16px;margin-top: 12px;" OnClientClick="Turnig.aspx" AlternateText="No Image available" runat="server" />
								
                                    </div>
                          

                    <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn" style="margin-bottom:20px;padding: 30px;">
                        <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click" ValidationGroup="B" />
                    </div>
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
                   <div id="dvFamilyId" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Family Id:</label><br />
                        <asp:Label ID="lblFamilyId" runat="server"></asp:Label>
                    </div>
                   <div id="dvFamilyIdInput" runat="server" style="display:none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Family Id:</label><br />
                        <asp:TextBox ID="txtFamilyId" runat="server" MaxLength="13"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFamilyId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Family Id." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtFamilyId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Family Id" ValidationExpression="^[A-Za-z0-9]{8,13}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                              
                        <asp:HiddenField ID="hdMobileNo" runat="server" />
                        <asp:HiddenField ID="hdEmailId" runat="server" />
                   </div>
            </div>

            <div id="dvUpdateFamilyId" runat="server" class="col-lg-12 col-md-12 col-sm-12 col-12 cus-getbtn text-center" style="margin-bottom:20px;padding: 30px;display:none;">
                        <asp:Button ID="btnUpdFamilyId" runat="server" CssClass="btn btn-danger" Text="Update Family Id" OnClick="btnUpdFamilyId_Click" ValidationGroup="A" />
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
    <script>
        function redirect() {
            window.location.href = "https://itiharyanaadmissions.nic.in/";
        }

    </script>
</html>
