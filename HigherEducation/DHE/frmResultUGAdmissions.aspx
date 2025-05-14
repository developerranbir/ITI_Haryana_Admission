<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmResultUGAdmissions.aspx.cs" Inherits="HigherEducation.DHE.frmResultUGAdmissions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Result Of ITI Admissions</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/styleResult.css" rel="stylesheet" />
    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet" />

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

            <div class="row header">
                <div class="col-xl-10 col-lg-10 col-md-10 col-sm-12 col-12">
                    <nav class="nav-menu d-none d-lg-block">
                        <ul class="list">

                            <li><a href="https://admissions.itiharyana.gov.in/"><span class="english">Home</span><span class="hindi" style="display: none;">होम</span></a></li>
                            <li><a href="frmDistrictWiseCollege.aspx"><span class="english">District Wise College</span><span class="hindi" style="display: none;">जिला वार कॉलेज</span></a></li>
                            <%-- <li><a href="frmViewCollegeSeatMatrix.aspx"><span class="english">View Seat Matrix</span><span class="hindi" style="display:none;">सीट मैट्रिक्स</span></a></li>
              <li><a href="frmUpdateFamilyId.aspx"><span class="english">Update FamilyId</span><span class="hindi" style="display:none;">अपडेट परिवार आईडी</span></a></li>
                            --%>
                            <li class="active"><a href="frmResultUGAdmissions.aspx"><span class="english">Know Your Result</span><span class="hindi" style="display: none;">अपना परिणाम जानिए</span></a></li>

                        </ul>

                    </nav>
                    <!-- .nav-menu -->
                </div>

            </div>
        </div>
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2">
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-8">
                        <h4 class="heading">Result Of ITI Admissions</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2 text-right back-btn" style="margin-top: 5px;">
                        <a href="#" onclick="redirect();" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-result-data">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-12 m-b-10">
                            <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>

                            <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegId"
                                CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="A"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                                CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-12 m-b-10">
                            <label class="cus-select-label">Name of Candidate:</label><span style="color: red">*</span>
                            <asp:TextBox ID="txtCandidateName" CssClass="form-control" MaxLength="100" runat="server" autocomplete="off"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCandidateName"
                                CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Candidate Name." ValidationGroup="A"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtCandidateName"
                                CssClass="badge badge-danger" ErrorMessage="Invalid Candidate Name" ValidationExpression="^[A-Za-z\s]{1,100}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                        </div>
                        <%-- <div class="col-lg-2 col-md-2 col-sm-2 col-12">
                         <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Select Admission:</label><span style="color: red">*</span>
                        <asp:DropDownList ID="ddlUGPG" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUGPG_SelectedIndexChanged">
                             <asp:ListItem Text="--Please Select Admission--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="UG" Value="UG"></asp:ListItem>
                            <asp:ListItem Text="PG" Value="PG"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUGPG"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Admission" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>--%>
                        <div id="dvUGMeritList" runat="server" class="col-lg-2 col-md-2 col-sm-2 col-12 m-b-10">
                            <label class="cus-select-label">Select MeritList:</label><span style="color: red">*</span>
                            <asp:DropDownList ID="ddlCounselling" runat="server" CssClass="form-control">
                          <%--    <asp:ListItem Text="1st MeritList" Value="1"></asp:ListItem> 
                            <asp:ListItem Text="2nd MeritList" Value="2"></asp:ListItem>  
				 			<asp:ListItem Text="3rd MeritList" Value="3"></asp:ListItem> --%>
							<asp:ListItem Text="4th MeritList" Value="4"></asp:ListItem>
    <%--    <asp:ListItem Text="5th MeritList" Value="5"></asp:ListItem> --%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCounselling"
                                CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Counselling" ValidationGroup="A"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-12 top-section-btn" style="margin-top: 30px;">
                            <asp:Button ID="btnGo" runat="server" CssClass="btn btn-success" Text="Go" OnClick="btnGo_Click" ValidationGroup="A" />
                            <asp:Button ID="btReset" runat="server" CssClass="btn btn-success" Text="Reset" OnClick="btReset_Click" ValidationGroup="B" />
                        </div>
                    </div>

                </div>
            </div>

            <div class="cus-grid-table">
                <div id="dvSection" class="col-lg-12 col-md-12 col-sm-12 col-12 cus-middle-section" runat="server" style="display: none;">
                    <div class="row" style="padding: 0.5rem 0.8rem;">
                        <div id="dvRegId" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12 bt-3">
                            <label class="cus-select-label">Registration Id:</label><br />
                            <asp:Label ID="lblRegId" runat="server"></asp:Label>
                        </div>
                        <div id="dvCandidate" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12 bt-3">
                            <label class="cus-select-label">Name of Candidate:</label><br />
                            <asp:Label ID="lblCandidateName" runat="server"></asp:Label>
                        </div>
                        <div id="dvFatherName" runat="server" class="col-lg-4 col-md-4 col-sm-6 col-12 bt-3">
                            <label class="cus-select-label">Father Name:</label><br />
                            <asp:Label ID="lblFatherName" runat="server"></asp:Label>
                        </div>

                    </div>
                </div>

                <div class="row">
                    <div id="dvGrdResultInfo" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display: none;">
                        <asp:GridView ID="GrdResultInfo" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" Style="width: 100%; margin-top: 20px;">
                            <Columns>
                                <asp:TemplateField HeaderText="ITI Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("collegename") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Trade Section Name"><%--SectionName--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("SectionName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Preference No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrefNo" runat="server" Text='<%# Eval("course_preference") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <HeaderStyle />
                        </asp:GridView>

                    </div>
                </div>

                <div id="dvNote" class="col-lg-12 col-md-12 col-sm-12 col-12 cus-note" runat="server" style="display: none">
                    <h5 style="color: red">NOTE: 4th Round Merit List (Valid upto August 6, 2024).
			         To accept the admission for 4th round merit list, pay your fee online upto August 6, 2024 in student <a href="#" onclick="redirect();">login</a> section. You can choose only one trade, in case your name comes in multiple trades.</h5>
                </div>

            </div>
            <%--  <div id="dvNote" class="col-lg-12 col-md-12 col-sm-12 col-12 cus-note" runat="server" style="display:none">
            <h5 style="color:red">NOTE: This merit is valid upto 8th October 2020. To accept the admission, pay your fee online between 2nd October 2020 to 8th October 2020  in student <a href="#" onclick="redirect();">login</a> section. You can choose only one course, in case your name comes in multiple courses.</h5>
        </div>--%>
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
    function redirect() {
        window.location.href = "https://admissions.itiharyana.gov.in/UG/Account/Login";
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
