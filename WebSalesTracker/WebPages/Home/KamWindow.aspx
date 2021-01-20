<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KamWindow.aspx.cs" Inherits="WebSalesMine.WebPages.Home.KamWindow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>kam Window </title>
    <link href="../../App_Themes/CSS/Site.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function freezeHeader() {
            var GridId = "<%=gridShowAllUserData.ClientID %>";
            var ScrollHeight = 160;
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ReloadtheparentWindow() {
            window.opener.location.reload(true);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <div style="color: Red">
        <asp:Literal ID="litMessage" runat="server" Visible="false"> Please check the KAM ID OR Contact HelpDesk</asp:Literal></div>
    <asp:Panel ID="pnlShowKamWindow" runat="server" Visible="false">
        <table width="800px">
            <tr>
                <td style="width: 50px">
                    &nbsp;
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rdoAccountName" runat="server" Text="Account Name" GroupName="RdoFilterKam" />
                            </td>
                            <td class="style1">
                                <asp:RadioButton ID="rdoManagedGroup" runat="server" Text="Managed Group" GroupName="RdoFilterKam" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtbSearchKam" runat="server" MaxLength="20" Width="240px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearchKam" runat="server" Text="Search" OnClick="btnSearchKam_Click"
                                    CssClass="button" />
                                &nbsp; &nbsp;
                                <asp:Button ID="BtnClearSearchKam" runat="server" Text="Show All" OnClick="BtnClearSearchKam_Click"
                                    CssClass="button" />
                            </td>
                        </tr>
                        <tr>
                            <td style="color: Red">
                                <asp:Literal ID="litErrorinGrid" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 50px">
                    &nbsp;
                </td>
                <td>
                    <table border="0" cellpadding="1" cellspacing="2" style="border-color: Blue;">
                        <tr>
                            <td>
                                <asp:Button ID="OrderHistory" runat="server" Text="Show Order History" OnClick="btnShowPage"
                                    Width="160" />
                            </td>
                            <td>
                                <asp:Button ID="SiteAndContactInfo" runat="server" Text="Show Site & Contact Info"
                                    OnClick="btnShowPage" Width="160" />
                            </td>
                            <td>
                                <asp:Button ID="Quotes" runat="server" Text="Show Quotes" OnClick="btnShowPage" Width="160" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="ProductSummary" runat="server" Text="Show Product Summary" OnClick="btnShowPage"
                                    Width="160" />
                            </td>
                            <td>
                                <asp:Button ID="NotesCommHistory" runat="server" Text="Show Notes & Com Hist" OnClick="btnShowPage"
                                    Width="160" />
                            </td>
                            <td>
                                <asp:Button ID="CustomerLookUp" runat="server" Text="Show Customer LookUp" OnClick="btnShowPage"
                                    Width="160" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 50px">
                    &nbsp;
                </td>
                <td>
                    <asp:UpdatePanel ID="KamGridUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div>
                                <table width="700px">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlOverflow" runat="server" ScrollBars="none" Height="180">
                                                <asp:GridView ID="gridShowAllUserData" runat="server" AutoGenerateColumns="false"
                                                    OnRowDataBound="gridShowAllUserData_RowDataBound" OnSelectedIndexChanged="gridShowAllUserData_SelectedIndexChanged"
                                                    OnRowCommand="gridShowAllUserData_RowCommand" AllowPaging="False" AllowSorting="True"
                                                    OnSorting="GridView1_Sorting" CssClass="freezeHeader" BorderColor="#999999" BorderStyle="Solid"
                                                    ForeColor="Black">
                                                    <Columns>
                                                        <asp:ButtonField Text="SingleClick" CommandName="SingleClick" Visible="false" />
                                                        <asp:BoundField HeaderText="Account Number" DataField="SOLD_TO" SortExpression="SOLD_TO" />
                                                        <asp:BoundField HeaderText="Account Name" DataField="NAME" SortExpression="NAME"
                                                            ItemStyle-CssClass="Shorter" />
                                                        <asp:BoundField HeaderText="Managed Group" DataField="MG_NAME" SortExpression="MG_NAME"
                                                            ItemStyle-CssClass="Shorter" />
                                                        <asp:BoundField HeaderText="Last Ordered Date" DataField="LPDCUST" SortExpression="LPDCUST"
                                                            DataFormatString="{0:MMM dd, yyyy}" />
                                                        <asp:BoundField HeaderText="Life Time Sales" DataField="LTSALES" SortExpression="LTSALES"
                                                            DataFormatString="{0:C}" />
                                                        <asp:ButtonField CommandName="Select" Visible="false" />
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                    <PagerStyle BackColor="#EDEDED" Font-Size="15px" HorizontalAlign="Left" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" CssClass="sample" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
