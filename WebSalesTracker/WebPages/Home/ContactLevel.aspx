<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactLevel.aspx.cs" 
Inherits="WebSalesMine.WebPages.Home.ContactLevel"
    EnableEventValidation="false" %>

<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<%@ Import Namespace="WebSalesMine.WebPages.Home" %>
<%@ Import Namespace="AppLogic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--  <link href="../../App_Themes/CSS/Site.css" rel="Stylesheet" type="text/css" />--%>
    <link href="../../App_Themes/CSS/styles.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/ButtonEffect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../App_Themes/JS/JqueryJS/jquery-1.7.1.min.js"></script>
    <script language="javascript" type="text/javascript">

        function getCookie(c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        }

        function setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value + ';path=/';
        }

        function checkCookie() {
            var username = getCookie("username");
            if (username != null && username != "") {
                alert("Welcome again " + username);
            }
            else {
                username = prompt("Please enter your name:", "");
                if (username != null && username != "") {
                    setCookie("username", username, 365);
                }
            }
        }

        function pageLoad() {
            var radio = null;
            var globalcontactVal = null;
            var globalAccntVal = null;
            var globalcontactnameVal = null;

            $(document).ready(function () {
                $("#gridShowContact  tr:has(td)").hover(function () {
                    $(this).css("cursor", "pointer"); ;
                });

                $("#gridShowContact tr").not($("#gridShowContact tr").eq(0)).click(function () {
                    $("#gridShowContact").find("INPUT[type='radio']").attr("checked", false);

                    var FIREFOX = /Firefox/i.test(navigator.userAgent);
                    radio = $(this).find("INPUT[type='radio']");
                    var list = radio.closest('table').find("INPUT[type='radio']").not(radio);
                    list.closest('TR').removeClass('SelectedRowStyle');

                    radio.attr("checked", true);
                    radio.closest('TR').addClass('SelectedRowStyle');
                    $("#hfIndex").val($(this).index());


                    var CNo = $(this).find("td:eq(1)").html();
                    var CName = $(this).find("td:eq(2)").html();

                    $get('txtCustomerNumber').value = CNo;
                    $get('txtCustomerName').value = CName;

                    setCookie('CNoTemp', CNo, 1);
                    setCookie('CNameTemp', CName, 1);

                    // setCookie('Xpos', $get('pnl1').scrollLeft, 1);
                    // setCookie('Ypos', $get('pnl1').scrollTop, 1);
                });

                $('#txtSearchContact').keyup(function (e) {

                    $("#gridShowContact tr:has(td)").hide(); // Hide all the rows.

                    var sSearchTerm = $('#txtSearchContact').val(); //Get the search box value

                    if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                    {
                        $("#gridShowContact tr:has(td)").show();
                        return false;
                    }

                    if ($get('rdoContactName').checked == true) {
                        //Iterate through all the td.
                        $("#gridShowContact tr").children("td:nth-child(3)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    else if ($get('rdoContactNumber').checked == true) {
                        $("#gridShowContact tr").children("td:nth-child(2)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    e.preventDefault();
                });

                //Click Button
                $('#imbSearchAcntNumber').keyup(function (e) {

                    $("#gridShowContact tr:has(td)").hide(); // Hide all the rows.

                    var sSearchTerm = $('#txtSearchContact').val(); //Get the search box value

                    if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                    {
                        $("#gridShowContact tr:has(td)").show();
                        return false;
                    }

                    if ($get('rdoContactName').checked == true) {
                        //Iterate through all the td.
                        $("#gridShowContact tr").children("td:nth-child(3)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    else if ($get('rdoContactNumber').checked == true) {
                        $("#gridShowContact tr").children("td:nth-child(2)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    e.preventDefault();
                });

                $("#gridShowContact").show(function (e) {
                    $("#gridShowContact tr").children("td:nth-child(2)").each(function () {
                        var cellText = $(this).text().toLowerCase();
                        var FIREFOX = /Firefox/i.test(navigator.userAgent);

                        if (cellText == $get("Number").value) //Check if data matches
                        {
                            radio = $(this).parent().find("INPUT[type='radio']");
                            radio.attr("checked", true);
                            radio.closest('TR').addClass('SelectedRowStyle');

                            var CNo = $(this).parent().find("td:eq(1)").html();
                            var CName = $(this).parent().find("td:eq(2)").html();

                            $get('txtCustomerNumber').value = CNo;
                            $get('txtCustomerName').value = CName;

                            setCookie('CNo', CNo, 1);
                            setCookie('CName', CName, 1);

                            //$get('pnl1').scrollLeft = getCookie('XposT');
                            // $get('pnl1').scrollTop = getCookie('YposT');

                            return false;
                        }

                    })

                    return false;
                });

            })
        }

        function SelectSingleRadiobutton(rdBtnID) {
            var rduser = $(document.getElementById(rdBtnID));

            rduser.closest('TR').addClass('SelectedRowStyle');
            rduser.checked = true;
            var list = rduser.closest('table').find("INPUT[type='radio']").not(rduser);
            list.attr('checked', false);
            list.closest('TR').removeClass('SelectedRowStyle');

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerContact" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <asp:Panel ID="PnlContactLevel" runat="server" DefaultButton="imbSearchAcntNumber">
        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="rdoContactName" CssClass="radioButton" ClientIDMode="Static"
                        AutoPostBack="true" runat="server" Text=" Contact Name" GroupName="RdoFilterKam"
                        OnCheckedChanged="rdoContactName_CheckedChanged" />
                </td>
                <td style="padding-left: 10px;">
                    <asp:RadioButton ID="rdoContactNumber" CssClass="radioButton" AutoPostBack="true"
                        runat="server" Text=" Contact Number" GroupName="RdoFilterKam" OnCheckedChanged="rdoContactNumber_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="SearchBox">
                        <asp:TextBox ID="txtSearchContact" CssClass="txtSearch" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="imbSearchAcntNumber" ClientIDMode="Static" CssClass="SearchBox btnSearch"
                            ImageUrl="~/App_Themes/Images/New Design/magnifying-glass-search-icon.jpg" runat="server"
                            ToolTip="Search Account" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="CssErrorLabel">
                    <asp:Literal ID="litErrorinGrid" Visible="false" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdateContactLevel" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="table-wrapper page1">
                                    <table style="border: 0;" cellpadding="10" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="DataDiv" runat="server" ClientIDMode="Static" ScrollBars="Auto" Width="628px"
                                                    Height="180px">
                                                    <asp:GridView ClientIDMode="Static" AllowSorting="true" ID="gridShowContact" runat="server"
                                                        AutoGenerateColumns="false" DataKeyNames="contmerg" EmptyDataText="No Record Found"
                                                        ForeColor="Black" BackColor="#FFFFFF" EnablePersistedSelection="true" GridLines="None"
                                                        CellPadding="4" CellSpacing="2" 
                                                        OnPreRender="gridShowContact_Prerender" 
                                                        OnSorting="gridShowContact_Sorting"
                                                        >
                                                        <AlternatingRowStyle BackColor="#e5e5e5" />
                                                        <EditRowStyle CssClass="EditRowStyle" />
                                                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                        <RowStyle CssClass="RowStyle" Wrap="false" />
                                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:RadioButton runat="server" ID="rdbUser" onclick="javascript:SelectSingleRadiobutton(this.id)" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="CONTACT NUMBER" DataField="contmerg" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="contmerg" />
                                                            <asp:BoundField HeaderText="CONTACT NAME" DataField="name" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="name" />
                                                            <asp:BoundField HeaderText="CONTACT TYPE" DataField="contact_type" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="contact_type" />
                                                            <asp:BoundField HeaderText="RECENCY" DataField="rec_mo" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="rec_mo" />
                                                            <asp:BoundField HeaderText="Total Orders" DataField="OrderQuote" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="OrderQuote" />
                                                            <asp:BoundField HeaderText="SALES 12M" DataField="Sales12M" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="Sales12M" />
                                                            <asp:BoundField HeaderText="SAP STATUS" DataField="CONTACT_STATUS" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="CONTACT_STATUS" />
                                                            <asp:BoundField HeaderText="FUNCTION" DataField="function" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="function" />
                                                            <asp:BoundField HeaderText="REP CONT STAT" DataField="REP_CONTSTAT" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="REP_CONTSTAT" />
                                                            <asp:BoundField HeaderText="REP JOB AREA" DataField="REP_JOBAREA" HeaderStyle-Wrap="false"
                                                                HeaderStyle-HorizontalAlign="Center" SortExpression="REP_JOBAREA" />
                                                            <asp:ButtonField CommandName="Select" Visible="false" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Panel ID="pnlGridIndex" runat="server">
                                    <%--  <i>You are viewing page
                                        <%=gridShowContact.PageIndex + 1%>
                                        of
                                        <%=gridShowContact.PageCount%>
                                    </i>--%>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imbSearchAcntNumber" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="txtSearchContact" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdateSelect" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblCustomerNumber" CssClass="LabelFont" Text="Customer Number" runat="server" />
                    </td>
                    <td style="padding-left: 10px;">
                        <asp:Label ID="lblCustomerName" CssClass="LabelFont" Text="Customer Name" runat="server"
                            AssociatedControlID="txtCustomerName" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCustomerNumber" CssClass="textbox curved" ClientIDMode="Static"
                            runat="server" AutoPostBack="True" OnTextChanged="txtCustomerNumber_TextChanged" />
                        <%--<asp:Label ID="txtCustomerNumber" ClientIDMode="Static" runat="server" CssClass="textboxLabel" ></asp:Label>--%>
                    </td>
                    <td style="padding-left: 10px;">
                        <%--<asp:Label ID="txtCustomerName" runat="server" ClientIDMode="Static" CssClass="textboxLabel" ></asp:Label>--%>
                        <input type="text" name="txtCustomerName" class="textbox curved" id="txtCustomerName"
                            style="width: 100%;" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gridShowContact" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <%--    </div>--%>
    <asp:HiddenField ID="Name" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Number" runat="server" ClientIDMode="Static" />
    </form>
</body>
</html>
