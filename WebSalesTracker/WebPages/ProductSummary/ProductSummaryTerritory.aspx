<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductSummaryTerritory.aspx.cs"
    Inherits="WebSalesMine.WebPages.ProductSummary.ProductSummaryTerritory" MasterPageFile="~/WebPages/UserControl/SalesMine.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<asp:Content runat="server" ContentPlaceHolderID="MAINCONTENT" ID="PST">
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
    <table style="padding-left: 20px;" width="800px">
        <tr>
            <td>
                <asp:Button ID="BtnShowAllSkuSummary" runat="server" Text="Refresh Sku Summary" OnClick="BtnShowAllSkuSummary_Click"
                    CssClass="button" />
                &nbsp;
                <asp:Button ID="btnExportToExcel" Text="Export Excel" CssClass="button" runat="server"
                    ToolTip="Click to Export to Excel" OnClick="btn_Export2ExcelClick" />
                <%if (TArrnageColumnstring == "lvwSKUSummaryT")
                  { %>
                <input runat="server" id="Button2" type="button" class="button" value="Arrange SKU Summary Columns"
                    onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwSKUSummaryT','mywindow','width=700,height=400,scrollbars=yes')" />
                <% }%>
                <%if (TArrnageColumnstring == "lvwPCSKUSummaryT")
                  { %>
                <input runat="server" id="Button1" type="button" class="button" value="Arrange SKU Summary Columns"
                    onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwPCSKUSummaryT','mywindow','width=700,height=400,scrollbars=yes')" />
                <% }%>
            </td>
        </tr>
    </table>
    <br />
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
                        <asp:Panel ID="Panel6" runat="server" ScrollBars="Auto" Width="1200px" Height="300px">
                            <asp:GridView ID="grdProductLineSummary" runat="server" AutoGenerateColumns="false"
                                CssClass="GridViewStyle" Width="1200px" EmptyDataText="No data available." PageSize="10"
                                BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px"
                                ForeColor="Black" AllowPaging="False" CellPadding="4" CellSpacing="1" Font-Size="12px"
                                OnRowCommand="grdProductLineSummary_RowCommand" AllowSorting="true" OnSorting="grdProductLineSummary_Sorting"
                                OnPageIndexChanging="grdProductLineSummaryDataPageEventHandler">
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Filter">
                                        <ItemTemplate>
                                            <div align="center">
                                                <asp:LinkButton ID="lbtnShowSKU" runat="server" CommandName="ShowPLSKU" CommandArgument='<%# Eval("sku_category")%>'
                                                    Text="Filter" /></div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Product Line" DataField="sku_category" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                        SortExpression="sku_category" />
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
                                        DataFormatString="{0:d}" SortExpression="last_order_date" ItemStyle-HorizontalAlign="Right" />
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
                            <br />
                            <asp:Panel ID="pnlGridIndex" runat="server" CssClass="CssLabel" Visible="false">
                            </asp:Panel>
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
                            <asp:GridView ID="grdSkuSummary" runat="server" AutoGenerateColumns="true" Width="1200px"
                                CssClass="GridViewStyle" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid"
                                BorderWidth="3px" ForeColor="Black" EmptyDataText="No data available." PageSize="10"
                                AllowPaging="false" CellPadding="4" CellSpacing="1" Font-Size="12px" OnPageIndexChanging="grdSkuSummaryDataPageEventHandler"
                                AllowSorting="true" OnSorting="grdSkuSummary_Sorting" OnRowDataBound="grdSkuSummary_RowDataBound">
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <RowStyle CssClass="RowStyle" Wrap="false" />
                            </asp:GridView>
                            <br />
                            <asp:Panel ID="Panel1" runat="server" CssClass="CssLabel" Visible="false">
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                    <br />
                    <asp:Label ID="lblErrorSkuSummary" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlPCCampaign" runat="server" Visible="false">
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
                                    <asp:Label ID="Label1" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div>
                        <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" Width="1100px" Height="300px">
                            <asp:GridView ID="grdPCProductLineSummary" runat="server" AutoGenerateColumns="false"
                                CssClass="GridViewStyle" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid"
                                BorderWidth="3px" ForeColor="Black" Width="1100px" EmptyDataText="No data available."
                                PageSize="10" AllowPaging="false" OnRowDataBound="grdPCProductLineSummary_RowDataBound"
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
                                    <asp:TemplateField HeaderText="Filter">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnShowSKU" runat="server" CommandName="ShowPLSKU" CommandArgument='<%# Eval("sku_family")%>'
                                                Text="Filter" />
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
                            <asp:Panel ID="Panel2" runat="server" CssClass="CssLabel" Visible="false">
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
        <table style="padding-left: 20px;" width="1100px">
            <tr>
                <td>
                    <table style="height: 25px; background-color: #ededed; width: 1100px;">
                        <tbody>
                            <tr>
                                <td>
                                    <div class="GridNameHeader">
                                        <asp:Label ID="Label5" runat="server" Text="SKU Summary:"></asp:Label></div>
                                </td>
                                <td>
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
                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Width="1200px" Height="300px">
                            <asp:GridView ID="grdPCSKUSummary" runat="server" AutoGenerateColumns="false" Width="1200px"
                                CssClass="GridViewStyle" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid"
                                BorderWidth="3px" ForeColor="Black" OnRowDataBound="grdPCSkuSummary_RowDataBound"
                                EmptyDataText="No data available." PageSize="10" AllowPaging="false" CellPadding="4"
                                CellSpacing="1" Font-Size="12px" OnPageIndexChanging="grdPCSkuSummaryDataPageEventHandler"
                                AllowSorting="true" OnSorting="grdPCSKUSummary_Sorting">
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <RowStyle CssClass="RowStyle" />
                            </asp:GridView>
                            <asp:Panel ID="Panel4" runat="server" CssClass="CssLabel" Visible="false">
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                    <br />
                    <asp:Label ID="Label7" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <tr runat="server" id="trBlank" visible="false">
        <td style="height: 175px">
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
                                                                                                <asp:RadioButton ID="rdoProductSummary" runat="server" Text="ProductSummary Territory"
                                                                                                    GroupName="RdoExportFile" CssClass="CssLabel" Checked="true" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="144">
                                                                                                <asp:RadioButton ID="rdoSKUSummary" runat="server" Text="SKU Summary Territory" GroupName="RdoExportFile"
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
</asp:Content>
