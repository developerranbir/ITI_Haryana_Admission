<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InactiveTradesStudents.aspx.cs" Inherits="HigherEducation.DHE.InactiveTradesStudents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student's Shift Trade</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylecancel.css" rel="stylesheet" />

    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>

    <script>


        function sweetAlertConfirm(btnShift) {

            if (btnShift.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnShift.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: 'Are you sure?',
                    text: "You won't be able to revert this!",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, shift it!'
                })
                    .then(function () {
                        // Set data-confirmed attribute to indicate that the action was confirmed
                        btnShift.dataset.confirmed = true;
                        // Trigger button click programmatically
                        btnShift.click();
                    }).catch(function (reason) {
                        // The action was canceled by the user
                    });
            }
        }
        function ConfirmOnShift() {
            var validated = Page_ClientValidate('A');
            if (validated) {

                if (confirm("Do you really want to shift trade of student?") == true)
                    return true;
                else
                    return false;
            }

        }
    </script>


    <style type="text/css">
        .modalBackground {
            background-color: rgb(192,234,255);
            filter: alpha(opacity=70);
            opacity: 0.7;
            z-index: 100 !important;
        }

        .modalPopup {
            background-color: #ffffff;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            /*width: 250px;*/
            z-index: 100;
        }

        .btnPop {
            background-color: #033280;
            color: White;
            font-size: 12px;
            font-weight: bold;
            padding-left: 5px;
        }
    </style>
    <script>
        function showCollegeInfo() {
            $("#myModal").modal('show');

        }
        function showSubjectComb() {
            $("#myModal2").modal('show');

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>

            <div class="container-fluid mt-5">
                <div class="row">
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2"></div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-8 text-center top-heading" style="margin-top: 5px;">
                        <h4 class="heading">Inactive Trade Students</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-2 text-right back-btn" style="margin-top: 5px;">
                        <a href="#" id="btnBack" class="btn btn-icon icon-left btn-primary" onclick="HEMenu.aspx"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                
                </div>
                <div class="row mt-3">
                    <div class=" col-2"></div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-4">
                        Inactive Institute:
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlInatctiveTradeClg" AutoPostBack="true" OnSelectedIndexChanged="ddlInatctiveTradeClg_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="col-4">
                        Inactive  Trade Section:
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlInactiveTradeSection"></asp:DropDownList>
                    </div>
                    <div class="col-2">
                    </div> 
                    <div class="col-12 text-center mt-3"><h3><strong>Shift To</strong></h3>
                    </div>
                </div>

     
                <div class="row">
                    <div class="col-2"></div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-4">
                        Active Institute:
                        <asp:DropDownList runat="server"  CssClass="form-control"  ID="ddlActiveClg" Enabled="false"   AutoPostBack="true"></asp:DropDownList>
                    </div>

                    <div class="col-4">
                        Active  Trade Section:
                        <asp:DropDownList runat="server"  CssClass="form-control"  ID="ddlActiveSection"></asp:DropDownList>
                    </div>
                    <div class=" col-2">
                    </div>
                </div>
                <div class="row">
                     <div class="col-12 text-center mt-3"> <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Shift" CssClass="btn  btn-success" /></div>
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
                            <img src="/assets/images/nic-logo.png" style="width: 100px;" />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
</body>
</html>
