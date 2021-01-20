using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLogic;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Text;

namespace WebSalesMine.WebPages.Home
{
    public partial class ContactLevel : System.Web.UI.Page
    {

        private int Customername = 1000;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rdoContactName.Checked = true;
                if (Request.Cookies["CNo"] != null)
                {
                    Name.Value = Request.Cookies["CName"].Value;
                    Number.Value = Request.Cookies["CNo"].Value;
                }
                BindContactLevel();
                SessionFacade.NameOrContacts = "";
            }
        }

        public void BindContactLevel()
        {
            DataSet drExisting = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                       Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                       + "-ContactLevel" + ".xml";

            try
            {
                string[] TempAccount = new string[6];
                cContactLevel objContactLevel = new cContactLevel();
                //Campaign
                objContactLevel.SearchCampaign = SessionFacade.CampaignName.ToString().Trim();

                //Search by Account
                objContactLevel.SearchAccount = SessionFacade.AccountNo.ToString().Trim();

                switch (Path.GetFileName(Request.UrlReferrer.ToString()))
                {
                    case "Quotes.aspx":
                        objContactLevel.Page = "Quotes";
                        break;
                    case "OrderHistory.aspx":
                        objContactLevel.Page = "Orders";
                        break;
                    case "ProductSummaryT.aspx":
                    case "ProductSummary.aspx":
                        objContactLevel.Page = "Product";
                        break;

                    default:
                        objContactLevel.Page = "Notes";
                        break;
                }

                //if (Path.GetFileName(Request.UrlReferrer.ToString()).IndexOf("OrderHistory.aspx") > -1)
                //    objContactLevel.Page = "Orders";
                //else if (Path.GetFileName(Request.UrlReferrer.ToString()).IndexOf("Quotes.aspx") > -1)
                //    objContactLevel.Page = "Quotes";
                //else if (Path.GetFileName(Request.UrlReferrer.ToString()).IndexOf("ProductSummary.aspx") > -1
                //    || Path.GetFileName(Request.UrlReferrer.ToString()).IndexOf("ProductSummaryT.aspx") > -1)
                //    objContactLevel.Page = "Product";
                //else
                //    objContactLevel.Page = "Notes";


                drExisting = objContactLevel.GetListContactLevel();

                if (drExisting.Tables.Count > 0)
                {
                    if (drExisting.Tables[0].Rows.Count > 0)
                    {
                        TempAccount = SessionFacade.LastAccount;
                        TempAccount[5] = SessionFacade.AccountNo.ToString().Trim();
                        SessionFacade.LastAccount = TempAccount;

                        //SetOrdinal(drExisting);

                        gridShowContact.DataSource = drExisting;
                        gridShowContact.DataBind();

                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        drExisting.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                    }
                    else
                    {
                        gridShowContact.DataSource = null;
                        gridShowContact.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Contact Level - BindingData", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                //litErrorinGrid.Text = "Error on getting Data";
                gridShowContact.DataSource = null;
                gridShowContact.DataBind();

            }
        }

        public DataTable GetDatafromXML()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                      Path.DirectorySeparatorChar + "XMLFiles\\" + 
                      SessionFacade.LoggedInUserName
                      + "-ContactLevel" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = "1=1 ";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            //To Copy the Schema.
            dtTemp = ds.Tables[0].Clone();

            //Customer Name and Number
            //if (rdoContactName.Checked == true)
            //{
            //    Query = Query + " and name Like '%" + txtSearchContact.Text + "%'";
            //}
            //else if (rdoContactNumber.Checked == true)
            //{
            //    Query = Query + " and contmerg Like '%" + txtSearchContact.Text + "%'";
            //}

            results = ds.Tables[0].Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;
        }

        protected void btnSearchContact_Click(object sender, EventArgs e)
        {
            string[] TempAccount = new string[6];
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName 
                + "-ContactLevel" + ".xml";
            if (SessionFacade.AccountNo != null)
            {
                if (SessionFacade.LastAccount[5] == SessionFacade.AccountNo.ToString().Trim())
                {
                    gridShowContact.DataSource = GetDatafromXML();
                    gridShowContact.DataBind();
                }
                else
                {
                    BindContactLevel();
                }
            }
        }

        protected void txtSearchContact_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                        Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                        + "-ContactLevel" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            gridShowContact.DataSource = ds;
            gridShowContact.DataBind();

            //rdoContactName.Checked = false;
            //rdoContactNumber.Checked = false;
            txtSearchContact.Text = string.Empty;

            SessionFacade.NameOrContacts = "";
        }

        #region PageChanging
        protected void gridShowContact_Paging(object sender, GridViewPageEventArgs e)
        {

            DataSet dsTemp = new DataSet();

            dsTemp.Tables.Add(GetDatafromXML());

            //SetOrdinal(dsTemp);

            gridShowContact.DataSource = dsTemp;
            gridShowContact.PageIndex = e.NewPageIndex;
            gridShowContact.DataBind();
            //pnl1.Visible = true;
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
        protected void gridShowContact_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridShowContact(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridShowContact(sortExpression, "ASC");
            }
        }
        private void SortGridShowContact(string sortExpression, string direction)
        {
            try 
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                        Path.DirectorySeparatorChar + "XMLFiles\\" + 
                        SessionFacade.LoggedInUserName
                        + "-ContactLevel" + ".xml";
                    if (File.Exists(Pathname))
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = GetDatafromXML();
                        DataView dv = new DataView(dt);

                        dv.Sort = sortExpression + " " + direction;

                        ds.Tables.Add(dv.ToTable());

                        //SetOrdinal(ds);

                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridShowContact.DataSource = dv;
                        gridShowContact.DataBind();

                    }

                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Contact Level - Button Login Click Error", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
        #endregion

        protected void txtCustomerNumber_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void BtnClearSearchContact_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                        Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                        + "-ContactLevel" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            gridShowContact.DataSource = ds;
            gridShowContact.DataBind();

            //rdoContactName.Checked = false;
            //rdoContactNumber.Checked = false;
            txtSearchContact.Text = string.Empty;

            SessionFacade.NameOrContacts = "";
        }

        protected void txtSearchContact_TextChanged1(object sender, EventArgs e)
        {
            gridShowContact.DataSource =  GetDatafromXML();
            gridShowContact.DataBind();
        }

        protected void gridShowContact_Prerender(object sender, EventArgs e)
        {
            if (gridShowContact.HeaderRow != null)
            {
                LinkButton lnk = new LinkButton();
                lnk = gridShowContact.HeaderRow.Cells[5].Controls[0] as LinkButton;

                //switch (Path.GetFileName(Request.UrlReferrer.ToString()))
                //{
                //    case "Quotes.aspx":
                //        lnk.Text = "NO OF QUOTES";
                //        break;
                //    case "OrderHistory.aspx":
                //        lnk.Text = "NO OF ORDERS";
                //        break;
                //    case "ProductSummaryT.aspx":
                //    case "ProductSummary.aspx":
                //        lnk.Text = "NO OF PRODUCTS";
                //        break;
                //    default:
                //        lnk.Text = "NO OF NOTES";
                //        break;
                //}

                if (Path.GetFileName(Request.UrlReferrer.ToString()).IndexOf("OrderHistory.aspx") > -1)
                    lnk.Text = "NO OF ORDERS";
                else if (Path.GetFileName(Request.UrlReferrer.ToString()).IndexOf("Quotes.aspx") > -1)
                    lnk.Text = "NO OF QUOTES";
                else if (Path.GetFileName(Request.UrlReferrer.ToString()).IndexOf("ProductSummary.aspx") > -1
                    || Path.GetFileName(Request.UrlReferrer.ToString()).IndexOf("ProductSummaryT.aspx") > -1)
                    lnk.Text = "NO OF PRODUCTS";
                else
                    lnk.Text = "NO OF NOTES";
            }
        }

        //private void SetOrdinal(DataSet dsTemp)
        //{

        //    try
        //    {

        //        if (dsTemp.Tables[0].Columns.Contains("name"))
        //        {
        //            Customername = dsTemp.Tables[0].Columns["name"].Ordinal;
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogException(err, "SetOrdinal", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
        //    }
        //}

        #region AutoComplete
        public DataTable GetDatafromXMLData(string txtSearchContact)
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                      Path.DirectorySeparatorChar + "XMLFiles\\" +
                      SessionFacade.LoggedInUserName
                      + "-ContactLevel" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = "1=1 ";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            //To Copy the Schema.
            dtTemp = ds.Tables[0].Clone();

            //Customer Name and Number
            if (SessionFacade.NameOrContacts == "name")
                Query = Query + " and name Like '%" + txtSearchContact + "%'";
            else if (SessionFacade.NameOrContacts == "number")
                Query = Query + " and contmerg Like '%" + txtSearchContact + "%'";

            results = ds.Tables[0].Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;
        }

        public string CheckRadioButton()
        {
            if (SessionFacade.NameOrContacts == "name")
                return "name";
            else if (SessionFacade.NameOrContacts == "number")
                return "number";
            else
                return string.Empty;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count)
        {
            DataTable dt;

            ContactLevel m = new ContactLevel();

            dt = m.GetDatafromXMLData(prefixText);

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                if (m.CheckRadioButton() == "name")
                {
                    dbValues = row["name"].ToString();
                    dbValues = dbValues.ToLower();
                }
                else if (m.CheckRadioButton() == "number")
                    dbValues = row["contmerg"].ToString();
                else
                    dbValues = "";

                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }

        #endregion

        protected void rdoContactName_CheckedChanged(object sender, EventArgs e)
        {
            SessionFacade.NameOrContacts = "name";
        }

        protected void rdoContactNumber_CheckedChanged(object sender, EventArgs e)
        {
            SessionFacade.NameOrContacts = "number";
        }

        protected void gridShowContact_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

    }
}