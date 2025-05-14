<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmViewPaymentStatus.aspx.cs" Inherits="HigherEducation.DHE.frmViewPaymentStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Status</title>
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

            <div class="row header">
                <div class="col-xl-10 col-lg-10 col-md-10 col-sm-12 col-12">
                    <nav class="nav-menu d-none d-lg-block">
                        <ul class="list">

                            <li><a href="https://itiharyanaadmissions.nic.in/"><span class="english">Home</span><span class="hindi" style="display: none;">होम</span></a></li>
                            <li><a href="frmDistrictWiseCollege.aspx"><span class="english">District Wise College</span><span class="hindi" style="display: none;">जिला वार कॉलेज</span></a></li>
                            <li><a href="frmViewCollegeSeatMatrix.aspx"><span class="english">View Seat Matrix</span><span class="hindi" style="display: none;">सीट मैट्रिक्स</span></a></li>
                            <li><a href="frmUpdateFamilyId.aspx"><span class="english">Update FamilyId</span><span class="hindi" style="display: none;">अपडेट परिवार आईडी</span></a></li>
                            <li><a href="frmResultUGAdmissions.aspx"><span class="english">Know Your Result</span><span class="hindi" style="display: none;">अपना परिणाम जानिए</span></a></li>
                            <li class="active"><a href="frmViewPaymentStatus.aspx"><span class="english">View Payment Status</span><span class="hindi" style="display: none;">भुगतान की स्थिति देखें</span></a></li>
                             <%--<li><a href="MeritList.aspx"><span class="english">Merit List</span><span class="hindi" style="display:none;">योग्यता क्रम सूची</span></a></li>--%>

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
                        <h4 class="heading">Payment Status</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2 text-right back-btn" style="margin-top: 5px;">
                        <a href="#" onclick="redirect();" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="container-fluid" style="margin-top: 20px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2">
                       
                        <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-12">
                      
                      
                        <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                    </div>
                     <div class="col-lg-2 col-md-2">
                        <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Select Admission:</label><span style="color: red">*</span>
                    </div>
                     <div class="col-lg-3 col-md-3 col-sm-3 col-12">
                        <asp:DropDownList ID="ddlUGPG" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--Please Select Admission--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="UG" Value="UG"></asp:ListItem>
                            <asp:ListItem Text="PG" Value="PG"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlUGPG"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Admission" ValidationGroup="A"></asp:RequiredFieldValidator>

                    </div>
                     <div class="col-lg-1 col-md-1 col-sm-1 col-12 top-section-btn">
                            <asp:Button ID="btnGo" runat="server" CssClass="btn btn-success" Text="Go" OnClick="btnGo_Click" ValidationGroup="A" />

                        </div>
                </div>
                </div>
            </div>
            
            
        
           

        <%--</div>--%>
    

         <div id="dvSection" runat="server" style="display: none;" class="col-lg-12 col-md-12 col-sm-12 col-12 cus-middle-section">
                <div class="row">
                    <div id="dvStuName" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Student Name:</label><br />
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
                    <div id="dvCollegeName" style="display: none;" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">College Name</label><br />
                        <asp:Label ID="lblCollegeName" runat="server"></asp:Label>
                    </div>
                     <div id="dvCourseName" style="display: none;" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Course Name:</label><br />
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </div>
                    <div id="dvSectionName" style="display: none;" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Section Name:</label><br />
                        <asp:Label ID="lblSectionName" runat="server"></asp:Label>
                    </div>
                    <div id="dvSubjectComb" style="display: none;" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Subject Combination:</label><br />
                        <asp:Label ID="lblSubComb" runat="server"></asp:Label>
                    </div>
                 </div>
            </div>
        <div class="cus-grid-table">


            <div class="row">
                <div id="dvPaymentStatus" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display: none;">
                    <asp:GridView ID="GrdPaymentStatus" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" Style="width: 100%; margin-top: 20px;">
                        <Columns>
                            <asp:TemplateField HeaderText="College Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCollegeNameGrd" runat="server" Text='<%# Eval("CollegeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Course Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseNameGrd" runat="server" Text='<%# Eval("CourseName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Section Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSectionNameGrd" runat="server" Text='<%# Eval("SectionName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Subject Combination:">
                                <ItemTemplate>
                                    <asp:Label ID="lblCombinationNameGrd" runat="server" Text='<%# Eval("CombinationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Payment Transaction Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayment_transactionId" runat="server" Text='<%# Eval("Payment_transactionId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Installment/ Annual">
                                <ItemTemplate>
                                    <asp:Label ID="lblPart_Full" runat="server" Text='<%# Eval("Part_Full") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Fee (Rs.)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalFee" runat="server" Text='<%# Eval("TotalFee") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fee Paid (Rs.)">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaidFee" runat="server" Text='<%# Eval("PaidFee") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Payment Gateway">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayment_gateway" runat="server" Text='<%# Eval("Payment_gateway") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Initiate Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentInitiateDate" runat="server" Text='<%# Eval("PaymentInitiateDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Bank Reference No">
                                <ItemTemplate>
                                    <asp:Label ID="lblbank_ref_no" runat="server" Text='<%# Eval("bank_ref_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tracking Id">
                                <ItemTemplate>
                                    <asp:Label ID="lbltracking_id" runat="server" Text='<%# Eval("tracking_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblorder_status" runat="server" Text='<%# Eval("order_status") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                           

                          
                          


                        </Columns>
                        <HeaderStyle />
                    </asp:GridView>

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
    function redirect() {
        window.location.href = "https://itiharyanaadmissions.nic.in/Account/Login";
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
