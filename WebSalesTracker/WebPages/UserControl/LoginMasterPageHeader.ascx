<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginMasterPageHeader.ascx.cs"
    Inherits="WebSalesMine.WebPages.UserControl.LoginMasterPageHeader" %>
<link href="../../App_Themes/CSS/SalesMineMasterPage.css" rel="stylesheet" type="text/css" />

<table class="headerTable_Master" style="height: 60px;" runat="server" id="tblHeaderTable">
  <tr>
    <td class="headerLogo_Master" style="width: 87%">
      <table id="LogoTable" cellpadding="0" width="100%" cellspacing="0" border="0">
        <tr visible="False">
          <td style="width: 159px">
            <asp:HyperLink ID="imgbradylogo" runat="server" 
              ImageUrl="~/App_Themes/Images/brady-logo.gif"></asp:HyperLink>
         
          </td>
          <td align="right" style="width: 100%" valign="top">
            <table cellpadding="0" cellspacing="0" border="0">
              <tr>
                <td valign="top" align="right">
                  <asp:Label ID="lblUser" runat="server" Visible="False" CssClass="lbluser_Master"
                    meta:resourcekey="lblUserResource1"></asp:Label>
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
           
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>