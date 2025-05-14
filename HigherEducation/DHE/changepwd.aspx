<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changepwd.aspx.cs" Inherits="HigherEducation.HigherEducations.changepwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


 <script language="javascript">
     function noBack() { window.history.forward() }
     noBack();
     window.onload = noBack;
     window.onpageshow = function (evt) { if (evt.persisted) noBack() }
     window.onunload = function () { void (0) }
		</script>
  <script type="text/javascript" language="Javascript">
      history.go(1); // disable the browser's back button
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Change password</title>
      <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/csschangepwd.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    
<!-- Include WinCloseScript.js file which contains functions that are called from Body onunload event-->
<script language="Javascript" type="text/javascript" src="<%=ResolveUrl("~/scripts/WinCloseScript.js")%>"></script>


    <script src="../assets/js/jquery-3.4.1.js" language="Javascript" type="text/javascript"></script>
	 <script  language="Javascript" type="text/javascript" src="../assets/js/jquery/jquery.min.js"></script>
	<script src="../assets/js/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
	 <script src="../assets/js/popperjs/popper.min.js" language="Javascript" type="text/javascript"></script>
    <script src="../assets/js/moment-with-locales.min.js" language="Javascript" type="text/javascript"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js" language="Javascript" type="text/javascript"></script>
	
	 <script src="../scripts/md5.js" language="Javascript" type="text/javascript"></script>
    <link href="../Content/Scripts/sweetalert.css" rel="stylesheet" />
    <script src="../Content/Scripts/sweetalert.min.js" type="text/javascript"></script>
		<script language="Javascript" type="text/javascript">
            function OnChangeConfirmPassword() {

                var pwd = document.getElementById('<%= txtnewpwd1.ClientID %>').value;
                 var Confpwd = document.getElementById('<%= txtnewpwd2.ClientID %>').value;
                if (pwd != "") {
                    if (pwd != Confpwd) {
                         document.getElementById('<%= txtnewpwd2.ClientID %>').value = "";
                         alert("Confirm Password did not matched with New Password");
                     }
                 }
             }
		    function md5auth(seed) {


		        var username = document.getElementById('txtuid').value;


		        //var rexp = /^\w+$/;
		        var rexp = "";
		        if (username.length == 0 || username.length > 50) {
		            alert("Please enter valid username");
		            document.getElementById('txtuid').focus();
		            return false;
		        }
		        if (username.search(rexp) == -1) {
		            alert("Please enter valid username");
		            document.getElementById('txtuid').focus();
		            return false;
		        }



		        var password = document.getElementById('txtoldpwd').value;


		        if (password.length < 6 || password.length > 15) {
		            alert("Please enter valid old password");
		            document.getElementById('txtoldpwd').focus();
		            return false;
		        }


		        //check for alphanumeric
		        var flag_num = 0;
		        var flag_alpha = 0;


		        for (i = 0; i < password.length; i++) {
		            if ((password.charAt(i) >= '0') && (password.charAt(i) <= '9')) {
		                flag_num = flag_num + 1;
		            }
		            else if (((password.charAt(i) >= 'A') && (password.charAt(i) <= 'Z')) || ((password.charAt(i) >= 'a') && (password.charAt(i) <= 'z'))) {
		                flag_alpha = flag_alpha + 1;
		            }

		        }

		        if (flag_num == 0) {
		            alert("Invalid old password");
		            document.getElementById('txtoldpwd').focus();
		            return (false);
		        }

		        if (flag_alpha == 0) {
		            alert("Invalid old password");
		            document.getElementById('txtoldpwd').focus();
		            return (false);
		        }


		        //check for alphanumeric

		        //it will allow alpha numeric and some special chars,but special chars NOT in the beginning of pwd
		        //var rexp1 =/^(?=[\w]{6,15})[\w$#_!@()]{6,15}$/;

		        //it will allow alpha numeric and some special chars,but special chars AT THE the beginning of pwd  ALSO
		        var rexp1 = /^(?=[\w$#_!@]{6,15})[\w$#_!@]{6,15}$/;



		        if (password.search(rexp1) == -1) {
		            //alert("Please enter valid old password");
		            document.forms[0].txtoldpwd.focus();
		            return false;
		        }


		        var hash = seed + hex_md5(password);
		        //alert(hash);
		        document.getElementById('txtoldpwd').value = hash;
		        //var hash = hex_md5(password); 
		        //document.getElementById('txtoldpwd').value=hash;




		        var password1 = document.getElementById('txtnewpwd1').value;

		        if (password1.length < 6 || password1.length > 15) {
		            alert("Please enter valid new password");
		            document.getElementById('txtnewpwd1').focus();
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
		            document.getElementById('txtnewpwd1').focus();
		            return (false);
		        }

		        if (flag_alpha1 == 0) {
		            alert("Invalid new password");
		            document.getElementById('txtnewpwd1').focus();
		            return (false);
		        }

		        if (flag_spchars1 == 0) {
		            alert("Invalid new password");
		            document.getElementById('txtnewpwd1').focus();
		            return (false);
		        }
		        //check for alphanumeric






		        if (password1.search(rexp1) == -1) {
		            //alert("Please enter valid new password");
		            document.forms[0].txtnewpwd1.focus();
		            return false;
		        }

		        //var hash1 = hex_md5(password1);
		        //document.getElementById('txtnewpwd1').value=hash1;

		        var hash1 = seed + hex_md5(password1);
		        document.getElementById('txtnewpwd1').value = hash1;



		        var password2 = document.getElementById('txtnewpwd2').value;

		        if (password2.length < 6 || password2.length > 15) {
		            alert("Please enter valid confirm password");
		            document.getElementById('txtnewpwd2').focus();
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
		            alert("Invalid new password");
		            document.getElementById('txtnewpwd2').focus();
		            return (false);
		        }

		        if (flag_alpha2 == 0) {
		            alert("Invalid new password");
		            document.getElementById('txtnewpwd2').focus();
		            return (false);
		        }
		        if (flag_spchars2 == 0) {
		            alert("Invalid new password");
		            document.getElementById('txtnewpwd2').focus();
		            return (false);
		        }

		        //check for alphanumeric









		        if (password2.search(rexp1) == -1) {
		            //alert("Please enter valid new confirm password");
		            document.forms[0].txtnewpwd2.focus();
		            return false;
		        }


		        //var hash2 = hex_md5(password2); 
		        //document.getElementById('txtnewpwd2').value=hash2;
		        var hash2 = seed + hex_md5(password2);
		        document.getElementById('txtnewpwd2').value = hash2;





		        return true;

		    }
        </script>
 

 

</head>
<body onunload="exitSession()"  style="margin-top: 0px; left: 0px; margin-left: 0px; top: 0px">
            
    <form id="form1" runat="server" autocomplete="off">
            <div class="container-fluid">
                 <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="Online Admission Portal"/>
            </div>
                
        <div class="container">
            <div class="cus-containerbg">
               
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 cus-left-section">
                    <h3>Change Password</h3>
                                       
                    <h6><span style="color:#E91E63;font-weight:800;">Note:&nbsp;</span>Password must be between 6 and 15 characters; must contain at least one alphabet letter, one numeric digit, and one special character ($,# or @)</h6>
                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="#492A7F" Text="Please change your password...."></asp:Label>
                    
                </div>
                 
                <div class="col-lg-6 col-md-6 col-sm-12 cus-right-section">
                 
                    <!-- form card change password -->
                    <div class="card card-outline-secondary">
                     
                        <div class="card-body">
                            <form class="form" role="form" autocomplete="off">
                                 <div class="form-group">
                                     <div class="row">
                                         <div class="col-lg-6 col-md-6 col-sm-12">
                                              <label for="inputPasswordOld">User ID</label>
                                      <input id="txtuid" maxlength="15" name="txtuid" type="text" runat="server" class="textbox form-control" disabled="disabled" size="15" />
                                         </div>
                                         <div class="col-lg-6 col-md-6 col-sm-12">
                                             <label>User Name</label> 
                                             <input id="lblunm" runat="server" type="text" runat="server" class="textbox form-control" disabled="disabled"/>                                            </div>
                                     </div>
                                   
                                </div>
                                <div class="form-group">
                                    <label>Old Password</label>
                                    <input name="txtoldpwd" id="txtoldpwd" runat="server" maxlength="50" type="password" placeholder="Enter Old Password"  autocomplete="off" class="textbox form-control" />
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtoldpwd"
                         CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Invalid password" ValidationExpression="^[A-Za-z0-9$#@$]{6,50}$"
                         Width="119px"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtoldpwd"
                         Display="Dynamic" CssClass="badge badge-danger" ErrorMessage="Enter old password"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="inputPasswordNew">New Password</label>
                                   <input name="txtnewpwd1" id="txtnewpwd1" runat="server" maxlength="50" type="password" placeholder="Enter New Password" autocomplete="off" class="textbox form-control" />
                                    <span class="form-text small text-muted">
                                            
                                        </span>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnewpwd1"
                         CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Invalid password" ValidationExpression="^[A-Za-z0-9$#@$]{6,50}$"
                         ></asp:RegularExpressionValidator>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnewpwd1"
                         Display="Dynamic" ErrorMessage="Enter new password" CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="inputPasswordNewVerify">Confirm New Password</label>
                                    <input name="txtnewpwd2" id="txtnewpwd2" runat="server" onchange="OnChangeConfirmPassword();" maxlength="50" type="password" placeholder="Confirm New Password" autocomplete="off" class="textbox form-control" />
                                    
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtnewpwd2"
                         CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Invalid password" ValidationExpression="^[A-Za-z0-9$#@$]{6,50}$"
                         ></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtnewpwd2"
                         Display="Dynamic" ErrorMessage="Enter confirm password"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group text-center cus-formbtn">
                                    <asp:Button ID="Button1" runat="server" Text="OK"  CssClass="btn btn-success btn-sm" OnClick="Button1_Click" />
                                     <asp:Button ID="btncancel" runat="server" CausesValidation="False" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btncancel_Click" />
                                </div>
                            </form>
                        </div>
                    </div>
                    <!-- /form card change password -->

   
                    </div>
            </div>
            </div>
        </div>
 </div>
    </form>
                 <!-- ======= Footer ======= -->
  <footer id="footer">
     <div class="container">
 
      <div class="credits">
      
        Designed by <a href="#">National Informatics Center, Haryana</a>
      </div>
    </div>
  </footer><!-- End Footer -->
</body>
</html>

