<%@ Page Title="Workshop Reports" Language="C#" AutoEventWireup="true" CodeBehind="ITIWorkshopReports.aspx.cs" Inherits="HigherEducation.PublicLibrary.ITIWorkshopReports" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ITI Workshop Reports</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <!-- SweetAlert2 CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" />
    <!-- DataTables CSS -->
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/2.4.1/css/buttons.bootstrap5.min.css" />

    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --accent-color: #e74c3c;
            --light-color: #ecf0f1;
            --dark-color: #34495e;
        }

        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .navbar-custom {
            background: linear-gradient(135deg, var(--primary-color), var(--dark-color));
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

        .section-title {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 3px solid var(--secondary-color);
        }

        .report-card {
            background: white;
            border-radius: 15px;
            padding: 25px;
            margin-bottom: 30px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
            border-left: 4px solid #3498db;
        }

        .filter-section {
            background: linear-gradient(135deg, #f8f9fa, #e9ecef);
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 25px;
        }

        .booking-status {
            padding: 4px 12px;
            border-radius: 20px;
            font-size: 0.8rem;
            font-weight: 600;
        }

        .status-confirmed {
            background: #d4edda;
            color: #155724;
        }

        .status-pending {
            background: #fff3cd;
            color: #856404;
        }

        .export-buttons {
            margin-bottom: 20px;
        }

        .summary-section {
            background: #fff3cd;
            border-radius: 8px;
            padding: 15px;
            margin: 15px 0;
            border-left: 4px solid #f39c12;
        }

        .footer {
            background: var(--primary-color);
            color: white;
            padding: 20px 0;
            margin-top: 40px;
        }

        .loading-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(255, 255, 255, 0.9);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 9999;
            display: none;
        }

        .user-info {
            background: linear-gradient(135deg, #d6eaf8, #ebf5fb);
            border-radius: 10px;
            padding: 15px;
            margin-bottom: 20px;
        }

        .summary-section {
            background: linear-gradient(135deg, #f8f9fa, #e9ecef);
            border-radius: 12px;
            padding: 20px;
            margin: 15px 0;
            border-left: 4px solid #3498db;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }

            .summary-section .bg-success {
                background-color: #28a745 !important;
            }

            .summary-section .bg-warning {
                background-color: #ffc107 !important;
            }

            .summary-section .bg-primary {
                background-color: #007bff !important;
            }

            .summary-section .bg-info {
                background-color: #17a2b8 !important;
            }

            .summary-section .rounded-circle {
                width: 50px;
                height: 50px;
                display: flex;
                align-items: center;
                justify-content: center;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>

        <!-- Loading Overlay -->
        <div class="loading-overlay" id="loadingOverlay">
            <div class="text-center">
                <div class="spinner-border text-primary" style="width: 3rem; height: 3rem;" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
                <p class="mt-3">Loading...</p>
            </div>
        </div>

        <!-- Navigation -->
        <nav class="navbar navbar-expand-lg navbar-dark navbar-custom">
            <div class="container">
                <a class="navbar-brand" href="#">
                    <i class="fas fa-chart-line me-2"></i>
                    <strong>ITI Workshop Reports</strong>
                </a>
                <div class="navbar-nav ms-auto">
                    <div class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown">
                            <i class="fas fa-user-circle me-1"></i>
                            <asp:Literal ID="litUserName" runat="server"></asp:Literal>
                        </a>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="../DHE/HEMenu.aspx"><i class="fas fa-tachometer-alt me-2"></i>Dashboard</a></li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkLogout" runat="server" CssClass="dropdown-item" OnClick="lnkLogout_Click">
                                    <i class="fas fa-sign-out-alt me-2"></i>Logout
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </nav>

        <!-- Hidden field for SweetAlert -->
        <asp:HiddenField ID="hdnSweetAlertMessage" runat="server" />
        <asp:HiddenField ID="hdnSweetAlertType" runat="server" />
        <asp:HiddenField ID="hdnSweetAlertTitle" runat="server" />

        <div class="container mt-4">
            <!-- User Info -->
            <div class="user-info">
                <div class="row">
                    <div class="col-md-4">
                        <strong><i class="fas fa-building me-2"></i>ITI:</strong>
                        <span class="ms-2">
                            <asp:Literal ID="litITIName" runat="server"></asp:Literal>
                        </span>
                    </div>
                    <div class="col-md-4">
                        <strong><i class="fas fa-calendar me-2"></i>Current Date:</strong>
                        <span class="ms-2">
                            <asp:Literal ID="litCurrentDate" runat="server"></asp:Literal>
                        </span>
                    </div>
                    <div class="col-md-4">
                        <strong><i class="fas fa-clock me-2"></i>Current Time:</strong>
                        <span class="ms-2">
                            <asp:Literal ID="litCurrentTime" runat="server"></asp:Literal>
                        </span>
                    </div>
                </div>
            </div>

            <!-- Filter Section -->
            <div class="report-card">
                <h4 class="mb-4"><i class="fas fa-filter me-2"></i>Filter Bookings</h4>

                <asp:UpdatePanel ID="upFilter" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="filter-section">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="mb-3">
                                        <label class="form-label">Report Type</label>
                                        <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                            <asp:ListItem Value="DAILY">Daily Report</asp:ListItem>
                                            <asp:ListItem Value="RANGE">Date Range Report</asp:ListItem>
                                            <asp:ListItem Value="MONTHLY">Monthly Report</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlDailyDate" runat="server" CssClass="col-md-4">
                                    <div class="mb-3">
                                        <label class="form-label">Select Date</label>
                                        <asp:TextBox ID="txtReportDate" runat="server" CssClass="form-control"
                                            TextMode="Date" AutoPostBack="true" OnTextChanged="txtReportDate_TextChanged"></asp:TextBox>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlDateRange" runat="server" CssClass="col-md-8" Visible="false">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="mb-3">
                                                <label class="form-label">From Date</label>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
                                                    TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="mb-3">
                                                <label class="form-label">To Date</label>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"
                                                    TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlMonthly" runat="server" CssClass="col-md-4" Visible="false">
                                    <div class="mb-3">
                                        <label class="form-label">Select Month</label>
                                        <asp:TextBox ID="txtMonth" runat="server" CssClass="form-control"
                                            TextMode="Month"></asp:TextBox>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlWorkshopSlot" runat="server" CssClass="col-md-4" Visible="false">
                                    <div class="mb-3">
                                        <label class="form-label">Workshop Slot</label>
                                        <asp:DropDownList ID="ddlWorkshopSlot" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="">All Slots</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>

                                <div class="col-md-4">
                                    <div class="mb-3">
                                        <label class="form-label">Booking Status</label>
                                        <asp:DropDownList ID="ddlBookingStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="">All Status</asp:ListItem>
                                            <asp:ListItem Value="CONFIRMED">Confirmed</asp:ListItem>
                                            <asp:ListItem Value="PENDING">Pending</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report"
                                        CssClass="btn btn-primary" OnClick="btnGenerateReport_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset Filters"
                                        CssClass="btn btn-outline-secondary" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <!-- Summary Section -->
            <asp:UpdatePanel ID="upSummary" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlSummary" runat="server" Visible="false" CssClass="summary-section">
                        <div class="row">
                            <!-- Confirmed Bookings -->
                            <div class="col-md-3">
                                <div class="d-flex align-items-center">
                                    <div class="bg-success rounded-circle p-2 me-3">
                                        <i class="fas fa-check-circle text-white"></i>
                                    </div>
                                    <div>
                                        <div class="fw-bold text-success">
                                            <asp:Literal ID="litConfirmedCount" runat="server" Text="0"></asp:Literal>
                                        </div>
                                        <small class="text-muted">Confirmed Bookings</small>
                                        <div class="fw-bold text-success">
                                            ₹<asp:Literal ID="litConfirmedAmount" runat="server" Text="0"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Pending Bookings -->
                            <div class="col-md-3">
                                <div class="d-flex align-items-center">
                                    <div class="bg-warning rounded-circle p-2 me-3">
                                        <i class="fas fa-clock text-white"></i>
                                    </div>
                                    <div>
                                        <div class="fw-bold text-warning">
                                            <asp:Literal ID="litPendingCount" runat="server" Text="0"></asp:Literal>
                                        </div>
                                        <small class="text-muted">Pending Bookings</small>
                                        <div class="fw-bold text-warning">
                                            ₹<asp:Literal ID="litPendingAmount" runat="server" Text="0"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Total Summary -->
                            <div class="col-md-3">
                                <div class="d-flex align-items-center">
                                    <div class="bg-primary rounded-circle p-2 me-3">
                                        <i class="fas fa-chart-bar text-white"></i>
                                    </div>
                                    <div>
                                        <div class="fw-bold text-primary">
                                            <asp:Literal ID="litTotalCount" runat="server" Text="0"></asp:Literal>
                                        </div>
                                        <small class="text-muted">Total Bookings</small>
                                        <div class="fw-bold text-primary">
                                            ₹<asp:Literal ID="litTotalAmount" runat="server" Text="0"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Date Range -->
                            <div class="col-md-3">
                                <div class="d-flex align-items-center">
                                    <div class="bg-info rounded-circle p-2 me-3">
                                        <i class="fas fa-calendar-alt text-white"></i>
                                    </div>
                                    <div>
                                        <div class="fw-bold">
                                            <asp:Literal ID="litSummaryDateRange" runat="server"></asp:Literal>
                                        </div>
                                        <small class="text-muted">Report Period</small>
                                        <div class="text-muted small">
                                            <asp:Literal ID="litReportType" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Export Buttons -->
            <asp:Panel ID="pnlExport" runat="server" Visible="false" CssClass="export-buttons">
                <div class="d-flex gap-2">
                    <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel"
                        CssClass="btn btn-success" OnClick="btnExportExcel_Click" />
                    <asp:Button ID="btnPrint" runat="server" Text="Print Report"
                        CssClass="btn btn-info" OnClick="btnPrint_Click" />
                </div>
            </asp:Panel>

            <!-- Reports Grid -->
            <asp:UpdatePanel ID="upReports" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="report-card">
                        <h4 class="mb-4"><i class="fas fa-table me-2"></i>Booking Details</h4>

                        <asp:Panel ID="pnlNoData" runat="server" Visible="false" CssClass="text-center py-4">
                            <i class="fas fa-inbox fa-3x text-muted mb-3"></i>
                            <h5 class="text-muted">No bookings found for the selected criteria</h5>
                            <p class="text-muted">Try adjusting your filters or select a different date range.</p>
                        </asp:Panel>

                        <asp:Panel ID="pnlGrid" runat="server" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-striped table-bordered table-hover nowrap"
                                    OnRowDataBound="gvBookings_RowDataBound"
                                    DataKeyNames="BookingID"
                                    ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:BoundField DataField="BookingID" HeaderText="Booking ID" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="FullName" HeaderText="Student Name" ItemStyle-CssClass="text-nowrap" />
                                        <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" ItemStyle-CssClass="text-nowrap" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="text-truncate" ItemStyle-Width="200px" />
                                        <asp:BoundField DataField="WorkshopDate" HeaderText="Workshop Date"
                                            DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="StartTime" HeaderText="Start Time" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="EndTime" HeaderText="End Time" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="Duration" HeaderText="Duration (hrs)"
                                            DataFormatString="{0:N1}" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="BookingAmount" HeaderText="Amount"
                                            DataFormatString="₹{0:N0}" HtmlEncode="false" ItemStyle-CssClass="text-right" />
                                        <asp:TemplateField HeaderText="Booking Status" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server"
                                                    Text='<%# Eval("BookingStatus") %>'
                                                    CssClass='<%# GetStatusClass(Eval("BookingStatus")?.ToString()) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentStatus" runat="server"
                                                    Text='<%# Eval("PaymentStatus") %>'
                                                    CssClass='<%# GetPaymentStatusClass(Eval("PaymentStatus")?.ToString()) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreatedDate" HeaderText="Booked On"
                                            DataFormatString="{0:dd-MMM-yyyy HH:mm}" HtmlEncode="false" ItemStyle-CssClass="text-center" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="text-center py-4">
                                            <i class="fas fa-inbox fa-2x text-muted mb-3"></i>
                                            <p class="text-muted">No bookings found for the selected criteria</p>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <!-- Footer -->
        <footer class="footer">
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <p>&copy; 2024 ITI Workshop Management System. All rights reserved.</p>
                    </div>
                    <div class="col-md-6 text-end">
                        <p>
                            Generated on:
                            <asp:Literal ID="litFooterDate" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
            </div>
        </footer>

        <!-- Bootstrap JS -->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
        <!-- jQuery -->
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <!-- SweetAlert2 JS -->
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        <!-- DataTables JS -->
        <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
        <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap5.min.js"></script>

        <script type="text/javascript">
            // SweetAlert configuration
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 5000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            });

            // Function to show SweetAlert messages
            function showSweetAlert(title, message, type) {
                if (type === 'success') {
                    Toast.fire({
                        icon: 'success',
                        title: title,
                        text: message
                    });
                } else if (type === 'error') {
                    Swal.fire({
                        icon: 'error',
                        title: title,
                        text: message,
                        confirmButtonColor: '#3085d6',
                    });
                } else if (type === 'warning') {
                    Swal.fire({
                        icon: 'warning',
                        title: title,
                        text: message,
                        confirmButtonColor: '#3085d6',
                    });
                } else if (type === 'info') {
                    Swal.fire({
                        icon: 'info',
                        title: title,
                        text: message,
                        confirmButtonColor: '#3085d6',
                    });
                } else {
                    Toast.fire({
                        icon: 'info',
                        title: title,
                        text: message
                    });
                }
            }

            // Show loading overlay
            function showLoading() {
                document.getElementById('loadingOverlay').style.display = 'flex';
            }

            // Hide loading overlay
            function hideLoading() {
                document.getElementById('loadingOverlay').style.display = 'none';
            }



            // Check for server-side SweetAlert messages
            function checkForAlerts() {
                var message = document.getElementById('<%= hdnSweetAlertMessage.ClientID %>').value;
                var type = document.getElementById('<%= hdnSweetAlertType.ClientID %>').value;
                var title = document.getElementById('<%= hdnSweetAlertTitle.ClientID %>').value;

                if (message && type && title) {
                    showSweetAlert(title, message, type);
                    // Clear the hidden fields
                    document.getElementById('<%= hdnSweetAlertMessage.ClientID %>').value = '';
                    document.getElementById('<%= hdnSweetAlertType.ClientID %>').value = '';
                    document.getElementById('<%= hdnSweetAlertTitle.ClientID %>').value = '';
                }
            }

            // Initialize on page load
            document.addEventListener('DOMContentLoaded', function () {
                checkForAlerts();

                // Set default dates
                var today = new Date();
                var dd = String(today.getDate()).padStart(2, '0');
                var mm = String(today.getMonth() + 1).padStart(2, '0');
                var yyyy = today.getFullYear();
                var todayFormatted = yyyy + '-' + mm + '-' + dd;

                // Set default date values
                var reportDate = document.getElementById('<%= txtReportDate.ClientID %>');
                if (reportDate) reportDate.value = todayFormatted;

                var fromDate = document.getElementById('<%= txtFromDate.ClientID %>');
                if (fromDate) fromDate.value = todayFormatted;

                var toDate = document.getElementById('<%= txtToDate.ClientID %>');
                if (toDate) toDate.value = todayFormatted;

                var month = document.getElementById('<%= txtMonth.ClientID %>');
                if (month) month.value = yyyy + '-' + mm;
            });

            // Handle postbacks
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function (sender, args) {
                showLoading();
            });

            prm.add_endRequest(function (sender, args) {
                hideLoading();
                checkForAlerts();

                // Re-initialize DataTable after postback if grid is visible
                if ($('#<%= gvBookings.ClientID %>').length && $('#<%= gvBookings.ClientID %>').is(':visible')) {
                    initializeDataTable();
                }
            });

            // Initialize DataTable when grid becomes visible
            function initDataTableIfVisible() {
                if ($('#<%= gvBookings.ClientID %>').length && $('#<%= gvBookings.ClientID %>').is(':visible')) {
                    initializeDataTable();
                }
            }

            // Call this after the page loads and grid might be visible
            setTimeout(initDataTableIfVisible, 1000);


            // Enhanced Print function
            function printReport() {
                // Store original styles
                var originalStyles = {};
                var elementsToHide = document.querySelectorAll('.navbar-custom, .export-buttons, .footer, .filter-section, .section-title');

                // Hide elements that shouldn't be printed
                elementsToHide.forEach(function (element) {
                    originalStyles[element] = element.style.display;
                    element.style.display = 'none';
                });

                // Add print-specific styles
                var printStyles = `
        <style>
            @media print {
                body { 
                    font-size: 12pt; 
                    background: white !important; 
                    color: black !important;
                    margin: 0;
                    padding: 20px;
                }
                .container {
                    max-width: 100% !important;
                    width: 100% !important;
                    margin: 0 !important;
                    padding: 0 !important;
                }
                .report-card { 
                    box-shadow: none !important; 
                    border: 1px solid #ddd !important;
                    margin: 10px 0 !important;
                    padding: 15px !important;
                }
                .table { 
                    font-size: 10pt; 
                    width: 100% !important;
                    border-collapse: collapse !important;
                }
                .table th,
                .table td {
                    padding: 6px !important;
                    border: 1px solid #ddd !important;
                }
                .summary-section {
                    background: #f8f9fa !important;
                    border: 1px solid #ddd !important;
                    margin: 10px 0 !important;
                }
                .badge, .booking-status { 
                    border: 1px solid #000 !important;
                    color: #000 !important;
                    background: transparent !important;
                    padding: 2px 6px !important;
                }
                .btn, .no-print { display: none !important; }
                
                /* Ensure table breaks properly across pages */
                table { 
                    page-break-inside: auto !important; 
                }
                tr { 
                    page-break-inside: avoid !important; 
                    page-break-after: auto !important; 
                }
                thead { 
                    display: table-header-group !important; 
                }
                tfoot { 
                    display: table-footer-group !important; 
                }
                
                /* Hide user info in print */
                .user-info {
                    display: none !important;
                }
            }
            
            @page {
                margin: 1cm;
                size: landscape;
            }
        </style>
    `;

                // Get the reports content
                var reportsContent = document.getElementById('upReports');
                if (!reportsContent) {
                    alert('No report data available to print.');
                    return;
                }

                // Create print window
                var printWindow = window.open('', '_blank');
                var currentDate = new Date().toLocaleString();

                printWindow.document.write(`
        <html>
            <head>
                <title>ITI Workshop Report - ${new Date().toISOString().slice(0, 19).replace(/:/g, '')}</title>
                ${printStyles}
            </head>
            <body>
                <div style="text-align: center; margin-bottom: 20px; border-bottom: 2px solid #000; padding-bottom: 10px;">
                    <h2 style="margin: 0; color: #000;">ITI WORKSHOP BOOKINGS REPORT</h2>
                    <p style="margin: 5px 0; font-size: 12pt;">
                        <strong>ITI:</strong> ${document.getElementById('<%= litITIName.ClientID %>')?.innerText || 'N/A'} |
                        <strong>Report Type:</strong> ${document.querySelector('[id*="ddlReportType"] option:checked')?.text || 'N/A'} |
                        <strong>Date Range:</strong> ${document.getElementById('<%= litSummaryDateRange.ClientID %>')?.innerText || 'N/A'}
                    </p>
                    <p style="margin: 5px 0; font-size: 11pt;">
                        Generated on: ${currentDate}
                    </p>
                </div>
                ${reportsContent.innerHTML}
            </body>
        </html>
    `);

    printWindow.document.close();

    // Wait for content to load before printing
    setTimeout(function () {
        printWindow.focus();
        printWindow.print();

        // Close window after print
        setTimeout(function () {
            printWindow.close();
        }, 500);

        // Restore original styles
        elementsToHide.forEach(function (element) {
            if (originalStyles[element] !== undefined) {
                element.style.display = originalStyles[element];
            }
        });
    }, 500);
}

        </script>
        <script type="text/javascript">
            function initializeDataTable() {
                try {
                    var table = $('#<%= gvBookings.ClientID %>');

                    if (table.length === 0) {
                        console.log('Table not found');
                        return;
                    }

                    // Check if table has headers and data
                    var headerCount = table.find('thead th').length;
                    var firstRowCount = table.find('tbody tr:first td').length;

                    console.log('Header columns: ' + headerCount + ', First row columns: ' + firstRowCount);

                    if (headerCount === 0 || firstRowCount === 0) {
                        console.log('No headers or data found');
                        return;
                    }

                    if (headerCount !== firstRowCount) {
                        console.warn('Column count mismatch: Headers=' + headerCount + ', Data=' + firstRowCount);
                    }

                    // Destroy existing instance
                    if ($.fn.DataTable.isDataTable(table)) {
                        table.DataTable().destroy();
                        table.removeClass('dataTable');
                    }

                    // Initialize with error handling
                    var dataTable = table.DataTable({
                        "responsive": true,
                        "lengthChange": true,
                        "autoWidth": false,
                        "pageLength": 25,
                        "order": [[0, 'desc']],
                        "language": {
                            "emptyTable": "No bookings found",
                            "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                            "infoEmpty": "Showing 0 to 0 of 0 entries",
                            "infoFiltered": "(filtered from _MAX_ total entries)",
                            "lengthMenu": "Show _MENU_ entries",
                            "search": "Search:",
                            "zeroRecords": "No matching records found"
                        },
                        "drawCallback": function (settings) {
                            console.log('DataTable draw complete');
                        },
                        "error": function (settings, techNote, message) {
                            console.error('DataTables error: ', message);
                        }
                    });

                    console.log('DataTable initialized successfully');

                } catch (error) {
                    console.error('Error initializing DataTable:', error);
                }
            }

            // Initialize on document ready
            $(document).ready(function () {
                initializeDataTable();
            });

            // Reinitialize after async postbacks
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                initializeDataTable();
            });

        </script>
    </form>
</body>
</html>
