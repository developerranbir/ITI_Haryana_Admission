<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPvtITIFeePaid.aspx.cs" Inherits="HigherEducation.DHE.frmPvtITIFeePaid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ITI Fee Paid Module</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
   <%-- <link href="../assets/css/styleGlance.css" rel="stylesheet" />--%>
     <link href="../assets/css/styleResult.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>
    <style>
        #dvPvtITIFeePaid table th {
            background: #71738c;
            color: #fff;
        }
   .cus-top-section {
    display: flex;
    justify-content: center;
}

    .cus-top-section label {
        color: #492A7F;
        font-weight: 700;
        font-size: 16px;
    }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
        
         <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1"></div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Pvt. ITI Fee Paid</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            
           </div>    
        <div class="col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                <label>Name of ITI:</label>
                        <asp:TextBox ID="txtCollegeName" CssClass="form-control" disabled="true" runat="server"></asp:TextBox>
                    </div>
                 <div class="col-lg-4 col-md-4 col-sm-6 col-12">  
                  <label>Trade Name:</label>
                     
                        <asp:DropDownList ID="ddlCouseName" runat="server" CssClass="form-control"> </asp:DropDownList>
                      
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn" style="margin-top: 30px;">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                    </div>
                </div>

        </div>
        <div class="cus-grid-table">
            <div class="row">
                <div class="col-lg-3">
                                    <input type="text" id="myInput" onkeyup="myFunctions()" placeholder="Search Registration ID..." class="form-control" title="Type Registration ID" />
                                </div>
                <div class="col-lg-3">
                                    <input type="text" id="myInputName" onkeyup="myFunctionsName()" placeholder="Search Name.." class="form-control" title="Type Name" />
                                </div>
                <div id="dvPvtITIFeePaid" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display: none;">
                    <asp:GridView ID="GrdPvtITIFeePaid" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" Style="width: 100%; margin-top: 20px;">
                        <Columns>
                            <asp:TemplateField HeaderText="Registration ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegId" runat="server" Text='<%# Eval("registrationID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("applicant_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Father Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("applicant_father_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Date of Birth">
                                <ItemTemplate>
                                    <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("applicant_dob") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <%--<asp:TemplateField HeaderText="Trade Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblTradeName" runat="server" Text='<%# Eval("CourseName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                             <asp:TemplateField HeaderText="Trade Section Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("SectionName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Fee Paid">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkFeePaid" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                        <HeaderStyle />
                    </asp:GridView>

                </div>
                <div>
                    <asp:HiddenField ID="hdCollegeid" runat="server" />
                </div>
                   
                 <div id="dvSubmit" runat="server"  class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="margin-top: 2%; margin-bottom: 5%;" visible="false">
                      <div id="dvCaptcha" runat="server" style="padding: 0; margin:15px;">
                       
							<div class="row cus-captchalist">
								<div class="col-lg-3 col-md-3 col-sm-6 col-12 text-right">
									<asp:Label ID="lblCaptcha" CssClass="col-form-label" runat="server" Text="Captcha"></asp:Label><span id="spcaptcha" style="color: red">*</span>
								</div>
								<div class="col-lg-4 col-md-4 col-sm-6 col-12">
									<asp:TextBox ID="txtturing" runat="server" MaxLength="10" placeholder="Enter Captcha" CssClass="form-control"></asp:TextBox>
								</div>
								<div class="col-lg-1 col-md-1 col-sm-6 col-7 mobilecaptcha">
									<img id="imgCaptcha" runat="server" src="Turing.aspx" style="margin-top: 8px; width: 100px; height: 32px; margin-top: 5px;" alt="" />
								</div>
								<div class="col-lg-1 col-md-1 col-sm-6 col-2 login-captcha">
									<asp:ImageButton ID="imgRefreshCaptcha" ImageUrl="../assets/images/refresh.png" Style="width: 32px;" OnClientClick="Turing.aspx" AlternateText="No Image available" runat="server" />
								</div>
								<asp:RequiredFieldValidator Display="Dynamic" CssClass="badge badge-danger" ID="RequiredFieldValidator10" runat="server"
									ErrorMessage="Enter Captcha" ControlToValidate="txtturing" ValidationGroup="A"></asp:RequiredFieldValidator>
                               
							</div>
						</div>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" ValidationGroup="A" />
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
                            <img src="../assets/images/nic-logo.png" style="width: 100px;">
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
    <!-- End Footer -->
</body>
<script>
   

</script>
<script>
    // Smooth scroll for the navigation menu and links with .scrollto classes
    var scrolltoOffset = $('#header').outerHeight() - 17;
    $(document).on('click', '.nav-menu a, .mobile-nav a, .scrollto', function (e) {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            if (target.length) {
                e.preventDefault();

                var scrollto = target.offset().top - scrolltoOffset;

                if ($(this).attr("href") == '#header') {
                    scrollto = 0;
                }

                $('html, body').animate({
                    scrollTop: scrollto
                }, 1500, 'easeInOutExpo');

                if ($(this).parents('.nav-menu, .mobile-nav').length) {
                    $('.nav-menu .active, .mobile-nav .active').removeClass('active');
                    $(this).closest('li').addClass('active');
                }

                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                    $('.mobile-nav-overly').fadeOut();
                }
                return false;
            }
        }
    });

    // Activate smooth scroll on page load with hash links in the url
    $(document).ready(function () {
        if (window.location.hash) {
            var initial_nav = window.location.hash;
            if ($(initial_nav).length) {
                var scrollto = $(initial_nav).offset().top - scrolltoOffset;
                $('html, body').animate({
                    scrollTop: scrollto
                }, 1500, 'easeInOutExpo');
            }
        }
    });
    function myFunctions() {
        var input, filter, table, tr, td, i, txtValue;
        input = document.getElementById("myInput");
        filter = input.value.toUpperCase();
        table = document.getElementById("GrdPvtITIFeePaid");
        tr = table.getElementsByTagName("tr");
        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[0];
            if (td) {
                txtValue = td.textContent || td.innerText;
                if (txtValue.toUpperCase().trim().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }
    function myFunctionsName() {
        var input, filter, table, tr, td, i, txtValue;
        input = document.getElementById("myInputName");
        filter = input.value.toUpperCase();
        table = document.getElementById("GrdPvtITIFeePaid");
        tr = table.getElementsByTagName("tr");
        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[1];
            if (td) {
                txtValue = td.textContent || td.innerText;
                if (txtValue.toUpperCase().trim().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }
    // Mobile Navigation
    if ($('.nav-menu').length) {
        var $mobile_nav = $('.nav-menu').clone().prop({
            class: 'mobile-nav d-lg-none'
        });
        $('body').append($mobile_nav);
        $('body').prepend('<button type="button" class="mobile-nav-toggle d-lg-none"><i class="icofont-navigation-menu"></i></button>');
        $('body').append('<div class="mobile-nav-overly"></div>');

        $(document).on('click', '.mobile-nav-toggle', function (e) {
            $('body').toggleClass('mobile-nav-active');
            $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
            $('.mobile-nav-overly').toggle();
        });

        $(document).on('click', '.mobile-nav .drop-down > a', function (e) {
            e.preventDefault();
            $(this).next().slideToggle(300);
            $(this).parent().toggleClass('active');
        });

        $(document).click(function (e) {
            var container = $(".mobile-nav, .mobile-nav-toggle");
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                    $('.mobile-nav-overly').fadeOut();
                }
            }
        });
    } else if ($(".mobile-nav, .mobile-nav-toggle").length) {
        $(".mobile-nav, .mobile-nav-toggle").hide();
    }

    // Navigation active state on scroll
    var nav_sections = $('section');
    var main_nav = $('.nav-menu, #mobile-nav');

    $(window).on('scroll', function () {
        var cur_pos = $(this).scrollTop() + 200;

        nav_sections.each(function () {
            var top = $(this).offset().top,
                bottom = top + $(this).outerHeight();

            if (cur_pos >= top && cur_pos <= bottom) {
                if (cur_pos <= bottom) {
                    main_nav.find('li').removeClass('active');
                }
                main_nav.find('a[href="#' + $(this).attr('id') + '"]').parent('li').addClass('active');
            }
            if (cur_pos < 300) {
                $(".nav-menu ul:first li:first").addClass('active');
            }
        });
    });
</script>

</html>
