<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HigherEducation.PublicLibrary.Home" MasterPageFile="~/PublicLibrary/LibraryMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --accent-color: #e74c3c;
            --light-color: #ecf0f1;
            --dark-color: #34495e;
        }

        .hero-section {
            background: linear-gradient(135deg, rgba(44, 62, 80, 0.9), rgba(52, 73, 94, 0.9)), url('https://images.unsplash.com/photo-1507842217343-583bb7270b66?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80');
            background-size: cover;
            background-position: center;
            color: white;
            padding: 80px 0;
            text-align: center;
            margin-bottom: 50px;
        }

        .hero-title {
            font-size: 3rem;
            font-weight: 700;
            margin-bottom: 20px;
        }

        .hero-subtitle {
            font-size: 1.3rem;
            margin-bottom: 30px;
            opacity: 0.9;
             color: white;
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

        .features-section {
            padding: 60px 0;
            background: rgba(255, 255, 255, 0.7);
            margin-top: 50px;
        }

        .feature-item {
            text-align: center;
            padding: 30px 20px;
            transition: all 0.3s ease;
        }

        .feature-item:hover {
            transform: translateY(-5px);
        }

        .feature-icon {
            font-size: 3rem;
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

        .stats-section {
            background: linear-gradient(135deg, var(--primary-color), var(--dark-color));
            color: white;
            padding: 60px 0;
            text-align: center;
        }

        .stat-item {
            padding: 20px;
        }

        .stat-number {
            font-size: 3rem;
            font-weight: 700;
            margin-bottom: 10px;
        }

        .stat-label {
            font-size: 1.2rem;
            opacity: 0.9;
        }

        /* Mobile Responsiveness */
        @media (max-width: 768px) {
            .hero-title {
                font-size: 2.2rem;
            }

            .hero-subtitle {
                font-size: 1.1rem;
            }

            .plan-card {
                padding: 20px;
                margin-bottom: 20px;
            }

            .plan-icon {
                font-size: 2.5rem;
            }

            .plan-price {
                font-size: 2rem;
            }

            .features-section, .stats-section {
                padding: 40px 0;
            }

            .feature-icon {
                font-size: 2.5rem;
            }
        }

        @media (max-width: 576px) {
            .hero-section {
                padding: 60px 0;
            }

            .hero-title {
                font-size: 1.8rem;
            }

            .plan-card {
                padding: 15px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <!-- Subscription Plans -->
    <section class="plans-section">
        <div class="container">
              
            <div class="row justify-content-center">
                <h2 class="section-title"><p class="hero-subtitle">Access a vast collection of resources and book workshop sessions for your educational needs</p></h2>
            
                <!-- Reading Plans -->
                <div class="col-lg-5 col-md-6 mb-4">
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

                         <a href="Subscription.aspx" class="btn btn-primary-custom">
     <i class="fas fa-book me-2"></i>Browse Library
 </a>
                        <%--<asp:Button runat="server" Text="View Details" CssClass="btn btn-primary-custom" PostBackUrl="Subscription.aspx" />--%>
                    </div>
                </div>
                
                <!-- Workshop Section -->
                <div class="col-lg-5 col-md-6 mb-4">
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
                          <a href="WorkshopSlotBooking.aspx" class="btn btn-primary-custom" style="background: linear-gradient(135deg, var(--accent-color), #c0392b);">
      <i class="fas fa-tools me-2"></i>Book Workshop
  </a>
                        <%--<asp:Button runat="server" Text="Book Now" CssClass="btn btn-primary-custom" PostBackUrl="~/PublicLibrary/WorkshopSlotBooking.aspx" />--%>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Features Section -->
    <section class="features-section">
        <div class="container">
            <h2 class="section-title">Why Choose Us?</h2>
            <div class="row">
                <div class="col-lg-3 col-md-6 mb-4">
                    <div class="feature-item">
                        <div class="feature-icon">
                            <i class="fas fa-university"></i>
                        </div>
                        <h5>Multiple ITI Libraries</h5>
                        <p>Access books from various ITI libraries across the region with a single subscription</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 mb-4">
                    <div class="feature-item">
                        <div class="feature-icon">
                            <i class="fas fa-books"></i>
                        </div>
                        <h5>Vast Collection</h5>
                        <p>Thousands of books covering all ITI subjects and specializations</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 mb-4">
                    <div class="feature-item">
                        <div class="feature-icon">
                            <i class="fas fa-clock"></i>
                        </div>
                        <h5>Flexible Timing</h5>
                        <p>Open all weekdays with extended hours to accommodate your schedule</p>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 mb-4">
                    <div class="feature-item">
                        <div class="feature-icon">
                            <i class="fas fa-user-graduate"></i>
                        </div>
                        <h5>Student Focused</h5>
                        <p>Resources and facilities designed specifically for ITI students</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Stats Section -->
    <section class="stats-section">
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-6">
                    <div class="stat-item">
                        <div class="stat-number">10,000+</div>
                        <div class="stat-label">Books Available</div>
                    </div>
                </div>
                <div class="col-md-3 col-6">
                    <div class="stat-item">
                        <div class="stat-number">190+</div>
                        <div class="stat-label">ITI Libraries</div>
                    </div>
                </div>
                <div class="col-md-3 col-6">
                    <div class="stat-item">
                        <div class="stat-number">50,000+</div>
                        <div class="stat-label">Happy Students</div>
                    </div>
                </div>
                <div class="col-md-3 col-6">
                    <div class="stat-item">
                        <div class="stat-number">50+</div>
                        <div class="stat-label">Workshop Tools</div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>