<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewMyWorkshopBookings.aspx.cs" Inherits="HigherEducation.PublicLibrary.ViewMyWorkshopBookings" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Workshop Bookings</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .gridview-container {
            margin: 20px 0;
        }
        .status-confirmed { background-color: #d1edff; }
        .status-pending { background-color: #fff3cd; }
        .status-cancelled { background-color: #f8d7da; }
        .status-completed { background-color: #d1e7dd; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h4 class="card-title mb-0">
                        <i class="fas fa-calendar-check me-2"></i>My Workshop Bookings
                    </h4>
                </div>
                <div class="card-body">
                    
                    <!-- Search and Filter Section -->
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" 
                                placeholder="Search by ITI, district..." AutoPostBack="true" 
                                OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="">All Status</asp:ListItem>
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Confirmed">Confirmed</asp:ListItem>
                                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                <asp:ListItem Value="Completed">Completed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" 
                                CssClass="btn btn-success" OnClick="btnExport_Click" />
                        </div>
                        <div class="col-md-2 text-end">
                            <asp:Label ID="lblTotalRecords" runat="server" CssClass="text-muted"></asp:Label>
                        </div>
                    </div>

                    <!-- GridView -->
                    <div class="gridview-container">
                        <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="False" 
                            CssClass="table table-striped table-bordered" AllowPaging="True" 
                            PageSize="10" OnPageIndexChanging="gvBookings_PageIndexChanging"
                            OnRowDataBound="gvBookings_RowDataBound" EmptyDataText="No bookings found."
                            OnRowCommand="gvBookings_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="BookingID" HeaderText="Booking ID" SortExpression="BookingID" />
                                <asp:BoundField DataField="District" HeaderText="District" SortExpression="District" />
                                <asp:BoundField DataField="ITI_Name" HeaderText="ITI Name" SortExpression="ITI_Name" />
                                <asp:BoundField DataField="WorkshopDate" HeaderText="Workshop Date" 
                                    DataFormatString="{0:dd-MM-yyyy}" SortExpression="WorkshopDate" />
                                <asp:BoundField DataField="WorkshopTime" HeaderText="Time" 
                                    DataFormatString="{0:hh\\:mm}" SortExpression="WorkshopTime" />
                                <asp:BoundField DataField="WorkshopDuration" HeaderText="Duration (hrs)" 
                                    SortExpression="WorkshopDuration" />
                                <asp:BoundField DataField="BookingAmount" HeaderText="Amount" 
                                    DataFormatString="₹{0:0}" SortExpression="BookingAmount" />
                                <asp:TemplateField HeaderText="Payment Status" SortExpression="PaymentStatus">
                                    <ItemTemplate>
                                        <span class='badge bg-<%# GetPaymentStatusBadge(Eval("PaymentStatus").ToString()) %>'>
                                            <%# Eval("PaymentStatus") %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatedAt" HeaderText="Booked On" 
                                    DataFormatString="{0:dd-MM-yyyy HH:mm}" SortExpression="CreatedAt" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewDetails" runat="server" Text="View Details" 
                                            CssClass="btn btn-info btn-sm" CommandName="ViewDetails" 
                                            CommandArgument='<%# Eval("BookingID") %>' />
                                        <asp:Button ID="btnCancelBooking" runat="server" Text="Cancel" 
                                            CssClass="btn btn-danger btn-sm mt-1" CommandName="CancelBooking" 
                                            CommandArgument='<%# Eval("BookingID") %>' 
                                            Visible='<%# Eval("BookingStatus").ToString() == "Pending" || Eval("BookingStatus").ToString() == "Confirmed" %>'
                                            OnClientClick='<%# $"return confirm(\"Are you sure you want to cancel booking {Eval("BookingID")}?\");" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="gridview-pager" HorizontalAlign="Center" />
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
                            <EmptyDataRowStyle CssClass="text-center" />
                        </asp:GridView>
                    </div>

                    <!-- Pagination Info -->
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <asp:Label ID="lblPageInfo" runat="server" CssClass="text-muted"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>