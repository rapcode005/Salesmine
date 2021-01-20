<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" EnableEventValidation="false"
    Inherits="WebSalesMine.WebPages.OrderHistory.OrderHistory" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master" %>

<%@ MasterType VirtualPath="~/WebPages/UserControl/NewMasterPage.Master" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="T2" runat="server" ContentPlaceHolderID="container">
    <style type="text/css">
        .FixedPostion
        {
            position: fixed;
        }
    </style>
    <script type='text/javascript' src="../../App_Themes/JS/accounting.js"></script>
    <%-- <script src="../../App_Themes/JS/JsGrid/jquery.tablesorter.pager.js" type="text/javascript"></script>--%>
    <script type="text/javascript">

        function HideColumn() {
            //var $tbl = $("#gridOrderHistory");
            $(function () {
                //Hide Column
                var thOrderType = $("#gridOrderHistory th:contains('Order_type')");
                thOrderType.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                var thUvals = $("#gridOrderHistory th:contains('uvals')");
                thUvals.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thUvals.index()).css("display", "none");
                });

                var thReRej = $("#gridOrderHistory th:contains('ReRej')");
                thReRej.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thReRej.index()).css("display", "none");
                });

                var thaccount = $("#gridOrderHistory th:contains('accountnum')");
                thaccount.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thaccount.index()).css("display", "none");
                });

                var thorderDate = $("#gridOrderHistory th:contains('order_date')");
                thorderDate.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thorderDate.index()).css("display", "none");
                });

                var thpartnum = $("#gridOrderHistory th:contains('part_number')");
                thpartnum.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thpartnum.index()).css("display", "none");
                });

                var thDesc = $("#gridOrderHistory th:contains('Descrp')");
                thDesc.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thDesc.index()).css("display", "none");
                });

                var thunitprice = $("#gridOrderHistory th:contains('unit_price')");
                thunitprice.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thunitprice.index()).css("display", "none");
                });

                var thextprice = $("#gridOrderHistory th:contains('ext_price')");
                thextprice.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thextprice.index()).css("display", "none");
                });

                var thquantity = $("#gridOrderHistory th:contains('quantity')");
                thquantity.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thquantity.index()).css("display", "none");
                });

                var thordernum = $("#gridOrderHistory th:contains('order_num')");
                thordernum.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thordernum.index()).css("display", "none");
                });

                var thlne = $("#gridOrderHistory th:contains('lne')");
                thlne.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thlne.index()).css("display", "none");
                });

                var thconvdate = $("#gridOrderHistory th:contains('conv_date')");
                thconvdate.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thconvdate.index()).css("display", "none");
                });

                var thorderblk = $("#gridOrderHistory th:contains('order_blk')");
                thorderblk.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thorderblk.index()).css("display", "none");
                });

                var thcuspo = $("#gridOrderHistory th:contains('cus_po')");
                thcuspo.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thcuspo.index()).css("display", "none");
                });

                var thcusname = $("#gridOrderHistory th:contains('cus_name')");
                thcusname.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thcusname.index()).css("display", "none");
                });

                var thorigacc = $("#gridOrderHistory th:contains('orig_acc')");
                thorigacc.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thorigacc.index()).css("display", "none");
                });

                var thconnum = $("#gridOrderHistory th:contains('con_num')");
                thconnum.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thconnum.index()).css("display", "none");
                });

                var thlstname = $("#gridOrderHistory th:contains('lst_name')");
                thlstname.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thlstname.index()).css("display", "none");
                });

                var thfrtname = $("#gridOrderHistory th:contains('frt_name')");
                thfrtname.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thfrtname.index()).css("display", "none");
                });

                var themladdr = $("#gridOrderHistory th:contains('eml_addr')");
                themladdr.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(themladdr.index()).css("display", "none");
                });

                var thphonenum = $("#gridOrderHistory th:contains('phone_num')");
                thphonenum.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thphonenum.index()).css("display", "none");
                });

                var thshipname = $("#gridOrderHistory th:contains('ship_name')");
                thshipname.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipname.index()).css("display", "none");
                });

                var thshipmail = $("#gridOrderHistory th:contains('ship_mail')");
                thshipmail.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipmail.index()).css("display", "none");
                });

                var thshipcity = $("#gridOrderHistory th:contains('ship_city')");
                thshipcity.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipcity.index()).css("display", "none");
                });

                var thshipzip = $("#gridOrderHistory th:contains('ship_zip')");
                thshipzip.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipzip.index()).css("display", "none");
                });

                var thshipstate = $("#gridOrderHistory th:contains('ship_state')");
                thshipstate.css("display", "none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipstate.index()).css("display", "none");
                });
            });
        }


        function OnSuccess(Data) {
            var RowIndex = Data;
            var grd = document.getElementById('gridOrderHistory');
            var AccountNumber, OrderDate, PartNumber, Description, UnitPrice, Qty, ExtPrice, OrderNumber, Line, OrderType, ConvertedDate, ReasonforRejection, OrderBlock
            var CustomerPO, CustomerName, ORIGAccount, ContactNumber, LastName, FirstName, EmailAddress, Phone, ShipName, ShipMailing, ShipCity, ShipZip, ShipState
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            var myDate;

            if (grd) {
                if (isNaN(RowIndex) == false) {
                    totalCols = $("#gridOrderHistory").find('tr')[0].cells.length;
                    for (var i = 1; i < totalCols + 1; i++) {
                        var ColName = $('#gridOrderHistory tr').find('th:nth-child(' + i + ')').text();

                        if (ColName == 'ACCOUNT NUMBER' || ColName == 'accountnum') {
                            if (grd.rows[RowIndex].cells[i - 1]) {
                                AccountNumber = grd.rows[RowIndex].cells[i - 1].innerHTML;
                                if (AccountNumber == undefined || AccountNumber == '&nbsp;') {
                                    AccountNumber = '';
                                }
                            }
                            else
                                AccountNumber = '';
                        }
                        else if (ColName == 'ORDER DATE' || ColName == 'order_date') {
                            if (grd.rows[RowIndex].cells[i - 1]) {
                                OrderDate = grd.rows[RowIndex].cells[i - 1].innerHTML;
                                if (OrderDate == undefined || OrderDate == '&nbsp;') {
                                    OrderDate = '';
                                }
                            }
                            else
                                OrderDate = '';
                        }
                        else if (ColName == 'PART NUMBER' || ColName == 'part_number') {
                            if (grd.rows[RowIndex].cells[i - 1]) {
                                PartNumber = grd.rows[RowIndex].cells[i - 1].innerHTML;
                                if (PartNumber == undefined || PartNumber == '&nbsp;') {
                                    PartNumber = '';
                                }
                            }
                            else
                                PartNumber = '';
                        }
                        else if (ColName == 'DESCRIPTION' || ColName == 'Descrp') {
                            if (grd.rows[RowIndex].cells[i - 1]) {
                                Description = grd.rows[RowIndex].cells[i - 1].innerHTML;
                                if (Description == undefined || Description == '&nbsp;') {
                                    Description = '';
                                }
                            }
                            else
                                Description = '';
                        }
                        else if (ColName == 'UNIT PRICE' || ColName == 'unit_price') {

                            UnitPrice = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (UnitPrice == undefined || UnitPrice == '&nbsp;') {
                                UnitPrice = '';
                            }
                        }
                        else if (ColName == 'QTY' || ColName == 'quantity') {

                            Qty = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (Qty == undefined || Qty == '&nbsp;') {
                                Qty = '';
                            }
                        }
                        else if (ColName == 'EXT PRICE' || ColName == 'ext_price') {

                            ExtPrice = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ExtPrice == undefined || ExtPrice == '&nbsp;') {
                                ExtPrice = '';
                            }
                        }
                        else if (ColName == 'ORDER NUMBER' || ColName == 'order_num') {

                            OrderNumber = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (OrderNumber == undefined || OrderNumber == '&nbsp;') {
                                OrderNumber = '';
                            }
                        }

                        else if (ColName == 'LINE' || ColName == 'lne') {

                            Line = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (Line == undefined || Line == '&nbsp;') {
                                Line = '';
                            }
                        }
                        else if (ColName == 'ORDER TYPE' || ColName == 'Order_type') {

                            OrderType = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (OrderType == undefined || OrderType == '&nbsp;') {
                                OrderType = '';
                            }
                        }
                        else if (ColName == 'CONVERTED DATE' || ColName == 'conv_date') {

                            ConvertedDate = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ConvertedDate == undefined || ConvertedDate == '&nbsp;') {
                                ConvertedDate = '';
                            }
                        }
                        else if (ColName == 'REASON FOR REJECTION' || ColName == 'ReRej') {

                            ReasonforRejection = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ReasonforRejection == undefined || ReasonforRejection == '&nbsp;') {
                                ReasonforRejection = '';
                            }
                        }
                        else if (ColName == 'ORDER BLOCK' || ColName == 'order_blk') {

                            OrdeBlock = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (OrdeBlock == undefined || OrdeBlock == '&nbsp;') {
                                OrdeBlock = '';
                            }
                        }
                        else if (ColName == 'CUSTOMER PO' || ColName == 'cus_po') {

                            CustomerPO = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CustomerPO == undefined || CustomerPO == '&nbsp;') {
                                CustomerPO = '';
                            }
                        }
                        else if (ColName == 'CUSTOMER NAME' || ColName == 'cus_name') {

                            CustomerName = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CustomerName == undefined || CustomerName == '&nbsp;') {
                                CustomerName = '';
                            }
                        }
                        else if (ColName == 'ORIG ACCOUNT' || ColName == 'orig_acc') {

                            ORIGAccount = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ORIGAccount == undefined || ORIGAccount == '&nbsp;') {
                                ORIGAccount = '';
                            }
                        }
                        else if (ColName == 'CONTACT NUMBER' || ColName == 'con_num') {

                            ContactNumber = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ContactNumber == undefined || ContactNumber == '&nbsp;') {
                                ContactNumber = '';
                            }
                        }
                        else if (ColName == 'LAST NAME' || ColName == 'lst_name') {

                            LastName = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (LastName == undefined || LastName == '&nbsp;') {
                                LastName = '';
                            }
                        }
                        else if (ColName == 'FIRST NAME' || ColName == 'frt_name') {

                            FirstName = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (FirstName == undefined || FirstName == '&nbsp;') {
                                FirstName = '';
                            }
                        }
                        else if (ColName == 'EMAIL ADDRESS' || ColName == 'eml_addr') {

                            EmailAddress = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (EmailAddress == undefined || EmailAddress == '&nbsp;') {
                                EmailAddress = '';
                            }
                        }
                        else if (ColName == 'PHONE' || ColName == 'phone_num') {

                            Phone = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (Phone == undefined || Phone == '&nbsp;') {
                                Phone = '';
                            }
                        }
                        else if (ColName == 'SHIP NAME' || ColName == 'ship_name') {

                            ShipName = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ShipName == undefined || ShipName == '&nbsp;') {
                                ShipName = '';
                            }
                        }
                        else if (ColName == 'SHIP MAILING' || ColName == 'ship_mail') {

                            ShipMailing = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ShipMailing == undefined || ShipMailing == '&nbsp;') {
                                ShipMailing = '';
                            }
                        }
                        else if (ColName == 'SHIP CITY' || ColName == 'ship_city') {

                            ShipCity = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ShipCity == undefined || ShipCity == '&nbsp;') {
                                ShipCity = '';
                            }
                        }
                        else if (ColName == 'SHIP ZIP' || ColName == 'ship_zip') {

                            ShipZip = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ShipZip == undefined || ShipZip == '&nbsp;') {
                                ShipZip = '';
                            }
                        }
                        else if (ColName == 'SHIP STATE' || ColName == 'ship_state') {

                            ShipState = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ShipState == undefined || ShipState == '&nbsp;') {
                                ShipState = '';
                            }
                        }

                    }
                }
            }



            if (FIREFOX) {

                if (AccountNumber != undefined && AccountNumber != '&nbsp;') {
                    $get('txtAccountNumber1').textContent = AccountNumber;
                }
                else {
                    $get('txtAccountNumber1').textContent = '';
                }

                if (OrderDate != undefined && OrderDate != '&nbsp;') {
                    $get('txtOrderDate').textContent = OrderDate;
                }
                else {
                    $get('txtOrderDate').textContent = '';
                }

                if (Description != undefined && Description != '&nbsp;') {
                    $get('txtDescription').textContent = Description;
                }
                else {
                    $get('txtDescription').textContent = '';
                }

                if (OrderNumber != undefined && OrderNumber != '&nbsp;') {
                    $get('txtOrderNumber').textContent = OrderNumber;
                }
                else {
                    $get('txtOrderNumber').textContent = '';
                }

                if (ReasonforRejection != undefined && ReasonforRejection != '&nbsp;') {
                    $get('txtReasonRejection').textContent = ReasonforRejection;
                }
                else {
                    $get('txtReasonRejection').textContent = '';
                }

                if (ConvertedDate != undefined && ConvertedDate != '&nbsp;') {
                    $get('txtConvertedDate').textContent = ConvertedDate;
                }
                else {
                    $get('txtConvertedDate').textContent = '';
                }

                if (OrderType != undefined && OrderType != '&nbsp;') {
                    $get('txtOrderType').textContent = OrderType;
                }
                else {
                    $get('txtOrderType').textContent = '';
                }

                if (Line != undefined && Line != '&nbsp;') {
                    $get('txtLine').textContent = Line;
                }
                else {
                    $get('txtLine').textContent = '';
                }

                if (ExtPrice != undefined && ExtPrice != '&nbsp;') {
                    $get('txtExtPriceOrder').textContent = ExtPrice.replace('&nbsp;', ' ');
                }
                else {
                    $get('txtExtPriceOrder').textContent = '';
                }

                if (UnitPrice != undefined && UnitPrice != '&nbsp;') {
                    $get('txtUnitprice').textContent = UnitPrice;
                }
                else {
                    $get('txtUnitprice').textContent = '';
                }


                if (Qty != undefined && Qty != '&nbsp;') {
                    $get('txtQty').textContent = Qty;
                }
                else {
                    $get('txtQty').textContent = '';
                }

                if (PartNumber != undefined && PartNumber != '&nbsp;') {
                    $get('txtPartNumber').textContent = PartNumber;
                }
                else {
                    $get('txtPartNumber').textContent = '';
                }

                if (OrderBlock != undefined && OrderBlock != '&nbsp;') {
                    $get('txtOrderBlock').textContent = OrderBlock;
                }
                else {
                    $get('txtOrderBlock').textContent = '';
                }

                if (CustomerPO != undefined && CustomerPO != '&nbsp;') {
                    $get('txtCustomerPOLabel').textContent = CustomerPO;
                }
                else {
                    $get('txtCustomerPOLabel').textContent = '';
                }

                if (CustomerName != undefined && CustomerName != '&nbsp;') {
                    $get('txtCustomerName').textContent = CustomerName;
                }
                else {
                    $get('txtCustomerName').textContent = '';
                }

                if (ORIGAccount != undefined && ORIGAccount != '&nbsp;') {
                    $get('txtORIGAccount').textContent = ORIGAccount;
                }
                else {
                    $get('txtORIGAccount').textContent = '';
                }

                if (ContactNumber != undefined && ContactNumber != '&nbsp;') {
                    $get('txtContactNumber').textContent = ContactNumber;
                }
                else {
                    $get('txtContactNumber').textContent = '';
                }

                if (LastName != undefined && LastName != '&nbsp;') {
                    $get('txtLastName').textContent = LastName;
                }
                else {
                    $get('txtLastName').textContent = '';
                }


                if (FirstName != undefined && FirstName != '&nbsp;') {
                    $get('txtFirstName').textContent = FirstName;
                }
                else {
                    $get('txtFirstName').textContent = '';
                }

                if (EmailAddress != undefined && EmailAddress != '&nbsp;') {
                    $get('txtEmailAddress').textContent = EmailAddress;
                }
                else {
                    $get('txtEmailAddress').textContent = '';
                }

                if (Phone != undefined && Phone != '&nbsp;') {
                    $get('txtPhone').textContent = Phone;
                }
                else {
                    $get('txtPhone').textContent = '';
                }

                if (ShipName != undefined && ShipName != '&nbsp;') {
                    $get('txtShipName').textContent = ShipName;
                }
                else {
                    $get('txtShipName').textContent = '';
                }


                if (ShipMailing != undefined && ShipMailing != '&nbsp;') {
                    $get('txtShipMailing').textContent = ShipMailing;
                }
                else {
                    $get('txtShipMailing').textContent = '';
                }

                if (ShipCity != undefined && ShipCity != '&nbsp;') {
                    $get('txtShipCity').textContent = ShipCity;
                }
                else {
                    $get('txtShipCity').textContent = '';
                }

                if (ShipZip != undefined && ShipZip != '&nbsp;') {
                    $get('txtShipZip').textContent = ShipZip;
                }
                else {
                    $get('txtShipZip').textContent = '';
                }

                if (ShipState != undefined && ShipState != '&nbsp;') {
                    $get('txtShipState').textContent = ShipState;
                }
                else {
                    $get('txtShipState').textContent = '';
                }

            }
            else {

                if (AccountNumber != undefined && AccountNumber != '&nbsp;') {
                    $get('txtAccountNumber1').innerText = AccountNumber;
                }
                else {
                    $get('txtAccountNumber1').innerText = '';
                }

                if (OrderDate != undefined && OrderDate != '&nbsp;') {
                    $get('txtOrderDate').innerText = OrderDate;
                }
                else {
                    $get('txtOrderDate').innerText = '';
                }

                if (Description != undefined && Description != '&nbsp;') {
                    $get('txtDescription').innerText = Description;
                }
                else {
                    $get('txtDescription').innerText = '';
                }

                if (OrderNumber != undefined && OrderNumber != '&nbsp;') {
                    $get('txtOrderNumber').innerText = OrderNumber;
                }
                else {
                    $get('txtOrderNumber').innerText = '';
                }

                if (ReasonforRejection != undefined && ReasonforRejection != '&nbsp;') {
                    $get('txtReasonRejection').innerText = ReasonforRejection;
                }
                else {
                    $get('txtReasonRejection').innerText = '';
                }

                if (ConvertedDate != undefined && ConvertedDate != '&nbsp;') {
                    $get('txtConvertedDate').innerText = ConvertedDate;
                }
                else {
                    $get('txtConvertedDate').innerText = '';
                }

                if (OrderType != undefined && OrderType != '&nbsp;') {
                    $get('txtOrderType').innerText = OrderType;
                }
                else {
                    $get('txtOrderType').innerText = '';
                }

                if (Line != undefined && Line != '&nbsp;') {
                    $get('txtLine').innerText = Line;
                }
                else {
                    $get('txtLine').innerText = '';
                }

                if (ExtPrice != undefined && ExtPrice != '&nbsp;') {
                    $get('txtExtPriceOrder').innerText = ExtPrice.replace('&nbsp;', ' ');
                }
                else {
                    $get('txtExtPriceOrder').innerText = '';
                }

                if (UnitPrice != undefined && UnitPrice != '&nbsp;') {
                    $get('txtUnitprice').innerText = UnitPrice;
                }
                else {
                    $get('txtUnitprice').innerText = '';
                }


                if (Qty != undefined && Qty != '&nbsp;') {
                    $get('txtQty').innerText = Qty;
                }
                else {
                    $get('txtQty').innerText = '';
                }

                if (PartNumber != undefined && PartNumber != '&nbsp;') {
                    $get('txtPartNumber').innerText = PartNumber;
                }
                else {
                    $get('txtPartNumber').innerText = '';
                }

                if (OrderBlock != undefined && OrderBlock != '&nbsp;') {
                    $get('txtOrderBlock').innerText = OrderBlock;
                }
                else {
                    $get('txtOrderBlock').innerText = '';
                }

                if (CustomerPO != undefined && CustomerPO != '&nbsp;') {
                    $get('txtCustomerPOLabel').innerText = CustomerPO;
                }
                else {
                    $get('txtCustomerPOLabel').innerText = '';
                }

                if (CustomerName != undefined && CustomerName != '&nbsp;') {
                    $get('txtCustomerName').innerText = CustomerName;
                }
                else {
                    $get('txtCustomerName').innerText = '';
                }

                if (ORIGAccount != undefined && ORIGAccount != '&nbsp;') {
                    $get('txtORIGAccount').innerText = ORIGAccount;
                }
                else {
                    $get('txtORIGAccount').innerText = '';
                }

                if (ContactNumber != undefined && ContactNumber != '&nbsp;') {
                    $get('txtContactNumber').innerText = ContactNumber;
                }
                else {
                    $get('txtContactNumber').innerText = '';
                }

                if (LastName != undefined && LastName != '&nbsp;') {
                    $get('txtLastName').innerText = LastName;
                }
                else {
                    $get('txtLastName').innerText = '';
                }


                if (FirstName != undefined && FirstName != '&nbsp;') {
                    $get('txtFirstName').innerText = FirstName;
                }
                else {
                    $get('txtFirstName').innerText = '';
                }

                if (EmailAddress != undefined && EmailAddress != '&nbsp;') {
                    $get('txtEmailAddress').innerText = EmailAddress;
                }
                else {
                    $get('txtEmailAddress').innerText = '';
                }

                if (Phone != undefined && Phone != '&nbsp;') {
                    $get('txtPhone').innerText = Phone;
                }
                else {
                    $get('txtPhone').innerText = '';
                }

                if (ShipName != undefined && ShipName != '&nbsp;') {
                    $get('txtShipName').innerText = ShipName;
                }
                else {
                    $get('txtShipName').innerText = '';
                }


                if (ShipMailing != undefined && ShipMailing != '&nbsp;') {
                    $get('txtShipMailing').innerText = ShipMailing;
                }
                else {
                    $get('txtShipMailing').innerText = '';
                }

                if (ShipCity != undefined && ShipCity != '&nbsp;') {
                    $get('txtShipCity').innerText = ShipCity;
                }
                else {
                    $get('txtShipCity').innerText = '';
                }

                if (ShipZip != undefined && ShipZip != '&nbsp;') {
                    $get('txtShipZip').innerText = ShipZip;
                }
                else {
                    $get('txtShipZip').innerText = '';
                }

                if (ShipState != undefined && ShipState != '&nbsp;') {
                    $get('txtShipState').innerText = ShipState;
                }
                else {
                    $get('txtShipState').innerText = '';
                }
            }
        }

        function CheckString(val) {
            if (val)
                return val;
            else
                return "";
        }

        function CheckValue(val) {
            if (val == "&nbsp;")
                return "";
            else
                return val;
        }

        function OnSuccessFunction(Data) {
            if (Data == "OR") {
                setCookie("ORNo", $get("txtCustomerLookUp").value, 1);
                setCookie("PONo", '', -1);
                __doPostBack('= btnRefresh.ClientID ', '');
            }
            else if (Data == "PO") {
                setCookie("PONo", $get("txtCustomerLookUp").value, 1);
                //setCookie("PONo",'', -1);
                setCookie("ORNo", '', -1);
                __doPostBack('= btnRefresh.ClientID ', '');
            }
            else {
                setCookie("PONo", '', -1);
                setCookie("ORNo", '', -1);
                __doPostBack('= btnRefresh.ClientID ', '');
            }
        }

        function pageLoad() {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            $(document).ready(function () {

                $("#gridOrderHistory  tr:has(td)").hover(function () {
                    $(this).css("cursor", "pointer");
                });

                $("#gridOrderHistory tr").click(function () {
                    //Checking if function is a paging, sort or highlight a row.
                    if (($(this).closest("tr")[0].rowIndex > 0 &&
                    $(this).closest("tr")[0].rowIndex <= 50) &&
                    ($(this).closest("tr")[0].outerHTML.indexOf("grid_paging") == -1)) {

                        $("#gridOrderHistory  tr").closest('TR').removeClass('SelectedRowStyle');
                        $(this).addClass('SelectedRowStyle');

                        var row;

                        var totalCols = $("#gridOrderHistory").find('tr')[0].cells.length;

                        for (var i = 1; i < totalCols + 1; i++) {
                            var ColName = $('#gridOrderHistory  tr').find('th:nth-child(' + (i) + ')').text();
                            if (ColName == 'ROW') {
                                row = $(this).find("td:eq(" + (i - 1) + ")").html();
                            }
                        }

                        if ($get('PageIndex').value == "Page")
                            OnSuccess($(this).index());
                        else if ($get('PageIndex').value == "Sort")
                            OnSuccess($(this).index());
                        else
                            OnSuccess($(this).index());

                        //PageMethods.GetDatafromXMLDetails(row, OnSuccess);

                        var mydiv = $('#pnlPopupOrderHistory');
                        mydiv.dialog({ autoOpen: false,
                            title: "Order Details",
                            resizable: false,
                            width: "auto",
                            dialogClass: "FixedPostion",
                            closeOnEscape: true,
                            open: function (type, data) {
                                $(this).parent().appendTo("form"); //won't postback unless within the form tag
                            }
                        });

                        mydiv.dialog('open');

                        return false;
                    }
                    else if ($(this).closest("tr")[0].rowIndex == 0) {
                        $get('PageIndex').value = "Sort";
                    }
                    else {
                        $get('PageIndex').value = "Page";
                    }

                });

                $('#<%= lnkCustomerLookup.ClientID %>').click(function () {

                    var mydiv = $('#ratePanel');

                    mydiv.dialog({ autoOpen: false,
                        title: "Order/Customer Lookup",
                        resizable: false,
                        width: 290,
                        modal: true,
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        open: function (type, data) {
                            $(this).keydown(function (e) {
                                if (e.keyCode == 13) {
                                    var txtSearch = $get("txtCustomerLookUp").value;
                                    var rdoOrderNumber = $get("rdoOrderNumber").checked;
                                    var rdoCustomerLookUp = $get("rdoCustomerLookUp").checked;
                                    PageMethods.btnOk_Click1(txtSearch, rdoOrderNumber, rdoCustomerLookUp, OnSuccessFunction);
                                    mydiv.dialog('close');
                                }
                            });
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        },
                        buttons: {
                            "Ok": function () {
                                var txtSearch = $get("txtCustomerLookUp").value;
                                var rdoOrderNumber = $get("rdoOrderNumber").checked;
                                var rdoCustomerLookUp = $get("rdoCustomerLookUp").checked;
                                PageMethods.btnOk_Click1(txtSearch, rdoOrderNumber, rdoCustomerLookUp, OnSuccessFunction);
                                mydiv.dialog('close');
                            },
                            "Cancel": function () {
                                mydiv.dialog('close');
                            }
                        }
                    });

                    mydiv.dialog('open');

                    return false;
                });

                $('#<%= lnkContactLevel.ClientID %>').click(function () {
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
                    setCookie('CNo', "", -1);
                    __doPostBack('= btnRefresh.ClientID ', '');
                    return false;
                });

                $("#ImageSelectContact").hover(function () {
                    $(this).css("cursor", "pointer");
                });

                $('#<%= ImageSelectContact.ClientID %>').click(function () {
                    setCookie('CName', "", -1);
                    setCookie('CNo', "", -1);
                    __doPostBack('= btnRefresh.ClientID ', '');
                    return false;
                });


                $('#<%= lnkCustomerLookupSearch.ClientID %>').click(function () {
                    setCookie('ORNo', "", -1);
                    setCookie('PONo', "", -1);
                    setCookie('Remove', "Ok", 1);
                    __doPostBack('= btnRefresh.ClientID ', '');
                    return false;
                });

                $("#ImageCustomerLookupSearch").hover(function () {
                    $(this).css("cursor", "pointer");
                });


                $('#ImageCustomerLookupSearch').click(function () {
                    setCookie('ORNo', "", -1);
                    setCookie('PONo', "", -1);
                    setCookie('Remove', "Ok", 1);
                    __doPostBack('= btnRefresh.ClientID ', '');
                    return false;
                });

            });

            $addHandler(document, 'keydown', onKeypress);
        }

        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                var mydiv = $('#pnlPopupOrderHistory');
                mydiv.dialog('close');
            }
            if (args.keyCode == Sys.UI.Key.enter) {
                {
                    var mydivOrder = $('#ratePanel');
                    var txtSearch = $get("txtCustomerLookUp").value;
                    var rdoOrderNumber = $get("rdoOrderNumber").checked;
                    var rdoCustomerLookUp = $get("rdoCustomerLookUp").checked;
                    PageMethods.btnOk_Click1(txtSearch, rdoOrderNumber, rdoCustomerLookUp, OnSuccessFunction);
                    mydivOrder.dialog('open');
                    mydivOrder.dialog('close');
                }
            }
        }
     
    </script>
    <div id="container">
        <asp:UpdatePanel ID="UpdatePanelTest" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" class="object-container">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td class="object-wrapper" style="height: 20px">
                                                                <asp:LinkButton ID="lnkContactLevel" runat="server" CssClass="LabelFont">Select Contact</asp:LinkButton>
                                                                &nbsp;
                                                                <img id="ImageSelectContact" src="../../App_Themes/Images/New Design/btn-red-x.gif"
                                                                    runat="server" height="10" border="0" clientidmode="Static" />
                                                                <asp:LinkButton ID="lnkContactSelected" Style="text-decoration: none; border-bottom: 1px solid red;"
                                                                    runat="server" Font-Names="Arial" ClientIDMode="Static" ToolTip="Click to remove contact"
                                                                    Font-Size="12px" ForeColor="Black"></asp:LinkButton>
                                                            </td>
                                                            <td class="object-wrapper" style="height: 20px">
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lnkCustomerLookup" ToolTip="Click to Lookup for Click" runat="server"
                                                                    ClientIDMode="Static" CssClass="LabelFont">Customer PO Lookup</asp:LinkButton>
                                                                &nbsp;
                                                                <img id="ImageCustomerLookupSearch" src="../../App_Themes/Images/New Design/btn-red-x.gif"
                                                                    runat="server" clientidmode="Static" height="10" border="0" />
                                                                <asp:LinkButton ID="lnkCustomerLookupSearch" runat="server" Font-Names="Arial" ClientIDMode="Static"
                                                                    Style="text-decoration: none; border-bottom: 1px solid red;" Font-Size="12px"
                                                                    ToolTip="Click to remove filter" ForeColor="Black"></asp:LinkButton>
                                                            </td>
                                                            <td class="object-wrapper" style="height: 20px" id="lnkExportToExcel" runat="server">
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="btnExportToExcel" ToolTip="Click to Export to Excel" OnClick="btn_Export2ExcelClick"
                                                                    runat="server" CssClass="LabelFont">Export to Excel</asp:LinkButton>
                                                                &nbsp;
                                                            </td>
                                                            <td class="object-wrapper" style="padding-right: 0; height: 20px;">
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="BtnArrangeColumn" runat="server" OnClick="ArrangeColumn_Click"
                                                                    CssClass="LabelFont">Arrange Columns</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td class="object-wrapper" style="height: 20px;">
                                                    <asp:CheckBox ID="BD" runat="server" ClientIDMode="Static" OnCheckedChanged="ByDate_CheckedChanged"
                                                        AutoPostBack="True" />
                                                </td>
                                                <td class="object-wrapper" style="height: 20px; width: 58px; white-space: nowrap;">
                                                    <asp:Label ID="Label4" Text="Start Date:" ClientIDMode="Static" runat="server" CssClass="LabelFont" />
                                                </td>
                                                <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                    <asp:TextBox CssClass="textbox curved" ClientIDMode="Static" ID="txtStartDate" runat="server"
                                                        AutoPostBack="True" Style="width: 70px;"></asp:TextBox>
                                                </td>
                                                <td style="height: 20px; vertical-align: middle; white-space: nowrap;">
                                                    <asp:ImageButton ID="imgstartCal" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                                        runat="server" />
                                                </td>
                                                <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate"
                                                        PopupButtonID="imgstartCal">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; width: 58px; white-space: nowrap;">
                                                    <asp:Label ID="Label1" ClientIDMode="Static" CssClass="LabelFont" Text="End Date:"
                                                        runat="server" />
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; white-space: nowrap;">
                                                    <asp:TextBox CssClass="textbox curved" ClientIDMode="Static" ID="txtEndDate" runat="server"
                                                        AutoPostBack="True" Style="width: 70px;"></asp:TextBox>
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; white-space: nowrap;">
                                                    <asp:ImageButton ID="imgEndCal" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                                        runat="server" />
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; white-space: nowrap;">
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate"
                                                        PopupButtonID="imgEndCal">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td class="object-wrapper">
                                                    <img src="../../App_Themes/Images/New Design/divider-2.png" width="1" height="25"
                                                        border="0" />
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; white-space: nowrap;">
                                                    <asp:CheckBox ID="ByCal" ClientIDMode="Static" AutoPostBack="True" OnCheckedChanged="ByCal_CheckedChanged"
                                                        runat="server" />
                                                </td>
                                                <td style="height: 48px; white-space: nowrap;">
                                                    <asp:RadioButton ID="rdoFiscalYear" ClientIDMode="Static" runat="server" Text=" Fiscal Year"
                                                        GroupName="RdoByCals" Checked="True" AutoPostBack="True" CssClass="LabelFont" />
                                                </td>
                                                <td class="object-wrapper" style="padding-right: 0; height: 48px; white-space: nowrap;">
                                                    <asp:RadioButton ID="rdoCalender" ClientIDMode="Static" runat="server" Text=" Calendar"
                                                        GroupName="RdoByCals" AutoPostBack="True" CssClass="LabelFont" />
                                                </td>
                                                <td class="object-wrapper" style="padding-right: 0; height: 48px; white-space: nowrap;">
                                                    <asp:DropDownList ID="ddlBycal" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddiYear_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="object-wrapper">
                                                    <img src="../../App_Themes/Images/New Design/divider-2.png" width="1" height="25"
                                                        border="0" />
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; white-space: nowrap;">
                                                    <asp:Label ID="Label2" CssClass="LabelFont" Text="Total Orders:" runat="server" />
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; white-space: nowrap;">
                                                    <asp:TextBox ID="txtbTotalOrders" runat="server" Text="0" Width="50px" ReadOnly="true"
                                                        CssClass="textboxLabel" Style="font-size: 14px;"></asp:TextBox>
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; white-space: nowrap;">
                                                    <asp:Label ID="Label3" CssClass="LabelFont" Text="Total Sales:" runat="server" />
                                                </td>
                                                <td class="object-wrapper" style="height: 48px; white-space: nowrap;">
                                                    <asp:TextBox ID="txtbTatalSales" runat="server" Text="$0.00" Width="85px" ReadOnly="true"
                                                        CssClass="textboxLabel" Style="font-size: 14px;"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="BD" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ByCal" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlBycal" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="rdoCalender" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="rdoFiscalYear" EventName="CheckedChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <div class="table-wrapper page1">
                        <table style="border: 0; width: 100%; height: 100%" cellpadding="10" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ScrollBars="None" ID="DataDiv" Width="100%">
                                                <asp:GridView AutoGenerateColumns="true" ClientIDMode="Static" ID="gridOrderHistory"
                                                    AllowPaging="true" AllowSorting="true" PageSize="50" runat="server" ForeColor="Black"
                                                    BackColor="#FFFFFF" Font-Size="12px" OnPageIndexChanging="gridOrderHistoryPageChanging"
                                                    OnSorting="gridOrderHistory_Sorting" OnRowDataBound="gridOrderHistory_RowDataBound"
                                                    CellPadding="4" CellSpacing="2" GridLines="None">
                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                    <RowStyle CssClass="RowStyle" BackColor="#e5e5e5" Wrap="false" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <AlternatingRowStyle BackColor="White" Wrap="false" />
                                                    <PagerStyle CssClass="grid_paging" />
                                                </asp:GridView>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlGridIndex" runat="server" CssClass="CssLabel">
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rdoCalender" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="rdoFiscalYear" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlBycal" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ByCal" EventName="CheckedChanged" />
                                            <asp:PostBackTrigger ControlID="btnExportToExcel" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table border="0" cellpadding="10" cellspacing="0" width="100%" bgcolor="#F0F0F0">
                        <tr>
                            <td>
                                <div class="CssErrorLabel" style="font-size: 12px; font-weight: bold;">
                                    <asp:Literal ID="litErrorinGrid" runat="server"></asp:Literal></div>
                            </td>
                        </tr>
                        <tr runat="server" id="trBlank" visible="false">
                            <td style="height: 175px">
                                &nbsp;
                            </td>
                        </tr>
                        <asp:UpdatePanel ID="upOuter" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        <asp:Button Style="display: none" ID="btnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--Popup Dialog for Customer PO Search--%>
    <asp:Panel ID="ratePanel" ClientIDMode="Static" runat="server" Style="display: none;">
        <br />
        <asp:RadioButton ID="rdoOrderNumber" ClientIDMode="Static" runat="server" CssClass="radioButton"
            Text="Order Number" GroupName="RdoLookUp" Checked="true" />
        <asp:RadioButton ID="rdoCustomerLookUp" ClientIDMode="Static" runat="server" CssClass="radioButton"
            Text="Customer PO" GroupName="RdoLookUp" />
        <br />
        <br />
        <asp:TextBox ID="txtCustomerLookUp" ClientIDMode="Static" CssClass="textbox curved"
            Width="95%" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ValidationGroup="ResourceGroup" ID="reqType"
            ControlToValidate="txtCustomerLookUp" InitialValue="Please select" ErrorMessage="Please enter the LookUp"
            ForeColor="Red" Display="Dynamic" SetFocusOnError="True" />
        <br />
    </asp:Panel>
    <%--Popup Dialog for Order Details--%>
    <asp:Panel ID="pnlPopupOrderHistory" runat="server" Style="display: none;" ClientIDMode="Static">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblAccountName" CssClass="LabelFont" runat="server" Text="Account Number"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtAccountNumber1" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrderDate" CssClass="LabelFont" runat="server" Text="Order Date"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtOrderDate" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPartNumber" CssClass="LabelFont" runat="server" Text="Part Number"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtPartNumber" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescription" Width="100%" CssClass="LabelFont" runat="server" Text="Description"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtDescription" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUnitprice" CssClass="LabelFont" runat="server" Text="Unit Price"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtUnitprice" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQty" CssClass="LabelFont" runat="server" Text="Qty"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtQty" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblExtPrice" CssClass="LabelFont" runat="server" Text="Ext Price"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtExtPriceOrder" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrderNumber" CssClass="LabelFont" runat="server" Text="Order Number"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtOrderNumber" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLine" CssClass="LabelFont" runat="server" Text="Line"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtLine" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrderType" CssClass="LabelFont" runat="server" Text="Order Type"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtOrderType" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblConvertedDate" CssClass="LabelFont" runat="server" Text="Converted Date"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtConvertedDate" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReasonRejection" CssClass="LabelFont" runat="server" Text="Reason for Rejection"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtReasonRejection" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrderBlockName" CssClass="LabelFont" runat="server" Text="Order Block"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtOrderBlock" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCustomerPOName" CssClass="LabelFont" runat="server" Text="Customer PO"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCustomerPOLabel" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCustomerName1" CssClass="LabelFont" runat="server" Text="Customer Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCustomerName" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblORIGAccount" CssClass="LabelFont" runat="server" Text="ORIG Account"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtORIGAccount" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContactNumber" CssClass="LabelFont" runat="server" Text="Contact Number"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtContactNumber" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLastName1" CssClass="LabelFont" runat="server" Text="Last Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtLastName" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFirstName" CssClass="LabelFont" runat="server" Text="First Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtFirstName" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmailAddress" CssClass="LabelFont" runat="server" Text="Email Address"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtEmailAddress" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPhone" CssClass="LabelFont" runat="server" Text="Phone"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtPhone" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShipName" CssClass="LabelFont" runat="server" Text="Ship Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtShipName" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShipMailing" CssClass="LabelFont" runat="server" Text="Ship Mailing"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtShipMailing" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShipCity" CssClass="LabelFont" runat="server" Text="Ship City"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtShipCity" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShipZip" CssClass="LabelFont" runat="server" Text="Ship Zip"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtShipZip" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShipState" CssClass="LabelFont" runat="server" Text="Ship State"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtShipState" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShipDate" CssClass="LabelFont" runat="server" Text="Ship Date"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtShipDate" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrackingNo" CssClass="LabelFont" runat="server" Text="Tracking No"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtTrackingNo" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBillSales" CssClass="LabelFont" runat="server" Text="Invoice Sales"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtBillSales" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBillNo" CssClass="LabelFont" runat="server" Text="Invoice No"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtBillNo" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBillDate" CssClass="LabelFont" runat="server" Text="Bill Date"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtBillDate" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 100%;" ClientIDMode="Static">
                    </asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="PageIndex" ClientIDMode="Static" runat="server" />
</asp:Content>
