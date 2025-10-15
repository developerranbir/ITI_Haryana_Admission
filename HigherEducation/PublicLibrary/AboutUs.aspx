<%@ Page Title="" Language="C#" MasterPageFile="~/PublicLibrary/LibraryMaster.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="HigherEducation.PublicLibrary.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --accent-color: #e74c3c;
            --light-color: #ecf0f1;
            --dark-color: #34495e;
        }

        .about-hero {
            background: linear-gradient(135deg, rgba(44, 62, 80, 0.9), rgba(52, 73, 94, 0.9)), url('https://images.unsplash.com/photo-1521587760476-6c12a4b040da?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80');
            background-size: cover;
            background-position: center;
            color: white;
            padding: 80px 0;
            text-align: center;
            margin-bottom: 50px;
        }

        .about-hero h1 {
            font-size: 3rem;
            font-weight: 700;
            margin-bottom: 20px;
        }

        .about-hero p {
            font-size: 1.3rem;
            max-width: 700px;
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

        

        .mission-vision {
            background: rgba(255, 255, 255, 0.7);
            padding: 60px 0;
        }

        .info-card {
            background: white;
            border-radius: 15px;
            padding: 30px;
            margin-bottom: 30px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
            border-left: 5px solid var(--secondary-color);
            transition: all 0.3s ease;
            height: 100%;
        }

        .info-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 15px 35px rgba(0, 0, 0, 0.15);
        }

        .info-card h3 {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 20px;
            display: flex;
            align-items: center;
        }

        .info-card h3 i {
            margin-right: 15px;
            color: var(--secondary-color);
        }

        .contact-info {
            padding: 60px 0;
        }

        .contact-item {
            display: flex;
            align-items: flex-start;
            margin-bottom: 25px;
            padding: 20px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.05);
            transition: all 0.3s ease;
        }

        .contact-item:hover {
            transform: translateX(5px);
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
        }

        .contact-icon {
            width: 60px;
            height: 60px;
            background: linear-gradient(135deg, var(--secondary-color), #2980b9);
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-right: 20px;
            flex-shrink: 0;
        }

        .contact-icon i {
            color: white;
            font-size: 1.5rem;
        }

        .contact-details h4 {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 5px;
        }

        .contact-details p {
            color: #7f8c8d;
            margin-bottom: 0;
            font-size: 1.1rem;
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

        .team-section {
            padding: 60px 0;
            background: rgba(255, 255, 255, 0.7);
        }

        .team-member {
            text-align: center;
            margin-bottom: 30px;
        }

        .member-img {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            margin: 0 auto 20px;
            background: linear-gradient(135deg, var(--secondary-color), #2980b9);
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-size: 3rem;
        }

        .team-member h4 {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 5px;
        }

        .team-member p {
            color: #7f8c8d;
        }

        /* Mobile Responsiveness */
        @media (max-width: 768px) {
            .about-hero h1 {
                font-size: 2.2rem;
            }

            .about-hero p {
                font-size: 1.1rem;
            }

            .contact-item {
                flex-direction: column;
                text-align: center;
            }

            .contact-icon {
                margin-right: 0;
                margin-bottom: 15px;
            }

            .stat-number {
                font-size: 2.5rem;
            }
        }

        @media (max-width: 576px) {
            .about-hero {
                padding: 60px 0;
            }

            .about-hero h1 {
                font-size: 1.8rem;
            }

            .info-card {
                padding: 20px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- About Content -->
    <section class="about-content">
        <div class="container">
        
               
                    <p class="lead text-center mb-5" style="color: white; font-size: 2rem;">
                        The ITI Library and Workshop is a premier educational resource center dedicated to supporting the learning and skill development needs of ITI students and the wider community.
                    </p>
                
            
            <div class="row">
                <div class="col-md-6 mb-4">
                    <div class="info-card">
                        <h3><i class="fas fa-bullseye"></i> Our Mission</h3>
                        <p>To provide accessible, high-quality educational resources and practical workshop facilities that empower individuals to acquire technical skills, enhance their knowledge, and achieve their career goals in industrial trades and technologies.</p>
                    </div>
                </div>
                <div class="col-md-6 mb-4">
                    <div class="info-card">
                        <h3><i class="fas fa-eye"></i> Our Vision</h3>
                        <p>To become the leading center of excellence for technical education resources, fostering innovation, skill development, and lifelong learning for ITI students and the broader community.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Mission & Vision -->
    <section class="mission-vision">
        <div class="container">
            <h2 class="section-title">What We Offer</h2>
            <div class="row">
                <div class="col-md-4 mb-4">
                    <div class="info-card text-center">
                        <div class="member-img">
                            <i class="fas fa-book"></i>
                        </div>
                        <h4>Extensive Library</h4>
                        <p>Access to thousands of technical books, reference materials, and digital resources covering all ITI trades and subjects.</p>
                    </div>
                </div>
                <div class="col-md-4 mb-4">
                    <div class="info-card text-center">
                        <div class="member-img">
                            <i class="fas fa-tools"></i>
                        </div>
                        <h4>Workshop Facilities</h4>
                        <p>State-of-the-art workshop with professional equipment and tools for hands-on practical training and skill development.</p>
                    </div>
                </div>
                <div class="col-md-4 mb-4">
                    <div class="info-card text-center">
                        <div class="member-img">
                            <i class="fas fa-users"></i>
                        </div>
                        <h4>Expert Guidance</h4>
                        <p>Support from experienced instructors and librarians to help you make the most of our resources and facilities.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>

  
    <!-- Contact Information -->
    <section class="contact-info">
        <div class="container">
            <h2 class="section-title">Contact Information</h2>
            <div class="row justify-content-center">
                <div class="col-lg-8">
                    <div class="contact-item">
                        <div class="contact-icon">
                            <i class="fas fa-clock"></i>
                        </div>
                        <div class="contact-details">
                            <h4>Our Timing</h4>
                            <p>9:00 AM to 5:00 PM (All Working Days)</p>
                        </div>
                    </div>
                    
                    <div class="contact-item">
                        <div class="contact-icon">
                            <i class="fas fa-phone"></i>
                        </div>
                        <div class="contact-details">
                            <h4>TollFree Numbers</h4>
                            <p>0172-2996321, 2997265, 2586071</p>
                        </div>
                    </div>
                    
                    <div class="contact-item">
                        <div class="contact-icon">
                            <i class="fas fa-envelope"></i>
                        </div>
                        <div class="contact-details">
                            <h4>Email Us</h4>
                            <p>helpdesk.itiadmission@gmail.com</p>
                        </div>
                    </div>
                    
                    <%--<div class="contact-item">
                        <div class="contact-icon">
                            <i class="fas fa-map-marker-alt"></i>
                        </div>
                        <div class="contact-details">
                            <h4>Visit Us</h4>
                            <p>ITI Campus, Sector 28, Educational Complex, Chandigarh</p>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </section>

  <%--  <!-- Team Section -->
    <section class="team-section">
        <div class="container">
            <h2 class="section-title">Our Team</h2>
            <div class="row">
                <div class="col-md-4">
                    <div class="team-member">
                        <div class="member-img">
                            <i class="fas fa-user-tie"></i>
                        </div>
                        <h4>Dr. Rajesh Kumar</h4>
                        <p>Library Director</p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="team-member">
                        <div class="member-img">
                            <i class="fas fa-user-graduate"></i>
                        </div>
                        <h4>Ms. Priya Sharma</h4>
                        <p>Head Librarian</p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="team-member">
                        <div class="member-img">
                            <i class="fas fa-user-cog"></i>
                        </div>
                        <h4>Mr. Amit Singh</h4>
                        <p>Workshop Supervisor</p>
                    </div>
                </div>
            </div>
        </div>
    </section>--%>
</asp:Content>