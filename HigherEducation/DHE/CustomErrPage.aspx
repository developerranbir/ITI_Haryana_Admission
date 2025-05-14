<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomErrPage.aspx.cs" Inherits="HigherEducation.HigherEducations.CustomErrPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row d-none d-sm-block main-banner">
				<img src="../assets/images/banner.jpg" style="width: 100%" />
			</div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="display: grid;place-items:center;line-height: 40px;margin-top: 10%;">
           
            <asp:ImageButton id="ImageButton1" runat="server" ImageUrl="..\assets\Images\error1.jpg"></asp:ImageButton>
             <div class="row">
             <asp:label id="Label1"  runat="server"
				Font-Bold="True" ForeColor="Maroon" Font-Names="Arial" Height="24px">Invalid Page....</asp:label>
        </div>
             <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Click here to login</asp:LinkButton>
        </div>
    </form>
</body>
</html>
