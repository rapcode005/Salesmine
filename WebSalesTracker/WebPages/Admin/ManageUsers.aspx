<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="WebSalesMine.WebPages.Admin.ManageUsers"
    MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="T2" runat="server" ContentPlaceHolderID="container">
    <style type="text/css">
        .DisplayNone
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        var xPos, yPos;
        var FIREFOX = /Firefox/i.test(navigator.userAgent);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        function Save() {
            alert('Successsfully Saved');

        }

        function SaveScreen() {
            var mydiv = $('#pnlPopupAdmin');
            mydiv.dialog('close');
        }

        function SaveMutiple() {
            var mydiv = $('#pnlMutipleUser');
            mydiv.dialog('close'); 
        }

        function onsuccessShow(Data) {
            var column = Data.tables[0];
            var chkPageList = $get('chkPageList');
            var chkBox = chkPageList.getElementsByTagName("input");
            var chkCampaignList = $get('chkCampaignList');
            var chkBoxCam = chkCampaignList.getElementsByTagName("input");
            var ListPage;
            var ListCampaign;

            if (column.rows != null) {
                ListPage = column.rows[0]['PageName'].split(",");
                ListCampaign = column.rows[0]['CampaignValue'].split(",");

                //Check Page Selected
                for (var i = 0; i < chkBox.length; i++) {
                    for (var y = 0; y < ListPage.length; y++) {
                        if (chkBox[i].value.toUpperCase().trim() == ListPage[y].toUpperCase().trim()) {
                            chkBox[i].checked = true;
                            break;
                        }
                    }
                }

                //Check Campaign Selected
                for (var i = 0; i < chkBoxCam.length; i++) {
                    for (var y = 0; y < ListCampaign.length; y++) {
                        if (chkBoxCam[i].value.toUpperCase().trim() == ListCampaign[y].toUpperCase().trim()) {
                            chkBoxCam[i].checked = true;
                            break;
                        }
                    }
                }
            }
            else {

                //Check Page Selected
                for (var i = 0; i < chkBox.length; i++) {
                    chkBox[i].checked = false;
                }

                //Check Campaign Selected
                for (var i = 0; i < chkBoxCam.length; i++) {
                    chkBoxCam[i].checked = false;
                }

            }

        }

        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                {
                    var modalPopup = $find('mpe').hide();
                }
            }
        }


        function pageLoad() {
            $addHandler(document, 'keydown', onKeypress);
            $(document).ready(function () {

                //For Admin Table
                $("#grdError tr").not($("#grdError tr").eq(0)).click(function () {
                    var grd = document.getElementById('grdError');
                    var totalCols = $("#grdError").find('tr')[0].cells.length;
                    var RowIndex = $(this).index();

                    for (var i = 1; i < totalCols + 1; i++) {
                        var ColName = $('#grdError tr').find('th:nth-child(' + i + ')').text();

                        ColName = ColName.trim();

                        if (ColName == 'Campaign') {
                            
                            if(grd.rows[RowIndex] != null )
                            {
                                if (FIREFOX)
                                    $get('CampaignValuePrevious').value = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    $get('CampaignValuePrevious').value = grd.rows[RowIndex].cells[i - 1].innerText;

                                if ($get('CampaignValuePrevious').value == undefined || $get('CampaignValuePrevious').value == '&nbsp;') {
                                    $get('CampaignValuePrevious').value = '';
                            
                            }
                            else
                                $get('CampaignValuePrevious').value = '';
                        }

                        else if (ColName == 'User Role') {
                            
                            if(grd.rows[RowIndex] != null )
                            {
                                if (FIREFOX)
                                    $get('UserRolePrevious').value = grd.rows[RowIndex].cells[i - 1].textContent;
                                else
                                    $get('UserRolePrevious').value = grd.rows[RowIndex].cells[i - 1].innerText;

                                if ($get('UserRolePrevious').value == undefined || $get('UserRolePrevious').value == '&nbsp;') {
                                    $get('UserRolePrevious').value = '';
                                }
                            }
                            else
                                $get('UserRolePrevious').value = '';
                        }


                    }
                });


                //For Note Type Table
                $("#grdNoteTypeList tr").not($("#grdNoteTypeList tr").eq(0)).click(function () {

                    var grd = document.getElementById('grdNoteTypeList');
                    var totalCols = $("#grdNoteTypeList").find('tr')[0].cells.length;
                    var RowIndex = $(this).index();
                    var NoteType = '';

                    $get('NoteTypeValues').value = '';

                    for (var i = 3; i < totalCols + 1; i++) {
                        var ColName = $('#grdError tr').find('th:nth-child(' + i + ')').text();
                        ColName = ColName.trim();

                        if (FIREFOX)
                            NoteType = NoteType + grd.rows[RowIndex].cells[i - 1].textContent.trim() + '|';
                        else
                            NoteType = NoteType + grd.rows[RowIndex].cells[i - 1].innerText.trim() + '|';
                    }

                    //Remove last characters
                    NoteType = NoteType.substring(0, NoteType.length - 1);

                    $get('NoteTypeValues').value = NoteType;
                });


                $('#txtNtLoginMutiple').keypress(function (event) {
                    if (event.keyCode == 13) {
                        $("#btnAddNtLoginMutiple").focus();
                        $("#btnAddNtLoginMutiple").click();
                        return false;
                    }
                });

                $('#txtSalesTeamMutiple').keypress(function (event) {
                    if (event.keyCode == 13) {
                        $("#btnAddSalesTeamMutiple").focus();
                        $("#btnAddSalesTeamMutiple").click();
                        return false;
                    }
                });

                $('#txtFullNameMutiple').keypress(function (event) {
                    if (event.keyCode == 13) {
                        $("#btnAddFullNameMutiple").focus();
                        $("#btnAddFullNameMutiple").click();
                        return false;
                    }
                });

                $('#txtToEmail').keyup(function (event) {
                    if (event.keyCode == 188) {
                        var str = $(this).val();
                        //Replace all space to blank
                        str = str.replace(/\ /g, "");
                        //Add space after comma
                        $get('txtToEmail').value = str.replace(/\,/g, ", ");
                    }
                });

                //Adjust the height based on text
                $('#txtToEmail').keyup(function (event) {
                    var textLength = $("#txtToEmail").val().length;
                    if (textLength % 50 == 0) {
                        var height = textLength / 50;
                        $("#txtToEmail").css('height', 20 + (height * 20));
                    }
                });

                $('#txtCCEMail').keyup(function (event) {
                    if (event.keyCode == 188) {
                        var str = $(this).val();
                        //Replace all space to blank
                        str = str.replace(/\ /g, "");
                        //Add space after comma
                        $get('txtCCEMail').value = str.replace(/\,/g, ", ");
                    }
                });

                //Adjust the height based on text
                $('#txtCCEMail').keyup(function (event) {
                    var textLength = $("#txtCCEMail").val().length;
                    if (textLength % 50 == 0) {
                        var height = textLength / 50;
                        $("#txtCCEMail").css('height', 15 + (height * 15));
                    }
                });


                $('#DropDownListChoose').click(function (e) {

                    var mydiv = $('#pnlPopupAdmin');
                    mydiv.dialog({ autoOpen: false,
                        title: "Customerized View",
                        resizable: false,
                        width: "auto",
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                            $get('Username').value = $get('txtUSERNAME').value;
                            $get('CampaignName').value = $("#DropDownList1 option:selected").text();
                            $get('CampaignValue').value = $get('DropDownList1').value;
                            PageMethods.GetAllCustomizedPage(onsuccessShow);

                        }
                    });

                    mydiv.dialog('open');
                });

                $('#btnAddMutipleUser').click(function (e) {

                    var mydiv = $('#pnlMutipleUser');
                    mydiv.dialog({ autoOpen: false,
                        title: "Add Mutiple User",
                        resizable: false,
                        width: "auto",
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        }
                    });

                    mydiv.dialog('open');
                });

                $('#btnEmail').click(function (e) {

                    var mydiv = $('#pnlPopupEmail');

                    mydiv.dialog('close');

                    mydiv.dialog({ autoOpen: false,
                        title: "Send Email",
                        resizable: false,
                        width: "auto",
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        open: function (type, data) {
                            $get('txtToEmail').value = "";
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        }
                    });

                    mydiv.dialog('open');
                });

                $('#lnkCampaignChoose').click(function (e) {

                    var mydiv = $('#pnlPopupAdmin');
                    mydiv.dialog({ autoOpen: false,
                        title: "Customerized View",
                        resizable: false,
                        width: "auto",
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                            $get('Username').value = "M";
                            $get('CampaignName').value = $("#ddlCampaignMutiple option:selected").text();
                            $get('CampaignValue').value = $get('ddlCampaignMutiple').value;
                            var chkBoxCam = chkCampaignList.getElementsByTagName("input");
                            for (var i = 0; i < chkBoxCam.length; i++) {
                                if (chkBoxCam[i].value.toUpperCase().trim() == $get('CampaignValue').value.toUpperCase().trim()) {
                                    chkBoxCam[i].checked = true;
                                }
                                else
                                    chkBoxCam[i].checked = false;
                            }
                        }
                    });

                    mydiv.dialog('open');
                });



                $('#lnkCampaignList').click(function (e) {

                    var mydiv = $('#pnlPopupCampaignList');
                    mydiv.dialog({ autoOpen: false,
                        title: "Customerized View",
                        resizable: false,
                        width: "auto",
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        }
                    });

                    mydiv.dialog('open');
                });


                $('#lnkNoteType').click(function (e) {

                    var mydiv = $('#pnlPopupNoteType');
                    mydiv.dialog({ autoOpen: false,
                        title: "Manage Notetype",
                        resizable: false,
                        width: 1000,
                        height: "auto",
                        dialogClass: "FixedPostion",
                        closeOnEscape: true,
                        open: function (type, data) {
                            $(this).parent().appendTo("form"); //won't postback unless within the form tag
                        }
                    });

                    mydiv.dialog('open');
                });

            });
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

        function fetch_user() {
            var email = document.getElementById('<%= txtbLdapEmail.ClientID %>').value;
            if (email == "") {
                alert("Enter valid email address");
                document.getElementById('<%= txtbLdapEmail.ClientID %>').focus();
                return false;
            }
        }


        function ShowManageNoteType() {
            var mydiv = $('#pnlPopupNoteType');
            mydiv.dialog({ autoOpen: false,
                title: "Manage Notetype",
                resizable: false,
                width: 1000,
                height: "auto",
                dialogClass: "FixedPostion",
                closeOnEscape: true,
                open: function (type, data) {
                    $(this).parent().appendTo("form"); //won't postback unless within the form tag
                }
            });

            mydiv.dialog('open');
        }

    </script>
    <div id="container">
        <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%"
            height="100%">
            <tr>
                <td style="width: 265px">
                    <table>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td class="object-wrapper" style="height: 20px">
                                                    <asp:LinkButton ID="btnAdd2" runat="server" CssClass="LabelFont" OnClick="btnAdd_Click"
                                                        PostBackUrl="~/WebPages/Admin/ManageUsers.aspx">Add New User</asp:LinkButton>
                                                </td>
                                                <td class="object-wrapper" style="height: 20px">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="BtnGetLdapUser2" runat="server" CssClass="LabelFont" OnClick="BtnGetLdapUser_Click">Get Windows User ID</asp:LinkButton>
                                                </td>
                                                <td class="object-wrapper" style="height: 20px">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnAddMutipleUser" runat="server" CssClass="LabelFont" ClientIDMode="Static">Add Mutiple User</asp:LinkButton>
                                                </td>
                                                <td class="object-wrapper" style="height: 20px">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnEmail" runat="server" CssClass="LabelFont" ClientIDMode="Static">Compose Email</asp:LinkButton>
                                                </td>
                                                <td class="object-wrapper" style="height: 20px">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkCampaignList" runat="server" CssClass="LabelFont" ClientIDMode="Static">Add Campaign</asp:LinkButton>
                                                </td>
                                                <td class="object-wrapper" style="height: 20px">
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnkNoteType" runat="server" CssClass="LabelFont" ClientIDMode="Static">Manage Note Type</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAddMutipleUser" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="BtnGetLdapUser2" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnAdd2" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnEmail" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="lnkCampaignList" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="lnkNoteType" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
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
                        <asp:Panel runat="server" ScrollBars="None" ID="DataDiv" Width="100%">
                            <asp:GridView ID="grdError" runat="server" AutoGenerateColumns="false" ClientIDMode="Static"
                                OnRowDataBound="grdError_RowDataBound" OnRowCancelingEdit="grdError_RowCancelingEdit"
                                OnRowEditing="grdError_RowEditing" ShowFooter="true" GridLines="None" AsyncRendering="false"
                                OnRowCommand="grdError_RowCommand" OnRowCreated="grdError_RowCreated" OnRowUpdating="grdError_RowUpdating"
                                OnRowDeleting="grdError_RowDeleting" EmptyDataText="No data available." CellPadding="4"
                                CellSpacing="2" Font-Size="12px" BackColor="#FFFFFF" ForeColor="Black" AllowPaging="false"
                                PageSize="100">
                                <AlternatingRowStyle BackColor="#e5e5e5" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                <RowStyle CssClass="RowStyle" Wrap="false" />
                                <FooterStyle CssClass="grid_pagingAdmin" />
                                <Columns>
                                    <asp:CommandField HeaderText="Edit-Update" ShowEditButton="True" ItemStyle-Width="1%" />
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            USERNAME
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false" Visible="true">
                                        <HeaderTemplate>
                                            LDAP/Windows NT UserName
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblddUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUSERNAME" runat="server" ClientIDMode="Static" Text='<%# Eval("USERNAME") %>'
                                                Width="100px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Campaign
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblddCampaign" runat="server" Text='<%# Eval("CampaignName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList1" runat="server" ClientIDMode="Static" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            User Role
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblddUserRole" runat="server" Text='<%# Eval("USERROLE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownListUserRole" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListUserRole_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="DropDownListChoose" runat="server" ClientIDMode="Static" Text="Open"
                                                CssClass="DisplayNone" OnClientClick="return false;">
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Sales Team ID
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("KamId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtbKamId" runat="server" Text='<%# Eval("KamId") %>' Width="200px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Full Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKamName" runat="server" Text='<%# Eval("KamName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtbKamName" runat="server" Text='<%# Eval("KamName") %>' Width="150px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusName" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblStatusName1" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false" Visible="false">
                                        <HeaderTemplate>
                                            Edit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnedit" runat="server" CommandName="Edit" Text="Edit" Visible="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" Text="Update"></asp:LinkButton>
                                            <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Delete
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span onclick="return confirm('Are you sure to Delete the record?')">
                                                <asp:LinkButton ID="btnDelete" runat="Server" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                            </span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <table style="padding-left: 20px;">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div class="CssErrorLabel">
                    <asp:Literal ID="litErrorinGrid" runat="server"></asp:Literal></div>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button Style="display: none" ID="btnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DragPanelExtender ID="drpeLdap" runat="server" TargetControlID="ratePanel" BehaviorID="drpeLdap"
                    DragHandleID="pnlDragable">
                </asp:DragPanelExtender>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="mpe" DynamicServicePath="" Enabled="True" PopupControlID="ratePanel"
                    RepositionMode="None" TargetControlID="btnHidden">
                </asp:ModalPopupExtender>
                <asp:Panel ID="ratePanel" runat="server" CssClass="modalPopup">
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlDragable" runat="server" Style="background: #2e73af url(  '../Images/top-bg.gif' ) repeat-x top;">
                                    <asp:Label ID="LTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                        Font-Underline="False" ForeColor="White" Width="450px" Text="LDAP/Windows NT Id:"></asp:Label>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            Brady Email Id:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtbLdapEmail" runat="server" Width="180px"></asp:TextBox>
                                            &nbsp;
                                            <asp:LinkButton ID="lnkFetch" runat="server" Text="Get LDAP ID" OnClick="lnkFetch_Click"
                                                OnClientClick="javascript:return fetch_user()"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            First/Last Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtbLdapFirstName" runat="server" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            LDAP/Windows NT Id:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtbLdapuserName" runat="server" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="CssErrorLabel">
                                                <asp:Literal ID="litLdapError" runat="server"></asp:Literal>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="float: right;">
                        <tr>
                            <td>
                                <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="button" ToolTip="Save User"
                                    Width="60px" ValidationGroup="ResourceGroup" OnClick="btnOk_Click1" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
                                    Width="60px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlMutipleUser" runat="server" Style="display: none;" ClientIDMode="Static">
        <asp:UpdateProgress ID="UpdateProgress1" runat="Server" AssociatedUpdatePanelID="UpdatePanel4"
            DisplayAfter="1">
            <ProgressTemplate>
                <asp:Label ID="lblLabel" CssClass="LabelFont" runat="server" Text="Please Wait..."
                    Style="font-size: 15px;" ForeColor="Red"></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblLabelNote" CssClass="LabelFont" runat="server" Text="It is important for those mutiple input that order matches to every fields."
                    Style="font-size: 15px;" Font-Bold="true"></asp:Label>
                <br />
                <table>
                    <tr>
                        <td style="padding-top: 10px;">
                            <asp:Label ID="lblNtLoginMutiple" CssClass="LabelFont" runat="server" Text="*LDAP/Windows NT UserName:"></asp:Label>
                        </td>
                        <td style="padding-top: 10px;">
                            <asp:ListBox ID="lbNtLoginMutiple" CssClass="textbox curved" runat="server" ClientIDMode="Static"
                                Style="font-size: 12px; height: 100%;"></asp:ListBox>
                            <asp:TextBox ID="txtNtLoginMutiple" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                Style="font-size: 12px;" MaxLength="10" TabIndex="1"></asp:TextBox>
                            <asp:Button ID="btnAddNtLoginMutiple" runat="server" Text="Add" CssClass="button"
                                class="button" Style="font-size: 12px; width: 60px;" ClientIDMode="Static" OnClick="btnAddNtLoginMutiple_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px;">
                            <asp:Label ID="lblSalesTeamMutiple" CssClass="LabelFont" runat="server" Text="Sales Team ID:"></asp:Label>
                        </td>
                        <td style="padding-top: 10px;">
                            <asp:ListBox ID="lbSalesTeamMutiple" CssClass="textbox curved" runat="server" ClientIDMode="Static"
                                Style="font-size: 12px;"></asp:ListBox>
                            <asp:TextBox ID="txtSalesTeamMutiple" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                Style="font-size: 12px;" MaxLength="10" TabIndex="2"></asp:TextBox>
                            <asp:Button ID="btnAddSalesTeamMutiple" runat="server" Text="Add" CssClass="button"
                                class="button" Style="font-size: 12px; width: 60px;" ClientIDMode="Static" OnClick="btnAddSalesTeamMutiple_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px;">
                            <asp:Label ID="lblFullNameMutiple" CssClass="LabelFont" runat="server" Text="Full Name:"></asp:Label>
                        </td>
                        <td style="padding-top: 10px;">
                            <asp:ListBox ID="lbFullNameMutiple" CssClass="textbox curved" runat="server" ClientIDMode="Static"
                                Style="font-size: 12px;"></asp:ListBox>
                            <asp:TextBox ID="txtFullNameMutiple" runat="server" CssClass="textbox curved" ClientIDMode="Static"
                                Style="font-size: 12px;" MaxLength="100" TabIndex="3"></asp:TextBox>
                            <asp:Button ID="btnAddFullNameMutiple" runat="server" Text="Add" CssClass="button"
                                class="button" Style="font-size: 12px; width: 60px;" ClientIDMode="Static" OnClick="btnAddFullNameMutiple_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px;">
                            <asp:Label ID="lblCampaignMutiple" CssClass="LabelFont" runat="server" Text="Campaign:"></asp:Label>
                        </td>
                        <td style="padding-top: 10px;">
                            <asp:DropDownList ID="ddlCampaignMutiple" CssClass="textbox curved" runat="server"
                                Style="font-size: 12px;" ClientIDMode="AutoID" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaignMutiple_SelectIndexChanged"
                                TabIndex="4">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px;">
                            <asp:Label ID="lblUserRoleMutiple" CssClass="LabelFont" runat="server" Text="User Role:"></asp:Label>
                        </td>
                        <td style="padding-top: 10px;">
                            <asp:DropDownList ID="ddlUserRoleMutiple" CssClass="textbox curved" runat="server"
                                EnableViewState="true" Style="font-size: 12px;" ClientIDMode="AutoID" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlUserRoleMutiple_SelectIndexChanged" TabIndex="6">
                            </asp:DropDownList>
                            <asp:LinkButton ID="lnkCampaignChoose" runat="server" ClientIDMode="Static" Text="Open"
                                CssClass="DisplayNone" OnClientClick="return false;" TabIndex="6"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="float: right;">
                    <tr>
                        <td>
                            <asp:Button ID="btnSaveMutiple" runat="server" Text="Save" CssClass="button" class="button"
                                Style="font-size: 12px; width: 60px;" OnClick="btnSaveMutiple_Click" TabIndex="8" />
                        </td>
                        <td>
                            <asp:Button ID="btnClearMutiple" runat="server" Text="Clear" CssClass="button" class="button"
                                Style="font-size: 12px; width: 60px;" OnClick="ClearListBox" ClientIDMode="Static"
                                TabIndex="9" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="btnSaveMutiple" EventName="Click" />--%>
                <asp:PostBackTrigger ControlID="btnSaveMutiple" />
                <asp:AsyncPostBackTrigger ControlID="btnClearMutiple" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAddFullNameMutiple" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAddSalesTeamMutiple" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAddNtLoginMutiple" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlCampaignMutiple" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlUserRoleMutiple" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlPopupAdmin" runat="server" Style="display: none;" ClientIDMode="Static">
        <asp:UpdateProgress ID="UpdateProgress2" runat="Server" AssociatedUpdatePanelID="UpdatePanel1"
            DisplayAfter="1">
            <ProgressTemplate>
                <asp:Label ID="lblLabel1" CssClass="LabelFont" runat="server" Text="Please Wait..."
                    Style="font-size: 15px;" ForeColor="Red"></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="float: left;">
                            <table style="border: 1px solid; float: left;">
                                <tr>
                                    <td style="background-color: #d6e0ec;">
                                        <asp:Label ID="lblCampaignHeader" Font-Bold="True" CssClass="LabelFont" runat="server"
                                            Text="Campaign"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="chkCampaignList" runat="server" CssClass="radioList" ClientIDMode="Static">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="float: left; padding-left: 15px;">
                            <table style="border: 1px solid;">
                                <tr>
                                    <td style="background-color: #d6e0ec;">
                                        <asp:Label ID="Label1" Font-Bold="True" CssClass="LabelFont" runat="server" Text="Page"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="chkPageList" runat="server" CssClass="radioList" ClientIDMode="Static">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="float: right;">
                    <tr>
                        <td>
                            <asp:Button ID="btnSaveShow" runat="server" Text="Save" CssClass="button" class="button"
                                Style="font-size: 12px; width: 60px;" OnClick="btnSaveShow_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelShow" runat="server" Text="Close" CssClass="button" class="button"
                                Style="font-size: 12px; width: 60px;" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveShow" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlPopupEmail" runat="server" Style="display: none;" ClientIDMode="Static">
        <asp:UpdateProgress ID="UpdateProgress3" runat="Server" AssociatedUpdatePanelID="UpdatePanel2"
            DisplayAfter="1">
            <ProgressTemplate>
                <asp:Label ID="lblPleaseWaitEmail" CssClass="LabelFont" runat="server" Text="Please Wait..."
                    Style="font-size: 15px;" ForeColor="Red"></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblFromEmail" runat="server" CssClass="LabelFont" Text="From:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromEmail" CssClass="textbox curved" runat="server" Style="font-size: 12px;"
                                ClientIDMode="Static" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblToEmail" runat="server" CssClass="LabelFont" Text="To:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtToEmail" CssClass="textbox curved" runat="server" Style="font-size: 12px;
                                resize: none;" Height="15px" ClientIDMode="Static" Width="300px" TextMode="MultiLine"></asp:TextBox>
                            <asp:LinkButton ID="lnkAddCC" runat="server" Text="CC" OnClick="AddCC"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCCEmail" runat="server" CssClass="DisplayNone" Text="CC:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCCEMail" CssClass="DisplayNone" runat="server" Style="font-size: 12px;
                                resize: none;" Height="15px" ClientIDMode="Static" Width="300px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubjectEmail" runat="server" CssClass="LabelFont" Text="Subject:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubjectEmail" CssClass="textbox curved" runat="server" Style="font-size: 12px;"
                                ClientIDMode="Static" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessageEmail" runat="server" CssClass="LabelFont" Text="Message:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMessageEmail" CssClass="textbox curved" runat="server" Style="font-size: 12px;"
                                TextMode="MultiLine" Height="200px" Width="300px" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td style="padding-right: 10px;">
                            <asp:Button ID="btnSendEmail" runat="server" Text="Send" CssClass="button" Style="font-size: 12px;
                                width: 60px;" OnClick="SendEmail" />
                        </td>
                        <td style="padding-right: 10px;">
                            <asp:Button ID="bntEditEmail" runat="server" Text="Edit Message" CssClass="button"
                                Style="font-size: 12px;" OnClick="bntEditEmail_Click" />
                        </td>
                        <td style="padding-right: 10px;">
                            <asp:Button ID="btnCancelEmail" runat="server" Text="Cancel" CssClass="DisplayNone"
                                Style="font-size: 12px; width: 60px;" OnClick="btnCancelEmail_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSendEmail" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="bntEditEmail" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelEmail" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="txtMessageEmail" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtSubjectEmail" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlPopupCampaignList" runat="server" Style="display: none;" ClientIDMode="Static">
        <asp:UpdateProgress ID="UpdateProgress4" runat="Server" AssociatedUpdatePanelID="UpdatePanel2"
            DisplayAfter="1">
            <ProgressTemplate>
                <asp:Label ID="lblPleaseWait" CssClass="LabelFont" runat="server" Text="Please Wait..."
                    Style="font-size: 15px;" ForeColor="Red"></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblAddNewCampaignValue" runat="server" CssClass="LabelFont" Text="Campaign Value:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddNewCampaignValue" CssClass="textbox curved" runat="server"
                                Style="font-size: 12px;" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAddNewCampaignName" runat="server" CssClass="LabelFont" Text="Campaign Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddNewCampaignName" CssClass="textbox curved" runat="server"
                                Style="font-size: 12px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAddNewCurrencyCode" runat="server" CssClass="DisplayNone" Text="Currency Code:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddNewCurrencyCode" CssClass="DisplayNone" runat="server" Style="font-size: 12px;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td style="padding-right: 10px;">
                            <asp:Button ID="btnSaveNewCampaign" runat="server" Text="Save" CssClass="button"
                                Style="font-size: 12px; width: 60px;" />
                        </td>
                        <td style="padding-right: 10px;">
                            <asp:Button ID="btnCancelNewCampaign" runat="server" Text="Cancel" CssClass="button"
                                Style="font-size: 12px; width: 60px;" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="pnlCampaignList2" runat="server" Width="400px" Style="max-height: 200px;"
                    ScrollBars="Auto">
                    <asp:GridView ID="gridCampaignList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        GridLines="None" CellPadding="4" CellSpacing="2" BackColor="#FFFFFF">
                        <AlternatingRowStyle BackColor="#e5e5e5" />
                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" Wrap="false" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="CAMPAIGN VALUE" DataField="CampaignValue" SortExpression="CampaignValue">
                                <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="CAMPAIGN NAME" DataField="CampaignName" SortExpression="CampaignName">
                                <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="CURRENCY CODE" DataField="Currency" SortExpression="Currency">
                                <HeaderStyle VerticalAlign="Middle" Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveNewCampaign" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelNewCampaign" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--For Manage Note Type--%>
    <asp:Panel ID="pnlPopupNoteType" runat="server" Style="display: none;" ClientIDMode="Static">
        <asp:UpdateProgress ID="UpdateProgress5" runat="Server" AssociatedUpdatePanelID="UpdatePanel5"
            DisplayAfter="1">
            <ProgressTemplate>
                <asp:Label ID="lblPleaseWait2" CssClass="LabelFont" runat="server" Text="Please Wait..."
                    Style="font-size: 15px;" ForeColor="Red"></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="table-wrapper page1">
                    <table border="0" cellpadding="10" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                    <asp:GridView ID="grdNoteTypeList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        GridLines="None" CellPadding="4" CellSpacing="2" BackColor="#FFFFFF" OnRowEditing="grdNoteTypeList_RowEditing"
                                        ClientIDMode="Static" OnRowCancelingEdit="grdNoteTypeList_RowCancelingEdit" Font-Size="12px">
                                        <AlternatingRowStyle BackColor="#e5e5e5" />
                                        <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                        <RowStyle CssClass="RowStyle" Wrap="false" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <Columns>
                                            <asp:CommandField HeaderText="Edit-Update" ShowEditButton="True" ItemStyle-Width="1%" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Note Type
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddNoteType" runat="server" Text='<%# Eval("NoteType") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtManageNoteTypeEdit" runat="server" ClientIDMode="Static" Text='<%# Eval("NoteType") %>'
                                                        CssClass="textbox curved" Font-Size="12px"></asp:TextBox>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton Canada
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddCA" runat="server" Text='<%# Eval("CA") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddCA" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Emedco
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddEMED" runat="server" Text='<%# Eval("EMED") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddEMED" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton US
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddUS" runat="server" Text='<%# Eval("US") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddUS" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Personnel Concepts
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddPC" runat="server" Text='<%# Eval("PC") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddPC" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Clement
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddCL" runat="server" Text='<%# Eval("CL") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddCL" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton UK
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddUK" runat="server" Text='<%# Eval("UK") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddUK" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Safetyshop UK
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddSUK" runat="server" Text='<%# Eval("SUK") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddSUK" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton Austria
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddAT" runat="server" Text='<%# Eval("AT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddAT" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton Germany
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddDE" runat="server" Text='<%# Eval("DE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddDE" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton Switzerland
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddCH" runat="server" Text='<%# Eval("CH") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddCH" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton Belgium
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddBE" runat="server" Text='<%# Eval("BE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddBE" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton France
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddFR" runat="server" Text='<%# Eval("FR") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddFR" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Seton Netherland
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblddNL" runat="server" Text='<%# Eval("NL") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlddNL" CssClass="textbox curved" Font-Size="12px" runat="server">
                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
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
    </asp:Panel>
    <asp:HiddenField ID="CampaignMutiple" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="UserRoleMutiple" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="SalesTeamIDMutiple" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="UserNameMutiple" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="Username" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="FullNameMutiple" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="CampaignName" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="CampaignValue" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="CampaignValuePrevious" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="UserRolePrevious" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="NoteTypeValues" ClientIDMode="Static" runat="server" />
</asp:Content>
