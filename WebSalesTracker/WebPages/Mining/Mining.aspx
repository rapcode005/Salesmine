<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Mining.aspx.cs" Inherits="WebSalesMine.WebPages.Mining.Mining" %>

<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<%@ Import Namespace="AppLogic" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
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
        function ClearFields() {
            jQuery("#cmbKAM").val('');
            jQuery("#cmbOfficeWarehouse").val('');
            jQuery("#cmbNoQuestion").val('');
            jQuery("#txtIfOthers").val('');
            jQuery("#dlSafetySigns").val('');
            jQuery("#Dept1").val('');
            
//            jQuery("#txtNextFollowupContact").val('');
//            jQuery("#txtFollowupDate").val('');
//            jQuery("#txtNumCall").val('');
//            jQuery("#txtlastUpdatedBy3").val('');
//            jQuery("#txtLastUpdatedDate3").val('');
//            jQuery("#txtfollowupNotes").val('');
        }

        function OnSuccess(Data) {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
           // var myDate = new Date(CheckString(Data.rows[0]['Valid_From']));
        
            jQuery("#cmbKAM").val('');
            jQuery("#cmbOfficeWarehouse").val('');
            jQuery("#cmbNoQuestion").val('');
            jQuery("#txtIfOthers").val('');
            jQuery("#dlSafetySigns").val('');
            jQuery("#dlHandled").val('');
            jQuery("#txtQOther").val('');
            jQuery("#dlDisposition").val('');
            jQuery("#lblLastUpdatedDate2").val('');
            jQuery("#lblLastUpdatedBy2").val('');

            jQuery("#txtQMailingAdd").val('');
            jQuery("#txtQFirstname").val('');
            jQuery("#txtQLastname").val('');
            jQuery("#txtQTitle").val('');
            jQuery("#txtQPhoneNum").val('');
            jQuery("#txtQExtensionNum").val('');
            jQuery("#txtQEmailAdd").val('');

            jQuery("#txtQMailingAdd").val(CheckString(Data.rows[0]['MailAddress']));
            jQuery("#txtQFirstname").val(CheckString(Data.rows[0]['FirstName']));
            jQuery("#txtQLastname").val(CheckString(Data.rows[0]['LastName']));
            jQuery("#txtQTitle").val(CheckString(Data.rows[0]['Title']));
            jQuery("#txtQPhoneNum").val(CheckString(Data.rows[0]['PhoneNumber']));
            jQuery("#txtQExtensionNum").val(CheckString(Data.rows[0]['ExtNumber']));
            jQuery("#txtQEmailAdd").val(CheckString(Data.rows[0]['EmailAddress']));
            
            jQuery("#cmbKAM").val(CheckString(Data.rows[0]['ActiveMineSite']));
            jQuery("#cmbOfficeWarehouse").val(CheckString(Data.rows[0]['AddressOfficeWare']));
            jQuery("#cmbNoQuestion").val(CheckString(Data.rows[0]['NoQuestion']));
            jQuery("#txtIfOthers").val(CheckString(Data.rows[0]['OtherNoQuestion']));
            jQuery("#dlSafetySigns").val(CheckString(Data.rows[0]['SafetySignsFacility']));
            jQuery("#dlHandled").val(CheckString(Data.rows[0]['YesCorporateOfficeSiteLevel']));
            jQuery("#txtQOther").val(CheckString(Data.rows[0]['YesCorporateOfficeSiteLevelOther']));
            jQuery("#dlDisposition").val(CheckString(Data.rows[0]['Disposition']));
                        jQuery("#lblLastUpdatedBy2").val(CheckString(Data.rows[0]['createdby']));
                        jQuery("#lblLastUpdatedDate2").val(CheckString(Data.rows[0]['Valid_From']));

            $get('lblLastUpdatedDate2').textContent = (myDate.getMonth() + 1) + "/" +
                                                                    myDate.getDate() + "/" +
                                                                    myDate.getFullYear();
            $get('lblLastUpdatedBy2').textContent = CheckString(Data.rows[0]['createdby']);

        }

        function OnSuccessDetails(Data) {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            document.getElementById('txtFirstname').value = CheckString(Data.rows[0]['first_name']);
            jQuery("#txtSalutation").val(CheckString(Data.rows[0]['salutation']));
            jQuery("#txtSurname").val(CheckString(Data.rows[0]['Last_Name']));
            jQuery("#txtFunction").val(CheckString(Data.rows[0]['Function_Code']));
            jQuery("#txtFunctionDesc").val(CheckString(Data.rows[0]['Function_Description']));
            jQuery("#txtSource").val(CheckString(Data.rows[0]['source']));
        
        }
        
        function OnSuccessDept(Data) {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            //var myDate = new Date(CheckString(Data.rows[0]['valid_from']));
            //alert(Data.rows[0]['Row']);
            //alert(CheckString(Data.rows[0]['ID']));

            var rowCount = Data.rows.length;
            jQuery("#Dept1").val('');
            jQuery("#Dept2").val('');
            jQuery("#Dept3").val('');

            for (var i = 0; i < rowCount + 1; i++) {
                var rowValue = Data.rows[i]["Department"];

                if (i == 0) {
                    jQuery("#Dept1").val(CheckString(Data.rows[i]['Department']));
                }
                if (i == 1) {
                    jQuery("#Dept2").val(CheckString(Data.rows[i]['Department']));
                }
                if (i == 2) {
                    jQuery("#Dept3").val(CheckString(Data.rows[i]['Department']));
                }
            }

        }

        function OnSuccessSCO(Data) {
            var FIREFOX = /Firefox/i.test(navigator.userAgent);
            //        var myDate = new Date(CheckString(Data.rows[0]['valid_from']));
            //alert(Data.rows[0]['Row']);
            //alert(CheckString(Data.rows[0]['ID']));

            jQuery("#txtQMailingAdd").val('');
            jQuery("#txtQFirstname").val('');
            jQuery("#txtQLastname").val('');
            jQuery("#txtQTitle").val('');
            jQuery("#txtQPhoneNum").val('');
            jQuery("#txtQExtensionNum").val('');
            jQuery("#txtQEmailAdd").val('');

            jQuery("#txtQMailingAdd").val(CheckString(Data.rows[0]['MailAddress']));
            jQuery("#txtQFirstname").val(CheckString(Data.rows[0]['FirstName']));
            jQuery("#txtQLastname").val(CheckString(Data.rows[0]['LastName']));
            jQuery("#txtQTitle").val(CheckString(Data.rows[0]['Title']));
            jQuery("#txtQPhoneNum").val(CheckString(Data.rows[0]['PhoneNumber']));
            jQuery("#txtQExtensionNum").val(CheckString(Data.rows[0]['ExtNumber']));
            jQuery("#txtQEmailAdd").val(CheckString(Data.rows[0]['EmailAddress']));

           
        }


        function CheckString(val) {
            // alert(val);
            if (val)
                return val;
            else
                return "";
        }


        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                var mydiv = $('#pnlAllNotes');
                mydiv.dialog('close');
            }

        }

        function onUpdateQASuccess(Stat) {
            //document.getElementById('hdnProjectID2').value = '';
//            var varProjectID = document.getElementById('hdnProjectID2').value;

            if (Stat == true) {
               
             alert("Successfully Update");

            }
            else {
                alert("Update Failed");
            }

        }

        function onInsertSuccess(Stat) {
            //document.getElementById('hdnProjectID2').value = '';
            //            var varProjectID = document.getElementById('hdnProjectID2').value;

            if (Stat == true) {

                alert("Successfully Added");
                 jQuery("#txtNotes2").val('');
                 __doPostBack('', '');
            }
            else {
                alert("Adding Failed");
            }

        }

         function ConfirmationNotes() {

          var conf = confirm("Are you sure you want to add?");

           if (conf == true) {
          var varNotes = $("#txtNotes2").val();
         // var ContactID = document.getElementById('hdnMiningContact').value
          PageMethods.InserNotes(varNotes, onInsertSuccess);
           }

           else
           return false
         
         }



        function ConfirmationQA() {

            var conf = confirm("Are you sure you want to update?");
            //var updateStat = new Boolean(0);
            var varActiveMineSite = $("#cmbKAM").val();
            var varAddressOfficeWare = $("#cmbOfficeWarehouse").val();
            var varNoQuestion = $("#cmbNoQuestion").val();
            var varOtherNoQuestion = $("#txtIfOthers").val();
            var varSafetySignsFacility = $("#dlSafetySigns").val();
            var varYesCorporateOfficeSiteLevel = $("#dlHandled").val(); ;
            var varYesCorporateOfficeSiteLevelOther = $("#txtQOther").val();
            var varDisposition = $("#dlDisposition").val();

            var ContactID = document.getElementById('hdnMiningContact').value
            var MailAddress = $("#txtQMailingAdd").val();
            var FirstName = $("#txtQFirstname").val();
            var LastName = $("#txtQLastname").val();
            var Title = $("#txtQTitle").val();
            var PhoneNumber = $("#txtQPhoneNum").val();
            var ExtNumber = $("#txtQExtensionNum").val();
            var EmailAddress = $("#txtQEmailAdd").val();

            var depValue;
            

            if (conf == true) {
                //alert(varContractorName);
                
                

                for (var i = 1; i <= 3; i++) {
                    if (i == 1) {
                        depValue = $("#Dept1").val();
                        //alert(depValue);
                        PageMethods.updateDept(ContactID, depValue, i);                      
                    }
                    if (i == 2) {
                        depValue = $("#Dept2").val();
                       // alert(depValue);
                        PageMethods.updateDept(ContactID, depValue, i);                       
                    }

                    if (i == 3) {
                        depValue = $("#Dept3").val();
                        //alert(depValue);
                        PageMethods.updateDept(ContactID, depValue,i);
                    }
                }
                PageMethods.updateSCO(ContactID, MailAddress, FirstName, LastName, Title, PhoneNumber, ExtNumber, EmailAddress);
                PageMethods.updateQA(varActiveMineSite,ContactID, varAddressOfficeWare, varNoQuestion, varOtherNoQuestion, varSafetySignsFacility, varYesCorporateOfficeSiteLevel, varYesCorporateOfficeSiteLevelOther, varDisposition, onUpdateQASuccess);

                // PageMethods.updateGenCon(varContractorName, varContractorEmail, varContractorPhone, varContractorCustomer, varContractorAccount, varKAM, varCalled, onUpdateSuccess);

            }
            return false;
        }

        function pageLoad() {

            $("#<%=gwMining.ClientID%>  tr:has(td)").hover(function () {
                $(this).css("cursor", "pointer");
            });

            jQuery("#cmbKAM").val('');
            jQuery("#cmbOfficeWarehouse").val('');
            jQuery("#cmbNoQuestion").val('');
            jQuery("#txtIfOthers").val('');
            jQuery("#dlSafetySigns").val('');
            jQuery("#Dept1").val('');
            jQuery("#Dept2").val('');
            jQuery("#Dept3").val('');
            jQuery("#dlHandled").val('');
            jQuery("#txtQOther").val('');
            jQuery("#txtQMailingAdd").val('');
            jQuery("#txtQFirstname").val('');
            jQuery("#txtQLastname").val('');
            jQuery("#txtQTitle").val('');
            jQuery("#txtQPhoneNum").val('');
            jQuery("#txtQExtensionNum").val('');
            jQuery("#txtQEmailAdd").val('');
            jQuery("#dlDisposition").val('');
            

            $("#gwMining tr").not($("#gwMining tr").eq(0)).click(function () {
                $("#gwMining  tr").closest('TR').removeClass('SelectedRowStyle');
                $(this).addClass('SelectedRowStyle');

                var totalGVcols = $("#gwMining").find('tr')[0].cells.length;
                var totcol = 0;
                var RowID;
                var SiteNum;
                var ContactNum;

                jQuery("#cmbKAM").val('');
                jQuery("#cmbOfficeWarehouse").val('');
                jQuery("#cmbNoQuestion").val('');
                jQuery("#txtIfOthers").val('');
                jQuery("#dlSafetySigns").val('');
                jQuery("#Dept1").val('');
                jQuery("#Dept2").val('');
                jQuery("#Dept3").val('');
                jQuery("#dlHandled").val('');
                jQuery("#txtQOther").val('');
                jQuery("#txtQMailingAdd").val('');
                jQuery("#txtQFirstname").val('');
                jQuery("#txtQLastname").val('');
                jQuery("#txtQTitle").val('');
                jQuery("#txtQPhoneNum").val('');
                jQuery("#txtQExtensionNum").val('');
                jQuery("#txtQEmailAdd").val('');
                jQuery("#dlDisposition").val('');

                for (var i = 0; i < totalGVcols + 1; i++) {
                    // alert("ULIT");
                    var ColName = $('#gwMining tr').find('th:nth-child(' + i + ')').text();

                    if (ColName == 'Row') {

                        RowID = $(this).find("td:eq(" + (i - 1) + ")").html();
                        //                        alert(RowID);
                        //  alert(RowID);
                        document.getElementById('hdnMiningContact').value = RowID;
                        //PageMethods.GetMiningDetails(RowID, OnSuccess);
                    }

                    if (ColName == 'Site Number') {
                        SiteNum = $(this).find("td:eq(" + (i - 1) + ")").html();

                    }

                    if (ColName == 'Contact Number') {
                        ContactNum = $(this).find("td:eq(" + (i - 1) + ")").html();
                        document.getElementById('hdnMiningContact').value = ContactNum;
                        PageMethods.GetMiningDetails(ContactNum, OnSuccessDetails);
                        PageMethods.GetMiningQASCO(ContactNum, OnSuccess);
                        PageMethods.GetMiningDeptXML(SiteNum, ContactNum, OnSuccessDept);
                       // __doPostBack('','');
                       // PageMethods.GetMiningSCOXML(SiteNum,ContactNum, OnSuccessSCO);
                    }

                }

            });


            $('#<%= lnkAllNotes.ClientID %>').click(function () {

                $(document).ready(function () {

                    var myNotes = $('#pnlAllNotes');
                    myNotes.dialog({ autoOpen: false,
                        title: "MINING NOTES",
                        resizable: false,
                        width: "auto",
                        height: "auto",
                        draggable: true,
                        closeOnEscape: true,
                        dialogClass: "FixedPostion",
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        },

//                        buttons: {
//                            "Save": function () {
//                                var varNotes = $("#txtNotes2").val();
//                                PageMethods.InserNotes(varNotes, onInsertSuccess);
//                                jQuery("#txtNotes2").val('');
//                               // PageMethods.GetMiningNotesXML();

//                                myNotes.dialog('close');
//                            },
//                            "Cancel": function () {

//                                myNotes.dialog('close');
//                            }
//                        }
                    });
                    myNotes.dialog('open');
                    return false;
                });
                return false;

            });



            $("#btnUpdateQA").click(function () {

                $("input[type=submit][id=btnSaveQA]").attr('disabled', false);
                $("input[type=submit][id=btnCancelQA]").attr('disabled', false);
                $("input[type=submit][id=btnUpdateQA]").attr('disabled', true);

                jQuery("#cmbKAM").attr('disabled', false);
                jQuery("#cmbOfficeWarehouse").attr('disabled', false);
                jQuery("#cmbNoQuestion").attr('disabled', false);
                jQuery("#dlSafetySigns").attr('disabled', false);
                jQuery("#dlHandled").attr('disabled', false);
                jQuery("#txtIfOthers").attr('disabled', false);
                jQuery("#Dept1").attr('disabled', false);
                jQuery("#Dept2").attr('disabled', false);
                jQuery("#Dept3").attr('disabled', false);
                jQuery("#txtQOther").attr('disabled', false);
                jQuery("#txtQMailingAdd").attr('disabled', false);
                jQuery("#txtQFirstname").attr('disabled', false);
                jQuery("#txtQLastname").attr('disabled', false);
                jQuery("#txtQTitle").attr('disabled', false);
                jQuery("#txtQPhoneNum").attr('disabled', false);
                jQuery("#txtQExtensionNum").attr('disabled', false);
                jQuery("#txtQEmailAdd").attr('disabled', false);
                jQuery("#dlDisposition").attr('disabled', false);
                

            });


            $("#btnCancelQA").click(function () {

                $("input[type=submit][id=btnSaveQA]").attr('disabled', true);
                $("input[type=submit][id=btnCancelQA]").attr('disabled', true);
                $("input[type=submit][id=btnUpdateQA]").attr('disabled', false);

                jQuery("#cmbKAM").attr('disabled', true);
                jQuery("#cmbOfficeWarehouse").attr('disabled', true);
                jQuery("#cmbNoQuestion").attr('disabled', true);
                jQuery("#dlSafetySigns").attr('disabled', true);
                jQuery("#dlHandled").attr('disabled', true);
                jQuery("#txtIfOthers").attr('disabled', true);
                jQuery("#Dept1").attr('disabled', true);
                jQuery("#Dept2").attr('disabled', true);
                jQuery("#Dept3").attr('disabled', true);
                jQuery("#txtQOther").attr('disabled', true);
                jQuery("#txtQMailingAdd").attr('disabled', true);
                jQuery("#txtQFirstname").attr('disabled', true);
                jQuery("#txtQLastname").attr('disabled', true);
                jQuery("#txtQTitle").attr('disabled', true);
                jQuery("#txtQPhoneNum").attr('disabled', true);
                jQuery("#txtQExtensionNum").attr('disabled', true);
                jQuery("#txtQEmailAdd").attr('disabled', true);
                jQuery("#dlDisposition").attr('disabled', true);
            });


             $(document).ready(function () {
            $('#txtNotes2').keyup(function () {
                var len = this.value.length;

                if (len >= 150) {
                    this.value = this.value.substring(0, 150);
                }

                $('#CharterLeft').text(150 - len);
            });
        });


            $addHandler(document, 'keydown', onKeypress);
                        
        }

    </script>
    <div id="container">
        <asp:Label ID="lblProjectInfo" CssClass="GridHeaderLabel" Text="Site Information"
            runat="server"></asp:Label>
        <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="object-container">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr style="white-space: nowrap">
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblCompName" Text="Company Name:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCompName" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAddress" Text="Address:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSalutation" Text="Salutation:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSalutation" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFunction" Text="Function:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFunction" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="white-space: nowrap">
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblCompanyPhone" Text="Company Phone:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyPhone" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCity" Text="City:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFirstname" Text="Firstname:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstname" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblFunctionDesc" Text="Function Desc:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFunctionDesc" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="white-space: nowrap">
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblSIC" Text="SIC:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSIC" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblState" Text="State:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtState" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSurname" Text="Surname:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSurname" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblSource" Text="Source:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSource" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="white-space: nowrap">
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblEmployeeSize" Text="Employee Size:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmployeeSize" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                    <td style="white-space: nowrap">
                                        <asp:Label ID="lblPostalCode" Text="Postal Code:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td>
                    <div class="table-wrapper page1">
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:UpdatePanel ID="updatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gwMining" runat="server" AutoGenerateColumns="False" Width="100%"
                                                CellPadding="4" CellSpacing="1" Font-Size="12px" ForeColor="Black" BackColor="#FFFFFF"
                                                GridLines="None" AsyncRendering="false" EmptyDataText="No data available." PageSize="10"
                                                AllowPaging="false" AllowSorting="true" ClientIDMode="Static">
                                                <AlternatingRowStyle BackColor="#e5e5e5" />
                                                <EditRowStyle CssClass="EditRowStyle" />
                                                <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                <RowStyle CssClass="RowStyle" Wrap="false" />
                                                <PagerStyle CssClass="grid_paging" />
                                                <Columns>
                                                    <%-- <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:RadioButton runat="server" ID="rbProj" onclick="javascript:SelectSingleRadiobutton(this.id)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="Site_number" HeaderText="Site Number" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Site Number" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Company_Name" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Company Name" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Company_Phone" HeaderText="Company Phone" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Company Phone" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Function_Description" HeaderText="Function Description"
                                                        HeaderStyle-HorizontalAlign="Center" SortExpression="Function Description" HeaderStyle-VerticalAlign="Middle"
                                                        HeaderStyle-Width="15%" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="first_name" HeaderText="Firstname" SortExpression="Firstname"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Last_Name" HeaderText="Lastname" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Lastname" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Sic" HeaderText="SIC" SortExpression="SIC" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Employee_Size" HeaderText="Employee Size" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Employee Size" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="address" HeaderText="Address" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Address" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="city" HeaderText="City" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="City" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="state" HeaderText="State" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="State" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Postal_Code" HeaderText="Postal Code" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Postal Code" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="salutation" HeaderText="Salutation" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Salutation" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Function_Code" HeaderText="Function" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Function" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Campaign_Name" HeaderText="Campaign Name" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Campaign Name" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="source" HeaderText="Source" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Source" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Contact_number" HeaderText="Contact Number" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="Contact_number" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="Row" HeaderText="Row">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" Wrap="False" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        </br>
        <%-- <asp:Panel ID="pnlQADetails" runat="server" Style="display: none;" ClientIDMode="Static">--%>
        <table style="border: 1px solid; width: 100%;">
            <tr>
                <td style="background-color: #d6e0ec;">
                    <asp:Label ID="lblMailHeader" Font-Bold="True" CssClass="LabelFont" runat="server"
                        Text="Mining Questions"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="object-container">
            <asp:Panel ID="pnlQuestions" runat="server" ScrollBars="Vertical" Height="320px">
                <asp:UpdatePanel ID="updatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td class="object-containerQuestion">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblQ1" Text="Is this an active mine site?:" runat="server" CssClass="LabelFont" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cmbKAM" runat="server" Height="20px" ClientIDMode="Static"
                                                    Enabled="false">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>I don't know what that means</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" Text="If I don't know what that means" runat="server" CssClass="LabelFont"
                                                    ForeColor="Red" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" Text="Are mining operations conducted at this address or is this an office or warehouse?"
                                                    runat="server" CssClass="LabelFont" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cmbOfficeWarehouse" runat="server" Height="20px" ClientIDMode="Static"
                                                    Enabled="false">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Office</asp:ListItem>
                                                    <asp:ListItem>Warehouse</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" Text="If No Select:" runat="server" CssClass="LabelFont" ForeColor="Red" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="cmbNoQuestion" runat="server" Height="20px" ClientIDMode="Static"
                                                                Enabled="false">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem>Office</asp:ListItem>
                                                                <asp:ListItem>Inactive Mine</asp:ListItem>
                                                                <asp:ListItem>Warehouse</asp:ListItem>
                                                                <asp:ListItem>Other</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" Text="Other:" runat="server" CssClass="LabelFont" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIfOthers" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" Text="Are safety signs, lock-out tag-out, and/or traffic and <br/>parking lot products purchased out of this office/Site?"
                                                    runat="server" CssClass="LabelFont" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="dlSafetySigns" runat="server" Height="20px" ClientIDMode="Static"
                                                    Enabled="false">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" Text="Do you know what department handles the purchasing?"
                                                    runat="server" CssClass="LabelFont" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label13" Text="1." runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Dept1" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox><br />
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
                                                                        <asp:Label ID="Label14" Text="2." runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Dept2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox><br />
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
                                                                        <asp:Label ID="Label15" Text="3." runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Dept3" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
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
                                <td class="object-containerQuestion">
                                    <table>
                                        <tr>
                                            <td class="object-containerQuestion">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <%--<td>
                                                                        <asp:Label ID="Label6" Text="If No:" runat="server" CssClass="LabelFont" ForeColor="Red" />
                                                                    </td>--%>
                                                                    <td>
                                                                        <asp:Label ID="Label9" Text="Do you know where this is handled?" runat="server" CssClass="LabelFont" />
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
                                                                        <asp:DropDownList ID="dlHandled" runat="server" Height="20px" ClientIDMode="Static"
                                                                            Enabled="false">
                                                                            <asp:ListItem></asp:ListItem>
                                                                            <asp:ListItem>Corporate Office</asp:ListItem>
                                                                            <asp:ListItem>Site Level</asp:ListItem>
                                                                            <asp:ListItem>I don't know</asp:ListItem>
                                                                            <asp:ListItem>Other</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblQOther" Text="Other:" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQOther" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblQOther" Text="Other:" runat="server" CssClass="LabelFont" ForeColor="Red" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQOther" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px;"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>--%>
                                                    <%--<tr>
                                                        <td>
                                                            <asp:Label ID="Label8" Text=" If site level:" runat="server" CssClass="LabelFont"
                                                                ForeColor="Red" />
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" Text="Could I have the contact information for that site?"
                                                                runat="server" CssClass="LabelFont" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblQMailingAdd" Text="Mailing Address" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQMailingAdd" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblQPhoneNumber" Text="Phone Number" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQPhoneNum" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblQFirstname" Text="First Name" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQFirstname" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblExtensionNum" Text="Extension Number" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQExtensionNum" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblQLastname" Text="Last Name" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQLastname" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblQEmailAdd" Text="Email Address" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQEmailAdd" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblQTitle" Text="Title" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtQTitle" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                            AutoPostBack="false" Style="width: 150px; color: Black;" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="object-containerQuestion">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                                AutoPostBack="false" Style="width: 250px; height: 110px;" TextMode="MultiLine"></asp:TextBox>--%>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDisposition" Text="Disposition" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="dlDisposition" runat="server" Height="20px" ClientIDMode="Static"
                                                                            Enabled="false">
                                                                            <asp:ListItem></asp:ListItem>
                                                                            <asp:ListItem>New Lead Level 4</asp:ListItem>
                                                                            <asp:ListItem>Not Interested - General</asp:ListItem>
                                                                            <asp:ListItem>Customer Hung-up</asp:ListItem>
                                                                            <asp:ListItem>Residential</asp:ListItem>
                                                                            <asp:ListItem>Referred Number</asp:ListItem>
                                                                            <asp:ListItem>Language Barrier</asp:ListItem>
                                                                            <asp:ListItem>Fax or Modem</asp:ListItem>
                                                                            <asp:ListItem>Agent No Answer</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label6" Text="Last Updated by:" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblLastUpdatedBy2" runat="server" CssClass="LabelFont" ClientIDMode="Static" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label11" Text="Last Updated Date:" runat="server" CssClass="LabelFont" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblLastUpdatedDate2" runat="server" CssClass="LabelFont" ClientIDMode="Static" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkAllNotes" runat="server" CssClass="LabelFont" ClientIDMode="Static">Show all notes</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Button ID="btnUpdateQA" value="Update Answers" type="submit" runat="server"
                                                                                        Text="Update" ToolTip="Update Answers" CssClass="Const-UpdateButton" ClientIDMode="Static"
                                                                                        Style="color: White" Width="80px" OnClientClick="return false;" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="btnSaveQA" value="Save Answers" type="submit" runat="server" Text="Save"
                                                                                        ToolTip="Save Answers" CssClass="Const-UpdateButton" ClientIDMode="Static" Style="color: White"
                                                                                        Width="80px" Enabled="false" OnClick="btnSaveQA_Click" />
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:Button ID="btnCancelQA" value="Cancel" type="submit" runat="server" Text="Cancel"
                                                                                        ToolTip="" CssClass="Const-UpdateButton" ClientIDMode="Static" Style="color: White"
                                                                                        Width="80px" Enabled="false" OnClientClick="return false;" />
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
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlAllNotes" runat="server" Style="display: none;" ClientIDMode="Static">
                            <div class="table-wrapper page1">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtNotes2" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                                TextMode="MultiLine" AutoPostBack="false" Style="width: 100%; height: 100px;
                                                font-size: 12px;"></asp:TextBox>
                                            <div align="right">
                                                <asp:Label ID="CharterLeft" runat="server" ClientIDMode="Static" Text="150"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="float: right;">
                                                <asp:Button ID="btnSaveNotes" runat="server" Width="70px" Text="Save" CssClass="Const-UpdateButton"
                                                    OnClick="btnSaveNotes_Click" Style="height: 20px; color: White; font-size: 12px;
                                                    text-align: center;" />
                                                <asp:Button ID="btnCancelNotes" runat="server" Width="70px" Text="Cancel" CssClass="Const-UpdateButton"
                                                    OnClick="btnCancelNotes_Click" Style="height: 20px; color: White; font-size: 12px;
                                                    text-align: center;" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel runat="server" ScrollBars="Both" ID="DataDiv" Width="100%">
                                                <div class="table-wrapper page5">
                                                    <asp:GridView ID="gvNotes" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        CellPadding="4" CellSpacing="1" Font-Size="12px" ForeColor="Black" BackColor="#FFFFFF"
                                                        GridLines="None" AsyncRendering="false" EmptyDataText="No data available." PageSize="5"
                                                        AllowPaging="false" AllowSorting="true" ClientIDMode="Static">
                                                        <AlternatingRowStyle BackColor="#e5e5e5" />
                                                        <EditRowStyle CssClass="EditRowStyle" />
                                                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                        <RowStyle CssClass="RowStyle" Wrap="false" />
                                                        <PagerStyle CssClass="grid_paging" />
                                                        <Columns>
                                                            <%-- <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:RadioButton runat="server" ID="rbProj" onclick="javascript:SelectSingleRadiobutton(this.id)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                            <asp:BoundField DataField="Notes" HeaderText="Notes" HeaderStyle-HorizontalAlign="Center"
                                                                SortExpression="Site Notes" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                                HeaderStyle-Wrap="false" />
                                                            <asp:BoundField DataField="Createdby" HeaderText="Created by" HeaderStyle-HorizontalAlign="Center"
                                                                SortExpression="Createdby" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%"
                                                                HeaderStyle-Wrap="false" />
                                                            <asp:BoundField DataField="Createdon" HeaderText="Created on" HeaderStyle-HorizontalAlign="Center"
                                                                HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" DataFormatString="{0:MM/dd/yyyy}"
                                                                HeaderStyle-Wrap="false" />
                                                           
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <%-- </asp:Panel>--%>
    </div>
    <asp:HiddenField ID="hdnMiningContact" Value="0" ClientIDMode="Static" runat="server" />
</asp:Content>
