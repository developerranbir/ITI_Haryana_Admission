<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmlogin.aspx.cs" Inherits="HigherEducation.HigherEducation.frmlogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Skill Development Login</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/stylehelogin.css" rel="stylesheet" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="~/assets/css/theme.css" rel="stylesheet" />
    <%-- <link href="../assets/css/font-awesome.min.css" rel="stylesheet" />--%>
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/md5.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>
    <script src="../assets/js/jquery.cookie.js"></script>

    <script type="text/javascript">
        history.go(1); // disable the browser's back button

        function md5auth(seed)
        {

            debugger;
            if ((document.getElementById('txtpassword').value == '') || (document.getElementById('txtUserId').value == ''))
            {
                alert("Please enter UserId and Password");
                return -1;
            }
            var password = document.getElementById('txtpassword').value;

            if (password.length = 0 || password.length < 6 || password.length > 15)
            {
                document.getElementById('txtpassword').focus();
                return -1;
            }

            var rexp1 = /^(?=[\w$#@]{6,15})[\w$#@]{6,15}$/;

            if (password.search(rexp1) == -1)
            {
                alert("Please enter valid UserId or password");
                //document.forms[0].txtpassword.focus();
                return -1;
            }
            //alert(seed);
            var hash = hex_md5(seed + hex_md5(password));
            document.getElementById('txtpassword').value = hash;
            var username = document.getElementById('txtUserId').value;

            //var rexp = /^\w+$/;
            var rexp = "^[a-zA-Z0-9@.]+$";
            if (username.length < 5 || username.length > 50)
            {
                alert("Please enter valid UserId or password");
                document.getElementById('txtUserId').focus();
                return -1;
            }

            if (username.search(rexp) == -1)
            {
                alert("Please enter valid UserId or password");
                //document.getElementById('txtUserId').focus();
                return -1;
            }
            return true;
        }

        function capLock(e)
        {

            var msg1, msg2;
            kc = e.keyCode ? e.keyCode : e.which;
            sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);
            var isCtrlPressed = e.ctrlKey;
            //alert(isCtrlPressed);
            //alert(sk);

            //if ((isCtrlPressed) && (kc==86))
            //{
            //msg1='';
            //}

            //else
            //{	
            if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
            {
                //alert(sk);
                //alert(kc);

                if ((isCtrlPressed))
                {
                    msg1 = '';
                }
                else
                {
                    msg1 = 'Caps Lock';
                }
            }
            else
            {
                msg1 = '';
            }
            //}
            if ((e.keyCode == 144) && (kc >= 48 && kc <= 57))
            {
                if (msg1 == "")
                {
                    msg2 = 'Num Lock';
                }
                else
                {
                    msg2 = ' and Num Lock';
                }
                //alert('dd');
            }
            else
            {

                msg2 = '';
            }



            if ((msg1 == '') && (msg2 == '')) { document.getElementById('divMayus').style.display = 'none'; }
            else { document.getElementById('divMayus').style.display = 'block'; }


            document.getElementById('<%=Label1.ClientID%>').innerHTML = msg1 + msg2 + ' is on';
        }


    </script>
    <style>
	</style>
</head>
<body class="homepage" style="margin-top: 0px; left: 0px; margin-left: 0px; top: 0px">
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 cus-maintop-banner d-none d-sm-block">
        <div id="exchanger" class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-right selectbox" style="">


            <a href="#topbar" class="skip-content scroll" title="skip to main content">Skip to Main Content</a>
            <a href="#!" class="button-toggle-remove" title="Standard">A</a>
            <a href="#!" class="button-toggle-highcontrast" title="Black/Yellow">A</a>
            <a href="#" id="incfont" class="increase" title="Increased Text">A+</a>
            <a href="#" id="decfont" class="decrease" title="Decreased text">A-</a>
            <a href="#" id="decfont" class="resetMe" title="Reset Text">A</a>

        </div>

    </div>
    <form id="form1" runat="server" defaultbutton="btnlogin">
        <div class="container-fluid full-bg h-100">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" />
            </div>
            <%-- <div class="row d-sm-none d-md-none d-lg-none d-xl-none">
				<img src="assets/images/VCMeet-mobile.png" style="width: 100%" />
			</div>--%>

            <div class="container-fluid block">
                <div class="row" id="topbar">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-topbar-label text-right">
                        <!--  <a  class="btn btn-info" id="btnHome" href="~/index.html"><i class="fa fa-home"></i>&nbsp;<span class="english" style="display:none;">Home</span><span class="hindi">होम</span></a> -->
                        <a class="btn btn-info" id="btnHome" href="https://admissions.itiharyana.gov.in/"><i class="fa fa-home"></i>&nbsp;Home</a>

                    </div>
                </div>
                <div class="row top-login">
                    <div class="col-lg-5 col-md-8 col-sm-12 col-12 login-box mb-30">
                        <div class="title-box">
                            <h2>Login Here :1</h2>
                        </div>
                        <div class="row">
                            <asp:TextBox ID="txtUserId" runat="server" placeholder="Enter Username" CssClass="form-control inpt-sm"></asp:TextBox>
                        </div>
                        <div class="row">
                            <asp:TextBox ID="txtpassword" runat="server" type="password" placeholder="Enter Password" CssClass="form-control inpt-sm" onkeypress="capLock(event)" onkeydown="capLock(event)"></asp:TextBox>
                            <div id="divMayus" style="display: none">
                                <asp:Label ID="Label1" runat="server" Text="Caps Lock is On.!" CssClass=" col-form-label badge badge-danger"></asp:Label>

                            </div>
                            <asp:RegularExpressionValidator ID="revUseralphanum" runat="server" ControlToValidate="txtUserId"
                                ValidationExpression="[aA-zZ0-9@.]*" ErrorMessage="Either User ID or password is incorrect.."
                                Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revpassword" runat="server" ControlToValidate="txtpassword"
                                ValidationExpression="[aA-zZ0-9$#@]*" ErrorMessage="Either User ID or password is incorrect.."
                                Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-lg-12 cus-forgot">
                            <asp:Label ID="label3" runat="server" CssClass=" col-form-label badge badge-danger" Visible="False"></asp:Label>
                            <a target="_blank" href="ForgetPassword.aspx">Forgot Password?</a>
                        </div>

                        <div id="dvCaptcha" runat="server" class="col-lg-12 col-md-12 col-sm-12" style="padding: 0; display: none;">
                            <div class="row cus-captchalist">
                                <div class="col-lg-3 col-md-3 col-sm-6 col-12">
                                    <asp:Label ID="lblCaptcha" CssClass="col-form-label" runat="server" Text="Captcha"></asp:Label><span id="spcaptcha" style="color: red">*</span>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                                    <asp:TextBox ID="txtturing" runat="server" MaxLength="10" placeholder="Enter Captcha" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-6 col-7 mobilecaptcha">
                                    <img id="imgCaptcha" runat="server" src="Turing.aspx" style="margin-top: 8px; width: 100px; height: 32px; margin-top: 5px;" alt="" />
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6 col-2 login-captcha">
                                    <asp:ImageButton ID="imgRefreshCaptcha" ImageUrl="../assets/images/refresh.png" Style="width: 32px;" OnClientClick="Turnig.aspx" AlternateText="No Image available" runat="server" />
                                </div>
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="badge badge-danger" ID="RequiredFieldValidator10" runat="server"
                                    ErrorMessage="Enter Captcha" ControlToValidate="txtturing" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="submot-row" style="display:none;">
                            <div class="btn btn-md btn-success">
                                <i class="fas fa-paper-plane"></i>
                                <asp:Button ID="btnlogin" runat="server" CssClass="movedown" OnClick="btnlogin_Click" Text="Login" />
                            </div>
                        </div>
                        <div class="submot-row">
                            <div class="">
                                
                                <asp:LinkButton ID="btnlogin1" runat="server" CssClass="btn btn-md btn-success movedown" OnClick="btnlogin_Click"  >
                                    <i class="fas fa-paper-plane"></i> Login
                                </asp:LinkButton>
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

<script>
    //Text Increase and Decrease jquery start
    jQuery(document).ready(function ()
    {
        var all_tags = { "h1": '50', "h2": '24', "h3": '20', "h4": '14', "h5": '24', "h6": '12', "p": '14', "li": '18px' };
        jQuery.each(all_tags, function (i, val)
        {
            jQuery(i).css('font-size', val + 'px');
        });
        checkBesicSize();
        jQuery(".resetMe").click(function ()
        {
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');

            });

        });
        jQuery(".increase").click(function ()
        {
            jQuery.each(all_tags, function (i)
            {
                changeFontSize(i, 2);
            });
            return false;
        });
        jQuery(".decrease").click(function ()
        {
            jQuery.each(all_tags, function (i)
            {
                changeFontSize(i, -2);
            });
            return false;
        });

        function changeFontSize(tag, size)
        {
            var currentSize = jQuery(tag).css('font-size');
            var currentSize = parseFloat(currentSize) + parseFloat(size);
            jQuery(tag).css('font-size', currentSize);
        }

    });
    jQuery(window).resize(function ()
    {
        checkBesicSize();
    });
    function checkBesicSize()
    {
        var width = jQuery(window).width();
        if (width > 1290)
        {
            var all_tags = { "h1": '50', "h2": '22', "h3": '20', "h4": '14', "h5": '24', "h6": '12', "p": '14', "li": '20px' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 1290)
        {
            var all_tags = { "h1": '50', "h2": '22', "h3": '20', "h4": '14', "h5": '24', "h6": '12', "p": '14', "li": '20px' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 1100)
        {
            var all_tags = { "h1": '40', "h2": '22', "h3": '20', "h4": '14', "h5": '24', "h6": '12', "p": '14', "li": '20px' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 1024)
        {
            var all_tags = { "h1": '40', "h2": '22', "h3": '20', "h4": '14', "h5": '24', "h6": '12', "p": '14', "li": '20px' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 980)
        {
            var all_tags = { "h1": '36', "h2": '22', "h3": '18', "h4": '12', "h5": '24', "h6": '10', "p": '12', "li": '20px' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 800)
        {
            var all_tags = { "h1": '36', "h2": '22', "h3": '18', "h4": '12', "h5": '20', "h6": '10', "p": '12', "li": '18px' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 768)
        {
            var all_tags = { "h1": '36', "h2": '22', "h3": '18', "h4": '12', "h5": '20', "h6": '10', "p": '12', "li": '18px' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 640)
        {
            var all_tags = { "h1": '36', "h2": '22', "h3": '16', "h4": '12', "h5": '20', "h6": '10', "p": '12' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 480)
        {
            var all_tags = { "h1": '36', "h2": '22', "h3": '16', "h4": '12', "h5": '20', "h6": '10', "p": '12' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 400)
        {
            var all_tags = { "h1": '36', "h2": '22', "h3": '16', "h4": '12', "h5": '20', "h6": '10', "p": '12' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
        if (width < 360)
        {
            var all_tags = { "h1": '36', "h2": '22', "h3": '16', "h4": '12', "h5": '20', "h6": '10', "p": '12' };
            jQuery.each(all_tags, function (i, val)
            {
                jQuery(i).css('font-size', val + 'px');
            });
        }
    }
    //Text Increase and Decrease jquery end
    //Css Color Change jquery start
    // color-changing-script
    // Check (onLoad) if the cookie is there and set the class if it is
    if (jQuery.cookie('highcontrast') == "yes")
    {
        jQuery("body").addClass("highcontrast");
    }
    // When the element is clicked
    jQuery("a.button-toggle-highcontrast").click(function ()
    {
        if (jQuery.cookie('highcontrast') == "undefined" || jQuery.cookie('highcontrast') == "no")
        {
            jQuery.cookie('highcontrast', 'yes', {
                expires: 7,
                path: '/'
            });
            jQuery("body").addClass("highcontrast");
        } else
        {
            jQuery.cookie('highcontrast', 'yes', {
                expires: 7,
                path: '/'
            });
            jQuery("body").addClass("highcontrast");
        }
    });
    jQuery("a.button-toggle-remove").click(function ()
    {
        jQuery('body').removeClass('highcontrast');
        if (jQuery.cookie('highcontrast') == "yes")
        {
            jQuery.cookie("highcontrast", null, {
                path: '/'
            });
        }
    });
    $(document).ready(function ()
    {
        let scroll_link = $('.scroll');

        //smooth scrolling -----------------------
        scroll_link.click(function (e)
        {
            e.preventDefault();
            let url = $('body').find($(this).attr('href')).offset().top;
            $('html, body').animate({
                scrollTop: url
            }, 1000);
            $(this).parent().addClass('active');
            $(this).parent().siblings().removeClass('active');
            return false;
        });
    });
</script>
