<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSectionMaster.aspx.cs" Inherits="HigherEducation.DHE.frmSectionMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
         <title>Add/Edit Section</title>
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
        function Search_Gridview(strKey) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=grdSection.ClientID %>");
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
                        <h4 class="heading">Add/Edit Section</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
                  <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                <div class="row cus-fee-top-section">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>College:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCollege"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                     <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Course:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCourse"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Section Name:</label><span style="color: red">*</span>
                        <asp:TextBox ID="txtSectionName" CssClass="form-control" runat="server" MaxLength="150"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSectionName"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Section Name" ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtSectionName"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Section Name" ValidationExpression="^[A-Za-z0-9,.()\s]{5,150}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>         
                         <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is Active for Under Graduate?</label>
                             <asp:RadioButtonList ID="rdbIsActive" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="ACTIVE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="INACTIVE"></asp:ListItem>
                             </asp:RadioButtonList>

                    </div>
                     <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Is Active for Post Graduate?</label>
                             <asp:RadioButtonList ID="rdbIsActivePG" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="ACTIVE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="INACTIVE"></asp:ListItem>
                             </asp:RadioButtonList>

                    </div>
                   
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="margin-top: 2%; margin-bottom: 5%;">
                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success"   ValidationGroup="A" OnClick="btnSubmit_Click"  />
               <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click"    />
            </div>
                     <div>
                           Search : <asp:TextBox ID="txtSearch" runat="server"  onkeyup="Search_Gridview(this)" Width="150px" ></asp:TextBox>
                      </div>
                </div>
                      <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-grid-table">
                          <div class="table-responsive">
                          <asp:GridView ID="grdSection" runat="server" AutoGenerateColumns="false" DataKeyNames="SectionId" PagerSettings-Mode="Numeric" PagerSettings-Position="TopAndBottom"
                               AllowPaging="true" PageSize="50" OnPageIndexChanging="grdSection_PageIndexChanging" OnRowCommand="grdSection_RowCommand" >
                              <Columns>
                                    <asp:TemplateField HeaderText="SectionId" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSectionId" runat="server" Text='<%# Eval("SectionId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Sr.No." >
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="CollegeId" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCollegeId" runat="server" Text='<%# Eval("CollegeId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="College Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("CollegeName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="CourseId" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCourseId" runat="server" Text='<%# Eval("CourseId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Course Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("CourseName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section Name" >
                    <ItemTemplate>
                        <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("SectionName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText="IsActive for UG" >
                    <ItemTemplate>
                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="IsActive for PG" >
                    <ItemTemplate>
                        <asp:Label ID="lblIsActivePG" runat="server" Text='<%# Eval("IsActivePG") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
      <asp:TemplateField HeaderText="Edit" >
                    <ItemTemplate>
                       <asp:Button ID="btnEdit" runat="server" CommandName="SEL" Text="Edit" CssClass="btn btn-sm btn-primary" />
                    </ItemTemplate>
                </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                               </div>
                      </div>
            </div>
              </div>
        <footer id="footer">
        <div class="container">
            <div class="credits">
                Designed by <a href="#">National Informatics Center, Haryana</a>
            </div>
        </div>
    </footer>
    <!-- End Footer -->
    </form>
</body>
</html>
