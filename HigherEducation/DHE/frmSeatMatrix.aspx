<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmSeatMatrix.aspx.cs" Inherits="HigherEducation.HigherEducations.frmSeatMatrix" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View/Update Seat Matrix Breakup</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/styleindex.css" rel="stylesheet" />
    <link href="../assets/css/stylesearch.css" rel="stylesheet" />
    <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet" />
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>

    <style>
        input[type="number"] {
            width: 50px !important;
        }

        #footer {
            background: #225132;
            padding: 10px;
            color: #fff;
            font-size: 14px;
            text-align: center;
            position: relative;
            bottom: 0;
            width: 100%;
            margin-top: 50px;
        }
    </style>
    <script type="text/javascript">    
        function ConfirmOnChange() {
            if (confirm("Do you want to Update?") == true) {
                return true;
            }
            else {
                return false;
            }


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

        </div>
        <div class="container-fluid">

            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading text-center">
                <div class="row">
                    <div class="col-lg-1 col-md-1"></div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Seat Bifurcation Matrix</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div style="margin-top: 30px;">
                <div class="row">

                    <label class="col-lg-4 col-md-4 col-sm-12 col-12 cus-select-label">Select Scheme:</label>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-12">

                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlScheme"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>


                    <div class="col-lg-2 col-md-2 col-sm-4 col-12 cus-getbtn">
                        <asp:Button ID="btSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btSearch_Click" ValidationGroup="A" />
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12 col-md-12 col-sm-12 col-12">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-12" id="dvFullForm" runat="server" style="display: none">

                        <asp:GridView ID="grdViewUpdate" runat="server" AutoGenerateColumns="false" class="table table-borderless table-striped table-hover table-sm">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:Label ID="txtsrno" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Seat Size">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtSeatSize" runat="server" Text='<%# Eval("seat size") %>'></asp:TextBox>
                                        <asp:Label ID="lblid" CssClass="d-none" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Open-M">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtOpenM" runat="server" Text='<%# Eval("Open-M") %>'></asp:TextBox><br />
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                

                                <asp:TemplateField HeaderText="OPEN-F">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtOpenF" runat="server" Text='<%# Eval("OPEN-F") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ESM-Gen">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtESMGen" runat="server" Text='<%# Eval("ESM-Gen") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BC-A (M)">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtBCAM" runat="server" Text='<%# Eval("BC-AM") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BC-A (F)">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtBCAF" runat="server" Text='<%# Eval("BC-AF") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BC-B (M)">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtBCBM" runat="server" Text='<%# Eval("BC-BM") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BC-B (F)">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtBCBF" runat="server" Text='<%# Eval("BC-BF") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ESM-BC-A">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtESMBCA" runat="server" Text='<%# Eval("ESM-BCA") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SC-M">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtSCM" runat="server" Text='<%# Eval("SC-M") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dep. SC-M">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtDepSCM" runat="server" Text='<%# Eval("DepSC-M") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SC-F">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtSCF" runat="server" Text='<%# Eval("SC-F") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dep. SC-F">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtDepSCF" runat="server" Text='<%# Eval("DepSC-F") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SC-ESM">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtSCESM" runat="server" Text='<%# Eval("SC-ESM") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EWS-M">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtEWSM" runat="server" Text='<%# Eval("EWS-M") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EWS-F">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtEWSF" runat="server" Text='<%# Eval("EWS-F") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PH">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txtPH" runat="server" Text='<%# Eval("PH") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Total Seats">
                                    <ItemTemplate>
                                        <asp:TextBox TextMode="Number" ID="txttotalConfirm" runat="server" Text='<%# Eval("totalConfirm") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                            </Columns>

                        </asp:GridView>
                     
                                <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center" style="margin-bottom: 50px;">
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClientClick="return ConfirmOnChange();" OnClick="btnUpdate_Click" />
                                </div>
                          

                    </div>
                </div>
            </div>
        </div>


    </form>
    <!-- ======= Footer ======= 
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
                            <img src="../assets/images/nic-logo.png" style="width: 100px;" />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </footer>
    <!-- End Footer -->



</body>
</html>
