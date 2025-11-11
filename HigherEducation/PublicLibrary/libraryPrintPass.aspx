<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="libraryPrintPass.aspx.cs" Inherits="HigherEducation.PublicLibrary.libraryPrintPass" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Pass - ITI</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f5f5f5;
        }

        .pass-container {
            max-width: 600px;
            margin: 0 auto;
            background: white;
            border: 2px solid #333;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }

        .header {
            text-align: center;
            border-bottom: 2px solid #333;
            padding-bottom: 15px;
            margin-bottom: 20px;
        }



        .iti-name {
            font-size: 28px;
            font-weight: bold;
            color: #2c3e50;
            margin: 10px 0;
        }

        .pass-title {
            font-size: 24px;
            color: #e74c3c;
            margin: 15px 0;
        }

        .pass-details {
            margin: 25px 0;
        }

        .detail-row {
            display: flex;
            justify-content: space-between;
            margin: 12px 0;
            padding: 8px 0;
            border-bottom: 1px dashed #ccc;
        }

        .detail-label {
            font-weight: bold;
            color: #555;
        }

        .detail-value {
            color: #333;
        }

        .warning-note {
            background-color: #fff3cd;
            border: 1px solid #ffeaa7;
            border-radius: 5px;
            padding: 15px;
            margin: 20px 0;
            text-align: center;
            font-weight: bold;
            color: #856404;
        }

        .subscription-id {
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            padding: 10px;
            text-align: center;
            font-family: monospace;
            font-size: 18px;
            margin: 15px 0;
        }

        .print-button {
            text-align: center;
            margin: 20px 0;
        }

        .btn-print {
            background-color: #007bff;
            color: white;
            padding: 12px 30px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
        }

            .btn-print:hover {
                background-color: #0056b3;
            }

        @media print {
            body {
                background-color: white;
                padding: 0;
            }

            .pass-container {
                box-shadow: none;
                border: 3px solid #000;
            }

            .print-button {
                display: none;
            }

            .warning-note {
                background-color: transparent;
                border: 2px solid #000;
            }
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div class="pass-container">
            <!-- Header Section -->
            <div class="header ">


                <div class="qr-container">



                    <asp:Image ID="imgLogo" runat="server" CssClass="iti-logo" Style="height: 120px; margin-left: 120px"
                        ImageUrl="images/Logo_ITI.png" AlternateText="ITI Logo" />

                    <asp:Image ID="imgQrCode" runat="server" AlternateText="QR Code" Style="height: 120px; float: right;" />



                    <div class="iti-name">
                        <asp:Label ID="InstituteName" runat="server"></asp:Label>
                    </div>

                </div>







                <div class="pass-title">Library PASS</div>
            </div>

            <!-- Subscription ID -->
            <div class="subscription-id">
                Pass ID:
                <asp:Label ID="lblSubscriptionId" runat="server" Text=""></asp:Label>
            </div>

            <!-- Pass Details -->

            <div class="pass-details">
                <div class="detail-row">
                    <span class="detail-label">Passholder Name:</span>
                    <span class="detail-value">
                        <asp:Label ID="lblPassholderName" runat="server" Text=""></asp:Label>
                    </span>
                </div>

                <div class="detail-row">
                    <span class="detail-label">Amount Paid:</span>
                    <span class="detail-value">
                        <asp:Label ID="lblAmountPaid" runat="server" Text=""></asp:Label>
                    </span>
                </div>

                <div class="detail-row">
                    <span class="detail-label">Valid Up To:</span>
                    <span class="detail-value">
                        <asp:Label ID="lblValidUpto" runat="server" Text=""></asp:Label>
                    </span>
                </div>

                <div class="detail-row">
                    <span class="detail-label">Access Type:</span>
                    <span class="detail-value">
                        <asp:Label ID="lblCourse" runat="server" Text=""></asp:Label>
                    </span>
                </div>

                <div class="detail-row">
                    <span class="detail-label">Issued Date:</span>
                    <span class="detail-value">
                        <asp:Label ID="lblIssuedDate" runat="server" Text=""></asp:Label>
                    </span>
                </div>
            </div>



            <!-- Important Note -->
            <div class="warning-note">
                ⚠️ CARRY AN ID PROOF WITH THIS PASS WHEN VISITING ITI
            </div>


        </div>
        <!-- Print Button -->
        <div class="print-button">
            <button type="button" class="btn-print" onclick="window.print()">Print Pass</button>
            <a href="MySubscription.aspx" class="btn btn-primary-custom ">Back</a>
        </div>

        <!-- Error Message -->
        <asp:Panel ID="pnlError" runat="server" Visible="false" Style="text-align: center; color: red; margin: 20px 0;">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>
