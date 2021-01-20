using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data; // Name space for Dataset, Dataadapter etc
using System.Configuration; //name space needed to access the Web Config file
using System.IO; //This namespace has to be used if you are doing any file operation ( For example  read or write text file,excel,)
using System.Text;
using System.Collections; //This namespace has to be used while using arrays etc
using System.Xml; //This will be used if you want to read Or use xml files
using System.Data.SqlClient; ////This namespace has to be used while accessing  SQL Database
using AppLogic;
using System.Globalization;

namespace WebSalesMine.WebPages.CustomerLookUp
{
    public partial class CustomerLookUp : System.Web.UI.Page
    {
        public int DateOrdinal = 1000;
        public int myTest = 1000;
        public string varFilterBy = "";
        public DataSet dsCustomerLookup;
        public string varkamid = "";
        public string userRule = "";

        protected System.Data.DataTable DtCustomerLookUp
        {
            get // a DataTable filled in any way, for example:
            {

                CustomerLookup cCustLookup = new CustomerLookup();
                DataSet ds;
                DataTable dtTemp = new DataTable();

                var datable = new System.Data.DataTable();

                varFilterBy = RadioButtonList1.SelectedValue.ToString().Trim();
                cCustLookup.CampaignName = SessionFacade.CampaignName.ToString().Trim();
                cCustLookup.SearchType = varFilterBy;
                cCustLookup.TextSearch = txtSearch2.Text;

                dsCustomerLookup = cCustLookup.GetCustomerLookUp();
                try
                {
                    datable = cCustLookup.GetCustomerLookUp().Tables[0];

                }
                catch
                {

                }


                var dt = new System.Data.DataTable();

                foreach (DataColumn dc in datable.Columns)
                {
                    dt.Columns.Add(dc.ColumnName.ToString(), dc.DataType).Caption = dc.ColumnName.ToString();

                }

                DataRow[] results;

                string Query;

                Query = " 1=1";
                results = datable.Select(Query);

                DateTime temp;
                foreach (DataRow dr in datable.Rows)
                {
                    object[] array = new object[datable.Columns.Count];
                    for (int i = 0; i < datable.Columns.Count; i++)
                    {

                        array[i] = dr[i];
                    }
                    dt.Rows.Add(array);
                }
                return dt;
            }


        }

        protected void LoadData()
        {
            var googleDataTable = new Bortosky.Google.Visualization.GoogleDataTable(this.DtCustomerLookUp);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "fundata", string.Format("var fundata = {0};", googleDataTable.GetJson()), true);
        }

        protected void loadsearchbutton()
        {
            var googleDataTable = new Bortosky.Google.Visualization.GoogleDataTable(this.DtCustomerLookUp);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "fundata", string.Format("var fundata = {0};", googleDataTable.GetJson()), true);
            ClientScript.RegisterStartupScript(this.GetType(), "fundata", string.Format("var fundata = {0};", googleDataTable.GetJson()), true);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Label lblTemp = Master.FindControl("lblAccountName") as Label;
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            userRule = SessionFacade.UserRole.Trim();

            //Hide Account Name
            lblTemp.Visible = false;

            //Hide Export To Excel
            //btnExportToExcel2.Visible = false;
            //View Export Excel
            if (userRule == "PC-ONT" ||
                userRule == "ADMIN" ||
                userRule == "CUS")
            {
                if (ddlTemp.SelectedValue.ToString() == "PC")
                {
                    btnExportToExcel2.Visible = true;
                }
                else
                    btnExportToExcel2.Visible = false;
            }
            else
                btnExportToExcel2.Visible = false;

            varkamid = "";
            txtSearch2.Attributes["onmouseover"] = "javascript:this.focus();";
            txtSearch2.Attributes["onfocus"] = "javascript:this.select();";
            txtSearch2.Attributes.Add("onfocus()", "FocusValueAccount(this);");
            txtStartDate.Attributes.Add("onfocus()", "FocusValueAccount(this);");
            txtSearch2.Focus();
            varkamid = SessionFacade.KamId;

            if (IsPostBack)
            {
                string ControlId = string.Empty;
                ControlId = getPostBackControlID();
                if (ControlId == "ddlCampaign" || (ControlId == "lnkCustomerLookup"))
                {
                    if (txtSearch2.Text.Replace(" ", "") != "")
                    {
                        ShowCustomerLookUp();
                    }
                    else if (txtStartDate.Text.Replace(" ", "") != "")
                    {
                        NewBuyerQuery();
                    }
                    else
                    {
                        gridCustomerLookup.DataSource = null;
                        gridCustomerLookup.DataBind();

                    }

                }
                   
                        
            }
            

            if (Request.Cookies["CNo"] != null)
            {
                HttpCookie myCookie = new HttpCookie("CNo");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            //if (gridCustomerLookup.Rows.Count > 0)
            //{

            //    gridCustomerLookup.UseAccessibleHeader = true;
            //    gridCustomerLookup.HeaderRow.TableSection = TableRowSection.TableHeader;

            //}
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
            return control.ID;
        }

        [System.Web.Services.WebMethod]
        public static void PassVal(string account, string contact, string ContName)
        {
            CustomerLookup strContactLevel = new CustomerLookup();
            if (account.Length > 10)
            {
                SessionFacade.AccountNo = string.Empty;
                SessionFacade.ContactlevelVal = string.Empty;
                SessionFacade.BuyerCt = string.Empty;
                SessionFacade.ContactName = string.Empty;
            }
            else
            {
                SessionFacade.AccountNo = account;
                SessionFacade.ContactlevelVal = contact;

                SessionFacade.BuyerCt = contact;
                SessionFacade.ContactName = ContName;
            }            


        }





        public void ShowCustomerLookUp()
        {
            if (txtSearch2.Text.Replace(" ", "") != "")
            {
                CustomerLookup cCustLookup = new CustomerLookup();
                DataSet ds;
                DataTable dtTemp = new DataTable();
                // String xmlName = "-CustomerLookUp";
                DropDownList ddlCampaign = Master.FindControl("ddlCampaign") as DropDownList;
                varFilterBy = RadioButtonList1.SelectedValue.ToString().Trim();
                txtStartDate.Text = string.Empty;
                 string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-CustomerLookUp" + ".xml";

                if (Request.Cookies["CNo"] != null || Request.Cookies["CName"] != null || SessionFacade.SKUCategory != null)
                {
                    HttpCookie myCookie = new HttpCookie("CNo");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    HttpCookie myCookieName = new HttpCookie("CName");
                    myCookieName.Expires = DateTime.Now.AddDays(-1d);
                    SessionFacade.SKUCategory = null;
                    Response.Cookies.Add(myCookie);
                    Response.Cookies.Add(myCookieName);
                }
                DateTime d;

                if (DateTime.TryParse(txtStartDate.Text, out d) == false)
                {
                    if (!string.IsNullOrEmpty(ddlCampaign.SelectedItem.Value))
                    {

                        try
                        {
                            cCustLookup.CampaignName = ddlCampaign.SelectedItem.Value.Trim();//SessionFacade.CampaignName.ToString().Trim();
                            cCustLookup.SearchType = varFilterBy;
                            cCustLookup.TextSearch = txtSearch2.Text.Replace("%","");

                            dsCustomerLookup = cCustLookup.GetCustomerLookUp();
                            //DataTable dt = dsCustomerLookup.Tables[0];
                            //dtTemp = dt;

                             System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                             dsCustomerLookup.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                             xmlSW.Close();

                            //DataSet ReArrangeDs = new DataSet();
                            //ReArrangeDs.Tables.Add(dtTemp);
                            //ReArrangeDs = dsCustomerLookup;
                            //ReArrangeDs.Tables.Add(GetDatafromXML(xmlName));


                            if (SessionFacade.UserRole == "ADMIN" || SessionFacade.CampaignName == "ADMIN")
                            {
                                //if (ReArrangeDs != null)
                                //{
                                //    cArrangeDataSet ADS = new cArrangeDataSet();
                                //    ADS.CampaignName = SessionFacade.CampaignValue;
                                //    ADS.UserName = SessionFacade.LoggedInUserName;
                                //    ADS.Listview = "lvwLookupData";

                                //    int IsReorder = ADS.ColumnReorderCount();
                                //    if (IsReorder > 0)
                                //    {
                                //        ReArrangeDs = ADS.RearangeDS(ReArrangeDs);

                                //    }
                                //}

                                //System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname);
                                //ReArrangeDs.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
                                //xmlSW2.Close();

                                //if (ReArrangeDs.Tables[0].Columns.Contains("Order Date"))
                                //{
                                //    DateOrdinal = ReArrangeDs.Tables[0].Columns["Order Date"].Ordinal;
                                //}
                                //else
                                //{
                                //    DateOrdinal = 1000;
                                //}
                            }

                            if (dsCustomerLookup.Tables.Count > 0 && dsCustomerLookup.Tables[0].Rows.Count > 0)
                            {

                                gridCustomerLookup.DataSource = dsCustomerLookup.Tables[0];
                                gridCustomerLookup.DataBind();
                                
                                // ds = ReArrangeDs;
                                //System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                                //ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                                //xmlSW.Close();

                                //if (ds.Tables[0].Rows.Count == 0)
                                //{
                                //    //lookuptable.Visible = false;
                                //    // ckContactLevel.Visible = false;
                                //}
                                //else
                                //{

                                //    gridCustomerLookup.Visible = true;
                                //    //ckContactLevel.Visible = true;
                                //    //lookuptable.Visible = true;
                                //}
                            }
                            //else if (dsCustomerLookup.Tables[0].Rows.Count > 0)
                            //{

                            //    //lookuptable.Visible = true;
                            //    gridCustomerLookup.Visible = true;
                            //    //ckContactLevel.Visible = true;
                            //    gridCustomerLookup.DataSource = dsCustomerLookup;
                            //    gridCustomerLookup.DataBind();

                            //    ds = dsCustomerLookup;

                            //    System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname);
                            //    ds.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
                            //    xmlSW2.Close();
                            //}

                            else
                            {
                                //grdCustLookup.Visible = false;
                                //pnlGridIndex.Visible = false;
                                gridCustomerLookup.DataSource = null;
                                gridCustomerLookup.DataBind();

                                
                                //ckContactLevel.Visible = false;
                                //lookuptable.Visible = false;
                            }




                        }

                        catch (Exception ex)
                        {
                            gridCustomerLookup.DataSource = null;
                            gridCustomerLookup.DataBind();
                           
                            new ArgumentNullException();
                            //throw ex;
                        }

                    }

                }
                //else
                //{
                //    NewBuyerQuery();
                //}
            }
            else
            {
                gridCustomerLookup.DataSource = null;
                gridCustomerLookup.DataBind();
              
            }
          
        }


        public void NewBuyerQuery()
        {
           
            NewBuyerSince cNewBuyerSince = new NewBuyerSince();
            DataSet ReArrangeDs = new DataSet();
            DataSet ds;
            DataTable dtTemp = new DataTable();
            DropDownList ddlCampaign = Master.FindControl("ddlCampaign") as DropDownList;
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-CustomerLookUp" + ".xml";
            txtSearch2.Text = string.Empty;

            if (!string.IsNullOrEmpty(ddlCampaign.SelectedItem.Value) && txtStartDate.Text.Replace(" ","")!="")
            {

                try
                {
                    cNewBuyerSince.CampaignName = ddlCampaign.SelectedItem.Value.Trim(); //SessionFacade.CampaignName.ToString().Trim();
                    cNewBuyerSince.fpdcont = txtStartDate.Text.Replace("%","");
                    cNewBuyerSince.SalesTeam = SessionFacade.KamId.ToString().Trim();

                    dsCustomerLookup = cNewBuyerSince.GetNewBuyer();
                    //ReArrangeDs = dsCustomerLookup;
                    if (dsCustomerLookup.Tables.Count > 0 && dsCustomerLookup.Tables[0].Rows.Count>0)
                    {
                       // cArrangeDataSet ADS = new cArrangeDataSet();
                        //ADS.CampaignName = SessionFacade.CampaignValue;
                       // ADS.UserName = SessionFacade.LoggedInUserName;
                       // ADS.Listview = "lvwLookupData";

                        //int IsReorder = ADS.ColumnReorderCount();
                        //if (IsReorder > 0)
                        //{
                        //    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);

                        //}

                        //if (ReArrangeDs != null)
                        //{


                            //ds = ReArrangeDs;
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            dsCustomerLookup.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            gridCustomerLookup.DataSource = dsCustomerLookup.Tables[0];
                            gridCustomerLookup.DataBind();
                            
                            //if (ds.Tables[0].Rows.Count == 0)
                            //{
                            //    //lookuptable.Visible = false;
                            //    // ckContactLevel.Visible = false;
                            //}
                            //else
                            //{

                            //    gridCustomerLookup.Visible = true;
                            //    //ckContactLevel.Visible = true;
                            //    //lookuptable.Visible = true;
                            //}
                        }
                        //else if (dsCustomerLookup.Tables[0].Rows.Count > 0)
                        //{

                        //    //lookuptable.Visible = true;
                        //    gridCustomerLookup.Visible = true;
                        //    //ckContactLevel.Visible = true;
                        //    gridCustomerLookup.DataSource = dsCustomerLookup;
                        //    gridCustomerLookup.DataBind();

                        //    ds = dsCustomerLookup;

                        //    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        //    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        //    xmlSW.Close();
                        //}
                        else
                        {
                            //grdCustLookup.Visible = false;
                            //pnlGridIndex.Visible = false;
                            gridCustomerLookup.DataSource = null;
                            gridCustomerLookup.DataBind();
                      
                           
                            //ckContactLevel.Visible = false;
                            //lookuptable.Visible = false;
                        }
                    //}


                }
                catch (Exception ex)
                {
                    new ArgumentNullException();

                }
            }
            else
            {
                gridCustomerLookup.DataSource = null;
                gridCustomerLookup.DataBind();
                
            }
        }

    


        protected void gridCustomerLookup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;


            // Get the selected index and the command name

            int _selectedIndex = 0;
            try
            {
                _selectedIndex = int.Parse(e.CommandArgument.ToString());
            }
            catch
            {
                _selectedIndex = 0;
            }
            if (_selectedIndex != 0)
            {
                string _commandName = e.CommandName;

                switch (_commandName)
                {
                    case ("SingleClick"):
                        _gridView.SelectedIndex = _selectedIndex;
                        GridViewRow row = _gridView.SelectedRow;

                        SessionFacade.AccountNo = row.Cells[7].Text.ToString().Trim();
                        SessionFacade.AccountName = row.Cells[2].Text.ToString().Trim();

                        break;

                }
            }


        }

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

        protected void gridCustomerLookup_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                GridViewRow row = gridCustomerLookup.SelectedRow;

                //SessionFacade.AccountNo = row.Cells[2].Text.ToString().Trim();
                CustomerLookup chkContactLevel = new CustomerLookup();

                //if (ckContactLevel.Checked == true)
                //{

                HttpCookie aCookie = new HttpCookie("CNo");
                aCookie.Value = SessionFacade.ContactlevelVal;//row.Cells[GetColumnIndexByName(row, "Contact_Num")].Text.ToString();
                // SessionFacade.AccountNo = row.Cells[GetColumnIndexByName(row, "Account_Num")].Text.ToString();
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);
                //}
                //else
                //{
                //    SessionFacade.AccountNo = row.Cells[GetColumnIndexByName(row, "Account_Num")].Text.ToString();
                //    SessionFacade.BuyerCt = "";

                if (Request.Cookies["CNo"] != null)
                {
                    HttpCookie myCookie = new HttpCookie("CNo");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                }

                //}

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gridCustomerLookup', 'HeaderDiv');</script>");

            }
            catch (Exception x)
            {
                new ArgumentNullException();
            }

        }


        protected void gridCustomerLookup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                bool varColumnExist = false;
                int columnIndex;
                string[] list = { "CONTACT SALES 12M","ACCOUNT NAME", "CONTACT NAME" };
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                DropDownList ddlTempValue = Master.FindControl("ddlCampaignValue") as DropDownList;

                //if (!string.IsNullOrEmpty(ddlTemp.SelectedValue))
                //{
                //    ddlTempValue.ClearSelection();

                //    if (ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue) != null)
                //    {
                //        ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue).Selected = true;
                //    }

                //}


                if (!string.IsNullOrEmpty(ddlTemp.SelectedValue))
                {
                    if (!string.IsNullOrEmpty(ddlTempValue.SelectedValue))
                    {
                        ddlTempValue.ClearSelection();
                    }
                    else
                    {
                        ddlTempValue.Items.Clear();

                        ddlTempValue.Items.AddRange(ddlTemp.Items.OfType<ListItem>().ToArray());
                    }


                    if (ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue) != null)
                    {
                        ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue).Selected = true;
                    }

                }


                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
                {

                    // when mouse is over the row, save original color to new attribute, and change it to highlight color
                    //e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F8BB28;'");

                    // when mouse leaves the row, change the bg color to its original value  
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

                    //e.Row.Attributes.Add("onclick", "Page.ClientScript.GetPostBackClientHyperlink(selectButton, String.Empty)");



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
                                if (list[i] == "CONTACT SALES 12M")
                                {
                                    if (e.Row.RowType == DataControlRowType.DataRow)
                                        e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;

                                    //convert values to $ currency
                                    decimal temp;
                                    if (decimal.TryParse(((DataRowView)e.Row.DataItem)[list[i]].ToString(), out temp) == true)
                                    {
                                       e.Row.Cells[columnIndex].Text = FormatCurrency(decimal.Parse(((DataRowView)e.Row.DataItem)[list[i]].ToString()), ddlTempValue.SelectedItem.Text);
                                       // if (ddlTemp.SelectedValue == "EMED" || ddlTemp.SelectedValue == "US" ||
                                       //ddlTemp.SelectedValue == "CA" || ddlTemp.SelectedValue == "CL" ||
                                       //ddlTemp.SelectedValue == "PC")
                                       //     e.Row.Cells[columnIndex].Text = decimal.Parse(((DataRowView)e.Row.DataItem)[list[i]].ToString()).ToString("C2");
                                       // else if (ddlTemp.SelectedValue == "FR" || ddlTemp.SelectedValue == "BE" ||
                                       //     ddlTemp.SelectedValue == "DE" || ddlTemp.SelectedValue == "NL" ||
                                       //     ddlTemp.SelectedValue == "AT")
                                       //     e.Row.Cells[columnIndex].Text = FormatCurrency(decimal.Parse(((DataRowView)e.Row.DataItem)[list[i]].ToString()), "EUR");
                                       // else if (ddlTemp.SelectedValue == "UK" || ddlTemp.SelectedValue == "SUK")
                                       //     e.Row.Cells[columnIndex].Text = FormatCurrency(decimal.Parse(((DataRowView)e.Row.DataItem)[list[i]].ToString()), "GBP");
                                       // else if (ddlTemp.SelectedValue == "CH")
                                       //     e.Row.Cells[columnIndex].Text = FormatCurrency(decimal.Parse(((DataRowView)e.Row.DataItem)[list[i]].ToString()), "CHF");

                                    }
                                }
                                else if (list[i] == "ACCOUNT NAME")
                                {
                                    byte[] data = Encoding.Default.GetBytes(((DataRowView)e.Row.DataItem)["ACCOUNT NAME"].ToString());
                                    string output = Encoding.UTF8.GetString(data);

                                    e.Row.Cells[columnIndex].Text = output;
                                }
                                else if (list[i] == "CONTACT NAME")
                                {
                                    byte[] data = Encoding.Default.GetBytes(((DataRowView)e.Row.DataItem)["CONTACT NAME"].ToString());
                                    string output = Encoding.UTF8.GetString(data);

                                    e.Row.Cells[columnIndex].Text = output;
                                }

                            }

                            
                           
                                //e.Row.Cells[columnIndex].Text = decimal.Parse(((DataRowView)e.Row.DataItem)[list[i]].ToString()).ToString("C2");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ArgumentNullException();

            }
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

        #region Export to Excel Function
        protected void btn_Export2ExcelClick(object sender, EventArgs e)
        {
            ExportToExcel();
        }

   


        public void ExportToExcel()
        {
            try
            {
                string xmlName;
                DataSet ds = new DataSet();
                string attachment = "attachment; filename=" + SessionFacade.LoggedInUserName + "CustomerLookUp.xls";
                Response.Clear();
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                //if (SessionFacade.UserTrig == "1")
                //{
                //    xmlName = "-CustomerLookUp";
                //}
                //else
                //{
                //    xmlName = "-CustomerLookUp";
                //}
                xmlName = "-CustomerLookUp";
                ds.Tables.Add(GetDatafromXML(xmlName));
                if (ds != null && ds.Tables.Count > 0)
                {
                    //Remove Row Columm
                    if (ds.Tables[0].Columns.Contains("Row"))
                    {
                        ds.Tables[0].Columns.Remove("Row");
                    }
                    gridCustomerLookup.DataSource = ds;
                    gridCustomerLookup.AllowPaging = false;
                    gridCustomerLookup.AllowSorting = false;
                    gridCustomerLookup.Columns[0].Visible = false;
                    gridCustomerLookup.DataBind();

                    //Get the HTML for the control.
                    gridCustomerLookup.RenderControl(hw);
                    //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                    Response.Output.Write(tw.ToString());
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Close();

                    gridCustomerLookup.Columns[0].Visible = true;
                    gridCustomerLookup.AllowPaging = true;
                    gridCustomerLookup.AllowSorting = true;
                    gridCustomerLookup.DataBind();
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

        protected void btnSearch_Click1(object sender, ImageClickEventArgs e)
        {
            ShowCustomerLookUp();
            //if (gridCustomerLookup.Rows.Count > 0)
            //{

            //    gridCustomerLookup.UseAccessibleHeader = true;
            //    gridCustomerLookup.HeaderRow.TableSection = TableRowSection.TableHeader;

            //}
        }

        protected void QuerySearch_Click(object sender, EventArgs e)
        {

            NewBuyerQuery();
            //if (gridCustomerLookup.Rows.Count > 0)
            //{

            //    gridCustomerLookup.UseAccessibleHeader = true;
            //    gridCustomerLookup.HeaderRow.TableSection = TableRowSection.TableHeader;

            //}
        }

        protected void CustomerLookUpPageChanging(object sender, GridViewPageEventArgs e)
        {
            //gridCustomerLookup.SelectedIndex = -1;
            string xmlName;
            xmlName = "-CustomerLookUp";
            //if (SessionFacade.UserTrig == "1")
            //{
            //    xmlName = "-CustomerLookUp";
            //}
            //else
            //{
            //    xmlName = "-CustomerLookUp";
            //}
            gridCustomerLookup.DataSource = GetDatafromXML(xmlName);
            gridCustomerLookup.PageIndex = e.NewPageIndex;
            gridCustomerLookup.DataBind();

        }

        public DataTable GetDatafromXML(string xmlName = "-CustomerLookUp")
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + xmlName + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = "1 = 1";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            //To Copy the Schema.
            dtTemp = ds.Tables[0].Clone();


            results = ds.Tables[0].Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {

                GridViewSortDirection = SortDirection.Descending;

                SortGridView(sortExpression, "DESC");

            }

            else
            {

                GridViewSortDirection = SortDirection.Ascending;

                SortGridView(sortExpression, "ASC");

            }

        }

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

        private void SortGridView(string sortExpression, string direction)
        {

            // You can cache the DataTable for improving performance
            try
            {
                DataSet ds = new DataSet();

                string xmlName;
                if (SessionFacade.UserTrig == "1")
                {
                    xmlName = "-CustomerLookUp";
                }
                else
                {
                    xmlName = "-CustomerLookUp";
                }

                DataTable dt = GetDatafromXML(xmlName);
                DataView dv = new DataView(dt);
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + xmlName + ".xml";

                dv.Sort = sortExpression + " " + direction;
                dt = dv.ToTable();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                dt.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                gridCustomerLookup.DataSource = dv;
                gridCustomerLookup.DataBind();
            }
            catch (Exception ex)
            {
                new ArgumentNullException();

            }

        }


        #region Read From XML Customer And load to pop

        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLDetails(string AccntVal, string ContVal)
        {
            DataSet ds = new DataSet();

            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-CustomerLookUp" + ".xml";
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


                    if (AccntVal != null && ContVal != null && ContVal != "&nbsp;")
                    {

                        Query = Query + " and [ACCOUNT NUM] = '" + AccntVal + "' and  [CONTACT NUM] ='" + ContVal + "'";

                    }


                    if (AccntVal != null && ContVal == "&nbsp;")

                    {
                        Query = Query + " and [ACCOUNT NUM] = '" + AccntVal + "'";
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

        #region Code not in use


        //public void ExportExcelFunction()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();

        //        string xmlName;
        //        if (SessionFacade.UserTrig == "1")
        //        {
        //            xmlName = "-CustomerLookUp";
        //        }
        //        else
        //        {
        //            xmlName = "-CustomerLookUp";
        //        }

        //        // dt = GetDatafromXML(xmlName);

        //        if (dt.Rows.Count > 0)
        //        {




        //            string UserFileName = SessionFacade.LoggedInUserName + "CustomerLookUp.xls";
        //            if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName)))
        //                File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

        //            File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/CustomerLookUptemp.xls"), Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

        //            string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName);
        //            bool exportToEx = Export2Excel.Export(dt, filename);

        //            //true means Excel File has been written
        //            if (exportToEx == true)
        //            {
        //                if (Request.Browser.Type == "Desktop") //For chrome
        //                {
        //                    ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

        //                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=CustomerLookUp&FilePath=" + UserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=CustomerLookUp&FilePath=" + UserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
        //                }
        //            }
        //            else
        //            {
        //                // Response.Write("Data not Exported to Excel File");
        //            }
        //        }




        //    }
        //    catch (Exception err)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogMessage("Customer LookUp  - Error in Export to Excel" + err.ToString());
        //    }
        //}

  

       

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{

        //     //LoadData();
        //   // loadsearchbutton();

        //     ShowCustomerLookUp();

        //}






        

      

       

       

        //protected void BtnArrangeCol_Click(object sender, EventArgs e)
        //{
           
        //}

       

        //protected void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    txtStartDate.Text = "";
        //    ShowCustomerLookUp();
        //    if (gridCustomerLookup.Rows.Count > 0)
        //    {

        //        gridCustomerLookup.UseAccessibleHeader = true;
        //        gridCustomerLookup.HeaderRow.TableSection = TableRowSection.TableHeader;

        //    }
        //}

        //protected void txtStartDate_TextChanged(object sender, EventArgs e)
        //{
        //    NewBuyerSince cNewBuyerSince = new NewBuyerSince();
        //    DataSet ReArrangeDs = new DataSet();
        //    DataSet ds;
        //    DataTable dtTemp = new DataTable();
        //    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-CustomerLookUp" + ".xml";

        //    if (!string.IsNullOrEmpty(SessionFacade.CampaignName))
        //    {

        //        try
        //        {
        //            cNewBuyerSince.CampaignName = SessionFacade.CampaignName.ToString().Trim();
        //            cNewBuyerSince.fpdcont = txtStartDate.Text;
        //            cNewBuyerSince.SalesTeam = SessionFacade.KamId.ToString().Trim();

        //            dsCustomerLookup = cNewBuyerSince.GetNewBuyer();
        //            ReArrangeDs = dsCustomerLookup;
        //            if (ReArrangeDs != null)
        //            {
        //                cArrangeDataSet ADS = new cArrangeDataSet();
        //                ADS.CampaignName = SessionFacade.CampaignValue;
        //                ADS.UserName = SessionFacade.LoggedInUserName;
        //                ADS.Listview = "lvwLookupData";

        //                int IsReorder = ADS.ColumnReorderCount();
        //                if (IsReorder > 0)
        //                {
        //                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);

        //                }

        //                if (ReArrangeDs != null)
        //                {

        //                    gridCustomerLookup.DataSource = ReArrangeDs;
        //                    gridCustomerLookup.DataBind();

        //                    ds = ReArrangeDs;
        //                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
        //                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //                    xmlSW.Close();

        //                    if (ds.Tables[0].Rows.Count == 0)
        //                    {
        //                        //lookuptable.Visible = false;
        //                        // ckContactLevel.Visible = false;
        //                    }
        //                    else
        //                    {

        //                        gridCustomerLookup.Visible = true;
        //                        //ckContactLevel.Visible = true;
        //                        //lookuptable.Visible = true;
        //                    }
        //                }
        //                else if (dsCustomerLookup.Tables[0].Rows.Count > 0)
        //                {

        //                    //lookuptable.Visible = true;
        //                    gridCustomerLookup.Visible = true;
        //                    //ckContactLevel.Visible = true;
        //                    gridCustomerLookup.DataSource = dsCustomerLookup;
        //                    gridCustomerLookup.DataBind();

        //                    ds = dsCustomerLookup;

        //                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
        //                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //                    xmlSW.Close();
        //                }

        //                else
        //                {
        //                    //grdCustLookup.Visible = false;
        //                    //pnlGridIndex.Visible = false;
        //                    gridCustomerLookup.Visible = false;
        //                    //ckContactLevel.Visible = false;
        //                    //lookuptable.Visible = false;
        //                }
        //            }

        //            txtStartDate.Focus();
        //        }
        //        catch (Exception ex)
        //        {
        //            new ArgumentNullException();

        //        }
        //    }

        //    if (gridCustomerLookup.Rows.Count > 0)
        //    {

        //        gridCustomerLookup.UseAccessibleHeader = true;
        //        gridCustomerLookup.HeaderRow.TableSection = TableRowSection.TableHeader;

        //    }

        //}
        //protected void btnSearch_Click1(object sender, EventArgs e)
        //{

        //    ShowCustomerLookUp();
        //    if (gridCustomerLookup.Rows.Count > 0)
        //    {

        //        gridCustomerLookup.UseAccessibleHeader = true;
        //        gridCustomerLookup.HeaderRow.TableSection = TableRowSection.TableHeader;

        //    }
        //}
        #endregion

       
    }



}