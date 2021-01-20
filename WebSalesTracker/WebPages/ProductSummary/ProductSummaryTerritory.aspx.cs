using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AppLogic;
using System.IO;
using BradyCorp.Log;

namespace WebSalesMine.WebPages.ProductSummary
{
    public partial class ProductSummaryTerritory : System.Web.UI.Page
    {
        public bool first = false;
        public bool Second = false;
        private int DateOrdinal = 1000;
        private int PCDateOrdinal = 1000;
        private int PCReviousDateOrdinal = 1000;
        public string TArrnageColumnstring = "lvwSKUSummaryT";

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            SetButtonVisible();

            if (!IsPostBack)
            {
                if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                {
                    pnlOterCampaign.Visible = true;
                    pnlPCCampaign.Visible = false;

                }
                if (SessionFacade.CampaignName.ToString().Trim() == "PC")
                {
                    pnlOterCampaign.Visible = false;
                    pnlPCCampaign.Visible = true;
                }

            }
            if (txtTemp.Text.ToString().Trim() != "")
            {
                if (Request.Cookies["CNo"] != null)
                {
                    SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                }

                LoadProductSummary();
                //   SessionFacade.SKUCategory = null;
                LoadSKUSummary();

                if (first == true && Second == true)
                {
                    trBlank.Visible = false;
                }
                else
                {
                    trBlank.Visible = true;
                }

                if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                {
                    pnlOterCampaign.Visible = true;
                    pnlPCCampaign.Visible = false;
                    TArrnageColumnstring = "lvwSKUSummaryT";

                }
                if (SessionFacade.CampaignName.ToString().Trim() == "PC")
                {
                    pnlOterCampaign.Visible = false;
                    pnlPCCampaign.Visible = true;
                    TArrnageColumnstring = "lvwPCSKUSummaryT";
                }
            }
            else
            {
                trBlank.Visible = true;
            }
        }

        #region Enable Disable Buttons on the page
        protected void SetButtonVisible()
        {
            if (SessionFacade.UserRole == "ADMIN")
            {
                btnExportToExcel.Visible = true;
            }
            else
            {
                btnExportToExcel.Visible = false;
            }
        }

        #endregion

        #region LoadProductSummary
        private void LoadProductSummary()
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsProductLineSummary = null;
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = SessionFacade.CampaignName;
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsProductLineSummary = objProductSummary.GetTeritoryProductSummary();


                    if (dsProductLineSummary.Tables[0].Rows.Count > 0)
                    {
                        pnlGridIndex.Visible = true;
                        Panel2.Visible = true;
                        grdProductLineSummary.DataSource = null;
                        if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                        {
                            pnlOterCampaign.Visible = true;
                            pnlPCCampaign.Visible = false;
                            grdSkuSummary.Visible = true;
                            grdProductLineSummary.DataSource = dsProductLineSummary;
                            grdProductLineSummary.DataBind();
                            lblErrorProductLineSummary.Text = "";


                        }
                        else
                        {
                            pnlOterCampaign.Visible = false;
                            pnlPCCampaign.Visible = true;
                            grdPCSKUSummary.Visible = true;
                            grdPCProductLineSummary.DataSource = dsProductLineSummary;
                            grdPCProductLineSummary.DataBind();
                            Panel2.Visible = true;
                            //lblErrorProductLineSummary.Text = "";

                        }
                        first = true;
                    }
                    else
                    {
                        if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                        {
                            grdSkuSummary.Visible = false;
                        }
                        else
                        {
                            grdPCSKUSummary.Visible = false;
                        }
                        pnlGridIndex.Visible = false;
                        Panel2.Visible = false;
                        first = false;
                    }



                }

            }
            catch (Exception ex)
            {
                lblErrorProductLineSummary.Text = "Error in Loading Product Summary table";
                BradyCorp.Log.LoggerHelper.LogException(ex, "Product Summary Page - Load ProductSummary Data", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }
        protected DataSet ReturnLoadProductSummary()
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsProductLineSummary = null;
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = SessionFacade.CampaignName;
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsProductLineSummary = objProductSummary.GetTeritoryProductSummary();


                    if (dsProductLineSummary.Tables[0].Rows.Count > 0)
                    {
                        return dsProductLineSummary;
                    }
                }
                return dsProductLineSummary;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Load SKU Summary
        private void LoadSKUSummary()
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = SessionFacade.CampaignName;
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();
                    DataSet ReArrangeDs = null;

                    grdProductLineSummary.DataSource = null;
                    if (dsSKUSummary.Tables[0].Rows.Count > 0)
                    {
                        Panel1.Visible = true;
                        Panel4.Visible = true;
                        if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                        {
                            pnlOterCampaign.Visible = true;
                            pnlPCCampaign.Visible = false;
                            grdSkuSummary.Visible = true;

                            ReArrangeDs = dsSKUSummary;

                            if (ReArrangeDs != null)
                            {
                                cArrangeDataSet ADS = new cArrangeDataSet();
                                ADS.CampaignName = SessionFacade.CampaignValue;
                                ADS.UserName = SessionFacade.LoggedInUserName;
                                ADS.Listview = "lvwSKUSummaryT";

                                int IsReorder = ADS.ColumnReorderCount();
                                if (IsReorder > 0)
                                {
                                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                                }
                            }

                            DateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;
                            grdSkuSummary.DataSource = ReArrangeDs;
                            grdSkuSummary.DataBind();
                            lblErrorSkuSummary.Text = "";
                        }
                        else
                        {
                            pnlOterCampaign.Visible = false;
                            pnlPCCampaign.Visible = true;
                            grdPCSKUSummary.Visible = true;
                            ReArrangeDs = dsSKUSummary;

                            if (ReArrangeDs != null)
                            {
                                cArrangeDataSet ADS = new cArrangeDataSet();
                                ADS.CampaignName = SessionFacade.CampaignValue;
                                ADS.UserName = SessionFacade.LoggedInUserName;
                                ADS.Listview = "lvwPCSKUSummaryT";

                                int IsReorder = ADS.ColumnReorderCount();
                                if (IsReorder > 0)
                                {
                                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                                }
                            }

                            PCDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;
                            PCReviousDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Revision Date"].Ordinal;
                            grdPCSKUSummary.DataSource = ReArrangeDs;
                            grdPCSKUSummary.DataBind();
                            Panel4.Visible = true;
                            // lblErrorSkuSummary.Text = "";
                        }
                        Second = true;
                    }
                    else
                    {
                        if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                        {
                            grdSkuSummary.Visible = false;
                        }
                        else
                        {
                            grdPCSKUSummary.Visible = false;
                        }
                        Panel1.Visible = false;
                        Panel4.Visible = false;
                        Second = false;
                    }


                }

            }
            catch (Exception ex)
            {
                lblErrorProductLineSummary.Text = "Error in Loading SKU Summary table";
                BradyCorp.Log.LoggerHelper.LogException(ex, "SKU Summary Page - Load ProductSummary Data", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected DataSet ReturnSKUSummary()
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = SessionFacade.CampaignName;
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();


                    if (dsSKUSummary.Tables[0].Rows.Count > 0)
                    {

                        return dsSKUSummary;
                    }

                }
                return dsSKUSummary;
            }
            catch (Exception ex)
            {
                return dsSKUSummary;
            }
        }
        #endregion

        #region Grid Event - RowCommand,RowDataBound

        protected void grdSkuSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool varColumnExist = false;
            int columnIndex;
            string[] list = { "F09 Sales", "F10 Sales", "F11 Sales", "F12 Sales", "Lifetime Sales", "Lifetime Orders", "F09 Units", "F10 Units", "F11 Units", "F12 Units" };

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Last Ordered Date"].ToString(), out temp) == true)
                        e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Last Ordered Date"]).ToString("MM/dd/yyyy");
                }

            }
        }

        protected void grdProductLineSummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowPLSKU")
            {
                string vsku_category = e.CommandArgument.ToString();
                SessionFacade.SKUCategory = vsku_category;
                LoadSKUSummary();
            }
        }

        protected void grdPCProductLineSummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowPLSKU")
            {
                string vsku_category = e.CommandArgument.ToString();
                SessionFacade.SKUCategory = vsku_category;
                LoadSKUSummary();
            }
        }


        protected void grdPCProductLineSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string strReviosanDate = string.Empty;
            string strOrderDate = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                strReviosanDate = DataBinder.Eval(e.Row.DataItem, "last_revision_date").ToString();
                strOrderDate = DataBinder.Eval(e.Row.DataItem, "last_order_date").ToString();
                if (strReviosanDate.Length > 0 && strOrderDate.Length > 0)
                {
                    DateTime dtReviosndate = Convert.ToDateTime(strReviosanDate);
                    DateTime dtOrderDate = Convert.ToDateTime(strOrderDate);

                    if (dtReviosndate > dtOrderDate)
                    {
                        e.Row.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void grdPCSkuSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool varColumnExist = false;
            int columnIndex;
            string[] list = { "F09 Sales", "F10 Sales", "F11 Sales", "F12 Sales", "Lifetime Sales", "Lifetime Orders", "F09 Units", "F10 Units", "F11 Units", "F12 Units" };

            string strReviosanDate = string.Empty;
            string strOrderDate = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                if (PCDateOrdinal != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Last Ordered Date"].ToString(), out temp) == true)
                        e.Row.Cells[PCDateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Last Ordered Date"]).ToString("MM/dd/yyyy");
                }

                if (PCReviousDateOrdinal != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Last Revision Date"].ToString(), out temp) == true)
                        e.Row.Cells[PCReviousDateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Last Revision Date"]).ToString("MM/dd/yyyy");
                }

                strReviosanDate = DataBinder.Eval(e.Row.DataItem, "Last Revision Date").ToString().Trim();
                strOrderDate = DataBinder.Eval(e.Row.DataItem, "Last Ordered Date").ToString().Trim();
                if (strReviosanDate.Length > 0 && strOrderDate.Length > 0)
                {
                    DateTime dtReviosndate = Convert.ToDateTime(strReviosanDate);
                    DateTime dtOrderDate = Convert.ToDateTime(strOrderDate);

                    if (dtReviosndate > dtOrderDate)
                    {
                        e.Row.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        #endregion

        #region Paging for the Other Campaign Grid

        // grdProductLineSummary Grid Paging Event

        protected void grdProductLineSummaryDataPageEventHandler(object sender, GridViewPageEventArgs e)
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsProductLineSummary = null;
            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
            {
                objProductSummary.CampaignName = SessionFacade.CampaignName;
                objProductSummary.SoldTo = SessionFacade.AccountNo;
                objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                dsProductLineSummary = objProductSummary.GetTeritoryProductSummary();
                grdProductLineSummary.DataSource = dsProductLineSummary;
                grdProductLineSummary.PageIndex = e.NewPageIndex;
                grdProductLineSummary.DataBind();

            }

        }

        //grdProductLineSummary Grid Paging Ends Here

        // grdSkuSummary Grid Paging Event
        protected void grdSkuSummaryDataPageEventHandler(object sender, GridViewPageEventArgs e)
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
            {
                objProductSummary.CampaignName = SessionFacade.CampaignName;
                objProductSummary.SoldTo = SessionFacade.AccountNo;
                objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();
                DataSet ReArrangeDs = null;
                ReArrangeDs = dsSKUSummary;

                if (ReArrangeDs != null)
                {
                    cArrangeDataSet ADS = new cArrangeDataSet();
                    ADS.CampaignName = SessionFacade.CampaignValue;
                    ADS.UserName = SessionFacade.LoggedInUserName;
                    ADS.Listview = "lvwSKUSummary";

                    int IsReorder = ADS.ColumnReorderCount();
                    if (IsReorder > 0)
                    {
                        ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                    }
                }

                DateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;


                grdSkuSummary.DataSource = ReArrangeDs;
                grdSkuSummary.PageIndex = e.NewPageIndex;
                grdSkuSummary.DataBind();
            }

        }
        // grdSkuSummary Grid Paging End here
        #endregion


        #region Paging for the PC Campaign Grid

        protected void grdPCProductLineSummaryDataPageEventHandler(object sender, GridViewPageEventArgs e)
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
            {
                objProductSummary.CampaignName = SessionFacade.CampaignName;
                objProductSummary.SoldTo = SessionFacade.AccountNo;
                objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();

                grdPCProductLineSummary.PageIndex = e.NewPageIndex;
                grdPCProductLineSummary.DataBind();
            }

        }


        protected void grdPCSkuSummaryDataPageEventHandler(object sender, GridViewPageEventArgs e)
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
            {
                objProductSummary.CampaignName = SessionFacade.CampaignName;
                objProductSummary.SoldTo = SessionFacade.AccountNo;
                objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();
                grdPCSKUSummary.DataSource = dsSKUSummary;
                grdPCSKUSummary.PageIndex = e.NewPageIndex;
                grdPCSKUSummary.DataBind();
            }

        }
        #endregion

        protected void BtnShowAllSkuSummary_Click(object sender, EventArgs e)
        {
            if (SessionFacade.CampaignName.ToString().Trim() != "PC")
            {
                pnlOterCampaign.Visible = true;
                pnlPCCampaign.Visible = false;

            }
            if (SessionFacade.CampaignName.ToString().Trim() == "PC")
            {
                pnlOterCampaign.Visible = false;
                pnlPCCampaign.Visible = true;

            }
            LoadProductSummary();
            SessionFacade.SKUCategory = null;
            LoadSKUSummary();
        }

        #region Sorting for 4 Grids

        public SortDirection GridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        private int GetColumnIndex(string SortExpression, GridView grdName)
        {
            int i = 0;
            foreach (DataControlField c in grdName.Columns)
            {
                if (c.SortExpression == SortExpression)
                    break;
                i++;
            }
            return i;
        }

        protected void grdProductLineSummary_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridProductLineSummaryView(sortExpression, "DESC");

                if (SessionFacade.CampaignName.Trim() != "PC")
                {
                    grdProductLineSummary.HeaderRow.Cells[GetColumnIndex(e.SortExpression, grdProductLineSummary)].CssClass = "sortdesc-header";
                }
                else
                {
                    grdPCProductLineSummary.HeaderRow.Cells[GetColumnIndex(e.SortExpression, grdPCProductLineSummary)].CssClass = "sortdesc-header";
                }
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridProductLineSummaryView(sortExpression, "ASC");

                if (SessionFacade.CampaignName.Trim() != "PC")
                {
                    grdProductLineSummary.HeaderRow.Cells[GetColumnIndex(e.SortExpression, grdProductLineSummary)].CssClass = "sortasc-header";
                }
                else
                {
                    grdPCProductLineSummary.HeaderRow.Cells[GetColumnIndex(e.SortExpression, grdPCProductLineSummary)].CssClass = "sortasc-header";
                }

            }
        }


        private void SortGridProductLineSummaryView(string sortExpression, string direction)
        {

            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsProductLineSummary = null;
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = SessionFacade.CampaignName;
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsProductLineSummary = objProductSummary.GetTeritoryProductSummary();


                    if (dsProductLineSummary.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = dsProductLineSummary.Tables[0];
                        DataView dv = new DataView(dt);

                        dv.Sort = sortExpression + " " + direction;

                        if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                        {
                            pnlOterCampaign.Visible = true;
                            pnlPCCampaign.Visible = false;

                            grdProductLineSummary.DataSource = dv;
                            grdProductLineSummary.DataBind();


                            lblErrorProductLineSummary.Text = "";
                        }
                        else
                        {
                            pnlOterCampaign.Visible = false;
                            pnlPCCampaign.Visible = true;

                            grdPCProductLineSummary.DataSource = dv;
                            grdPCProductLineSummary.DataBind();

                        }



                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorProductLineSummary.Text = "Error in Loading Product Summary table";
                BradyCorp.Log.LoggerHelper.LogException(ex, "Product Summary Page - Load ProductSummary Data", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }


        }
        //grdSkuSummary_Sorting

        protected void grdSkuSummary_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridSkuSummaryView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridSkuSummaryView(sortExpression, "ASC");
            }
        }


        private void SortGridSkuSummaryView(string sortExpression, string direction)
        {

            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
            {
                objProductSummary.CampaignName = SessionFacade.CampaignName;
                objProductSummary.SoldTo = SessionFacade.AccountNo;
                objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();

                DataSet ReArrangeDs = null;
                ReArrangeDs = dsSKUSummary;
                if (ReArrangeDs != null)
                {
                    cArrangeDataSet ADS = new cArrangeDataSet();
                    ADS.CampaignName = SessionFacade.CampaignValue;
                    ADS.UserName = SessionFacade.LoggedInUserName;
                    ADS.Listview = "lvwSKUSummary";

                    int IsReorder = ADS.ColumnReorderCount();
                    if (IsReorder > 0)
                    {
                        ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                    }
                }
                DateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;

                DataTable dt = dsSKUSummary.Tables[0];
                DataView dv = new DataView(dt);

                dv.Sort = sortExpression + " " + direction;
                grdSkuSummary.DataSource = dv;
                grdSkuSummary.DataBind();

                if (direction == "DESC")
                {
                    grdSkuSummary.HeaderRow.Cells[ReArrangeDs.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortdesc-header";

                }
                else
                {
                    grdSkuSummary.HeaderRow.Cells[ReArrangeDs.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortasc-header";
                }
            }
        }

        //grdPCSKUSummary

        protected void grdPCSKUSummary_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortgrdPCSKUSummaryView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortgrdPCSKUSummaryView(sortExpression, "ASC");
            }
        }


        private void SortgrdPCSKUSummaryView(string sortExpression, string direction)
        {

            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
            {
                objProductSummary.CampaignName = SessionFacade.CampaignName;
                objProductSummary.SoldTo = SessionFacade.AccountNo;
                objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();

                DataSet ReArrangeDs = null;
                ReArrangeDs = dsSKUSummary;
                if (ReArrangeDs != null)
                {
                    cArrangeDataSet ADS = new cArrangeDataSet();
                    ADS.CampaignName = SessionFacade.CampaignValue;
                    ADS.UserName = SessionFacade.LoggedInUserName;
                    ADS.Listview = "lvwPCSKUSummary";

                    int IsReorder = ADS.ColumnReorderCount();
                    if (IsReorder > 0)
                    {
                        ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                    }
                }

                PCDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;
                PCReviousDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Revision Date"].Ordinal;

                DataTable dt = dsSKUSummary.Tables[0];
                DataView dv = new DataView(dt);

                dv.Sort = sortExpression + " " + direction;
                grdPCSKUSummary.DataSource = dv;
                grdPCSKUSummary.DataBind();

                if (direction == "DESC")
                {
                    grdPCSKUSummary.HeaderRow.Cells[ReArrangeDs.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortdesc-header";

                }
                else
                {
                    grdPCSKUSummary.HeaderRow.Cells[ReArrangeDs.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortasc-header";
                }
              
            }
        }

        #endregion


        #region Export to Excel Function
        protected void btn_Export2ExcelClick(object sender, EventArgs e)
        {
            //Jonmar, Please read the Notes in orde rhistory page 
            ExportExcelFunction();
        }

        public void ExportExcelFunction()
        {

            btnOk.Visible = true;
            btnUpdate.Visible = false;
            OpenPopup();
        }

        private void OpenPopup()
        {
            Panel66.Visible = true;
            ModalPopupExtender1.Show();
        }

        protected void btnOk_Click1(object sender, EventArgs e)
        {
            try
            {
                string PageName = string.Empty;
                string tempPageName = string.Empty;
                DataSet ds = new DataSet();
                string DestinationUserFileName = string.Empty;
                if (rdoProductSummary.Checked == true && rdoSKUSummary.Checked == false)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "ProductLineTerritory.xls";
                    tempPageName = "ProductLineTerritorytemp.xls";
                    PageName = "ProductLineSummaryTerritory";
                    ds = ReturnLoadProductSummary();

                }
                if (rdoProductSummary.Checked == false && rdoSKUSummary.Checked == true)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "ProductLineSKUTerritory.xls";
                    ds = ReturnSKUSummary();
                    tempPageName = "ProductLineSKUTerritorytemp.xls";
                    PageName = "ProductLineSKUSummaryTerritory";
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName)))
                        File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName));

                    File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/" + tempPageName), Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName));
                }


                string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName);
                bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

                if (exportToEx == true)
                {
                    if (Request.Browser.Type == "Desktop") //For chrome
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=ProductSummary&FilePath=" + DestinationUserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=ProductSummary&FilePath=" + DestinationUserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
                    }
                }
                else
                {
                    // Response.Write("Data not Exported to Excel File");
                }


            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Product Summary Territory- Error in Export to Excel" + err.ToString());
            }
        }
        private void ClosePopup(string vPageName, string vFilePath)
        {
            Panel66.Visible = false;
            ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=" + vPageName + "&FilePath=" + vFilePath + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);

        }
        #endregion

    }
}