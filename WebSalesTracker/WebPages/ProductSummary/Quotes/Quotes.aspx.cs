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
using System.Globalization;
using System.Text;
using WebSalesMine.WebPages.UserControl;

namespace WebSalesMine.WebPages.Quotes
{
    public partial class Quotes : System.Web.UI.Page
    {

        string DefaultQuotesColumns = "Quote_Doc_No|Quote_Line|Quote_Date|Quote_PO_Type|Quote_PO_Type_Desc|Quote_Item_Categ_Desc|Quote_Mat_Entrd|Quote_Mat_Entrd_Desc|Quote_SlsTeamIN|Quote_Discount|Quote_Net_Sales|DM_Product_Line_Desc|quote_qty|quote_unit_price|Quote_Cost";
        //string DefaultQuotesColumns = "Quote_Doc_No|Quote_Line|Quote_Date|Quote_PO_Type|Quote_PO_Type_Desc|Quote_Item_Categ_Desc|Quote_Mat_Entrd|Quote_Mat_Entrd_Desc|Quote_SlsTeamIN|Quote_Discount|Quote_Net_Sales|DM_Product_Line_Desc";
        public string userRule = "",
               AccountQ = string.Empty;
        public DropDownList ddlCampaignCurrencyQ = new DropDownList();
        public DropDownList ddlCampaignQ = new DropDownList();

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] ColumnName = { "Quote_Doc_No",
                                      "Quote_Doc_createdon" };
            NewMasterPage MasterPage = Master as NewMasterPage;
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes" + ".xml";
            DataSet ds = new DataSet();
            DataTable dtDistinctValues;
            DataView view;
            userRule = SessionFacade.UserRole.Trim();

            GetAccountInfo(MasterPage);

            //Browser
            if (Request.Browser.Type.Contains("Chrome")) // replace with your check
            {
                tdQuoteInfo.Style.Add("position", "absolute");
                tdQuoteInfo.Style.Add("left", "270px");
                tdCusInfo.Style.Add("position", "absolute");
                tdCusInfo.Style.Add("left", "830px");
                tdCusInfo.Style.Add("top", "134px");
            }
            else
            {
                tdQuoteInfo.Style.Add("position", "absolute");
                tdQuoteInfo.Style.Add("left", "260px");
                tdCusInfo.Style.Add("position", "absolute");
                tdCusInfo.Style.Add("left", "790px");
                tdCusInfo.Style.Add("top", "132px");
            }

            lnkMasterDataChange.Visible = false;

            if (gridQuoteNumber.SelectedIndex == -1)
            {
                //Check if Account No is not Empty
                if (AccountQ.Trim() != "")
                {
                    BindListBox();
                    if (Request.Cookies["OKSelectContact"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("OKSelectContact");
                        myCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(myCookie);
                    }
                    //This code used in Linkbuton in Quote Pipeline Page
                    if (Request.Cookies["QNoTemp"] == null)
                    {

                        if (Request.Cookies["QNo"] != null)
                        {

                            for (int i = 0; i < gridQuoteNumber.Rows.Count; i++)
                            {

                                if (gridQuoteNumber.Rows[i].Cells[1].Text == Request.Cookies["QNo"].Value)
                                {
                                    gridQuoteNumber.SelectedIndex = i;
                                    break;
                                }
                            }

                            DisplayQuote();
                        }
                    }
                }
            }
            else
            {
                if ((Request.Cookies["OKSelectContact"] != null) && (Request.Cookies["OKSelectContact"].Value != ""))
                {
                    if (Request.Cookies["OKSelectContact"].Value != "")
                    {
                        BindListBox();
                    }

                }

                string ControlId = string.Empty;
                ControlId = getPostBackControlID();

                if ((AccountQ.Trim() != "" && AccountQ.Trim() != "0000000000"))
                {
                    if (ControlId == null || ControlId == "BtnNotesColumn2" ||
                        ControlId == "ddlCampaign" || ControlId == "imbSearchProjID" ||
                        ControlId == "txtProjectID" ||
                        ControlId == "imbSearchAcntNumber" || ControlId == "txtAccountNumber")
                    {
                        BindListBox();
                    }
                }
            }

            string CName;
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
                    Request.Cookies["CNo"].Expires = DateTime.Now;

                    //Remove Cookies
                    HttpCookie myCookie = new HttpCookie("CNo");
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookie);

                    ////Copy to view
                    view = new DataView(GetDatafromXML());

                    // Distinct Quote Number
                    dtDistinctValues = view.ToTable(true, ColumnName);

                    gridQuoteNumber.DataSource = dtDistinctValues;
                    gridQuoteNumber.DataBind();

                    DisplayQuote();

                }
                ImageSelectContact.Visible = false;
                lnkContactSelected.Text = "";
            }
            CurrencyCode.Value = ddlCampaignCurrencyQ.SelectedItem.Text;
        }

        private void GetAccountInfo(NewMasterPage MasterPage)
        {
            if (MasterPage != null)
            {
                AccountQ = MasterPage.AccountNumberMaster.FormatAccountNumber();
                ddlCampaignQ = MasterPage.CampaignMaster;
                ddlCampaignCurrencyQ = MasterPage.CampaignCurrencyMaster;
            }
        }

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
                return null;
            }
            else
            {
                return control.ID;
            }
        }

        private void ClearControls()
        {
            txtSalesRep.Text = "";
            txtQuoteDate.Text = "";
            txtQuoteTime.Text = "";
            txtQuoteValue.Text = "";
            txtQuoteCOG.Text = "";
            txtQuoteMargin.Text = "";
            //txtMainProductLine.Text = "";
            //txtQuoteType.Text = "";
            txtStatus.Text = "";
            txtOrderDate.Text = "";
            txtOrderValue.Text = "";
            txtOrderMargin.Text = "";
            //txtContactType.Text = "";
            txtNoOfQuotes.Text = "";
            txtQuoteConverted.Text = "";
            txtNoOfQuotes0.Text = "";
            txtQuoteConverted0.Text = "";
            txtPercentConversion.Text = "";
            txtPercentConversion0.Text = "";
            lblName.Text = "";
            txtQuote.Text = "";
            gridQuotes.DataSource = null;
            gridQuotes.DataBind();
        }

        public DataTable GetDatafromXML()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = "1=1 ";

            if (File.Exists(Pathname))
            {

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
                        if (Request.Cookies["CNo"].Expires == DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                            Query = Query + " and buyerct_SVR = " + Request.Cookies["CNo"].Value;
                        }
                    }
                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

            }
            else
                dtTemp = null;


            return dtTemp;
        }

        //GridView
        public DataTable GetDatafromXML2()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes2" + ".xml";
            DataTable dtTemp = new DataTable();

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds.Tables[0];
        }

        [System.Web.Services.WebMethod]
        public static DataTable GetData(string docNo, string QuoteLine)
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = "1=1 ";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (ds.Tables.Count > 0)
            {
                dtTemp = ds.Tables[0].Clone();

                if (docNo != "")
                    Query = Query + " and Quote_Doc_No ='" + docNo + "'";

                if (QuoteLine != "")
                    Query = Query + " and Quote_Line ='" + QuoteLine + "'";

                results = ds.Tables[0].Select(Query);

                foreach (DataRow dr in results)
                    dtTemp.ImportRow(dr);
            }
            else
                dtTemp = null;

            return dtTemp;
        }

        protected void gridQuotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NewMasterPage MasterPage = Master as NewMasterPage;
            bool varColumnExist = false;
            int columnIndex;
            string[] list = { "QUOTE DATE",
                              "QUOTE DISCOUNT",
                              "QUOTE NET SALES",
                              "QUOTE MAT ENTRD DESC",
                              "QUOTE QTY",
                              "QUOTE UNIT PRICE",
                              "QUOTE COST"};

            //Use to Check what Currency Code going to provide.
            if (MasterPage != null)
            {
                ddlCampaignQ = MasterPage.CampaignMaster;
                ddlCampaignCurrencyQ = MasterPage.CampaignCurrencyMaster;
            }


            if (!string.IsNullOrEmpty(ddlCampaignQ.SelectedValue))
            {
                if (!string.IsNullOrEmpty(ddlCampaignCurrencyQ.SelectedValue))
                {
                    ddlCampaignCurrencyQ.ClearSelection();
                }
                else
                {
                    ddlCampaignCurrencyQ.Items.Clear();

                    ddlCampaignCurrencyQ.Items.AddRange(ddlCampaignQ.Items.OfType<ListItem>().ToArray());
                }


                if (ddlCampaignCurrencyQ.Items.FindByValue(ddlCampaignQ.SelectedValue) != null)
                {
                    ddlCampaignCurrencyQ.Items.FindByValue(ddlCampaignQ.SelectedValue).Selected = true;
                }

            }


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
                        {
                            if (list[i] == "QUOTE DATE")
                            {
                                DateTime temp;
                                if (DateTime.TryParse(e.Row.Cells[columnIndex].Text, out temp) == true)
                                    e.Row.Cells[columnIndex].Text = Convert.ToDateTime(e.Row.Cells[columnIndex].Text).ToString("MM/dd/yyyy");
                            }
                            else if (list[i] == "QUOTE QTY")
                            {
                                e.Row.Cells[columnIndex].Text = e.Row.Cells[columnIndex].Text.ToString().Replace("0", "").Replace(".", "");
                            }
                            else if (list[i] == "QUOTE DISCOUNT" ||
                                     list[i] == "QUOTE NET SALES" ||
                                    list[i] == "QUOTE UNIT PRICE" ||
                                    list[i] == "QUOTE COST")
                            {
                                e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;
                                decimal temp;
                                if (decimal.TryParse(e.Row.Cells[columnIndex].Text, out temp) == true)
                                {
                                    e.Row.Cells[columnIndex].Text = FormatCurrency(decimal.Parse(e.Row.Cells[columnIndex].Text), ddlCampaignCurrencyQ.SelectedItem.Text);
                                }

                            }
                            else if (list[i] == "QUOTE MAT ENTRD DESC")
                            {
                                if (ddlCampaignQ.SelectedValue == "DE")
                                {
                                    byte[] data = Encoding.Default.GetBytes(((DataRowView)e.Row.DataItem)["QUOTE MAT ENTRD DESC"].ToString());
                                    string output = Encoding.UTF8.GetString(data);

                                    e.Row.Cells[columnIndex].Text = output;
                                }
                            }

                        }
                    }
                }
            }
        }

        protected void BindListBox()
        {
            string[] ColumnName = { "Quote_Doc_No",
                                      "Quote_Doc_createdon" };
            string[] TempAccount = new string[6];
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes" + ".xml";
            DataSet ds = new DataSet();
            DataTable dtExisting, dtDistinctValues;
            DataView view;
            string controlID = string.Empty;
            string status = string.Empty;
            NewMasterPage MasterPage = Master as NewMasterPage;
            try
            {

                ClearControls();

                if (SessionFacade.LastAccount[4] == AccountQ &&
                    SessionFacade.CampaignValue == ddlCampaignQ.SelectedValue)
                {
                    if (SessionFacade.Update_Bool == true)
                    {
                        cQuotes objQuotes = new cQuotes();

                        //Account
                        objQuotes.SearchQuoteByAccount = AccountQ;

                        //Campaign
                        objQuotes.SearchQuoteByCampaign = ddlCampaignQ.SelectedValue.ToString().Trim();

                        dtExisting = objQuotes.GetListQuotes();

                        ds.Tables.Add(dtExisting);

                        //Writing XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                    }

                    dtExisting = GetDatafromXML();

                    //Copy to view

                    if (dtExisting != null)
                    {

                        view = new DataView(dtExisting);

                        //Distinct Quote Number
                        dtDistinctValues = view.ToTable(true, ColumnName);

                        gridQuoteNumber.DataSource = dtDistinctValues;
                        gridQuoteNumber.DataBind();
                    }
                }
                else
                {
                    cQuotes objQuotes = new cQuotes();

                    //Account
                    objQuotes.SearchQuoteByAccount = AccountQ;

                    //Campaign
                    objQuotes.SearchQuoteByCampaign = ddlCampaignQ.SelectedValue.ToString().Trim();

                    dtExisting = objQuotes.GetListQuotes();

                    TempAccount = SessionFacade.LastAccount;

                    TempAccount[4] = AccountQ;

                    SessionFacade.LastAccount = TempAccount;

                    if (dtExisting.Rows.Count > 0 || dtExisting != null)
                    {
                        if (dtExisting.Columns.Contains("Quote_Doc_No") == true)
                        {
                            ds.Tables.Add(dtExisting);
                            //Writing XML
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            ////Copy to view
                            view = new DataView(GetDatafromXML());

                            // Distinct Quote Number
                            dtDistinctValues = view.ToTable(true, ColumnName);
                            tblQuoteList.Visible = true;

                            gridQuoteNumber.DataSource = dtDistinctValues;
                            gridQuoteNumber.DataBind();

                            //Select First Row by Default
                            SessionFacade.Update_Bool = true;
                        }
                        else
                        {
                            gridQuoteNumber.DataSource = null;
                            gridQuoteNumber.DataBind();
                        }
                    }
                    else
                    {
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();
                        //gridQuoteNumber.Items.Clear();
                        gridQuoteNumber.DataSource = null;
                        gridQuoteNumber.DataBind();
                        tblQuoteList.Visible = false;
                    }
                }

                if (gridQuoteNumber.Rows.Count > 0 && gridQuoteNumber.SelectedIndex != 0)
                {
                    if (SessionFacade.Update_Bool == true)
                    {
                        gridQuoteNumber.SelectedIndex = 0;
                        DisplayQuote();
                        SessionFacade.Update_Bool = false;
                    }
                    else
                    {
                        gridQuoteNumber.SelectedIndex = 0;
                        DisplayQuote();
                    }
                }
                else if (SessionFacade.Update_Bool == true && gridQuoteNumber.Rows.Count > 0)
                {
                    DisplayQuote();
                    SessionFacade.Update_Bool = false;
                }

            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Quote Number - BindingData", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void CheckNumber(TextBox control, string value1)
        {
            if (value1 == "")
                control.Text = "0.00 %";
            else
                control.Text = decimal.Parse(value1).ToString("P");
        }

        public string FormatCurrency(decimal amount, string currencyCode)
        {
            var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                           let r = new RegionInfo(c.LCID)
                           where r != null
                           && r.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
                           select c).FirstOrDefault();

            if (culture == null)
                return amount.ToString("0.00");

            return string.Format(culture, "{0:C}", amount);
        }

        public string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        #region Paging
        protected void PageChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes2" + ".xml";

            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            gridQuotes.DataSource = ds;
            gridQuotes.PageIndex = e.NewPageIndex;
            gridQuotes.DataBind();
        }

        protected void gridQuoteNumber_PageChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            DataView view = new DataView();
            DataTable dtDistinctValues = new DataTable();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes" + ".xml";
            string[] ColumnName = { "Quote_Doc_No",
                                      "Quote_Doc_createdon" };


            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            view = new DataView(ds.Tables[0]);

            //Distinct Quote Number
            dtDistinctValues = view.ToTable(true, ColumnName);

            gridQuoteNumber.DataSource = dtDistinctValues;
            gridQuoteNumber.PageIndex = e.NewPageIndex;
            gridQuoteNumber.DataBind();

            DisplayQuote();
        }
        #endregion

        #region Sorting
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
        protected void gridQuotes_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridQuotes(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridQuotes(sortExpression, "ASC");
            }
        }
        private void SortGridQuotes(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes2" + ".xml";
                    if (File.Exists(Pathname))
                    {

                        DataTable dt = GetDatafromXML2();
                        DataView dv = new DataView(dt);
                        DataSet ds = new DataSet();

                        dv.Sort = sortExpression.ToLower().Replace(' ', '_') + " " + direction;

                        ds.Tables.Add(dv.ToTable());

                        gridQuotes.DataSource = UpperCase(ds);
                        gridQuotes.DataBind();

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected SortDirection GridViewSortDirectionQuoteList
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }
        protected void gridQuoteNumber_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirectionQuoteList == SortDirection.Ascending)
            {
                GridViewSortDirectionQuoteList = SortDirection.Descending;
                SortGridQuotesList(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirectionQuoteList = SortDirection.Ascending;
                SortGridQuotesList(sortExpression, "ASC");
            }
        }
        private void SortGridQuotesList(string sortExpression, string direction)
        {
            string[] ColumnName = { "Quote_Doc_No",
                                      "Quote_Doc_createdon" };
            DataView view;
            DataTable dtDistinctValues;
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes" + ".xml";
                    if (File.Exists(Pathname))
                    {

                        DataTable dt = GetDatafromXML();
                        DataView dv = new DataView(dt);
                        DataSet ds = new DataSet();

                        dv.Sort = sortExpression + " " + direction;

                        //Copy Sorting Row to XML
                        dt = dv.ToTable();
                        ds.Tables.Add(dt);

                        ////Copy to view
                        view = new DataView(ds.Tables[0]);

                        // Distinct Quote Number
                        dtDistinctValues = view.ToTable(true, ColumnName);

                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridQuoteNumber.DataSource = dtDistinctValues;
                        gridQuoteNumber.DataBind();
                        if (gridQuoteNumber.Rows.Count > 0 || gridQuoteNumber.SelectedIndex != 0)
                        {
                            gridQuoteNumber.SelectedIndex = 0;
                            DisplayQuote();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        protected void gridQuoteNumber_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gridQuoteNumber, "SELECT$" + e.Row.RowIndex.ToString());
            }
        }

        protected void gridQuoteNumber_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRow[] results;

            string status = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtExisting = GetDatafromXML();
                results = dtExisting.Select("[Quote_Doc_No] ='" + e.Row.Cells[1].Text + "'");
                foreach (DataRow dr in results)
                {
                    status = dr["Status"].ToString();
                }
                if (status.ToUpper() == "NOT CONVERTED")
                {


                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        protected void DisplayQuote()
        {
            DataSet ds = new DataSet();
            string HPC = string.Empty,
                QuoteBucket = string.Empty,
                QuoteBucketTemp = string.Empty,
                numericvalue = string.Empty,
                FirstSecondValue = string.Empty;
            DataTable dt = GetDatafromXML();
            DataRow[] results;
            DataTable above24 = new DataTable();
            int index = 0;


            if (dt != null)
                above24 = dt.Clone();
            DataRow row;
            DataTable dtTemp = new DataTable();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" +
                SessionFacade.LoggedInUserName + "-Quotes2" + ".xml";
            string AveQuoteDiscount = "", AveOrderDiscount = "";

            dtTemp = dt;

            DataTable CloneDummyDT = new DataTable();
            DataTable DummyDT = new DataTable();

            NewMasterPage MasterPage = Master as NewMasterPage;

            //Use to Check what Currency Code going to provide.
            if (MasterPage != null)
            {
                ddlCampaignQ = MasterPage.CampaignMaster;
                ddlCampaignCurrencyQ = MasterPage.CampaignCurrencyMaster;
            }


            if (!string.IsNullOrEmpty(ddlCampaignQ.SelectedValue))
            {
                if (!string.IsNullOrEmpty(ddlCampaignCurrencyQ.SelectedValue))
                {
                    ddlCampaignCurrencyQ.ClearSelection();
                }
                else
                {
                    ddlCampaignCurrencyQ.Items.Clear();

                    ddlCampaignCurrencyQ.Items.AddRange(ddlCampaignQ.Items.OfType<ListItem>().ToArray());
                }


                if (ddlCampaignCurrencyQ.Items.FindByValue(ddlCampaignQ.SelectedValue) != null)
                {
                    ddlCampaignCurrencyQ.Items.FindByValue(ddlCampaignQ.SelectedValue).Selected = true;
                }

            }


            if (dtTemp != null)
            {
                DummyDT = dtTemp;
                CloneDummyDT = DummyDT.Clone();
            }
            GridViewRow rows = null;
            if (gridQuoteNumber.Rows.Count > 0)
                rows = gridQuoteNumber.SelectedRow;

            //Clear Selected Quote Number
            foreach (GridViewRow rowsd in gridQuoteNumber.Rows)
                rowsd.Cells[0].Text = "";

            if (dt != null)
            {
                if (rows != null)
                {

                    Image img1 = (Image)rows.FindControl("ImageArrow");

                    rows.Cells[0].Text = "<img src='../../../App_Themes/Images/New Design/RightSelectedArrow.png' style='border-width:0px'/>";
                    results = dt.Select("[Quote_Doc_No] ='" + rows.Cells[1].Text + "'");
                    foreach (DataRow dr in results)
                        above24.ImportRow(dr);
                    foreach (DataRow dr2 in results)
                        CloneDummyDT.ImportRow(dr2);

                    row = above24.Rows[0];

                    if (ddlCampaignCurrencyQ.SelectedItem.Text == "GBP")
                        QuoteBucket = row["Quote_Bucket"].ToString().ToUpper().Replace("$", "£");
                    else if (ddlCampaignCurrencyQ.SelectedItem.Text == "EUR")
                    {
                        QuoteBucketTemp = row["Quote_Bucket"].ToString().ToUpper().Replace("$", "€");

                        index = QuoteBucketTemp.Length - 1;

                        //Get Numeric value
                        while (QuoteBucketTemp[index].ToString() != "€")
                        {
                            numericvalue = numericvalue + QuoteBucketTemp[index].ToString();
                            index -= 1;
                        }

                        //Reverse String
                        numericvalue = Reverse(numericvalue.ToString());

                        //Get First and Second Value
                        FirstSecondValue = QuoteBucketTemp[0].ToString() + QuoteBucketTemp[1].ToString();

                        //Combining all values
                        QuoteBucket = FirstSecondValue + numericvalue + " €";
                    }
                    else if (ddlCampaignCurrencyQ.SelectedItem.Text == "CHF")
                    {
                        QuoteBucket = row["Quote_Bucket"].ToString().ToUpper().Replace("$", "fr.");
                    }
                    else
                        QuoteBucket = row["Quote_Bucket"].ToString().ToUpper();

                    txtSalesRep.Text = row["Sales_Team_Name"].ToString();
                    txtQuoteDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(row["Quote_Doc_createdon"].ToString().ToUpper()));
                    //txtQuoteTime.Text = row["Quote_Date"].ToString().ToUpper();
                    txtQuoteTime.Text = row["Quote_Create_Time"] == null ? "" : String.Format("{0:T}", DateTime.Parse(row["Quote_Create_Time"].ToString()));
                    //txtQuoteTime.Text = row["Quote_Doc_Create_Time"] == null ? "" : String.Format("{0:T}", DateTime.Parse(row["Quote_Doc_Create_Time"].ToString()));
                    decimal tempDec;
                    if (decimal.TryParse(row["quote_sales_doc"].ToString().ToUpper(), out tempDec) == true)
                        txtQuoteValue.Text = FormatCurrency(decimal.Parse(row["quote_sales_doc"].ToString().ToUpper()), ddlCampaignCurrencyQ.SelectedItem.Text);
                    else
                        txtQuoteValue.Text = FormatCurrency(decimal.Parse("0"), ddlCampaignCurrencyQ.SelectedItem.Text);
                    //txtQuoteCOG.Text = row["quote_cost_doc"].ToString().ToUpper();

                    if (decimal.TryParse(row["quote_cost_doc"].ToString().ToUpper(), out tempDec) == true)
                        txtQuoteCOG.Text = FormatCurrency(decimal.Parse(row["quote_cost_doc"].ToString().ToUpper()), ddlCampaignCurrencyQ.SelectedItem.Text);
                    else
                        txtQuoteCOG.Text = FormatCurrency(decimal.Parse("0"), ddlCampaignCurrencyQ.SelectedItem.Text);

                    txtQuoteMargin.Text = decimal.Parse(row["Quote_GM_percent"].ToString()).ToString("P").ToUpper();
                    txtStatus.Text = row["Status"].ToString().ToUpper();
                    if (row["order_date_converted"].ToString().ToUpper() != "")
                        txtOrderDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(row["order_date_converted"].ToString().ToUpper()));
                    else
                        txtOrderDate.Text = "";
                    txtOrderValue.Text = row["order_sales_doc"].ToString().ToUpper();

                    CheckNumber(txtOrderMargin, row["Order_GM_percent"].ToString().ToUpper());

                    //txtContactType.Text = row["tag"].ToString().ToUpper();
                    txtNoOfQuotes.Text = row["num_quotes_contact"].ToString().ToUpper();
                    txtQuoteConverted.Text = row["num_quotes_conv_contact"].ToString().ToUpper();
                    //txtgrossmargin.Text = ((decimal.Parse(row["quote_sales_doc"].ToString()) - decimal.Parse(row["quote_cost_doc"].ToString())) / decimal.Parse(row["quote_sales_doc"].ToString())).ToString("P").ToUpper();
                    CheckNumber(txtPercentConversion, row["percent_conversion_cont"].ToString().ToUpper());

                    txtNoOfQuotes0.Text = row["num_quotes_site"].ToString().ToUpper();
                    txtQuoteConverted0.Text = row["num_quotes_conv_site"].ToString().ToUpper();

                    CheckNumber(txtPercentConversion0, row["Percent_Conversion_Site"].ToString().ToUpper());

                    lblName.Text = row["surname"].ToString().ToUpper() + ", " + row["firstname"].ToString().ToUpper();

                    float temp;

                    if (float.TryParse(row["ave_quote_disc"].ToString(), out temp))
                        AveQuoteDiscount = (float.Parse(row["ave_quote_disc"].ToString()) * 100).ToString("0.00") + " %";

                    if (float.TryParse(row["ave_order_disc"].ToString(), out temp))
                        AveOrderDiscount = (float.Parse(row["ave_order_disc"].ToString()) * 100).ToString("0.00") + " %";

                    HPC = row["Hist_Num_Quotes_Conv"].ToString().ToUpper() + " out of " + row["Hist_Num_Quotes"].ToString().ToUpper() +
                        " or " + decimal.Parse(row["Hist_Perc_Conv"].ToString()).ToString("P").ToUpper() +
                        " quotes of the same type have been converted based on these categories:\r\n" +
                        " - Customer Type: " + row["tag"].ToString().ToUpper() + "\r\n" + " - Product Line: " +
                        row["DM_Product_Line_Desc"].ToString().ToUpper() + "\r\n" + " - Quote Type: " + row["Quote_Doc_Reason_Code_Desc"].ToString().Trim().ToUpper() + "\r\n" +
                        " - Quote Bucket: " + QuoteBucket + "\r\n - Average Quote Discount: " + AveQuoteDiscount + "\r\n - Average Order Discount: " +
                        AveOrderDiscount;

                    txtQuote.Text = HPC;

                    ds.Tables.Add(above24);

                    DataSet ReArrangeDs = new DataSet();
                    ReArrangeDs = ds;

                    if (ReArrangeDs != null)
                    {
                        cArrangeDataSet ADS = new cArrangeDataSet();
                        ADS.CampaignName = SessionFacade.CampaignValue;
                        ADS.UserName = SessionFacade.LoggedInUserName;
                        ADS.Listview = "lvwQuoteitem";

                        int IsReorder = ADS.ColumnReorderCount();
                        if (IsReorder > 0)
                        {
                            ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                        }
                        else
                        {
                            DataSet dsTemp = new DataSet();
                            dsTemp = ReArrangeDs.Clone();
                            bool retain;
                            string RemoveColumn = string.Empty;
                            //string AllDefaultQuotesColumns = "Quote_Line|Quote_Doc_No|Quote_Date|Quote_Created_By|Quote_Create_Time|Quote_PO_Type_Desc|Quote_Item_Categ_Desc|Quote_Reason_Code_Desc|Quote_Reason_Rej_Desc|Quote_SlsTeamIN|Quote_Material_Desc|Quote_Mat_Entrd|Product_Family_Desc|Quote_Coupon_CODE|Quote_Discount|Quote_Net_Sales|Quote_Freight|Quote_Cost|Order_Doc_No|Order_Line|Order_Date|Order_Createdby|Order_PO_Type_Desc|Order_Item_Categ_Desc|Order_Material_Desc|Order_Mat_Entrd_Desc|Order_Discount|Order_Net_Sales|Order_Freight|Order_Cost|Order_Refer_Doc|Order_Refer_Itm|Order_Coupon_CODE|Surname|Firstname";
                            //string[] SplitAllDefaultQuotesColumns = AllDefaultQuotesColumns.Split('|');
                            string[] AllColumnName = DefaultQuotesColumns.Split('|');
                            for (int i = 0; i < dsTemp.Tables[0].Columns.Count; i++)
                            {
                                retain = false;
                                for (int y = 0; y < AllColumnName.Length; y++)
                                {
                                    if (dsTemp.Tables[0].Columns[i].ColumnName == AllColumnName[y])
                                    {
                                        retain = true;
                                        break;
                                    }
                                    else
                                        retain = false;
                                }
                                if (retain == false)
                                {
                                    if (ReArrangeDs.Tables[0].Columns.Contains(dsTemp.Tables[0].Columns[i].ColumnName) == true)
                                        ReArrangeDs.Tables[0].Columns.Remove(dsTemp.Tables[0].Columns[i].ColumnName);
                                }
                            }

                        }
                    }

                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ReArrangeDs.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    //Bind Gridview
                    gridQuotes.DataSource = UpperCase(ReArrangeDs);//ReArrangeDs;
                    gridQuotes.DataBind();

                }
                else
                {
                    gridQuotes.DataSource = null;
                    gridQuotes.DataBind();
                }
            }
            else
            {
                gridQuotes.DataSource = null;
                gridQuotes.DataBind();
            }



        }

        #region Export to Excel Function
        protected void btn_Export2ExcelClick(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        //public void ExportExcelFunction()
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        string UserFileName = SessionFacade.LoggedInUserName + "Quotes.xls";
        //        string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes2" + ".xml";
        //        ds.ReadXml(Pathname);
        //        if (ds != null)
        //        {


        //            if (ds.Tables[0].Rows.Count > 0)
        //            {

        //                // ds.Tables[0].Columns.Remove("Quote_Doc_createdon");
        //                //ds.Tables[0].Columns.Remove("Quote_Doc_Create_Time");
        //                // ds.Tables[0].Columns.Remove("quote_sales_doc");
        //                //ds.Tables[0].Columns.Remove("quote_cost_doc");
        //                //ds.Tables[0].Columns.Remove("Quote_PO_Type");
        //                //ds.Tables[0].Columns.Remove("Quote_Reason_Code");
        //                //ds.Tables[0].Columns.Remove("Quote_Material");
        //                //ds.Tables[0].Columns.Remove("Quote_Mat_Entrd");
        //                //ds.Tables[0].Columns.Remove("Product_Line_Desc");

        //                //ds.Tables[0].Columns.Remove("Quote_GM_percent");
        //                //ds.Tables[0].Columns.Remove("DM_Product_Line_Desc");
        //                //ds.Tables[0].Columns.Remove("Quote_Doc_Reason_Code_Desc");
        //                //ds.Tables[0].Columns.Remove("Status");
        //                //ds.Tables[0].Columns.Remove("order_date_converted");
        //                //ds.Tables[0].Columns.Remove("order_sales_doc");

        //                //ds.Tables[0].Columns.Remove("Order_GM_percent");
        //                //ds.Tables[0].Columns.Remove("tag");
        //                //ds.Tables[0].Columns.Remove("num_quotes_contact");
        //                //ds.Tables[0].Columns.Remove("num_quotes_conv_contact");
        //                //ds.Tables[0].Columns.Remove("percent_conversion_cont");
        //                //ds.Tables[0].Columns.Remove("num_quotes_site");

        //                //ds.Tables[0].Columns.Remove("num_quotes_conv_site");
        //                //ds.Tables[0].Columns.Remove("percent_conversion_site");
        //                //ds.Tables[0].Columns.Remove("Quote_Bucket");
        //                //ds.Tables[0].Columns.Remove("Hist_Perc_Conv");
        //                //ds.Tables[0].Columns.Remove("Hist_Num_Quotes");
        //                //ds.Tables[0].Columns.Remove("Hist_Num_Quotes_Conv");
        //                //ds.Tables[0].Columns.Remove("buyerct_SVR");
        //                //ds.Tables[0].Columns.Remove("sales_team_name");


        //            }
        //            ds.AcceptChanges();

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {

        //                if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName)))
        //                    File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

        //                File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/Quotestemp.xls"), Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

        //                string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName);
        //                bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

        //                //true means Excel File has been written
        //                if (exportToEx == true)
        //                {
        //                    if (Request.Browser.Type == "Desktop") //For chrome
        //                    {
        //                        ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

        //                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=Quotes&FilePath=" + UserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=Quotes&FilePath=" + UserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
        //                    }
        //                }
        //                else
        //                {
        //                    // Response.Write("Data not Exported to Excel File");
        //                }
        //            }

        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogMessage("Order History - Error in Export to Excel" + err.ToString());
        //    }
        //}

        public void ExportToExcel()
        {
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Quotes2" + ".xml";
                DataSet ds = new DataSet();
                string attachment = "attachment; filename=" + SessionFacade.LoggedInUserName + "Quotes.xls";
                Response.Clear();
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                ds.ReadXml(Pathname);
                if (ds != null && ds.Tables.Count > 0)
                {
                    //Remove Row Columm
                    if (ds.Tables[0].Columns.Contains("Row"))
                    {
                        ds.Tables[0].Columns.Remove("Row");
                    }
                    gridQuotes.DataSource = UpperCase(ds);
                    gridQuotes.AllowPaging = false;
                    gridQuotes.AllowSorting = false;
                    gridQuotes.DataBind();

                    //Get the HTML for the control.
                    gridQuotes.RenderControl(hw);
                    //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                    Response.Output.Write(tw.ToString());
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Close();

                    gridQuotes.AllowSorting = true;
                    gridQuotes.DataBind();
                }
            }
            catch (System.Threading.ThreadAbortException lException)
            {

                // do nothing

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Viewing Quote Pipeline Won or Loss", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        #endregion

        protected void gridQuoteNumber_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DisplayQuote();
        }

        protected void ArrangeColumn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                  "call me", "window.open('../Home/ArrangeColumns.aspx?Data=lvwQuoteitem','mywindow','width=700,height=400,scrollbars=yes')", true);
        }

        protected DataSet UpperCase(DataSet ds)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                ds.Tables[0].Columns[i].ColumnName =
                   ds.Tables[0].Columns[i].ColumnName.ToString().ToUpper().Replace('_', ' ');
            }
            return ds;
        }
    }
}