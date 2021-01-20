<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterPageHeader.ascx.cs"
    Inherits="WebSalesMine.WebPages.UserControl.MasterPageHeader" %>
<link href="../../App_Themes/CSS/WSMMasterPage.css" rel="stylesheet" type="text/css" />
<link href="../../App_Themes/CSS/ButtonEffect.css" rel="stylesheet" type="text/css" />
<table>
    <tr>
        <td>
            <table class="headerTable_Master">
                <tr>
                    <td class="headerLogo_Master">
                        <table style="width: 1240px;" cellpadding="0" cellspacing="0" border="0">
                            <tr visible="False">
                                <td style="width: 459px">
                                    <asp:HyperLink ID="imgbradylogo" runat="server" ImageUrl="~/App_Themes/Images/Emedco.gif"></asp:HyperLink>
                                </td>
                                <td style="width: 630px;" align="right">
                                    <asp:Label ID="lblUser" runat="server" Visible="TRUE" CssClass="lbluser_Master" meta:resourcekey="lblUserResource1"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblRole" runat="server" Visible="TRUE" CssClass="lbluser_Master" meta:resourcekey="lblRoleResource1"></asp:Label>
                                </td>
                                <td class="logoutIcon_Master" id="LogOutTD" runat="server" style="width: 151px">
                                    <asp:ImageButton Width="66px" Height="17px" runat="server" CssClass="HandCursor" UseSubmitBehavior="false"
                                        ID="btnlogout" border="0" ImageUrl="~/App_Themes/Images/logout-logo.gif" ToolTip="Logout"
                                        Visible="true" OnClick="btnlogout_Click" meta:resourcekey="btnlogoutResource1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table class="headingTable_Master">
                <tr>
                    <td>
                        <div class="headingCell_MasterBox">
                            <div class="headingCell_MasterLogo">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Images/HeaderName.JPG"
                                    AlternateText="header" Width="35" Height="30" ImageAlign="Top" CssClass="logoBox" />
                                <asp:Label ID="Label4" runat="server" Text=" Web SalesMine" CssClass="logoText"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
