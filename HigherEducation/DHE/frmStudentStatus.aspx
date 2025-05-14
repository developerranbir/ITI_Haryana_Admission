<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmStudentStatus.aspx.cs" Inherits="HigherEducation.HigherEducations.frmStudentStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application Status</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
     <link href="../assets/css/styleindex.css" rel="stylesheet" />
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
        #dvAdmissionStatus table th {
    background: #71738c;
    color: #fff;
}
    </style>
    <script>
        function showCollegeInfo() {
            $("#myModal").modal('show');

        }
        function showSubjectComb() {
            $("#myModal2").modal('show');

        }

        function ShowDocument() {
            $("#myModal1").modal('show');
            $("#iframepdf").show();
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
            <div class="col-xl-8 col-lg-8 col-md-6 col-sm-12 col-12">
                <nav class="nav-menu d-none d-lg-block">
                    <ul class="list">
                        <li><a href="https://itiharyanaadmissions.nic.in/"><span class="english">Home</span><span class="hindi" style="display:none;">होम</span></a></li>
                        <li><a href="UG/Reports/SearchCollegeIndex"><span class="english">Search College / Course</span><span class="hindi" style="display:none;">कॉलेज या पाठ्यक्रम खोजें</span></a></li> 
                        <li class="active"><a href="UG/DHE/frmStudentStatus.aspx"><span class="english">View Application Status</span><span class="hindi" style="display:none;">View Application Status</span></a></li>
                     <!--  <li><a href="UG/DHE/frmViewVacantSeatsPG.aspx"><span class="english">View Vacant Seats-PG </span><span class="hindi" style="display:none;">खाली सीट देखें</span></a></li> --> 
	                    <!-- <li><a href="UG/DHE/frmUpdateFamilyId.aspx"><span class="english">Update FamilyId</span><span class="hindi" style="display:none;">अपडेट परिवार आईडी</span></a></li> -->
                        <!--   <li><a href="#" onclick="javascript: alert('Integration of Merit Module with Fee Module is in progress. Revised merit list along with fee payment option will be available shortly');"><span class="english">Merit List</span><span class="hindi" style="display:none;">योग्यता क्रम सूची</span></a></li> -->
                    <!-- <li><a href="UG/reports/meritList"><span class="english">Merit List</span><span class="hindi" style="display:none;">योग्यता क्रम सूची</span></a></li>  -->
                        <!-- <li><a href="UG/DHE/frmResultUGAdmissions.aspx"><span class="english">Know Your Result</span><span class="hindi" style="display:none;">अपना परिणाम जानिए</span></a></li> -->
                      <!-- <li><a href="UG/DHE/frmViewPaymentStatus.aspx"><span class="english">View Payment Status </span><span class="hindi" style="display:none;">भुगतान की स्थिति देखें</span></a></li> -->
                        <!--  <li><a href="#" onclick="javascript: alert('Integration of Merit Module with Fee Module is in progress. Revised merit list along with fee payment option will be available shortly');"><span class="english">Know Your Result</span><span class="hindi" style="display:none;">अपना परिणाम जानिए</span></a></li> -->
                       <!-- <li><a href="DHE/CutOffList.aspx"><span class="english">Cut Off List</span><span class="hindi" style="display:none;">कट ऑफ लिस्ट</span></a></li>-->


                    </ul>

                </nav><!-- .nav-menu -->
            </div>
 
        </div>
        </div>
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-12">
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Application Status</h4>
                    </div>

                </div>
            </div>
            <div class="container" style="margin-top: 20px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2">
                        
                        <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Registration Id:<span style="color: red">*</span></label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-12">
                     
                        <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                         </div>              
                  
                      <div class="col-lg-2 col-md-2">
                        <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Select Admission:</label><span style="color: red">*</span>
                   </div>
                         <div class="col-lg-2 col-md-2 col-sm-2 col-12">
                        <asp:DropDownList ID="ddlUGPG" runat="server" CssClass="form-control">
                            <asp:ListItem Text="UG" Value="UG"></asp:ListItem>
                          <%-- <asp:ListItem Text="PG" Value="PG"></asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlUGPG"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Admission" ValidationGroup="A"></asp:RequiredFieldValidator>

             </div>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn">
                        <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click" ValidationGroup="B" />
                    </div>
                </div>
            </div>
            
            
            <div id="dvSection" runat="server" style="display: none;"   >
                <div class="row" >
                    <div id="dvStuName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Student Name:</label><br />
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
               
                    <div id="dvRegId" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Registration Id:</label><br />
                        <asp:Label ID="lblRegId" runat="server"></asp:Label>
                    </div>
                    
                 
                    <div id="dvStudentStatus" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Status</label><br />
                        <asp:Label ID="lblStudentStatus" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdMaxPage" runat="server" />
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
