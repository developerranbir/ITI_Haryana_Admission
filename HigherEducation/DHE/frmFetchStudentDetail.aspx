<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmFetchStudentDetail.aspx.cs" Inherits="HigherEducation.HigherEducations.frmFetchStudentDetail" %>

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
    <link href="../assets/css/stylecancel.css" rel="stylesheet" />

    <script src="../assets/js/jquery/jquery.min.js"></script>
    <script src="../assets/js/jquery-ui/jquery-ui.min.js"></script>
    <script src="../assets/js/jquery-3.4.1.js"></script>
    <script src="../assets/js/popperjs/popper.min.js"></script>
    <script src="../assets/js/moment-with-locales.min.js"></script>
    <script src="../assets/js/bootstrap/js/bootstrap.min.js"></script>
    <script src="../scripts/sweetalert.min.js"></script>

    <script>


        function sweetAlertConfirm(btnDelete) {
            debugger;
            if (btnDelete.dataset.confirmed) {
                // The action was already confirmed by the user, proceed with server event
                btnDelete.dataset.confirmed = false;
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
                    confirmButtonText: 'Yes, delete it!'
                })
                    .then(function () {
                        // Set data-confirmed attribute to indicate that the action was confirmed
                        btnDelete.dataset.confirmed = true;
                        // Trigger button click programmatically
                        btnDelete.click();
                    }).catch(function (reason) {
                        // The action was canceled by the user
                    });
            }
        }
        function ConfirmOnDelete() {
            var validated = Page_ClientValidate('A');
            if (validated) {

                if (confirm("Do you really want to cancel student registration?") == true)
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
        #dvAdmissionStatus table th {
    background: #71738c;
    color: #fff;
}
    </style>
    <script>
        function showCollegeInfo() {
            $("#myModal").modal('show');

        }
        function showSubjectComb() {
            $("#myModal2").modal('show');

        }

        function ShowDocument() {
            $("#myModal1").modal('show');
            $("#iframepdf").show();
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
                        <h4 class="heading">Student's Detail</h4>
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1 text-right back-btn" style="margin-top: 5px;">
                        <a href="HEMenu.aspx" id="btnBack" class="btn btn-icon icon-left btn-primary"><i class="fas fa-arrow-alt-circle-left"></i></a>
                    </div>
                </div>
            </div>
            <div class="container" style="margin-top: 20px;">
                <div class="row">
                    <div class="col-lg-2 col-md-2">
                        <%-- <label class="cus-select-label">Roll No:<span style="color: red">*</span></label>--%>
                        <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Registration Id:<span style="color: red">*</span></label>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-12">
                        <%--<asp:TextBox ID="txtRollNo" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRollNo"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Roll No." ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtRollNo"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Roll No" ValidationExpression="^[0-9\s]{1,20}$" ValidationGroup="B"></asp:RegularExpressionValidator>--%>

                        <asp:TextBox ID="txtRegId" CssClass="form-control" MaxLength="30" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" Display="Dynamic" ErrorMessage="Enter Registration Id" ValidationGroup="B"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtRegId"
                            CssClass="badge badge-danger" ErrorMessage="Invalid Registration Id" ValidationExpression="^[A-Za-z0-9]{1,30}$" ValidationGroup="B"></asp:RegularExpressionValidator>
                         </div>              
                  
                      <div class="col-lg-2 col-md-2">
                        <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Select Admission:</label><span style="color: red">*</span>
                   </div>
                         <div class="col-lg-2 col-md-2 col-sm-2 col-12">
                        <asp:DropDownList ID="ddlUGPG" runat="server" CssClass="form-control">
                            <asp:ListItem Text="UG" Value="UG"></asp:ListItem>
                           <asp:ListItem Text="PG" Value="PG"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlUGPG"
                            CssClass="badge badge-danger" InitialValue="0" ErrorMessage="Please Select Admission" ValidationGroup="A"></asp:RequiredFieldValidator>

             </div>

                    <div class="col-lg-2 col-md-2 col-sm-2 col-12 cus-getbtn">
                        <asp:Button ID="btnGo" runat="server" CssClass="btn btn-primary" Text="Go" OnClick="btnGo_Click" ValidationGroup="B" />
                    </div>
                </div>
            </div>
            
            
            <div id="dvSection" runat="server">
                <div class="row">
                    <div id="dvStuName" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Student Name:</label><br />
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                    </div>
                    <div id="dvStuFatherName" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Father Name:</label><br />
                        <asp:Label ID="lblStuFatherName" runat="server"></asp:Label>
                    </div>
                    <div id="dvRegId" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Registration Id:</label><br />
                        <asp:Label ID="lblRegId" runat="server"></asp:Label>
                    </div>
                    <div id="dvBoard" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Board:</label><br />
                        <asp:Label ID="lblBoard" runat="server"></asp:Label>
                    </div>
                    <div id="dvPassingYear" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Passing Year:</label><br />
                        <asp:Label ID="lblPassingYear" runat="server"></asp:Label>
                    </div>
                    <div id="dvMobileNo" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Mobile No:</label><br />
                        <asp:Label ID="lblMobileNo" runat="server"></asp:Label>
                    </div>
                    <div id="dvEmailId" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Email Id:</label><br />
                        <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                    </div>
                    <div id="dvSubmitDate" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Submission Date:</label><br />
                        <asp:Label ID="lblSubmissionDate" runat="server"></asp:Label>
                    </div>
                    <div id="dvUnlockedDate" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Unlocked Date:</label><br />
                        <asp:Label ID="lblUnlockDate" runat="server"></asp:Label>
                    </div>
                    <div id="dvobjectionRaised" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Objection Raised by Verifier:</label><br />
                        <asp:Label ID="lblObjectionRaisedRemarks" runat="server"></asp:Label>
                    </div>
                    <div id="dvCollegeName" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">College Name</label><br />
                        <asp:Label ID="lblCollegeName" runat="server"></asp:Label>
                    </div>
                    <div id="dvOpenCounselling" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <label class="cus-select-label">Applied for open councelling:</label><br />
                        <asp:Label ID="lblOpenCouncelling" runat="server"></asp:Label>
                    </div>
                    <div id="dvDocument" runat="server" style="display: none;" class="col-lg-4 col-md-4 col-sm-6 col-12">
                        <a href="#" runat="server" id="lnkShowDocument" onclick="ShowDocument();">Show Document</a>
                    </div>
                </div>
            </div>
             <div id="dvAdmissionStatus" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display: none;">
                  <asp:Label ID="lbladmissionStatus" cssclass="cus-select-label" runat="server" Text="Admission Status:" style="color: #492A7F; font-weight: 800"></asp:Label>
                <asp:GridView ID="GrdAdmissionStatus" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" Style="width: 100%; margin-top: 20px; margin-bottom:10%;">
                    <Columns>
                        <asp:TemplateField HeaderText="College Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("collegename") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Section Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("courseSectionName") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject Combination">
                            <ItemTemplate>
                                 <asp:Label ID="lblSubComb" runat="server" Text='<%# Eval("SubjectCombination") %>'></asp:Label> 

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Admission Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("admissionstatus") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Counselling">
                            <ItemTemplate>
                                <asp:Label ID="lblCounselling" runat="server" Text='<%# Eval("Counselling") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Payment Transaction Id">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentTransId" runat="server" Text='<%# Eval("Payment_transactionId") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>
                        
                    </Columns>
                    <HeaderStyle />
                </asp:GridView>


            </div>
               <div id="dvGrdResultInfo" class="col-lg-12 col-md-12 col-sm-12 col-12 table-responsive" runat="server" style="display:none;" >                  
                    <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Result:</label>
                <asp:GridView ID="GrdResultInfo" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover" style="width:100%;margin-top:20px;"> 
                      <Columns>
                     <asp:TemplateField HeaderText="College Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("collegename") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Course Name"> <%--SectionName--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("SectionName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Subject Combination">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubComb" runat="server" Text='<%# Eval("subjectCombination") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         
                          </Columns>
                    <HeaderStyle />
                </asp:GridView>
                  

            </div>
            <div id="dvActionHistory" class=" col-lg-12 col-md-12 col-sm-12 col-12 table-responsive cus-grid-table" runat="server" style="display: none;">
                <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Action History:</label>
              
                    <asp:GridView ID="grdhistory" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Timestamp">
                                <ItemTemplate>
                                    <asp:Label ID="lblTimestamp" runat="server" Text='<%# Eval("statusdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action By">
                                <ItemTemplate>
                                    <asp:Label ID="lblActionBy" runat="server" Text='<%# Eval("updatedby") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#492A7F" />
                    </asp:GridView>
                </div>

            </div>
       <div id="dvFeePaid" class=" col-lg-12 col-md-12 col-sm-12 col-12 table-responsive cus-grid-table" runat="server" style="display: none;" >
                <label class="cus-select-label" style="color: #492A7F; font-weight: 800">Fee Paid:</label>
           
                    <asp:GridView ID="GrdFeeDetail" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("applicant_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="College Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("collegename") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblcourseName" runat="server" Text='<%# Eval("courseName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSectionName" runat="server" Text='<%# Eval("SectionName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seat Allocation Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeatAllocationCategory" runat="server" Text='<%# Eval("SeatAllocationCategory") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblcategoryname" runat="server" Text='<%# Eval("categoryname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Fee Paid">
                                <ItemTemplate>
                                    <asp:Label ID="lblFee_paid" runat="server" Text='<%# Eval("Fee_paid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Installment/Annual">
                                <ItemTemplate>
                                    <asp:Label ID="lblPart_Full" runat="server" Text='<%# Eval("Part_Full") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Mode">
                                <ItemTemplate>
                                    <asp:Label ID="lblpayment_mode" runat="server" Text='<%# Eval("payment_mode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayment_Date" runat="server" Text='<%# Eval("Payment_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Payment TransactionId">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayment_transactionId" runat="server" Text='<%# Eval("Payment_transactionId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblorder_status" runat="server" Text='<%# Eval("order_status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Reference No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblBankRefNo" runat="server" Text='<%# Eval("bank_ref_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Payment Gateway">
                                <ItemTemplate>
                                    <asp:Label ID="lblPayment_gateway" runat="server" Text='<%# Eval("Payment_gateway") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#492A7F" />
                    </asp:GridView>

              </div>

        <div class="modal cus-modal" id="myModal1" role="dialog">
            <div class="modal-dialog  modal-lg" style="margin-top: 1%;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row" style="width: 100%;">
                            <div style="display: inline-block; width: 70%; float: left; text-align: left">
                                <h6 class="modal-title" style="font-weight: bold; padding-left: 15px;">Document</h6>
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
