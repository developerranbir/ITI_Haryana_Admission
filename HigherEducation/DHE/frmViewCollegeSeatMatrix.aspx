<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmViewCollegeSeatMatrix.aspx.cs" Inherits="HigherEducation.HigherEducations.frmViewCollegeSeatMatrix" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ITI Seat Matrix</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
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
  


   
 

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
            <div class="row header">
            <div class="col-xl-8 col-lg-8 col-md-6 col-sm-12 col-12">
                <nav class="nav-menu d-none d-lg-block">
                    <ul class="list">
                        <li><a href="https://admissions.itiharyana.gov.in/"><span class="english">Home</span><span class="hindi" style="display:none;">होम</span></a></li>
                        <li><a href="frmDistrictWiseCollege.aspx"><span class="english">District Wise College</span><span class="hindi" style="display: none;">जिला वार कॉलेज</span></a></li>
                       <li><a href="frmResultUGAdmissions.aspx"><span class="english">Know Your Result</span><span class="hindi" style="display: none;">अपना परिणाम जानिए</span></a></li>
                        <li class="active"><a href="frmViewCollegeSeatMatrix.aspx"><span class="english">View Seat Matrix</span><span class="hindi" style="display:none;">सीट मैट्रिक्स</span></a></li>
                   
                    </ul>

                </nav><!-- .nav-menu -->
            </div>
 
        </div>
        </div>
        <div class="container-fluid">

            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading">
                <h4 class="heading">ITI Seat Matrix</h4>
            </div>

            <div class="cus-top-section" style="margin-top: 30px;">
                <div class="row">
                                     
                    <label class="col-lg-2 col-md-2 col-sm-3 col-12 cus-select-label">Select ITI:</label>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-12">

                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCollege"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="A"></asp:RequiredFieldValidator>
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
                  <!--  <div class="col-lg-12 col-md-12 col-sm-12 col-12" id="dvFullForm" runat="server" style="display: none">
                             <span style="font-size: 13px;"><strong>OPEN GEN:</strong>Open General Category, <strong>Open GEN-ESM:</strong>Open General Category for Ex-Servicemen, <strong>BC:</strong>Backward Class Category, <strong>ESM-BC:</strong>Ex-Servicemen Backward Class Category, <strong>SC:</strong>Scheduled Caste Category, <strong>ESM-SC:</strong>Ex-Servicemen Scheduled Class Category, 
                             <strong>EWS:</strong>Economically Weaker Section Category, <strong>PH:</strong>Physical Handicap Category
                        </span>
                      </div>
-->
                    <div class="table-responsive cus-grid-table-four">
                          <asp:GridView ID="GridView3" ShowFooter="true" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">

                            <Columns>
                                 <asp:TemplateField HeaderText="Sr.No" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrno" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Trade Section Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("sectionname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Seats" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalSeats" runat="server" Text='<%# Eval("totalseats") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Total Vacant Seats" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalVacantSeats" runat="server" Text='<%# Eval("totalvacantseats") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OPEN GEN" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOPENGEN" runat="server" Text='<%# Eval("OPENGEN") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OPEN GEN-ESM" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOPENGENESM" runat="server" Text='<%# Eval("OPENGENESM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BC" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBC" runat="server" Text='<%# Eval("BC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ESM-BC" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblESMBC" runat="server" Text='<%# Eval("ESMBC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SC" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSC" runat="server" Text='<%# Eval("SC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ESM-SC" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblESMSC" runat="server" Text='<%# Eval("ESMSC") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EWS" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEWS" runat="server" Text='<%# Eval("EWS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="PH" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPH" runat="server" Text='<%# Eval("PH") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                
                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="Red" />
                        </asp:GridView>

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
