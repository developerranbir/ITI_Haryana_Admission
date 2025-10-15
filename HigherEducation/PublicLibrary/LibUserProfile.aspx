
<%@ Page Title="" Language="C#" MasterPageFile="~/PublicLibrary/LibraryMaster.Master" AutoEventWireup="true" CodeBehind="LibUserProfile.aspx.cs" Inherits="HigherEducation.PublicLibrary.LibUserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --success-color: #27ae60;
            --warning-color: #f39c12;
            --danger-color: #e74c3c;
            --light-color: #ecf0f1;
            --dark-color: #34495e;
        }

        .profile-hero {
            background: linear-gradient(135deg, rgba(44, 62, 80, 0.9), rgba(52, 73, 94, 0.9)), url('https://images.unsplash.com/photo-1517077304055-6e89abbf09b0?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80');
            background-size: cover;
            background-position: center;
            color: white;
            padding: 60px 0;
            text-align: center;
            margin-bottom: 40px;
        }

        .profile-hero h1 {
            font-size: 2.5rem;
            font-weight: 700;
            margin-bottom: 15px;
        }

        .profile-hero p {
            font-size: 1.2rem;
            max-width: 600px;
            margin: 0 auto;
            opacity: 0.9;
        }

        .profile-card {
            background: white;
            border-radius: 15px;
            padding: 30px;
            margin-bottom: 30px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
        }

        .section-title {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 3px solid var(--secondary-color);
        }

        .profile-header {
            text-align: center;
            margin-bottom: 30px;
        }

        .profile-avatar {
            width: 120px;
            height: 120px;
            background: linear-gradient(135deg, var(--secondary-color), #2980b9);
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 20px;
            color: white;
            font-size: 3rem;
        }

        .profile-info {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 25px;
            margin-bottom: 20px;
        }

        .info-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 12px 0;
            border-bottom: 1px solid #e9ecef;
        }

        .info-item:last-child {
            border-bottom: none;
        }

        .info-label {
            font-weight: 600;
            color: var(--primary-color);
        }

        .info-value {
            color: #6c757d;
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

        .btn-primary-custom {
            background: linear-gradient(135deg, var(--secondary-color), #2980b9);
            border: none;
            border-radius: 10px;
            padding: 12px 30px;
            font-weight: 600;
            color: white;
            transition: all 0.3s ease;
        }

        .btn-primary-custom:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(52, 152, 219, 0.4);
        }

        .btn-outline-custom {
            border: 2px solid var(--secondary-color);
            border-radius: 10px;
            padding: 10px 25px;
            font-weight: 600;
            color: var(--secondary-color);
            background: transparent;
            transition: all 0.3s ease;
        }

        .btn-outline-custom:hover {
            background: var(--secondary-color);
            color: white;
            transform: translateY(-2px);
        }

        .password-section {
            background: linear-gradient(135deg, #d1ecf1, #a2d9ce);
            padding: 25px;
            border-radius: 10px;
            border-left: 4px solid var(--secondary-color);
            margin: 20px 0;
        }

        .alert-custom {
            border-radius: 10px;
            padding: 15px;
            margin-bottom: 20px;
            border: none;
        }

        .progress {
            height: 5px;
            margin-bottom: 20px;
        }

        .password-strength {
            font-size: 0.9rem;
            margin-top: -15px;
            margin-bottom: 15px;
        }

        .strength-weak { color: var(--danger-color); }
        .strength-medium { color: var(--warning-color); }
        .strength-strong { color: var(--success-color); }

        .input-group {
            margin-bottom: 20px;
        }

        .input-group-text {
            background: var(--primary-color);
            color: white;
            border: 2px solid var(--primary-color);
            border-radius: 10px 0 0 10px;
        }

        .input-group .form-control {
            border-radius: 0 10px 10px 0;
            margin-bottom: 0;
        }

        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
            margin-top: 30px;
        }

        .stat-card {
            background: white;
            border-radius: 10px;
            padding: 20px;
            text-align: center;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
            border-left: 4px solid var(--secondary-color);
        }

        .stat-number {
            font-size: 2rem;
            font-weight: 700;
            color: var(--primary-color);
            margin-bottom: 5px;
        }

        .stat-label {
            color: #6c757d;
            font-size: 0.9rem;
        }

        @media (max-width: 768px) {
            .profile-hero h1 {
                font-size: 2rem;
            }

            .profile-hero p {
                font-size: 1rem;
            }

            .profile-card {
                padding: 20px;
            }

            .info-item {
                flex-direction: column;
                align-items: flex-start;
            }

            .info-value {
                margin-top: 5px;
            }
        }

        @media (max-width: 576px) {
            .profile-hero {
                padding: 40px 0;
            }

            .stats-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="container">
        <!-- Alert Messages -->
        <asp:Panel runat="server" ID="pnlAlert" CssClass="alert alert-custom" Visible="false">
            <asp:Label runat="server" ID="lblAlertMessage" />
        </asp:Panel>

        <div class="row">
            <!-- Profile Information -->
            <div class="col-lg-8">
                <div class="profile-card">
                    <div class="profile-header">
                        <div class="profile-avatar">
                            <i class="fas fa-user"></i>
                        </div>
                        <h2 class="section-title"><asp:Literal ID="litUserName" runat="server" /></h2>
                        <p class="text-muted">Member since <asp:Literal ID="litMemberSince" runat="server" /></p>
                    </div>

                    <div class="profile-info">
                        <h5 class="mb-4"><i class="fas fa-info-circle me-2"></i>Personal Information</h5>
                        
                        <div class="info-item">
                            <span class="info-label">Full Name:</span>
                            <span class="info-value"><asp:Literal ID="litFullName" runat="server" /></span>
                        </div>
                        
                        <div class="info-item">
                            <span class="info-label">Mobile Number:</span>
                            <span class="info-value"><asp:Literal ID="litMobile" runat="server" /></span>
                        </div>
                        
                        <div class="info-item">
                            <span class="info-label">Email Address:</span>
                            <span class="info-value"><asp:Literal ID="litEmail" runat="server" /></span>
                        </div>
                        
                        <div class="info-item">
                            <span class="info-label">User ID:</span>
                            <span class="info-value"><asp:Literal ID="litUserId" runat="server" /></span>
                        </div>
                    </div>

                    <!-- Change Password Section -->
                    <div class="password-section">
                        <h5><i class="fas fa-lock me-2"></i>Change Password</h5>
                        <p class="text-muted">Update your password to keep your account secure</p>

                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-lock"></i></span>
                            <asp:TextBox runat="server" ID="txtCurrentPassword" TextMode="Password" 
                                CssClass="form-control" placeholder="Current Password" />
                        </div>

                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-key"></i></span>
                            <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" 
                                CssClass="form-control" placeholder="New Password" onkeyup="checkPasswordStrength(this.value)" />
                        </div>

                        <div class="progress">
                            <div id="passwordStrengthBar" class="progress-bar" role="progressbar" 
                                style="width: 0%" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                        <div id="passwordStrengthText" class="password-strength"></div>

                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-key"></i></span>
                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" 
                                CssClass="form-control" placeholder="Confirm New Password" />
                        </div>

                        <div class="d-flex gap-3">
                            <asp:Button runat="server" ID="btnChangePassword" Text="Change Password" 
                                CssClass="btn btn-primary-custom" OnClick="btnChangePassword_Click" />
                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" 
                                CssClass="btn btn-outline-custom" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Statistics Sidebar -->
            <div class="col-lg-4">
                <div class="profile-card">
                    <h5 class="section-title"><i class="fas fa-chart-bar me-2"></i>Account Statistics</h5>
                    
                    <div class="stats-grid">
                        <div class="stat-card">
                            <div class="stat-number"><asp:Literal ID="litTotalSubscriptions" runat="server" Text="0" /></div>
                            <div class="stat-label">Library Subscriptions</div>
                        </div>
                        
                        <div class="stat-card">
                            <div class="stat-number"><asp:Literal ID="litWorkshopBookings" runat="server" Text="0" /></div>
                            <div class="stat-label">Workshop Bookings</div>
                        </div>
                        
                        <div class="stat-card">
                            <div class="stat-number"><asp:Literal ID="litActiveSubscriptions" runat="server" Text="0" /></div>
                            <div class="stat-label">Active Subscriptions</div>
                        </div>
                        
                        <div class="stat-card">
                            <div class="stat-number"><asp:Literal ID="litTotalSpent" runat="server" Text="₹0" /></div>
                            <div class="stat-label">Total Spent</div>
                        </div>
                    </div>

                    <div class="mt-4">
                        <h6><i class="fas fa-clock me-2"></i>Quick Actions</h6>
                        <div class="d-grid gap-2">
                            <a href="MySubscription.aspx" class="btn btn-outline-custom">
                                <i class="fas fa-book me-2"></i>My Subscriptions
                            </a>
                            <a href="ViewMyWorkshopBookings.aspx" class="btn btn-outline-custom">
                                <i class="fas fa-tools me-2"></i>Workshop Bookings
                            </a>
                            <a href="Home.aspx" class="btn btn-outline-custom">
                                <i class="fas fa-home me-2"></i>Back to Home
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        // Password Strength Checker
        function checkPasswordStrength(password) {
            var strength = 0;
            var tips = "";
            var bar = document.getElementById('passwordStrengthBar');
            var text = document.getElementById('passwordStrengthText');

            if (password.length < 6) {
                tips = "Password too short";
                bar.style.width = "20%";
                bar.className = "progress-bar bg-danger";
                text.className = "password-strength strength-weak";
            } else {
                if (password.match(/[a-z]/)) strength++;
                if (password.match(/[A-Z]/)) strength++;
                if (password.match(/[0-9]/)) strength++;
                if (password.match(/[^a-zA-Z0-9]/)) strength++;

                switch (strength) {
                    case 0:
                    case 1:
                        tips = "Weak password";
                        bar.style.width = "25%";
                        bar.className = "progress-bar bg-danger";
                        text.className = "password-strength strength-weak";
                        break;
                    case 2:
                        tips = "Medium password";
                        bar.style.width = "50%";
                        bar.className = "progress-bar bg-warning";
                        text.className = "password-strength strength-medium";
                        break;
                    case 3:
                        tips = "Strong password";
                        bar.style.width = "75%";
                        bar.className = "progress-bar bg-info";
                        text.className = "password-strength strength-strong";
                        break;
                    case 4:
                        tips = "Very strong password";
                        bar.style.width = "100%";
                        bar.className = "progress-bar bg-success";
                        text.className = "password-strength strength-strong";
                        break;
                }
            }

            text.innerHTML = tips;
        }

        // Clear password fields
        function clearPasswordFields() {
            document.getElementById('<%= txtCurrentPassword.ClientID %>').value = '';
            document.getElementById('<%= txtNewPassword.ClientID %>').value = '';
            document.getElementById('<%= txtConfirmPassword.ClientID %>').value = '';
            document.getElementById('passwordStrengthBar').style.width = '0%';
            document.getElementById('passwordStrengthText').innerHTML = '';
        }
    </script>
</asp:Content>