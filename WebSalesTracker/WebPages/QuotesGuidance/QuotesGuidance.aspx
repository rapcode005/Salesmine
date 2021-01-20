<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="QuotesGuidance.aspx.cs" Inherits="WebSalesMine.WebPages.QuoteGuidance.QuotesGuidance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
    <script language="javascript" type="text/javascript">
        function Message() {
            alert('No Record Matched');
        }
    </script>
    <div id="container">
        <asp:Panel ID="pnlResult" runat="server" DefaultButton="btnSearch">
            <table style="border: #ccc solid 1px; background-color: White;">
                <tr>
                    <td style="background-color: #d6e0ec;">
                        <asp:Label ID="lblQuoteGuidanceHeader" Font-Bold="True" CssClass="LabelFont" runat="server"
                            Text="Quote Discount Guidance"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="border: #ccc solid 1px; background-color: White;">
                                    <tr>
                                        <td style="background-color: #d6e0ec;">
                                            <asp:Label ID="lblSearchby" Font-Bold="True" CssClass="LabelFont" runat="server"
                                                Text="Search By"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td id="Td1" class="object-wrapper" style="height: 20px" runat="server">
                                                        <asp:RadioButton ID="rdoSearchByQuoteNo" CssClass="LabelFont" ClientIDMode="Static"
                                                            AutoPostBack="true" runat="server" GroupName="rdoSearchBy" Text=" Quote Number"
                                                            OnCheckedChanged="rdoSearchByQuoteNo_CheckedChanged" />
                                                    </td>
                                                    <td id="Td2" class="object-wrapper" style="height: 20px" runat="server">
                                                        <asp:RadioButton ID="rdoSearchByCustomerNo" CssClass="LabelFont" ClientIDMode="Static"
                                                            runat="server" Text=" Customer Number" GroupName="rdoSearchBy" AutoPostBack="true"
                                                            OnCheckedChanged="rdoSearchByCustomerNo_CheckedChanged" />
                                                    </td>
                                                    <td id="Td4" class="object-wrapper" style="height: 20px" runat="server">
                                                        <asp:RadioButton ID="rdoSearchByContactNumber" CssClass="LabelFont" ClientIDMode="Static"
                                                            runat="server" Text=" Contact Number" GroupName="rdoSearchBy" AutoPostBack="true"
                                                            OnCheckedChanged="rdoSearchByContactNo_CheckedChanged" />
                                                    </td>
                                                    <td id="Td3" class="object-wrapper" style="height: 20px" runat="server">
                                                        <asp:RadioButton ID="rdoSearchBy4Categories" CssClass="LabelFont" ClientIDMode="Static"
                                                            runat="server" Text=" 4 Categories" GroupName="rdoSearchBy" AutoPostBack="true"
                                                            OnCheckedChanged="rdoSearchBy4Categories_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 7px; padding-bottom: 5px;">
                                            <asp:Label ID="lblInstruction" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <table style="padding 10px;">
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lblCustomerNumber" CssClass="LabelFont" runat="server" Text="Customer Number"></asp:Label>
                                                                <asp:Label ID="lblCustomerNumberReq" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                    Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCustomerNumber" CssClass="textbox curved" Style="font-size: 12px;"
                                                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="ftbeCustomerNumber" runat="server" TargetControlID="txtCustomerNumber"
                                                                    FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                                <asp:Label ID="lblCustomerNameValue" runat="server" CssClass="textboxLabel" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lblContactNumber" CssClass="LabelFont" runat="server" Text="Contact Number"></asp:Label>
                                                                <asp:Label ID="lblContactNumberReq" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                    Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtContactNumber" CssClass="textbox curved" Style="font-size: 12px;"
                                                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="ftbeContactNumber" runat="server" TargetControlID="txtContactNumber"
                                                                    FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                                <asp:Label ID="lblContactNameValue" runat="server" CssClass="textboxLabel" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lbl" CssClass="LabelFont" runat="server" Text="Quote Number"></asp:Label>
                                                                <asp:Label ID="lblQuoteNumberReq" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                    Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtQuoteNumber" CssClass="textbox curved" Style="font-size: 12px;"
                                                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="ftbeQuoteNumber" runat="server" TargetControlID="txtQuoteNumber"
                                                                    FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lblCustType" CssClass="LabelFont" runat="server" Text="Customer Type"></asp:Label>
                                                                <asp:Label ID="lblCustTypeReq" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                    Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCustType" runat="server" ClientIDMode="Static" CssClass="textbox curved"
                                                                    Style="font-size: 12px; width: 230px;">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lblProdLine" CssClass="LabelFont" runat="server" Text="Product Line"></asp:Label>
                                                                <asp:Label ID="lblProdLineReq" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                    Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlProdLine" runat="server" ClientIDMode="Static" CssClass="textbox curved"
                                                                    Style="font-size: 12px; width: 230px;">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr id="tdMaterial" runat="server">
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lblMaterialEntrd" CssClass="LabelFont" runat="server" Text="SKU Product Code"></asp:Label>
                                                                <asp:Label ID="lblMaterialEntrdReq" runat="server" CssClass="LabelFont" ForeColor="Red" Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMaterialEntrd" CssClass="textbox curved" Style="font-size: 12px;  width: 223px;" ClientIDMode="Static"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lblQuoteType" CssClass="LabelFont" runat="server" Text="Quote Type"></asp:Label>
                                                                <asp:Label ID="lblQuoteTypeReq" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                    Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlQuoteType" runat="server" ClientIDMode="Static" CssClass="textbox curved"
                                                                    Style="font-size: 12px; width: 230px;">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Label ID="lblQuoteBucket" CssClass="LabelFont" runat="server" Text="Quote Bucket"></asp:Label>
                                                                <asp:Label ID="lblQuoteBucketReq" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                    Text="*"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlQuoteBucket" runat="server" ClientIDMode="Static" CssClass="textbox curved"
                                                                    Style="font-size: 12px; width: 230px;">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="rdoSearchByQuoteNo" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="rdoSearchByCustomerNo" EventName="CheckedChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="rdoSearchBy4Categories" EventName="CheckedChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="rdoSearchByQuoteNo" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="rdoSearchByCustomerNo" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="rdoSearchBy4Categories" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="rdoSearchByContactNumber" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 10px;">
                        <asp:UpdatePanel ID="UpdateResult" runat="server">
                            <ContentTemplate>
                                <table style="border: #ccc solid 1px; background-color: White; width: 100%;">
                                    <tr>
                                        <td style="background-color: #d6e0ec;">
                                            <asp:Label ID="Label1" Font-Bold="True" CssClass="LabelFont" runat="server" Text="Result"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr id="tdReseller" runat="server">
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblReseller" CssClass="LabelFont" runat="server" Text="Reseller"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblResellerValue" CssClass="textboxLabel" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tdGovernment" runat="server">
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblGovernment" CssClass="LabelFont" runat="server" Text="Government"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGovernmentValue" CssClass="textboxLabel" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tdSuccessRateSite" runat="server">
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblSuccessRateSite" CssClass="LabelFont" runat="server" Text="Success Rate Site"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSuccessRateSiteValue" CssClass="textboxLabel" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tdSuccessRateContact" runat="server">
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblSuccessRateContact" CssClass="LabelFont" runat="server" Text="Success Rate Contact"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSuccessRateContactValue" CssClass="textboxLabel" runat="server"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblAverageQuoteDiscount" CssClass="LabelFont" runat="server" Text="Average Quote Discount"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAverageQuoteDiscountValue" CssClass="textboxLabel" runat="server"
                                                            ClientIDMode="Static" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblAverageOrderDiscount" CssClass="LabelFont" runat="server" Text="Average Order Discount"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAverageOrderDiscountValue" CssClass="textboxLabel" runat="server"
                                                            ClientIDMode="Static" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblNumQuotes" CssClass="LabelFont" runat="server" Text="# Quotes"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNumQuotesValue" CssClass="textboxLabel" runat="server" ClientIDMode="Static"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblNumOrders" CssClass="LabelFont" runat="server" Text="# Orders"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNumOrdersValue" CssClass="textboxLabel" runat="server" ClientIDMode="Static"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblCloseRate" CssClass="LabelFont" runat="server" Text="Close Rate"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCloseRateValue" CssClass="textboxLabel" runat="server" ClientIDMode="Static"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblGMQuote" CssClass="LabelFont" runat="server" Text="GM Quote"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGMQuoteValue" CssClass="textboxLabel" runat="server" ClientIDMode="Static"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="lblGmOrder" CssClass="LabelFont" runat="server" Text="GM Order"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGmOrderValue" CssClass="textboxLabel" runat="server" ClientIDMode="Static"
                                                            Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 10px; padding-bottom: 10px;">
                        <table style="float: right;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" ClientIDMode="Static" CssClass="button"
                                        Text="Search" Style="font-size: 12px;" Width="60px" OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnClear" runat="server" ClientIDMode="Static" CssClass="button"
                                        Text="Clear" Style="font-size: 12px;" Width="60px" OnClick="btnClear_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
