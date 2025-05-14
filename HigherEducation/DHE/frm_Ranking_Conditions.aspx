<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_Ranking_Conditions.aspx.cs" Inherits="HigherEducation.DHE.frm_Ranking_Conditions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ranking And Conditions</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <%--<link href="../assets/css/stylecancel.css" rel="stylesheet" />--%>
    <link href="../assets/css/styleResult.css" rel="stylesheet" />
    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet" />
    <style>
        #dvPaymentStatus table th {
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
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>

        </div>
        <div class="container-fluid" style="margin-bottom:100px;">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2">
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-8">
                        <h4 class="heading">Ranking And Conditions</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2 text-right back-btn" style="margin-top: 5px;">
                        <a href="#" onclick="redirect();" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="container-fluid" style="margin-top: 20px;">
                <div class="row">

                    <div class="col-lg-4 col-md-4 col-sm-4 col-12">
                    </div>
                    <div class="col-lg-2 col-md-2">
                        <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Select Counselling:</label><span style="color: red">*</span>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-12">
                        <asp:DropDownList ID="ddlcounselling" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcounselling_SelectedIndexChanged" AutoPostBack="true">
                            
                        </asp:DropDownList>

                    </div>

                    <div class=" col-12 text-center overflow-auto" style="margin-top: 20px;">

                        <%--gridview area--%>
                        <asp:GridView ID="gdvconditions" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvconditions_RowCommand"
                            CssClass="table table-sm table-borderless table-hover" OnRowDataBound="gdvconditions_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="cases" HeaderText="Case" />
                                <asp:BoundField DataField="conditions" HeaderText="Condition" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("IsApplied" )%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnapply" CssClass="btn btn-success btn-sm" Text="Apply" CommandName="cmdapply" CommandArgument='<%# Eval("id" )%>' />
                                        <asp:Button runat="server" ID="btnremove" CssClass="btn btn-danger btn-sm" Text="Remove" CommandName="cmdremove" CommandArgument='<%# Eval("id" )%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-5 col-md-5 col-sm-5 col-12">
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-12">
                        <asp:Button ID="btnGenRanking" runat="server" CssClass="btn btn-sm btn-success" Text="Genrate Ranking" OnClick="btnGenRanking_Click" Visible="false" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-12">
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-5 col-md-5 col-sm-5 col-12">
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-12">
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-12">
                    </div>
                </div>
            </div>
        </div>





        <%--</div>--%>
    </form>
    <!-- ======= Footer ======= -->
    <%--<footer id="footer">--%>
    <footer id="footer" style="position: fixed;bottom: 0;width: 100%;">
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
    function redirect() {
        //window.location.href = "https://itiharyanaadmissions.nic.in/Account/Login";
        window.history.go(-1);
        return false;
    }

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
