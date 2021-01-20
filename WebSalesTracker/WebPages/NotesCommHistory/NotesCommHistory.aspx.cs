using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AppLogic;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using WebSalesMine.WebPages.UserControl;

namespace WebSalesMine.WebPages.Notes_CommHistory
{

    public partial class NotesCommHistory : System.Web.UI.Page
    {

        public string userRule = string.Empty,
                   AccountNH = string.Empty,
                   ContactMH = string.Empty;
        public DropDownList ddlCampaignCurrencyNH = new DropDownList();
        public DropDownList ddlCampaignNH = new DropDownList();
        public int TabValue = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            NewMasterPage MasterPage = Master as NewMasterPage;
            cNotesCommHistory objNotes = new cNotesCommHistory();

            GetAccountInfo(MasterPage);

            //View Export Excel
            lnkExportToExcel.Visible = objNotes.StatusExcelButton();

            //Load Note Type Condition
            if (!IsPostBack)
            {
                LoadNoteType();
            }
            else
            {
                string Contro = string.Empty;
                Contro = getPostBackControlID();
                if (Contro == "ddlCampaign")
                    LoadNoteType();
            }

            if (SearchBy.Value == "0")
            {

                cDialerData objDialerHistory = new cDialerData();
                string PreviousAccountNo, PreviousCampaign;
                ImageSelectContact.Visible = false;

                PreviousAccountNo = SessionFacade.AccountNo;
                PreviousCampaign = SessionFacade.CampaignName;
                SessionFacade.AccountNo = AccountNH;
                SessionFacade.CampaignName = ddlCampaignNH.SelectedValue;
                objDialerHistory.SearchOrderCampaignName = SessionFacade.CampaignName;
                objDialerHistory.SearchOrderAccount = SessionFacade.AccountNo.ToString().Trim();
                objDialerHistory.SearchOrderContact = SessionFacade.BuyerCt;
                userRule = SessionFacade.UserRole.Trim();

                if (!IsPostBack)
                {

                    TabValue = 1;

                    string acTUser = SessionFacade.LoggedInUserName;
                    btnAllNotes2.Text = "Show All My Notes";

                    txtNoteStartDate.Attributes.Add("readonly", "true");
                    txtStartDate.Attributes.Add("readonly", "true");
                    txtEndDate.Attributes.Add("readonly", "true");

                    string CName;

                    ImageSelectContact.Visible = false;
                    if (Request.Cookies["CName"] != null)
                    {
                        CName = Request.Cookies["CName"].Value.ToString();
                        CName = CName.Replace("%2C", ",");
                        CName = CName.Replace("%20", " ");
                        lnkContactSelected.Text = CName;
                        SessionFacade.BuyerCt = Request.Cookies["CNo"].Value.ToString();
                        ImageSelectContact.Visible = true;

                    }
                    else
                    {
                        SessionFacade.BuyerCt = null;
                        lnkContactSelected.Text = "";
                        ImageSelectContact.Visible = false;

                    }

                    ShowNotesHistory();
                }
                else
                {
                    //string CName;

                    TabValue = 2;

                    ImageSelectContact.Visible = false;

                    string ControlId = string.Empty;
                    ControlId = getPostBackControlID();

                    if (ControlId == null || ControlId == "BtnNotesColumn2")
                    {
                        LoadNoteDetails();
                    }
                    else if (ControlId == "ddlCampaign" || ControlId == "imbSearchProjID" ||
                        ControlId == "txtProjectID")
                    {
                        TabValue = 1;
                        LoadNoteDetails();

                    }
                    else if (ControlId == "imbSearchAcntNumber" || ControlId == "txtAccountNumber")
                    {

                        SessionFacade.UserTrig = "0";

                        //Check if Account Number is Changed
                        if (PreviousAccountNo != AccountNH)
                            TabValue = 1;
                        else
                            TabValue = 2;

                        LoadNoteDetails();

                    }
                    else if (ControlId == "txtStartDate" || ControlId == "txtEndDate" || ControlId == "ByDate")
                    {
                        if (txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "")
                        {
                            SessionFacade.UserTrig = "0";
                            LoadNoteDetails();
                        }
                    }
                }
            }
            else
            {
                SearchBy.Value = "0";
            }

            if (grdNotesHistory.Rows.Count > 0)
            {

                grdNotesHistory.UseAccessibleHeader = true;
                grdNotesHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

            }

        }

        private void GetAccountInfo(NewMasterPage MasterPage)
        {
            if (MasterPage != null)
            {
                AccountNH = MasterPage.AccountNumberMaster.FormatAccountNumber();
                ddlCampaignNH = MasterPage.CampaignMaster;
                //SessionFacade.CampaignName = MasterPage.CampaignMaster.SelectedValue;
                ddlCampaignCurrencyNH = MasterPage.CampaignCurrencyMaster;
            }
        }

        public void LoadNoteDetails()
        {
            string CName;

            if (Request.Cookies["CName"] != null)
            {
                CName = Request.Cookies["CName"].Value.ToString();
                CName = CName.Replace("%2C", ",");
                CName = CName.Replace("%20", " ");
                lnkContactSelected.Text = CName;
                SessionFacade.BuyerCt = Request.Cookies["CNo"].Value.ToString();
                ImageSelectContact.Visible = true;

                ShowNotesHistory();

            }
            else
            {
                SessionFacade.BuyerCt = null;

                if (SessionFacade.UserTrig == "1")
                {
                    ShowNotesHistory(true);
                }
                else
                {
                    ShowNotesHistory();
                }

                lnkContactSelected.Text = "";
                ImageSelectContact.Visible = false;

            }
        }

        public void LoadNoteType()
        {
            //NewMasterPage MasterPage = Master as NewMasterPage;
            DataSet ds = new DataSet();
            cNotesCommHistory objNoteType = new cNotesCommHistory();

            //if (MasterPage != null)
            //{
            //    ddlCampaignNH = MasterPage.CampaignMaster;
            //}

            objNoteType.CampaignName = ddlCampaignNH.SelectedValue;

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

                    L1 = new ListItem("All", "All");
                    ddlNoteType.Items.Insert(0, L1);

                    L1 = new ListItem("SAP Tickler", "SAP");
                    ddlNoteType.Items.Insert(1, L1);

                    L1 = new ListItem("Dialer", "Dialer");
                    ddlNoteType.Items.Insert(2, L1);

                    L1 = new ListItem("Disposition Notes", "Disposition Notes");
                    ddlNoteType.Items.Insert(3, L1);

                    //Note Type in Add Note
                    NoteTypes.DataSource = ds;
                    NoteTypes.DataTextField = "NoteType";
                    NoteTypes.DataValueField = "NoteType";
                    NoteTypes.DataBind();

                    L1 = new ListItem("", "");
                    NoteTypes.Items.Insert(0, L1);
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
        public void ExportExcelFunction()
        {
            try
            {
                //Need to chk this code
                //DataSet ds = new DataSet();
                //string UserFileName = SessionFacade.LoggedInUserName + "NoteHistory" + ".xls";
                //if (BindNotesCommHistory() != null)
                //{
                //    if (BindNotesCommHistory().Tables[0].Rows.Count > 0)
                //    {
                //       // ds = BindNotesCommHistory(); //GetDatafromXML();
                //        if (SessionFacade.UserTrig != "1")
                //        {
                //            ds.Tables.Add(GetDatafromXML());
                //            ds.Tables[0].Columns.Remove("Row");
                //        }
                //        else
                //        {
                //            string xmlName = "-UserNotesHistory";
                //            ds.Tables.Add(GetDatafromXML(xmlName));
                //            ds.Tables[0].Columns.Remove("Row");
                //        }

                //    }



                //    if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName)))
                //        File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

                //    File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/NoteHistorytemp.xls"), Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

                //    string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName);

                //    bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

                //    //true means Excel File has been written
                //    if (exportToEx == true)
                //    {
                //        if (Request.Browser.Type == "Desktop") //For chrome
                //        {
                //            ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

                //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=NoteHistorySummary&FilePath=" + UserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=NoteHistorySummary&FilePath=" + UserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
                //        }
                //    }
                //    else
                //    {
                //        //litErrorinGrid.Text = "Un able to export the data. Please contact Administartor";
                //        // Response.Write("Data not Exported to Excel File");
                //    }
                //}



            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Notes - Error in Export to Excel" + err.ToString());
            }
        }

        public void ExportToExcel()
        {
            try
            {

                DataSet ds = new DataSet();
                string attachment = "attachment; filename=" + SessionFacade.LoggedInUserName + "NoteHistory.xls";
                Response.Clear();
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.Charset = "";
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                //if (SessionFacade.UserTrig != "1")
                //{
                //    ds.Tables.Add(GetDatafromXML());
                //    ds.Tables[0].Columns.Remove("Row");
                //}
                //else
                //{
                //    string xmlName = "-UserNotesHistory";
                //    ds.Tables.Add(GetDatafromXML(xmlName));
                //    ds.Tables[0].Columns.Remove("Row");
                //}

                //string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";

                //if (File.Exists(Pathname))
                //{

                //    //Reading XML
                //    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                //    ds.ReadXml(fsReadXml);
                //    fsReadXml.Close();
                //}

                //ds = GetDatafromXMLArrangeColumn();
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    //Remove Row Columm
                //if (ds.Tables[0].Columns.Contains("Row"))
                //{
                //    ds.Tables[0].Columns.Remove("Row");
                //}

                // grdNotesHistory.DataSource = ds;
                grdNotesHistory.AllowPaging = false;
                grdNotesHistory.AllowSorting = false;
                if (grdNotesHistory.Rows.Count > 0)
                {
                    grdNotesHistory.RowStyle.CssClass = "RowStyle";
                    grdNotesHistory.AlternatingRowStyle.BackColor = System.Drawing.Color.FromName("#e5e5e5");
                }
                // grdNotesHistory.DataBind();

                //Get the HTML for the control.
                grdNotesHistory.RenderControl(hw);
                //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                Response.Output.Write(tw.ToString());
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Close();

                grdNotesHistory.AllowPaging = true;
                grdNotesHistory.AllowSorting = true;
                grdNotesHistory.DataBind();
                //}
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

        public void returnnoteType()
        {
            Home.Notes.NoteType();
        }

        [System.Web.Services.WebMethod]
        public void ReturnString(string notedate, string notetype, string textnote, string AccountNum = "")
        {

            var context = HttpContext.Current;
            try
            {

                Notes_CommHistory.NotesCommHistory notescom = new Notes_CommHistory.NotesCommHistory();
                cAddNote objactAddNote = new cAddNote();
                objactAddNote.Campaign = SessionFacade.CampaignValue.ToUpper();
                objactAddNote.CreatedBy = SessionFacade.LoggedInUserName.ToUpper();
                objactAddNote.NoteDate = notedate;//SessionFacade.NoteDate;
                objactAddNote.NoteType = notetype;// SessionFacade.NoteType;//objactAddNote.NoteType;
                objactAddNote.Note = textnote.ToUpper();//SessionFacade.TextNote.ToUpper();//objactAddNote.Note;

                if (AccountNum != "")
                    objactAddNote.AccountNum = SessionFacade.AccountNo;
                else
                    objactAddNote.AccountNum = AccountNum;

                //objactAddNote.AccountNum = SessionFacade.AccountNo;
                if (SessionFacade.BuyerCt == "")
                {
                    objactAddNote.ContactNum = "null";
                }
                else
                {
                    objactAddNote.ContactNum = SessionFacade.BuyerCt;
                }

                objactAddNote.NoteDate = objactAddNote.NoteDate;
                objactAddNote.Createdon = DateTime.Now.ToString();
                objactAddNote.AddNote();
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Hello');", true);

            }

            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Nottes & Com History Page - Button Login Click Error", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                throw new ArgumentException("Invalid Data", ex);
            }

        }

        [System.Web.Services.WebMethod]
        public static string reer()
        {
            return "2";
        }

        public static void Show(string message)
        {
            // Cleans the message to allow single quotation marks 
            string cleanMessage = message.Replace("'", "\'");
            string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

            // Gets the executing web page 
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page 
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", script, true /* addScriptTags */);


            }
        }

        [System.Web.Services.WebMethod]
        public static void Validation()
        {
            //if (SessionFacade.VeriEntry != false)
            //{
            //    return true;

            //}
            //else
            //{
            //    return false;
            //}

        }

        protected void btnRefreshNotes_Click(object sender, EventArgs e)
        {

            ShowNotesHistory(true);

            //grdNotesHistory.DataSource = BindNotesCommHistory();
            //grdNotesHistory.DataBind();
            grdDialerHistory.DataSource = BindDialerData();
            grdDialerHistory.DataBind();

        }

        #region NotesComHist

        public DataSet BindNotesCommHistory(bool UserNotes = false)
        {
            try
            {
                DataSet drExisting = new DataSet();
                DataSet drSortDate = new DataSet();
                DataSet drGoldmine = new DataSet();
                DataSet drUnit = new DataSet();
                cNotesCommHistory objNotesCommHistory = new cNotesCommHistory();
                cDialerData objDialerHistory = new cDialerData();
                //NewMasterPage MasterPage = Master as NewMasterPage;

                objNotesCommHistory.CampaignName = ddlCampaignNH.SelectedValue.ToString().Trim();
                objNotesCommHistory.AccountNum = AccountNH;
                objNotesCommHistory.CreatedBy = SessionFacade.LoggedInUserName.ToUpper();
                //objNotesCommHistory.Contact = SessionFacade.BuyerCt;

                if (Request.Cookies["CNo"] != null)
                {
                    SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                    objNotesCommHistory.Contact = Request.Cookies["CNo"].Value;
                }


                objDialerHistory.SearchOrderCampaignName = ddlCampaignNH.SelectedValue.ToString().Trim();
                objDialerHistory.SearchOrderAccount = AccountNH;
                objDialerHistory.SearchOrderContact = SessionFacade.BuyerCt;
                StringBuilder strName = new StringBuilder();

                if (UserNotes == false)
                {
                    string[] TempAccount = new string[6];


                    drExisting = objNotesCommHistory.GetNotesCommHistory();

                    if (objNotesCommHistory.CampaignName == "CL")
                    {
                        drGoldmine = objNotesCommHistory.GetGoldMineNotesCommHistory();

                        // GoldMineDsetCol = drGoldmine.Tables[0].Columns.Count;
                        if (drGoldmine.CheckDataRecords())
                        {
                            if (drExisting.CheckDataRecords())
                            {
                                foreach (DataRow row in drGoldmine.Tables[0].Rows)
                                {
                                    DataRow anyRow = drExisting.Tables[0].NewRow();

                                    int i = 0;
                                    for (i = 0; i <= 10; i++)
                                    {
                                        anyRow[i] = row[i];
                                    }


                                    drExisting.Tables[0].Rows.Add(anyRow);
                                }
                            }
                            else
                            {
                                drExisting = drGoldmine;
                            }
                        }


                        if ((SessionFacade.BuyerCt == null || SessionFacade.BuyerCt == "") && (objDialerHistory.DialerKAMID == null || objDialerHistory.DialerKAMID == "" || objDialerHistory.DialerKAMID.ToUpper() == "DIALER"))
                        {
                            drUnit = objDialerHistory.GetDialerData();
                        }

                        if (drUnit.CheckDataRecords())
                        {
                            if (drExisting.CheckDataRecords())
                            {
                                foreach (DataRow row in drUnit.Tables[0].Rows)
                                {
                                    DataRow unitRow = drExisting.Tables[0].NewRow();

                                    int i = 0;
                                    for (i = 0; i <= 10; i++)
                                    {
                                        unitRow[i] = row[i];
                                    }


                                    drExisting.Tables[0].Rows.Add(unitRow);
                                }
                            }
                            else
                            {
                                drExisting = drUnit;
                            }
                        }
                    }

                    else
                    {
                        if (SessionFacade.CampaignName.In("EMED", "US", "PC", "CA"))
                        {
                            if ((SessionFacade.BuyerCt == null || SessionFacade.BuyerCt == "") && (objDialerHistory.DialerKAMID == null || objDialerHistory.DialerKAMID == "" || objDialerHistory.DialerKAMID.ToUpper() == "DIALER"))
                            {
                                drUnit = objDialerHistory.GetDialerData();
                            }


                            if (drUnit.CheckDataRecords())
                            {
                                if (drExisting.CheckDataRecords())
                                {
                                    foreach (DataRow row in drUnit.Tables[0].Rows)
                                    {
                                        DataRow unitRow = drExisting.Tables[0].NewRow();

                                        int i = 0;
                                        for (i = 0; i <= 11; i++)
                                        {
                                            unitRow[i] = row[i];
                                        }


                                        drExisting.Tables[0].Rows.Add(unitRow);
                                    }
                                }
                                else
                                {
                                    drExisting = drUnit;
                                }
                            }
                        }
                    }
                    TempAccount = SessionFacade.LastAccount;
                    TempAccount[3] = SessionFacade.AccountNo.ToString().Trim();
                    SessionFacade.LastAccount = TempAccount;
                }
                else
                {
                    drExisting = objNotesCommHistory.GetUserNotes();
                }


                return drExisting;

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During:BindNotesCommHistory",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return null;
            }

        }

        protected void NotesCommHistoryPageChanging(object sender, GridViewPageEventArgs e)
        {

            string xmlName;
            string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";
            DataTable dtNotes = new DataTable();

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            dtNotes.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (dtNotes.Rows.Count > 0)
            {
                grdNotesHistory.DataSource = dtNotes;
                grdNotesHistory.PageIndex = e.NewPageIndex;
                grdNotesHistory.DataBind();
            }
        }

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

        protected void grdNotesHistory_Sorting(object sender, GridViewSortEventArgs e)
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
                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";
                    //string Pathname2 = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-UserNotesHistory" + ".xml";

                    if (File.Exists(Pathname))
                    {
                        DataTable dtNotes = new DataTable();
                        // DataTable dt = GetDatafromXML();

                        //Reading XML
                        System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                        dtNotes.ReadXml(fsReadXml);
                        fsReadXml.Close();

                        if (dtNotes.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dtNotes);

                            dv.Sort = sortExpression + " " + direction;

                            dtNotes = dv.ToTable();

                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            dtNotes.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            //System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname2);
                            //dt.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
                            //xmlSW2.Close();

                            grdNotesHistory.DataSource = dtNotes;
                            grdNotesHistory.DataBind();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        public void ShowNotesHistory(bool UserNotes = false)
        {
            try
            {
                DataSet ReArrangeDs = new DataSet();
                cArrangeDataSet ADS = new cArrangeDataSet();
                DataSet ds = new DataSet();
                DataView dtView = new DataView();
                DataSet dsFilter = new DataSet();

                ADS.CampaignName = SessionFacade.CampaignValue;
                ADS.UserName = SessionFacade.LoggedInUserName;
                ADS.Listview = "lvwNotesData";

                string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";

                if (UserNotes == false)
                {
                    if (TabValue == 1)
                    {
                        ds = BindNotesCommHistory();

                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        if (ds.CheckDataRecords())
                        {
                            //Check If Contact Include in Search
                            if (Request.Cookies["CNo"] != null)
                            {
                                SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                                ds = new DataSet();
                                ds.Tables.Add(GetDatafromXML());
                            }

                            dtView = ds.Tables[0].DefaultView;
                            dtView.Sort = "DATE  DESC";
                            dsFilter.Tables.Add(dtView.ToTable());

                            ReArrangeDs = ADS.RearangeDS(dsFilter);
                        }


                    }
                    else if (TabValue == 2)
                    {
                        ds.Tables.Add(GetDatafromXML());

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            dtView = ds.Tables[0].DefaultView;
                            dtView.Sort = "DATE  DESC";
                            dsFilter.Tables.Add(dtView.ToTable());

                            ReArrangeDs = ADS.RearangeDS(dsFilter);
                        }


                    }


                    grdNotesHistory.Visible = true;
                    pnlGridIndex.Visible = true;



                    if (ReArrangeDs.Tables.Count > 0 && ReArrangeDs.Tables[0].Rows.Count > 0)
                    {

                        grdNotesHistory.DataSource = ReArrangeDs;
                        grdNotesHistory.DataBind();
                        grdNotesHistory.Visible = true;
                        pnlGridIndex.Visible = true;

                    }
                    else
                    {
                        grdNotesHistory.DataSource = null;
                        grdNotesHistory.DataBind();
                        grdNotesHistory.Visible = false;
                        pnlGridIndex.Visible = false;
                    }


                    SessionFacade.UserTrig = "0";
                }
                else
                {
                    ds = BindNotesCommHistory(true);


                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {

                            ReArrangeDs = ADS.RearangeDS(ds);

                        }
                    }



                    SessionFacade.UserTrig = "1";

                    grdNotesHistory.DataSource = null;


                    if (ReArrangeDs.Tables.Count > 0 && ReArrangeDs.Tables[0].Rows.Count > 0)
                    {
                        grdNotesHistory.DataSource = ReArrangeDs;
                        grdNotesHistory.DataBind();
                        grdNotesHistory.Visible = true;
                        pnlGridIndex.Visible = true;
                    }
                    else
                    {
                        grdNotesHistory.DataSource = null;
                        grdNotesHistory.DataBind();
                        grdNotesHistory.Visible = false;
                        pnlGridIndex.Visible = false;
                    }


                }
            }
            catch (Exception e)
            {
                new ArgumentNullException();
                grdNotesHistory.Visible = false;
                pnlGridIndex.Visible = false;
            }
        }

        public DataTable GetDatafromXML()
        {
            try
            {

                //DataRow rowOrders;
                DataSet ds = new DataSet();
                string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();

                if (File.Exists(Pathname))
                {
                    string Query;

                    Query = "1 = 1";

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
                        Query = Query + " and [CONTACT NUMBER] = " + Request.Cookies["CNo"].Value;
                    }


                    //Search by Date
                    if (ByDate.Checked == true)
                    {
                        if ((txtStartDate.Text != string.Empty) && (txtEndDate.Text != string.Empty))
                        {
                            Query = Query + " and [DATE] >= '" + txtStartDate.Text + "' and [DATE] <=  '" +
                            txtEndDate.Text + "'";

                        }
                    }

                    if ((ddlNoteType.Text.Trim().ToUpper() != "ALL") && (ddlNoteType.Text.Trim().ToUpper() != ""))
                    {
                        Query = Query + "and [NOTE TYPE] ='" + ddlNoteType.Text.Trim().ToUpper() + "'";
                    }

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }

                return dtTemp;
            }
            catch (Exception ex)
            {
                new ArgumentNullException();
                return null;
            }


        }

        #endregion NotesComHist

        #region DialerData

        public DataSet BindDialerData()
        {
            //NewMasterPage MasterPage = Master as NewMasterPage;
            DataSet drExisting = new DataSet();
            cDialerData objDialerData = new cDialerData();
            string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";

            objDialerData.SearchOrderCampaignName = ddlCampaignNH.SelectedValue.ToString().Trim();
            objDialerData.SearchOrderAccount = AccountNH;

            drExisting = objDialerData.GetDialerData();

            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            drExisting.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();
            SessionFacade.UserTrig = "1";
            if (drExisting.Tables[0].Rows.Count > 0)
            {
                grdDialerHistory.Visible = true;
                PnlDialerHistory.Style.Add("height", "250");
                Panel3.Visible = true;
                lblNoDialerData.Visible = false;
            }
            else
            {
                lblNoDialerData.Visible = true;
                Panel3.Visible = false;

            }

            return drExisting;
        }

        public DataTable GetXMLDialerData(string xmlName = "-DialerData")
        {
            try
            {
                DataSet ds = new DataSet();
                string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";
                DataTable dtTemp = new DataTable();

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                dtTemp = ds.Tables[0].Clone();

                dtTemp = ds.Tables[0];
                return dtTemp;
            }
            catch (Exception ex)
            {
                new ArgumentNullException();
                return null;
            }


        }

        protected void DialerDataPageChanging(object sender, GridViewPageEventArgs e)
        {
            grdDialerHistory.DataSource = GetXMLDialerData();
            grdDialerHistory.PageIndex = e.NewPageIndex;
            grdDialerHistory.DataBind();
        }

        public void ShowDialerData(bool UserNotes = false)
        {

            try
            {
                if (SessionFacade.CampaignName != "UK")
                {
                    DataSet ds = new DataSet();
                    if (UserNotes == false)
                    {
                        string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";
                        SessionFacade.LastAccount[3] = null;

                        //if same account number = last account number get data from xml
                        if (SessionFacade.LastAccount[3] == SessionFacade.AccountNo.ToString().Trim())
                        {
                            DataTable dialertable = GetXMLDialerData();
                            grdDialerHistory.DataSource = dialertable;
                            grdDialerHistory.DataBind();
                        }
                        else
                        {

                            //if new account number, get data from database and write to xml

                            ds = BindDialerData();
                            DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                            dc.AutoIncrement = true;
                            dc.AutoIncrementSeed = 1;
                            dc.AutoIncrementStep = 1;
                            ds.Tables[0].Columns.Add(dc);

                            DataSet ReArrangeDs = new DataSet();
                            ReArrangeDs = ds;

                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                if (ReArrangeDs != null)
                                {
                                    cArrangeDataSet ADS = new cArrangeDataSet();
                                    ADS.CampaignName = SessionFacade.CampaignValue;
                                    ADS.UserName = SessionFacade.LoggedInUserName;
                                    ADS.Listview = "lvwDialerData";

                                    int IsReorder = ADS.ColumnReorderCount();
                                    if (IsReorder > 0)
                                    {
                                        ReArrangeDs = ADS.RearangeDS(ReArrangeDs);

                                    }
                                }

                                ds = ReArrangeDs;
                                grdDialerHistory.Visible = true;
                                Panel3.Visible = true;
                                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                                xmlSW.Close();
                                DataTable dialertable2 = GetXMLDialerData();
                                grdDialerHistory.DataSource = dialertable2;
                                grdDialerHistory.DataBind();
                            }
                            else
                            {
                                //Writing XML
                                //grdDialerHistory.Visible = false;
                                Panel3.Visible = false;
                                grdDialerHistory.DataSource = null;
                                grdDialerHistory.DataBind();
                            }

                        }
                        SessionFacade.UserTrig = "0";
                    }
                    else
                    {
                        string xmlName = "-DialerData";
                        string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";
                        ds = BindDialerData();
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();
                        SessionFacade.UserTrig = "1";
                        DataTable dialertable3 = GetXMLDialerData();
                        grdDialerHistory.DataSource = dialertable3;
                        grdDialerHistory.DataBind();

                    }
                }

            }
            catch (Exception ex)
            {
                new ArgumentNullException();
                grdDialerHistory.Visible = false;
                Panel3.Visible = false;
            }

        }


        protected SortDirection GridViewSortDirectionDialer
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }


        protected void grdDialerHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirectionDialer == SortDirection.Ascending)
            {

                GridViewSortDirectionDialer = SortDirection.Descending;
                SortGridDialerData(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirectionDialer = SortDirection.Ascending;
                SortGridDialerData(sortExpression, "ASC");


            }
        }

        private void SortGridDialerData(string sortExpression, string direction)
        {
            try
            {

                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";
                    if (File.Exists(Pathname))
                    {

                        DataTable dt = GetXMLDialerData();
                        DataView dv = new DataView(dt);

                        dv.Sort = sortExpression + " " + direction;
                        dt = dv.ToTable();

                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        dt.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();


                        grdDialerHistory.DataSource = dv;
                        grdDialerHistory.DataBind();

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion DialerData

        #region Read From XML Notes And load to pop

        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLDetails(string RowNum)
        {
            DataSet ds = new DataSet();

            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";
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


                    if (RowNum != null)
                    {

                        Query = Query + " and [Row] = '" + RowNum + "' ";

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

        #region Read From XML Notes And load to pop

        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLDialerDetails(string RowNum)
        {
            DataSet ds = new DataSet();

            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";
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


                    if (RowNum != null)
                    {

                        Query = Query + " and [Row] = '" + RowNum + "' ";

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

        protected void ddlNoteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionFacade.UserTrig = "0";
            TabValue = 2;
            LoadNoteDetails();
            if (grdNotesHistory.Rows.Count > 0)
            {

                grdNotesHistory.UseAccessibleHeader = true;
                grdNotesHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        protected void StatusSearchDateRange(bool value)
        {
            imgstartCal.Enabled = value;
            imgEndCal.Enabled = value;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtEndDate.Enabled = value;
            txtStartDate.Enabled = value;
        }

        protected void btnOkay_Click(object sender, EventArgs e)
        {
            //Response.Write(@"<script language='javascript'>alert('The following errors have occurred: \n" + 'a' + " .');</script>");
            cAddNote objactAddNote = new cAddNote();
            objactAddNote.Campaign = SessionFacade.CampaignValue.ToUpper();
            objactAddNote.CreatedBy = SessionFacade.LoggedInUserName.ToUpper();
            //objactAddNote.NoteType = ddlAddNoteType.SelectedValue;
            //objactAddNote.Note = txtAddNote.Text.Replace("'","''");

            objactAddNote.AccountNum = SessionFacade.AccountNo;
            if (SessionFacade.BuyerCt == "")
            {
                objactAddNote.ContactNum = "null";
            }
            else
            {
                objactAddNote.ContactNum = SessionFacade.BuyerCt;
            }

            //objactAddNote.NoteDate = txtNoteDate.Text;
            objactAddNote.Createdon = DateTime.Now.ToString();

            bool test = objactAddNote.AddNote();

        }
        protected void ByDate_CheckedChanged(object sender, EventArgs e)
        {
            //Need to chk this code

            if (ByDate.Checked == true)
            {
                StatusSearchDateRange(true);

                //ShowNotesHistory();  
                //grdNotesHistory.DataSource = GetDatafromXML();
                //if (txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "")
                //{
                //    grdNotesHistory.DataSource = BindNotesCommHistory();
                //    grdNotesHistory.DataBind();
                //}

            }
            else
            {

                StatusSearchDateRange(false);
                ddlNoteType.SelectedIndex = 0;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                ShowNotesHistory();
                //ShowNotesHistory();
                //grdNotesHistory.DataSource = GetDatafromXML();

                //grdNotesHistory.DataSource = BindNotesCommHistory();
                //grdNotesHistory.DataBind();
                //ShowNotesHistory();
            }

            if (grdNotesHistory.Rows.Count > 0)
            {

                grdNotesHistory.UseAccessibleHeader = true;
                grdNotesHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }


        protected void btnAllNotes_Click(object sender, EventArgs e)
        {
            TextBox txtAccountNumber = Master.FindControl("txtAccountNumber") as TextBox;
            Label lblAccountName = Master.FindControl("lblAccountName") as Label;
            ByDate.Checked = false;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtAccountNumber.Text = "";
            lblAccountName.Text = "";
            SessionFacade.AccountNo = "";
            ShowNotesHistory(true);
            if (grdNotesHistory.Rows.Count > 0)
            {

                grdNotesHistory.UseAccessibleHeader = true;
                grdNotesHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        protected void btnAdfsfdNote_Click(object sender, EventArgs e)
        {
            // Panel67.Visible = true;
            // ModalPopupExtender2.Show();
            //Response.Write(@"<script language='javascript'>alert('Update is successful.')</script>");
        }

        #region Export to Excel Function


        protected void btn_Export2ExcelClick(object sender, EventArgs e)
        {
            //Jonmar, Please read the Notes in orde rhistory page 
            ExportToExcel();

        }

        //public void ExportExcelFunction()
        //{

        //    //btnOk.Visible = true;
        //   // btnUpdate.Visible = false;
        //    OpenPopup();
        //}

        private void OpenPopup()
        {
            // Panel66.Visible = true;
            // ModalPopupExtender1.Show();
        }

        public static void exportToExcel()
        {

        }

        //[System.Web.Services.WebMethod]
        //public static  void export2(bool notes,bool dialer,string camp)
        //{
        //    try
        //    {
        //        string PageName = string.Empty;
        //        string tempPageName = string.Empty;
        //        DataSet ds = new DataSet();
        //        DataSet drExisting=null;
        //        string DestinationUserFileName = string.Empty;
        //        if (notes == true && dialer == false)
        //        {
        //            DestinationUserFileName = SessionFacade.LoggedInUserName + "NoteHistory.xls";
        //            tempPageName = "NoteHistorytemp.xls";
        //            PageName = "NoteHistorySummary";


        //           // DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;

        //            cNotesCommHistory objNotesCommHistory = new cNotesCommHistory();

        //            //Campaign
        //            objNotesCommHistory.CampaignName = camp.ToString().Trim();//ddlTemp.SelectedValue.ToString().Trim();
        //            objNotesCommHistory.AccountNum = SessionFacade.AccountNo.ToString().Trim();
        //            objNotesCommHistory.CreatedBy = SessionFacade.LoggedInUserName.ToUpper();
        //            objNotesCommHistory.Contact = SessionFacade.BuyerCt;


        //                string[] TempAccount = new string[6];

        //                drExisting = objNotesCommHistory.GetNotesCommHistory();
        //                TempAccount = SessionFacade.LastAccount;
        //                TempAccount[3] = SessionFacade.AccountNo.ToString().Trim();
        //                SessionFacade.LastAccount = TempAccount;



        //           // ds = BindNotesCommHistory();
        //                ds = drExisting;


        //        }
        //        if (notes == false && dialer == true)
        //        {
        //            cDialerData objDialerData = new cDialerData();
        //            objDialerData.SearchOrderCampaignName = camp.ToString().Trim();
        //            DestinationUserFileName = SessionFacade.LoggedInUserName + "DialerData.xls";
        //           // ds = BindDialerData();

        //            drExisting = objDialerData.GetDialerData();
        //            ds = drExisting;
        //            tempPageName = "DialerDatatemp.xls";
        //            PageName = "DialerDataSummary";
        //        }

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName)))
        //                File.Delete(System.Web.HttpContext.Current.Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName));

        //            File.Copy(System.Web.HttpContext.Current.Server.MapPath("../../App_Data/Export2ExcelTemplate/" + tempPageName), System.Web.HttpContext.Current.Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName));
        //        }

        //        string filename = System.Web.HttpContext.Current.Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName);
        //        bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

        //        //ClosePopup(PageName, DestinationUserFileName);
        //      //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=" + PageName + "&FilePath=" + DestinationUserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
        //        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "callme", "closeChildWindows()", true);
        //       // ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Hello');", true);

        //    }
        //    catch (Exception err)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogMessage("Notes and ComHist - Error in Export to Excel" + err.ToString());
        //    }
        //}


        //public static void export2(bool notes, bool dialer, string camp, string contnum, string startdate, string enddate, bool ischeck, string notetype)
        [System.Web.Services.WebMethod]
        public static void export2(bool notes, bool dialer, string camp, string contnum, string startdate, string enddate, bool ischeck, string notetype)
        {
            try
            {
                string xmlName = "-NotesHistory";
                string PageName = string.Empty;
                string tempPageName = string.Empty;
                DataSet ds = new DataSet();
                DataSet drExisting = null;
                string DestinationUserFileName = string.Empty;
                DataTable dtTemp = new DataTable();
                string Query;
                DataRow[] results;
                //---Reading XML File----------
                string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";

                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();
                ds.Tables[0].Columns.Remove("Row");
                dtTemp = ds.Tables[0].Clone();

                Query = "1 = 1";

                if (contnum != "")
                {
                    SessionFacade.BuyerCt = contnum;
                    Query = Query + " and [CONTACT NUMBER] = " + contnum;
                }


                //Search by Date---
                if (ischeck == true)
                {
                    if ((startdate != "") && (enddate != ""))
                    {
                        //Query = Query + " and Created_on >= '" + txtStartDate.Text + "' and Created_on <=  '" +
                        //txtEndDate.Text + "'";
                        Query = Query + " and [DATE] >= '" + startdate + "' and [DATE] <=  '" +
                           enddate + "'";
                    }
                }
                if ((notetype.ToString().Trim() != "All") && (notetype.ToString().Trim() != ""))
                {
                    Query = Query + " and [NOTE TYPE] ='" + notetype.ToString().ToUpper().Trim() + "'";
                }

                results = ds.Tables[0].Select(Query);

                foreach (DataRow dr in results)
                    dtTemp.ImportRow(dr);

                ds.Tables.Clear();

                ds.Tables.Add(dtTemp);
                //----------------------------

                if (notes == true && dialer == false)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "NoteHistory.xls";
                    tempPageName = "NoteHistorytemp.xls";
                    PageName = "NoteHistorySummary";


                    cNotesCommHistory objNotesCommHistory = new cNotesCommHistory();

                }
                if (notes == false && dialer == true)
                {
                    cDialerData objDialerData = new cDialerData();
                    objDialerData.SearchOrderCampaignName = camp.ToString().Trim();
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "DialerData.xls";
                    // ds = BindDialerData();

                    drExisting = objDialerData.GetDialerData();
                    ds = drExisting;
                    tempPageName = "DialerDatatemp.xls";
                    PageName = "DialerDataSummary";
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName)))
                        File.Delete(System.Web.HttpContext.Current.Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName));

                    File.Copy(System.Web.HttpContext.Current.Server.MapPath("../../App_Data/Export2ExcelTemplate/" + tempPageName), System.Web.HttpContext.Current.Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName));
                }

                string filename = System.Web.HttpContext.Current.Server.MapPath("../../App_Data/ExportExcelFiles/" + DestinationUserFileName);
                bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);



            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Notes and ComHist - Error in Export to Excel" + err.ToString());
            }
        }

        protected void btnOk_Click1(object sender, EventArgs e)
        {
            try
            {
                string PageName = string.Empty;
                string tempPageName = string.Empty;
                DataSet ds = new DataSet();
                string DestinationUserFileName = string.Empty;
                if (rdoNotesHistory.Checked == true && rdoDialerData2.Checked == false)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "NoteHistory.xls";
                    tempPageName = "NoteHistorytemp.xls";
                    PageName = "NoteHistorySummary";
                    ds = BindNotesCommHistory();

                }
                if (rdoNotesHistory.Checked == false && rdoDialerData2.Checked == true)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "DialerData.xls";
                    ds = BindDialerData();
                    tempPageName = "DialerDatatemp.xls";
                    PageName = "DialerDataSummary";
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


            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Notes and ComHist - Error in Export to Excel" + err.ToString());
            }
        }
        private void ClosePopup(string vPageName, string vFilePath)
        {
            //Panel66.Visible = false;
            //ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=" + vPageName + "&FilePath=" + vFilePath + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);

        }

        #endregion

        [System.Web.Services.WebMethod]
        public static void AddNote2(string notedate, string notetype, string textnote)
        {
            var context = HttpContext.Current;
            try
            {

                Notes_CommHistory.NotesCommHistory notescom = new Notes_CommHistory.NotesCommHistory();
                cAddNote objactAddNote = new cAddNote();
                objactAddNote.Campaign = SessionFacade.CampaignValue.ToUpper();
                objactAddNote.CreatedBy = SessionFacade.LoggedInUserName.ToUpper();
                objactAddNote.NoteDate = notedate;//SessionFacade.NoteDate;
                objactAddNote.NoteType = notetype;// SessionFacade.NoteType;//objactAddNote.NoteType;
                objactAddNote.Note = textnote;//SessionFacade.TextNote.ToUpper();//objactAddNote.Note;


                objactAddNote.AccountNum = SessionFacade.AccountNo;
                if (SessionFacade.BuyerCt == "")
                {
                    objactAddNote.ContactNum = "null";
                }
                else
                {
                    objactAddNote.ContactNum = SessionFacade.BuyerCt;
                }

                objactAddNote.NoteDate = objactAddNote.NoteDate;
                objactAddNote.Createdon = DateTime.Now.ToString();
                objactAddNote.AddNote();

                DataSet ds = new DataSet();

                string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";

                if (File.Exists(Pathname))
                {
                    //Reading XML
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    ds.ReadXml(fsReadXml);
                    fsReadXml.Close();

                    if (ds.Tables.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        if (CheckTableColumn(dr, "DATE"))
                        {
                            if(ds.Tables[0].Columns["DATE"].DataType.Name == "DateTime")
                                dr["DATE"] = System.DateTime.Now.ToShortDateString();
                        }

                        if (CheckTableColumn(dr, "CREATED BY"))
                            dr["CREATED BY"] = SessionFacade.LoggedInUserName.ToUpper();

                        if (CheckTableColumn(dr, "NOTE TYPE"))
                            dr["NOTE TYPE"] = notetype.ToUpper().Trim();

                        if (CheckTableColumn(dr, "NOTES"))
                            dr["NOTES"] = textnote;

                        if (CheckTableColumn(dr, "SCHEDULED DATE"))
                            dr["SCHEDULED DATE"] = notedate;

                        if (CheckTableColumn(dr, "ACCOUNT NUMBER"))
                        {
                            int temp;
                            if (int.TryParse(SessionFacade.AccountNo, out temp))
                                dr["ACCOUNT NUMBER"] = SessionFacade.AccountNo;
                        }

                        if (CheckTableColumn(dr, "CONTACT NAME"))
                            dr["CONTACT NAME"] = "";

                        Double tempd;
                        if (Double.TryParse(SessionFacade.BuyerCt.Trim(), out tempd) == true)
                        {
                            if (CheckTableColumn(dr, "CONTACT NUMBER"))
                                dr["CONTACT NUMBER"] = Convert.ToDouble(SessionFacade.BuyerCt.Trim());
                        }
                        else
                        {
                            if (CheckTableColumn(dr, "CONTACT NUMBER"))
                                dr["CONTACT NUMBER"] = DBNull.Value;
                        }

                        if (CheckTableColumn(dr, "DISPOSITION CODE"))
                            dr["DISPOSITION CODE"] = "";

                        if (CheckTableColumn(dr, "DISPOSITION DESCRIPTION"))
                            dr["DISPOSITION DESCRIPTION"] = "";

                        if (CheckTableColumn(dr, "AGENT NAME"))
                            dr["AGENT NAME"] = "";

                        if (CheckTableColumn(dr, "PHONE NUMBER"))
                            dr["PHONE NUMBER"] = "";

                        ds.Tables[0].Rows.Add(dr);
                    }
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                }


                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Hello');", true);

            }

            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Nottes & Com History Page", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                throw new ArgumentException("Invalid Data", ex);
            }
        }

        public static bool CheckTableColumn(DataRow dr, string ColumnName)
        {
            if (dr.Table.Columns.Contains(ColumnName))
                return true;
            else
                return false;
        }

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {

            try
            {
                TextBox txtAccountNumber = Master.FindControl("txtAccountNumber") as TextBox;
                ReturnString(txtNoteStartDate.Text, NoteTypes.Text, AddNote.Text, txtAccountNumber.Text);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "Information()", true);
                AddNote.Text = "";
                txtNoteStartDate.Text = "";
                NoteTypes.Text = "Please Select";

                //ModalPopupExtender2.Hide();
            }
            catch (Exception ex)
            {
                new ArgumentNullException();

            }


        }

        protected void CancelExport_Click(object sender, EventArgs e)
        {
            // ModalPopupExtender1.Hide();
            // ModalPopupExtender2.Hide();
        }

        protected void grdNotesHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                bool varColumnExist = false;
                int columnIndex;
                string[] list = { "DATE", "ACCOUNT NUMBER" };

                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
                {

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

                            //convert values to $ currency
                            DateTime temp;
                            if (DateTime.TryParse(((DataRowView)e.Row.DataItem)[list[i]].ToString(), out temp) == true)
                                e.Row.Cells[columnIndex].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)[list[i]]).ToString("MM/dd/yyyy");




                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ArgumentNullException();
            }
        }

        protected void grdDialerHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdNotesHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdDialerHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                bool varColumnExist = false;
                int columnIndex;
                string[] list = { "CONTACT DATE" };

                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
                {

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

                            //convert values to $ currency
                            DateTime temp;
                            if (DateTime.TryParse(((DataRowView)e.Row.DataItem)[list[i]].ToString(), out temp) == true)
                                e.Row.Cells[columnIndex].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)[list[i]]).ToString("MM/dd/yyyy");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ArgumentNullException();
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string PageName = string.Empty;
                string tempPageName = string.Empty;
                DataSet ds = new DataSet();
                string DestinationUserFileName = string.Empty;
                if (rdoNotesHistory.Checked == true && rdoDialerData2.Checked == false)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "NoteHistory.xls";
                    tempPageName = "NoteHistorytemp.xls";
                    PageName = "NoteHistorySummary";
                    ds = BindNotesCommHistory();

                    //Note: If you get the error of Number of query values and destination fields are not the same 

                    // UnComment the following all lines

                    //In test me you will get all the columns name of dataset

                    //Open the excel and see what all columns name should be there and now you have to map according to that

                    //string testme = "";
                    //foreach (DataColumn dc in ds.Tables[0].Columns)
                    //{
                    //    testme += dc.ToString() + ",";
                    //}
                    //string me = "";


                    // Use this code, give the proper column name and rremove from the dataset . Now it will work

                    //if (ds.Tables[0].Rows.Count > 0)
                    //{

                    //    ds.Tables[0].Columns.Remove("Quote_Doc_createdon");
                    //    ds.Tables[0].Columns.Remove("Quote_Doc_Create_Time");

                    //}
                    //ds.AcceptChanges();

                }
                if (rdoNotesHistory.Checked == false && rdoDialerData2.Checked == true)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "DialerData.xls";
                    ds = BindDialerData();
                    tempPageName = "DialerDatatemp.xls";
                    PageName = "DialerDataSummary";
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


            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Notes and ComHist - Error in Export to Excel" + err.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender1.Hide();
        }

        protected void BtnNoteCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
