<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation ="false" CodeBehind="frmCollegeSeatMatrix.aspx.cs" Inherits="HigherEducation.HigherEducations.frmCollegeSeatMatrix" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>College Seat</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
     <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/styleseatmatrix.css" rel="stylesheet" />
     
    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>
    <script>
        function startCalc(ele) {
            debugger;
            var K_dash = $(ele).attr('id');
            var start = parseInt(K_dash.lastIndexOf('_')) + 1;
            var end = K_dash.length;

            var K = parseInt(K_dash.substring(start, end));

            var label = parseInt($("#GridView1_lblTotalSeats_" + K).text());
            var total = 0;
            var totallabel = 0;
            var F = $("#GridView1 > tbody:nth-child(1) > tr:nth-child(" + parseInt(K+2) + ") > td > input[type=text]")
            $.each(F, function (X) {
                total += parseInt($(F[X]).val());
                
                //total = totallabel;
            });
           // total = total - parseInt($("#GridView1_hdHOG_" + K).val()) - parseInt($("#GridView1_hdTotalSC" + K).val()) - parseInt($("#GridView1_hdTotalBC_" + K).val()) - parseInt($("#GridView1_hdTotalSC" + K).val()) - parseInt($("#GridView1_hdTotalESM" + K).val());
            //total = total - parseInt($(F[3]).val()) - parseInt($(F[6]).val()) - parseInt($(F[9]).val()) - parseInt($(F[13]).val()) - parseInt($(F[19]).val());
            //totallabel = parseInt($(F[3]).val()) + parseInt($(F[6]).val()) + parseInt($(F[9]).val()) + parseInt($(F[13]).val());// + parseInt($(F[19]).val()); //(Total ESM)
            //total = total - totallabel;
            if (total > label) {
                swal('Alert', 'Sum of all Category Seats cannot exceed Max. no of seats :' + label, 'warning');
                $(ele).val('');
                return false;
            }
            return true;
        }
        
        function TotalCalcHOGCEWS(ele) {
           // debugger;
            var K_dash = $(ele).attr('id');
            var start = parseInt(K_dash.lastIndexOf('_')) + 1;
            var end = K_dash.length;

            var K = parseInt(K_dash.substring(start, end));
            var HOGC = parseInt($("#GridView1_txtHryGen_" + K).val());
            var EWS = parseInt($("#GridView1_txtEcoWeaker_" + K).val());
            var ESMG = parseInt($("#GridView1_txtESMG_" + K).val());
            var total = 0;
            total = HOGC + EWS + ESMG;
            var label = $("#GridView1_lblTotalHOGCEWS_" + K);
            var hdlabel = $("#GridView1_hdHOG_" + K)
            $("#GridView1_lblTotalHOGCEWS_" + K).text(total);
            //$("#GridView1_hdHOG_" + K).val(total);
            return true;
        }
        function TotalCalcSCDSC(ele) {
          //  debugger;
            var K_dash = $(ele).attr('id');
            var start = parseInt(K_dash.lastIndexOf('_')) + 1;
            var end = K_dash.length;

            var K = parseInt(K_dash.substring(start, end));
            var SC = parseInt($("#GridView1_txtSC_" + K).val());
            var DSC = parseInt($("#GridView1_txtDSC_" + K).val());
            var ESMSC = parseInt($("#GridView1_txtESMSC_" + K).val());
            var ESMDSC = parseInt($("#GridView1_txtESMDSC_" + K).val());
            var total = 0;
            total = SC + DSC + ESMSC + ESMDSC;
            var label = $("#GridView1_lblTotalSC_" + K);
            $("#GridView1_lblTotalSC_" + K).text(total);
            //$("#GridView1_hdTotalSC_" + K).val(total);
            return true;
        }
        function TotalCalcBCABCB(ele) {
         //   debugger;
            var K_dash = $(ele).attr('id');
            var start = parseInt(K_dash.lastIndexOf('_')) + 1;
            var end = K_dash.length;

            var K = parseInt(K_dash.substring(start, end));
            var BCA = parseInt($("#GridView1_txtBCA_" + K).val());
            var BCB = parseInt($("#GridView1_txtBCB_" + K).val());
            var ESMBCA = parseInt($("#GridView1_txtESMBCA_" + K).val());
            var ESMBCB = parseInt($("#GridView1_txtESMBCB_" + K).val());
            var total = 0;
            total = BCA + BCB + ESMBCA + ESMBCB;
            var label = $("#GridView1_lblTotalBC_" + K);
            $("#GridView1_lblTotalBC_" + K).text(total);
            //$("#GridView1_hdTotalBC_" + K).val(total);
            return true;
        }
        
        function TotalCalcDAGBCSC(ele) {
           // debugger;
            var K_dash = $(ele).attr('id');
            var start = parseInt(K_dash.lastIndexOf('_')) + 1;
            var end = K_dash.length;

            var K = parseInt(K_dash.substring(start, end));
            var DAG = parseInt($("#GridView1_txtDAG_" + K).val());
            var DABC = parseInt($("#GridView1_txtDABC_" + K).val());
            var DASC = parseInt($("#GridView1_txtDASC_" + K).val());
            var total = 0;
            total = DAG + DABC + DASC;
            var label = $("#GridView1_lblTotalDA_" + K);
            $("#GridView1_lblTotalDA_" + K).text(total);
            //$("#GridView1_hdTotalDA_" + K).val(total);
            return true;
        }
        
        //function TotalCalcESMW(ele) {
        // //  debugger;
        //    var K_dash = $(ele).attr('id');
        //    var start = parseInt(K_dash.lastIndexOf('_')) + 1;
        //    var end = K_dash.length;

        //    var K = parseInt(K_dash.substring(start, end));
        //    var ESMG = parseInt($("#GridView1_txtESMG_" + K).val());
        //    var ESMBCA = parseInt($("#GridView1_txtESMBCA_" + K).val());
        //    var ESMBCB = parseInt($("#GridView1_txtESMBCB_" + K).val());
        //    var ESMSC = parseInt($("#GridView1_txtESMSC_" + K).val());
        //    var ESMDSC = parseInt($("#GridView1_txtESMDSC_" + K).val());
        //    var total = 0;
        //    total = ESMG + ESMBCA + ESMBCB + ESMSC + ESMDSC;
        //    var label = $("#GridView1_lblTotalESM_" + K);
        //    $("#GridView1_lblTotalESM_" + K).text(total);
        //    $("#GridView1_hdTotalESM_" + K).val(total);
        //    return true;
        //}
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
           
            <div class="row">
               
            <div class="col-lg-11 col-md-11 col-sm-11 col-10 top-heading">
                <h4 class="heading">Category Wise Distribution of Seats(2020-2021)</h4>
                </div>
                  <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right cus-getbtn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
           </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12" style="margin-top: 5px;">

               <%-- <div class="cus-top-section">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-12">
                        <label>Select College</label><br />
                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCollege"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4 col-12">
                        <label>Select Course</label><br />
                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourse"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="A"></asp:RequiredFieldValidator>
                    </div>
                     <%--<div class="col-lg-4 col-md-4 col-sm-4 col-12">
                        <label>Select Section</label><br />
                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"> </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSection"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="A"></asp:RequiredFieldValidator>
                          
                    </div>
                     
                </div>--%>
               <%-- <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center" id="dvExport" runat="server" style="display:none;">
                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success" Text="Export" OnClick="btnExptoExcel_Click"   ValidationGroup="A" />
                         </div>--%>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12" id="dvlblTotal" runat="server" style="display:none;" >
                  <%-- <label style="color:#492A7F;font-weight:800;">Total Course Seats:</label>
             <asp:Label ID="lblTotalCourseSeats" runat="server"></asp:Label> ||
                <label style="color:#492A7F;font-weight:800;">Total Subject Combination Seats:</label>
                <asp:Label ID="lblTotalSubCombSeats" runat="server"></asp:Label>--%>
             </div>
               
            <div class="col-lg-12 col-md-12 col-sm-12 col-12" id="dvFullForm" runat="server" >
                <span style="font-size: 60%;"><strong>AIOC:</strong> ALL India Open Category, <strong>HOGC:</strong> Haryan General, <strong>EWS:</strong> Eco Weaker Section, <strong>SC:</strong> Scheduled Caste, <strong>DSC:</strong> Deprived Scheduled Caste, <strong>BCA:</strong> Backward Class -A, <strong>BCB:</strong> Backward Class-B, <strong>DA:</strong> Differently Abled, <strong>DAG:</strong> Differently Abled General, <strong>DABC:</strong> Differently Abled Backward Class, <strong>DASC:</strong> Differently Abled Schedule Caste, <strong>ESMG:</strong> Ex Service Man General, ESMBCA:Ex Service Man Backward Class A, <strong>ESMBCB:</strong> Ex Service Man Backward Class A, <strong>ESMSC:</strong> Ex Service Man Schedule Caste, <strong>ESMDSC:</strong>Ex Service Man Deprived Scheduled Caste</span>
                </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-12 cus-grid-table">

                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" ShowFooter="true"  AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand" class="table table-bordered table-striped table-hover">

                        <Columns>
                          
                                <asp:TemplateField HeaderText="Course Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("course") %>' />
                                </ItemTemplate>

                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Section Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("sectionname") %>' />
                                </ItemTemplate>

                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Total Seats" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalSeats" runat="server" Text='<%# Eval("totalseats") %>'  ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            
                             
                            <asp:TemplateField HeaderText="AIOC" ItemStyle-BackColor="LightGray">
                                <ItemTemplate >
                                  
                                    <asp:TextBox ID="txtOpen" MaxLength="3" runat="server" Text='<%# Eval("openSeats") %>' onblur="startCalc(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOpen"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtOpen" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HOGC" ItemStyle-BackColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHryGen" MaxLength="3"  runat="server" Text='<%# Eval("haryanaGeneral") %>' onblur="startCalc(this); TotalCalcHOGCEWS(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHryGen"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtHryGen" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="EWS" ItemStyle-BackColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEcoWeaker" MaxLength="3" runat="server" Text='<%# Eval("EcoWeaker") %>' onblur="startCalc(this); TotalCalcHOGCEWS(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEcoWeaker"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtEcoWeaker" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="ESMG" ItemStyle-BackColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtESMG" MaxLength="3" runat="server" Text='<%# Eval("ESMGenCatWise") %>' onblur="startCalc(this); TotalCalcHOGCEWS(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtESMG"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtESMG" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Total HOGCEWS" ItemStyle-BackColor="LightBlue" ControlStyle-Font-Bold="true">
                                <ItemTemplate>
                                  <asp:Label ID="lblTotalHOGCEWS" runat="server" Text='<%# Eval("TotalHOGCEWS") %>'   ></asp:Label>
                                   <%--<asp:HiddenField ID="hdHOG" runat="server" Value='<%# Eval("TotalHOGCEWS") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="SC" ItemStyle-BackColor="LightGray">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSC" MaxLength="3" runat="server" Text='<%# Eval("SC") %>' onblur="startCalc(this); TotalCalcSCDSC(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSC" ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtSC" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="DSC" ItemStyle-BackColor="LightGray">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDSC" MaxLength="3" runat="server" Text='<%# Eval("DSC") %>' onblur="startCalc(this); TotalCalcSCDSC(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDSC"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtDSC" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                          <%--  <% 01/09/2020 hide ESMSC and ESMBCB asked by alok sir %>--%>
                             <asp:TemplateField HeaderText="ESMSC" ItemStyle-BackColor="White" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtESMSC" MaxLength="3" runat="server" Text='<%# Eval("ESMSCCatWise") %>' onblur="startCalc(this); TotalCalcSCDSC(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtESMSC"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtESMSC" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ESMDSC"  ItemStyle-BackColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtESMDSC" MaxLength="3" runat="server" Text='<%# Eval("ESMSCDCatwise") %>' onblur="startCalc(this); TotalCalcSCDSC(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtESMDSC"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtESMDSC" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Total SC" ItemStyle-BackColor="LightBlue" ControlStyle-Font-Bold="true">
                                <ItemTemplate>
                                  <asp:Label ID="lblTotalSC" runat="server" Text='<%# Eval("SCTotal") %>' ></asp:Label>
                                    <%-- <asp:HiddenField ID="hdTotalSC" runat="server" Value='<%# Eval("SCTotal") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="BCA" ItemStyle-BackColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBCA" MaxLength="3" runat="server" Text='<%# Eval("BCA") %>' onblur="startCalc(this); TotalCalcBCABCB(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBCA"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtBCA" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BCB" ItemStyle-BackColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBCB" MaxLength="3" runat="server" Text='<%# Eval("BCB") %>' onblur="startCalc(this); TotalCalcBCABCB(this)" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBCB"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtBCB" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="ESMBCA"  ItemStyle-BackColor="White">

                                <ItemTemplate>                               
                                    <asp:TextBox ID="txtESMBCA" MaxLength="3" runat="server" Text='<%# Eval("ESMBCACatWise") %>' onblur="startCalc(this); TotalCalcBCABCB(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtESMBCA"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtESMBCA" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%--  <% 01/09/2020 hide ESMSC and ESMBCB asked by alok sir %>--%>
                            <asp:TemplateField HeaderText="ESMBCB" ItemStyle-BackColor="White" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtESMBCB" MaxLength="3" runat="server" Text='<%# Eval("ESMBCBCatWise") %>' onblur="startCalc(this); TotalCalcBCABCB(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtESMBCB"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtESMBCB" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Total BC" ItemStyle-BackColor="LightBlue" ControlStyle-Font-Bold="true" >
                                <ItemTemplate>
                                  <asp:Label ID="lblTotalBC" runat="server" Text='<%# Eval("BCTotal") %>' ></asp:Label>
                                    <%--<asp:HiddenField ID="hdTotalBC" runat="server" Value='<%# Eval("BCTotal") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                                                 
                             <asp:TemplateField HeaderText="DAG" ItemStyle-BackColor="LightGray">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDAG" MaxLength="3" runat="server" Text='<%# Eval("PHGen") %>' onblur="startCalc(this); TotalCalcDAGBCSC(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorPHGen" runat="server" ControlToValidate="txtDAG"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPHGen" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtDAG" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="DABC" ItemStyle-BackColor="LightGray">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDABC" MaxLength="3" runat="server" Text='<%# Eval("PHBC") %>' onblur="startCalc(this); TotalCalcDAGBCSC(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorPHBC" runat="server" ControlToValidate="txtDABC"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPHBC" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtDABC" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="DASC" ItemStyle-BackColor="LightGray">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDASC" MaxLength="3" runat="server" Text='<%# Eval("PHSC") %>' onblur="startCalc(this); TotalCalcDAGBCSC(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorPHSC" runat="server" ControlToValidate="txtDASC"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPHSC" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtDASC" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total DA" ItemStyle-BackColor="LightBlue" ControlStyle-Font-Bold="true">
                                <ItemTemplate>
                               
                                    <asp:Label ID="lblTotalDA" runat="server" Text='<%# Eval("DA") %>' ></asp:Label>
                                   <%--   <asp:HiddenField ID="hdTotalDA" runat="server" Value='<%# Eval("DA") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                          <%--  <asp:TemplateField HeaderText="ESMEWS" ItemStyle-BackColor="White">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtESMEWS" MaxLength="3" runat="server" Text='<%# Eval("ESMGenecowCatWise") %>' onblur="startCalc(this); TotalCalcESMW(this);" CssClass="form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtESMEWS"  ErrorMessage="Enter Value" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" Display="Dynamic" runat="server" ErrorMessage="Invalid Value" ControlToValidate="txtESMEWS" ValidationExpression="^[0-9]{1,3}$" ValidationGroup="A"  CssClass="badge badge-danger"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                          
                            
                            <%-- <asp:TemplateField HeaderText="Total ESM" ItemStyle-BackColor="LightBlue" ControlStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalESM" runat="server" Text='<%# Eval("TotalESM") %>' ></asp:Label>
                                    <asp:HiddenField ID="hdTotalESM" runat="server" Value='<%# Eval("TotalESM") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="CollegeId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCollegeid" runat="server" Text='<%# Eval("CollageId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CourseCombId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseCombId" runat="server" Text='<%# Eval("courseCombinationid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="SectionId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSectionId" runat="server" Text='<%# Eval("SectionId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CourseId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseId" runat="server" Text='<%# Eval("CourseId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle Font-Bold="True"  />
                    </asp:GridView>
                </div>

            </div>
                          <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 form-group cus-declaration" id="dvchkId" runat="server" style="display:none">
                                            <asp:CheckBox ID="chkId" runat="server" />
                                            <span style="color: black;" id="spengInfo" runat="server" >It is certified that the seats mentioned in the above seat matrix table is correct  as per the State Reservation Policy.</span>
                                           
                                        </div>
          <div class="col-lg-12 col-md-12 col-sm-12 col-12 text-center cus-getbtn" id="dvUpdate" runat="server" style="margin-bottom:4%; display:none;">
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click"  ValidationGroup="A" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnCancel_Click"  />
          </div>

            </div>
    </form>
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
