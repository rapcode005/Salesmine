<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductSummary.aspx.cs"
    Inherits="WebSalesMine.WebPages.ProductSummary.ProductSummary" EnableEventValidation="false"
    MasterPageFile="~/WebPages/UserControl/NewMasterPage.Master" %>

    <%@ MasterType VirtualPath="~/WebPages/UserControl/NewMasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<asp:Content ID="T2" runat="server" ContentPlaceHolderID="container">
    <style type="text/css">
        .FixedPostion
        {
            position: fixed;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            $addHandler(document, 'keydown', onKeypress);
                  $(document).ready(function () {

            $('#<%= lnkContactLevel.ClientID %>').click(function () {
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
                                        //                                        $get('lnkContactSelected').innerText = "(x)" + getCookie('CNameTemp');
                                        //                                        $get('lnkContactSelected').outerText = "(x)" + getCookie('CNameTemp');
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

            $('#lnkContactSelected').click(function () {
                setCookie('CName', "", -1);
                setCookie('CNo', "", -1);
                __doPostBack('= btnRefresh.ClientID ', '');
                return false;
            });

        });
 
        }

        function OnSuccess(Data) {
             var FIREFOX = /Firefox/i.test(navigator.userAgent);

             if (Data)
             {

                    var myDate = new Date(CheckString(Data.rows[0]['last_order_date']));
                    if (document.getElementById('<%=((DropDownList)Master.FindControl("ddlCampaign")).ClientID %>').value != "PC"){
           
                        $get('txtSKUFamily').value = CheckString(Data.rows[0]['sku_family']);
            
                    }
                    else
                    {
                        if (FIREFOX) 
                        {
                          $get('lblSKUFamily').textContent="Space Code";
                        }
                        else
                        {
                        $get('lblSKUFamily').innerText="Space Code";
                        }
            
                        $get('txtSKUFamily').value = CheckString(Data.rows[0]['SPACE_CODE']);
                    }

                    $get('txtSKUDesc').value = CheckString(Data.rows[0]['SKU_Description']);
                    $get('txtSKUNumber').value = CheckString(Data.rows[0]['sku_number']);
                    $get('txtF09').value = CheckDecimal(Math.round(CheckString(Data.rows[0]['sales_3fy_ago'])).toFixed(0));
                    $get('txtF10').value = CheckDecimal(Math.round(CheckString(Data.rows[0]['sales_2fy_ago'])).toFixed(0));
                    $get('txtF11').value = CheckDecimal(Math.round(CheckString(Data.rows[0]['sales_1fy_ago'])).toFixed(0));
                    $get('txtF12').value = CheckDecimal(Math.round(CheckString(Data.rows[0]['sales_currfy'])).toFixed(0));

                    if( Math.round(CheckString(Data.rows[0]['Total_sales'])).toFixed(0)!=0){$get('txtlifetimesales').value = Math.round(CheckString(Data.rows[0]['Total_sales'])).toFixed(0);}

                    if(Math.round(CheckString(Data.rows[0]['NO_orders'])).toFixed(0)!=0){$get('txtlifetimeorders').value = Math.round(CheckString(Data.rows[0]['NO_orders'])).toFixed(0);}
            
                    if ( CheckString(Data.rows[0]['last_order_date']) != "") {
                                            $get('txtlastordereddate').value = (myDate.getMonth() + 1) + "/" +
                                                                            myDate.getDate() + "/" +
                                                                            myDate.getFullYear();
                }
                   $get('txtf09units').value = CheckDecimal(Math.round(CheckString(Data.rows[0]['units_3fy_ago'])).toFixed(0));
                   $get('txtf10units').value = CheckDecimal(Math.round(CheckString(Data.rows[0]['units_2fy_ago'])).toFixed(0));
                   $get('txtf11units').value = CheckDecimal(Math.round(CheckString(Data.rows[0]['units_1fy_ago'])).toFixed(0));
                   $get('txtf12units').value = CheckDecimal(Math.round(CheckString(Data.rows[0]['units_currfy'])).toFixed(0));

           }

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



        $(document).ready(function () {
        $addHandler(document, 'keydown', onKeypress);

            $("#grdSkuSummary  tr:has(td)").hover(function () {

                $(this).css("cursor", "pointer");
            });
            
            $("#grdPCSKUSummary  tr:has(td)").hover(function () {

                $(this).css("cursor", "pointer");
            });
             
            $("#grdSkuSummary tr").not($("#grdSkuSummary tr").eq(0)).click(function () {

                $("#grdSkuSummary  tr").closest('TR').removeClass('SelectedRowStyle');
                $(this).addClass('SelectedRowStyle');

                var totalCols = ($("#grdSkuSummary").find('tr')[0].cells.length + 1);
         
                for (var i = 0; i < totalCols; i++) {
                    var ColName = $('#grdSkuSummary tr').find('th:nth-child(' + i + ')').text();
                  
                    if (ColName == 'PRODUCT NUMBER') {

                        var SKUNumber = $(this).find("td:eq(" + (i - 1) + ")").html();
                    }
                    
                    else if (ColName == 'LAST ORDER DATE') {
              
                        var LasrOrderDate = $(this).find("td:eq(" + (i - 1) + ")").html();

                    }
                }
 
              PageMethods.GetDatafromXMLDetails(SKUNumber, LasrOrderDate,OnSuccess);

                 var mydiv = $('#pnlSKUSummary');
                    mydiv.dialog({ autoOpen: false,
                        title: "Product Summary",
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


            $("#grdPCSKUSummary tr").not($("#grdPCSKUSummary tr").eq(0)).click(function () {

                $("#grdPCSKUSummary  tr").closest('TR').removeClass('SelectedRowStyle');
                $(this).addClass('SelectedRowStyle');

                var totalCols = ($("#grdPCSKUSummary").find('tr')[0].cells.length + 1);
         //alert(totalCols);
                for (var i = 0; i < totalCols; i++) {
                    var ColName = $('#grdPCSKUSummary tr').find('th:nth-child(' + i + ')').text();
                 // alert(ColName);
                    if (ColName == 'PRODUCT NUMBER') {

                        var SKUNumber = $(this).find("td:eq(" + (i - 1) + ")").html();
                       // alert(SKUNumber);
                    }
                    
                    else if (ColName == 'LAST ORDER DATE') {
              
                        var LasrOrderDate = $(this).find("td:eq(" + (i - 1) + ")").html();
                        //alert(LasrOrderDate);
                    }
                }
 
              PageMethods.GetDatafromXMLDetails(SKUNumber, LasrOrderDate,OnSuccess);

                 var mydiv = $('#pnlSKUSummary');
                    mydiv.dialog({ autoOpen: false,
                        title: "Product Summary",
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

        function onKeypress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
             //                    var modalPopup = $find('mpe').hide();
                    var mydiv = $('#pnlSKUSummary');
                        mydiv.dialog('close');
                }
     
          
           
           
        }

  


    </script>
    <div id="container">
        <asp:UpdatePanel ID="UpdatePanelTest" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="object-container" width="100%"
                    height="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td class="object-wrapper" style="height: 20px">
                                                <asp:LinkButton ID="lnkContactLevel" runat="server" CssClass="LabelFont">Select Contact</asp:LinkButton>
                                                &nbsp;
                                                <img id="ImageSelectContact2" src="../../App_Themes/Images/New Design/btn-red-x.gif"
                                                    runat="server" height="10" border="0" />
                                                <asp:LinkButton ID="lnkContactSelected" Style="text-decoration: none; border-bottom: 1px solid red;"
                                                    runat="server" Font-Names="Arial" ClientIDMode="Static" Font-Size="12px" ForeColor="Black"
                                                    ToolTip="Click to remove contact"></asp:LinkButton>
                                            </td>
                                            <td class="object-wrapper" style="height: 20px">
                                                <asp:LinkButton ID="LinkFilterBy" runat="server" CssClass="LabelFont" Visible="false">Filter by: </asp:LinkButton>
                                                <img id="ImageSelectContact" src="../../App_Themes/Images/New Design/btn-red-x.gif"
                                                    runat="server" height="10" border="0" visible="false" />
                                                <asp:LinkButton ID="lnkRemoveFilters" Style="text-decoration: none; border-bottom: 1px solid red;"
                                                    runat="server" Font-Names="Arial" ClientIDMode="Static" Font-Size="12px" ForeColor="Black"
                                                    OnClick="BtnShowAllSkuSummary_Click" ToolTip="Click to remove filter"></asp:LinkButton>
                                            </td>
                                            <%if (userRule != "PC-MAN" || userRule != "PC-ONT")
                                              { %>
                                            <td class="object-wrapper" style="height: 20px">
                                                <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="LabelFont" OnClick="btn_Export2ExcelClick">Export to Excel</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <% }%>
                                            <%if (ArrnageColumnstring == "lvwSKUSummary")
                                              { %>
                                            <td class="object-wrapper" style="height: 20px">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="LabelFont" OnClientClick="window.open('../Home/ArrangeColumns.aspx?Data=lvwSKUSummary','mywindow','width=700,height=400,scrollbars=yes')">Arrange Product Summary Columns</asp:LinkButton>
                                            </td>
                                            <% }%>
                                            <%if (ArrnageColumnstring == "lvwPCSKUSummary")
                                              { %>
                                            <td class="object-wrapper" style="height: 20px">
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="LabelFont" OnClientClick="window.open('../Home/ArrangeColumns.aspx?Data=lvwPCSKUSummary','mywindow','width=700,height=400,scrollbars=yes')">Arrange Product Summary Columns</asp:LinkButton>
                                            </td>
                                            <% }%>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="pnlOterCampaign" runat="server" Visible="false">
                    <div class="GridHeaderLabel">
                        PRODUCT LINE SUMMARY
                        <asp:Label ID="lblErrorProductLineSummary" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                    </div>
                    <div class="table-wrapper page2">
                        <table border="0" cellpadding="10" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="uppanelMain" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ScrollBars="None" ID="DataDiv" Style="width: 100%; height: 100%;">
                                                <asp:GridView ID="grdProductLineSummary" runat="server" AutoGenerateColumns="False"
                                                    EmptyDataText="No data available." Width="100%" AllowPaging="True" CellPadding="4"
                                                    CellSpacing="1" Font-Size="12px" OnRowCommand="grdProductLineSummary_RowCommand"
                                                    BackColor="#FFFFFF" AllowSorting="True" OnSorting="grdProductLineSummary_Sorting"
                                                    OnPageIndexChanging="grdProductLineSummaryDataPageEventHandler" ForeColor="Black"
                                                    GridLines="None" AsyncRendering="false" OnPreRender="grdProductLineSummary_Prerender">
                                                    <AlternatingRowStyle BackColor="#e5e5e5" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Filter">
                                                            <ItemTemplate>
                                                                <div align="center">
                                                                    <asp:LinkButton ID="lbtnShowSKU" runat="server" CommandName="ShowPLSKU" CommandArgument='<%# Eval("SKU CATEGORY")%>'
                                                                        Text="Filter" PostBackUrl="~/WebPages/ProductSummary/ProductSummary.aspx" /></div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="PRODUCT LINE" DataField="SKU CATEGORY" SortExpression="SKU CATEGORY" />
                                                        <asp:BoundField HeaderText="" DataField="F12 SALES" SortExpression="F12 SALES" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="F13 SALES" SortExpression="F13 SALES" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="F14 SALES" SortExpression="F14 SALES" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="F15 SALES" SortExpression="F15 SALES" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="LIFETIME SALES" DataField="TOTAL SALES" SortExpression="TOTAL SALES"
                                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="LIFETIME ORDERS" DataField="LIFETIME ORDERS" SortExpression="LIFETIME ORDERS"
                                                            ItemStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="LAST ORDER DATE" DataField="LAST ORDER DATE" DataFormatString="{0:MM/dd/yyyy}"
                                                            SortExpression="LAST ORDER DATE" ItemStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="F12 UNITS" SortExpression="F12 UNITS" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="F13 UNITS" SortExpression="F13 UNITS" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="F14 UNITS" SortExpression="F14 UNITS" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="F15 UNITS" SortExpression="F15 UNITS" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString="{0:##,#####}">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlGridIndex" runat="server" CssClass="CssLabel" Visible="false">
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnExportToExcel" />
                                            <asp:PostBackTrigger ControlID="btnOk" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="GridHeaderLabel">
                        PRODUCT SUMMARY &nbsp;
                        <asp:Label ID="Label2" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                    </div>
                    <div class="table-wrapper page1">
                        <table border="0" cellpadding="10" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlgrdSkuSummary" runat="server" ScrollBars="Auto" Style="width: 100%;
                                                height: 100%;">
                                                <asp:GridView ID="grdSkuSummary" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    ClientIDMode="Static" EmptyDataText="No data available." PageSize="10" AllowPaging="false"
                                                    AllowSorting="true" CellPadding="4" CellSpacing="1" Font-Size="12px" ForeColor="Black"
                                                    GridLines="None" BackColor="#FFFFFF" AsyncRendering="false" OnPageIndexChanging="grdSkuSummaryDataPageEventHandler"
                                                    OnSorting="grdSkuSummary_Sorting">
                                                    <AlternatingRowStyle BackColor="#e5e5e5" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                </asp:GridView>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel1" runat="server" CssClass="CssLabel" Visible="false">
                                            </asp:Panel>
                                            <br />
                                            <asp:Label ID="lblErrorSkuSummary" runat="server"></asp:Label>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnExportToExcel" />
                                            <asp:PostBackTrigger ControlID="btnOk" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlPCCampaign" runat="server" Visible="false">
                    <div class="GridHeaderLabel">
                        PRODUCT LINE SUMMARY &nbsp;
                        <asp:Label ID="Label4" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                    </div>
                    <div class="table-wrapper page2">
                        <table border="0" cellpadding="10" cellspacing="0" width="100%">
                            <tr>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel5" runat="server" Width="100%">
                                                <asp:GridView ID="grdPCProductLineSummary" runat="server" AutoGenerateColumns="false"
                                                    Width="100%" CellPadding="4" CellSpacing="1" Font-Size="12px" ForeColor="Black"
                                                    BackColor="#FFFFFF" GridLines="None" AsyncRendering="false" EmptyDataText="No data available."
                                                    PageSize="10" AllowPaging="false" OnRowDataBound="grdPCProductLineSummary_RowDataBound"
                                                    OnRowCommand="grdPCProductLineSummary_RowCommand" AllowSorting="true" OnSorting="grdProductLineSummary_Sorting"
                                                    OnPageIndexChanging="grdPCProductLineSummaryDataPageEventHandler" OnPreRender="grdPCProductLineSummary_Prerender">
                                                    <AlternatingRowStyle BackColor="#e5e5e5" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Filter">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnShowSKU" runat="server" CommandName="ShowPLSKU" CommandArgument='<%# Eval("PRODUCT FAMILY")%>'
                                                                    Text="Filter" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="PRODUCT THEME" DataField="PRODUCT FAMILY" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="PRODUCT FAMILY" />
                                                        <asp:BoundField HeaderText="LAST REVISION DATE" DataField="LAST REVISION DATE" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="LAST REVISION DATE" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="" DataField="F12 SALES" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="F12 SALES" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                        <asp:BoundField HeaderText="" DataField="F13 SALES" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="F13 SALES" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                        <asp:BoundField HeaderText="" DataField="F14 SALES" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="F14 SALES" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                        <asp:BoundField HeaderText="" DataField="F15 SALES" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="F15 SALES" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                        <asp:BoundField HeaderText="TOTAL SALES" DataField="TOTAL SALES" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="TOTAL SALES" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                        <asp:BoundField HeaderText="LIFETIME ORDERS" DataField="LIFETIME ORDERS" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="LIFETIME ORDERS" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="LAST ORDER DATE" DataField="LAST ORDER DATE" HeaderStyle-HorizontalAlign="Center"
                                                            DataFormatString="{0:MM/dd/yyyy}" SortExpression="LAST ORDER DATE" ItemStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="" DataField="F12 UNITS" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="F12 UNITS" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                        <asp:BoundField HeaderText="" DataField="F13 UNITS" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="F13 UNITS" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                        <asp:BoundField HeaderText="" DataField="F14 UNITS" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="F14 UNITS" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                        <asp:BoundField HeaderText="" DataField="F15 UNITS" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="15%" HeaderStyle-Wrap="false"
                                                            SortExpression="F15 UNITS" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:##,#####}" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel2" runat="server" CssClass="CssLabel" Visible="false">
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="GridHeaderLabel">
                        PRODUCT SUMMARY &nbsp;
                        <asp:Label ID="Label6" runat="server" CssClass="lblMsg_ResourceEntry"></asp:Label>
                    </div>
                    <div class="table-wrapper page1">
                        <table border="0" cellpadding="10" cellspacing="0" width="100%">
                            <tr>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                <asp:GridView ID="grdPCSKUSummary" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    ClientIDMode="Static" CellPadding="4" CellSpacing="1" Font-Size="12px" ForeColor="Black"
                                                    GridLines="None" BackColor="#FFFFFF" AsyncRendering="false" EmptyDataText="No data available."
                                                    PageSize="10" OnRowDataBound="grdPCSkuSummary_RowDataBound" AllowPaging="false"
                                                    OnPageIndexChanging="grdPCSkuSummaryDataPageEventHandler" HeaderStyle-HorizontalAlign="Center"
                                                    AllowSorting="true" OnSorting="grdPCSKUSummary_Sorting">
                                                    <AlternatingRowStyle BackColor="#e5e5e5" />
                                                    <EditRowStyle CssClass="EditRowStyle" />
                                                    <HeaderStyle BackColor="#D6E0EC" Wrap="false" Height="30px" CssClass="HeaderStyle" />
                                                    <RowStyle CssClass="RowStyle" Wrap="false" />
                                                </asp:GridView>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel4" runat="server" CssClass="CssLabel" Visible="false">
                                            </asp:Panel>
                                            <br />
                                            <asp:Label ID="Label7" runat="server"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnExportToExcel" />
                                            <asp:PostBackTrigger ControlID="btnOk" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <tr runat="server" id="trBlank" visible="false">
        <td style="height: 200px">
            &nbsp;
        </td>
    </tr>
    <table>
        <tr>
            <td>
                <asp:Button Style="display: none" ID="btnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <cc3:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                    BehaviorID="mpe" DynamicServicePath="" Enabled="True" PopupControlID="ratePanel"
                    CancelControlID="btnCancel" PopupDragHandleControlID="pnlDragable" RepositionMode="None"
                    TargetControlID="btnHidden">
                </cc3:ModalPopupExtender>
                <asp:Panel ID="ratePanel" runat="server" Style="display: block" Height="0px" Width="0px"
                    meta:resourcekey="ratePanelResource1">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <!--  <asp:Panel runat="server" Height="50px" Width="100%" ID="Panel66" Visible="False">  -->
                            <table class="resRecpopUpTable_ResourceEntry">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlDragable" runat="server" meta:resourcekey="pnlDragableResource1">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="2">
                                                                <table class="resRecpuContent_ResourceEntry">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <table class="resRecpuheadingTable_ResourceEntry">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="resRecpuheadingCell_ResourceEntry" align="left">
                                                                                                <asp:Label ID="LTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                                                                    Font-Underline="False" ForeColor="White" Width="224px" Text="Select Table" meta:resourcekey="LTitleResource1"></asp:Label>
                                                                                            </td>
                                                                                            <td class="resRecpuheadTopBrdr_ResourceEntry">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="resRecpuIPFields_ResourceEntry" colspan="2" align="left">
                                                                                <table class="resRecpuDetails_ResourceEntry">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td width="165">
                                                                                                <asp:RadioButton ID="rdoProductSummary" runat="server" Text=" Product Line Summary"
                                                                                                    GroupName="RdoExportFile" CssClass="CssLabel" Checked="true" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="144">
                                                                                                <asp:RadioButton ID="rdoSKUSummary" runat="server" Text=" Product Summary" GroupName="RdoExportFile"
                                                                                                    CssClass="CssLabel" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="resRecbtnCell_ResourceEntry">
                                                                                <table class="resRecpuInputBtns_ResourceEntry">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td class="addOk_ResourceEntry">
                                                                                                <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="button" ToolTip="Save User"
                                                                                                    Width="60px" ValidationGroup="ResourceGroup" OnClick="btnOk_Click1" meta:resourcekey="btnOkResource1" />
                                                                                                <asp:Button ID="btnUpdate" runat="server" CssClass="button" ToolTip="Export" Text="Export the selected"
                                                                                                    ValidationGroup="ResourceGroup" Visible="False" Width="60px" meta:resourcekey="btnUpdateResource1" />
                                                                                            </td>
                                                                                            <td class="addCancel_ResourceEntry">
                                                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" ToolTip="Cancel"
                                                                                                    Width="60px" meta:resourcekey="btnCancelResource1" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <!--   </asp:Panel>-->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <%--Popup Dialog for SKU Details--%>
    <asp:Panel ID="pnlSKUSummary" runat="server" Style="display: none;" ClientIDMode="Static">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSKUFamily" CssClass="LabelFont" runat="server" Text="Product Family"
                        ClientIDMode="Static"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSKUFamily" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSKUDesc" CssClass="LabelFont" runat="server" Text="Product Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSKUDesc" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSKUNumber" CssClass="LabelFont" runat="server" Text="Product Number"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSKUNumber" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFO9" Width="100%" CssClass="LabelFont" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtF09" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        Width="100%" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblF10" CssClass="LabelFont" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtF10" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblF11" CssClass="LabelFont" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtF11" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblF12" CssClass="LabelFont" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtF12" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbllifetimesales" CssClass="LabelFont" runat="server" Text="Lifetime Sales"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlifetimesales" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbllifetimeorders" CssClass="LabelFont" runat="server" Text="Lifetime Orders"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlifetimeorders" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbllastordereddate" CssClass="LabelFont" runat="server" Text="Last Ordered Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlastordereddate" CssClass="textboxLabel" Style="font-size: 12px;
                        width: 220px;" ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblf09units" CssClass="LabelFont" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtf09units" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblf10units" CssClass="LabelFont" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtf10units" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblf11units" CssClass="LabelFont" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtf11units" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblf12units" CssClass="LabelFont" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtf12units" CssClass="textboxLabel" Style="font-size: 12px; width: 220px;"
                        ReadOnly="true" ClientIDMode="Static" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--                 <tr>
                        <td>
        </table>
    </div>--%>
    <%--</asp:Content>--%>
</asp:Content>
