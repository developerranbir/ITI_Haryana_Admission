<%@ Page Title="Workshop Reports" Language="C#" AutoEventWireup="true" CodeBehind="ITIWorkshopReports.aspx.cs" Inherits="HigherEducation.PublicLibrary.ITIWorkshopReports" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ITI Workshop Reports</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <!-- SweetAlert2 CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <!-- DataTables CSS -->
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/2.4.1/css/buttons.bootstrap5.min.css">

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
                            <li><a class="dropdown-item" href="ITIDashboard.aspx"><i class="fas fa-tachometer-alt me-2"></i>Dashboard</a></li>
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
                                <div class="col-md-4">
                                    <div class="mb-3">
                                        <label class="form-label">Workshop Slot</label>
                                        <asp:DropDownList ID="ddlWorkshopSlot" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="">All Slots</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <!-- Date Selection based on Report Type -->
                            <div class="row">
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
                            <div class="col-md-4">
                                <strong>Total Bookings:</strong>
                                <asp:Literal ID="litSummaryTotal" runat="server"></asp:Literal>
                            </div>
                            <div class="col-md-4">
                                <strong>Total Amount:</strong>
                                ₹<asp:Literal ID="litSummaryAmount" runat="server"></asp:Literal>
                            </div>
                            <div class="col-md-4">
                                <strong>Date Range:</strong>
                                <asp:Literal ID="litSummaryDateRange" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Export Buttons -->
            <asp:UpdatePanel ID="upExport" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlExport" runat="server" Visible="false" CssClass="export-buttons">
                        <div class="d-flex gap-2">
                            <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel"
                                CssClass="btn btn-success" OnClick="btnExportExcel_Click" />
                            <asp:Button ID="btnPrint" runat="server" Text="Print Report"
                                CssClass="btn btn-info" OnClick="btnPrint_Click" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

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
                                    CssClass="table table-striped table-bordered table-hover"
                                    OnRowDataBound="gvBookings_RowDataBound"
                                    DataKeyNames="BookingID"
                                    ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:BoundField DataField="BookingID" HeaderText="Booking ID" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="FullName" HeaderText="Student Name" ItemStyle-CssClass="text-nowrap" />
                                        <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" ItemStyle-CssClass="text-nowrap" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="text-truncate" />
                                        <asp:BoundField DataField="WorkshopDate" HeaderText="Workshop Date"
                                            DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="StartTime" HeaderText="Start Time" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="EndTime" HeaderText="End Time" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="Duration" HeaderText="Duration (hrs)"
                                            DataFormatString="{0:N1}" ItemStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="BookingAmount" HeaderText="Amount"
                                            DataFormatString="₹{0:N0}" HtmlEncode="false" ItemStyle-CssClass="text-right" />
                                        <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="text-center">
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
                                            DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ItemStyle-CssClass="text-center" />
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

            // Print function
            function printReport() {
                window.print();
            }
        </script>
        <script type="text/javascript">
            function initializeDataTable() {
                var table = $('#<%= gvBookings.ClientID %>');

                // Check if table exists and has data
                if (table.length > 0 && table.find('tbody tr').length > 0) {
                    // Destroy existing DataTable instance if exists
                    if ($.fn.DataTable.isDataTable(table)) {
                        table.DataTable().destroy();
                    }

                    // Initialize DataTables
                    table.DataTable({
                        "responsive": true,
                        "lengthChange": true,
                        "autoWidth": false,
                        "pageLength": 25,
                        "order": [[0, 'desc']], // Sort by BookingID descending
                        "language": {
                            "emptyTable": "No bookings found",
                            "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                            "infoEmpty": "Showing 0 to 0 of 0 entries",
                            "infoFiltered": "(filtered from _MAX_ total entries)",
                            "lengthMenu": "Show _MENU_ entries",
                            "search": "Search:",
                            "zeroRecords": "No matching records found"
                        }
                    });
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
