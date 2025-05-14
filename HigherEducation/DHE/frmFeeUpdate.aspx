<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmFeeUpdate.aspx.cs" Inherits="HigherEducation.HigherEducations.frmFeeUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fee Adjustment</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/sweetalert.css" rel="stylesheet" />
    <link href="../assets/css/styleFeeUpdate.css" rel="stylesheet" />

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
        .cus-middle-section-new{
            padding: 1rem;
    box-shadow: inset 0px 0px 10px 4px rgba(0,0,0,0.4);
    height:300px;
        }
        div#dvSection {
    width: 100%;
}
         div#dvSectionNew {
    width: 100%;
}
    </style>
    <script>
        function RadioCheck(rb) {

            var gv = document.getElementById("<%=dvAdmissionStatus.ClientID%>");

         var rbs = gv.getElementsByTagName("input");



         var row = rb.parentNode.parentNode;

         for (var i = 0; i < rbs.length; i++) {

             if (rbs[i].type == "radio") {

                 if (rbs[i].checked && rbs[i] != rb) {

                     rbs[i].checked = false;

                     break;

                 }

             }

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
                    <div class="col-lg-1 col-md-1 col-sm-1 col-12">
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-10 col-10">
                        <h4 class="heading">Fee Adjustment</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="cus-top-section" style="margin-top: 20px;">
                <div class="row">
                     <div class="col-lg-4 col-md-4"> 
                        <label class="cus-select-label">Registration Id:<span style="color: red">*</span></label>
                    </div>
                          <div class="col-lg-6 col-md-6 col-sm-6 col-12">                      
                        <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="30" runat="server" autocompleteoff="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter RegistrationId" ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid RegistrationId" ValidationExpression="^[A-Za-z0-9\s]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                 </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn">
                        <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click" ValidationGroup="B" />
                    </div>
               </div>
                </div>
            <div id="dvSection" runat="server" style="display:none;" >
              <div class="row" style="width:100%;">
                  <div id="dvStuName" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Candidate Name:</label><br />
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
                    <div id="dvStuFatherName" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Father Name:</label><br />
                        <asp:Label ID="lblStuFatherName" runat="server"></asp:Label>
                    </div>
                   <div id="dvRollNo" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Roll No:</label><br />
                        <asp:Label ID="lblRollNo" runat="server"></asp:Label>
                    </div>
                    <div id="dvRegId" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Registration Id:</label><br />
                        <asp:Label ID="lblRegId" runat="server"></asp:Label>
                    </div>
                  <div id="dvGender" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Gender:</label><br />
                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                    </div>
                    <div id="dvMobileNo" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">MobileNo:</label><br />
                        <asp:Label ID="lblMobileNo" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdMobileNo" runat="server" />
                    </div>
                  <%-- <div id="dvEmail" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">EmailId:</label><br />
                        <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                    </div>--%>
                  <div id="dvCounselling" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Counselling Round:</label><br />
                        <asp:Label ID="lblCounselling" runat="server"></asp:Label>
                    </div>
                <div id="dvStudentCategory" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Student Category:</label><br />
                        <asp:Label ID="lblStudentCategory" runat="server"></asp:Label>
                    </div>
                <div id="dvSeatAllocationCategory" runat="server"  class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Seat Allocation Category:</label><br />
                        <asp:Label ID="lblSeatAllocationCategory" runat="server"></asp:Label>
                    </div>   
                    </div>
                </div>
                <div class="row">
                   <div id="dvAdmissionStatus" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display: none;">
                  <asp:Label ID="lbladmissionStatus" cssclass="cus-select-label" runat="server" Text="Payment Status:" style="color: #492A7F; font-weight: 800"></asp:Label>
                <asp:GridView ID="GrdAdmissionStatus" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" Style="width: 100%; margin-top: 20px;">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:RadioButton ID="RadioButton1" runat="server" onclick="RadioCheck(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="College Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("collegename") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Section Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("SectionName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject Combination">
                            <ItemTemplate>
                                <asp:Label ID="lblSubComb" runat="server" Text='<%# Eval("SubjectCombination") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("admissionstatus") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Counselling">
                            <ItemTemplate>
                                <asp:Label ID="lblCounselling" runat="server" Text='<%# Eval("Counselling") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>

                        
                          <asp:TemplateField HeaderText="PMS Status">
                            <ItemTemplate>
                                <asp:Label ID="lblPMSStatus" runat="server" Text='<%# Eval("PMSStatus") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Fee">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalFee" runat="server" Text='<%# Eval("TotalFee") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Concession">
                            <ItemTemplate>
                                <asp:Label ID="lblConcession" runat="server" Text='<%# Eval("Concession") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Fee Collected">
                            <ItemTemplate>
                                <asp:Label ID="lblFeePaid" runat="server" Text='<%# Eval("Fee_paid") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Balance Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblPendingFee" runat="server" Text='<%# Eval("pendingFee") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Payment Mode">
                            <ItemTemplate>
                                <asp:Label ID="lblPendingMode" runat="server" Text='<%# Eval("payment_mode") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Date">
                            <ItemTemplate>
                                <asp:Label ID="lblPendingDate" runat="server" Text='<%# Eval("PaymentDate") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Transaction Id">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentTransId" runat="server" Text='<%# Eval("Payment_transactionId") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Collegeid" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCollegeid" runat="server" Text='<%# Eval("College_id") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <HeaderStyle />
                </asp:GridView>
                    </div>
                    
                    </div>
            <div id="dvSectionNew" runat="server" class="cus-middle-section-new" style="display:none;">
             <div class="row">
            <div id="dvRefAddFee" runat="server" class="col-lg-12 col-md-12 col-sm-12 col-12 m-b-10" style="display: none">
                       <div style="display:flex;justify-content:center;">
                        <asp:RadioButtonList ID="rbtRefundAddFee" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="true" Text="Refund" Value="RF"></asp:ListItem>
                            <asp:ListItem  Text="Additional Fee" Value="AF"></asp:ListItem>
                        </asp:RadioButtonList>
                           </div>
                </div>
                    </div>
                        <div class="row">
                 <div id="dvAmount" class="col-lg-3" runat="server" style="display: none">
                 <label class="cus-select-label">Amount:</label><span style="color: red">*</span>
                          <asp:TextBox ID="txtAmount" CssClass="form-control" MaxLength="6"  runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmount"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Amount." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtAmount"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Amount" ValidationExpression="[0-9]{0,10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
             
                    <div id="dvRemarks" class="col-lg-9" runat="server" style="display: none">
                        <label class="cus-select-label">Remarks:</label><span style="color: red">*</span><br />
                        <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="500" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Remarks." ValidationGroup="A"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtRemarks"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Remarks" ValidationExpression="^[A-Za-z0-9\s]{5,500}$" ValidationGroup="A"></asp:RegularExpressionValidator>
                    </div>
                            </div>
                
                 <div id="dvCaptcha" class="col-lg-12" runat="server" style="padding: 0; display: none;margin:15px;">                       
							<div class="row cus-captchalist">
								<div class="col-lg-3 col-md-3 col-sm-6 col-12 text-right">
									<asp:Label ID="lblCaptcha" CssClass="col-form-label" runat="server" Text="Captcha"></asp:Label><span id="spcaptcha" style="color: red">*</span>
								</div>
								<div class="col-lg-5 col-md-5 col-sm-6 col-12">
									<asp:TextBox ID="txtturing" runat="server" MaxLength="10" placeholder="Enter Captcha" CssClass="form-control"></asp:TextBox>
								</div>
								<div class="col-lg-1 col-md-1 col-sm-6 col-7 mobilecaptcha">
									<img id="imgCaptcha" runat="server" src="Turing.aspx" style="margin-top: 8px; width: 100px; height: 32px; margin-top: 5px;" alt="" />
								</div>
								<div class="col-lg-1 col-md-1 col-sm-6 col-2 login-captcha">
									<asp:ImageButton ID="imgRefreshCaptcha" ImageUrl="../assets/images/refresh.png" Style="width: 32px;" OnClientClick="Turing.aspx" AlternateText="No Image available" runat="server" />
								</div>
								<asp:RequiredFieldValidator Display="Dynamic" CssClass="badge badge-danger" ID="RequiredFieldValidator10" runat="server"
									ErrorMessage="Enter Captcha" ControlToValidate="txtturing" ValidationGroup="A"></asp:RequiredFieldValidator>
                               
							</div>
                     </div>
                   
                <div  class="col-lg-12 col-md-12 col-sm-12 col-12 cus-getbtn text-center" style="margin-top:15px; margin-bottom:10%">
                     <asp:Button ID="btnSubmit" style="display:none" runat="server" CssClass="btn btn-danger btn-sm" Text="Submit"   OnClick="btnSubmit_Click" ValidationGroup="A" />
                    </div>
              </div>
                  
              <div> 
             <asp:HiddenField ID="hdCollegeid" runat="server" />
             <asp:HiddenField ID="hdCombinationid" runat="server" />
             <asp:HiddenField ID="hdPaymentTransId" runat="server" />
           </div>   
       


    </form>
    <!-- ======= Footer ======= -->
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
                        <img src="/assets/images/nic-logo.png" style="width:100px;" />
                    </div>
</div>
                </div>
                
            </div>
           
        </div>
    </footer>
</body>
</html>
