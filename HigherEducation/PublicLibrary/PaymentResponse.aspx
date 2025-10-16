<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentResponse.aspx.cs" Inherits="HigherEducation.PublicLibrary.PaymentResponse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Response</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .payment-status {
            max-width: 600px;
            margin: 40px auto;
        }
        .status-success { border-left: 5px solid #28a745; }
        .status-failure { border-left: 5px solid #dc3545; }
        .status-pending { border-left: 5px solid #ffc107; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Payment Status Display -->
            <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="card payment-status status-success">
                <div class="card-body text-center">
                    <div class="mb-4">
                        <i class="fas fa-check-circle fa-4x text-success mb-3"></i>
                        <h2 class="card-title text-success">Payment Successful!</h2>
                        <p class="card-text">Your workshop booking payment has been completed successfully.</p>
                    </div>
                    
                    <div class="row text-start">
                        <div class="col-md-6">
                            <p><strong>Booking ID:</strong> <asp:Literal ID="litBookingIdSuccess" runat="server"></asp:Literal></p>
                            <p><strong>Transaction ID:</strong> <asp:Literal ID="litTransactionId" runat="server"></asp:Literal></p>
                            <p><strong>Amount Paid:</strong> ₹<asp:Literal ID="litAmountPaid" runat="server"></asp:Literal></p>
                        </div>
                        <div class="col-md-6">
                            <p><strong>Payment Date:</strong> <asp:Literal ID="litPaymentDate" runat="server"></asp:Literal></p>
                            <p><strong>Payment Mode:</strong> <asp:Literal ID="litPaymentMode" runat="server"></asp:Literal></p>
                            <p><strong>Status:</strong> <span class="badge bg-success">Completed</span></p>
                        </div>
                    </div>
                    
                    <div class="mt-4">
                        <asp:Button ID="btnViewBooking" runat="server" Text="Print Card" 
                            CssClass="btn btn-success me-2" OnClick="btnViewBooking_Click" />
                        <asp:Button ID="btnPrintReceipt" runat="server" Text="Print Receipt" 
                            CssClass="btn btn-outline-primary me-2" OnClientClick="window.print();return false;" />
                        <asp:Button ID="btnNewBooking" runat="server" Text="Book Another Slot" 
                            CssClass="btn btn-primary" OnClick="btnNewBooking_Click" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlFailure" runat="server" Visible="false" CssClass="card payment-status status-failure">
                <div class="card-body text-center">
                    <div class="mb-4">
                        <i class="fas fa-times-circle fa-4x text-danger mb-3"></i>
                        <h2 class="card-title text-danger">Payment Failed</h2>
                        <p class="card-text">Your payment could not be processed. Please try again.</p>
                    </div>
                    
                    <div class="alert alert-warning text-start">
                        <strong>Reason:</strong> <asp:Literal ID="litFailureReason" runat="server"></asp:Literal>
                    </div>
                    
                    <div class="mt-4">
                        <asp:Button ID="btnRetryPayment" runat="server" Text="Retry Payment" 
                            CssClass="btn btn-danger me-2" OnClick="btnRetryPayment_Click" />
                        <asp:Button ID="btnContactSupport" runat="server" Text="Contact Support" 
                            CssClass="btn btn-outline-secondary me-2" OnClick="btnContactSupport_Click" />
                        <asp:Button ID="btnBackToBookings" runat="server" Text="My Bookings" 
                            CssClass="btn btn-primary" OnClick="btnBackToBookings_Click" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlPending" runat="server" Visible="false" CssClass="card payment-status status-pending">
                <div class="card-body text-center">
                    <div class="mb-4">
                        <i class="fas fa-clock fa-4x text-warning mb-3"></i>
                        <h2 class="card-title text-warning">Payment Pending</h2>
                        <p class="card-text">Your payment is being processed. Please check back later.</p>
                    </div>
                    
                    <div class="mt-4">
                        <asp:Button ID="btnCheckStatus" runat="server" Text="Check Status" 
                            CssClass="btn btn-warning me-2" OnClick="btnCheckStatus_Click" />
                        <asp:Button ID="btnPendingBack" runat="server" Text="My Bookings" 
                            CssClass="btn btn-primary" OnClick="btnPendingBack_Click" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Loading Panel -->
            <asp:Panel ID="pnlLoading" runat="server" CssClass="text-center mt-5">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Processing payment...</span>
                </div>
                <p class="mt-3">Processing your payment, please wait...</p>
            </asp:Panel>
        </div>
    </form>
</body>
</html>