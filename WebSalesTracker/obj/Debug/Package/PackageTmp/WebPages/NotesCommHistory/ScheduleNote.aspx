<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleNote.aspx.cs" Inherits="WebSalesMine.WebPages.NotesCommHistory.ScheduleNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../App_Themes/CSS/styles.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/KamCss.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/PopUpModal.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Theme_Roller/jquery-ui-1.8.17.custom/css/custom-theme/jquery-ui-1.8.17.custom.css"
        rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/JS/jquery.min.js" type="text/javascript"></script>
    <script src="../../App_Themes/JS/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../App_Themes/JS/JsGrid/jquery.tablesorter.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $(document).ready(function () {
                $("#grdSchduleNote").tablesorter({ widgets: ['zebra'] });
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMessage" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <div>
        <div class="table-wrapper page1">
            <table border="0" cellpadding="10" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel runat="server" ScrollBars="None" ID="panel2" Width="100%">
                            <asp:GridView ID="grdSchduleNote" runat="server" AutoGenerateColumns="False" GridLines="None"
                                ForeColor="Black" CellPadding="4" CellSpacing="2" BackColor="#FFFFFF" EmptyDataText="No Schedule Note"
                                ClientIDMode="Static" Font-Size="12px" AsyncRendering="false">
                                <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" BackColor="#e5e5e5" Wrap="false" />
                                <AlternatingRowStyle BackColor="White" Wrap="false" />
                                <Columns>
                                    <asp:BoundField HeaderText="ACCOUNT NO." DataField="Account" HeaderStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" SortExpression="Account" />
                                    <asp:BoundField HeaderText="NOTE TYPE" DataField="Type" HeaderStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" SortExpression="Type" />
                                    <asp:BoundField HeaderText="NOTES" DataField="Notes" HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center"
                                        SortExpression="Notes" />
                                    <asp:BoundField HeaderText="ACCOUNT NAME" DataField="Name" HeaderStyle-Wrap="false"
                                        HeaderStyle-HorizontalAlign="Center" SortExpression="Name" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="htdSortBy" runat="server" ClientIDMode="Static" Value="0" />
    </div>
    </form>
</body>
</html>
