<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkshopSlotBooking.aspx.cs" Inherits="HigherEducation.PublicLibrary.WorkshopSlotBooking" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Book Workshop Slot</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
        .card {
            border: none;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
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

        .slot-card {
            border: 2px solid #e9ecef;
            border-radius: 8px;
            padding: 15px;
            margin-bottom: 15px;
            transition: all 0.3s ease;
            cursor: pointer;
        }

            .slot-card:hover {
                border-color: #0d6efd;
                background-color: #f8f9fa;
            }

            .slot-card.selected {
                border-color: #0d6efd;
                background-color: #e7f1ff;
            }

        .slot-radio {
            margin-right: 10px;
        }

        .price-badge {
            background-color: #198754;
            color: white;
            padding: 5px 10px;
            border-radius: 20px;
            font-weight: bold;
        }

        .available-seats {
            color: #198754;
            font-weight: bold;
        }

        .full-slot {
            opacity: 0.6;
            cursor: not-allowed;
        }

        .amount-display {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 20px;
            border-radius: 10px;
            text-align: center;
        }

        .amount-value {
            font-size: 2.5rem;
            font-weight: bold;
            margin: 10px 0;
        }

        .user-info {
            background-color: #d1ecf1;
            border-radius: 5px;
            padding: 15px;
            margin-bottom: 20px;
        }

        .seat-info {
            background-color: #fff3cd;
            border: 1px solid #ffeaa7;
            border-radius: 5px;
            padding: 10px;
            margin-bottom: 15px;
        }
    </style>
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container mt-4">

        <%-- Add this at the top of your content, inside the form tag --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>

        <!-- Success/Error Messages -->
        <asp:Panel ID="pnlMessage" runat="server" CssClass="alert alert-dismissible fade show" role="alert" Style="display: none;">
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </asp:Panel>

        <div class="card shadow p-4 mb-4">
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h4 class="text-center mb-0 flex-grow-1">Book Workshop Slot</h4>
            </div>

            <!-- User Information -->
            <div class="user-info">
                <div class="row">
                    <div class="col-md-6">
                        <strong>Current Date:</strong>
                        <span class="current-date">
                            <asp:Literal ID="litCurrentDate" runat="server"></asp:Literal>
                        </span>
                    </div>
                    <div class="col-md-6">
                        <strong>Current Time:</strong>
                        <span class="current-time">
                            <asp:Literal ID="litCurrentTime" runat="server"></asp:Literal>
                        </span>
                    </div>
                </div>
            </div>

            <!-- Single Seat Booking Info -->
            <div class="seat-info">
                <div class="row align-items-center">
                    <div class="col-md-1 text-center">
                        <i class="bi bi-info-circle" style="font-size: 1.5rem; color: #856404;"></i>
                    </div>
                    <div class="col-md-11">
                        <strong>Booking Information:</strong> Each user can book only <strong>1 seat</strong> per workshop slot. 
                        The booking amount is <strong>₹300 per hour</strong>. Select only one slot by clicking on it.
                    </div>
                </div>
            </div>

            <!-- District and ITI Selection -->
            <div class="row mb-4">
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="ddlDistrict" class="form-label required">Select District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-select" AutoPostBack="true"
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
                        <asp:DropDownList ID="ddlITI" runat="server" CssClass="form-select" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlITI_SelectedIndexChanged">
                            <asp:ListItem Value="">-- Select ITI --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvITI" runat="server"
                            ControlToValidate="ddlITI" ErrorMessage="Please select an ITI"
                            CssClass="text-danger small" Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <!-- Available Slots -->
            <asp:Panel ID="pnlSlots" runat="server" Visible="false">
                <div class="mb-4">
                    <h5 id="head" runat="server" class="mb-3">Available Workshop Slots for Today</h5>
                    <p id="showText" runat="server" class="text-muted">Select one slot to book. Price: ₹300 per hour</p>

                    <asp:Repeater ID="rptSlots" runat="server" OnItemDataBound="rptSlots_ItemDataBound">
                        <ItemTemplate>
                            <div class="slot-card" id="slotCard" runat="server" onclick="selectSlot(this)">
                                <div class="row align-items-center">
                                    <div class="col-md-1">
                                        <asp:RadioButton ID="rbSlot" runat="server"
                                            CssClass="slot-radio"
                                            GroupName="WorkshopSlot"
                                            OnCheckedChanged="rbSlot_CheckedChanged"
                                            AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-3">
                                        <strong>Time:</strong>
                                        <asp:Literal ID="litTime" runat="server"></asp:Literal>
                                        <asp:Label ID="lblSlotStatus" runat="server" CssClass="small"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <strong>Duration:</strong>
                                        <asp:Literal ID="litDuration" runat="server"></asp:Literal>
                                    </div>
                                    <div class="col-md-2">
                                        <strong>Available Seats:</strong>
                                        <span class="available-seats">
                                            <asp:Literal ID="litAvailableSeats" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                    <div class="col-md-2">
                                        <strong>Amount:</strong>
                                        <span class="price-badge">₹<asp:Literal ID="litAmount" runat="server"></asp:Literal>
                                        </span>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Literal ID="litSlotId" runat="server" Visible="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Label ID="lblNoSlots" runat="server" Text="No available slots for selected ITI today."
                        CssClass="text-muted text-center d-block" Visible="false"></asp:Label>
                </div>

                <!-- Amount Display -->
                <asp:Panel ID="pnlAmount" runat="server" Visible="false" CssClass="mb-4">
                    <div class="amount-display">
                        <h5>Total Amount to Pay</h5>
                        <div class="amount-value">₹<asp:Literal ID="litTotalAmount" runat="server"></asp:Literal></div>
                        <p class="mb-0">
                            for
                            <asp:Literal ID="litSelectedDuration" runat="server"></asp:Literal>
                            workshop (1 seat)
                        </p>
                    </div>
                </asp:Panel>

                <!-- Booking Form -->
                <asp:Panel ID="pnlBookingForm" runat="server" Visible="false">
                    <div class="card shadow p-4">
                        <h5 class="mb-3">Booking Details</h5>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="txtFullName" class="form-label required">Full Name</label>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"
                                        placeholder="Enter your full name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvStudentName" runat="server"
                                        ControlToValidate="txtFullName" ErrorMessage="Name is required"
                                        CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="txtEmail" class="form-label required">Email Address</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                        TextMode="Email" placeholder="Enter your email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                        ControlToValidate="txtEmail" ErrorMessage="Email is required"
                                        CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                        ControlToValidate="txtEmail" ErrorMessage="Invalid email format"
                                        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                        CssClass="text-danger small" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="txtMobileNumber" class="form-label required">Mobile Number</label>
                                    <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control"
                                        TextMode="Phone" placeholder="Enter your phone number" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server"
                                        ControlToValidate="txtMobileNumber" ErrorMessage="Phone number is required"
                                        CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revPhone" runat="server"
                                        ControlToValidate="txtMobileNumber" ErrorMessage="Please enter a valid 10-digit mobile number"
                                        ValidationExpression="^[6-9]\d{9}$" CssClass="text-danger small" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label class="form-label">Seats to Book</label>
                                    <div class="form-control" style="background-color: #e9ecef;">
                                        <strong>1 Seat</strong> <small class="text-muted">(Single seat per booking)</small>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="d-grid gap-2">
                            <asp:Button ID="btnBookSlot" runat="server" Text="Confirm Booking"
                                CssClass="btn btn-primary btn-lg" OnClick="btnBookSlot_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                CssClass="btn btn-outline-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
    </form>

    <script>
        function selectSlot(element) {
            // Check if slot is not disabled
            if (element.classList.contains('full-slot')) {
                return; // Don't select disabled slots
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
            // Check if any radio is already selected and highlight its card
            var selectedRadio = document.querySelector('input[type="radio"][name$="WorkshopSlot"]:checked');
            if (selectedRadio) {
                var selectedSlot = selectedRadio.closest('.slot-card');
                if (selectedSlot) {
                    selectedSlot.classList.add('selected');
                }
            }
        });

        // Handle postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            // Re-initialize after async postback
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
        });
    </script>
</body>
</html>
