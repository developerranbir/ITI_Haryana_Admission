<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmRestoreAdmission.aspx.cs" Inherits="HigherEducation.HigherEducations.frmRestoreAdmission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restore Admission</title>
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


        function sweetAlertConfirm(btnDelete) {

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
                    confirmButtonText: 'Yes, Restore it!'
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

                if (confirm("Do you really want to restore student admission?") == true)
                    return true;
                else
                    return false;
            }

        }
    </script>

<script>
    function previewFile() {
        debugger;
        var preview = document.querySelector('#<%=Image1.ClientID%>');
             var file = document.querySelector('#<%=File_Upload.ClientID%>').files[0];
        var reader = new FileReader();
        reader.onloadend = function () {

            preview.src = reader.result;
        }
        if (file) {

            var ext = file.name.substr(-3);//$('#File1').val().split('.').pop();
            if (ext == "pdf") {
                preview.src = "../assets/images/pdf.png";
                //preview.src = URL.createObjectURL(file);

                // $('#Image1').hide();

            }
            else {
                preview.src = "../assets/images/pdf.png";
            }

        }
        else {
            preview.src = "../assets/images/pdf.png";
        }

    }

       <%--function image1_preview() {
            debugger;
            var file = document.querySelector('#<%=File_Upload.ClientID%>').files[0];
             if (file) {

                 var ext = file.name.substr(-3);
                 if (ext == "pdf") {

                     $("#myModal1").modal('show');
                     $("#iframepdf").attr('src', URL.createObjectURL(file));
                     $("#iframepdf").show();
                 }
                

            }
             else if($('#hdPros').val()=='p'){

                 $("#myModal1").modal('show');
                 $("#iframepdf").show();
             }
         }--%>
    function ValidationFile1() {
        debugger;
        //Check FileUpload Validation
        //Check FileUpload Validation
        var lblError = $("#lblError");
        if ($('#<%=File_Upload.ClientID%>')[0].files.length === 0) {
                swal("Please upload document.");
                lblError.html("Please upload document.");
                $('#<%=File_Upload.ClientID%>').focus();
                return false;
            }
            else {
                document.getElementById("<%= lblError.ClientID %>").innerText = ""




                if ($('#<%=File_Upload.ClientID%>').val() != "") {

                    var allowedFiles = [".pdf"];
                    var fileUpload = $('#<%=File_Upload.ClientID%>');

                var filename1 = $('#<%=File_Upload.ClientID%>').val().replace(/C:\\fakepath\\/i, '');

                var filename = filename1.split('.').length - 1;
                var lblError = $("#lblError");
                var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
                if (!regex.test(fileUpload.val().toLowerCase()) || parseInt(filename) > 1) {
                    lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");

                    swal("", "Invalid filename/format", "");
                    return false;
                }
                else {
                    document.getElementById("<%= lblError.ClientID %>").innerText = ""
                }

                if (typeof ($('#<%=File_Upload.ClientID%>')[0].files) != "undefined") {
                    var size = parseInt($('#<%=File_Upload.ClientID%>')[0].files[0].size / 1024);
                    if (size > 1024) {
                        lblError.html("Document size should not be greater than " + "1 MB");
                        swal("", "Document size should not be greater than " + "1 MB", "");
                        $('#<%=File_Upload.ClientID%>').val('');
                        return false;
                    }

                }
                else {
                    document.getElementById("<%= lblError.ClientID %>").innerText = ""
                    return true;
                }

            }
            else {
                document.getElementById("<%= lblError.ClientID %>").innerText = ""
                return true;
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
                                    <h4 class="heading">Restore Cancelled Admission</h4>
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
                                <div id="dvExamPassed" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <label class="cus-select-label">Qualifying Exam(Year):</label><br />
                                    <asp:Label ID="lblExamPassed" runat="server"></asp:Label>
                                </div>
                                <div id="Div1" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
                                </div>
                                <div id="Div2" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12">
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
                        
                        
                        <div id="dvCaptcha" runat="server" style="padding: 0; display: none; margin: 15px;">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                                <label class="cus-select-label">Remarks:</label><span style="color: red">*</span><br />
                                <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="500" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                                    CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Remarks." ValidationGroup="A"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtRemarks"
                                    CssClass="badge badge-danger" ErrorMessage="Invalid Remarks" ValidationExpression="^[A-Za-z0-9\s]{5,500}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                            </div>

                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 m-b-10" >
                                <label>Document:</label><span style="color: red">*</span><br />
                                <asp:Image ID="Image1" runat="server" Height="100" ImageUrl="~/assets/images/pdf.png" Width="120" CssClass="img-fluid"  />
                                <asp:FileUpload ID="File_Upload" runat="server" onchange="previewFile(); ValidationFile1();" />
                                <asp:Label ID="lblFilename" CssClass="col-form-label" runat="server"></asp:Label><br />
                                <asp:Label ID="lblFile1Msg" runat="server" Text="(Only upload .pdf format. Max size upto 1 MB.)"></asp:Label>
                                <asp:Label ID="lblError" CssClass="badge badge-danger" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdDoc" runat="server" />
                    
                    
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
                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center cus-getbtn" id="dvSave" runat="server" style="margin-bottom: 4%; display: none;">
                            
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Restore Admission" OnClick="btnSave_Click"  OnClientClick="ValidationFile1(); ConfirmOnDelete();" ValidationGroup="A" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Reset" OnClick="btnCancel_Click" />
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
