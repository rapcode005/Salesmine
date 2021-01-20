<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master" AutoEventWireup="true" CodeBehind="Splash.aspx.cs" Inherits="WebSalesMine.WebPages.Home.Splash" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
 <!-- SPLASH -->
    <div id="wrapper">
    <div id="container">
    	
        <div id="splash">
        	<div class="splash-header">Let's get Started.</div>
			<ul id="splash-list">
            	<li class="item curved order-history">
                	<div class="name">Order History</div>
                    <div class="desc">Order line details for the last 5 years</div>
                </li>
                <li class="item curved notes">
                	<div class="name">Notes</div>
                    <div class="desc">SalesMine and SAP tickler notes history</div>
                </li>
                <li class="item curved product-summary">
                	<div class="name">Product Summary</div>
                    <div class="desc">Summarized view of customer activity by product categories for the last 3 years</div>
                </li>
                <li class="item curved quotes">
                	<div class="name">Quotes</div>
                    <div class="desc">Detailed quote history by customer</div>
                </li>
                <li class="item curved customer-information">
                	<div class="name">Customer Information</div>
                    <div class="desc">Detailed information about the company (industry, lifetime sales, etc.) and contacts (job function, phone extension, etc.)</div>
                </li>
                <li class="item curved customer-search">
                	<div class="name">Customer Search</div>
                    <div class="desc">Allows you to search by customer name,<br />phone number, email address and first order date</div>
                </li>
                <div class="clear"></div>
            </ul>                    
        </div>
        
    </div><!-- End Container -->
    	<div id="push"></div>
    </div>
    <!-- End OBJECTS -->
    
    <div id="footer">
    	Copyright March 2012 © by Brady Philippines Direct Marketing Inc. All rights reserved.
    </div>
</asp:Content>
