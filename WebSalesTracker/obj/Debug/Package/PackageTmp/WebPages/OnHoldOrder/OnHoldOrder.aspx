<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="OnHoldOrder.aspx.cs" Inherits="WebSalesMine.WebPages.OnHoldOrder.OnHoldOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
    <div id="container">
        <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%"
            height="100%">
            <tr>
                <td style="width: 265px">
                    <table>
                        <tr>
                            <td class="object-wrapper" style="height: 20px">,
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="btnShowMyOnHoldOrder" runat="server" CssClass="LabelFont" OnClick="btnShowMyOnHoldOrder_Click">Show My On Hold Order</asp:LinkButton>
                            </td>
                            <td class="object-wrapper" style="height: 20px">
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="btnShowAllOnHoldOrder" runat="server" CssClass="LabelFont" ClientIDMode="Static"
                                    OnClick="btnShowAllOnHoldOrder_Click">Show All On Hold Order</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="table-wrapper page1">
            <table style="border: 0; width: 100%; height: 100%" cellpadding="10" cellspacing="0">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Visible="false" CssClass="GridHeaderLabel"
                                        Text="Filter By:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="llblFilterByOnHoldOrder" runat="server" CssClass="GridHeaderLabel"
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ScrollBars="None" ID="DataDiv" Width="100%">
                            <asp:GridView AutoGenerateColumns="false" ClientIDMode="Static" ID="gridOnHoldOrder"
                                AllowPaging="true" AllowSorting="true" PageSize="50" runat="server" ForeColor="Black"
                                BackColor="#FFFFFF" Font-Size="12px" CellPadding="4" CellSpacing="2" GridLines="None"
                                AsyncRendering="false" OnSorting="gridOnHoldOrder_Sorting"
                                OnPageIndexChanging="gridOnHoldOrderPageChanging" >
                                <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" BackColor="#e5e5e5" Wrap="false" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <AlternatingRowStyle BackColor="White" Wrap="false" />
                                <PagerStyle CssClass="grid_paging" />
                                <Columns>
                                    <asp:BoundField HeaderText="SALESTEAM ID" DataField="salesteam_ID" HeaderStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" SortExpression="salesteam_ID" />
                                    <asp:BoundField HeaderText="CUSTOMER NO." DataField="customer" HeaderStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" SortExpression="customer" />
                                    <asp:BoundField HeaderText="ORDER DATE" DataField="createdon" DataFormatString="{0:MMM dd, yyyy}"
                                        HeaderStyle-Wrap="false" SortExpression="createdon" />
                                    <asp:BoundField HeaderText="ORDER NUMBER" DataField="doc_number" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" SortExpression="doc_number" />
                                    <asp:BoundField HeaderText="HEADER DELIVERY BLOCK" DataField="header_del_block_desc"
                                        HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" SortExpression="header_del_block_desc" />
                                    <asp:BoundField HeaderText="LINE" DataField="s_ord_item" HeaderStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" SortExpression="s_ord_item" />
                                    <asp:BoundField HeaderText="PART NUMBER" DataField="mat_entrd" HeaderStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" SortExpression="mat_entrd" />
                                    <asp:BoundField HeaderText="EXT PRICE" DataField="net_value" HeaderStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:##,#####}" SortExpression="net_value" />
                                    <asp:BoundField HeaderText="ITEM DELIVERY BLOCK" DataField="item_del_block_desc"
                                        HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" SortExpression="item_del_block_desc" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No Data
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlGridIndex" runat="server" CssClass="CssLabel">
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
