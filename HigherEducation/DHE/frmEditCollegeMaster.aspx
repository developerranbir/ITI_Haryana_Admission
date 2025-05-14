<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEditCollegeMaster.aspx.cs" Inherits="HigherEducation.DHE.frmEditCollegeMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit College</title>
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
    <link href="../assets/css/styleseatmatrix.css" rel="stylesheet" />
    <%-- Search by Grid --%>
    <script type="text/javascript">

        <%--function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=grdCollege.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        };--%>

        //$(document).on("keyup", "#txtITIName", function () {
        //    if (/[^a-zA-Z\s]/g.test(this.value)) {
        //        this.value = this.value.replace(/[^a-z\s]/g, '');
        //        swal("Warning!", "Please enter only alphabets", "warning")
        //        return false;
        //        // alert("Please enter only alphabets");
        //    }
        //});
        function Validation(obj) {

            var x = document.getElementById('<%=btnSubmit.ClientID%>').value
            

            var list = document.getElementById("rdbIsActive");
            var inputs = list.getElementsByTagName("input");
            var selected;
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked) {
                    selected = inputs[i];
                    break;
                }
            }

            var ActivePG = document.getElementById("rdbIsActivePG");
            var inputspg = ActivePG.getElementsByTagName("input");
            var selected;
            for (var i = 0; i < inputspg.length; i++) {
                if (inputspg[i].checked) {
                    selectedPG = inputspg[i];
                    break;
                }
            }
            if (selected.value == "ACTIVE" && selectedPG.value == "ACTIVE" && x == "Update") {

            }
            else if (x == "Update" && (selected.value === "INACTIVE" || selectedPG.value === "INACTIVE")) {
                if (confirm('If you deactivate the college then it would Deactivate all the courses, combinations, fees and seats details of this college. Do you want to continue?')) {

                } else {
                    return false;
                }
            }
            else if (x == "Save" && (selected.value === "INACTIVE" && selectedPG.value === "INACTIVE")) {
                alert("New college to be added should be active for at least Under Graduate or Post Graduate!");
                return false;
            }

            //if (selected.value != "ACTIVE" || selectedPG.value != "ACTIVE" && x !="Update") {

            //}
            //else if (x == "Update") {
            //    alert();
            //    if (confirm('If you deactivate the college then it would Deactivate all the courses, combinations, fees and seats details of this college. Do you want to continue?')) {
            //        // Save it!
            //        console.log('Thing was saved to the database.');
            //    } else {
            //        return false;
            //    }
            //}
            //else {
            //   alert("New college to be added should be active for at least Under Graduate or Post Graduate!");
            //   return false;
            //}
           

        }



    </script>
    <%-- Search by Grid --%>
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
                    <div class="col-lg-11 col-md-11 col-sm-11 col-11">
                        <h4 class="heading">Edit ITI Details</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
             <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                    <div class="row cus-fee-top-section">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-12 m-b-10">
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-12 m-b-10">
                            <label>Enter Institue ID:</label><span style="color: red">*</span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-12 m-b-10">
                            
                            <asp:TextBox ID="txtID" CssClass="form-control" runat="server" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtID"
                                CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Institue ID" ValidationGroup="A"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ControlToValidate="txtID"
                                CssClass="badge badge-danger" ErrorMessage="Invalid Institue Name" ValidationExpression="^[A-Za-z0-9,-.()\s]{5,500}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-12 col-12 top-section-btn text-center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" ValidationGroup="A" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12 col-12 m-b-10">
                        </div>
                    </div>
             </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                <div class="row cus-fee-top-section">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Institue Name:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtITIName" CssClass="form-control" runat="server" MaxLength="500"></asp:TextBox>
                        
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Institute Type:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlCollegeType" runat="server" CssClass="form-control"></asp:DropDownList>
                        

                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Institute Address:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtInstAddress" CssClass="form-control" runat="server"></asp:TextBox>
                        
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>District:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>
                        
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Education Mode:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlEduMode" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Co-Ed" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Only Girls Institute" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Only Boys Institute" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is ITI for Ex-Serviceman:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlExServicemen" runat="server" CssClass="form-control">
                             <asp:ListItem Text="--Please Select--" Value="00"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is ITI for Deaf & Dumb:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlDeafDumb" runat="server" CssClass="form-control">
                             <asp:ListItem Text="--Please Select--" Value="00"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                     <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is ITI for Public-Private Partnerships:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlPPP" runat="server" CssClass="form-control">
                            <asp:ListItem Text="--Please Select--" Value="00"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Email Address:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" MaxLength="50"></asp:TextBox>
    
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Phone/ Mobile Number:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtMobile" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Website:</label><span style="color: red"></span>
                        <asp:TextBox ID="txtWebsite" CssClass="form-control" runat="server"></asp:TextBox>
    
                    </div>
                    
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Principal Name:</label><span style="color: red"></span>
                        <asp:TextBox ID="txtPrinc" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Principal Phone/ Mobile Number:</label><span style="color: red"></span>
                        <asp:TextBox ID="txtPrincePhone" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Nodal Officer For Admission:</label><span style="color: red"></span>
                        <asp:TextBox ID="txtNodalOff" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Nodal Officer Phone/ Mobile Number:</label><span style="color: red"></span>
                        <asp:TextBox ID="txtNodalOffPhone" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Main Attraction of Institute:</label><span style="color: red"></span>
                        <asp:TextBox ID="txtAttraction" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>NCVT/SCVT Council:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlAffType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="NCVT" Value="NCVT"></asp:ListItem>
                            <asp:ListItem Text="SCVT" Value="SCVT"></asp:ListItem>
                            <asp:ListItem Text="NCVT/SCVT" Value="NCVT/SCVT"></asp:ListItem>

                        </asp:DropDownList>
                        
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>NCVT MIS Code (if any):</label><span style="color: red"></span>
                        <asp:TextBox ID="txtMISCode" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Institute Rating:</label><span style="color: red"></span>
                        <asp:TextBox ID="txtRating" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is Active:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="ACTIVE" Value="ACTIVE"></asp:ListItem>
                            <asp:ListItem Text="INACTIVE" Value="INACTIVE"></asp:ListItem>

                        </asp:DropDownList>
    
                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="margin-top: 2%; margin-bottom: 5%;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="btn btn-success" ValidationGroup="A" OnClientClick="return Validation(this);" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
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
                            <img src="/assets/images/nic-logo.png" style="width:100px;">
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
    <!-- End Footer -->
</body>
</html>
