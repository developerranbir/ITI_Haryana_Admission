<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewStudentDetails.aspx.cs" Inherits="HigherEducation.DHE.ViewStudentDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/styleGlance.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Js/sweetalert.css" rel="stylesheet" />
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>
    <link href="../assets/css/styleseatmatrix.css" rel="stylesheet" />

    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylesearch.css" rel="stylesheet" />
    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <script src="../assets/dataTable/dataTables/jquery-3.5.1.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/dataTable/dataTables/jquery.dataTables.min.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>



    <link href="../assets/css/stylereport.css" rel="stylesheet" />
    <link href="../assets/dataTable/dataTables/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../assets/dataTable/dataTables/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../assets/dataTable/dataTables/buttons.dataTables.min.css" rel="stylesheet" />
    <script src="../assets/dataTable/dataTables/dataTables.buttons.min.js"></script>
    <script src="../assets/dataTable/dataTables/buttons.html5.min.js"></script>
    <script src="../assets/dataTable/dataTables/jszip.min.js"></script>
    <script src="../assets/dataTable/dataTables/pdfmake.min.js"></script>
    <script src="../assets/dataTable/dataTables/vfs_fonts.js"></script>
    <script src="../assets/dataTable/dataTables/buttons.print.min.js"></script>


    <link href="~/assets/css/jquery-ui.css" rel="stylesheet" />
    <link href="~/assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="~/assets/css/styleregister.css" rel="stylesheet" />
    <link href="~/swalJs/swal-forms.css" rel="stylesheet" />
    <script src="~/swalJs/swal-forms.js"></script>




   

    
  <%--  <link href="../assets/css/all.css" rel="stylesheet" />--%>
   
   


    <style>
        .mygoclass {
            background: #492A7F;
            cursor: pointer;
            border: 1px solid #492A7F;
            color: #fff;
            background-color: #007bff;
            border-color: #007bff;
            display: inline-block;
            font-weight: 400;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            padding: .5rem .75rem;
            font-size: 1rem;
            line-height: 1.25;
            border-radius: .25rem;
            transition: all .15s ease-in-out;
        }

        .cus-grid-table {
            height: 100%;
            min-height: auto;
        }

        .cus-grid-table-down {
            margin-bottom: 9.4%;
        }

        input#DOB2 {
            border: none;
            background-color: #fff;
        }

        #cover {
            /*background: url("to/your/ajaxloader.gif") no-repeat scroll center center #FFF;*/
            background-color: rgba(178, 0, 255, 0.81);
            position: fixed;
            top: 0px;
            bottom: 0px;
            height: 100%;
            width: 100%;
            z-index: 100000;
        }

            #cover i {
                /*background: url("to/your/ajaxloader.gif") no-repeat scroll center center #FFF;*/
                margin-left: 50%;
                margin-top: 50vh;
            }
        /*.cus-grid-table-down {
    margin-bottom: 0px;
}*/
    </style>
    <script>

        /* allow only alphabet & Numeric javascript function */
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45' || keyEntry == '.')
                return true;
            else {

                alert('Please Enter Only Character e.g. a,b,c,d');
                return false;
            }
        }
        function AllowNumeric(key) {
            var keycode = (key.which) ? key.which : key.keyCode;
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                alert('Please Enter Only Digits e.g. 1,2,3,4');
                return false;
            }
            else {
                return true;
            }
        }
        var errorFn = function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            alert("Error: " + msg);
            $("#cover").fadeOut("slow");
        };
        $(document).ready(function () {
            debugger;
            $("#cover").fadeOut("slow");
            var UserStatus1 = '<%=HttpContext.Current.Session["UserType"]%>';
            if (UserStatus1 == '2') {
                $('#OnlyCan').show();
            }
            else {
                $('#OnlyCan').hide();
            }
            ImagePop();
            $("#f_UploadImage").on('change', function () {
                var a = this.files[0].size / 1024;
                fileName = document.querySelector('#f_UploadImage').value;
                regex = new RegExp('[^.]+$');
                extension = fileName.match(regex);

                var b = 0;
                if (extension == "pdf") {
                    b = 300;
                }
                else {
                    b = 1000;
                }
                if (parseInt(a) <= parseInt(b)) {
                    if (typeof (FileReader) != "undefined") {
                        var dvPreview = $("#myUploadedImg");
                        dvPreview.html("");
                        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                        $($(this)[0].files).each(function () {
                            var file = $(this);
                            if (regex.test(file[0].name.toLowerCase())) {
                                var reader = new FileReader();
                                reader.onload = function (e) {
                                    var img = $("<img />");
                                    img.attr("style", "height:100px;width: 100px");
                                    img.attr("src", e.target.result);
                                    dvPreview.append(img);
                                    $("#myUploadedImg").attr("src", e.target.result);

                                }
                                reader.readAsDataURL(file[0]);
                            }
                            else {
                                alert(file[0].name + " is not a valid image file.");
                                dvPreview.html("");
                                $("#f_UploadImage").val("");
                                return false;
                            }
                        });
                    }
                    else {
                        alert("This browser does not support HTML5 FileReader.");
                    }
                }
                else {
                    swal("Alert", "file size should not exceed 1 MB for compression", "warning");
                }



            });
            $('#sec_box').show();
            $('#btndetails').hide();
            $('#Alldetails').hide();
        });
        function validation(abc) {
            debugger;
            var image = $('#myUploadedImg').attr('src');
            var regid = $("#txtphoto").val();
            var dataToSend = {};
            dataToSend.image = image;
            dataToSend.regid = regid;
            dataToSend.docid = "8";
            $.ajax({
                type: "POST",
                url: '/api/v1/Fees/Photo',
                data: dataToSend,
                success: function (data) {
                    if (data) {
                        if (data == 1) {
                            swal('Update!', 'Updated', 'success');
                            $('#txtphoto').val("");

                        }
                        else {
                            swal('Error', 'Something went wrong', 'error');
                        }

                        //swal('Personal details Update successfully');
                        //$('#txtRagiste').val("");
                        //$('#txtCandidateName').val("");
                        //$('#txtFName').val("");
                        //$('#txtmother').val("");
                        //$('#txtmobile').val("");
                        //$('#DOB').val("");
                        //$('#Edu_box').hide();

                    }
                },
                error: function () {
                    swal('Failed');
                }
            });
        }
        function GetPhoto() {
            if ($("#txtphoto").val() == "" || $("#txtphoto").val() == "0") {
                swal('Enter Registration No');
                $("#txtphoto").focus();
                return false;
            }
            var regid = $("#txtphoto").val();

            GetMyphoto(regid);
            $('#divphoto').show();
            $('#Edu_box').hide();
            $('#sec_box').hide();
            $('#Photo_box').show();

        };
        function GetMyphoto(RegNO) {
            debugger
            $.ajax({
                url: "/Reports/GetMyphoto",
                type: 'Get',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                data: { RegNO: RegNO },
                success: function (data) {
                    var response_DataSet = JSON.parse(data);
                    if (response_DataSet.Table.length > 0) {
                        response_DataSet.Table.forEach(function (Y) {
                            if (Y.DocId == 8) {
                                addImageToControl('myUploadedImg', Y.Docs);
                            }
                        });
                    }
                    else {

                        $("#myUploadedImg").attr("src", "../assets/images/no-img.png");
                    }
                },
                error: errorFn

            })
        }
        function addImageToControl(imgId, base64) {

            var switch_on = base64.toString().substr(0, 5);
            $("#" + imgId).show();
            $("#" + imgId + '_PDF').hide();
            switch (switch_on) {
                case 'iVBOR':

                    $("#" + imgId).attr("src", "data:image/png;base64," + base64);

                    break;
                case '/9j/4':

                    $("#" + imgId).attr("src", "data:image/jpeg;base64," + base64);

                    break;
                case 'R0lGO':

                    $("#" + imgId).attr("src", "data:image/gif;base64," + base64);

                    break;
                case 'JVBER':

                    var newUrl = "data:application/pdf;base64," + base64;
                    var divEl = document.getElementById(imgId + '_CONTAINER');
                    var objEl = document.getElementById(imgId + '_PDF');
                    objEl.data = newUrl;
                    // Refresh the content
                    divEl.innerHTML = divEl.innerHTML;
                    $("#" + imgId).hide();
                    $("#" + imgId + '_PDF').show();
                    /*$("#" + imgId + '_PDF').children().remove();*/
                    $("#" + imgId + '_PDF > p > a').attr("href", ("data:application/pdf;base64," + base64));
                    break;
                default:
                    $("#" + imgId).attr("src", "../assets/images/no-img.png" + base64);


            }
        }
        function ImagePop() {
            $('.pop').on('click', function () {
                debugger
                $('.imagepreview').attr('src', $(this).find('img').attr('src'));
                $('#imagemodal').modal('show');
            });
        }

        function GetNCVTData() {
            if ($("#txtNCVTData").val() == "" || $("#txtNCVTData").val() == "0") {
                swal('Enter Registration No');
                $("#txtNCVTData").focus();
                $('#txtNCVTMO').val("");
                $('#txtNCVTEmail').val("");
                return false;
            }
            $('#MoEm').show();
            var regid = $("#txtNCVTData").val();
            getNCVTDetails(regid);
        };
        var NCVTMO = "";
        var NCVTEmail = "";
        function getNCVTDetails(RegNO) {

            $.ajax({
                url: "/Reports/getNCVTDetails",
                type: 'Get',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                data: { RegNO: RegNO },
                success: function (data) {
                    var collegedistict = JSON.parse(data);
                    if (collegedistict.Table.length > 0) {
                        NCVTMO = collegedistict.Table[0]["MobileNo"];
                        NCVTEmail = collegedistict.Table[0]["Email"];
                        $('#txtNCVTMO').val(NCVTMO);
                        $('#txtNCVTEmail').val(NCVTEmail);
                    }
                    else {
                        $('#txtNCVTMO').attr("value", "");
                        $('#txtNCVTEmail').attr("value", "");
                        swal('Ragistration No Is Not Exists ');
                        $('#txtNCVTMO').val("");
                        $('#txtNCVTEmail').val("");

                    }
                },
                error: errorFn

            })
        }
        function updateNCVTDetails() {
            debugger
            if ($("#txtNCVTMO").val() == "" || $("#txtNCVTMO").val() == "0") {
                alert("Please Mobile No");
                return false;

            }
            if ($("#txtNCVTEmail").val() == "" || $("#txtNCVTEmail").val() == "0") {
                alert("Please Enter Email Id");
                return false;

            }
            var dataToSend = {};
            dataToSend.RegId = $("#txtNCVTData").val();
            dataToSend.NCVTMO = $("#txtNCVTMO").val();
            dataToSend.NCVTEmail = $("#txtNCVTEmail").val();
            $.ajax({
                type: "POST",
                url: '/api/v1/Fees/UpdateNcvtData',
                data: dataToSend,
                success: function (data) {
                    if (data) {
                        swal('NCVT details Update successfully');
                        $('#txtNCVTData').val("");
                        $('#txtNCVTMO').val("");
                        $('#txtNCVTEmail').val("");
                    }
                },

            });
        };
        //function validation(abc) {
        //    debugger
        //    //var image =  $('#myUploadedImg').attr('src');
        //    var id = abc.id;
        //    var selectedFile = document.getElementById(id).files;
        //    var a = abc.files[0].size / 1024;
        //    var fname = (document.getElementById(id).value).substring(12);
        //    var extension = fname.replace(/^.*\./, '');
        //    console.log(extension);
        //    var b = 0;
        //    if (extension == "pdf") {
        //        b = 300;
        //    }
        //    else {
        //        b = 1000;
        //    }
        //    if (a < parseInt(b)) {

        //        //Check File is not Empty
        //        if (selectedFile.length > 0) {
        //            //
        //            var re = /(\.pdf)$/i;
        //            var re1 = /(\.jpeg)$/i;
        //            var re2 = /(\.jpg)$/i;
        //            var re3 = /(\.png)$/i;

        //            var fname = (document.getElementById(id).value).substring(12);
        //            var res = fname.split(".");
        //            var count = res.length;
        //            if (count > 2) {
        //                document.getElementById(id).value = null;
        //                alert("File not supported! Kindly select pdf/png/jpg/jpeg file only");
        //                return false;
        //            }
        //            if (re.exec(fname) || re1.exec(fname) || re2.exec(fname) || re3.exec(fname)) {
        //                var extenstion = (document.getElementById(id).value).substring(12);
        //                // Select the very first file from list
        //                var fileToLoad = selectedFile[0];
        //                // FileReader function for read the file.
        //                var fileReader = new FileReader();
        //                var base64;
        //                var filesubstring;
        //                // Onload of file read the file content
        //                fileReader.onload = function (fileLoadedEvent) {
        //                    base64 = fileLoadedEvent.target.result;
        //                    substring = "JVBER";
        //                    substring2 = "/9j/4";
        //                    substring3 = "iVBOR";

        //                    if (base64.includes(substring) || base64.includes(substring2) || base64.includes(substring3)) {
        //                    }
        //                    else {
        //                        document.getElementById(id).value = null;
        //                        swal("Alert!", "File not supported. Kindly select pdf/png/jpg/jpeg file only.", "warning");
        //                    }
        //                    //console.log(base64);
        //                };
        //                // Convert data to base64
        //                fileReader.readAsDataURL(fileToLoad);
        //            }
        //            else {
        //                document.getElementById(id).value = null;
        //                swal("Alert!", "File not supported. Kindly select pdf/png/jpg/jpeg.", "warning");
        //            }
        //        }
        //    }
        //    else {
        //        document.getElementById(id).value = null;
        //        swal("Warning!", "File size is not exceed " + b + " KB", "warning");
        //    }
        //}


        $(function () {
            $('#PerDeta').click(function () {
                $('#Edu_box').hide();
                $('#sec_box').show();
                $('#Photo_box').hide();
                $('#NCVTData_box').hide();
            });
            $('#edudetails').click(function () {
                $('#sec_box').hide();
                $('#Edu_box').show();
                $('#List').hide();
                $('#Photo_box').hide();
                $('#NCVTData_box').hide();

            });
            $('#photo').click(function () {
                $('#sec_box').hide();
                $('#Edu_box').hide();
                $('#List').hide();
                $('#Photo_box').show();
                $('#NCVTData_box').hide();
            });
            $('#NCVTData').click(function () {
                $('#sec_box').hide();
                $('#Edu_box').hide();
                $('#List').hide();
                $('#Photo_box').hide();
                $('#NCVTData_box').show();
            });

        });

        function txtChanged(mytxt) {
            $("#txtRagiste").focus();
            $('#txtCandidateName').val("");
            $('#txtFName').val("");
            $('#txtmother').val("");
            $('#txtmobile').val("");
            $('#DOB').val("");
            $('#details').hide();
            $('#Alldetails').hide();

            /*GetCollegeMaster(regid);*/

        }
        /*var urlCollegeName = '@Url.Action("getCondidateDetail", "Reports")';*/

        function GetPersonalDedail() {
            if ($("#txtRagiste").val() == "" || $("#txtRagiste").val() == "0") {
                swal('Enter Registration No');
                $("#txtRagiste").focus();
                $('#txtCandidateName').val("");
                $('#txtFName').val("");
                $('#txtmother').val("");
                $('#txtmobile').val("");
                $('#DOB').val("");
                return false;

            }
            var txtRegId = $("#txtRagiste").val();
            getDetails(txtRegId);
            $('#details').show();
        };
        function GetEduDedail() {
            $('#EdeQualification1').hide();
            $('#EdeQualification2').hide();
            $('#EdeQualification3').hide();
            $('#EdeQualification4').hide();
            $('#EdeQualification5').hide();
            if ($("#EduRagistration").val() == "" || $("#EduRagistration").val() == "0") {
                swal('Enter Ragistration No');
                $("#EduRagistration").focus();
                return false;
            }
            var regid = $("#EduRagistration").val();

            GetCollegeMaster(regid);
            $('#Edu_box').show();
            $('#sec_box').hide();

        };
        var CandidateName = "";
        var FHName = "";
        var MotherName = "";
        var MobileNo = "";
        var Qualification = "";
        var BOD = "";
        var ITIName = "";
        var url2 = '@Url.Action("getCondidateDetail", "Reports")';
        function getDetails(RegNO) {
            debugger
            document.getElementById("yesCheck").checked = false;
            document.getElementById("yesCheckFather").checked = false;
            document.getElementById("yesCheckMother").checked = false;
            document.getElementById("yesCheckDOB").checked = false;
            document.getElementById('ifYes').style.visibility = 'hidden';
            document.getElementById('ifYesFather').style.visibility = 'hidden';
            document.getElementById('ifYesMother').style.visibility = 'hidden';
            document.getElementById('ifYesDOB').style.visibility = 'hidden';
            var UserStatus = '<%=HttpContext.Current.Session["UserType"]%>';
          
            $.ajax({
                //url: url2,
               /* url: "/Reports/getCondidateDetail",*/
                 url: "/UG/Reports/getCondidateDetail",
                type: 'GET',
                contentType: 'application/json;charset=utf-8',
                data: { RegNO: RegNO, UserStatus: UserStatus },
                dataType: 'json',
                success: function (data) {
                    debugger
                    var collegedistict = JSON.parse(data);
                    if (collegedistict.Table1[0]["success"]=='3') {
                        swal('You cannot update as this Registration Id does not belong to your ITI');
                        $('#Alldetails').hide();
                        return false;
                    }
                    if (collegedistict.Table1[0]["success"] == '2') {
                        swal('You have already updated Trainee  detail');
                        $('#Alldetails').hide();
                        return false;
                    }
                    if (collegedistict.Table1[0]["success"]=='1' && collegedistict.Table.length > 0) {
                        /* $("#cover").fadeIn();*/
                        $('#Alldetails').show();
                        CandidateName = collegedistict.Table[0]["CandidateName"];
                        FHName = collegedistict.Table[0]["FatherName"];
                        MotherName = collegedistict.Table[0]["MotherName"];
                        MobileNo = collegedistict.Table[0]["MobileNo"];
                        BOD = collegedistict.Table[0]["BirthDate"];
                        ITIName = collegedistict.Table[0]["ITIName"];
                        /* $('#txtCandidateName').val(CandidateName);*/
                        $('#lblCandidateName').text(CandidateName);
                        $('#lblFName').text(FHName);
                        $('#lblmother').text(MotherName)
                        $('#txtmobile').val(MobileNo);
                        $('#DOB').val(BOD);
                        $('#DOB2').val(BOD);
                        $('#lblITIName').text(ITIName);
                        var v1 = document.getElementById("DOB2");
                        v1.setAttribute("disabled", "true");

                        //$('#txtFName').attr("value", FHName);
                        //$('#txtmother').attr("value", MotherName);
                        //$('#txtmobile').attr("value", MobileNo);
                        //$('#DOB').attr("value", BOD);
                        $("#txtmobile").prop("disabled", true);
                        
                       /* $("#cover").fadeOut("slow");*/
                    }
                    else {
                        $('#txtCandidateName').attr("value", "");
                        $('#txtFName').attr("value", "");
                        $('#txtmother').attr("value", "");
                        $('#txtmobile').attr("value", "");
                        $('#DOB').attr("value", "");

                       
                        $('#Alldetails').hide();
                        $('#txtCandidateName').val("");
                        $('#txtFName').val("");
                        $('#txtmother').val("");
                        $('#txtmobile').val("");
                        $('#DOB').val("");
                        $('#lblITIName').text("");
                        swal('Record not found');   //may be fee paid unsucess
                    }

                },
                error: errorFn

            })
        }
        //function txtChangededu(mytxt) {

        //    $('#List').hide();
        //    var regid = mytxt.value;
        //    GetCollegeMaster(regid);
        //    $('#Edu_box').show();
        //    $('#sec_box').hide();


        //}
        function GetCollegeMaster(RegNO) {
            $.ajax({
                url: "/api/v1/Fees/GetCollegeMaster1",
                /* url: "/UG/api/v1/Fees/GetCollegeMaster1",*/
                type: "GET",
                contentType: "application/json;charset=utf-8",
                data: { RegNO: RegNO },
                dataType: "json",
                success: function (data) {
                    $('#showdata').empty();
                    if ($('#showdata thead>tr').length == 0) {
                        $('#showdata').append("<thead><tr><th>SrNo </th><th>ExamPassed </th><th>RegistrationRollno</th><th>MarksObt</th><th>MaxMarks</th><th>Percentage</th><th style='display: none;'>P_Id</th><th>Action</th></tr></thead>");
                    }
                    $('#showdata').append("<tbody id=tbodyid>");
                    for (var i = 0; i < data.length; i++) {
                        $('#showdata').append("<tr SrNo=" + i + "><td>" + data[i].SrNo + "</td><td>" + data[i].ExamPassed + "</td><td>" + data[i].RegistrationRollno + "</td><td>" + data[i].MarksObt + "</td><td>" + data[i].MaxMarks + "</td><td>" + data[i].Percentage + "</td><td style='display: none;'>" + data[i].P_Id + "</td><td>" + "<input id='btnView" + i + "' type='button' value='Edit'   onclick='getSelectedRow(this," + i + ");' />" + "</td></tr>");
                    }
                    $('#showdata').append("</tbody>");
                    $('#showdata').DataTable();
                    $('#Edu_box').show();
                    /*nav - link text - uppercase font - weight - bold mr - sm - 3 rounded - 0 active*/
                },
                error: function (x, e) {
                    swal("Error");
                }
            });
        }
        function getSelectedRow(button, id) {

            $('#EdeQualification1').show();
            $('#EdeQualification2').show();
            $('#EdeQualification3').show();
            $('#EdeQualification4').show();
            $('#EdeQualification5').show();

            var row = $(button).closest("TR");
            var idx = row[0].rowIndex;
            var col1 = row.find("td:eq(0)").html();
            var col2 = row.find("td:eq(1)").html();
            var col3 = row.find("td:eq(2)").html();
            var col4 = row.find("td:eq(3)").html();
            var col5 = row.find("td:eq(4)").html();
            var col6 = row.find("td:eq(5)").html();
            var col7 = row.find("td:eq(6)").html();
            $("#txtRollNo").val(col3);
            $("#txtMarksObt").val(col4);
            $("#tblMaxMarks").val(col5);
            $("#txtPercentage").val(col6);
            $("#txtPercentage").prop("disabled", true);
            $("#EduRagistration").prop("disabled", true);
            $("#txtRollNo").prop("disabled", true);
            var UpdateID = document.getElementById('<%=hiddenID.ClientID%>').value = col7;
          <%--  $("#txtCollegeName").val(col4);
            $("#txtInstCode").val(col5);
            $("#ddlCollegeType").val(col8);
            $("#ddlDistrict").val(col2);
            $("#ddlUniversity").val(col6);
            $("#ddlEduMode").val(col10);
            $('input[name="' + "rdbIsActive" + '"][value="' + col12.trim() + '"]').prop('checked', true);
            $('input[name="' + "rdbIsActivePG" + '"][value="' + col13.trim() + '"]').prop('checked', true);
            document.getElementById('<%=hdId.ClientID%>').value = col14;
               document.getElementById('btnSubmit').value = "Update";--%>
        }
        var P_Id = "";
        function updateData() {
            P_Id = document.getElementById('<%=hiddenID.ClientID%>').value;
            var RegNO = $("#EduRagistration").val();
            var RollNo = $("#txtRollNo").val();
            var MarksObt = $("#txtMarksObt").val();
            if ($("#txtMarksObt").val() == "" || $("#txtMarksObt").val() == "0") {
                alert("Please Select Marks");
                return false;
            }

            var Percentage = $("#txtPercentage").val();
            var MaxMarks = $("#tblMaxMarks").val();
            if ($("#tblMaxMarks").val() == "" || $("#tblMaxMarks").val() == "0") {
                alert("Please Select Marks");
                return false;
            }
            var dataToSend = {};
            dataToSend.RollNo = RollNo;
            dataToSend.MarksObt = MarksObt;
            dataToSend.Percentage = Percentage;
            dataToSend.P_Id = P_Id;
            dataToSend.MaxMarks = MaxMarks;
            $.ajax({
                type: "POST",
                url: '/api/v1/Fees/UpdateEducation',
                /* url: '/UG/api/v1/Fees/UpdateEducation',*/
                data: dataToSend,
                success: function (data) {
                    if (data) {
                        swal('Education  detail Update successfully');

                        //$("#txtSubHeadName").val('');
                        //$('input[name="' + "iswaive" + '"][value="' + "NO" + '"]').prop('checked', true);
                        //$('input[name="' + "isactive" + '"][value="' + "ACTIVE" + '"]').prop('checked', true);
                        GetCollegeMaster("0");
                        $("#EduRagistration").val("");
                        $("#txtRollNo").val("");
                        $("#txtMarksObt").val("");
                        $("#tblMaxMarks").val("");
                        $("#txtPercentage").val("");
                        $('#EdeQualification4').hide();
                        $("#txtPercentage").prop("disabled", false);
                        $("#EduRagistration").prop("disabled", false);
                        $("#txtRollNo").prop("disabled", false);
                        $('#EdeQualification1').hide();
                        $('#EdeQualification2').hide();
                        $('#EdeQualification3').hide();
                        $('#EdeQualification4').hide();
                        $('#EdeQualification5').hide();
                    }
                },
                error: function () {
                    swal('Failed');
                }
            });
        };
        //$("#txtMarksObt").myFunction(function () {
        //    alert();
        //});
        var perc = "";
        function myFunction() {

            var MarksObt = parseInt($('#txtMarksObt').val());
            var MaxMarks = parseInt($('#tblMaxMarks').val());
            perc = "";
            if (MaxMarks >= MarksObt && MarksObt > 0) {
                perc = ((MarksObt / MaxMarks) * 100).toFixed(2);

            }

            else {
                perc = " ";
                alert("Marks Obtained cannot be greater then Maximum Marks")
            }


            $('#txtPercentage').val(perc);
            $("#txtPercentage").prop("disabled", true);
        }
        function Clear() {

            /*$("#txtRagiste").val("");*/
            $("#txtCandidateName").val("");
            $("#txtFName").val("");
            $("#txtmother").val("");
            $("#txtmobile").val("");
            $("#DOB").val("");

        }
        function ValidateDOB(dateString) {
            var parts = dateString.split("/");
            var dtDOB = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
            var dtCurrent = new Date("10/01/2021");
            if (dtCurrent.getFullYear() - dtDOB.getFullYear() < 14) {
                alert("Eligibility minimum age limit is 14 years.");
                return false;
            }

            if (dtCurrent.getFullYear() - dtDOB.getFullYear() == 14) {
                if (dtCurrent.getMonth() < dtDOB.getMonth()) {
                    alert("Eligibility minimum age limit is 14 years.");
                    return false;
                }
                if (dtCurrent.getMonth() == dtDOB.getMonth()) {
                    if (dtCurrent.getDate() < dtDOB.getDate()) {
                        alert("Eligibility minimum age limit is 14 years.");
                        return false;
                    }
                }
            }
            return true;
        }
        $(function () {

            $("#DOB").datepicker({
                changeMonth: true,
                yearRange: '1950:2021',
                minDate: '01/01/1950',
                maxDate: '31/12/2021',
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                controlType: 'select',
                //timeFormat: 'HH:mm:ss',
                showOn: 'focus',
                showButtonPanel: true,
                closeText: 'Clear', // Text to show for "close" button
                onSelect: function (dateString, txtDate) {
                    var t = ValidateDOB(dateString);
                    if (t == false) {
                        $(this).val('');
                    }
                },
                onClose: function () {
                    var event = arguments.callee.caller.caller.arguments[0];
                    //If "Clear" gets clicked, then really clear it
                    if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                        $(this).val('');
                    }
                }
            });
            $("#DOB2").datepicker({
                changeMonth: true,
                yearRange: '1950:2021',
                minDate: '01/01/1950',
                maxDate: '31/12/2021',
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                controlType: 'select',
                //timeFormat: 'HH:mm:ss',
                showOn: 'focus',
                showButtonPanel: true,
                closeText: 'Clear', // Text to show for "close" button
                onSelect: function (dateString, txtDate) {
                    var t = ValidateDOB(dateString);
                    if (t == false) {
                        $(this).val('');
                    }
                },
                onClose: function () {
                    var event = arguments.callee.caller.caller.arguments[0];
                    //If "Clear" gets clicked, then really clear it
                    if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                        $(this).val('');
                    }
                }
            });
        });
        //function Validation(obj) {
        //};
        function updatedetils() {
            debugger

            if (document.getElementById('yesCheck').checked == false && document.getElementById('yesCheckFather').checked == false && document.getElementById('yesCheckMother').checked == false && document.getElementById('yesCheckDOB').checked == false) {
                alert("Please select at least one parameter to update.");
                return false;

            }

            if (document.getElementById('yesCheck').checked && $("#txtCandidateName").val() == "" || $("#txtCandidateName").val() == "0") {
                alert("Please Enter Trainee Name");
                document.getElementById("<%=txtCandidateName.ClientID%>").focus();
                return false;

            }
            if (document.getElementById('yesCheckFather').checked && $("#txtFName").val() == "" || $("#txtFName").val() == "0") {
                alert("Please Enter Father Name");
                document.getElementById("<%=txtFName.ClientID%>").focus();
                return false;

            }
            if (document.getElementById('yesCheckMother').checked && $("#txtmother").val() == "" || $("#txtmother").val() == "0") {
                alert("Please Enter Mother Name");
                document.getElementById("<%=yesCheckMother.ClientID%>").focus();
                return false;

            }
            var DOB = $('#DOB').val();
            if (document.getElementById('yesCheckDOB').checked && DOB == '' || DOB == null) {
                $("#DOB").focus();
                swal("Alert!", "Please enter your DOB.", "warning")
                $('#DOB_error').html("* required");
            }


            var countCount = 0;
            if (document.getElementById('yesCheck').checked) {
                countCount++;

            }
            if (document.getElementById('yesCheckFather').checked) {
                countCount++;
            }
            if (document.getElementById('yesCheckMother').checked) {
                countCount++;

            }
            if (document.getElementById('yesCheckDOB').checked) {
                countCount++;

            }
            var Candidate = document.getElementById('yesCheck').checked;
            var Father = document.getElementById('yesCheckFather').checked;
            var Mother = document.getElementById('yesCheckMother').checked;
            var checkDOB = document.getElementById('yesCheckDOB').checked

            var LblName = $("#lblCandidateName").text();
            var LblFName = $("#lblFName").text();
            var Lblmother = $("#lblmother").text();
            var LblDOB2 = $("#DOB2").val();



            var Ragiste = $("#txtRagiste").val();
            var CandidateName = $("#txtCandidateName").val();
            var FName = $("#txtFName").val();
            var mother = $("#txtmother").val();
            var mobile = $("#txtmobile").val();
            var X = $("#DOB").val().split('/');
            var dd = new Date(X[2] + '-' + X[1] + '-' + X[0]);
            var DOB = dd.getFullYear() + '-' + (dd.getMonth() + 1) + '-' + dd.getDate();

            var X = $("#DOB2").val().split('/');
            var dd2 = new Date(X[2] + '-' + X[1] + '-' + X[0]);
            var DOBnew = dd.getFullYear() + '-' + (dd2.getMonth() + 1) + '-' + dd2.getDate();

            var CollegeStatus = '<%=HttpContext.Current.Session["UserType"]%>';
            var dataToSend = {};
            dataToSend.RegId = Ragiste;
            dataToSend.CandidateName = CandidateName;
            dataToSend.FatherName = FName;
            dataToSend.motherName = mother;
            /*dataToSend.MobileNo = mobile;*/
            dataToSend.DOB = DOB;
            dataToSend.CountCount = countCount;

            dataToSend.Candidate = Candidate;
            dataToSend.Father = Father;
            dataToSend.Mother = Mother;
            dataToSend.checkDOB = checkDOB;
            dataToSend.LblName = LblName;
            dataToSend.LblFName = LblFName;
            dataToSend.Lblmother = Lblmother;
            dataToSend.LblDOB2 = DOBnew;
            dataToSend.CollegeStatus = CollegeStatus;
            $.ajax({
                type: "POST",
               /* url: '/api/v1/Fees/Updatepersonals',*/
                 url: '/UG/api/v1/Fees/Updatepersonals',
                data: dataToSend,
                success: function (data) {
                    if (data) {
                        if (data == '2') {
                            swal('You have already updated Trainee  detail.');
                        }
                        if (data == '1') {
                            swal('Trainee  detail updated successfully');
                            $('#Alldetails').hide();
                            $('#txtRagiste').val("");
                            $('#txtCandidateName').val("");
                            $('#txtFName').val("");
                            $('#txtmother').val("");
                            $('#txtmobile').val("");
                            $('#lblCandidateName').text("");
                            $('#lblFName').text("");
                            $('#lblmother').text("");
                            $("#DOB2").val("");
                            $('#lblITIName').text("");
                            $('#details').hide();
                            document.getElementById("yesCheck").disabled = false;
                            document.getElementById("yesCheck").checked = false;
                            document.getElementById('ifYes').style.visibility = 'hidden';

                            document.getElementById("yesCheckFather").disabled = false;
                            document.getElementById("yesCheckFather").checked = false;
                            document.getElementById('ifYesFather').style.visibility = 'hidden';

                            document.getElementById("yesCheckMother").disabled = false;
                            document.getElementById("yesCheckMother").checked = false;
                            document.getElementById('ifYesMother').style.visibility = 'hidden';

                            document.getElementById("yesCheckDOB").disabled = false;
                            document.getElementById("yesCheckDOB").checked = false;
                            document.getElementById('ifYesDOB').style.visibility = 'hidden';

                            $('#Edu_box').hide();

                        }
                        if (data == '00') {
                            swal('Only two parameters are allowed to be updated');
                        }
                        if (data == '3') {
                            swal('Only two parameters are allowed to be updated.');
                        }
                        /* GetCollegeMaster(Ragiste)*/


                    }
                },
                error: function () {
                    swal('Failed');
                }
            });
        };

        function yesnoCheck(C) {
            var countCeckBoc = 0;
            if (document.getElementById('yesCheck').checked) {
                var CheckCandidate = yesCheck.name;
                countCeckBoc++;
                document.getElementById('ifYes').style.visibility = 'visible';
                // alert(countCeckBoc);
            }
            else {
                $('#txtCandidateName').val("");
                document.getElementById('ifYes').style.visibility = 'hidden';

            }
            if (document.getElementById('yesCheckFather').checked) {
                countCeckBoc++;
                // alert(countCeckBoc);
                document.getElementById('ifYesFather').style.visibility = 'visible';
            }
            else {
                $('#txtFName').val("");
                document.getElementById('ifYesFather').style.visibility = 'hidden';

            }
            if (document.getElementById('yesCheckMother').checked) {
                countCeckBoc++;
                document.getElementById('ifYesMother').style.visibility = 'visible';
                //alert(countCeckBoc);
            }
            else {
                $('#txtmother').val("");
                document.getElementById('ifYesMother').style.visibility = 'hidden';

            }
            if (document.getElementById('yesCheckDOB').checked) {
                countCeckBoc++;
                document.getElementById('ifYesDOB').style.visibility = 'visible';
                //alert(countCeckBoc);
            }
            else {
                document.getElementById('ifYesDOB').style.visibility = 'hidden';

            }

            if (countCeckBoc <= 2) {
                // if (C == 'C') {
                var UserType = '<%=HttpContext.Current.Session["UserType"]%>';
                if (UserType == '2') {
                    if (document.getElementById('yesCheck').checked && document.getElementById('yesCheckFather').checked) {

                        document.getElementById("yesCheckMother").disabled = true;
                        document.getElementById("yesCheckMother").checked = false;

                        document.getElementById("yesCheckDOB").disabled = true;
                        document.getElementById("yesCheckDOB").checked = false;

                        return false;
                    }
                    if (document.getElementById('yesCheck').checked && document.getElementById('yesCheckMother').checked) {

                        document.getElementById("yesCheckFather").disabled = true;
                        document.getElementById("yesCheckFather").checked = false;

                        document.getElementById("yesCheckDOB").disabled = true;

                        return false;

                    }
                    if (document.getElementById('yesCheck').checked && document.getElementById('yesCheckDOB').checked) {

                        document.getElementById("yesCheckFather").disabled = true;
                        document.getElementById("ifYesFather").checked = false;

                        document.getElementById("yesCheckMother").disabled = true;
                        document.getElementById("ifYesMother").checked = false;

                        return false;
                    }
                    if (document.getElementById('yesCheckFather').checked && document.getElementById('yesCheckMother').checked) {

                        document.getElementById("yesCheck").disabled = true;
                        document.getElementById("ifYes").checked = false;

                        document.getElementById("yesCheckDOB").disabled = true;
                        document.getElementById("ifYesDOB").checked = false;

                        return false;
                    }
                    if (document.getElementById('yesCheckFather').checked && document.getElementById('yesCheckDOB').checked) {

                        document.getElementById("yesCheck").disabled = true;
                        document.getElementById("ifYes").checked = false;

                        document.getElementById("yesCheckMother").disabled = true;
                        document.getElementById("ifYesMother").checked = false;

                        return false;
                    }
                    if (document.getElementById('yesCheckMother').checked && document.getElementById('yesCheckDOB').checked) {

                        document.getElementById("yesCheck").disabled = true;
                        document.getElementById("ifYes").checked = false;

                        document.getElementById("yesCheckFather").disabled = true;
                        document.getElementById("ifYesFather").checked = false;

                        return false;
                    }
                    else {

                        document.getElementById("yesCheck").disabled = false;
                        document.getElementById("yesCheckFather").disabled = false;
                        document.getElementById("yesCheckMother").disabled = false;
                        document.getElementById("yesCheckDOB").disabled = false;

                    }
                }
            }
            else {
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <div id="cover">
            <i class="fa fa-spinner fa-spin" style="font-size: xx-large"></i>
        </div>--%>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
        </div>

        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2"></div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-8 text-center top-heading" style="margin-top: 5px;">
                        <h4 class="heading">Update Trainee  Detail</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary" onclick="javascript: window.history.back();"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-grid-table-down">
                <div class="bg-white rounded shadow mb-5">

                    <ul id="myTab2" role="tablist" class="nav nav-tabs nav-pills with-arrow lined flex-column flex-sm-row text-center">
                        <li class="nav-item flex-sm-fill">
                            <a id="PerDeta" data-toggle="tab" href="#tab1" role="tab" aria-controls="tab1" aria-selected="true" class="nav-link text-uppercase font-weight-bold mr-sm-3 rounded-0 active">Personal Detals</a>
                        </li>
                        <%-- <li class="nav-item flex-sm-fill">
                            <a id="edudetails" data-toggle="tab" href="#tab2" role="tab" aria-controls="tab2" aria-selected="false" class="nav-link text-uppercase font-weight-bold mr-sm-3 rounded-0">Education Detals</a>
                        </li>--%>
                        <%--<li class="nav-item flex-sm-fill">
                            <a id="photo" data-toggle="tab" href="#tab3" role="tab" aria-controls="tab3" aria-selected="false" class="nav-link text-uppercase font-weight-bold rounded-0">Update Photo</a>
                        </li>
                        <li class="nav-item flex-sm-fill">
                            <a id="NCVTData" data-toggle="tab" href="#tab4" role="tab" aria-controls="tab4" aria-selected="false" class="nav-link text-uppercase font-weight-bold rounded-0">Update NCVT gtp api Data</a>
                        </li>--%>
                    </ul>

                    <div id='OnlyCan' style="text-align: center; font-weight: bold;color:Highlight; display: none;">
                       <b> Note:   
                    Detail of only Admitted Trainees can be updated. Only two parameters are allowed to be updated and updation is allowed once.</b>
                    </div>
                    <div id="sec_box" style="display: none;">
                        <div class="cus-top-section" style="margin-top: 20px;">
                            <div class="row">
                                <div class="col-lg-4 col-md-4">
                                    <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                                </div>
                                <div class="col-lg-5 col-md-5 col-sm-5 col-12">
                                    <asp:TextBox ID="txtRagiste" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off" onchange="javascript: txtChanged(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRagiste"
                                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic" ControlToValidate="txtRagiste"
                                        CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                              
                                    </div>
                                
                                <div class="col-lg-3 col-md-3 col-sm-3 col-12">
                                    <input type="button" id="btnGo" value="Go" class="mygoclass" onclick="GetPersonalDedail()" />
                                    <%-- <input type="button" id="btnCancel" value="Cancel" class="mygoclass" />--%>
                                    <%-- <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go"  ValidationGroup="B" OnClientClick="return GetPersonalDedail()"/>--%>
                                </div>

                            </div>

                        </div>
                         
                               <b> <div style="text-align:center; margin-bottom:-14px">ITI Name: <asp:Label ID="lblITIName" runat="server"></asp:Label>
							   </b>
							   </div>
                        <div id="Alldetails" class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                            <div class="row cus-fee-top-section">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-10">
                                                <asp:CheckBox ID="yesCheck" runat="server" onclick="javascript:yesnoCheck('C');" />
                                                <label>Trainee  Name:</label><span style="color: red"></span>


                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-12" style="word-break:break-all;">
                                                <asp:Label ID="lblCandidateName" runat="server"></asp:Label>
                                                <%--  <asp:TextBox ID="txtCandidateName" CssClass="form-control" runat="server" onkeypress="return AllowAlphabet(event)"></asp:TextBox>--%>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-12">
                                                <div id="ifYes" style="visibility: hidden">
                                                    <asp:TextBox ID="txtCandidateName" CssClass="form-control" runat="server" MaxLength="100" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-10" style="word-break:break-all;">
                                                <asp:CheckBox ID="yesCheckFather" runat="server" onclick="javascript:yesnoCheck('F');" />
                                                <label>Father Name:</label><span style="color: red"></span>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-12">
                                                <%--<asp:TextBox ID="txtFName" CssClass="form-control" runat="server" onkeypress="return AllowAlphabet(event)"></asp:TextBox>--%>
                                                <asp:Label ID="lblFName" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-12">
                                                <div id="ifYesFather" style="visibility: hidden">
                                                    <asp:TextBox ID="txtFName" CssClass="form-control" runat="server" MaxLength="100" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-10" style="word-break:break-all;">
                                                <asp:CheckBox ID="yesCheckMother" runat="server" onclick="javascript:yesnoCheck('M');" />
                                                <label>Mother Name:</label><span style="color: red"></span>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-12">
                                                <%--<asp:TextBox ID="txtmother" CssClass="form-control" runat="server" onkeypress="return AllowAlphabet(event)"></asp:TextBox>--%>
                                                <asp:Label ID="lblmother" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-12">
                                                <div id="ifYesMother" style="visibility: hidden">
                                                    <asp:TextBox ID="txtmother" CssClass="form-control" runat="server" MaxLength="100" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-10">
                                                <asp:CheckBox ID="yesCheckDOB" runat="server" onclick="javascript:yesnoCheck('DOB');" />
                                                <label for="DOB"  color: #34495E;">Date of Birth:<span style="color: red;"><strong></strong></span></label>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-12">
                                                <input type="text" autocomplete="off" id="DOB2" name="DOB2" />

                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-12 m-b-12">

                                                <div id="ifYesDOB" style="visibility: hidden">

                                                    <input placeholder="Enter Date..." type="text" autocomplete="off" class="form-control input-shadow" onkeydown="return false" id="DOB" name="DOB" />
                                                    <span class="help-block badge badge-success">(For Ex. DD/MM/YYYY)</span><span id="DOB_error" style="color: red"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="details" class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="display: none">
                                    <%--<asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success" ValidationGroup="A"  OnClick="btnSubmit_Click" />--%>
                                    <%-- <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success" ValidationGroup="A" OnClientClick="return Validation(this);" OnClick="btnSubmit_Click" />
                                    --%>
                                    <input type="button" id="btnSubmit" class="mygoclass" value="Update" onclick="updatedetils()" />
                                    <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger"  OnClientClick="return Clear()"/>--%>
                                    <%--<input type="button" id="btnCancel" value="Cancel"/>--%>
                                </div>
                            </div>

                        </div>


                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 " style="display: none;">
                            <div class="row cus-fee-top-section">
                                <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10" >
                                    <label>Mobile:</label><span style="color: red">*</span>
                                    <asp:TextBox ID="txtmobile" CssClass="form-control" runat="server" MaxLength="12" onkeypress="return AllowNumeric(event)"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtmobile"
                                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Mobile No" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ControlToValidate="txtmobile"
                                        CssClass="badge badge-danger" ErrorMessage="Invalid Mobile No" ValidationExpression="^[0-9]{10,10}$" ValidationGroup="A"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="Edu_box" style="display: none;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                            <div class="cus-top-section" style="margin-top: 20px;">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4">

                                        <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                        <asp:TextBox ID="EduRagistration" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="EduRagistration"
                                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="B"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ControlToValidate="EduRagistration"
                                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="col-lg-2 col-md-2 col-sm-2 col-12">
                                        <input type="button" id="btnGo2" value="Go" class="mygoclass" onclick="GetEduDedail()" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cus-fee-top-section">
                                <%--<div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                                    <label>Registration No:</label><span style="color: red">*</span>
                                    <asp:TextBox ID="EduRagistration" CssClass="form-control" runat="server" MaxLength="100" onchange="javascript: txtChangededu(this);"></asp:TextBox>
                                </div>--%>

                                <div id="EdeQualification1" class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10" style="display: none;">
                                    <label>Roll No:</label><span style="color: red"></span>
                                    <asp:TextBox ID="txtRollNo" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                </div>
                                <div id="EdeQualification2" class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10" style="display: none;">
                                    <label>MarksObt:</label><span style="color: red"></span>
                                    <asp:TextBox ID="txtMarksObt" CssClass="form-control" runat="server" MaxLength="100" onchange="return myFunction()" onkeypress="return AllowNumeric(event)"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMarksObt"
                                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Marks" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtMarksObt"
                                        CssClass="badge badge-danger" ErrorMessage="Invalid Marks" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div id="EdeQualification5" class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10" style="display: none;">
                                    <label>MaxMarks:</label><span style="color: red"></span>
                                    <asp:TextBox ID="tblMaxMarks" CssClass="form-control" runat="server" MaxLength="100" onchange="return myFunction()" onkeypress="return AllowNumeric(event)"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tblMaxMarks"
                                        CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Marks" ValidationGroup="A"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic" ControlToValidate="tblMaxMarks"
                                        CssClass="badge badge-danger" ErrorMessage="Invalid Marks" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div id="EdeQualification3" class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10" style="display: none;">
                                    <label>Percentage:</label><span style="color: red"></span>
                                    <asp:TextBox ID="txtPercentage" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                </div>
                                <div id="EdeQualification4" class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="margin-top: 2%; margin-bottom: 5%; display: none">
                                    <%-- <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-success" OnClientClick="return updateData()" />--%>
                                    <input type="button" id="btnupdate" value="Update" class="mygoclass" onclick="updateData()" />
                                    <%-- <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-outline-danger"  />--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-grid-table">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-grid-table">
                                            <asp:HiddenField ID="hiddenID" runat="server" />
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-sub-table-section">
                                                <table id="showdata" class="table table-bordered table-hover">
                                                </table>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="Photo_box" style="display: none;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                            <div class="cus-top-section" style="margin-top: 20px;">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4">

                                        <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                        <asp:TextBox ID="txtphoto" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtphoto"
                                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="B"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtphoto"
                                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-12">
                                        <input type="button" id="btnGo3" value="Go" class="mygoclass" onclick="GetPhoto()" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cus-fee-top-section" id="divphoto" style="display: none;">
                                <div class="col-lg-3 col-md-3 col-sm-3 col-12"></div>
                                <div id="EdeQualification11" class="col-lg-3 col-md-3 col-sm-3 col-6">
                                    <a href="#" class="pop">
                                        <img src="../assets/images/no-img.png" class="img-fluid img-thumbnail" id="myUploadedImg" alt="Photo" style="width: 180px;" /><br />
                                    </a>

                                    <input type="file" class="mygoclass" id="f_UploadImage">
                                    <input type="button" class="mygoclass" value="Update" id="but_upload" onclick='validation(this)'><br />
                                    <label class="large-label" for="your-name"><a>(jpeg/jpg/png max size 150Kb)</a></label>

                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-12"></div>

                            </div>

                        </div>
                    </div>
                    <div id="NCVTData_box" style="display: none;">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                            <div class="cus-top-section" style="margin-top: 20px;">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4">

                                        <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                        <asp:TextBox ID="txtNCVTData" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNCVTData"
                                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="B"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtNCVTData"
                                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-2 col-12">
                                        <input type="button" id="btnGo4" value="Go" class="mygoclass" onclick="GetNCVTData()" />
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="row cus-fee-top-section" id="divphoto" style="display: none;">
                                <div class="col-lg-3 col-md-3 col-sm-3 col-12"></div>
                                <div id="EdeQualification11" class="col-lg-3 col-md-3 col-sm-3 col-6">
                                    <a href="#" class="pop">
                                        <img src="../assets/images/no-img.png" class="img-fluid img-thumbnail" id="myUploadedImg" alt="Photo" style="width: 180px;" /><br />
                                    </a>

                                    <input type="file" class="mygoclass" id="f_UploadImage">
                                    <input type="button" class="mygoclass" value="Update" id="but_upload" onclick='validation(this)'><br />
                                    <label class="large-label" for="your-name"><a>(jpeg/jpg/png max size 150Kb)</a></label>

                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3 col-12"></div>

                            </div>--%>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                            <div class="row cus-fee-top-section">


                                <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                                    <label>Mobile:</label><span style="color: red">*</span>
                                    <asp:TextBox ID="txtNCVTMO" CssClass="form-control" runat="server" MaxLength="12" onkeypress="return AllowNumeric(event)"></asp:TextBox>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                                    <label>Email Id:</label><span style="color: red">*</span>
                                    <asp:TextBox ID="txtNCVTEmail" CssClass="form-control" runat="server" onkeypress="return AllowAlphabet(event)"></asp:TextBox>
                                </div>


                                <div id="MoEm" class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="display: none">

                                    <input type="button" id="btnSubmit1" class="mygoclass" value="Update" onclick="updateNCVTDetails()" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- End Footer -->
    </form>
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

    <script src="../assets/js/Verification_main.js"></script>
    <script type="text/javascript" src="../assets/js/bootstrap/Js/bootstrap.min.js"></script>

    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <img src="" class="imagepreview" style="width: 100%;">
                </div>
            </div>
        </div>
    </div>
</body>
</html>
