<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="HigherEducation.PublicLibrary.Subscription" EnableEventValidation="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subscription</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"/>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css">
    <style type="text/css">

        body {
            background: #f8f9fa;
        }

        .section-title {
            font-size: 2rem;
            font-weight: 700;
            margin-bottom: 1.5rem;
            color: #343a40;
        }

        .plan-card {
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            padding: 2rem;
            margin-bottom: 2rem;
        }

            .plan-card.featured {
                border: 2px solid #007bff;
            }

        .plan-header {
            text-align: center;
            margin-bottom: 1rem;
        }

        .plan-icon {
            font-size: 2.5rem;
            color: #007bff;
            margin-bottom: 0.5rem;
        }

        .plan-price {
            font-size: 2rem;
            font-weight: 700;
            color: #007bff;
        }

        .plan-period {
            font-size: 1rem;
            color: #6c757d;
        }

        .plan-features {
            list-style: none;
            padding: 0;
            margin: 1rem 0;
        }

            .plan-features li {
                font-size: 1rem;
                margin-bottom: 0.5rem;
            }

        .btn-primary-custom {
            background: #007bff;
            color: #fff;
            border-radius: 25px;
            padding: 0.5rem 2rem;
            font-weight: 600;
            border: none;
        }

            .btn-primary-custom:hover {
                background: #0056b3;
            }

        .feature-grid {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 2rem;
            margin-top: 2rem;
        }

        .feature-item {
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            padding: 1.5rem;
            text-align: center;
            width: 220px;
        }

            .feature-item i {
                font-size: 2rem;
                color: #007bff;
                margin-bottom: 0.5rem;
            }

            .feature-item h5 {
                font-size: 1.1rem;
                font-weight: 600;
                margin-bottom: 0.5rem;
            }

            .feature-item p {
                font-size: 0.95rem;
                color: #6c757d;
            }

        @media (max-width: 768px) {
            .feature-grid {
                flex-direction: column;
                align-items: center;
            }

            .feature-item {
                width: 90%;
                margin-bottom: 1rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <asp:Panel runat="server" ID="pnlAlert" CssClass="alert alert-custom" Visible="false">
     <asp:Label runat="server" ID="lblAlertMessage" />
 </asp:Panel>

        <div class="container py-5">
            <div class="row mb-4">
                <div class="col-md-6">
                    <asp:Label ID="lblDistrict" runat="server" Text="Select District:" CssClass="font-weight-bold" />
                    <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control mt-2" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblITI" runat="server" Text="Select ITI:" CssClass="font-weight-bold" />
                    <asp:DropDownList runat="server" ID="ddlITI" CssClass="form-control mt-2"></asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" CssClass="font-weight-bold" />
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control mt-2" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="font-weight-bold" />
                    <asp:Label ID="lblEndDateValue" runat="server" CssClass="form-control mt-2" />
                </div>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>

            <!-- Subscription Plans -->
            <div class="row mt-5">
                <div class="col-12 text-center">
                    <h2 class="section-title">Library Subscription Plans</h2>
                </div>
                <!-- Reading Plans -->
                <div class="col-md-6">
                    <div class="plan-card">
                        <div class="plan-header">
                            <div class="plan-icon">
                                <i class="fas fa-book-open"></i>
                            </div>
                            <h4>Reading + Book Issue</h4>
                            <div class="plan-price">₹500</div>
                            <div class="plan-period">per month</div>
                        </div>
                        <ul class="plan-features">
                            <li><i class="fas fa-check"></i>Unlimited Reading Access</li>
                            <li><i class="fas fa-check"></i>Book Issuing Facility</li>
                            <li><i class="fas fa-check"></i>Access to All Sections</li>
                            <li><i class="fas fa-check"></i>Digital Resources</li>
                            <li><i class="fas fa-check"></i>Priority Support</li>
                        </ul>
                        <asp:Button runat="server" Text="Choose Plan" CssClass="btn btn-primary-custom" ID="btnPremiumPlan" OnClick="btnPremiumPlan_Click" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="plan-card">
                        <div class="plan-header">
                            <div class="plan-icon">
                                <i class="fas fa-chair"></i>
                            </div>
                            <h4>Reading Only</h4>
                            <div class="plan-price">₹100</div>
                            <div class="plan-period">per month</div>
                        </div>
                        <ul class="plan-features">
                            <li><i class="fas fa-check"></i>Unlimited Reading Access</li>
                            <li><i class="fas fa-check"></i>Study Space</li>
                            <li><i class="fas fa-check"></i>Reference Section</li>
                            <li><i class="fas fa-times text-danger"></i>No Book Issuing</li>
                            <li><i class="fas fa-check"></i>Basic Support</li>
                        </ul>
                        <asp:Button runat="server" Text="Choose Plan" CssClass="btn btn-primary-custom" ID="btnBasicPlan" OnClick="btnBasicPlan_Click" />
                    </div>
                </div>

<asp:GridView ID="gvSubscriptions" runat="server" OnRowCommand="gvSubscriptions_RowCommand" AutoGenerateColumns="False" CssClass="table table-borderless table-striped table-hover shadow-sm rounded mt-4" HeaderStyle-BackColor="#007bff" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="True" GridLines="None">
    <HeaderStyle CssClass="font-weight-bold" />
    <RowStyle CssClass="align-middle" />
    <AlternatingRowStyle CssClass="bg-light" />
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="User Name" ItemStyle-CssClass="align-middle" />
        <asp:BoundField DataField="collegename" HeaderText="ITI Name" ItemStyle-CssClass="align-middle" />
        <asp:BoundField DataField="SubscriptionType" HeaderText="Subscription Type" ItemStyle-CssClass="align-middle" />
        <asp:BoundField DataField="PaymentDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-CssClass="align-middle" />
        <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" ItemStyle-CssClass="align-middle" />
        <asp:BoundField DataField="Amount" HeaderText="Amount (₹)" DataFormatString="{0:N2}" ItemStyle-CssClass="align-middle text-middle" />

     
        
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button ID="btnAction" runat="server" 
                    Text='<%# Eval("PaymentStatus").ToString() == "Pending" ? "Pay Fee" : "Print Pass" %>'
                    CommandName='<%# Eval("PaymentStatus").ToString()%>'
                    CommandArgument='<%# Eval("SubscriptionId") %>'
                    CssClass='<%# Eval("PaymentStatus").ToString() == "Completed" ? " btn btn-success" : " btn btn-primary" %>' />
            </ItemTemplate>
        </asp:TemplateField>



    </Columns>
</asp:GridView>
                <!-- Features Grid -->
                <div class=" m-5 text-center">
                    <div class="col-12 ">
                        <h2 class="section-title">Why Choose Our Library?</h2>
                    </div>
                    <div class="feature-grid">
                        <div class="feature-item">
                            <i class="fas fa-university"></i>
                            <h5>Multiple ITI Libraries</h5>
                            <p>Access books from various ITI libraries across the region</p>
                        </div>
                        <div class="feature-item">
                            <i class="fas fa-books"></i>
                            <h5>Vast Collection</h5>
                            <p>Thousands of books covering all ITI's subjects</p>
                        </div>
                        <div class="feature-item">
                            <i class="fas fa-clock"></i>
                            <h5>Flexible Timing</h5>
                            <p>Open all weekdays</p>
                        </div>
                        <div class="feature-item">
                            <i class="fas fa-user-graduate"></i>
                            <h5>Student Focused</h5>
                            <p>Designed specifically for students</p>
                        </div>
                    </div>
                </div>
            </div>
            

        
        </div>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var today = new Date();
            var yyyy = today.getFullYear();
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var dd = String(today.getDate()).padStart(2, '0');
            var minDate = yyyy + '-' + mm + '-' + dd;
            var txtStartDate = document.getElementById('<%= txtStartDate.ClientID %>');
        if (txtStartDate) {
            txtStartDate.setAttribute('min', minDate);
            txtStartDate.value = minDate;
        }
        setEndDate();
        txtStartDate.onchange = setEndDate;
        function setEndDate() {
            var startDateValue = txtStartDate.value;
            if (startDateValue) {
                var startDate = new Date(startDateValue);
                startDate.setMonth(startDate.getMonth() + 1);
                var yyyy = startDate.getFullYear();
                var mm = String(startDate.getMonth() + 1).padStart(2, '0');
                var dd = String(startDate.getDate()).padStart(2, '0');
                var endDate = yyyy + '-' + mm + '-' + dd;
                document.getElementById('<%= lblEndDateValue.ClientID %>').innerText = endDate;
                }
            }
        };
</script>
</body>
</html>
