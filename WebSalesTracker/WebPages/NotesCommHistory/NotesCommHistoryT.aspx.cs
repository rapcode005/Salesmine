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

namespace WebSalesMine.WebPages.Notes_CommHistory
{
      
    public partial class NotesCommHistoryT : System.Web.UI.Page
    {

        public string userRule = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            TextBox txtTempContact = Master.FindControl("txtContactNumber") as TextBox;
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            string tempCampaign = ddlTemp.SelectedValue.ToString().Trim();
            ImageSelectContact.Visible = false;
            userRule = SessionFacade.UserRole.Trim();
            if (!IsPostBack)
            {
                //tempCampaign = ddlTemp.SelectedValue.ToString().Trim();
                StatusSearchDateRange(false);
                //////////ByDate.Checked = true;
                ////////txtStartDate.Text = DateTime.Now.AddMonths(-2).Date.ToString("MM/dd/yyyy");
                ////////txtEndDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
                ////////imgstartCal.Enabled = true;
                string acTUser = SessionFacade.LoggedInUserName;
                //btnAllNotes2.Text = "Show All " + acTUser.ToUpper() + "'s Notes";
                btnAllNotes2.Text = "Show All My Notes";
                //grdDialerHistory.DataSource = BindDialerData();
                //grdDialerHistory.DataBind();
               ///// ShowDialerData();
                txtNoteStartDate.Attributes.Add("readonly", "true");
                txtStartDate.Attributes.Add("readonly", "true");
                txtEndDate.Attributes.Add("readonly", "true");
                ShowNotesHistory();
               
            }
            else
            {
                //ByDate.Checked = true;
              //  txtStartDate.Text = "";
                //txtEndDate.Text = "";
                //imgEndCal.Enabled = true;


                //DropDownList tempcampaign2 = Master.FindControl("ddlCampaign") as DropDownList;
                //if (txtTemp.Text.ToString().Trim() != "" && tempCampaign != "") //&&  Request.Cookies["CNo"] != null)
                //{
                //    ShowNotesHistory();
                //    ShowDialerData();
             //  }

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

                    ShowNotesHistory();
                    ////ShowDialerData();
                    
                }
             //   else if (ByDate.Checked == true)
                else
               {
                    SessionFacade.BuyerCt = null;
                 
                    ShowNotesHistory();
                   //// ShowDialerData();
                    lnkContactSelected.Text = "";
                    ImageSelectContact.Visible = false;
                   
               }
                //else if (ByDate.Checked == false)
                //{
                    //StatusSearchDateRange(false);
                    
                  //  ShowNotesHistory();
                //}


                if (ddlTemp.SelectedValue != SessionFacade.CampaignValue)
                {
                    ShowNotesHistory();
                    //////ShowDialerData();
                    //for (int x = 0; x < grdNotesHistory.HeaderRow.Cells.Count; x++)
                    //{
                    //    if (grdNotesHistory.Columns[x].HeaderText == "Row")
                    //    {
                    //        //grdNotesHistory.Columns[2].Visible = false;
                    //        grdNotesHistory.Columns[x].Visible = false;
                    //    }
                    //}
                }
                //else
                //{
                //    grdNotesHistory.DataSource = GetDatafromXML();
                //    grdNotesHistory.DataBind();
                //}


            }

          

        }

        public void ExportExcelFunction()
        {
            try
            {
                DataSet ds = new DataSet();
                string UserFileName = SessionFacade.LoggedInUserName + "NoteHistory" + ".xls";
                if (GetDatafromXML() != null)
                {
                    if (GetDatafromXML().Rows.Count > 0)
                    {
                        // ds = BindNotesCommHistory(); //GetDatafromXML();
                        ds.Tables.Add(GetDatafromXML());
                        //ds.Tables[0].Columns.Remove("Row");

                    }



                    if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName)))
                        File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

                    File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/NoteHistorytemp.xls"), Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

                    string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName);

                    bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

                    //true means Excel File has been written
                    if (exportToEx == true)
                    {
                        if (Request.Browser.Type == "Desktop") //For chrome
                        {
                            ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=NoteHistorySummary&FilePath=" + UserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=NoteHistorySummary&FilePath=" + UserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
                        }
                    }
                    else
                    {
                        //litErrorinGrid.Text = "Un able to export the data. Please contact Administartor";
                        // Response.Write("Data not Exported to Excel File");
                    }
                }



            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Notes - Error in Export to Excel" + err.ToString());
            }
        }

        public void returnnoteType()
        {
            Home.Notes.NoteType();
        }

        [System.Web.Services.WebMethod]
        public void ReturnString(string notedate, string notetype, string textnote)
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
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            DataSet drExisting;
            cNotesCommHistory objNotesCommHistory = new cNotesCommHistory();

            //Campaign
            objNotesCommHistory.CampaignName = ddlTemp.SelectedValue.ToString().Trim();
           // objNotesCommHistory.AccountNum = SessionFacade.AccountNo.ToString().Trim();
            objNotesCommHistory.CreatedBy = SessionFacade.LoggedInUserName.ToUpper();
            objNotesCommHistory.Contact = SessionFacade.BuyerCt;
            objNotesCommHistory.NotesKAMID = SessionFacade.KamId;
            if (UserNotes == false)
            {
                string[] TempAccount = new string[6];




                drExisting = objNotesCommHistory.GetNotesCommHistoryT();
           
                TempAccount = SessionFacade.LastAccount;
                TempAccount[3] = SessionFacade.AccountNo.ToString().Trim();
                SessionFacade.LastAccount = TempAccount;

                //drExisting = objNotesCommHistory.GetUserNotes();
               
            }
            else
            {
                DataSet ds = objNotesCommHistory.GetUserNotes();
                DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dc.AutoIncrementStep = 1;
                ds.Tables[0].Columns.Add(dc);

                drExisting = ds;
                //drExisting = objNotesCommHistory.GetUserNotes();
                

            }

            return drExisting;
        }


        protected void NotesCommHistoryPageChanging(object sender, GridViewPageEventArgs e)
        {
            string xmlName;
            string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";

            if (SessionFacade.UserTrig == "1")
            {
                xmlName = "-UserNotesHistory";
            }
            else
            {
                xmlName = "-NotesHistory";
            }

            //DataTable dt = GetDatafromXML(xmlName);
            //System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);

            //dt.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            //xmlSW.Close();
            //DataTable dt2 = GetDatafromXML();

            //DataView dv = new DataView(dt2);
            grdNotesHistory.DataSource = GetDatafromXML(xmlName);
            grdNotesHistory.PageIndex = e.NewPageIndex;
            grdNotesHistory.DataBind();
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
                    string Pathname2 = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-UserNotesHistory" + ".xml";

                    if (File.Exists(Pathname))
                    {
                        DataTable dt = GetDatafromXML();
                        DataView dv = new DataView(dt);


                        dv.Sort = sortExpression + " " + direction;

                        dt = dv.ToTable();

                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        dt.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname2);
                        dt.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
                        xmlSW2.Close();

                        grdNotesHistory.DataSource = dv;
                        grdNotesHistory.DataBind();

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
                TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
                DataSet ds = new DataSet();
                if (UserNotes == false)
                {

                    ////DataRow rowOrders;

                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";
                    string Pathname2 = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-UserNotesHistory" + ".xml";
                    DataSet ReArrangeDs = new DataSet();
                    cArrangeDataSet ADS = new cArrangeDataSet();
                    ADS.CampaignName = SessionFacade.CampaignValue;
                    ADS.UserName = SessionFacade.LoggedInUserName;
                    ADS.Listview = "lvwNotesDataTer";

                    //SessionFacade.LastAccount[3] = null;
                    //if same account number = last account number get data from xml
                    if (SessionFacade.LastAccount[3] == SessionFacade.AccountNo.ToString().Trim())
                    {
                        //string xmlName = "-UserNotesHistory";
                        string xmlName = "-NotesHistory";
                        ReArrangeDs.Tables.Add(GetDatafromXML(xmlName));
                        if (GetDatafromXML(xmlName).Rows.Count > 0)
                        {
                            

                            int IsReorder = ADS.ColumnReorderCount();
                            if (IsReorder > 0)
                            {
                                ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                            }
                            DataTable notetable = ReArrangeDs.Tables[0];
                            grdNotesHistory.DataSource = null;
                            
                            grdNotesHistory.DataSource = notetable;
                            grdNotesHistory.DataBind();
                            grdNotesHistory.Visible = true;
                            pnlGridIndex.Visible = true;
                        }
                        else
                        {
                            //grdNotesHistory.Visible = false;
                            grdNotesHistory.DataSource = null;
                            grdNotesHistory.DataBind();
                            pnlGridIndex.Visible = false;
                        }
                    }
                    else
                    {

                        //if new account number, get data from database and write to xml


                        ds = BindNotesCommHistory();
                        DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                        dc.AutoIncrement = true;
                        dc.AutoIncrementSeed = 1;
                        dc.AutoIncrementStep = 1;
                        ds.Tables[0].Columns.Add(dc);

                        
                        ReArrangeDs = ds;

                        if (ds.Tables[0].Rows.Count > 0)
                        {


                            if (ReArrangeDs != null)
                            {
                                int IsReorder = ADS.ColumnReorderCount();
                                if (IsReorder > 0)
                                {
                                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                                }

                               
                            }


                            ds = ReArrangeDs;
                            grdNotesHistory.Visible = true;
                            pnlGridIndex.Visible = true;
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            //DataTable testonly = GetDatafromXML();
                           
                            System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname2);
                            ds.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
                            xmlSW2.Close();

                            if (GetDatafromXML().Rows.Count > 0)
                            {
                                DataTable notetable2 = GetDatafromXML();
                                //notetable2.Columns.Remove("Row");
                                grdNotesHistory.DataSource = null;
                                
                                //grdNotesHistory.DataSource = GetDatafromXML();
                                grdNotesHistory.DataSource = notetable2;
                                grdNotesHistory.DataBind();
                                pnlGridIndex.Visible = true;
                            }
                            else
                            {
                                //Writing XML
                                grdNotesHistory.Visible = false;
                                pnlGridIndex.Visible = false;
                            }
                        }

                        else
                        {
                            grdNotesHistory.DataSource = null;
                            grdNotesHistory.DataBind();
                            pnlGridIndex.Visible = false;
                        }



                    }
                    SessionFacade.UserTrig = "0";
                }
                else
                {
                    string xmlName = "-UserNotesHistory";
                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";
                    string Pathname2 = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";
                   
                    ds = BindNotesCommHistory(true);
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();


                    System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname2);
                    ds.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
                    xmlSW2.Close();

                    SessionFacade.UserTrig = "1";
                    DataTable notetable3 = GetDatafromXML(xmlName);
                   //notetable3.Columns.Remove("Row");
                   grdNotesHistory.DataSource = null;
                    //grdNotesHistory.DataSource = GetDatafromXML(xmlName);
                    grdNotesHistory.DataSource = notetable3;
                    grdNotesHistory.DataBind();
                    pnlGridIndex.Visible = true;
                }
            }
            catch (Exception e)
            {
                new ArgumentNullException();
                grdNotesHistory.Visible = false;
                pnlGridIndex.Visible = false;
            }
        }




        public DataTable GetDatafromXML(string xmlName = "-NotesHistory")
        {
            try
            {

                //DataRow rowOrders;
                DataSet ds = new DataSet();
                string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";
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
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            DataSet drExisting;
            cDialerData objDialerData = new cDialerData();
            string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";
            objDialerData.SearchOrderCampaignName = ddlTemp.SelectedValue.ToString().Trim();
            objDialerData.DialerKAMID = SessionFacade.KamId;

            drExisting = objDialerData.GetDialerDataT();

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


        public DataTable GetXMLDialerData(string xmlName = "-DialerDataT")
        {
            try
            {


                //DataRow rowOrders;
                DataSet ds = new DataSet();
                string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";
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
            string xmlName;
            if (SessionFacade.UserTrig == "1")
            {
                xmlName = "-DialerData";
            }
            else
            {
                xmlName = "-DialerData";
            }
            grdDialerHistory.DataSource = GetXMLDialerData();
            grdDialerHistory.PageIndex = e.NewPageIndex;
            grdDialerHistory.DataBind();
        }


        public void ShowDialerData(bool UserNotes = false)
        {

            try
            {

                DataSet ds = new DataSet();
                if (UserNotes == false)
                {

                    ////DataRow rowOrders;

                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerDataT" + ".xml";
                    SessionFacade.LastAccount[3] = null;

                    //if same account number = last account number get data from xml
                    if (SessionFacade.LastAccount[3] == SessionFacade.AccountNo.ToString().Trim())
                    {
                        DataTable dialertable = GetXMLDialerData();
                       //dialertable.Columns.Remove("Row");
                        //grdDialerHistory.DataSource = GetXMLDialerData();
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
                            //dialertable2.Columns.Remove("Row");
                            //grdDialerHistory.DataSource = GetXMLDialerData();
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
                    //dialertable3.Columns.Remove("Row");
                    //grdDialerHistory.DataSource = GetXMLDialerData();
                    grdDialerHistory.DataSource = dialertable3;
                    grdDialerHistory.DataBind();

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
            //ShowNotesHistory();
            grdNotesHistory.DataSource = null;
            grdNotesHistory.DataSource = GetDatafromXML();
            grdNotesHistory.DataBind();
        }
        protected void StatusSearchDateRange(bool value)
        {
            imgstartCal.Enabled = value;
            imgEndCal.Enabled = value;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
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
            if (ByDate.Checked == true)
            {
               StatusSearchDateRange(true);
            //    ShowNotesHistory();
               grdNotesHistory.DataSource = GetDatafromXML();
               grdNotesHistory.DataBind();
               //ShowNotesHistory();
            }
            else
            {
                StatusSearchDateRange(false);
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                //ShowNotesHistory();
                grdNotesHistory.DataSource = GetDatafromXML();
                grdNotesHistory.DataBind();
                //ShowNotesHistory();
            }
        }


        protected void btnAllNotes_Click(object sender, EventArgs e)
        {
            ShowNotesHistory(true);
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
            ExportExcelFunction();

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
                    //// ds = BindDialerData();

                    //drExisting = objDialerData.GetDialerDataT();
                    ds.Tables.Clear();
                    DataSet dialerds = new DataSet();
                    string DialerPathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName +"-DialerDataT.xml";
                    DataRow[] dialerresults;
                    DataTable dialerdtTemp = new DataTable();
                    

                    Query = "1 = 1";

                    ////Reading XML
                    //System.IO.FileStream dialerfsReadXml = new System.IO.FileStream(DialerPathname, System.IO.FileMode.Open);
                    //dialerds.ReadXml(dialerfsReadXml);
                    //dialerfsReadXml.Close();

                    ////To Copy the Schema.
                    //dialerdtTemp = dialerds.Tables[0].Clone();
                    
                    //dialerdtTemp = dialerds.Tables[0];
                    //drExisting.Tables[0].Columns.Remove("Row");
                   // dialerds = drExisting;


                    System.IO.FileStream dialerfsReadXml = new System.IO.FileStream(DialerPathname, System.IO.FileMode.Open);
                    ds.ReadXml(dialerfsReadXml);
                    fsReadXml.Close();
                    ds.Tables[0].Columns.Remove("Row");
                   // dtTemp.Clear();
                    dtTemp = ds.Tables[0].Clone();

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);

                    ds.Tables.Clear();

                    ds.Tables.Add(dtTemp);

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
                objactAddNote.Note = textnote.ToUpper();//SessionFacade.TextNote.ToUpper();//objactAddNote.Note;


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
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Hello');", true);

            }

            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Nottes & Com History Page - Button Login Click Error", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                throw new ArgumentException("Invalid Data", ex);
            }
        }
        protected void btnAddNotes_Click(object sender, EventArgs e)
        {

            try
            {
               ReturnString(txtNoteStartDate.Text, NoteTypes.Text, AddNote.Text);
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
                string[] list = { "DATE", "SCHEDULED DATE", "ACCOUNT NUMBER" };

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
