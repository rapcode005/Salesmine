<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArrangeColumns.aspx.cs"
    Inherits="WebSalesMine.WebPages.Home.ArrangeColumns" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ReArrange Columns</title>
    <script type="text/javascript" src="../../App_Themes/JS/JqueryJS/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../../App_Themes/JS/JsGrid/ArrangeColumnProductSummary.js"></script>
    <script type="text/javascript" src="../../App_Themes/JS/JsGrid/ArrangeColumn.js"></script>
    <script type="text/javascript">

        var FIREFOX = /Firefox/i.test(navigator.userAgent);


        function ReloadtheparentWindow() {
            window.opener.location.reload(true);
            window.close();
        }

        function onbutton2choose() {
            if ($get('Page').value != "ProductSummary") {
                onclickButton2();
            }
            else {
                onclickButton2ProductSum();
            }
        }

        function onbutton3choose() {
            if ($get('Page').value != "ProductSummary") {
                onclickButton3();
            }
            else {
                onclickButton3ProductSum();
            }
        }

        function onbutton4choose() {
            if ($get('Page').value != "ProductSummary") {
                onclickButton4();
            }
            else {
                onclickButton4ProductSum();
            }
        }

        function onbutton5choose() {
            if ($get('Page').value != "ProductSummary") {
                onclickButton5();
            }
            else {
                onclickButton5ProductSum();
            }
        }

        function onMoveupchoose() {
            if ($get('Page').value != "ProductSummary") {
                onclickbtnUp();
            }
            else {
                onclickbtnUpProductSum();
            }
        }

        function onMovedownchoose() {
            if ($get('Page').value != "ProductSummary") {
                onclickbtnDown();
            }
            else {
                onclickbtnDownProductSum();
            }
        }



        function onfocusbtnSave() {
            for (var i = 0; i < $('#ListBox1').find("option").length; i++) {
                $get('ListBox1')[i].selected = false;
            }
            for (var i = 0; i < $('#ListBox2').find("option").length; i++) {
                $get('ListBox2')[i].selected = false;
            }
        }

        function pageLoad() {

            $addHandler(document.getElementById('btnSave'), 'focus', onfocusbtnSave);

            if ($get('Page').value == "ProductSummary") {
                $addHandler(document.getElementById('ListBox2'), 'keydown', onKeydownListBox2ProductSum);
                $addHandler(document.getElementById('ListBox1'), 'keydown', onKeydownListBox1ProductSum);
            }
            else {
                $addHandler(document.getElementById('ListBox2'), 'keydown', onKeydownListBox2);
                $addHandler(document.getElementById('ListBox1'), 'keydown', onKeydownListBox1);
            }

            //Adding for Saving
            var htmlSelect2 = $get('ListBox2');
            var st = "";

            for (var i = 0; i <= htmlSelect2.length - 1; i++) {
                if (i < htmlSelect2.length - 1)
                    st = st + htmlSelect2[i].value + "|";
                else
                    st = st + htmlSelect2[i].value;
            }
            document.getElementById('<%= ListBox2Value.ClientID %>').value = st;
        }
    </script>
</head>
<body bgcolor="#f1f5f6">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        AsyncPostBackTimeout="3600" EnablePageMethods="true" EnablePartialRendering="true"
        EnableSecureHistoryState="false">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="uppanelMain" runat="server">
        <ContentTemplate>
            <div align="center" style="font-family: Arial; font-size: 12px; color: Red; font-weight: bold">
                <asp:Literal ID="litInfo" runat="server"></asp:Literal>
            </div>
            <br />
            <table align="center">
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="Label1" Text="Available Columns" runat="server" Font-Names="Arial" Font-Size="14px"
                                Font-Bold="true"></asp:Label>
                        </div>
                        <asp:ListBox ID="ListBox1" ClientIDMode="Static" runat="server" Width="175" Height="300"
                            SelectionMode="Multiple"></asp:ListBox>
                    </td>
                    <td>
                        <%-- <asp:Button ID="Button2" runat="server"  ClientIDMode="Static" Text=">" Style="width: 50px" />--%>
                        <input type="button" id="Button2" value=">" style="width: 50px" onclick="onbutton2choose()" />
                        <br />
                        <br />
                        <input type="button" id="Button3" value=">>>" style="width: 50px" onclick="onbutton3choose()" />
                        <%--               <asp:Button ID="Button3" runat="server" ClientIDMode="Static" Text=">>>" Style="width: 50px" />--%>
                        <br />
                        <br />
                        <input type="button" id="Button4" value="<" style="width: 50px" onclick="onbutton4choose()" />
                        <%-- <asp:Button ID="Button4" ClientIDMode="Static" runat="server" Text="<" Style="width: 50px" />--%>
                        <br />
                        <br />
                        <input type="button" id="Button5" value="<<<" style="width: 50px" onclick="onbutton5choose()" />
                        <%--  <asp:Button ID="Button5" ClientIDMode="Static" runat="server" Text="<<<" Style="width: 50px" />--%>
                    </td>
                    <td>
                        <div>
                            <asp:Label ID="Label2" Text="User Selected Columns" runat="server" Font-Names="Arial"
                                Font-Size="14px" Font-Bold="true"></asp:Label>
                        </div>
                        <asp:ListBox ID="ListBox2" ClientIDMode="Static" runat="server" Width="175" Height="300"
                            SelectionMode="Multiple"></asp:ListBox>
                    </td>
                    <td>
                        <%--<asp:Button ID="btnUp" runat="server" Text="MOVE UP" OnClick="MoveUp" Style="width: 100px" />--%>
                        <input type="button" id="btnUp" value="MOVE UP" style="width: 100px" onclick="onMoveupchoose()" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <input type="button" id="btnDown" value="MOVE DOWN" style="width: 100px" onclick="onMovedownchoose()" />
                        <%--<asp:Button ID="Button1" runat="server" Text="MOVE DOWN" OnClick="MoveDown" />--%>
                    </td>
                </tr>
            </table>
            <br />
            <table align="center">
                <tr>
                    <td>
                        <asp:Button ID="Button6" runat="server" ToolTip="Default" Text="Default" OnClick="btnDefault_Click"
                            Width="100" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" ToolTip="Save Order"
                            Text="Save" OnClick="btnSave_Click" Style="width: 100px" />
                    </td>
                    <td>
                        <input type="button" value="Close Window" onclick="window.close();">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="ListBox2Value" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Page" runat="server" ClientIDMode="Static" Value="" />
    </form>
</body>
</html>
