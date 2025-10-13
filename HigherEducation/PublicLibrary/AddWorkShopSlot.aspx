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
    </style>
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container mt-4">

        <!-- Success/Error Messages -->
        <asp:Panel ID="pnlMessage" runat="server" CssClass="alert alert-dismissible fade show" role="alert" style="display: none;">
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
                    <div class="col-md-6">
                        <strong>Current Date:</strong> <span class="current-date">
                            <asp:Literal ID="litCurrentDate" runat="server"></asp:Literal></span>
                    </div>
                </div>
            </div>

            <!-- Current Date and Time Slot Info -->
            <div class="time-slot-info">
                <div class="row">
                    <div class="col-md-3">
                        <strong>Current Time:</strong> <span class="current-time">
                            <asp:Literal ID="litCurrentTime" runat="server"></asp:Literal></span>
                    </div>
                    <div class="col-md-3">
                        <strong>Available Hours:</strong> 9:00 AM - 5:00 PM
                    </div>
                    <div class="col-md-3">
                        <strong>Last Start Time:</strong> 4:00 PM
                    </div>
                    <div class="col-md-3">
                        <strong>Duration:</strong> 1-4 hours
                    </div>
                </div>
                <div class="mt-2 text-muted small">
                    <i class="bi bi-info-circle"></i>Slots must be between 9 AM to 5 PM with 1-4 hours duration. Last start time is 4:00 PM. Time slots are available in 15-minute intervals.
                </div>
            </div>

            <!-- Slot Details Form -->
            <div class="row">
                <div class="col-md-6">
                    <div class="mb-3">
                        <label for="ddlStartTime" class="form-label required">Start Time</label>
                        <asp:DropDownList ID="ddlStartTime" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlStartTime_SelectedIndexChanged">
                            <asp:ListItem Value="">-- Select Start Time --</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvStartTime" runat="server"
                            ControlToValidate="ddlStartTime" ErrorMessage="Start time is required"
                            CssClass="text-danger small" Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                        <div class="form-text">Only future time slots are available for selection</div>
                    </div>
                </div>

                <div class="col-md-6">
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
            </div>

            <div class="mb-4">
                <label for="txtRemarks" class="form-label">Remarks (Optional)</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"
                    placeholder="Add any additional notes about this workshop slot..."></asp:TextBox>
            </div>

            <div class="d-grid gap-2">
                <asp:Button ID="btnSave" runat="server" Text="Save Workshop Slot" CssClass="btn btn-primary btn-lg" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>

        <!-- Existing Slots Grid -->
        <div class="card shadow p-4">
            <h5 class="mb-3">Today's Workshop Slots</h5>
            <asp:GridView ID="gvSlots" runat="server" CssClass="table table-bordered table-striped table-hover"
                AutoGenerateColumns="False" DataKeyNames="ID"
                OnRowDeleting="gvSlots_RowDeleting" OnRowDataBound="gvSlots_RowDataBound"
                EmptyDataText="No workshop slots added yet today." ShowHeaderWhenEmpty="true">

                <Columns>
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
                                OnClientClick="return confirm('Are you sure you want to delete this workshop slot?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <EmptyDataTemplate>
                    <div class="text-center py-4">
                        <p class="text-muted">No workshop slots have been created for today.</p>
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

    </form>

    <script>
        // Client-side validation for available seats
        function validateSeats(input) {
            var value = parseInt(input.value);
            if (isNaN(value) || value < 1 || value > 20) {
                input.setCustomValidity('Please enter a number between 1 and 20');
            } else {
                input.setCustomValidity('');
            }
        }

        // Initialize seat validation
        document.addEventListener('DOMContentLoaded', function () {
            var seatsInput = document.getElementById('<%= txtAvailableSeats.ClientID %>');
            if (seatsInput) {
                seatsInput.addEventListener('input', function () {
                    validateSeats(this);
                });
            }
        });

        // Handle postbacks and maintain functionality
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            // Re-initialize after async postback
            var seatsInput = document.getElementById('<%= txtAvailableSeats.ClientID %>');
            if (seatsInput) {
                seatsInput.addEventListener('input', function () {
                    validateSeats(this);
                });
            }
        });
    </script>
</body>
</html>
