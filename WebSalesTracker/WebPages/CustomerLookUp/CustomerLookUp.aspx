<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerLookUp.aspx.cs"
    EnableEventValidation="false" Inherits="WebSalesMine.WebPages.CustomerLookUp.CustomerLookUp"
    MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master" %>

<%@ Register Assembly="Utilities" Namespace="Utilities" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="AppLogic" %>
<asp:Content ID="LookUpContent" ContentPlaceHolderID="container" runat="server">
    <script type='text/javascript' src="../../App_Themes/JS/jquery.cookie.js"></script>

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

        var radio = null;
        var globalcontactVal = null;
        var globalAccntVal = null;
        var globalcontactnameVal = null;
        $(document).ready(function () {
            $("#gridCustomerLookup tr").not($("#gridCustomerLookup tr").eq(0)).click(function () {
                $("#gridShowAllUserData").find("INPUT[type='radio']").attr("checked", false);

                radio = $(this).find("INPUT[type='radio']");
                var list = radio.closest('table').find("INPUT[type='radio']").not(radio);
                list.closest('TR').removeClass('SelectedRowStyle');

                radio.attr("checked", true);
                radio.closest('TR').addClass('SelectedRowStyle');
                $("#hfIndex").val($(this).index());
                
            });
        });

        function CheckString(val) {
            if (val)
                return val;
            else
              
                return "";
        }


        function SelectSingleRadiobutton(rdBtnID) {
            var rduser = $(document.getElementById(rdBtnID));

            rduser.closest('TR').addClass('SelectedRowStyle');
            rduser.checked = true;
            var list = rduser.closest('table').find("INPUT[type='radio']").not(rduser);
            list.attr('checked', false);
            list.closest('TR').removeClass('SelectedRowStyle');
        }

        

  function OnSuccessCustomer(Data) {
             var RowIndex = Data ;
             var grd = document.getElementById('gridCustomerLookup');
             var CONTACT_STATUS, ACCOUNT_NUM, ACCOUNT_NAME, MANAGED_GROUP, BUYER_ORG, KAM_NAME, CONTACT_NUM, CONTACT_NAME, CONTACT_TYPE, CONTACT_PHONE
             var CONTACT_RECENCY, CONTACT_SALES_12M, FUNCTION, LAST_DISP_DATE, LAST_DISP_NOTE, EMAIL_ADDRESS, REP_CONTACT_STATUS, REP_JOB_AREA,LAST_PURCHASED_DATE
       
              if (grd) {
                if (isNaN(RowIndex) == false) {
                    totalCols = $("#gridCustomerLookup").find('tr')[0].cells.length;
                    for (var i = 1; i < totalCols + 1; i++) {
                        var ColName = $('#gridCustomerLookup tr').find('th:nth-child(' + i + ')').text();

                        if (ColName == 'CONTACT STATUS') {
                            CONTACT_STATUS = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CONTACT_STATUS == undefined || CONTACT_STATUS == '&nbsp;') {
                                CONTACT_STATUS = '';
                            }
                        }
                        else if (ColName == 'ACCOUNT NUM') {

                            ACCOUNT_NUM = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ACCOUNT_NUM == undefined || ACCOUNT_NUM == '&nbsp;') {
                                ACCOUNT_NUM = '';
                            }
                        }
                        else if (ColName == 'ACCOUNT NAME') {

                            ACCOUNT_NAME = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (ACCOUNT_NAME == undefined || ACCOUNT_NAME == '&nbsp;') {
                                ACCOUNT_NAME = '';
                            }
                        }
                        else if (ColName == 'MANAGED GROUP') {

                            MANAGED_GROUP = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (MANAGED_GROUP == undefined || MANAGED_GROUP == '&nbsp;') {
                                MANAGED_GROUP = '';
                            }
                        }
                        else if (ColName == 'BUYER ORG') {

                            BUYER_ORG = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (BUYER_ORG == undefined || BUYER_ORG == '&nbsp;') {
                                BUYER_ORG = '';
                            }
                        }
                        else if (ColName == 'KAM NAME') {

                            KAM_NAME = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (KAM_NAME == undefined || KAM_NAME == '&nbsp;') {
                                KAM_NAME = '';
                            }
                        }
                        else if (ColName == 'CONTACT NUM') {

                            CONTACT_NUM = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CONTACT_NUM == undefined || CONTACT_NUM == '&nbsp;') {
                                CONTACT_NUM = '';
                            }
                        }
                        else if (ColName == 'CONTACT NAME') {

                            CONTACT_NAME = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CONTACT_NAME == undefined || CONTACT_NAME == '&nbsp;') {
                                CONTACT_NAME = '';
                            }
                        }
                        else if (ColName == 'CONTACT TYPE') {

                            CONTACT_TYPE = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CONTACT_TYPE == undefined || CONTACT_TYPE == '&nbsp;') {
                                CONTACT_TYPE = '';
                            }
                        }
                        else if (ColName == 'CONTACT PHONE') {

                            CONTACT_PHONE = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CONTACT_PHONE == undefined || CONTACT_PHONE == '&nbsp;') {
                                CONTACT_PHONE = '';
                            }
                        }
                        else if (ColName == 'CONTACT RECENCY') {

                            CONTACT_RECENCY = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CONTACT_RECENCY == undefined || CONTACT_RECENCY == '&nbsp;') {
                                CONTACT_RECENCY = '';
                            }
                        }
                        else if (ColName == 'CONTACT SALES 12M') {

                            CONTACT_SALES_12M = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (CONTACT_SALES_12M == undefined || CONTACT_SALES_12M == '&nbsp;') {
                                CONTACT_SALES_12M = '';
                            }
                        }
                        else if (ColName == 'FUNCTION') {

                            FUNCTION = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (FUNCTION == undefined || FUNCTION == '&nbsp;') {
                                FUNCTION = '';
                            }
                        }
                        else if (ColName == 'LAST DISP DATE') {

                            LAST_DISP_DATE = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (LAST_DISP_DATE == undefined || LAST_DISP_DATE == '&nbsp;') {
                                LAST_DISP_DATE = '';
                            }
                        }
                        else if (ColName == 'LAST DISP NOTE') {

                            LAST_DISP_NOTE = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (LAST_DISP_NOTE == undefined || LAST_DISP_NOTE == '&nbsp;') {
                                LAST_DISP_NOTE = '';
                            }
                        }
                        else if (ColName == 'EMAIL ADDRESS') {

                            EMAIL_ADDRESS = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (EMAIL_ADDRESS == undefined || EMAIL_ADDRESS == '&nbsp;') {
                                EMAIL_ADDRESS = '';
                            }
                        }
                        else if (ColName == 'REP CONTACT STATUS') {

                            REP_CONTACT_STATUS = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (REP_CONTACT_STATUS == undefined || REP_CONTACT_STATUS == '&nbsp;') {
                                REP_CONTACT_STATUS = '';
                            }
                        }
                        else if (ColName == 'REP JOB AREA') {

                            REP_JOB_AREA = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (REP_JOB_AREA == undefined || REP_JOB_AREA == '&nbsp;') {
                                REP_JOB_AREA = '';
                            }
                        }
                        else if (ColName == 'LAST PURCHASED DATE') {

                            LAST_PURCHASED_DATE = grd.rows[RowIndex].cells[i - 1].innerHTML;
                            if (LAST_PURCHASED_DATE == undefined || LAST_PURCHASED_DATE == '&nbsp;') {
                                LAST_PURCHASED_DATE = '';
                            }
                        }
                   

                    }
                }
           }

           if (CONTACT_STATUS != undefined && CONTACT_STATUS != '&nbsp;') {
                 $('#txtcontactstatus').val(CONTACT_STATUS);                
                }
                else {
                  $('#txtcontactstatus').val(''); 
                }
                           

                if (ACCOUNT_NUM != undefined && ACCOUNT_NUM != '&nbsp;') {
                    $('#txtaccountnum').val(ACCOUNT_NUM);      
                }
                else {
                    $('#txtaccountnum').val('');  
                }

                if (ACCOUNT_NAME != undefined && ACCOUNT_NAME != '&nbsp;') {
                   $('#txtaccountname').val((ACCOUNT_NAME).replace(/amp;/ig,''));   
                }
                else {
                    $('#txtaccountname').val('');  
                }

                if (MANAGED_GROUP != undefined && MANAGED_GROUP != '&nbsp;') {
                     $('#txtmanagegroup').val((MANAGED_GROUP).replace(/amp;/ig,''));     
                }
                else {
                    $('#txtmanagegroup').val('');   
                }

                if (BUYER_ORG != undefined && BUYER_ORG != '&nbsp;') {
                     $('#txtbuyerorg').val((BUYER_ORG).replace(/amp;/ig,''));    
                }
                else {
                     $('#txtbuyerorg').val('');   
                }


                if (KAM_NAME != undefined && KAM_NAME != '&nbsp;') {
                   $('#txtkamname').val((KAM_NAME).replace(/amp;/ig,''));    
                }
                else {
                     $('#txtkamname').val('');   
                }

                if (CONTACT_NUM != undefined && CONTACT_NUM != '&nbsp;') {
                     $('#txtcontactnum').val(CONTACT_NUM);
                }
                else {
                      $('#txtcontactnum').val('');
                }

                if (CONTACT_NAME != undefined && CONTACT_NAME != '&nbsp;') {
                    $('#txtcontactname').val((CONTACT_NAME).replace(/amp;/ig,''));   
                }
                else {
                   $('#txtcontactname').val('');
                }

                if (CONTACT_TYPE != undefined && CONTACT_TYPE != '&nbsp;') {
                    $('#txtcontacttype').val(CONTACT_TYPE);
                }
                else {
                      $('#txtcontacttype').val('');
                }

                if (CONTACT_PHONE != undefined && CONTACT_PHONE != '&nbsp;') {
                     $('#txtcontactphone').val(CONTACT_PHONE);
                }
                else {
                     $('#txtcontactphone').val('');
                }


                if (CONTACT_RECENCY != undefined && CONTACT_RECENCY != '&nbsp;') {
                   $('#txtcontactrecency').val(CONTACT_RECENCY);
                }
                else {
                   $('#txtcontactrecency').val('');
                }

                if (CONTACT_SALES_12M != undefined && CONTACT_SALES_12M != '&nbsp;') {
                      $('#txtcontactsales12').val(CONTACT_SALES_12M);
                }
                else {
                    $('#txtcontactsales12').val('');
                }

                if (FUNCTION != undefined && FUNCTION != '&nbsp;') {
                    $('#txtfunction').val((FUNCTION).replace(/amp;/ig,''));  
                }
                else {
                     $('#txtfunction').val('');
                }

                if (LAST_DISP_DATE != undefined && LAST_DISP_DATE != '&nbsp;') {
                   $('#txtlastdispdate').val(LAST_DISP_DATE);
                }
                else {
                    $('#txtlastdispdate').val('');
                }

                if (LAST_DISP_NOTE != undefined && LAST_DISP_NOTE != '&nbsp;') {
                    $('#txtlastdispnote').val((LAST_DISP_NOTE).replace(/amp;/ig,''));
                }
                else {
                     $('#txtlastdispnote').val('');
                }

                if (LAST_PURCHASED_DATE != undefined && LAST_PURCHASED_DATE != '&nbsp;') {
                    $('#txtlastpurchaseddate').val(LAST_PURCHASED_DATE);
                }
                else {
                   $('#txtlastpurchaseddate').val('');
                }

                if (EMAIL_ADDRESS != undefined && EMAIL_ADDRESS != '&nbsp;') {
                     $('#txtemailaddress').val((EMAIL_ADDRESS).replace(/amp;/ig,''));
                }
                else {
                    $('#txtemailaddress').val('');
                }

            
                if (REP_CONTACT_STATUS != undefined && REP_CONTACT_STATUS != '&nbsp;') {
                    $('#txtrepcontactstat').val(REP_CONTACT_STATUS);
                }
                else {
                    $('#txtrepcontactstat').val('');
                }

                if (REP_JOB_AREA != undefined && REP_JOB_AREA != '&nbsp;') {
                     $('#txtrepjobarea').val((REP_JOB_AREA).replace(/amp;/ig,''));
                }
                else {
                     $('#txtrepjobarea').val('');
                }

//            var purchaseddate= new Date(CheckString(Data.rows[0]['LAST PURCHASED DATE']));
//            // alert(purchaseddate);
//            if (CheckString(Data.rows[0]['LAST PURCHASED DATE']) !="")
//            {
//            $get('txtlastpurchaseddate').value=(purchaseddate.getMonth() + 1) + "/" +
//                                                                    purchaseddate.getDate() + "/" +
//                                                                   purchaseddate.getFullYear();
//            }

//            $get('txtcontactstatus').value= CheckString(Data.rows[0]['CONTACT STATUS']);
//            $get('txtaccountnum').value= CheckString(Data.rows[0]['ACCOUNT NUM']);
//            $get('txtaccountname').value= CheckString(Data.rows[0]['ACCOUNT NAME']);
//            $get('txtmanagegroup').value= CheckString(Data.rows[0]['MANAGED GROUP']);
//            $get('txtbuyerorg').value= CheckString(Data.rows[0]['BUYER ORG']);
//            $get('txtkamname').value= CheckString(Data.rows[0]['KAM NAME']);
//            $get('txtcontactnum').value= CheckString(Data.rows[0]['CONTACT NUM']);
//            $get('txtcontactname').value= CheckString(Data.rows[0]['CONTACT NAME']);
//            $get('txtcontacttype').value= CheckString(Data.rows[0]['CONTACT TYPE']);
//            $get('txtcontactphone').value= CheckString(Data.rows[0]['CONTACT PHONE']);
//            $get('txtcontactrecency').value= CheckString(Data.rows[0]['CONTACT RECENCY']);
//            if(CheckString(Data.rows[0]['CONTACT SALES 12M'])!="")
//            {
//            $get('txtcontactsales12').value= '$ ' + CheckString(Data.rows[0]['CONTACT SALES 12M']).toFixed(2);
//            }
//            else
//            {
//             $get('txtcontactsales12').value="";
//            }
//            $get('txtfunction').value= CheckString(Data.rows[0]['FUNCTION']);
//            $get('txtlastdispdate').value= CheckString(Data.rows[0]['LAST DISP DATE']);
//            $get('txtlastdispnote').value= CheckString(Data.rows[0]['LAST DISP NOTE']);
//           // $get('txtlastpurchaseddate').value= CheckString(Data.rows[0]['LAST_PURCHASED_DATE']);
//            $get('txtemailaddress').value= CheckString(Data.rows[0]['EMAIL ADDRESS']);
//            $get('txtrepcontactstat').value= CheckString(Data.rows[0]['REP CONTACT STATUS']);
//            $get('txtrepjobarea').value= CheckString(Data.rows[0]['REP JOB AREA']);
           
        }


  function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
             //                    var modalPopup = $find('mpe').hide();
                    var mydiv = $('#pnlCustomer');
                        mydiv.dialog('close');
                }
     
          
           
           
        }

        function pageLoad(sender, args) {

           

            $("#<%=gridCustomerLookup.ClientID%>  tr:has(td)").hover(function () {
                $(this).css("cursor", "pointer");
            });

             $('#<%= btnSearch2.ClientID %>').click(function () {
            // alert("Test");
           $('#txtStartDate').val('');  
           //document.getElementById('txtStartDate').value = "";
              //$get('txtStartDate').value ="";
             });

            $("#gridCustomerLookup tr").not($("#gridCustomerLookup tr").eq(0)).click(function () {
             $addHandler(document, 'keydown', onKeypress);
                $("#gridCustomerLookup").find("INPUT[type='radio']").attr("checked", false);

                radio = $(this).find("INPUT[type='radio']");
                radio.closest('TR').addClass('SelectedRowStyle');
                var list = radio.closest('table').find("INPUT[type='radio']").not(radio);
                list.closest('TR').removeClass('SelectedRowStyle');
                radio.attr("checked", true);

                $("#hfIndex").val($(this).index());
                var grdIndex = ($(this).index());

               
                var totalCols = $("#gridCustomerLookup").find('tr')[0].cells.length;
                var totcol = 0;
          
                for (var i = 0; i < totalCols; i++) {
                    var CellValue = $(this).find("td:eq(" + i + ")").html();
                    
                    var ColName = $('#gridCustomerLookup tr').find('th:nth-child(' + i + ')').text();
         
                    if (ColName == 'ACCOUNT NUM') {
                    
                        AccntVal = $(this).find("td:eq(" + (i - 1) + ")").html();
                        globalAccntVal = $(this).find("td:eq(" + (i - 1) + ")").html();
                        // alert(AccntVal + " " + totcol);
                        totcol = 1;
                       
                    }

                    if (ColName == 'CONTACT NUM') {
                        var ContVal = $(this).find("td:eq(" + (i - 1) + ")").html();
                        globalcontactVal = $(this).find("td:eq(" + (i - 1) + ")").html();
                        totcol += 1;
                        // alert(ContVal + " " + totcol);
                    }

                    if (ColName == 'CONTACT NAME') {
                        var ContNameVal = $(this).find("td:eq(" + (i - 1) + ")").html();
                        globalcontactnameVal = $(this).find("td:eq(" + (i - 1) + ")").html();
                        totcol += 1;
                        // alert(ContVal + " " + totcol);
                    }


                    if (totcol > 2) {
                        //alert(totcol);
                        PageMethods.PassVal(AccntVal, ContVal, ContNameVal);

                        setCookie('CName', ContNameVal, 1);
                        setCookie('CNo', ContVal, 1);
                        //  alert(ContVal + ContNameVal);

                        break;
                    }

                }

                //alert(ContVal);
                if (AccntVal.length <= 10){

               

                //PageMethods.GetDatafromXMLDetails(AccntVal,ContVal,OnSuccessCustomer);

               OnSuccessCustomer($(this).index());

             

                var mydiv = $('#pnlCustomer');
                        mydiv.dialog({ autoOpen: false,
                            title: "Customer Details",
                            resizable: false,
                            width: 370,
                            dialogClass: "FixedPostion",
                            closeOnEscape: true,
                            open: function (type, data) {
                                $(this).parent().appendTo("form"); //won't postback unless within the form tag
                            },
                            
                        });

                    mydiv.dialog('open');

                    return false;
                    }
            });
            
           $("#" + $(radio).attr("id")).closest('TR').addClass('SelectedRowStyle');

                
//               $(document).ready(function () {

//                 $('#' + '<%=gridCustomerLookup.ClientID%>').tablesorter({
//                     widgets: ['zebra'],
//                     widgetZebra: { css: ["NormalRowStyle", "AltRowStyle"]} // css classes to apply to rows
//                  });
 
                //  $('#' + '<%=gridCustomerLookup.ClientID%>').tablesorterPager({ container: $("#pager"), size: 50, fixedHeight: true });
                  //});
                

             


        }


    </script>
    <div id="container">
        <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%"
            height="100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <td class="object-wrapper" style="height: 20px">
                                        <asp:LinkButton ID="btnExportToExcel2" runat="server" CssClass="LabelFont" OnClick="btn_Export2ExcelClick">Export to Excel</asp:LinkButton>
                                    </td>
                                    <td class="object-wrapper" style="height: 20px">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="lnkCustomerLookup" runat="server" CssClass="LabelFont" OnClientClick="window.open('../Home/ArrangeColumns.aspx?Data=lvwLookupData','mywindow','width=700,height=400,scrollbars=yes')">Arrange Columns</asp:LinkButton>
                                    </td>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td align="left">
                                <div>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="27px" Width="751px"
                                        AutoPostBack="false" RepeatDirection="Horizontal" Font-Size="12px">
                                        <asp:ListItem Value=" Customer Name" Selected="True" />
                                        <asp:ListItem Value=" Phone Number" />
                                        <asp:ListItem Value=" Email Address" />
                                        <asp:ListItem Value=" Contact Name" />
                                        <asp:ListItem Value=" Managed Group" />
                                        <asp:ListItem Value=" Buyer Org" />
                                    </asp:RadioButtonList>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <%if (varkamid != "")
                  { %>
                <td align="right">
                    <table class="newQuery">
                        <tr>
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <td valign="middle">
                                            <asp:Panel ID="Panel3" runat="server" DefaultButton="QuerySearch">
                                                <div id="searchboxquery" style="padding-left: 5px; padding-top: 5px">
                                                    <div id="DateSince">
                                                        <asp:Label ID="Label1" Text="Find new buyers since " runat="server" ClientIDMode="Static"
                                                            Font-Names="Arial" Font-Size="12px"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtSearchQuery" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:ImageButton ID="QuerySearch" ClientIDMode="Static" CssClass="QuerySearch" ImageUrl="~/App_Themes/Images/New Design/magnifying-glass-search-icon.jpg"
                                                        runat="server" ToolTip="Search" OnClick="QuerySearch_Click" />
                                                </div>
                                            </asp:Panel>
                                        </td>
                                        <td valign="middle">
                                            <asp:ImageButton ID="imgEndCal" Width="17px" Height="20px" border="0" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                                runat="server" />
                                        </td>
                                        <td valign="middle" style="padding-right: 3px">
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate"
                                                PopupButtonID="imgEndCal">
                                            </asp:CalendarExtender>
                                        </td>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </tr>
                    </table>
                    <%-- <table class="newQuery">
                        <tr>
                            <td>
                                <asp:Label ID="Label3" Text="New buyer query" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                        </tr>
                    </table>--%>
                </td>
                <% }%>
            </tr>
            <tr>
                <td valign="middle">
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch2">
                        <div id="searchboxdiv" style="padding-left: 5px; padding-top: 5px">
                            <div id="label2">
                                Value</div>
                            <asp:TextBox ID="txtSearch2" runat="server" ClientIDMode="Static" CssClass="txtSearch">
                            </asp:TextBox>
                            <asp:ImageButton ID="btnSearch2" ClientIDMode="Static" CssClass="btnSearch" ImageUrl="~/App_Themes/Images/New Design/magnifying-glass-search-icon.jpg"
                                runat="server" ToolTip="Search Account" OnClick="btnSearch_Click1" />
                            <div class="clear">
                                <br />
                            </div>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <div id="lookuptable" runat="server" clientidmode="Static" class="table-wrapper page1">
            <table border="0" cellpadding="10" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upNotesHistory" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="PnlNotesCommHistory" runat="server" ScrollBars="Auto" Height="100%"
                                    Width="100%">
                                    <asp:GridView AutoGenerateColumns="True" ClientIDMode="Static" ID="gridCustomerLookup"
                                        DataKeyNames="Account Num,Contact Num" runat="server" AllowSorting="true" AllowPaging="true" PageSize="50"
                                        BackColor="#FFFFFF" BorderColor="#999999" CellPadding="4" CellSpacing="2" ForeColor="Black"
                                        Font-Size="12px" OnRowDataBound="gridCustomerLookup_RowDataBound" OnPageIndexChanging="CustomerLookUpPageChanging"
                                        OnSelectedIndexChanged="gridCustomerLookup_SelectedIndexChanged" EmptyDataText="No Data Available"
                                        GridLines="None" EnablePersistedSelection="true"  OnSorting="GridView1_Sorting">                                        
                                        <PagerStyle CssClass="PagerStyle" />
                                        <AlternatingRowStyle BackColor="#e5e5e5" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" Wrap="false" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <PagerStyle CssClass="grid_paging" />                                    
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:RadioButton runat="server" ID="rdbUser" onclick="javascript:SelectSingleRadiobutton(this.id)"
                                                        OnSelectedIndexChanged="gridCustomerLookup_SelectedIndexChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </asp:Panel>
                               
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExportToExcel2" />
                                <asp:AsyncPostBackTrigger ControlID="gridCustomerLookup" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--Popup Dialog for Customer Details--%>
    <asp:Panel ID="pnlCustomer" runat="server" Style="display: none;" ClientIDMode="Static">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblcontactstatus" CssClass="LabelFont" runat="server" Text="Contact Status"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontactstatus" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblaccountnnum" CssClass="LabelFont" runat="server" Text="Account Number"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtaccountnum" CssClass="textboxLabel" Style="font-size: 12px; width: 200px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblaccountname" CssClass="LabelFont" runat="server" Text="Account Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtaccountname" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblmanagegroup" Width="100%" CssClass="LabelFont" runat="server" Text="Manage Group"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmanagegroup" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" Width="100%" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblbuyerorg" CssClass="LabelFont" runat="server" Text="Buyer Org."></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtbuyerorg" CssClass="textboxLabel" Style="font-size: 12px; width: 200px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblkamname" CssClass="LabelFont" runat="server" Text="KAM Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtkamname" CssClass="textboxLabel" Style="font-size: 12px; width: 200px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcontactnum" CssClass="LabelFont" runat="server" Text="Contact Number"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontactnum" CssClass="textboxLabel" Style="font-size: 12px; width: 200px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcontactname" CssClass="LabelFont" runat="server" Text="Contact Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontactname" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcontacttype" CssClass="LabelFont" runat="server" Text="Contact Type"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontacttype" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcontactphone" CssClass="LabelFont" runat="server" Text="Contact Phone"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontactphone" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcontactrecency" CssClass="LabelFont" runat="server" Text="Contact Recency"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontactrecency" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcontactsales12" CssClass="LabelFont" runat="server" Text="Contact Sales 12M"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontactsales12" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblfunction" CssClass="LabelFont" runat="server" Text="Function"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtfunction" CssClass="textboxLabel" Style="font-size: 12px; width: 200px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbllastdispdate" CssClass="LabelFont" runat="server" Text="Last Disp Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlastdispdate" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbllastdispnote" CssClass="LabelFont" runat="server" Text="Last Disp Note"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlastdispnote" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblpurchaseddate" CssClass="LabelFont" runat="server" Text="Last Purchased Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlastpurchaseddate" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblemailadd" CssClass="LabelFont" runat="server" Text="Email Address"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtemailaddress" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblrepcontactstatus" CssClass="LabelFont" runat="server" Text="Rep Contact Status"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtrepcontactstat" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 200px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblrepjobarea" CssClass="LabelFont" runat="server" Text="Rep Job Area"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtrepjobarea" CssClass="textboxLabel" Style="font-size: 12px; width: 200px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%-- -----------------------------------------------------------------------------------------------------------------%>
   <%-- <center>
        <table style="padding-left: 20px; margin: auto; height: 400px;">
            <tr>
                <td style="margin: auto; height: 400px;">
                    <div align="left">
                        <%-- gridview
                    </div>
                </td>
            </tr>
        </table>--%>
</asp:Content>
