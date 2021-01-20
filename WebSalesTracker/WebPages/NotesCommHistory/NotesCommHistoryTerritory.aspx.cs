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

namespace WebSalesMine.WebPages.Notes_CommHistory
{
    public partial class NotesCommHistoryTerritory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            TextBox txtTempContact = Master.FindControl("txtContactNumber") as TextBox;
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            string tempCampaign = ddlTemp.SelectedValue.ToString().Trim();

            if (!IsPostBack)
            {
                
                tempCampaign = ddlTemp.SelectedValue.ToString().Trim();
                StatusSearchDateRange(false);
                string acTUser = SessionFacade.LoggedInUserName;
                btnAllNotes.Text = "Show All " + acTUser.ToUpper() + "'s Notes";

                //grdDialerHistory.DataSource = BindDialerData();
                //grdDialerHistory.DataBind();
                ShowDialerData();
                txtNoteStartDate.Attributes.Add("readonly", "true");
                txtStartDate.Attributes.Add("readonly", "true");
                txtEndDate.Attributes.Add("readonly", "true");
                btnRefreshNotes.Attributes.Add("onMouseOver", "this.className='hooverbutton'");
                ShowNotesHistory();
            }
            else
            {
                if (ddlTemp.SelectedValue != SessionFacade.CampaignValue)
                {
                    ShowNotesHistory();
                    ShowDialerData();
                }
            }
           
        }


      
        [System.Web.Services.WebMethod]
        public static void ReturnString(string notedate, string notetype, string textnote)
        {

            // return DateTime.Now;
           
            Home.Notes.NoteType();
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

            bool test = objactAddNote.AddNote();

        }



        protected void btnRefreshNotes_Click(object sender, EventArgs e)
        {

            ShowNotesHistory();
            
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
            
           // objNotesCommHistory.CampaignName = SessionFacade.CampaignName.ToString().Trim();
            objNotesCommHistory.CampaignName = ddlTemp.SelectedValue.ToString().Trim();
            objNotesCommHistory.AccountNum = SessionFacade.AccountNo.ToString().Trim();
            objNotesCommHistory.CreatedBy = SessionFacade.LoggedInUserName.ToUpper();
            objNotesCommHistory.NotesKAMID = SessionFacade.KamId;
            if (UserNotes == false)
            {
                string[] TempAccount = new string[6];

                drExisting = objNotesCommHistory.GetNotesCommHistoryT();

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


        protected void NotesCommHistoryPageChanging(object sender, GridViewPageEventArgs e)
        {
            string xmlName;
            if (SessionFacade.UserTrig == "1")
            {
                xmlName = "-UserNotesHistory";
            }
            else
            {
                xmlName = "-NotesHistory";
            }
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

                DataSet ds = new DataSet();
                if (UserNotes == false)
                {

                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";
                    string Pathname2 = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-UserNotesHistory" + ".xml";

                    //SessionFacade.LastAccount[3] = null;

                    //if same account number = last account number get data from xml
                    if (SessionFacade.LastAccount[3] == SessionFacade.AccountNo.ToString().Trim())
                    {
                        string xmlName = "-UserNotesHistory";
                        if (GetDatafromXML(xmlName).Rows.Count > 0)
                        {
                        grdNotesHistory.DataSource = GetDatafromXML(xmlName);
                        grdNotesHistory.DataBind();
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
                        ViewState["NotesHist"] = ds;
                        DataSet ReArrangeDs = new DataSet();
                        ReArrangeDs = ds;

                        //if (GetDatafromXML().Rows.Count > 0)
                        //{
                            
                        //    ViewState["NotesHist"] = GetDatafromXML();
                        //    ds = (DataSet)ViewState["NotesHist"];
                        //}
                        //else
                        //{
                        //    ds = BindNotesCommHistory();
                        //    ViewState["NotesHist"] = ds;
                        //}

                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            if (ReArrangeDs != null)
                            {
                                cArrangeDataSet ADS = new cArrangeDataSet();
                                ADS.CampaignName = SessionFacade.CampaignValue;
                                ADS.UserName = SessionFacade.LoggedInUserName;
                                ADS.Listview = "lvwNotesDataTer";

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

                            System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname2);
                            ds.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
                            xmlSW2.Close();

                            if (GetDatafromXML().Rows.Count > 0)
                            {
                                grdNotesHistory.DataSource = GetDatafromXML();
                                // grdNotesHistory.DataSource = (DataSet)ViewState["NotesHist"];
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
                            grdNotesHistory.Visible = false;
                            pnlGridIndex.Visible = false;
                        }

                    }
                   
                    SessionFacade.UserTrig = "0";
               
                }
                else
                {
                    string xmlName = "-UserNotesHistory";
                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";
                    ds = BindNotesCommHistory(true);
                    ViewState["NotesHist"] = ds;
                   
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();
                    SessionFacade.UserTrig = "1";
                    grdNotesHistory.DataSource = (DataSet)ViewState["NotesHist"]; //GetDatafromXML(xmlName);
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

                if (Request.Cookies["CNo"] != null)
                {
                    SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                    Query = Query + " and [Contact_Number] = " + Request.Cookies["CNo"].Value;
                }

                //Search by Date
                if (ByDate.Checked == true)
                {
                    if ((txtStartDate.Text != "") || (txtEndDate.Text != ""))
                    {
                        Query = Query + " and Created_on >= '" + txtStartDate.Text + "' and Created_on <=  '" +
                        txtEndDate.Text + "'";
                    }
                }
                if ((ddlNoteType.Text.Trim().ToUpper() != "ALL") && (ddlNoteType.Text.Trim().ToUpper() != ""))
                {
                    Query = Query + " and Note_Type ='" + ddlNoteType.Text.Trim().ToUpper() + "'";
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

            //DataSet drExisting;
            //cDialerData objDialerData = new cDialerData();
            //string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";

            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            DataSet drExisting;
            cDialerData objDialerData = new cDialerData();
            string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";
            objDialerData.SearchOrderCampaignName = ddlTemp.SelectedValue.ToString().Trim();

            drExisting = objDialerData.GetDialerData();

            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            drExisting.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();
            SessionFacade.UserTrig = "1";
            if (drExisting.Tables[0].Rows.Count > 0)
            {
                grdDialerHistory.Visible = true;
                Panel3.Visible = true;

            }
            else
            {
                grdDialerHistory.Visible = false;
                Panel3.Visible = false;

            }

            return drExisting;
        }


        public DataTable GetXMLDialerData(string xmlName = "-DialerData")
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

                //Search by Date
                //if (ByDate.Checked == true)
                //{
                //    if ((txtStartDate.Text != "") || (txtEndDate.Text != ""))
                //    {
                //        Query = Query + " and Created_on >= '" + txtStartDate.Text + "' and Created_on <=  '" +
                //        txtEndDate.Text + "'";
                //    }
                //}
                //if ((ddlNoteType.Text.Trim().ToUpper() != "ALL") && (ddlNoteType.Text.Trim().ToUpper() != ""))
                //{
                //    Query = Query + " and Note_Type ='" + ddlNoteType.Text.Trim().ToUpper() + "'";
                //}

                //results = ds.Tables[0].Select(Query);

                //foreach (DataRow dr in results)
                //    dtTemp.ImportRow(dr);

                //return dtTemp;
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

                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";
                    SessionFacade.LastAccount[3] = null;

                    //if same account number = last account number get data from xml
                    if (SessionFacade.LastAccount[3] == SessionFacade.AccountNo.ToString().Trim())
                    {
                        grdDialerHistory.DataSource = GetXMLDialerData();
                        grdDialerHistory.DataBind();
                    }
                    else
                    {

                        //if new account number, get data from database and write to xml

                        ds = BindDialerData();
                        ViewState["DialerData"] = ds;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            grdDialerHistory.Visible = true;
                            Panel3.Visible = true;
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();
                            grdDialerHistory.DataSource = GetXMLDialerData();
                            grdDialerHistory.DataBind();
                        }
                        else
                        {
                            //Writing XML
                            grdDialerHistory.Visible = false;
                            Panel3.Visible = false;
                        }

                    }
                    SessionFacade.UserTrig = "0";
                }
                else
                {
                    string xmlName = "-DialerData";
                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";
                    ds = BindDialerData();
                    ViewState["DialerData"] = ds;
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();
                    SessionFacade.UserTrig = "1";
                    grdDialerHistory.DataSource = GetXMLDialerData();
                    grdDialerHistory.DataBind();

                }
            }
            catch (Exception e)
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


        protected void ddlNoteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowNotesHistory();
        }
        protected void StatusSearchDateRange(bool value)
        {
            imgstartCal.Enabled = value;
            imgEndCal.Enabled = value;
        }
        protected void btnAddNote_Click(object sender, EventArgs e)
        {

        }
        protected void grdNotesHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                bool varColumnExist = false;
                int columnIndex;
                string[] list = { "Created_on", "Date", "Account_Number" };

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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
           
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
                
            }
            else
                StatusSearchDateRange(false);
        }



        protected void btnAddNote_Click1(object sender, EventArgs e)
        {
            //txtAddNote.Text = "";
            //txtNoteDate.Text = "";
        }

        protected void btnAllNotes_Click(object sender, EventArgs e)
        {
            
            ShowNotesHistory(true);
        }

        protected void btnAdfsfdNote_Click(object sender, EventArgs e)
        {
            Panel67.Visible = true;
            ModalPopupExtender2.Show();
        }

        protected void imgstartCal_Click(object sender, ImageClickEventArgs e)
        {

        }

           protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {

        }
        protected void grdDialerHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                bool varColumnExist = false;
                int columnIndex;
                string[] list = { "Contact Date" };

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
                if (rdoNotesHistory.Checked == true && rdoDialerData.Checked == false)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "NoteHistory.xls";
                    tempPageName = "NoteHistorytemp.xls";
                    PageName = "NoteHistorySummary";
                    //ds = BindNotesCommHistory();
                    ds = (DataSet)ViewState["NotesHist"];
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
                if (rdoNotesHistory.Checked == false && rdoDialerData.Checked == true)
                {
                    DestinationUserFileName = SessionFacade.LoggedInUserName + "DialerData.xls";
                    //ds = BindDialerData();
                    ds = (DataSet)ViewState["DialerData"];
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
            Panel66.Visible = false;
            ModalPopupExtender2.Hide();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=" + vPageName + "&FilePath=" + vFilePath + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);

        }
        
        #endregion

        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            try
            {
                ReturnString(txtNoteStartDate.Text, NoteTypes.Text, AddNote.Text);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "Information()", true);
            }
            catch (Exception ex)
            {
                new ArgumentNullException();

            }
        }

        protected void NoteTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtNoteStartDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AddNote_TextChanged(object sender, EventArgs e)
        {

        }

        protected void imgstartCal_Click1(object sender, ImageClickEventArgs e)
        {

        }

              
    }
}