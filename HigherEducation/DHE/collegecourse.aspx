<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="collegecourse.aspx.cs" Inherits="HigherEducation.HigherEducation.collegecourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>College Course</title>
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
    <%--<form id="form1" runat="server">--%>
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
                    <h4>College Course</h4>
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

    <div id="save_details" style="display: none">
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-fee-top-section">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                        <label class="cus-label" id="lblcoursetype">Course Type<span style="color: red;"><strong>*</strong></span></label><br />
                        <select id="ddlCourseType" class="form-control"></select>

                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-12">

                        <label class="cus-label" id="lblcourse">Course<span style="color: red;"><strong>*</strong></span></label><br />
                        <select id="ddlCourse" class="form-control"></select>
                    </div>
                    <%-- <div class="col-lg-3 col-md-3 col-sm-6 col-12">

                        <label class="cus-label" id="lblsection">Section<span style="color: red;"><strong>*</strong></span></label><br />
                        <select id="ddlSection" class="form-control"></select>
                    </div>--%>


                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="cus-label" id="lbltotalseat">Total Seats<span style="color: red;"><strong>*</strong></span></label><br />
                        <input type="text" maxlength="4" id="txtTotalseat" class="form-control" />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="cus-label" id="lblsportsseat">Additional Sports seat if required<span style="color: red;"></span></label><br />
                        <input type="text" maxlength="4" id="txtSportseat" class="form-control" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 top-section-btn text-center">
            <a href="#" id="btnsave" class="btn btn-icon icon-left btn-success"><i class="fas fa-save"></i>&nbsp;Save</a>
            <a href="#" id="btnupdate" class="btn btn-icon icon-left btn-primary" style="display: none"><i class="fas fa-save"></i>&nbsp;Update</a>

        </div>
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-right cus-add-course">
        <%--<div class="col-lg-12 col-md-12 col-sm-12 col-12 text-right m-b-10" style="">--%>
        <a href="#" id="btnAddcourse" class="btn btn-primary" runat="server"><i class="fas fa-plus"></i>&nbsp;Add Course</a>
    </div>
         <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-12" style="text-align: center;margin-top:1rem;">

                <span style="color: #213f71; font-weight: bold; text-align: center; padding:1rem;"><b>Note:-</b> In case of courses having Subject Combinations, <mark>IGNORE</mark> the Practical Fee on 
                    this screen.The same is shown under the head in <br /> <strong> Course Management &rarr; Add/Update UG Subject Combination, Subject Combination Fee & Subject Combination Seats.</strong>
                </span>
            <br />
            <span style="color: #223f73; font-weight: bold; text-align: left; margin-bottom: 0px; margin-top: 7px;">
                        <b>Note:-</b> In Case of Courses having Subject Combination, the Subject Combination fee shall be taken from the Student as updated by the College in <br /> <strong> Course Management &rarr; Add/Update UG Subject Combination, Subject Combination Fee & Subject Combination Seats.</strong>
                    </span>
         
        </div>
    </div>
    <div class="outer-loader" style="display: none;">
        <div class="loader-img loader" style="display: none;"></div>
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center">
        <label id="freeze_status" style="color: red; font-weight: bold; text-align: center; margin-bottom: 0px; display: none"></label>
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive cus-fee-table-section">
        <table class="table table-striped table-bordered table-hover" id="tblCourseDetail" style="width: 100%; border-collapse: collapse; font-size: small; padding: 1px 1px;">
            <thead>
                <tr>
                    <th>Sr</th>
                    <th style="display: none">College course id </th>
                    <th>Course</th>
                    <th>Section</th>
                    <th>Course Type</th>
                    <th>Total Seats</th>
                    <th>Sports Seat</th>
                    <th>Is Active</th>
                    <th>Course Fee (&#8377)</th>
                    <th>Practical Fee (&#8377)</th>
                    <th style="display: none">IsCombination</th>
                    <th>Edit Seat / Deactivate</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center m-b-10" id="dvfreeze" style="display: none">
            <input type="checkbox" id="chkfreeze" class="custom-checkbox" />
            <span style="color: #223f73; font-weight: bold; text-align:center;margin-top:1rem;margin-bottom:1rem;">I certify that Courses/Section/Course Type/Total Seats/Course Fee/Practical Fee given/submitted/entered/provided by me in respect of my institution, are in order, checked and verified by me.
            </span>
            <br />
            <input type="button" value="Freeze" class="btn btn-primary" id="btnfreeze" />
            
        </div>
    </div>

        </div>
    <%--</form>--%>
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
        var table;
        var collegecourseid;
       // $('#ddlCourse').select2();
        $(document).ready(function () {

            $('input[id="txtTotalseat"],input[id="txtSportseat"]').keyup(function (e) {
                if (/\D/g.test(this.value)) {
                    this.value = this.value.replace(/\D/g, '');
                    swal("Alert!", "Please enter only numeric value", "warning");
                }
            });

            GetSession();
            GetCourseType();
            GetCourse();

            $('#btnAddcourse').on('click', function () {

                if ($('#ddlSession :selected').val() == "0") {

                    swal('Alert', 'Please select session', 'warning');
                    return false;
                }
                else {
                    $('#save_details').show();
                    $('#btnupdate').hide();
                    $('#btnsave').show();
                    return true;
                }
            });

            //$('#ddlCourse').on('change', function () {
            //    GetSection();
            //});
            //$('#ddlCourseType').on('change', function () {
            //	GetCourse();
            //});

            $('#ddlSession').on('change', function () {
                GetCourseDetail();
            });

            $('#btnsave').on('click', function () {
                //validation 
                //var vv = $('#txtTotalseat').val();
                //if (/\D/g.test(vv)) {
                //    vv = vv.replace(/\D/g, '');
                //    swal("Alert!", "Please enter only numeric value", "warning");
                //    $('#txtTotalseat').val('');
                //    return;
                //}

                var course = $('#ddlCourse :selected').val();
                //var section = $('#ddlSection :selected').val();
                var section = $('#ddlCourse :selected').text().trim() + "-" + $('#ddlCourseType :selected').text().trim() + "-I";

                if ($('#ddlCourseType :selected').val().trim() == "0") {

                    swal('Alert', 'Course type required', 'warning');
                    return;
                }
                if ($('#ddlSession :selected').val().trim() == "0") {

                    swal('Alert', 'Session required', 'warning');
                    return;
                }
                if (course == "0" || course == undefined) {

                    swal('Alert', 'Course required', 'warning');
                    return;
                }
                //if (section == "0" || section == undefined) {

                //    swal('Alert', 'Section required', 'warning');
                //    return;
                //}
                if ($('#txtTotalseat').val().trim() == "") {

                    swal('Alert', 'Total seat required', 'warning');
                    return;
                }
                var data = {

                    Session: $('#ddlSession').val(),
                    Course: $('#ddlCourse').val(),
                    //Section: $('#ddlSection').val(),
                    Section: section,
                    TotalSeat: $('#txtTotalseat').val(),
                    CourseType: $('#ddlCourseType').val(),
                    SportSeat: $('#txtSportseat').val(),
                };
                SaveCourse(data);
            });

            $('#btnupdate').on('click', function () {

                //validation 
                var course = $('#ddlCourse :selected').val();
                var section = $('#ddlCourse :selected').text().trim() + "-" + $('#ddlCourseType :selected').text().trim() + "-I";

                //var section = $('#ddlSection :selected').val();

                if ($('#ddlCourseType :selected').val().trim() == "0") {

                    swal('Alert', 'Course type required', 'warning');
                    return;
                }
                if ($('#ddlSession :selected').val().trim() == "0") {

                    swal('Alert', 'Session required', 'warning');
                    return;
                }
                if (course == "0" || course == undefined) {

                    swal('Alert', 'Course required', 'warning');
                    return;
                }
                //if (section == "0" || section == undefined) {

                //    swal('Alert', 'Section required', 'warning');
                //    return;
                //}
                if ($('#txtTotalseat').val().trim() == "") {

                    swal('Alert', 'Total seat required', 'warning');
                    return;
                }

                var Editdata = {

                    Session: $('#ddlSession').val(),
                    Course: $('#ddlCourse').val(),
                    //Section: $('#ddlSection').val(),
                    Section: section,
                    TotalSeat: $('#txtTotalseat').val(),
                    CourseType: $('#ddlCourseType').val(),
                    Collegecourseid: collegecourseid,
                    SportSeat: $('#txtSportseat').val()
                };
                startPreloader();
                $.ajax({
                    type: "POST",
                    url: '/api/v1/Course/UpdateCourse',
                    data: Editdata,
                    success: function (data) {
                        if (data) {
                            stopPreloader();

                            if (data == 1)
                            {
                                //Refresh Datatable
                                $('#ddlCourse').val(0);
                                $('#txtTotalseat').val('');
                                $('#txtSportseat').val('');
                                $('#save_details').show();
                                $('#btnupdate').hide();
                                $('#btnsave').show();
                                $('#ddlCourseType').removeAttr('disabled');
                                $('#ddlSession').removeAttr('disabled');
                                $('#ddlCourse').removeAttr('disabled');
                                GetCourseDetail();
                                swal('Updated', "Course Seats Updated successfully..!!", 'success');

                            }
                            else if (data == 2)
                            {
                                swal('Alert!', "Course Seats cannot be updated as Course seats cannot be less than Sum of Subject Combination seats(Activated)!", 'warning');
                                return;
                            }
                            
                          
                        }
                    },
                    error: function () {
                        swal('Something went wrong..!!');
                    }
                });
            });



            // Edit record
            $('#tblCourseDetail').on('click', 'a.editor_edit', function () {
                //e.preventDefault();
                collegecourseid = "";

                var data_row = table.row($(this).closest('tr')).data();
                $('#save_details').css('display', 'block');
                $('#txtTotalseat').val(data_row.totalseats);
                $('#txtSportseat').val(data_row.SportsSeat);

                $('#ddlCourseType option').attr('selected', false);

                $("#ddlCourseType option").each(function () {
                    if ($(this).text() == data_row.courseaffiliationtype) {
                        $(this).attr('selected', 'selected');
                    }
                });

                $('#ddlCourse').empty();
                $('#ddlCourse').append($("<option></option>").val(0).html('-Please Select-'));

                $.ajax({
                    url: "/api/v1/Course/GetCourse",
                    type: "GET",
                    async: false,
                    contentType: "application/json;charset=utf-8",
                    data: { "CourseType": $('#ddlCourseType').val() },
                    dataType: "json",
                    success: function (data) {

                        $.each(data, function (key, value) {
                            $('#ddlCourse').append($("<option></option>").val(value.Value).html(value.Text));
                        });
                    },
                    error: function (x, e) {
                        swal("Error");
                    }
                });

                $("#ddlCourse option").each(function () {
                    if ($(this).text() == data_row.course) {
                        $(this).attr('selected', 'selected');
                        $('#ddlCourse').attr('disabled', 'disabled');
                        $('#ddlSession').attr('disabled', 'disabled');
                        $('#ddlCourseType').attr('disabled', 'disabled');
                    }
                });

                //$("#ddlCourse  option:contains(" + data_row.course + ")").attr('selected', 'selected');

                //$('#ddlSection').empty();
                //$('#ddlSection').append($("<option></option>").val(0).html('-Please Select-'));

                //$.ajax({
                //    url: "/api/v1/Course/GetSection",
                //    type: "GET",
                //    async: false,
                //    contentType: "application/json;charset=utf-8",
                //    data: { "Courseid": $('#ddlCourse').val() },
                //    dataType: "json",
                //    success: function (data) {

                //        $.each(data, function (key, value) {
                //            $('#ddlSection').append($("<option></option>").val(value.Value).html(value.Text));
                //        });
                //    },
                //    error: function (x, e) {
                //        swal("Error");
                //    }
                //});

                //$("#ddlSection option").each(function () {
                //    if ($(this).text() == data_row.sectionname) {
                //        $(this).attr('selected', 'selected');
                //    }
                //});
                //$("#ddlSection  option:contains(" + data_row.sectionname + ")").attr('selected', 'selected');

                collegecourseid = data_row.collegecourseid
                $('#btnsave').css('display', 'none');
                $('#btnupdate').show();



            });

            // Delete record
            $('#tblCourseDetail').on('click', 'a.editor_remove', function () {
                // e.preventDefault();
                var data_row = table.row($(this).closest('tr')).data();
                var row_collegecourseid = data_row.collegecourseid;
                var rowiscomb = data_row.iscombination;
                var collegecourseid =
                    {
                        Collegecourseid: row_collegecourseid
                    };
                 if (rowiscomb == "Y") {
                     $.ajax({
                         type: "POST",
                         url: '/api/v1/Course/GetCourseSubjectComb',
                         data: collegecourseid,
                         success: function (data) {
                             if (data) {
                                 if (data > 0) {
                                     swal('Alert', "Course cannot be Deactivated.First Deactivate all the Subject Combination(s) of this course.", 'warning');
                                     GetCourseDetail();
                                 }
                                 else {
                                     $.ajax({
                                         type: "POST",
                                         url: '/api/v1/Course/DeleteCourse',
                                         data: collegecourseid,
                                         success: function (data) {
                                             if (data) {
                                                 swal('Course Deactivated Sucessfully!', data, 'success');
                                                 GetCourseDetail();

                                             }
                                         },
                                         error: function () {
                                             swal('Something went wrong..!!');
                                         }
                                     });
                                 }

                             }
                         },
                         error: function () {
                             swal('Something went wrong..!!');
                         }
                     });
                 
                     }
                   else if (rowiscomb == "N") {
                     $.ajax({
                         type: "POST",
                         url: '/api/v1/Course/DeleteCourse',
                         data: collegecourseid,
                         success: function (data) {
                             if (data) {
                                 swal('Course Deactivated Sucessfully!', data, 'success');
                                 GetCourseDetail();

                             }
                         },
                         error: function () {
                             swal('Something went wrong..!!');
                         }
                     });

                                            }



            });

            // Activate Record record
            $('#tblCourseDetail').on('click', 'a.editor_remove_Active', function () {
                // e.preventDefault();

                var data_row = table.row($(this).closest('tr')).data();
                var row_collegecourseid = data_row.collegecourseid;
                var rowiscomb = data_row.iscombination;
                var collegecourseid =
                    {
                        Collegecourseid: row_collegecourseid
                    };

                $.ajax({
                    type: "POST",
                    url: '/api/v1/Course/ActiveCourse',
                    data: collegecourseid,
                    success: function (data) {
                        if (data) {
                            if (rowiscomb == "Y") {
                                swal('Course Activated Sucessfully! Kindly Activate subject combination(s) also from Course Management → Update Subject Combination, Fee & Seats."', data, 'success');
                                GetCourseDetail();
                            }
                            else if (rowiscomb == "N") {
                                swal('Course Activated Sucessfully!', data, 'success');
                                GetCourseDetail();
                            }    
                        }
                    },
                    error: function () {
                        swal('Something went wrong..!!');
                    }
                });

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


        function GetCourse() {
            $('#ddlCourse').empty();
            $('#ddlCourse').append($("<option></option>").val(0).html('-Please Select-'));

            $.ajax({
                url: "/api/v1/Course/GetCourse",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: {},
                dataType: "json",
                success: function (data) {

                    $.each(data, function (key, value) {
                        $('#ddlCourse').append($("<option></option>").val(value.Value).html(value.Text));
                    });
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }


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

        //function GetSection() {
        //    $('#ddlSection').empty();
        //    $('#ddlSection').append($("<option></option>").val(0).html('-Please Select-'));

        //    $.ajax({
        //        url: "/api/v1/Course/GetSection",
        //        type: "GET",
        //        contentType: "application/json;charset=utf-8",
        //        data: { "Courseid": $('#ddlCourse').val() },
        //        dataType: "json",
        //        success: function (data) {

        //            $.each(data, function (key, value) {
        //                $('#ddlSection').append($("<option></option>").val(value.Value).html(value.Text));
        //            });
        //        },
        //        error: function (x, e) {
        //            swal("Error");
        //        }
        //    });
        //}

        function GetCourseType() {
            $('#ddlCourseType').empty();
            //$('#ddlCourseType').append($("<option></option>").val(0).html('-Please Select-'));

            $.ajax({
                url: "/api/v1/Course/GetCourseType",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: {},
                dataType: "json",
                success: function (data) {

                    $.each(data, function (key, value) {
                        $('#ddlCourseType').append($("<option></option>").val(value.Value).html(value.Text));
                    });
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }

        function Clear() {

            $('#ddlSession').val("0");
            $('#ddlCourse').val("0");
            $('#ddlSection').val("0");
            $('#txtTotalseat').val("");
            $('#ddlCourseType').val("0");
        }


        function SaveCourse(data) {
            startPreloader();
            $.ajax({
                type: "POST",
                url: '/api/v1/Course/SaveCourse',
                data: data,
                success: function (data) {
                    stopPreloader();
                    if (data == 0) {
                        swal('Alert', 'Course details already exists.!', 'warning');
                    }
                    else {
                        swal('Saved!', data, 'success');
                        $('#ddlCourse').val(0);
                        $('#txtTotalseat').val('');
                        $('#txtSportseat').val('');
                        GetCourseDetail();
                    }
                },
                error: function () {
                    swal('Failed');
                }
            })
            // Clear();
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
                        //$('#tblCourseDetail').css('display', 'block');  

                        if (data["Table"][0].IsFreezed == "Y") {
                            //$('#btnfreeze').attr("disabled", "disabled");
                            //$('#btnfreeze').addClass("btn btn-danger");
                            //$('#chkfreeze').prop('checked', true);
                            //$('#btnfreeze').val('Freezed');
                            //$('#chkfreeze').attr("disabled", "disabled");
                            $('#dvfreeze').hide();
                            $('#save_details').hide();
                            $('#btnupdate').hide();
                            $('#btnsave').hide();
                            $('#btnAddcourse').hide();
                            $('#freeze_status').show();
                            $('#freeze_status').text("Status: Freezed");

                        }
                        else {
                            //$('#dvfreeze').show();
                            $('#btnAddcourse').show();
                        }
                    }
                    table = $('#tblCourseDetail').DataTable({
                        dom: 'Bfrtip',
                        buttons: [
                            
                            'copy', 'excel', 'pdf', 'csv'
                        ],

                        buttons: [
                            {
                                extend: 'print',
                                text: 'Print',
                                autoPrint: true
                            }
                        ],
                        orderCellsTop: true,
                        destroy: true,
                        buttons: true,
                        "searching": true,
                        data: dataset.Table,
                        "paging": false,

                        columns: [
                            { data: 'sn' },
                            { data: 'collegecourseid', visible: false },
                            { data: 'course' },
                            { data: 'sectionname' },
                            { data: 'courseaffiliationtype' },
                            { data: 'totalseats' },
                            { data: 'SportsSeat' },
                            { data: 'isactive', visible: false },
                            { data: 'coursefee' },
                            { data: 'practical_fee' },
                            { data: 'iscombination', visible: false },
                            {
                                "targets": -1,
                                data: 'courseid',
                                className: "center",
                                //visible: false,
                                "mRender": function (courseid, data, type, rowIndex) {
                                    if (type.IsFreezed == 'Y') {
                                        return '';
                                    }
                                    else {
                                        if (type.isactive == 'ACTIVE') {
                                            // if (type.isactive == true) {
                                            return '<a href="#" class="editor_edit btn btn-primary btn-sm">Edit Seat</a> <a href="#" class="editor_remove btn btn-info btn-sm">Click to Deactivate</a>';
                                        }
                                        else {
                                            // return '';
                                            return '<a href="#" class="editor_remove_Active btn btn-warning btn-sm">Click to Activate</a>';
                                        }
                                    }

                                }
                            }

                        ]
                        ,
                        "rowCallback": function (row, data, type) {
                            if (data.isactive == 'ACTIVE') {
                                $(row).css('color', 'black');
                            } else {
                                $(row).css('color', 'red');
                            }
                        }

                    });

                    //table.on('order.dt search.dt', function () {
                    //    table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    //        cell.innerHTML = i + 1;
                    //    });
                    //}).draw();

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
