<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderHistory1.aspx.cs" Inherits="WebSalesMine.WebPages.OrderHistory.OrderHistory1" MasterPageFile="~/WebPages/UserControl/SalesMine.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="T2" runat="server" ContentPlaceHolderID="MAINCONTENT">
    
    <script src="../../App_Themes/JS/JsGrid/VwdCmsSplitterBar.js" type="text/javascript"></script>
       <script language="javascript" type="text/javascript"><!--

           function splitterOnResize(width) {
               // do any other work that needs to happen when the 
               // splitter resizes. this is a good place to handle 
               // any complex resizing rules, etc.

               // make sure the width is in number format
               if (typeof width == "string") {
                   width = new Number(width.replace("px", ""));
               }

           }
	
// -->
</script>


     
    <div>
		
		<div style="margin:0px;padding:0px;width:1200px;overflow:hidden;">
		<table border="0" cellpadding="0" cellspacing="0" 
			style="width:1100px;height:600px;border:solid 1px #6699CC;">
			<tr style="height:600px;">
				<td runat="server" id="tdTree1" style="width:650px;height:600px;" align="left" valign="top"> 
					<div runat="server" id="divTree1" style="width:250px;height:100%;overflow:auto;padding:0px;margin:0px;">
					  <div style="padding-left: 20px;">
        <asp:Panel ID="HeaderButton" runat="server" Height="76px" Width="1180px" class="demo">
            <table>
                <tr class="demo">
                    <td>
                        <asp:Button ID="btnShowOrders" runat="server" Text="Show Orders" OnClick="btnShowOrders_Click"
                            CssClass="button" />
                        &nbsp;
                        <asp:Button ID="btnExportToExcel" Text="Export Excel" CssClass="button" runat="server"
                            ToolTip="Click to Export to Excel" OnClick="btn_Export2ExcelClick" />
                        &nbsp;
                        <asp:Button ID="btn_POLOOKUP" Text="Order/Customer PO LookUp" CssClass="button" runat="server"
                            ToolTip="Click to Export to Excel" OnClick="btn_POLOOKUPClick" CausesValidation="false" />
                        &nbsp;
                        <input runat="server" id="BtnArrangeColumn" type="button" class="button" value="Arrange Columns"
                            onclick="window.open('../Home/ArrangeColumns.aspx?Data=lvwData','mywindow','width=700,height=400,scrollbars=yes')" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" Height="29px" Width="1126px" class="demo" Style="margin-right: 28px">
                <table>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="ByDateUpdate" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="ByDate" Text="" runat="server" OnCheckedChanged="ByDate_CheckedChanged"
                                        AutoPostBack="True" />
                                    Start Date:
                                    <asp:TextBox ID="txtStartDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                    <asp:ImageButton ID="imgstartCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                    &nbsp; End Date:
                                    <asp:TextBox ID="txtEndDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                    <asp:ImageButton ID="imgEndCal" ImageUrl="~/App_Themes/Images/Calender.jpg" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate"
                                        PopupButtonID="imgstartCal">
                                    </asp:CalendarExtender>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate"
                                        PopupButtonID="imgEndCal">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ByDate" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="ByCal" Text="" runat="server" AutoPostBack="True" OnCheckedChanged="ByCal_CheckedChanged" />
                                    <asp:RadioButton ID="rdoFiscalYear" runat="server" Text="Fiscal Year" GroupName="RdoByCals"
                                        Checked="True" AutoPostBack="True" />&nbsp;
                                    <asp:RadioButton ID="rdoCalender" runat="server" Text="Calendar" GroupName="RdoByCals"
                                        AutoPostBack="True" />&nbsp;
                                    <asp:DropDownList ID="ddlBycal" runat="server" OnSelectedIndexChanged="ddiYear_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ByCal" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelTotalOrdersSales" runat="server">
                                <ContentTemplate>
                                    Total Orders:<asp:TextBox ID="txtbTotalOrders" runat="server" Text="0" Width="50px"
                                        ReadOnly="True"></asp:TextBox>
                                    &nbsp; Total Sales:<asp:TextBox ID="txtbTatalSales" runat="server" Text="$0.00" Width="85px"
                                        ReadOnly="True"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlBycal" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </div>
    <br />
    <table style="padding-left: 20px;">
        <tr>
            <td>
                <div class="CssErrorLabel">
                    <asp:Literal ID="litErrorinGrid" runat="server"></asp:Literal></div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <%-- <asp:Panel ID="PnlOrderHistory" runat="server"  ScrollBars="Auto" Width="1200px" Height="400px">--%>
                        <div id="HeaderDiv">
                        </div>
                        <div id="DataDiv" style="overflow: auto; border: 1px solid olive; width: 1200px;
                            height: 400px;" onscroll="Onscrollfnction();">
                                <asp:GridView AutoGenerateColumns="True" 
                                ClientIDMode="Static" ID="gridOrderHistory"
                                AllowSorting="true" CssClass="GridViewStyle" 
                                runat="server" BackColor="#CCCCCC"
                                BorderColor="#999999" BorderStyle="Solid" ForeColor="Black" 
                                AllowPaging="false"
                                Font-Size="12px" OnSorting="gridOrderHistory_Sorting" 
                                OnRowDataBound="gridOrderHistory_RowDataBound"
                                BorderWidth="3px" CellPadding="4"  CellSpacing="2">
                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                <PagerStyle BackColor="#EDEDED" Font-Size="15px" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <HeaderStyle BackColor="#507CD1" ForeColor="White" Wrap="False" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <EditRowStyle CssClass="EditRowStyle" />
                                <RowStyle CssClass="RowStyle" Wrap="false" />
                            </asp:GridView>
                        </div>
                        <%--                        </asp:Panel>--%>
                        <asp:Panel ID="pnlGridIndex" runat="server" CssClass="CssLabel">
                            <i>You are viewing page
                                <%=gridOrderHistory.PageIndex + 1%>
                                of
                                <%=gridOrderHistory.PageCount%>
                            </i>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlBycal" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnShowOrders" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ByCal" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr runat="server" id="trBlank" visible="false">
            <td style="height: 175px">
                &nbsp;
            </td>
        </tr>
        <asp:UpdatePanel ID="upOuter" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <tr>
                    <td>
                        <asp:Button Style="display: none" ID="btnHidden" runat="server" meta:resourcekey="btnHiddenResource1">
                        </asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                            BehaviorID="mpe" DynamicServicePath="" Enabled="True" PopupControlID="ratePanel"
                            PopupDragHandleControlID="pnlDragable" RepositionMode="None" TargetControlID="btnHidden">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="ratePanel" runat="server" Style="display: block" Height="0px" Width="0px"
                            meta:resourcekey="ratePanelResource1">
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
                                                                                                    Font-Underline="False" ForeColor="White" Width="224px" Text="Order/Customer LookUp"
                                                                                                    meta:resourcekey="LTitleResource1"></asp:Label>
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
                                                                                            <td width="144">
                                                                                                <asp:RadioButton ID="rdoOrderNumber" runat="server" Text="Order Number" GroupName="RdoLookUp"
                                                                                                    CssClass="CssLabel" Checked="true" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="144">
                                                                                                <asp:RadioButton ID="rdoCustomerLookUp" runat="server" Text="Customer PO" GroupName="RdoLookUp"
                                                                                                    CssClass="CssLabel" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtCustomerLookUp" runat="server" MaxLength="10"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="ResourceGroup" ID="reqType"
                                                                                                    ControlToValidate="txtCustomerLookUp" InitialValue="Please select" ErrorMessage="Please enter the LookUp"
                                                                                                    ForeColor="Red" Display="Dynamic" SetFocusOnError="True" />
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
                                                                                                <asp:Button ID="btnUpdate" runat="server" CssClass="button" ToolTip="Export" Text="Order/Customer Po Lookup"
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
                        </asp:Panel>
                    </td>
                </tr>
            </ContentTemplate>
        </asp:UpdatePanel>
    </table>
					</div>			
				</td>
				<td id="tdMid1" style="height:200px;width:6px;background-color:lightsteelblue;"></td>
				<td id="tdEdit1" align="left" valign="top" style="height:200px;"> 
						Right side text
				</td>
			
			</tr>
		</table>
		
		
		</div>	
            
        <vwdcms:splitterbar runat="server" ID="vsbSplitter1" 
			LeftResizeTargets="tdTree1;divTree1" 
			MinWidth="100" 
			MaxWidth="700"
			BackgroundColor="lightsteelblue" 
			BackgroundColorLimit="firebrick"
			BackgroundColorHilite="steelblue"
			BackgroundColorResizing="purple"
			SaveWidthToElement="txtWidth1"
			OnResize="splitterOnResize"
			style="background-image:url(vsplitter.gif);
				background-position:center center;
				background-repeat:no-repeat;"/> 
	
    </div>

</asp:Content>
