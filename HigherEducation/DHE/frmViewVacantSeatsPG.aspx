<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmViewVacantSeatsPG.aspx.cs" Inherits="HigherEducation.HigherEducations.frmViewVacantSeatsPG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Vacant Seats</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylesearch.css" rel="stylesheet" />
    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet" />
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>

    <script>
        function downloadPDF(pdf) {
            const linkSource = `data:application/pdf;base64,${pdf}`;
            const downloadLink = document.createElement("a");
            const fileName = "Prospectus.pdf";
            downloadLink.href = linkSource;
            downloadLink.download = fileName;
            downloadLink.click();
        }
    </script>

    <style type="text/css">
        .modalBackground {
            background-color: rgb(192,234,255);
            filter: alpha(opacity=70);
            opacity: 0.7;
            z-index: 100 !important;
        }

        .modalPopup {
            background-color: #ffffff;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            /*width: 250px;*/
            z-index: 100;
        }

        .btnPop {
            background-color: #033280;
            color: White;
            font-size: 12px;
            font-weight: bold;
            padding-left: 5px;
        }
    </style>
    <script>
        function showCollegeInfo() {
            $("#myModal").modal('show');

        }
        function showSubjectComb() {
            $("#myModal2").modal('show');

        }
    </script>
<style>
.k-grid-footer{
    display: none;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
            <div class="row header">
                <div class="col-xl-10 col-lg-10 col-md-10 col-sm-12 col-12">
                    <nav class="nav-menu d-none d-lg-block">
                        <ul>

                            <li><a href="https://itiharyanaadmissions.nic.in/"><span class="english">Home</span><span class="hindi" style="display: none;">होम</span></a></li>
                            <li><a href="frmDistrictWiseCollege.aspx"><span class="english">District Wise College</span><span class="hindi" style="display: none;">जिला वार कॉलेज</span></a></li>
                            <li><a href="frmViewCollegeSeatMatrix.aspx"><span class="english">View Seat Matrix</span><span class="hindi" style="display: none;">सीट मैट्रिक्स</span></a></li>
                            <li><a href="frmUpdateFamilyId.aspx"><span class="english">Update FamilyId</span><span class="hindi" style="display: none;">अपडेट परिवार आईडी</span></a></li>
                            <li class="active"><a href="frmViewVacantSeatsPG.aspx"><span class="english">View Vacant Seats</span><span class="hindi" style="display: none;">रिक्त सीटें</span></a></li>
                            <li><a href="frmResultUGAdmissions.aspx"><span class="english">Know Your Result</span><span class="hindi" style="display: none;">अपना परिणाम जानिए</span></a></li>

                            <!--  <li><a href="MeritList.aspx">Merit List</a></li>
                            <li><a href="CutOffList.aspx">CutOff List</a></li> -->
                        </ul>

                    </nav>
                    <!-- .nav-menu -->
                </div>
            </div>
        </div>
		
        <div class="container-fluid">

            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading">
                <h4 class="heading">View Vacant Seats for PG Open Counselling</h4>
            </div>
        
<div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center">
                <h5 style="color:#f00;font-weight:800;">Seats may vary on account of ongoing process of cancellation of admissions and clearance of pending payments from banks.</h5>
            </div>		
            <div class="cus-top-section" style="margin-top: 20px;">
                <div class="row">
                    <label class="col-lg-3 col-md-3 col-sm-4 col-12 cus-select-label">Select College</label>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-12">

                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCollege"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>


                    <div class="col-lg-2 col-md-2 col-sm-4 col-12 cus-getbtn">
                        <asp:Button ID="btSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btSearch_Click" VlidationGroup="A" />
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
              
            <div class="row">
                <div style="display:none">
                <div id="dvCoursewise" runat="server" >
                <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading">
                    <h4 class="heading">Coursewise Vacant Seats</h4>
                </div>
                <div class="table-responsive cus-grid-table-four">
                    <asp:GridView ID="GridView1" ShowFooter="true" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">

                        <Columns>
                            <asp:TemplateField HeaderText="Course Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("course") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("sectionname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vacant Seats" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalSeats" runat="server" Text='<%# Eval("totalseats") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Open Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblHOGC" runat="server" Text='<%# Eval("haryanaGeneral") %>' CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SC/DSC">
                                <ItemTemplate>
                                    <asp:Label ID="lblSC" runat="server" Text='<%# Eval("SC") %>' CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Red" />
                    </asp:GridView>

                </div>
                    </div>
                </div>
                     <div id="dvSubComb" runat="server" style="display:none">
                <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading">
                    <h4 class="heading">Course Wise Vacant Seats</h4>
                </div>

                <div  class="table-responsive cus-grid-table-four">
                    <asp:GridView ID="GridView2" ShowFooter="true" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">

                        <Columns>
                            <asp:TemplateField HeaderText="Course Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("course") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("sectionname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="Vacant Seats" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalSeats" runat="server" Text='<%# Eval("Vacant_totalseats") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Sports Seats">
                                <ItemTemplate>
                                    <asp:Label ID="lblsportseat" runat="server" Text='<%# Eval("Vacant_SportsSeat") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
<FooterStyle Font-Bold="True" ForeColor="Red" CssClass="k-grid-footer"/>
                    </asp:GridView>
                   
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
                            <img src="../assets/images/nic-logo.png" style="width: 100px;" />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
    <!-- End Footer -->

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

</body>
</html>
