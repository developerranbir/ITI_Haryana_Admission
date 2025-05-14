<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmViewITIs.aspx.cs" Inherits="HigherEducation.DHE.frmViewITIs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ITI List</title>
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
    <style>
        .GridViewStyle {border:1px solid #ddd; border-collapse:collapse; font-family:Arial, sans-serif; table-layout:auto; font-size:14px ; }


/*Header*/

.HeaderStyle {border:1px, solid, #ddd; background-color:#938ede ; }

 

.HeaderStyle th {padding:5px 0px 5px 0px; color:#333; text-align:center ; }


/*Row*/

tr.RowStyle{text-align:center; background-color:#ffffff ; padding:10px;}

tr.RowStyle:hover {cursor:pointer; background-color:#f69542;}

 



/*Footer*/

.FooterStyle {background-color:#938ede; height:25px;}


/*Pager*/

.PagerStyle table { margin:auto;border:none;}

 

tr.PagerStyle {text-align:center; background-color:#ddd;}

 

.PagerStyle table td {border:1px; padding:5px ; }

 

.PagerStyle a {border:1px solid #fff; padding:2px 5px 2px 5px; color:#333; text-decoration:none;}

 

.PagerStyle span {padding:2px 5px 2px 5px; color:#000; font-weight:bold; border:2px solid #938ede;}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-11 col-md-11 col-sm-11 col-11">
                        <h4 class="heading">All ITI List</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div style="overflow-x:auto;margin-top:20px;margin-bottom:50px;">
                <asp:GridView ID="grdITIList" runat="server" Width="100%" AllowPaging="true" PageSize="15" OnPageIndexChanging="grdITIList_PageIndexChanging" HorizontalAlign="Center">  
                 <HeaderStyle CssClass="HeaderStyle" />
                 <FooterStyle CssClass="FooterStyle" />
                 <RowStyle CssClass="RowStyle" />
                 <PagerStyle CssClass="PagerStyle" />
            </asp:GridView>
                </div>
        </div>
    </form>
    <footer id="footer">

    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-12 text-center">
                <div class="row">
                    <div class="col-lg-10 col-md-10 text-left" style="margin-top:10px;">
                        <div class="credits">
                            <a>Site is technically designed, hosted and maintained by National Informatics Centre, Haryana</a>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2">
                        <img src="/assets/images/nic-logo.png" style="width:100px;">
                    </div>
                </div>
            </div>

        </div>

    </div>
</footer>
</body>
</html>
