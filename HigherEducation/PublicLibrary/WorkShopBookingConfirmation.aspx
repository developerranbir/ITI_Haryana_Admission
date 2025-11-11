<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkShopBookingConfirmation.aspx.cs" Inherits="HigherEducation.PublicLibrary.WorkShopBookingConfirmation" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Confirmation</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <style>
        @media print {
            .no-print, .btn {
                display: none !important;
            }
            body {
                padding: 20px;
            }
            .card {
                border: 2px solid #000 !important;
            }
        }
        .confirmation-card {
            max-width: 800px;
            margin: 20px auto;
            border: 2px solid #198754;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <!-- Booking Confirmation Card -->
            <div class="card confirmation-card">
                <div class="card-header bg-success text-white text-center">
                    <h4 class="card-title mb-0" style="margin-left:120px;">
                        Workshop Card
                    </h4>
                    <asp:Image ID="imgQrCode" runat="server" AlternateText="QR Code" Style="height: 120px; float: right;" />

                </div>
                <div class="card-body">
                    <div class="row">
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>Booking ID:</strong>
                                <asp:Literal ID="litBookingId" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>Name:</strong>
                                <asp:Literal ID="litConfName" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>Mobile:</strong>
                                <asp:Literal ID="litConfMobile" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>Email:</strong>
                                <asp:Literal ID="litConfEmail" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>ITI Name:</strong>
                                <asp:Literal ID="litConfITI" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>District:</strong>
                                <asp:Literal ID="litConfDistrict" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>Workshop Date:</strong>
                                <asp:Literal ID="litConfDate" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>Workshop Time:</strong>
                                <asp:Literal ID="litConfTime" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>Duration:</strong>
                                <asp:Literal ID="litConfDuration" runat="server"></asp:Literal>
                            </div>
                            <div class="col col-12 col-md-6 mb-3">
                                <strong>Amount Paid:</strong>
                                ₹<asp:Literal ID="litConfAmount" runat="server"></asp:Literal>
                            </div>
                    </div>
                    
                    <div class="alert alert-info mt-3">
                        <h6><i class="fas fa-info-circle me-2"></i>Important Instructions:</h6>
                        <ul class="mb-0">
                            <li>Please bring this confirmation to the workshop</li>
                            <li>Arrive 15 minutes before the scheduled time</li>
                            <li>Carry a valid government ID proof</li>
                            <li>Keep your mobile number handy for verification</li>
                        </ul>
                    </div>
                </div>
                <div class="card-footer text-center no-print">
                    <button class="btn btn-success me-2" onclick="window.print()">
                        <i class="fas fa-print me-1"></i>Print Confirmation
                    </button>
                    <asp:Button ID="btnGoHome" runat="server" Text="Go to Home" 
                        CssClass="btn btn-secondary" OnClick="btnGoHome_Click" />
                </div>
            </div>
        </div>
    </form>
    
    <script>
        // Auto-print option (uncomment if you want auto-print)
        // window.onload = function() {
        //     window.print();
        // };
    </script>
</body>
</html>