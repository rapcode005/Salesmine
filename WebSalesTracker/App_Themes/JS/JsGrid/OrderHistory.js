 function HideColumn() {
            //var $tbl = $("#gridOrderHistory");
            $(function () {
                //Hide Column
                var thOrderType = $("#gridOrderHistory th:contains('Order_type')");
                thOrderType.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                var thUvals = $("#gridOrderHistory th:contains('uvals')");
                thUvals.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thUvals.index()).css("display", "none");
                });

                var thReRej = $("#gridOrderHistory th:contains('ReRej')");
                thReRej.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thReRej.index()).css("display", "none");
                });

                var thaccount = $("#gridOrderHistory th:contains('accountnum')");
                thaccount.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thaccount.index()).css("display", "none");
                });

                var thorderDate = $("#gridOrderHistory th:contains('order_date')");
                thorderDate.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thorderDate.index()).css("display", "none");
                });

                var thpartnum = $("#gridOrderHistory th:contains('part_number')");
                thpartnum.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thpartnum.index()).css("display", "none");
                });

                var thDesc = $("#gridOrderHistory th:contains('Descrp')");
                thDesc.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thDesc.index()).css("display", "none");
                });

                var thunitprice = $("#gridOrderHistory th:contains('unit_price')");
                thunitprice.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thunitprice.index()).css("display", "none");
                });

                var thextprice = $("#gridOrderHistory th:contains('ext_price')");
                thextprice.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thextprice.index()).css("display", "none");
                });

                var thquantity = $("#gridOrderHistory th:contains('quantity')");
                thquantity.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thquantity.index()).css("display", "none");
                });

                var thordernum= $("#gridOrderHistory th:contains('order_num')");
                thordernum.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thordernum.index()).css("display", "none");
                });

                var thlne= $("#gridOrderHistory th:contains('lne')");
                thlne.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thlne.index()).css("display", "none");
                });

                var thconvdate= $("#gridOrderHistory th:contains('conv_date')");
                thconvdate.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thconvdate.index()).css("display", "none");
                });

                var thorderblk= $("#gridOrderHistory th:contains('order_blk')");
                thorderblk.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thorderblk.index()).css("display", "none");
                });

                var thcuspo= $("#gridOrderHistory th:contains('cus_po')");
                thcuspo.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thcuspo.index()).css("display", "none");
                });

                var thcusname= $("#gridOrderHistory th:contains('cus_name')");
                thcusname.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thcusname.index()).css("display", "none");
                });

                var thorigacc= $("#gridOrderHistory th:contains('orig_acc')");
                thorigacc.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thorigacc.index()).css("display", "none");
                });

                var thconnum= $("#gridOrderHistory th:contains('con_num')");
                thconnum.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thconnum.index()).css("display", "none");
                });

                var thlstname= $("#gridOrderHistory th:contains('lst_name')");
                thlstname.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thlstname.index()).css("display", "none");
                });

                var thfrtname= $("#gridOrderHistory th:contains('frt_name')");
                thfrtname.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thfrtname.index()).css("display", "none");
                });

                var themladdr= $("#gridOrderHistory th:contains('eml_addr')");
                themladdr.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(themladdr.index()).css("display", "none");
                });

                var thphonenum= $("#gridOrderHistory th:contains('phone_num')");
                thphonenum.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thphonenum.index()).css("display", "none");
                });

                var thshipname= $("#gridOrderHistory th:contains('ship_name')");
                thshipname.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipname.index()).css("display", "none");
                });

                var thshipmail= $("#gridOrderHistory th:contains('ship_mail')");
                thshipmail.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipmail.index()).css("display", "none");
                });

                var thshipcity= $("#gridOrderHistory th:contains('ship_city')");
                thshipcity.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipcity.index()).css("display", "none");
                });

                var thshipzip= $("#gridOrderHistory th:contains('ship_zip')");
                thshipzip.css("display","none");
                $("#gridOrderHistory tr").each(function () {
                    $(this).find("td").eq(thshipzip.index()).css("display", "none");
                });

                var thshipstate= $("#gridOrderHistory th:contains('ship_state')");
                thshipstate.css("display","none");
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

                        if (ColName == 'ACCOUNT NUMBER' || ColName=='accountnum') {
                            if(grd.rows[RowIndex].cells[i - 1])
                            {
                                AccountNumber = grd.rows[RowIndex].cells[i - 1].innerHTML;
                                if (AccountNumber == undefined || AccountNumber == '&nbsp;') {
                                    AccountNumber = '';
                                }
                            }
                            else
                                AccountNumber = '';
                        }
                        else if (ColName == 'ORDER DATE' || ColName=='order_date') {
                            if(grd.rows[RowIndex].cells[i - 1])
                            {
                                OrderDate = grd.rows[RowIndex].cells[i - 1].innerHTML;
                                if (OrderDate == undefined || OrderDate == '&nbsp;') {
                                    OrderDate = '';
                                }
                            }
                            else
                                 OrderDate = '';
                        }
                        else if (ColName == 'PART NUMBER' || ColName=='part_number') {
                            if(grd.rows[RowIndex].cells[i - 1])
                            {
                                PartNumber = grd.rows[RowIndex].cells[i - 1].innerHTML;
                                if (PartNumber == undefined || PartNumber == '&nbsp;') {
                                    PartNumber = '';
                                }
                            }
                            else
                                PartNumber = '';
                        }
                        else if (ColName == 'DESCRIPTION' || ColName == 'Descrp') {
                            if(grd.rows[RowIndex].cells[i - 1])
                            {
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
                        else if (ColName == 'QTY' || ColName == 'quantity' ) {

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
                        else if (ColName == 'ORDER TYPE' || ColName =='Order_type') {

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
                    $get('txtExtPriceOrder').textContent = ExtPrice.replace('&nbsp;',' ');
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
                    $get('txtExtPriceOrder').innerText = ExtPrice.replace('&nbsp;',' ');
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
                                row = $(this).find("td:eq(" + (i-1) + ")").html();
                            }
                        } 

                        if ($get('PageIndex').value == "Page")
                            OnSuccess($(this).index());
                        else if ( $get('PageIndex').value == "Sort")
                            OnSuccess($(this).index());
                        else
                            OnSuccess($(this).index() + 1);
                        
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
                    else if ($(this).closest("tr")[0].rowIndex == 0)
                    {
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

        //Loading Message show while the gridview being bind.
        function EndGetData(arg) {
            document.getElementById("grid").innerHTML = arg;
        }
        setTimeout("<asp:literal runat="server" id="ltCallback1" />", 10);
        


        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                var mydiv = $('#pnlPopupOrderHistory');
                mydiv.dialog('close');
            }
            if (args.keyCode == Sys.UI.Key.enter) {
                {
                    var mydiv = $('#ratePanel');
                    var txtSearch = $get("txtCustomerLookUp").value;
                    var rdoOrderNumber = $get("rdoOrderNumber").checked;
                    var rdoCustomerLookUp = $get("rdoCustomerLookUp").checked;
                    PageMethods.btnOk_Click1(txtSearch, rdoOrderNumber, rdoCustomerLookUp, OnSuccessFunction);
                    mydiv.dialog('open');
                    mydiv.dialog('close');
                }
            }
        }


          // It is important to place this JavaScript code after ScriptManager1
      var xPos, yPos;
      var prm = Sys.WebForms.PageRequestManager.getInstance();

   function BeginRequestHandler(sender, args) {
        if ($get('<%=Panel1.ClientID%>') != null) {
          // Get X and Y positions of scrollbar before the partial postback
          xPos = $get('<%=Panel1.ClientID%>').scrollLeft;
          yPos = $get('<%=Panel1.ClientID%>').scrollTop;
        }
     }

     function EndRequestHandler(sender, args) {
         if ($get('<%=Panel1.ClientID%>') != null) {
           // Set X and Y positions back to the scrollbar
           // after partial postback
           $get('<%=Panel1.ClientID%>').scrollLeft = xPos;
           $get('<%=Panel1.ClientID%>').scrollTop = yPos;
         }
     }


     prm.add_beginRequest(BeginRequestHandler);
     prm.add_endRequest(EndRequestHandler);