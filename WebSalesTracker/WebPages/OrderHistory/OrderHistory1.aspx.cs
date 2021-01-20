using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AppLogic;
using System.Collections;
using System.IO;
using ClassLibrary1;

namespace WebSalesMine.WebPages.OrderHistory
{
    public partial class OrderHistory1 : System.Web.UI.Page
    {
        private int DateOrdinal = 1000;
        private int UnitPrice = 1000;
        private int ExtPrice = 1000;
        private int ConvertedDate = 1000;
        private int uval = 1000;

        protected void Page_Load(object sender, EventArgs e)
        {

            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            SetButtonVisible();
            if (!IsPostBack)
            {
                if (txtTemp.Text.ToString().Trim() != "")
                    ShowOrder();
                else
                {
                    pnlGridIndex.Visible = false;
                    trBlank.Visible = true;
                }

                StatusSearchDateRange(false);
                StatusSearchYear(false);
                InsertCalendarYearToDropDown();
            }
            else
            {
                if (txtTemp.Text.ToString().Trim() != "")
                    ShowOrder();
                else
                {
                    gridOrderHistory.DataSource = null;
                    gridOrderHistory.DataBind();
                    pnlGridIndex.Visible = false;
                    trBlank.Visible = true;
                }
            }


            if (gridOrderHistory.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gridOrderHistory', 'HeaderDiv');</script>");
            }

        }

        #region Enable Disable Buttons on the page
        protected void SetButtonVisible()
        {
            if (SessionFacade.UserRole == "ADMIN")
            {
                btnExportToExcel.Visible = true;
                BtnArrangeColumn.Visible = true;
            }
            else
            {
                btnExportToExcel.Visible = false;
                BtnArrangeColumn.Visible = false;
            }
        }
        #endregion

        protected void btnShowOrders_Click(object sender, EventArgs e)
        {

            if (ByDate.Checked == true)
            {
                if (txtStartDate.Text == "")
                    litErrorinGrid.Text = "Select Start Date.";
                else if (txtEndDate.Text == "")
                    litErrorinGrid.Text = "Select End Date.";
                else
                    ShowOrder();
            }
            else
            {
                ShowOrder();
            }
        }

        protected void ByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ByDate.Checked == true)
            {
                ByCal.Checked = false;
                StatusSearchDateRange(true);
                StatusSearchYear(false);
            }
            else
                StatusSearchDateRange(false);
        }

        protected void ByCal_CheckedChanged(object sender, EventArgs e)
        {
            if (ByCal.Checked == true)
            {
                ByDate.Checked = false;
                txtStartDate.Text = string.Empty;
                txtEndDate.Text = string.Empty;
                StatusSearchDateRange(false);
                StatusSearchYear(true);
            }
            else
            {
                StatusSearchYear(false);
            }
        }

        protected void gridOrderHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool varColumnExist = false;
            int columnIndex;
            string[] list = { "Unit Price", "Ext Price","QTY",
                                "Line" };

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].CssClass = "locked";
                //e.Row.Cells[1].CssClass = "locked";
                //e.Row.Cells[2].CssClass = "locked";

                //Right Align of Currency Column
                for (int i = 0; i < list.Length; i++)
                {
                    columnIndex = 0;
                    foreach (DataControlFieldCell cell in e.Row.Cells)
                    {
                        if (cell.ContainingField is BoundField)
                        {
                            if (((BoundField)cell.ContainingField).DataField.Equals(list[i]))
                            {
                                varColumnExist = true;
                                break;
                            }
                            else
                                varColumnExist = false;
                        }
                        columnIndex++;
                    }

                    if (varColumnExist == true)
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                            e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;

                    }
                }


                if (DateOrdinal != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Order Date"].ToString(), out temp) == true)
                        e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Order Date"]).ToString("MM/dd/yyyy");
                }
                if (UnitPrice != 1000)
                {
                    decimal temp;
                    if (decimal.TryParse(((DataRowView)e.Row.DataItem)["Unit Price"].ToString(), out temp) == true)
                        e.Row.Cells[UnitPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["Unit Price"].ToString()).ToString("C2");
                }
                if (ExtPrice != 1000)
                {
                    decimal temp;
                    if (decimal.TryParse(((DataRowView)e.Row.DataItem)["Ext Price"].ToString(), out temp) == true)
                        e.Row.Cells[ExtPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["Ext Price"].ToString()).ToString("C2");
                }
                if (ConvertedDate != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Converted Date"].ToString(), out temp) == true)
                        e.Row.Cells[ConvertedDate].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Converted Date"]).ToString("MM/dd/yyyy");
                }
            }
        }

        protected void ddiYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOrder();
        }

        #region Function

        private int GetColumnIndexByName(GridViewRow row, string SearchColumnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                {
                    if (((BoundField)cell.ContainingField).DataField.Equals(SearchColumnName))
                    {
                        break;
                    }
                }
                columnIndex++;
            }
            return columnIndex;
        }

        private void GetTotalOrdersSales(DataTable dt)
        {
            FunctionVb Function = new FunctionVb();
            decimal sum = 0;
            DataView view;

            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            //Check Standard Order Only
            Query = "[Order Type]='Standard Order' and [Reason Rejection] Is Null and uvals='C'";

            dtTemp = dt.Clone();

            results = dt.Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            //Total Sales
            foreach (DataRow dr in dtTemp.Rows)
                sum += Function.ConvertDecimal(dr[6].ToString().Trim());

            txtbTatalSales.Text = decimal.Parse(sum.ToString()).ToString("C2");

            //Total Orders
            view = new DataView(dtTemp);

            dtTemp = view.ToTable(true, "Order Number");

            txtbTotalOrders.Text = dtTemp.Rows.Count.ToString();

        }

        public DataSet BindOrderHistory()
        {
            string[] TempAccount = new string[6];
            DataSet drExisting;
            try
            {
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
                cOrderHistory objOrderHistory = new cOrderHistory();

                //Campaign
                objOrderHistory.SearchOrderCampaignName = ddlTemp.Text.ToString().Trim();

                //Search by Account
                objOrderHistory.SearchOrderAccount = txtTemp.Text.ToString().Trim();

                drExisting = objOrderHistory.GetOrderHistory();
                DataSet ReturnDs = null;
                if (drExisting.Tables.Count > 0)
                {
                    TempAccount = SessionFacade.LastAccount;

                    TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();

                    SessionFacade.LastAccount = TempAccount;

                    ReturnDs = drExisting;

                    DateOrdinal = ReturnDs.Tables[0].Columns["Order Date"].Ordinal;

                    UnitPrice = ReturnDs.Tables[0].Columns["Unit Price"].Ordinal;

                    ExtPrice = ReturnDs.Tables[0].Columns["Ext Price"].Ordinal;

                    ConvertedDate = ReturnDs.Tables[0].Columns["Converted Date"].Ordinal;

                    uval = ReturnDs.Tables[0].Columns["uvals"].Ordinal;

                }
                else
                {
                    ReturnDs = null;
                }



                return ReturnDs;

            }
            catch (Exception ex)
            {
                DateOrdinal = 1000;
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Order History - BindingData");
                litErrorinGrid.Text = "There was a error while retrieving Data";
                trBlank.Visible = true;
                gridOrderHistory.DataSource = null;
                gridOrderHistory.DataBind();
                return null;
            }
        }

        public void ShowOrder()
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            SessionFacade.CustomerLookUp = "";
            SessionFacade.OrderLookUp = "";
            DataTable dtTemp = new DataTable();
            string[] TempAccount = new string[6];
            DataSet ds = new DataSet();
            DataSet dsTotalOrderSales = new DataSet();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";

            if ((SessionFacade.LastAccount[0] == txtTemp.Text)
                &&
               (ddlTemp.SelectedItem.Value == SessionFacade.CampaignValue))
            {
                dtTemp = GetDatafromXML();

                if (dtTemp == null)
                {
                    ds = BindOrderHistory();

                    //if (ds != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    dtTemp = GetDatafromXML();
                    //}
                }

                if (dtTemp.Rows.Count > 0)
                {
                    DataSet ReArrangeDs = new DataSet();
                    ReArrangeDs.Tables.Add(dtTemp);

                    GetTotalOrdersSales(dtTemp);

                    //Working
                    if (ReArrangeDs.Tables[0].Columns.Contains("uvals") == true)
                        ReArrangeDs.Tables[0].Columns.Remove("uvals");

                    gridOrderHistory.DataSource = ArrangeColumn(ReArrangeDs);
                    gridOrderHistory.DataBind();

                    litErrorinGrid.Text = "";
                    if (ReArrangeDs.Tables[0].Rows.Count > 0)
                    {
                        pnlGridIndex.Visible = true;
                        trBlank.Visible = false;
                        litErrorinGrid.Text = "";
                    }
                    else
                    {
                        pnlGridIndex.Visible = false;
                        trBlank.Visible = true;
                        litErrorinGrid.Text = "No Record Found";
                    }
                }
                else
                {
                    gridOrderHistory.DataSource = null;
                    gridOrderHistory.DataBind();
                }


            }
            else
            {
                ds = BindOrderHistory();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtTemp = GetDatafromXML();

                    GetTotalOrdersSales(dtTemp);

                    DataSet ReArrangeDs = new DataSet();
                    ReArrangeDs.Tables.Add(dtTemp);
                    if (uval != 1000)
                    {
                        ReArrangeDs.Tables[0].Columns.Remove("uvals");
                    }

                    ReArrangeDs = ArrangeColumn(ReArrangeDs);

                    //Working

                    ReArrangeDs.AcceptChanges();

                    gridOrderHistory.DataSource = ReArrangeDs;

                    gridOrderHistory.DataBind();

                    if (ReArrangeDs.Tables[0].Rows.Count > 0)
                    {
                        pnlGridIndex.Visible = true;
                        trBlank.Visible = false;
                        litErrorinGrid.Text = "";
                    }
                    else
                    {
                        pnlGridIndex.Visible = false;
                        trBlank.Visible = true;
                        litErrorinGrid.Text = "No Record Found";
                    }

                    try
                    {
                        string strAccntNo = ds.Tables[0].Rows[0]["Account Number"].ToString().Trim();

                        if (strAccntNo == SessionFacade.AccountNo.ToString().Trim())
                        {
                            SessionFacade.AccountNo = strAccntNo;
                            TempAccount = SessionFacade.LastAccount;
                            TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();
                            SessionFacade.LastAccount = TempAccount;
                        }
                        else
                        {
                            SessionFacade.AccountNo = "";
                            TempAccount = SessionFacade.LastAccount;
                            TempAccount[0] = "";
                            SessionFacade.LastAccount = TempAccount;
                        }
                    }
                    catch
                    {
                        SessionFacade.AccountNo = "";
                        TempAccount = SessionFacade.LastAccount;
                        TempAccount[0] = "";
                        SessionFacade.LastAccount = TempAccount;
                    }


                    /* //Shrinidhi
                     * Removed on Marach 01 2012
                     * Removed Raf's Code
                     * if (gridOrderHistory.Rows.Count > 0)
                     {
                         //if( gridOrderHistory.Rows[0].Cells["Account Number"].Text ! = SessionFacade.AccountNo.ToString().Trim())
                         if (gridOrderHistory.Rows[0].Cells[0].Text != SessionFacade.AccountNo.ToString().Trim())
                         {
                             SessionFacade.AccountNo = gridOrderHistory.Rows[0].Cells[0].Text;
                             TempAccount = SessionFacade.LastAccount;
                             TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();
                             SessionFacade.LastAccount = TempAccount;
                         }
                     }
                     else
                     {
                         SessionFacade.AccountNo = "";
                         TempAccount = SessionFacade.LastAccount;
                         TempAccount[0] = "";
                         SessionFacade.LastAccount = TempAccount;
                     } */



                }


                else
                {
                    //   SessionFacade.AccountNo = "";
                    pnlGridIndex.Visible = false;
                    gridOrderHistory.DataSource = null;
                    gridOrderHistory.DataBind();
                    txtbTotalOrders.Text = "0";
                    txtbTatalSales.Text = "$0.00";
                    litErrorinGrid.Text = "No Record Found";
                    trBlank.Visible = true;
                    TempAccount = SessionFacade.LastAccount;
                    TempAccount[0] = "";
                    SessionFacade.LastAccount = TempAccount;
                }

                /*
                  * Removed on Marach 01 2012
                    * Removed Raf's Code
                  //Removed Reduent code
                 */

                //if (BindOrderHistory() != null) // Main Loop Starts here
                //{
                //    if (BindOrderHistory().Tables[0].Rows.Count > 0)
                //    {
                //        ds = BindOrderHistory();
                //    }
                //    else
                //    {
                //    }
                //} //Main Loop ends here


            }


        }

        #region write to XML File //Accepts DataSet

        private void WriteXmlToFile(DataSet thisDataSet)
        {
            string filename = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + Environment.UserName + "-OrderHistory" + ".xml";
            if (thisDataSet == null) { return; }
            // Create a file name to write to.

            // Create the FileStream to write with.
            System.IO.FileStream myFileStream = new System.IO.FileStream
               (filename, System.IO.FileMode.Create);
            // Create an XmlTextWriter with the fileStream.
            System.Xml.XmlTextWriter myXmlWriter =
               new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode);
            // Write to the file with the WriteXml method.
            thisDataSet.WriteXml(myXmlWriter);
            myXmlWriter.Close();
        }
        #endregion

        private void AssignTotalOrders(OrderSales c)
        {
            txtbTotalOrders.Text = c.Orders;
            txtbTatalSales.Text = c.Sales;
        }

        public DataTable GetDatafromXML()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();

            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();
                    if (Request.Cookies["CNo"] != null)
                    {
                        SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                        Query = Query + " and [Contact Number] = " + Request.Cookies["CNo"].Value;
                    }

                    //Search by Date
                    if (ByDate.Checked == true)
                    {
                        if ((txtStartDate.Text.ToString().Trim() != "") && (txtEndDate.Text.ToString().Trim() != ""))
                        {
                            Query = Query + " and [Order Date] >= '" + txtStartDate.Text + "' and [Order Date] <=  '" +
                            txtEndDate.Text + "'";
                        }
                    }
                    if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                    {
                        if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                        {
                            Query = Query + " and [Order Number] = '" + SessionFacade.OrderLookUp + "'";
                        }

                    }

                    if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                    {
                        if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                        {
                            Query = Query + " and [Customer PO] = '" + SessionFacade.CustomerLookUp + "'";
                        }
                    }
                    else if (ByCal.Checked == true)
                    {
                        if (rdoCalender.Checked == true)
                        {
                            Query = Query + " and [Order Date] >= '" + "1/1/" + ddlBycal.Text +
                                "' and [Order Date] <= '" + "12/31/" + ddlBycal.Text + "'";
                        }
                        else
                        {
                            Query = Query + " and ([Order Date] >= '" + "8/1/" + (int.Parse(ddlBycal.Text) - 1).ToString() +
                                "' and [Order Date] <= '" + "7/31/" + ddlBycal.Text + "')";
                        }
                    }

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        public DataSet GetDatafromXMLArrangeColumn()
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }

        protected DataTable GetDatafromXMLTotalOrdersAndSales()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-OrderHistoryTotalOrdersSales" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = " 1=1";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            //To Copy the Schema.

            dtTemp = ds.Tables[0].Clone();
            // dtTemp.Columns["Order Date"].DataType = System.Type.GetType("System.Date");
            if (Request.Cookies["CNo"] != null)
            {
                SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                Query = Query + " and buyerct = " + Request.Cookies["CNo"].Value;
            }

            //Search by Date
            if (ByDate.Checked == true)
            {
                if ((txtStartDate.Text.ToString().Trim() != "") && (txtEndDate.Text.ToString().Trim() != ""))
                {
                    Query = Query + " and itemcreatedon >= '" + txtStartDate.Text + "' and itemcreatedon <=  '" +
                    txtEndDate.Text + "'";
                }
            }
            if (SessionFacade.OrderLookUp.ToString().Trim() != "")
            {
                if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                {
                    Query = Query + " and DOC_NUMBER = '" + SessionFacade.OrderLookUp + "'";
                }

            }

            if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
            {
                if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                {
                    Query = Query + " and BSTKD = '" + SessionFacade.CustomerLookUp + "'";
                }
            }
            else if (ByCal.Checked == true)
            {
                if (rdoCalender.Checked == true)
                {
                    Query = Query + " and itemcreatedon >= '" + "1/1/" + ddlBycal.Text +
                        "' and itemcreatedon <= '" + "12/31/" + ddlBycal.Text + "'";
                }
                else
                {
                    Query = Query + " and itemcreatedon >= '" + "8/1/" + (int.Parse(ddlBycal.Text) - 1).ToString() +
                        "' and itemcreatedon <= '" + "7/31/" + ddlBycal.Text + "'";
                }
            }

            results = ds.Tables[0].Select(Query);



            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;

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

        protected DataSet ArrangeColumn(DataSet ReArrangeDs)
        {
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";
            if (ReArrangeDs != null)
            {
                cArrangeDataSet ADS = new cArrangeDataSet();
                ADS.CampaignName = SessionFacade.CampaignValue;
                ADS.UserName = SessionFacade.LoggedInUserName;
                ADS.Listview = "lvwData";

                int IsReorder = ADS.ColumnReorderCount();
                if (IsReorder > 0)
                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
            }

            if (ReArrangeDs.Tables[0].Columns.Contains("Order Date"))
                DateOrdinal = ReArrangeDs.Tables[0].Columns["Order Date"].Ordinal;
            else
                DateOrdinal = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Unit Price"))
                UnitPrice = ReArrangeDs.Tables[0].Columns["Unit Price"].Ordinal;
            else
                UnitPrice = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Ext Price"))
                ExtPrice = ReArrangeDs.Tables[0].Columns["Ext Price"].Ordinal;
            else
                ExtPrice = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Converted Date"))
                ConvertedDate = ReArrangeDs.Tables[0].Columns["Converted Date"].Ordinal;
            else
                ConvertedDate = 1000;

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameArrangeColumn);
            ReArrangeDs.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            return ReArrangeDs;
        }

        #endregion

        #region PageChanging
        protected void gridOrderHistoryPageChanging(object sender, GridViewPageEventArgs e)
        {
            gridOrderHistory.DataSource = GetDatafromXMLArrangeColumn();
            gridOrderHistory.PageIndex = e.NewPageIndex;
            gridOrderHistory.DataBind();
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
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                    "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";

                    if (File.Exists(Pathname))
                    {

                        DataSet ReArrangeDs = new DataSet();
                        DataTable dtAllColumns = GetDatafromXML();
                        DataSet ds = new DataSet();
                        DataSet Sortedds = new DataSet();
                        DataView dv = new DataView(dtAllColumns);

                        dv.Sort = sortExpression + " " + direction;

                        ds.Tables.Add(dv.ToTable());

                        //WriteXmlToFile(ds);
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();
                        Sortedds = ArrangeColumn(ds);

                        //Working
                        //Sortedds.Tables[0].Columns.Remove("uvals");

                        if (ds.Tables[0].Columns.Contains("uvals") == true)
                            ds.Tables[0].Columns.Remove("uvals");

                        gridOrderHistory.DataSource = Sortedds;
                        gridOrderHistory.DataBind();

                        if (direction == "DESC")
                        {
                            gridOrderHistory.HeaderRow.Cells[Sortedds.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortdesc-header";

                        }
                        else
                        {
                            gridOrderHistory.HeaderRow.Cells[Sortedds.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortasc-header";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Export to Excel Function
        protected void btn_Export2ExcelClick(object sender, EventArgs e)
        {
            //Jonmar, Please read the Notes below
            ExportExcelFunction();
        }
        public void ExportExcelFunction()
        {
            try
            {
                DataSet ds = new DataSet();
                string UserFileName = SessionFacade.LoggedInUserName + "OrderHistory" + ".xls";
                if (BindOrderHistory() != null)
                {
                    if (BindOrderHistory().Tables[0].Rows.Count > 0)
                    {
                        ds = BindOrderHistory();

                    }

                    ds.Tables[0].Columns.Remove("uvals");
                    ds.AcceptChanges();

                    if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName)))
                        File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

                    File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/Ordertemp.xls"), Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));


                    string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName);


                    bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

                    //true means Excel File has been written
                    if (exportToEx == true)
                    {
                        if (Request.Browser.Type == "Desktop") //For chrome
                        {
                            ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=OrderHistory&FilePath=" + UserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=OrderHistory&FilePath=" + UserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
                        }
                    }
                    else
                    {
                        litErrorinGrid.Text = "Un able to export the data. Please contact Administartor";
                        // Response.Write("Data not Exported to Excel File");
                    }
                }



            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Order History - Error in Export to Excel" + err.ToString());
            }
        }
        #endregion

        protected void btn_ArrangeColumns(object sender, EventArgs e)
        {
            OpenNewWindow("../Home/ArrangeColumns.aspx?Data=lvwData");
        }

        public void OpenNewWindow(string url)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
        }

        protected void btn_POLOOKUPClick(object sender, EventArgs e)
        {
            //Jonmar, Please read the Notes in orde rhistory page 
            POLookUpFunction();
        }

        public void POLookUpFunction()
        {
            btnOk.Visible = true;
            btnUpdate.Visible = false;
            OpenPopup();
        }

        private void OpenPopup()
        {
            Panel66.Visible = true;
            ModalPopupExtender1.Show();
            upOuter.Update();
        }

        protected void btnOk_Click1(object sender, EventArgs e)
        {
            try
            {
                string MyLookUpText = txtCustomerLookUp.Text.ToString().Trim();
                if (MyLookUpText.Length > 0)
                {
                    MyLookUpText = MyLookUpText.Replace(",", "");
                }

                if (MyLookUpText.Length > 0)
                {
                    if (rdoOrderNumber.Checked == true && rdoCustomerLookUp.Checked == false)
                    {
                        SessionFacade.OrderLookUp = txtCustomerLookUp.Text.ToString().Trim().PadLeft(10, '0');
                        SessionFacade.CustomerLookUp = "";
                    }
                    if (rdoOrderNumber.Checked == false && rdoCustomerLookUp.Checked == true)
                    {
                        SessionFacade.OrderLookUp = "";
                        SessionFacade.CustomerLookUp = txtCustomerLookUp.Text.ToString();
                    }
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Order History- Error in Customer PO LookUp" + err.ToString());
            }

            ClosePopup();

            DataTable dt = GetDatafromXML();
            GetTotalOrdersSales(dt);

            //Working
            if (dt.Columns.Contains("uvals") == true)
                dt.Columns.Remove("uvals");

            gridOrderHistory.DataSource = dt;
            gridOrderHistory.DataBind();
            litErrorinGrid.Text = "";

            upOuter.Update();
        }

        private void ClosePopup()
        {
            Panel66.Visible = false;
            ModalPopupExtender1.Hide();
            upOuter.Update();
        }

    }
}