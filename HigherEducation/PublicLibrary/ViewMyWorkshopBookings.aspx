<%@ Page Title="" Language="C#" MasterPageFile="~/PublicLibrary/LibraryMaster.Master" AutoEventWireup="true" CodeBehind="ViewMyWorkshopBookings.aspx.cs" Inherits="HigherEducation.PublicLibrary.ViewMyWorkshopBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <style>
        .gridview-container {
            margin: 20px 0;
        }

        .badge {
            font-size: 0.75em;
            padding: 0.35em 0.65em;
        }

        .table th {
            background-color: #f8f9fa;
            border-bottom: 2px solid #dee2e6;
        }

        .btn-sm {
            margin: 2px;
        }

        .expired-booking {
            background-color: #fff3cd !important;
        }

        .payment-message {
            font-size: 0.8em;
            font-weight: 500;
        }

        /* Enhanced Pagination Styles */
        .custom-pagination {
            margin: 20px 0;
        }

            .custom-pagination .pagination {
                justify-content: center;
                flex-wrap: wrap;
            }

                .custom-pagination .pagination .page-item .page-link {
                    color: #495057;
                    border: 1px solid #dee2e6;
                    margin: 2px;
                    border-radius: 5px;
                    transition: all 0.3s ease;
                    font-weight: 500;
                    min-width: 40px;
                    text-align: center;
                }

                .custom-pagination .pagination .page-item.active .page-link {
                    background: linear-gradient(135deg, #007bff, #0056b3);
                    border-color: #007bff;
                    color: white;
                    transform: scale(1.05);
                    box-shadow: 0 2px 5px rgba(0, 123, 255, 0.3);
                }

                .custom-pagination .pagination .page-item:not(.active) .page-link:hover {
                    background-color: #e9ecef;
                    border-color: #007bff;
                    color: #007bff;
                    transform: translateY(-1px);
                }

                .custom-pagination .pagination .page-item.disabled .page-link {
                    color: #6c757d;
                    background-color: #f8f9fa;
                    border-color: #dee2e6;
                    opacity: 0.6;
                }

        .pagination-info {
            background: linear-gradient(135deg, #f8f9fa, #e9ecef);
            border-radius: 10px;
            padding: 15px;
            margin: 15px 0;
            border-left: 4px solid #007bff;
        }

            .pagination-info .info-text {
                font-size: 0.9rem;
                color: #495057;
                font-weight: 500;
            }

            .pagination-info .page-stats {
                background: white;
                padding: 5px 10px;
                border-radius: 5px;
                border: 1px solid #dee2e6;
                font-weight: 600;
                color: #007bff;
            }

        /* GridView Pager Specific Styles */
        .gridview-pager table {
            margin: 0 auto;
        }

        .gridview-pager td {
            padding: 5px;
        }

        .gridview-pager a {
            text-decoration: none;
            display: inline-block;
            padding: 8px 16px;
            margin: 2px;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            color: #495057;
            font-weight: 500;
            transition: all 0.3s ease;
            background: white;
        }

            .gridview-pager a:hover {
                background: #007bff;
                color: white;
                border-color: #007bff;
                transform: translateY(-1px);
                box-shadow: 0 2px 5px rgba(0, 123, 255, 0.3);
            }

        .gridview-pager span {
            display: inline-block;
            padding: 8px 16px;
            margin: 2px;
            border: 1px solid #007bff;
            border-radius: 5px;
            background: linear-gradient(135deg, #007bff, #0056b3);
            color: white;
            font-weight: 600;
            transform: scale(1.05);
            box-shadow: 0 2px 5px rgba(0, 123, 255, 0.3);
        }

        /* Mobile Responsive Pagination */
        @media (max-width: 768px) {
            .custom-pagination .pagination {
                font-size: 0.9rem;
            }

                .custom-pagination .pagination .page-link {
                    padding: 6px 12px;
                    min-width: 35px;
                }

            .pagination-info {
                padding: 10px;
            }

                .pagination-info .info-text {
                    font-size: 0.8rem;
                }
        }

        @media (max-width: 576px) {
            .custom-pagination .pagination {
                font-size: 0.8rem;
            }

                .custom-pagination .pagination .page-link {
                    padding: 4px 8px;
                    min-width: 30px;
                    margin: 1px;
                }
        }

        .payment-message {
            font-size: 0.8em;
            font-weight: 500;
            display: block;
            margin-top: 5px;
        }

        .text-warning.payment-message {
            color: #856404 !important;
            background-color: #fff3cd;
            padding: 5px 10px;
            border-radius: 4px;
            border: 1px solid #ffeaa7;
        }

        .text-danger.payment-message {
            color: #721c24 !important;
            background-color: #f8d7da;
            padding: 5px 10px;
            border-radius: 4px;
            border: 1px solid #f5c6cb;
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

                <!-- Action Buttons Section -->
                <div class="row mb-3">
                    <div class="col-md-6">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh"
                            CssClass="btn btn-outline-primary" OnClick="btnRefresh_Click" />
                    </div>
                    <div class="col-md-6 text-end">
                        <asp:Label ID="lblTotalRecords" runat="server" CssClass="text-muted"></asp:Label>
                    </div>
                </div>

                <!-- GridView -->
                <div class="gridview-container table-responsive">
                    <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover"
                        AllowPaging="True" PageSize="5"
                        OnPageIndexChanging="gvBookings_PageIndexChanging"
                        OnRowCommand="gvBookings_RowCommand"
                        OnRowDataBound="gvBookings_RowDataBound"
                        EmptyDataText="No workshop bookings found.">
                        <Columns>
                            <asp:BoundField DataField="BookingID" HeaderText="Booking ID"
                                SortExpression="BookingID" ItemStyle-Width="80px" />

                            <asp:BoundField DataField="District" HeaderText="District"
                                SortExpression="District" ItemStyle-Width="120px" />

                            <asp:BoundField DataField="ITI_Name" HeaderText="ITI Name"
                                SortExpression="ITI_Name" />

                            <asp:BoundField DataField="workshopType" HeaderText="Workshop Type"
                                SortExpression="workshopType" />

                            <asp:TemplateField HeaderText="Workshop Date" SortExpression="WorkshopDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkshopDate" runat="server"
                                        Text='<%# Eval("WorkshopDate", "{0:yyyy-MM-dd}") %>' Visible="false" />
                                    <%# Eval("WorkshopDate", "{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Workshop Time" SortExpression="WorkshopTime">
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkshopTime" runat="server"
                                        Text='<%# Eval("WorkshopTime") %>' Visible="false" />
                                    <%# FormatTime(Eval("WorkshopTime")) %>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="WorkshopDuration" HeaderText="Duration"
                                SortExpression="WorkshopDuration"
                                DataFormatString="{0} hrs"
                                ItemStyle-Width="80px" />

                            <asp:BoundField DataField="BookingAmount" HeaderText="Amount"
                                DataFormatString="₹{0:0}" SortExpression="BookingAmount"
                                ItemStyle-Width="100px" />

                            <asp:TemplateField HeaderText="Booking Status" SortExpression="BookingStatus">
                                <ItemTemplate>
                                    <asp:Label ID="lblBookingStatus" runat="server"
                                        Text='<%# Eval("PaymentStatus") %>' Visible="false" />
                                    <span class='badge bg-<%# GetPaymentStatusBadge(Eval("BookingStatus").ToString()) %>'>
                                        <%# Eval("BookingStatus") %>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Payment Status" SortExpression="PaymentStatus">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentStatus" runat="server"
                                        Text='<%# Eval("PaymentStatus") %>' Visible="false" />
                                    <span class='badge bg-<%# GetPaymentStatusBadge(Eval("PaymentStatus").ToString()) %>'>
                                        <%# Eval("PaymentStatus") %>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Actions" ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <!-- Pay Now Button (Visible only for pending payments with future workshop dates AND available seats) -->
                                    <asp:Button ID="btnMakePayment" runat="server" Text="Pay Now"
                                        CommandName="MakePayment" CommandArgument='<%# Eval("BookingID") %>'
                                        CssClass="btn btn-success btn-sm" Visible="false"
                                        OnClientClick="return confirm('Are you sure you want to proceed with payment?');" />

                                    <!-- View Details Button -->
                                    <asp:Button ID="btnViewDetails" runat="server" Text="View Details"
                                        CommandName="ViewDetails" CommandArgument='<%# Eval("BookingID") %>'
                                        CssClass="btn btn-info btn-sm" Visible="false" />

                                    <!-- Payment Not Available Messages -->
                                    <asp:Label ID="lblPaymentMessage" runat="server"
                                        CssClass="payment-message" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="gridview-pager custom-pagination" HorizontalAlign="Center" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"
                            FirstPageText="« First" LastPageText="Last »"
                            PreviousPageText="‹ Prev" NextPageText="Next ›" />
                        <EmptyDataRowStyle CssClass="text-center p-4" />
                        <HeaderStyle CssClass="table-primary" />
                        <RowStyle CssClass="align-middle" />
                        <AlternatingRowStyle CssClass="align-middle" />
                    </asp:GridView>
                </div>

                <!-- Enhanced Pagination Info -->
                <div class="pagination-info">
                    <div class="row align-items-center">
                        <div class="col-md-8">
                            <div class="info-text">
                                <i class="fas fa-info-circle me-2 text-primary"></i>
                                Navigate through your bookings using the pagination controls above
                            </div>
                        </div>
                        <div class="col-md-4 text-end">
                            <asp:Label ID="lblPageInfo" runat="server" CssClass="page-stats"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
