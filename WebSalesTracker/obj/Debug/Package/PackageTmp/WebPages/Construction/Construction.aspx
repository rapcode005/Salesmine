<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Construction.aspx.cs" Inherits="WebSalesMine.WebPages.Construction.Construction"
    MaintainScrollPositionOnPostback="false" %>

<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<%@ Import Namespace="AppLogic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
    <link type="text/css" rel="stylesheet" href="../../App_Themes/CSS/demos.css" />
    <style type="text/css">
        .FixedPostion
        {
            /*position: fixed;
            vertical-align:bottom;*/
            position: fixed;
            text-decoration: none;
        }
    </style>
    <script type="text/javascript">
        var varcallnotes;


        function onDeleteSuccess() {

            alert("Successfully Deleted");

        }

        function onUpdateSuccess() {
            alert("Successfully Update");
        }


        function onAddSuccess(Stat) {

            // document.getElementById('hdnProjectID2').value = '';

            var varProjectID = document.getElementById('hdnProjectID2').value;

            if (Stat == true) {
                alert("Successfully Added");
                document.getElementById('hdnSubConGWCount').value = "0";
                setCookie('CProjID', varProjectID, -1);
                __doPostBack($('#<%= btnAddCon.ClientID %>').attr('name'), '');
                ClearTxtbox();
                $("#gwReedTerritories  tr").closest('TR').removeClass('SelectedRowStyle');
            }
            else {
                alert("Adding Failed");
            }

        }

        function ClearTxtbox() {

            jQuery("#txtTitle").val('');
            jQuery("#txtStage").val('');
            jQuery("#txtCountry").val('');
            jQuery("#txtCity").val('');
            jQuery("#TxtStateProv").val('');
            jQuery("#txtValue").val('');
            jQuery("#txtBidDate").val('');
            jQuery("#txtMatchingDoc").val('');

            jQuery("#rdoActive").attr("checked", false);
            jQuery("#rdoInActive").attr("checked", false);
            jQuery("#chkDonotCall").attr("checked", false);
            jQuery("#chkKAMManaged").attr("checked", false);


            //SubCon


        }


        function ClearCallNotes() {
            jQuery("#txtCallNotes").val('');
            jQuery("#txtNextFollowupContact").val('');
            jQuery("#txtFollowupDate").val('');
            jQuery("#txtNumCall").val('');
            jQuery("#txtlastUpdatedBy3").val('');
            jQuery("#txtLastUpdatedDate3").val('');
            jQuery("#txtfollowupNotes").val('');
        }

        function onMouseHover() {
            $('#gwReedTerritories').css("cursor", "pointer");
        }

        function onKeyupSearchKam(args) {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            var index;
        }


        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {

            }
        }

        function OnSuccess(Data) {
            var RowIndex = Data;
            var grd = document.getElementById('gwSubCon');
            var CompanyName, Title, SubName, SubPhone, SubEmail, Notes, Account, Customer, KAM, username, Date, UID
            var FIREFOX = /Firefox/i.test(navigator.userAgent);

            if (grd) {
                if (isNaN(RowIndex) == false && RowIndex != -1) {
                    totalCols = $("#gwSubCon").find('tr')[0].cells.length;
                    for (var i = 1; i < totalCols + 1; i++) {
                        var ColName = $('#gwSubCon tr').find('th:nth-child(' + i + ')').text();

                        if (ColName == 'Company Name') {
                            CompanyName = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CompanyName == undefined || CompanyName == '&nbsp;') {
                                CompanyName = '';
                            }
                        }
                        else if (ColName == 'Title') {

                            Title = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (Title == undefined || Title == '&nbsp;') {
                                Title = '';
                            }
                        }
                        else if (ColName == 'Name') {

                            SubName = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (SubName == undefined || SubName == '&nbsp;') {
                                SubName = '';
                            }
                        }
                        else if (ColName == 'Phone') {

                            SubPhone = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (SubPhone == undefined || SubPhone == '&nbsp;') {
                                SubPhone = '';
                            }
                        }
                        else if (ColName == 'Email') {

                            SubEmail = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (SubEmail == undefined || SubEmail == '&nbsp;') {
                                SubEmail = '';
                            }
                        }
                        else if (ColName == 'Notes') {

                            Notes = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (Notes == undefined || Notes == '&nbsp;') {
                                Notes = '';
                            }
                        }
                        else if (ColName == 'Account #') {

                            Account = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (Account == undefined || Account == '&nbsp;') {
                                Account = '';
                            }
                        }
                        else if (ColName == 'Customer') {

                            Customer = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (Customer == undefined || Customer == '&nbsp;') {
                                Customer = '';
                            }
                        }
                        else if (ColName == 'KAM') {

                            KAM = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (KAM == undefined || KAM == '&nbsp;') {
                                KAM = '';
                            }
                        }
                        else if (ColName == 'Username') {

                            username = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (username == undefined || username == '&nbsp;') {
                                username = '';
                            }
                        }
                        else if (ColName == 'Date Created') {

                            Date = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (Date == undefined || Date == '&nbsp;') {
                                Date = '';
                            }
                        }
                        else if (ColName == 'UID') {

                            UID = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (UID == undefined || UID == '&nbsp;') {
                                UID = '';
                            }
                        }
                    }



                    if (UID != undefined && UID != '&nbsp;') {
                        document.getElementById('hdnSubConID').value = UID;
                    }
                    else {
                        document.getElementById('hdnSubConID').value = '';
                    }

                    if (CompanyName != undefined && CompanyName != '&nbsp;') {
                        $('#txtCompanyName').val((CompanyName).replace(/amp;/ig, ''));
                    }
                    else {
                        $('#txtCompanyName').val('');
                    }

                    if (Title != undefined && Title != '&nbsp;') {
                        $('#txtTitle2').val((Title).replace(/amp;/ig, ''));
                    }
                    else {
                        $('#txtTitle2').val('');
                    }

                    if (SubName != undefined && SubName != '&nbsp;') {
                        $('#txtName2').val((SubName).replace(/amp;/ig, ''));
                    }
                    else {
                        $('#txtName2').val('');
                    }

                    if (SubPhone != undefined && SubPhone != '&nbsp;') {
                        $('#txtPhone2').val(SubPhone);
                    }
                    else {
                        $('#txtPhone2').val('');
                    }


                    if (SubEmail != undefined && SubEmail != '&nbsp;') {

                        $('#txtEmail2').val((SubEmail).replace(/amp;/ig, ''));

                    }
                    else {
                        $('#txtEmail2').val('');
                    }

                    if (Notes != undefined && Notes != '&nbsp;') {
                        $('#txtNotes2').val((Notes).replace(/amp;/ig, ''));
                    }
                    else {
                        $('#txtNotes2').val('');
                    }

                    if (Account != undefined && Account != '&nbsp;') {
                        $('#txtAccountNum2').val(Account);
                    }
                    else {
                        $('#txtAccountNum2').val('');
                    }

                    if (Customer != undefined && Customer != '&nbsp;') {
                        if (Customer == 'No') {
                            Customer = 1;
                        }
                        else {
                            Customer = 0;
                        }
                        $('#dlCustomerYN').val(Customer);
                    }
                    else {
                        $('#dlCustomerYN').val(2);
                    }

                    if (KAM != undefined && KAM != '&nbsp;') {
                        if (KAM == 'No') {
                            KAM = 1;
                        }
                        else {
                            KAM = 0;
                        }
                        $('#dlKANYN').val(KAM);
                    }
                    else {
                        $('#dlKANYN').val(2);
                    }

                    if (username != undefined && username != '&nbsp;') {
                        $('#lblLastUpdatedBy2').val(username);
                    }
                    else {
                        $('#lblLastUpdatedBy2').val('');
                    }

                    if (Date != undefined && Date != '&nbsp;') {
                        $('#lblLastUpdatedDate2').val(Date);
                    }
                    else {
                        $('#lblLastUpdatedDate2').val('');
                    }

                    $("input[type=submit][id=btnDelete]").attr('disabled', false);
                    $("input[type=submit][id=btnSubUpdate]").attr('disabled', false);
                }
            }
        }

        function checkLength(o, n, min, max) {

            if (o.val().length > max || o.val().length < min) {
                o.addClass("ui-state-error");
                updateTips("Length of " + n + " must be between " +
					min + " and " + max + ".");

                return false;
            } else {

                return true;
            }
        }

        function updateTips(t) {
            tips = $(".validateTips");

            tips
				.text(t)
				.addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }

        //        function OnSuccess2(Data) {

        //            var RowIndex = Data + 1;
        //            var grd = document.getElementById('gwReedTerritories');
        //            var ProjectId, BidDate, Title, Stage, Status, DoNotCall=0, Managed=0, Country, City, State, Value, MatchingDocs

        //            var FIREFOX = /Firefox/i.test(navigator.userAgent);


        //            if (grd) {
        //                if (isNaN(RowIndex) == false) {
        //                    totalCols = $("#gwReedTerritories").find('tr')[0].cells.length;
        //                    for (var i = 1; i < totalCols + 1; i++) {
        //                        var ColName = $('#gwReedTerritories tr').find('th:nth-child(' + i + ')').text();

        //                        if (ColName == 'Project ID') {
        //                            ProjectId = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (ProjectId == undefined || ProjectId == '&nbsp;') {
        //                                ProjectId = '';
        //                            }
        //                        }

        //                        if (ColName == 'Bid Date') {
        //                            BidDate = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (BidDate == undefined || BidDate == '&nbsp;') {
        //                                BidDate = '';
        //                            }
        //                        }

        //                        if (ColName == 'Title') {
        //                            Title = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (Title == undefined || Title == '&nbsp;') {
        //                                Title = '';
        //                            }
        //                        }

        //                        if (ColName == 'Stage') {
        //                            Stage = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (Stage == undefined || Stage == '&nbsp;') {
        //                                Stage = '';
        //                            }
        //                        }

        //                        if (ColName == 'Status') {
        //                            Status = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (Status == undefined || Status == '&nbsp;') {
        //                                Status = '';
        //                            }
        //                        }

        //                        if (ColName == 'Country') {
        //                            Country = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (Country == undefined || Country == '&nbsp;') {
        //                                Country = '';
        //                            }
        //                        }

        //                        if (ColName == 'State/Province') {
        //                            State = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (State == undefined || State == '&nbsp;') {
        //                                State = '';
        //                            }
        //                        }

        //                        if (ColName == 'City') {
        //                            City = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (City == undefined || City == '&nbsp;') {
        //                                City = '';
        //                            }
        //                        }

        //                        if (ColName == 'Value') {
        //                            Value = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (Value == undefined || Value == '&nbsp;') {
        //                                Value = '';
        //                            }
        //                        }

        //                        if (ColName == '# Of Matching Doc') {
        //                            MatchingDocs = grd.rows[RowIndex].cells[i - 1].innerHTML;
        //                            if (MatchingDocs == undefined || MatchingDocs == '&nbsp;') {
        //                                MatchingDocs = '';
        //                            }
        //                        }

        //                    }
        //                }
        //            }


        //            if (FIREFOX) {

        //                if (ProjectId != undefined && ProjectId != '&nbsp;') {
        //                    $get('txtProjectID').textContent = Title;
        //                }
        //                else {
        //                    $get('txtProjectID').textContent = '';
        //                }

        //                if (Title != undefined && Title != '&nbsp;') {
        //                    $get('txtTitle').textContent = Title;
        //                }
        //                else {
        //                    $get('txtTitle').textContent = '';
        //                }

        //                if (Stage != undefined && Stage != '&nbsp;') {
        //                    $get('txtStage').textContent = Stage;
        //                }
        //                else {
        //                    $get('txtStage').textContent = '';
        //                }

        //                if (Status != undefined && Status != '&nbsp;') {
        //                    if (Status == "ACTIVE") {
        //                        $('#rdoActive').attr('checked', true);
        //                        $('#rdoInActive').attr('checked', false);
        //                    }
        //                    else if (Status == "INACTIVE") {
        //                        $('#rdoActive').attr('checked', false);
        //                        $('#rdoInActive').attr('checked', true);
        //                    }

        //                }

        //                if (DoNotCall == "1") {

        //                    $('#chkDonotCall').attr('checked', true);
        //                }
        //                else {
        //                    $('#chkDonotCall').attr('checked', false);
        //                }

        //                if (Managed == "1") {

        //                    $('#chkKAMManaged').attr('checked', true);
        //                }
        //                else {
        //                    $('#chkKAMManaged').attr('checked', false);
        //                }

        //                if (Country != undefined && Country != '&nbsp;') {
        //                    $get('txtCountry').textContent = Country;
        //                }
        //                else {
        //                    $get('txtCountry').textContent = '';
        //                }

        //                if (City != undefined && City != '&nbsp;') {
        //                    $get('txtCity').textContent = City;
        //                }
        //                else {
        //                    $get('txtCity').textContent = '';
        //                }

        //                if (State != undefined && State != '&nbsp;') {
        //                    $get('TxtStateProv').textContent = State;
        //                }
        //                else {
        //                    $get('TxtStateProv').textContent = '';
        //                }

        //                if (Value != undefined && Value != '&nbsp;') {
        //                    $get('txtValue').textContent = Value;
        //                }
        //                else {
        //                    $get('txtValue').textContent = '';
        //                }

        //                if (BidDate != undefined && BidDate != '&nbsp;') {
        //                    $get('txtBidDate').textContent = BidDate;
        //                }
        //                else {
        //                    $get('txtBidDate').textContent = '';
        //                }

        //                if (MatchingDocs != undefined && MatchingDocs != '&nbsp;') {
        //                    $get('txtMatchingDoc').textContent = MatchingDocs;
        //                }
        //                else {
        //                    $get('txtMatchingDoc').textContent = '';
        //                }
        //            }
        //            else {

        //                if (ProjectId != undefined && ProjectId != '&nbsp;') {
        //                    $get('txtProjectID').innerText = Title;
        //                }
        //                else {
        //                    $get('txtProjectID').innerText = '';
        //                }

        //                if (Title != undefined && Title != '&nbsp;') {
        //                    $get('txtTitle').innerText = Title;
        //                }
        //                else {
        //                    $get('txtTitle').innerText = '';
        //                }

        //                if (Stage != undefined && Stage != '&nbsp;') {
        //                    $get('txtStage').innerText = Stage;
        //                }
        //                else {
        //                    $get('txtStage').innerText = '';
        //                }

        //                if (Status != undefined && Status != '&nbsp;') {
        //                    if (Status == "ACTIVE") {
        //                        $('#rdoActive').attr('checked', true);
        //                        $('#rdoInActive').attr('checked', false);
        //                    }
        //                    else if (Status == "INACTIVE") {
        //                        $('#rdoActive').attr('checked', false);
        //                        $('#rdoInActive').attr('checked', true);
        //                    }

        //                }

        //                if (DoNotCall == "1") {

        //                    $('#chkDonotCall').attr('checked', true);
        //                }
        //                else {
        //                    $('#chkDonotCall').attr('checked', false);
        //                }

        //                if (Managed == "1") {

        //                    $('#chkKAMManaged').attr('checked', true);
        //                }
        //                else {
        //                    $('#chkKAMManaged').attr('checked', false);
        //                }

        //                if (Country != undefined && Country != '&nbsp;') {
        //                    $get('txtCountry').innerText = Country;
        //                }
        //                else {
        //                    $get('txtCountry').innerText = '';
        //                }

        //                if (City != undefined && City != '&nbsp;') {
        //                    $get('txtCity').innerText = City;
        //                }
        //                else {
        //                    $get('txtCity').innerText = '';
        //                }

        //                if (State != undefined && State != '&nbsp;') {
        //                    $get('TxtStateProv').innerText = State;
        //                }
        //                else {
        //                    $get('TxtStateProv').innerText = '';
        //                }

        //                if (Value != undefined && Value != '&nbsp;') {
        //                    $get('txtValue').innerText = Value;
        //                }
        //                else {
        //                    $get('txtValue').innerText = '';
        //                }

        //                if (BidDate != undefined && BidDate != '&nbsp;') {
        //                    $get('txtBidDate').innerText = BidDate;
        //                }
        //                else {
        //                    $get('txtBidDate').innerText = '';
        //                }

        //                if (MatchingDocs != undefined && MatchingDocs != '&nbsp;') {
        //                    $get('txtMatchingDoc').innerText = MatchingDocs;
        //                }
        //                else {
        //                    $get('txtMatchingDoc').innerText = '';
        //                }
        //            }

        //            //var myDate = new Date(CheckString(Data.rows[0]['Bid Date']));
        //            // alert(myDate);

        //           // jQuery("#txtTitle").val(CheckString(Data.rows[0]['Title']));
        //            //jQuery("#txtStage").val(CheckString(Data.rows[0]['stage']));


        ////            if (CheckString(Data.rows[0]['Status']) == "Active") {

        ////                jQuery("#rdoActive").attr('checked', true);
        ////            }
        ////            else {
        ////                jQuery("#rdoActive").attr('checked', false);
        ////            }

        ////            if (CheckString(Data.rows[0]['Status']) == "Inactive") {

        ////                jQuery("#rdoInActive").attr('checked', true);
        ////            }
        ////            else {
        ////                jQuery("#rdoInActive").attr('checked', false);
        ////            }


        ////            if (CheckString(Data.rows[0]['DoNotCall']) == "1") {

        ////                jQuery("#chkDonotCall").attr('checked', true);
        ////            }
        ////            else {
        ////                jQuery("#chkDonotCall").attr('checked', false);
        ////            }

        ////            if (CheckString(Data.rows[0]['Managed']) == "1") {

        ////                jQuery("#chkKAMManaged").attr('checked', true);
        ////            }
        ////            else {
        ////                jQuery("#chkKAMManaged").attr('checked', false);
        ////            }

        ////            jQuery("#txtCountry").val(CheckString(Data.rows[0]['Country']));
        ////            jQuery("#txtCity").val(CheckString(Data.rows[0]['City']));
        ////            jQuery("#TxtStateProv").val(CheckString(Data.rows[0]['State/Province']));
        ////            jQuery("#txtValue").val(CheckString(Data.rows[0]['Value']));
        ////            jQuery("#txtBidDate").val((myDate.getMonth() + 1) + "/" + myDate.getDate() + "/" + myDate.getFullYear());


        ////            jQuery("#txtMatchingDoc").val(CheckString(Data.rows[0]['# of Matching Docs']));

        //        }



        //        function OnSuccessGeneralCon(Data) {
        //            var FIREFOX = /Firefox/i.test(navigator.userAgent);
        //            var myDate = new Date(CheckString(Data.rows[0]['Valid_From']));
        //            // alert(myDate);

        //            jQuery("#txtName").val(CheckString(Data.rows[0]['Name']));
        //            jQuery("#txtPhoneNum").val(CheckString(Data.rows[0]['Phone']));
        //            jQuery("#txtEmail").val(CheckString(Data.rows[0]['Email']));
        //            jQuery("#txtAccountNum").val(CheckString(Data.rows[0]['Account']));

        //            if (Data.rows[0]['Customer'] == '0') {
        //                jQuery("#cmbCustomer").val('Yes');
        //            }
        //            else if (Data.rows[0]['Customer'] == '1') {
        //                jQuery("#cmbCustomer").val('No');
        //            }
        //            else {
        //                jQuery("#cmbCustomer").val('');
        //            }

        //            if (Data.rows[0]['KAM'] == '0') {
        //                jQuery("#cmbKAM").val('Yes');
        //            }
        //            else if (Data.rows[0]['KAM'] == '1') {
        //                jQuery("#cmbKAM").val('No');
        //            }
        //            else {
        //                jQuery("#cmbKAM").val('');
        //            }



        //            if (Data.rows[0]['calledNotes'] == '1') {
        //                jQuery("#cmbCalled").val('Yes');
        //            }
        //            else if (Data.rows[0]['KAM'] == '2') {
        //                jQuery("#cmbCalled").val('No');
        //            }
        //            else {
        //                jQuery("#cmbCalled").val('');
        //            }


        //            $get('txtlastupdatedby').textContent = CheckString(Data.rows[0]['Username']).toString();
        //            $get('txtLastUpdatedDate').textContent = (myDate.getMonth() + 1) + "/" +
        //                                                                    myDate.getDate() + "/" +
        //                                                                    myDate.getFullYear();

        //        }




        function CheckString(val) {
            // alert(val);
            if (val)
                return val;
            else
                return "";
        }



        //        $('#tabMasterConst').bind('tabsselect', function (event, ui) {
        //            alert("OK");
        //        });
        //        $(function () {
        //            $("#tabMasterConst").tabs();
        //            $("#tabMasterConst").bind('tabsselect', function (e, tab) {
        //                alert("The tab at index " + tab.index + " was selected");
        //            });
        //        });

        //>>>>>>------------Page Load-----------------------<<<<<<<<



        function pageLoad() {
            //document.getElementById('hdnProjectID2').value = '';
            var RowNum = document.getElementById('hdnTerID').value;
            var hndProjId = document.getElementById('hdnProjectID').value;
            var varProjectID = document.getElementById('hdnProjectID2').value;
            var varSubConID = document.getElementById('hdnSubConID').value;
            var totalSubConcols = 0;
            varcallnotes = $("#cmbCalled").val();
            //alert("PageLoad: " + varcallnotes);
            $(document).ready(function (e) {

                //                $('#' + '<%=gwReedTerritories.ClientID%>').tablesorter({
                //                    widgets: ['zebra'],
                //                    widgetZebra: { css: ["NormalRowStyle", "AltRowStyle"]} // css classes to apply to rows
                //                });

                $addHandler(document, 'keydown', onKeypress);

                $("#gwReedTerritories  tr:has(td)").hover(function () {

                    $(this).css("cursor", "pointer");
                });


                $("#btnGenUpdate").click(function () {
                    //alert("thisLink was clicked");
                    //$("#btnGenSave").attr("enabled", "enabled");
                    //$("input[type=submit][id=btnGenSave]").attr('enable',true);
                    $("input[type=submit][id=btnGenSave]").attr('disabled', false);
                    $("input[type=submit][id=btnGenUpdate]").attr('disabled', true);
                    $("input[type=submit][id=btnGenCancel]").attr('disabled', false);
                    jQuery("#txtName").attr('disabled', false);
                    jQuery("#txtPhoneNum").attr('disabled', false);
                    jQuery("#txtEmail").attr('disabled', false);
                    jQuery("#txtAccountNum").attr('disabled', false);
                    jQuery("#cmbCustomer").attr('disabled', false);
                    jQuery("#cmbCalled").attr('disabled', false);
                    jQuery("#cmbKAM").attr('disabled', false);
                    // $('#cmbKAM').attr('disabled', false);
                    //ev.stopPropagation();
                });


                $("#btnDelete").click(function () {

                    // var varSubConID = document.getElementById('hdnSubConID').value;
                    var conf = confirm("Are you sure you want to delete?");

                    if (conf == true) {
                        //   PageMethods.DeleteSubCon(varSubConID, onDeleteSuccess);
                        return true;
                    }
                    else {
                        return false;
                    }
                    //   alert("Delete");

                });


                $("#btnGenSave").click(function () {
                    var conf = confirm("Are you sure you want to update?");


                    if (conf == true) {

                        return true;

                    }
                    else {
                        return false;
                    }
                });



                $("#btnCallSave").click(function () {
                    var conf = confirm("Are you sure you want to update?");


                    if (conf == true) {

                        return true;

                    }
                    else {
                        return false;
                    }
                });



                $("#btnSubConSave").click(function () {
                    var conf = confirm("Are you sure you want to update?");


                    if (conf == true) {

                        return true;

                    }
                    else {
                        return false;
                    }
                });



                //                $("#btnGenCancel").click(function () {
                //                    $("input[type=submit][id=btnGenSave]").attr('disabled', true);
                //                    $("input[type=submit][id=btnGenUpdate]").attr('disabled', false);
                //                    $("input[type=submit][id=btnGenCancel]").attr('disabled', true);
                //                    jQuery("#txtName").attr('disabled', true);
                //                    jQuery("#txtPhoneNum").attr('disabled', true);
                //                    jQuery("#txtEmail").attr('disabled', true);
                //                    jQuery("#txtAccountNum").attr('disabled', true);
                //                    jQuery("#cmbCustomer").attr('disabled', true);
                //                    jQuery("#cmbCalled").attr('disabled', true);
                //                    jQuery("#cmbKAM").attr('disabled', true);
                //                });



                $("#btnSubUpdate").click(function () {

                    $("input[type=submit][id=btnSubConSave]").attr('disabled', false);
                    $("input[type=submit][id=btnSubConCancel]").attr('disabled', false);
                    $("input[type=submit][id=btnSubUpdate]").attr('disabled', true);

                    jQuery("#txtCompanyName").attr('disabled', false);
                    jQuery("#txtTitle2").attr('disabled', false);
                    jQuery("#txtName2").attr('disabled', false);
                    jQuery("#txtPhone2").attr('disabled', false);
                    jQuery("#txtEmail2").attr('disabled', false);
                    jQuery("#txtNotes2").attr('disabled', false);
                    jQuery("#dlCustomerYN").attr('disabled', false);
                    jQuery("#dlKANYN").attr('disabled', false);
                    jQuery("#txtAccountNum2").attr('disabled', false);
                    return false;
                });



                $("#btnCallUpdate").click(function () {

                    $("input[type=submit][id=btnCallSave]").attr('disabled', false);
                    $("input[type=submit][id=btnCallCancel]").attr('disabled', false);
                    $("input[type=submit][id=btnCallUpdate]").attr('disabled', true);

                });

                $("#btnSubConCancel").click(function () {

                    // alert("CancelClick");

                    // $("input[type=submit][id=btnSubConSave]").attr('disabled', false);
                    // $("input[type=submit][id=btnSubConCancel]").attr('disabled', false);
                    $("input[type=submit][id=btnSubUpdate]").attr('disabled', false);

                    // alert("Done");
                    //return false;
                });



                $("#<%=gwSubCon.ClientID%>  tr:has(td)").hover(function () {
                    $(this).css("cursor", "pointer");
                });

                if (hndProjId != "0") {
                    try {
                        var index = parseInt(hndProjId) + parseInt(1)
                        $("#gwReedTerritories tr")[index].className = 'SelectedRowStyle';
                        //                        ClearCallNotes();
                        //ClearSubCon();
                        //                        var cookieval = getCookie("CProjID");
                        //                        alert(cookieval);
                        //PageMethods.GetDatafromXMLProjectInfo(varProjectID, OnSuccess2);



                        //PageMethods.GetDataGeneralCon(varProjectID, OnSuccessGeneralCon);
                        //alert(varProjectID);
                        //PageMethods.GetDatafromXMLCallNotes(varProjectID, OnSuccessCallNotes);
                        document.getElementById('hdnProjectID').value = "0";
                    }
                    catch (e) {
                        lert('An error has occurred: ' + e.message)
                    }
                }


                //                if (document.getElementById('hdnSubConGWCount').value != null) {
                //                    try {
                //                        totalSubConcols = document.getElementById('hdnSubConGWCount').value;
                //                    }
                //                    catch (e) {
                //                        //lert('An error has occurred: ' + e.message)
                //                    }
                //                }



                //                if (totalSubConcols > 1) {

                //                    try {
                //                        PageMethods.GetDatafromXMLSubCon(varSubConID, OnSuccess);
                //                        $("#gwSubCon tr")[varSubConID].className = 'SelectedRowStyle';
                //                    }
                //                    catch (e) {
                //                        lert('An error has occurred: ' + e.message)
                //                    }
                //                }

                $("#btnAddCon").click(function () {
                    var mydiv = $('#SubPanel');

                    mydiv.dialog({ autoOpen: false,
                        title: "Add Sub Contractor",
                        resizable: false,
                        width: 290,
                        modal: false,
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        open: function (type, data) {
                        },
                        buttons: {
                            "Ok": function () {

                                var bValid = true;
                                var varCompanyName = $("#txtSubCompanyName");
                                var varTitle = $("#txtSubTitle");
                                var varName = $("#txtSubName");
                                allFields = $([]).add(varCompanyName).add(varTitle).add(varName);

                                allFields.removeClass("ui-state-error");

                                bValid = bValid && checkLength(varCompanyName, "Company Name", 3, 25);
                                bValid = bValid && checkLength(varTitle, "Company Name", 3, 25);
                                bValid = bValid && checkLength(varName, "Company Name", 3, 25);

                                if (bValid) {

                                    var varCompanyName = $get('txtSubCompanyName').value;
                                    var varTitle = $get('txtSubTitle').value;
                                    var varName = $get('txtSubName').value;


                                    var conf = confirm("Are you sure you want to add?");

                                    //var updateStat = new Boolean(0);
                                    var varSubCompanyName = $("#txtSubCompanyName").val();
                                    var varSubTitle = $("#txtSubTitle").val();
                                    var varSubName = $("#txtSubName").val();
                                    var varSubPhone = $("#txtSubPhone").val();
                                    var varSubEmail = $("#txtSubEmail").val();
                                    var varSubCustomer;
                                    var varSubKAM;
                                    var varSubAccount = $("#txtSubAccountNum").val();
                                    var varSubNotes = $("#txtSubNotes").val();
                                    var varID = document.getElementById('hdnSubConID').value;


                                    if ($("#dlSubCustomer").val() == 'Yes') {
                                        varSubCustomer = '0';
                                    }
                                    else if ($("#dlSubCustomer").val() == 'No') {
                                        varSubCustomer = '1';
                                    }
                                    else {
                                        varSubCustomer = '2';
                                    }
                                    if ($("#dlSubKAM").val() == 'Yes') {
                                        varSubKAM = '0';
                                    }
                                    else if ($("#dlSubKAM").val() == 'No') {
                                        varSubKAM = '1';
                                    }
                                    else {
                                        varSubKAM = '2';
                                    }


                                    if (conf == true) {

                                        //PageMethods.AddSubCon(varSubCompanyName, onUpdateSuccess);

                                        PageMethods.AddSubCon(varSubCompanyName, varSubTitle, varSubName, varSubPhone, varSubEmail, varSubCustomer, varSubKAM, varSubAccount, varSubNotes, varID, onAddSuccess);

                                        // PageMethods.updateGenCon(varContractorName, varContractorEmail, varContractorPhone, varContractorCustomer, varContractorAccount, varKAM, varCalled, onUpdateSuccess);

                                    }






                                    // alert("Successsfully Saved");
                                    $(this).dialog("close");
                                    // __doPostBack('', '');
                                }

                            },
                            "Cancel": function () {
                                mydiv.dialog('close');
                            }
                        }
                    });

                    mydiv.dialog('open');

                    return false;
                });



                $("#gwReedTerritories tr").not($("#gwReedTerritories tr").eq(0)).click(function () {

                    if ($(this).closest("tr")[0].rowIndex > 0 && $(this).closest("tr")[0].rowIndex <= 200) {

                        document.getElementById('hdnSubConID').value = 1;
                        document.getElementById('hdnProjectID').value = $(this).index();

                        $("#gwReedTerritories  tr").closest('TR').removeClass('SelectedRowStyle');
                        $(this).addClass('SelectedRowStyle');

                        var totalReedcols = $("#gwReedTerritories").find('tr')[0].cells.length;
                        var totcol = 0;
                        for (var i = 0; i < totalReedcols + 1; i++) {
                            var ColName = $('#gwReedTerritories tr').find('th:nth-child(' + i + ')').text();
                            //alert(ColName);
                            if (ColName == 'Row') {

                                RowNum = $(this).find("td:eq(" + (i - 1) + ")").html();
                                document.getElementById('hdnTerID').value = RowNum;

                            }
                            if (ColName == 'Project ID') {

                                varProjectID = $(this).find("td:eq(" + (i - 1) + ")").html();
                                document.getElementById('hdnProjectID2').value = $(this).find("td:eq(" + (i - 1) + ")").html();

                                setCookie('CProjID', document.getElementById('hdnProjectID2').value, 1);
                                totcol += 1;
                            }

                            if (totcol != 0 && i == totalReedcols) {
                                //alert(varProjectID);

                                setCookie('CProjID', varProjectID, 1);
                                //PageMethods.GetDataGeneralCon(varProjectID);
                                //                            PageMethods.GetDataGeneralCon(varProjectID, OnSuccessGeneralCon);
                                //PageMethods.GetDatafromXMLProjectInfo(varProjectID, OnSuccess2);
                                //alert(varProjectID);
                                //PageMethods.GetDatafromXMLCallNotes(varProjectID, OnSuccessCallNotes);

                                break;

                            }
                            // PageMethods.GetDatafromXMLProjectInfo(varProjectID, OnSuccess2);


                        }

                        // OnSuccess2($(this).index());

                        __doPostBack('', '');
                        return true;

                    }
                });

                $("#gwSubCon tr").not($("#gwSubCon tr").eq(0)).click(function () {
                    $("#gwSubCon  tr").closest('TR').removeClass('SelectedRowStyle');
                    $(this).addClass('SelectedRowStyle');
                    // document.getElementById('hdnSubConID').value = $(this).index();

                    var totalSubConcols = $("#gwSubCon").find('tr')[0].cells.length;

                    for (var i = 0; i < totalSubConcols + 1; i++) {
                        var SubColName = $('#gwSubCon tr').find('th:nth-child(' + i + ')').text();

                        if (SubColName == 'Row') {
                            SubRowNum = $(this).find("td:eq(" + (i - 1) + ")").html();
                            // PageMethods.GetDatafromXMLSubCon(SubRowNum, OnSuccess);
                        }


                        if (SubColName == 'UID') {
                            //alert($(this).find("td:eq(" + (i - 1) + ")").html());
                            document.getElementById('hdnSubConID').value = $(this).find("td:eq(" + (i - 1) + ")").html();
                        }

                    }
                    OnSuccess($(this).index());
                });


                //>>>>>>------------txtFilter Event-----------------------<<<<<<<<
                $('#txtFilter').keyup(function (e) {

                    $("#gwReedTerritories tr:has(td)").hide(); // Hide all the rows.
                    index = 1;
                    var sSearchTerm = $('#txtFilter').val(); //Get the search box value

                    if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                    {
                        //alert(sSearchTerm.length);
                        $("#gwReedTerritories tr:has(td)").show();

                        $("#gwReedTerritories tr").children("td:nth-child(1)").each(function () {

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
                    //>>>>>>------------txtFilter Load-----------------------<<<<<<<<
                    if ($get('rdoFilterTitle').checked == true) {
                        //Iterate through all the td.

                        $("#gwReedTerritories tr").children("td:nth-child(2)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
                                //alert("tRUE");
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

                    if ($get('rdoFilterGenCont').checked == true) {
                        //Iterate through all the td.
                        //alert("GC");
                        $("#gwReedTerritories tr").children("td:nth-child(5)").each(function () {
                            var cellText = $(this).text().toLowerCase();
                            if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                            {
                                //alert("tRUE");
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

            });

        }
        //>>>>>>------------End Page Load-----------------------<<<<<<<<

    </script>
    <div id="container">
        <script type='text/javascript' src="../../App_Themes/JS/accounting.js"></script>
        <asp:Label ID="lblProjectInfo" CssClass="GridHeaderLabel" Text="Project Information"
            runat="server"></asp:Label>
        <asp:UpdatePanel ID="updatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="object-container" height="100%"
                    width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lbltitle" Text="Title:" runat="server" CssClass="LabelFont" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                Enabled="false" TextMode="MultiLine" AutoPostBack="True" Style="width: 200px;
                                height: 40px;"></asp:TextBox>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="Label1" Text="Stage:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:TextBox ID="txtStage" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            Enabled="false" AutoPostBack="True" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="Label2" Text="Project Status:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton ID="rdoActive" runat="server" Text="Active" GroupName="RdoExportFile"
                                                            CssClass="LabelFont" ClientIDMode="Static" />
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="rdoInActive" runat="server" Text="InActive" GroupName="RdoExportFile"
                                                            CssClass="LabelFont" ClientIDMode="Static" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<asp:Label ID="Label3" Text="Project Status:" runat="server" CssClass="LabelFont" />--%>
                                    </td>
                                    <td>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="white-space: nowrap">
                                                        <%-- <asp:RadioButton ID="RadioButton1" runat="server" Text="Active" GroupName="RdoExportFile"
                                                    CssClass="CssLabel" Checked="true" />--%>
                                                        <asp:CheckBox ID="chkDonotCall" runat="server" ClientIDMode="Static" Text="Do not call"
                                                            CssClass="LabelFont" Enabled="false" />
                                                    </td>
                                                    <td style="white-space: nowrap">
                                                        <asp:CheckBox ID="chkKAMManaged" runat="server" ClientIDMode="Static" Text="KAM Managed"
                                                            CssClass="LabelFont" Enabled="false" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblCountry" Text="Country:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="True" Style="width: 100px;" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblCity" Text="City:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="True" Style="width: 100px;" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" Text="X" runat="server" CssClass="LabelFont" Style="color: #dddbdc" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblStateProv" Text="State/Province:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtStateProv" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="True" Style="width: 100px;" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblValue" Text="Value:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtValue" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="True" Style="width: 100px;" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" Text="X" runat="server" CssClass="LabelFont" Style="color: #dddbdc" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblBiddate" Text="Bid Date:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBidDate" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="True" Style="width: 100px;" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblMatchingDoc" Text="# of Matching Docs:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMatchingDoc" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="True" Style="width: 100px;" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUpdateStat" value="Update Project Status" type="submit" runat="server"
                                            Text="Update Status" ToolTip="Update Project Status" CssClass="Const-UpdateButton"
                                            ClientIDMode="Static" Style="color: White" Width="112px" OnClientClick="if (!confirm('Are you sure you want to update?')) return false;"
                                            OnClick="btnUpdateStat_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lblContInfo" CssClass="GridHeaderLabel" Text="General Contractor Information"
            runat="server"></asp:Label>'
        <asp:UpdatePanel ID="updatePanel6" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="object-container">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblName" Text="Name:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td style="width: 123px">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblEmail" Text="Email:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td style="width: 90px">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblCustomer" Text="Customer Y/N:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td style="width: 123px">
                                        <asp:DropDownList ID="cmbCustomer" runat="server" Height="20px" ClientIDMode="Static">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 125px">
                                        <asp:Label ID="lblCalles" Text="Called:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td style="width: 123px">
                                        <asp:DropDownList ID="cmbCalled" runat="server" Height="20px" ClientIDMode="Static"
                                            AutoPostBack="false">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblLastUpdateby" Text="Last Updated by:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td style="white-space: nowrap">
                                        <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                        AutoPostBack="True" Style="width: 150px;"></asp:TextBox>--%>
                                        <asp:Label ID="txtlastupdatedby" Text="" runat="server" CssClass="LabelFont" ClientIDMode="Static" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblPhoneNum" Text="Phone #:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td style="width: 123px">
                                        <asp:TextBox ID="txtPhoneNum" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblAccntNum" Text="Account Number:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAccountNum" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblKAM" Text="KAM Y/N:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td style="width: 123px">
                                        <asp:DropDownList ID="cmbKAM" runat="server" Height="20px" ClientIDMode="Static">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="Label3" Text="Last Updated Date:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLastUpdatedDate" Text="" runat="server" CssClass="LabelFont" ClientIDMode="Static" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<asp:Button ID="btnReedData" value="Reed Data" type="submit" runat="server" Text="Reed Data"
                                    ToolTip="Reed Data" CssClass="Const-UpdateButton" Style="color: White" Width="80px" />--%>
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
                                    <td align="right">
                                        <asp:Button ID="btnGenUpdate" value="Save" type="submit" runat="server" Text="Update"
                                            ToolTip="Update" CssClass="Const-UpdateButton" Style="color: White" Width="80px"
                                            OnClick="btnGenUpdate_Click" ClientIDMode="Static" OnClientClick="return false;" />
                                    </td>
                                    <td>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="btnGenSave" value="Save" type="submit" runat="server" Text="Save"
                                                            ToolTip="Save" CssClass="Const-UpdateButton" Style="color: White" ClientIDMode="Static"
                                                            Width="80px" OnClick="btnGenSave_Click" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnGenCancel" value="Save" type="submit" runat="server" Text="Cancel"
                                                            ToolTip="Cancel" CssClass="Const-UpdateButton" Style="color: White" Width="80px"
                                                            ClientIDMode="Static" OnClientClick="return false;" OnClick="btnGenCancel_Click" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updatePanel5" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
                    <tr>
                        <td>
                            <asp:TabContainer ID="tabMasterConst" runat="server" ActiveTabIndex="0" ClientIDMode="Static">
                                <asp:TabPanel ID="tabSubCons" runat="server" Font-Size="12px" Font-Names="Arial"
                                    HeaderText="Sub Contractor Info.">
                                    <ContentTemplate>
                                        <div style="height: 220px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddCon" value="Add Sub" type="submit" runat="server" Text="Add Sub Contractor"
                                                            ToolTip="Add Sub Contractor" CssClass="Const-UpdateButton" Style="color: White"
                                                            Width="130px" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDelete" value="Delete" type="submit" runat="server" Text="Delete"
                                                            OnClick="btnDelete_Click" ToolTip="Delete" CssClass="Const-UpdateButton" Style="color: White;"
                                                            Width="80px" Enabled="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div>
                                                <table border="0" cellpadding="10" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="left">
                                                            <div>
                                                                <table class="object-container">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCompanyName" Text="Company Name:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCustomerYN" Text="Customer Y/N:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="dlCustomerYN" runat="server" Height="20px" ClientIDMode="Static">
                                                                                <asp:ListItem Text="" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblTitle2" Text="Title:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtTitle2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblKAMYN" Text="KAM Y/N:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="dlKANYN" runat="server" Height="20px" ClientIDMode="Static">
                                                                                <asp:ListItem Text="" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblName2" Text="Name:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtName2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblAccntNum2" Text="Account #:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAccountNum2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblPhone" Text="Phone:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPhone2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                                                        </td>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:Label ID="Label10" Text="Last Updated by:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:Label ID="lblLastUpdatedBy2" runat="server" CssClass="LabelFont" ClientIDMode="Static" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblEmail2" Text="Email:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtEmail2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label11" Text="Last Updated Date:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblLastUpdatedDate2" runat="server" CssClass="LabelFont" ClientIDMode="Static" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblNotes" Text="Notes:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNotes2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                TextMode="MultiLine" AutoPostBack="false" Style="width: 200px; height: 40px;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Button ID="btnSubUpdate" value="Save" type="submit" runat="server" Text="Update"
                                                                                ToolTip="Update" CssClass="Const-UpdateButton" Style="color: White" Width="80px"
                                                                                Enabled="false" OnClick="btnSubUpdate_Click" OnClientClick="return false;" ClientIDMode="Static" />
                                                                        </td>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="btnSubConSave" value="Save" type="submit" runat="server" Text="Save"
                                                                                            ToolTip="Save" CssClass="Const-UpdateButton" Style="color: White" Width="80px"
                                                                                            ClientIDMode="Static" OnClick="btnSubConSave_Click" Enabled="false" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btnSubConCancel" value="Save" type="submit" runat="server" Text="Cancel"
                                                                                            ToolTip="Cancel" CssClass="Const-UpdateButton" Style="color: White" Width="80px"
                                                                                            OnClientClick="return false;" ClientIDMode="Static" Enabled="false" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td align="left">
                                                            <asp:UpdatePanel ID="updatePanelSubCon" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="table-wrapper page1">
                                                                        <asp:Panel ID="Panel1" ScrollBars="Horizontal" Width="600px" Height="180px" runat="server">
                                                                            <table border="0" cellpadding="10" cellspacing="0" width="100%" clientidmode="Static">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:GridView ID="gwSubCon" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                                            CellPadding="4" CellSpacing="1" Font-Size="12px" ForeColor="Black" BackColor="White"
                                                                                            GridLines="None" EmptyDataText="No data available." AllowPaging="false" ClientIDMode="Static"
                                                                                            OnRowCreated="gwSubCon_RowCreated" OnSelectedIndexChanged="gwSubCon_SelectedIndexChanged">
                                                                                            <AlternatingRowStyle BackColor="#E5E5E5" />
                                                                                            <EditRowStyle CssClass="EditRowStyle" />
                                                                                            <HeaderStyle BackColor="#D6E0EC" Wrap="False" Height="30px" CssClass="HeaderStyle" />
                                                                                            <RowStyle CssClass="RowStyle" Wrap="False" />
                                                                                            <PagerStyle CssClass="grid_paging" />
                                                                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Company Name" HeaderText="Company Name">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Title" HeaderText="Title">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="SubName" HeaderText="Name">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="SubPhone" HeaderText="Phone">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="SubEmail" HeaderText="Email">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Account" HeaderText="Account #">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Notes" HeaderText="Notes">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Username" HeaderText="Username">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="valid_from" HeaderText="Date Created" DataFormatString="{0:MM/dd/yyyy}">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="KAM" HeaderText="KAM">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Customer" HeaderText="Customer">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="ID" HeaderText="UID">
                                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="gwSubCon" EventName="SelectedIndexChanged" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="tabCallInfo" runat="server" Font-Size="12px" Font-Names="Arial"
                                    HeaderText="Call Info." Width="900px">
                                    <ContentTemplate>
                                        <div style="height: 220px; width: 100%">
                                            <asp:UpdatePanel ID="updatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table clientidmode="Static">
                                                        <tr>
                                                            <td>
                                                                <table class="object-container" clientidmode="Static">
                                                                    <tr>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:Label ID="lblCallNotes" Text="Call Notes:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:TextBox ID="txtCallNotes" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                TextMode="MultiLine" AutoPostBack="false" Style="width: 200px; height: 40px;"></asp:TextBox>
                                                                        </td>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:Label ID="lblfollowupNotes" Text="Next Followup Notes:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:TextBox ID="txtfollowupNotes" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                TextMode="MultiLine" AutoPostBack="false" Style="width: 200px; height: 40px;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label5" Text="Next Followup Contact:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:TextBox ID="txtNextFollowupContact" runat="server" CssClass="textbox curved"
                                                                                ClientIDMode="Static" TextMode="MultiLine" AutoPostBack="false" Style="width: 200px;
                                                                                height: 40px;"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblNextFollowupDate" Text="Next Followup Date:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:TextBox ID="txtFollowupDate" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                TextMode="MultiLine" AutoPostBack="false" Style="width: 200px; height: 40px;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblNumCall" Text="# of Call:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td style="white-space: nowrap">
                                                                            <asp:TextBox ID="txtNumCall" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                                AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtNumCall"
                                                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblLastUpdateby3" Text="Last Updated by:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="txtlastUpdatedBy3" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblLastUpdatedate3" Text="Last Updated Date:" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="txtLastUpdatedDate3" runat="server" CssClass="LabelFont" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Button ID="btnCallUpdate" value="Save" type="submit" runat="server" Text="Update"
                                                                                OnClientClick="return false;" ToolTip="Update" CssClass="Const-UpdateButton"
                                                                                Style="color: White" Width="80px" ClientIDMode="Static" />
                                                                        </td>
                                                                        <td>
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <asp:Button ID="btnCallSave" value="Save" type="submit" runat="server" Text="Save"
                                                                                                ToolTip="Save" CssClass="Const-UpdateButton" Style="color: White" Width="80px"
                                                                                                ClientIDMode="Static" OnClick="btnCallSave_Click" />
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:Button ID="btnCallCancel" value="Save" type="submit" runat="server" Text="Cancel"
                                                                                                OnClientClick="return false;" ToolTip="Cancel" CssClass="Const-UpdateButton"
                                                                                                Style="color: White" Width="80px" ClientIDMode="Static" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <%--<table>
            <tr style="width: auto">
                <td style="background-color: #d6e0ec;">
                    <asp:Label ID="Label7" Font-Bold="True" CssClass="LabelFont" runat="server" Text="Reed Territories"></asp:Label>
                </td>
            </tr>
        </table>--%>
        <asp:Label ID="Label6" CssClass="GridHeaderLabel" Text="REED TERRITORIES" runat="server"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%">
                    <tr>
                        <td style="white-space: nowrap; width: 226px;">
                            <table style="border: 1px solid black; height: 92px; padding: 5px;">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="GridHeaderLabel">
                                                        FILTER BY
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdoFilterTitle" ClientIDMode="Static" runat="server" Text=" Title"
                                                        GroupName="RdoFilter" Checked="True" AutoPostBack="True" CssClass="LabelFont" />
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rdoFilterGenCont" ClientIDMode="Static" runat="server" Text=" General Contractor"
                                                        GroupName="RdoFilter" Checked="false" AutoPostBack="True" CssClass="LabelFont" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<asp:TextBox ID="TextBox5" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                    AutoPostBack="True" Style="width: inherit;"></asp:TextBox>--%>
                                        <div id="searchboxdivConst" class="SearchBox" style="padding-left: 5px; padding-top: 5px;">
                                            <%--     <div id="SearchBox">--%>
                                            <asp:Label CssClass="SearchBox lbl" ID="lblValueCons" runat="server" ClientIDMode="Static"
                                                Text="Value " Font-Names="Arial" Font-Size="12px"></asp:Label>
                                            <%--  </div>--%>
                                            <asp:TextBox ID="txtFilter" runat="server" CssClass="txtSearchQuery" ClientIDMode="Static"
                                                AutoPostBack="True" Width="80px"></asp:TextBox>
                                            <asp:ImageButton ID="btnSearch" ClientIDMode="Static" CssClass="btnSearchConst" ImageUrl="~/App_Themes/Images/New Design/magnifying-glass-search-icon.jpg"
                                                runat="server" ToolTip="Search Account" OnClick="btnSearch_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <table style="border: 1px solid black; height: 92px; padding: 5px;">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="GridHeaderLabel">
                                                        STATUS
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="object-wrapper" style="padding-right: 0; white-space: nowrap;">
                                                    <asp:RadioButton ID="rbActive" ClientIDMode="Static" runat="server" Text=" Active"
                                                        GroupName="RdoStatus" Checked="True" AutoPostBack="True" OnCheckedChanged="GetXMLReed"
                                                        CssClass="LabelFont" />
                                                </td>
                                                <td class="object-wrapper" style="padding-right: 0; white-space: nowrap;">
                                                    <asp:RadioButton ID="rbActiveInActive" ClientIDMode="Static" runat="server" Text=" Active and Inactive"
                                                        GroupName="RdoStatus" Checked="False" AutoPostBack="True" OnCheckedChanged="GetXMLReed"
                                                        CssClass="LabelFont" />
                                                </td>
                                                <td class="object-wrapper" style="padding-right: 0; white-space: nowrap;">
                                                    <asp:RadioButton ID="rbInActive" ClientIDMode="Static" runat="server" Text=" Inactive"
                                                        GroupName="RdoStatus" Checked="False" AutoPostBack="True" OnCheckedChanged="GetXMLReed"
                                                        CssClass="LabelFont" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="table-wrapper page1">
                    <table border="0" cellpadding="10" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="updatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gwReedTerritories" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CellPadding="4" CellSpacing="1" Font-Size="12px" ForeColor="Black" BackColor="#FFFFFF"
                                            OnSorting="gwReedTerritories_Sorting" AllowPaging="true" AllowSorting="true"
                                            PageSize="200" GridLines="None" AsyncRendering="false" EmptyDataText="No data available."
                                            ClientIDMode="Static" OnPageIndexChanging="gwReedTerritories_PageIndexChanging">
                                            <RowStyle CssClass="RowStyle" Wrap="false" />
                                            <AlternatingRowStyle BackColor="#e5e5e5" />
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                            <PagerStyle CssClass="grid_paging" />
                                            <Columns>
                                                <asp:BoundField DataField="Project ID" HeaderText="Project ID" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="Project ID" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="Title" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="Country" HeaderText="Country" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="Country" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="City" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="General Contractor Name" HeaderText="General Contractor Name"
                                                    SortExpression="General Contractor Name" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="State/Province" HeaderText="State/Province" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="State/Province" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="General Contractor Phone #" HeaderText="General Contractor Phone #"
                                                    SortExpression="General Contractor Phone #" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="Value" HeaderText="Value" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="Value" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="Stage" HeaderText="Stage" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="Stage" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="Bid Date" HeaderText="Bid Date" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="Bid Date" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="# of Matching Docs" HeaderText="# Of Matching Doc" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="# of Matching Docs" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="Status" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                    HeaderStyle-Wrap="false" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <%-- <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rbActive" EventName="OnCheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="rbActiveInActive" EventName="OnCheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="rbInActive" EventName="OnCheckedChanged" />
                    </Triggers>--%>
        </asp:UpdatePanel>
    </div>
    <div>
        <asp:Panel ID="SubPanel" ClientIDMode="Static" runat="server" Style="display: none;">
            <%--  <p class="validateTips">
                        All form fields are required.</p>--%>
            <table runat="server">
                <tr>
                    <td>
                        <asp:Label ID="lblSubCompanyName" Text="Company Name:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubCompanyName" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                            AutoPostBack="false" Style="width: 150px;" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubTitle" Text="Title:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubTitle" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                            AutoPostBack="false" Style="width: 150px;" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubName" Text="Name:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubName" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                            AutoPostBack="false" Style="width: 150px;" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubPhone" Text="Phone:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubPhone" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                            AutoPostBack="false" Style="width: 150px;" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubEmail" Text="Email:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubEmail" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                            AutoPostBack="false" Style="width: 150px;" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="white-space: nowrap">
                        <asp:Label ID="lblSubCustomer" Text="Customer Y/N:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td style="width: 123px">
                        <asp:DropDownList ID="dlSubCustomer" runat="server" Height="20px" ClientIDMode="Static"
                            Style="font-size: 14px;">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="white-space: nowrap">
                        <asp:Label ID="lblSubKAM" Text="KAM Y/N:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td style="width: 123px">
                        <asp:DropDownList ID="dlSubKAM" runat="server" Height="20px" ClientIDMode="Static"
                            Style="font-size: 14px;">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubAccountNum" Text="Account Number:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubAccountNum" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                            AutoPostBack="false" Style="width: 150px;" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubNotes" Text="Notes:" runat="server" CssClass="LabelFont" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubNotes" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                            TextMode="MultiLine" AutoPostBack="false" Style="width: 150px;" Font-Size="12px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="hdnProjectID" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnProjectID2" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnSubConID" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnCallInfo" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnSubConGWCount" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnTerID" Value="0" ClientIDMode="Static" runat="server" />
</asp:Content>
