<%@ Page Title="" Language="C#" MasterPageFile="~/PublicLibrary/LibraryMaster.Master" AutoEventWireup="true" CodeBehind="WorkshopSlotBooking.aspx.cs" Inherits="HigherEducation.PublicLibrary.WorkshopSlotBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- SweetAlert2 CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --accent-color: #e74c3c;
            --light-color: #ecf0f1;
            --dark-color: #34495e;
        }

        .workshop-hero {
            background: linear-gradient(135deg, rgba(44, 62, 80, 0.9), rgba(52, 73, 94, 0.9)), url('https://images.unsplash.com/photo-1581094794329-cd6d5d3e64d5?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80');
            background-size: cover;
            background-position: center;
            color: white;
            padding: 10px 0;
            text-align: center;
        }

            .workshop-hero h1 {
                font-size: 2.5rem;
                font-weight: 700;
                margin-bottom: 15px;
            }

            .workshop-hero p {
                font-size: 1.2rem;
                max-width: 600px;
                margin: 0 auto;
                opacity: 0.9;
            }

        .main-content-card {
            background: white;
            border-radius: 15px;
            padding: 30px;
            margin-bottom: 30px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
        }

        .section-title {
            color: var(--primary-color);
            font-weight: 700;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 3px solid var(--secondary-color);
        }

        .user-info-panel {
            background: linear-gradient(135deg, #d6eaf8, #ebf5fb);
            border: none;
            border-radius: 10px;
            border-left: 5px solid var(--secondary-color);
            padding: 20px;
            margin-bottom: 25px;
        }

        .info-highlight {
            background: #fff3cd;
            border: 1px solid #ffeaa7;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 25px;
            display: flex;
            align-items: center;
        }

        .info-icon {
            font-size: 2rem;
            color: #856404;
            margin-right: 20px;
            flex-shrink: 0;
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

        .required::after {
            content: " *";
            color: var(--accent-color);
        }

        .slot-card {
            border: 2px solid #e9ecef;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 15px;
            transition: all 0.3s ease;
            cursor: pointer;
            background: white;
        }

            .slot-card:hover {
                border-color: var(--secondary-color);
                background-color: #f8f9fa;
                transform: translateY(-2px);
                box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
            }

            .slot-card.selected {
                border-color: var(--secondary-color);
                background: linear-gradient(135deg, #e7f1ff, #d4e6ff);
                box-shadow: 0 5px 15px rgba(52, 152, 219, 0.2);
            }

            .slot-card.disabled {
                opacity: 0.6;
                cursor: not-allowed;
                background-color: #f8f9fa;
            }

        .slot-radio {
            margin-right: 10px;
        }

        .price-badge {
            background: linear-gradient(135deg, #27ae60, #2ecc71);
            color: white;
            padding: 8px 15px;
            border-radius: 20px;
            font-weight: bold;
            font-size: 0.9rem;
        }

        .available-seats {
            color: #27ae60;
            font-weight: bold;
            font-size: 1.1rem;
        }

        .full-slot {
            color: var(--accent-color);
            font-weight: bold;
        }

        .amount-display {
            background: linear-gradient(135deg, var(--secondary-color), #2980b9);
            color: white;
            padding: 25px;
            border-radius: 15px;
            text-align: center;
            margin: 25px 0;
            box-shadow: 0 5px 15px rgba(52, 152, 219, 0.3);
        }

        .amount-value {
            font-size: 2.5rem;
            font-weight: bold;
            margin: 10px 0;
        }

        .booking-form-panel {
            background: white;
            border-radius: 15px;
            padding: 25px;
            margin-top: 20px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
            border-top: 5px solid var(--secondary-color);
        }

        .btn-primary-custom {
            background: linear-gradient(135deg, var(--secondary-color), #2980b9);
            border: none;
            border-radius: 10px;
            padding: 14px 30px;
            font-weight: 600;
            color: white;
            transition: all 0.3s ease;
            font-size: 1.1rem;
        }

            .btn-primary-custom:hover {
                transform: translateY(-2px);
                box-shadow: 0 7px 20px rgba(52, 152, 219, 0.4);
            }

        .btn-outline-custom {
            border: 2px solid var(--secondary-color);
            border-radius: 10px;
            padding: 12px 30px;
            font-weight: 600;
            color: var(--secondary-color);
            background: transparent;
            transition: all 0.3s ease;
        }

            .btn-outline-custom:hover {
                background: var(--secondary-color);
                color: white;
                transform: translateY(-2px);
            }

        .alert-custom {
            border-radius: 10px;
            border: none;
            padding: 15px 20px;
            margin-bottom: 25px;
        }

        .time-badge {
            background: var(--primary-color);
            color: white;
            padding: 5px 12px;
            border-radius: 20px;
            font-weight: 600;
            font-size: 0.9rem;
        }

        .duration-badge {
            background: #f39c12;
            color: white;
            padding: 5px 12px;
            border-radius: 20px;
            font-weight: 600;
            font-size: 0.9rem;
        }

        .workshop-type-badge {
            background: linear-gradient(135deg, #9b59b6, #8e44ad);
            color: white;
            padding: 5px 12px;
            border-radius: 20px;
            font-weight: 600;
            font-size: 0.9rem;
            margin-right: 5px;
            margin-bottom: 5px;
            display: inline-block;
        }

        .equipment-badge {
            background: linear-gradient(135deg, #e74c3c, #c0392b);
            color: white;
            padding: 4px 10px;
            border-radius: 15px;
            font-weight: 500;
            font-size: 0.8rem;
            margin-right: 3px;
            margin-bottom: 3px;
            display: inline-block;
        }

        .date-selection {
            background: linear-gradient(135deg, #f8f9fa, #e9ecef);
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 25px;
            border-left: 4px solid var(--secondary-color);
        }

        .date-info {
            background: #e8f4fd;
            border-radius: 8px;
            padding: 15px;
            margin-top: 15px;
            border-left: 4px solid #3498db;
        }

        .weekend-warning {
            background: #ffeaa7;
            border-radius: 8px;
            padding: 15px;
            margin-top: 15px;
            border-left: 4px solid #f39c12;
        }

        /* Loading Indicator */
        .update-progress {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background: rgba(255, 255, 255, 0.95);
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 5px 25px rgba(0, 0, 0, 0.2);
            z-index: 10000;
            border: 2px solid var(--secondary-color);
            text-align: center;
            min-width: 200px;
        }

        .loading-spinner {
            color: var(--secondary-color);
            font-weight: bold;
            font-size: 1.1rem;
        }

        .spinner-border {
            width: 2rem;
            height: 2rem;
            margin-bottom: 10px;
            color: var(--secondary-color);
        }

        /* Date validation error styling */
        .date-error {
            border-color: #dc3545 !important;
            background-color: #fff8f8;
        }

        .alert-danger {
            border-radius: 8px;
            border-left: 4px solid #dc3545;
        }

        /* Hide default alert */
        #pnlMessage {
            display: none !important;
        }

        .workshop-details {
            background: #f8f9fa;
            border-radius: 8px;
            padding: 15px;
            margin-top: 10px;
            border-left: 3px solid #3498db;
        }

        .equipment-container {
            margin-top: 8px;
        }

        .workshop-info {
            font-size: 0.9rem;
            color: #555;
        }

        /* Mobile Responsiveness */
        @media (max-width: 768px) {
            .workshop-hero h1 {
                font-size: 2rem;
            }

            .workshop-hero p {
                font-size: 1rem;
            }

            .main-content-card {
                padding: 20px;
            }

            .info-highlight {
                flex-direction: column;
                text-align: center;
            }

            .info-icon {
                margin-right: 0;
                margin-bottom: 15px;
            }

            .slot-card .row {
                text-align: center;
            }

            .amount-value {
                font-size: 2rem;
            }

            .workshop-type-badge, .equipment-badge {
                font-size: 0.7rem;
                padding: 3px 8px;
            }
        }

        @media (max-width: 576px) {
            .workshop-hero {
                padding: 40px 0;
            }

                .workshop-hero h1 {
                    font-size: 1.8rem;
                }

            .slot-card {
                padding: 15px;
            }
        }

        .slot-past {
            opacity: 0.6;
            background-color: #f8f9fa !important;
        }

        .slot-today {
            border-left: 4px solid #3498db !important;
        }

        .slot-future {
            border-left: 4px solid #27ae60 !important;
        }

        .slot-full {
            background-color: #ffe6e6 !important;
        }

        .slot-available {
            background-color: #e6ffe6 !important;
        }

        .disabled {
            opacity: 0.5;
            cursor: not-allowed !important;
            background-color: #f8f9fa !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>

    <!-- SweetAlert2 JS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- Hidden field to store SweetAlert messages -->
    <asp:HiddenField ID="hdnSweetAlertMessage" runat="server" />
    <asp:HiddenField ID="hdnSweetAlertType" runat="server" />
    <asp:HiddenField ID="hdnSweetAlertTitle" runat="server" />

    <div class="container">
        <!-- Success/Error Messages (Hidden - used for server-side only) -->
        <asp:UpdatePanel ID="upMessage" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlMessage" runat="server" CssClass="alert alert-custom" role="alert" Style="display: none;">
                    <div class="d-flex justify-content-between align-items-center">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="main-content-card">
            <!-- Header Section -->
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h2 class="section-title mb-0">Book Your Workshop Slot</h2>
                <asp:HyperLink ID="hlViewBooking" runat="server"
                    Text="View My Bookings"
                    CssClass="btn btn-primary-custom"
                    NavigateUrl="~/PublicLibrary/ViewMyWorkshopBookings.aspx" />
            </div>

            
            <!-- Booking Information -->
            <div class="info-highlight">
                <div class="info-icon">
                    <i class="fas fa-info-circle"></i>
                </div>
                <div>
                    <h5 class="mb-2">Booking Information</h5>
                    <p class="mb-0">
                        Each user can book only <strong>1 seat</strong> per workshop slot. 
                        The booking amount is <strong>₹300 per hour</strong>. Select only one slot by clicking on it.
                        All workshop sessions include access to professional tools and expert guidance.
                    </p>
                </div>
            </div>

            <!-- Date Selection -->
            <asp:UpdatePanel ID="upDateSelection" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="date-selection">
                        <h5 class="mb-3"><i class="fas fa-calendar-alt me-2"></i>Select Workshop Date</h5>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="txtSlotDate" class="form-label required">Workshop Date</label>
                                    <asp:TextBox ID="txtSlotDate" runat="server" CssClass="form-control"
                                        TextMode="Date" AutoPostBack="true" OnTextChanged="txtSlotDate_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSlotDate" runat="server"
                                        ControlToValidate="txtSlotDate" ErrorMessage="Please select a date"
                                        CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="cvSlotDate" runat="server"
                                        ControlToValidate="txtSlotDate" Display="Dynamic"
                                        CssClass="text-danger" ErrorMessage="Please select a weekday (Monday to Friday)"
                                        OnServerValidate="cvSlotDate_ServerValidate" />
                                </div>
                            </div>
                        </div>

                        <!-- Weekend Warning -->
                        <asp:Panel ID="pnlWeekendWarning" runat="server" Visible="false" CssClass="weekend-warning">
                            <div class="d-flex align-items-center">
                                <i class="fas fa-exclamation-triangle me-3 text-warning" style="font-size: 1.5rem;"></i>
                                <div>
                                    <strong>Weekend Selected</strong>
                                    <p class="mb-0">Workshops are not available on weekends. Please select a weekday (Monday to Friday).</p>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- District and ITI Selection -->
            <asp:UpdatePanel ID="upDistrictITI" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row mb-4">
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="ddlDistrict" class="form-label required">Select District</label>
                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                    <asp:ListItem Value="">-- Select District --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDistrict" runat="server"
                                    ControlToValidate="ddlDistrict" ErrorMessage="Please select a district"
                                    CssClass="text-danger small" Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="mb-3">
                                <label for="ddlITI" class="form-label required">Select ITI</label>
                                <asp:DropDownList ID="ddlITI" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlITI_SelectedIndexChanged">
                                    <asp:ListItem Value="">-- Select ITI --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvITI" runat="server"
                                    ControlToValidate="ddlITI" ErrorMessage="Please select an ITI"
                                    CssClass="text-danger small" Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtSlotDate" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>

            <!-- Available Slots Section -->
            <asp:UpdatePanel ID="upSlots" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlSlots" runat="server" Visible="false">
                        <div class="mb-4">
                            <h4 class="section-title" id="head" runat="server">Available Workshop Slots</h4>
                            <p id="showText" runat="server" class="text-muted mb-4">
                                Select one slot to book. Price: ₹300 per hour. Green indicates available seats.
                            </p>

                            <asp:Repeater ID="rptSlots" runat="server" OnItemDataBound="rptSlots_ItemDataBound">
                                <ItemTemplate>
                                    <div class="slot-card" id="slotCard" runat="server" onclick="selectSlot(this)">
                                        <div class="row align-items-center">
                                            <div class="col-md-1 text-center">
                                                <asp:RadioButton ID="rbSlot" runat="server"
                                                    CssClass="slot-radio"
                                                    GroupName="WorkshopSlot"
                                                    OnCheckedChanged="rbSlot_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </div>
                                            <div class="col-md-2">
                                                <span class="time-badge">
                                                    <i class="fas fa-clock me-1"></i>
                                                    <asp:Literal ID="litTime" runat="server"></asp:Literal>
                                                </span>
                                                <asp:Label ID="lblSlotStatus" runat="server" CssClass="small d-block mt-1"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <span class="duration-badge">
                                                    <i class="fas fa-hourglass-half me-1"></i>
                                                    <asp:Literal ID="litDuration" runat="server"></asp:Literal>
                                                </span>
                                            </div>
                                            <div class="col-md-2">
                                                <strong>Available Seats:</strong>
                                                <asp:Label ID="lblAvailableSeats" runat="server" CssClass="ms-2"></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <span class="price-badge">₹<asp:Literal ID="litAmount" runat="server"></asp:Literal>
                                                </span>
                                            </div>
                                            <div class="col-md-3 text-end">
                                                <asp:Literal ID="litSlotId" runat="server" Visible="false"></asp:Literal>
                                            </div>
                                        </div>
                                        
                                        <!-- Workshop Type and Equipment Details -->
                                        <div class="workshop-details mt-3">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <strong>Workshop Type:</strong>
                                                    <span class="workshop-type-badge">
                                                        <asp:Literal ID="litWorkshopType" runat="server"></asp:Literal>
                                                    </span>
                                                </div>
                                                <div class="col-md-6">
                                                    <strong>Equipment:</strong>
                                                    <div class="equipment-container">
                                                        <asp:Literal ID="litEquipment" runat="server"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:Label ID="lblNoSlots" runat="server" Text="No available slots for selected ITI on the selected date."
                                CssClass="text-muted text-center d-block mt-4" Visible="false"></asp:Label>
                        </div>

                        <!-- Amount Display -->
                        <asp:Panel ID="pnlAmount" runat="server" Visible="false">
                            <div class="amount-display">
                                <h4><i class="fas fa-receipt me-2"></i>Total Amount to Pay</h4>
                                <div class="amount-value">₹<asp:Literal ID="litTotalAmount" runat="server"></asp:Literal></div>
                                <p class="mb-0">
                                    for <strong>
                                        <asp:Literal ID="litSelectedDuration" runat="server"></asp:Literal></strong> workshop session (1 seat)
                                </p>
                            </div>
                        </asp:Panel>

                        <!-- Booking Form -->
                        <asp:Panel ID="pnlBookingForm" runat="server" Visible="false">
                            <div class="booking-form-panel">
                                <h5 class="mb-4 text-center">Confirm Your Booking</h5>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <asp:Button ID="btnBookSlot" runat="server" Text="Confirm & Proceed to Payment"
                                            CssClass="btn btn-primary-custom w-100 py-3" OnClick="btnBookSlot_Click" />
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel Selection"
                                            CssClass="btn btn-outline-custom w-100 py-3" OnClick="btnCancel_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlITI" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtSlotDate" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="rptSlots" EventName="ItemDataBound" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- Loading Indicator -->
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upSlots">
        <ProgressTemplate>
            <div class="update-progress">
                <div class="spinner-border" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
                <div class="loading-spinner">Loading available slots...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

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

        // Function to show confirmation dialog
        function showConfirmation(title, message, confirmCallback) {
            Swal.fire({
                title: title,
                text: message,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, proceed!',
                cancelButtonText: 'Cancel'
            }).then((result) => {
                if (result.isConfirmed) {
                    confirmCallback();
                }
            });
        }

        function selectSlot(element) {
            // Check if slot is disabled
            if (element.classList.contains('disabled')) {
                showSweetAlert('Slot Not Available', 'This slot is not available for selection. Please choose another slot.', 'warning');
                return;
            }

            // Remove selected class from all slots
            var allSlots = document.querySelectorAll('.slot-card');
            for (var i = 0; i < allSlots.length; i++) {
                allSlots[i].classList.remove('selected');
            }

            // Add selected class to clicked slot
            element.classList.add('selected');

            // Uncheck all radio buttons first
            var allRadios = document.querySelectorAll('input[type="radio"][name$="WorkshopSlot"]');
            for (var i = 0; i < allRadios.length; i++) {
                allRadios[i].checked = false;
            }

            // Find and check the radio button in clicked slot
            var radioButton = element.querySelector('input[type="radio"]');
            if (radioButton && !radioButton.disabled) {
                radioButton.checked = true;

                // Trigger ASP.NET postback
                __doPostBack(radioButton.name, '');
            }
        }

        // Initialize on page load
        document.addEventListener('DOMContentLoaded', function () {
            // Check for server-side SweetAlert messages
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

            // Set date input min and max attributes
            var dateInput = document.getElementById('<%= txtSlotDate.ClientID %>');
            if (dateInput) {
                // Set min to today - prevent past date selection
                var today = new Date();
                var dd = String(today.getDate()).padStart(2, '0');
                var mm = String(today.getMonth() + 1).padStart(2, '0');
                var yyyy = today.getFullYear();
                var todayFormatted = yyyy + '-' + mm + '-' + dd;
                dateInput.setAttribute('min', todayFormatted);

                // Set max to 30 days from today
                var maxDate = new Date();
                maxDate.setDate(maxDate.getDate() + 30);
                var maxDD = String(maxDate.getDate()).padStart(2, '0');
                var maxMM = String(maxDate.getMonth() + 1).padStart(2, '0');
                var maxYYYY = maxDate.getFullYear();
                dateInput.setAttribute('max', maxYYYY + '-' + maxMM + '-' + maxDD);

                // Prevent manual entry of past dates
                dateInput.addEventListener('input', function () {
                    validateDateSelection(this);
                });

                // Also validate on change (for browser date picker)
                dateInput.addEventListener('change', function () {
                    validateDateSelection(this);
                });

                // Validate on blur as well
                dateInput.addEventListener('blur', function () {
                    validateDateSelection(this);
                });
            }

            // Check if any radio is already selected and highlight its card
            var selectedRadio = document.querySelector('input[type="radio"][name$="WorkshopSlot"]:checked');
            if (selectedRadio) {
                var selectedSlot = selectedRadio.closest('.slot-card');
                if (selectedSlot) {
                    selectedSlot.classList.add('selected');
                }
            }

            // Add disabled class to full slots
            var fullSlots = document.querySelectorAll('.slot-card');
            fullSlots.forEach(function (slot) {
                var radio = slot.querySelector('input[type="radio"]');
                if (radio && radio.disabled) {
                    slot.classList.add('disabled');
                }
            });
        });

        // Function to validate date selection (prevent past dates and weekends)
        function validateDateSelection(dateInput) {
            if (!dateInput.value) return;

            var selectedDate = new Date(dateInput.value);
            var today = new Date();

            // Reset time part for accurate comparison
            today.setHours(0, 0, 0, 0);
            selectedDate.setHours(0, 0, 0, 0);

            var dayOfWeek = selectedDate.getDay(); // 0 = Sunday, 6 = Saturday

            // Check if date is in the past
            if (selectedDate < today) {
                dateInput.value = '';
                showSweetAlert('Invalid Date', 'Please select today or a future date. Past dates are not allowed.', 'error');
                return;
            }

            // Check if date is a weekend
            if (dayOfWeek === 0 || dayOfWeek === 6) {
                dateInput.value = '';
                showSweetAlert('Weekend Selected', 'Please select a weekday (Monday to Friday). Weekends are not available for workshop booking.', 'warning');
                return;
            }

            // If valid, clear any existing errors
            clearDateError();
        }

        // Function to clear date error message
        function clearDateError() {
            var dateInput = document.getElementById('<%= txtSlotDate.ClientID %>');
            var existingError = dateInput.parentNode.querySelector('.alert-danger');
            if (existingError) {
                existingError.remove();
            }
        }

        // Handle postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            // Check for server-side SweetAlert messages after postback
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

            // Re-initialize date input after async postback
            var dateInput = document.getElementById('<%= txtSlotDate.ClientID %>');
            if (dateInput) {
                // Set min to today again (in case it was reset)
                var today = new Date();
                var dd = String(today.getDate()).padStart(2, '0');
                var mm = String(today.getMonth() + 1).padStart(2, '0');
                var yyyy = today.getFullYear();
                var todayFormatted = yyyy + '-' + mm + '-' + dd;
                dateInput.setAttribute('min', todayFormatted);

                // Re-attach event listeners
                dateInput.addEventListener('input', function () {
                    validateDateSelection(this);
                });
                dateInput.addEventListener('change', function () {
                    validateDateSelection(this);
                });
                dateInput.addEventListener('blur', function () {
                    validateDateSelection(this);
                });
            }

            // Re-initialize slot selection
            var selectedRadio = document.querySelector('input[type="radio"][name$="WorkshopSlot"]:checked');
            if (selectedRadio) {
                var selectedSlot = selectedRadio.closest('.slot-card');
                if (selectedSlot) {
                    // Remove selected class from all
                    var allSlots = document.querySelectorAll('.slot-card');
                    for (var i = 0; i < allSlots.length; i++) {
                        allSlots[i].classList.remove('selected');
                    }
                    // Add to selected one
                    selectedSlot.classList.add('selected');
                }
            }

            // Re-apply disabled class to full slots
            var fullSlots = document.querySelectorAll('.slot-card');
            fullSlots.forEach(function (slot) {
                var radio = slot.querySelector('input[type="radio"]');
                if (radio && radio.disabled) {
                    slot.classList.add('disabled');
                } else {
                    slot.classList.remove('disabled');
                }
            });
        });
    </script>
</asp:Content>