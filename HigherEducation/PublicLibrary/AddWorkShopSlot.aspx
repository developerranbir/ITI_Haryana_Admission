<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddWorkShopSlot.aspx.cs" Inherits="HigherEducation.PublicLibrary.AddWorkShopSlot" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Add Workshop Slot</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
        /* Your existing CSS styles remain the same */
        .card {
            border: none;
            border-radius: 10px;
        }

        .form-label {
            font-weight: 500;
            margin-bottom: 0.5rem;
        }

        .btn-primary {
            padding: 0.75rem;
            font-weight: 500;
        }

        .required::after {
            content: " *";
            color: #dc3545;
        }

        .time-slot-info {
            background-color: #e9ecef;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
        }

        .current-date {
            font-weight: bold;
            color: #0d6efd;
        }

        .duration-badge {
            font-size: 0.8em;
        }

        .current-time {
            font-weight: bold;
            color: #198754;
        }

        .user-info {
            background-color: #d1ecf1;
            border-radius: 5px;
            padding: 10px;
            margin-bottom: 15px;
        }

        .status-available {
            color: #198754;
            font-weight: bold;
        }

        .status-booked {
            color: #dc3545;
            font-weight: bold;
        }

        .weekend-date {
            color: #dc3545;
            font-weight: bold;
        }

        .weekday-date {
            color: #198754;
            font-weight: bold;
        }

        .date-picker-container {
            max-width: 300px;
        }

        /* Style for disabled weekend dates */
        input[type="date"]:invalid {
            color: #dc3545;
            border-color: #dc3545;
        }

        .confirmation-details {
            background-color: #f8f9fa;
            border-left: 4px solid #0d6efd;
            padding: 15px;
            margin: 10px 0;
            border-radius: 4px;
        }

        /* Styles for disabled state */
        .btn-disabled {
            opacity: 0.5;
            cursor: not-allowed;
            pointer-events: none;
        }

        /* Tooltip styles */
        [tooltip] {
            position: relative;
        }

            [tooltip]:hover::after {
                content: attr(tooltip);
                position: absolute;
                bottom: 100%;
                left: 50%;
                transform: translateX(-50%);
                background: #333;
                color: white;
                padding: 5px 10px;
                border-radius: 4px;
                font-size: 12px;
                white-space: nowrap;
                z-index: 1000;
            }

        /* Status styles */
        .status-available {
            color: #198754;
            font-weight: bold;
        }

        .status-booked {
            color: #dc3545;
            font-weight: bold;
        }

        .status-completed {
            color: #6c757d;
            font-weight: bold;
        }

        .status-upcoming {
            color: #0d6efd;
            font-weight: bold;
        }

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

        /* Equipment TextBox Styles */
        .equipment-badge {
            background-color: #e9ecef;
            border: 1px solid #dee2e6;
            border-radius: 12px;
            padding: 2px 8px;
            font-size: 0.8em;
            color: #495057;
            display: inline-block;
            margin: 1px;
        }

        .equipment-tags-container {
            background-color: #f8f9fa;
            border-radius: 5px;
            padding: 8px 12px;
            border-left: 3px solid #007bff;
            min-height: 40px;
        }

        .equipment-tag {
            display: inline-block;
            background: #007bff;
            color: white;
            padding: 4px 8px;
            margin: 2px;
            border-radius: 4px;
            font-size: 0.85em;
        }

        .equipment-tag-remove {
            margin-left: 5px;
            cursor: pointer;
            font-weight: bold;
        }

        .equipment-input-group {
            display: flex;
        }

        .equipment-input {
            flex: 1;
        }

        .equipment-add-btn {
            margin-left: 5px;
        }
    </style>
</head>
<body class="bg-light">
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

        <!-- Hidden field for SweetAlert -->
        <asp:HiddenField ID="hdnSweetAlertMessage" runat="server" />
        <asp:HiddenField ID="hdnSweetAlertType" runat="server" />
        <asp:HiddenField ID="hdnSweetAlertTitle" runat="server" />
        <!-- Hidden field to store equipment data -->
        <asp:HiddenField ID="hdnEquipmentData" runat="server" />

        <!-- Navigation -->
        <nav class="navbar navbar-expand-lg navbar-dark navbar-custom">
            <div class="container">
                <a class="navbar-brand" href="#">
                    <i class="fas fa-chart-line me-2"></i>
                    <strong>Add Workshop</strong>
                </a>
                <div class="navbar-nav ms-auto">
                    <div class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown">
                            <i class="fas fa-user-circle me-1"></i>
                            <asp:Literal ID="litUserName" runat="server"></asp:Literal>
                        </a>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="../DHE/HEMenu.aspx"><i class="fas fa-tachometer-alt me-2"></i>Dashboard</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </nav>

        <div class="container mt-4">
            <!-- Success/Error Messages -->
            <asp:Panel ID="pnlMessage" runat="server" CssClass="alert alert-dismissible fade show" role="alert" Style="display: none;">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <div class="card shadow p-4 mb-4">
                <!-- Current Date and Time Slot Info -->
                <div class="time-slot-info">
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
                            <strong>Available Hours:</strong> 9:00 AM - 5:00 PM
                        </div>
                        <div class="col-md-3">
                            <strong>Duration:</strong> 1-4 hours
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-muted small text-center">
                            <i class="bi bi-info-circle"></i>Slots must be between 9 AM to 5 PM with 1-4 hours duration. Only weekdays (Monday-Friday) are allowed. Time slots are available in 15-minute intervals.
                        </div>
                    </div>
                </div>

                <!-- Slot Details Form -->
                <div class="row">
                    <!-- Date Selection -->
                    <div class="col-md-4">
                        <div class="mb-3 date-picker-container">
                            <label for="txtSlotDate" class="form-label required">Select Date</label>
                            <asp:TextBox ID="txtSlotDate" runat="server" CssClass="form-control"
                                TextMode="Date" AutoPostBack="true" OnTextChanged="txtSlotDate_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSlotDate" runat="server"
                                ControlToValidate="txtSlotDate" ErrorMessage="Date is required"
                                CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvSlotDate" runat="server"
                                ControlToValidate="txtSlotDate" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Please select a weekday (Monday to Friday)"
                                OnServerValidate="cvSlotDate_ServerValidate" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mb-3">
                            <label for="ddlStartTime" class="form-label required">Start Time</label>
                            <asp:DropDownList ID="ddlStartTime" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlStartTime_SelectedIndexChanged">
                                <asp:ListItem Value="">-- Select Start Time --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStartTime" runat="server"
                                ControlToValidate="ddlStartTime" ErrorMessage="Start time is required"
                                CssClass="text-danger small" Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="mb-3">
                            <label for="ddlEndTime" class="form-label required">End Time</label>
                            <asp:DropDownList ID="ddlEndTime" runat="server" CssClass="form-select">
                                <asp:ListItem Value="">-- Select End Time --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvEndTime" runat="server"
                                ControlToValidate="ddlEndTime" ErrorMessage="End time is required"
                                CssClass="text-danger small" Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                            <asp:Label ID="lblDuration" runat="server" CssClass="text-info small mt-1 d-block"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="mb-3">
                            <label for="ddlWorkshopType" class="form-label required">Workshop Type</label>
                            <asp:DropDownList ID="ddlWorkshopType" runat="server" CssClass="form-select">
                                <asp:ListItem Value="">-- Select Workshop Type --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvWorkshopType" runat="server"
                                ControlToValidate="ddlWorkshopType" ErrorMessage="Workshop Type is required"
                                CssClass="text-danger small" Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="mb-3">
                            <label for="txtWorkshopEqp" class="form-label required">Workshop Equipment</label>

                            <!-- Change to TextBox for manual equipment entry -->
                            <div class="equipment-input-group">
                                <asp:TextBox ID="txtWorkshopEqp" runat="server" CssClass="form-control equipment-input"
                                    placeholder="Enter equipment name and press Enter or click Add" />
                                <!-- Change to HTML button to prevent postback -->
                                <button type="button" id="btnAddEquipment" class="btn btn-outline-primary equipment-add-btn" onclick="addEquipment()">
                                    Add
                                </button>
                            </div>

                            <div class="mt-2">
                                <small class="text-muted">
                                    <i class="fas fa-info-circle"></i>Enter equipment names and press Enter or click Add to add multiple items
                                </small>
                            </div>

                            <!-- Equipment tags display -->
                            <div id="equipmentTagsContainer" class="equipment-tags-container mt-2">
                                <span id="noEquipmentMessage" class="text-muted">No equipment added yet</span>
                                <!-- Equipment tags will be dynamically added here -->
                            </div>

                            <asp:CustomValidator ID="cvWorkshopEquipment" runat="server"
                                ControlToValidate="txtWorkshopEqp" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="At least one equipment must be added"
                                OnServerValidate="cvWorkshopEquipment_ServerValidate" ClientValidationFunction="validateEquipment" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="txtAvailableSeats" class="form-label required">Available Seats</label>
                            <asp:TextBox ID="txtAvailableSeats" runat="server" CssClass="form-control"
                                TextMode="Number" min="1" max="20" placeholder="Enter number of seats (1-20)" />
                            <asp:RequiredFieldValidator ID="rfvAvailableSeats" runat="server"
                                ControlToValidate="txtAvailableSeats" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Available seats is required" />
                            <asp:CustomValidator ID="cvAvailableSeats" runat="server"
                                ControlToValidate="txtAvailableSeats" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Seats must be between 1 and 20"
                                OnServerValidate="cvAvailableSeats_ServerValidate" />
                            <small class="form-text text-muted">Maximum 20 seats allowed</small>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="txtRemarks" class="form-label">Remarks (Optional)</label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"
                                placeholder="Add any additional notes about this workshop slot..."></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="btnSave" runat="server" Text="Save Workshop Slot" CssClass="btn btn-primary btn-lg" OnClick="btnSave_Click" OnClientClick="return confirmSave();" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
                </div>
            </div>

            <!-- Existing Slots Grid -->
            <div class="card shadow p-4">
                <h5 class="mb-3">
                    <asp:Literal ID="litGridTitle" runat="server"></asp:Literal>
                </h5>
                <asp:GridView ID="gvSlots" runat="server" CssClass="table table-bordered table-striped table-hover"
                    AutoGenerateColumns="False" DataKeyNames="ID"
                    OnRowDeleting="gvSlots_RowDeleting" OnRowDataBound="gvSlots_RowDataBound"
                    EmptyDataText="No workshop slots added for selected date." ShowHeaderWhenEmpty="true">

                    <Columns>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblSlotDate" runat="server" Text='<%# Eval("SlotDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Workshop Type">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkshopType" runat="server" Text='<%# Eval("workshopType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Equipment">
                            <ItemTemplate>
                                <asp:Label ID="lblEquipment" runat="server" Text='<%# Eval("EquipmentList") %>' CssClass="equipment-badge"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Start Time">
                            <ItemTemplate>
                                <asp:Label ID="lblStartTime" runat="server" Text='<%# Eval("StartTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="End Time">
                            <ItemTemplate>
                                <asp:Label ID="lblEndTime" runat="server" Text='<%# Eval("EndTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Duration">
                            <ItemTemplate>
                                <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Seats">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalSeats" runat="server" Text='<%# Eval("TotalSeats") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Available Seats">
                            <ItemTemplate>
                                <asp:Label ID="lblAvailableSeats" runat="server" Text='<%# Eval("AvailableSeats") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" CssClass="status-available" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server"
                                    CommandName="Delete"
                                    Text="Delete"
                                    CausesValidation="false"
                                    CssClass="btn btn-danger btn-sm"
                                    OnClientClick="return confirmDelete(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <EmptyDataTemplate>
                        <div class="text-center py-4">
                            <p class="text-muted">No workshop slots have been created for the selected date.</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

        <!-- Footer -->
        <footer class="footer">
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <p>&copy; 2025 ITI Workshop Management System. All rights reserved.</p>
                    </div>
                </div>
            </div>
        </footer>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        // Equipment array to store added equipment
        let equipmentList = [];

        // Initialize SweetAlert2 Toast
        const Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
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

        // Equipment management functions
        function addEquipment() {
            var equipmentInput = document.getElementById('<%= txtWorkshopEqp.ClientID %>');
            var equipmentName = equipmentInput.value.trim();

            if (equipmentName === '') {
                showSweetAlert('Warning', 'Please enter equipment name', 'warning');
                return false;
            }

            // Check for duplicates
            if (equipmentList.includes(equipmentName)) {
                showSweetAlert('Warning', 'Equipment already added', 'warning');
                return false;
            }

            // Add to equipment list
            equipmentList.push(equipmentName);

            // Update hidden field with equipment data
            updateEquipmentHiddenField();

            // Update display
            updateEquipmentDisplay();

            // Clear input
            equipmentInput.value = '';

            // Focus back to input
            equipmentInput.focus();

            return false; // Prevent any default behavior
        }

        function removeEquipment(index) {
            equipmentList.splice(index, 1);
            updateEquipmentHiddenField();
            updateEquipmentDisplay();
        }

        function updateEquipmentDisplay() {
            var container = document.getElementById('equipmentTagsContainer');
            var noEquipmentMessage = document.getElementById('noEquipmentMessage');

            // Clear container
            container.innerHTML = '';

            if (equipmentList.length === 0) {
                noEquipmentMessage.style.display = 'inline';
                container.appendChild(noEquipmentMessage);
            } else {
                noEquipmentMessage.style.display = 'none';

                equipmentList.forEach(function (equipment, index) {
                    var tag = document.createElement('span');
                    tag.className = 'equipment-tag';
                    tag.innerHTML = equipment +
                        '<span class="equipment-tag-remove" onclick="removeEquipment(' + index + ')">×</span>';
                    container.appendChild(tag);
                });
            }
        }

        function updateEquipmentHiddenField() {
            document.getElementById('<%= hdnEquipmentData.ClientID %>').value = equipmentList.join('|');
        }

        function validateEquipment(source, args) {
            args.IsValid = equipmentList.length > 0;
        }

        // Handle Enter key in equipment input
        function handleEquipmentKeyPress(event) {
            if (event.key === 'Enter') {
                event.preventDefault();
                addEquipment();
                return false;
            }
        }

        // Set minimum date to today and restrict future dates if needed
        document.addEventListener('DOMContentLoaded', function () {
            // Initialize equipment system
            initializeEquipment();

            checkForAlerts();

            var dateInput = document.getElementById('<%= txtSlotDate.ClientID %>');
            if (dateInput) {
                // Set min attribute to today
                var today = new Date();
                var dd = String(today.getDate()).padStart(2, '0');
                var mm = String(today.getMonth() + 1).padStart(2, '0');
                var yyyy = today.getFullYear();

                today = yyyy + '-' + mm + '-' + dd;
                dateInput.setAttribute('min', today);

                // Set max attribute to 30 days from today
                var maxDate = new Date();
                maxDate.setDate(maxDate.getDate() + 30);
                var maxDD = String(maxDate.getDate()).padStart(2, '0');
                var maxMM = String(maxDate.getMonth() + 1).padStart(2, '0');
                var maxYYYY = maxDate.getFullYear();

                dateInput.setAttribute('max', maxYYYY + '-' + maxMM + '-' + maxDD);

                // Add event listener to prevent weekend selection
                dateInput.addEventListener('input', function () {
                    disableWeekendSelection(this);
                });

                // Initial check
                disableWeekendSelection(dateInput);
            }

            // Function to disable weekend selection
            function disableWeekendSelection(dateInput) {
                if (!dateInput.value) return;

                var selectedDate = new Date(dateInput.value);
                var dayOfWeek = selectedDate.getDay();

                if (dayOfWeek === 0 || dayOfWeek === 6) {
                    dateInput.value = '';
                    alert('Please select a weekday (Monday to Friday). Weekends are not available for slot booking.');
                }
            }

            // Client-side validation for available seats
            function validateSeats(input) {
                var value = parseInt(input.value);
                if (isNaN(value) || value < 1 || value > 20) {
                    input.setCustomValidity('Please enter a number between 1 and 20');
                } else {
                    input.setCustomValidity('');
                }
            }

            var seatsInput = document.getElementById('<%= txtAvailableSeats.ClientID %>');
            if (seatsInput) {
                seatsInput.addEventListener('input', function () {
                    validateSeats(this);
                });
            }

            // Prevent keyboard input for date field
            var dateField = document.getElementById('<%= txtSlotDate.ClientID %>');
            if (dateField) {
                dateField.addEventListener('keydown', function (e) {
                    e.preventDefault();
                });
            }
        });
        // Handle postbacks and maintain functionality
        // Handle postbacks and maintain functionality
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            // Re-initialize equipment system after async postback
            initializeEquipment();

            var dateInput = document.getElementById('<%= txtSlotDate.ClientID %>');
            if (dateInput) {
                dateInput.addEventListener('input', function () {
                    disableWeekendSelection(this);
                });
            }

            var seatsInput = document.getElementById('<%= txtAvailableSeats.ClientID %>');
            if (seatsInput) {
                seatsInput.addEventListener('input', function () {
                    validateSeats(this);
                });
            }
        });
        // Function to disable weekend selection (needed for postback)
        function disableWeekendSelection(dateInput) {
            if (!dateInput.value) return;

            var selectedDate = new Date(dateInput.value);
            var dayOfWeek = selectedDate.getDay();

            if (dayOfWeek === 0 || dayOfWeek === 6) {
                dateInput.value = '';
                alert('Please select a weekday (Monday to Friday). Weekends are not available for slot booking.');
            }
        }

        // Function to validate seats (needed for postback)
        function validateSeats(input) {
            var value = parseInt(input.value);
            if (isNaN(value) || value < 1 || value > 20) {
                input.setCustomValidity('Please enter a number between 1 and 20');
            } else {
                input.setCustomValidity('');
            }
        }

        // Confirmation function before saving
        function confirmSave() {
            // First, check if the form is valid
            if (typeof Page_ClientValidate === 'function') {
                Page_ClientValidate();
                if (!Page_IsValid) {
                    return false;
                }
            }

            // Get form values for confirmation message
            var date = document.getElementById('<%= txtSlotDate.ClientID %>').value;
            var startTime = document.getElementById('<%= ddlStartTime.ClientID %>');
            var endTime = document.getElementById('<%= ddlEndTime.ClientID %>');
            var seats = document.getElementById('<%= txtAvailableSeats.ClientID %>').value;
            var remarks = document.getElementById('<%= txtRemarks.ClientID %>').value;
            var workshopType = document.getElementById('<%= ddlWorkshopType.ClientID %>');

            // Format date for display
            var dateObj = new Date(date);
            var formattedDate = dateObj.toLocaleDateString('en-US', {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });

            // Get selected time texts
            var startTimeText = startTime.options[startTime.selectedIndex].text;
            var endTimeText = endTime.options[endTime.selectedIndex].text;
            var workshopTypeText = workshopType.options[workshopType.selectedIndex].text;

            // Extract just the time part
            startTimeText = startTimeText.split(' (')[0];
            endTimeText = endTimeText.split(' (')[0];

            // Build confirmation message
            var confirmationMessage =
                "Please confirm the workshop slot details:\n\n" +
                "📅 Date: " + formattedDate + "\n" +
                "⏰ Time: " + startTimeText + " to " + endTimeText + "\n" +
                "🏭 Workshop Type: " + workshopTypeText + "\n" +
                "💺 Available Seats: " + seats + "\n" +
                "🔧 Equipment: " + (equipmentList.length > 0 ? equipmentList.join(', ') : 'None') + "\n";

            // Add remarks if provided
            if (remarks && remarks.trim() !== '') {
                confirmationMessage += "📝 Remarks: " + remarks.substring(0, 100) + (remarks.length > 100 ? "..." : "") + "\n";
            }

            confirmationMessage += "\nAre you sure you want to create this workshop slot?";

            // Show confirmation dialog
            return confirm(confirmationMessage);
        }

        // Enhanced delete confirmation with reason checking
        function confirmDelete(deleteButton) {
            // Check if button is disabled
            if (deleteButton.disabled || deleteButton.style.display === 'none') {
                alert('This slot cannot be deleted. It may have already started or has booked seats.');
                return false;
            }

            var row = deleteButton.closest('tr');
            var statusCell = row.querySelector('[id*="lblStatus"]');
            var statusText = statusCell ? statusCell.innerText : '';

            var confirmationMessage = "Are you sure you want to delete this workshop slot?";

            if (statusText.includes('booked')) {
                confirmationMessage += "\n\n⚠️ Note: This slot has booked seats. Deleting it will cancel all existing bookings.";
            }

            return confirm(confirmationMessage);
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

        // Call checkForAlerts on page load
        document.addEventListener('DOMContentLoaded', function () {
            checkForAlerts();
        });

        // Equipment management functions
        function addEquipment() {
            var equipmentInput = document.getElementById('<%= txtWorkshopEqp.ClientID %>');
            var equipmentName = equipmentInput.value.trim();

            if (equipmentName === '') {
                showSweetAlert('Warning', 'Please enter equipment name', 'warning');
                return false;
            }

            // Check for duplicates
            if (equipmentList.includes(equipmentName)) {
                showSweetAlert('Warning', 'Equipment already added', 'warning');
                return false;
            }

            // Add to equipment list
            equipmentList.push(equipmentName);

            // Update hidden field with equipment data
            updateEquipmentHiddenField();

            // Update display
            updateEquipmentDisplay();

            // Clear input
            equipmentInput.value = '';

            // Focus back to input
            equipmentInput.focus();

            return false; // Prevent any default behavior
        }

        function removeEquipment(index) {
            equipmentList.splice(index, 1);
            updateEquipmentHiddenField();
            updateEquipmentDisplay();
        }

        function updateEquipmentDisplay() {
            var container = document.getElementById('equipmentTagsContainer');

            // Clear container
            container.innerHTML = '';

            if (equipmentList.length === 0) {
                // Show "no equipment" message
                var noEquipmentMessage = document.createElement('span');
                noEquipmentMessage.id = 'noEquipmentMessage';
                noEquipmentMessage.className = 'text-muted';
                noEquipmentMessage.textContent = 'No equipment added yet';
                container.appendChild(noEquipmentMessage);
            } else {
                // Show equipment tags
                equipmentList.forEach(function (equipment, index) {
                    var tag = document.createElement('span');
                    tag.className = 'equipment-tag';
                    tag.innerHTML = equipment +
                        '<span class="equipment-tag-remove" onclick="removeEquipment(' + index + ')">×</span>';
                    container.appendChild(tag);
                });
            }
        }

        function updateEquipmentHiddenField() {
            document.getElementById('<%= hdnEquipmentData.ClientID %>').value = equipmentList.join(' | ');
        }

        function validateEquipment(source, args) {
            args.IsValid = equipmentList.length > 0;
        }

        // Handle Enter key in equipment input
        function handleEquipmentKeyPress(event) {
            if (event.key === 'Enter') {
                event.preventDefault();
                addEquipment();
                return false;
            }
        }

        // Initialize equipment display on page load
        function initializeEquipment() {
            // Check if there's existing equipment data in the hidden field (for postbacks)
            var existingEquipment = document.getElementById('<%= hdnEquipmentData.ClientID %>').value;
            if (existingEquipment && existingEquipment.trim() !== '') {
                equipmentList = existingEquipment.split('|').filter(function (item) {
                    return item.trim() !== '';
                });
            }
            updateEquipmentDisplay();

            // Add event listener for Enter key in equipment input
            var equipmentInput = document.getElementById('<%= txtWorkshopEqp.ClientID %>');
            if (equipmentInput) {
                equipmentInput.addEventListener('keypress', handleEquipmentKeyPress);
            }
        }
    </script>
</body>
</html>
