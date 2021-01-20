<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Quotes.aspx.cs" EnableEventValidation="false"
    Inherits="WebSalesMine.WebPages.Quotes.Quotes" %>

<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
    <style type="text/css">
        .Invisible
        {
            visibility: hidden;
        }
        .FixedPostion
        {
            position: fixed;
        }
    </style>
    <script type='text/javascript' src="../../App_Themes/JS/accounting.js"></script>
    <script type="text/javascript">
        var FIREFOX = /Firefox/i.test(navigator.userAgent);

        function CheckString(val) {
            if (val)
                return val;
            else
                return "";
        }

        function FormatMoney(value) {
            var MoneyFormat;

            if ($get('CurrencyCode').value == "GBP") {
                MoneyFormat = accounting.formatMoney(value, "£", 2);
            }
            else if ($get('CurrencyCode').value == "EUR") {
                MoneyFormat = accounting.formatMoney(value, { symbol: " €", format: "%v %s", thousand: ".", decimal: ",", precision: 2 });
            }
            else if ($get('CurrencyCode').value == "CHF") {
                MoneyFormat = accounting.formatMoney(value, { symbol: "fr.", format: "%s %v", thousand: "'", decimal: ".", precision: 2 });
            }
            else {
                MoneyFormat = accounting.formatMoney(value, "$", 2);
            }

            return MoneyFormat;
        }

        function onsuccess(Data) {
            var myDate;
            if (FIREFOX) {
                $get('txtQuoteDocNoDetails').textContent = CheckString(Data.rows[0]['Quote_Doc_No']);
                $get('txtQuoteLineDetails').textContent = CheckString(Data.rows[0]['Quote_Line']);
                if (CheckString(Data.rows[0]['Quote_Date'])) {
                    myDate = new Date(CheckString(Data.rows[0]['Quote_Date']));
                    $get('txtQuoteDateDetails').textContent = (myDate.getMonth() + 1) + "/" +
                                                         myDate.getDate() + "/" +
                                                         myDate.getFullYear();
                }
                else
                    $get('txtQuoteDateDetails').textContent = "";
                $get('txtQuotePOTypeDetails').textContent = CheckString(Data.rows[0]['Quote_PO_Type']);
                $get('txtQuotePOTypeDescDetails').textContent = CheckString(Data.rows[0]['Quote_PO_Type_Desc']);
                $get('txtQuoteItemCategDescDetails').textContent = CheckString(Data.rows[0]['Quote_Item_Categ_Desc']);
                $get('txtQuoteSisTeamINDetails').textContent = CheckString(Data.rows[0]['Quote_SlsTeamIN']);
                $get('txtQuoteMatEntrdDetails').textContent = CheckString(Data.rows[0]['Quote_Mat_Entrd']);
                $get('txtQuoteMatEntrdDescDetails').textContent = CheckString(Data.rows[0]['Quote_Mat_Entrd_Desc']);
                $get('txtQuoteDiscountDetails').textContent = FormatMoney(CheckString(Data.rows[0]['Quote_Discount']));
                $get('txtQuoteNetSalesDetails').textContent = FormatMoney(CheckString(Data.rows[0]['Quote_Net_Sales']));
                $get('txtDMProductLineDescDetails').textContent = CheckString(Data.rows[0]['DM_Product_Line_Desc']);
                $get('txtQuoteQtyDetails').textContent = CheckString(Data.rows[0]['quote_qty']);
                $get('txtQuoteUnitPriceDetails').textContent = FormatMoney(CheckString(Data.rows[0]['quote_unit_price']));
                $get('txtQuoteCostDetails').textContent = FormatMoney(CheckString(Data.rows[0]['Quote_Cost']));
            }
            else {
                $get('txtQuoteDocNoDetails').innerText = CheckString(Data.rows[0]['Quote_Doc_No']);
                $get('txtQuoteLineDetails').innerText = CheckString(Data.rows[0]['Quote_Line']);
                if (CheckString(Data.rows[0]['Quote_Date'])) {
                    myDate = new Date(CheckString(Data.rows[0]['Quote_Date']));
                    $get('txtQuoteDateDetails').innerText = (myDate.getMonth() + 1) + "/" +
                                                         myDate.getDate() + "/" +
                                                         myDate.getFullYear();
                }
                else
                    $get('txtQuoteDateDetails').innerText = "";
                $get('txtQuotePOTypeDetails').innerText = CheckString(Data.rows[0]['Quote_PO_Type']);
                $get('txtQuotePOTypeDescDetails').innerText = CheckString(Data.rows[0]['Quote_PO_Type_Desc']);
                $get('txtQuoteItemCategDescDetails').innerText = CheckString(Data.rows[0]['Quote_Item_Categ_Desc']);
                $get('txtQuoteSisTeamINDetails').innerText = CheckString(Data.rows[0]['Quote_SlsTeamIN']);
                $get('txtQuoteMatEntrdDetails').innerText = CheckString(Data.rows[0]['Quote_Mat_Entrd']);
                $get('txtQuoteMatEntrdDescDetails').innerText = CheckString(Data.rows[0]['Quote_Mat_Entrd_Desc']);
                $get('txtQuoteDiscountDetails').innerText = FormatMoney(CheckString(Data.rows[0]['Quote_Discount']));
                $get('txtQuoteNetSalesDetails').innerText = FormatMoney(CheckString(Data.rows[0]['Quote_Net_Sales']));
                $get('txtDMProductLineDescDetails').innerText = CheckString(Data.rows[0]['DM_Product_Line_Desc']);
                $get('txtQuoteQtyDetails').innerText = CheckString(Data.rows[0]['quote_qty']);
                $get('txtQuoteUnitPriceDetails').innerText = FormatMoney(CheckString(Data.rows[0]['quote_unit_price']));
                $get('txtQuoteCostDetails').innerText = FormatMoney(CheckString(Data.rows[0]['Quote_Cost']));
            }
        }


        function RoundOffTwoDecimal(original) {
            var result = Math.round(original * 100) / 100;
            return result;
        }

        function Remove() {
            setCookie('CNo', "", -1);
        }

        function pageLoad() {

            $("#<%=gridQuoteNumber.ClientID%>  tr:has(td)").hover(function () {
                $(this).css("cursor", "pointer");
            });

            $("#<%=gridQuotes.ClientID%>  tr:has(td)").hover(function () {
                $(this).css("cursor", "pointer");
            });

            $(document).ready(function () {

                $("#gridQuotes tr").not($("#gridQuotes tr").eq(0)).click(function () {

                    $("#gridQuotes  tr").closest('TR').removeClass('SelectedRowStyle');
                    $(this).addClass('SelectedRowStyle');

                    var QuoteDocNo;
                    var QuoteLine;
                    var totalCols = $("#gridQuotes").find('tr')[0].cells.length;

                    for (var i = 0; i < totalCols; i++) {
                        var ColName = $('#gridQuotes tr').find('th:nth-child(' + i + ')').text();

                        if (ColName == 'QUOTE DOC NO') {
                            QuoteDocNo = $(this).find("td:eq(" + (i - 1) + ")").html();
                        }
                        else if (ColName == 'QUOTE LINE')
                            QuoteLine = $(this).find("td:eq(" + (i - 1) + ")").html();
                    }

                    PageMethods.GetData(QuoteDocNo, QuoteLine, onsuccess);

                    var mydiv = $('#pnlPopupQuotesDetails');
                    mydiv.dialog({ autoOpen: false,
                        title: "Quotes Details",
                        resizable: false,
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        width: "auto",
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        }
                    });

                    mydiv.dialog('open');
                });

                $('#<%= lnkSelectContact.ClientID %>').click(function () {
                    var mydiv = $('<div></div>')
                            .html('<iframe frameborder="0" src="../Home/ContactLevel.aspx" width="100%" height="100%"></iframe>')
                            .dialog({ autoOpen: false,
                                title: "Contact Level",
                                resizable: false,
                                modal: true,
                                dialogClass: "Frame",
                                height: 400,
                                width: 680,
                                buttons: {
                                    "Ok": function () {
                                        setCookie('XposT', getCookie('Xpos'), 1);
                                        setCookie('YposT', getCookie('Ypos'), 1);
                                        setCookie('CName', getCookie('CNameTemp'), 1);
                                        setCookie('CNo', getCookie('CNoTemp'), 1);
                                        setCookie("OKSelectContact", "OK", 1);
                                        __doPostBack('= btnRefresh.ClientID ', '');
                                        mydiv.dialog('close');
                                    },
                                    "Cancel": function () {
                                        __doPostBack('= btnRefresh.ClientID ', '');
                                        mydiv.dialog('close');
                                    }
                                }
                            });

                    mydiv.dialog("open");

                    return false;
                });

                $('#<%= lnkContactSelected.ClientID %>').click(function () {
                    setCookie('CName', "", -1);
                    setCookie('OKSelectContact', "OK", 1);
                    __doPostBack('= btnRefresh.ClientID ', '');
                    return false;
                });

                $('#<%= ImageSelectContact.ClientID %>').hover(function () {
                    $(this).css("cursor", "pointer");
                });

                $('#<%= ImageSelectContact.ClientID %>').click(function () {
                    setCookie('CName', "", -1);
                    setCookie('CNo', "", -1);
                    __doPostBack('= btnRefresh.ClientID ', '');
                    return false;
                });

            });

            $addHandler(document, 'keydown', onKeydown);
        }

        function onKeydown(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                var mydiv = $('#pnlPopupQuotesDetails');
                mydiv.dialog('close');
            }
        }

    </script>
    <div id="container">
        <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%"
            height="100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="object-wrapper" style="height: 20px">
                                <asp:LinkButton ID="lnkSelectContact" ClientIDMode="Static" runat="server" Text="Select Contact"
                                    CssClass="LabelFont"></asp:LinkButton>
                                &nbsp;
                                <img id="ImageSelectContact" src="~/App_Themes/Images/New Design/btn-red-x.gif" runat="server"
                                    height="10" border="0" />
                                <asp:LinkButton ID="lnkContactSelected" ClientIDMode="Static" runat="server" Text=""
                                    Style="text-decoration: none; border-bottom: 1px solid red;" Font-Size="12px"
                                    ToolTip="Click to remove contact" ForeColor="Black" CssClass="LabelFont"></asp:LinkButton>
                            </td>
                            <%if (userRule != "PC-MAN" || userRule != "PC-ONT")
                              { %>
                            <td class="object-wrapper" style="height: 20px">
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lnkMasterDataChange" ClientIDMode="Static" OnClick="btn_Export2ExcelClick"
                                    runat="server" CssClass="LabelFont">Export to Excel</asp:LinkButton>
                            </td>
                            <% }%>
                            <td class="object-wrapper" style="height: 20px">
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkArrange" Text="Arrange Quote Details Columns" OnClick="ArrangeColumn_Click"
                                    CssClass="LabelFont" runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblQuoteList" runat="server" clientidmode="Static">
            <tr>
                <td>
                    <asp:UpdatePanel ID="updatePanelQuoteNumber" runat="server">
                        <ContentTemplate>
                            <div class="table-wrapper page3">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="samplePanel" runat="server" ScrollBars="Vertical" Height="351px" Width="100%">
                                                <asp:GridView AutoGenerateColumns="false" ClientIDMode="Static" ID="gridQuoteNumber"
                                                    ForeColor="Black" AllowSorting="true" runat="server" BackColor="#FFFFFF" GridLines="None"
                                                    CellPadding="4" CellSpacing="2" EmptyDataText="No data available." AllowPaging="true"
                                                    Font-Size="12px" PageSize="10" OnPageIndexChanging="gridQuoteNumber_PageChanging"
                                                    OnSelectedIndexChanged="gridQuoteNumber_SelectedIndexChanged1" OnRowDataBound="gridQuoteNumber_RowDataBound"
                                                    OnRowCreated="gridQuoteNumber_RowCreated" OnSorting="gridQuoteNumber_Sorting">
                                                    <AlternatingRowStyle BackColor="#e5e5e5" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyleQuoted" />
                                                    <PagerSettings PageButtonCount="5" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Image ID="ImageArrow" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="QUOTE LIST" DataField="Quote_Doc_No" HeaderStyle-Wrap="false"
                                                            HeaderStyle-HorizontalAlign="Center" SortExpression="Quote_Doc_No" />
                                                        <asp:BoundField HeaderText="QUOTE DATE" DataField="Quote_Doc_createdon" HeaderStyle-Wrap="false"
                                                            HeaderStyle-HorizontalAlign="Center" SortExpression="Quote_Doc_createdon" DataFormatString="{0:MM/dd/yyyy}" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gridQuoteNumber" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td id="tdQuoteInfo" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="border: #ccc solid 1px; background-color: White;">
                                <tr>
                                    <td style="background-color: #d6e0ec;">
                                        <asp:Label ID="lblQuoteInfoHeader" Font-Bold="True" CssClass="LabelFont" runat="server"
                                            Text="Quote Info"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" CssClass="LabelFont" Text="Sales Rep" AssociatedControlID="txtSalesRep"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSalesRep" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" CssClass="LabelFont" Text="Status" AssociatedControlID="txtStatus"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStatus" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblQuoteDate" runat="server" CssClass="LabelFont" Text="Quote Date"
                                                        AssociatedControlID="txtQuoteDate"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQuoteDate" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" CssClass="LabelFont" runat="server" Text="Order Date" AssociatedControlID="txtOrderDate"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOrderDate" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label10" CssClass="LabelFont" runat="server" Text="Quote Time" AssociatedControlID="txtQuoteTime"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQuoteTime" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label11" CssClass="LabelFont" runat="server" Text="Order Value" AssociatedControlID="txtOrderValue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOrderValue" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label12" CssClass="LabelFont" runat="server" Text="Quote Value" AssociatedControlID="txtQuoteValue"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQuoteValue" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label13" CssClass="LabelFont" runat="server" Text="Order Margin" AssociatedControlID="txtOrderMargin"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOrderMargin" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label14" CssClass="LabelFont" runat="server" Text="Quote COG" AssociatedControlID="txtQuoteCOG"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQuoteCOG" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <%-- <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                 <td>
                                                    <asp:Label ID="Label18" CssClass="LabelFont" runat="server" Text="Quote Cost" AssociatedControlID="txtquotecost"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtquotecost" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label16" CssClass="LabelFont" runat="server" Text="Quote Margin" AssociatedControlID="txtQuoteMargin"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQuoteMargin" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <%--<td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                 <td>
                                                    <asp:Label ID="Label19" CssClass="LabelFont" runat="server" Text="Gross Margin" AssociatedControlID="txtgrossmargin"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtgrossmargin" CssClass="textboxLabelQuotes" runat="server" ReadOnly="true"></asp:TextBox>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtQuote" CssClass="textboxLabelQuotes" runat="server" Style="resize: none;"
                                            TextMode="MultiLine" Height="130px" Width="470px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gridQuoteNumber" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td id="tdCusInfo" runat="server">
                    <asp:UpdatePanel ID="UpdateQuotes" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table style="border: #ccc solid 1px; background-color: White;">
                                                        <tr>
                                                            <td style="background-color: #d6e0ec;">
                                                                <asp:Label ID="Label3" Font-Bold="True" CssClass="LabelFont" runat="server" Text="Contact Information"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblName" CssClass="LabelFont" ForeColor="Blue" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label5" runat="server" CssClass="LabelFont" Text="# of Quotes" AssociatedControlID="txtNoOfQuotes"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNoOfQuotes" CssClass="textboxLabelQuotes" runat="server" Width="60px"
                                                                                ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label6" runat="server" CssClass="LabelFont" Text="# of Quote Converted"
                                                                                AssociatedControlID="txtQuoteConverted"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtQuoteConverted" CssClass="textboxLabelQuotes" runat="server"
                                                                                Width="60px" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label15" runat="server" CssClass="LabelFont" Text="% of Conversion"
                                                                                AssociatedControlID="txtQuoteConverted"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPercentConversion" CssClass="textboxLabelQuotes" runat="server"
                                                                                Width="60px" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="border: #ccc solid 1px; background-color: White;">
                                                        <tr>
                                                            <td style="background-color: #d6e0ec;">
                                                                <asp:Label ID="Label7" Font-Bold="True" CssClass="LabelFont" runat="server" Text="Site Information"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label8" runat="server" CssClass="LabelFont" Text="# of Quotes" AssociatedControlID="txtNoOfQuotes0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNoOfQuotes0" CssClass="textboxLabelQuotes" runat="server" Width="60px"
                                                                                ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label17" runat="server" CssClass="LabelFont" Text="# of Quote Converted"
                                                                                AssociatedControlID="txtQuoteConverted0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtQuoteConverted0" CssClass="textboxLabelQuotes" runat="server"
                                                                                Width="60px" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label9" runat="server" CssClass="LabelFont" Text="% of Conversion"
                                                                                AssociatedControlID="txtQuoteConverted0"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPercentConversion0" CssClass="textboxLabelQuotes" runat="server"
                                                                                Width="60px" ReadOnly="true"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gridQuoteNumber" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <table style="height: 25px; width: 1095px;">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <div class="GridHeaderLabel">
                                        <asp:Label Font-Bold="true" Font-Size="12px" ID="Label20" runat="server" Text="Quotes Details"></asp:Label></div>
                                </td>
                                <td>
                                    <asp:Label ID="Label21" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-wrapper page1">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlOrderHistory" runat="server" ScrollBars="Auto">
                                                <asp:GridView AutoGenerateColumns="true" ID="gridQuotes" AllowSorting="true" runat="server"
                                                    BackColor="#FFFFFF" GridLines="None" ForeColor="Black" CellPadding="4" CellSpacing="2"
                                                    EmptyDataText="No data available." AllowPaging="false" Font-Size="12px" OnRowDataBound="gridQuotes_RowDataBound"
                                                    OnSorting="gridQuotes_Sorting" ClientIDMode="Static">
                                                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                    <AlternatingRowStyle BackColor="#e5e5e5" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gridQuoteNumber" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnlPopupQuotesDetails" runat="server" Style="display: none;" ClientIDMode="Static">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteDocNoDetails" CssClass="LabelFont" runat="server" Text="Document No"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteDocNoDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteLineDetails" CssClass="LabelFont" runat="server" Text="Line No"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteLineDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteDateDetails" CssClass="LabelFont" Width="100%" runat="server"
                        Text="Date"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteDateDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuotePOTypeDetails" CssClass="LabelFont" runat="server" Text="PO Type"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuotePOTypeDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuotePOTypeDescDetails" CssClass="LabelFont" runat="server" Text="PO Type Description"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuotePOTypeDescDetails" runat="server" CssClass="textboxLabel"
                        Style="font-size: 12px; width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteItemCategDescDetails" CssClass="LabelFont" runat="server"
                        Text="Item Category Description"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteItemCategDescDetails" runat="server" CssClass="textboxLabel"
                        Style="font-size: 12px; width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteSisTeamINDetails" CssClass="LabelFont" runat="server" Text="Sales Team"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteSisTeamINDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDMProductLineDescDetails" CssClass="LabelFont" runat="server" Text="DM Product Line Description"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtDMProductLineDescDetails" runat="server" CssClass="textboxLabel"
                        Style="font-size: 12px; width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteQtyDetails" CssClass="LabelFont" runat="server" Text="Qty"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteQtyDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteMatEntrdDetails" CssClass="LabelFont" runat="server" Text="Material Entered"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteMatEntrdDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteMatEntrdDescDetails" CssClass="LabelFont" runat="server" Text="Material Entered Description"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteMatEntrdDescDetails" runat="server" CssClass="textboxLabel"
                        Style="font-size: 12px; width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteDiscountDetails" CssClass="LabelFont" runat="server" Text="Discount"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteDiscountDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteNetSalesDetails" CssClass="LabelFont" runat="server" Text="Net Sales"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteNetSalesDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteUnitPriceDetails" CssClass="LabelFont" runat="server" Text="Unit Price"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteUnitPriceDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuoteCostDetails" CssClass="LabelFont" runat="server" Text="Cost"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQuoteCostDetails" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="CurrencyCode" runat="server" ClientIDMode="Static" Value="" />
</asp:Content>
