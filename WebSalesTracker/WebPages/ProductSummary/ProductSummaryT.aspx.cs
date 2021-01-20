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
using System.Text;

namespace WebSalesMine.WebPages.ProductSummary
{
    public partial class ProductSummaryT : System.Web.UI.Page
    {
        public bool first = false;
        public bool Second = false; 
        private int DateOrdinal = 1000; 
        private int PCDateOrdinal = 1000;
        private int PCReviousDateOrdinal = 1000; 
        private int F9Sales = 1000;
        private int F10Sales = 1000;
        private int F11Sales = 1000;
        private int F12Sales = 1000;
        private int lifetimesales = 1000;
        private int F9Units = 1000;
        private int F10Units = 1000;
        private int F11Units = 1000;
        private int F12Units = 1000;
        public string ArrnageColumnstring = "lvwSKUSummaryT";
        public string userRule = "";
        //private int ProductLine = 1000;
        private int ProductDescription = 1000;
        // 
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            SetButtonVisible();
            ImageSelectContact.Visible = false;
            userRule = SessionFacade.UserRole.Trim();
            if (SessionFacade.SKUCategory != "")
            {

                lnkRemoveFilters.Text = SessionFacade.SKUCategory;

            }
            else
            {
                ImageSelectContact.Visible = false;
                LinkFilterBy.Visible = false;
            }

            //btnExportToExcel.Text = "hahahaahahaa";

            //if (txtTemp.Text.ToString().Trim() != "")
            //{

                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                SessionFacade.CampaignValue = ddlTemp.Text.ToString().Trim();
                SessionFacade.CampaignName = ddlTemp.SelectedValue.ToString().Trim();



                if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                {
                    pnlOterCampaign.Visible = true;
                    pnlPCCampaign.Visible = false;
                    ArrnageColumnstring = "lvwSKUSummaryT";



                }
                if (SessionFacade.CampaignName.ToString().Trim() == "PC")
                {
                    pnlOterCampaign.Visible = false;
                    pnlPCCampaign.Visible = true;
                    ArrnageColumnstring = "lvwPCSKUSummaryT";
                }

                if (Request.Cookies["CNo"] != null)
                {
                    SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;

                    //chkContactLevel.Checked = true;
                }
                else
                {
                    SessionFacade.BuyerCt = "";
                    //  chkContactLevel.Checked = false;
                }

                string CName;
                if (Request.Cookies["CName"] != null)
                {
                    CName = Request.Cookies["CName"].Value.ToString();
                    CName = CName.Replace("%2C", ",");
                    CName = CName.Replace("%20", " ");
                    lnkContactSelected.Text = CName;
                    ImageSelectContact2.Visible = true;
                }
                else
                {
                    lnkContactSelected.Text = "";
                    ImageSelectContact2.Visible = false;
                }

                
              


                LoadProductSummary();
                //SessionFacade.SKUCategory = null;
                LoadSKUSummary();



            //}
            //else
            //{
            //    trBlank.Visible = true;
            //}

                //Label Name
                RenameLabelName();

        }

        #region Renaming Label for Fiscal Sales and Units
        private void RenameLabelName()
        {
            int year;
            if (DateTime.Now.Month >= 8)
            {
                //Get Last to digit of a year.
                year = (DateTime.Now.Year % 100) + 1;
            }
            else
            {
                //Get Last to digit of a year.
                year = (DateTime.Now.Year % 100);
            }

            lblFO9.Text = "F" + (year - 3).ToString() + " Sales";
            lblF10.Text = "F" + (year - 2).ToString() + " Sales";
            lblF11.Text = "F" + (year - 1).ToString() + " Sales";
            lblF12.Text = "F" + (year).ToString() + " Sales";

            lblf09units.Text = "F" + (year - 3).ToString() + " Units";
            lblf10units.Text = "F" + (year - 2).ToString() + " Units";
            lblf11units.Text = "F" + (year - 1).ToString() + " Units";
            lblf12units.Text = "F" + (year).ToString() + " Units";
        }
        #endregion


        #region Read From XML And load to pop

        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLDetails(string SKUNumber, string LastOrderedDate)
        {
            DataSet ds = new DataSet();

            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ProductSummary" + ".xml";
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

                    //Search by Date


                    if (SKUNumber != null && LastOrderedDate != null)
                    {

                        Query = Query + " and [sku_number] = '" + SKUNumber + "' and " +
                                   "[last_order_date] = '" + LastOrderedDate + "'";
                  
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

        #endregion
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

        #region Load Product Summary from the DB
        protected DataSet PSummaryFromDB()
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsProductLineSummary = null;
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-ProductSummary" + ".xml";
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = SessionFacade.CampaignName;
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsProductLineSummary = objProductSummary.GetProductSummary();
                }

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                dsProductLineSummary.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                return dsProductLineSummary;
            }
            catch (Exception ex)
            {
                lblErrorProductLineSummary.Text = "Error in getting Product Summary table from the DataBase";
                BradyCorp.Log.LoggerHelper.LogException(ex, "Product Summary Page - Load ProductSummary Data", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),SessionFacade.KamId.ToString());
                return null;
            }
        }
        #endregion

        #region Load Product Summary from XML
        protected DataSet PSummaryFromXML()
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-ProductSummary" + ".xml";
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();
            return ds;

            if (Request.Cookies["CNo"] != null)
            {
                SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;

                //chkContactLevel.Checked = true;
            }
            //    else
            //        //chkContactLevel.Checked = false;
        }
        #endregion

        #region Prerender for ProductLine
        protected void grdProductLineSummary_Prerender(object sender, EventArgs e)
        {
            if (grdProductLineSummary.HeaderRow != null)
            {
                int year;
                LinkButton lnk = new LinkButton();

                if (DateTime.Now.Month >= 8)
                {
                    //Get Last to digit of a year.
                    year = (DateTime.Now.Year % 100) + 1;
                }
                else
                {
                    //Get Last to digit of a year.
                    year = (DateTime.Now.Year % 100);
                }


                lnk = grdProductLineSummary.HeaderRow.Cells[2].Controls[0] as LinkButton;
                lnk.Text = "F" + (year - 3).ToString() + " SALES";

                lnk = grdProductLineSummary.HeaderRow.Cells[3].Controls[0] as LinkButton;
                lnk.Text = "F" + (year - 2).ToString() + " SALES";

                lnk = grdProductLineSummary.HeaderRow.Cells[4].Controls[0] as LinkButton;
                lnk.Text = "F" + (year - 1).ToString() + " SALES";

                lnk = grdProductLineSummary.HeaderRow.Cells[5].Controls[0] as LinkButton;
                lnk.Text = "F" + (year).ToString() + " SALES";

                lnk = grdProductLineSummary.HeaderRow.Cells[9].Controls[0] as LinkButton;
                lnk.Text = "F" + (year - 3).ToString() + " UNITS";

                lnk = grdProductLineSummary.HeaderRow.Cells[10].Controls[0] as LinkButton;
                lnk.Text = "F" + (year - 2).ToString() + " UNITS";

                lnk = grdProductLineSummary.HeaderRow.Cells[11].Controls[0] as LinkButton;
                lnk.Text = "F" + (year - 1).ToString() + " UNITS";

                lnk = grdProductLineSummary.HeaderRow.Cells[12].Controls[0] as LinkButton;
                lnk.Text = "F" + (year).ToString() + " UNITS";
            }
        }
        #endregion

        #region LoadProductSummary
        private void LoadProductSummary()
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsProductLineSummary = null;
            try
            {

                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = ddlTemp.Text.ToString().Trim();
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;


                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsProductLineSummary = objProductSummary.GetTeritoryProductSummary();

                    if (dsProductLineSummary.Tables[0].Rows.Count > 0)
                    {
                        grdProductLineSummary.DataSource = null;
                        pnlGridIndex.Visible = true;
                        Panel2.Visible = true;
                        if (ddlTemp.Text.ToString().Trim() != "PC")
                        {
                            pnlOterCampaign.Visible = true;
                            pnlPCCampaign.Visible = false;
                            grdProductLineSummary.Visible = true;
                            grdProductLineSummary.DataSource = dsProductLineSummary;
                            grdProductLineSummary.DataBind();
                            lblErrorProductLineSummary.Text = "";
                        }
                        else
                        {
                            grdPCProductLineSummary.DataSource = null;
                            pnlOterCampaign.Visible = false;
                            pnlPCCampaign.Visible = true;
                            grdPCProductLineSummary.Visible = true;
                            grdPCProductLineSummary.DataSource = dsProductLineSummary;
                            grdPCProductLineSummary.DataBind();
                            Panel2.Visible = true;
                            //lblErrorProductLineSummary.Text = "";

                        }
                        first = true;

                    }
                    else
                    {
                        if (ddlTemp.Text.ToString().Trim() != "PC")
                        {
                            grdProductLineSummary.Visible = false;
                        }
                        else
                        {
                            grdPCProductLineSummary.Visible = false;
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

                    dsProductLineSummary = objProductSummary.GetProductSummary();


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
        #region Old Code
        //private void LoadSKUSummary()
        //{
        //    cProductSummary objProductSummary = new cProductSummary();
        //    DataSet dsSKUSummary = null;
        //    DataSet dsXMLSKUSummary = null; 
        //    try
        //    {
        //        DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
        //        if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
        //        {
        //            objProductSummary.CampaignName = ddlTemp.Text.ToString().Trim();
        //            objProductSummary.SoldTo = SessionFacade.AccountNo;
        //            objProductSummary.SKUCategory = SessionFacade.SKUCategory;
        //            objProductSummary.BuyerCt = SessionFacade.BuyerCt;

        //            dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();
        //            DataSet ReArrangeDs = null;

        //            if (dsSKUSummary.Tables[0].Rows.Count > 0)
        //            {
        //                // Panel1.Visible = true;
        //                // Panel4.Visible = true;
        //                grdProductLineSummary.DataSource = null;
        //                if (ddlTemp.Text.ToString().Trim() != "PC")
        //                {
        //                    dsSKUSummary.Tables[0].Columns.Remove("PRODUCT CATEGORY");
        //                    grdSkuSummary.DataSource = null;
        //                    pnlOterCampaign.Visible = true;
        //                    pnlPCCampaign.Visible = false;
        //                    grdSkuSummary.Visible = true;

        //                    ReArrangeDs = dsSKUSummary;

        //                    if (ReArrangeDs != null)
        //                    {
        //                        cArrangeDataSet ADS = new cArrangeDataSet();
        //                        ADS.CampaignName = SessionFacade.CampaignValue;
        //                        ADS.UserName = SessionFacade.LoggedInUserName;
        //                        ADS.Listview = "lvwSKUSummary";

        //                        int IsReorder = ADS.ColumnReorderCount();
        //                        if (IsReorder > 0)
        //                        {
        //                            ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
        //                        }
        //                    }


        //                    if (ReArrangeDs.Tables[0].Columns.Contains("LAST ORDER DATE"))
        //                        DateOrdinal = ReArrangeDs.Tables[0].Columns["LAST ORDER DATE"].Ordinal;
        //                    else
        //                        DateOrdinal = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("F10 SALES"))
        //                        F9Sales = ReArrangeDs.Tables[0].Columns["F10 SALES"].Ordinal;
        //                    else
        //                        F9Sales = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("F11 SALES"))
        //                        F10Sales = ReArrangeDs.Tables[0].Columns["F11 SALES"].Ordinal;
        //                    else
        //                        F10Sales = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("F12 SALES"))
        //                        F11Sales = ReArrangeDs.Tables[0].Columns["F12 SALES"].Ordinal;
        //                    else
        //                        F11Sales = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("F13 SALES"))
        //                        F12Sales = ReArrangeDs.Tables[0].Columns["F13 SALES"].Ordinal;
        //                    else
        //                        F12Sales = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("LIFETIME SALES"))
        //                        lifetimesales = ReArrangeDs.Tables[0].Columns["LIFETIME SALES"].Ordinal;
        //                    else
        //                        lifetimesales = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("F10 UNITS"))
        //                        F9Units = ReArrangeDs.Tables[0].Columns["F10 UNITS"].Ordinal;
        //                    else
        //                        F9Units = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("F11 UNITS"))
        //                        F10Units = ReArrangeDs.Tables[0].Columns["F11 UNITS"].Ordinal;
        //                    else
        //                        F10Units = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("F12 UNITS"))
        //                        F11Units = ReArrangeDs.Tables[0].Columns["F12 UNITS"].Ordinal;
        //                    else
        //                        F11Units = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("F13 UNITS"))
        //                        F12Units = ReArrangeDs.Tables[0].Columns["F13 UNITS"].Ordinal;
        //                    else
        //                        F12Units = 1000;

        //                    grdSkuSummary.DataSource = ReArrangeDs;
        //                    grdSkuSummary.DataBind();
        //                    lblErrorSkuSummary.Text = "";


        //                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ProductSummary" + ".xml";
        //                    if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
        //                    {

        //                        dsXMLSKUSummary = objProductSummary.GetAllTeritorySKUSummary();
        //                    }

        //                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
        //                    dsXMLSKUSummary.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //                    xmlSW.Close();


        //                }
        //                else
        //                {

        //                    grdPCSKUSummary.DataSource = null;
        //                    pnlOterCampaign.Visible = false;
        //                    pnlPCCampaign.Visible = true;
        //                    grdPCSKUSummary.Visible = true;

        //                    ReArrangeDs = dsSKUSummary;

        //                    if (ReArrangeDs != null)
        //                    {
        //                        cArrangeDataSet ADS = new cArrangeDataSet();
        //                        ADS.CampaignName = SessionFacade.CampaignValue;
        //                        ADS.UserName = SessionFacade.LoggedInUserName;
        //                        ADS.Listview = "lvwPCSKUSummary";

        //                        int IsReorder = ADS.ColumnReorderCount();
        //                        if (IsReorder > 0)
        //                        {
        //                            ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
        //                        }
        //                    }

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("LAST ORDER DATE"))
        //                        PCDateOrdinal = ReArrangeDs.Tables[0].Columns["LAST ORDER DATE"].Ordinal;
        //                    else
        //                        PCDateOrdinal = 1000;

        //                    if (ReArrangeDs.Tables[0].Columns.Contains("LAST REVISION DATE"))
        //                        PCReviousDateOrdinal = ReArrangeDs.Tables[0].Columns["LAST REVISION DATE"].Ordinal;
        //                    else
        //                        PCReviousDateOrdinal = 1000;

        //                    grdPCSKUSummary.DataSource = ReArrangeDs;
        //                    grdPCSKUSummary.DataBind();
        //                    Panel4.Visible = true;


        //                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ProductSummary" + ".xml";
        //                    if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
        //                    {

        //                        dsXMLSKUSummary = dsSKUSummary;
        //                    }

        //                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
        //                    dsXMLSKUSummary.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //                    xmlSW.Close();


        //                    // lblErrorSkuSummary.Text = "";
        //                }
        //                Second = true;

        //            }
        //            else
        //            {
        //                if (ddlTemp.Text.ToString().Trim() != "PC")
        //                {
        //                    grdSkuSummary.Visible = false;
        //                }
        //                else
        //                {
        //                    grdPCSKUSummary.Visible = false;
        //                }
        //                //Panel1.Visible = false;
        //                //Panel4.Visible = false;
        //                Second = false;
        //            }

        //        }


        //        // SessionFacade.FILTERVAL = vsku_category;
        //        if (SessionFacade.SKUCategory == "")
        //        {
        //            ImageSelectContact.Visible = false;
        //            SessionFacade.SKUCategory = "";
        //            LinkFilterBy.Visible = false;
        //        }
        //        else
        //        {
        //            //SessionFacade.PRODLINE = vsku_category;
        //            LinkFilterBy.Visible = true;
        //            lnkRemoveFilters.Text =  SessionFacade.SKUCategory;
        //            ImageSelectContact.Visible = true;
        //            lnkRemoveFilters.Visible = true;
        //        }


        //        //if (SessionFacade.PRODLINE == "")
        //        //{
        //        //    ImageSelectContact.Visible = false;
        //        //    SessionFacade.PRODLINE = "";
        //        //}
        //        //else
        //        //{

        //        //    lnkRemoveFilters.Text = SessionFacade.PRODLINE;
        //        //    ImageSelectContact.Visible = true;
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        lblErrorProductLineSummary.Text = "Error in Loading SKU Summary table";
        //        BradyCorp.Log.LoggerHelper.LogException(ex, "SKU Summary Page - Load ProductSummary Data", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
        //    }
        //}
        #endregion

        private void LoadSKUSummary()
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            DataSet dsXMLSKUSummary = null;
            try
            {
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;

    
                    objProductSummary.CampaignName = ddlTemp.Text.ToString().Trim();
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsSKUSummary = objProductSummary.GetAllTeritorySKUSummary();
                    DataSet ReArrangeDs = null;

                    if (dsSKUSummary != null && dsSKUSummary.Tables.Count > 0)
                    {
                        int year;
                        if (DateTime.Now.Month >= 8)
                        {
                            //Get Last to digit of a year.
                            year = (DateTime.Now.Year % 100) + 1;
                        }
                        else
                        {
                            //Get Last to digit of a year.
                            year = (DateTime.Now.Year % 100);
                        }

                        if (ddlTemp.Text.ToString().Trim() != "PC")
                        {
                            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ProductSummary" + ".xml";
                            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                            {
                                dsXMLSKUSummary = dsSKUSummary;
                            }

                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            dsXMLSKUSummary.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            if (dsSKUSummary.Tables[0].Columns.Contains("sku_category"))
                                dsSKUSummary.Tables[0].Columns.Remove("sku_category");
                            grdSkuSummary.DataSource = null;
                            pnlOterCampaign.Visible = true;
                            pnlPCCampaign.Visible = false;
                            grdSkuSummary.Visible = true;

                            //Load Column Headers
                            DataSet dsColumnAvailable = new DataSet();
                            cArrangeDataSet ADS = new cArrangeDataSet();
                            ADS.CampaignName = ddlTemp.Text.ToString().Trim();
                            ADS.Listview = "lvwSKUSummary";

                            dsColumnAvailable = ADS.GetColumnDetails();

                            if (dsColumnAvailable != null && dsColumnAvailable.Tables.Count > 0)
                            {
                                //Assign Column 
                                grdSkuSummary.Columns.Clear();
                                //Gererate Column
                                BoundField bfield = new BoundField();
                                foreach (DataRow dr in dsColumnAvailable.Tables[0].Rows)
                                {
                                    bfield = new BoundField();

                                    bfield.HeaderText = dr["ColumnName"].ToString();

                                    bfield.DataField = dr["ColumnValue"].ToString();
                                    bfield.SortExpression = dr["ColumnValue"].ToString();

                                    //Alignment of the value
                                    if (dr["Position"].ToString() == "L")
                                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                                    else
                                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                                    //Data format
                                    if (dr["DataFormat"].ToString() != "")
                                        bfield.DataFormatString = dr["DataFormat"].ToString();

                                    grdSkuSummary.Columns.Add(bfield);
                                }
                            }

                            if (ddlTemp.SelectedValue == "DE" ||
                                ddlTemp.SelectedValue == "AT")
                            {
                                if (dsSKUSummary.Tables[0].Columns.Contains("SKU_Description"))
                                    ProductDescription = dsSKUSummary.Tables[0].Columns["SKU_Description"].Ordinal;
                                else
                                    ProductDescription = 1000;
                            }


                            grdSkuSummary.DataSource = dsSKUSummary;
                            grdSkuSummary.DataBind();
                            lblErrorSkuSummary.Text = "";

                        }
                        else
                        {


                            //Load Column Headers
                            DataSet dsColumnAvailable = new DataSet();
                            cArrangeDataSet ADS = new cArrangeDataSet();
                            ADS.CampaignName = ddlTemp.Text.ToString().Trim();
                            ADS.Listview = "lvwPCSKUSummary";

                            dsColumnAvailable = ADS.GetColumnDetails();

                            if (dsColumnAvailable != null && dsColumnAvailable.Tables.Count > 0)
                            {
                                //Assign Column 
                                grdPCSKUSummary.Columns.Clear();
                                //Gererate Column
                                BoundField bfield = new BoundField();
                                foreach (DataRow dr in dsColumnAvailable.Tables[0].Rows)
                                {
                                    bfield = new BoundField();

                                    bfield.HeaderText = dr["ColumnName"].ToString();

                                    bfield.DataField = dr["ColumnValue"].ToString();
                                    bfield.SortExpression = dr["ColumnValue"].ToString();

                                    //Alignment of the value
                                    if (dr["Position"].ToString() == "L")
                                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                                    else
                                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                                    //Data format
                                    if (dr["DataFormat"].ToString() != "")
                                        bfield.DataFormatString = dr["DataFormat"].ToString();

                                    grdPCSKUSummary.Columns.Add(bfield);
                                }
                            }

                            grdPCSKUSummary.DataSource = dsSKUSummary;
                            grdPCSKUSummary.DataBind();
                            Panel4.Visible = true;

                            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ProductSummary" + ".xml";
                            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                            {
                                dsXMLSKUSummary = dsSKUSummary;
                            }

                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            dsXMLSKUSummary.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();



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
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = ddlTemp.Text.ToString().Trim();
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

        #region Select individual ProductLine from top grid
        protected void grdProductLineSummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowPLSKU")
            {
                string vsku_category = e.CommandArgument.ToString();
                SessionFacade.SKUCategory = vsku_category;
               // SessionFacade.PRODLINE = vsku_category;
                // SessionFacade.FILTERVAL = vsku_category;

                if (SessionFacade.SKUCategory == "")
                {
                    ImageSelectContact.Visible = false;
                    SessionFacade.SKUCategory = "";
                    LinkFilterBy.Visible = false;
                }
                else
                {
                    //SessionFacade.PRODLINE = vsku_category;
                    LinkFilterBy.Visible = true;
                    lnkRemoveFilters.Text =  SessionFacade.SKUCategory;
                    ImageSelectContact.Visible = true;
                    lnkRemoveFilters.Visible = true;
                }
                
                
                LoadSKUSummary();
            }
        }

        protected void grdPCProductLineSummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowPLSKU")
            {

                string vsku_category = e.CommandArgument.ToString();
                SessionFacade.SKUCategory = vsku_category;
                //SessionFacade.PRODLINE = vsku_category;
                // SessionFacade.FILTERVAL = vsku_category;

                if (SessionFacade.SKUCategory == "")
                {
                    ImageSelectContact.Visible = false;
                    SessionFacade.SKUCategory = "";
                    LinkFilterBy.Visible = false;
                }
                else
                {
                    //SessionFacade.PRODLINE = vsku_category;
                    LinkFilterBy.Visible = true;
                    lnkRemoveFilters.Text = SessionFacade.SKUCategory;
                    ImageSelectContact.Visible = true;
                    lnkRemoveFilters.Visible = true;
                }
                
                LoadSKUSummary();
            }
        }

        #endregion

        #region RowBound for Both Grids
        #region Old Code
        //protected void grdSkuSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    bool varColumnExist = false;
        //    int columnIndex;
        //    string[] list = { "F10 SALES", "F11 SALES", "F12 SALES", "F13 SALES", "LIFETIME SALES", "LIFETIME ORDERS", "F10 UNITS", "F11 UNITS", "F12 UNITS", "F13 UNITS","LAST ORDER DATE" };

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //Right Align of Currency Column
        //        for (int i = 0; i < list.Length; i++)
        //        {
        //            columnIndex = 0;
        //            foreach (DataControlFieldCell cell in e.Row.Cells)
        //            {
        //                if (cell.ContainingField is BoundField)
        //                {
        //                    if (((BoundField)cell.ContainingField).DataField.Equals(list[i]))
        //                    {
        //                        varColumnExist = true;
        //                        break;
        //                    }
        //                    else
        //                        varColumnExist = false;
        //                }
        //                columnIndex++;
        //            }

        //            if (varColumnExist == true)
        //            {
        //                if (e.Row.RowType == DataControlRowType.DataRow)
        //                    e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;

        //            }
        //        }
        //        if (DateOrdinal != 1000)
        //        {
        //            DateTime temp;
        //            if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["LAST ORDER DATE"].ToString(), out temp) == true)
        //                e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["LAST ORDER DATE"]).ToString("MM/dd/yyyy");


        //        }

        //        if (F9Sales != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[F9Sales].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["F10 SALES"].ToString()).ToString("##,#####");
        //        }

        //        if (F10Sales != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[F10Sales].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["F11 SALES"].ToString()).ToString("##,#####");
        //        }

        //        if (F11Sales != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[F11Sales].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["F12 SALES"].ToString()).ToString("##,#####");
        //        }

        //        if (F12Sales != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[F12Sales].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["F13 SALES"].ToString()).ToString("##,#####");
        //        }

        //        if (lifetimesales != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[lifetimesales].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["LIFETIME SALES"].ToString()).ToString("##,#####");
        //        }

        //        if (F9Units != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[F9Units].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["F10 UNITS"].ToString()).ToString("##,#####");
        //        }

        //        if (F10Units != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[F10Units].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["F11 UNITS"].ToString()).ToString("##,#####");
        //        }

        //        if (F11Units != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[F11Units].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["F12 UNITS"].ToString()).ToString("##,#####");
        //        }

        //        if (F12Units != 1000)
        //        {
        //            //e.Row.Cells[DateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["F09 Sales"]).ToString("c0");
        //            e.Row.Cells[F12Units].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["F13 UNITS"].ToString()).ToString("##,#####");
        //        }

        //    }

        //    //            If e.Row.Cells("GoodsDesc").Text.Length > MaxChar Then
        //    //e.Row.Cells("GoodsDesc").Text = e.Row.Cells(6).Text.Substring(0, MaxChar)
        //    //End If
        //}
        #endregion

        protected void grdSkuSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Change the unknown character for Germany unit.
                if (ddlTemp.SelectedValue == "DE" ||
                    ddlTemp.SelectedValue == "AT")
                {
                    if (ProductDescription != 1000)
                    {
                        byte[] data = Encoding.Default.GetBytes(((DataRowView)e.Row.DataItem)["SKU_Description"].ToString());
                        string output = Encoding.UTF8.GetString(data);

                        e.Row.Cells[ProductDescription].Text = output;
                    }
                }
            }
        }

        protected void grdPCProductLineSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string strReviosanDate = string.Empty;
            string strOrderDate = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                strReviosanDate = DataBinder.Eval(e.Row.DataItem, "LAST REVISION DATE").ToString();
                strOrderDate = DataBinder.Eval(e.Row.DataItem, "LAST ORDER DATE").ToString();
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
            string[] list = { "F12 SALES", "F13 SALES", "F14 SALES", "F15 SALES", "LIFETIME SALES", "LIFETIME ORDERS", "F12 UNITS", "F13 UNITS", "F14 UNITS", "F15 UNITS","LAST ORDER DATE" };

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
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["LAST ORDER DATE"].ToString(), out temp) == true)
                        e.Row.Cells[PCDateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["LAST ORDER DATE"]).ToString("MM/dd/yyyy");
                }

                if (PCReviousDateOrdinal != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["LAST REVISION DATE"].ToString(), out temp) == true)
                        e.Row.Cells[PCReviousDateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["LAST REVISION DATE"]).ToString("MM/dd/yyyy");
                }

                strReviosanDate = DataBinder.Eval(e.Row.DataItem, "LAST REVISION DATE").ToString().Trim();
                strOrderDate = DataBinder.Eval(e.Row.DataItem, "LAST ORDER DATE").ToString().Trim();
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

               // dsProductLineSummary = objProductSummary.GetProductSummary();
                dsProductLineSummary = objProductSummary.GetTeritoryProductSummary();//.GetAllTeritorySKUSummary();

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

                dsSKUSummary = objProductSummary.GetAllSKUSummary();
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

                if (ReArrangeDs.Tables[0].Columns.Contains("Last Ordered Date"))
                    DateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;
                else
                    DateOrdinal = 1000;

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

               // dsSKUSummary = objProductSummary.GetAllSKUSummary();
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

                dsSKUSummary = objProductSummary.GetAllSKUSummary();

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

                if (ReArrangeDs.Tables[0].Columns.Contains("Last Ordered Date"))
                    PCDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;
                else
                    PCDateOrdinal = 1000;

                if (ReArrangeDs.Tables[0].Columns.Contains("Last Revision Date"))
                    PCReviousDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Revision Date"].Ordinal;
                else
                    PCReviousDateOrdinal = 1000;

                grdPCSKUSummary.DataSource = ReArrangeDs;
                grdPCSKUSummary.PageIndex = e.NewPageIndex;
                grdPCSKUSummary.DataBind();
            }

        }
        #endregion

        protected void BtnShowAllSkuSummary_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(SessionFacade.FILTERVAL))
            //{
            SessionFacade.PRODLINE = "";
            lnkRemoveFilters.Text = "";
            //lnkRemoveFilters.Visible=false;
            ImageSelectContact.Visible=false;
            if (SessionFacade.CampaignName.ToString().Trim() != "PC")
            {
                pnlOterCampaign.Visible = true;
                pnlPCCampaign.Visible = false;

            }
            if (SessionFacade.CampaignName.ToString().Trim() == "PC")
            {
                pnlOterCampaign.Visible = false;
                pnlPCCampaign.Visible = true;
                SessionFacade.SKUCategory = null;
            }

            //  SessionFacade.FILTERVAL = null;
            LoadProductSummary();
            SessionFacade.SKUCategory = null;
            LoadSKUSummary();
            //  }

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

                    dsProductLineSummary = objProductSummary.GetProductSummary();


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

                dsSKUSummary = objProductSummary.GetAllSKUSummary();
                dsSKUSummary.Tables[0].Columns.Remove("PRODUCT CATEGORY");
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

                if (ReArrangeDs.Tables[0].Columns.Contains("LAST ORDERED DATE"))
                    DateOrdinal = ReArrangeDs.Tables[0].Columns["LAST ORDERED DATE"].Ordinal;
                else
                    DateOrdinal = 1000;

                DataTable dt = ReArrangeDs.Tables[0];
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
                // grdPCSKUSummary.HeaderRow.Cells[SKUGetColumnIndex(e.SortExpression)].CssClass = "sortdesc-header";
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortgrdPCSKUSummaryView(sortExpression, "ASC");
                //grdPCSKUSummary.HeaderRow.Cells[SKUGetColumnIndex(e.SortExpression)].CssClass = "sortasc-header";
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

                dsSKUSummary = objProductSummary.GetAllSKUSummary();

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

                if (ReArrangeDs.Tables[0].Columns.Contains("LAST ORDERED DATE"))
                    PCDateOrdinal = ReArrangeDs.Tables[0].Columns["LAST ORDERED DATE"].Ordinal;
                else
                    PCDateOrdinal = 1000;

                if (ReArrangeDs.Tables[0].Columns.Contains("LAST REVISION DATE"))
                    PCReviousDateOrdinal = ReArrangeDs.Tables[0].Columns["LAST REVISION DATE"].Ordinal;
                else
                    PCReviousDateOrdinal = 1000;

                DataTable dt = ReArrangeDs.Tables[0];
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
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "ProductLine.xls";
                    tempPageName = "ProductLinetemp.xls";
                    PageName = "ProductLineSummary";
                    ds = ReturnLoadProductSummary();


                }
                if (rdoProductSummary.Checked == false && rdoSKUSummary.Checked == true)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "ProductLineSKU.xls";
                    ds = ReturnSKUSummary();
                    tempPageName = "ProductLineSKUtemp.xls";
                    PageName = "ProductLine_SKUSummary";


                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName)))
                        File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName));

                    File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/" + tempPageName), Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName));
                }

                string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName);
                bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

                ClosePopup(PageName, DestinationUserFileName);

                if (exportToEx == true)
                {
                    if (Request.Browser.Type == "Desktop") //For chrome
                    {
                        ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

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
                BradyCorp.Log.LoggerHelper.LogMessage("Product Summary - Error in Export to Excel" + err.ToString());
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