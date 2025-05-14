<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadCollegeProspectus.aspx.cs" Inherits="HigherEducation.HigherEducations.UploadCollegeProspectus" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <title>ITI Prospectus</title>
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

    <script>
        $(document).ready(function () {
         
            var prosdoc = $('#<%=hdProsDoc.ClientID%>').val();
            if (prosdoc != "") {
                $('#btnPros').show();
            }
            else {
                $('#btnPros').hide();
            }
        });
        function downloadPDF(pdf) {
                const linkSource = `data:application/pdf;base64,${pdf}`;
                const downloadLink = document.createElement("a");
                const fileName = "Prospectus.pdf";
                downloadLink.href = linkSource;
                downloadLink.download = fileName;
                downloadLink.click();
           
        }
        function previewFile() {
           // debugger;
            var preview = document.querySelector('#<%=Image1.ClientID%>');
            var file = document.querySelector('#<%=File_Upload.ClientID%>').files[0];
            var reader = new FileReader();
            reader.onloadend = function () {

                preview.src = reader.result;
            }
            if (file) {

                var ext = file.name.substr(-3);//$('#File1').val().split('.').pop();
                if (ext == "pdf") {
                    preview.src = "../assets/images/preview.png";
                    //preview.src = URL.createObjectURL(file);

                    // $('#Image1').hide();

                }
                else {
                    reader.readAsDataURL(file);
                }

            }
            else {
                preview.src = "../assets/images/preview.png";
            }

        }

        function image1_preview() {
          //  debugger;
            var file = document.querySelector('#<%=File_Upload.ClientID%>').files[0];
             if (file) {

                 var ext = file.name.substr(-3);
                 if (ext == "pdf") {

                     $("#myModal1").modal('show');
                     $("#iframepdf").attr('src', URL.createObjectURL(file));
                     $("#iframepdf").show();
                 }
                

            }
             else if($('#hdPros').val()=='p'){

                 $("#myModal1").modal('show');
                 $("#iframepdf").show();
             }
         }
       function ValidationFile1() {
            //debugger;
            //Check FileUpload Validation
           

            if ($('#<%=File_Upload.ClientID%>').val() != "") {

                var allowedFiles = [".pdf"];
                var fileUpload = $('#<%=File_Upload.ClientID%>');

                var filename1 = $('#<%=File_Upload.ClientID%>').val().replace(/C:\\fakepath\\/i, '');

                var filename = filename1.split('.').length - 1;
                var lblError = $("#lblError");
                var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
                if (!regex.test(fileUpload.val().toLowerCase()) || parseInt(filename) > 1) {
                    lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");

                    swal("", "Invalid filename/format", "");
                    return false;
                }
                else {
                    document.getElementById("<%= lblError.ClientID %>").innerText = ""
                }

                if (typeof ($('#<%=File_Upload.ClientID%>')[0].files) != "undefined") {
                    var size = parseInt($('#<%=File_Upload.ClientID%>')[0].files[0].size / 1024);
                    if (size > 5120) {
                        lblError.html("Prospectus size should not be greater than " + "5 MB");
                        swal("", "Prospectus size should not be greater than " + "5 MB", "");
                        $('#<%=File_Upload.ClientID%>').val('');
                        return false;
                    }

                }
                else {
                    document.getElementById("<%= lblError.ClientID %>").innerText = ""
                    return true;
                }

            }
            else {
                document.getElementById("<%= lblError.ClientID %>").innerText = ""
                return true;
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
        </div>
        <div class="container-fluid">

            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1"></div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Upload ITI Prospectus</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
                <div class="row cus-fee-top-section">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Name of ITI:</label>
                        <asp:TextBox ID="txtCollegeName" CssClass="form-control" disabled="true" runat="server"></asp:TextBox>

                    </div>
                  <%--  <div class="col-lg-3 col-md-3 col-sm-4 col-12 m-b-10">
                        <label>Affiliated With:</label>
                        <asp:TextBox ID="txtAffiliated" CssClass="form-control" disabled="true" runat="server"></asp:TextBox>
                    </div>--%>
                     <div class="col-lg-12 col-md-12 col-sm-12 col-12 m-b-10">
                        <label>Prospectus:</label><br />
                        <asp:Image ID="Image1" runat="server" Height="100" ImageUrl="~/assets/images/preview.png" Width="120" CssClass="img-fluid" onclick="image1_preview();" />
                        <asp:FileUpload ID="File_Upload" runat="server" onchange="previewFile(); ValidationFile1();" />
                        <asp:Label ID="lblFilename" CssClass="col-form-label" runat="server"></asp:Label><br />
                        <asp:Label ID="lblFile1Msg" runat="server" Text="(Only upload .pdf format. Max size upto 5 MB.)"></asp:Label>
                        <asp:Label ID="lblError" CssClass="badge badge-danger" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdPros" runat="server" />
                    
                    
                    </div>
                </div>
            </div>
          
         
          
           
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-section-btn text-center" style="margin-top: 2%; margin-bottom: 5%;">
                <asp:Button ID="btnSubmit" runat="server" Text="Upload Prospectus" CssClass="btn btn-success" OnClick="btnSubmit_Click" OnClientClick="validationFile1(); return false;" ValidationGroup="A" />
                 <button id="btnPros" class="btn btn-success" onclick="downloadPDF($('#hdProsDoc').val()); return false;">Download Prospectus</button>
                 <asp:HiddenField ID="hdProsDoc" runat="server" />
            </div>
            <div class="modal cus-modal" id="myModal1" role="dialog">
                <div class="modal-dialog  modal-lg" style="margin-top: 1%;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row" style="width: 100%;">
                                <div style="display: inline-block; width: 70%; float: left; text-align: left">
                                    <h6 class="modal-title" style="font-weight: bold; padding-left: 15px;">Prospectus</h6>
                                </div>
                                <div style="display: inline-block; width: 28%; float: left; text-align: right">
                                    <button type="button" class="close" data-dismiss="modal" onclick="">&times;</button>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body" id="mydatamodel1" style="display: block;">

                            <iframe id="iframepdf" runat="server" src="" width="100%" height="300px"></iframe>
                            <div class="input-group input-group-sm mb-3">
                            </div>
                        </div>
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
</body>
</html>
