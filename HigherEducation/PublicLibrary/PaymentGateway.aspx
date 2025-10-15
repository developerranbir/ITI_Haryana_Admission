<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentGateway.aspx.cs" Inherits="HigherEducation.PublicLibrary.PaymentGateway" %>

<!DOCTYPE html>
<html>
<head>
    <title>Redirecting to Payment Gateway</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 50px;">
            <h3>Redirecting to Payment Gateway...</h3>
            <p>Please wait while we redirect you to the secure payment page.</p>
        </div>
    </form>
    
    <script type="text/javascript">
        window.onload = function () {
            // Auto-submit form to CCAvenue
            document.forms["cca_redirect_form"].submit();
        }
    </script>

    <form method="post" name="cca_redirect_form" action="https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction">
        <input type="hidden" name="encRequest" value="<%= Session["strEncRequest"] %>" />
        <input type="hidden" name="access_code" value="<%= Session["strAccessCode"] %>" />
    </form>
</body>
</html>
