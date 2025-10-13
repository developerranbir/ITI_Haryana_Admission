<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="HigherEducation.PublicLibrary.SignUp" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ITI Library - User Authentication</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --success-color: #27ae60;
            --warning-color: #f39c12;
            --danger-color: #e74c3c;
        }
        
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            margin: 0;
            padding: 0;
        }
        
        .auth-container {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }
        
        .auth-card {
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(10px);
            border-radius: 20px;
            box-shadow: 0 15px 35px rgba(0, 0, 0, 0.1);
            border: 1px solid rgba(255, 255, 255, 0.2);
            overflow: hidden;
            max-width: 500px;
            width: 100%;
        }
        
        .auth-header {
            background: linear-gradient(135deg, var(--primary-color), var(--secondary-color));
            color: white;
            padding: 30px;
            text-align: center;
        }
        
        .auth-header h1 {
            margin: 0;
            font-size: 2rem;
            font-weight: 700;
        }
        
        .auth-header .subtitle {
            font-size: 1rem;
            opacity: 0.9;
            margin-top: 10px;
        }
        
        .auth-body {
            padding: 40px;
        }
        
        .nav-tabs-custom {
            border-bottom: 3px solid #ecf0f1;
            margin-bottom: 30px;
        }
        
        .nav-tabs-custom .nav-link {
            border: none;
            color: #7f8c8d;
            font-weight: 600;
            padding: 15px 25px;
            border-radius: 10px 10px 0 0;
            margin-right: 5px;
            transition: all 0.3s ease;
        }
        
        .nav-tabs-custom .nav-link.active {
            background: var(--secondary-color);
            color: white;
            border: none;
        }
        
        .nav-tabs-custom .nav-link:hover {
            color: var(--primary-color);
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
            width: 100%;
        }
        
        .btn-primary-custom:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(52, 152, 219, 0.4);
        }
        
        .btn-success-custom {
            background: linear-gradient(135deg, var(--success-color), #229954);
            border: none;
            border-radius: 10px;
            padding: 12px 30px;
            font-weight: 600;
            color: white;
            transition: all 0.3s ease;
            width: 100%;
        }
        
        .otp-section {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            border-left: 4px solid var(--warning-color);
            margin: 20px 0;
        }
        
        .otp-timer {
            color: var(--danger-color);
            font-weight: 600;
            text-align: center;
            margin: 10px 0;
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
        
        @media (max-width: 576px) {
            .auth-body {
                padding: 20px;
            }
            
            .auth-header h1 {
                font-size: 1.5rem;
            }
            
            .nav-tabs-custom .nav-link {
                padding: 10px 15px;
                font-size: 0.9rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auth-container">
            <div class="auth-card">
                <div class="auth-header">
                    <h1><i class="fas fa-book-reader me-2"></i>ITI Library</h1>
                    <div class="subtitle">Create Account to Continue</div>
                </div>
                
                <div class="auth-body">
                    <!-- Alert Messages -->
                    <asp:Panel runat="server" ID="pnlAlert" CssClass="alert alert-custom" Visible="false">
                        <asp:Label runat="server" ID="lblAlertMessage" />
                    </asp:Panel>

                    <a href="Login.aspx" class="btn btn-primary-custom fw-bold w-100 mb-3" style="font-size:1.1rem;">Login if already Signup</a>

                    <!-- Tab Content -->
                    <div class="tab-content" id="authTabsContent">
                        

                        <!-- Signup Tab -->
                        <div class="tab-pane fade show active" id="signup" role="tabpanel">
                            <h4 class="text-center mb-4">Create New Account</h4>
                            
                            <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" 
                                placeholder="Full Name" />
                            
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" 
                                        placeholder="Mobile Number" MaxLength="10" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Button runat="server" ID="btnSendOTP" Text="Send OTP" 
                                        CssClass="btn btn-warning" OnClick="btnSendOTP_Click" 
                                        Enabled="false" />
                                </div>
                            </div>
                            
                            <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" CssClass="form-control" 
                                placeholder="Email Address" />
                            
                            <!-- OTP Verification Section -->
                            <asp:Panel runat="server" ID="pnlOTP" CssClass="otp-section" Visible="false">
                                <h5><i class="fas fa-mobile-alt me-2"></i>Mobile Verification</h5>
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtOTP" CssClass="form-control" 
                                            placeholder="Enter OTP" MaxLength="6" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button runat="server" ID="btnVerifyOTP" Text="Verify OTP" 
                                            CssClass="btn btn-success-custom" OnClick="btnVerifyOTP_Click" />
                                    </div>
                                </div>
                                <div class="otp-timer">
                                    OTP Valid for: <span id="lblOTPTimer">05:00</span>
                                </div>
                                <div class="text-center">
                                    <asp:LinkButton runat="server" ID="btnResendOTP" Text="Resend OTP" 
                                        CssClass="text-decoration-none" OnClick="btnResendOTP_Click" />
                                </div>
                            </asp:Panel>
                            
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" 
                                CssClass="form-control" placeholder="Password" onkeyup="checkPasswordStrength(this.value)" />
                            
                            <div class="progress">
                                <div id="passwordStrengthBar" class="progress-bar" role="progressbar" 
                                    style="width: 0%" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                            </div>
                            <div id="passwordStrengthText" class="password-strength"></div>
                            
                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" 
                                CssClass="form-control" placeholder="Confirm Password" />
                            
                           
                            
                            <asp:Button runat="server" ID="btnSignup" Text="Create Account" 
                                CssClass="btn btn-primary-custom" OnClick="btnSignup_Click"  />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
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

        // Mobile number validation for OTP
        document.getElementById('<%= txtMobile.ClientID %>').addEventListener('input', function (e) {
            var mobile = e.target.value;
            var btnSendOTP = document.getElementById('<%= btnSendOTP.ClientID %>');
            var mobileRegex = /^[6-9]\d{9}$/;

            if (mobileRegex.test(mobile)) {
                btnSendOTP.disabled = false;
                btnSendOTP.className = "btn btn-warning";
            } else {
                btnSendOTP.disabled = true;
                btnSendOTP.className = "btn btn-secondary";
            }
        });

        // OTP Timer
        var timeLeft = 300; // 5 minutes in seconds
        var timerElement = document.getElementById('lblOTPTimer');

        function startOTPTimer() {
            var timer = setInterval(function () {
                if (timeLeft <= 0) {
                    clearInterval(timer);
                    timerElement.innerHTML = "00:00";
                    document.getElementById('<%= btnVerifyOTP.ClientID %>').disabled = true;
                } else {
                    var minutes = Math.floor(timeLeft / 60);
                    var seconds = timeLeft % 60;
                    timerElement.innerHTML = minutes.toString().padStart(2, '0') + ":" + seconds.toString().padStart(2, '0');
                    timeLeft--;
                }
            }, 1000);
        }

        // Start timer when OTP panel becomes visible
        var observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.type === 'attributes' && mutation.attributeName === 'style') {
                    var otpPanel = document.getElementById('<%= pnlOTP.ClientID %>');
                    if (otpPanel.style.display !== 'none') {
                        timeLeft = 300;
                        startOTPTimer();
                    }
                }
            });
        });

        var otpPanel = document.getElementById('<%= pnlOTP.ClientID %>');
        if (otpPanel) {
            observer.observe(otpPanel, { attributes: true });
        }

        // Form validation
        function validateSignupForm() {
            var btnSignup = document.getElementById('<%= btnSignup.ClientID %>');
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
            var confirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
            var otpVerified = '<%= Session["OTPVerified"] != null ? "true" : "false" %>';

            if (termsChecked && password === confirmPassword && password.length >= 6 && otpVerified === 'true') {
                btnSignup.disabled = false;
                btnSignup.className = "btn btn-primary-custom";
            } else {
                btnSignup.disabled = true;
                btnSignup.className = "btn btn-secondary";
            }
        }

        // Attach event listeners
        document.getElementById('<%= txtPassword.ClientID %>').addEventListener('input', validateSignupForm);
        document.getElementById('<%= txtConfirmPassword.ClientID %>').addEventListener('input', validateSignupForm);
    </script>
</body>
</html>
