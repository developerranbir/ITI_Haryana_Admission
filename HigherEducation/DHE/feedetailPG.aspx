<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedetailPG.aspx.cs" Inherits="HigherEducation.DHE.feedetailPG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fee Detail for Post Graduate</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600" rel="stylesheet" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylefee.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/datatables.min.css" rel="stylesheet" />
    <style>
        #viewdata > tbody > tr > td:nth-child(1) {
            display: none !important;
        }

        #viewdata > thead > tr > th:nth-child(1) {
            display: none !important;
        }

        #viewdata > tbody > tr > td:nth-child(2) {
            display: none !important;
        }

        #viewdata > thead > tr > th:nth-child(2) {
            display: none !important;
        }

        /*#viewdata > tbody > tr > td:nth-child(7) {
            display: none !important;
        }

        #viewdata > thead > tr > th:nth-child(7) {
            display: none !important;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" alt="Online Admission Portal" />
            </div>
        </div>
        <div class="container-fluid cus-bg-div">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center top-heading">
                <div class="row">

                    <div class="col-lg-11 col-md-11 col-sm-11 col-11">
                        <h4>Fee Detail for Post Graduate</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>


            </div>

            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-12" style="margin-top: 10px; text-align: center">

                    <div class="card c-shadow" style="margin-bottom: 2%;">
                        <span style="color: #492A7F; font-weight: bold; text-align: center; margin-bottom: 0px; margin-top: 7px;">Note: Wherever Fee Sub Head is not applicable for your college, kindly enter the Yearly Amount as Zero (0)
                        </span>
                    </div>
                </div>
            </div>
            <div class=" cus-fee-top-section">
                <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 m-b-10">
                            <label class="cus-label" id="lblcollege">College</label>
                            <select id="ddlCollege" class="form-control"></select>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-12 m-b-10">
                            <label class="cus-label" id="lblsession">Session</label>
                            <select id="ddlSession" class="form-control"></select>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-12 m-b-10">
                            <label class="cus-label" id="lblcourse">Courses</label>
                            <select id="ddlCourse" class="form-control"></select>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-12 m-b-10">
                            <label class="cus-label" id="lblsection">Course Section</label>
                            <select id="ddlSection" class="form-control"></select>
                        </div>
                        <%--  <div class="col-lg-4 col-md-4 col-sm-6 col-12 m-b-10">
                            <label class="cus-label" id="lblfeetype">Fee Type</label><br />
                            <input type="radio" id="rdbfeetypeY" name="rdbfeetype" value="Y" checked="checked" />
                            <label for="Y">Yearly </label>
                            <input type="radio" id="rdbfeetypeS" name="rdbfeetype" value="S" />
                            <label for="S">Semester Wise</label>
                        </div>--%>
                        <div class="col-lg-4 col-md-4 col-sm-12 col-12 m-b-10" style="margin-top: 30px">
                            <input type="button" id="btnGo" class="btn btn-primary" value="Go" title="Go" onclick="javascript: GetFreezeData();  " />
                        </div>
                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-sub-table-section" id="dvView">

                        <table id="viewdata" class="table table-bordered table-hover table-striped">
                        </table>


                    </div>

                    <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" id="dvSave" style="margin-bottom: 4%;">
                        <div>
                            <input type="checkbox" id="freezechk" name="freeze" value="F" style="display: none">
                            <label for="freezechk" style="display: none">I certify that fee & fund details, given/submitted/entered/provided by me in respect of my institution, are in order, checked and verified by me.</label>
                        </div>
                        <a href="#" id="btnsave" class="btn btn-icon icon-left btn-success"><i class="fas fa-save"></i>&nbsp;Save Data</a>
                        <a href="#" id="btnfreeze" class="btn btn-icon icon-left btn-info" style="display: none"><i class="fas fa-save"></i>&nbsp;Freeze Data</a>

                    </div>
                </div>
            </div>

        </div>
        <%--        <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-sub-table-section">
            <table id="showdata" class="table table-bordered table-hover table-striped">
            </table>
        </div>--%>
        <!-- ======= Footer ======= -->
        <footer id="footer">
            <div class="container">

                <div class="credits">
                    Designed by <a href="#">National Informatics Center, Haryana</a>
                </div>
            </div>
        </footer>
        <!-- End Footer -->
    </form>
    <script type="text/javascript" src="../assets/js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../Content/Js/sweetalert.min.js"></script>
    <script type="text/javascript" src="../assets/js/popperjs/popper.min.js"></script>
    <script type="text/javascript" src="../assets/js/bootstrap/Js/bootstrap.min.js"></script>
    <script src="../Content/DataTables11020/js/jquery-1.8.2.min.js"></script>
    <script src="../Content/DataTables11020/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        function yearamt(index) {
            var checkboxName = "#checksel" + index;
            $(checkboxName).prop('checked', true);
        }
        //function sem1amt(index) {
        //    var checkboxName = "#checksel" + index;
        //    $(checkboxName).prop('checked', true);
        //}
        //function sem2amt(index) {
        //    var checkboxName = "#checksel" + index;
        //    $(checkboxName).prop('checked', true);
        //}
        function CheckYearAmount(index) {
            var textboxName = "#txtyearamt" + index;
            var userinput = $(textboxName).val();
            if (!(userinput == "" || userinput == null)) {
                var pattern = /^[0-9]{1,6}$/i
                if (!pattern.test(userinput)) {
                    swal('warning!', 'Invalid Yearly Amount', 'warning');
                    $(textboxName).val('');
                    return false;
                }
            }
        }
        // function Semester1Amount(index) {
        //     var textboxName = "#txtsem1amt" + index;
        //    var userinput = $(textboxName).val();
        //    if (!(userinput == "" || userinput == null)) {
        //        var pattern = /^[0-9]{1,10}$/i
        //        if (!pattern.test(userinput)) {
        //            swal('warning!', 'Invalid Semester1 Amount', 'warning');
        //            $(elem).val('');
        //            return false;
        //        }
        //    }
        //}
        // function Semester2Amount(index) {
        //     var textboxName = "#txtsem2amt" + index;
        //    var userinput = $(textboxName).val();
        //    if (!(userinput == "" || userinput == null)) {
        //        var pattern = /^[0-9]{1,10}$/i
        //        if (!pattern.test(userinput)) {
        //            swal('warning!', 'Invalid Semester2 Amount', 'warning');
        //            $(elem).val('');
        //            return false;
        //        }
        //    }
        //}

        function totalsum() {
            debugger;

            var calculated_total_sum = 0;
            $("#viewdata .txty").each(function () {
                var get_textbox_value = $(this).val();
                if ($.isNumeric(get_textbox_value)) {
                    calculated_total_sum += parseFloat(get_textbox_value);
                }
            });
            $("#total_sum_value").html(calculated_total_sum);

            $("#total_val").text("Total Amount(₹)");

        }
        $(document).ready(function () {
            GetSession();
            GetCollege();
            $('#dvView').hide();
            $('#dvSave').hide();
            $('#ddlCourse').on('change', function () {
                GetSection();
            });
            $("#viewdata").on('input', '.txty', function () {
                var calculated_total_sum = 0;
                $("#viewdata .txty").each(function () {
                    var get_textbox_value = $(this).val();
                    if ($.isNumeric(get_textbox_value)) {
                        calculated_total_sum += parseFloat(get_textbox_value);
                    }
                });
                $("#total_sum_value").html(calculated_total_sum);
                $("#total_val").html("Total Amount(₹)");
            });



            $('#btnsave').on('click', function () {
                if ($("#ddlCollege").val() == "" || $("#ddlCollege").val() == "0") {
                    alert("Please Select College");
                    return false;

                }
                else if ($("#ddlSession").val() == "" || $("#ddlSession").val() == "0") {
                    alert("Please Select Session");
                    return false;

                }
                else if ($("#ddlCourse").val() == "" || $("#ddlCourse").val() == "0") {
                    alert("Please Select Course");
                    return false;

                }
                else if ($("#ddlSection").val() == "" || $("#ddlSection").val() == "0") {
                    alert("Please Select Section");
                    return false;

                }
                else {
                    debugger;
                    var val = "";
                    var FeeDetail = [];
                    //var radioButtons = document.getElementsByName("rdbfeetype");
                    //for (var i = 0; i < radioButtons.length; i++) {
                    //    if (radioButtons[i].checked == true) {
                    //        val = radioButtons[i].value;
                    //    }
                    //}
                    //FeeType: val.trim(),
                    //Semester1Amount: row.find(".txt1").val(),
                    //  //Semester2Amount: row.find(".txt2").val()
                    //var jsonTable = $("#viewdata input[type=checkbox]:checked").map(function () {
                    //    var row = $(this).closest("TR");
                    //    return {
                    //        FeeDetailID: row.find("td:eq(0)").html(),
                    //        FeeSubHeadID: row.find("td:eq(1)").html(),
                    //        CollegeID: $('#ddlCollege').val(),
                    //        SessionID: $('#ddlSession').val(),
                    //        CourseID: $('#ddlCourse').val(),
                    //        SectionID: $('#ddlSection').val(),
                    //        YearAmount: row.find(".txty").val()

                    //    }
                    //}).get();
                    var jsonTable = $("#viewdata input[type=text]").map(function () {
                        var row = $(this).closest("TR");
                        if (row.find(".txty").val() != "") {
                            return {
                                FeeDetailID: row.find("td:eq(0)").html(),
                                FeeSubHeadID: row.find("td:eq(1)").html(),
                                CollegeID: $('#ddlCollege').val(),
                                SessionID: $('#ddlSession').val(),
                                CourseID: $('#ddlCourse').val(),
                                SectionID: $('#ddlSection').val(),
                                YearAmount: row.find(".txty").val()
                            }
                        }
                    }).get();
                    console.log(jsonTable);
                    FeeDetail = jsonTable;
                    debugger;
                    $.ajax({
                        type: "POST",
                        url: '/UG/api/v1/FeesPG/SaveFeeDetail',
                        dataType: 'JSON',
                        contentType: 'application/json',
                        data: JSON.stringify(FeeDetail),
                        success: function (data) {
                            if (data) {
                                $("#viewdata > tbody").html("");
                                $("#viewdata > tfoot").html("");
                                $('#dvView').hide();
                                $('#dvSave').hide();
                                $("#freezechk").prop("checked", false);
                                swal(data);
                            }
                        },
                        error: function () {
                            swal('Failed');
                        }
                    });
                    //GetFeeDetailData();
                }

            });
            $('#btnfreeze').on('click', function () {
                var atLeastOneIsChecked = $('input[name="freeze"]:checked').length > 0;
                if (atLeastOneIsChecked > 0) {
                    if ($("#ddlCollege").val() == "" || $("#ddlCollege").val() == "0") {
                        alert("Please Select College");
                        return false;

                    }
                    else if ($("#ddlSession").val() == "" || $("#ddlSession").val() == "0") {
                        alert("Please Select Session");
                        return false;

                    }
                    else if ($("#ddlCourse").val() == "" || $("#ddlCourse").val() == "0") {
                        alert("Please Select Course");
                        return false;

                    }
                    else if ($("#ddlSection").val() == "" || $("#ddlSection").val() == "0") {
                        alert("Please Select Section");
                        return false;

                    }
                    else {
                        var FeeData = new Object();
                        FeeData.CollegeID = $('#ddlCollege').val();
                        FeeData.SessionID = $('#ddlSession').val();
                        FeeData.CourseID = $('#ddlCourse').val();
                        FeeData.SectionID = $('#ddlSection').val();

                        $.ajax({
                            type: "POST",
                            url: '/UG/api/v1/FeesPG/SaveFreezeData',
                            dataType: 'JSON',
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(FeeData),
                            success: function (data) {
                                if (data) {
                                    $("#viewdata > tbody").html("");
                                    $("#viewdata > tfoot").html("");
                                    $('#dvView').hide();
                                    $('#dvSave').hide();
                                    $("#freezechk").prop("checked", false);
                                    swal(data);
                                }
                            },
                            error: function () {
                                swal('Failed');
                            }
                        });
                    }

                }
                else {
                    alert("Please select the Consent");
                    return false;
                }
            });
        });


        function GetCollege() {
            $('#ddlCollege').empty();
            $('#ddlCollege').append($("<option></option>").val(0).html('-Please Select-'));

            var CollegeID = '<%=HttpContext.Current.Session["CollegeId"]%>';

            $.ajax({
                url: "/UG/api/v1/FeesPG/GetCollege",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: {},
                dataType: "json",
                success: function (data) {

                    $.each(data, function (key, value) {
                        $('#ddlCollege').append($("<option></option>").val(value.Value).html(value.Text));
                    });
                    $("#ddlCollege").val(CollegeID);
                    GetCourse();
                    //GetFeeDetailData();
                    $("#ddlCollege").prop("disabled", true);
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }
        function GetCourse() {
            debugger;
            $('#ddlCourse').empty();
            $('#ddlCourse').append($("<option></option>").val(0).html('-Please Select-'));

            $.ajax({
                url: "/UG/api/v1/FeesPG/GetCourse",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: { "Collegeid": $('#ddlCollege').val(), "Sessionid": $('#ddlSession').val() },
                dataType: "json",
                success: function (data) {
                    debugger;
                    $.each(data, function (key, value) {
                        $('#ddlCourse').append($("<option></option>").val(value.Value).html(value.Text));
                    });
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }

        function GetSection() {
            $('#ddlSection').empty();
            $('#ddlSection').append($("<option></option>").val(0).html('-Please Select-'));

            $.ajax({
                url: "/UG/api/v1/FeesPG/GetSection",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: { "Courseid": $('#ddlCourse').val(), "Sessionid": $('#ddlSession').val() },
                dataType: "json",
                success: function (data) {

                    $.each(data, function (key, value) {
                        $('#ddlSection').append($("<option></option>").val(value.Value).html(value.Text));
                    });
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }

        function GetSession() {
            $('#ddlSession').empty();

            $.ajax({
                url: "/UG/api/v1/FeesPG/GetSession",
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



        function GetFreezeData() {
            $("#viewdata > tbody").html("");
            $("#viewdata > tfoot").html("");
            $('#dvView').hide();
            $('#dvSave').hide();
            $("#freezechk").prop("checked", false);
            if ($("#ddlCollege").val() == "" || $("#ddlCollege").val() == "0") {
                alert("Please Select College");
                return false;

            }
            else if ($("#ddlSession").val() == "" || $("#ddlSession").val() == "0") {
                alert("Please Select Session");
                return false;

            }
            else if ($("#ddlCourse").val() == "" || $("#ddlCourse").val() == "0") {
                alert("Please Select Course");
                return false;

            }
            else if ($("#ddlSection").val() == "" || $("#ddlSection").val() == "0") {
                alert("Please Select Section");
                return false;

            }
            else {
                $.ajax({
                    url: "/UG/api/v1/FeesPG/GetFreezeData",
                    type: "GET",
                    contentType: "application/json;charset=utf-8",
                    data: { "CollegeID": $('#ddlCollege').val(), "SessionID": $('#ddlSession').val(), "CourseID": $('#ddlCourse').val(), "SectionID": $('#ddlSection').val() },
                    dataType: "json",
                    success: function (data) {
                        if (data == "Y") {
                            $('#dvSave').hide();
                        }
                        else {
                            $('#dvSave').show();
                        }
                    },
                    error: function (x, e) {
                        swal("Error");
                    }
                });
                GetFeeDetail();
            }



        }

        function GetFeeDetail() {


            $.ajax({
                url: "/UG/api/v1/FeesPG/GetFeeDetail",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: { "CollegeID": $('#ddlCollege').val(), "SessionID": $('#ddlSession').val(), "CourseID": $('#ddlCourse').val(), "SectionID": $('#ddlSection').val() },
                dataType: "json",
                success: function (data) {
                    $("#viewdata > tbody").html("");
                    $("#viewdata > tfoot").html("");
                    var dataSet = [];
                    //"<input type='number'   value='" + data[i].Semester1 + "' id='txtsem1amt" + i + "'  step='1'  min='0' class='txt1' onchange='javascript: sem1amt(" + i + ");Semester1Amount(" + i + ");' >",
                    // "<input type='number'   value='" + data[i].Semester2 + "'  id='txtsem2amt" + i + "' step='1'  min='0' class='txt2' onchange='javascript: sem2amt(" + i + ");Semester2Amount(" + i + ");' >",
                    // yearamt(" + i + "); "<input type='checkbox' id='checksel" + i + "' name='IsSelected' />"
                    //data[i].Iswaiver,
                    for (var i = 0; i < data.length; i++) {
                        dataSet.push([data[i].FeeDetailID, data[i].FeeSubHeadID, data[i].FeeHeadName, data[i].FeeSubHeadName,
                        " <input type='text'  value='" + data[i].Yearly + "' id='txtyearamt" + i + "'   min='0' max='999999' maxlength='6'  class='txty'  onchange='javascript:CheckYearAmount(" + i + ");'    >"
                        ]);
                    }
                    //{ title: "Semester1 Amount" },
                    //{ title: "Semester2 Amount" },
                    //  { title: "Selected" }
                    //{ title: "Is Waiver" },
                    jQuery('#viewdata').DataTable({
                        data: dataSet,
                        columns: [
                            { title: "FeeDetailID" },
                            { title: "FeeSubHeadID" },
                            { title: "Fee Head Name" },
                            { title: "Fee Sub Head Name" },
                            { title: "Yearly Amount(₹) " }

                        ],
                        destroy: true,
                        "bPaginate": false,
                        "bInfo": true,
                        "order": [[2, "asc"]]
                    });
                    //$('#viewdata').append("<div class='col-lg-2' style='float:right;'></div>");
                    // $('#viewdata').append("<div class='col-lg-10' style='float:right;'></div>"); 
                    //   "searching": true,
                    //  "pageLength": 50,
                    //"bPaginate": true,
                    //$('#viewdata').empty();
                    //if ($('#viewdata thead>tr').length == 0) {
                    //    $('#viewdata').append("<thead><tr><th style='display: none;'>FeeDetailID </th><th style='display: none;'>FeeSubHeadID</th><th>FeeHeadName </th><th>FeeSubHeadName </th><th>Iswaiver </th><th>Yearly </th><th>Semester1</th><th>Semester2</th> <th style='display: none;'>Selected</th></tr></thead>");
                    //}
                    //$('#viewdata').append("<tbody id=tbodyid>");
                    //if (data != null) {
                    //    for (var i = 0; i < data.length; i++) {
                    //        debugger;
                    //        $('#viewdata').append("<tr id=" + i + "><td style='display: none;'>" + data[i].FeeDetailID + "</td><td style='display: none;'>" + data[i].FeeSubHeadID + "</td><td>" + data[i].FeeHeadName + "</td><td>" + data[i].FeeSubHeadName + "</td><td>" + data[i].Iswaiver + "</td><td>" + " <input type='number'  value='"  +  data[i].Yearly + "' id='txtyearamt" + i + "'  step='1'  min='0'  class='txty'  onchange='javascript: yearamt(" + i + ");'   >" + "</td><td>" + "<input type='number'   value='"  +  data[i].Semester1 + "' id='txtsem1amt" + i + "'  step='1'  min='0' class='txt1' onchange='javascript: sem1amt(" + i + ");' >" + "</td><td>" + "<input type='number'   value='"  +  data[i].Semester2 + "'  id='txtsem2amt" + i + "' step='1'  min='0' class='txt2' onchange='javascript: sem2amt(" + i + ");' >" + "</td><td style='display: none;'>" + "<input type='checkbox' id='checksel" + i + "' name='IsSelected' />" + "</td></tr>");
                    //    }
                    //}
                    $('#viewdata').append(`
 <tfoot>
                                <tr>
<td colspan=1'></td>
<td> <div class="col-lg-12 col-md-12 col-sm-12" style="text-align: left;">
                                            <span id='total_val' style='font-weight: 800;'></span>
                                        </div></td>
<td><div class="col-lg-12 col-md-12 col-sm-12" style="text-align: left;">
                                            <span id='total_sum_value' style='font-weight: 800;'></span>
                                        </div></td>
                                   
                                </tr>
                            </tfoot>
`);
                    $('#dvView').show();
                    //$('#dvSave').show();

                    totalsum();
                },
                error: function (x, e) {
                    swal("Error");
                }
            });


        }
        function GetFeeDetailData() {

            $.ajax({
                url: "/UG/api/v1/FeesPG/GetFeeDetailData",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: { "Collegeid": $('#ddlCollege').val(), "Sessionid": $('#ddlSession').val() },
                dataType: "json",
                success: function (data) {
                    $("#showdata > tbody").html("");
                    var dataSet = [];
                    //data[i].Sem1Amnt, data[i].Sem2Amnt
                    for (var i = 0; i < data.length; i++) {
                        dataSet.push([data[i].SrNo, data[i].SessionName, data[i].SectionName, data[i].YrAmnt]);
                    }
                    //{ title: "Semester1 Amount" },
                    //{ title: "Semester2 Amount" }
                    jQuery('#showdata').DataTable({
                        data: dataSet,
                        columns: [
                            { title: "SrNo" },
                            { title: "Session" },
                            { title: "Course Section" },
                            { title: "Yearly Amount" }
                        ],
                        destroy: true,
                        "searching": true,
                        "pageLength": 50,
                        "bPaginate": true,
                        "bInfo": true
                    });
                    //      $('#showdata').empty();
                    //if ($('#showdata thead>tr').length == 0) {
                    // $('#showdata').append("<thead><tr><th>SrNo</th><th>Session </th><th>Course Section </th><th>Yearly Amount </th><th>Semester1 Amount</th><th>Semester2 Amount</th> </tr></thead>");
                    //}
                    //     $('#showdata').append("<tbody id=tbodyid>");
                    //     if (data != null) {
                    //         for (var i = 0; i < data.length; i++) {
                    //             $('#showdata').append("<tr id=" + i + "><td>" + data[i].SrNo + "</td><td>" + data[i].SessionName + "</td><td>" + data[i].SectionName + "</td><td>" + data[i].YrAmnt + "</td><td>" + data[i].Sem1Amnt + "</td><td>" + data[i].Sem2Amnt + "</td></tr>");
                    //         }
                    //     }
                    //     $('#showdata').append("</tbody>");
                    //          $('#showdata').DataTable();
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }
    </script>
</body>
</html>
