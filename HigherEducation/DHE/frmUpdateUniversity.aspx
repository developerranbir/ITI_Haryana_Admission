<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUpdateUniversity.aspx.cs" Inherits="HigherEducation.DHE.frmUpdateUniversity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add/Update Associated University</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600" rel="stylesheet" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylecourse.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

       <style>
    .cus-fee-table-section table th
    {
            background: #223f73;
    color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
           <div class="container-fluid">
        <div class="row main-banner">
            <img src="../assets/images/banner.jpg" alt="Online Admission Portal" style="width: 100vw;" />
        </div>
    </div>
         <div class="container-fluid">
               <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center top-heading">
            <div class="row">
                <div class="col-lg-1"></div>
                <div class="col-lg-10 col-md-10 col-sm-11 col-11">
                    <h4>Add/Update Associated University</h4>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                    <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                </div>
            </div>
        </div>
              <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-choose-collge" id="dvCollege" style="display:none;" runat="server">
        <div class="row">
            <label class="cus-label col-lg-2 col-md-2 col-sm-6 col-12" id="lblcollegename">College Name</label>
            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                <div style="background:#fff;padding:0.4rem;border-radius:0.4rem;"><%=HttpContext.Current.Session["CollegeName"]%></div>
            </div>
            <label class="cus-label col-lg-2 col-md-2 col-sm-6 col-12" id="lbluniv">University Name</label>
            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
             <div style="background:#fff;padding:0.4rem;border-radius:0.4rem;">
                 <asp:Label ID="lblUniv" runat="server" Text=""></asp:Label></div>
            </div>
      
        </div>
                  <div class="row">
                               <label class="cus-label col-lg-2 col-md-2 col-sm-6 col-12" id="lblAssuniv" style="margin-top:1rem;">Associated University<span style="color: red;"><strong> (Select Associated University which eligibility rules are applicable) </strong></span></label>
            <div  class="col-lg-4 col-md-4 col-sm-6 col-12" style="margin-top:1rem;margin-bottom:2rem;">
                <asp:DropDownList ID="ddlAssociatedUniv" runat="server" CssClass="form-control" ></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" InitialValue="0" Display="Dynamic" Font-Bold="true" CssClass="badge badge-danger"
                                ErrorMessage="Select Associated University" ControlToValidate="ddlAssociatedUniv" ValidationGroup="a"></asp:RequiredFieldValidator>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center m-b-10">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="a" CssClass="btn btn-primary" />
                 <asp:Label ID="lblMsgCollege" runat="server" Text="" Font-Bold="true"></asp:Label>
            </div>
                  </div>
    </div>
              <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-choose-collge" id="dvState"  style="display:none;" runat="server">
                  <div  class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive cus-fee-table-section">
                      <asp:GridView ID="grdCollege" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdCollege_RowDataBound" CssClass="table table-hover table-striped table-bordered">
                      <Columns>
                               <asp:TemplateField HeaderText="CollegeId" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCollegeId" runat="server" Text='<%# Eval("collegeid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                           <asp:TemplateField HeaderText = "Sr No." >
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>
                               <asp:TemplateField HeaderText="College Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("collegename") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                               <asp:TemplateField HeaderText="University Name">
                    <ItemTemplate>
                        <asp:Label ID="lblUnivName" runat="server" Text='<%# Eval("univeristyname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Associated University(Select Associated University which eligibility rules are applicable)">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlAssUniv" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                      </Columns>
                  </asp:GridView>
                  </div>
                  
                   <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center m-b-10">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"/>
                       <asp:Label ID="lblMsgState" runat="server" Text="" Font-Bold="true"></asp:Label>
            </div>
                  </div>
             </div>
    </form>
</body>
</html>
