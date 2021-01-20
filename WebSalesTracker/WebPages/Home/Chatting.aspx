<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chatting.aspx.cs" Inherits="WebSalesMine.WebPages.Home.Chatting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salesmine Messaging</title>
    <link href="../../App_Themes/CSS/styles.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/ButtonEffect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../App_Themes/JS/JqueryJS/jquery-1.7.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtMessageList" CssClass="textbox curved" ClientIDMode="Static"
                        runat="server" TextMode="MultiLine" Width="300px" Height="300px" />
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" CssClass="LabelFont" Text="Message" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtMessage" CssClass="textbox curved" ClientIDMode="Static" runat="server" 
                                Width="200px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button id="btnMessage" runat="server" Text="Send" OnClick="onMessageClick" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
