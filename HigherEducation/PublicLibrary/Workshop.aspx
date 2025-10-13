<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workshop.aspx.cs" Inherits="HigherEducation.PublicLibrary.Workshop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Workshop Subscription</title>
    <style>
        .subscription-container {
            width: 400px;
            margin: 40px auto;
            padding: 30px;
            border: 1px solid #ccc;
            border-radius: 8px;
            background: #f9f9f9;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }

        .subscription-title {
            font-size: 1.5em;
            margin-bottom: 20px;
            text-align: center;
            color: #333;
        }

        .form-group {
            margin-bottom: 16px;
        }

        .form-label {
            display: block;
            margin-bottom: 6px;
            font-weight: 600;
        }

        .form-input {
            width: 100%;
            padding: 8px;
            border: 1px solid #bbb;
            border-radius: 4px;
        }

        .form-button {
            width: 100%;
            padding: 10px;
            background: #0078d4;
            color: #fff;
            border: none;
            border-radius: 4px;
            font-size: 1em;
            cursor: pointer;
        }

            .form-button:hover {
                background: #005fa3;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="subscription-container">
            <div class="subscription-title">Workshop Subscription</div>
            <div class="container py-5">
                <div class="row mb-4">
                    <div class="col-md-6">
                        <asp:Label ID="lblDistrict" runat="server" Text="Select District:" CssClass="font-weight-bold" />
                        <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control mt-2" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblITI" runat="server" Text="Select ITI:" CssClass="font-weight-bold" />
                        <asp:DropDownList runat="server" ID="ddlITI" CssClass="form-control mt-2"></asp:DropDownList>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
