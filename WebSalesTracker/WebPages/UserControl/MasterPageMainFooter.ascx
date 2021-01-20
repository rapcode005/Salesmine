<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterPageMainFooter.ascx.cs"
    Inherits="WebSalesMine.WebPages.UserControl.MasterPageMainFooter" %>
<link href="../../App_Themes/CSS/WSMLoginPage.css" rel="stylesheet" type="text/css" />
<table id="footer_Master" class="footer_Master">
    <tr>
        <td style="text-align: left; padding-left: 5px;">
            <asp:Label ID="lblVersion" runat="server"></asp:Label>
        </td>
        <td style="text-align: right; padding-left: 5px;">
            <asp:Label ID="lblConfidentialityText" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblCopyRights" runat="server"></asp:Label>
        </td>
    </tr>
</table>
