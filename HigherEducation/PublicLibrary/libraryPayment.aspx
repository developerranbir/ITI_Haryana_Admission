<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="libraryPayment.aspx.cs" Inherits="HigherEducation.PublicLibrary.libraryPayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

         
          action="https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction">
        <asp:HiddenField ID="hdnEncRequest" runat="server" Name="encRequest" />
        <asp:HiddenField ID="hdnAccessCode" runat="server" Name="access_code" />
 

 
        </div>
    </form>

       <script type="text/javascript">
           window.onload = function () {
               document.forms[0].submit();
           };
       </script>
</body>
</html>
