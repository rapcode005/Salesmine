<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/SalesMine.Master"
    AutoEventWireup="true" CodeBehind="NotesCommHistoryTerritory.aspx.cs" Inherits="WebSalesMine.WebPages.Notes_CommHistory.NotesCommHistoryTerritory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="AppLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MAINCONTENT" runat="server">
    <link type="text/css" href="../../App_Themes/CSS/Comman.css" rel="stylesheet" />
    <script type="text/javascript" src="../../App_Themes/JS/JqueryJS/common.js"></script>
    <script type="text/javascript" src="../../App_Themes/JS/JqueryJS/index.js"></script>
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <link type="text/css" rel="stylesheet" href="../../App_Themes/CSS/demos.css" />
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            $addHandler(document, 'keydown', onKeypress);
        }

        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                {
                    var modalPopup = $find('mpe').hide();

                }
            }
        }
        function Information() {
            alert("Successsfully Saved");
        }


        function onCalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 1000001;
        }


        function onCalendarHidden(sender, args) {
            sender._popupBehavior._element.style.zIndex = 0;
        }
    </script>
    <script type="text/javascript">



        $("#dialog-form").dialog({
            autoOpen: false,
            height: 410,
            width: 530,
            modal: true,
            show: "blind",
            hide: "explode",
            buttons: {
                "Cancel"
                            : function () {

                                $(this).dialog("close");
                            }
                            ,
                Add: function () {

                    if (document.getElementById('txtAddNote').value == "" || document.getElementById('txtNoteDate').value == "" || txtnotetype == "") {

                        window.alert("Invalid Entry");

                    }
                    else {
                        var txtaddnote = document.getElementById('txtAddNote').value;
                        var txtnotedate = document.getElementById('txtNoteDate').value;
                        var e = document.getElementById('NoteType');
                        var txtnotetype = e.options[e.selectedIndex].text;

                        PageMethods.ReturnString(txtnotedate, txtnotetype, txtaddnote);
                        window.alert("Successfully Save!");
                        $(this).dialog("close");

                    }

                }

            },


            close: function () {
                allFields.val("").removeClass("ui-state-error");
            }

        });

       
    </script>
    <div>
        <table style="width: 100%; padding-left: 20px;">
            <tr>
                <td>
                    <asp:Button Style="display: none" ID="Button1" runat="server" meta:resourcekey="BtnNoteCancelResource1">
                    </asp:Button>
                </td>
                <td>
                    <table class="demo" style="width: 100%;">
                        <tr>
                            <td colspan="2">
                                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                    DynamicServicePath="" Enabled="True" PopupControlID="addnotepanels" PopupDragHandleControlID="PnlAddNoteDrgble"
                                    RepositionMode="None" TargetControlID="Button1" CancelControlID="BtnNoteCancel"
                                    X="350" Y="250">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="addnotepanels" runat="server" Style="display: block" Height="0px"
                                    Width="0px" meta:resourcekey="ratePanelResource1">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <!--  <asp:Panel runat="server" Height="50px" Width="100%" ID="Panel67" Visible="False">  -->
                                            <table class="resRecpopUpTable_ResourceEntry">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="PnlAddNoteDrgble" runat="server" meta:resourcekey="pnlDragableResource1">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table class="resRecpuContent_ResourceEntry">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <table class="resRecpuheadingTable_ResourceEntry">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td class="resRecpuheadingCell_ResourceEntry" align="left">
                                                                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                                                                                    Font-Underline="False" ForeColor="White" Width="224px" Text="Add a note" meta:resourcekey="LTitleResource1"></asp:Label>
                                                                                                            </td>
                                                                                                            <td class="resRecpuheadTopBrdr_ResourceEntry">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="resRecpuIPFields_ResourceEntry" colspan="2" align="left">
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td align="left">
                                                                                                            Note Type :
                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <asp:DropDownList ID="NoteTypes" runat="server" Height="20px" ValidationGroup="ResourceGroup"
                                                                                                                Width="109px">
                                                                                                                <asp:ListItem>Please select</asp:ListItem>
                                                                                                                <asp:ListItem> Follow Up </asp:ListItem>
                                                                                                                <asp:ListItem> Reminder </asp:ListItem>
                                                                                                                <asp:ListItem> Other </asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="ResourceGroup" ID="reqType"
                                                                                                                ControlToValidate="NoteTypes" InitialValue="Please select" ErrorMessage="Please select the Note Type"
                                                                                                                ForeColor="Red" Display="Dynamic" SetFocusOnError="True" />
                                                                                                            <br />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr align="left">
                                                                                                        <td class="style2">
                                                                                                            Date :
                                                                                                        </td>
                                                                                                        <td align="left">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" ClientIDMode="Static">
                                                                                                                <ContentTemplate>
                                                                                                                    <div style="position: relative;">
                                                                                                                        <asp:TextBox ID="txtNoteStartDate" runat="server" Width="100px"></asp:TextBox>
                                                                                                                        &nbsp;
                                                                                                                        <asp:ImageButton ID="imgstartCalNote" ImageUrl="~/App_Themes/Images/Calender.jpg"
                                                                                                                            runat="server" />
                                                                                                                        <asp:CalendarExtender ID="NoteCalendar" runat="server" Format="MM/dd/yyyy" TargetControlID="txtNoteStartDate"
                                                                                                                            PopupButtonID="imgstartCalNote" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
                                                                                                                        </asp:CalendarExtender>
                                                                                                                        <asp:RequiredFieldValidator runat="server" ID="ReqDate" ControlToValidate="txtNoteStartDate"
                                                                                                                            ErrorMessage="Please select Note Date" ForeColor="Red" ValidationGroup="ResourceGroup"
                                                                                                                            Display="Dynamic" SetFocusOnError="True" />
                                                                                                                        <asp:RegularExpressionValidator ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                                                                                                            ID="rxpEmail" runat="server" ErrorMessage="Date should be mm/dd/yyyy format"
                                                                                                                            Text=" Date should be mm/dd/yyyy format " ForeColor="Red" Display="Dynamic" SetFocusOnError="True"
                                                                                                                            ControlToValidate="txtNoteStartDate"></asp:RegularExpressionValidator>
                                                                                                                    </div>
                                                                                                                </ContentTemplate>
                                                                                                                <Triggers>
                                                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="ByDate" EventName="CheckedChanged" />--%>
                                                                                                                </Triggers>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <br />
                                                                                                <div class="demo" style="padding: 0px 0px 0px 5px; width: 471px;">
                                                                                                    <div style="font-size: x-small; font-family: Arial, Helvetica, sans-serif; padding: 0px 0px 0px 5px;
                                                                                                        width: 452px;" align="left">
                                                                                                        Please provide relevant notes about recent customer or contact interaction.
                                                                                                    </div>
                                                                                                    <asp:TextBox TextMode="MultiLine" name="usermessage" ID="AddNote" Height="118px"
                                                                                                        Width="456px" runat="server"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="AddNote"
                                                                                                        ErrorMessage="Please enter Note" ForeColor="Red" ValidationGroup="ResourceGroup" />
                                                                                                    <br />
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td class="resRecbtnCell_ResourceEntry">
                                                                                                                <div style="font-size: x-small; font-family: Arial, Helvetica, sans-serif; padding: 0px 0px 0px 5px;
                                                                                                                    width: 452px;" align="left">
                                                                                                                    Examples :<br />
                                                                                                                    &nbsp; Dave is interested in buying xyz products but need approval from his boss
                                                                                                                    first.
                                                                                                                    <br />
                                                                                                                    &nbsp; Andy asked to be called again 1 week from now when he'll have budget approved.
                                                                                                                    <br />
                                                                                                                    &nbsp; Sarah not interested in buying from us. Company has consolidated purchases
                                                                                                                    <br />
                                                                                                                    &nbsp; and is buying from ABC Company.
                                                                                                                    <br />
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="resRecbtnCell_ResourceEntry">
                                                                                                <table class="resRecpuInputBtns_ResourceEntry">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td class="addOk_ResourceEntry">
                                                                                                                <asp:Button ID="btnAddNotes" runat="server" Text="Ok" CssClass="button" ToolTip="Save Note"
                                                                                                                    Width="60px" ValidationGroup="ResourceGroup" meta:resourcekey="btnOkResource2"
                                                                                                                    OnClick="btnAddNotes_Click" CausesValidation="true" />
                                                                                                            </td>
                                                                                                            <td class="addCancel_ResourceEntry">
                                                                                                                <asp:Button ID="BtnNoteCancel" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
                                                                                                                    Width="60px" meta:resourcekey="BtnNoteCancelResource1" ValidationGroup="ResourceGroup" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!--   </asp:Panel>-->
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </td>
                            <td style="width: 85px; height: 64px;">
                                <asp:Button ID="btnRefreshNotes" runat="server" Text="Refresh Notes" Width="85px"
                                    OnClick="btnRefreshNotes_Click" ClientIDMode="Static" CssClass="button" Height="20px" />
                            </td>
                            <td style="width: 87px; height: 64px;">
                                <asp:Button ID="btnAddNote" runat="server" Text="Add Notes" Width="85px" OnClick="btnAdfsfdNote_Click"
                                    ClientIDMode="Static" CssClass="button" Height="19px" />
                            </td>
                            <td style="width: 91px; height: 64px;">
                                <asp:Button ID="btnExportToExcel" Text="Export Excel" Width="85px" CssClass="button"
                                    runat="server" ToolTip="Click to Export to Excel" OnClick="btn_Export2ExcelClick" />
                            </td>
                            <td style="width: 434px; height: 64px;">
                                <table border="1">
                                    <tr>
                                        <td style="width: 594px">
                                            <asp:UpdatePanel ID="ByDateUpdate" runat="server">
                                                <ContentTemplate>
                                                    <div style="position: relative;">
                                                        &nbsp &nbsp &nbsp &nbsp
                                                        <asp:CheckBox ID="ByDate" Text="" runat="server" OnCheckedChanged="ByDate_CheckedChanged"
                                                            AutoPostBack="True" />
                                                        Start Date:<asp:TextBox ID="txtStartDate" runat="server" Width="100px"></asp:TextBox>
                                                        <asp:ImageButton ID="imgstartCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                                        &nbsp; End Date:<asp:TextBox ID="txtEndDate" runat="server" Width="100px"></asp:TextBox>
                                                        <asp:ImageButton ID="imgEndCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate"
                                                            PopupButtonID="imgstartCal" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden">
                                                        </asp:CalendarExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate"
                                                            PopupButtonID="imgEndCal" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 146px; height: 64px;">
                                <table border="1">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlNoteType" runat="server" Height="28px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlNoteType_SelectedIndexChanged">
                                                <asp:ListItem> All </asp:ListItem>
                                                <asp:ListItem> SAP Tickler </asp:ListItem>
                                                <asp:ListItem> Follow Up </asp:ListItem>
                                                <asp:ListItem> Master Data </asp:ListItem>
                                                <asp:ListItem> Reminder </asp:ListItem>
                                                <asp:ListItem> Disposition Notes </asp:ListItem>
                                                <asp:ListItem> Project </asp:ListItem>
                                                <asp:ListItem> Other </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50px; height: 64px;">
                                <asp:Button ID="btnAllNotes" runat="server" Text="Add Notes" Width="199px" OnClick="btnAllNotes_Click"
                                    ClientIDMode="Static" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <%--
**************
END OF HEADER
**************
    --%>
    <%--TOP TABLE--%>
    <table style="height: 25px; width: 1070px; padding-left: 20px;">
        <tr>
            <td style="height: 25px">
                <div class="GridHeaderLabel">
                    <asp:Label ID="lblNotesHistory" runat="server" Text="Notes History"></asp:Label>
                    &nbsp;
                    <input runat="server" id="BtnNotesColumn" type="button" class="button" value="Arrange Notes History Columns"
                        onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwNotesDataTer','mywindow','width=700,height=400,scrollbars=yes')" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upNotesHistory" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlNotesCommHistory" runat="server" ScrollBars="Auto" Width="1101px"
                            Height="300px">
                            <asp:GridView AutoGenerateColumns="True" ID="grdNotesHistory" CssClass="GridViewStyle"
                                runat="server" OnPageIndexChanging="NotesCommHistoryPageChanging" CellPadding="4"
                                CellSpacing="2" ForeColor="Black" EmptyDataText="No data available." AllowSorting="True"
                                AllowPaging="false" PageSize="7" Font-Size="12px" GridLines="Vertical" OnRowDataBound="grdNotesHistory_RowDataBound"
                                OnSorting="grdNotesHistory_Sorting" Visible="False" Width="211px">
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <RowStyle CssClass="RowStyle" />
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlGridIndex" runat="server" CssClass="CssLabel" Visible="False">
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlNoteType" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Separator" runat="server" Height="5px">
    </asp:Panel>
    <%--BOTTOM TABLE--%>
    <table style="height: 25px; width: 744px; padding-left: 20px;">
        <tr>
            <td style="height: 25px">
                <div class="GridHeaderLabel">
                    <asp:Label ID="Label1" runat="server" Text="Dialer Data"></asp:Label>
                    &nbsp;
                    <input runat="server" id="BtnDialerColumn" type="button" class="button" value="Arrange Dialer Data Columns"
                        onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwDialerDataTer','mywindow','width=700,height=400,scrollbars=yes')" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PnlDialerHistory" runat="server" ScrollBars="Auto" Height="300px"
                    Width="1101px">
                    <asp:UpdatePanel ID="upDialerData" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdDialerHistory" CssClass="GridViewStyle" runat="server" OnPageIndexChanging="DialerDataPageChanging"
                                CellPadding="4" CellSpacing="2" ForeColor="Black" EmptyDataText="No data available."
                                AllowPaging="false" PageSize="7" Font-Size="12px" GridLines="Vertical" AllowSorting="True"
                                OnSorting="grdDialerHistory_Sorting" Visible="False" AutoGenerateColumns="True"
                                OnRowDataBound="grdDialerHistory_RowDataBound">
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <%-- <HeaderStyle CssClass="HeaderStyle" />--%>
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <RowStyle CssClass="RowStyle" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlNoteType" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:Panel ID="Panel3" runat="server" CssClass="CssLabel" Visible="False">
                    </asp:Panel>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <%--  *************************************************************** --%>
    <table>
        <tr>
            <td>
                <asp:Button Style="display: none" ID="btnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                    BehaviorID="mpe" DynamicServicePath="" Enabled="True" PopupControlID="ratePanel"
                    PopupDragHandleControlID="pnlDragable" RepositionMode="None" TargetControlID="btnHidden"
                    X="450" Y="300">
                </asp:ModalPopupExtender>
                <asp:Panel ID="ratePanel" runat="server" Style="display: block" Height="0px" Width="0px"
                    meta:resourcekey="ratePanelResource1">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <!--  <asp:Panel runat="server" Height="50px" Width="100%" ID="Panel66" Visible="False">  -->
                            <table class="resRecpopUpTable_ResourceEntry">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlDragable" runat="server" meta:resourcekey="pnlDragableResource1">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="2">
                                                                <table class="resRecpuContent_ResourceEntry">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table class="resRecpuheadingTable_ResourceEntry">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="resRecpuheadingCell_ResourceEntry" align="left">
                                                                                                <asp:Label ID="LTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                                                                    Font-Underline="False" ForeColor="White" Width="224px" Text="Select Table" meta:resourcekey="LTitleResource1"></asp:Label>
                                                                                            </td>
                                                                                            <td class="resRecpuheadTopBrdr_ResourceEntry">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="resRecpuIPFields_ResourceEntry" colspan="2" align="left">
                                                                                <table class="resRecpuDetails_ResourceEntry">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td width="144">
                                                                                                <asp:RadioButton ID="rdoNotesHistory" runat="server" Text="Notes History" GroupName="RdoExportFile"
                                                                                                    CssClass="CssLabel" Checked="true" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="144">
                                                                                                <asp:RadioButton ID="rdoDialerData" runat="server" Text="Dialer Data" GroupName="RdoExportFile"
                                                                                                    CssClass="CssLabel" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="resRecbtnCell_ResourceEntry">
                                                                                <table class="resRecpuInputBtns_ResourceEntry">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="addOk_ResourceEntry">
                                                                                                <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="button" ToolTip="Save User"
                                                                                                    Width="60px" ValidationGroup="ResourceGroup" OnClick="btnOk_Click1" meta:resourcekey="btnOkResource1" />
                                                                                                <asp:Button ID="btnUpdate" runat="server" CssClass="button" ToolTip="Export" Text="Export the selected"
                                                                                                    ValidationGroup="ResourceGroup" Visible="False" Width="60px" meta:resourcekey="btnUpdateResource1" />
                                                                                            </td>
                                                                                            <td class="addCancel_ResourceEntry">
                                                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
                                                                                                    Width="60px" meta:resourcekey="btnCancelResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <!--   </asp:Panel>-->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="loading" style="position: absolute; width: 100%; text-align: center; top: 300px;">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/indicator_medium.gif" />
                <br />
                Please wait...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- *************************************************************** --%>
</asp:Content>
