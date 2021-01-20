<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebSalesMine.WebPages.Login.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">

    function setCookie(c_name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value + ';path=/';
    }

    function textboxfunctionPass(sender, e) {
        var password = document.getElementById("<%=txtPassword.ClientID%>").value; //  $get('txtPassword').textContent;
        document.getElementById("txtPassword").className = "";
        document.getElementById("txtPassword").className = "login-textbox";


        if (password == '') {
            //alert("Empty");
            document.getElementById("txtPassword").className = "";
            document.getElementById("txtPassword").className = "login-textbox-empty-pass";

        }
        else {
            document.getElementById("txtPassword").className = "";
            document.getElementById("txtPassword").className = "login-textbox";

        }
        if (document.getElementById("txtUserName").value != '' && document.getElementById("txtPassword").value != '') {
            // document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-enable";
            document.getElementById("imbLogin").disable = true;

            //alert("FunctionPass");

            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-enable.png';
        }
        else {
            // document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-disable";
            document.getElementById("imbLogin").disable = false;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-disable.png';
        }

        if (e) {
            kc = e.keyCode ? e.keyCode : e.which;
            sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);
            if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
                document.getElementById('divMayus').style.visibility = 'visible';
            else
                document.getElementById('divMayus').style.visibility = 'hidden';
        }

    }

    function textboxfunctionUser(sender, e) {
        var username = document.getElementById("<%=txtUserName.ClientID%>").value; //  $get('username').textContent;
        var password = document.getElementById("<%=txtPassword.ClientID%>").value; //  $get('password').textContent;
        document.getElementById("txtUserName").className = "";
        document.getElementById("txtUserName").className = "login-textbox";


        if (username == '') {
            //alert("Empty");
            document.getElementById("txtUserName").className = "";
            document.getElementById("txtUserName").className = "login-textbox-empty-user";

        }
        else {

            document.getElementById("txtUserName").className = "";
            document.getElementById("txtUserName").className = "login-textbox";

        }

        if (password == '') {
            //alert("Empty");
            document.getElementById("txtPassword").className = "";
            document.getElementById("txtPassword").className = "login-textbox-empty-pass";

        }
        else {
            document.getElementById("txtPassword").className = "";
            document.getElementById("txtPassword").className = "login-textbox";

        }

        if (document.getElementById("txtUserName").value != '' && document.getElementById("txtPassword").value != '') {
            //  document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-enable";
            document.getElementById("imbLogin").disable = true;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-enable.png';
            // alert("User");
        }
        else {
            // document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-disable";
            document.getElementById("imbLogin").disable = false;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-disable.png';
        }


        if (e) {
            kc = e.keyCode ? e.keyCode : e.which;
            sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);
            if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
                document.getElementById('divMayus').style.visibility = 'visible';
            else
                document.getElementById('divMayus').style.visibility = 'hidden';
        }

    }

    function disableautocompletion(id) {
        var passwordControl = document.getElementById(id);
        passwordControl.setAttribute("autocomplete", "off");
    }


    function textBoxFocus(sender, e) {
        if (document.getElementById("txtPassword").value != '') {
            document.getElementById("txtPassword").className = "login-textbox";
        }
        else {
            document.getElementById("txtPassword").className = "login-textbox-empty-pass";
        }


        if (document.getElementById("txtPassword").value != '') {
            document.getElementById("txtPassword").className = "login-textbox";
        }
        else {
            document.getElementById("txtPassword").className = "login-textbox-empty-pass";
        }

        if (document.getElementById("txtUserName").value != '' && document.getElementById("txtPassword").value != '') {
            //  document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-enable";
            document.getElementById("imbLogin").disable = true;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-enable.png';
            // alert("Focus");
        }
        else {
            // document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-disable";
            document.getElementById("imbLogin").disable = false;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-disable.png';
        }

        if (e) {
            kc = e.keyCode ? e.keyCode : e.which;
            sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);
            if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
                document.getElementById('divMayus').style.visibility = 'visible';
            else
                document.getElementById('divMayus').style.visibility = 'hidden';
        }
    }


    function textboxkeydownPass(sender, e) {
        //        document.getElementById("txtPassword").className = "login-textbox";
        if (document.getElementById("txtPassword").value != '') {
            document.getElementById("txtPassword").className = "login-textbox";
        }
        else {
            document.getElementById("txtPassword").className = "login-textbox-empty-pass";
        }

        if (document.getElementById("txtUserName").value != '' && document.getElementById("txtPassword").value != '') {
            // document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-enable";
            document.getElementById("imbLogin").disable = true;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-enable.png';
            // alert("Password");

        }
        else {
            // document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-disable";
            document.getElementById("imbLogin").disable = false;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-disable.png';

        }

        if (e) {
            kc = e.keyCode ? e.keyCode : e.which;
            sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);
            if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
                document.getElementById('divMayus').style.visibility = 'visible';
            else
                document.getElementById('divMayus').style.visibility = 'hidden';
        }
        //alert("keydown");
    }



    function DisplayNullmessage() {

        //Check Username and Password
        if (document.getElementById("<%=txtUserName.ClientID%>").value == "" && document.getElementById("<%=txtPassword.ClientID%>").value == "") {
            document.getElementById("<%=lblErrorMessage.ClientID%>").innerText = "User Name and Password cannot Empty";
        }
        //Check Username
        else if (document.getElementById("<%=txtUserName.ClientID%>").value == "") {
            document.getElementById("<%=txtUserName.ClientID%>").focus();
            document.getElementById("<%=lblErrorMessage.ClientID%>").innerText = "User Name cannot Empty";
        }
        //Check Password
        else if (document.getElementById("<%=txtPassword.ClientID%>").value == "") {
            document.getElementById("<%=txtPassword.ClientID%>").focus();
            document.getElementById("<%=lblErrorMessage.ClientID%>").innerText = "Password cannot Empty";
        }
    }

    function CleanDisplayMessage() {
        //debugger;
        document.getElementById("<%=lblErrorMessage.ClientID%>").innerText = "";
        document.getElementById("<%=txtUserName.ClientID%>").value = "";
        document.getElementById("<%=txtPassword.ClientID%>").value = "";
        document.getElementById("<%=txtUserName.ClientID%>").focus();
        return false;
    }

    function pageLoad() {

        setCookie('CSS', '', -1);

        //document.getElementById("txtPassword").value = 'Password';

        if (document.getElementById("txtPassword").value != '') {
            document.getElementById("txtPassword").className = "login-textbox";
        }
        else {
            document.getElementById("txtPassword").className = "login-textbox-empty-pass";
        }


        if (document.getElementById("txtUserName").value != '') {
            document.getElementById("txtUserName").className = "login-textbox";
        }
        else {
            document.getElementById("txtUserName").className = "login-textbox-empty-user";
        }

        if (document.getElementById("txtUserName").value != '' && document.getElementById("txtPassword").value != '') {
            //document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-enable";
            document.getElementById("imbLogin").disable = false;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-enable.png';
        }
        else {
            //  document.getElementById("loginbtn").className = "loginbtn";
            document.getElementById("imbLogin").className = "imbLogin-disable";
            document.getElementById("imbLogin").disable = true;
            document.getElementById("imbLogin").src = '../../App_Themes/Images/New Design/button-login-disable.png';


        }



    }

</script>
<script type="text/javascript">
    window.history.forward();
    function noBack() { window.history.forward(); }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Web SalesMine</title>
    <link href="../../App_Themes/CSS/LoginControl.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../App_Themes/CSS/main-stylesheet.css" />
    <link rel="stylesheet" type="text/css" href="../../App_Themes/CSS/styles.css" />
    <link id="LoginIco" runat="server" rel="shortcut icon" href="../../App_Themes/Images/sales_mine_icon.ico"
        type="image/x-icon" />
    <link id="LoginIco2" runat="server" rel="icon" href="../../App_Themes/Images/sales_mine_icon.ico"
        type="image/ico" />
    <script language="javascript" type="text/javascript" src="../../App_Themes/JS/MasterPage.js"></script>
</head>
<body class="login">
    <div id="login-container">
        <div id="logo">
            <img src="../../App_Themes/Images/New%20Design/salesmine-logo.png" width="315" height="59"
                alt="Salesmine" border="0" /></div>
        <form id="form1" runat="server" name="">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
            AsyncPostBackTimeout="3600">
        </asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" align="center" style="margin: 0px auto;">
            <tr>
                <td height="10">
                    <%--  Company Email / Brc Login--%>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="DivUserName" style="padding-left: 5px; padding-top: 5px">
                        <asp:TextBox ID="txtUserName" type="text" CssClass="login-textbox" runat="server"
                            ClientIDMode="Static" onkeypress="textboxfunctionUser(this, event)" AutoCompleteType="Disabled"
                            AutoComplete="off" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="#FF8000"
                            SetFocusOnError="True" ControlToValidate="txtUserName" Text="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator><br />
                    </div>
                </td>
            </tr>
            <tr>
                <td height="15">
                    <%--  Password--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PanelPassword" runat="server" DefaultButton="imbLogin">
                        <div id="DivPassword" style="padding-left: 5px; padding-top: 5px">
                            <asp:TextBox ID="txtPassword" type="text" CssClass="login-textbox-empty-pass" runat="server"
                                ClientIDMode="Static" onkeypress="textboxfunctionPass(this, event)"
                                onblur="textboxfunctionPass(this, event)" TextMode="Password" />
                        </div>
                        <div id="loginbtn">
                            <asp:ImageButton ID="imbLogin" ClientIDMode="Static" runat="server" ToolTip="Login"
                                OnClick="imbLogin_Click" value="" ImageUrl="../../App_Themes/Images/New Design/button-login-disable.png" />
                        </div>
                        <br />
                        <div style="padding-left: 5px; padding-top: 5px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="#FF8000"
                                Style="border-style: none; outline: none;" SetFocusOnError="True" ControlToValidate="txtPassword"
                                Text="&nbsp;*" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        </div>
                        <br />
                        <div id="divMayus" style="visibility: hidden; color: Red; padding-left: 5px; padding-top: 5px">
                            Caps Lock is on.</div>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="lblMsg_LoginControl" ForeColor="Red"
                        meta:resourcekey="lblErrorMessageResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <div align="left">
                        <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="chkRememberMe" Text=" Keep me signed in" />
                    </div>
                </td>
            </tr>
            <%--<tr>
                <td valign="bottom" height="60">
                    <asp:Button ID="loginbutton" value="LOGIN" type="submit" runat="server" Text="" OnClick="loginbutton_Click"
                        ToolTip="Please login using your InsideBrady account" CssClass="login-Button" />
                </td>
            </tr>--%>
        </table>
        </form>
    </div>
</body>
</html>
