using System;
using System.Data; // Name space for Dataset, Dataadapter etc
using System.Configuration; //name space needed to access the Web Config file
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AppLogic;

using System.IO; //This namespace has to be used if you are doing any file operation ( For example  read or write text file,excel,)
using System.Text;
using System.Collections; //This namespace has to be used while using arrays etc
using System.Xml; //This will be used if you want to read Or use xml files
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Web.Script.Services; ////This namespace has to be used while accessing  SQL Database


namespace WebSalesMine.WebPages.Home
{
    public partial class KamWindow1 : System.Web.UI.Page
    {
        public static string MainAccountNo = "";
        public static string MainAccountName = "";
        public string OnHoldDate = "";
        public string CodeBehindVar;

        public static SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
        public static SqlConnection OnlineConstre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaOnlineCONNECTON"].ToString());

        protected DataSet KamDataSet = new DataSet();

        protected void Page_init(object sender, EventArgs e)
        {
            TitleV.Text = "Kam Window : " + SessionFacade.KamId + " - " + SessionFacade.KamName;
        }

        public void LoadNoteType()
        {
            DataSet ds = new DataSet();
            cNotesCommHistory objNoteType = new cNotesCommHistory();
            objNoteType.CampaignName = SessionFacade.CampaignName;

            ds = objNoteType.GetNoteType();

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    ListItem L1 = new ListItem();

                    //Note Type Filtering
                    ddlNoteType.DataSource = ds;
                    ddlNoteType.DataTextField = "NoteType";
                    ddlNoteType.DataValueField = "NoteType";
                    ddlNoteType.DataBind();

                    L1 = new ListItem("", "");
                    ddlNoteType.Items.Insert(0, L1);
                }
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

        private void BindDummy()
        {
            try
            {
                DataTable dummy = new DataTable();
                dummy.Columns.Add("SOLD_TO");
                dummy.Columns.Add("NAME");
                dummy.Columns.Add("MG_NAME");
                dummy.Columns.Add("LPDCUST");
                dummy.Columns.Add("Retention");
                dummy.Columns.Add("sales12M");
                dummy.Columns.Add("sales13to24M");
                dummy.Columns.Add("LTSALES");
                dummy.Columns.Add("Date");
                dummy.Columns.Add("NOTES");
                dummy.Columns.Add("TYPE");
                dummy.Columns.Add("CREATEDON");
                if (SessionFacade.CampaignName == "DE" ||
                   SessionFacade.CampaignName == "UK" ||
                   SessionFacade.CampaignName == "FR")
                    dummy.Columns.Add("OnHoldDate");
                dummy.Rows.Add();
                gridShowAllUserData.DataSource = dummy;
                gridShowAllUserData.DataBind();
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "KAM: BindDummy", SessionFacade.CampaignName,
                    SessionFacade.LoggedInUserName, SessionFacade.AccountNo,
                    SessionFacade.KamId.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionFacade.KamId != null)
                {
                    if (SessionFacade.KamId.Length > 0)
                    {
                        pnlShowKamWindow.Visible = true;
                        litMessage.Visible = false;
                        BindGrid();
                        BindDummy();
                    }
                    else
                    {
                        pnlShowKamWindow.Visible = false;
                        litMessage.Visible = true;
                    }

                }

                LoadNoteType();

                //Show only to Germany
                if (SessionFacade.CampaignName == "DE" ||
                    SessionFacade.CampaignName == "UK" ||
                    SessionFacade.CampaignName == "FR")
                    lnkOnHold.Visible = true;
                else
                    lnkOnHold.Visible = false;
            }

        }

        [System.Web.Services.WebMethod]
        public static void GetData(string Val)
        {
            AppLogic.SessionFacade.AccountNo = Val;
        }

        public int selectedroindex
        {
            get
            {
                if (ViewState["selectedroindex"] != null)
                    return (int)ViewState["selectedroindex"];
                else
                    return -1;
            }
            set
            {
                ViewState["selectedroindex"] = value;
            }
        }

        protected void gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int myOldSelectedRowIndex = selectedroindex;
            if (myOldSelectedRowIndex != -1)
            {
                GridViewRow selectedPreviousRow = gridShowAllUserData.Rows[myOldSelectedRowIndex];
                selectedPreviousRow.CssClass = "RowStyle";// .BackColor = System.Drawing.Color.White;
            }

            GridViewRow mySelectedRow =
               ((GridViewRow)(((DataControlFieldCell)(((WebControl)(e.CommandSource)).Parent)).Parent));

            int myNewSelectedRowIndex = mySelectedRow.RowIndex;
            ViewState["selectedroindex"] = myNewSelectedRowIndex;

            GridViewRow currentSelectedRow = gridShowAllUserData.Rows[myNewSelectedRowIndex];
            currentSelectedRow.CssClass = "SelectedRowStyle"; //.BackColor = System.Drawing.Color.Aqua;

            switch (e.CommandName)
            {

                case "Click":
                    {

                        SessionFacade.AccountNo = e.CommandArgument.ToString();
                        break;

                    }
                default: break;

            }

        }

        protected void gridShowAllUserData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    byte[] data = Encoding.Default.GetBytes(((DataRowView)e.Row.DataItem)["NAME"].ToString());
                    string output = Encoding.UTF8.GetString(data);

                    e.Row.Cells[1].Text = output;
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Row Databound - KAM",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        public void clearcss()
        {
            HttpCookie aCookie2 = new HttpCookie("CSS");
            aCookie2.Value = "None";
            aCookie2.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(aCookie2);
        }

        protected void btnShowPage(object sender, EventArgs e)
        {
            string s = hfIndex.Value; //Put breakpoint here to check value
            if (s.Length > 0)
            {
                SessionFacade.AccountNo = gridShowAllUserData.Rows[Int32.Parse(s) - 1].Cells[1].Text.ToString();
                // gridShowAllUserData.Rows[Int32.Parse(s) - 1].CssClass = "SelectedRowStyle";
                //SelectSingleRadiobutton

                //LinkButton b = sender as LinkButton;
                //var blink = ((LinkButton)sender).ID.ToString();

                //if (blink.Length > 0)
                //{
                //    if (blink == "lnkOrderHistory")
                //    {
                //        //ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../OrderHistory/OrderHistory.aspx'; ", true);
                //        Page.ClientScript.RegisterStartupScript(GetType(), "callme", "window.opener.location='../OrderHistory/OrderHistory.aspx'; ", true);


                //    }

                //}


                LinkButton b = ((LinkButton)sender);

                if (b != null)
                {
                    if (b.ID == "lnkOrderHistory")
                    {
                        clearcss();
                        ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", " OrderHistoryClick();window.opener.location.href='../OrderHistory/OrderHistory.aspx';", true);
                        //ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../OrderHistory/OrderHistory.aspx'; ", true);
                        // Page.ClientScript.RegisterStartupScript(GetType(), "callme", "window.opener.location='../OrderHistory/OrderHistory.aspx'; ", true);
                    }
                    if (b.ID == "lnkProductSummary")
                    {
                        clearcss();
                        ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "productClick();window.opener.location='../ProductSummary/ProductSummary.aspx';", true);
                        //ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../ProductSummary/ProductSummary.aspx'; ", true);
                    }

                    if (b.ID == "lnkNotes")
                    {
                        clearcss();
                        ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "NotesClick();window.opener.location='../NotesCommHistory/NotesCommHistory.aspx';", true);
                        //ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../NotesCommHistory/NotesCommHistory.aspx'; ", true);
                    }
                    if (b.ID == "lnkQuotes")
                    {
                        clearcss();
                        ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "QuoteClick();window.opener.location='../Quotes/Quotes.aspx';", true);
                        //ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../Quotes/Quotes.aspx'; ", true);
                    }

                    if (b.ID == "lnkCustomerInfo")
                    {
                        clearcss();
                        ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "CustomerClick();window.opener.location='../SiteAndContactInfo/SiteAndContactInfo.aspx';", true);
                        //ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../SiteAndContactInfo/SiteAndContactInfo.aspx'; ", true);
                    }
                    //if (b.ID == "CustomerLookUp")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../CustomerLookUp/CustomerLookUp.aspx'; ", true);
                    //}



                }


            }
            LinkButton c = ((LinkButton)sender);
            if (c.ID == "lnkNotesTerritory")
            {
                clearcss();
                ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "NotesTClick();notesTCaution(); ", true);
                //ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../NotesCommHistory/NotesCommHistoryTerritory.aspx'; ", true);
            }
            if (c.ID == "lnkProductSummaryTerritory")
            {
                clearcss();
                ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "productTClick();productCaution(); ", true);
                //ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../ProductSummary/ProductSummaryT.aspx'; ", true);
            }
        }

        private void BindGrid()
        {
            BoundField bfield = new BoundField();
            try
            {

                if (SessionFacade.CampaignName == "DE" ||
                   SessionFacade.CampaignName == "UK" ||
                   SessionFacade.CampaignName == "FR")
                {
                    bfield.HeaderText = "Last OnHold Order Date";
                    bfield.DataField = "OnHoldDate";
                    bfield.SortExpression = "OnHoldDate";
                    bfield.DataFormatString = "{0:MMM dd, yyyy}";

                    lnkOnHold.Visible = true;

                    gridShowAllUserData.Columns.Add(bfield);

                    //gridShowAllUserData.DataSource = null;
                    //gridShowAllUserData.DataBind();
                }
                else
                    lnkOnHold.Visible = false;


              
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Binding Gridview in KAM", SessionFacade.CampaignName,
                    SessionFacade.LoggedInUserName, SessionFacade.AccountNo,
                    SessionFacade.KamId.ToString());
            }

        }

        [System.Web.Services.WebMethod]
        public static void UpdateNote(string Note, string Type,
            string Createdon, string customer)
        {
            try
            {
                cAddNote objUpdateNote = new cAddNote();

                objUpdateNote.Note = Note;
                objUpdateNote.NoteType = Type;
                objUpdateNote.Createdon = Createdon;
                objUpdateNote.AccountNum = customer;
                objUpdateNote.Campaign = SessionFacade.CampaignName;

                if (objUpdateNote.UpdateNote())
                {


                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Updating Note in KAM Window.",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                    SessionFacade.KamId.ToString());
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetKAMData(string PageNum, string Search, string Searchby)
        {
            try
            {
                string pagesize = "100";
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-KAM" + ".xml";
                //DataSet dsCurrent = new DataSet(), dsNew = new DataSet();
                //DataTable dtTemp = new DataTable();
                DataSet dsCurrent = new DataSet();
                string strResult = string.Empty;

                if (Search == "")
                    strResult = "SelectKAMNewV3 " + SessionFacade.CampaignName + ",'" + SessionFacade.KamId + "','"
                    + Searchby + "'," + pagesize + "," + PageNum;
                else
                    strResult = "SelectKAMNewV3 " + SessionFacade.CampaignName + ",'" + SessionFacade.KamId + "','" +
                     Searchby + "'," + pagesize + "," + PageNum + ",'Name','rn','" + Search + "'";

                dsCurrent = SqlHelper.SqlDataExcuteDataSet(OnlineConstre, CommandType.Text, strResult);

                DataTable dt = new DataTable("Pager");
                dt.Columns.Add("PageIndex");
                dt.Columns.Add("PageSize");
                dt.Columns.Add("RecordCount");
                dt.Rows.Add();
                dt.Rows[0]["PageIndex"] = PageNum;
                dt.Rows[0]["PageSize"] = pagesize;
                dt.Rows[0]["RecordCount"] = dsCurrent.Tables[0].Rows.Count > 0 ? dsCurrent.Tables[0].Rows[0]["total_rows"] : 0;
                //dsCurrent.Tables.Add(dtTemp);
                dsCurrent.Tables.Add(dt);

                return dsCurrent.GetXml();
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Getting Data from Database in KAM", SessionFacade.CampaignName,
                    SessionFacade.LoggedInUserName, SessionFacade.AccountNo,
                    SessionFacade.KamId.ToString());
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetDatafromXMLSort(string Searchby, string PageNum, string Sortby,
            string AscDsc, string Search = "")
        {
            try
            {
               // string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-KAM" + ".xml";
                DataTable dtTemp = new DataTable();
                DataSet dsCurrent = new DataSet();
                string strResult = string.Empty, pagesize = "100";

                if (Search == "" || Search == "undefined")
                    strResult = "SelectKAMNewV3 " + SessionFacade.CampaignName + ",'" + SessionFacade.KamId + "','" +
                     Searchby + "'," + pagesize + "," + PageNum + ",'" + Sortby + "','" + AscDsc + "'";
                else
                    strResult = "SelectKAMNewV3 " + SessionFacade.CampaignName + ",'" + SessionFacade.KamId + "','" +
                    Searchby + "'," + pagesize + "," + PageNum + ",'" + Sortby + "','" + AscDsc + "','" + Search + "'";

                dsCurrent = SqlHelper.SqlDataExcuteDataSet(OnlineConstre, CommandType.Text, strResult);

                DataTable dt = new DataTable("Pager");
                dt.Columns.Add("PageIndex");
                dt.Columns.Add("PageSize");
                dt.Columns.Add("RecordCount");
                dt.Rows.Add();
                dt.Rows[0]["PageIndex"] = PageNum;
                dt.Rows[0]["PageSize"] = "100";
                dt.Rows[0]["RecordCount"] = dsCurrent.Tables[0].Rows[0]["total_rows"];
                //dsCurrent.Tables.Add(dtTemp);
                dsCurrent.Tables.Add(dt);

                return dsCurrent.GetXml();
            }
            catch
            {
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetAutoCompleteResult(string Search, string SearchBy)
        {
            try
            {
                DataSet dsCurrent = new DataSet();
                string strResult = string.Empty;

                strResult = "SelectKAMAutoComplete " + SessionFacade.CampaignName + ",'" + SessionFacade.KamId + "','"
                    + Search + "','" + SearchBy + "'";

                dsCurrent = SqlHelper.SqlDataExcuteDataSet(OnlineConstre, CommandType.Text, strResult);
               
                return dsCurrent.GetXml();
            }
            catch
            {
                return null;
            }
        }

    }

    public class KamData
    {
        public string Name { get; set; }
        public string Sold_to { get; set; }
        public string MgName { get; set; }
        public string Lpdcust { get; set; }
        public string Retention { get; set; }
        public string sales12M { get; set; }
        public string sales13to24M { get; set; }
        public string LTSALES { get; set; }
        public string Date { get; set; }
    }
}