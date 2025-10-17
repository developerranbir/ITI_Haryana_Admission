<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddWorkShopSlot.aspx.cs" Inherits="HigherEducation.PublicLibrary.AddWorkShopSlot" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Add Workshop Slot</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
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
    </style>
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container mt-4">

        <!-- Success/Error Messages -->
        <asp:Panel ID="pnlMessage" runat="server" CssClass="alert alert-dismissible fade show" role="alert" Style="display: none;">
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </asp:Panel>

        <div class="card shadow p-4 mb-4">
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h4 class="text-center mb-0 flex-grow-1">Add Workshop Slot</h4>
            </div>

            <!-- User Information from Session -->
            <div class="user-info">
                <div class="row">
                    <div class="col-md-6">
                        <strong>ITI Name:</strong> <span class="current-date">
                            <asp:Literal ID="litITIId" runat="server"></asp:Literal></span>
                    </div>
                    <div class="col-md-4">
                        <strong>Current Date:</strong> <span class="current-date">
                            <asp:Literal ID="litCurrentDate" runat="server"></asp:Literal></span>
                    </div>
                    <div class="col-md-2">
                        <strong>Current Time:</strong> <span class="current-time">
                            <asp:Literal ID="litCurrentTime" runat="server"></asp:Literal></span>
                    </div>
                </div>
            </div>

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

    </form>

    <script>
        // Set minimum date to today and restrict future dates if needed
        document.addEventListener('DOMContentLoaded', function () {
            var dateInput = document.getElementById('<%= txtSlotDate.ClientID %>');
            if (dateInput) {
                // Set min attribute to today
                var today = new Date();
                var dd = String(today.getDate()).padStart(2, '0');
                var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                var yyyy = today.getFullYear();

                today = yyyy + '-' + mm + '-' + dd;
                dateInput.setAttribute('min', today);

                // Set max attribute to 30 days from today (optional)
                var maxDate = new Date();
                maxDate.setDate(maxDate.getDate() + 10);
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
                var selectedDate = new Date(dateInput.value);
                var dayOfWeek = selectedDate.getDay(); // 0 = Sunday, 6 = Saturday

                if (dayOfWeek === 0 || dayOfWeek === 6) {
                    // If weekend is selected, clear the value and show message
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

            // Prevent keyboard input for date field (optional)
            var dateField = document.getElementById('<%= txtSlotDate.ClientID %>');
            if (dateField) {
                dateField.addEventListener('keydown', function (e) {
                    e.preventDefault();
                });
            }
        });

        // Handle postbacks and maintain functionality
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            // Re-initialize after async postback
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
            var dayOfWeek = selectedDate.getDay(); // 0 = Sunday, 6 = Saturday

            if (dayOfWeek === 0 || dayOfWeek === 6) {
                // If weekend is selected, clear the value and show message
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
                    return false; // Don't show confirmation if form is invalid
                }
            }

            // Get form values for confirmation message
            var date = document.getElementById('<%= txtSlotDate.ClientID %>').value;
            var startTime = document.getElementById('<%= ddlStartTime.ClientID %>');
            var endTime = document.getElementById('<%= ddlEndTime.ClientID %>');
            var seats = document.getElementById('<%= txtAvailableSeats.ClientID %>').value;
            var remarks = document.getElementById('<%= txtRemarks.ClientID %>').value;

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

            // Extract just the time part (remove the duration in parentheses)
            startTimeText = startTimeText.split(' (')[0];
            endTimeText = endTimeText.split(' (')[0];

            // Build confirmation message
            var confirmationMessage =
                "Please confirm the workshop slot details:\n\n" +
                "📅 Date: " + formattedDate + "\n" +
                "⏰ Time: " + startTimeText + " to " + endTimeText + "\n" +
                "💺 Available Seats: " + seats + "\n";

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
            // Check if button is disabled (shouldn't happen due to visibility, but just in case)
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
    </script>
</body>
</html>
