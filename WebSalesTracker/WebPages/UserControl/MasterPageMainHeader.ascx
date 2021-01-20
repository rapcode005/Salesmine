<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterPageMainHeader.ascx.cs"
    Inherits="WebSalesMine.WebPages.UserControl.MasterPageMainHeader" %>
<link href="../../App_Themes/CSS/ButtonEffect.css" rel="stylesheet" type="text/css" />
<center>
<table class="headerTable_Master" runat="server" id="tblHeaderTable">
    <tr>
        <td class="headerLogo_Master" style="width: 90%">
            <table id="LogoTable" cellpadding="0" width="100%" cellspacing="0" border="0">
                <tr visible="False">
                    <td style="width: 159px; border: none;">
                        <asp:HyperLink ID="imgbradylogo" runat="server" ImageUrl="~/App_Themes/Images/Emedco.gif"></asp:HyperLink>
                    </td>
                    <td align="right" style="width: 100%" valign="top">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label ID="lblUser" runat="server" Visible="true" CssClass="lbluser_Master" meta:resourcekey="lblUserResource1"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblRole" runat="server" Visible="False" CssClass="lbluser_Master"
                                        meta:resourcekey="lblRoleResource1"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 10px">
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </td>
        <td class="headerDivider_Master" id="LineLogOutTD" runat="server">
            <img width="2" height="60" runat="server" id="imgheaderdivider" visible="false" meta:resourcekey="imgheaderdividerResource1" />
        </td>
        <td>
            <table style="right: 0px">
                <tr>
                    <td class="logoutIcon_Master" id="LogOutTD" runat="server" valign="top">
                        <asp:Button ID="btnLogoutButton" runat="server" UseSubmitBehavior="False" CssClass="BackButton"
                            ToolTip="Logout" OnClick="btnlogout_Click1" Width="66px" Height="19px" BorderWidth="0"
                             />
                        <%-- <asp:ImageButton Width="66px" Height="17px" runat="server" CssClass="HandCursor" UseSubmitBehavior="false"
                            ID="btnlogout" border="0" ImageUrl="~/App_Themes/Images/logout-logo.gif" ToolTip="Logout"
                            OnClick="btnlogout_Click" />--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</center>