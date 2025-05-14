<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feehead.aspx.cs" Inherits="HigherEducation.HigherEducations.feehead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fee Head</title>
    <link rel="icon" href="../assets/images/favicon.ico" type="image/x-icon">
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" id="btnAdd" value="Add Fee Sub Head" />
        </div>
        <div class="container-fluid cus-bg-div" id="addhead" style="display: none;">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 cus-input-div">
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 cus-labels">
                        <label class="cus-labels-name" id="lblcollege">College</label>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 cus-input">
                        <select id="ddlCollege"></select>
                    </div>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                 <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 cus-input-div">
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 cus-labels">
                        <label class="cus-labels-name" id="lblsession">Session</label>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 cus-input">
                        <select id="ddlSession"></select>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 cus-input-div">
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 cus-labels">
                        <label class="cus-labels-name" id="lblfeehead">Fee Head</label>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 cus-input">
                        <select id="ddlFeeHead"></select>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 cus-input-div">
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 cus-labels">
                        <label class="cus-labels-name" id="lblfeename">Fee Sub Head Name</label>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 cus-input">
                        <input type="text" id="txtSubHeadName" />
                    </div>
                </div>
               
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 cus-input-div">
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 cus-labels">
                        <label class="cus-labels-name" id="lblwavie">Is Waivable?</label>
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 cus-input">
                        <input type="radio" id="rdbwaivey" name="iswaive" value="YES" />
                        <label for="YES">Yes</label>
                        <input type="radio" id="rdbwaiven" name="iswaive" value="NO" checked="checked" />
                        <label for="NO">No</label>
                    </div>
                </div>
            </div>
            <div>
                <input type="button" id="btnsave" />
                <input type="button" id="btncancel" value="Cancel" />
                <input type="hidden" id="subheadid" />
            </div>
        </div>
        <div>
            <table id="showdata">
            </table>
        </div>
    </form>
    <script type="text/javascript" src="../assets/js/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="../Content/Js/sweetalert.min.js"></script>
    <script type="text/javascript" src="../assets/js/popper.js/popper.min.js"></script>
    <script type="text/javascript" src="../assets/js/bootstrap/Js/bootstrap.min.js"></script>
    <script src="../Content/DataTables-1.10.20/js/jquery-1.8.2.min.js"></script>
    <script src="../Content/DataTables-1.10.20/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAdd").click(function () {
                $("#addhead").toggle(1000);
            });
            $("#btncancel").click(function () {
                $("#ddlCollege").val('0');
                $("#ddlFeeHead").val('0');
                $("#txtSubHeadName").val('');
                $("#ddlSession").val('0');
                $('input[name="' + "iswaive" + '"][value="' + "NO" + '"]').prop('checked', true);
                $('#subheadid').val('');
                $("#addhead").hide();
            });
            $('#btnsave').attr('value', "Save");
            GetCollege();
            GetSession();
            GetFeeHead();
            $('#ddlCollege').on('change', function () {

       
            });
            $('#btnsave').on('click', function () {
                var val = "";
                var radioButtons = document.getElementsByName("iswaive");
                for (var i = 0; i < radioButtons.length; i++) {
                    if (radioButtons[i].checked == true) {
                        val = radioButtons[i].value;
                    }
                }
                var data = {

                    CollegeID: $('#ddlCollege').val(),
                    FeeHeadID: $('#ddlFeeHead').val(),
                    FeeHeadName: $('#txtSubHeadName').val(),
                    SessionID: $('#ddlSession').val(),
                    Waivable: val.trim(),
                    FeeSubHeadID: $('#subheadid').val()
                };
                SaveFeeHead(data);
                GetFeeHeadData();
            });
        });
        function GetCollege() {
            $('#ddlCollege').empty();
            $('#ddlCollege').append($("<option></option>").val(0).html('-Please Select-'));
            $.ajax({
                url: "/UG/api/v1/Fees/GetCollege",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: {},
                dataType: "json",
                success: function (data) {
                    $.each(data, function (key, value) {
                        $('#ddlCollege').append($("<option></option>").val(value.Value).html(value.Text));
                    });
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }
        function GetFeeHead() {
            $('#ddlFeeHead').empty();
            $('#ddlFeeHead').append($("<option></option>").val(0).html('-Please Select-'));
            $.ajax({
                url: "/UG/api/v1/Fees/GetFeeHead",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: { },
                dataType: "json",
                success: function (data) {
                    $.each(data, function (key, value) {
                        $('#ddlFeeHead').append($("<option></option>").val(value.Value).html(value.Text));
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
                url: "/UG/api/v1/Fees/GetFeeSession",
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
        function SaveFeeHead(data) {
            $.ajax({
                type: "POST",
                url: '/UG/api/v1/Fees/SaveFeeHead',
                data: data,
                success: function (data) {
                    if (data) {
                        $("#ddlCollege").val('0');
                        $("#ddlFeeHead").val('0');
                        $("#txtSubHeadName").val('');
                        $("#ddlSession").val('0');
                        $('input[name="' + "iswaive" + '"][value="' + "NO" + '"]').prop('checked', true);
                        $('#subheadid').val('');
                        swal(data);
                    }
                },
                error: function () {
                    swal('Failed');
                }
            });
        }
        // var tb;
        function GetFeeHeadData() {
            $.ajax({
                url: "/UG/api/v1/Fees/GetFeeHeadData",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                 data: { "Collegeid": $('#ddlCollege').val() },
                dataType: "json",
                success: function (data) {
                    $('#showdata').empty();
                    //typeof tb == 'undefined' ? console.log("") : tb.destroy();
                    if ($('#showdata thead>tr').length == 0) {
                        $('#showdata').append("<thead><tr><th>FeeSubHeadID </th><th>FeeHeadID </th><th>FeeHeadName </th><th>FeeSubHeadName </th><th>CollegeID </th><th>FeeSessionID </th><th>FeeSessionName</th><th>IsWavier </th><th>Action </th> </tr></thead>");
                    }
                    $('#showdata').append("<tbody id=tbodyid>");
                    for (var i = 0; i < data.length; i++) {
                        $('#showdata').append("<tr id=" + i + "><td>" + data[i].FeeSubHeadID + "</td><td>" + data[i].FeeHeadID + "</td><td>" + data[i].FeeHeadName + "</td><td>" + data[i].FeeSubHeadName + "</td><td>" + data[i].CollegeID + "</td><td>" + data[i].FeeSessionID +   "</td><td>" + data[i].FeeSessionName + "</td><td>" + data[i].IsWavier + "</td><td>" + "<input id='btnView" + i + "' type='button' value='Edit'   onclick='getSelectedRow(this," + i + ");' />" + "<input id='btnDel" + i + "' type='button' value='Delete'   onclick='DeleteRow(this," + i + ");' />" + "</td></tr>");
                    }
                    $('#showdata').append("</tbody>");
                    // tb =
                    //   $('#showdata').DataTable({
                    //order: [2],
                    // columnDefs: [{ orderable: false, targets: [7] }],
                    // });
                    $('#showdata').DataTable();
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }
        function getSelectedRow(button, id) {
            var row = $(button).closest("TR");
            var idx = row[0].rowIndex;
            var col1 = row.find("td:eq(0)").html();
            var col2 = row.find("td:eq(1)").html();
            var col3 = row.find("td:eq(2)").html();
            var col4 = row.find("td:eq(3)").html();
            var col5 = row.find("td:eq(4)").html();
            var col6 = row.find("td:eq(5)").html();
            var col7 = row.find("td:eq(6)").html();
            $("#ddlCollege").val(col5);
            $("#ddlFeeHead").val('0');
            $("#txtSubHeadName").val(col4);
            $("#ddlSession").val(col6);
            $('input[name="' + "iswaive" + '"][value="' + col7 + '"]').prop('checked', true);
            $('#btnsave').attr('value', "Update");
            $('#subheadid').val(col1);
            $("#addhead").show();
        }
        function DeleteRow(button, id) {
            var result = confirm("Are you sure to delete this fee sub head?");
            if (result) {
                var row = $(button).closest("TR");
                var idx = row[0].rowIndex;
                var col1 = row.find("td:eq(0)").html();
                $.ajax({
                    type: "Get",
                    url: '/UG/api/v1/Fees/DeleteFeeHead',
                    data: { "FeeHeadID": col1 },
                    success: function (data) {
                        if (data) {
                            GetFeeHeadData();
                            swal(data);
                        }
                    },
                    error: function () {
                        swal('Failed');
                    }
                });
            }
        }
    </script>
</body>
</html>
