<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmStudentInfo.aspx.cs" Inherits="HigherEducation.HigherEducations.frmStudentInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Detail</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/jqueryui.css" rel="stylesheet" />
   <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet"/>

    <link href="../assets/css/stylecancel.css" rel="stylesheet" />
    <%--<script src="../assets/js/jquery-3.4.1.js"></script>--%>
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>

   



</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
        </div>
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-12">
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Student Details</h4>
                    </div>
                     <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
             <div class="col-lg-12 col-md-12 col-sm-12 col-12 ">
            <div class="cus-top-section" style="margin-top: 20px;">
                <div class="row">
                   
                        <div class="col-lg-8 col-md-8 col-sm-6 col-12">  
                            <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                        <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="20" runat="server" autocomplete="off" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id." ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9\s]{1,20}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                    </div>
                        

                    <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn" style="margin-bottom:20px;padding: 30px;">
                        <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click" ValidationGroup="B" />
                        <asp:HiddenField ID="hdRefId" runat="server" />
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
