<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAddFeeSubHead.aspx.cs" Inherits="HigherEducation.HigherEducations.frmAddFeeSubHead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fee Sub Head Master</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600" rel="stylesheet" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylefeesub.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/datatables.min.css" rel="stylesheet" />
    <style>
        #showdata > tbody > tr > td:nth-child(2) {
            display: none !important;
        }

        #showdata > thead > tr > th:nth-child(2) {
            display: none !important;
        }

        #showdata > tbody > tr > td:nth-child(3) {
            display: none !important;
        }

        #showdata > thead > tr > th:nth-child(3) {
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


        <div class="container-fluid cus-bg-div">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center top-heading">
                <div class="row">

                    <div class="col-lg-11 col-md-11 col-sm-11 col-11">
                        <h4>Fee Sub Head</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="cus-fee-top-section">
                <div class="col-lg-12 col-md-12 col-sm-12 col-12" id="addhead">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12">
                            <label class="cus-label" id="lblfeehead">Fee Head</label>
                            <select id="ddlFeeHead" class="form-control"></select>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-12">
                            <label class="cus-label" id="lblfeename">Fee Sub Head Name</label>
                            <input type="text" id="txtSubHeadName" class="form-control" maxlength="50" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-12">
                            <label class="cus-label" id="lblwavie">Is Waivable?</label><br />
                            <input type="radio" id="rdbwaivey" name="iswaive" value="YES" />
                            <label for="YES">Yes</label>
                            <input type="radio" id="rdbwaiven" name="iswaive" value="NO" checked="checked" />
                            <label for="NO">No</label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-12 cus-input">
                            <label class="cus-label" id="lblactive">Is Active?</label><br />
                            <input type="radio" id="rdbactivey" name="isactive" checked="checked" value="ACTIVE" />
                            <label for="YES">Yes</label>
                            <input type="radio" id="rdbactiven" name="isactive" value="INACTIVE" />
                            <label for="NO">No</label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 col-12 top-section-btn">
                            <a href="#" id="btnsave" class="btn btn-icon icon-left btn-success"></a>
                            <a href="#" id="btncancel" class="btn btn-icon icon-left btn-outline-danger">Cancel</a>
                            <input type="hidden" id="subheadid" />
                        </div>
                    </div>
                </div>

            </div>
            <%-- <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-sub-table-section">
                <table id="showdata" class="table table-bordered table-hover">
                </table>
            </div>--%>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-sub-table-section">
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
            GetFeeHead();
            $('#txtSubHeadName').on('change', function () {
                CheckSubHeadName(this);
            });
            $('#ddlFeeHead').on('change', function () {
                //$('#load').show();
                GetFeeSubHeadMaster();
            });
            $("#btncancel").click(function () {
                $("#ddlFeeHead").val('0');
                $("#txtSubHeadName").val('');
                $('input[name="' + "iswaive" + '"][value="' + "NO" + '"]').prop('checked', true);
                $('input[name="' + "isactive" + '"][value="' + "ACTIVE" + '"]').prop('checked', true);
                $('#subheadid').val('');
                $('#showdata').parent().empty();
                $('#btnsave').text("Save");
            });
            $('#btnsave').on('click', function () {
                var val = "";
                var radioButtons = document.getElementsByName("iswaive");
                for (var i = 0; i < radioButtons.length; i++) {
                    if (radioButtons[i].checked == true) {
                        val = radioButtons[i].value;
                    }
                }
                var val1 = "";
                var radioButtons1 = document.getElementsByName("isactive");
                for (var i = 0; i < radioButtons1.length; i++) {
                    if (radioButtons1[i].checked == true) {
                        val1 = radioButtons1[i].value;
                    }
                }
                var data = {
                    FeeHeadID: $('#ddlFeeHead').val(),
                    FeeSubHeadName: $('#txtSubHeadName').val(),
                    IsActive: val1.trim(),
                    IsWavier: val.trim(),
                    FeeSubHeadID: $('#subheadid').val()
                };
                SaveFeeSubHeadMaster(data);

            });
        });
        function CheckSubHeadName(elem) {
            var userinput = $(elem).val();
            if (!(userinput == "" || userinput == null)) {
                var pattern = /^[A-Za-z0-9.\s]{1,50}$/i

                if (!pattern.test(userinput)) {
                    swal('warning!', 'Invalid Sub Head Name', 'warning');
                    $(elem).val('');
                    return false;
                }
            }

        }
        function GetFeeHead() {
            $('#ddlFeeHead').empty();
            $('#ddlFeeHead').append($("<option></option>").val(0).html('-Please Select-'));
            $.ajax({
                url: "/UG/api/v1/Fees/GetFeeHead",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: {},
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
        function GetFeeSubHeadMaster() {
            $.ajax({
                url: "/UG/api/v1/Fees/GetFeeSubHeadMaster",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: { "FeeHeadID": $('#ddlFeeHead').val() },
                dataType: "json",
                success: function (data) {
                    $("#showdata > tbody").html("");
                    var dataSet = [];
                    for (var i = 0; i < data.length; i++) {
                        dataSet.push([data[i].SrNo, data[i].FeeSubHeadID, data[i].FeeHeadID, data[i].FeeHeadName, data[i].FeeSubHeadName, data[i].IsWavier, data[i].IsActive, "<input id='btnView" + i + "' type='button' value='Edit'   onclick='getSelectedRow(this," + i + ");' />" + "<input id='btnDel" + i + "' type='button' value='Deactivate'   onclick='DeleteRow(this," + i + ");' />"]);
                    }
                    jQuery('#showdata').DataTable({
                        data: dataSet,
                        columns: [
                            { title: "SrNo" },
                            { title: "FeeSubHeadID" },
                            { title: "FeeHeadID" },
                            { title: "Fee Head Name" },
                            { title: "Fee Sub Head Name " },
                            { title: "Is Wavier" },
                            { title: "IsActive" },
                            { title: "Action" }
                        ],
                        destroy: true,
                        "searching": true,
                        "pageLength": 50,
                        "bPaginate": true,
                        "bInfo": true
                    });
                    //$('#load').hide();
                    //$('#showdata').empty();
                    //if ($('#showdata thead>tr').length == 0) {
                    //    $('#showdata').append("<thead><tr><th>SrNo </th><th style='display: none;'>FeeSubHeadID </th><th style='display: none;'>FeeHeadID </th><th>FeeHead Name </th><th>FeeSubHead Name </th><th>IsWavier </th><th>IsActive </th><th>Action </th> </tr></thead>");
                    //}
                    //$('#showdata').append("<tbody id=tbodyid>");
                    //if (data != null) {
                    //    for (var i = 0; i < data.length; i++) {
                    //        $('#showdata').append("<tr id=" + i + "><td>" + data[i].SrNo + "</td><td style='display: none;'>" + data[i].FeeSubHeadID + "</td><td style='display: none;'>" + data[i].FeeHeadID + "</td><td>" + data[i].FeeHeadName + "</td><td>" + data[i].FeeSubHeadName + "</td><td>" + data[i].IsWavier + "</td><td>" + data[i].IsActive + "</td><td>" + "<input id='btnView" + i + "' type='button' value='Edit'   onclick='getSelectedRow(this," + i + ");' />" + "<input id='btnDel" + i + "' type='button' value='Deactivate'   onclick='DeleteRow(this," + i + ");' />" + "</td></tr>");
                    //    }
                    //}
                    //$('#showdata').append("</tbody>");
                    // $('#showdata').DataTable();
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
            $("#ddlFeeHead").val(col3);
            $("#txtSubHeadName").val(col5);
            $('input[name="' + "iswaive" + '"][value="' + col6.trim() + '"]').prop('checked', true);
            $('input[name="' + "isactive" + '"][value="' + col7.trim() + '"]').prop('checked', true);
            //$('#btnsave').attr('value', "Update");
            $('#btnsave').text("Update");
            $('#subheadid').val(col2);
        }
        function DeleteRow(button, id) {
            var result = confirm("Are you sure to Deactivate this fee sub head?");
            if (result) {
                var row = $(button).closest("TR");
                var idx = row[0].rowIndex;
                var col1 = row.find("td:eq(1)").html();
                $.ajax({
                    type: "Get",
                    url: '/UG/api/v1/Fees/DeleteFeeSubHead',
                    data: { "FeeSubHeadID": col1 },
                    success: function (data) {
                        if (data) {
                            GetFeeSubHeadMaster();
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
            var txt = $('#txtSubHeadName');
            if ($("#ddlFeeHead").val() == "" || $("#ddlFeeHead").val() == "0") {
                alert("Please Select  Fee Head");
                return false;
            }
            else if (txt.val() == null || txt.val() == '') {
                alert("Please Enter Fee Sub Head Name");
                document.getElementById('txtSubHeadName').focus();
                return false;
            }

            else {
                return true;
            }

        }
        function SaveFeeSubHeadMaster(data) {
            if (fncValidate() == true) {
                $.ajax({
                    type: "POST",
                    url: '/UG/api/v1/Fees/SaveFeeSubHeadMaster',
                    data: data,
                    success: function (data) {
                        if (data) {
                            $("#txtSubHeadName").val('');
                            $('input[name="' + "iswaive" + '"][value="' + "NO" + '"]').prop('checked', true);
                            $('input[name="' + "isactive" + '"][value="' + "ACTIVE" + '"]').prop('checked', true);
                            $('#subheadid').val('');
                            GetFeeSubHeadMaster();
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
