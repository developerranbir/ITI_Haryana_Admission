<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HigherEducation.PublicLibrary.Home" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ITI Library Management System</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --accent-color: #e74c3c;
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

        .main-container {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }

        .library-card {
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(10px);
            border-radius: 20px;
            box-shadow: 0 15px 35px rgba(0, 0, 0, 0.1);
            border: 1px solid rgba(255, 255, 255, 0.2);
            overflow: hidden;
            max-width: 1200px;
            width: 100%;
        }

        .library-header {
            background: linear-gradient(135deg, var(--primary-color), var(--dark-color));
            color: white;
            padding: 30px;
            text-align: center;
            position: relative;
        }

            .library-header h1 {
                margin: 0;
                font-size: 2.5rem;
                font-weight: 700;
            }

            .library-header .subtitle {
                font-size: 1.2rem;
                opacity: 0.9;
                margin-top: 10px;
            }

        .library-body {
            padding: 40px;
        }

        .nav-tabs-custom {
            border-bottom: 3px solid var(--light-color);
            margin-bottom: 30px;
        }

            .nav-tabs-custom .nav-link {
                border: none;
                color: var(--dark-color);
                font-weight: 600;
                padding: 15px 30px;
                border-radius: 10px 10px 0 0;
                margin-right: 5px;
            }

                .nav-tabs-custom .nav-link.active {
                    background: var(--secondary-color);
                    color: white;
                    border: none;
                }

        .form-section {
            background: var(--light-color);
            padding: 30px;
            border-radius: 15px;
            margin-bottom: 30px;
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

        .plan-card {
            background: white;
            border-radius: 15px;
            padding: 25px;
            margin-bottom: 25px;
            border: 2px solid transparent;
            transition: all 0.3s ease;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
        }

            .plan-card:hover {
                transform: translateY(-5px);
                border-color: var(--secondary-color);
                box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
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
            margin-bottom: 20px;
        }

        .plan-icon {
            font-size: 2.5rem;
            color: var(--secondary-color);
            margin-bottom: 15px;
        }

        .plan-price {
            font-size: 2rem;
            font-weight: 700;
            color: var(--primary-color);
            margin: 10px 0;
        }

        .plan-period {
            color: #7f8c8d;
            font-size: 0.9rem;
        }

        .plan-features {
            list-style: none;
            padding: 0;
            margin: 20px 0;
        }

            .plan-features li {
                padding: 8px 0;
                border-bottom: 1px solid #ecf0f1;
                color: #2c3e50;
            }

                .plan-features li:last-child {
                    border-bottom: none;
                }

                .plan-features li i {
                    color: #27ae60;
                    margin-right: 10px;
                }

        .section-title {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 30px;
            padding-bottom: 15px;
            border-bottom: 3px solid var(--secondary-color);
            display: inline-block;
        }

        .feature-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin-top: 30px;
        }

        .feature-item {
            text-align: center;
            padding: 20px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 3px 10px rgba(0, 0, 0, 0.1);
        }

            .feature-item i {
                font-size: 2rem;
                color: var(--secondary-color);
                margin-bottom: 15px;
            }

        .alert-info-custom {
            background: linear-gradient(135deg, #d6eaf8, #ebf5fb);
            border: none;
            border-radius: 10px;
            border-left: 5px solid var(--secondary-color);
            padding: 20px;
            margin-bottom: 30px;
        }

        @media (max-width: 768px) {
            .library-body {
                padding: 20px;
            }

            .library-header h1 {
                font-size: 2rem;
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
        <div class="main-container">
            <div class="library-card">
                <div class="library-header">
                    <h1><i class="fas fa-book-reader me-3"></i>ITI Library Management System</h1>
                    <div class="subtitle">Access Knowledge Across Multiple ITI Libraries</div>
                </div>





                <!-- Subscription Plans -->
                <div class="row mt-5">
                    <div class="col-12 text-center">
                        <h2 class="section-title ">Library Subscription Plans</h2>
                    </div>

                    <!-- Reading Plans -->
                    <div class="col-md-6">
                        <div class="plan-card">
                            <div class="plan-header">
                                <div class="plan-icon">
                                    <i class="fas fa-book-open"></i>
                                </div>
                                <h4>Reading / Book Issue</h4>
                                <div class="plan-price">100₹/500₹</div>
                                <div class="plan-period">per month</div>
                            </div>
                            <ul class="plan-features">
                                <li><i class="fas fa-check"></i>Unlimited Reading Access</li>
                                <li><i class="fas fa-check"></i>Book Issuing Facility</li>
                                <li><i class="fas fa-check"></i>Access to All Sections</li>
                                <li><i class="fas fa-check"></i>Digital Resources</li>
                                <li><i class="fas fa-check"></i>Priority Support</li>
                            </ul>

                            <asp:Button runat="server" Text="Choose Plan" CssClass="btn btn-primary-custom" PostBackUrl="Subscription.aspx" />
                        </div>
                    </div>
                    <!-- Workshop Section -->
                    <div class="col-md-6">
                        <div class="plan-card">
                            <div class="plan-header">
                                <div class="plan-icon">
                                    <i class="fas fa-chair"></i>
                                </div>
                                <h4>Workshop Access</h4>
                                <div class="plan-price">₹300</div>
                                <div class="plan-period">per hour</div>
                            </div>
                            <ul class="plan-features">
                                <li><i class="fas fa-check"></i>Access to Workshop Tools</li>
                                <li><i class="fas fa-check"></i>Professional Equipment</li>
                                <li><i class="fas fa-check"></i>Expert Guidance</li>
                                <li><i class="fas fa-check"></i>Safety Equipment</li>
                                <li><i class="fas fa-check"></i>Flexible Timing</li>
                            </ul>
                            <asp:Button runat="server" Text="Choose Plan" CssClass="btn btn-primary-custom" PostBackUrl="~/PublicLibrary/WorkshopSlotBooking.aspx" />
                        </div>
                    </div>
                </div>




                <!-- Features Grid -->
                <div class="row mt-5">
                    <div class="col-12 text-center">
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

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        // Initialize tooltips
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl)
        });

        // Auto-switch to signup if coming from specific referral
        function showSignup() {
            var signupTab = new bootstrap.Tab(document.getElementById('signup-tab'));
            signupTab.show();
        }

        // Show login tab
        function showLogin() {
            var loginTab = new bootstrap.Tab(document.getElementById('login-tab'));
            loginTab.show();
        }
    </script>
</body>
</html>
