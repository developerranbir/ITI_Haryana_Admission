<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCollegeGlance.aspx.cs" Inherits="HigherEducation.HigherEducations.frmCollegeGlance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <title>College Glance</title>
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/styleGlance.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>

    <script>
        function DisableRightClick(event) {
            if (event.button == 2) {
                alert("Right Clicking not allowed!");
            }
        }
        function MobileNoCheck() {
            var InvalideMobileList = ['0000000000', '1111111111', '2222222222', '3333333333', '4444444444', '5555555555', '6666666666', '7777777777', '8888888888', '9999999999', '1234567890', '9876543210']
            var MobileNo = document.getElementById('txtNodalOfficerPhoneNo').value;
            if (MobileNo == "") {
                swal("", "Please enter Phone No", "");
            }
            else if (InvalideMobileList.indexOf(MobileNo) > -1) {
                document.getElementById('txtNodalOfficerPhoneNo').value = "";
                swal("", "Phone No is not valid", "");
            }
        }
        <%--function previewFile() {
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
                    preview.src = "../assets/images/preview.png";
                    //preview.src = URL.createObjectURL(file);

                    // $('#Image1').hide();

                }
                else {
                    reader.readAsDataURL(file);
                }

            }
            else {
                preview.src = "../assets/images/preview.png";
            }

        }--%>

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
        <%--function ValidationFile1() {
            //debugger;
            //Check FileUpload Validation
           

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
                    if (size > 5120) {
                        lblError.html("Prospectus size should not be greater than " + "5 MB");
                        swal("", "Prospectus size should not be greater than " + "5 MB", "");
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

        }--%>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
        </div>
        <div class="container-fluid">

            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1"></div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">ITI at a Glance (Profile)</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                <div class="row cus-fee-top-section">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Name of ITI:</label>
                        <asp:TextBox ID="txtCollegeName" CssClass="form-control" disabled="true" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="rfddlCollege" runat="server" ControlToValidate="ddlCollege"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10" style="display:none">
                        <label>Affiliated With:</label>
                        <asp:TextBox ID="txtAffiliated" CssClass="form-control" disabled="true" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>District:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlDistrict"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select District" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                       <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is ITI for Ex-Serviceman:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlExServicemen" runat="server" CssClass="form-control">
                             <asp:ListItem Text="--Please Select--" Value="00"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            
                           
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlExServicemen"
                            CssClass="badge badge-danger" InitialValue="00" ErrorMessage="Please Select Ex-Servicemen" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is ITI for Deaf & Dumb:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlDeafDumb" runat="server" CssClass="form-control">
                             <asp:ListItem Text="--Please Select--" Value="00"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                          
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlDeafDumb"
                            CssClass="badge badge-danger" InitialValue="00" ErrorMessage="Please Select Deaf & Dumb" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                     <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is ITI for Public-Private Partnerships:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlPPP" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Please Select--" Value="00"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                           
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlPPP"
                            CssClass="badge badge-danger" InitialValue="00" ErrorMessage="Please Select Public-Private Partnerships" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>ITI Type:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlCollegeType" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCollegeType"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select College Type" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Education Mode:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlEduMode" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Co-Ed" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Only Girls College" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Only Boys College" Value="1"></asp:ListItem>

                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlEduMode"
                            CssClass="badge badge-danger" InitialValue="00" ErrorMessage="Please Select Education Mode" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Website:</label>
                        <asp:TextBox ID="txtwebsite" MaxLength="100" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtwebsite"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Invalid Website" ValidationExpression="(http|http(s)?://)?([\w-]+\.)+[\w-]+[.com|.in|.org]+(\[\?%&=]*)?" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Email:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                        <label style="color:#fff;" class="badge badge-primary">(Forget Password Link will be sent on this email id.)</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Email." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="txtEmail"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Invalid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="A"></asp:RegularExpressionValidator>
                        <asp:Label ID="lblscroll1" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Contact No.:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtContactNo" CssClass="form-control" MaxLength="25" runat="server"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMobile" runat="server" ControlToValidate="txtContactNo"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Contact No." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorMobile" runat="server" Display="Dynamic" ControlToValidate="txtContactNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Contact No" ValidationExpression="[0-9,]{10,25}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Name of Principal:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtPrincipalName" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrincipalName"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Principal Name." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtPrincipalName"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Principal Name" ValidationExpression="^[A-Za-z0-9.\s]{5,100}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Phone No. of Principal:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtPrincipalPhoneNo" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrincipalPhoneNo"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Phone No." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtPrincipalPhoneNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Phone No" ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Address/Location:</label><span style="color: red">*</span>

                        <asp:TextBox ID="txtAddress" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldAdress" runat="server" ControlToValidate="txtAddress"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Address" ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorAddress" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="Invalid Address"
                            CssClass="badge badge-danger" ValidationExpression="^^[A-Za-z0-9.,\s\/]{1,150}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
           
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading">
                <h4 class="heading">Admission Helpdesk</h4>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                <div class="row cus-fee-top-section">
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Nodal Officer (Online Admission):</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtNodalOfficer" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNodalOfficer"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Nodal Officer." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic" ControlToValidate="txtNodalOfficer"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Nodal Office" ValidationExpression="^[A-Za-z0-9.\s]{5,100}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Phone:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtNodalOfficerPhoneNo" CssClass="form-control" MaxLength="10" runat="server" onchange="MobileNoCheck();"></asp:TextBox>
                        <label style="color:#fff;" class="badge badge-primary">(Forget Password Link will be sent on this Mobile No.)</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNodalOfficerPhoneNo"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Phone No." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ControlToValidate="txtNodalOfficerPhoneNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Phone No" ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Co-Ordinator (One):</label>
                        <asp:TextBox ID="txtCrdArtstrmName" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCrdArtstrmName"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Co-Ordinator (Arts Stream)." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ControlToValidate="txtCrdArtstrmName"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Co-Ordinator (Arts Stream)" ValidationExpression="^[A-Za-z0-9.\s]{5,100}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Phone:</label>
                        <asp:TextBox ID="txtCrdArtstrmPhoneNo" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>

                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCrdArtstrmPhoneNo"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Phone No." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic" ControlToValidate="txtCrdArtstrmPhoneNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Phone No" ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Co-Ordinator (Two):</label>
                        <asp:TextBox ID="txtCrdCommStrmName" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCrdCommStrmName"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Co-Ordinator (Commerce Stream)." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic" ControlToValidate="txtCrdCommStrmName"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Co-Ordinator (Commerce Stream)" ValidationExpression="^[A-Za-z0-9.\s]{5,100}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Phone:</label><%--<span style="color: red">*</span>--%>
                        <asp:TextBox ID="txtCrdCommStrmPhoneNo" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>

                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCrdCommStrmPhoneNo"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Phone No." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" Display="Dynamic" ControlToValidate="txtCrdCommStrmPhoneNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Phone No" ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Co-Ordinator (Three):</label><%--<span style="color: red">*</span>--%>
                        <asp:TextBox ID="txtCrdSciStrmName" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCrdSciStrmName"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Co-Ordinator (Science Stream)." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" Display="Dynamic" ControlToValidate="txtCrdSciStrmName"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Co-Ordinator (Science Stream)" ValidationExpression="^[A-Za-z0-9.\s]{5,100}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Phone:</label><%--<span style="color: red">*</span>--%>
                        <asp:TextBox ID="txtCrdSciStrmPhoneNo" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>

                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCrdSciStrmPhoneNo"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Phone No." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" Display="Dynamic" ControlToValidate="txtCrdSciStrmPhoneNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Phone No" ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Co-Ordinator (Four):</label><%--<span style="color: red">*</span>--%>
                        <asp:TextBox ID="txtCrdJobCourses" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCrdJobCourses"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Co-Ordinator (Job-Oriented Courses)." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" Display="Dynamic" ControlToValidate="txtCrdJobCourses"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Co-Ordinator (Job-Oriented Courses)" ValidationExpression="^[A-Za-z0-9.\s]{5,100}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Phone:</label><%--<span style="color: red">*</span>--%>
                        <asp:TextBox ID="txtCrdJobCoursesPhoneNo" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>

                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCrdJobCoursesPhoneNo"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Phone No." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" Display="Dynamic" ControlToValidate="txtCrdJobCoursesPhoneNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Phone No" ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Co-Ordinator (Five):</label><%--<span style="color: red">*</span>--%>
                        <asp:TextBox ID="txtCrdFeeStruct" CssClass="form-control" MaxLength="100" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCrdFeeStruct"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Co-Ordinator (Fee Structure)." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" Display="Dynamic" ControlToValidate="txtCrdFeeStruct"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Co-Ordinator (Fee Structure)" ValidationExpression="^[A-Za-z0-9.\s]{5,100}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-4 col-12 m-b-10">
                        <label>Phone:</label><%--<span style="color: red">*</span>--%>
                        <asp:TextBox ID="txtCrdFeeStructPhoneNo" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>

                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtCrdFeeStructPhoneNo"
                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Phone No." ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="Dynamic" ControlToValidate="txtCrdFeeStructPhoneNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Phone No" ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 m-b-10">
                        <label>Main Attraction of the ITI:</label><%--<span style="color: red">*</span>--%>
                        <asp:TextBox ID="txtMainAttract" ForeColor="Black" runat="server" TextMode="MultiLine" CssClass="form-control" style="height:150px;"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator Display="Dynamic" CssClass="badge badge-danger" ID="RequiredFieldValidator16" runat="server"
                                        ErrorMessage="Enter Main Attraction" ControlToValidate="txtMainAttract" ValidationGroup="A"></asp:RequiredFieldValidator> --%>
                        <asp:RegularExpressionValidator
                            ID="RegularExpressionValidator16" runat="server" CssClass="badge badge-warning" ControlToValidate="txtMainAttract"
                            Display="Dynamic" ErrorMessage="Invalid Main Attraction. Only Alphabets, numbers,space,slash(/),comma(,),decimal(.),Colon(:),At the rate(@) are allowed." ValidationExpression="^[A-Za-z0-9,.:@\s\/]{1,2000}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>

                    <%-- <div class="col-lg-12 col-md-12 col-sm-12 col-12 m-b-10">
                        <label>Prospectus:</label><br />
                        <asp:Image ID="Image1" runat="server" Height="100" ImageUrl="~/assets/images/preview.png" Width="120" CssClass="img-fluid" onclick="image1_preview();" />
                        <asp:FileUpload ID="File_Upload" runat="server" onchange="previewFile(); ValidationFile1();" />
                        <asp:Label ID="lblFilename" CssClass="col-form-label" runat="server"></asp:Label><br />
                        <asp:Label ID="lblFile1Msg" runat="server" Text="(Only upload .pdf format. Max size upto 5 MB.)"></asp:Label>
                        <asp:Label ID="lblError" CssClass="badge badge-danger" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdPros" runat="server" />
                    
                    
                    </div>--%>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="margin-top: 2%; margin-bottom: 5%;">
                <asp:Button ID="btnSubmit" runat="server" Text="Update Profile" CssClass="btn btn-success" OnClick="btnSubmit_Click" OnClientClick="validationFile1(); return false;" ValidationGroup="A" />
            </div>
            <div class="modal cus-modal" id="myModal1" role="dialog">
                <div class="modal-dialog  modal-lg" style="margin-top: 1%;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row" style="width: 100%;">
                                <div style="display: inline-block; width: 70%; float: left; text-align: left">
                                    <h6 class="modal-title" style="font-weight: bold; padding-left: 15px;">Prospectus</h6>
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
