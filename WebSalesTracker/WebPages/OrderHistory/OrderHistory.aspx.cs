using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AppLogic;
using System.Globalization;
using System.Text;
using WebSalesMine.WebPages.UserControl;
using ClassLibrary1;

namespace WebSalesMine.WebPages.OrderHistory
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        private int DateOrdinal = 1000, DateOrdinal1 = 1000,
            UnitPrice = 1000, UnitPrice1 = 1000,
            ExtPrice = 1000, ExtPrice1 = 1000,
            ConvertedDate = 1000, ConvertedDate1 = 1000;
        public string userRule, AccountOH;
        public DropDownList ddlCampaignCurrencyOH = new DropDownList(), ddlCampaignOH = new DropDownList();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            NewMasterPage MasterPage = Master as NewMasterPage;
            string ControlId = string.Empty, CName = string.Empty;
            cOrderHistory objOrderHis = new cOrderHistory();

            userRule = SessionFacade.UserRole.Trim();

            GetAccountInfo(MasterPage);

            lnkExportToExcel.Visible = objOrderHis.StatusExcelButton();

            VisibleControl(false);

            switch (!IsPostBack)
            {
                case true:
                    {
                        switch (SessionFacade.AccountNo)
                        {
                            case "0000000000":
                                {
                                    pnlGridIndex.Visible = false;
                                    trBlank.Visible = true;
                                    break;
                                }
                            default:
                                {
                                    ShowOrder();
                                    break;
                                }
                        }
                        StatusSearchDateRange(false);
                        StatusSearchYear(false);
                        InsertCalendarYearToDropDown();
                        break;
                    }
                default:
                    {
                        if (SessionFacade.AccountNo.Trim() != "0000000000")
                        {
                            ControlId = getPostBackControlID();
                            if (ControlId.In("","BtnNotesColumn2","ddlCampaign","imbSearchProjID",
                                "txtProjectID","imbSearchAcntNumber","txtAccountNumber","ByCal", "rdoFiscalYear",
                                "rdoCalender",null))
                                 ShowOrder();
                            else if (ControlId.In("txtStartDate","txtEndDate","BD"))
                            {
                                 if (txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "") ShowOrder();
                            }  
                        
                        }
                        break;
                    }
            }

            CName = ShowContactInfo(CName);

            CheckOrdNumPONum();
        }

        private void CheckOrdNumPONum()
        {
            if (Request.Cookies["ORNo"] != null)
            {
                lnkCustomerLookupSearch.Text = "OR#" + Request.Cookies["ORNo"].Value.Replace("%20", " ");
                ImageCustomerLookupSearch.Visible = true;
            }
            else if (Request.Cookies["PONo"] != null)
            {
                lnkCustomerLookupSearch.Text = "PO#" + Request.Cookies["PONo"].Value.ToString().Replace("%20", " ");
                ImageCustomerLookupSearch.Visible = true;
            }
            else
            {

                if (Request.Cookies["Remove"] != null)
                {

                    if (SessionFacade.AccountNo != "")
                    {
                        HttpCookie myCookie = new HttpCookie("Remove");
                        myCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(myCookie);
                    }

                }

                ImageCustomerLookupSearch.Visible = false;
                lnkCustomerLookupSearch.Text = "";
            }
        }

        private void GetAccountInfo(NewMasterPage MasterPage)
        {
            if (MasterPage != null)
            {
                //FormaT account number with leading zeroes
                SessionFacade.AccountNo = MasterPage.AccountNumberMaster.FormatAccountNumber();
                //SessionFacade.AccountNo = objFuncrtion;
                SessionFacade.CampaignName = MasterPage.CampaignMaster.SelectedValue;
                ddlCampaignOH = MasterPage.CampaignMaster;
                ddlCampaignCurrencyOH = MasterPage.CampaignCurrencyMaster;

                //This code use to determine what Currency Required.
                if (!string.IsNullOrEmpty(ddlCampaignOH.SelectedValue))
                {
                    if (!string.IsNullOrEmpty(ddlCampaignCurrencyOH.SelectedValue))
                    {
                        ddlCampaignCurrencyOH.ClearSelection();
                    }
                    else
                    {
                        ddlCampaignCurrencyOH.Items.Clear();

                        ddlCampaignCurrencyOH.Items.AddRange(ddlCampaignOH.Items.OfType<ListItem>().ToArray());
                    }


                    if (ddlCampaignCurrencyOH.Items.FindByValue(ddlCampaignOH.SelectedValue) != null)
                    {
                        ddlCampaignCurrencyOH.Items.FindByValue(ddlCampaignOH.SelectedValue).Selected = true;
                    }

                }
            }
        }

        protected void InsertCalendarYearToDropDown()
        {
            int Year;
            ddlBycal.Items.Clear();
            if (DateTime.Now.Month >= 8)
            {
                for (int index = 3; index >= -1; index--)
                {
                    Year = DateTime.Now.Year - index;
                    ddlBycal.Items.Add(Year.ToString());
                }
            }
            else
            {
                for (int index = 4; index >= 0; index--)
                {
                    Year = DateTime.Now.Year - index;
                    ddlBycal.Items.Add(Year.ToString());
                }
            }
            ddlBycal.Text = DateTime.Now.Year.ToString();
        }

        private string ShowContactInfo(string CName)
        {
            if (Request.Cookies["CName"] != null)
            {
                CName = Request.Cookies["CName"].Value.ToString();
                CName = CName.Replace("%2C", ",");
                CName = CName.Replace("%20", " ");
                lnkContactSelected.Text = CName;
                ImageSelectContact.Visible = true;
            }
            else
            {
                if (Request.Cookies["CNo"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("CNo");
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookie);
                }

                ImageSelectContact.Visible = false;
                lnkContactSelected.Text = "";
            }
            return CName;
        }

        private void VisibleControl(bool Select = true)
        {
            lblShipDate.Visible = Select;
            txtShipDate.Visible = Select;
            lblTrackingNo.Visible = Select;
            txtTrackingNo.Visible = Select;
            lblBillDate.Visible = Select;
            txtBillDate.Visible = Select;
            lblBillNo.Visible = Select;
            txtBillNo.Visible = Select;
            lblBillSales.Visible = Select;
            txtBillSales.Visible = Select;
        }
        #endregion

        protected void ByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (BD.Checked == true)
            {
                ByCal.Checked = false;
                StatusSearchDateRange(true);
                StatusSearchYear(false);
            }
            else
                StatusSearchDateRange(false);

           // //if (gridOrderHistory.Rows.Count > 0)
           // //{

           // //    gridOrderHistory.UseAccessibleHeader = true;
           // //    gridOrderHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

           // //}
           //gridOrderHistory.HeaderRow.TableSection = (gridOrderHistory.Rows.Count > 0) ?
           //TableRowSection.TableHeader : TableRowSection.TableBody;
        }

        protected void ByCal_CheckedChanged(object sender, EventArgs e)
        {
            if (ByCal.Checked == true)
            {
                BD.Checked = false;
                txtStartDate.Text = string.Empty;
                txtEndDate.Text = string.Empty;
                StatusSearchDateRange(false);
                StatusSearchYear(true);
            }
            else
            {
                StatusSearchYear(false);
            }

            ////if (gridOrderHistory.Rows.Count > 0)
            ////{

            ////    gridOrderHistory.UseAccessibleHeader = true;
            ////    gridOrderHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

            ////}
           //gridOrderHistory.HeaderRow.TableSection = (gridOrderHistory.Rows.Count > 0) ?
           //TableRowSection.TableHeader : TableRowSection.TableBody;
        }

        protected void gridOrderHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (DateOrdinal != 1000)
                    {
                        DateTime temp;
                        if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["ORDER DATE"].ToString(), out temp) == true)
                            e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["ORDER DATE"]).ToString("MM/dd/yyyy");
                    }
                    if (DateOrdinal1 != 1000)
                    {
                        DateTime temp;
                        if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["order_date"].ToString(), out temp) == true)
                            e.Row.Cells[DateOrdinal1].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["order_date"]).ToString("MM/dd/yyyy");
                    }

                    if (UnitPrice != 1000)
                    {
                        decimal temp;
                        if (decimal.TryParse(((DataRowView)e.Row.DataItem)["UNIT PRICE"].ToString(), out temp) == true)
                        {
                            e.Row.Cells[UnitPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["UNIT PRICE"].ToString()).FormatMoney(ddlCampaignCurrencyOH.SelectedItem.Text);
                        }
                        e.Row.Cells[UnitPrice].HorizontalAlign = HorizontalAlign.Right;
                    }
                    if (UnitPrice1 != 1000)
                    {
                        decimal temp;
                        if (decimal.TryParse(((DataRowView)e.Row.DataItem)["unit_price"].ToString(), out temp) == true)
                        {
                            e.Row.Cells[UnitPrice1].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["unit_price"].ToString()).FormatMoney(ddlCampaignCurrencyOH.SelectedItem.Text); ;
                        }
                       
                    }

                    if (ExtPrice != 1000)
                    {
                        decimal temp;
                        if (decimal.TryParse(((DataRowView)e.Row.DataItem)["EXT PRICE"].ToString(), out temp) == true)
                        {
                            e.Row.Cells[ExtPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["EXT PRICE"].ToString()).FormatMoney(ddlCampaignCurrencyOH.SelectedItem.Text);
                        }
                        //e.Row.Cells[ExtPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["EXT PRICE"].ToString()).ToString("C2");
                        e.Row.Cells[ExtPrice].HorizontalAlign = HorizontalAlign.Right;
                    }
                    if (ExtPrice1 != 1000)
                    {
                        decimal temp;
                        if (decimal.TryParse(((DataRowView)e.Row.DataItem)["ext_price"].ToString(), out temp) == true)
                        {
                            e.Row.Cells[ExtPrice1].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["ext_price"].ToString()).FormatMoney(ddlCampaignCurrencyOH.SelectedItem.Text);
                        }
                       
                    }


                    if (ConvertedDate != 1000)
                    {
                        DateTime temp;
                        if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["CONVERTED DATE"].ToString(), out temp) == true)
                            e.Row.Cells[ConvertedDate].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["CONVERTED DATE"]).ToString("MM/dd/yyyy");
                    }
                    if (ConvertedDate1 != 1000)
                    {
                        DateTime temp;
                        if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["conv_date"].ToString(), out temp) == true)
                            e.Row.Cells[ConvertedDate1].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["conv_date"]).ToString("MM/dd/yyyy");
                    }

                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Row Databound", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void ddiYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOrder();
        }
     
        #region Function

        private string getPostBackControlID()
        {
            Control control = null;
            //first we will check the "__EVENTTARGET" because if post back made by       the controls
            //which used "_doPostBack" function also available in Request.Form collection.
            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = Page.FindControl(ctrlname);
            }
            // if __EVENTTARGET is null, the control is a button type and we need to
            // iterate over the form collection to find it
            else
            {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in Page.Request.Form)
                {
                    //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                    //mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        ctrlStr = ctl.Substring(0, ctl.Length - 2);
                        c = Page.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = Page.FindControl(ctl);
                    }
                    if (c is System.Web.UI.WebControls.Button ||
                             c is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }

            if (control == null)
            {
                return "";
            }
            else
            {
                return control.ID;
            }
        }

        private void GetTotalOrdersSales(DataTable dt)
        {

            try
            {
                //FunctionNum objGrid = new FunctionNum();
                FunctionVb Function = new FunctionVb();
                decimal sum = 0;
                DataView view;

                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query, ResultName = string.Empty;
                string[] ColumnName = { "EXT PRICE", "ext_price" };

                try
                {

                    //Check Standard Order Only
                    Query = "[Order_type]='Standard Order' and [ReRej] Is Null and uvals='C'";

                    dtTemp = dt.Clone();

                    results = dt.Select(Query);

                    foreach (DataRow dr in results) dtTemp.ImportRow(dr);
                }
                catch (Exception err)
                {
                    BradyCorp.Log.LoggerHelper.LogException(err, "Check Standard Order Only", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                }

                //Total Sales
                sum = 0;
                try
                {

                    ResultName = dtTemp.CheckValidColumn(ColumnName);

                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        sum += Function.ConvertDecimal(dr[ResultName].ToString().Trim());
                    }

                    txtbTatalSales.Text = sum.FormatMoney(ddlCampaignCurrencyOH.SelectedItem.Text);

                }
                catch (Exception ex)
                {
                    BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Total Sales", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                }



                //Total Orders
                try
                {
                    ResultName = dtTemp.CheckValidColumn(new [] {"ORDER NUMBER","order_num"});

                    //if (dtTemp.Columns.Contains(ResultName) == true)
                    //{
                    view = new DataView(dtTemp);
                    dtTemp = view.ToTable(true, ResultName);
                    //}

                    if (dtTemp.Rows.Count > 0)
                    {
                        txtbTotalOrders.Text = dtTemp.Rows.Count.ToString();
                    }

                }
                catch (Exception ex)
                {
                    BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Total Orders", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                }



            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Total Sales and Orders", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        private void SetOrdinal(DataSet dsTemp)
        {

            try
            {
                DateOrdinal = (dsTemp.Tables[0].Columns.Contains("ORDER DATE")) ?
                    dsTemp.Tables[0].Columns["ORDER DATE"].Ordinal : 1000;

                DateOrdinal1 = (dsTemp.Tables[0].Columns.Contains("order_date")) ?
                    dsTemp.Tables[0].Columns["order_date"].Ordinal : 1000;

                UnitPrice =  (dsTemp.Tables[0].Columns.Contains("UNIT PRICE")) ?
                    dsTemp.Tables[0].Columns["UNIT PRICE"].Ordinal : 1000; 

                UnitPrice1 =  (dsTemp.Tables[0].Columns.Contains("unit_price")) ?
                    dsTemp.Tables[0].Columns["unit_price"].Ordinal : 1000;

                ExtPrice = (dsTemp.Tables[0].Columns.Contains("EXT PRICE")) ?
                    dsTemp.Tables[0].Columns["EXT PRICE"].Ordinal : 1000;

                ExtPrice1 = (dsTemp.Tables[0].Columns.Contains("ext_price")) ?
                    dsTemp.Tables[0].Columns["ext_price"].Ordinal : 1000;

                ConvertedDate =(dsTemp.Tables[0].Columns.Contains("CONVERTED DATE")) ?
                    dsTemp.Tables[0].Columns["CONVERTED DATE"].Ordinal : 1000;

                ConvertedDate1 =(dsTemp.Tables[0].Columns.Contains("conv_date")) ?
                    dsTemp.Tables[0].Columns["conv_date"].Ordinal : 1000;

                if (dsTemp.Tables[0].Columns.Contains("REASON REJECTION"))
                    dsTemp.Tables[0].Columns["REASON REJECTION"].ColumnName = "REASON FOR REJECTION";
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "SetOrdinal", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        public void ShowOrder()
        {
            try
            {
                DataSet ds = GetOrders();

                //Check if there is a data
                if (ds.CheckDataRecords())
                {
                    GetTotalOrdersSales(ds.Tables[0]);

                    //For Formating Date and Currency
                    SetOrdinal(ds);

                    pnlGridIndex.Visible = true;
                    trBlank.Visible = false;
                    litErrorinGrid.Text = "";

                    CheckColumnContains(ds);

                    gridOrderHistory.DataSource = ds;
                    gridOrderHistory.DataBind();

                    //Hide Unnecessary column such as 'Rerej,Order_type or etc..
                    if (ddlCampaignOH.SelectedValue == "PC")
                        ScriptManager.RegisterStartupScript(this, GetType(),
                        "Hide", "onSuccessGet(); HideColumn();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, GetType(),
                        "Hide", "HideColumn();", true);
                }
                else
                {
                    gridOrderHistory.DataSource = null;
                    gridOrderHistory.DataBind();
                    pnlGridIndex.Visible = false;
                    trBlank.Visible = true;
                    txtbTatalSales.Text = decimal.Parse("0").FormatMoney(ddlCampaignCurrencyOH.SelectedItem.Text);
                    txtbTotalOrders.Text = "0";
                    if (litErrorinGrid.Text == "") litErrorinGrid.Text = "No Record Found";
                }


            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Displaying Orders", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        private DataSet GetOrders()
        {
            try
            {
                cOrderHistory obj = new cOrderHistory();

                obj.AccountHis = SessionFacade.AccountNo;
                obj.CampaignHis = SessionFacade.CampaignName;
                obj.Sdate = (BD.Checked) ? txtStartDate.Text.Trim() : string.Empty;
                obj.Edate = (BD.Checked) ? txtEndDate.Text.Trim() : string.Empty;
                obj.year = (ByCal.Checked) ? Convert.ToInt32(ddlBycal.Text) : -9999;
                obj.intyrtype = (obj.year == -9999) ? -1 : ((rdoCalender.Checked) ? 1 : 0);
                obj.PONum = ((Request.Cookies["PONo"] != null) ?
                    Request.Cookies["PONo"].Value.ToString().Replace("%20", " ") : string.Empty);
                obj.OrdNum = ((Request.Cookies["ORNo"] != null) ?
                    Request.Cookies["ORNo"].Value.ToString().Replace("%20", " ") : string.Empty);
            
                DataSet ds = obj.LoadOrders() ?? new DataSet();
                return ds;
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "GetOrders",
                 SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                 SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return null;
            }
        }

        private void CheckColumnContains(DataSet ds)
        {
            try
            {
                string AccountSurvivorized,ResultName;
                ResultName = ds.Tables[0].CheckValidColumn(new[] { "accountnum", "ACCOUNT NUMBER" });
                AccountSurvivorized = ds.Tables[0].Rows[0][ResultName].ToString();
                SessionFacade.AccountNo = AccountSurvivorized;
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "CheckColumnContains",
                 SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                 SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void StatusSearchDateRange(bool value)
        {
            imgstartCal.Enabled = value;
            imgEndCal.Enabled = value;
        }

        protected void StatusSearchYear(bool value)
        {
            rdoCalender.Enabled = value;
            rdoFiscalYear.Enabled = value;
            ddlBycal.Enabled = value;
        }

        #endregion

        #region PageChanging
        protected void gridOrderHistoryPageChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataSet dsOrder = GetOrders();

                //Check if there is a data
                if (dsOrder.CheckDataRecords())
                {
                    //Working
                    if (dsOrder.Tables[0].Columns.Contains("UVALS") == true)
                        dsOrder.Tables[0].Columns.Remove("UVALS");

                    //For Formating Date and Currency
                    SetOrdinal(dsOrder);

                    gridOrderHistory.DataSource = dsOrder.Tables[0];
                    gridOrderHistory.PageIndex = e.NewPageIndex;

                    if (gridOrderHistory.Rows.Count > 0)
                    {
                        gridOrderHistory.UseAccessibleHeader = true;
                        gridOrderHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    gridOrderHistory.DataBind();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error During Pagining Orders",
                   SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                   SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }


        }
        #endregion

        #region Sorting Order History

        protected SortDirection GridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridOrderHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridOrderHistory(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridOrderHistory(sortExpression, "ASC");
            }


        }

        private void SortGridOrderHistory(string sortExpression, string direction)
        {
            try
            {
                DataSet dsOrder = GetOrders();

                if (dsOrder.CheckDataRecords())
                {
                    //Working
                    //if (dsOrder.Tables[0].Columns.Contains("UVALS") == true)
                    //    dsOrder.Tables[0].Columns.Remove("UVALS");

                    //For Formating Date and Currency
                    SetOrdinal(dsOrder);

                    DataView dv = new DataView(dsOrder.Tables[0]);

                    dv.Sort = sortExpression + " " + direction;

                    gridOrderHistory.DataSource = dv;
                    gridOrderHistory.DataBind();

                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error During Sorting Orders",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        #endregion

        #region Export to Excel Function
        protected void btn_Export2ExcelClick(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        public void ExportToExcel()
        {
            try
            {
                DataSet ds = new DataSet();
                string attachment = "attachment; filename=" + SessionFacade.LoggedInUserName + "OrderHistory.xls";
                Response.Clear();
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                gridOrderHistory.AllowPaging = false;
                gridOrderHistory.AllowSorting = false;
                ShowOrder();
                gridOrderHistory.DataBind();

                for (int i = 0; i < gridOrderHistory.Rows.Count; i++)
                {

                    GridViewRow row = gridOrderHistory.Rows[i];

                    row.Attributes.Add("class", "textmode");

                }



                //Get the HTML for the control.
                gridOrderHistory.RenderControl(hw);
                //     //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                Response.Output.Write(tw.ToString());
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Close();

                // gridOrderHistory.DataBind();

                // }
            }
            catch (System.Threading.ThreadAbortException lException)
            {

                // do nothing

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Viewing Exporting to Excel for Order History", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        #endregion

        [System.Web.Services.WebMethod]
        public static string btnOk_Click1(string txtCustomerLookUp, bool rdoOrderNumber, bool rdoCustomerLookUp)
        {
            try
            {
                string MyLookUpText = txtCustomerLookUp.ToString().Trim();
                if (MyLookUpText.Length > 0)
                {
                    MyLookUpText = MyLookUpText.Replace(",", "");
                }

                if (MyLookUpText.Length > 0)
                {
                    if (rdoOrderNumber == true && rdoCustomerLookUp == false)
                    {
                        return "OR";
                    }
                    if (rdoOrderNumber == false && rdoCustomerLookUp == true)
                    {
                        return "PO";
                    }
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Order History- Error in Customer PO LookUp" + err.ToString());
            }

            return "";
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ArrangeColumn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                   "call me", "window.open('../Home/ArrangeColumns.aspx?Data=lvwData','mywindow','width=700,height=400,scrollbars=yes')", true);
        }

    }



}