<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="WebSalesMine.WebPages.Home.Message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../App_Themes/CSS/styles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../App_Themes/JS/JqueryJS/jquery-1.7.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerMessage" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <div >
            <asp:TextBox ID="txtMessage" TextMode="MultiLine" style="resize:none;"
                runat="server" Height="130px" Font-Size="15px" CssClass="Frame"
                Width="400px"  BorderStyle="None" ReadOnly="True"></asp:TextBox>
        </div>
    </form>
</body>
</html>
