<%@ Page Title="" Language="C#" MasterPageFile="~/PublicLibrary/LibraryMaster.Master" AutoEventWireup="true" CodeBehind="MySubscription.aspx.cs" Inherits="HigherEducation.PublicLibrary.MySubscription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="subscription-table">
       
        <div class=" container bg-white p-3  text-center">
             <h3><i class="fas fa-history me-2"></i>Your Library Subscription History</h3>
            <asp:GridView ID="gvSubscriptions" runat="server" AutoGenerateColumns="False" 
                CssClass="table table-hover" HeaderStyle-CssClass="table-primary" 
                GridLines="None" EmptyDataText="No subscriptions found">
                <HeaderStyle CssClass="fw-bold" />
                <RowStyle CssClass="align-middle" />
                <AlternatingRowStyle CssClass="bg-light" />
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="User Name" />
                    <asp:BoundField DataField="collegename" HeaderText="ITI Name" />
                    <asp:BoundField DataField="SubscriptionType" HeaderText="Subscription Type" />
                    <asp:BoundField DataField="PaymentDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount (₹)" DataFormatString="{0:N2}" />

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <a
                                href='<%# Eval("PaymentStatus").ToString() == "Pending" 
                                    ? "libraryPayment.aspx?id=" + Eval("SubscriptionId") 
                                    : "libraryPrintPass.aspx?id=" + Eval("SubscriptionId") %>'
                                class='<%# Eval("PaymentStatus").ToString() == "Completed" 
                                    ? "btn btn-success btn-sm" 
                                    : "btn btn-primary btn-sm" %>'>
                                <%# Eval("PaymentStatus").ToString() == "Pending" ? "Pay Fee" : "Print Pass" %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
