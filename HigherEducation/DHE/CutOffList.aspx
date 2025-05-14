<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CutOffList.aspx.cs" Inherits="HigherEducation.DHE.CutOffList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CutOffList</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylesearch.css" rel="stylesheet" />
    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet">
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <%--<script src="../assets/js/jquery-3.4.1.js"></script>--%>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <%--<script src="../assets/js/moment-with-locales.min.js"></script>--%>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>
    <link href="../assets/dataTable/dataTables/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../assets/dataTable/dataTables/jquery.dataTables.min.js"></script>
    <link href="../assets/dataTable/dataTables/buttons.dataTables.min.css" rel="stylesheet" />
    <script src="../assets/dataTable/dataTables/dataTables.buttons.min.js"></script>
    <script src="../assets/dataTable/dataTables/buttons.html5.min.js"></script>
    <script src="../assets/dataTable/dataTables/buttons.print.min.js"></script>
    <script src="../assets/dataTable/dataTables/jszip.min.js"></script>
    <script src="../assets/dataTable/dataTables/pdfmake.min.js"></script>
    <script src="../assets/dataTable/dataTables/vfs_fonts.js"></script>
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

        function pageLoad() {

            $('#<%=GridView1.ClientID%>').DataTable({
                "paging": false, "ordering": true, "searching": true, dom: 'Bfrt',
                buttons: [{ extend: 'pdfHtml5', pagesize: 'legal', orientation: 'landscape', title: 'Department of Higher Education, Haryana\nCut-Off List' }, 'copyHtml5', 'excelHtml5', 'print', 'csvHtml5'],
            })
        };

    </script>
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
                            <li><a href="../DHE/frmDistrictWiseCollege.aspx"><span class="english">District Wise College</span><span class="hindi" style="display: none;">जिला वार कॉलेज</span></a></li>
                            <li><a href="../DHE/frmViewCollegeSeatMatrix.aspx"><span class="english">View Seat Matrix</span><span class="hindi" style="display: none;">सीट मैट्रिक्स</span></a></li>
                            <li><a href="../DHE/frmUpdateFamilyId.aspx"><span class="english">Update FamilyId</span><span class="hindi" style="display: none;">अपडेट परिवार आईडी</span></a></li>
                            <!--   <li><a href="#" onclick="javascript: alert('Integration of Merit Module with Fee Module is in progress. Revised merit list along with fee payment option will be available shortly');"><span class="english">Merit List</span><span class="hindi" style="display:none;">योग्यता क्रम सूची</span></a></li> -->
                            <li><a href="/reports/meritList"><span class="english">Merit List</span><span class="hindi" style="display: none;">योग्यता क्रम सूची</span></a></li>

                            <li><a href="../DHE/frmResultUGAdmissions.aspx"><span class="english">Know Your Result</span><span class="hindi" style="display: none;">अपना परिणाम जानिए</span></a></li>

                            <!--  <li><a href="#" onclick="javascript: alert('Integration of Merit Module with Fee Module is in progress. Revised merit list along with fee payment option will be available shortly');"><span class="english">Know Your Result</span><span class="hindi" style="display:none;">अपना परिणाम जानिए</span></a></li> -->
                            <li class="active"><a href="../DHE/CutOffList.aspx"><span class="english">Cut Off List</span><span class="hindi" style="display: none;">कट ऑफ लिस्ट</span></a></li>


                        </ul>
                    </nav>
                    <!-- .nav-menu -->
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading">
                <h4 class="heading">Cut Off List</h4>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12" style="margin-top: 20px;">
                <div class="cus-top-section">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12">
                        <label>Select College</label><br />

                        <asp:DropDownList ID="ddlCollege" runat="server" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                    <%--New --%>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12">
                        <label>Select Course</label><br />
                        <asp:DropDownList ID="ddlCourse" runat="server" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCourse"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-4 col-12">
                        <label>Select Section</label><br />
                        <asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSection"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12" style="">
                        <label>Select Subject Combination</label><br />
                        <asp:DropDownList ID="ddlChoice" runat="server" OnSelectedIndexChanged="ddlChoice_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlChoice"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Choice" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                    <%--New --%>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-grid-table">
                <div class="table-responsive-lg table-responsive-md table-responsive-sm">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">
                                <Columns>
                                    <asp:TemplateField HeaderText="SrNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="College Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("CollegeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AIOC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAIOC" runat="server" Text='<%# Eval("AIOC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HOGC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHOGC" runat="server" Text='<%# Eval("HOGC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EWS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEWS" runat="server" Text='<%# Eval("EWS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSC" runat="server" Text='<%# Eval("SC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DSC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDSC" runat="server" Text='<%# Eval("DSC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BCA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBCA" runat="server" Text='<%# Eval("BCA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BCB">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBCB" runat="server" Text='<%# Eval("BCB") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDA" runat="server" Text='<%# Eval("DA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DAG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDAG" runat="server" Text='<%# Eval("DAG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DABC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDABC" runat="server" Text='<%# Eval("DABC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DASC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDASC" runat="server" Text='<%# Eval("DASC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMG">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMG" runat="server" Text='<%# Eval("ESMG") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMBCB">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMBCB" runat="server" Text='<%# Eval("ESMBCB") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMSC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMSC" runat="server" Text='<%# Eval("ESMSC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESMDSC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblESMDSC" runat="server" Text='<%# Eval("ESMDSC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlChoice" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="modal cus-modal cus-inner-modal" id="myModal2" role="dialog">
                <div class="modal-dialog  modal-lg" style="margin-top: 1%;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-body" id="mydatamodel2" style="display: block;">
                            <div class="table-responsive cus-grid-table-three">
                            </div>
                        </div>
                    </div>
                    <div class="input-group input-group-sm mb-3">
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
                        <div class="col-lg-10 col-md-10 text-left" style="margin-top:10px;">
                            <div class="credits">
                                <a>Site is technically designed, hosted and maintained by National Informatics Centre, Haryana</a>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2">
                            <img src="../assets/images/nic-logo.png" style="width:100px;" />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer><!-- End Footer -->
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
