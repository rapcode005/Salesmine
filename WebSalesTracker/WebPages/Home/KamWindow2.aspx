<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KamWindow2.aspx.cs" Inherits="WebSalesMine.WebPages.Home.KamWindow2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>kam Window </title>
    <script src="../../App_Themes/JS/JsGrid/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../App_Themes/JS/JsGrid/jquery.tablesorter.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(
                  function () {


                      $('#' + '<%=GridView1.ClientID%>').tablesorter();


                  }
                            );

        }
    </script>
    <style type="text/css">
        table.tablesorter
        {
            font-family: arial;
            background-color: #FFFFFF;
            margin: 10px 0pt 15px;
            font-size: 9pt;
            width: 100%;
            text-align: left;
        }
        table.tablesorter thead tr th, table.tablesorterBlue tfoot tr th
        {
            background-color: #225588;
            color: White;
            border: 1px solid #FFF;
            font-size: 9pt;
            padding: 4px;
        }
        table.tablesorter thead tr .header
        {
            background-image: url("../../App_Themes/Images/lev0_bg1.gif");
            background-repeat: no-repeat,repeat-x;
            background-position: right;
            text-align: left;
            cursor: pointer;
            border-left: 1px solid #FFF;
            border-right: 1px solid #000;
            border-top: 1px solid #FFF;
            padding-left: 8px;
            height: auto;
        }
        table.tablesorter tbody td
        {
            color: #3D3D3D;
            padding: 4px;
            vertical-align: top;
        }
        table.tablesorter tbody tr.odd td
        {
            background-color: #F0F0F6;
        }
        table.tablesorter thead tr .headerSortUp
        {
            background-image: url("../../App_Themes/Images/asc.gif");
            background-repeat: no-repeat,repeat-x;
            background-position: right;
            background-color: #8dbdd8;
            text-align: left;
            color: Black;
        }
        table.tablesorter thead tr .headerSortDown
        {
            background-image: url("../../App_Themes/Images/desc.gif");
            background-repeat: no-repeat,repeat-x;
            background-position: right;
            background-color: #8dbdd8;
            text-align: left;
            color: Black;
        }
        table.tablesorter thead tr .headerSortDown, table.tablesorterBlue thead tr .headerSortUp
        {
            background-color: #8dbdd8;
            color: Black;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function ReloadtheparentWindow() {
            window.opener.location.reload(true);

        }
    </script>

 
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
        
            xPos = $get('pnlOverflow').scrollLeft;

            yPos = $get('pnlOverflow').scrollTop;

        }
        function EndRequestHandler(sender, args) {
            //freezeHeader();
            $get('pnlOverflow').scrollLeft = xPos;
            $get('pnlOverflow').scrollTop = yPos;

        } 
    </script>
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
                                    Width="165" />
                            </td>
                            <td>
                                <asp:Button ID="SiteAndContactInfo" runat="server" Text="Show Site & Contact Info"
                                    OnClick="btnShowPage" Width="165" />
                            </td>
                            <td>
                                <asp:Button ID="Quotes" runat="server" Text="Show Quotes" OnClick="btnShowPage" Width="165" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="ProductSummary" runat="server" Text="Show Product Summary" OnClick="btnShowPage"
                                    Width="165" />
                            </td>
                            <td>
                                <asp:Button ID="NotesCommHistory" runat="server" Text="Show Notes & Com Hist" OnClick="btnShowPage"
                                    Width="165" />
                            </td>
                            <td>
                                <asp:Button ID="CustomerLookUp" runat="server" Text="Show Customer LookUp" OnClick="btnShowPage"
                                    Width="165" />
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
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlOverflow" runat="server" ScrollBars="Both" Height="210">
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="tablesorter"
                                                                OnRowCommand="gridview1_RowCommand" >
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Account Number" HeaderStyle-Width="120px">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkMySelect" runat="server" Text='<%# Eval ("Account Number") %>'
                                                                                CausesValidation="False" CommandName="Click" CommandArgument='<%#Eval("Account Number") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Account Name" HeaderText="Account Name" />
                                                                    <asp:BoundField DataField="Managed Group" HeaderText="Managed Group" />
                                                                    <asp:BoundField DataField="Last Ordered Date" HeaderText="Last Ordered Date" DataFormatString="{0:MMM dd, yyyy}" HeaderStyle-Width="150px" />
                                                                    <asp:BoundField DataField="Life Time Sales" HeaderText="Life Time Sales" DataFormatString="{0:C}" HeaderStyle-Width="100px"
                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
