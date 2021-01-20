<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebSalesMine.WebPages.Home.Main"
    MasterPageFile="../UserControl/NewMasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="Server">
    <%--    <table>
        <tr>
            <td>
                <div style="text-align: center">
                    <table style="width: 90%" border="0">
                        <tr>
                            <td style="width: 20px; height: 20px;">
                            </td>
                            <td>
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td>
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td>
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td>
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td>
                            </td>
                            <td style="width: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;">
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td rowspan="10">
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td rowspan="10">
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td rowspan="10">
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td rowspan="10">
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td style="width: 16%;">
                            </td>
                        </tr>
                        <tr>
                            <td>
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
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td rowspan="10">
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                            <td style="padding-right: 12px;" align="center">
                            </td>
                            <td style="padding-right: 12px;" align="center">
                            </td>
                            <td style="padding-right: 12px;" align="center">
                            </td>
                            <td style="padding-right: 12px;" align="center">
                            </td>
                            <td style="padding-right: 12px;" align="center">
                            </td>
                            <td style="padding-right: 12px;" align="center">
                            </td>
                            <td style="padding-right: 12px;" align="center">
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td rowspan="10">
                            </td>
                            <td style="width: 16%;">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20px; height: 20px;">
                            </td>
                            <td>
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td>
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td>
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td>
                            </td>
                            <td style="width: 2px">
                            </td>
                            <td>
                            </td>
                            <td style="width: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="center">
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
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 240px">
                            </td>
                            <td style="padding-right: 12px;" align="center">
                            </td>
                            <td style="padding-right: 5px;">
                            </td>
                            <td style="padding-right: 5px;">
                            </td>
                            <td style="padding-right: 7px;">
                            </td>
                            <td style="padding-right: 9px;">
                            </td>
                            <td style="padding-right: 9px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;">
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
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </td> </tr> </table>--%>
   <%-- <table border="0" cellpadding="0" cellspacing="0" class="object-container" width=""
        height="100%">
        <tr>
            <td class="object-wrapper">
                <a href="#" class="link">This is a Link</a>
            </td>
            <td class="object-wrapper">
                <input type="checkbox" checked="checked" class="checkBox" />This is a Check Box
            </td>
            <td class="object-wrapper">
                <input type="radio" class="radioButton" />This is a Radio Button
            </td>
            <td class="object-wrapper">
                <img src="../../App_Themes/Images/New Design/divider-2.png" width="1" height="25"
                    border="0" />
            </td>
            <td class="object-wrapper" style="padding-right: 0;">
                <input type="text" class="textbox curved" />
            </td>
            <td class="object-wrapper" style="padding-left: 0;">
                <input type="image" src="../../App_Themes/Images/New Design/calendar-icon.png" />
            </td>
        </tr>
    </table>--%>


        <!-- SPLASH -->
    <div id="wrapper">
    <div id="container">
    	
        <div id="splash">
        	<div class="splash-header">Let's get Started.</div>
			<ul id="splash-list">
            	<li class="item curved order-history"><a href="../OrderHistory/OrderHistory.aspx" style=" text-decoration:None" onclick="OrderHistoryClick()">
                	<div class="name">Order History</div>
                    <div class="desc">Order line details for the last 5 years</div></a>
                </li>
                <li class="item curved notes"><a href="../NotesCommHistory/NotesCommHistory.aspx" style=" text-decoration:None" onclick="NotesClick()">
                	<div class="name">Notes</div>
                    <div class="desc">SalesMine and SAP tickler notes history</div></a>
                </li>
                <li class="item curved product-summary"><a href="../ProductSummary/ProductSummary.aspx" style=" text-decoration:None" onclick="productClick()">
                	<div class="name">Product Summary</div>
                    <div class="desc">Summarized view of customer activity <br /> by product categories for the last 3 years</div></a>
                </li>
                 <%if (campaign != "PC-MAN")
                              { %>
                  <li id="liCustomerSearch" runat="server" class="item curved customer-search"><a href="../CustomerLookUp/CustomerLookUp.aspx" style=" text-decoration:None" onclick="CustomerLookupClick()">
                	<div class="name">Customer Search</div>
                    <div class="desc">Allows you to search by customer name,<br />phone number, email address and first order date</div></a>
                </li>
              <% }%>
                <li  <%if (campaign != "PC-MAN")
                              { %>class="item curved customer-information" <% }%> <%if (campaign == "PC-MAN")
                              { %> class="item curved customer-search"<% }%> ><a href="../SiteAndContactInfo/SiteAndContactInfo.aspx" style=" text-decoration:None" onclick="CustomerClick()">
                	<div class="name">Customer Information</div>
                    <div class="desc">Detailed information about the company <br /> (industry, lifetime sales, etc.) and <br /> contacts (job function, phone extension, etc.)</div></a>
                </li>
                 <%if (campaign != "PC-MAN" && campaign != "PC-ONT")
                   { %>
                 <li id="liQuotes" runat="server" class="item curved quotes"><a href="../Quotes/Quotes.aspx" style=" text-decoration:None" onclick="QuoteClick()">
                	<div class="name">Quotes</div>
                    <div class="desc">Detailed quote history by customer</div></a>
                </li>
                <% }%>
                <div class="clear"></div>
            </ul>                    
        </div>
        
    </div><!-- End Container -->
    	<div id="push"></div>
    </div>
    <!-- End OBJECTS -->
    
    <%--<div id="footer">
    	Copyright March 2012 © by Brady Philippines Direct Marketing Inc. All rights reserved.
    </div>--%>
</asp:Content>
