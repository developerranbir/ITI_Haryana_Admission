<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HigherEducation.PublicLibrary.Login" %>

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
                    <div class="subtitle">Login to Continue</div>
                </div>
                
                <div class="auth-body">
                    <!-- Alert Messages -->
                    <asp:Panel runat="server" ID="pnlAlert" CssClass="alert alert-custom" Visible="false">
                        <asp:Label runat="server" ID="lblAlertMessage" />
                    </asp:Panel>

                      <a href="SignUp.aspx" class="btn btn-primary-custom fw-bold w-100 mb-3" style="font-size:1.1rem;">Signup if not done previously</a>
                   

                    <!-- Tab Content -->
                    <div class="tab-content" id="authTabsContent">
                        <!-- Login Tab -->
                        <div class="tab-pane fade show active" id="login" role="tabpanel">
                            <h4 class="text-center mb-4">Login to Your Account</h4>
                            
                            <asp:TextBox runat="server" ID="txtLoginId" CssClass="form-control" 
                                placeholder="Enter Mobile Number or Email Address" />
                            
                            <asp:TextBox runat="server" ID="txtLoginPassword" TextMode="Password" 
                                CssClass="form-control" placeholder="Enter Password" />
                            
                           
                            
                            <asp:Button runat="server" ID="btnLogin" Text="Login" 
                                CssClass="btn btn-primary-custom " OnClick="btnLogin_Click" />
                            
                            <div class="text-center mt-3">
                                <a href="ForgotPassword.aspx" class="text-decoration-none">Forgot Password?</a>
                            </div>
                        </div>

                       
                    </div>
                </div>
            </div>
        </div>
    </form>

    
</body>
</html>

