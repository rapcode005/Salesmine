<%@ Page EnableEventValidation="false" Language="C#" 
MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="SiteAndContactInfo.aspx.cs" 
    Inherits="WebSalesMine.WebPages.Site_ContactInfo.SiteContactInfo" %>

<%@ MasterType VirtualPath="~/WebPages/UserControl/NewMasterPage.Master" %>
<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<%@ Import Namespace="AppLogic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
    <input type="hidden" id="hiddenColor" />
    <script type='text/javascript' src="../../App_Themes/JS/accounting.js"></script>
    <style type="text/css">
        .FixedPostion
        {
            position: fixed;
        }
    </style>
    <script type="text/javascript">
        var FIREFOX = /Firefox/i.test(navigator.userAgent);
        function CalendarShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }

        $(document).ready(function () {

            $('#lnkMasterDataChange').click(function () {
                $addHandler(document, 'keydown', onKeypress);
                var mydiv = $('#Panel4');
                mydiv.dialog({ autoOpen: false,
                    title: "Master Data Change",
                    resizable: false,
                    width: "auto",
                    open: function (type, data) {
                        $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        if (FIREFOX)
                            $get('txtAccountNameChanges').value = $get('txtSiteName').textContent;
                        else
                            $get('txtAccountNameChanges').value = $get('txtSiteName').innerText;
                    }
                });

                mydiv.dialog('open');

                //                mydiv.parent().appendTo($("form:first"));

                return false;
            });
        });

        $(document).ready(function () {
            $('#lnkAddNewContact').click(function () {
                var SiteName;

                if (FIREFOX)
                    SiteName = $get('txtSiteName').textContent;
                else
                    SiteName = $get('txtSiteName').innerText;

                if (SiteName != '') {
                    var mydiv = $('#pnlNewContact');
                    mydiv.dialog({ autoOpen: false,
                        title: "Add New Contact",
                        resizable: false,
                        width: "auto",
                        open: function (type, data) {
                            $get('txtEmailNewContact').value = "";
                            $get('txtFirstnameNewContact').value = "";
                            $get('txtOtherNewContact').value = "";
                            $get('txtPhoneNewContact').value = "";
                            $get('txtLastanameNewContact').value = "";
                            $get('ddlFunctionNewContact').value = "";
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        },
                        modal: true
                    });

                    mydiv.dialog('open');
                }
                else {
                    alert('There is no account under this unit.');
                }

                return false;
            });


            $('#btnCancelNew').click(function () {
                $get('txtEmailNewContact').value = "";
                $get('txtFirstnameNewContact').value = "";
                $get('txtOtherNewContact').value = "";
                $get('txtPhoneNewContact').value = "";
                $get('txtLastanameNewContact').value = "";
                $get('ddlFunctionNewContact').value = "";

                return false;
            });


        });



        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {

                var mydiv = $('#pnlPopupCustInfo');
                mydiv.dialog('close');
                var mydivmasterdata = $('#Panel4');
                mydivmasterdata.dialog('close');
            }


        }

        function SaveSuccess() {
            alert("Successsfully Saved");

            if (document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value == "CL") {
                Title = "Contact Details";
                document.getElementById('CustInfo').className = "";
                width = "auto";
            }
            else {
                document.getElementById('CustInfo').className = "BorderStyle";
                document.getElementById('QQInfo').className = "BorderStyle";
                width = 700;
                if (document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value == "PC") {
                    Title = "Contact Details &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Qualifying Questions";
                }
                else {
                    Title = "Contact Details &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Qualifying Questions";
                }
            }

            var mydiv = $('#pnlPopupCustInfo');
            mydiv.dialog({ autoOpen: false,
                title: Title,
                resizable: false,
                dialogClass: "FixedPostion",
                closeOnEscape: true,
                width: width,
                open: function (type, data) {
                    $(this).parent().appendTo("form"); //won't postback unless within the form tag
                }
            });

            mydiv.dialog('open');

            return false;

        }


        function Information() {
            alert("Firstname and Lastname already exist.");
        }

        function InformationMessage(value) {
            alert(value);
        }

        function pageLoad() {
            // call the savePanelPosition when the panel is moved
            $addHandler(document, 'keydown', onKeypress);
            var index;
            $(document).ready(function () {

                $("#gridSiteContactInfo  tr:has(td)").hover(function () {
                    $(this).css("cursor", "pointer");
                });

                $('#txtSearchContact').keyup(function (e) {

                    $("#gridSiteContactInfo tr:has(td)").hide(); // Hide all the rows.
                    index = 1;
                    var sSearchTerm = $('#txtSearchContact').val(); //Get the search box value

                    if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                    {
                        $("#gridSiteContactInfo tr:has(td)").show();

                        $("#gridSiteContactInfo tr").children("td:nth-child(2)").each(function () {

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



                        });

                        return false;
                    }

                    if ($get('rdoContactNumber').checked == true) {
                        var ownerIndex = $("#gridSiteContactInfo th:contains('CONTACT NUMBER')").index() + 1;
                        //Iterate through all the td.
                        $("#gridSiteContactInfo tr").children("td:nth-child(" + ownerIndex + ")").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
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

                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    else if ($get('rdoContactFName').checked == true) {
                        var ownerIndex = $("#gridSiteContactInfo th:contains('FIRST NAME')").index() + 1;
                        $("#gridSiteContactInfo tr").children("td:nth-child(" + ownerIndex + ")").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
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

                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    else if ($get('rdoContactLName').checked == true) {
                        var ownerIndex = $("#gridSiteContactInfo th:contains('LAST NAME')").index() + 1;
                        $("#gridSiteContactInfo tr").children("td:nth-child(" + ownerIndex + ")").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
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

                                $(this).parent().show();
                                return true;
                            }

                        });
                    }

                    e.preventDefault();
                });


                $('#txtSearchContact').click(function (e) {

                    $("#gridSiteContactInfo tr:has(td)").hide(); // Hide all the rows.
                    index = 1;
                    var sSearchTerm = $('#txtSearchContact').val(); //Get the search box value

                    if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                    {
                        $("#gridSiteContactInfo tr:has(td)").show();


                        $("#gridSiteContactInfo tr").children("td:nth-child(2)").each(function () {

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

                        });
                        return false;
                    }

                    if ($get('rdoContactNumber').checked == true) {
                        //Iterate through all the td.
                        $("#gridSiteContactInfo tr").children("td:nth-child(2)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {

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

                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    else if ($get('rdoContactFName').checked == true) {
                        $("#gridSiteContactInfo tr").children("td:nth-child(3)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
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

                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    else if ($get('rdoContactLName').checked == true) {
                        $("#gridSiteContactInfo tr").children("td:nth-child(4)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {

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

                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    e.preventDefault();



                });


                var FIREFOX = /Firefox/i.test(navigator.userAgent);
                $('#rdoContactNumber').click(function (e) {

                    if ($(this)[0].checked) {
                        if (FIREFOX) {
                            $get('lblSearchBy').textContent = "Contact # ";
                        }
                        else
                            $get('lblSearchBy').innerText = "Contact # ";
                    }

                    return true;
                    e.preventDefault();
                })

                $('#rdoContactFName').click(function (e) {

                    if ($(this)[0].checked) {
                        if (FIREFOX) {
                            $get('lblSearchBy').textContent = "First Name ";
                        }
                        else
                            $get('lblSearchBy').innerText = "First Name ";
                    }

                    return true;
                    e.preventDefault();
                })

                $('#rdoContactLName').click(function (e) {

                    if ($(this)[0].checked) {
                        if (FIREFOX) {
                            $get('lblSearchBy').textContent = "Last Name ";
                        }
                        else
                            $get('lblSearchBy').innerText = "Last Name ";
                    }

                    return true;
                    e.preventDefault();
                })


                var Row;
//                var RowID = document.getElementById('hdnEmailID').value;
//                //After Postback
//                //Check Selected Row
//                if (RowID != "0") {

//                    $("#gridSiteContactInfo tr")[RowID].className = 'SelectedRowStyle';
//                    $("#gridSiteContactInfo tr").find("INPUT[type='radio']")[RowID - 1].checked = true;

//                    var totalCols = $("#gridSiteContactInfo").find('tr')[0].cells.length;

//                    for (var i = 0; i < totalCols + 1; i++) {
//                        var ColName = $('#gridSiteContactInfo  tr').find('th:nth-child(' + i + ')').text();
//                        if (ColName == 'ROW' || ColName == 'Row') {
//                            Row = $(this).find("td:eq(" + (i - 1) + ")").html();
//                        }
//                    }

//                    PageMethods.GetData(Row, OnSuccess);
//                }

                //Check Last Search
                $("#gridSiteContactInfo tr:has(td)").hide();
                var stSearch = $('#txtSearchContact').val();
                index = 1;
                if (stSearch.length == 0) {
                    $("#gridSiteContactInfo tr:has(td)").show();

                    $("#gridSiteContactInfo tr").children("td:nth-child(2)").each(function () {

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

                    });
                }
                else {
                    if ($get('rdoContactNumber').checked == true) {
                        //Iterate through all the td.
                        $("#gridSiteContactInfo tr").children("td:nth-child(2)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(stSearch.toLowerCase()) >= 0) //Check if data matches
                            {
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
                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    else if ($get('rdoContactFName').checked == true) {
                        $("#gridSiteContactInfo tr").children("td:nth-child(3)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(stSearch.toLowerCase()) >= 0) //Check if data matches
                            {
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
                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                    else if ($get('rdoContactLName').checked == true) {
                        $("#gridSiteContactInfo tr").children("td:nth-child(4)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(stSearch.toLowerCase()) >= 0) //Check if data matches
                            {
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
                                $(this).parent().show();
                                return true;
                            }

                        });
                    }
                }

                $("#gridSiteContactInfo tr").click(function () {
                    $("#gridSiteContactInfo").find("INPUT[type='radio']").attr("checked", false);

                    if ($(this).closest("tr")[0].rowIndex == 0) {
                        
                    }
                    else {
                        var Row;
                        var Title;
                        var width;

                        radio = $(this).find("INPUT[type='radio']");
                        var list = radio.closest('table').find("INPUT[type='radio']").not(radio);
                        list.closest('TR').removeClass('SelectedRowStyle');

//                        if (RowID != "0")
//                            $("#gridSiteContactInfo tr")[RowID].className = 'RowStyle';

                        radio.attr("checked", true);
                        if (radio.closest('TR').hasClass('even')) {
                            radio.closest('TR').removeClass();
                            radio.closest('TR').addClass('even SelectedRowStyle');
                        }
                        else
                            radio.closest('TR').addClass('SelectedRowStyle');

                       document.getElementById('hdnEmailID').value = $(this).index();

                        var totalCols = $("#gridSiteContactInfo").find('tr')[0].cells.length;

                        for (var i = 0; i < totalCols + 1; i++) {
                            var ColName = $('#gridSiteContactInfo  tr').find('th:nth-child(' + i + ')').text();
                            if (ColName == 'ROW' || ColName == 'Row') {
                                Row = $(this).find("td:eq(" + (i - 1) + ")").html();
                            }
                        }

                        PageMethods.GetData(Row, OnSuccess);

                        if (document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value == "CL") {
                            Title = "Contact Details";
                            document.getElementById('CustInfo').className = "";
                            width = "auto";
                        }
                        else {
                            document.getElementById('CustInfo').className = "BorderStyle";
                            document.getElementById('QQInfo').className = "BorderStyle";
                            width = 700;
                            if (document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value == "PC") {
                                Title = "Contact Details &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Qualifying Questions";
                            }
                            else {
                                Title = "Contact Details &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Qualifying Questions";
                            }
                        }

                        var mydiv = $('#pnlPopupCustInfo');
                        mydiv.dialog({ autoOpen: false,
                            title: Title,
                            resizable: false,
                            dialogClass: "FixedPostion",
                            closeOnEscape: true,
                            width: width,
                            open: function (type, data) {
                                $(this).parent().appendTo("form"); //won't postback unless within the form tag
                            }
                        });

                        mydiv.dialog('open');
                        return false;
                    }
                    
                });
            });
        }

        //Format Currency
        function FormatCurrency(val, curcode) {
            var MoneyValue;
            if (curcode == "GBP") {
                MoneyValue = accounting.formatMoney(val, "£", 2);
            }
            else if (curcode == "EUR") {
                MoneyValue = accounting.formatMoney(val, { symbol: " €", format: "%v %s", thousand: ".", decimal: ",", precision: 2 });
            }
            else if (curcode == "CHF") {
                MoneyValue = accounting.formatMoney(val, { symbol: "fr.", format: "%s %v", thousand: "'", decimal: ".", precision: 2 });
            }
            else {
                MoneyValue = accounting.formatMoney(val, "$", 2);
            }
            return MoneyValue;
        }


        function OnSuccess(Data) {
            var myDate;
            var FIREFOX = /Firefox/i.test(navigator.userAgent);

            if (FIREFOX) {
                $get('txtPopLastName').textContent = CheckString(Data.tables[0].rows[0]['Last Name']);
                $get('txtPopFirstName').textContent = CheckString(Data.tables[0].rows[0]['First Name']);
                $get('txtPopContactType').textContent = CheckString(Data.tables[0].rows[0]['Contact Type']);
                $get('txtPopSAPStat').textContent = CheckString(Data.tables[0].rows[0]['Contact Status']);
                $get('txtPopRecency').textContent = CheckString(Data.tables[0].rows[0]['Recency']);
                $get('txtPopDepartment').textContent = CheckString(Data.tables[0].rows[0]['Department']);
                $get('txtPopFunction').textContent = CheckString(Data.tables[0].rows[0]['Job Function']);
                $get('txtPopTitle').textContent = CheckString(Data.tables[0].rows[0]['Title']);
                $get('txtPopDirectPhone').textContent = CheckString(Data.tables[0].rows[0]['Direct Phone']);
                $get('txtPopSitePhone').textContent = CheckString(Data.tables[0].rows[0]['Site Phone']);
                $get('txtPopEmail').textContent = CheckString(Data.tables[0].rows[0]['Email Address']);
                $get('txtPopDoNotMail').textContent = CheckString(Data.tables[0].rows[0]['Do Not Mail']);
                $get('txtPopDoNotFax').textContent = CheckString(Data.tables[0].rows[0]['Do Not Fax']);
                $get('txtPopDoNotEmail').textContent = CheckString(Data.tables[0].rows[0]['Do Not Email']);
                $get('txtPopDoNotCall').textContent = CheckString(Data.tables[0].rows[0]['Do Not Call']);
                $get('txtPopLifeTimeSales').textContent = FormatCurrency(CheckString(Data.tables[0].rows[0]['Lifetime Sales']),$get('CurrencyCode').value);
                $get('txtPopLast12MSales').textContent = FormatCurrency(CheckString(Data.tables[0].rows[0]['Last 12M Sales']), $get('CurrencyCode').value);
                $get('txtPopPhoneExt').textContent = CheckString(Data.tables[0].rows[0]['Phone Extension']);
            }
            else {
                $get('txtPopLastName').innerText = CheckString(Data.tables[0].rows[0]['Last Name']);
                $get('txtPopFirstName').innerText = CheckString(Data.tables[0].rows[0]['First Name']);
                $get('txtPopContactType').innerText = CheckString(Data.tables[0].rows[0]['Contact Type']);
                $get('txtPopSAPStat').innerText = CheckString(Data.tables[0].rows[0]['Contact Status']);
                $get('txtPopRecency').innerText = CheckString(Data.tables[0].rows[0]['Recency']);
                $get('txtPopDepartment').innerText = CheckString(Data.tables[0].rows[0]['Department']);
                $get('txtPopFunction').innerText = CheckString(Data.tables[0].rows[0]['Job Function']);
                $get('txtPopTitle').innerText = CheckString(Data.tables[0].rows[0]['Title']);
                $get('txtPopDirectPhone').innerText = CheckString(Data.tables[0].rows[0]['Direct Phone']);
                $get('txtPopSitePhone').innerText = CheckString(Data.tables[0].rows[0]['Site Phone']);
                $get('txtPopEmail').innerText = CheckString(Data.tables[0].rows[0]['Email Address']);
                $get('txtPopDoNotMail').innerText = CheckString(Data.tables[0].rows[0]['Do Not Mail']);
                $get('txtPopDoNotFax').innerText = CheckString(Data.tables[0].rows[0]['Do Not Fax']);
                $get('txtPopDoNotEmail').innerText = CheckString(Data.tables[0].rows[0]['Do Not Email']);
                $get('txtPopDoNotCall').innerText = CheckString(Data.tables[0].rows[0]['Do Not Call']);
                $get('txtPopLifeTimeSales').innerText = FormatCurrency(CheckString(Data.tables[0].rows[0]['Lifetime Sales']), $get('CurrencyCode').value);
                $get('txtPopLast12MSales').innerText = FormatCurrency(CheckString(Data.tables[0].rows[0]['Last 12M Sales']), $get('CurrencyCode').value);
                $get('txtPopPhoneExt').innerText = CheckString(Data.tables[0].rows[0]['Phone Extension']);
            }

            $get('txtLastnameContactChanges').value = CheckString(Data.tables[0].rows[0]['Last Name']);
            $get('txtFirstnameContactChanges').value = CheckString(Data.tables[0].rows[0]['First Name']);

            if (document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value != "PC" &&
             document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value.trim() != "CL") {

                if (CheckString(Data.tables[0].rows[0]['Repdata Contact Status']).trim() == "No Longer there / Deceased")
                    $get('ddlContactStatus').value = "No Longer there";
                else
                    $get('ddlContactStatus').value = CheckString(Data.tables[0].rows[0]['Repdata Contact Status']).trim();

                //                $get('ddlContBudget').value = CheckString(Data.tables[0].rows[0]['Repdata Budget']).trim();
                $get('ddlContFunc').value = CheckString(Data.tables[0].rows[0]['Repdata Function']).trim();

                if (FIREFOX)
                    $get('lblUpdatedByWho').textContent = CheckString(Data.tables[0].rows[0]['UPDATEDBY']).trim();
                else
                    $get('lblUpdatedByWho').innerText = CheckString(Data.tables[0].rows[0]['UPDATEDBY']).trim();


                myDate = new Date(CheckString(Data.tables[0].rows[0]['VALID_FROM']));

                if (FIREFOX) {

                    if (CheckString(Data.tables[0].rows[0]['VALID_FROM']) != "") {
                        $get('lblUpdatedDateWhen').textContent = (myDate.getMonth() + 1) + "/" +
                                                                    myDate.getDate() + "/" +
                                                                    myDate.getFullYear();
                    }
                    else
                        $get('lblUpdatedDateWhen').textContent = "";

                }
                else {
                    if (CheckString(Data.tables[0].rows[0]['VALID_FROM']) != "") {
                        $get('lblUpdatedDateWhen').innerText = (myDate.getMonth() + 1) + "/" +
                                                                    myDate.getDate() + "/" +
                                                                    myDate.getFullYear();
                    }
                    else
                        $get('lblUpdatedDateWhen').innerText = "";

                }


            }


            else if (document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value == "PC") {

                //For PC

                $get('ddlNewContactStatus').value = $.trim(CheckString(Data.tables[0].rows[0]['Repdata Contact Status']));
                $get('ddlJobArea').value = $.trim(CheckString(Data.tables[0].rows[0]['Repdata Function']));
                $get('ddlHealth').value = $.trim(CheckString(Data.tables[0].rows[0]['OHIE']));
                $get('ddlSpanish').value = $.trim(CheckString(Data.tables[0].rows[0]['HSE']));
                //$get('ddlEmployeeSize').value = $.trim(CheckString(Data.tables[0].rows[0]['ES']));
                $get('txtEmployeeSizeValue').value = $.trim(CheckString(Data.tables[0].rows[0]['ES']));
                if (FIREFOX) {
                    $get('lblUpdatedbyPCWho').textContent = $.trim(CheckString(Data.tables[0].rows[0]['PC_update_by']));
                }
                else
                    $get('lblUpdatedbyPCWho').innerText = $.trim(CheckString(Data.tables[0].rows[0]['PC_update_by']));

                myDate = new Date(CheckString(Data.tables[0].rows[0]['PC_update_on']));

                if (FIREFOX) {
                    if (CheckString(Data.tables[0].rows[0]['PC_update_on']) != "") {
                        $get('lblDateWho').textContent = (myDate.getMonth() + 1) + "/" +
                                                        myDate.getDate() + "/" +
                                                        myDate.getFullYear();
                    }
                    else
                        $get('lblDateWho').textContent = "";
                }
                else {
                    if (CheckString(Data.tables[0].rows[0]['PC_update_on']) != "") {
                        $get('lblDateWho').innerText = (myDate.getMonth() + 1) + "/" +
                                                        myDate.getDate() + "/" +
                                                        myDate.getFullYear();
                    }
                    else
                        $get('lblDateWho').innerText = "";
                }
            }

            if (CheckString(Data.tables[0].rows[0]['Last Purchased Date'])) {
                myDate = new Date(CheckString(Data.tables[0].rows[0]['Last Purchased Date']));

                if (FIREFOX) {
                    $get('txtPopLastOrderDate').textContent = "";
                    $get('txtPopLastOrderDate').textContent = (myDate.getMonth() + 1) + "/" +
                                                         myDate.getDate() + "/" +
                                                         myDate.getFullYear();
                }
                else {
                    $get('txtPopLastOrderDate').innerText = "";
                    $get('txtPopLastOrderDate').innerText = (myDate.getMonth() + 1) + "/" +
                                                            myDate.getDate() + "/" +
                                                            myDate.getFullYear();
                }
            }
            else {
                if (FIREFOX)
                    $get('txtPopLastOrderDate').textContent = "";
                else
                    $get('txtPopLastOrderDate').innerText = "";
            }

            if (FIREFOX)
                $get('txtContactNameMasterChange').value = $get('txtPopLastName').textContent + ", " + $get('txtPopFirstName').textContent;
            else
                $get('txtContactNameMasterChange').value = $get('txtPopLastName').innerText + ", " + $get('txtPopFirstName').innerText;

            if (Data.tables[0].rows[0]['Contact Number'] == "0") {
                $get('txtContactNumberMasterChange').value = Data.tables[0].rows[0]['new_contact_key'];
                document.getElementById('hdnContactNumber').value = Data.tables[0].rows[0]['new_contact_key'];
            }
            else {
                $get('txtContactNumberMasterChange').value = Data.tables[0].rows[0]['Contact Number'];
                document.getElementById('hdnContactNumber').value = Data.tables[0].rows[0]['Contact Number'];
            }
        }


        function SelectSingleRadiobutton(rdBtnID) {
            var rduser = $(document.getElementById(rdBtnID));
            rduser.closest('TR').addClass('SelectedRowStyle');
            rduser.checked = true;
            var list = rduser.closest('table').find("INPUT[type='radio']").not(rduser);
            list.attr('checked', false);
            list.closest('TR').removeClass('SelectedRowStyle');
        }

        function CheckString(val) {
            if (val)
                return val;
            else
                return "";
        }

        function CheckNumber(val) {
            if (val)
                return val;
            else
                return 0;
        }

        function HideColumn() {
            //var $tbl = $("#gridOrderHistory");
            var thColumn;
            $(function () {
                //Hide Column
                thColumn = $("#gridSiteContactInfo th:contains('ContactNum')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('FirstName')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('LastName')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('ContactType')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('RepContactStatus')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('RepFunction')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('Recen')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('Dep')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('JobFunction')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('Tit')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('DirectPhone')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('SitePhone')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('EmailAddress')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('DoNotMail')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('DoNotFax')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('DoNotEmail')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('DoNotCall')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('LifetimeSales')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('Last12MSales')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('LifetimeOrders')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('Last12MOrders')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('LastPurchasedDate')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('PhoneExtension')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });

                thColumn = $("#gridSiteContactInfo th:contains('q4')");
                thColumn.css("display", "none");
                $("#gridSiteContactInfo tr").each(function () {
                    $(this).find("td").eq(thOrderType.index()).css("display", "none");
                });
            });
        }


        function BeginRequestHandler(sender, args) {
            if ($get('DataDiv') != null) {
                xPos = $get('DataDiv').scrollLeft;
                yPos = $get('DataDiv').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('DataDiv') != null) {
                $get('DataDiv').scrollLeft = xPos;
                $get('DataDiv').scrollTop = yPos;
            }
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    </script>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <div id="container">
        <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%"
            height="100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="object-wrapper" style="height: 20px">
                                <asp:LinkButton ID="lnkAddNewContact" runat="server" Text="Add New Contact" ClientIDMode="Static"
                                    CssClass="LabelFont"></asp:LinkButton>
                            </td>
                            <td class="object-wrapper" style="height: 20px">
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkMasterDataChange" ClientIDMode="Static" runat="server" CssClass="LabelFont">Master Data Change</asp:LinkButton>
                            </td>
                            <td class="object-wrapper" style="height: 20px">
                                &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkArrange" Text="Arrange Column" OnClick="ArrangeColumn_Click"
                                    CssClass="LabelFont" runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="font-size: 11px; width: 200px;">
            <asp:LinkButton ID="LinkButton1" OnClick="btnShowSiteContactInfo_Click" Style="text-decoration: none;"
                Font-Size="Medium" runat="server">
                <asp:Label ID="lblTitleSiteContactInfo" Font-Size="X-Large" runat="server"></asp:Label></asp:LinkButton>
        </div>
        <table style="display: none">
            <tr>
                <td>
                    <asp:UpdatePanel ID="NewContactUpdate" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlNewContact" ClientIDMode="Static" Style="display: none" runat="server"
                                CssClass="modalPopupMaster">
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFirstnameNewContact" CssClass="LabelFont" runat="server" Text="First Name"
                                                            AssociatedControlID="txtFirstnameNewContact"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblRequiredFirstname" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                        Text="*"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFirstnameNewContact" ClientIDMode="Static" CssClass="textbox curved"
                                                                        runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ValidationGroup="ResourceGroup" ID="reqFirstname" ControlToValidate="txtFirstnameNewContact"
                                                                        runat="server" CssClass="LabelFont" ForeColor="Red"><src="../../App_Themes/Images/New Design/animated-gifs-exclamation-points-006.gif" />
                                                                    </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblLastanameNewContact" CssClass="LabelFont" runat="server" Text="Last Name"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblRequiredLastname" runat="server" CssClass="LabelFont" ForeColor="Red"
                                                                        Text="*"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLastanameNewContact" ClientIDMode="Static" CssClass="textbox curved"
                                                                        runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ValidationGroup="ResourceGroup" ID="reqLastname" ControlToValidate="txtLastanameNewContact"
                                                                        runat="server" CssClass="LabelFont" ForeColor="Red">
                                                                <img src="../../App_Themes/Images/New Design/animated-gifs-exclamation-points-006.gif" />
                                                                    </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEmailNewContact" CssClass="LabelFont" runat="server" Text="Email Address"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEmailNewContact" ClientIDMode="Static" CssClass="textbox curved"
                                                                        runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red"
                                                                        ValidationExpression="^((?:(?:(?:\w[\.\-\+]?)*)\w)+)\@((?:(?:(?:\w[\.\-\+]?){0,62})\w)+)\.(\w{2,6})$"
                                                                        ErrorMessage="Invalid Email Format." ControlToValidate="txtEmailNewContact">
                                                                <img src="../../App_Themes/Images/New Design/animated-gifs-exclamation-points-006.gif" />
                                                                    </asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label36" CssClass="LabelFont" runat="server" Text="Phone"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPhoneNewContact" ClientIDMode="Static" CssClass="textbox curved"
                                                                        runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFunctionNewContact" CssClass="LabelFont" runat="server" Text="Function"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" CssClass="LabelFont" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlFunctionNewContact" ClientIDMode="Static" CssClass="textbox"
                                                                        runat="server" Style="font-size: 12px">
                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Facilities"></asp:ListItem>
                                                                        <asp:ListItem Text="Maintenance"></asp:ListItem>
                                                                        <asp:ListItem Text="Office/Admin"></asp:ListItem>
                                                                        <asp:ListItem Text="Owner/Pres/Senior Mgmt"></asp:ListItem>
                                                                        <asp:ListItem Text="Plant/Prod/Ops"></asp:ListItem>
                                                                        <asp:ListItem Text="Project Manager"></asp:ListItem>
                                                                        <asp:ListItem Text="Purchasing"></asp:ListItem>
                                                                        <asp:ListItem Text="Quality"></asp:ListItem>
                                                                        <asp:ListItem Text="Safety"></asp:ListItem>
                                                                        <asp:ListItem Text="Security"></asp:ListItem>
                                                                        <asp:ListItem Text="Shipping/Warehouse"></asp:ListItem>
                                                                        <asp:ListItem Text="HR"></asp:ListItem>
                                                                        <asp:ListItem Text="OTHER"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ValidationGroup="ResourceGroup" ID="RequiredFieldValidator1"
                                                                        ControlToValidate="ddlFunctionNewContact" runat="server" Font-Size="12px" ForeColor="Red">
                                                    <img src="../../App_Themes/Images/New Design/animated-gifs-exclamation-points-006.gif" />
                                                                    </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblOtherNewContact" CssClass="LabelFont" runat="server" Text="Other Details"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOtherNewContact" ClientIDMode="Static" CssClass="textbox curved"
                                                                        Height="50px" runat="server" TextMode="MultiLine" Style="font-size: 12px; resize: none;"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table style="float: right;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnOkNew" runat="server" Text="Ok" CssClass="button" ToolTip="Save Contact"
                                                CausesValidation="true" Width="60px" OnClick="btnOkNew_Click" ValidationGroup="ResourceGroup" />
                                            <%--        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnOkNew"
                                                ConfirmText="Are you sure you want to save?">
                                            </asp:ConfirmButtonExtender>--%>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancelNew" ClientIDMode="Static" runat="server" Text="Reset" CssClass="button"
                                                ToolTip="Cancel" Width="60px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:Panel ID="Panel4" Style="display: none" ClientIDMode="Static" runat="server"
                        CssClass="modalPopupMaster">
                        <table>
                            <tr>
                                <td>
                                    <table style="float: left;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAccountNumberMasterChange" CssClass="LabelFont" runat="server"
                                                    Text="Account Number"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccountNumberMasterChange" CssClass="textbox curved2" Width="80px"
                                                    ClientIDMode="Static" runat="server" ReadOnly="true" Style="font-size: 12px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label41" runat="server" CssClass="LabelFont" Text="Contact Number"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContactNumberMasterChange" CssClass="textbox curved2" Width="80px"
                                                    ClientIDMode="Static" runat="server" ReadOnly="true" Style="font-size: 12px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="float: right;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label42" CssClass="LabelFont" runat="server" Text="Account Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccountNameMasterChange" Width="262px" CssClass="textbox curved2"
                                                    ClientIDMode="Static" runat="server" ReadOnly="true" Style="font-size: 12px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label43" CssClass="LabelFont" runat="server" Text="Contact Name"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContactNameMasterChange" CssClass="textbox curved2" Width="262px"
                                                    ClientIDMode="Static" runat="server" ReadOnly="true" Style="font-size: 12px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TabContainer ID="tabMasterChange" runat="server">
                                        <asp:TabPanel ID="tabContactPreferences" runat="server" Font-Size="12px" Font-Names="Arial"
                                            HeaderText="Contact Preferences">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table style="border: 1px solid; width: 250px;">
                                                                <tr>
                                                                    <td style="background-color: #d6e0ec;">
                                                                        <asp:Label ID="lblMailHeader" Font-Bold="True" CssClass="LabelFont" runat="server"
                                                                            Text="Mail"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdoMail" CellSpacing="6" CssClass="radioList" runat="server">
                                                                            <asp:ListItem Text=" Reduce Mail attempts on Contact"></asp:ListItem>
                                                                            <asp:ListItem Text=" Never Mail Contact"></asp:ListItem>
                                                                            <asp:ListItem Text=" Never Mail Site"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td>
                                                            <table style="border: 1px solid; width: 250px;">
                                                                <tr>
                                                                    <td style="background-color: #d6e0ec;">
                                                                        <asp:Label ID="lblPhoneHeader" Font-Bold="True" CssClass="LabelFont" runat="server"
                                                                            Text="Phone"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdoPhone" CellSpacing="6" CssClass="radioList" runat="server">
                                                                            <asp:ListItem Text=" Reduce Call attempts on Contact"></asp:ListItem>
                                                                            <asp:ListItem Text=" Never Call Contact"></asp:ListItem>
                                                                            <asp:ListItem Text=" Never Call Site"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 10px;">
                                                            <table style="border: 1px solid; width: 250px;">
                                                                <tr>
                                                                    <td style="background-color: #d6e0ec;">
                                                                        <asp:Label ID="lblFaxHeader" Font-Bold="True" CssClass="LabelFont" runat="server"
                                                                            Text="Fax"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdoFax" CellSpacing="6" CssClass="radioList" runat="server">
                                                                            <asp:ListItem Text=" Never Fax Contact"></asp:ListItem>
                                                                            <asp:ListItem Text=" Never Fax Site"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td style="padding-top: 10px;">
                                                            <table style="border: 1px solid; width: 250px; height: 73px;">
                                                                <tr>
                                                                    <td style="background-color: #d6e0ec; height: 25%;">
                                                                        <asp:Label ID="lblEmailHeader" Font-Bold="True" CssClass="LabelFont" runat="server"
                                                                            Text="Email"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdoEmail" CellSpacing="6" CssClass="radioList" runat="server">
                                                                            <asp:ListItem Text=" Never Email Contact"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <div style="float: right;">
                                                                <asp:Button ID="btnSavePreferences" runat="server" Width="70px" Text="Save" OnClick="btnSavePreferences_Click"
                                                                    CssClass="button" />
                                                                <asp:Button ID="btnCancelPreferences" runat="server" Width="70px" Text="Clear" OnClick="btnCancelPreferences_Click"
                                                                    CssClass="button" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="titeContactPreferenceHeader" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                Font-Size="14px" Text="REQUEST HISTORY"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="table-wrapper page1">
                                                                <table style="border: 0; width: 100%; height: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="Panel7" runat="server" Width="560px" Style="max-height: 200px;" ScrollBars="Auto">
                                                                                <asp:GridView ID="gridContactPreferences" runat="server" AllowSorting="True" OnSorting="gridPreferences_Sorting"
                                                                                    AutoGenerateColumns="False" Font-Size="12px" GridLines="None" CellPadding="4"
                                                                                    CellSpacing="2" BackColor="White" Font-Names="Arial">
                                                                                    <AlternatingRowStyle BackColor="#E5E5E5" />
                                                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="False" Height="30px" CssClass="HeaderStyle" />
                                                                                    <RowStyle CssClass="RowStyle" Wrap="False" />
                                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="ACCOUNT NUMBER" DataField="sold_to" SortExpression="sold_to">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CREATED BY" DataField="createdby" SortExpression="createdby">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CREATED DATE" DataFormatString="{0:MMM dd, yyyy}" DataField="createdon"
                                                                                            SortExpression="createdon">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CONTACT NUMBER" DataField="Buyerct" SortExpression="Buyerct">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CONTACT NAME" DataField="ContactName" SortExpression="ContactName">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="MAIL" DataField="Mail" SortExpression="Mail">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="FAX" DataField="Fax" SortExpression="Fax">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="EMAIL" DataField="Email" SortExpression="Email">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="PHONE" DataField="Phone" SortExpression="Phone">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="tabAccountChanges" runat="server" HeaderText="Account Changes">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label44" CssClass="LabelFont" runat="server" Text="Account Name"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAccountNameChanges" ClientIDMode="Static" CssClass="textbox curved"
                                                                            Width="200px" runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label52" runat="server" CssClass="LabelFont" Text="Country"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlCountry" Width="210px" CssClass="textbox curved" runat="server"
                                                                            Style="font-size: 12px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label45" CssClass="LabelFont" runat="server" Text="Phone Number"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhoneNumberAccountChanges" CssClass="textbox curved" Width="200px"
                                                                            runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label48" CssClass="LabelFont" runat="server" Text="State"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtStateAccountChanges" CssClass="textbox curved" Width="200px"
                                                                            runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label47" CssClass="LabelFont" runat="server" Text="Fax Number"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFaxNumberAccountChanges" Width="200px" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label50" CssClass="LabelFont" runat="server" Text="Zip"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtZipAccountChanges" CssClass="textbox curved" Width="200px" runat="server"
                                                                            Style="font-size: 12px"> </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label49" CssClass="LabelFont" runat="server" Text="Address 1"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAddress1AccountChanges" Width="200px" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label46" CssClass="LabelFont" runat="server" Text="City"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCityAccountChanges" CssClass="textbox curved" Width="200px" runat="server"
                                                                            Style="font-size: 12px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label51" runat="server" CssClass="LabelFont" Text="Address 2"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAddress2AccountChanges" Width="200px" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label53" CssClass="LabelFont" runat="server" Text="Comment"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCommentAccountChanges" CssClass="textbox curved" Width="200px"
                                                                            runat="server" Style="font-size: 12px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="float: right;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnSaveAccountChanges" runat="server" Width="70px" Text="Save" OnClick="SaveAccountChanges_Click"
                                                                            CssClass="button" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnCancelAccountChanges" runat="server" Width="70px" Text="Clear"
                                                                            CssClass="button" OnClick="CancelAccountChanges_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Bold="true" Font-Size="14px"
                                                                Text="REQUEST HISTORY"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="table-wrapper page1">
                                                                <table style="border: 0; width: 100%; height: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="Panel8" runat="server" Width="560px" Style="max-height: 200px;" ScrollBars="Auto">
                                                                                <asp:GridView ID="gridAccountChanges" runat="server" AllowSorting="True" OnSorting="gridAccountChanges_Sorting"
                                                                                    AutoGenerateColumns="False" GridLines="None" CellPadding="4" CellSpacing="2"
                                                                                    BackColor="#FFFFFF">
                                                                                    <AlternatingRowStyle BackColor="#e5e5e5" />
                                                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="ACCOUNT NUMBER" DataField="sold_to" SortExpression="sold_to">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CREATED BY" DataField="createdby" SortExpression="createdby">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CREATED DATE" DataFormatString="{0:MMM dd, yyyy}" DataField="createdon"
                                                                                            SortExpression="createdon">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="ACCOUNT NAME" DataField="AccountName" SortExpression="AccountName">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="PHONE" DataField="Phone" SortExpression="Phone">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="FAX" DataField="Fax" SortExpression="Fax">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="1st ADDRESS" DataField="Address1" SortExpression="Address1">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="2nd ADDRESS" DataField="Address2" SortExpression="Address2">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CITY" DataField="City" SortExpression="City">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="STATE" DataField="State" SortExpression="State">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="ZIP" DataField="Zip" SortExpression="Zip">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="COUNTRY" DataField="Country" SortExpression="Country">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="COMMENT" DataField="Comment" SortExpression="Comment">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="tabContactChanges" runat="server" HeaderText="Contact Changes">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label542" CssClass="LabelFont" runat="server" Text="First Name"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFirstnameContactChanges" Width="150px" ClientIDMode="Static"
                                                                            CssClass="textbox curved" runat="server" Style="font-size: 12px;"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label58a" CssClass="LabelFont" runat="server" Text="Email Address"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmailAddressContactChanges" Width="210px" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px;">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label55" CssClass="LabelFont" runat="server" Text="Last Name"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLastnameContactChanges" Width="150px" ClientIDMode="Static" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px;"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label54" CssClass="LabelFont" runat="server" Text="Title"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTitleContactChanges" Width="210px" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px;"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label541" CssClass="LabelFont" runat="server" Text="Phone"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhoneContactChanges" Width="150px" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px;"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label57" CssClass="LabelFont" runat="server" Text="Status"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlStatusContactChanges" CssClass="textbox curved" Width="220px"
                                                                            runat="server" Style="font-size: 12px;">
                                                                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Primary Decision Maker"></asp:ListItem>
                                                                            <asp:ListItem Text="Decision Influencer"></asp:ListItem>
                                                                            <asp:ListItem Text="Purchasing Agent"></asp:ListItem>
                                                                            <asp:ListItem Text="No Longer there"></asp:ListItem>
                                                                            <asp:ListItem Text="Duplicate Contact"></asp:ListItem>
                                                                            <asp:ListItem Text="Added wrong Customer"></asp:ListItem>
                                                                            <asp:ListItem Text="Deceased"></asp:ListItem>
                                                                            <asp:ListItem Text="Other Contact"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label56" runat="server" CssClass="LabelFont" Text="Phone Extension"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPhoneExtContactChanges" Width="150px" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px;"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label57s" CssClass="LabelFont" runat="server" Text="Function"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlFunctionContactChanges" CssClass="textbox curved" Width="220px"
                                                                            runat="server" Style="font-size: 12px;">
                                                                            <asp:ListItem Text="" Selected="True">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Facilities">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Maintenance">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Office/Admin">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Owner/Pres/Senior Mgmt">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Plant/Prod/Ops">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Project Manager">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Purchasing">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Quality">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Safety">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Security">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="Shipping/Warehouse">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="HR">
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Text="OTHER">
                                                                            </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label58s" CssClass="LabelFont" runat="server" Text="Department"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDepartmentContactChanges" Width="150px" CssClass="textbox curved"
                                                                            runat="server" Style="font-size: 12px;"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCommentContactChanges" CssClass="LabelFont" runat="server" Text="Comment"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCommentContactChanges" CssClass="textbox curved" Width="210px"
                                                                            runat="server" Style="font-size: 12px;"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="float: right;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnSaveContactChanges" runat="server" CssClass="button" Width="70px"
                                                                            Text="Save" OnClick="SaveContactChanges_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnCancelContactChanges" runat="server" CssClass="button" Width="70px"
                                                                            Text="Clear" OnClick="CancelContactChanges_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Names="Arial" Font-Size="14px"
                                                                Text="REQUEST HISTORY"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="table-wrapper page1">
                                                                <table style="border: 0; width: 100%; height: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="Panel9" ScrollBars="Auto" runat="server" Width="560px" Style="max-height: 200px;">
                                                                                <asp:GridView ID="gridContactChanges" runat="server" AllowSorting="true" OnSorting="gridContactChanges_Sorting"
                                                                                    AutoGenerateColumns="False" CellPadding="4" CellSpacing="2" BackColor="#FFFFFF"
                                                                                    GridLines="None">
                                                                                    <AlternatingRowStyle BackColor="#e5e5e5" />
                                                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                    <Columns>
                                                                                        <asp:BoundField HeaderText="ACCOUNT NUMBER" DataField="sold_to" SortExpression="sold_to">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CREATED BY" DataField="createdby" SortExpression="createdby">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CREATED DATE" DataFormatString="{0:MMM dd, yyyy}" DataField="createdon"
                                                                                            SortExpression="createdon">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="CONTACT NUMBER" DataField="Buyerct" SortExpression="Buyerct">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="FIRST NAME" DataField="firstname" SortExpression="firstname">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="LAST NAME" DataField="lastname" SortExpression="lastname">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="STATUS" DataField="Status" SortExpression="Status">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="FUNCTION" DataField="Function" SortExpression="Function">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="TITLE" DataField="Title" SortExpression="Title">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="PHONE" DataField="Phone" SortExpression="Phone">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="PHONE EXTENSION" DataField="PhoneExt" SortExpression="PhoneExt">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="DEPARTMENT" DataField="Department" SortExpression="Department">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="EMAIL ADDRESS" DataField="Email_Address" SortExpression="Email_Address">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="COMMENT" DataField="comment" SortExpression="comment">
                                                                                            <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:Panel ID="PnlOrderHistory" runat="server" BorderStyle="Solid" BorderWidth="2px"
            Style="background-color: White;" BorderColor="#9b9b9b">
            <table>
                <tr>
                    <td style="vertical-align: top; padding: 10px;">
                        <table style="padding-bottom: 5px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSiteName" CssClass="LabelFont" runat="server" Text="Site Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtSiteName" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblOrganization" CssClass="LabelFont" runat="server" Text="Organization"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtOrganization" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIndustry" CssClass="LabelFont" runat="server" Text="Industry"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtIndustry" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="SIC2" runat="server" CssClass="LabelFont" Text="SIC2"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtSIC2" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top; padding: 10px;">
                        <table style="padding-bottom: 5px;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label31" runat="server" CssClass="LabelFont" Text="Employee Size"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtEmployeeSize" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblBuyerOrg" CssClass="LabelFont" runat="server" Text="Buyer Org"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtBuyerOrg" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblKeyAcctMng" CssClass="LabelFont" runat="server" Text="Key Acct Mng"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtKeyAcctMng" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top; padding: 10px;">
                        <table style="padding-bottom: 5px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLifetimeSales" CssClass="LabelFont" runat="server" Text="Lifetime Sales"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtLifetimeSales" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLast12MSales" CssClass="LabelFont" runat="server" Text="Last 12M Sales"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtLast12MSales" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLifetimeOrders" CssClass="LabelFont" runat="server" Text="Lifetime Orders"
                                        AssociatedControlID="txtLifetimeOrders"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtLifetimeOrders" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top; padding: 10px;">
                        <table style="padding-bottom: 5px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLast12MOrder" CssClass="LabelFont" runat="server" Text="Last 12M Orders"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtLast12MOrder" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLastOrderDate" CssClass="LabelFont" runat="server" Text="Last Order Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtLastOrderDate" ClientIDMode="Static" CssClass="textboxLabel" Style="min-width: 200px;"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%--   <asp:CollapsiblePanelExtender ID="cpeSiteContactInfo" TargetControlID="PnlOrderHistory"
            runat="server">
        </asp:CollapsiblePanelExtender>--%>
        <asp:UpdatePanel ID="UpdateSiteContactInfo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div align="left" style="font-size: 11px; width: 200px;">
                    <asp:LinkButton ID="LinkButton2" OnClick="btnTitleContactInfo_Click" Style="text-decoration: none;"
                        Font-Size="Medium" runat="server">
                        <asp:Label ID="lblTitleContactInfo" Font-Size="X-Large" runat="server"></asp:Label></asp:LinkButton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--    <asp:CollapsiblePanelExtender ID="cpeSearchContact" TargetControlID="PnlContactInfo"
            runat="server">
        </asp:CollapsiblePanelExtender>--%>
        <asp:Panel ID="PnlContactInfo" runat="server" Style="display: inline;">
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdoContactNumber" CssClass="radioButton" ClientIDMode="Static"
                                        runat="server" Checked="true" Text=" Contact Number" GroupName="RdoFilterKam" />
                                </td>
                                <td>
                                    <asp:RadioButton ID="rdoContactFName" CssClass="radioButton" ClientIDMode="Static"
                                        runat="server" Text=" First Name" GroupName="RdoFilterKam" />
                                </td>
                                <td>
                                    <asp:RadioButton ID="rdoContactLName" CssClass="radioButton" ClientIDMode="Static"
                                        runat="server" Text=" Last Name" GroupName="RdoFilterKam" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <div class="SearchBox">
                                        <asp:Label CssClass="SearchBox lbl" ID="lblSearchBy" Text="Contact # " runat="server"
                                            ClientIDMode="Static" Font-Names="Arial" Font-Size="12px"></asp:Label>
                                        <asp:TextBox ID="txtSearchContact" Width="100px" ClientIDMode="Static" runat="server"
                                            CssClass="txtSearchQuery"></asp:TextBox>
                                        <asp:ImageButton ID="btnSearchContact" ClientIDMode="Static" CssClass="SearchBox btnSearch"
                                            ImageUrl="~/App_Themes/Images/New Design/magnifying-glass-search-icon.jpg" runat="server"
                                            ToolTip="Search" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:UpdatePanel ID="UpdateContactInfo" runat="server">
            <ContentTemplate>
                <div class="table-wrapper page1">
                    <table style="border: 0;" cellpadding="0" cellspacing="0" width="100%" height="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="DataDiv" ClientIDMode="Static" Style="width: 100%; height: 100%;"
                                    runat="server">
                                    <asp:GridView AutoGenerateColumns="true" ClientIDMode="Static" ID="gridSiteContactInfo"
                                        runat="server" AllowPaging="false" PageSize="4" CellPadding="4" CellSpacing="2"
                                        ForeColor="Black" EmptyDataText="No data available." AllowSorting="True" Font-Size="12px"
                                        BackColor="#FFFFFF" OnSorting="gridSiteContactInfo_Sorting" GridLines="None"
                                        OnRowDataBound="gridSiteContactInfo_RowDataBound" DataKeyNames="Row">
                                        <%--   <AlternatingRowStyle BackColor="#e5e5e5" />--%>
                                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" Wrap="false" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:RadioButton runat="server" ID="rdbUser" onclick="javascript:SelectSingleRadiobutton(this.id)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--Popup Dialog for Contact Details--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlPopupCustInfo" runat="server" Style="display: none;" ClientIDMode="Static">
                    <table>
                        <tr>
                            <td id="CustInfo">
                                <table style="padding-bottom: 60px;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopFirstName" CssClass="LabelFont" runat="server" Text="First Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopFirstName" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopLastName" CssClass="LabelFont" runat="server" Text="Last Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopLastName" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopContactType" CssClass="LabelFont" runat="server" Text="Contact Type"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopContactType" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopSAPStat" CssClass="LabelFont" runat="server" Text="SAP Status"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopSAPStat" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopRecency" CssClass="LabelFont" runat="server" Text="Recency"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopRecency" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopDepartment" CssClass="LabelFont" runat="server" Text="Department"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopDepartment" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopFunction" CssClass="LabelFont" runat="server" Text="Function"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopFunction" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopTitle" CssClass="LabelFont" runat="server" Text="Title"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopTitle" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopDirectPhone" CssClass="LabelFont" runat="server" Text="Direct Phone"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopDirectPhone" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopSitePhone" CssClass="LabelFont" runat="server" Text="Site Phone"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopSitePhone" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopPhoneExt" CssClass="LabelFont" runat="server" Text="Phone Extension"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopPhoneExt" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopEmail" CssClass="LabelFont" runat="server" Text="Email"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopEmail" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopDoNotMail" CssClass="LabelFont" runat="server" Text="Do Not Mail"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopDoNotMail" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopDoNotFax" CssClass="LabelFont" runat="server" Text="Do Not Fax"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopDoNotFax" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopDoNotEmail" CssClass="LabelFont" runat="server" Text="Do Not Email"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopDoNotEmail" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopDoNotCall" CssClass="LabelFont" runat="server" Text="Do Not Call"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopDoNotCall" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopLifeTimeSales" CssClass="LabelFont" runat="server" Text="Life Time Sales"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopLifeTimeSales" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopLast12MSales" CssClass="LabelFont" runat="server" Text="Last 12M Sales"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopLast12MSales" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPopLastOrderDate" CssClass="LabelFont" runat="server" Text="Last Order Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtPopLastOrderDate" runat="server" CssClass="textboxLabel" Style="font-size: 12px;
                                                width: 100%;" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%-----------------------------FOR NOT PC-------------------------------%>
                            <%if (Clement == "false")
                              { %>
                            <td id="QQInfo">
                                <table>
                                    <tr>
                                        <td>
                                            <%if (Contpcman == "false")
                                              { %>
                                            <table>
                                                <tr>
                                                    <td valign="middle">
                                                        <table id="ContactLevelNotPC" style="border: 1px solid; height: 106px;" runat="server"
                                                            clientidmode="Static">
                                                            <tr>
                                                                <td style="background-color: #d6e0ec;">
                                                                    <asp:Label ID="lblContactInfoHeader" Font-Bold="true" CssClass="LabelFont" runat="server"
                                                                        Text="Contact Level Information"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left: 10px; padding-right: 0px;">
                                                                    <table style="padding-left: 5px; padding-right: 0px;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblContactStatusQQ" CssClass="LabelFont" runat="server" AssociatedControlID="ddlContactStatus"
                                                                                    Text="Contact Status"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlContactStatus" Width="200px" CssClass="textbox curved" ClientIDMode="Static"
                                                                                    runat="server" Style="font-size: 12px;">
                                                                                    <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Primary Decision Maker"></asp:ListItem>
                                                                                    <asp:ListItem Text="Decision Influencer"></asp:ListItem>
                                                                                    <asp:ListItem Text="Purchasing Agent"></asp:ListItem>
                                                                                    <asp:ListItem Text="No Longer there"></asp:ListItem>
                                                                                    <asp:ListItem Text="Duplicate Contact"></asp:ListItem>
                                                                                    <asp:ListItem Text="Added wrong Customer"></asp:ListItem>
                                                                                    <asp:ListItem Text="Deceased"></asp:ListItem>
                                                                                    <asp:ListItem Text="Other Contact"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label3" runat="server" CssClass="LabelFont" Text="Job Area"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlContFunc" Width="200px" CssClass="textbox curved" ClientIDMode="Static"
                                                                                    runat="server" Style="font-size: 12px;">
                                                                                    <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Facilities"></asp:ListItem>
                                                                                    <asp:ListItem Text="Maintenance"></asp:ListItem>
                                                                                    <asp:ListItem Text="Office/Admin"></asp:ListItem>
                                                                                    <asp:ListItem Text="Owner/Pres/Senior Mgmt"></asp:ListItem>
                                                                                    <asp:ListItem Text="Plant/Prod/Ops"></asp:ListItem>
                                                                                    <asp:ListItem Text="Project Manager"></asp:ListItem>
                                                                                    <asp:ListItem Text="Purchasing"></asp:ListItem>
                                                                                    <asp:ListItem Text="Quality"></asp:ListItem>
                                                                                    <asp:ListItem Text="Safety"></asp:ListItem>
                                                                                    <asp:ListItem Text="Security"></asp:ListItem>
                                                                                    <asp:ListItem Text="Shipping/Warehouse"></asp:ListItem>
                                                                                    <asp:ListItem Text="HR"></asp:ListItem>
                                                                                    <asp:ListItem Text="OTHER"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <% }%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%-----------------------------FOR PC-------------------------------%>
                                            <%if (Contpcman == "true")
                                              { %>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table id="SiteLevelPC" style="border: 1px solid; height: 106px;" runat="server"
                                                            clientidmode="Static">
                                                            <tr>
                                                                <td style="background-color: #d6e0ec;">
                                                                    <asp:Label ID="Label4" Font-Bold="true" CssClass="LabelFont" runat="server" Text="Site Level Information"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Emp" CssClass="LabelFont" runat="server" Text="Offers Health Insurance to Employees?"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlHealth" CssClass="textbox curved" ClientIDMode="Static"
                                                                                    runat="server" Style="font-size: 12px;">
                                                                                    <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                    <asp:ListItem Text="No"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblSpanish" CssClass="LabelFont" ClientIDMode="Static" runat="server"
                                                                                    Text="Employs Spanish"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlSpanish" CssClass="textbox curved" ClientIDMode="Static"
                                                                                    runat="server" Style="font-size: 12px;">
                                                                                    <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="Yes"></asp:ListItem>
                                                                                    <asp:ListItem Text="No"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblEmployeeSize2" CssClass="LabelFont" runat="server" Text="Employee Size"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <%--<asp:DropDownList ID="ddlEmployeeSize" CssClass="textbox curved" ClientIDMode="Static"
                                                                                    runat="server" Style="font-size: 12px;">
                                                                                    <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                                    <asp:ListItem Text="A: 1-19"></asp:ListItem>
                                                                                    <asp:ListItem Text="B: 20-49"></asp:ListItem>
                                                                                    <asp:ListItem Text="C: 50-99"></asp:ListItem>
                                                                                    <asp:ListItem Text="D: 100-249"></asp:ListItem>
                                                                                    <asp:ListItem Text="E: 250-499"></asp:ListItem>
                                                                                    <asp:ListItem Text="F: 500+"></asp:ListItem>
                                                                                    <asp:ListItem Text="G: Unknown"></asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                                                <asp:TextBox ID="txtEmployeeSizeValue" Width="70px" CssClass="textbox curved"
                                                                            runat="server" ClientIDMode="Static" Style="font-size: 12px;"></asp:TextBox>
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
                                    <tr>
                                        <td>
                                            <table id="ContactLevelPC" style="border: 1px solid; height: 106px;" runat="server"
                                                clientidmode="Static">
                                                <tr>
                                                    <td style="background-color: #d6e0ec; height: 12%;">
                                                        <asp:Label ID="Label5" Font-Bold="true" CssClass="LabelFont" runat="server" Text="Contact Level Information"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblContactStatusPC" CssClass="LabelFont" runat="server" Text="Contact Status"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlNewContactStatus" CssClass="textbox curved" ClientIDMode="Static"
                                                                        runat="server" Style="font-size: 12px;">
                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Primary Decision Maker"></asp:ListItem>
                                                                        <asp:ListItem Text="Decision Influencer"></asp:ListItem>
                                                                        <asp:ListItem Text="Purchasing Agent"></asp:ListItem>
                                                                        <asp:ListItem Text="No Longer there"></asp:ListItem>
                                                                        <asp:ListItem Text="Duplicate Contact"></asp:ListItem>
                                                                        <asp:ListItem Text="Added wrong Customer"></asp:ListItem>
                                                                        <asp:ListItem Text="Other Contact"></asp:ListItem>
                                                                        <asp:ListItem Text="Deceased"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblJobArea" CssClass="LabelFont" runat="server" Text="Job Area"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlJobArea" CssClass="textbox curved" ClientIDMode="Static"
                                                                        runat="server" Style="font-size: 12px;">
                                                                        <asp:ListItem Text="" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Facilities"></asp:ListItem>
                                                                        <asp:ListItem Text="Maintenance"></asp:ListItem>
                                                                        <asp:ListItem Text="Office/Admin"></asp:ListItem>
                                                                        <asp:ListItem Text="Owner/Pres/Senior Mgmt"></asp:ListItem>
                                                                        <asp:ListItem Text="Plant/Prod/Ops"></asp:ListItem>
                                                                        <asp:ListItem Text="Project Manager"></asp:ListItem>
                                                                        <asp:ListItem Text="Purchasing"></asp:ListItem>
                                                                        <asp:ListItem Text="Quality"></asp:ListItem>
                                                                        <asp:ListItem Text="Security"></asp:ListItem>
                                                                        <asp:ListItem Text="Shipping/Warehouse"></asp:ListItem>
                                                                        <asp:ListItem Text="HR"></asp:ListItem>
                                                                        <asp:ListItem Text="OTHER"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
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
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblUpdatedbyPC" CssClass="LabelFont" runat="server" Text="Last Updated by"></asp:Label>
                                                        <asp:Label ID="lblUpdatedbyPCWho" CssClass="textboxLabel" ClientIDMode="Static" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDate" CssClass="LabelFont" runat="server" Text="Last Updated Date"></asp:Label>
                                                        <asp:Label ID="lblDateWho" CssClass="textboxLabel" ClientIDMode="Static" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%-- <asp:Button ID="btnUpdatePC" Width="80px" CssClass="button" runat="server" Text="Update"
                                                            OnClick="UpdatePC" />--%>
                                                        <asp:Button ID="btnSavePC" Width="80px" CssClass="button" runat="server" Text="Save"
                                                            OnClick="SavePC" />
                                                        <%--<asp:Button ID="btnCancelPC" Width="80px" CssClass="button" runat="server" Text="Cancel"
                                                            OnClick="CancelPC" />--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <% }%>
                                    <%if (Contpcman == "false")
                                      { %>
                                    <tr>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="lblUpdatedBy" CssClass="LabelFont" runat="server" Text="Last Updated By:"></asp:Label>
                                            <asp:Label ID="lblUpdatedByWho" CssClass="textboxLabel" ClientIDMode="Static" runat="server"
                                                Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="lblUpdatedDate" CssClass="LabelFont" runat="server" Text="Last Updated Date:"></asp:Label>
                                            <asp:Label ID="lblUpdatedDateWhen" CssClass="textboxLabel" ClientIDMode="Static"
                                                runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%-- <asp:Button ID="btnUpdateQQ" CssClass="button" runat="server" Text="Update" Width="50px"
                                                OnClick="UpdateQQ" />
                                            &nbsp;--%>
                                            &nbsp;
                                            <asp:Button ID="btnSaveQQ" CssClass="button" runat="server" Text="Save" Width="50px"
                                                OnClick="SaveQQ" ClientIDMode="Static" />
                                            <%-- &nbsp;
                                            <asp:Button ID="btnCancelQQ" CssClass="button" runat="server" Text="Cancel" Width="50px"
                                                OnClick="CancelQQ" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <% }%>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="visibility: hidden">
                                                test</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <%--<tr>
                                            <td>
                                                <asp:Label ID="lblUpdatedBy" CssClass="LabelFont" runat="server" Text="Last Updated By:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUpdatedByWho" CssClass="LabelFont" ClientIDMode="Static" runat="server"
                                                    Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblUpdatedDate" CssClass="LabelFont" runat="server" Text="Last Updated Date:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUpdatedDateWhen" CssClass="LabelFont" ClientIDMode="Static" runat="server"
                                                    Text=""></asp:Label>
                                            </td>
                                        </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <% }%>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="PopupConfrm" runat="server" Style="display: none;" ClientIDMode="Static">
                    <table>
                        <tr>
                            <td>
                                <div>
                                    <img id="conImage" src="../../App_Themes/Images/New Design/confrm.gif" />
                                </div>
                            </td>
                            <td valign="middle">
                                Are you sure?</div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnEmailID" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
    <asp:HiddenField ID="tempSafety" runat="server" Value="0" />
    <asp:HiddenField ID="hfModalVisible" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnContactNumber" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="CurrencyCode" runat="server" ClientIDMode="Static" Value="" />
</asp:Content>
