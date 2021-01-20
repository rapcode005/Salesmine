<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Multiview.aspx.cs" Inherits="WebSalesMine.WebPages.Home.Multiview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<%@ Import Namespace="AppLogic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web SalesMIne Test</title>
      <link href="../../App_Themes/CSS/SalesMineMasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/Site.css" rel="Stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/ButtonEffect.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/PopUpModal.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../../App_Themes/CSS/JqueryCSS/jquery-ui.css" rel="stylesheet" />
    <link type="text/css" href="../../App_Themes/CSS/JqueryCSS/jquery.css" rel="stylesheet" />
    <link type="text/css" href="../../App_Themes/CSS/JqueryCSS/index.css" rel="stylesheet" />
    <link type="text/css" href="../../App_Themes/CSS/Comman.css" rel="stylesheet" />
    <link href="../../App_Themes/CSS/all.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .material_tb td
        {
            height: 24px;
        }
        .productivity_table td
        {
            border: solid 1px #CCCCCC;
            border-bottom: solid 1px #CCCCCC;
            border-left: solid 1px #CCCCCC;
        }
        .center_align
        {
            text-align: center;
        }
        .right_align
        {
            text-align: right;
        }
        .no_under
        {
            text-decoration: none;
            color: #000066;
        }
    </style>
    <!-- Order History Js -->
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            $addHandler(document, 'keydown', onKeypress);
        }

        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                {
                    var modalPopup = $find('OHmpe').hide();

                }
            }
        }

      
    </script>
    <!-- Order History ends Here -->
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        AsyncPostBackTimeout="3600" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="uppanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View0" runat="server">
                    <table class="menu_bg" cellpadding="3" cellspacing="3">
                        <tr>
                            <td align="left">
                            
                                &nbsp;<img src="../../App_Themes/Images/BD21481_.GIF" alt="brady_bullet" />&nbsp;
                                <asp:LinkButton ID="LinkButton8" CssClass="no_under" runat="server" OnClick="ShowProductSummaryyPanel">Product Summary</asp:LinkButton>
                                &nbsp;<img src="../../App_Themes/Images/BD21481_.GIF" alt="brady_bullet" />&nbsp;
                                <asp:LinkButton ID="LinkButton9" CssClass="no_under" runat="server" OnClick="ShowSCPanel">Site & Contact Info</asp:LinkButton>
                                &nbsp;<img src="../../App_Themes/Images/BD21481_.GIF" alt="brady_bullet" />&nbsp;
                                <asp:LinkButton ID="LinkButton3" CssClass="no_under" runat="server" OnClick="ShowNCHPanel">Notes & Comm History</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
            <asp:MultiView ID="MultiView2" runat="server">
                <asp:View ID="View20" runat="server">
                    Home Page
                </asp:View>
                <asp:View ID="View1" runat="server">
                    <table>
                        <tr>
                            <td>
                                <div style="padding-left: 20px;">
                                    <asp:Panel ID="HeaderButton" runat="server" Height="76px" Width="1180px">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnShowOrders" runat="server" Text="Show Orders" OnClick="btnShowOrders_Click"
                                                        CssClass="button" />
                                                    &nbsp;
                                                    <asp:Button ID="OHbtnExportToExcel" Text="Export Excel" CssClass="button" runat="server"
                                                        ToolTip="Click to Export to Excel" OnClick="OHbtn_Export2ExcelClick" />
                                                    &nbsp;
                                                    <asp:Button ID="btn_POLOOKUP" Text="Order/Customer PO LookUp" CssClass="button" runat="server"
                                                        ToolTip="Click to Export to Excel" OnClick="btn_POLOOKUPClick" CausesValidation="false" />
                                                    &nbsp;
                                                    <input runat="server" id="Button5" type="button" class="button" value="Arrange Columns"
                                                        onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwData','mywindow','width=700,height=400,scrollbars=yes')" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="OHPanel1" runat="server" Height="29px" Width="1126px" Style="margin-right: 28px">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="ByDateUpdate" runat="server">
                                                            <ContentTemplate>
                                                                <asp:CheckBox ID="ByDate" Text="" runat="server" OnCheckedChanged="ByDate_CheckedChanged"
                                                                    AutoPostBack="True" />
                                                                Start Date:
                                                                <asp:TextBox ID="txtStartDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <asp:ImageButton ID="imgstartCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                                                &nbsp; End Date:
                                                                <asp:TextBox ID="txtEndDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <asp:ImageButton ID="imgEndCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate"
                                                                    PopupButtonID="imgstartCal">
                                                                </asp:CalendarExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate"
                                                                    PopupButtonID="imgEndCal">
                                                                </asp:CalendarExtender>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ByDate" EventName="CheckedChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:CheckBox ID="ByCal" Text="" runat="server" AutoPostBack="True" OnCheckedChanged="ByCal_CheckedChanged" />
                                                                <asp:RadioButton ID="rdoFiscalYear" runat="server" Text="Fiscal Year" GroupName="RdoByCals"
                                                                    Checked="True" AutoPostBack="True" />&nbsp;
                                                                <asp:RadioButton ID="rdoCalender" runat="server" Text="Calendar" GroupName="RdoByCals"
                                                                    AutoPostBack="True" />&nbsp;
                                                                <asp:DropDownList ID="ddlBycal" runat="server" OnSelectedIndexChanged="ddiYear_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ByCal" EventName="CheckedChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanelTotalOrdersSales" runat="server">
                                                            <ContentTemplate>
                                                                Total Orders:<asp:TextBox ID="txtbTotalOrders" runat="server" Text="0" Width="50px"
                                                                    ReadOnly="True"></asp:TextBox>
                                                                &nbsp; Total Sales:<asp:TextBox ID="txtbTatalSales" runat="server" Text="$0.00" Width="85px"
                                                                    ReadOnly="True"></asp:TextBox>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlBycal" EventName="SelectedIndexChanged" />
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
                                                <asp:Literal ID="litErrorinGrid" runat="server"></asp:Literal></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView AutoGenerateColumns="True" ID="gridOrderHistory" AllowSorting="true"
                                                        CssClass="GridViewStyle" runat="server" BackColor="#CCCCCC" BorderColor="#999999"
                                                        BorderStyle="Solid" ForeColor="Black" AllowPaging="false" Font-Size="12px" OnSorting="gridOrderHistory_Sorting"
                                                        OnRowDataBound="gridOrderHistory_RowDataBound" BorderWidth="3px" CellPadding="4"
                                                        CellSpacing="2">
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
                                                    <asp:AsyncPostBackTrigger ControlID="ddlBycal" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnShowOrders" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="ByCal" EventName="CheckedChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <asp:UpdatePanel ID="upOuter" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Button Style="display: none" ID="Button6" runat="server" meta:resourcekey="btnHiddenResource1">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:ModalPopupExtender ID="OHModalPopupExtender1" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                                        BehaviorID="OHmpe" DynamicServicePath="" Enabled="True" PopupControlID="ratePanel"
                                                        PopupDragHandleControlID="pnlDragable" RepositionMode="None" TargetControlID="btnHidden">
                                                    </asp:ModalPopupExtender>
                                                    <asp:Panel ID="Panel9" runat="server" Style="display: block" Height="0px" Width="0px"
                                                        meta:resourcekey="ratePanelResource1">
                                                        <!--  <asp:Panel runat="server" Height="50px" Width="100%" ID="OHPanel66" Visible="False">  -->
                                                        <table class="resRecpopUpTable_ResourceEntry">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="Panel19" runat="server" meta:resourcekey="pnlDragableResource1">
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
                                                                                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
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
                                                                                                                            <asp:RadioButton ID="rdoOrderNumber" runat="server" Text="Order Number" GroupName="RdoLookUp"
                                                                                                                                CssClass="CssLabel" Checked="true" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td width="144">
                                                                                                                            <asp:RadioButton ID="rdoCustomerLookUp" runat="server" Text="Customer PO" GroupName="RdoLookUp"
                                                                                                                                CssClass="CssLabel" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtCustomerLookUp" runat="server" MaxLength="10"></asp:TextBox>
                                                                                                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="ResourceGroup" ID="reqType"
                                                                                                                                ControlToValidate="txtCustomerLookUp" InitialValue="Please select" ErrorMessage="Please enter the LookUp"
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
                                                                                                                            <asp:Button ID="Button7" runat="server" CssClass="button" ToolTip="Export" Text="Order/Customer Po Lookup"
                                                                                                                                ValidationGroup="ResourceGroup" Visible="False" Width="60px" meta:resourcekey="btnUpdateResource1" />
                                                                                                                        </td>
                                                                                                                        <td class="addCancel_ResourceEntry">
                                                                                                                            <asp:Button ID="Button8" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
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
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table border="1">
                        <tr>
                            <td>
                                <table style="padding-left: 20px;" width="800px">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnShowAllSkuSummary" runat="server" Text="Show All Sku Family" OnClick="BtnShowAllSkuSummary_Click"
                                                CssClass="button" />
                                            &nbsp;
                                            <asp:Button ID="btnExportToExcel" Text="Export Excel" CssClass="button" runat="server"
                                                ToolTip="Click to Export to Excel" OnClick="btn_Export2ExcelClick" />
                                            &nbsp;
                                            <%if (ArrnageColumnstring == "lvwSKUSummary")
                                              { %>
                                            <input runat="server" id="Button2" type="button" class="button" value="Arrange SKU Summary Columns"
                                                onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwSKUSummary','mywindow','width=700,height=400,scrollbars=yes')" />
                                            <% }%>
                                            <%if (ArrnageColumnstring == "lvwPCSKUSummary")
                                              { %>
                                            <input runat="server" id="Button1" type="button" class="button" value="Arrange SKU Summary Columns"
                                                onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwPCSKUSummary','mywindow','width=700,height=400,scrollbars=yes')" />
                                            <% }%>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlOterCampaign" runat="server" Visible="false">
                                            <table style="padding-left: 20px;" width="800px">
                                                <tr>
                                                    <td>
                                                        <div class="GridHeaderLabel">
                                                            Product Line Summary:
                                                        </div>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblErrorProductLineSummary" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <div>
                                                            <asp:Panel ID="Panel6" runat="server" ScrollBars="Auto" Width="1200px">
                                                                <asp:GridView ID="grdProductLineSummary" runat="server" AutoGenerateColumns="false"
                                                                    Width="1200px" EmptyDataText="No data available." PageSize="10" BorderColor="#999999"
                                                                    BorderStyle="Solid" BorderWidth="3px" AllowPaging="true" CellPadding="4" CellSpacing="1"
                                                                    Font-Size="12px" OnRowCommand="grdProductLineSummary_RowCommand" AllowSorting="true"
                                                                    OnSorting="grdProductLineSummary_Sorting" OnPageIndexChanging="grdProductLineSummaryDataPageEventHandler"
                                                                    ForeColor="Black" CssClass="GridViewStyle">
                                                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                    <PagerStyle CssClass="PagerStyle" />
                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                                    <RowStyle CssClass="RowStyle" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="View">
                                                                            <ItemTemplate>
                                                                                <div align="center">
                                                                                    <asp:LinkButton ID="lbtnShowSKU" runat="server" CommandName="ShowPLSKU" CommandArgument='<%# Eval("sku_category")%>'
                                                                                        Text="View" /></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Product Line" DataField="sku_category" SortExpression="sku_category" />
                                                                        <asp:BoundField HeaderText="F09 Sales" DataField="sales_3fy_ago" SortExpression="sales_3fy_ago"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F10 Sales" DataField="sales_2fy_ago" SortExpression="sales_2fy_ago"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F11 Sales" DataField="sales_1fy_ago" SortExpression="sales_1fy_ago"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F12 Sales" DataField="sales_currfy" SortExpression="sales_currfy"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="Lifetime sales" DataField="Total_sales" SortExpression="Total_sales"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="Lifetime Orders" DataField="NO_orders" SortExpression="NO_orders"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="Last Ordered Date" DataField="last_order_date" DataFormatString="{0:d}"
                                                                            SortExpression="last_order_date" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F09 Units" DataField="units_3fy_ago" SortExpression="units_3fy_ago"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F10 Units" DataField="units_2fy_ago" SortExpression="units_2fy_ago"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F11 Units" DataField="units_1fy_ago" SortExpression="units_1fy_ago"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F12 Units" DataField="units_currfy" SortExpression="units_currfy"
                                                                            ItemStyle-HorizontalAlign="Right" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlGridIndex" runat="server" CssClass="CssLabel" Visible="false">
                                                                <i>You are viewing page
                                                                    <%=grdProductLineSummary.PageIndex + 1%>
                                                                    of
                                                                    <%=grdProductLineSummary.PageCount%>
                                                                </i>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <table style="padding-left: 20px;" width="1100px">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div class="GridHeaderLabel">
                                                                            SKU Summary: &nbsp;
                                                                        </div>
                                                                        <asp:Label ID="Label2" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <div>
                                                            <asp:Panel ID="pnlgrdSkuSummary" runat="server" ScrollBars="Auto" Width="1200px"
                                                                Height="300px">
                                                                <asp:GridView ID="grdSkuSummary" runat="server" AutoGenerateColumns="True" Width="1200px"
                                                                    CssClass="GridViewStyle" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid"
                                                                    BorderWidth="3px" ForeColor="Black" EmptyDataText="No data available." PageSize="10"
                                                                    Font-Size="12px" AllowPaging="false" CellPadding="4" CellSpacing="2" HeaderStyle-HorizontalAlign="Center"
                                                                    OnPageIndexChanging="grdSkuSummaryDataPageEventHandler" AllowSorting="true" OnSorting="grdSkuSummary_Sorting"
                                                                    OnRowDataBound="grdSkuSummary_RowDataBound">
                                                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                    <PagerStyle CssClass="PagerStyle" />
                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel1" runat="server" CssClass="CssLabel" Visible="false">
                                                                <i>You are viewing page
                                                                    <%=grdSkuSummary.PageIndex + 1%>
                                                                    of
                                                                    <%=grdSkuSummary.PageCount%>
                                                                </i>
                                                            </asp:Panel>
                                                        </div>
                                                        <br />
                                                        <asp:Label ID="lblErrorSkuSummary" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPCCampaign" runat="server" Visible="false">
                                            <table style="padding-left: 20px;" width="800px">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div class="GridHeaderLabel">
                                                                            Product Line Summary: &nbsp;
                                                                        </div>
                                                                        <asp:Label ID="Label4" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <div>
                                                            <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" Width="1200px">
                                                                <asp:GridView ID="grdPCProductLineSummary" runat="server" AutoGenerateColumns="false"
                                                                    CssClass="GridViewStyle" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid"
                                                                    BorderWidth="3px" ForeColor="Black" Width="1200px" EmptyDataText="No data available."
                                                                    PageSize="10" AllowPaging="true" OnRowDataBound="grdPCProductLineSummary_RowDataBound"
                                                                    CellPadding="4" CellSpacing="1" Font-Size="12px" OnRowCommand="grdPCProductLineSummary_RowCommand"
                                                                    AllowSorting="true" OnSorting="grdProductLineSummary_Sorting" OnPageIndexChanging="grdPCProductLineSummaryDataPageEventHandler">
                                                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                    <PagerStyle CssClass="PagerStyle" />
                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                                    <RowStyle CssClass="RowStyle" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="View">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnShowSKU" runat="server" CommandName="ShowPLSKU" CommandArgument='<%# Eval("sku_family")%>'
                                                                                    Text="View" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Product Theme" DataField="sku_family" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="sku_family" />
                                                                        <asp:BoundField HeaderText="Last Revision Date" DataField="last_revision_date" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="last_revision_date" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField HeaderText="F09 Sales" DataField="sales_3fy_ago" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="sales_3fy_ago" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F10 Sales" DataField="sales_2fy_ago" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="sales_2fy_ago" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F11 Sales" DataField="sales_1fy_ago" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="sales_1fy_ago" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F12 Sales" DataField="sales_currfy" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="sales_currfy" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="Lifetime sales" DataField="Total_sales" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="Total_sales" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="Lifetime Orders" DataField="NO_orders" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="NO_orders" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="Last Ordered Date" DataField="last_order_date" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            DataFormatString="{0:d}" SortExpression="last_order_date" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField HeaderText="F09 Units" DataField="units_3fy_ago" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="units_3fy_ago" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F10 Units" DataField="units_2fy_ago" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="units_2fy_ago" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F11 Units" DataField="units_1fy_ago" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="units_1fy_ago" ItemStyle-HorizontalAlign="Right" />
                                                                        <asp:BoundField HeaderText="F12 Units" DataField="units_currfy" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                                            SortExpression="units_currfy" ItemStyle-HorizontalAlign="Right" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel2" runat="server" CssClass="CssLabel" Visible="false">
                                                                <i>You are viewing page
                                                                    <%=grdPCProductLineSummary.PageIndex + 1%>
                                                                    of
                                                                    <%=grdPCProductLineSummary.PageCount%>
                                                                </i>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table style="padding-left: 20px;" width="1100px">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div class="GridHeaderLabel">
                                                                            SKU Summary: &nbsp;
                                                                        </div>
                                                                        <asp:Label ID="Label6" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <div>
                                                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Width="1200px">
                                                                <asp:GridView ID="grdPCSKUSummary" runat="server" AutoGenerateColumns="True" Width="1200px"
                                                                    CssClass="GridViewStyle" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid"
                                                                    BorderWidth="3px" ForeColor="Black" OnRowDataBound="grdPCSkuSummary_RowDataBound"
                                                                    EmptyDataText="No data available." PageSize="10" AllowPaging="false" CellPadding="4"
                                                                    CellSpacing="1" Font-Size="12px" OnPageIndexChanging="grdPCSkuSummaryDataPageEventHandler"
                                                                    HeaderStyle-HorizontalAlign="Center" AllowSorting="true" OnSorting="grdPCSKUSummary_Sorting">
                                                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                    <PagerStyle CssClass="PagerStyle" />
                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                                    <RowStyle CssClass="RowStyle" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel4" runat="server" CssClass="CssLabel" Visible="false">
                                                                <i>You are viewing page
                                                                    <%=grdPCSKUSummary.PageIndex + 1%>
                                                                    of
                                                                    <%=grdPCSKUSummary.PageCount%>
                                                                </i>
                                                            </asp:Panel>
                                                        </div>
                                                        <br />
                                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <tr runat="server" id="trBlank" visible="false">
                                    <td style="height: 200px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button Style="display: none" ID="btnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <cc3:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                                BehaviorID="mpe" DynamicServicePath="" Enabled="True" PopupControlID="ratePanel"
                                                PopupDragHandleControlID="pnlDragable" RepositionMode="None" TargetControlID="btnHidden">
                                            </cc3:ModalPopupExtender>
                                            <asp:Panel ID="ratePanel" runat="server" Style="display: block" Height="0px" Width="0px"
                                                meta:resourcekey="ratePanelResource1">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
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
                                                                                                                            <asp:RadioButton ID="rdoProductSummary" runat="server" Text="ProductSummary" GroupName="RdoExportFile"
                                                                                                                                CssClass="CssLabel" Checked="true" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td width="144">
                                                                                                                            <asp:RadioButton ID="rdoSKUSummary" runat="server" Text="SKU Summary" GroupName="RdoExportFile"
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
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <table>
                        <tr>
                            <td>
                                <script language="javascript" type="text/javascript">

                                    function CalendarShown(sender, args) {
                                        sender._popupBehavior._element.style.zIndex = 10005;
                                    }

                                    function onKeypress(args) {
                                        if (args.keyCode == Sys.UI.Key.esc) {
                                            {
                                                var modalPopup = $find('mpe').hide();
                                                var modalPopup1 = $find('mpe4').hide();
                                            }
                                        }
                                    }

                                    function Information() {
                                        alert("Firstname and Lastname already exist.");
                                    }

                                    function InformationMessage(value) {
                                        alert(value);
                                    }

                                    function pageLoad() {
                                        // call the savePanelPosition when the panel is moved
                                        $addHandler(document, 'keydown', onKeypress);


                                    }



                                    function CreateGridHeader(DataDiv, gridSiteContactInfo, HeaderDiv) {
                                        var DataDivObj = document.getElementById(DataDiv);
                                        var DataGridObj = document.getElementById(gridSiteContactInfo);
                                        var HeaderDivObj = document.getElementById(HeaderDiv);
                                        //********* Creating new table which contains the header row ***********
                                        var HeadertableObj = HeaderDivObj.appendChild(document.createElement('table'));
                                        DataDivObj.style.paddingTop = '0px';
                                        var DataDivWidth = DataDivObj.clientWidth;
                                        DataDivObj.style.width = '5000px';
                                        //********** Setting the style of Header Div as per the Data Div ************
                                        HeaderDivObj.className = DataDivObj.className;
                                        HeaderDivObj.style.cssText = DataDivObj.style.cssText;
                                        //**** Making the Header Div scrollable. *****
                                        HeaderDivObj.style.overflow = 'auto';
                                        //*** Hiding the horizontal scroll bar of Header Div ****
                                        //*** this is because we have to scroll the Div along with the DataDiv.  
                                        HeaderDivObj.style.overflowX = 'hidden';
                                        //**** Hiding the vertical scroll bar of Header Div **** 
                                        HeaderDivObj.style.overflowY = 'hidden';
                                        HeaderDivObj.style.height = DataGridObj.rows[0].clientHeight + 'px';
                                        //**** Removing any border between Header Div and Data Div ****
                                        HeaderDivObj.style.borderBottomWidth = '0px';
                                        //********** Setting the style of Header Table as per the GridView ************
                                        HeadertableObj.className = DataGridObj.className;
                                        //**** Setting the Headertable css text as per the GridView css text 
                                        HeadertableObj.style.cssText = DataGridObj.style.cssText;
                                        HeadertableObj.border = '1px';
                                        HeadertableObj.rules = 'all';
                                        HeadertableObj.cellPadding = DataGridObj.cellPadding;
                                        HeadertableObj.cellSpacing = DataGridObj.cellSpacing;
                                        //********** Creating the new header row **********
                                        var Row = HeadertableObj.insertRow(0);
                                        Row.className = DataGridObj.rows[0].className;
                                        Row.style.cssText = DataGridObj.rows[0].style.cssText;
                                        Row.style.fontWeight = 'bold';
                                        //******** This loop will create each header cell *********
                                        for (var iCntr = 0; iCntr < DataGridObj.rows[0].cells.length; iCntr++) {
                                            var spanTag = Row.appendChild(document.createElement('td'));
                                            spanTag.innerHTML = DataGridObj.rows[0].cells[iCntr].innerHTML;
                                            var width = 0;
                                            //****** Setting the width of Header Cell **********
                                            if (spanTag.clientWidth > DataGridObj.rows[1].cells[iCntr].clientWidth) {
                                                width = spanTag.clientWidth;
                                            }
                                            else {
                                                width = DataGridObj.rows[1].cells[iCntr].clientWidth;
                                            }
                                            if (iCntr <= DataGridObj.rows[0].cells.length - 2) {
                                                spanTag.style.width = width + 'px';
                                            }
                                            else {
                                                spanTag.style.width = width + 20 + 'px';
                                            }
                                            DataGridObj.rows[1].cells[iCntr].style.width = width + 'px';
                                        }
                                        var tableWidth = DataGridObj.clientWidth;
                                        //********* Hidding the original header of GridView *******
                                        DataGridObj.rows[0].style.display = 'none';
                                        //********* Setting the same width of all the components **********
                                        HeaderDivObj.style.width = DataDivWidth + 'px';
                                        DataDivObj.style.width = DataDivWidth + 'px';
                                        DataGridObj.style.width = tableWidth + 'px';
                                        HeadertableObj.style.width = tableWidth + 20 + 'px';
                                        return false;
                                    }

                                    function Onscrollfnction() {
                                        var div = document.getElementById('DataDiv');
                                        var div2 = document.getElementById('HeaderDiv');
                                        //****** Scrolling HeaderDiv along with DataDiv ******
                                        div2.scrollLeft = div.scrollLeft;
                                        return false;
                                    }


                                    function BeginRequestHandler(sender, args) {

                                        if ($get('DataDiv') != null) {
                                            xPos = $get('DataDiv').scrollLeft;
                                            yPos = $get('DataDiv').scrollTop;

                                        }
                                    }

                                    function EndRequestHandler(sender, args) {

                                        if ($get('SCDataDiv') != null) {
                                            $get('SCDataDiv').scrollLeft = xPos;
                                            $get('SCDataDiv').scrollTop = yPos;
                                            CreateGridHeader('SCDataDiv', 'gridSiteContactInfo', 'HeaderDiv');
                                        }

                                    }

                                    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                                </script>
                                <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
                                </asp:ScriptManagerProxy>
                                <div align="left" style="font-size: 11px; width: 200px;">
                                    <asp:LinkButton ID="LinkButton1" OnClick="btnShowSiteContactInfo_Click" Style="text-decoration: none;"
                                        Font-Size="Medium" runat="server">
                                        <asp:Label ID="lblTitleSiteContactInfo" Font-Size="X-Large" runat="server"></asp:Label></asp:LinkButton>
                                </div>
                                <asp:Panel ID="PnlOrderHistory" runat="server">
                                    <fieldset style="width: 1024px; height: auto; font-size: 12px; vertical-align: top;">
                                        <table>
                                            <tr>
                                                <td style="vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSiteName" runat="server" Text="Firstname" AssociatedControlID="txtSiteName"
                                                                    Font-Size="Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox Font-Size="Small" ID="txtSiteName" runat="server" Width="220px" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblOrganization" Font-Size="Small" runat="server" Text="Organization"
                                                                    AssociatedControlID="txtOrganization"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOrganization" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblIndustry" Font-Size="Small" runat="server" Text="Industry" AssociatedControlID="txtIndustry"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtIndustry" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="SIC2" Font-Size="Small" runat="server" Text="SIC2" AssociatedControlID="txtSIC2"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSIC2" runat="server" ReadOnly="true" Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label31" runat="server" Text="Employee Size" AssociatedControlID="txtSiteName"
                                                                    Font-Size="Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEmployeeSize" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBuyerOrg" Font-Size="Small" runat="server" Text="Buyer Org" AssociatedControlID="txtBuyerOrg"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBuyerOrg" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblKeyAcctMng" Font-Size="Small" runat="server" Text="Key Acct Mng"
                                                                    AssociatedControlID="txtKeyAcctMng"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtKeyAcctMng" runat="server" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLifetimeSales" Font-Size="Small" runat="server" Text="Lifetime Sales"
                                                                    AssociatedControlID="txtLifetimeSales"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLifetimeSales" runat="server" Style="width: 100%; max-width: 100px;"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLast12MSales" Font-Size="Small" runat="server" Text="Last 12M Sales"
                                                                    AssociatedControlID="txtLast12MSales"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLast12MSales" runat="server" Style="width: 100%; max-width: 100px;"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLifetimeOrders" Font-Size="Small" runat="server" Text="Lifetime Orders"
                                                                    AssociatedControlID="txtLifetimeOrders"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLifetimeOrders" runat="server" Style="width: 100%; max-width: 100px;"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="vertical-align: top;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLast12MOrder" Font-Size="Small" runat="server" Text="Last 12M Orders"
                                                                    AssociatedControlID="txtLast12MOrder"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLast12MOrder" runat="server" Style="width: 100%; max-width: 100px;"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLastOrderDate" Font-Size="Small" runat="server" Text="Last Order Date"
                                                                    AssociatedControlID="txtLastOrderDate"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLastOrderDate" runat="server" Style="width: 100%; max-width: 100px;"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="margin: auto 0; float: right;">
                                            <tr>
                                                <td>
                                                    <asp:Button Style="display: none" ID="SCbtnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                                                    </asp:Button>
                                                    <asp:DragPanelExtender ID="drpeNewContact" runat="server" TargetControlID="pnlNewContact"
                                                        BehaviorID="drpeNewContact" DragHandleID="pnlDragNew">
                                                    </asp:DragPanelExtender>
                                                    <asp:ModalPopupExtender ID="SCModalPopupExtender2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                                        CancelControlID="btnCancelNew" PopupControlID="pnlNewContact" TargetControlID="SCbtnHidden">
                                                    </asp:ModalPopupExtender>
                                                    <asp:Panel ID="pnlNewContact" runat="server" Style="display: block" Height="0px"
                                                        Width="0px">
                                                        <table class="resRecpopUpTable_ResourceEntry">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlDragNew" runat="server" meta:resourcekey="pnlDragableResource1">
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
                                                                                                                            <asp:Label ID="lblAddNewContactTitle" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                                                                                Font-Size="10pt" Font-Underline="False" ForeColor="White" Width="224px" Text="Add New Contact"
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
                                                                                                            <table width="300">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <table>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <asp:Label ID="lblFirstnameNewContact" runat="server" Text="*First Name" AssociatedControlID="txtFirstnameNewContact"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:TextBox ID="txtFirstnameNewContact" runat="server"></asp:TextBox>
                                                                                                                                        <br />
                                                                                                                                        <asp:RequiredFieldValidator ValidationGroup="ResourceGroup" ID="reqFirstname" ControlToValidate="txtFirstnameNewContact"
                                                                                                                                            runat="server" ErrorMessage="Enter a Firstname!" Font-Size="8px" ForeColor="Red">
                                                                                                                                        </asp:RequiredFieldValidator>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <asp:Label ID="lblLastanameNewContact" runat="server" Text="*Last Name"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:TextBox ID="txtLastanameNewContact" runat="server"></asp:TextBox>
                                                                                                                                        <br />
                                                                                                                                        <asp:RequiredFieldValidator ValidationGroup="ResourceGroup" ID="reqLastname" ControlToValidate="txtLastanameNewContact"
                                                                                                                                            runat="server" ErrorMessage="Enter a Lastname!" Font-Size="8px" ForeColor="Red">
                                                                                                                                        </asp:RequiredFieldValidator>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <asp:Label ID="lblEmailNewContact" runat="server" Text="Email Address" Width="90px"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:TextBox ID="txtEmailNewContact" runat="server"></asp:TextBox>
                                                                                                                                        <br />
                                                                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red"
                                                                                                                                            ValidationExpression="^((?:(?:(?:\w[\.\-\+]?)*)\w)+)\@((?:(?:(?:\w[\.\-\+]?){0,62})\w)+)\.(\w{2,6})$"
                                                                                                                                            ErrorMessage="Invalid Email Format." ControlToValidate="txtEmailNewContact"></asp:RegularExpressionValidator>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <asp:Label ID="Label36" runat="server" Text="Phone"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:TextBox ID="txtPhoneNewContact" runat="server"></asp:TextBox>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <asp:Label ID="lblFunctionNewContact" runat="server" Text="*Function"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:DropDownList ID="ddlFunctionNewContact" runat="server">
                                                                                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Facilities"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Maintenance"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Office/Admin"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Owner/Pres/Senior Mgmt"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Plant/Prod/Ops"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Project Manager"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Purchasing"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Quality"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Safety"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Security"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="Shipping/Warehouse"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="HR"></asp:ListItem>
                                                                                                                                            <asp:ListItem Text="OTHER"></asp:ListItem>
                                                                                                                                        </asp:DropDownList>
                                                                                                                                        <br />
                                                                                                                                        <asp:RequiredFieldValidator ValidationGroup="ResourceGroup" ID="RequiredFieldValidator1"
                                                                                                                                            ControlToValidate="ddlFunctionNewContact" runat="server" ErrorMessage="Select a Function!"
                                                                                                                                            Font-Size="10px" ForeColor="Red">
                                                                                                                                        </asp:RequiredFieldValidator>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <asp:Label ID="lblOtherNewContact" runat="server" Text="Other Details"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:TextBox ID="txtOtherNewContact" Height="50px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
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
                                                                                                                            <asp:Button ID="btnOkNew" runat="server" Text="Ok" CssClass="button" ToolTip="Save Contact"
                                                                                                                                CausesValidation="true" Width="60px" ValidationGroup="ResourceGroup" OnClick="btnOkNew_Click"
                                                                                                                                meta:resourcekey="btnOkResource1" />
                                                                                                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnOkNew"
                                                                                                                                ConfirmText="Are you sure you want to save?">
                                                                                                                            </asp:ConfirmButtonExtender>
                                                                                                                        </td>
                                                                                                                        <td class="addCancel_ResourceEntry">
                                                                                                                            <asp:Button ID="btnCancelNew" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
                                                                                                                                CausesValidation="false" Width="60px" meta:resourcekey="btnCancelResource1" />
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
                                                    </asp:Panel>
                                                    <asp:Button ID="btnNewContact" OnClick="Show_NewContact" runat="server" Text="Add New Contact"
                                                        CausesValidation="false" CssClass="button" />
                                                </td>
                                                <td>
                                                    <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                                        PopupControlID="Panel4" BehaviorID="mpe4" PopupDragHandleControlID="Panel6" TargetControlID="hiddenButtonSubmit">
                                                    </asp:ModalPopupExtender>
                                                    <asp:Panel ID="Panel8" runat="server" Style="display: block" Height="0px" Width="0px">
                                                        <table class="resRecpopUpTable_ResourceEntry" style="width: 100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <table class="resRecpuContent_ResourceEntry">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td colspan="2">
                                                                                                        <asp:Panel ID="Panel10" runat="server" meta:resourcekey="pnlDragableResource1">
                                                                                                            <table class="resRecpuheadingTable_ResourceEntry">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td class="resRecpuheadingCell_ResourceEntry" align="left">
                                                                                                                            <asp:Label ID="Label40" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                                                                                                Font-Underline="False" ForeColor="White" Text="Master Data Change" meta:resourcekey="LTitleResource1"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td class="resRecpuheadTopBrdr_ResourceEntry">
                                                                                                                            &nbsp;
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="resRecpuIPFields_ResourceEntry" colspan="2" align="left">
                                                                                                        <table>
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <table style="float: left;">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="lblAccountNumberMasterChange" runat="server" Text="Account Number"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtAccountNumberMasterChange" Width="180px" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label41" runat="server" Text="Contact Number"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtContactNumberMasterChange" Width="180px" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                        <table style="float: right;">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label42" runat="server" Text="Account Name"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtAccountNameMasterChange" Width="180px" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label43" runat="server" Text="Contact Name"></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtContactNameMasterChange" Width="180px" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:TabContainer ID="tabMasterChange" runat="server">
                                                                                                                            <asp:TabPanel ID="tabContactPreferences" runat="server" HeaderText="Contact Preferences">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <fieldset style="height: 100%; width: 95%;">
                                                                                                                                                    <legend>Mail</legend>
                                                                                                                                                    <asp:RadioButtonList ID="rdoMail" runat="server">
                                                                                                                                                        <asp:ListItem Text="Reduce Mail attempts on Contact"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Text="Never Mail Contact"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Text="Never Mail Site"></asp:ListItem>
                                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                                </fieldset>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                &nbsp;&nbsp;&nbsp
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <fieldset style="height: 100%; width: 90%;">
                                                                                                                                                    <legend>Phone</legend>
                                                                                                                                                    <asp:RadioButtonList ID="rdoPhone" runat="server">
                                                                                                                                                        <asp:ListItem Text="Reduce Call attempts on Contact"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Text="Never Call Contact"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Text="Never Call Site"></asp:ListItem>
                                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                                </fieldset>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <fieldset style="width: 95%; height: 90%;">
                                                                                                                                                    <legend>Fax</legend>
                                                                                                                                                    <asp:RadioButtonList ID="rdoFax" runat="server">
                                                                                                                                                        <asp:ListItem Text="Never Fax Contact"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Text="Never Fax Site"></asp:ListItem>
                                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                                </fieldset>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                &nbsp;&nbsp;&nbsp
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <fieldset style="width: 90%; height: 75px;">
                                                                                                                                                    <legend>Email</legend>
                                                                                                                                                    <asp:RadioButtonList ID="rdoEmail" runat="server">
                                                                                                                                                        <asp:ListItem Text="Never Email Contact"></asp:ListItem>
                                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                                </fieldset>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <div style="float: right;">
                                                                                                                                            <tr>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    &nbsp;
                                                                                                                                                </td>
                                                                                                                                                <td style="float: right;">
                                                                                                                                                    <asp:Button ID="btnSavePreferences" runat="server" Width="70px" Text="Save" OnClick="btnSavePreferences_Click"
                                                                                                                                                        CssClass="button" />
                                                                                                                                                    <asp:Button ID="btnCancelPreferences" runat="server" Width="70px" Text="Cancel" OnClick="btnCancelPreferences_Click"
                                                                                                                                                        CssClass="button" />
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </div>
                                                                                                                                    </table>
                                                                                                                                    <asp:Panel ID="Panel11" CssClass="gridview" runat="server" Width="580px" Style="height: 100%;
                                                                                                                                        max-height: 150px;" ScrollBars="Auto">
                                                                                                                                        <asp:GridView ID="gridContactPreferences" runat="server" AllowSorting="True" OnSorting="gridPreferences_Sorting"
                                                                                                                                            AutoGenerateColumns="False">
                                                                                                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                                                                                            <PagerStyle CssClass="PagerStyle" />
                                                                                                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                                                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                                                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                                                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                                                                                                            <RowStyle CssClass="RowStyle" />
                                                                                                                                            <Columns>
                                                                                                                                                <asp:BoundField HeaderText="Account Number" DataField="Sold_to" SortExpression="Sold_to">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Created By" DataField="createdby" SortExpression="createdby">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Created Date" DataFormatString="{0:MMM dd, yyyy}" DataField="createdon"
                                                                                                                                                    SortExpression="createdon">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Contact Number" DataField="Buyerct" SortExpression="Buyerct">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Contact Name" DataField="ContactName" SortExpression="ContactName">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Mail" DataField="Mail" SortExpression="Mail">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Fax" DataField="Fax" SortExpression="Fax">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Phone" DataField="Phone" SortExpression="Phone">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                            </Columns>
                                                                                                                                        </asp:GridView>
                                                                                                                                    </asp:Panel>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:TabPanel>
                                                                                                                            <asp:TabPanel ID="tabAccountChanges" runat="server" HeaderText="Account Changes">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table style="float: left;">
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label44" runat="server" Text="Account Name"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtAccountNameChanges" Width="200px" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label45" runat="server" Text="Phone Number"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtPhoneNumberAccountChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label47" runat="server" Text="Fax Number"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtFaxNumberAccountChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label49" runat="server" Text="Address 1"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtAddress1AccountChanges" Width="200px" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label51" runat="server" Text="Address 2"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtAddress2AccountChanges" Width="200px" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                    <table>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label52" runat="server" Text="Country"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:DropDownList ID="ddlCountry" Height="100%" runat="server">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label48" runat="server" Text="State"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtStateAccountChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label50" runat="server" Text="Zip"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtZipAccountChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label46" runat="server" Text="City"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtCityAccountChanges" Width="200px" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label53" runat="server" Text="Comment"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtCommentAccountChanges" Width="200px" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                    <div style="float: right;">
                                                                                                                                        <asp:Button ID="btnSaveAccountChanges" runat="server" Width="70px" Text="Save" OnClick="SaveAccountChanges_Click"
                                                                                                                                            CssClass="button" />
                                                                                                                                        <asp:Button ID="btnCancelAccountChanges" runat="server" Width="70px" Text="Cancel"
                                                                                                                                            CssClass="button" OnClick="CancelAccountChanges_Click" />
                                                                                                                                    </div>
                                                                                                                                    <br />
                                                                                                                                    <asp:Panel ID="Panel12" CssClass="gridview" runat="server" Width="600px" Style="height: 100%;
                                                                                                                                        max-height: 150px;" ScrollBars="Auto">
                                                                                                                                        <asp:GridView ID="gridAccountChanges" runat="server" AllowSorting="true" OnSorting="gridAccountChanges_Sorting"
                                                                                                                                            AutoGenerateColumns="False">
                                                                                                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                                                                                            <PagerStyle CssClass="PagerStyle" />
                                                                                                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                                                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                                                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                                                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                                                                                                            <RowStyle CssClass="RowStyle" />
                                                                                                                                            <Columns>
                                                                                                                                                <asp:BoundField HeaderText="Account Number" DataField="Sold_to" SortExpression="Sold_to">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Created By" DataField="createdby" SortExpression="createdby">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Created Date" DataFormatString="{0:MMM dd, yyyy}" DataField="createdon"
                                                                                                                                                    SortExpression="createdon">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Account Name" DataField="AccountName" SortExpression="AccountName">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Phone" DataField="Phone" SortExpression="Phone">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Fax" DataField="Fax" SortExpression="Fax">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Address1" DataField="Address1" SortExpression="Address1">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Address2" DataField="Address2" SortExpression="Address2">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="City" DataField="City" SortExpression="City">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="State" DataField="State" SortExpression="State">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Zip" DataField="Zip" SortExpression="Zip">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Country" DataField="Country" SortExpression="Country">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Comment" DataField="Comment" SortExpression="Comment">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                            </Columns>
                                                                                                                                        </asp:GridView>
                                                                                                                                    </asp:Panel>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:TabPanel>
                                                                                                                            <asp:TabPanel ID="tabContactChanges" runat="server" HeaderText="Contact Changes">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table style="float: left;">
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label542" runat="server" Text="First Name"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtFirstnameContactChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label55" runat="server" Text="Last Name"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtLastnameContactChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label541" runat="server" Text="Phone"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtPhoneContactChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label56" runat="server" Text="Phone Extension"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtPhoneExtContactChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label58s" runat="server" Text="Department"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtDepartmentContactChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                    <table style="float: left;">
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label58a" runat="server" Text="Email Address"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtEmailAddressContactChanges" runat="server">
                                                                                                                                                </asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label54" runat="server" Text="Title"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtTitleContactChanges" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label57" runat="server" Text="Status"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:DropDownList ID="ddlStatusContactChanges" Width="210px" runat="server">
                                                                                                                                                    <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Primary Decision Maker"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Decision Influencer"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Purchasing Agent"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="No Longer there"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Duplicate Contact"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Added wrong Customer"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Deceased"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Other Contact"></asp:ListItem>
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="Label57s" runat="server" Text="Function"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:DropDownList ID="ddlFunctionContactChanges" Width="210px" runat="server">
                                                                                                                                                    <asp:ListItem Text="" Selected="True">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Facilities">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Maintenance">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Office/Admin">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Owner/Pres/Senior Mgmt">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Plant/Prod/Ops">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Project Manager">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Purchasing">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Quality">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Safety">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Security">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="Shipping/Warehouse">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="HR">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                    <asp:ListItem Text="OTHER">
                                                                                                                                                    </asp:ListItem>
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <asp:Label ID="lblCommentContactChanges" runat="server" Text="Comment"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtCommentContactChanges" Width="210px" runat="server"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                    <div style="float: right;">
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td>
                                                                                                                                                    <asp:Button ID="btnSaveContactChanges" runat="server" CssClass="button" Width="70px"
                                                                                                                                                        Text="Save" OnClick="SaveContactChanges_Click" />
                                                                                                                                                    <asp:Button ID="btnCancelContactChanges" runat="server" CssClass="button" Width="70px"
                                                                                                                                                        Text="Cancel" OnClick="CancelContactChanges_Click" />
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </div>
                                                                                                                                    <br />
                                                                                                                                    <asp:Panel ID="Panel13" CssClass="gridview" runat="server" Width="580px" Style="height: 100%;
                                                                                                                                        max-height: 150px;" ScrollBars="Auto">
                                                                                                                                        <asp:GridView ID="gridContactChanges" runat="server" AllowSorting="true" OnSorting="gridContactChanges_Sorting"
                                                                                                                                            AutoGenerateColumns="False">
                                                                                                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                                                                                            <PagerStyle CssClass="PagerStyle" />
                                                                                                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                                                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                                                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                                                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                                                                                                            <RowStyle CssClass="RowStyle" />
                                                                                                                                            <Columns>
                                                                                                                                                <asp:BoundField HeaderText="Account Number" DataField="Sold_to" SortExpression="Sold_to">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Created By" DataField="createdby" SortExpression="createdby">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Created Date" DataFormatString="{0:MMM dd, yyyy}" DataField="createdon"
                                                                                                                                                    SortExpression="createdon">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Contact Number" DataField="Buyerct" SortExpression="Buyerct">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="First Name" DataField="firstname" SortExpression="firstname">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Last Name" DataField="lastname" SortExpression="lastname">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Function" DataField="Function" SortExpression="Function">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Title" DataField="Title" SortExpression="Title">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Phone" DataField="Phone" SortExpression="Phone">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Phone Extension" DataField="PhoneExt" SortExpression="PhoneExt">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Department" DataField="Department" SortExpression="Department">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Email Address" DataField="Email_Address" SortExpression="Email_Address">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                                <asp:BoundField HeaderText="Comment" DataField="comment" SortExpression="comment">
                                                                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                                </asp:BoundField>
                                                                                                                                            </Columns>
                                                                                                                                        </asp:GridView>
                                                                                                                                    </asp:Panel>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:TabPanel>
                                                                                                                        </asp:TabContainer>
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
                                                                                                                        <asp:Button ID="Button4" runat="server" Text="Ok" CssClass="button" ToolTip="Save Contact"
                                                                                                                            CausesValidation="true" Width="60px" Visible="false" ValidationGroup="ResourceGroup"
                                                                                                                            OnClick="btnOkNew_Click" meta:resourcekey="btnOkResource1" />
                                                                                                                    </td>
                                                                                                                    <td class="addCancel_ResourceEntry">
                                                                                                                        <asp:Button ID="btnCancelMasterDataChange" Visible="false" runat="server" Text="Cancel"
                                                                                                                            CssClass="button" ToolTip="Cancel" Width="60px" meta:resourcekey="btnCancelResource1" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Button Style="display: none" ID="hiddenButtonSubmit" runat="server" />
                                                    <%-- <input type="button" value="Maste Data Change" onclick="window.open('../SiteAndContactInfo/SubmitMasterDataChange.aspx','mywindow','width=700,height=400,scrollbars=yes')" />--%>
                                                    &nbsp;&nbsp;<asp:Button ID="btnSubmitMaster" runat="server" Text="Master Data Change"
                                                        OnClick="SubmitMaster_Click" CausesValidation="false" CssClass="button" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="cpeSiteContactInfo" TargetControlID="PnlOrderHistory"
                                    runat="server">
                                </asp:CollapsiblePanelExtender>
                                <asp:UpdatePanel ID="UpdateSiteContactInfo" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div align="left" style="font-size: 11px; width: 200px;">
                                            <asp:LinkButton ID="LinkButton2" OnClick="btnTitleContactInfo_Click" Style="text-decoration: none;"
                                                Font-Size="Medium" runat="server">
                                                <asp:Label ID="lblTitleContactInfo" Font-Size="X-Large" runat="server"></asp:Label></asp:LinkButton>
                                        </div>
                                        <asp:CollapsiblePanelExtender ID="cpeTabsSiteContact" TargetControlID="PnlTabContactInfo"
                                            runat="server">
                                        </asp:CollapsiblePanelExtender>
                                        <asp:Panel ID="PnlTabContactInfo" runat="server">
                                            <div align="left" style="width: 100%; max-width: 1024px; height: 100%; font-size: small;">
                                                <asp:TabContainer ID="tbconSiteContactInfo" runat="server" AutoPostBack="False">
                                                    <asp:TabPanel ID="tbContactInfo" runat="server" HeaderText="Contact Info">
                                                        <ContentTemplate>
                                                            <table style="float: left;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblFirstname" runat="server" Text="Firstname" AssociatedControlID="txtFirstname"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFirstname" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblLastname" runat="server" Text="Lastname" AssociatedControlID="txtLastname"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLastname" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblContactType" runat="server" Text="Contact Type" AssociatedControlID="txtContactType"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtContactType" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" Text="Status" AssociatedControlID="txtStatus"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtStatus" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblRecency" runat="server" Text="Recency(Month)" AssociatedControlID="txtRecency"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRecency" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="float: left;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDoNotMail" runat="server" Text="Do not Mail" AssociatedControlID="txtDoNotMail"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDoNotMail" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDoNotFax" runat="server" Text="Do not Fax" AssociatedControlID="txtDoNotFax"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDoNotFax" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDoNotEmail" runat="server" Text="Do not Email" AssociatedControlID="txtDoNotEmail"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDoNotEmail" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDoNotCall" runat="server" Text="Do not Call" AssociatedControlID="txtDoNoCall"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDoNoCall" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="float: left;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblLifetimeSales2" runat="server" Text="Lifetime Sales" AssociatedControlID="txtLifetimeSales2"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLifetimeSales2" runat="server" Style="width: 100%; max-width: 100px;"
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblLast12MSalesContact" runat="server" Text="Last 12M Sales" AssociatedControlID="txtLast12MSalesContact"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLast12MSalesContact" Style="width: 100%; max-width: 100px;" runat="server"
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblLifetimeOrders2" runat="server" Text="Lifetime Orders" AssociatedControlID="txtLifetimeOrders2"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLifetimeOrders2" Style="width: 100%; max-width: 100px;" runat="server"
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblLast12MOrderContact" runat="server" Text="Last 12M Orders" AssociatedControlID="txtLast12MOrderContact"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLast12MOrderContact" Style="width: 100%; max-width: 100px;" runat="server"
                                                                            ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblLastOrderDateContact" runat="server" Text="Last Order Date" AssociatedControlID="txtLastOrderDateContact"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLastOrderDateContact" Style="width: 100%; max-width: 100px;"
                                                                            runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table style="float: left;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblDepartment" runat="server" Text="Department" AssociatedControlID="txtDepartment"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblfunction" runat="server" Text="Function" AssociatedControlID="txtfunction"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtfunction" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblTitle" runat="server" Text="Title" AssociatedControlID="txtTitle"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtTitle" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblDirectPhone" runat="server" Text="Direct Phone" AssociatedControlID="txtDirectPhone"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDirectPhone" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblSitePhone" runat="server" Text="Site Phone" AssociatedControlID="txtSitePhone"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtSitePhone" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblEmailContact" runat="server" Text="Email" AssociatedControlID="txtEmailContact"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtEmailContact" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabQualifyingQuestion" runat="server" HeaderText="Qualifying Question">
                                                        <ContentTemplate>
                                                            <table style="float: left;">
                                                                <tr>
                                                                    <td>
                                                                        Key Attributes
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblContactStatusQQ" runat="server" AssociatedControlID="ddlContactStatus"
                                                                            Text="Contact Status?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlContactStatus" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Primary Decision Maker"></asp:ListItem>
                                                                            <asp:ListItem Text="Decision Influencer"></asp:ListItem>
                                                                            <asp:ListItem Text="Purchasing Agent"></asp:ListItem>
                                                                            <asp:ListItem Text="No Longer there"></asp:ListItem>
                                                                            <asp:ListItem Text="Duplicate Contact"></asp:ListItem>
                                                                            <asp:ListItem Text="Added wrong Customer"></asp:ListItem>
                                                                            <asp:ListItem Text="Deceased"></asp:ListItem>
                                                                            <asp:ListItem Text="Other Contact"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Requisitions Safety Products?
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSp" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Yes"></asp:ListItem>
                                                                            <asp:ListItem Text="No"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblProPurResp" runat="server" Text="What types of items are you responsible for purchasing?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="hyperProduct" Text="Product Purchase Responsibility" OnClick="hyperProduct_Click"
                                                                            runat="server"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblOtherVendors" runat="server" Text="Other Vendors?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="hyperOtherVendors" Text="Other Vendors" OnClick="HyperOthers_Click"
                                                                            runat="server"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblProjects" runat="server" Text="What projects are you working on?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="hyperProjects" runat="server" OnClick="hyperProjects_Click" Text="Projects"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <div>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblUpdatedBy" runat="server" Text="Last Updated By:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblUpdatedByWho" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblUpdatedDate" runat="server" Text="Last Updated Date:"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblUpdatedDateWhen" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="btnUpdateQQ" CssClass="button" runat="server" Text="Update" OnClick="UpdateQQ" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btnSaveQQ" CssClass="button" runat="server" Text="Save" OnClick="SaveQQ" />
                                                                            &nbsp;
                                                                            <asp:Button ID="btnCancelQQ" CssClass="button" runat="server" Text="Cancel" OnClick="CancelQQ" />
                                                                        </td>
                                                                    </tr>
                                                                </div>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Primary Question
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblContFunc" runat="server" AssociatedControlID="ddlContFunc" Text="What is your area of responsibility?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlContFunc" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Facilities"></asp:ListItem>
                                                                            <asp:ListItem Text="Maintenance"></asp:ListItem>
                                                                            <asp:ListItem Text="Office/Admin"></asp:ListItem>
                                                                            <asp:ListItem Text="Owner/Pres/Senior Mgmt"></asp:ListItem>
                                                                            <asp:ListItem Text="Plant/Prod/Ops"></asp:ListItem>
                                                                            <asp:ListItem Text="Project Manager"></asp:ListItem>
                                                                            <asp:ListItem Text="Purchasing"></asp:ListItem>
                                                                            <asp:ListItem Text="Quality"></asp:ListItem>
                                                                            <asp:ListItem Text="Safety"></asp:ListItem>
                                                                            <asp:ListItem Text="Security"></asp:ListItem>
                                                                            <asp:ListItem Text="Shipping/Warehouse"></asp:ListItem>
                                                                            <asp:ListItem Text="HR"></asp:ListItem>
                                                                            <asp:ListItem Text="OTHER"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSpVendor" runat="server" Text="Are we your primary safety products vendor?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSpVendor" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Yes"></asp:ListItem>
                                                                            <asp:ListItem Text="No"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblFactor" runat="server" Text="What is the most important factor when selecting a vendor?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList OnSelectedIndexChanged="ddlFactor_SelectedChanged" AutoPostBack="true"
                                                                            ID="ddlFactor" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Price"></asp:ListItem>
                                                                            <asp:ListItem Text="Delivery"></asp:ListItem>
                                                                            <asp:ListItem Text="Quality"></asp:ListItem>
                                                                            <asp:ListItem Text="Service"></asp:ListItem>
                                                                            <asp:ListItem Text="Other"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtOther" Visible="false" MaxLength="300" Height="100" Width="200"
                                                                            TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblContBudget" runat="server" Text="What is your annual spend of safety products?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlContBudget" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Less than 1K"></asp:ListItem>
                                                                            <asp:ListItem Text="Between 1 & 10K"></asp:ListItem>
                                                                            <asp:ListItem Text="Between 10 & 100K"></asp:ListItem>
                                                                            <asp:ListItem Text="More than 100K"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblPurchasing" runat="server" Text="Purchasing frequency in general for safety products?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlPurchasing" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Weekly"></asp:ListItem>
                                                                            <asp:ListItem Text="Monthly"></asp:ListItem>
                                                                            <asp:ListItem Text="Quarterly"></asp:ListItem>
                                                                            <asp:ListItem Text="Annually"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblview" runat="server" Text="Who also requisitions safety related products?"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button Style="display: none" ID="btnHiddenSafe" runat="server" meta:resourcekey="btnHiddenResource1">
                                                                        </asp:Button>
                                                                        <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                                                            CancelControlID="btnCancelView" DropShadow="true" PopupControlID="pnlView" TargetControlID="btnHiddenSafe">
                                                                        </asp:ModalPopupExtender>
                                                                        <asp:UpdatePanel ID="updateView" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DragPanelExtender ID="drpeSafety" runat="server" TargetControlID="pnlView" BehaviorID="drpeSafety"
                                                                                    DragHandleID="Panel5">
                                                                                </asp:DragPanelExtender>
                                                                                <asp:Panel ID="pnlView" runat="server" Style="display: block" Height="0px" Width="0px">
                                                                                    <table class="resRecpopUpTable_ResourceEntry">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table>
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td colspan="2">
                                                                                                                    <table class="resRecpuContent_ResourceEntry">
                                                                                                                        <tbody>
                                                                                                                            <tr>
                                                                                                                                <td colspan="2">
                                                                                                                                    <asp:Panel ID="Panel14" runat="server" meta:resourcekey="pnlDragableResource1">
                                                                                                                                        <table class="resRecpuheadingTable_ResourceEntry">
                                                                                                                                            <tbody>
                                                                                                                                                <tr>
                                                                                                                                                    <td class="resRecpuheadingCell_ResourceEntry" align="left">
                                                                                                                                                        <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                                                                                                                            Font-Underline="False" ForeColor="White" Width="224px" Text="Update Safety Products"
                                                                                                                                                            meta:resourcekey="LTitleResource1"></asp:Label>
                                                                                                                                                    </td>
                                                                                                                                                    <td class="resRecpuheadTopBrdr_ResourceEntry">
                                                                                                                                                        &nbsp;
                                                                                                                                                    </td>
                                                                                                                                                </tr>
                                                                                                                                            </tbody>
                                                                                                                                        </table>
                                                                                                                                    </asp:Panel>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td colspan="2" align="left">
                                                                                                                                    <table>
                                                                                                                                        <tbody>
                                                                                                                                            <tr>
                                                                                                                                                <td>
                                                                                                                                                    <table>
                                                                                                                                                        <tr>
                                                                                                                                                            <td class="resRecpuIPFields_ResourceEntry">
                                                                                                                                                                <asp:RadioButton ID="rdoContactFirstname" runat="server" AutoPostBack="true" OnCheckedChanged="rdoContactFirstname_Checked"
                                                                                                                                                                    Text="Firstname" GroupName="rdoFilterContact" />
                                                                                                                                                                <asp:RadioButton ID="rdoContactSurname" runat="server" AutoPostBack="true" OnCheckedChanged="rdoContactSurname_Checked"
                                                                                                                                                                    Text="Surname" GroupName="rdoFilterContact" />
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td class="resRecpuIPFields_ResourceEntry">
                                                                                                                                                                <asp:TextBox ID="txtSearchContactSafetyProducts" runat="server"></asp:TextBox>
                                                                                                                                                                <asp:AutoCompleteExtender BehaviorID="txtSearchContactSafetyProducts_AutoCompleteExtender"
                                                                                                                                                                    ID="txtSearchContact_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                                                                                                                                    MinimumPrefixLength="1" Enabled="True" ServicePath="" TargetControlID="txtSearchContactSafetyProducts"
                                                                                                                                                                    ServiceMethod="GetCompletionList" UseContextKey="True">
                                                                                                                                                                </asp:AutoCompleteExtender>
                                                                                                                                                                <asp:Button ID="btnSearchContactSafetyProducts" OnClick="Search_Product" runat="server"
                                                                                                                                                                    Text="Search" />
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                        <tr>
                                                                                                                                                            <td class="resRecpuIPFields_ResourceEntry">
                                                                                                                                                                <asp:Panel ID="pnlSafety" CssClass="gridview" runat="server" Width="550px" Height="200px"
                                                                                                                                                                    ScrollBars="Auto">
                                                                                                                                                                    <asp:GridView ID="gridSafetyProducts" runat="server" AutoGenerateColumns="false"
                                                                                                                                                                        OnRowUpdating="gridSafetyProducts_RowUpdating" OnRowCancelingEdit="gridSafetyProducts_RowCancelingEdit"
                                                                                                                                                                        OnRowEditing="gridSafetyProducts_RowEditing" OnRowDataBound="gridSafetyProducts_RowDataBound"
                                                                                                                                                                        Width="100%">
                                                                                                                                                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                                                                                                                        <PagerStyle CssClass="PagerStyle" />
                                                                                                                                                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                                                                                                        <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                                                                                                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                                                                                                                        <EditRowStyle CssClass="EditRowStyle" />
                                                                                                                                                                        <RowStyle CssClass="RowStyle" />
                                                                                                                                                                        <Columns>
                                                                                                                                                                            <asp:CommandField ShowEditButton="True" />
                                                                                                                                                                            <asp:TemplateField HeaderText="Contact Number" InsertVisible="False" SortExpression="CONTACT">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:Label ID="lblContactSafety" runat="server" Text='<%# Eval("[CONTACT NUMBER]") %>'></asp:Label>
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                            <asp:TemplateField HeaderText="Firstname" InsertVisible="False" SortExpression="First Name">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:Label ID="lblFirstNameSafety" runat="server" Text='<%# Eval("[First Name]") %>'></asp:Label>
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                            <%--<asp:BoundField HeaderText="Firstname" DataField="First Name" HeaderStyle-VerticalAlign="Middle"
                                                                                                                                                HeaderStyle-Wrap="false" SortExpression="First Name">
                                                                                                                                                <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>--%>
                                                                                                                                                                            <asp:TemplateField HeaderText="Lastname" InsertVisible="False" SortExpression="First Name">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <asp:Label ID="lblSurNameSafety" runat="server" Text='<%# Eval("[Last Name]") %>'></asp:Label>
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                            <%-- <asp:BoundField HeaderText="Surname" DataField="Last Name" HeaderStyle-VerticalAlign="Middle"
                                                                                                                                                HeaderStyle-Wrap="false" SortExpression="Last Name">
                                                                                                                                                <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                                                            </asp:BoundField>--%>
                                                                                                                                                                            <asp:TemplateField HeaderText="Tag" InsertVisible="False" SortExpression="SP">
                                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                                    <%--    <asp:DropDownList ID="ddlSafetyProductsWhoElses" runat="server" >
                                                                                                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                                                                                                                    </asp:DropDownList>--%>
                                                                                                                                                                                    <asp:Label ID="Label39" runat="server" Text='<%# Eval("SP") %>'></asp:Label>
                                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                                <EditItemTemplate>
                                                                                                                                                                                    <asp:DropDownList ID="ddlSafetyProductsWhoElse" runat="server">
                                                                                                                                                                                        <asp:ListItem Text=""></asp:ListItem>
                                                                                                                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                                                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                                                </EditItemTemplate>
                                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                                        </Columns>
                                                                                                                                                                    </asp:GridView>
                                                                                                                                                                </asp:Panel>
                                                                                                                                                            </td>
                                                                                                                                                        </tr>
                                                                                                                                                    </table>
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
                                                                                                                                                    <asp:Button ID="btnOkView" runat="server" Text="Ok" CssClass="button" ToolTip="Save User"
                                                                                                                                                        Width="60px" meta:resourcekey="btnOkResource1" />
                                                                                                                                                </td>
                                                                                                                                                <td class="addCancel_ResourceEntry">
                                                                                                                                                    <asp:Button ID="btnCancelView" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
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
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="gridSafetyProducts" EventName="SelectedIndexChanged" />
                                                                                <asp:AsyncPostBackTrigger ControlID="txtSearchContactSafetyProducts" EventName="TextChanged" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        <asp:Button ID="btnView" OnClick="View_Click" runat="server" Text="..." />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabPCQQ" runat="server" HeaderText="Qualifying Question">
                                                        <ContentTemplate>
                                                            <asp:Table ID="PCQQ" runat="server">
                                                                <asp:TableRow>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="Emp" runat="server" Text="Offers Health Insurance to Employees?"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:DropDownList ID="ddlHealth" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Yes"></asp:ListItem>
                                                                            <asp:ListItem Text="No"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblContactStatusPC" runat="server" Text="New Contact Status"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:DropDownList ID="ddlNewContactStatus" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Primary Decision Maker"></asp:ListItem>
                                                                            <asp:ListItem Text="Decision Influencer"></asp:ListItem>
                                                                            <asp:ListItem Text="Purchasing Agent"></asp:ListItem>
                                                                            <asp:ListItem Text="No Longer there"></asp:ListItem>
                                                                            <asp:ListItem Text="Duplicate Contact"></asp:ListItem>
                                                                            <asp:ListItem Text="Added wrong Customer"></asp:ListItem>
                                                                            <asp:ListItem Text="Other Contact"></asp:ListItem>
                                                                            <asp:ListItem Text="Deceased"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblUpdatedbyPC" runat="server" Text="Last Updated by"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblUpdatedbyPCWho" runat="server"></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblSpanish" runat="server" Text="Has Spanish Employees?"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:DropDownList ID="ddlSpanish" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Yes"></asp:ListItem>
                                                                            <asp:ListItem Text="No"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblJobArea" runat="server" Text="Job Area"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:DropDownList ID="ddlJobArea" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Facilities"></asp:ListItem>
                                                                            <asp:ListItem Text="Maintenance"></asp:ListItem>
                                                                            <asp:ListItem Text="Office/Admin"></asp:ListItem>
                                                                            <asp:ListItem Text="Owner/Pres/Senior Mgmt"></asp:ListItem>
                                                                            <asp:ListItem Text="Plant/Prod/Ops"></asp:ListItem>
                                                                            <asp:ListItem Text="Project Manager"></asp:ListItem>
                                                                            <asp:ListItem Text="Purchasing"></asp:ListItem>
                                                                            <asp:ListItem Text="Quality"></asp:ListItem>
                                                                            <asp:ListItem Text="Security"></asp:ListItem>
                                                                            <asp:ListItem Text="Shipping/Warehouse"></asp:ListItem>
                                                                            <asp:ListItem Text="HR"></asp:ListItem>
                                                                            <asp:ListItem Text="OTHER"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblDate" runat="server" Text="Last Updated Date"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblDateWho" runat="server"></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblEmployeeSize2" runat="server" Text="Employee Size"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:DropDownList ID="ddlEmployeeSize" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="A: 1-19"></asp:ListItem>
                                                                            <asp:ListItem Text="B: 20-49"></asp:ListItem>
                                                                            <asp:ListItem Text="C: 50-99"></asp:ListItem>
                                                                            <asp:ListItem Text="D: 100-249"></asp:ListItem>
                                                                            <asp:ListItem Text="E: 250-499"></asp:ListItem>
                                                                            <asp:ListItem Text="F: 500+"></asp:ListItem>
                                                                            <asp:ListItem Text="G: Unknown"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:Label ID="lblAnnual" runat="server" Text="Annual Spend"></asp:Label>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:DropDownList ID="ddlAnnual" runat="server">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Less than 1K"></asp:ListItem>
                                                                            <asp:ListItem Text="Between 1 & 10K"></asp:ListItem>
                                                                            <asp:ListItem Text="Between 10 & 100K"></asp:ListItem>
                                                                            <asp:ListItem Text="More than 100K"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:Button ID="btnUpdatePC" CssClass="button" runat="server" Text="Update" OnClick="UpdatePC" />
                                                                        <asp:Button ID="btnSavePC" CssClass="button" runat="server" Text="Save" OnClick="SavePC" />
                                                                        <asp:Button ID="btnCancelPC" CssClass="button" runat="server" Text="Cancel" OnClick="CancelPC" />
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabSecondQualifying" runat="server" HeaderText="Second Qualifying Question">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Table ID="Table2" runat="server">
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="SCLabel17" runat="server" Text="What is the buying process? (how many people are involved)"></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:TextBox ID="txtQ12QQ" runat="server" Width="200px"></asp:TextBox>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="SCLabel18" runat="server" Text="What are you doing to improve productivity?"></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:TextBox ID="txtQ22QQ" runat="server" Width="200px"></asp:TextBox>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="SCLabel19" runat="server" Text="What is your risk strategy?"></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:TextBox ID="txtQ32QQ" runat="server" Width="200px"></asp:TextBox>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="SCLabel20" runat="server" Text="Which departments are purchasing 
                                    safety products other than yours?"></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:TextBox ID="txtQ42QQ" runat="server" Width="200px"></asp:TextBox>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="SCLabel21" runat="server" Text="What are you doing to reduce costs?"></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:TextBox ID="txtQ52QQ" runat="server" Width="200px"></asp:TextBox>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                        </asp:Table>
                                                                    </td>
                                                                    <td>
                                                                        <div>
                                                                            <asp:Table ID="Table3" runat="server">
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell>
                                                                                        <asp:Button ID="btnAddSecond" runat="server" CssClass="button" Width="70px" Text="Add"
                                                                                            OnClick="btnAddSecond_Click" />
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                                <asp:TableRow>
                                                                                    <asp:TableCell>
                                                                                        <asp:Button ID="btnClearSecond" runat="server" CssClass="button" Text="Clear" Width="70px"
                                                                                            OnClick="btnClear_Click" />
                                                                                    </asp:TableCell>
                                                                                </asp:TableRow>
                                                                            </asp:Table>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblContactNameSecond" runat="server" Text="Contact Name"></asp:Label>
                                                                                    <asp:TextBox ID="txtContactNameSecond" ReadOnly="true" runat="server" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Panel ID="pnlSecondQQ" runat="server" CssClass="gridview" Width="600px" Height="200px"
                                                                                        ScrollBars="Auto">
                                                                                        <%-- <div class="container" style="height: 200px; width: 600px;">--%>
                                                                                        <asp:GridView ID="gridSecondQQ" runat="server" AllowSorting="true" OnSorting="gridSecondaryQ_Sorting"
                                                                                            AutoGenerateColumns="false">
                                                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                                            <PagerStyle CssClass="PagerStyle" />
                                                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                                                            <RowStyle CssClass="RowStyle" />
                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="Account" DataField="Sold_to" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="Sold_to">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Contact Name" DataField="Name" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="Name">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Question 1" DataField="Q1" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="Q1">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Question 2" DataField="Q2" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="Q2">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Question 3" DataField="Q3" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="Q3">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Question 4" DataField="Q4" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="Q4">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Question 5" DataField="Q5" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="Q5">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Created on" DataField="createdon" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="createdon">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField HeaderText="Created by" DataField="createdby" HeaderStyle-VerticalAlign="Middle"
                                                                                                    HeaderStyle-Wrap="false" SortExpression="createdby">
                                                                                                    <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                        <%--        </div>--%>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabProductPurchase" runat="server" HeaderText="Product Purchase Responsibility">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Table runat="server" ID="tableProductPurchase">
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="lblProductPurchase" runat="server" Text="Product Purchase Responsibility"></asp:Label>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="lblFacility" runat="server" Text="Facility and Site Maintenance"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlFacility" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="lblPPE" runat="server" Text="PPE"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlPPE" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label3" runat="server" Text="Spill Control"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlSpillControl" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label5" runat="server" Text="First Aide and Eyewash"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlFirst" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label8" runat="server" Text="Property ID"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlPropertyID" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label9" runat="server" Text="Tags"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlTags" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label10" runat="server" Text="Labels"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlLabels" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label11" runat="server" Text="Safety and Facility Signs"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlSafety" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label12" runat="server" Text="Traffic and Parking Signs"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlTraffic" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label13" runat="server" Text="Lockout"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlLockout" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label14" runat="server" Text="Safety Products"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlSafetyProducts" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label15" runat="server" Text="Traffic Control"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlTrafficControl" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label16" runat="server" Text="Office Signs"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlOffice" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label17" runat="server" Text="Seals"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlSeals" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label18" runat="server" Text="Warehouse"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlWarehouse" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                            <%--  --%>
                                                                            <asp:TableRow>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label19" runat="server" Text="Pipe and Valve Marking"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlPipe" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label20" runat="server" Text="Security Products"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlSecurity" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                    &nbsp;&nbsp;&nbsp;
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:Label ID="Label21" runat="server" Text="ETO / Custom"></asp:Label>
                                                                                </asp:TableCell>
                                                                                <asp:TableCell>
                                                                                    <asp:DropDownList ID="ddlETO" runat="server">
                                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                        <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                        <asp:ListItem Text="No"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </asp:TableCell>
                                                                            </asp:TableRow>
                                                                        </asp:Table>
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblLastUpdateProducts" runat="server" Text="Last Updated By"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblUpdatedByProductsWho" runat="server"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblLastUpdateDateProducts" runat="server" Text="Last Updated Date"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblLastUpdateDateProductsWhen" runat="server"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Button ID="btnUpdateProducts" runat="server" Text="Update" OnClick="UpdateProducts" />
                                                                                    <asp:Button ID="btnSaveProducts" runat="server" Text="Save" OnClick="SaveProducts" />
                                                                                    <asp:Button ID="btnCancelProducts" runat="server" Text="Cancel" OnClick="CancelProducts" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="tabProjectsOthers" runat="server" HeaderText="Other Vendors and Project">
                                                        <ContentTemplate>
                                                            <asp:Table ID="Table1" runat="server">
                                                                <asp:TableRow>
                                                                    <asp:TableCell>
                                                                        <asp:ModalPopupExtender ID="modalOther" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                                                            BehaviorID="mpe" CancelControlID="btnCancelOther" PopupControlID="OthersPanel"
                                                                            PopupDragHandleControlID="pnlDragable" TargetControlID="btnOtherVendors">
                                                                        </asp:ModalPopupExtender>
                                                                        <asp:Panel ID="OthersPanel" runat="server" Style="display: block" Height="0px" Width="0px">
                                                                            <table class="resRecpopUpTable_ResourceEntry">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Panel ID="Panel15" runat="server" meta:resourcekey="pnlDragableResource1">
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
                                                                                                                                                <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                                                                                                                    Font-Underline="False" ForeColor="White" Width="224px" Text="Add Other Vendors"
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
                                                                                                                                <table>
                                                                                                                                    <tbody>
                                                                                                                                        <tr>
                                                                                                                                            <td width="144">
                                                                                                                                                <asp:Table ID="Table111" runat="server">
                                                                                                                                                    <asp:TableRow>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:Label ID="Label23" runat="server" Text="Contact Name"></asp:Label>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:TextBox ID="txtContactNameOther" runat="server"></asp:TextBox>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                    <asp:TableRow>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:Label ID="Label24" runat="server" Text="Vendor Name"></asp:Label>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:TextBox ID="txtVendorName" runat="server"></asp:TextBox>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                    <asp:TableRow>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:Label ID="Label25" runat="server" Text="Comments"></asp:Label>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:TextBox ID="txtCommentOther" Height="50px" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                </asp:Table>
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
                                                                                                                                            <td>
                                                                                                                                                <%-- <asp:Button ID="btnOkOther" runat="server" Text="Ok" CssClass="button" ToolTip="Save Other Vendor"
                                                                                                                Width="60px" ValidationGroup="ResourceGroup" OnClick="btnOkOther_Click" />--%>
                                                                                                                                                <asp:Button ID="btnOkOther" OnClick="btnOkOther_Click" runat="server" CssClass="button"
                                                                                                                                                    ToolTip="Save Other Vendor" Text="OK" Width="60px" />
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:Button ID="btnClear" OnClick="btnClearOther_Click" runat="server" CssClass="button"
                                                                                                                                                    Text="Clear" Width="60px" />
                                                                                                                                            </td>
                                                                                                                                            <td class="addCancel_ResourceEntry">
                                                                                                                                                <asp:Button ID="btnCancelOther" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
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
                                                                        </asp:Panel>
                                                                        <asp:Button ID="btnOtherVendors" runat="server" Text="Add Other Vendors" CssClass="button" />
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                                                            PopupControlID="ratePanel" PopupDragHandleControlID="pnlDrag" TargetControlID="btnProjects">
                                                                        </asp:ModalPopupExtender>
                                                                        <%--    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                                    TargetControlID="InvisButton" PopupControlID="ResourceGalleryPanel" 
                                    BackgroundCssClass="modalProgressGreyBackground" DropShadow="true" 
                                    OkControlID="btnMFinish" CancelControlID="btnMClose" />--%>
                                                                        <%--<asp:Panel ID="ratePanel" runat="server" Style="display: block" Height="0px" Width="0px"
                                        >
                                        <!--  <asp:Panel runat="server" Height="50px" Width="100%" ID="Panel66" Visible="False">  -->
                                        <table class="resRecpopUpTable_ResourceEntry">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel3" runat="server" meta:resourcekey="pnlDragableResource1">
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
                                                                                                            <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                                                                                Font-Underline="False" ForeColor="White" Width="224px" Text="Add Projects" meta:resourcekey="LTitleResource1"></asp:Label>
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
                                                                                                            <asp:RadioButton ID="rdoOrderNumber" runat="server" Text="Order Number" GroupName="RdoLookUp"
                                                                                                                CssClass="CssLabel" Checked="true" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width="144">
                                                                                                            <asp:RadioButton ID="rdoCustomerLookUp" runat="server" Text="Customer PO" GroupName="RdoLookUp"
                                                                                                                CssClass="CssLabel" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtCustomerLookUp" runat="server" MaxLength="10"></asp:TextBox>
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
                                                                                                            <asp:Button ID="btnUpdate" runat="server" CssClass="button" ToolTip="Export" Text="Order/Customer Po Lookup"
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
                                    </asp:Panel>--%>
                                                                        <asp:Panel ID="Panel16" runat="server" Style="display: block" Height="0px" Width="0px">
                                                                            <table class="resRecpopUpTable_ResourceEntry">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Panel ID="pnlDrag" runat="server" meta:resourcekey="pnlDragableResource1">
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
                                                                                                                                                <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                                                                                                                    Font-Underline="False" ForeColor="White" Width="224px" Text="Add Projects" meta:resourcekey="LTitleResource1"></asp:Label>
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
                                                                                                                                <table>
                                                                                                                                    <tbody>
                                                                                                                                        <tr>
                                                                                                                                            <td width="600">
                                                                                                                                                <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtDateProject" OnClientShown="CalendarShown"
                                                                                                                                                    runat="server">
                                                                                                                                                </asp:CalendarExtender>
                                                                                                                                                <asp:Table ID="Table4" runat="server">
                                                                                                                                                    <asp:TableRow>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:Label ID="Label27" runat="server" Width="150px" Text="Contact Name"></asp:Label>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:TextBox ID="txtContactNameProjects" runat="server"></asp:TextBox>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                    <asp:TableRow>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:Label ID="Label28" runat="server" Text="Date"></asp:Label>
                                                                                                                                                            <br />
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:TextBox ID="txtDateProject" runat="server"></asp:TextBox>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                    <asp:TableRow>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:Label ID="Label29" runat="server" Text="Project Type"></asp:Label>
                                                                                                                                                            <br />
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:TextBox ID="txtProjectType" runat="server"></asp:TextBox>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                    <asp:TableRow>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:Label ID="Label30" runat="server" Text="% Chance"></asp:Label>
                                                                                                                                                            <br />
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:DropDownList ID="ddlChance" runat="server">
                                                                                                                                                                <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                                                                                                <asp:ListItem Text="0%"></asp:ListItem>
                                                                                                                                                                <asp:ListItem Text="1-15%"></asp:ListItem>
                                                                                                                                                                <asp:ListItem Text="16-30%"></asp:ListItem>
                                                                                                                                                                <asp:ListItem Text="31-45%"></asp:ListItem>
                                                                                                                                                                <asp:ListItem Text="46-60%"></asp:ListItem>
                                                                                                                                                                <asp:ListItem Text="61-75%"></asp:ListItem>
                                                                                                                                                                <asp:ListItem Text="76-90%"></asp:ListItem>
                                                                                                                                                                <asp:ListItem Text="91-100%"></asp:ListItem>
                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                    <asp:TableRow Width="100%">
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:Label ID="SCLabel31" runat="server" Text="Estimated Amt"></asp:Label>
                                                                                                                                                            <br />
                                                                                                                                                            <br />
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                        <asp:TableCell>
                                                                                                                                                            <asp:TextBox ID="txtEstimatedAmt" runat="server"></asp:TextBox>
                                                                                                                                                        </asp:TableCell>
                                                                                                                                                    </asp:TableRow>
                                                                                                                                                </asp:Table>
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
                                                                                                                                                <asp:Button ID="btnOkProjects" runat="server" Text="Ok" CssClass="button" ToolTip="Add Projects"
                                                                                                                                                    Width="60px" OnClick="SCbtnOk_Click1" meta:resourcekey="btnOkResource1" />
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                <asp:Button ID="btnClearProjects" runat="server" Text="Clear" CssClass="button" ToolTip="Clear"
                                                                                                                                                    Width="60px" OnClick="btnClearProjects_Click" meta:resourcekey="btnOkResource1" />
                                                                                                                                            </td>
                                                                                                                                            <td class="addCancel_ResourceEntry">
                                                                                                                                                <asp:Button ID="btnCancelProjects" runat="server" Text="Cancel" CssClass="button"
                                                                                                                                                    ToolTip="Cancel" Width="60px" meta:resourcekey="btnCancelResource1" />
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
                                                                        </asp:Panel>
                                                                        <asp:Button ID="btnProjects" runat="server" Text="Add Projects" CssClass="button" />
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Panel ID="Panel17" CssClass="gridview" runat="server" Width="495px" Height="200px"
                                                                            ScrollBars="Auto">
                                                                            <asp:GridView ID="gridOtherVendors" runat="server" AllowSorting="true" OnSorting="gridOtherVendors_Sorting"
                                                                                AutoGenerateColumns="false">
                                                                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                                <PagerStyle CssClass="PagerStyle" />
                                                                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                                <EditRowStyle CssClass="EditRowStyle" />
                                                                                <RowStyle CssClass="RowStyle" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Account Number" DataField="Sold_to" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="Sold_to">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Contact Name" DataField="contactName" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="contactName">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Vendor Name" DataField="Vendorname" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="Vendor">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Comments" DataField="other" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="other">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Created on" DataField="createdon" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="createdon">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Created by" DataField="createdby" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="createdby">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Panel ID="Panel18" CssClass="gridview" runat="server" Width="495px" Height="200px"
                                                                            ScrollBars="Auto">
                                                                            <asp:GridView ID="gridProjects" runat="server" AllowSorting="true" OnSorting="gridProjects_Sorting"
                                                                                AutoGenerateColumns="false">
                                                                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                                <PagerStyle CssClass="PagerStyle" />
                                                                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                                <EditRowStyle CssClass="EditRowStyle" />
                                                                                <RowStyle CssClass="RowStyle" />
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Account Number" DataField="Sold_to" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="Sold_to">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Contact Name" DataField="name" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="name">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Project Date" DataField="PROJECT_DATE" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" DataFormatString="{0:MMM dd, yyyy}" SortExpression="PROJECT_DATE">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Project Type" DataField="PROJECT_TYPE" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="PROJECT_TYPE">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Chance (%)" DataField="CHANCE" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="CHANCE">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Estimated Amt" DataField="ESTIMATED_AMT" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="ESTIMATED_AMT">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Created on" DataField="createdon" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" DataFormatString="{0:MMM dd, yyyy}" SortExpression="createdon">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField HeaderText="Created by" DataField="createdby" HeaderStyle-VerticalAlign="Middle"
                                                                                        HeaderStyle-Wrap="false" SortExpression="createdby">
                                                                                        <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                </asp:TabContainer>
                                            </div>
                                        </asp:Panel>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <input type="button" id="BtnArrangeColumn" runat="server" value="Arrange Columns"
                                                        class="button" onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwContInfo','mywindow','width=700,height=400,scrollbars=yes')" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="HeaderDiv">
                                                    </div>
                                                    <div id="SCDataDiv" runat="server" style="overflow: auto; border: 1px solid olive;
                                                        width: 1024px; height: 220px;" onscroll="Onscrollfnction();">
                                                        <asp:GridView AutoGenerateColumns="true" ClientIDMode="Static" DataKeyNames="Contact Number,First Name,Last Name"
                                                            ID="gridSiteContactInfo" runat="server" AllowPaging="false" PageSize="4" CssClass="GridViewStyle"
                                                            CellPadding="4" CellSpacing="2" ForeColor="Black" EmptyDataText="No data available."
                                                            AllowSorting="True" Font-Size="12px" OnSorting="gridSiteContactInfo_Sorting"
                                                            OnRowDataBound="gridSiteContactInfo_RowDataBound" OnSelectedIndexChanged="gridSiteContactInfo_SelectedIndexChanged"
                                                            EnablePersistedSelection="true">
                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                            <PagerStyle BackColor="#EDEDED" Font-Size="15px" HorizontalAlign="Left" />
                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                            <RowStyle CssClass="RowStyle" Wrap="false" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <cc1:GridViewRowSelector ID="GridViewRowSelector1" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gridSiteContactInfo" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:HiddenField ID="tempIndex" runat="server" />
                                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                <asp:HiddenField ID="tempSafety" runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View4" runat="server">
                    <table>
                        <tr>
                            <td>
                                <script language="javascript" type="text/javascript">
                                    var ld = (document.all);

                                    var ns4 = document.layers;
                                    var ns6 = document.getElementById && !document.all;
                                    var ie4 = document.all;

                                    if (ns4)
                                        ld = document.loading;
                                    else if (ns6)
                                        ld = document.getElementById("loading").style;
                                    else if (ie4)
                                        ld = document.all.loading.style;

                                    function init() {
                                        if (ns4) { ld.visibility = "hidden"; }
                                        else if (ns6 || ie4) ld.display = "none";
                                    }
                                </script>
                                <div>
                                    <table style="width: 100%; padding-left: 20px;">
                                        <tr>
                                            <td>
                                                <asp:Button Style="display: none" ID="NCButton1" runat="server" meta:resourcekey="BtnNoteCancelResource1">
                                                </asp:Button>
                                            </td>
                                            <td>
                                                <table class="demo" style="width: 100%;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:ModalPopupExtender ID="NCModalPopupExtender2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                                                DynamicServicePath="" Enabled="True" PopupControlID="NCaddnotepanels" PopupDragHandleControlID="NCPnlAddNoteDrgble"
                                                                RepositionMode="None" TargetControlID="NCButton1" CancelControlID="NCBtnNoteCancel"
                                                                X="350" Y="250">
                                                            </asp:ModalPopupExtender>
                                                            <asp:Panel ID="NCaddnotepanels" runat="server" Style="display: block" Height="0px"
                                                                Width="0px" meta:resourcekey="ratePanelResource1">
                                                                <asp:UpdatePanel ID="NCUpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <!--  <asp:Panel runat="server" Height="50px" Width="100%" ID="Panel67" Visible="False">  -->
                                                                        <table class="resRecpopUpTable_ResourceEntry">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Panel ID="NCPnlAddNoteDrgble" runat="server" meta:resourcekey="pnlDragableResource1">
                                                                                            <table>
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table class="resRecpuContent_ResourceEntry">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <table class="resRecpuheadingTable_ResourceEntry">
                                                                                                                                <tbody>
                                                                                                                                    <tr>
                                                                                                                                        <td class="resRecpuheadingCell_ResourceEntry" align="left">
                                                                                                                                            <asp:Label ID="NCLabel2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
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
                                                                                                                        <td class="resRecpuIPFields_ResourceEntry" align="left">
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
                                                                                                                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="ResourceGroup" ID="RequiredFieldValidator2"
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
                                                                                                                                        <asp:UpdatePanel ID="NCUpdatePanel3" runat="server" ClientIDMode="Static">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <div style="position: relative;">
                                                                                                                                                    <asp:TextBox ID="txtNoteStartDate" runat="server" Width="100px"></asp:TextBox>
                                                                                                                                                    &nbsp;
                                                                                                                                                    <asp:ImageButton ID="NCimgstartCalNote" ImageUrl="~/App_Themes/Images/Calender.jpg"
                                                                                                                                                        runat="server" />
                                                                                                                                                    <asp:CalendarExtender ID="NoteCalendar" runat="server" Format="MM/dd/yyyy" TargetControlID="txtNoteStartDate"
                                                                                                                                                        PopupButtonID="NCimgstartCalNote" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
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
                                                                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="AddNote"
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
                                                                OnClick="btnRefreshNotes_Click" ClientIDMode="Static" CssClass="button" />
                                                        </td>
                                                        <td style="width: 87px; height: 64px;">
                                                            <%--<asp:Button ID="btnAddNote" runat="server" Text="Add Notes" Width="85px" OnClick="btnAdfsfdNote_Click"
                                    ClientIDMode="Static" OnClientClick="return false;" CssClass="button" Height="19px" />--%>
                                                            <asp:Button ID="btnAddNote" runat="server" Text="Add Notes" Width="85px" OnClick="btnAdfsfdNote_Click"
                                                                ClientIDMode="Static" CssClass="button" />
                                                        </td>
                                                        <td style="width: 91px; height: 64px;">
                                                            <asp:Button ID="NCbtnExportToExcel" Text="Export Excel" Width="85px" CssClass="button"
                                                                runat="server" ToolTip="Click to Export to Excel" OnClick="NCbtn_Export2ExcelClick" />
                                                        </td>
                                                        <td style="width: 550px; height: 64px;">
                                                            <table border="1">
                                                                <tr>
                                                                    <td style="width: 650px">
                                                                        <asp:UpdatePanel ID="NCByDateUpdate" runat="server">
                                                                            <ContentTemplate>
                                                                                <div style="position: left;">
                                                                                    &nbsp &nbsp &nbsp &nbsp
                                                                                    <asp:CheckBox ID="CheckBox1" Text="" runat="server" OnCheckedChanged="NCByDate_CheckedChanged"
                                                                                        AutoPostBack="True" />
                                                                                    Start Date:<asp:TextBox ID="NCtxtStartDate" runat="server" Width="100px"></asp:TextBox>
                                                                                    <asp:ImageButton ID="NCimgstartCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                                                                    &nbsp; End Date:<asp:TextBox ID="NCtxtEndDate" runat="server" Width="100px"></asp:TextBox>
                                                                                    <asp:ImageButton ID="NCimgEndCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="NCtxtStartDate"
                                                                                        PopupButtonID="NCimgstartCal" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden">
                                                                                    </asp:CalendarExtender>
                                                                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="MM/dd/yyyy" TargetControlID="NCtxtEndDate"
                                                                                        PopupButtonID="NCimgEndCal" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden">
                                                                                    </asp:CalendarExtender>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                            <%--  <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ByDate" EventName="CheckedChanged" />
                                                </Triggers>--%>
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
                                <table style="height: 25px; width: 1114px; padding-left: 20px;">
                                    <tr>
                                        <td>
                                            <div class="GridHeaderLabel">
                                                <asp:Label ID="lblNotesHistory" runat="server" Text="Notes History"></asp:Label>
                                                &nbsp;
                                                <input runat="server" id="NCBtnNotesColumn" type="button" class="button" value="Arrange Notes History Columns"
                                                    onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwNotesData','mywindow','width=700,height=400,scrollbars=yes')" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upNotesHistory" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Panel ID="PnlNotesCommHistory" runat="server" ScrollBars="Auto" Width="1201px"
                                                        Height="200px">
                                                        <asp:GridView AutoGenerateColumns="True" ID="NCgrdNotesHistory" CssClass="GridViewStyle"
                                                            runat="server" OnPageIndexChanging="NotesCommHistoryPageChanging" CellPadding="4"
                                                            CellSpacing="2" ForeColor="Black" EmptyDataText="No data available." AllowSorting="True"
                                                            AllowPaging="false" PageSize="7" Font-Size="12px" GridLines="Vertical" OnSorting="NCgrdNotesHistory_Sorting"
                                                            OnRowDataBound="NCgrdNotesHistory_RowDataBound" Visible="False" OnSelectedIndexChanged="NCgrdNotesHistory_SelectedIndexChanged">
                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                            <PagerStyle CssClass="PagerStyle" />
                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                            <RowStyle CssClass="RowStyle" />
                                                            <Columns>
                                                            </Columns>
                                                        </asp:GridView>
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
                                <table style="height: 25px; width: 743px; padding-left: 20px;">
                                    <tr>
                                        <td>
                                            <div class="GridHeaderLabel">
                                                <asp:Label ID="Label32" runat="server" Text="Dialer Data"></asp:Label>
                                                &nbsp;
                                                <input runat="server" id="BtnDialerColumn" type="button" class="button" value="Arrange Dialer Data Columns"
                                                    onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwDialerData','mywindow','width=700,height=400,scrollbars=yes')" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlDialerHistory" runat="server" ScrollBars="Auto" Height="250px"
                                                Width="1201px">
                                                <asp:UpdatePanel ID="upDialerData" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grdDialerHistory" CssClass="GridViewStyle" runat="server" OnPageIndexChanging="DialerDataPageChanging"
                                                            CellPadding="4" CellSpacing="2" ForeColor="Black" EmptyDataText="No data available."
                                                            AllowPaging="false" PageSize="7" Font-Size="12px" GridLines="Vertical" AllowSorting="True"
                                                            OnSorting="grdDialerHistory_Sorting" Visible="False" AutoGenerateColumns="True"
                                                            OnSelectedIndexChanged="NCgrdDialerHistory_SelectedIndexChanged" OnRowDataBound="NCgrdDialerHistory_RowDataBound">
                                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                            <PagerStyle CssClass="PagerStyle" />
                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                            <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                                            <Columns>
                                                            </Columns>
                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                            <RowStyle CssClass="RowStyle" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNoteType" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <%--  *************************************************************** --%>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button Style="display: none" ID="NCbtnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:ModalPopupExtender ID="NCModalPopupExtender1" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                                                BehaviorID="NCmpe" DynamicServicePath="" Enabled="True" PopupControlID="NCratePanel"
                                                PopupDragHandleControlID="NCpnlDragable" RepositionMode="None" TargetControlID="NCbtnHidden"
                                                X="450" Y="300">
                                            </asp:ModalPopupExtender>
                                            <asp:Panel ID="NCratePanel" runat="server" Style="display: block" Height="0px" Width="0px"
                                                meta:resourcekey="ratePanelResource1">
                                                <asp:UpdatePanel ID="NCUpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <!--  <asp:Panel runat="server" Height="50px" Width="100%" ID="NCPanel66" Visible="False">  -->
                                                        <table class="resRecpopUpTable_ResourceEntry">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="NCpnlDragable" runat="server" meta:resourcekey="pnlDragableResource1">
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table class="resRecpuContent_ResourceEntry">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table class="resRecpuheadingTable_ResourceEntry">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td class="resRecpuheadingCell_ResourceEntry" align="left">
                                                                                                                            <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
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
                                                                                                        <td class="resRecpuIPFields_ResourceEntry" align="left">
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
                                                                                                                            <asp:Button ID="NCbtnOk" runat="server" Text="Ok" CssClass="button" ToolTip="Save User"
                                                                                                                                Width="60px" ValidationGroup="ResourceGroup" OnClick="NCbtnOk_Click1" meta:resourcekey="btnOkResource1" />
                                                                                                                            <asp:Button ID="NCbtnUpdate" runat="server" CssClass="button" ToolTip="Export" Text="Export the selected"
                                                                                                                                ValidationGroup="ResourceGroup" Visible="False" Width="60px" meta:resourcekey="btnUpdateResource1" />
                                                                                                                        </td>
                                                                                                                        <td class="addCancel_ResourceEntry">
                                                                                                                            <asp:Button ID="NCbtnCancel" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
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
                                <asp:UpdateProgress ID="prgId1" runat="server">
                                    <ProgressTemplate>
                                        <div id="loading" style="position: absolute; width: 100%; text-align: center; top: 300px;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/indicator_medium.gif" />
                                            <%-- <img src="~/App_Themes/Images/Loading.gif" border="0" alt="Please wait while loading data">--%>
                                            <br />
                                            Please wait...
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
