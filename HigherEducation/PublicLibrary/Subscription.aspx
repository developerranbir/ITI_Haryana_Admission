<%@ Page Title="" Language="C#" MasterPageFile="~/PublicLibrary/LibraryMaster.Master" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="HigherEducation.PublicLibrary.Subscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --accent-color: #e74c3c;
            --light-color: #ecf0f1;
            --dark-color: #34495e;
        }

        .subscription-hero {
            background: linear-gradient(135deg, rgba(44, 62, 80, 0.9), rgba(52, 73, 94, 0.9)), url('https://images.unsplash.com/photo-1481627834876-b7833e8f5570?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80');
            background-size: cover;
            background-position: center;
            color: white;
            padding: 60px 0;
            text-align: center;
            margin-bottom: 40px;
        }

        .subscription-hero h1 {
            font-size: 2.5rem;
            font-weight: 700;
            margin-bottom: 15px;
        }

        .subscription-hero p {
            font-size: 1.2rem;
            max-width: 600px;
            margin: 0 auto;
            opacity: 0.9;
        }

        .section-title {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 40px;
            text-align: center;
            position: relative;
            padding-bottom: 15px;
        }

        .section-title:after {
            content: '';
            position: absolute;
            bottom: 0;
            left: 50%;
            transform: translateX(-50%);
            width: 80px;
            height: 4px;
            background: var(--secondary-color);
            border-radius: 2px;
        }

        .form-section {
            background: white;
            border-radius: 15px;
            padding: 30px;
            margin-bottom: 30px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
            border-left: 5px solid var(--secondary-color);
        }

        .form-control {
            border: 2px solid #e9ecef;
            border-radius: 10px;
            padding: 12px 15px;
            margin-bottom: 20px;
            transition: all 0.3s ease;
        }

        .form-control:focus {
            border-color: var(--secondary-color);
            box-shadow: 0 0 0 0.2rem rgba(52, 152, 219, 0.25);
        }

        .plan-card {
            background: white;
            border-radius: 15px;
            padding: 30px;
            margin-bottom: 30px;
            border: 2px solid transparent;
            transition: all 0.3s ease;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
            height: 100%;
            display: flex;
            flex-direction: column;
        }

        .plan-card:hover {
            transform: translateY(-10px);
            border-color: var(--secondary-color);
            box-shadow: 0 15px 35px rgba(0, 0, 0, 0.15);
        }

        .plan-card.featured {
            border-color: var(--accent-color);
            position: relative;
            overflow: hidden;
        }

        .plan-card.featured::before {
            content: 'POPULAR';
            position: absolute;
            top: 20px;
            right: -30px;
            background: var(--accent-color);
            color: white;
            padding: 5px 40px;
            transform: rotate(45deg);
            font-size: 0.8rem;
            font-weight: 600;
        }

        .plan-header {
            text-align: center;
            margin-bottom: 25px;
            flex-shrink: 0;
        }

        .plan-icon {
            font-size: 3.5rem;
            color: var(--secondary-color);
            margin-bottom: 20px;
        }

        .plan-card h4 {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 15px;
        }

        .plan-price {
            font-size: 2.5rem;
            font-weight: 700;
            color: var(--primary-color);
            margin: 10px 0;
        }

        .plan-period {
            color: #7f8c8d;
            font-size: 1rem;
            margin-bottom: 10px;
        }

        .plan-features {
            list-style: none;
            padding: 0;
            margin: 20px 0;
            flex-grow: 1;
        }

        .plan-features li {
            padding: 12px 0;
            border-bottom: 1px solid #ecf0f1;
            color: #2c3e50;
            display: flex;
            align-items: center;
        }

        .plan-features li:last-child {
            border-bottom: none;
        }

        .plan-features li i {
            color: #27ae60;
            margin-right: 12px;
            font-size: 1.1rem;
        }

        .plan-features li .fa-times {
            color: #e74c3c;
        }

        .btn-primary-custom {
            background: linear-gradient(135deg, var(--secondary-color), #2980b9);
            border: none;
            border-radius: 10px;
            padding: 14px 30px;
            font-weight: 600;
            color: white;
            transition: all 0.3s ease;
            width: 100%;
            font-size: 1.1rem;
            margin-top: auto;
        }

        .btn-primary-custom:hover {
            transform: translateY(-3px);
            box-shadow: 0 7px 20px rgba(52, 152, 219, 0.4);
        }

        .btn-accent {
            background: linear-gradient(135deg, var(--accent-color), #c0392b);
        }

        .btn-accent:hover {
            box-shadow: 0 7px 20px rgba(231, 76, 60, 0.4);
        }

        .features-section {
            padding: 60px 0;
            background: rgba(255, 255, 255, 0.7);
            margin-top: 50px;
        }

        .feature-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 25px;
            margin-top: 30px;
        }

        .feature-item {
            text-align: center;
            padding: 30px 20px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
            transition: all 0.3s ease;
        }

        .feature-item:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
        }

        .feature-item i {
            font-size: 2.5rem;
            color: var(--secondary-color);
            margin-bottom: 20px;
        }

        .feature-item h5 {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 15px;
        }

        .feature-item p {
            color: #7f8c8d;
            line-height: 1.6;
        }

        .subscription-table {
            background: white;
            border-radius: 15px;
            overflow: hidden;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
            margin-top: 40px;
        }

        .table-header {
            background: linear-gradient(135deg, var(--primary-color), var(--dark-color));
            color: white;
            padding: 20px;
            text-align: center;
        }

        .table-header h3 {
            margin: 0;
            font-weight: 700;
        }

        .alert-custom {
            border-radius: 10px;
            border: none;
            padding: 15px 20px;
            margin-bottom: 25px;
        }

        /* Mobile Responsiveness */
        @media (max-width: 768px) {
            .subscription-hero h1 {
                font-size: 2rem;
            }

            .subscription-hero p {
                font-size: 1rem;
            }

            .form-section {
                padding: 20px;
            }

            .plan-card {
                padding: 20px;
            }

            .plan-icon {
                font-size: 2.5rem;
            }

            .plan-price {
                font-size: 2rem;
            }

            .features-section {
                padding: 40px 0;
            }
        }

        @media (max-width: 576px) {
            .subscription-hero {
                padding: 40px 0;
            }

            .subscription-hero h1 {
                font-size: 1.8rem;
            }

            .feature-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  

    <div class="container">
        <!-- Alert Panel -->
        <asp:Panel runat="server" ID="pnlAlert" CssClass="alert alert-custom" Visible="false">
            <asp:Label runat="server" ID="lblAlertMessage" />
        </asp:Panel>

        <!-- Subscription Form -->
        <div class="form-section">
            <h2 class="section-title">Subscription Details</h2>
            <div class="row">
                <div class="col-md-6">
                    <div class="mb-3">
                        <asp:Label ID="lblDistrict" runat="server" Text="Select District:" CssClass="form-label fw-bold" />
                        <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="mb-3">
                        <asp:Label ID="lblITI" runat="server" Text="Select ITI:" CssClass="form-label fw-bold" />
                        <asp:DropDownList runat="server" ID="ddlITI" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="mb-3">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" CssClass="form-label fw-bold" />
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="mb-3">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="form-label fw-bold" />
                        <asp:Label ID="lblEndDateValue" runat="server" CssClass="form-control bg-light" />
                    </div>
                </div>
            </div>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
        </div>

        <!-- Subscription Plans -->
        <div class="row mt-4">
            <div class="col-12">
                <h2 class="section-title">Choose Your Plan</h2>
            </div>
            
            <!-- Premium Plan -->
            <div class="col-lg-6 mb-4">
                <div class="plan-card featured">
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
                        <li><i class="fas fa-check"></i>Book Issuing Facility (Up to 3 books)</li>
                        <li><i class="fas fa-check"></i>Access to All Sections</li>
                        <li><i class="fas fa-check"></i>Digital Resources & E-books</li>
                        <li><i class="fas fa-check"></i>Priority Support & Assistance</li>
                        <li><i class="fas fa-check"></i>Extended Borrowing Period</li>
                    </ul>
                    <asp:Button runat="server" Text="Choose Premium Plan" CssClass="btn btn-primary-custom btn-accent" ID="btnPremiumPlan" OnClick="btnPremiumPlan_Click" />
                </div>
            </div>
            
            <!-- Basic Plan -->
            <div class="col-lg-6 mb-4">
                <div class="plan-card">
                    <div class="plan-header">
                        <div class="plan-icon">
                            <i class="fas fa-book-reader"></i>
                        </div>
                        <h4>Reading Only</h4>
                        <div class="plan-price">₹100</div>
                        <div class="plan-period">per month</div>
                    </div>
                    <ul class="plan-features">
                        <li><i class="fas fa-check"></i>Unlimited Reading Access</li>
                        <li><i class="fas fa-check"></i>Comfortable Study Space</li>
                        <li><i class="fas fa-check"></i>Access to Reference Section</li>
                        <li><i class="fas fa-check"></i>Newspapers & Magazines</li>
                        <li><i class="fas fa-times"></i>No Book Issuing Facility</li>
                        <li><i class="fas fa-check"></i>Basic Support Services</li>
                    </ul>
                    <asp:Button runat="server" Text="Choose Basic Plan" CssClass="btn btn-primary-custom" ID="btnBasicPlan" OnClick="btnBasicPlan_Click" />
                </div>
            </div>
        </div>

        <!-- Existing Subscriptions -->
        <div class="subscription-table">
            <div class="table-header">
                <h3><i class="fas fa-history me-2"></i>Your Subscription History</h3>
            </div>
            <div class="p-3">
                <asp:GridView ID="gvSubscriptions" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-hover" HeaderStyle-CssClass="table-primary" 
                    GridLines="None" EmptyDataText="No subscriptions found">
                    <HeaderStyle CssClass="fw-bold" />
                    <RowStyle CssClass="align-middle" />
                    <AlternatingRowStyle CssClass="bg-light" />
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="collegename" HeaderText="ITI Name" />
                        <asp:BoundField DataField="SubscriptionType" HeaderText="Subscription Type" />
                        <asp:BoundField DataField="PaymentDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount (₹)" DataFormatString="{0:N2}" />

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <a
                                    href='<%# Eval("PaymentStatus").ToString() == "Pending" 
                                        ? "libraryPayment.aspx?id=" + Eval("SubscriptionId") 
                                        : "libraryPrintPass.aspx?id=" + Eval("SubscriptionId") %>'
                                    class='<%# Eval("PaymentStatus").ToString() == "Completed" 
                                        ? "btn btn-success btn-sm" 
                                        : "btn btn-primary btn-sm" %>'>
                                    <%# Eval("PaymentStatus").ToString() == "Pending" ? "Pay Fee" : "Print Pass" %>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Features Section -->
        <section class="features-section">
            <div class="container">
                <h2 class="section-title">Why Choose Our Library?</h2>
                <div class="feature-grid">
                    <div class="feature-item">
                        <i class="fas fa-university"></i>
                        <h5>Multiple ITI Libraries</h5>
                        <p>Access books from various ITI libraries across the region with single subscription</p>
                    </div>
                    <div class="feature-item">
                        <i class="fas fa-books"></i>
                        <h5>Vast Collection</h5>
                        <p>Thousands of books covering all ITI trades and specializations</p>
                    </div>
                    <div class="feature-item">
                        <i class="fas fa-clock"></i>
                        <h5>Flexible Timing</h5>
                        <p>Open all weekdays with extended hours for your convenience</p>
                    </div>
                    <div class="feature-item">
                        <i class="fas fa-user-graduate"></i>
                        <h5>Student Focused</h5>
                        <p>Resources and environment designed specifically for ITI students</p>
                    </div>
                </div>
            </div>
        </section>
    </div>

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
</asp:Content>