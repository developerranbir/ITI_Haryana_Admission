<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAddFeeHead.aspx.cs" Inherits="HigherEducation.HigherEducations.frmAddFeeHead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fee Head Master</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600" rel="stylesheet" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylefeehead.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/datatables.min.css" rel="stylesheet" />
    <style>
        #showdata > tbody > tr > td:nth-child(2) {
            display: none !important;
        }

        #showdata > thead > tr > th:nth-child(2) {
            display: none !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" alt="Online Admission Portal" />
            </div>
        </div>

        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center top-heading">
                <div class="row">

                    <div class="col-lg-11 col-md-11 col-sm-11 col-11">
                        <h4>Fee Head</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="cus-fee-top-section">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="addhead">
                    <div class="row">
                        <label class="cus-label col-lg-3 col-md-3 col-sm-6 col-12" id="lblfeename">Fee Head Name</label>
                        <div class="col-lg-4 col-md-3 col-sm-6 col-12">
                            <input type="text" id="txtHeadName" class="form-control" maxlength="50" />
                        </div>
                        <label class="cus-label col-lg-1 col-md-3 col-sm-6 col-12" id="lblactive">Is Active?</label>
                        <div class="col-lg-2 col-md-3 col-sm-6 col-12">
                            <input type="radio" id="rdbactivey" name="isactive" checked="checked" value="ACTIVE" />
                            <label for="YES">Yes</label>
                            <input type="radio" id="rdbactiven" name="isactive" value="INACTIVE" />
                            <label for="NO">No</label>
                        </div>
                        <div class="col-lg-2 col-md-6 col-sm-6 col-12 top-section-btn">
                            <a href="#" id="btnsave" class="btn btn-icon icon-left btn-success"></a>
                            <a href="#" id="btncancel" class="btn btn-icon icon-left btn-outline-danger">Cancel</a>

                            <input type="hidden" id="headid" class="btn btn-primary" />
                        </div>
                    </div>

                </div>
            </div>
            <%--<div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-fee-table-section">
            <table id="showdata" class="table table-bordered table-hover">
            </table>
        </div>--%>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-fee-table-section">
                <%--         <i class="fa fa-spinner  fa-spin" style="font-size: 90px; text-align: center; display: none;" id="load"></i>--%>
                <table id="showdata" class="table table-bordered table-hover">
                </table>
            </div>
        </div>
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
        $(document).ready(function () {
            $('#btnsave').text("Save");
            $('#txtHeadName').on('change', function () {
                CheckHeadName(this);
            });
            $("#btncancel").click(function () {
                $("#txtHeadName").val('');
                $('input[name="' + "isactive" + '"][value="' + "ACTIVE" + '"]').prop('checked', true);
                $('#headid').val('');
                $('#btnsave').text("Save");
            });
            GetFeeHeadMaster();
            $('#btnsave').on('click', function () {
                var val = "";
                var radioButtons = document.getElementsByName("isactive");
                for (var i = 0; i < radioButtons.length; i++) {
                    if (radioButtons[i].checked == true) {
                        val = radioButtons[i].value;
                    }
                }
                var data = {
                    FeeHeadName: $('#txtHeadName').val(),
                    IsActive: val.trim(),
                    FeeHeadID: $('#headid').val()
                };
                SaveFeeHeadMaster(data);
                $('#btnsave').text("Save");
            });
        });
        function CheckHeadName(elem) {
            var userinput = $(elem).val();
            if (!(userinput == "" || userinput == null)) {
                var pattern = /^[A-Za-z0-9.\s]{1,50}$/i

                if (!pattern.test(userinput)) {
                    swal('warning!', 'Invalid Head Name', 'warning');
                    $(elem).val('');
                    return false;
                }
            }

        }
        function GetFeeHeadMaster() {
            $.ajax({
                url: "/UG/api/v1/Fees/GetFeeHeadMaster",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: {},
                dataType: "json",
                success: function (data) {
                    $("#showdata > tbody").html("");
                    var dataSet = [];
                    for (var i = 0; i < data.length; i++) {
                        dataSet.push([data[i].SrNo, data[i].FeeHeadID, data[i].FeeHeadName, data[i].IsActive, "<input id='btnView" + i + "' type='button' value='Edit'   onclick='getSelectedRow(this," + i + ");' />" + "<input id='btnDel" + i + "' type='button' value='Deactivate'   onclick='DeleteRow(this," + i + ");' />"]);
                    }
                    jQuery('#showdata').DataTable({
                        data: dataSet,
                        columns: [
                            { title: "SrNo" },
                            { title: "FeeHeadID" },
                            { title: "Fee Head Name" },
                            { title: "IsActive" },
                            { title: "Action" }
                        ],
                        destroy: true,
                        "searching": true,
                        "pageLength": 50,
                        "bPaginate": true,
                        "bInfo": true
                    });
                    //for (j = 1; j < data.length; j++)
                    //{
                    //    $('#showdata > tbody > tr:nth-child('+ j +') > th:nth-child(2)').hide();
                    //}
                    //$('#showdata > thead > tr:nth-child(' + 1 + ') > th:nth-child(2)').hide();
                    //$('#showdata').empty();
                    //if ($('#showdata thead>tr').length == 0) {
                    //    $('#showdata').append("<thead><tr><th>SrNo </th><th style='display: none;'>FeeHeadID </th><th>FeeHeadName </th><th>IsActive </th><th>Action </th> </tr></thead>");
                    //}
                    //$('#showdata').append("<tbody id=tbodyid>");
                    //if (data !=null) {
                    //    for (var i = 0; i < data.length; i++) {
                    //    $('#showdata').append("<tr id=" + i + "><td>" + data[i].SrNo + "</td><td style='display: none;'>" + data[i].FeeHeadID + "</td><td>" + data[i].FeeHeadName + "</td><td>" + data[i].IsActive  + "</td><td>" + "<input id='btnView" + i + "' type='button' value='Edit'   onclick='getSelectedRow(this," + i + ");' />" + "<input id='btnDel" + i + "' type='button' value='Deactivate'   onclick='DeleteRow(this," + i + ");' />" + "</td></tr>");
                    //     }
                    //}
                    //$('#showdata').append("</tbody>");
                    //$('#showdata').DataTable();
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }
        function BuildDetails(dataTable) {
            debugger;
            var content = [];
            for (var row in dataTable) {
                for (var column in dataTable[row]) {
                    content.push("<tr>")
                    //content.push("<td><b>")
                    //content.push(column)
                    //content.push("</td></b>")
                    content.push("<td>")
                    content.push(dataTable[row][column])
                    content.push("</td>")
                    content.push("</tr>")
                }
            }
            var top = "<table border='1' class='dvhead'>";
            var bottom = "</table>";
            return top + content.join("") + bottom;
        }

        //function BuildTable(dataTable) {
        //         debugger;
        //        var headers = [];
        //        var rows = [];
        //        //column
        //        headers.push("<tr>");
        //        for (var column in dataTable[0])
        //            headers.push("<td><b>" + column + "</b></td>");
        //        headers.push("</tr>");
        //        //row
        //        for (var row in dataTable) {
        //            rows.push("<tr>");
        //            for (var column in dataTable[row]) {
        //                rows.push("<td>");
        //                rows.push(dataTable[row][column]);
        //                rows.push("</td>");
        //            }
        //            rows.push("</tr>");
        //        }
        //        var top = "<table border='1' class='gvhead'>";
        //        var bottom = "</table>";
        //        return top + headers.join("") + rows.join("") + bottom;
        //    }

        function getSelectedRow(button, id) {
            debugger;
            var row = $(button).closest("TR");
            var idx = row[0].rowIndex;
            var col1 = row.find("td:eq(0)").html();
            var col2 = row.find("td:eq(1)").html();
            var col3 = row.find("td:eq(2)").html();
            var col4 = row.find("td:eq(3)").html();
            $("#txtHeadName").val(col3);
            $('input[name="' + "isactive" + '"][value="' + col4.trim() + '"]').prop('checked', true);
            // $('#btnsave').attr('value', "Update");
            $('#btnsave').text("Update");
            $('#headid').val(col2);
        }
        function DeleteRow(button, id) {
            var result = confirm("Are you sure to Deactivate this fee head?");
            if (result) {
                debugger;
                var row = $(button).closest("TR");
                var idx = row[0].rowIndex;
                var col1 = row.find("td:eq(1)").html();
                $.ajax({
                    type: "Get",
                    url: '/UG/api/v1/Fees/DeleteFeeHead',
                    data: { "FeeHeadID": col1 },
                    success: function (data) {
                        if (data) {
                            GetFeeHeadMaster();
                            swal(data);
                        }
                    },
                    error: function () {
                        swal('Failed');
                    }
                });
            }
        }
        function fncValidate() {
            var txt = $('#txtHeadName');
            if (txt.val() == null || txt.val() == '') {
                alert("Please Enter Fee Head Name");
                document.getElementById('txtHeadName').focus();
                return false;
            }

            else {
                return true;
            }

        }
        function SaveFeeHeadMaster(data) {
            if (fncValidate() == true) {
                $.ajax({
                    type: "POST",
                    url: '/UG/api/v1/Fees/SaveFeeHeadMaster',
                    data: data,
                    success: function (data) {
                        if (data) {
                            $("#txtHeadName").val('');
                            $('input[name="' + "isactive" + '"][value="' + "ACTIVE" + '"]').prop('checked', true);
                            $('#headid').val('');
                            GetFeeHeadMaster();
                            swal(data);
                        }
                    },
                    error: function () {
                        swal('Failed');
                    }
                });
            }
            else {
                return false;
            }
        }
    </script>
</body>
</html>
