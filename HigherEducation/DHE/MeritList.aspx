<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeritList.aspx.cs" Inherits="HigherEducation.DHE.MeritList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/stylesearch.css" rel="stylesheet" />
		 <link href="../assets/vendor/icofont/icofont.min.css" rel="stylesheet">

    <link href="../assets/css/stylehome.css" rel="stylesheet" />
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>


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


</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="row main-banner">
                <img src="../assets/images/banner.jpg" style="width: 100%" alt="online admission portal" />
            </div>
            <div class="row header">
                <div class="col-xl-10 col-lg-10 col-md-10 col-sm-12 col-12">
                    <nav class="nav-menu d-none d-lg-block">
                        <ul>

							<li><a href="https://itiharyanaadmissions.nic.in/"><span class="english">Home</span><span class="hindi" style="display:none;">होम</span></a></li>
              <li><a href="frmDistrictWiseCollege.aspx"><span class="english">District Wise College</span><span class="hindi" style="display:none;">जिला वार कॉलेज</span></a></li>
              <li><a href="frmViewCollegeSeatMatrix.aspx"><span class="english">View Seat Matrix</span><span class="hindi" style="display:none;">सीट मैट्रिक्स</span></a></li>
              <li class="active"><a href="MeritList.aspx"><span class="english">Merit List</span><span class="hindi" style="display:none;">योग्यता क्रम सूची</span></a></li>
              <li><a href="CutOffList.aspx"><span class="english">Cut Off List</span><span class="hindi" style="display:none;">कट ऑफ लिस्ट</span></a></li>
              
                            <%--<li><a href="#">Search College</a></li>--%>
                        </ul>
                    </nav>
                    <!-- .nav-menu -->
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 top-heading">
                <h4 class="heading">Merit List</h4>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12" style="margin-top: 20px;">
                <div class="cus-top-section">
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12">
                        <label>Select Collage</label><br />
                        <asp:DropDownList ID="ddlCollege" runat="server" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCollege"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Collage" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-4 col-12">
                        <label>Select Course</label><br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCourse" runat="server" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourse"
                                    CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="A"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCollege" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-4 col-12 cus-getbtn">
                    </div>
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-grid-table">
                <div class="table-responsive-lg table-responsive-md table-responsive-sm">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SrNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSeatAllocationCategory" runat="server" Text='<%# Eval("SeatAllocationCategory") %>' CssClass="form-control"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rank">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrank_id" runat="server" Text='<%# Eval("rank_id") %>' CssClass="form-control"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblapplicant_name" runat="server" Text='<%# Eval("applicant_name") %>' CssClass="form-control"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Father Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblapplicant_father_name" runat="server" Text='<%# Eval("applicant_father_name") %>' CssClass="form-control"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsection" runat="server" Text='<%# Eval("section") %>' CssClass="form-control"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Course">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcourseName" runat="server" Text='<%# Eval("courseName") %>' CssClass="form-control"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCollege" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlCourse" />
                                    </Triggers>
                                </asp:UpdatePanel>
                </div>
            </div>
            <div class="modal cus-modal cus-inner-modal" id="myModal2" role="dialog">
                <div class="modal-dialog  modal-lg" style="margin-top: 1%;">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-body" id="mydatamodel2" style="display: block;">
                            <div class="table-responsive cus-grid-table-three">
                                
                            </div>
                        </div>
                    </div>
                    <div class="input-group input-group-sm mb-3">
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- ======= Footer ======= -->
    <footer id="footer">
        <div class="container">
            <div class="credits">
                Designed by <a href="#">National Informatics Center, Haryana</a>
            </div>
        </div>
    </footer>
    <!-- End Footer -->
</body>
</html>
