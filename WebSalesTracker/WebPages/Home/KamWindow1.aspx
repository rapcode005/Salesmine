<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KamWindow1.aspx.cs" Inherits="WebSalesMine.WebPages.Home.KamWindow1"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="AppLogic" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="TitleV" runat="server" clientidmode="Static">kam Window </title>
    <link href="../../App_Themes/CSS/styles.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/KamCss.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/CSS/PopUpModal.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Theme_Roller/jquery-ui-1.8.17.custom/css/custom-theme/jquery-ui-1.8.17.custom.css"
        rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/JS/AutoComplete/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../../App_Themes/JS/AutoComplete/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../App_Themes/JS/JsGrid/DefaultScript.js" type="text/javascript"></script>
    <script src="../../App_Themes/JS/JSSS/CustomPaging.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Asc = ' <img src=\"../../App_Themes/Images/asc.gif\" /> ',
            Desc = ' <img src=\"../../App_Themes/Images/desc.gif\" /> ';

        function HideColumn() {
            $(function () {
                //Hide Column
                var thNotes = $("#gridShowAllUserData th:contains('Notes')");
                thNotes.css("display", "none");
                $("#gridShowAllUserData tr").each(function () {
                    $(this).find("td").eq(thNotes.index()).css("display", "none");
                });

                var thType = $("#gridShowAllUserData th:contains('TYPE')");
                thType.css("display", "none");
                $("#gridShowAllUserData tr").each(function () {
                    $(this).find("td").eq(thType.index()).css("display", "none");
                });

                var thCreatedon = $("#gridShowAllUserData th:contains('CREATEDON')");
                thCreatedon.css("display", "none");
                $("#gridShowAllUserData tr").each(function () {
                    $(this).find("td").eq(thCreatedon.index()).css("display", "none");
                });

            });
        }

        function onMouseHover() {
            $('#gridShowAllUserData').css("cursor", "pointer");
        }

        function checkstring(str) {
            if (str)
                return str.trim();
            else
                return "";
        }
        //pads left
        function lpad(str, padString, length) {
            while (str.length < length)
                str = padString + str;
            return str;
        }

        //pads right
        function rpad(str, padString, length) {
            while (str.length < length)
                str = str + padString;
            return str;
        }

        function onSuccessUpdateNoteMessage() {
            var index = 0;
            alert("Successsfully Update.");
            return true;
        }

        function onSuccessSearchAccount(data) {

            $get('txtNoteKam').value = checkstring(data.rows[0]['NOTES']);

            var myDate = data.rows[0]['CREATEDON'];

            if (Object.prototype.toString.call(myDate) === "[object Date]") {

                var Date = myDate.getFullYear() + "-" + lpad((myDate.getMonth() + 1).toString(), "0", 2) + "-"
                + lpad(myDate.getDate().toString(), "0", 2) + " " + lpad(myDate.getHours().toString(), "0", 2) + ":" + lpad(myDate.getMinutes().toString(), "0", 2) +
                ":" + lpad(myDate.getSeconds().toString(), "0", 2) + ":" + lpad(myDate.getMilliseconds().toString(), "0", 3);

                $get('htdCreatedon').value = Date;

                var Type = checkstring(data.rows[0]['TYPE']);

                if (Type != "Follow up") {
                    $get('ddlNoteType').value = Type;
                }
                else {
                    $get('ddlNoteType').value = Type.replace("u", "U");
                }

                $('#btnUpdateNoteKam').show();
            }
            else {

                $('#btnUpdateNoteKam').hide();
                $get('ddlNoteType').value = "";
            }



            //Open Popup
            var mydiv = $('#pnlPopupNote');

            mydiv.dialog({ autoOpen: false,
                title: "Notes",
                resizable: false,
                width: 400,
                modal: false,
                dialogClass: "FixedPostionTest",
                closeOnEscape: true,
                open: function (type, data) {
                    $(this).parent().appendTo("form"); //won't postback unless within the form tag
                }
            });

            mydiv.dialog('open');
        }

        function OnSuccessGrid(response) {
            if (response.d) {
                if (response.d.length > 0) {
                    var myDate;
                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");
                    var row = $("#gridShowAllUserData tr:last-child").clone(true);
                    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                           "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                        ];
                    var Count = 1, AllAddedAccountNum = [];

                    $("#gridShowAllUserData tr").not($("#gridShowAllUserData tr").eq(0)).remove();
                    $.each(customers, function () {
                        var Search;
                        var SearchText = $get('txtbSearchKam').value;

                        if (AllAddedAccountNum.indexOf($(this).find("Sold_to").text()) < 0) {
                            AllAddedAccountNum.push($(this).find("Sold_to").text());

                            //Add to Gridview
                            $("td", row).eq(0).html($(this).find("Sold_to").text());

                            if (CheckSearchBy() == "Name") {

                                Search = $(this).find("Name").text();

                                if ($get('txtbSearchKam').value != '') {
                                    SearchText = SearchText.replace(/(\s+)/, "(<[^>]+>)*$1(<[^>]+>)*");
                                    var pattern = new RegExp("(" + SearchText + ")", "gi");
                                    Search = Search.replace(pattern, "<mark>$1</mark>");
                                    // Search = Search.replace(/(<mark>[^<>]*)((<[^>]+>)+)([^<>]*<\/mark>)/, "$1</mark>$2<mark>$4");
                                }

                                $("td", row).eq(1).html(Search);
                                $("td", row).eq(2).html($(this).find("MG_NAME").text());

                            }
                            else {

                                Search = $(this).find("MG_NAME").text();

                                if ($get('txtbSearchKam').value != '') {
                                    SearchText = SearchText.replace(/(\s+)/, "(<[^>]+>)*$1(<[^>]+>)*");
                                    var pattern = new RegExp("(" + SearchText + ")", "gi");
                                    Search = Search.replace(pattern, "<mark>$1</mark>");
                                    // Search = Search.replace(/(<mark>[^<>]*)((<[^>]+>)+)([^<>]*<\/mark>)/, "$1</mark>$2<mark>$4");
                                }

                                $("td", row).eq(1).html($(this).find("Name").text());
                                $("td", row).eq(2).html(Search);

                            }

                            if ($(this).find("LPDCUST").text() != "") {
                                myDate = new Date($(this).find("LPDCUST").text());
                                $("td", row).eq(3).html(monthNames[myDate.getMonth()] + " " +
                                             ("0" + myDate.getDate()).slice(-2) + ", " +
                                             myDate.getFullYear());
                            }
                            else
                                $("td", row).eq(3).html("");

                            if ($(this).find("Retention").text() != "") {
                                var Percent;
                                Percent = Number($(this).find("Retention").text()) * 100;
                                $("td", row).eq(4).html(Percent.toFixed(2));
                            }
                            else
                                $("td", row).eq(4).html("");


                            if ($(this).find("sales12M").text() != "") {
                                $("td", row).eq(5).html(Number($(this).find("sales12M").text()).toFixed(2));
                            }
                            else
                                $("td", row).eq(5).html("");

                            if ($(this).find("sales13to24M").text() != "") {
                                $("td", row).eq(6).html(Number($(this).find("sales13to24M").text()).toFixed(2));
                            }
                            else
                                $("td", row).eq(6).html("");

                            if ($(this).find("LTSALES").text() != "") {
                                $("td", row).eq(7).html(Number($(this).find("LTSALES").text()).toFixed(2));
                            }
                            else {
                                $("td", row).eq(7).html("");
                            }

                            if ($(this).find("Date").text() != "") {
                                myDate = new Date($(this).find("Date").text());
                                $("td", row).eq(8).html(monthNames[myDate.getMonth()] + " " +
                                             ("0" + myDate.getDate()).slice(-2) + ", " +
                                             myDate.getFullYear());
                            }
                            else
                                $("td", row).eq(8).html("");

                            $("td", row).eq(9).html($(this).find("NOTES").text());
                            $("td", row).eq(10).html($(this).find("TYPE").text());

                            $("td", row).eq(11).html($(this).find("CREATEDON").text());


                            if ('<%=SessionFacade.CampaignName%>' == 'DE' ||
                            '<%=SessionFacade.CampaignName%>' == 'FR' ||
                            '<%=SessionFacade.CampaignName%>' == 'UK') {
                                if ($(this).find("OnHoldDate").text() != "") {
                                    myDate = new Date($(this).find("OnHoldDate").text());
                                    $("td", row).eq(12).html(monthNames[myDate.getMonth()] + " " +
                                                 ("0" + myDate.getDate()).slice(-2) + ", " +
                                                 myDate.getFullYear());
                                }
                                else
                                    $("td", row).eq(12).html("");
                            }


                            //Alternative rows color
                            if (Count % 2 != 0) {
                                row.removeClass();
                                row.addClass("Rows");
                            }
                            else {
                                row.removeClass();
                                row.addClass("RowsWhite");
                            }

                            $("#gridShowAllUserData").append(row);

                            Count = Count + 1;

                            row = $("#gridShowAllUserData tr:last-child").clone(true);
                        }


                    });

                    var pager = xml.find("Pager");
                    $(".Pager").CustomPaging({
                        ActiveCssClass: "current",
                        PagerCssClass: "Pager",
                        PageIndex: parseInt(pager.find("PageIndex").text()),
                        PageSize: parseInt(pager.find("PageSize").text()),
                        TotalSize: parseInt(pager.find("RecordCount").text())
                    });

                    HideColumn();
                }
            }
            $("body").removeClass("loading");
        }

        function GetAllData(num, search, searchby) {

            $("body").addClass("loading");

            var DataParamater;

            DataParamater = "{PageNum: '" + num + "',Search: '" + search + "',Searchby: '" +
            searchby + "'}";

            $.ajax({
                type: 'POST',
                url: 'KamWindow1.aspx/GetKAMData',
                data: DataParamater,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: true,
                success: OnSuccessGrid,
                failure: function (response) {
                    alert('Fail');
                    $("body").removeClass("loading");
                },
                error: function (response, textStatus, errorThrown) {
                    alert(errorThrown);
                    $("body").removeClass("loading");
                }
            });

        }

        function GetAllDataSort(num, sortby, ascdsc, search, searchby) {

            $("body").addClass("loading");

            var DataParamater;

            DataParamater = "{Searchby:'" + searchby + "',PageNum: '" + num + "',Sortby: '" + sortby + "',AscDsc: '"
            + ascdsc + "',Search: '" + search + "'}";

            $.ajax({
                type: "POST",
                url: "KamWindow1.aspx/GetDatafromXMLSort",
                data: DataParamater,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: OnSuccessGrid,
                failure: function (response) {
                    alert(response.d);
                    $("body").removeClass("loading");
                },
                error: function (response) {
                    alert(response.d);
                    $("body").removeClass("loading");
                }
            });

        }

        function onSuccessUpdateNoteMessage() {
            alert("Successsfully Update.");
            return true;

        }

        function CheckBrowser(e) {
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox
            return key;
        }

        function CheckSearchBy() {
            if ($get('rdoAccountName').checked == true)
                return 'Name';
            else
                return 'MG_NAME'; 
        }

        // FUNCTION TO RUN AFTER PARTIAL POSTBACK
        function pageLoad() {

            var DataAutoCom;
            var tableOffset;
            var $header;
            var $fixedHeader;
            var Notes, Type;
           
            var FIREFOX = /Firefox/i.test(navigator.userAgent);

            $addHandler(document.getElementById('gridShowAllUserData'), 'mouseover', onMouseHover);

            $(document).ready(function () {

                //For Search Label
                $('#trInstruction').css("display", "none");

                //First Load
                GetAllData(1, '', CheckSearchBy());

                $get('txtbSearchKam').value = '';

                $('#RefreshKam').click(function (e) {
                    window.location.href = "KamWindow1.aspx";
                });

                //Paging 
                $(".Pager .page").live("click", function () {
                    if ($get('Sort').value != "") {
                        if ($get('AscDsc').value == "")
                            GetAllDataSort(parseInt($(this).attr('page')), $get('OrderBy').value, "rn", $get('txtbSearchKam').value, CheckSearchBy());
                        else
                            GetAllDataSort(parseInt($(this).attr('page')), $get('OrderBy').value, $get('AscDsc').value, $get('txtbSearchKam').value, CheckSearchBy());
                    }
                    else
                        GetAllData(parseInt($(this).attr('page')), $get('txtbSearchKam').value, CheckSearchBy());
                });

                $("#gridShowAllUserData tr").not($("#gridShowAllUserData tr").eq(0)).click(function () {

                    $("#gridShowAllUserData tr").removeClass('SelectedRowStyle');

                    if ($(this).hasClass('even')) {
                        $(this).removeClass();
                        $(this).addClass('even SelectedRowStyle');
                    }
                    else
                        $(this).addClass('SelectedRowStyle');

                    if ($(this).index() > 0) {
                        Notes = checkstring($(this)[0].cells[9].innerHTML);
                        Type = checkstring($(this)[0].cells[10].innerHTML);

                        document.getElementById('hfIndex').value = $(this).index();

                        document.getElementById('htdAccountValue').value = checkstring($(this)[0].cells[0].innerHTML)

                        document.getElementById('htdCreatedon').value = checkstring($(this)[0].cells[11].innerHTML)

                        $get('ddlNoteType').value = Type.replace("u", "U");

                        if (FIREFOX)
                            $get('txtNoteKam').textContent = Notes;
                        else
                            $get('txtNoteKam').innerText = Notes;

                        var mydiv = $('#pnlPopupNote');

                        mydiv.dialog({ autoOpen: false,
                            title: "Notes",
                            resizable: false,
                            width: 400,
                            modal: false,
                            dialogClass: "FixedPostionTest",
                            closeOnEscape: true,
                            open: function (type, data) {
                                $(this).parent().appendTo("form"); //won't postback unless within the form tag
                            }
                        });

                        mydiv.dialog("open");
                    }

                });

                //Sorting
                $("#gridShowAllUserData th").live("click", function () {
                    var ColHeader = $(this)[0].outerHTML;
                    var CurrentHTML = $(this).html();
                    var Text = $(this).text();
                    var length = Text.length;
                    var sortBy;
                    var Orderby;
                    var Final;
                    var ColHeaderList;

                    if ('<%=SessionFacade.CampaignName%>' == 'DE' ||
                        '<%=SessionFacade.CampaignName%>' == 'FR' ||
                        '<%=SessionFacade.CampaignName%>' == 'UK') {
                        ColHeaderList = [['Account Number', 'SOLD_TO'], ['Account Name',
                        'NAME'], ['Managed Group', 'MG_NAME'], ['Last Ordered Date', 'LPDCUST'],
                        ['12 M Retention %', '(SELECT CASE when sales13to24M=0 then 0 else (sales12M/sales13to24M) end )'], ['Sales 12M', 'sales12M'],
                        ['Sales 13 to 24M', 'sales13to24M'], ['Life Time Sales', 'LTSALES'],
                        ['Last Note Date', 'Date'], ['Last OnHold Order Date', 'c.createdon']];
                    }
                    else {
                        ColHeaderList = [['Account Number', 'SOLD_TO'], ['Account Name',
                        'NAME'], ['Managed Group', 'MG_NAME'], ['Last Ordered Date', 'LPDCUST'],
                        ['12 M Retention %', '(SELECT CASE when sales13to24M=0 then 0 else (sales12M/sales13to24M) end )'], ['Sales 12M', 'sales12M'],
                        ['Sales 13 to 24M', 'sales13to24M'], ['Life Time Sales', 'LTSALES'],
                        ['Last Note Date', 'Date']];
                    }



                    //Remove an image beside the column header
                    var totalCols = $("#gridShowAllUserData th").length;

                    for (var i = 1; i <= totalCols; i++) {
                        var Rows = $('#gridShowAllUserData').find('th:nth-child(' + i + ')'), NewVal;
                        if ((Rows.html().search("asc.gif") > 0) &&
                            $(this).index() != Rows.index()) {
                            NewVal = Rows[0].outerHTML.replace(' <img src="../../App_Themes/Images/asc.gif"> ', "");
                            Rows.replaceWith(NewVal);
                            break;
                        }
                        else if ((Rows.html().search("desc.gif") > 0) &&
                            $(this).index() != Rows.index()) {
                            NewVal = Rows[0].outerHTML.replace(' <img src="../../App_Themes/Images/desc.gif"> ', "");
                            Rows.replaceWith(NewVal);
                            break;
                        }
                    }

                    //Ascending or Descending
                    if ($get('AscDsc').value == "") {
                        $get('AscDsc').value = "rn";
                        sortBy = Asc;
                    }
                    else if ($get('AscDsc').value == "rn") {
                        $get('AscDsc').value = "rn_reversed";
                        sortBy = Desc;
                    }
                    else {
                        $get('AscDsc').value = "rn";
                        sortBy = Asc;
                    }

                    //Image if asc or desc
                    if (ColHeader.search("asc.gif") > 0) {
                        Final = $(this)[0].outerHTML;
                        Final = Final.replace("asc.gif", "desc.gif");
                    }
                    else if (ColHeader.search("desc.gif") > 0) {
                        Final = $(this)[0].outerHTML;
                        Final = Final.replace("desc.gif", "asc.gif");
                    }
                    else {
                        Final = ColHeader.substring(0, 16 + length) + sortBy + ColHeader.substring(16 + length, ColHeader.length);
                    }

                    //Search for which column to be use in sorting.
                    for (var XColHeader = 0; XColHeader < ColHeaderList.length; XColHeader++) {
                        if (ColHeader.search(ColHeaderList[XColHeader][0]) > 0) {
                            Orderby = ColHeaderList[XColHeader][1];
                            break;
                        }
                    }

                    $(this).replaceWith(Final);

                    $get('Sort').value = CurrentHTML;
                    $get('OrderBy').value = Orderby;

                    GetAllDataSort(1, Orderby, $get('AscDsc').value, $get('txtbSearchKam').value, CheckSearchBy());
                });

                //Searching
                $("#txtbSearchKam").keydown(function (e) {
                    var code = CheckBrowser(e); // recommended to use e.which, it's normalized across browsers
                    if (code == 13) {
                        if ($get('Sort').value != "") {
                            if ($get('AscDsc').value == "")
                                GetAllDataSort(1, $get('OrderBy').value, "rn", $get('txtbSearchKam').value, CheckSearchBy());
                            else
                                GetAllDataSort(1, $get('OrderBy').value, $get('AscDsc').value, $get('txtbSearchKam').value, CheckSearchBy());
                        }
                        else
                            GetAllData(1, $get('txtbSearchKam').value, CheckSearchBy());
                    } // missing closing if brace
                    if (code == 220) {
                        e.preventDefault();
                        return false;
                    }
                });

                $("#txtbSearchKam").keypress(function (e) {
                    var code = CheckBrowser(e); // recommended to use e.which, it's normalized across browsers
                    if (code == 13 || code == 220) {
                        e.preventDefault();
                        return false;
                    }
                    else {
                        return true;
                    }
                });

                $("#txtbSearchKam").autocomplete({
                    source: function (request, response) {

                        var Return, search, searchBy;

                        search = $get('txtbSearchKam').value;
                        searchBy = CheckSearchBy();

                        $.ajax({
                            type: "POST",
                            url: "KamWindow1.aspx/GetAutoCompleteResult",
                            data: "{Search: '" + search + "',SearchBy: '" + searchBy + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            success: function OnSuccessAutocomplete(Success) {
                                if (Success.d) {
                                    if (Success.d.length > 0) {
                                        var xmlDoc = $.parseXML(Success.d);
                                        var xml = $(xmlDoc);
                                        var search = xml.find("Table");
                                        var List = [];

                                        if (search.length > 0) {
                                            $.each(search, function () {
                                                List.push($(this).text().toString().trim());
                                            });

                                            $('#trInstruction').css("display", "inline");

                                            response(List);
                                        }
                                        else
                                            response("");
                                    }
                                }
                            },
                            failure: function (response) {
                                alert(response.d);
                            },
                            error: function (response) {
                                alert(response.d);
                            }
                        });
                    },
                    select: function (event, ui) {
                        if ($get('Sort').value != "") {
                            if ($get('AscDsc').value == "")
                                GetAllDataSort(1, $get('OrderBy').value, "rn", ui.item.label, CheckSearchBy());
                            else
                                GetAllDataSort(1, $get('OrderBy').value, $get('AscDsc').value, ui.item.label, CheckSearchBy());
                        }
                        else
                            GetAllData(1, ui.item.label, CheckSearchBy());
                    },
                    close: function (event, ui) {
                        $('#trInstruction').css("display", "none");
                    }
                });

                //Update Note
                $("#btnUpdateNoteKam").click(function () {
                    var Note = checkstring($get('txtNoteKam').value);
                    var Type = checkstring($get('ddlNoteType').value);
                    var Account = checkstring($get('htdAccountValue').value);
                    var Createdon = checkstring($get('htdCreatedon').value);
                    var rowindex = document.getElementById('hfIndex').value;
                    var grd = $get("gridShowAllUserData");

                    grd.rows[rowindex].cells[9].childNodes[0].textContent = Note;
                    grd.rows[rowindex].cells[10].childNodes[0].textContent = Type;

                    PageMethods.UpdateNote(Note, Type, Createdon, Account, onSuccessUpdateNoteMessage);
                });

                var mydivJump = $('#pnlPopupKam');

                mydivJump.dialog({ autoOpen: true,
                    title: "Table Options",
                    resizable: false,
                    width: 1030,
                    height: 130,
                    draggable: false,
                    closeOnEscape: false,
                    position: ["center", "top"],
                    open: function (type, data) {
                        $(".ui-dialog-titlebar-close").hide();
                        $(this).parent().appendTo("form"); //won't postback unless within the form tag
                    },
                    modal: false,
                    beforeclose: function () {
                        return false;
                    }
                });

                //                $('#pnlPopupKam').parent().css({ position: "fixed" }).end().dialog('open');
            });

        }

        function clearTable() {
            var x = 0;
            while (x < $('#header-fixed').find('th').length) {
                $('#header-fixed').find('th').remove(x);
                x = x + 1;
            }
        }

    </script>
    <style type="text/css">
        .Pager span
        {
            text-align: center;
            color: #FFFFFF;
            display: inline-block;
            width: 20px;
            background-color: #263441;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #DBEAFF;
        }
        .Pager a
        {
            text-align: center;
            display: inline-block;
            width: 20px;
            background-color: #EBECEE;
            border: 1px solid #B5BAC0;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
        }
        .Rows
        {
            background-color: #e5e5e5;
        }
        .RowsWhite
        {
            background-color: White;
        }
        .modal
        {
            display: none;
            position: fixed;
            z-index: 1000;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            background: rgba( 255, 255, 255, .8 ) url('../../App_Themes/Images/New%20Design/ajax-loader.gif') 50% 50% no-repeat;
        }
        /* When the body has the loading class, we turn
   the scrollbar off with overflow:hidden */
        body.loading
        {
            overflow: hidden;
        }
        
        /* Anytime the body has the loading class, our
   modal element will be visible */
        body.loading .modal
        {
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scManager" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div style="color: Red">
        <asp:Literal ID="litMessage" runat="server" Visible="false"> Please check the KAM ID OR Contact HelpDesk</asp:Literal></div>
    <div id="containerKAM">
        <asp:Panel ID="pnlShowKamWindow" runat="server" Visible="false">
            <asp:Panel ID="pnlPopupKam3" runat="server" Style="display: none;" ClientIDMode="Static">
            </asp:Panel>
            <div style="position: fixed; height: 50px; width: 100%; background-color: Red;">
                <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%">
                    <tr>
                        <td class="style2">
                            <asp:UpdatePanel ID="KamGridUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                <a style="font-family: Arial; font-size: 12px; color: Blue;" href="#" onclick="Assign(); OrderHistoryClick(); window.opener.location.href='../OrderHistory/OrderHistory.aspx';">
                                                    Show Order History</a> &nbsp;
                                            </td>
                                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                <a style="font-family: Arial; font-size: 12px; color: Blue;" href="#" onclick="Assign(); productClick(); window.opener.location.href='../ProductSummary/ProductSummary.aspx';">
                                                    Show Product Summary</a> &nbsp;
                                            </td>
                                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                <a style="font-family: Arial; font-size: 12px; color: Blue;" href="#" onclick="Assign();  CustomerClick(); window.opener.location.href='../SiteAndContactInfo/SiteAndContactInfo.aspx';">
                                                    Show Customer Info</a> &nbsp;
                                            </td>
                                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                <a style="font-family: Arial; font-size: 12px; color: Blue;" href="#" onclick="Assign(); QuoteClick();  window.opener.location.href='../Quotes/Quotes.aspx';">
                                                    Show Quotes</a> &nbsp;
                                            </td>
                                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                <a style="font-family: Arial; font-size: 12px; color: Blue;" href="#" onclick="Assign(); NotesClick(); window.opener.location.href='../NotesCommHistory/NotesCommHistory.aspx';">
                                                    Show Notes</a> &nbsp;
                                            </td>
                                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                <a id="lnkOnHold" runat="server" style="font-family: Arial; font-size: 12px; color: Blue;"
                                                    href="#" onclick="Assign(); OnHoldOrderClick(); window.opener.location.href='../OnHoldOrder/OnHoldOrder.aspx';">
                                                    Show On Hold Order</a> &nbsp;
                                            </td>
                                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                <a style="font-family: Arial; font-size: 12px; color: Blue;" href="#" onclick="Assign(); productClick(); productCaution();">
                                                    Show Product Summary (Territory)</a> &nbsp;
                                            </td>
                                            <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                                <a style="font-family: Arial; font-size: 12px; color: Blue;" href="#" onclick="Assign(); NotesClick(); notesTCaution();">
                                                    Show Notes (Territory)</a> &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" defaultbutton="btnSearch" class="style2" valign="middle">
                            <table>
                                <tr id="trInstruction">
                                    <td>
                                        <div style="padding-left: 5px; padding-top: 5px">
                                            <asp:Label ID="lblInstructionSearchKam" Text="Press enter to search." ClientIDMode="Static"
                                                runat="server" CssClass="LabelFont" Font-Bold="true" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                            <div id="searchboxdiv" style="padding-left: 5px; padding-top: 5px">
                                                <div id="label2">
                                                    Value</div>
                                                <asp:TextBox ID="txtbSearchKam" runat="server" ClientIDMode="Static" CssClass="txtSearch">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </asp:TextBox>
                                                <asp:ImageButton ID="btnSearch" ClientIDMode="Static" CssClass="btnSearch" ImageUrl="~/App_Themes/Images/New Design/magnifying-glass-search-icon.jpg"
                                                    runat="server" ToolTip="Search Account" />
                                                &nbsp;<div class="clear">
                                                    <br />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <a id="RefreshKam" style="font-family: Arial; font-size: 12px; color: Blue; height: 25px;"
                                            href="#">Refresh</a> &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                        <img src="../../App_Themes/Images/New Design/divider-2.png" width="1" height="25"
                                            border="0" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rdoAccountName" runat="server" Text=" Account Name" GroupName="RdoFilterKam"
                                            Checked="true" />
                                    </td>
                                    <td>
                                        &nbsp;
                                        <img src="../../App_Themes/Images/New Design/divider-2.png" width="1" height="25"
                                            border="0" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rdoManagedGroup" runat="server" Text=" Managed Group" GroupName="RdoFilterKam" />
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
                </table>
            </div>
            <div id="KAMtable" runat="server" clientidmode="Static" class="table-wrapper2 page2">
                <a name="kamtable"></a>
                <table border="0" cellpadding="10" cellspacing="0" width="100%" style="height: 100%;
                    padding-top: 90px;">
                    <tr>
                        <td>
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gridShowAllUserData" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" ForeColor="Black" BorderColor="Black" Font-Size="12px" CellPadding="4"
                                                CellSpacing="2" GridLines="None">
                                                <EditRowStyle CssClass="EditRowStyle" />
                                                <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                <RowStyle CssClass="RowStyle" Wrap="false" />
                                                <Columns>
                                                    <asp:BoundField DataField="SOLD_TO" HeaderText="Account Number" SortExpression="SOLD_TO" />
                                                    <asp:BoundField DataField="NAME" HeaderText="Account Name" SortExpression="NAME" />
                                                    <asp:BoundField DataField="MG_NAME" HeaderText="Managed Group" SortExpression="MG_NAME" />
                                                    <asp:BoundField DataField="LPDCUST" HeaderText="Last Ordered Date" DataFormatString="{0:MMM dd, yyyy}"
                                                        SortExpression="LPDCUST" />
                                                    <asp:BoundField DataField="Retention" HeaderText="12 M Retention %" SortExpression="Retention"
                                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:P}" />
                                                    <asp:BoundField DataField="sales12M" HeaderText="Sales 12M" SortExpression="sales12M"
                                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,####}" />
                                                    <asp:BoundField DataField="sales13to24M" HeaderText="Sales 13 to 24M" SortExpression="sales13to24M"
                                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,####}" />
                                                    <asp:BoundField DataField="LTSALES" HeaderText="Life Time Sales" ItemStyle-HorizontalAlign="Right"
                                                        SortExpression="LTSALES" DataFormatString="{0:##,####}" />
                                                    <asp:BoundField DataField="Date" HeaderText="Last Note Date" SortExpression="Date"
                                                        DataFormatString="{0:MMM dd, yyyy}" />
                                                    <asp:BoundField DataField="NOTES" HeaderText="Notes" SortExpression="NOTES" />
                                                    <asp:BoundField DataField="TYPE" HeaderText="TYPE" SortExpression="TYPE" />
                                                    <asp:BoundField DataField="CREATEDON" HeaderText="CREATEDON" SortExpression="CREATEDON" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hfIndex" runat="server" />
                                            <asp:HiddenField ID="Sort" runat="server" ClientIDMode="Static" />
                                            <asp:HiddenField ID="AscDsc" runat="server" ClientIDMode="Static" Value="rn" />
                                            <asp:HiddenField ID="OrderBy" runat="server" ClientIDMode="Static" />
                                            <asp:HiddenField ID="SearchVal" runat="server" ClientIDMode="Static" />
                                            <div class="modal">
                                            </div>
                                            <div class="Pager">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    <asp:Panel ID="pnlPopupNote" runat="server" Style="display: none;" ClientIDMode="Static">
        <table cellspacing="10px">
            <tr>
                <td>
                    <asp:Label ID="Label1" CssClass="LabelFont" runat="server" Text="Note Type:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNoteType" runat="server" AutoPostBack="false" ClientIDMode="Static"
                        CssClass="textbox curved" Style="font-size: 12px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNoteDateKam" CssClass="LabelFont" runat="server" Text="Note:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNoteKam" CssClass="textbox" Style="font-size: 12px; width: 220px;"
                        Wrap="true" TextMode="MultiLine" Width="100%" Height="100px" ClientIDMode="Static"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnUpdateNoteKam" runat="server" Text="Update" CssClass="button"
            Style="font-size: 12px; width: 60px; float: right" ClientIDMode="Static" OnClientClick="return false;" />
    </asp:Panel>
    <asp:HiddenField ID="htdAccountValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="LoadAlready" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="htdCreatedon" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="htdSortBy" runat="server" ClientIDMode="Static" Value="0" />
    <input type="hidden" id="div_position" name="div_position" />
    </form>
</body>
</html>
