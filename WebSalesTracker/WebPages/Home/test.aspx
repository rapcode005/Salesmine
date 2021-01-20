<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WebSalesMine.WebPages.Home.test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        AsyncPostBackTimeout="3600" EnablePageMethods="true">
    </asp:ScriptManager>
    <div style="padding-left: 20px;">
        <asp:Panel ID="HeaderButton" runat="server" Height="76px" Width="1180px" class="demo">
            <table>
                <tr class="demo">
                    <td>
                        <asp:Button ID="OHbtnShowOrders" runat="server" Text="Show Orders" OnClick="OHbtnShowOrders_Click"
                            CssClass="button" />
                        &nbsp;
                        <asp:Button ID="OHbtnExportToExcel" Text="Export Excel" CssClass="button" runat="server"
                            ToolTip="Click to Export to Excel" OnClick="OHbtn_Export2ExcelClick" />
                        &nbsp;
                        <asp:Button ID="OHbtn_POLOOKUP" Text="Order/Customer PO LookUp" CssClass="button" runat="server"
                            ToolTip="Click to Export to Excel" OnClick="OHbtn_POLOOKUPClick" CausesValidation="false" />
                        &nbsp;
                        <input runat="server" id="OHBtnArrangeColumn" type="button" class="button" value="Arrange Columns"
                            onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwData','mywindow','width=700,height=400,scrollbars=yes')" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" Height="29px" Width="1126px" class="demo" Style="margin-right: 28px">
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="OHByDateUpdate" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="OHByDate" Text="" runat="server" OnCheckedChanged="OHByDate_CheckedChanged"
                                        AutoPostBack="True" />
                                    <asp:Label ID="OHstrDate" Font-Size="Small" runat="server">Start Date:</asp:Label>
                                    <asp:TextBox ID="OHtxtStartDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                    <asp:ImageButton ID="OHimgstartCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                    &nbsp;
                                    <asp:Label ID="OHEndDate" Font-Size="Small" runat="server">End Date:</asp:Label>
                                    <asp:TextBox ID="OHtxtEndDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                    <asp:ImageButton ID="OHimgEndCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                    <asp:CalendarExtender ID="OHCalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="OHtxtStartDate"
                                        PopupButtonID="OHimgstartCal">
                                    </asp:CalendarExtender>
                                    <asp:CalendarExtender ID="OHCalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="OHtxtEndDate"
                                        PopupButtonID="OHimgEndCal">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="OHByDate" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="OHUpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="OHByCal" Text="" runat="server" AutoPostBack="True" OnCheckedChanged="OHByCal_CheckedChanged" />
                                    <asp:RadioButton ID="OHrdoFiscalYear" runat="server" Font-Size="Small" Text="Fiscal Year"
                                        GroupName="RdoByCals" Checked="True" AutoPostBack="True" />&nbsp;
                                    <asp:RadioButton ID="OHrdoCalender" runat="server" Font-Size="Small" Text="Calendar"
                                        GroupName="RdoByCals" AutoPostBack="True" />&nbsp;
                                    <asp:DropDownList ID="OHddlBycal" runat="server" OnSelectedIndexChanged="OHddiYear_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="OHByCal" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelTotalOrdersSales" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="OHTotalOrders" Font-Size="Small" runat="server">Total Orders:</asp:Label><asp:TextBox
                                        ID="OHtxtbTotalOrders" runat="server" Text="0" Width="50px" ReadOnly="True"></asp:TextBox>
                                    &nbsp;
                                    <asp:Label ID="Label1" Font-Size="Small" runat="server">Total Sales:</asp:Label><asp:TextBox
                                        ID="OHtxtbTatalSales" runat="server" Text="$0.00" Width="85px" ReadOnly="True"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="OHddlBycal" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </div>
    <br />
    <table style="padding-left: 20px;">
        <tr>
            <td>
                <div class="CssErrorLabel">
                    <asp:Literal ID="OHlitErrorinGrid" runat="server"></asp:Literal></div>
            </td>
        </tr>
        <tr>
            <td>
                <%-- <center>
                    <div class="header" style="width: 1200px; height: 400px" id='table_div'>
                    </div>
                </center>--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="HeaderDiv" style="height: 50px;">
                        </div>
                        <div id="DataDiv" style="overflow: auto; width: 1200px; height: 400px;" onscroll="Onscrollfnction();">
                            <asp:GridView AutoGenerateColumns="True" ClientIDMode="Static" ID="OHgridOrderHistory"
                                AllowSorting="true" CssClass="GridViewStyle" runat="server" BackColor="#CCCCCC"
                                BorderColor="#999999" BorderStyle="Solid" ForeColor="Black" AllowPaging="false"
                                Font-Size="12px" OnSorting="OHgridOrderHistory_Sorting" OnRowDataBound="OHgridOrderHistory_RowDataBound"
                                BorderWidth="3px" CellPadding="4" CellSpacing="2">
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle BackColor="#EDEDED" Font-Size="15px" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <RowStyle CssClass="RowStyle" Wrap="false" />
                            </asp:GridView>
                        </div>
                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="OHddlBycal" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="OHbtnShowOrders" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="OHByCal" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr runat="server" id="trBlank" visible="false">
            <td style="height: 175px">
                &nbsp;
            </td>
        </tr>
        <asp:UpdatePanel ID="upOuter" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <tr>
                    <td>
                        <asp:Button Style="display: none" ID="OHbtnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                        </asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ModalPopupExtender ID="OHModalPopupExtender1" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                            BehaviorID="mpe" DynamicServicePath="" Enabled="True" PopupControlID="OHratePanel"
                            PopupDragHandleControlID="OHpnlDragable" RepositionMode="None" TargetControlID="btnHidden">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="OHratePanel" runat="server" Style="display: block" Height="0px" Width="0px"
                            meta:resourcekey="ratePanelResource1">
                            <!--  <asp:Panel runat="server" Height="50px" Width="100%" ID="OHPanel66" Visible="False">  -->
                            <table class="resRecpopUpTable_ResourceEntry">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="OHpnlDragable" runat="server" meta:resourcekey="pnlDragableResource1">
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
                                                                                                    Font-Underline="False" ForeColor="White" Width="224px" Text="Order/Customer LookUp"
                                                                                                    meta:resourcekey="LTitleResource1"></asp:Label>
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
                                                                                                <asp:RadioButton ID="OHrdoOrderNumber" runat="server" Text="Order Number" GroupName="RdoLookUp"
                                                                                                    CssClass="CssLabel" Checked="true" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="144">
                                                                                                <asp:RadioButton ID="OHrdoCustomerLookUp" runat="server" Text="Customer PO" GroupName="RdoLookUp"
                                                                                                    CssClass="CssLabel" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="OHtxtCustomerLookUp" runat="server" MaxLength="10"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="ResourceGroup" ID="reqType"
                                                                                                    ControlToValidate="OHtxtCustomerLookUp" InitialValue="Please select" ErrorMessage="Please enter the LookUp"
                                                                                                    ForeColor="Red" Display="Dynamic" SetFocusOnError="True" />
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
                                                                                                <asp:Button ID="OHbtnOk" runat="server" Text="Ok" CssClass="button" ToolTip="Save User"
                                                                                                    Width="60px" ValidationGroup="ResourceGroup" OnClick="OHbtnOk_Click1" meta:resourcekey="btnOkResource1" />
                                                                                                <asp:Button ID="OHbtnUpdate" runat="server" CssClass="button" ToolTip="Export" Text="Order/Customer Po Lookup"
                                                                                                    ValidationGroup="ResourceGroup" Visible="False" Width="60px" meta:resourcekey="btnUpdateResource1" />
                                                                                            </td>
                                                                                            <td class="addCancel_ResourceEntry">
                                                                                                <asp:Button ID="OHbtnCancel" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
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
                        </asp:Panel>
                    </td>
                </tr>
            </ContentTemplate>
        </asp:UpdatePanel>
    </table>
    </form>
</body>
</html>
