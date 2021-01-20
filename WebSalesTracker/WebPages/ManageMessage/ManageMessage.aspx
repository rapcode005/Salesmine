<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ManageMessage.aspx.cs" Inherits="WebSalesMine.WebPages.ManageMessage.ManageMessage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
    <style type="text/css">
        .FixedPostion
        {
            position: fixed;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            $addHandler(document.getElementById('txtbSearch'), 'keyup', onKeyup);
            $addHandler(document, 'keydown', onKeydownMessage);
            $(document).ready(function () {

                $('#lnkAddState').click(function () {

                    var mydiv = $('#pnlAddManageMessage');
                    mydiv.dialog({ autoOpen: false,
                        title: "Add New State Message",
                        resizable: false,
                        width: "auto",
                        height: "auto",
                        modal: true,
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                            var currentTime = new Date();
                            var month = currentTime.getMonth() + 1;
                            var day = currentTime.getDate();
                            var year = currentTime.getFullYear();
                            //$get('txtAddValidTo').value = month + "/" + day + "/" + year;
                            $get('ddlAddCampaign').value = "";
                            $get('txtAddState').value = "";
                            $get('txtAddMessage').value = "";
                            $get('btnOkState').value = "Ok";
                            $get('AddEdit').value = "Add";
                        }
                    });

                    mydiv.dialog('open');

                    return false;
                });

                $("#grdMessage tr").not($("#grdMessage tr").eq(0)).click(function () {

                    $("#grdMessage tr").closest('TR').removeClass('SelectedRowStyle');
                    $(this).addClass('SelectedRowStyle');

                    var Campaign = $(this).find("td:eq(0)").html();
                    var State = $(this).find("td:eq(1)").html();

                    $get('State').value = State;
                    setCookie('Campaign', Campaign, 1);

                    //PageMethods.GetDatafromXMLDetails(State, Campaign, onSuccess);

                    //View Details
                    var grd = document.getElementById('grdMessage');
                    var CampaignValue, StateValue, MessageValue, EffDateValue;
                    var index = $(this).index();
                    var ColName;

                    if (grd) {
                        if (isNaN(index) == false) {
                            totalCols = $("#grdMessage").find('tr')[0].cells.length;
                            for (var i = 1; i < totalCols + 1; i++) {

                                ColName = $('#grdMessage tr').find('th:nth-child(' + i + ')').text();

                                if (ColName == 'CAMPAIGN') {
                                    if (grd.rows[index].cells[i - 1]) {
                                        CampaignValue = grd.rows[index].cells[i - 1].innerHTML;
                                        if (CampaignValue == undefined || CampaignValue == '&nbsp;') {
                                            CampaignValue = '';
                                        }
                                    }
                                    else
                                        CampaignValue = '';
                                }
                                else if (ColName == 'STATE') {
                                    if (grd.rows[index].cells[i - 1]) {
                                        StateValue = grd.rows[index].cells[i - 1].innerHTML;
                                        if (StateValue == undefined || StateValue == '&nbsp;') {
                                            StateValue = '';
                                        }
                                    }
                                    else
                                        StateValue = '';
                                }
                                else if (ColName == 'MESSAGE') {
                                    if (grd.rows[index].cells[i - 1]) {
                                        MessageValue = grd.rows[index].cells[i - 1].innerHTML;
                                        if (MessageValue == undefined || MessageValue == '&nbsp;') {
                                            MessageValue = '';
                                        }
                                    }
                                    else
                                        MessageValue = '';
                                }
                                else if (ColName == 'EFFECTIVITY DATE') {
                                    if (grd.rows[index].cells[i - 1]) {
                                        EffDateValue = grd.rows[index].cells[i - 1].innerHTML;
                                        if (EffDateValue == undefined || EffDateValue == '&nbsp;') {
                                            EffDateValue = '';
                                        }
                                    }
                                    else
                                        EffDateValue = '';
                                }
                            }
                        }


                        $get('ddlAddCampaign').value = CampaignValue;
                        $get('txtAddState').value = StateValue;
//                        myDate = new Date(EffDateValue);
//                        if (EffDateValue != "") {
//                            $get('txtAddValidTo').value = (myDate.getMonth() + 1) + "/" +
//                                                myDate.getDate() + "/" +
//                                                myDate.getFullYear();
//                        }
//                        else
//                            $get('txtAddValidTo').value = '';

                        $get('txtAddMessage').value = MessageValue;

                    }


                    var mydiv = $('#pnlAddManageMessage');
                    mydiv.dialog({ autoOpen: false,
                        title: "Edit State Message",
                        resizable: false,
                        width: "auto",
                        modal: false,
                        dialogClass: "FixedPostion",
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                            $get('btnOkState').value = "Update";
                            $get('AddEdit').value = "Edit";
                        }
                    });

                    mydiv.dialog('open');

                    return false;

                });

            });


        }

        function onKeydownMessage(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                {
                    var mydiv = $('#pnlAddManageMessage');
                    mydiv.dialog('Close');
                }
            }
        }

        function onKeyup(e) {
            $("#grdMessage tr:has(td)").hide(); // Hide all the rows.

            var sSearchTerm = $('#txtbSearch').val(); //Get the search box value

            if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
            {
                $("#grdMessage tr:has(td)").show();
                return false;
            }

            if ($get('rdoState').checked == true) {
                //Iterate through all the td.
                $("#grdMessage tr").children("td:nth-child(2)").each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        return true;
                    }

                });
            }
            else if ($get('rdoCampaign').checked == true) {
                $("#grdMessage tr").children("td:nth-child(1)").each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        return true;
                    }

                });
            }
            e.preventDefault();
        }

        function CheckString(val) {
            if (val)
                return val;
            else
                return "";
        }

        function onSuccess(Data) {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            var myDate;

            if (Data) {
                $get('ddlAddCampaign').value = CheckString(Data.rows[0]['Campaign']);
                $get('txtAddState').value = CheckString(Data.rows[0]['State']);
//                myDate = new Date(CheckString(Data.rows[0]['EffDate']));
//                if (CheckString(Data.rows[0]['EffDate']) != "") {
//                    $get('txtAddValidTo').value = (myDate.getMonth() + 1) + "/" +
//                                                myDate.getDate() + "/" +
//                                                myDate.getFullYear();
//                }
//                $get('txtAddMessage').value = CheckString(Data.rows[0]['Message']);
            }
            else {
                $get('ddlAddCampaign').value = "";
            }

        }


        function Save() {
            alert('Successsfully Saved');
            
        }


        var confirm_value2 = document.createElement("INPUT");
        function Confirm() {
            PageMethods.GetDatafromXMLDetails($get('txtAddState').value, $get('ddlAddCampaign').value, onGettingData);
        }

        function onGettingData(Data) {
            if (Data.rows) {
                if (Data.rows.length > 0) {
                    if ($get('txtAddState').value == $get('State').value &&
                        $get('ddlAddCampaign').value == getCookie('Campaign')) {
                        PageMethods.Update($get('txtAddState').value, $get('State').value,
                            $get('ddlAddCampaign').value, getCookie('Campaign'), $get('txtAddMessage').value,
                            "9999-12-31", Save);
                        setCookie('Refresh', 'ok', 1);
                        __doPostBack('= btnRefresh.ClientID ', '');
                    }
                    else {
                        if (confirm('For this Campaign ' + $get('ddlAddCampaign').value + ' and State ' + $get('txtAddState').value + ' is already exist. /nDo you want still to update?') == true) {
                            PageMethods.Update($get('txtAddState').value, $get('State').value,
                            $get('ddlAddCampaign').value, getCookie('Campaign'), $get('txtAddMessage').value,
                            "9999-12-31", Save);
                            setCookie('Refresh', 'ok', 1);
                            __doPostBack('= btnRefresh.ClientID ', '');
                        }
                        else {
                            Open();
                        }
                    }
                }
                else {
                    if ($get('AddEdit').value == 'Add') {
                        PageMethods.AddMessage($get('txtAddState').value, $get('txtAddMessage').value,
                        "9999-12-31", $get('ddlAddCampaign').value, Save);
                        setCookie('Refresh', 'ok', 1);
                        __doPostBack('= btnRefresh.ClientID ', '');
                    }
                    else {
                        PageMethods.Update($get('txtAddState').value, $get('State').value,
                        $get('ddlAddCampaign').value, getCookie('Campaign'), $get('txtAddMessage').value,
                        "9999-12-31", Save);
                        setCookie('Refresh', 'ok', 1);
                        __doPostBack('= btnRefresh.ClientID ', '');
                    }
                }
            }
            else {
                if ($get('AddEdit').value == 'Add') {
                    PageMethods.AddMessage($get('txtAddState').value, $get('txtAddMessage').value,
                   "9999-12-31", $get('ddlAddCampaign').value, Save);
                    setCookie('Refresh', 'ok', 1);
                    __doPostBack('= btnRefresh.ClientID ', '');
                }
                else {
                    PageMethods.Update($get('txtAddState').value, $get('State').value,
                    $get('ddlAddCampaign').value, getCookie('Campaign'), $get('txtAddMessage').value,
                    "9999-12-31", Save);
                    setCookie('Refresh', 'ok', 1);
                    __doPostBack('= btnRefresh.ClientID ', '');
                }
            }

        }



        function SaveSuccess() {
            alert("Successsfully Saved");

            var mydiv = $('#pnlAddManageMessage');
            mydiv.dialog({ autoOpen: false,
                title: "Add New State Message",
                resizable: false,
                width: "auto",
                modal: true,
                dialogClass: "FixedPostion",
                closeOnEscape: true,
                open: function (type, data) {
                    $(this).parent().appendTo("form"); //won't postback unless within the form tag
                }
            });

            mydiv.dialog('close');

            return false;
        }

        function WithoutSave() {
            var mydiv = $('#pnlAddManageMessage');
            if ($get('AddEdit').value == "Add") {
                mydiv.dialog({ autoOpen: false,
                    title: "Add New State Message",
                    resizable: false,
                    width: "auto",
                    modal: true,
                    dialogClass: "FixedPostion",
                    open: function (type, data) {
                        $(this).parent().appendTo("form"); //won't postback unless within the form tag
                    }
                });
            }
            else {
                mydiv.dialog({ autoOpen: false,
                    title: "Edit New State Message",
                    resizable: false,
                    width: "auto",
                    modal: false,
                    dialogClass: "FixedPostion",
                    open: function (type, data) {
                        $(this).parent().appendTo("form"); //won't postback unless within the form tag
                    }
                });
            }
            mydiv.dialog('Close');

        }

        function Open() {
            var mydiv = $('#pnlAddManageMessage');
            if ($get('AddEdit').value == "Add") {
                mydiv.dialog({ autoOpen: false,
                    title: "Add New State Message",
                    resizable: false,
                    width: "auto",
                    modal: true,
                    dialogClass: "FixedPostion",
                    open: function (type, data) {
                        $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        $get('btnOkState').value = "Ok";
                        $get('AddEdit').value = "Add";
                    }
                });
            }
            else {
                mydiv.dialog({ autoOpen: false,
                    title: "Edit New State Message",
                    resizable: false,
                    width: "auto",
                    modal: false,
                    dialogClass: "FixedPostion",
                    open: function (type, data) {
                        $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        $get('btnOkState').value = "Update";
                        $get('AddEdit').value = "Edit";
                    }
                });
            }
            mydiv.dialog('open');
        }

    </script>
    <div id="container">
        <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%"
            height="100%">
            <tr>
                <td style="width: 265px">
                    <table>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td class="object-wrapper" style="height: 20px">
                                                                <asp:LinkButton ID="lnkAddState" runat="server" CssClass="LabelFont" Text="Add New State Message"
                                                                    ClientIDMode="Static"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="middle" style="width: 265px">
                    <asp:Panel ID="pnlMessage" runat="server">
                        <div id="searchboxdivAdmin" style="padding-left: 5px; padding-top: 5px">
                            <div id="label2">
                                Value</div>
                            <asp:TextBox ID="txtbSearch" runat="server" ClientIDMode="Static" CssClass="txtSearch">
                            </asp:TextBox>
                            <asp:ImageButton ID="btnSearch" ClientIDMode="Static" CssClass="btnSearch" ImageUrl="~/App_Themes/Images/New Design/magnifying-glass-search-icon.jpg"
                                runat="server" ToolTip="Search User" />
                            <div class="clear">
                                <br />
                            </div>
                        </div>
                    </asp:Panel>
                </td>
                <td align="left" style="width: 60px">
                    <asp:RadioButton ID="rdoState" runat="server" Text=" State" GroupName="RdoManageUser"
                        CssClass="radioList" ClientIDMode="Static" Checked="true" />
                </td>
                <td style="width: 11px">
                    <img src="../../App_Themes/Images/New Design/divider-2.png" width="1" height="25"
                        border="0" />
                </td>
                <td>
                    <asp:RadioButton ID="rdoCampaign" runat="server" Text=" Campaign" GroupName="RdoManageUser"
                        CssClass="radioList" ClientIDMode="Static" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <div class="table-wrapper page1">
                        <table border="0" cellpadding="10" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel runat="server" ScrollBars="None" ID="DataDiv" Width="100%">
                                        <asp:GridView ID="grdMessage" runat="server" AutoGenerateColumns="false" ClientIDMode="Static"
                                            GridLines="None" AsyncRendering="false" EmptyDataText="No data available." CellPadding="4"
                                            CellSpacing="2" Font-Size="12px" BackColor="#FFFFFF" ForeColor="Black">
                                            <AlternatingRowStyle BackColor="#e5e5e5" />
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                            <RowStyle CssClass="RowStyle" Wrap="false" />
                                            <Columns>
                                                <asp:BoundField HeaderText="CAMPAIGN" DataField="Campaign" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center" SortExpression="Campaign" />
                                                <asp:BoundField HeaderText="STATE" DataField="State" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="State" />
                                                <asp:BoundField HeaderText="MESSAGE" DataField="Message" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center" SortExpression="Message" />
                                                <asp:BoundField HeaderText="DATE CREATED" DataField="Valid_from" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center" SortExpression="Valid_from" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField HeaderText="USERNAME" DataField="Username" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center" SortExpression="Username" />
                                                <asp:BoundField HeaderText="EFFECTIVITY DATE" DataField="EffDate" HeaderStyle-Wrap="false"
                                                    HeaderStyle-HorizontalAlign="Center" SortExpression="EffDate" DataFormatString="{0:MM/dd/yyyy}" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlAddManageMessage" runat="server" Style="display: none;" ClientIDMode="Static">
            <table style="padding-bottom: 15px;">
        <%if (AddValidTo == "true")
                        { %>
            <tr>
                <td>
                    <asp:Label ID="lblAddValidTo" CssClass="LabelFont" runat="server" Text="Effectivity Date" Visible="false"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAddValidTo" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgstartCal" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                    runat="server" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtAddValidTo"
                                    PopupButtonID="imgstartCal">
                                </asp:CalendarExtender>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtAddValidTo"
                                    Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" InputDirection="RightToLeft"
                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
                <% }%>
                <tr>
                    <td>
                        <asp:Label ID="lblAddCampaign" CssClass="LabelFont" runat="server" Text="Campaign"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlAddCampaign" runat="server" ClientIDMode="Static" Style="font-size: 12px;"
                                        CssClass="textbox" OnSelectedIndexChanged="ddlAddCampaign_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddState" CssClass="LabelFont" runat="server" Text="State"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtAddState" MaxLength="2" CssClass="textbox curved" Style="font-size: 12px;
                                        text-transform: uppercase;" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddMessage" CssClass="LabelFont" runat="server" Text="Message"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddMessage" CssClass="textbox curved" TextMode="MultiLine" MaxLength="300"
                            Style="font-size: 12px; resize: none;" Height="80px" Width="200px" ClientIDMode="Static"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="float: right;">
                <tr>
                    <td>
                        <asp:Button ID="btnOkState" runat="server" ClientIDMode="Static" CssClass="button"
                            Text="Ok" Style="font-size: 12px;" Width="60px" OnClick="btnAdd_Click" />
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnOkState"
                            ConfirmText="Are you sure you want to save?">
                        </asp:ConfirmButtonExtender>
                    </td>
                    <td>
                        <asp:Button ID="btnCancelState" runat="server" ClientIDMode="Static" CssClass="button"
                            Text="Cancel" Style="font-size: 12px;" Width="60px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HiddenField ID="AddEdit" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="State" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="Campaign" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="CampaignNameList" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="CampaignValueList" ClientIDMode="Static" runat="server" />
    </div>
</asp:Content>
