<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmFreeze_UnFreezeData.aspx.cs" Inherits="HigherEducation.DHE.frmFreeze_UnFreezeData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Freeze/UnFreeze College Data</title>
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
        .hide_column {
    display : none;
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
                    <h4>Freeze/UnFreeze College Data</h4>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                    <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-choose-collge">
        <div class="row">
            <label class="cus-label col-lg-2 col-md-2 col-sm-6 col-12" id="lblsession">Session<span style="color: red;"><strong>*</strong></span></label>
            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                <select id="ddlSession" class="form-control"></select>
            </div>
        </div>
    </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive cus-fee-table-section">
        <table class="table table-striped table-bordered table-hover" id="tblCourseDetail" style="width: 100%; border-collapse: collapse; font-size: small; padding: 1px 1px;">
            <thead>
                <tr>
                    <th>Sr No.</th>
                    <th >College id </th>
                     <th>Last Status </th>
                    <th >New Status </th>
                    <th>College Name</th>
                    <th>District</th>
                    <th>Freeze/Unfreeze Course,Subject Combination and Fee Detail</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center m-b-10" id="dvfreeze">
         
            <input type="button" value="Submit" class="btn btn-primary" id="btnfreeze" />
            
        </div>
    </div>
        </div>
   <%-- </form>--%>
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
                            <img src="/assets/images/nic-logo.png" style="width: 100px;"/>
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
    <!-- End Footer -->
     <script type="text/javascript">
         function setTextValue(id) {
             var CurrentVaLUE = $("#txtvaL" + id).val();
             if (CurrentVaLUE == 'Y') {
                 $("#txtvaL" + id).val('N');
             }
             else if (CurrentVaLUE == 'N') {
                 $("#txtvaL" + id).val('Y');
             }
         }
         $(document).ready(function () {
             GetSession();

             $('#ddlSession').on('change', function () {
                 GetData();
            });

             $('#btnfreeze').on('click', function () {
                 if ($("#ddlSession").val() == "" || $("#ddlSession").val() == "0") {
                     alert("Please Select Session");
                     return false;
                 }
                 else {

                     var val = "";
                     var FreezeUnFreezeData = [];
                     var jsonTable = $("#tblCourseDetail input[type=checkbox]").map(function () {
                         var row = $(this).closest("TR");
                         return {
                             SessionId: $('#ddlSession').val(),
                             CollegeId: row.find("td:eq(1)").html(),
                             LastStatus: row.find("td:eq(2)").html(),
                             NewStatus: row.find(".txtn").val()
                             }
                     }).get();
                    // console.log(jsonTable);
                     FreezeUnFreezeData = jsonTable;
                     $.ajax({
                         type: "POST",
                         url: '/api/v1/Course/SaveFreezeUnFreezeData',
                         dataType: 'JSON',
                         contentType: 'application/json',
                         data: JSON.stringify(FreezeUnFreezeData),
                         success: function (data) {
                             if (data) {
                                 swal(data);
                             }
                         },
                         error: function () {
                             swal('Failed');
                         }
                     });
                 }

             });
         });
         function GetSession() {
             $('#ddlSession').empty();
             //$('#ddlSession').append($("<option></option>").val(0).html('-Please Select-'));

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
                     GetData();
                 },
                 error: function (x, e) {
                     swal("Error");
                 }
             });
         }
         function GetData() {
             // get datatable 
             startPreloader();
             $.ajax({
                 url: "/api/v1/Course/GetFreezeUnFreezeData",
                 type: "GET",
                 contentType: "application/json;charset=utf-8",
                 data: { "SessionId": $('#ddlSession').val() },
                 dataType: "json",
                 success: function (data) {
                     stopPreloader();
                     var dataset = data;
                     if (data.Table.length > 0) {

                        
                     }
                     table = $('#tblCourseDetail').DataTable({
                         orderCellsTop: true,
                         destroy: true,
                         "searching": true,
                         data: dataset.Table,
                         "paging": false,

                         columns: [
                             { data: null },
                             { data: 'collegeid' },
                             { data: 'LastStatus' },
                             {
                                 "targets": -1,
                                 data: 'NewStatus',
                                 "mRender": function (collegeid, data, type, rowIndex) {
                                     return " <input type='text'  value='" + type.NewStatus + "' class='txtn' id='txtvaL" + type.collegeid + "'    >";

                                 }
                             },
                             //{ data: 'NewStatus', visible: true },
                             { data: 'collegename' },
                             { data: 'districtname' },
                             {
                                 "targets": -1,
                                 data: 'collegeid',
                                 "mRender": function (d, t, r,m) {
                                     return " <input type='checkbox' onchange='setTextValue(" + d + ")' name='chkval' class='chk' id='chk" + d + "'  " + (r["LastStatus"] == "Y"? 'checked="checked"':'') + "  >";
                                 }
                             }

                         ]
                         , "columnDefs": [
                             {
                                 "targets": [1],
                                 className: "hide_column"
                             },
                             {
                                 "targets": [2],
                                 className: "hide_column"
                             },
                             {
                                 "targets": [3],
                                 className: "hide_column"
                                 //"searchable": false
                             },
                         ]
                         //,
                         //"rowCallback": function (row, data, type) {
                         //    if (data.isactive == 'ACTIVE') {
                         //        $(row).css('color', 'black');
                         //    } else {
                         //        $(row).css('color', 'red');
                         //    }
                         //}

                     });

                     table.on('order.dt search.dt', function () {
                         table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                             cell.innerHTML = i + 1;
                             table.cell(cell).invalidate('dom');
                         });
                     }).draw();

                 },

                 error: function (x, e) {
                     swal("Error");
                 }
             });
         }

         function startPreloader() {
             $(".loader").css("display", "block");
             $(".outer-loader").css("display", "block");

         }
         function stopPreloader() {

             $(".loader").css("display", "none");
             $(".outer-loader").css("display", "none");
         }
     </script>
</body>
</html>
