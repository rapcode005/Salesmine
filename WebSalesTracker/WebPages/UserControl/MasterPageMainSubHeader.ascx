<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterPageMainSubHeader.ascx.cs"
    Inherits="WebSalesMine.WebPages.UserControl.MasterPageMainSubHeader" %>

<table class="headingTable_Master" id="Table1" runat="server">
    <tr>
        <td class="headingcell_master">
            <div class="headingCell_MasterBox">
                <div class="headingCell_MasterLogo">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Images/HeaderName.JPG"
                        AlternateText="header" Width="35" Height="30" ImageAlign="Top" CssClass="logoBox" margin-top="3px" />
                    <asp:Label ID="Label4" runat="server" Text=" Web SalesMine" CssClass="logoText"></asp:Label>
                </div>
            </div>
        </td>
        <td>
            <table class="timesubmitlabel">
                <tr>
                    <td>
                        <asp:Label ID="lblTimeSubmitted" runat="server" CssClass="HeaderTable_Master" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
