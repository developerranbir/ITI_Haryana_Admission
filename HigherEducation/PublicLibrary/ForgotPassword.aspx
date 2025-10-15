<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="HigherEducation.PublicLibrary.ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ITI Library - Forgot Password</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
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

        .btn-warning-custom {
            background: linear-gradient(135deg, var(--warning-color), #e67e22);
            border: none;
            border-radius: 10px;
            padding: 12px 20px;
            font-weight: 600;
            color: white;
            transition: all 0.3s ease;
            width: 100%;
        }

        .otp-section {
            background: linear-gradient(135deg, #fff3cd, #ffeaa7);
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

        .password-section {
            background: linear-gradient(135deg, #d1ecf1, #a2d9ce);
            padding: 25px;
            border-radius: 10px;
            border-left: 4px solid var(--secondary-color);
            margin: 20px 0;
            display: none;
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

        .strength-weak {
            color: var(--danger-color);
        }

        .strength-medium {
            color: var(--warning-color);
        }

        .strength-strong {
            color: var(--success-color);
        }

        .verification-success {
            background: linear-gradient(135deg, #d4edda, #c3e6cb);
            border-left: 4px solid var(--success-color);
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 20px;
            display: none;
        }

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

        @media (max-width: 576px) {
            .auth-body {
                padding: 20px;
            }

            .auth-header h1 {
                font-size: 1.5rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auth-container">
            <div class="auth-card">
                <div class="auth-header">
                    <h1><i class="fas fa-book-reader me-2"></i>ITI Library & Workshop</h1>
                    <div class="subtitle">Reset Your Password</div>
                </div>

                <div class="auth-body">
                    <!-- Alert Messages -->
                    <asp:Panel runat="server" ID="pnlAlert" CssClass="alert alert-custom" Visible="false">
                        <asp:Label runat="server" ID="lblAlertMessage" />
                    </asp:Panel>

                    <a href="Login.aspx" class="btn btn-primary-custom fw-bold w-100 mb-4" style="font-size: 1.1rem;">
                        <i class="fas fa-sign-in-alt me-2"></i>Back to Login
                    </a>

                    <h4 class="text-center mb-4" style="color: var(--primary-color);">Password Recovery</h4>

                    <!-- Mobile Number Section -->
                    <div class="row">
                        <div class="col-md-8">
                            <div class="input-group">
                                <span class="input-group-text"><i class="fas fa-mobile-alt"></i></span>
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control"
                                    placeholder="Registered Mobile Number" autocomplete="off" MaxLength="10" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <asp:Button runat="server" ID="btnSendOTP" Text="Send OTP"
                                CssClass="btn btn-warning-custom" OnClick="btnSendOTP_Click"
                                Enabled="false" />
                        </div>
                    </div>

                    <!-- OTP Verification Section -->
                    <asp:Panel runat="server" ID="pnlOTP" CssClass="otp-section" Visible="false">
                        <h5><i class="fas fa-mobile-alt me-2"></i>Mobile Verification</h5>
                        <p class="text-muted">We've sent a verification code to your registered mobile number</p>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="input-group">
                                    <span class="input-group-text"><i class="fas fa-key"></i></span>
                                    <asp:TextBox runat="server" ID="txtOTP" CssClass="form-control"
                                        placeholder="Enter 6-digit OTP" MaxLength="6" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <asp:Button runat="server" ID="btnVerifyOTP" Text="Verify OTP"
                                    CssClass="btn btn-success-custom" OnClick="btnVerifyOTP_Click" />
                            </div>
                        </div>
                        <div class="otp-timer">
                            <i class="fas fa-clock me-1"></i>OTP Valid for: <span id="lblOTPTimer">05:00</span>
                        </div>
                        <div class="text-center">
                            <asp:LinkButton runat="server" ID="btnResendOTP" Text="Resend OTP"
                                CssClass="text-decoration-none fw-bold" OnClick="btnResendOTP_Click" />
                        </div>
                    </asp:Panel>

                    <!-- Verification Success Message -->
                    <div id="verificationSuccess" class="verification-success">
                        <div class="d-flex align-items-center">
                            <i class="fas fa-check-circle me-2" style="color: var(--success-color); font-size: 1.2rem;"></i>
                            <span class="fw-bold">Mobile number verified successfully!</span>
                        </div>
                    </div>

                    <!-- New Password Section -->
                    <div id="passwordSection" class="password-section">
                        <h5><i class="fas fa-lock me-2"></i>Create New Password</h5>
                        <p class="text-muted">Create a new secure password for your account</p>

                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-lock"></i></span>
                            <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password"
                                CssClass="form-control" placeholder="New Password" onkeyup="checkPasswordStrength(this.value)" />
                        </div>

                        <div class="progress">
                            <div id="passwordStrengthBar" class="progress-bar" role="progressbar"
                                style="width: 0%" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">
                            </div>
                        </div>
                        <div id="passwordStrengthText" class="password-strength"></div>

                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-lock"></i></span>
                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password"
                                CssClass="form-control" placeholder="Confirm New Password" />
                        </div>

                        <asp:Button runat="server" ID="btnResetPassword" Text="Reset Password"
                            CssClass="btn btn-primary-custom mt-3" OnClick="btnResetPassword_Click" />
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

            // Enable/disable reset button based on password strength
            validateResetForm();
        }

        // Mobile number validation for OTP
        document.getElementById('<%= txtMobile.ClientID %>').addEventListener('input', function (e) {
            var mobile = e.target.value;
            var btnSendOTP = document.getElementById('<%= btnSendOTP.ClientID %>');
            var mobileRegex = /^[6-9]\d{9}$/;

            // Remove any non-digit characters
            e.target.value = e.target.value.replace(/\D/g, '');

            if (mobileRegex.test(e.target.value)) {
                btnSendOTP.disabled = false;
                btnSendOTP.className = "btn btn-warning-custom";
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

        // Show OTP section after sending OTP
        function showOTPSection() {
            var otpPanel = document.getElementById('<%= pnlOTP.ClientID %>');
            if (otpPanel) {
                otpPanel.style.display = 'block';
            }
            startOTPTimer();
        }

        // Show password section after OTP verification
        function showPasswordSection() {
            var passwordSection = document.getElementById('passwordSection');
            var verificationSuccess = document.getElementById('verificationSuccess');
            var otpPanel = document.getElementById('<%= pnlOTP.ClientID %>');

            if (passwordSection) passwordSection.style.display = 'block';
            if (verificationSuccess) verificationSuccess.style.display = 'block';
            if (otpPanel) otpPanel.style.display = 'none';
        }

        // Form validation
        function validateResetForm() {
            var btnReset = document.getElementById('<%= btnResetPassword.ClientID %>');
            var password = document.getElementById('<%= txtNewPassword.ClientID %>').value;
            var confirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;

            if (password === confirmPassword && password.length >= 6) {
                if (btnReset) {
                    btnReset.disabled = false;
                    btnReset.className = "btn btn-primary-custom mt-3";
                }
            } else {
                if (btnReset) {
                    btnReset.disabled = true;
                    btnReset.className = "btn btn-secondary mt-3";
                }
            }
        }

        // Attach event listeners
        var newPasswordField = document.getElementById('<%= txtNewPassword.ClientID %>');
        var confirmPasswordField = document.getElementById('<%= txtConfirmPassword.ClientID %>');
        
        if (newPasswordField) {
            newPasswordField.addEventListener('input', validateResetForm);
        }
        if (confirmPasswordField) {
            confirmPasswordField.addEventListener('input', validateResetForm);
        }

        // Allow only numbers in mobile field
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        // Check if OTP is already verified (for page refresh scenarios)
        window.onload = function () {
            // If OTP is verified, show password section
            if (<%= Session["OTPVerified"] != null ? "true" : "false" %>) {
                showPasswordSection();
            }

            // If OTP is sent but not verified, show OTP section
            if (<%= Session["OTP"] != null ? "true" : "false" %>) {
                showOTPSection();
            }
        };

        // Start timer when OTP panel becomes visible
        var observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.type === 'attributes' && mutation.attributeName === 'style') {
                    var otpPanel = document.getElementById('<%= pnlOTP.ClientID %>');
                    if (otpPanel && otpPanel.style.display !== 'none') {
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

        // Prevent paste of non-numeric characters in mobile field
        var mobileField = document.getElementById('<%= txtMobile.ClientID %>');
        if (mobileField) {
            mobileField.addEventListener('paste', function (e) {
                e.preventDefault();
                var text = (e.originalEvent || e).clipboardData.getData('text/plain');
                var numbers = text.replace(/\D/g, '');
                this.value = numbers;
            });
        }
    </script>
</body>
</html>