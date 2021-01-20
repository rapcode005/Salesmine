using System;
using System.Data; 
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AppLogic;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;

namespace WebSalesMine.WebPages.Home
{
    public partial class ArrangeColumns : System.Web.UI.Page
    {
        //string DefaultOrderHistoryColumns = "ACCOUNT NUMBER|ORDER DATE|PART NUMBER|DESCRIPTION|UNIT PRICE|QTY|EXT PRICE|ORDER NUMBER|LINE|ORDER TYPE|CONVERTED DATE|REASON REJECTION|ORDER BLOCK|CUSTOMER PO|CUSTOMER NAME|ORIG ACCOUNT|CONTACT NUMBER|LAST NAME|FIRST NAME|EMAIL ADDRESS|PHONE|SHIP NAME|SHIP MAILING|SHIP CITY|SHIP ZIP|SHIP STATE|SHIP DATE|TRACKING NO|BILL SALES|BILL NO|BILL DATE";
        string DefaultOrderHistoryColumns = "ACCOUNT NUMBER|ORDER DATE|PART NUMBER|DESCRIPTION|UNIT PRICE|QTY|EXT PRICE|ORDER NUMBER|LINE|ORDER TYPE|CONVERTED DATE|REASON REJECTION|ORDER BLOCK|CUSTOMER PO|CUSTOMER NAME|ORIG ACCOUNT|CONTACT NUMBER|LAST NAME|FIRST NAME|EMAIL ADDRESS|PHONE|SHIP NAME|SHIP MAILING|SHIP CITY|SHIP ZIP|SHIP STATE";
        string DefaultCustomerLookUpColumns = @"CONTACT STATUS|ACCOUNT NUM|ACCOUNT NAME|MANAGED GROUP|BUYER ORG|KAM NAME|CONTACT NUM|CONTACT NAME|CONTACT TYPE|CONTACT PHONE|CONTACT RECENCY|CONTACT SALES 12M|FUNCTION|LAST DISP DATE|LAST DISP NOTE|LAST PURCHASED DATE|EMAIL ADDRESS|REP CONTACT STATUS|REP JOB AREA";
        //string DefaultQuotesColumns = "Quote_Doc_No|Quote_Line|Quote_Date|Quote_PO_Type|Quote_PO_Type_Desc|Quote_Item_Categ_Desc|Quote_Mat_Entrd|Quote_Mat_Entrd_Desc|Quote_SlsTeamIN|Quote_Discount|Quote_Net_Sales|DM_Product_Line_Desc";
        string DefaultQuotesColumns = "Quote_Doc_No|Quote_Line|Quote_Date|Quote_PO_Type|Quote_PO_Type_Desc|Quote_Item_Categ_Desc|Quote_Mat_Entrd|Quote_Mat_Entrd_Desc|Quote_SlsTeamIN|DM_Product_Line_Desc|quote_qty|Quote_Discount|Quote_Net_Sales|quote_unit_price|Quote_Cost";
        //string DefaultSite_ContactColumns = "CONTACT NUMBER|FIRST NAME|LAST NAME|CONTACT TYPE|CONTACT STATUS|REPDATA CONTACT STATUS|REPDATA FUNCTION|RECENCY|DEPARTMENT|JOB FUNCTION|TITLE|DIRECT PHONE|SITE PHONE|EMAIL ADDRESS|DO NOT MAIL|DO NOT EMAIL|DO NOT CALL|LIFETIME SALES|LAST 12M SALES|LIFETIME ORDERS|LAST 12M ORDERS|LAST PURCHASED DATE|OHIE|HSE|ES|PC UPDATE BY|PC UPDATE ON|CREATED BY|CREATED ON|SUPRESS DETAILS|MAIL|FAX|EMAIL|PHONE|COMMENT";
        string DefaultSite_ContactColumns = "Contact Number|First Name|Last Name|Contact Type|Contact Status|Repdata Contact Status|Repdata Function|Recency|Department|Job Function|Title|Direct Phone|Site Phone|Email Address|Do Not Mail|Do Not Email|Do Not Fax|Do Not Call|Lifetime Sales|Last 12M Sales|Lifetime Orders|last 12M Orders|Last Purchased Date|Phone Extension";

        
        //string DefaultQuotesColumns = "Quote_Line|Quote_Doc_No|Quote_Date|Quote_Created_By|Quote_Create_Time|Quote_PO_Type_Desc|Quote_Item_Categ_Desc|Quote_Reason_Code_Desc|Quote_Reason_Rej_Desc|Quote_SlsTeamIN|Quote_Material_Desc|Quote_Mat_Entrd|Product_Family_Desc|Quote_Coupon_CODE|Quote_Discount|Quote_Net_Sales|Quote_Freight|Quote_Cost|Order_Doc_No|Order_Line|Order_Date|Order_Createdby|Order_PO_Type_Desc|Order_Item_Categ_Desc|Order_Material_Desc|Order_Mat_Entrd_Desc|Order_Discount|Order_Net_Sales|Order_Freight|Order_Cost|Order_Refer_Doc|Order_Refer_Itm|Order_Coupon_CODE|Surname|Firstname";
        
        //Will be used in Product Summary Teritory for - SKU Summary: table
        string DefaultTProduct_SKUSummaryColumns = "PRODUCT FAMILY|PRODUCT DESCRIPTION|PRODUCT NUMBER|F10 SALES|F11 SALES|F12 SALES|F13 SALES|LIFETIME SALES|LIFETIME ORDERS|LAST ORDER DATE|F10 UNITS|F11 UNITS|F12 UNITS|F13 UNITS";
        //string DefaultTProduct_SKUSummaryColumns = "PRODUCT CATEGORY|PRODUCT FAMILY|PRODUCT DESCRIPTION|PRODUCT NUMBER|F10 SALES|F11 SALES|F12 SALES|F13 SALES|LIFETIME SALES|LIFETIME ORDERS|LAST ORDER DATE|F10 UNITS|F11 UNITS|F12 UNITS|F13 UNITS";
        string DefaultTProduct_PC_SKUSummaryColumns = "SPACE CODE|PRODUCT DESCRIPTION|PRODUCT NUMBER|LAST REVISION DATE|F11 SALES|F12 SALES|F13 SALES|F14 SALES|LIFETIME SALES|LIFETIME ORDERS|LAST ORDER DATE|F11 UNITS|F12 UNITS|F13 UNITS|F14 UNITS";

        //string DefaultTProduct_PC_SKUSummaryColumns = "Space Code|SKU Description|SKU Number|Last Revision Date|F09 Sales|F10 Sales|F11 Sales|F12 Sales|Lifetime Sales|Lifetime Orders|Last Ordered Date|F09 Units|F10 Units|F11 Units|F12 Units";
        // string DefaultTProduct_PC_SKUSummaryColumns = "PRODUCT FAMILY|LAST REVISION DATE|F10 SALES|F11 SALES|F12 SALES|F13 SALES|TOTAL SALES|LIFETIME ORDERS|LAST ORDER DATE|F10 UNITS|F11 UNITS|F12 UNITS|F13 UNITS";

        //string DefaultTProduct_PC_SKUSummaryColumns = "Space Code|SKU Description|SKU Number|Last Revision Date|F09 Sales|F10 Sales|F11 Sales|F12 Sales|Lifetime Sales|Lifetime Orders|Last Ordered Date|F09 Units|F10 Units|F11 Units|F12 Units";

        //Will be used in Product Summary for - SKU Summary: table
        string DefaultProduct_SKUSummaryColumns = "PRODUCT FAMILY|PRODUCT DESCRIPTION|PRODUCT NUMBER|F11 SALES|F12 SALES|F13 SALES|F14 SALES|LIFETIME SALES|LIFETIME ORDERS|LAST ORDER DATE|F11 UNITS|F12 UNITS|F13 UNITS|F14 UNITS";
        //string DefaultProduct_SKUSummaryColumns = "PRODUCT CATEGORY|PRODUCT FAMILY|PRODUCT DESCRIPTION|PRODUCT NUMBER|F10 SALES|F11 SALES|F12 SALES|F13 SALES|LIFETIME SALES|LIFETIME ORDERS|LAST ORDER DATE|F10 UNITS|F11 UNITS|F12 UNITS|F13 UNITS";
        string DefaultProduct_PC_SKUSummaryColumns = "SPACE CODE|PRODUCT DESCRIPTION|PRODUCT NUMBER|LAST REVISION DATE|F11 SALES|F12 SALES|F13 SALES|F14 SALES|LIFETIME SALES|LIFETIME ORDERS|LAST ORDERED DATE|F11 UNITS|F12 UNITS|F13 UNITS|F14 UNITS";
        //string DefaultProduct_PC_SKUSummaryColumns = "Space Code|SKU Description|SKU Number|Last Revision Date|F09 Sales|F10 Sales|F11 Sales|F12 Sales|Lifetime Sales|Lifetime Orders|Last Ordered Date|F09 Units|F10 Units|F11 Units|F12 Units";
        string DefaulNotesColumns = "DATE|CREATED BY|NOTE TYPE|NOTES|SCHEDULED DATE|ACCOUNT NUMBER|CONTACT NUMBER|DISPOSITION CODE|DISPOSITION DESCRIPTION|AGENT NAME|PHONE NUMBER";
        string DefaulDialerColumns = "ACCOUNT NUMBER|LIST NAME|CONTACT DATE|PHONE NUMBER|DISPOSITION|DESCNAME|AGENT LOGIN|AGENT NAME";
       // string DefaulDialerColumns = "Account Number|List Name|Contact Date|Phone Number|Disposition|DescName|Agent Login|Agent Name";

        //Will be used in Notes and ComHist Territory: table
        string DefaulNotesTerritoryColumns = "DATE|CREATED BY|NOTE TYPE|NOTES|SCHEDULED DATE|ACCOUNT NUMBER|CONTACT NUMBER|DISPOSITION CODE|DISPOSITION DESCRIPTION|AGENT NAME|PHONE NUMBER";
        //string DefaulNotesTerritoryColumns = "Created_on|Created_by|Note_Type|Notes|Date|Account_Number|Contact_Number|Disposition_Code|Disposition_Description|AGENT NAME|PHONE NUMBER";
        string DefaulDialerTerritoryColumns = "ACCOUNT NUMBER|LIST NAME|CONTACT DATE|PHONE NUMBER|DISPOSITION|DESCNAME|AGENT LOGIN|AGENT NAME";
        string DefaulQuotePipeline = "SOURCE|QUOTE DAY|ACCOUNT NO.|ACCOUNT NAME|QUOTE DOC|QUOTE ASSIGNMENT|ACCOUNT ASSIGNMENT|QUOTE VALUE|QUOTE COST|QUOTE GM PERCENTAGE|CLOSE PROBABILITY|WEIGHTED VALUE|PROPOSED DATE|COMPETITION|NOTES|SCHEDULE FOLLOWUP|PRODUCT LINE|CONSTRUCTION|MINING";

        protected string strPageDefaultColumns = string.Empty;
        public string listviewType = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ListBox1.Attributes.Add("onkeydown", "listbox1_keydown(this)");
                //ListBox2.Attributes.Add("onkeydown", "listbox2_keydown(this)");
                if (Request.QueryString["Data"] != null)
                {
                    litInfo.Text = "";
                    SessionFacade.ListVieweType = Request.QueryString["Data"];
                   
                    if (SessionFacade.ListVieweType == "lvwData")
                    {
                        strPageDefaultColumns = DefaultOrderHistoryColumns;
                    }
                    if (SessionFacade.ListVieweType == "lvwLookupData")
                    {
                        strPageDefaultColumns = DefaultCustomerLookUpColumns;
                    }
                    if (SessionFacade.ListVieweType == "lvwQuoteitem")
                    {
                        strPageDefaultColumns = DefaultQuotesColumns;
                    }
                    if (SessionFacade.ListVieweType == "lvwContInfo")
                    {
                        strPageDefaultColumns = DefaultSite_ContactColumns;
                    }

                    //1) Product Summary
                    //If its not PC Sku Summary
                    if (SessionFacade.ListVieweType == "lvwSKUSummary")
                    {
                        strPageDefaultColumns = "ProductSummary";
                        //LoadColumn();
                        Page.Value = "ProductSummary";
                    }

                    //If its PC Sku Summary
                    if (SessionFacade.ListVieweType == "lvwPCSKUSummary")
                    {
                        //strPageDefaultColumns = DefaultProduct_PC_SKUSummaryColumns;
                        strPageDefaultColumns = "ProductSummary";
                        //LoadColumn();
                        Page.Value = "ProductSummary";
                    }

                    // 2)Product Summary Teritory
                    //If its not PC Sku Summary
                    if (SessionFacade.ListVieweType == "lvwSKUSummaryT")
                    {
                        //strPageDefaultColumns = DefaultTProduct_SKUSummaryColumns;
                        strPageDefaultColumns = "ProductSummary";
                        //LoadColumn();
                        Page.Value = "ProductSummary";
                    }


                    //If its PC Sku Summary
                    if (SessionFacade.ListVieweType == "lvwPCSKUSummaryT")
                    {
                        strPageDefaultColumns = DefaultProduct_PC_SKUSummaryColumns;
                    }
                    
                    if (SessionFacade.ListVieweType == "lvwNotesData")
                    {
                        strPageDefaultColumns = DefaulNotesColumns;
                    }

                    if (SessionFacade.ListVieweType == "lvwDialerData")
                    {
                        strPageDefaultColumns = DefaulDialerColumns;
                    }

                    if (SessionFacade.ListVieweType == "lvwNotesDataTer")
                    {
                        strPageDefaultColumns = DefaulNotesTerritoryColumns;
                    }

                    if (SessionFacade.ListVieweType == "lvwDialerDataTer")
                    {
                        strPageDefaultColumns = DefaulDialerTerritoryColumns;
                    }

                    if (SessionFacade.ListVieweType == "lvwQuotePipeline")
                    {
                        strPageDefaultColumns = DefaulQuotePipeline;
                    }

                    LoadData();
                }

                
            }

        }



        #region Prepare for loading data into ListVieew's
        protected void LoadData()
        {
            string strListBox1Columns = string.Empty;
            string strListBox2Columns = string.Empty;

            cArrangeDataSet objArrangeCS = new cArrangeDataSet();
            objArrangeCS.CampaignName = SessionFacade.CampaignName;
            objArrangeCS.UserName = SessionFacade.LoggedInUserName;
            objArrangeCS.Listview = SessionFacade.ListVieweType;

            DataSet ds = new DataSet();

            ds = objArrangeCS.ReturnColumnOrder();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (strPageDefaultColumns != "ProductSummary")
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        strListBox2Columns += dr["ColumnName"].ToString().Trim() + "|";
                    }
                    strListBox2Columns = strListBox2Columns.Remove(strListBox2Columns.Length - 1, 1);
                    LoadListBox1(strListBox2Columns);
                    LoadListBox2(strListBox2Columns);
                }
                else
                {
                    LoadColumn(ListBox2);
                    LoadNotIncludeColumn(ListBox1);
                    //DefaultProduct_SKUSummaryColumns=
                }
            }
            else
            {
                if (strPageDefaultColumns != "ProductSummary")
                {
                    strListBox2Columns = strPageDefaultColumns;
                    LoadListBox1(strListBox2Columns);
                    LoadListBox2(strListBox2Columns);
                }
                else
                {
                    LoadColumn(ListBox2);
                    //LoadNotIncludeColumn(ListBox1);
                }

            }


        }
        #endregion

        #region Load Data in to Both ListView
        //Load First ListBox

        protected void LoadListBox1(string strListBox1Columns)
        {
            if (strListBox1Columns.Length < 1)
            {

                List<string> splitOrderHistory = new List<string>(strPageDefaultColumns.Split('|'));

                ListBox2.DataSource = splitOrderHistory;
                ListBox2.DataBind();
            }
            else
            {

                List<string> splitOrderHistory = new List<string>(strPageDefaultColumns.Split('|'));
                string[] strTemp = strListBox1Columns.Split('|');
                for (int i = 0; i < strTemp.Length; i++)
                {
                    string str = strTemp[i];

                    if (splitOrderHistory.Contains(str))
                    {
                        splitOrderHistory.Remove(str);
                    }

                }
                ListBox1.DataSource = splitOrderHistory;
                ListBox1.DataBind();
            }


        }

        //Load Second ListBox
        protected void LoadListBox2(string strListBox2Columns)
        {
            if (strListBox2Columns.Length > 0)
            {
                List<string> splitListBox2Columns = new List<string>(strListBox2Columns.Split('|'));

                ListBox2.DataSource = splitListBox2Columns;
                ListBox2.DataBind();
            }

        }
        #endregion

        #region Load ColumnName
        protected void LoadColumn(ListBox LtBox)
        {
            if (SessionFacade.ListVieweType == "lvwSKUSummary" ||
                SessionFacade.ListVieweType == "lvwPCSKUSummary")
            {
                DataSet dsColumn = new DataSet();
                cArrangeDataSet ADS = new cArrangeDataSet();
                ADS.CampaignName = SessionFacade.CampaignValue;
                ADS.Listview = SessionFacade.ListVieweType;

                dsColumn = ADS.GetColumnDetails();

                LtBox.DataSource = dsColumn;
                LtBox.DataValueField = "ColumnValue";
                LtBox.DataTextField = "ColumnName";
                LtBox.DataBind();
            }
        }

        protected void LoadNotIncludeColumn(ListBox LtBox)
        {
            if (SessionFacade.ListVieweType == "lvwSKUSummary" ||
                SessionFacade.ListVieweType == "lvwPCSKUSummary")
            {
                DataSet dsColumn = new DataSet();
                cArrangeDataSet ADS = new cArrangeDataSet();
                ADS.CampaignName = SessionFacade.CampaignValue;
                ADS.Listview = SessionFacade.ListVieweType;

                dsColumn = ADS.GetColumnNotIncluded();

                if (dsColumn.Tables[0].Rows.Count > 0)
                {
                    LtBox.DataSource = dsColumn;
                    LtBox.DataValueField = "ColumnValue";
                    LtBox.DataTextField = "ColumnName";
                    LtBox.DataBind();
                }
                else
                {
                    LtBox.DataSource = null;
                    LtBox.DataBind();
                }
            }
        }
        #endregion

        #region MoveUp & Move Down the Items
        protected void MoveUp(object sender, EventArgs e)
        {
            MoveListboxItem(-1, ListBox2);
        }
        protected void MoveDown(object sender, EventArgs e)
        {

            MoveListboxItem(1, ListBox2);
        }

        private void MoveListboxItem(int index, ListBox listBox)
        {
            if (listBox.SelectedIndex != -1) //is there an item selected?
            {
                //if it's moving up, the loop moves from first to last, otherwise, it moves from last to first
                for (int i = (index < 0 ? 0 : listBox.Items.Count - 1); index < 0 ? i < listBox.Items.Count : i > -1; i -= index)
                {
                    if (listBox.Items[i].Selected)
                    {
                        //if it's moving up, it should not be the first item, or, if it's moving down, it should not be the last
                        if ((index < 0 && i > 0) || (index > 0 && i < listBox.Items.Count - 1))
                        {
                            //if it's moving up, the previous item should not be selected, or, if it's moving down, the following item should not be selected
                            if ((index < 0 && !listBox.Items[i - 1].Selected) || (index > 0 && !listBox.Items[i + 1].Selected))
                            {
                                ListItem itemA = listBox.Items[i]; //the selected item

                                listBox.Items.Remove(itemA); //is removed

                                listBox.Items.Insert(i + index, itemA); //and swapped
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Save BUtton Click -- Save the Content from listview to 2 to Database
        //Save the Content from listview to 2 to Database
        //Save Button Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                litInfo.Text = "";

                string strItem = string.Empty;
                string ReqField = string.Empty;

                if (ListBox2.Items.Count > 0)
                {

                    cArrangeDataSet objArrangeCS = new cArrangeDataSet();
                    objArrangeCS.CampaignName = SessionFacade.CampaignName;
                    objArrangeCS.UserName = SessionFacade.LoggedInUserName;
                    objArrangeCS.Listview = SessionFacade.ListVieweType;

                    //For Customer Lookup. The Following column is required to identify the selected Contact Num and Account Num
                    if (SessionFacade.ListVieweType == "lvwLookupData")
                    {
                        for (int i = 0; i < ListBox1.Items.Count; i++)
                        {
                            ReqField = ListBox1.Items[i].Text;
                            if (ReqField == "ACCOUNT NUM" || ReqField == "CONTACT NUM" || ReqField == "CONTACT NAME")
                            {
                                ListBox2.Items.Add(ReqField);
                                ListBox1.Items.Remove(ReqField);
                            }

                        }


                    }

                    if (SessionFacade.ListVieweType == "lvwNotesData")
                    {
                        for (int i = 0; i < ListBox1.Items.Count; i++)
                        {
                            ReqField = ListBox1.Items[i].Text;
                            if (ReqField == "Row")
                            {
                                ListBox2.Items.Add(ReqField);
                                ListBox1.Items.Remove(ReqField);
                            }

                        }


                    }

                    if (SessionFacade.ListVieweType == "lvwPCSKUSummary" || SessionFacade.ListVieweType == "lvwSKUSummary" || SessionFacade.ListVieweType == "lvwSKUSummaryT" || SessionFacade.ListVieweType == "lvwPCSKUSummaryT")
                    {
                        for (int i = 0; i < ListBox1.Items.Count; i++)
                        {
                            ReqField = ListBox1.Items[i].Text;
                            if (ReqField == "Last Revision Date" || ReqField == "Last Ordered Date")
                            {
                                ListBox2.Items.Add(ReqField);
                                ListBox1.Items.Remove(ReqField);
                            }

                        }
                    }

                    int CountBeforeDelete = objArrangeCS.ColumnReorderCount();
                    if (CountBeforeDelete > 0)
                    {
                        int t = objArrangeCS.DeleteColumns();
                    }


                    //if (SessionFacade.ListVieweType == "lvwData")
                    //{
                        string[] splitValue = ListBox2Value.Value.ToString().Split('|');


                        for (int i = 0; i < splitValue.Length; i++)
                        {
                            int strItem1 = (i + 1);
                            objArrangeCS.mstrInsertColumnList = splitValue[i];
                            objArrangeCS.InsertPosition = strItem1;
                            int insertcol = objArrangeCS.InsertColumns();
                        }
                    //}
                    //else
                    //{
                    //    for (int i = 0; i < ListBox2.Items.Count; i++)
                    //    {
                    //        strItem += ListBox2.Items[i].Text + "|";
                    //        int strItem1 = (i + 1);
                    //        objArrangeCS.mstrInsertColumnList = ListBox2.Items[i].Text;
                    //        objArrangeCS.InsertPosition = strItem1;
                    //        int insertcol = objArrangeCS.InsertColumns();
                    //    }
                    //}

                   

                   
                }
                //    litInfo.Text = "Selected are" + strItem + " \n and there are values are: " + strItem1;
                if (strItem.Length > 0)
                {
                    strItem = strItem.Remove(strItem.Length - 1, 1);
                }
                litInfo.Text = "Records Saved sucessfully";
               // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ReloadtheparentWindow()", true);
                //ScriptManager.RegisterStartupScript(this.upMappings,typeof(string), "alertScript", "window.close();", true);
               //ScriptManager.RegisterClientScriptBlock(uppanelMain, typeof(UpdatePanel), uppanelMain.ClientID, "ReloadtheparentWindow();", true);

                SessionFacade.Update_Bool = true;
                SessionFacade.UpdateDefault_Or_Not = "Not";

                ScriptManager.RegisterStartupScript(this.uppanelMain, typeof(string), "callmee", "window.close(); window.opener.location.reload(true);", true);

              //Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "ReloadtheparentWindow();", true);
            }
            catch (Exception err)
            {
                litInfo.Text = "Error in saving the records. Please try later";
                BradyCorp.Log.LoggerHelper.LogException(err, "Re-Arrange Columns - Error in Arrange Columns", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
            // closeAllWindowQuiet()
        }

        #endregion

        #region Button Default Click -- Remove everything from databse and load the list view with default values

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
              Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
              + "-QuotePipelinecolumn" + ".xml";
            cArrangeDataSet objArrangeCS = new cArrangeDataSet();
            objArrangeCS.CampaignName = SessionFacade.CampaignName;
            objArrangeCS.UserName = SessionFacade.LoggedInUserName;
            objArrangeCS.Listview = SessionFacade.ListVieweType;

            SessionFacade.Update_Bool = true;
            SessionFacade.UpdateDefault_Or_Not = "Default";

            int CountBeforeDelete = objArrangeCS.ColumnReorderCount();
            if (CountBeforeDelete > 0)
            {
                objArrangeCS.DeleteColumns();
            }
            if (System.IO.File.Exists(Pathname))
                System.IO.File.Delete(Pathname);
            ScriptManager.RegisterStartupScript(this.uppanelMain, typeof(string), "callme", "window.opener.location.reload(true); window.close();", true);
        }
    #endregion

        #region Move single item from left to right listview
        protected void btnforward_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBox1.Items.Count > 0)
                {

                    //Adding Items from left to right
                    foreach (ListItem selecteditem in ListBox1.Items)
                    {
                        if (selecteditem.Selected)
                        {
                            ListBox2.Items.Add(selecteditem);
                        }

                    }

                    //Removing Items from left
                    foreach (ListItem selecteditem in ListBox1.Items)
                    {
                        if (selecteditem.Selected)
                        {
                            ListBox1.Items.Remove(selecteditem);
                        }

                    }


                    //// checking whether listbix1 has items or not if yes then moving items
                    //ListBox2.Items.Add(ListBox1.SelectedItem.ToString());
                    //ListBox1.Items.Remove(ListBox1.SelectedItem);
                }
            }

            catch (Exception ex)
            {
                litInfo.Text = "Error in moving items";
                BradyCorp.Log.LoggerHelper.LogException(ex, "Re-Arrange Columns - Error in Moving single Column from first to second", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());

            }

            if (ListBox1.Items.Count > 0)

                ListBox1.SelectedIndex = 0;
            ListBox2.SelectedIndex = ListBox2.Items.Count - 1;

        }
        #endregion

        #region Move all items from left to Right Listview
        protected void btnForwardAll_Click(object sender, EventArgs e)
        {
            try
            {
                int _count = ListBox1.Items.Count;
                if (_count != 0)
                {
                    for (int i = 0; i < _count; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = ListBox1.Items[i].Text;
                        item.Value = ListBox1.Items[i].Value;
                        //Add the item to selected employee list
                        ListBox2.Items.Add(item);
                    }
                }
                ListBox1.Items.Clear();

                //Remove Selected
                foreach (ListItem item in ListBox2.Items)
                {
                    item.Selected = false;
                }
            }

            catch (Exception ex)
            {

                litInfo.Text = "Error in moving items";
                BradyCorp.Log.LoggerHelper.LogException(ex, "Re-Arrange Columns - Error in Moving All Columns from first to second", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
            ListBox2.SelectedIndex = 0;
        }
        #endregion

        #region Remove one item at a time from Right Listview and place them in first listview
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBox2.Items.Count > 0)
                {
                    
                    //checking whether listbox2 has items or not. If yes then moving
                    //selected item from listbox2 to listbox1
                    ListBox1.Items.Add(ListBox2.SelectedItem.ToString());
                    ListBox2.Items.Remove(ListBox2.SelectedItem);
                }

            }

            catch (Exception ex)
            {
                litInfo.Text = "Error in moving items";
                BradyCorp.Log.LoggerHelper.LogException(ex, "Re-Arrange Columns - Error in Moving All Columns from second to first", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

            if (ListBox2.Items.Count > 0)
                ListBox2.SelectedIndex = 0;
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
        }
        #endregion

        #region Move all Items from Second and Place them in First Listview
        protected void btnRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                int _count = ListBox2.Items.Count;
                if (_count != 0)
                {
                    for (int i = 0; i < _count; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = ListBox2.Items[i].Text;
                        item.Value = ListBox2.Items[i].Value;
                        //Add the item to selected employee list
                        ListBox1.Items.Add(item);
                    }
                }
                ListBox2.Items.Clear();

            }

            catch (Exception ex)
            {
                litInfo.Text = "Error in moving items";
                BradyCorp.Log.LoggerHelper.LogException(ex, "Re-Arrange Columns - Error in Moving All Columns from second to first", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

            ListBox1.SelectedIndex = 0;
        }
        #endregion

        [System.Web.Services.WebMethod]
        public static void GetListboxMax1(string Ok, string names)
        {
            //ListBox2
            if(Ok=="Down")
            {
              
            }
        }
    }
}