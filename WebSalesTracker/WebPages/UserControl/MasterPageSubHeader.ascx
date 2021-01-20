<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterPageSubHeader.ascx.cs"
    Inherits="WebSalesMine.WebPages.UserControl.MasterPageSubHeader" %>
<link href="../../App_Themes/CSS/WSMMasterPage.css" rel="stylesheet" type="text/css" />
<table class="headingTable_Master">
    <tr>
        <td class="headingCell_Master">
            <div>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/HeaderName.JPG"
                    AlternateText="header" Width="35" Height="30" ImageAlign="Top" CssClass="logoBox" />
                <asp:Label ID="Label1" runat="server" Text=" Web SalesMine" CssClass="HeaderTable_Master"></asp:Label>
            </div>
        </td>
        <td>
        </td>
    </tr>
</table>
