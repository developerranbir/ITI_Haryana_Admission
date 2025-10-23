<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionManagement.aspx.cs" Inherits="HigherEducation.PublicLibrary.SubscriptionManagement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subscription Management</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <style>
        .card-header {
            background: linear-gradient(45deg, #007bff, #0056b3);
        }

        .status-badge-completed {
            background-color: #28a745;
        }

        .status-badge-pending {
            background-color: #ffc107;
            color: #000;
        }

        .status-badge-failed {
            background-color: #dc3545;
        }

        .table-container {
            max-height: 600px;
            overflow-y: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid mt-4">
            <!-- Header Card -->
            <div class="card shadow mb-4">
                <div class="card-header py-3 d-flex justify-content-between align-items-center text-white">
                    <h6 class="m-0 font-weight-bold">
                        <i class="fas fa-list-alt me-2"></i>Subscription Management
                    </h6>

                </div>

                <!-- Filter Section -->
                <div class="card-body">
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label fw-bold">Subscription Status:</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="active" Text="Active" Selected="True" />
                                <asp:ListItem Value="not active" Text="Not Active" />
                                <asp:ListItem Value="upcoming" Text="Upcoming" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <!-- Toolbar: Print / Export -->

                        <div class="col-md-3  mt-4">
                            <div class="d-flex gap-2">
                                <button type="button" class="btn btn-outline-primary" onclick="printGrid()">
                                    <i class="fas fa-print me-1"></i>Print
                                </button>
                                <button type="button" class="btn btn-outline-success" onclick="exportGridToExcel('Subscriptions.xls')">
                                    <i class="fas fa-file-excel me-1"></i>Export to Excel
                                </button>
                            </div>
                        </div>
                    </div>

                </div>


                <!-- Data Grid -->
                <div class="table-responsive">
                    <asp:GridView ID="gvSubscriptions" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-borderless table-striped table-hover" EmptyDataText="No subscriptions found">
                        <Columns>
                            <asp:BoundField DataField="SubscriptionId" HeaderText="Subscription ID" />
                            <asp:BoundField DataField="UserName" HeaderText="User Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="SubscriptionType" HeaderText="Type" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <span class='badge <%# GetStatusBadgeClass(Eval("PaymentStatus").ToString()) %>'>
                                        <%# Eval("PaymentStatus") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="table-dark" />
                    </asp:GridView>
                </div>
            </div>
        </div>
      
    </form>

    <script type="text/javascript">
   
        function getGridTable() {
            // The GridView renders a table element with the ClientID; get it directly.
            return document.getElementById('<%= gvSubscriptions.ClientID %>');
        }

        function printGrid() {
            var table = getGridTable();
            if (!table) {
                alert('Table not found.');
                return;
            }

            var win = window.open('', '_blank');
            if (!win) {
                alert('Popup blocked. Please allow popups for this site to print.');
                return;
            }

            var css = '<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet"/>';
            var style = '<style>body{font-family:Arial,Helvetica,sans-serif;margin:20px;}table{border-collapse:collapse;width:100%}th,td{border:1px solid #444;padding:6px;text-align:left} .badge{display:inline-block;padding:.35em .65em;border-radius:.25rem;}</style>';

            win.document.write('<!doctype html><html><head><meta charset="utf-8"><title>Print Subscriptions</title>' + css + style + '</head><body>');
            win.document.write('<h3>Subscription Management</h3>');
            // Clone the table to avoid issues with scripts; use outerHTML
            win.document.write(table.outerHTML);
            win.document.write('</body></html>');
            win.document.close();
            win.focus();

            // Delay to ensure resources load
            setTimeout(function () {
                win.print();
                // Some browsers keep the window open after print; close it for cleanliness.
                try { win.close(); } catch (e) { /* ignore */ }
            }, 250);
        }

        function exportGridToExcel(filename) {
            var table = getGridTable();
            if (!table) {
                alert('Table not found.');
                return;
            }

            // Build a minimal HTML file containing the table - Excel can open HTML tables.
            var html = '<!doctype html><html><head><meta charset="utf-8"><style>table{border-collapse:collapse;}th,td{border:1px solid #444;padding:6px;}</style></head><body>';
            html += table.outerHTML;
            html += '</body></html>';

            // Create a Blob and trigger download
            var blob = new Blob([html], { type: 'application/vnd.ms-excel' });
            var url = URL.createObjectURL(blob);
            var a = document.createElement('a');
            a.href = url;
            a.download = filename || 'export.xls';
            document.body.appendChild(a);
            a.click();
            setTimeout(function () {
                URL.revokeObjectURL(url);
                a.remove();
            }, 0);
        }
    </script>
</body>
</html>
