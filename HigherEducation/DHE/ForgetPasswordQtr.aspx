<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPasswordQtr.aspx.cs" Inherits="HigherEducation.HigherEducations.ForgetPasswordQtr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetExpires(DateTime.Now);
    Response.Buffer = true;

    Response.Expires = -1;
    Response.CacheControl = "no-cache";
    
 
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Forgot Password</title>
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/forgot-style-page.css" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <%--<script language="Javascript" type="text/javascript" src="js/jquery.js"></script>--%>
     <link href="../assets/css/sweetalert.css" rel="stylesheet" />
      <script language="Javascript" type="text/javascript" src="../scripts/sweetalert.min.js"></script>
   
    <script language="Javascript" type="text/javascript" src="../assets/js/jquery/jquery.min.js"></script>
    <script language="Javascript" type="text/javascript" src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script language="Javascript" type="text/javascript" src="../scripts/md5.js"></script>
    <script language="Javascript" type="text/javascript">
      

        history.go(1); // disable the browser's back button

        function md5auth(seed) {
            //alert("in1");

            //it will allow alpha numeric and some special chars,but special chars AT THE the beginning of pwd  ALSO
            var rexp1 = /^(?=[\w$#_!@]{6,15})[\w$#_!@]{6,15}$/;

            var password1 = document.getElementById('txtNewPassword').value;

            if (password1.length < 6 || password1.length > 15) {
                alert("Please enter valid new password");
                document.getElementById('txtNewPassword').focus();
                return false;
            }
            //check for alphanumeric
            var flag_num1 = 0;
            var flag_alpha1 = 0;
            var flag_spchars1 = 0;

            for (i = 0; i < password1.length; i++) {
                if ((password1.charAt(i) >= '0') && (password1.charAt(i) <= '9')) {
                    flag_num1 = flag_num1 + 1;
                }
                else if (((password1.charAt(i) >= 'A') && (password1.charAt(i) <= 'Z')) || ((password1.charAt(i) >= 'a') && (password1.charAt(i) <= 'z'))) {
                    flag_alpha1 = flag_alpha1 + 1;
                }

                else if ((password1.charAt(i) == '$') || (password1.charAt(i) == '#') || (password1.charAt(i) == '@')) {
                    flag_spchars1 = flag_spchars1 + 1;
                }

            }

            if (flag_num1 == 0) {
                alert("Invalid new password");
                document.getElementById('txtNewPassword').focus();
                return false;
            }

            if (flag_alpha1 == 0) {
                alert("Invalid new password");
                document.getElementById('txtNewPassword').focus();
                return false;
            }

            if (flag_spchars1 == 0) {
                alert("Invalid new password");
                document.getElementById('txtNewPassword').focus();
                return false;
            }
            //check for alphanumeric

            if (password1.search(rexp1) == -1) {
                alert("Please enter valid new password");
                //document.forms[0].txtnewpwd1.focus();
                document.getElementById('txtNewPassword').focus();
                return false;
            }

            var password2 = document.getElementById('txtCNewPassword').value;

            if (password2.length < 6 || password2.length > 15) {
                alert("Please enter valid new confirm password");
                document.getElementById('txtCNewPassword').focus();
                return false;
            }

            //check for alphanumeric
            var flag_num2 = 0;
            var flag_alpha2 = 0;
            var flag_spchars2 = 0;


            for (i = 0; i < password2.length; i++) {
                if ((password2.charAt(i) >= '0') && (password2.charAt(i) <= '9')) {
                    flag_num2 = flag_num2 + 1;
                }
                else if (((password2.charAt(i) >= 'A') && (password2.charAt(i) <= 'Z')) || ((password2.charAt(i) >= 'a') && (password2.charAt(i) <= 'z'))) {
                    flag_alpha2 = flag_alpha2 + 1;
                }
                else if ((password2.charAt(i) == '$') || (password2.charAt(i) == '#') || (password2.charAt(i) == '@')) {
                    flag_spchars2 = flag_spchars2 + 1;
                }
            }

            if (flag_num2 == 0) {
                alert("Invalid new confirm password");
                document.getElementById('txtCNewPassword').focus();
                return false;
            }

            if (flag_alpha2 == 0) {
                alert("Invalid new confirm password");
                document.getElementById('txtCNewPassword').focus();
                return false;
            }
            if (flag_spchars2 == 0) {
                alert("Invalid new confirm password");
                document.getElementById('txtCNewPassword').focus();
                return false;
            }

            //check for alphanumeric

            if (password2.search(rexp1) == -1) {
                alert("Please enter valid new confirm password");
                //document.forms[0].txtnewpwd2.focus();
                document.getElementById('txtCNewPassword').focus();
                return false;
            }

            //alert("password1=" + password1);
            //alert("password2=" + password2);
            if (password1 != "" && password2 != "") {
                var hash1 = seed + hex_md5(password1);
                document.getElementById('txtNewPassword').value = hash1;

                var hash2 = seed + hex_md5(password2);
                document.getElementById('txtCNewPassword').value = hash2;
            }
            //alert("hash1=" + hash1);
            //alert("hash2=" + hash2);
            return true;
        }
       
    </script>
</head>
<body style="background: #F8F9FA;">
    <form id="form1" runat="server" DefaultButton="btnSubmit">
    <div class="container-fluid">
        <div class="row main-banner" >   
                                 <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
                </div>
     
              <%--  <div class="row hidden-lg hidden-md hidden-sm">
                      <img src="assets/images/VCMeet-mobile.png" style="width:100%"/>
                </div>--%>
           
  </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-right back-btn" style="margin-top: 5px;">
					<asp:Button  id="btnBack"  runat="server" OnClick="btnBack_Click" Text="Login"  class="btn btn-info"></asp:Button>
				</div>
        <div class="d-flex justify-content-center">
            <div class="cus-login-div login-box">
                   <div class="title-box">
                            <h2>Forgot Password ?</h2>
                                               </div>
                <div class="col-lg-12 cus-sub-heading">
                     <span>Enter details to reset password</span>
                    </div>
                
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 cus-inputs">
                  <%--  <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 cus-input-labels">
                        <asp:Label ID="lblUserId" Text="UserId" runat="server"></asp:Label>
                    </div>--%>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 cus-input-values">
                        <asp:TextBox ID="txtUserId" placeholder="Enter your Registration ID" runat="server" MaxLength="50" class="form-control" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserId"
                            ErrorMessage="Please enter Registration Id" SetFocusOnError="True" class="badge badge-danger"
                            ValidationGroup="SendLink" Display="Dynamic" ForeColor=""></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUseralphanum" runat="server" ControlToValidate="txtUserId"
                            ValidationExpression="^[A-Za-z0-9@.]{5,50}$" ErrorMessage="Please enter valid Registration Id"
                            Display="Dynamic" class="badge badge-danger" ValidationGroup="SendLink" 
                            ForeColor="" ></asp:RegularExpressionValidator>
                    </div>
                   
                </div>
              
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 cus-inputs">
                  
                     <div class="col-lg-12 col-md-12 col-sm-12 col-12" id="dvNote" runat="server">
                         <h6 style="font-size:12px;"><span style="color:#E91E63;font-weight:800;">Note:&nbsp;</span>New Password will be sent to your registered Email Id/Mobile No.</h6>
                    </div>
                    <div id="dvCaptcha" runat="server" class="col-lg-12 col-md-12 col-sm-12" style="padding: 0;">
							<div class="row cus-captchalist">
								<div class="col-lg-2 col-md-2 col-sm-6 col-3" style="margin-top: 0.3rem;">
									<asp:Label ID="lblCaptcha" CssClass="col-form-label" runat="server" Text="Captcha" ></asp:Label><span id="spcaptcha" style="color: red">*</span>
								</div>
								<div class="col-lg-5 col-md-5 col-sm-6 col-9" >
									<asp:TextBox ID="txtturing" runat="server" MaxLength="10" placeholder="Enter Captcha" CssClass="form-control"></asp:TextBox>
								</div>
								<div class="col-lg-3 col-md-3 col-sm-6 col-8 mobilecaptcha">
									<img id="imgCaptcha" runat="server" src="Turing.aspx" style="margin-top: 8px; width: 100px; height: 32px; margin-top: 5px;" alt="" />
								</div>
								<div class="col-lg-2 col-md-2 col-sm-6 col-4 login-captcha">
									<asp:ImageButton ID="imgRefreshCaptcha" ImageUrl="../assets/images/refresh.png" Style="width: 16px;margin-top: 12px;" OnClientClick="Turnig.aspx" AlternateText="No Image available" runat="server" />
								</div>
                             <div class="col-lg-12 col-md-12 col-sm-12" style="margin-top:5px;">
								<asp:RequiredFieldValidator Display="Dynamic" CssClass="badge badge-danger" ID="RequiredFieldValidator10" runat="server"
									ErrorMessage="Enter Captcha" ControlToValidate="txtturing" ValidationGroup="SendLink"></asp:RequiredFieldValidator>
                                 </div>
							</div>

						</div>

                <div class="row"  style="margin-bottom:5px;margin-top:5px;padding:0;">
                    <asp:Label ID="lblError" class="badge badge-danger" runat="server" Visible="true" />
                    <asp:Label ID="lblSMsg" class="badge badge-success" runat="server" Visible="true" />
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center cus-bttns-div" style="margin-top:15px;">
                    <asp:Button ID="btnSubmit" Text="Reset Password" class="btn btn-danger btn-md" runat="server"
                        OnClick="btnSubmit_Click" ValidationGroup="SendLink" />
                    <%--<asp:Button ID="btnResetPassword" Text="Reset Password" class="btn btn-danger btn-md" runat="server"
                        ValidationGroup="ResetP" OnClick="btnResetPassword_Click" />--%>
                </div>
            </div>
            </div>
            </div>
       <%-- </div>
    </div>--%>
    </form>
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

