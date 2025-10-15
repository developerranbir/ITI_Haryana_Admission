<%@ Page Title="" Language="C#" MasterPageFile="~/PublicLibrary/LibraryMaster.Master" AutoEventWireup="true" CodeBehind="ViewMyWorkshopBookings.aspx.cs" Inherits="HigherEducation.PublicLibrary.ViewMyWorkshopBookings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
<style>
    .gridview-container {
        margin: 20px 0;
    }
</style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="container mt-4">
            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h4 class="card-title mb-0">
                        <i class="fas fa-calendar-check me-2"></i>My Workshop Bookings
                    </h4>
                </div>
                <div class="card-body">

                    <!-- Action Buttons Section (Removed Search) -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6 text-end">
                            <asp:Label ID="lblTotalRecords" runat="server" CssClass="text-muted"></asp:Label>
                        </div>
                    </div>

                    <!-- GridView -->
                    <div class="gridview-container">
                        <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered" AllowPaging="True"
                            PageSize="10" OnPageIndexChanging="gvBookings_PageIndexChanging"
                            EmptyDataText="No bookings found." OnRowCommand="gvBookings_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="BookingID" HeaderText="Booking ID" SortExpression="BookingID" />
                                <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber" />
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                <asp:BoundField DataField="District" HeaderText="District" SortExpression="District" />
                                <asp:BoundField DataField="ITI_Name" HeaderText="ITI Name" SortExpression="ITI_Name" />
                                <asp:BoundField DataField="WorkshopDate" HeaderText="Workshop Date"
                                    DataFormatString="{0:dd-MM-yyyy}" SortExpression="WorkshopDate" />
                                <asp:BoundField DataField="WorkshopTime" HeaderText="Time"
                                    SortExpression="WorkshopTime" />
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
                                            CommandArgument='<%# Eval("BookingID") %>'
                                            Visible='<%# Eval("PaymentStatus").ToString() != "Pending" %>' />
                                        <asp:Button ID="btnPaymentStatus" runat="server" Text="Check Payment Status"
                                            CssClass="btn btn-warning btn-sm" CommandName="CheckPayment"
                                            CommandArgument='<%# Eval("BookingID") %>'
                                            Visible='<%# Eval("PaymentStatus").ToString() == "Pending" %>' />
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
  </asp:Content>
