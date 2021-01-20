<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="WebSalesMine.WebPages.Home.Notes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="AppLogic" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../App_Themes/CSS/Site.css" rel="Stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../App_Themes/CSS/demos.css" />
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 54px;
        }
        .style2
        {
            height: 21px;
            width: 54px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="demo">
    <asp:ScriptManager ID="ScriptManagerContact" runat="server" EnablePageMethods="true"
        EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <div id="dialog-form" title="Add a note" class="demo" align="center">
        <asp:Panel ID="Panel23" runat="server" ScrollBars="Auto" Height="290px" Width="508px">
            <asp:UpdatePanel ID="updateDialog" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="style1" align="left">
                                Note Type :
                            </td>
                            <td align="left">
                                <asp:ComboBox ID="NoteTyped" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="NoteTyped_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value="1"> </asp:ListItem>
                                    <asp:ListItem Text="Follow Up" Value="Follow Up"> </asp:ListItem>
                                    <asp:ListItem Text="Reminder" Value="Reminder"> </asp:ListItem>
                                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                </asp:ComboBox>
                            </td>
                        </tr>
                        <tr align="left">
                            <td class="style2">
                                Date :
                            </td>
                            <td align="left">
                                <asp:UpdatePanel ID="ByDateUpdate" runat="server" ClientIDMode="Static">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtStartDate" runat="server" Width="100px" OnTextChanged="txtStartDate_TextChanged"
                                            AutoPostBack="true" Enabled="False"></asp:TextBox>
                                        <asp:ImageButton ID="imgstartCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server"
                                            OnClick="imgstartCal_Click" />
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate"
                                            PopupButtonID="imgstartCal">
                                        </asp:CalendarExtender>
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
                        <%--<asp:Label  ID="lblAddNoteMsg" runat="server" Text="Please provide relevant notes about recent customer or contact interaction. "
                            Font-Bold="True" Width="100%" />
                        <br />--%>
                        <asp:TextBox TextMode="MultiLine" name="usermessage" ID="AddNote" Height="118px"
                            Width="456px" runat="server" OnTextChanged="AddNote_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <table>
                            <tr>
                                <td>
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
                        <%--<asp:Button ID="btnclose" runat="server" Text="Close" Height="22px" 
                            Width="57px" onclick="btnclose_Click" />--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
