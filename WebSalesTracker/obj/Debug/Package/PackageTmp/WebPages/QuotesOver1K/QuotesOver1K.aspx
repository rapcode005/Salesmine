<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="QuotesOver1K.aspx.cs" EnableEventValidation="false"
    Inherits="WebSalesMine.WebPages.QuotesOver1K.QuotesOver1K" %>

<%@ Import Namespace="AppLogic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
    <script type='text/javascript' src="../../App_Themes/JS/accounting.js"></script>
    <script type="text/javascript" src="../../App_Themes/JS/JsGrid/jquery.tablesorter.min.js"></script>
    <script src="../../App_Themes/JS/JsGrid/jquery.tablesorter.pager.js" type="text/javascript"></script>
    <script type="text/javascript">

        function calendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
            //  sender._popupBehavior._element.style.position = "fixed";
        }

        function loadXMLDoc(dname) {
            var xhttp;
            if (window.XMLHttpRequest) {
                xhttp = new XMLHttpRequest();
            }
            else {
                xhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhttp.open("GET", dname, false);
            xhttp.send();

            return xhttp.responseXML;
        }

        function CheckString(val) {
            if (val)
                return val;
            else
                return "";
        }

        function onerrorMEssage() {
            alert("Error");
        }

        function onclickclose() {
            var mydiv = $('#pnlQuoteOver1K');
            mydiv.dialog('close');
        }

        function CheckString(val) {
            if (val)
                return val;
            else
                return "";
        }

        function onSuccessDocNumber(Data) {
            if (Data) {
                if (Data.rows) {
                    var AccountNum = CheckString(Data.rows[0]['CUSMERGE']);
                    setCookie('A1No', AccountNum);
                }
            }
        }

        function onSuccessRow(Data) {
            var RowIndex = Data + 1;
            var grd = document.getElementById('grdQuoteOver1K');
            var totalCols;
            var AccountNum, QuoteDay, Doc, QuoteValue, QuoteCost, QuoteGMPerc, Probability, Weighted, mydate, Competition, Notes, AccountName, salesTeamIn, SalesTeamCurrent, Status, Productline, Mining, Construction, sched_followup, ProposedDate
            var nAgt = navigator.userAgent;
            var FIREFOX = /Firefox/i.test(navigator.userAgent);

            if (grd) {
                if (isNaN(RowIndex) == false) {
                    totalCols = $("#grdQuoteOver1K").find('tr')[0].cells.length;
                      for (var i = 1; i < totalCols + 1; i++) {
                        var ColName = $('#grdQuoteOver1K tr').find('th:nth-child(' + i + ')').text();

                        switch (ColName) {
                            case 'ACCOUNT NO.':
                                if (FIREFOX)
                                    AccountNum = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    AccountNum = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (AccountNum == undefined || AccountNum == '&nbsp;') {
                                    AccountNum = '';
                                }
                            break;
                            case 'QUOTE DAY':
                                if (FIREFOX)
                                    QuoteDay = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    QuoteDay = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (QuoteDay == undefined || QuoteDay == '&nbsp;') {
                                    QuoteDay = '';
                                }
                            break;
                            case 'QUOTE DOC':
                                var lnkQuoteDoc = grd.rows[RowIndex].cells[i - 1].childNodes[0];
                                if (lnkQuoteDoc != undefined) {
                                    Doc = lnkQuoteDoc.innerHTML;
                                }
                                if (Doc == undefined || Doc == '&nbsp;') {
                                    Doc = '';
                                }
                            break;
                            case 'QUOTE COST':
                                if (FIREFOX)
                                    QuoteCost = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    QuoteCost = grd.rows[RowIndex].cells[i - 1].innerHTML;
   
                                if (QuoteCost == undefined || QuoteCost == '&nbsp;') {
                                    QuoteCost = '';
                                }
                            break;
                            case 'QUOTE VALUE':
                                if (FIREFOX)
                                    QuoteValue = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    QuoteValue = grd.rows[RowIndex].cells[i - 1].innerHTML;  
                            
                                if (QuoteValue == undefined || QuoteValue == '&nbsp;') {
                                    QuoteValue = '';
                                }
                            break;
                            case 'QUOTE GM PERCENTAGE':
                                if (FIREFOX)
                                    QuoteGMPerc = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    QuoteGMPerc = grd.rows[RowIndex].cells[i - 1].innerHTML;  

                                if (QuoteGMPerc == undefined || QuoteGMPerc == '&nbsp;') {
                                    QuoteGMPerc = '';
                                }
                            break;
                            case 'CLOSE PROBABILITY':
                                if (FIREFOX)
                                    Probability = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    Probability = grd.rows[RowIndex].cells[i - 1].innerHTML;  

                                    
                                if (Probability == undefined || Probability == '&nbsp;') {
                                    Probability = '';
                                }
                            break;
                            case 'WEIGHTED VALUE':
                                if (FIREFOX)
                                    Weighted = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    Weighted = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                    
                                if (Weighted == undefined || Weighted == '&nbsp;') {
                                    Weighted = '';
                                }
                            break;
                            case 'COMPETITION':
                                if (FIREFOX)
                                    Competition = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    Competition = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                Competition = grd.rows[RowIndex].cells[i - 1].innerHTML;
                                if (Competition == undefined || Competition == '&nbsp;') {
                                    Competition = '';
                                }
                            break;
                            case 'NOTES':
                                if (FIREFOX)
                                    Notes = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    Notes = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (Notes == undefined || Notes == '&nbsp;') {
                                    Notes = '';
                                }
                            break;
                            case 'ACCOUNT NAME':
                                if (FIREFOX)
                                    AccountName = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    AccountName = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (AccountName == undefined || AccountName == '&nbsp;') {
                                    AccountName = '';
                                }
                            break;
                            case 'QUOTE ASSIGNMENT':
                              if (FIREFOX)
                                    salesTeamIn = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    salesTeamIn = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (salesTeamIn == undefined || salesTeamIn == '&nbsp;') {
                                    salesTeamIn = '';
                                }
                            break;
                            case 'ACCOUNT ASSIGNMENT':
                                if (FIREFOX)
                                    SalesTeamCurrent = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    SalesTeamCurrent = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (SalesTeamCurrent == undefined || SalesTeamCurrent == '&nbsp;') {
                                    SalesTeamCurrent = '';
                                }
                            break;
                            case 'SOURCE':
                                if (FIREFOX)
                                    Status = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    Status = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (Status == undefined || Status == '&nbsp;') {
                                    Status = '';
                                }
                            break;
                            case 'PRODUCT LINE':
                                if (FIREFOX)
                                    Productline = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    Productline = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (Productline == undefined || Productline == '&nbsp;') {
                                    Productline = '';
                                }
                            break;
                            case 'PROPOSED DATE':
                                if (FIREFOX)
                                    ProposedDate = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    ProposedDate = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (ProposedDate == undefined || ProposedDate == '&nbsp;') {
                                    ProposedDate = '';
                                }
                            break;
                            case 'MINING':
//                                if (FIREFOX)
//                                    Mining = grd.rows[RowIndex].cells[i - 1].textContent;
//                                else
                                    Mining = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                if (Mining == undefined || Mining == '&nbsp;') {
                                    Mining = '';
                                }
                            break;
                            case 'CONSTRUCTION':
//                                if (FIREFOX)
//                                    Construction = grd.rows[RowIndex].cells[i - 1].textContent;
//                                else
                                    Construction = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                
                                if (Construction == undefined || Construction == '&nbsp;') {
                                    Construction = '';
                                }
                            break;
                            case 'SCHEDULE FOLLOWUP':
                                if (FIREFOX)
                                    sched_followup = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    sched_followup = grd.rows[RowIndex].cells[i - 1].innerHTML;

                                
                                if (sched_followup == undefined || sched_followup == '&nbsp;') {
                                    sched_followup = '';
                                }
                            break;
                        }

                      }
                }
            }


            

            $get('QuoteCost').value = QuoteCost;
            $get('QuoteGMPerc').value = QuoteGMPerc;
            $get('Status').value = Status;
            $get('QuoteDoc').value = Doc;
            $get('AccountNum').value = AccountNum;
            if (CheckString(ProposedDate) || ProposedDate != "") {
                myDate = new Date(CheckString(ProposedDate));
                if (myDate != "Invalid Date") {
                    $get('Date').value = (myDate.getMonth() + 1) + "/" +
                                         myDate.getDate() + "/" +
                                         myDate.getFullYear();
                    $get('txtProposedClose').value = (myDate.getMonth() + 1) + "/" +
                                         myDate.getDate() + "/" +
                                         myDate.getFullYear();
                }
                else {
                    $get('Date').value = "";
                    $get('txtProposedClose').value = "";
                }
            }
            else {
                $get('Date').value = "";
                $get('txtProposedClose').value = "";
            }

            $get('QuoteValue').value = RemoveCurrencySign(QuoteValue);
            $get('txtEditQuoteValue').value = QuoteValue;

            if (Probability == "0.00 %" || Probability == "") {
                $get('ddlProbablilty').value = "";
            }
            else {
                // var P = parseFloat(Probability) * 100.00;
                $get('ddlProbablilty').value = Probability;
            }
            if (FIREFOX)
                $get('lblWeightedValue').textContent = Weighted;
            else
                $get('lblWeightedValue').innerText = Weighted;

            $get('Weighted').value = RemoveCurrencySign(Weighted);

            $get('txtCompetition').value = Competition;
            $get('txtNotes').value = Notes;
            $get('AccountName').value = AccountName;
            if (CheckString(QuoteDay)) {
                myDate = new Date(CheckString(QuoteDay));
                if (myDate != "Invalid Date") {
                    $get('QuoteDay').value = (myDate.getMonth() + 1) + "/" +
                                         myDate.getDate() + "/" +
                                         myDate.getFullYear();
                }
                else {
                    $get('QuoteDay').value = "";
                    QuoteDay = "";
                }
            }
            $get('SalesTeamIn').value = salesTeamIn;
            $get('SalesTeamCurrent').value = SalesTeamCurrent;
            $get('txtProductLine').value = Productline;

            $get('txtEditQuoteAssignment').value = salesTeamIn;

            if (Construction != undefined && Construction != '&nbsp;') {
                $get('ddlConstruction').value = Construction.toUpperCase();
            }
            else {
                $get('ddlConstruction').value = '';
            }
            if (Mining != undefined && Mining != '&nbsp;') {
                $get('ddlMining').value = Mining.toUpperCase();
            }
            else {
                $get('ddlMining').value = '';
            }

            if (Productline != undefined && Productline != '&nbsp;') {
                $get('ProductLine').value = Productline;
            }

            if (Construction != undefined && Construction != '&nbsp;') {
                $get('Construction').value = Construction.toUpperCase();
            }
            if (Mining != undefined && Mining != '&nbsp;') {
                $get('Mining').value = Mining.toUpperCase();
            }
            if (CheckString(sched_followup)) {
                myDate = new Date(CheckString(sched_followup));
                if (myDate != "Invalid Date") {
                    $get('ScheDate').value = (myDate.getMonth() + 1) + "/" +
                                         myDate.getDate() + "/" +
                                         myDate.getFullYear();
                    $get('txtScheFollow').value = (myDate.getMonth() + 1) + "/" +
                                         myDate.getDate() + "/" +
                                         myDate.getFullYear();
                }
                else {
                    $get('ScheDate').value = "";
                    $get('txtScheFollow').value = "";
                }
            }
            else {
                $get('ScheDate').value = "";
                $get('txtScheFollow').value = "";
            }

            if ($get('ddlProbablilty').value == "1.00 %")
                $get('SelectedRetain').value = "0";
            else
                $get('SelectedRetain').value = "1";

            onSuccessShow();
        }

        function OpenExcel() {
            $('#btnExportToExcel').click(function () {
                var mydiv = $('#pnlDateRange')
                mydiv.dialog({ autoOpen: false,
                    title: "Export to Excel",
                    resizable: false,
                    width: "auto",
                    height: "auto",
                    modal: false,
                    open: function (type, data) {
                        $(this).parent().appendTo("form"); //won't postback unless within the form tag 
                    }
                });

                mydiv.dialog('close');

                mydiv.dialog('open');

                return false;

            });
        }


        function onMouseHover() {
            $('#grdQuoteOver1K').css("cursor", "pointer");
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(clearDisposableItems)

        function pageLoad() {

            var FIREFOX = /Firefox/i.test(navigator.userAgent);

            $(document).ready(function () {

                $addHandler(document, 'keydown', onKeypress);
                $addHandler(document.getElementById('txtEditQuoteValue'), 'keyup', onKeyupEditQuoteValue);
                $addHandler(document.getElementById('ddlProbablilty'), 'keyup', onkeyup);
                $addHandler(document.getElementById('ddlProbablilty'), 'change', onchange);
                $addHandler(document.getElementById('txtEditQuoteValue'), 'blur', onBlurQuoteValue);
                $addHandler(document.getElementById('grdQuoteOver1K'), 'mouseover', onMouseHover);

                $("#grdQuoteOver1K tr:has(td)").hover(function () {
                    $(this).css("cursor", "pointer");
                });



                $("#grdQuoteOver1K tr").not($("#grdQuoteOver1K tr").eq(0)).click(function () {
                    var AccountNum;
                    var QuoteNum;

                    $("#grdQuoteOver1K tr").removeClass('SelectedRowStyle');

                    if ($(this).hasClass('even')) {
                        $(this).removeClass();
                        $(this).addClass('even SelectedRowStyle');
                    }
                    else
                        $(this).addClass('SelectedRowStyle');

                    if ($(this).closest("tr")[0].rowIndex > 0 && $(this).closest("tr")[0].rowIndex <= 200) {

                        var totalCols = $("#grdQuoteOver1K").find('tr')[0].cells.length;
                        var count;
                        for (var i = 0; i < totalCols + 1; i++) {
                            var ColName = $('#grdQuoteOver1K tr').find('th:nth-child(' + i + ')').text();
                           
                            if (ColName == 'QUOTE DOC') {
                                if (FIREFOX) {
                                    QuoteNum = $(this).find("td:eq(" + (i - 1) + ")")[0].textContent.replace(" ", "");
                                }
                                else {
                                    QuoteNum = $(this).find("td:eq(" + (i - 1) + ")")[0].innerText.replace(" ", "");
                                }
                                count += 1;
                            }
                            if (ColName == 'ACCOUNT NO.') {
                                if (FIREFOX) {
                                    AccountNum = $(this).find("td:eq(" + (i - 1) + ")")[0].textContent.replace(" ", "");
                                }
                                else {
                                    AccountNum = $(this).find("td:eq(" + (i - 1) + ")")[0].innerText.replace(" ", "");
                                }
                                count += 1;
                            }
                            if (count == 2)
                                break;
                        }

                        if (QuoteNum) {
                            setCookie('QNo', QuoteNum);
                            setCookie('QNoTemp', QuoteNum); 
                            setCookie('A1No', AccountNum);
                            //onSuccessDocNumber();
                            //PageMethods.GetDatafromXMLDocNumber(QuoteNum, onSuccessDocNumber);
                            QuoteClick();
                        }
                        else {
                            setCookie('QNo', QuoteNum, -1);
                            setCookie('QNoTemp', QuoteNum, -1);
                            setCookie('A1No', AccountNum, -1);
                        }
                    }

                });

                $('#btnClearNewQuote').click(function () {
                    $get('txtAddAccountNumber').value = '';
                    $get('txtAddAccountName').value = '';
                    $get('txtAddQuoteDoc').value = '';
                    $get('txtAddQuoteSalesTeamIn').value = '';
                    $get('txtAddQuoteSalesTeamCurrent').value = '';
                    $get('txtAddQuoteValue').value = '';
                    return false;
                });

                $('#btnExportToExcel').click(function () {
                    var mydiv = $('#pnlDateRange')
                    mydiv.dialog({ autoOpen: false,
                        title: "Export to Excel",
                        resizable: false,
                        width: 530,
                        height: 300,
                        modal: false,
                        dialogClass: "DialogQuotePipeline",
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag 
                            var t = new Date();
                            var month = t.getMonth() + 1;
                            var day = t.getDate();
                            var year = t.getFullYear();
                            if (month < 10) {
                                $get('txtStartDateQuote1K').value = "0" + month + "/" + day + "/" + year;
                                $get('txtEndDateQuote1K').value = "0" + month + "/" + day + "/" + year;
                            }
                            else {
                                $get('txtStartDateQuote1K').value = month + "/" + day + "/" + year;
                                $get('txtEndDateQuote1K').value = month + "/" + day + "/" + year;
                            }
                        }
                    });

                    mydiv.dialog('open');

                    return false;

                });


                $('#btnAddNewQuote').click(function () {
                    var mydiv = $('#pnlNewQuote')
                    mydiv.dialog({ autoOpen: false,
                        title: "Add New Quote",
                        resizable: false,
                        width: 330,
                        height: "auto",
                        modal: true,
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag 
                            var t = new Date();
                            var month = t.getMonth() + 1;
                            var day = t.getDate();
                            var year = t.getFullYear();
                            if (month < 10) {
                                $get('txtAddQuoteDay').value = "0" + month + "/" + day + "/" + year;
                            }
                            else {
                                $get('txtAddQuoteDay').value = month + "/" + day + "/" + year;
                            }
                        }
                    });

                    mydiv.dialog('open');

                    return false;
                });

                $("#grdQuoteOver1K tr").not($("#grdQuoteOver1K tr").eq(0)).dblclick(function () {

                    if ($(this).closest("tr")[0].rowIndex > 0 && $(this).closest("tr")[0].rowIndex <= 200) {


                        $("#grdQuoteOver1K tr").removeClass('SelectedRowStyle');

                        if ($(this).hasClass('even')) {
                            $(this).removeClass();
                            $(this).addClass('even SelectedRowStyle');
                        }
                        else
                            $(this).addClass('SelectedRowStyle');

                        var RowID;
                        var Row;
                        $("#grdQuoteOver1K tr").closest('TR').removeClass('SelectedRowStyle');
                        $(this).addClass('SelectedRowStyle');

                        //                        var totalCols = $("#grdQuoteOver1K").find('tr')[0].cells.length;

                        //                        for (var i = 0; i < totalCols + 1; i++) {
                        //                            var ColName = $('#grdQuoteOver1K  tr').find('th:nth-child(' + i + ')').text();
                        //                            if (ColName == 'ROW' || ColName == 'Row') {
                        //                                Row = $(this).find("td:eq(" + (i - 1) + ")").html();
                        //                            }
                        //                        }

                        //Get the Index of Selected Row
                        $get('SelectedRow').value = $(this).index();
                        onSuccessRow($(this).index());

                        //  PageMethods.GetDatafromXMLRow(Row, onSuccessRow);
                    }

                    return false;
                });

                // $("#grdQuoteOver1K").tablesorter({ widgets: ['zebra'], widthFixed: true });

                // $("#grdQuoteOver1K").tablesorterPager({ container: $("#pager"), size: 300, fixedHeight: true });

            });
        }

        function onfocusGridview() {
            var index = 0;
            //Retain Color
            $("#grdQuoteOver1K tr").children("td:nth-child(2)").each(function () {

                if (index % 2 != 0) {
                    if ($(this).parent().hasClass('SelectedRowStyle')) {
                        $(this).parent().removeClass();
                        $(this).parent().addClass('SelectedRowStyle Odd');
                    }
                    else {
                        $(this).parent().removeClass();
                        $(this).parent().addClass('Odd');
                    }
                }
                else {
                    if ($(this).parent().hasClass('SelectedRowStyle')) {
                        $(this).parent().removeClass();
                        $(this).parent().addClass('SelectedRowStyle even');
                    }
                    else {
                        $(this).parent().removeClass();
                        $(this).parent().addClass('even');
                    }
                }

                index = index + 1;

                return true;

            });
        }

        function CheckString(val) {
            if (val)
                return val;
            else
                return "";
        }

        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                var mydiv = $('#pnlNewQuote');
                mydiv.dialog('close');
                mydiv = $('#pnlQuoteOver1K');
                mydiv.dialog('close');
            }
        }
        function onchange() {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            var sum;
            var num = $get('txtEditQuoteValue').value;
            var MoneyFormat;

            if ($get('ddlProbablilty').value != "-1.00 %" && $get('ddlProbablilty').value != "") {

                if (num != '') {

                    if ($get('CurrencyCode').value == "GBP") {
                        sum = parseFloat(num.replace('£', '').replace(',', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, "£", 2);
                    }
                    else if ($get('CurrencyCode').value == "EUR") {
                        sum = parseFloat(num.replace('.', '').replace(',', '.').replace(' €', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, { symbol: " €", format: "%v %s", thousand: ".", decimal: ",", precision: 2 });
                    }
                    else if ($get('CurrencyCode').value == "CHF") {
                        sum = parseFloat(num.replace("'", '').replace('fr. ', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, { symbol: "fr.", format: "%s %v", thousand: "'", decimal: ".", precision: 2 });
                    }
                    else {
                        sum = parseFloat(num.replace('$', '').replace(',', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, "$", 2);
                    }
                }
                else
                    MoneyFormat = "";
            }
            else if ($get('ddlProbablilty').value == "-1.00 %") {
                sum = -1;
                if ($get('CurrencyCode').value == "GBP") {
                    MoneyFormat = '£' + sum;
                }
                else if ($get('CurrencyCode').value == "EUR") {
                    MoneyFormat = sum + " €";
                }
                else if ($get('CurrencyCode').value == "CHF") {
                    MoneyFormat = 'fr. ' + sum;
                }
                else {
                    MoneyFormat = '$' + sum;
                }
            }
            else {
                sum = "";
                MoneyFormat = "";
            }

            $get('Weighted').value = sum;

            if (FIREFOX)
                $get('lblWeightedValue').textContent = MoneyFormat;
            else
                $get('lblWeightedValue').innerText = MoneyFormat;

            if ($get('ddlProbablilty').value == "1.00 %")
                $get('SelectedRetain').value = "0";
            else
                $get('SelectedRetain').value = "1";
        }

        function onkeyup() {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            var sum;
            var num = $get('txtEditQuoteValue').value;
            var MoneyFormat;

            if ($get('ddlProbablilty').value != "-1.00 %" && $get('ddlProbablilty').value != "") {

                if (num != '') {

                    if ($get('CurrencyCode').value == "GBP") {
                        sum = parseFloat(num.replace('£', '').replace(',', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, "£", 2);
                    }
                    else if ($get('CurrencyCode').value == "EUR") {
                        sum = parseFloat(num.replace('.', '').replace(',', '.').replace(' €', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, { symbol: " €", format: "%v %s", thousand: ".", decimal: ",", precision: 2 });
                    }
                    else if ($get('CurrencyCode').value == "CHF") {
                        sum = parseFloat(num.replace("'", '').replace('fr. ', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, { symbol: "fr.", format: "%s %v", thousand: "'", decimal: ".", precision: 2 });
                    }
                    else {
                        sum = parseFloat(num.replace('$', '').replace(',', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, "$", 2);
                    }
                }
                else
                    MoneyFormat = "";
            }
            else if ($get('ddlProbablilty').value == "-1.00 %") {
                sum = -1;
                if ($get('CurrencyCode').value == "GBP") {
                    MoneyFormat = '£' + sum;
                }
                else if ($get('CurrencyCode').value == "EUR") {
                    MoneyFormat = sum + " €";
                }
                else if ($get('CurrencyCode').value == "CHF") {
                    MoneyFormat = 'fr. ' + sum;
                }
                else {
                    MoneyFormat = '$' + sum;
                }
            }
            else {
                sum = "";
                MoneyFormat = "";
            }

            $get('Weighted').value = sum;

            if (FIREFOX)
                $get('lblWeightedValue').textContent = MoneyFormat;
            else
                $get('lblWeightedValue').innerText = MoneyFormat;

            if ($get('ddlProbablilty').value == "1.00 %")
                $get('SelectedRetain').value = "0";
            else
                $get('SelectedRetain').value = "1";

        }



        function onSuccessQuote(data) {
            if (data != null) {
                if (data.rows != null) {
                    var i = confirm('This quote number already exist. Do you want still to add this?');
                    if (i) {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return true;
        }

        function Message() {
            return PageMethods.GetDatafromXMLDocNumber($get('txtAddQuoteDoc').value, onSuccessQuote);
        }

        function SaveSuccess() {
            alert("Successsfully Saved.\nPlease click the refresh button.");
            return true;
        }

        function onSuccessShow(data) {
            var mydiv = $('#pnlQuoteOver1K');
            mydiv.dialog({ autoOpen: false,
                title: "Quote Pipeline",
                resizable: false,
                width: "auto",
                modal: false,
                dialogClass: "FixedPostionTest",
                open: function (type, data) {
                    $(this).parent().appendTo("form"); //won't postback unless within the form tag

                }
            });

            mydiv.dialog('open');

        }

        function onSuccesQuoteUpdate() {
            onUpdatingQuoteValue();
        }

        function onSuccesAddNewQuote() {

            onUpdateQuoteValueSuccess();
        }

        function onSaveQuoteComputing() {

            var Campaign = document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value;
            var AccountNumSave = $get('AccountNum').value;
            var CompetitionSave = $get('txtCompetition').value;
            var NotesSave = $get('txtNotes').value;
            var ProbabliltySave = $get('ddlProbablilty').value;
            var ProposedDate = $get('txtProposedClose').value;
            var Quote_Doc = $get('QuoteDoc').value;
            var Weighted = $get('Weighted').value;
            var WinorLoss = $('#ddlProbablilty').find(":selected").text();
            var Status = $get('Status').value;
            var AccountName = $get('AccountName').value;
            var QuoteDay = $get('QuoteDay').value;
            var QSTCurrent = $get('SalesTeamCurrent').value;
            var QSTIn = $get('SalesTeamIn').value;
            var QuoteValue = RemoveCurrencySign($get('txtEditQuoteValue').value);
            var QuoteCost = RemoveCurrencySign($get('QuoteCost').value);
            var ScheDate = $get('txtScheFollow').value;
            var Mining = $('#ddlMining').find(":selected").text();
            var Construction = $('#ddlConstruction').find(":selected").text();
            var ProductLine = $get('txtProductLine').value;
            var QuoteGMPerc = $get('QuoteGMPerc').value;

            if (ProposedDate == "__/__/____")
                ProposedDate = "";

            var QuotePipeline = getCookie('QuotePipiline');

            if (QuotePipeline)
                QuotePipeline = getCookie('QuotePipiline');
            else
                QuotePipeline = "";

            var i;
            var grd = document.getElementById('grdQuoteOver1K');
            //Hiding Row
            if ((QuotePipeline == '') || (QuotePipeline == 'OpenQuotes')) {
                if ((WinorLoss == 'Won') || (WinorLoss == 'Loss') || (WinorLoss == 'Duplicate')) {
                    var rows = document.getElementById('grdQuoteOver1K').rows;
                    if (rows) {
                        if ($get('SelectedRow').value != '') {
                            i = parseInt($get('SelectedRow').value)
                            grd.deleteRow(i + 1);
                        }
                    }
                }
                else {
                    updateGridview($get('SelectedRow').value);
                }
            }
            else {
                if ((WinorLoss != 'Won') && (WinorLoss != 'Loss') && (WinorLoss != 'Duplicate')) {
                    var rows = document.getElementById('grdQuoteOver1K').rows;
                    if (rows) {
                        if ($get('SelectedRow').value != '') {
                            i = parseInt($get('SelectedRow').value)
                            grd.deleteRow(i + 1);
                        }
                    }
                }
                else {
                    updateGridview($get('SelectedRow').value);
                }
            }

            PageMethods.QuoteComputing(CheckString(Campaign), CheckString(AccountNumSave), CheckString(CompetitionSave),
                CheckString(NotesSave), CheckString(ProbabliltySave), CheckString(ProposedDate), CheckString(Quote_Doc),
                CheckString(Weighted), CheckString(WinorLoss), CheckString(Status), CheckString(AccountName),
                CheckString(QuoteDay), CheckString(QSTCurrent), CheckString(QSTIn), CheckString(QuoteValue),
                CheckString(QuoteCost), CheckString(ScheDate), CheckString(Mining), CheckString(Construction),
                CheckString(ProductLine), CheckString(QuoteGMPerc));
            $('.txtAccNum').val(CheckString(Quote_Doc));
            onSuccesQuoteUpdate();
            //onfocusGridview();
        }

        function RemoveCurrencySign(QuoteValue) {
            if ($get('CurrencyCode').value == "GBP") {
                return QuoteValue.replace("£", "");
            }
            else if ($get('CurrencyCode').value == "EUR") {
                return QuoteValue.replace('.', '').replace(',', '.').replace(" €", "");
            }
            else if ($get('CurrencyCode').value == "CHF") {
                return QuoteValue.replace("'", '').replace('fr. ', '');
            }
            else {
                return QuoteValue.replace('$', '');
            }
        }

        function InsertCurrencySign(QuoteValue) {
            if ($get('CurrencyCode').value == "GBP") {
                return accounting.formatMoney(QuoteValue, "£", 2);
            }
            else if ($get('CurrencyCode').value == "EUR") {
                return accounting.formatMoney(QuoteValue, { symbol: " €", format: "%v %s", thousand: ".", decimal: ",", precision: 2 });
            }
            else if ($get('CurrencyCode').value == "CHF") {
                return accounting.formatMoney(QuoteValue, { symbol: "fr.", format: "%s %v", thousand: "'", decimal: ".", precision: 2 });
            }
            else {
                return MoneyFormat = accounting.formatMoney(QuoteValue, "$", 2);
            }
        }

        function updateGrid(Data) {

            var RowIndex = Data + 1;
            var grd = document.getElementById('grdQuoteOver1K');
            var totalCols;
            var AccountNum, QuoteDay, Doc, QuoteValue, QuoteCost, QuoteGMPerc, Probability, Weighted, mydate, Competition, Notes, AccountName, salesTeamIn, SalesTeamCurrent, Status, Productline, Mining, Construction, sched_followup, ProposedDate

            if (grd) {
                if (isNaN(RowIndex) == false) {
                    totalCols = $("#grdQuoteOver1K").find('tr')[0].cells.length;
                    for (var i = 1; i < totalCols + 1; i++) {
                        var ColName = $('#grdQuoteOver1K tr').find('th:nth-child(' + i + ')').text();

                        if (ColName == 'ACCOUNT NO.') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';

                        }
                        else if (ColName == 'QUOTE DAY') {

                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';

                        }
                        else if (ColName == 'QUOTE DOC') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'QUOTE COST') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'QUOTE VALUE') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'QUOTE GM PERCENTAGE') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'CLOSE PROBABILITY') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'WEIGHTED VALUE') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'COMPETITION') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'NOTES') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'ACCOUNT NAME') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'QUOTE ASSIGNMENT') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'ACCOUNT ASSIGNMENT') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'SOURCE') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'PRODUCT LINE') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'PROPOSED DATE') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'MINING') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'CONSTRUCTION') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                        else if (ColName == 'SCHEDULE FOLLOWUP') {
                            grd.rows[RowIndex].cells[i - 1].innerHTML = '123';
                        }
                    }
                }
            }
        }



        function onUpdatingQuoteValue() {

            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            var Campaign = document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value;
            var AccountNumEdit = $get('AccountNum').value;
            var QuoteDocEdit = $get('QuoteDoc').value;
            var QuoteValue = RemoveCurrencySign($get('txtEditQuoteValue').value);
            var WeightedEdit;
            if (FIREFOX)
                WeightedEdit = RemoveCurrencySign($get('lblWeightedValue').textContent);
            else
                WeightedEdit = RemoveCurrencySign($get('lblWeightedValue').innerText);

            var QuoteAssignment = $get('txtEditQuoteAssignment').value;

            PageMethods.UpdateQuoteValue(Campaign, AccountNumEdit, QuoteValue, QuoteDocEdit, WeightedEdit, QuoteAssignment, onUpdateQuoteValueSuccess);
        }

        function onUpdateQuoteValueSuccess(data) {

            //update($get('SelectedRow').value);
            alert("Successsfully Saved.");
            var mydiv = $('#pnlQuoteOver1K');
            mydiv.dialog('close');
        }

        function onKeyupEditQuoteValue() {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            var sum;
            var num;
            var MoneyFormat;
            if ($get('ddlProbablilty').value != "-1.00 %" && $get('ddlProbablilty').value != "") {

                //Check if EditQuoteValue is Valid
                if ($get('txtEditQuoteValue').value != '') {
                    num = $get('txtEditQuoteValue').value;

                    if ($get('CurrencyCode').value == "GBP") {
                        sum = parseFloat(num.replace('£', '').replace(',', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, "£", 2);
                    }
                    else if ($get('CurrencyCode').value == "EUR") {
                        sum = parseFloat(num.replace('.', '').replace(',', '.').replace(' €', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, { symbol: " €", format: "%v %s", thousand: ".", decimal: ",", precision: 2 });
                    }
                    else if ($get('CurrencyCode').value == "CHF") {
                        sum = parseFloat(num.replace("'", '').replace('fr. ', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, { symbol: "fr.", format: "%s %v", thousand: "'", decimal: ".", precision: 2 });
                    }
                    else {
                        sum = parseFloat(num.replace('$', '').replace(',', '')) * (parseFloat($get('ddlProbablilty').value) / 100);
                        MoneyFormat = accounting.formatMoney(sum, "$", 2);
                    }
                }
                else
                    MoneyFormat = "";
            }
            else if ($get('ddlProbablilty').value == "-1.00 %") {
                sum = -1;
                if ($get('CurrencyCode').value == "GBP") {
                    MoneyFormat = '£' + sum;
                }
                else if ($get('CurrencyCode').value == "EUR") {
                    MoneyFormat = sum + " €";
                }
                else if ($get('CurrencyCode').value == "CHF") {
                    MoneyFormat = 'fr. ' + sum;
                }
                else {
                    MoneyFormat = '$' + sum;
                }
            }
            else {
                MoneyFormat = "";
            }

            if (FIREFOX)
                $get('lblWeightedValue').textContent = MoneyFormat;
            else
                $get('lblWeightedValue').innerText = MoneyFormat;

        }

        function onBlurQuoteValue() {
            //$get('txtEditQuoteValue').value = accounting.toFixed($get('txtEditQuoteValue').value.replace('$', '').replace(',', ''), 2);
            var num = $get('txtEditQuoteValue').value;
            if ($get('CurrencyCode').value == "GBP") {
                num = accounting.formatMoney(parseFloat(num.replace('£', '').replace(',', '')), "£", 2);
                $get('txtEditQuoteValue').value = num;
            }
            else if ($get('CurrencyCode').value == "EUR") {
                num = accounting.formatMoney(parseFloat(num.replace('.', '').replace(',', '.').replace(' €', '')), { symbol: " €", format: "%v %s", thousand: ".", decimal: ",", precision: 2 });
                $get('txtEditQuoteValue').value = num;
            }
            else if ($get('CurrencyCode').value == "CHF") {
                num = accounting.formatMoney(parseFloat(num.replace("'", '').replace('fr. ', '')), { symbol: "fr.", format: "%s %v", thousand: "'", decimal: ".", precision: 2 });
                $get('txtEditQuoteValue').value = num;
            }
            else {
                num = accounting.formatMoney(parseFloat(num.replace('$', '').replace(',', '')), "$", 2);
                $get('txtEditQuoteValue').value = num;
            }
        }

        function updateGridview(rowIndexOfGridview) {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            var ri = parseInt(rowIndexOfGridview) + 1;
            var grd = document.getElementById('grdQuoteOver1K');
            var totalCols;
            var xmlDoc, x;
            var y;

            if (grd) {
                if (isNaN(ri) == false) {
                    totalCols = $("#grdQuoteOver1K").find('tr')[0].cells.length;
                    // grd.rows.length - 1;
                    y = 0;
                    for (var i = 0; i < totalCols; i++) {
                        var ColName = $('#grdQuoteOver1K tr').find('th:nth-child(' + i + ')').text();

                        switch (ColName) {
                            case 'CLOSE PROBABILITY':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('ddlProbablilty').value;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('ddlProbablilty').value;

                     
                                break;
                            case 'WEIGHTED VALUE':
//                                if (FIREFOX)
//                                    grd.rows[ri].cells[i - 1].textContent = $get('Weighted').value;
//                                else
//                                    grd.rows[ri].cells[i - 1].innerHTML = $get('Weighted').value;

                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('lblWeightedValue').textContent;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('lblWeightedValue').innerText;
                                    

                                break;
                            case 'PROPOSED DATE':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('txtProposedClose').value;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('txtProposedClose').value;

                          
                                break;
                            case 'COMPETITION':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('txtCompetition').value;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('txtCompetition').value;

                           
                                break;
                            case 'NOTES':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('txtNotes').value;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('txtNotes').value;

                          
                                break;
                            case 'SCHEDULE FOLLOWUP':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('txtScheFollow').value;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('txtScheFollow').value;

                
                                break;
                            case 'PRODUCT LINE':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('txtProductLine').value;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('txtProductLine').value;

                        
                                break;
                            case 'CONSTRUCTION':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $('#ddlConstruction').find(":selected").text();
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $('#ddlConstruction').find(":selected").text();

                            
                                break;
                            case 'MINING':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $('#ddlMining').find(":selected").text();
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $('#ddlMining').find(":selected").text();

                         
                                break;
                            case 'QUOTE VALUE':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('txtEditQuoteValue').value;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('txtEditQuoteValue').value;

                     
                                break;
                            case 'QUOTE ASSIGNMENT':
                                if (FIREFOX)
                                    grd.rows[ri].cells[i - 1].textContent = $get('txtEditQuoteAssignment').value;
                                else
                                    grd.rows[ri].cells[i - 1].innerHTML = $get('txtEditQuoteAssignment').value;

                        
                                break;
                        }

                    }
                }
            }
        }


        function clearDisposableItems(sender, args) {
            if (Sys.Browser.agent == Sys.Browser.InternetExplorer) {
                $get('grdQuoteOver1K').tBodies[0].removeNode(true);
            } else
                $get('grdQuoteOver1K').innerHTML = "";
        }

        window.onload = function () {
            var scrollY = parseInt('<%=Request.Form["scrollY"] %>');
            if (!isNaN(scrollY)) {
                window.scrollTo(0, scrollY);
            }
        };
        window.onscroll = function () {
            var scrollY = document.body.scrollTop;
            if (scrollY == 0) {
                if (window.pageYOffset) {
                    scrollY = window.pageYOffset;
                }
                else {
                    scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
                }
            }
            if (scrollY > 0) {
                var input = document.getElementById("scrollY");
                if (input == null) {
                    input = document.createElement("input");
                    input.setAttribute("type", "hidden");
                    input.setAttribute("id", "scrollY");
                    input.setAttribute("name", "scrollY");
                    document.forms[0].appendChild(input);
                }
                input.value = scrollY;
            }
        };

    </script>
    <div id="container">
        <table border="0" cellpadding="0" cellspacing="0" class="object-container" style="position: fixed;
            height: 40px; width: 100%; margin: 0px; padding: 0px; white-space: nowrap;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <%if (userRule == "CA")
                                                  { %>
                                                <td class="object-wrapper" style="height: 20px" id="Td4" runat="server">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnFilterbyReps" ToolTip="Click to filter by your quotes." Text="Show All Quotes"
                                                        runat="server" CssClass="LabelFont" OnClick="btnFilterbyReps_Click"></asp:LinkButton>
                                                    &nbsp;
                                                </td>
                                                <% }%>
                                                <td class="object-wrapper" style="height: 20px" id="lnkExportToExcel" runat="server">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnExportToExcel" ToolTip="Click to Export to Excel" runat="server"
                                                        CssClass="LabelFont" ClientIDMode="Static">Export to Excel</asp:LinkButton>
                                                    &nbsp;
                                                </td>
                                                <td class="object-wrapper" style="height: 20px" id="Td1" runat="server">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnAddNewQuote" ToolTip="Add New Quote" ClientIDMode="Static"
                                                        runat="server" CssClass="LabelFont">Add New Quote</asp:LinkButton>
                                                    &nbsp;
                                                </td>
                                                <td class="object-wrapper" style="height: 20px" id="Td2" runat="server">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnShowWonOrLossOnly" ToolTip="Show Won or Loss" ClientIDMode="Static"
                                                        runat="server" OnClick="btnShowWonOrLoss_Click" CssClass="LabelFont">Show Won or Loss</asp:LinkButton>
                                                    &nbsp;
                                                </td>
                                                <td class="object-wrapper" style="height: 20px" id="Td3" runat="server">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnShowOpenQuotes" ToolTip="Show Open Quotes" ClientIDMode="Static"
                                                        runat="server" OnClick="btnShowOpenQuotes_Click" CssClass="LabelFont">Show Open Quotes</asp:LinkButton>
                                                    &nbsp;
                                                </td>
                                                <td class="object-wrapper" style="height: 20px">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkArrange" Text="Arrange Quote Pipeline Columns" OnClick="ArrangeColumn_Click"
                                                        CssClass="LabelFont" runat="server"></asp:LinkButton>
                                                </td>
                                                <td class="object-wrapper" style="height: 20px">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkRefresh" Text="Refresh" OnClick="lnkRefresh_Click" CssClass="LabelFont"
                                                        runat="server"></asp:LinkButton>
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
        </table>
        <asp:Label ID="lblLabelQuotes" CssClass="LabelFont" runat="server" Text=""></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="table-wrapper page4">
                    <table style="border: 0; width: 100%; height: 100%" cellpadding="10" cellspacing="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" CssClass="GridHeaderLabel" Text="Table Name:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTitleQuotes1K" runat="server" CssClass="GridHeaderLabel" Text="Open Quotes"></asp:Label>
                                        </td>
                                    </tr>
                                    <%if (userRule == "CA")
                                      { %>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="GridHeaderLabel" Text="Filter By:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFilterByQuotes1K" runat="server" CssClass="GridHeaderLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <% }%>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ScrollBars="None" ID="DataDiv" Width="100%">
                                    <asp:GridView ID="grdQuoteOver1K" runat="server" AutoGenerateColumns="false" ClientIDMode="Static"
                                        GridLines="None" AsyncRendering="false" EmptyDataText="No data available." CellPadding="4"
                                        CellSpacing="2" Font-Size="12px" ShowHeaderWhenEmpty="true" BackColor="#FFFFFF"
                                        PageSize="200" ForeColor="Black" AllowPaging="true" OnRowDataBound="grdQuoteOver1K_RowDataBound"
                                        OnPageIndexChanging="grdQuoteOver1K_PageIndexChanging" AllowSorting="true" OnSorting="gridQuoteOpen1K_Sorting">
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" Wrap="false" />
                                        <AlternatingRowStyle BackColor="#e5e5e5" />
                                        <PagerStyle CssClass="grid_paging" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <%--        <div id="pager" style="position: fixed;">
                        <img src="../../App_Themes/Images/New Design/first.png" class="first" />
                        <img src="../../App_Themes/Images/New Design/prev.png" class="prev" />
                        <input type="text" class="pagedisplay" />
                        <img src="../../App_Themes/Images/New Design/next.png" class="next" />
                        <img src="../../App_Themes/Images/New Design/last.png" class="last" />
                        <select class="pagesize">
                            <option value="300">300 per page</option>
                            <option value="400">400 per page</option>
                            <option value="500">500 per page</option>
                        </select>
                    </div>--%>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportToExcel" />
                <asp:AsyncPostBackTrigger ControlID="btnShowWonOrLossOnly" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnShowOpenQuotes" EventName="Click" />
                <asp:PostBackTrigger ControlID="lnkRefresh" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:Panel ID="pnlQuoteOver1K" runat="server" Style="display: none;" ClientIDMode="Static">
        <table>
            <tr>
                <td style="border: 1px solid; padding: 10px;">
                    <table style="padding-bottom: 15px;">
                        <tr style="padding: 5px;">
                            <td>
                                <asp:Label ID="lblProposedClose" CssClass="LabelFont" runat="server" Text="Proposed Close Date"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtProposedClose" CssClass="textbox curved" Style="font-size: 12px;"
                                                ClientIDMode="Static" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgstartCal" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                                runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtProposedClose"
                                                PopupButtonID="imgstartCal">
                                            </asp:CalendarExtender>
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtProposedClose"
                                                Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" InputDirection="RightToLeft"
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblScheFollow" CssClass="LabelFont" runat="server" Text="Schedule Followup"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtScheFollow" CssClass="textbox curved" Style="font-size: 12px;"
                                                ClientIDMode="Static" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                                runat="server" />
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtScheFollow"
                                                PopupButtonID="ImageButton2">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProbablilty" CssClass="LabelFont" runat="server" Text="Close Probablilty"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlProbablilty" runat="server" ClientIDMode="Static" CssClass="textbox curved"
                                                Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="90.00 %"></asp:ListItem>
                                                <asp:ListItem Value="75.00 %"></asp:ListItem>
                                                <asp:ListItem Value="50.00 %"></asp:ListItem>
                                                <asp:ListItem Value="30.00 %"></asp:ListItem>
                                                <asp:ListItem Value="25.00 %"></asp:ListItem>
                                                <asp:ListItem Value="10.00 %"></asp:ListItem>
                                                <asp:ListItem Text="Won" Value="100.00 %"></asp:ListItem>
                                                <asp:ListItem Text="Loss" Value="1.00 %"></asp:ListItem>
                                                <asp:ListItem Text="Duplicate" Value="-1.00 %"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblWeighted" CssClass="LabelFont" runat="server" Text="Weighted Value"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblWeightedValue" CssClass="textboxLabel" runat="server" ClientIDMode="Static"
                                                Text=""></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCompetition" CssClass="LabelFont" runat="server" Text="Competition"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCompetition" CssClass="textbox curved" Style="font-size: 12px;"
                                                ClientIDMode="Static" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNotes" CssClass="LabelFont" runat="server" Text="Notes"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtNotes" CssClass="textbox curved" TextMode="MultiLine" MaxLength="300"
                                                Style="font-size: 12px; resize: none;" Height="80px" Width="200px" ClientIDMode="Static"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblProductLine" CssClass="LabelFont" runat="server" Text="Product Line"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtProductLine" CssClass="textbox curved" Style="font-size: 12px;"
                                                ClientIDMode="Static" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblConstruction" CssClass="LabelFont" runat="server" Text="Construction"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlConstruction" runat="server" ClientIDMode="Static" CssClass="textbox curved"
                                                Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="YES"></asp:ListItem>
                                                <asp:ListItem Value="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMining" CssClass="LabelFont" runat="server" Text="Mining"></asp:Label>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlMining" runat="server" ClientIDMode="Static" CssClass="textbox curved"
                                                Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="YES"></asp:ListItem>
                                                <asp:ListItem Value="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEditQuoteValue" CssClass="LabelFont" runat="server" Text="Quote Value"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditQuoteValue" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEditQuoteAssignment" CssClass="LabelFont" runat="server" Text="Quote Assignment"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditQuoteAssignment" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="float: right;">
                        <tr>
                            <td>
                                <input name="btnSave" id="btnSave" class="button" value="Save" style="font-size: 12px;
                                    width: 60px;" onclick="onSaveQuoteComputing()" type="button" />
                            </td>
                            <td>
                                <%--<asp:Button ID="btnCancel" runat="server" ClientIDMode="Static" CssClass="button"
                                    Text="Close" Style="font-size: 12px;" Width="60px" />--%>
                                <input name="btnCancel" id="btnCancel" class="button" value="Close" style="font-size: 12px;
                                    width: 60px;" type="button" onclick="onclickclose()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlNewQuote" runat="server" Style="display: none;" ClientIDMode="Static">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblAddQuoteDay" CssClass="LabelFont" runat="server" Text="Quote Day"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAddQuoteDay" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                    runat="server" />
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtAddQuoteDay"
                                    PopupButtonID="ImageButton1">
                                </asp:CalendarExtender>
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtAddQuoteDay"
                                    Mask="99/99/9999" MessageValidatorTip="false" MaskType="Date" InputDirection="RightToLeft"
                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="True" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddAccountNumber" CssClass="LabelFont" runat="server" Text="Account No."></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAddAccountNumber" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddAccountName" CssClass="LabelFont" runat="server" Text="Account Name"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAddAccountName" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddQuoteDoc" CssClass="LabelFont" runat="server" Text="Quote Doc"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAddQuoteDoc" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddQuoteSalesTeamIn" CssClass="LabelFont" runat="server" Text="Quote Assignment"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAddQuoteSalesTeamIn" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddQuoteSalesTeamCurrent" CssClass="LabelFont" runat="server" Text="Account Assignment"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAddQuoteSalesTeamCurrent" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddQuoteValue" CssClass="LabelFont" runat="server" Text="Quote Value"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAddQuoteValue" CssClass="textbox curved" Style="font-size: 12px;"
                                    ClientIDMode="Static" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="float: right;">
            <tr>
                <td>
                    <asp:Button ID="btnSaveNewQuote" runat="server" ClientIDMode="Static" CssClass="button"
                        Text="Save" Style="font-size: 12px;" Width="60px" OnClientClick="return Message();"
                        OnClick="btnSaveNewQuote_Click" />
                </td>
                <td>
                    <asp:Button ID="btnClearNewQuote" runat="server" ClientIDMode="Static" CssClass="button"
                        Text="Clear" Style="font-size: 12px;" Width="60px" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="txtAccNum" CssClass="txtAccNum" Font-Size=".1px"
                        ForeColor="White" Width=".1px" Height=".1px" BorderStyle="None"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDateRange" runat="server" Style="display: none;" ClientIDMode="Static">
        <asp:UpdatePanel ID="updateDateRange" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlBox" runat="server">
                    <table>
                        <tr>
                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:RadioButtonList ID="rdoSelectQuote1K" Style="margin-right: 20px; font-size: 14px;
                                    font-family: Arial; width: 300px;" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Text=" All" Value="A"></asp:ListItem>
                                    <asp:ListItem Text=" Date Range" Value="DR" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:Label ID="Label4" Text="Start Date:" ClientIDMode="Static" runat="server" CssClass="LabelFont" />
                            </td>
                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:TextBox CssClass="textbox curved" ClientIDMode="Static" ID="txtStartDateQuote1K"
                                    runat="server" Style="font-size: 12px; width: 100px;"></asp:TextBox>
                            </td>
                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:ImageButton ID="imgstartCalQuote1K" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                    runat="server" />
                            </td>
                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDateQuote1K"
                                    PopupButtonID="imgstartCalQuote1K">
                                </asp:CalendarExtender>
                            </td>
                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:Label ID="Label1" ClientIDMode="Static" CssClass="LabelFont" Text="End Date:"
                                    runat="server" />
                            </td>
                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:TextBox CssClass="textbox curved" ClientIDMode="Static" ID="txtEndDateQuote1K"
                                    runat="server" Style="font-size: 12px; width: 100px;"> </asp:TextBox>
                            </td>
                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:ImageButton ID="imgEndCalQuote1K" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                    runat="server" />
                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDateQuote1K"
                                    PopupButtonID="imgEndCalQuote1K">
                                </asp:CalendarExtender>
                            </td>
                            <%--  <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDateQuote1K"
                                    PopupButtonID="imgEndCalQuote1K" >
                                </asp:CalendarExtender>
                            </td>--%>
                        </tr>
                    </table>
                    <br />
                    <table style="position: absolute; bottom: 0px; right: 0px;">
                        <tr>
                            <td>
                                <asp:Button ID="btnExportExcelQuote1k" runat="server" ClientIDMode="Static" class="button"
                                    Text="Ok" Style="font-size: 12px; width: 60px;" OnClick="btn_Export2ExcelClick" />
                            </td>
                            <td class="object-wrapper">
                                <asp:Button ID="btnCancelQuote1K" runat="server" ClientIDMode="Static" class="button"
                                    Text="Cancel" Style="font-size: 12px; width: 60px;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportExcelQuote1k" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:HiddenField ID="QuoteDoc" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="AccountNum" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Date" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="QuoteValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Weighted" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="SelectedRow" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="SelectedRetain" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="AccountName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="QuoteDay" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="SalesTeamIn" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="SalesTeamCurrent" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Status" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="QuoteCost" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="QuoteGMPerc" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="ProductLine" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Construction" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="Mining" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="ScheDate" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="NewUpdate" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="PathName" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="CurrencyCode" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="Hdflnkcolindex" runat="server" />
    <asp:HiddenField ID="HdfWeightedindex" runat="server" />
    <asp:HiddenField ID="HdfProbabliltyindex" runat="server" />
    <asp:HiddenField ID="HdfGM_perc_indx" runat="server" />
    <asp:HiddenField ID="HdfQuoteCost" runat="server" />
    <asp:HiddenField ID="HdfQuoteValue" runat="server" />
</asp:Content>
