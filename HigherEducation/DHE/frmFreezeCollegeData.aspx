<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmFreezeCollegeData.aspx.cs" Inherits="HigherEducation.DHE.frmFreezeCollegeData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Freeze College Data</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600" rel="stylesheet" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylecourse.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/DataTables11020/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/DataTables11020/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../assets/dataTable/dataTables/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
    <style>
        .loader,
        .outer-loader {
            display: inline-block;
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0
        }

        .outer-loader {
            background: rgba(0, 0, 0, .3);
            width: 100%;
            height: 100%;
            z-index: 999
        }

        .loader {
            width: 69px;
            height: 89px;
            border: 1px solid #000;
            margin: auto
        }

        .loader-img {
            border: 15px solid #415bbb;
            border-radius: 50%;
            border-top: 15px solid #000;
            width: 80px;
            height: 80px;
            -webkit-animation: spin 1s linear infinite;
            animation: spin 1s linear infinite
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0)
            }

            100% {
                -webkit-transform: rotate(360deg)
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0)
            }

            100% {
                transform: rotate(360deg)
            }
        }

        .row_new {
            margin-left: 10px;
            margin-right: 10px
        }

        #logo img {
            height: auto;
            padding-left: 3px;
            width: 40px
        }

        body:not(.menu-on-top).desktop-detected {
            min-height: 907px !important
        }

        #feetbl {
            height: 500px;
            overflow: scroll;
            display: block;
        }

        .select2-container .select2-selection--single {
            height: calc(2.25rem + 2px);
        }

        .select2-container {
            width: 100% !important;
        }

    </style>
</head>
<body>
   <%-- <form id="form1" runat="server">--%>
           <div class="container-fluid">
        <div class="row main-banner">
            <img src="../assets/images/banner.jpg" alt="Online Admission Portal" style="width: 100vw;" />
        </div>
    </div>
       <div class="container-fluid">
             <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center top-heading">
            <div class="row">
                <div class="col-lg-1"></div>
                <div class="col-lg-10 col-md-10 col-sm-11 col-11">
                    <h4>Freeze College Data</h4>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                    <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                </div>
            </div>
        </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-choose-collge">
        <div class="row">
            <label class="cus-label col-lg-2 col-md-2 col-sm-6 col-12" id="lblcollegename">College Name</label>
            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                <div style="background:#fff;padding:0.4rem;border-radius:0.4rem;"><%=HttpContext.Current.Session["CollegeName"]%></div>
            </div>
            <label class="cus-label col-lg-2 col-md-2 col-sm-6 col-12" id="lblsession">Session<span style="color: red;"><strong>*</strong></span></label>
            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                <select id="ddlSession" class="form-control"></select>
            </div>
        </div>
    </div>
            <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12" style="margin-top: 10px; text-align: left">

                <div class="card c-shadow" style="margin:1rem;padding:1rem;">
                 <span style="color: #223f73; font-weight: bold; text-align: left; margin-bottom: 0px; margin-top: 7px;">
                        <b>Note:-</b> Click on the Freeze button only after final updations. Only one time freeze is allowed. So be careful while clicking the freeze button.
                    </span>
           </div>
                </div>
                </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center">
        <label id="freeze_status" style="color: red; font-weight: bold; text-align: center; margin-bottom: 0px; display: none"></label>
    </div>
              <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center m-b-10" id="dvfreeze">
            <input type="checkbox" id="chkfreeze" class="custom-checkbox" />
            <span style="color: #223f73; font-weight: bold; text-align:center;margin-top:1rem;margin-bottom:1rem;">I certify that Courses/Section/Course Type/Total Seats/Course Fee/Practical Fee given/submitted/entered/provided by me in respect of my institution, are in order, checked and verified by me.
            </span>
            <br />
            <input type="button" value="Freeze" class="btn btn-primary" id="btnfreeze" />
            
        </div>
           </div>
<%--    </form>--%>
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
    <script type="text/javascript" src="../assets/js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../Content/Js/sweetalert.min.js"></script>
    <script type="text/javascript" src="../assets/js/popperjs/popper.min.js"></script>
    <script type="text/javascript" src="../assets/js/bootstrap/Js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Content/DataTables11020/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../Content/DataTables11020/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../assets/dataTable/dataTables/dataTables.buttons.min.js"></script>
    <script src="../assets/dataTable/dataTables/buttons.print.min.js" type="text/javascript"></script>
    <script src="../assets/dataTable/dataTables/buttons.html5.min.js" type="text/javascript"></script>
    <script src="../scripts/select2.min.js"></script>
      <script type="text/javascript">
          $(document).ready(function () {
              GetSession();
              $('#ddlSession').on('change', function () {
                  GetCourseDetail();
              });
              //freeze record 
              $('#btnfreeze').on('click', function () {
                  if ($('#chkfreeze').prop('checked')) {

                      var SessionId =
                          {
                              SessionId: $('#ddlSession').val()
                          };

                      $.ajax({
                          type: "POST",
                          url: '/api/v1/Course/FreezeCourseUG',
                          data: SessionId,

                          success: function (data) {
                              if (data) {
                                  swal('Freezed', data, 'success');
                                  GetCourseDetail();
                              }
                          },
                          error: function () {
                              swal('Something went wrong..!!');
                          }
                      });
                  }
                  else {
                      swal("Alert!", "Please check the consent", "warning");
                      return;
                  }
              });
          });
          function GetSession() {
              $('#ddlSession').empty();
              $('#ddlSession').append($("<option></option>").val(0).html('-Please Select-'));

              $.ajax({
                  url: "/api/v1/Course/GetSession",
                  type: "GET",
                  contentType: "application/json;charset=utf-8",
                  data: {},
                  dataType: "json",
                  success: function (data) {

                      $.each(data, function (key, value) {
                          $('#ddlSession').append($("<option></option>").val(value.Value).html(value.Text));
                      });
                  },
                  error: function (x, e) {
                      swal("Error");
                  }
              });
          }
          function GetCourseDetail() {
              // get datatable 
              $.ajax({
                  url: "/api/v1/Course/GetCourseDetail",
                  type: "GET",
                  contentType: "application/json;charset=utf-8",
                  data: { "SessionId": $('#ddlSession').val() },
                  dataType: "json",
                  success: function (data) {

                      var dataset = data;
                      if (data.Table.length > 0) {
                          if (data["Table"][0].IsFreezed == "Y") {
                              $('#freeze_status').show();
                              $('#freeze_status').text("Status: Freezed");
                              $('#dvfreeze').hide();
                          }
                          else {
                              $('#dvfreeze').show();
                          }
                      }

                  },

                  error: function (x, e) {
                      swal("Error");
                  }
              });
          }
     </script>
</body>
</html>
