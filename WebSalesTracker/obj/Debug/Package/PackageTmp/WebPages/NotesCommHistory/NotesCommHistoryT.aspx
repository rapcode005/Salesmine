<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master"
    AutoEventWireup="true" CodeBehind="NotesCommHistoryT.aspx.cs" Inherits="WebSalesMine.WebPages.Notes_CommHistory.NotesCommHistoryT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="AppLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="container" runat="server">
    <meta http-equiv="Cache-Control" content="no-cache" />
    <!-- jQuery Library Ends Here -->
    <meta http-equiv="Cache-Control" content="no-cache" />
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
    <script language="javascript" type="text/javascript">

        //-------Note DatetimePicker------------



        //-------------------------

       
          function OnSuccess(Data) {
            
            var createdon = new Date(CheckString(Data.rows[0]['DATE']));
            var date= new Date(CheckString(Data.rows[0]['SCHEDULED DATE']));

            if (CheckString(Data.rows[0]['DATE']) !="")
            {
            $get('txtcreatedon').value=(createdon.getMonth() + 1) + "/" +
                                                                    createdon.getDate() + "/" +
                                                                   createdon.getFullYear();
            }

            $get('txtcreatedby').value= CheckString(Data.rows[0]['CREATED BY']);
            $get('txtnotetype').value= CheckString(Data.rows[0]['NOTE TYPE']);
            $get('txtnote').value= CheckString(Data.rows[0]['NOTES']);
            
            if (CheckString(Data.rows[0]['SCHEDULED DATE']) !="")
            {
            $get('txtdate').value=(date.getMonth() + 1) + "/" +
                                                                    date.getDate() + "/" +
                                                                   date.getFullYear();
            }
            $get('txtaccountnumber').value= CheckString(Data.rows[0]['ACCOUNT NUMBER']);
            $get('txtcontactnumber').value= CheckString(Data.rows[0]['CONTACT NUMBER']);
            $get('txtdispositioncode').value= CheckString(Data.rows[0]['DISPOSITION CODE']);
            $get('txtdispositiondesc').value= CheckString(Data.rows[0]['DISPOSITION DESCRIPTION']);

        }


         function OnSuccessDialer(Data) {

            var contactdate= new Date(CheckString(Data.rows[0]['CONTACT DATE']));

            if (CheckString(Data.rows[0]['CONTACT DATE']) !="")
            {
            $get('txtcontactdate').value=(contactdate.getMonth() + 1) + "/" +
                                                                    contactdate.getDate() + "/" +
                                                                   contactdate.getFullYear();
            }

            $get('txtaccountnumberd').value= CheckString(Data.rows[0]['ACCOUNT NUMBER']);
            $get('txtlistname').value= CheckString(Data.rows[0]['LIST NAME']);

            $get('txtphone').value= CheckString(Data.rows[0]['PHONE NUMBER']);
            $get('txtdispositiond').value= CheckString(Data.rows[0]['DISPOSITION']);
            $get('txtdescriptiond').value= CheckString(Data.rows[0]['DESCNAME']);
            $get('txtagentlogin').value= CheckString(Data.rows[0]['AGENT LOGIN']);
            $get('txtagentname').value= CheckString(Data.rows[0]['AGENT NAME']);


        }

        function pageLoad() {
           


            $(document).ready(function () {
               $addHandler(document, 'keydown', onKeypress);
                $("#grdNotesHistory  tr:has(td)").hover(function () {

                    $(this).css("cursor", "pointer");
                });

                $("#grdDialerHistory  tr:has(td)").hover(function () {

                    $(this).css("cursor", "pointer");
                });

                $("#grdNotesHistory tr").not($("#grdNotesHistory tr").eq(0)).click(function () {

                    $("#grdDialerHistory  tr").closest('TR').removeClass('SelectedRowStyle');
                    $("#grdNotesHistory  tr").closest('TR').removeClass('SelectedRowStyle');
                    $(this).addClass('SelectedRowStyle');

                    var totalnotescols = $("#grdNotesHistory").find('tr')[0].cells.length;

                    
                    //alert(totalnotescols);
               for (var i = 0; i < totalnotescols + 1; i++) {
                    var ColName = $('#grdNotesHistory tr').find('th:nth-child(' + i + ')').text();
//              

                   if (ColName == 'Row') {
                  // alert($(this).find("td:eq(" + (i - 1) + ")").html());
                        var RowNum = $(this).find("td:eq(" + (i - 1) + ")").html();
                       //var RowNum=$(this).index();
                       // alert(RowNum);
                   }
                    
               }
                PageMethods.GetDatafromXMLDetails(RowNum,OnSuccess);
                var mydiv2 = $('#pnlDialerDetails');
                mydiv2.dialog('close');
                     var mydiv = $('#pnlNotesDetails');
                        mydiv.dialog({ autoOpen: false,
                            title: "Notes Details",
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

            });




             $("#grdDialerHistory tr").not($("#grdDialerHistory tr").eq(0)).click(function () {
                     $("#grdNotesHistory  tr").closest('TR').removeClass('SelectedRowStyle');
                    $("#grdDialerHistory  tr").closest('TR').removeClass('SelectedRowStyle');
                    $(this).addClass('SelectedRowStyle');

                    var totaldialcols = ($("#grdDialerHistory").find('tr')[0].cells.length + 1);

                    
                   
                for (var i = 0; i < totaldialcols; i++) {
                   var ColName = $('#grdDialerHistory tr').find('th:nth-child(' + i + ')').text();                   
                    if (ColName == 'Row') {

                        var RowDialNum = $(this).find("td:eq(" + (i - 1) + ")").html();
                   
                         
                    }
                    
                }
                PageMethods.GetDatafromXMLDialerDetails(RowDialNum,OnSuccessDialer);
                var mydiv2 = $('#pnlNotesDetails');
                mydiv2.dialog('close');
                     var mydiv = $('#pnlDialerDetails');
                        mydiv.dialog({ autoOpen: false,
                            title: "Dialer Details",
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

            });

            });
        }

        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
              
                   
       var mydiv = $('#pnlNotesDetails');
       mydiv.dialog('close');
               
       var mydiv2 = $('#pnlDialerDetails');
       mydiv2.dialog('close');

       var mydiv3 = $('#pnlExportToExcel');
       mydiv3.dialog('close');
                        
            }
        }
        function Information() {
            alert("Successsfully Saved");
            var modalPopup = $find('mpe').hide();
        }

        function onCalendarShown(sender, args) {
            
            sender._popupBehavior._element.style.zIndex = 10005;
        }
       

        function onCalendarHidden(sender, args) {
            sender._popupBehavior._element.style.zIndex = 0;
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
            //alert(tips);
            tips
				.text(t)
				.addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }

        function CheckString(val) {
            if (val)
                return val;
            else
              
                return "";
        }


        function CheckDecimal(val) {
       
            if (val!=0){
                return val;
                }
            else{
                return "";
        }

        }

        //---------------------Note Character Limit------------
        $(document).ready(function () {
            $('#AddNote').keyup(function () {
                var len = this.value.length;

                if (len >= 150) {
                    this.value = this.value.substring(0, 150);
                }

                $('#CharterLeft').text(150 - len);
            });
        });



//        $(document).ready(function () {
//         $addHandler(document, 'keydown', onKeypress);

//         
//            $('#btnExportToExcel2').click(function () {
//             var contname = CheckString(getCookie("CNo"));
//             alert("test");
//                                var startdate = CheckString($get("<%=txtStartDate.ClientID%>").value); //$('#txtStartDate').value;
//                                var enddate = CheckString($get("<%=txtEndDate.ClientID%>").value);
//                                var bydate = $get('ByDate').checked;
//                                var notetype = CheckString($get("<%=ddlNoteType.ClientID%>").value);
//                                var camp = document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value;

//                                var logaccount = "<%=SessionFacade.LoggedInUserName%>" + "NoteHistory.xls";

//                                PageMethods.export2(true, false, camp, contname, startdate, enddate, bydate, notetype);
//                                alert("Notes was successfully export");
//                                var winref = window.open('../Home/Export2ExcelPage.aspx?PageName=NoteHistorySummary&FilePath="' + logaccount + '"', '_parent', 'toolbar=0,statusbar=0,height=400,width=500');


//                var mydiv = $('#pnlExportToExcel');

//                mydiv.dialog({ autoOpen: false,
//                    title: "Select Table",
//                    resizable: false,
//                    width: "auto",
//                    dialogClass: "FixedPostion",
//                    open: function (type, data) {
//                        $(this).parent().appendTo("form"); //won't postback unless within the form tag
//                    },
//                    modal: false,
//                    buttons: {
//                        "Ok": function () {




//                            if ($get('rdoDialerData2').checked == true) {

//                                var contname = CheckString(getCookie("CNo"));

//                                var startdate = CheckString($get("<%=txtStartDate.ClientID%>").value); //$('#txtStartDate').value;
//                                var enddate = CheckString($get("<%=txtEndDate.ClientID%>").value);
//                                var bydate = $get('ByDate').checked;
//                                var notetype = CheckString($get("<%=ddlNoteType.ClientID%>").value);
//                                var camp = document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value;

//                                var logaccount = "<%=SessionFacade.LoggedInUserName%>" + "DialerData.xls";



//                                PageMethods.export2(false, true, camp, contname, startdate, enddate, bydate, notetype);

//                                alert("Dialer was successfully export");
//                                var winref = window.open('../Home/Export2ExcelPage.aspx?PageName=DialerDataSummary&FilePath="' + logaccount + '"', '_parent', 'toolbar=0,statusbar=0,height=400,width=500');
//                                mydiv.dialog('close');


//                            }
//                            if ($get('rdoNotesHistory').checked == true) {


//                                var contname = CheckString(getCookie("CNo"));
//                                var startdate = CheckString($get("<%=txtStartDate.ClientID%>").value); //$('#txtStartDate').value;
//                                var enddate = CheckString($get("<%=txtEndDate.ClientID%>").value);
//                                var bydate = $get('ByDate').checked;
//                                var notetype = CheckString($get("<%=ddlNoteType.ClientID%>").value);
//                                var camp = document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value;

//                                var logaccount = "<%=SessionFacade.LoggedInUserName%>" + "NoteHistory.xls";

//                                PageMethods.export2(true, false, camp, contname, startdate, enddate, bydate, notetype);
//                                alert("Notes was successfully export");
//                                var winref = window.open('../Home/Export2ExcelPage.aspx?PageName=NoteHistorySummary&FilePath="' + logaccount + '"', '_parent', 'toolbar=0,statusbar=0,height=400,width=500');
//                                
//                                mydiv.dialog('close');
//                            }


//                        },

//                        "Cancel": function () {
//                            mydiv.dialog('close');

//                        }
//                    }
 //               });

//                mydiv.dialog('open');
//                // $(this).parent().appendTo("form");
//                mydiv.parent().appendTo($("form:first"));

//                return false;
//            });



//        });

        
        $(document).ready(function () {
            $('#btnAddNote2').click(function () {
                var mydivAddNotes = $('#pnlAddNotes');
                mydivAddNotes.dialog({ autoOpen: false,
                    title: "Add Note",
                    resizable: false,
                    width: "auto",
                    dialogClass: "FixedPostion",
                    open: function (type, data) {
                        $(this).parent().appendTo("form"); //won't postback unless within the form tag
                    },
                    modal: true,
                    buttons: {
                        "Ok": function () {
                            // alert("OK");
                            var bValid = true;

                            // alert(bValid);
                            var notedate = $("#txtNoteStartDate"); //$get('txtNoteStartDate').value;
                            var notetype = $("#NoteTypes"); //$get('NoteTypes').value;
                            var textnote = $("#AddNote"); //$get('AddNote').value;
                            allFields = $([]).add(notedate).add(notetype).add(textnote);

                            allFields.removeClass("ui-state-error");

                            bValid = bValid && checkLength(notetype, "Note Type", 3, 25);
                            bValid = bValid && checkLength(notedate, "Note Date", 10, 10);
                            bValid = bValid && checkLength(textnote, "Note", 1, 150);

                            // alert(bValid);
                            if (bValid) {

                                var notedate = $get('txtNoteStartDate').value;
                                var notetype = $get('NoteTypes').value;
                                var textnote = $get('AddNote').value;
                                PageMethods.AddNote2(notedate, notetype, textnote);
                                alert("Successsfully Saved");
                                $(this).dialog("close");
                                __doPostBack('', '');
                            }
                        },

                        "Cancel": function () {
                            mydivAddNotes.dialog('close');
                            allFields.val("").removeClass("ui-state-error");


                        }
                    },
                    close: function () {
                        allFields.val("").removeClass("ui-state-error");
                    }

                });

                mydivAddNotes.dialog('open');



                //                mydiv.parent().appendTo($("form:first"));

                return false;
            });




        });


//        $(document).ready(function () {

//            var mydivJump = $('#pnlJump');
//            mydivJump.dialog({ autoOpen: true,
//                title: "Table Options",
//                resizable: false,
//                width: "auto",
//                height: 80,
//                draggable: false,
//                closeOnEscape: false,
//                position: ["right", "bottom"],
//                dialogClass: "FixedPostion",
//                open: function (type, data) {
//                    $(".ui-dialog-titlebar-close").hide();
//                    $(this).parent().appendTo("form"); //won't postback unless within the form tag
//                },
//                modal: false,
//                beforeclose: function () {
//                    return false;
//                }

//            });

//            mydivJump.dialog('open');

//        });

        $(document).ready(function () {
            $('#<%=lnkContactLevel.ClientID %>').click(function () {
                //                    if ($(this)[0].checked) {
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

            $('#<%= ImageSelectContact.ClientID %>').hover(function () {
                $(this).css("cursor", "pointer");
            });

            $('#<%= ImageSelectContact.ClientID %>').click(function () {
                setCookie('CName', "", -1);
                setCookie('CNo', "", -1);
                __doPostBack('= btnRefresh.ClientID ', '');
                return false;
            });

        });

    </script>
    <script language="javascript" type="text/javascript">
        var ld = (document.all);

        var ns4 = document.layers;
        var ns6 = document.getElementById && !document.all;
        var ie4 = document.all;

        if (ns4)
            ld = document.loading;
        else if (ns6)
            ld = document.getElementById("loading").style;

        function init() {
            if (ns4) { ld.visibility = "hidden"; }
            else if (ns6 || ie4) ld.display = "none";
        }

        

    </script>
    <div id="container">
        <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%"
            height="100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td class="object-wrapper" style="height: 20px">
                                            <asp:LinkButton ID="lnkContactLevel" runat="server" CssClass="LabelFont">Select Contact</asp:LinkButton>
                                            &nbsp;
                                            <img id="ImageSelectContact" src="../../App_Themes/Images/New Design/btn-red-x.gif"
                                                runat="server" height="10" border="0" />
                                            <asp:LinkButton ID="lnkContactSelected" Style="text-decoration: none; border-bottom: 1px solid red;"
                                                runat="server" Font-Names="Arial" ClientIDMode="Static" Font-Size="12px" ForeColor="Black"></asp:LinkButton>
                                        </td>
                                        <td class="object-wrapper" style="height: 20px">
                                            <asp:LinkButton ID="btnAddNote2" ToolTip="Click to Add Note" runat="server" CssClass="LabelFont"
                                                ClientIDMode="Static">Add Note</asp:LinkButton>
                                        </td>

                                          <%if (userRule != "PC-MAN" || userRule != "PC-ONT")
                                                    { %> 
                                        <td class="object-wrapper" style="height: 20px">
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="btnExportToExcel2" ToolTip="Click to Export to Excel" runat="server"
                                                CssClass="LabelFont" ClientIDMode="Static" OnClick="btn_Export2ExcelClick">Export to Excel</asp:LinkButton>
                                        </td>
                                         <% }%>
                                        <td class="object-wrapper" style="padding-right: 0; height: 20px;">
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="BtnNotesColumn2" runat="server" CssClass="LabelFont" OnClientClick="window.open('../Home/ArrangeColumns.aspx?Data=lvwNotesDataTer','mywindow','width=700,height=400,scrollbars=yes')">Arrange Notes History Columns</asp:LinkButton>
                                        </td>
                                           <td class="object-wrapper" style="height: 20px">
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="btnAllNotes2" runat="server" CssClass="LabelFont" OnClick="btnAllNotes_Click">Show All My Notes</asp:LinkButton>
                                        </td>
                                        <td class="object-wrapper" style="padding-right: 0; height: 20px; visibility:hidden;">
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="BtnDialerColumn2" runat="server" CssClass="LabelFont" OnClientClick="window.open('../Home/ArrangeColumns.aspx?Data=lvwDialerDataTer','mywindow','width=700,height=400,scrollbars=yes')">Arrange Dialer Data Columns</asp:LinkButton>
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
                    <asp:UpdatePanel ID="ByDateUpdate" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td class="object-wrapper" style="height: 20px;">
                                        <asp:CheckBox ID="ByDate" runat="server" ClientIDMode="Static" OnCheckedChanged="ByDate_CheckedChanged"
                                            AutoPostBack="True" />
                                    </td>
                                    <td class="object-wrapper" style="height: 20px; width: 58px; white-space: nowrap;">
                                        <asp:Label ID="Label4" Text="Start Date:" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td class="object-wrapper" style="height: 20px">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                            AutoPostBack="True" Style="width: 70px;"></asp:TextBox>
                                    </td>
                                    <td style="height: 20px; vertical-align: middle;">
                                        <asp:ImageButton ID="imgstartCal" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                            runat="server" />
                                    </td>
                                    <td class="object-wrapper" style="height: 20px">
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate"
                                            PopupButtonID="imgstartCal" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="object-wrapper" style="height: 48px; width: 58px; white-space: nowrap;">
                                        <asp:Label ID="Label3" CssClass="LabelFont" Text="End Date:" runat="server" />
                                    </td>
                                    <td class="object-wrapper" style="height: 48px">
                                        <asp:TextBox ID="txtEndDate" runat="server" Style="width: 70px;" ClientIDMode="Static"
                                            AutoPostBack="True" CssClass="textbox curved"></asp:TextBox>
                                    </td>
                                    <td class="object-wrapper" style="height: 48px">
                                        <asp:ImageButton ID="imgEndCal" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                            runat="server" />
                                    </td>
                                    <td class="object-wrapper" style="height: 48px">
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate"
                                            PopupButtonID="imgEndCal" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="object-wrapper" style="height: 20px; white-space: nowrap;">
                                        <asp:Label ID="Label5" Text="Note Type :" runat="server" CssClass="LabelFont" />
                                    </td>
                                    <td class="object-wrapper" style="padding-right: 0; height: 48px;">
                                        <asp:DropDownList ID="ddlNoteType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoteType_SelectedIndexChanged">
                                            <asp:ListItem> All </asp:ListItem>
                                            <asp:ListItem> SAP Tickler </asp:ListItem>
                                            <asp:ListItem> Dialer </asp:ListItem>
                                            <asp:ListItem> Follow Up </asp:ListItem>
                                            <asp:ListItem> Master Data </asp:ListItem>
                                            <asp:ListItem> Reminder </asp:ListItem>
                                            <asp:ListItem> Disposition Notes </asp:ListItem>
                                            <asp:ListItem> Other </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ByDate" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div class="GridHeaderLabel">
            NOTES HISTORY <a name="NoteHis"></a>
        </div>
        <div class="table-wrapper page1">
            <table border="0" cellpadding="10" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upNotesHistory" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel runat="server" ScrollBars="None" ID="DataDiv" Width="100%">
                                    <asp:GridView AutoGenerateColumns="True" ID="grdNotesHistory" runat="server" OnPageIndexChanging="NotesCommHistoryPageChanging"
                                        CellPadding="4" CellSpacing="2" ForeColor="Black" EmptyDataText="No data available."
                                        ClientIDMode="Static" BackColor="White" AllowSorting="True" AllowPaging="false"
                                        Font-Size="12px" OnSorting="grdNotesHistory_Sorting" GridLines="None" AsyncRendering="false"
                                        OnRowDataBound="grdNotesHistory_RowDataBound" Visible="False" OnSelectedIndexChanged="grdNotesHistory_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="#e5e5e5" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" Wrap="false" />
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <asp:Panel ID="pnlGridIndex" runat="server" CssClass="CssLabel" Visible="False">
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlNoteType" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ByDate" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div style="visibility: hidden">
            Clear</div>
        <table style="float: none; visibility:hidden;">
            <tr>
                <td>
                    <div class="GridHeaderLabel">
                        DIALER DATA <a name="Dial"></a>
                    </div>
                </td>
            </tr>
        </table>
        <div class="table-wrapper page1" style="visibility: hidden">
            <table border="0" cellpadding="10" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upDialerData" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel runat="server" ScrollBars="None" ID="PnlDialerHistory" Width="100%">
                                    <asp:GridView ID="grdDialerHistory" runat="server" OnPageIndexChanging="DialerDataPageChanging"
                                        ClientIDMode="Static" CellPadding="4" CellSpacing="2" ForeColor="Black" EmptyDataText="No data available."
                                        AllowPaging="false" PageSize="7" Font-Size="12px" GridLines="None" AllowSorting="True"
                                        BackColor="White" OnSorting="grdDialerHistory_Sorting" Visible="False" AutoGenerateColumns="True"
                                        OnSelectedIndexChanged="grdDialerHistory_SelectedIndexChanged" OnRowDataBound="grdDialerHistory_RowDataBound">
                                        <AlternatingRowStyle BackColor="#e5e5e5" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" Wrap="false" />
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlNoteType" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel3" runat="server" CssClass="CssLabel" Visible="False">
                <div class="CssErrorLabel">
                    <asp:Label ID="lblNoDialerData" runat="server" Text=" :  No Dialer Records Aviable"
                        Visible="false"></asp:Label>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div>
        <%---------------------Export To Excel-------------------------------%>
        <asp:UpdatePanel ID="NewContactUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlExportToExcel" ClientIDMode="Static" Style="display: none" runat="server"
                    CssClass="modalPopupMaster">
                    <table style="height: 50px; border: 3px;">
                        <tr>
                            <td>
                                <br />
                                <asp:RadioButton ID="rdoNotesHistory" runat="server" Text=" Notes History" GroupName="RdoExportFile"
                                    ClientIDMode="Static" CssClass="CssLabel" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rdoDialerData2" runat="server" Text=" Dialer Data" GroupName="RdoExportFile"
                                    ClientIDMode="Static" CssClass="CssLabel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%---------------------Add Note-------------------------------%>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlAddNotes" ClientIDMode="Static" Style="display: none" runat="server"
                    CssClass="modalPopupMaster">
                    <p class="validateTips">
                        All form fields are required.</p>
                    <%--<p>
                        Format options:<br />
                        <select id="format">
                            <option value="mm/dd/yy">Default - mm/dd/yy</option>
                            <option value="yy-mm-dd">ISO 8601 - yy-mm-dd</option>
                            <option value="d M, y">Short - d M, y</option>
                            <option value="d MM, y">Medium - d MM, y</option>
                            <option value="DD, d MM, yy">Full - DD, d MM, yy</option>
                            <option value="'day' d 'of' MM 'in the year' yy">With text - 'day' d 'of' MM 'in the
                                year' yy</option>
                        </select>
                    </p>--%>
                    <table>
                        <tr>
                            <td align="left">
                                Note Type :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="NoteTypes" runat="server" Height="20px" ValidationGroup="ResourceGroup"
                                    Font-Size="12px" Width="109px" ClientIDMode="Static">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem> Follow Up </asp:ListItem>
                                    <asp:ListItem> Reminder </asp:ListItem>
                                    <asp:ListItem> Other </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td class="style2">
                                Scheduled Date :
                            </td>
                            <td align="left">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" ClientIDMode="Static">
                                    <ContentTemplate>
                                        <div style="position: relative;">
                                            <asp:TextBox ID="txtNoteStartDate" runat="server" Width="100px" Font-Size="12px"
                                                ClientIDMode="Static"></asp:TextBox>
                                            &nbsp;
                                            <asp:ImageButton ID="imgstartCalNote" ImageUrl="../../App_Themes/Images/New Design/calendar-icon.png"
                                                runat="server" />
                                            <asp:CalendarExtender ID="NoteCalendar" runat="server" Format="MM/dd/yyyy" TargetControlID="txtNoteStartDate"
                                                PopupButtonID="imgstartCalNote" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
                                            </asp:CalendarExtender>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <div class="demo" style="padding: 0px 0px 0px 5px; width: 471px;">
                        <div style="font-size: small; font-family: Arial, Helvetica, sans-serif; padding: 0px 0px 0px 2px;
                            width: 452px;" align="left">
                            Please provide relevant notes about recent customer or contact interaction.
                        </div>
                        <asp:TextBox TextMode="MultiLine" name="usermessage" ID="AddNote" Height="118px"
                            Width="456px" runat="server" Font-Size="12px" ClientIDMode="Static"></asp:TextBox>
                        <div align="right" style="padding-right: 15px">
                            <asp:Label ID="CharterLeft" runat="server" ClientIDMode="Static" Text="150"></asp:Label>
                        </div>
                        <table>
                            <tr>
                                <td class="resRecbtnCell_ResourceEntry">
                                    <div style="font-size: x-small; font-family: Arial, Helvetica, sans-serif; padding: 0px 0px 0px 5px;
                                        width: 452px;" align="left">
                                        Examples :<br />
                                        &nbsp; Dave is interested in buying xyz products but need approval from his boss
                                        first.
                                        <br />
                                        &nbsp; Andy asked to be called again 1 week from now when he'll have budget approved.
                                        <br />
                                        &nbsp; Sarah not interested in buying from us. Company has consolidated purchases
                                        <br />
                                        &nbsp; and is buying from ABC Company.
                                        <br />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlJump" ClientIDMode="Static" Style="display: none" runat="server"
                    CssClass="modalPopupMaster">
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="LinkJumpDialer" ToolTip="Click to Add Note" runat="server" Font-Names="Arial"
                                    Font-Size="12px" ClientIDMode="Static"> <a href="#Dial" style="color:Red">Jump to Dialer Data</a></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="LinkJumpToNotes" ToolTip="Click to Add Note" runat="server" Font-Names="Arial"
                                    Font-Size="12px" ClientIDMode="Static"><a href="#NoteHis" style="color:Red">Jump to Notes History</a></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--Popup Dialog for Notes Details--%>
    <asp:Panel ID="pnlNotesDetails" runat="server" Style="display: none;" ClientIDMode="Static">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblcreatedon" CssClass="LabelFont" runat="server" Text="Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcreatedon" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcreatedby" CssClass="LabelFont" runat="server" Text="Created by"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcreatedby" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblnotetype" CssClass="LabelFont" runat="server" Text="Note Type"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtnotetype" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblnote" Width="100%" CssClass="LabelFont" runat="server" Text="Note"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtnote" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        Wrap="true" TextMode="MultiLine" Width="100%" ReadOnly="true" ClientIDMode="Static"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldate" CssClass="LabelFont" runat="server" Text="Scheduled Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdate" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblaccountnum" CssClass="LabelFont" runat="server" Text="Account Number"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtaccountnumber" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcontactnumber" CssClass="LabelFont" runat="server" Text="Contact Number"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontactnumber" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldispositioncode" CssClass="LabelFont" runat="server" Text="Disposition Code"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdispositioncode" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldispositiondesc" CssClass="LabelFont" runat="server" Text="Disposition Desc."></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdispositiondesc" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--Popup Dialog for Dialer Details--%>
    <asp:Panel ID="pnlDialerDetails" runat="server" Style="display: none;" ClientIDMode="Static">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblaccountnumd" CssClass="LabelFont" runat="server" Text="Account Number"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtaccountnumberd" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbllistd" CssClass="LabelFont" runat="server" Text="List Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlistname" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcontactdate" CssClass="LabelFont" runat="server" Text="Contact Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcontactdate" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblphone" Width="100%" CssClass="LabelFont" runat="server" Text="Phone Number"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtphone" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        Width="100%" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldispositiond" CssClass="LabelFont" runat="server" Text="Disposition"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdispositiond" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbldescription" CssClass="LabelFont" runat="server" Text="Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtdescriptiond" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblagentlogin" CssClass="LabelFont" runat="server" Text="Agent Login"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtagentlogin" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblagentname" CssClass="LabelFont" runat="server" Text="Agent Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtagentname" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
