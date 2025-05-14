<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCourseMaster.aspx.cs" Inherits="HigherEducation.DHE.frmCourseMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add/Edit College</title>
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

        <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylesearch.css" rel="stylesheet" />
    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <script src="../assets/dataTable/dataTables/jquery-3.5.1.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/dataTable/dataTables/jquery.dataTables.min.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
   
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/stylereport.css" rel="stylesheet" />
    <link href="../assets/dataTable/dataTables/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../assets/dataTable/dataTables/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../assets/dataTable/dataTables/buttons.dataTables.min.css" rel="stylesheet" />
    <script src="../assets/dataTable/dataTables/dataTables.buttons.min.js"></script>
    <script src="../assets/dataTable/dataTables/buttons.html5.min.js"></script>
    <script src="../assets/dataTable/dataTables/jszip.min.js"></script>
    <script src="../assets/dataTable/dataTables/pdfmake.min.js"></script>
    <script src="../assets/dataTable/dataTables/vfs_fonts.js"></script>
    <script src="../assets/dataTable/dataTables/buttons.print.min.js"></script>



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
                        <h4 class="heading">Add New Course</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>

            <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">

                <div class="row cus-fee-top-section">
                    <%--   <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Institue Name:</label>
                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Institue" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>--%>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Course Name:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtCourseName" CssClass="form-control" runat="server" MaxLength="500"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCourseName"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Course Name" ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtCourseName"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Course Name" ValidationExpression="^[A-Za-z0-9,-.()\s]{5,500}$" ValidationGroup="A"></asp:RegularExpressionValidator>

                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Duration </label>
                        <asp:DropDownList ID="ddlDuration" runat="server" CssClass="form-control">

                            <asp:ListItem Text="Select Duration" Value="0"></asp:ListItem>
                            <asp:ListItem Text="6 Months" Value="1"></asp:ListItem>
                            <asp:ListItem Text="1 Yrs" Value="2"></asp:ListItem>
                            <asp:ListItem Text="2 Yr" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDuration"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Duration" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>


                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Trade Type </label>
                        <asp:DropDownList ID="ddltradetype" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Trade Type" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Engg." Value="1"></asp:ListItem>
                            <asp:ListItem Text="Non-Engg." Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddltradetype"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Trade Type" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>



                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Edu Qualification ITI </label>
                        <asp:DropDownList ID="ddlEduQualificationITI" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Edu Qualification" Value="0"></asp:ListItem>
                            <asp:ListItem Text="8th Pass" Value="1"></asp:ListItem>
                            <asp:ListItem Text="10th Pass" Value="2"></asp:ListItem>
                            <asp:ListItem Text="10th with Math And SciencePass" Value="3"></asp:ListItem>
                            <asp:ListItem Text="12th Pass" Value="4"></asp:ListItem>
                            <asp:ListItem Text="12th with Math And Science Pass" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlEduQualificationITI"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please select  Edu Qualification Required" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Unit Size </label>
                        <asp:DropDownList ID="ddlUnitSize" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select Unit Size" Value="0"></asp:ListItem>
                            <asp:ListItem Text="16" Value="1"></asp:ListItem>
                            <asp:ListItem Text="20" Value="2"></asp:ListItem>
                            <asp:ListItem Text="24" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlUnitSize"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please select UnitSize" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                    <%-- <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is Dual </label>
                        <asp:DropDownList ID="ddlIsDual" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select IsDual" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlIsDual"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please select Dual mode" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>--%>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is Women </label>
                        <asp:DropDownList ID="ddlIsWomen" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select IsWomen" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIsWomen"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please select Women mode" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is Steno </label>
                        <asp:DropDownList ID="ddlIsSteno" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select IsSteno" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlIsSteno"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please select Steno mode" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center">
                            <h3>PH Suitable Categories</h3>
                            <br />
                        </div>
                        <asp:CheckBoxList ID="chkdisability" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="margin-top: 2%; margin-bottom: 5%;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success" ValidationGroup="A" OnClientClick="return Validation(this);" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                    </div>
                </div>


            </div>
          <div class="col-lg-12 col-md-12 col-sm-12 col-12 mt-5 ">
      <h4 class="heading text-center">Added  Course</h4></div>
<div class="  mt-2">
    <asp:GridView ID="GdvCourses" runat="server" AutoGenerateColumns="false" CssClass="table table-sm  table-borderless ">
        <Columns>
            <asp:BoundField DataField="name" HeaderText="Course Name" />
            <asp:BoundField DataField="duration" HeaderText="Duration" />
            <asp:BoundField DataField="tradetype" HeaderText="Trade Type" />
            <asp:BoundField DataField="EduQualificationITI" HeaderText="Edu Qualification" />
            <asp:BoundField DataField="UnitSize" HeaderText="Unit Size" />
            <asp:BoundField DataField="isdual" HeaderText="Is Dual" />
            <asp:BoundField DataField="iswomen" HeaderText="Is Women" />
            <asp:BoundField DataField="issteno" HeaderText="Is Steno" />

            <asp:BoundField DataField="course_uuid_status" HeaderText="Course status" />
        </Columns>
    </asp:GridView>
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
    <script>
        $(function () {
            $("#GdvCourses").DataTable({
                "responsive": true, "lengthChange": false, "autoWidth": false,
                "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
            }).buttons().container().appendTo('#GdvCourses_wrapper .col-md-6:eq(0)');

        });
    </script>

</body>
</html>



