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
using System.Globalization;
using System.Collections;
using System.Text;
using System.Data.SqlTypes;
using ClassLibrary1;


namespace WebSalesMine.WebPages.Home
{
    public partial class Multiview : System.Web.UI.Page
    {
        //Order History variable declaration ends here
        private int OHDateOrdinal = 1000;
        private int OHUnitPrice = 1000;
        private int OHExtPrice = 1000;
        private int OHConvertedDate = 1000;
        private int OHuval = 1000;
        public string OHtxtTemp = "0000001447";
        public string OHddlCampaign = "EMED";
        public string OHddlTemp = "EMED";
        //Order History variabales ends ere

        // Need for Product Summary
        #region ProductSummary Variables
        public bool first = false;
        public bool Second = false;
        private int DateOrdinal = 1000;
        private int PCDateOrdinal = 1000;
        private int PCReviousDateOrdinal = 1000;
        public string ArrnageColumnstring = "lvwSKUSummary";
        public string txtTemp = "0000001447";
        public string ddlCampaign = "EMED";
        public string ddlTemp = "EMED";
        #endregion

        //sc variable declaration ends here
        public string SCtxtTemp = "0000001447";
        public string SCddlCampaign = "EMED";
        public string SCddlTemp = "EMED";


        //for notes & CommHistory

        public bool NCfirst = false;
        public bool NCSecond = false;
        private int NCDateOrdinal = 1000;
        private int NCPCDateOrdinal = 1000;
        private int NCPCReviousDateOrdinal = 1000;
        public string NCArrnageColumnstring = "lvwSKUSummary";
        public string NCtxtTemp = "0000001447";
        public string NCddlCampaign = "EMED";
        public string NCddlTemp = "EMED";


        protected void Page_Load(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View0);
            if (!IsPostBack)
            {
                ShowproductSummaryPage();
                ShowSiteContactInfoPage();
                ShowNotesCommPage();
            }
        }

        protected void ShowOrderHistoryPanel(object sender, EventArgs e)
        {
            try
            {
                ShowOrderHistoryPage();
            }
            catch (Exception err)
            {

            }
            MultiView2.SetActiveView(View1);
          
        }
        protected void ShowNCHPanel(object sender, EventArgs e)
        {
            MultiView2.SetActiveView(View4);
        }
        protected void ShowProductSummaryyPanel(object sender, EventArgs e)
        {
           
            MultiView2.SetActiveView(View2);

        }
        protected void ShowSCPanel(object sender, EventArgs e)
        {
           
            MultiView2.SetActiveView(View3);
        }
        #region Show Site & Contact Function
        protected void ShowSiteContactInfoPage()
        {
            if (!IsPostBack)
            {
                if (SessionFacade.AccountNo.ToString().Trim() != "")
                {
                    txtContactNameOther.Attributes.Add("readonly", "true");
                    txtContactNameProjects.Attributes.Add("readonly", "true");
                    txtDateProject.Attributes.Add("readonly", "true");
                    txtContactNameProjects.Attributes.Add("onchange", "document.getElementById(this.id + 'RO').value = this.value");
                    txtProjectType.Attributes.Add("onchange", "document.getElementById(this.id + 'RO').value = this.value");

                    if (SCddlTemp.ToString() != "PC")
                    {
                        GetSecondaryQuestion();
                        GetOtherVendors();
                        GetProjects();
                    }
                    cpeTabsSiteContact.Collapsed = false;
                    cpeSiteContactInfo.Collapsed = false;
                    lblTitleSiteContactInfo.Text = "-";
                    lblTitleContactInfo.Text = "-";
                    ShowSiteContactInfo();

                    SessionFacade.NameOrContacts = "";

                    // Master Data Changes
                    GetPreferences();
                    GetAccountChanges();
                    GetContactChanges();

                    txtAccountNameMasterChange.Text = txtSiteName.Text;
                    txtAccountNumberMasterChange.Text = SessionFacade.AccountNo;
                    ddlCountry.DataSource = GetCountryList();
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, "");
                    SortDDL(ref this.ddlCountry);

                    if (SCddlTemp.ToString() != "PC")
                    {
                        SCVisibility(true);
                    }
                    else
                    {
                        SCVisibility(false);
                    }

                    //Qualifying Question
                    StatusControlQQ(false);
                    StatusButtonQQ(true);
                    //Qualifying Question PC
                    StatusButtonPC(true);
                    StatusControlPC(false);
                    //Products
                    StatusButtonProducts(true);
                    StatusControlProducts(false);
                }

                //CollapsedText = "Click to Show the Site Info.";
            }
            else
            {
                if (SCddlTemp.ToString() != "PC")
                {
                    SCVisibility(true);
                }
                else
                {
                    SCVisibility(false);
                }
                if (SessionFacade.AccountNo.ToString().Trim() != "")
                {
                    ShowSiteContactInfo();
                }
                else
                {
                    gridSiteContactInfo.DataSource = null;
                    gridSiteContactInfo.DataBind();
                }

            }
        }

        #region Function

        public int RoundOff(int i)
        {
            return ((int)Math.Round(i / 10.0)) * 10;
        }

        protected string CheckSpace(string value)
        {
            if (value == "&nbsp;")
                return string.Empty;
            else
                return value.ToString().Trim().Replace("amp;", "");
        }

        protected string IsNumeric(string value)
        {
            if (value.ToString().Trim() == "")
                return "0";
            else
                return value;
        }

        protected void ClearContacts()
        {
            txtEmailNewContact.Text = string.Empty;
            txtFirstnameNewContact.Text = string.Empty;
            txtSiteName.Text = string.Empty;
            txtOtherNewContact.Text = string.Empty;
            txtPhoneNewContact.Text = string.Empty;
            txtLastanameNewContact.Text = string.Empty;
        }

        protected void ClearContactInfo()
        {
            txtFirstname.Text = string.Empty;
            txtLastname.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            txtDoNotMail.Text = string.Empty;
            txtLifetimeSales2.Text = string.Empty;
            txtfunction.Text = string.Empty;
            txtDoNotFax.Text = string.Empty;
            txtLast12MSalesContact.Text = string.Empty;
            txtContactType.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtDoNotEmail.Text = string.Empty;
            txtLifetimeOrders2.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtDirectPhone.Text = string.Empty;
            txtDoNoCall.Text = string.Empty;
            txtLast12MOrderContact.Text = string.Empty;
            txtRecency.Text = string.Empty;
            txtSitePhone.Text = string.Empty;
            txtLastOrderDateContact.Text = string.Empty;
            txtEmailContact.Text = string.Empty;
        }

        //Get ContactNumber
        protected string GetContactNumber(string value)
        {
            GridViewRow row = gridSiteContactInfo.SelectedRow;
            if (value == "0")
            {
                DataTable dtContactInfo = GetData(CheckSpace(value), txtFirstname.Text, txtLastname.Text);
                if (dtContactInfo.Rows.Count > 0)
                {
                    DataRow dr = dtContactInfo.Rows[0];
                    return CheckSpace(dr["new_contact_key"].ToString());
                }
                else
                    return value;
            }
            else
                return value;
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

        //Drop Down Country
        public List<string> GetCountryList()
        {

            List<string> list = new List<string>();

            CultureInfo[] cultures =

                        CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures |
                        CultureTypes.SpecificCultures);

            foreach (CultureInfo info in cultures)
            {

                RegionInfo info2 = new RegionInfo(info.LCID);

                if (!list.Contains(info2.EnglishName))
                {

                    list.Add(info2.EnglishName);

                }

            }

            return list;

        }

        private void SortDDL(ref DropDownList objDDL)
        {
            ArrayList textList = new ArrayList();
            ArrayList valueList = new ArrayList();


            foreach (ListItem li in objDDL.Items)
            {
                textList.Add(li.Text);
            }

            textList.Sort();


            foreach (object item in textList)
            {
                string value = objDDL.Items.FindByText(item.ToString()).Value;
                valueList.Add(value);
            }
            objDDL.Items.Clear();

            for (int i = 0; i < textList.Count; i++)
            {
                ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
                objDDL.Items.Add(objItem);
            }
        }

        //Visibility of Tabs
        private void SCVisibility(bool visible)
        {
            tabQualifyingQuestion.Visible = visible;
            tabSecondQualifying.Visible = visible;
            tabProductPurchase.Visible = visible;
            tabProjectsOthers.Visible = visible;
            tabPCQQ.Visible = !visible;
        }

        private void ShowSiteInfo(DataSet dsAcc, string PathnameAccount)
        {
            //Site Info
            if (dsAcc.Tables[0].Rows.Count > 0)
            {
                if (PathnameAccount != "")
                {
                    //Writing XML
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameAccount);
                    dsAcc.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();
                }

                txtSiteName.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();
                txtEmployeeSize.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(5).ToString().Trim();
                txtBuyerOrg.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(6).ToString().Trim();
                txtIndustry.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
                txtKeyAcctMng.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(7).ToString().Trim();
                if (dsAcc.Tables[0].Rows[0].ItemArray.GetValue(9).ToString().Trim() == "")
                    txtLast12MSales.Text = "$0.00";
                else
                    txtLast12MSales.Text = decimal.Parse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(9).ToString().Trim()).ToString("C");
                txtLifetimeOrders.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(10).ToString().Trim();
                if (dsAcc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString().Trim() == "")
                    txtLifetimeSales.Text = "$0.00";
                else
                    txtLifetimeSales.Text = decimal.Parse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString().Trim()).ToString("C");
                txtOrganization.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
                txtSIC2.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim();
                txtLast12MOrder.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(11).ToString().Trim();
                if (CheckSpace(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(12).ToString().Trim()) == "")
                    txtLastOrderDate.Text = "";
                else
                    txtLastOrderDate.Text = String.Format("{0:MMM dd, yyyy}", DateTime.Parse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(12).ToString().Trim()));
            }
            else
                ClearControlsSite();

        }

        private DataTable GetData(string Contact, string Fname, string Lname)
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = "[Contact Number] = " + Contact +
                    " and [First Name]='" + Fname + "' and [Last Name]='" + Lname + "'";


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

        private void ShowSiteContactInfo()
        {
            string[] TempAccount = new string[6];
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            string PathnameAccount = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoAccount" + ".xml";
            DataSet ds = new DataSet();
            DataSet dsAcc = new DataSet();
            DataTable dt = new DataTable();
            DataSet ReArrangeDs = new DataSet();
            try
            {
                if (SessionFacade.LastAccount[2] == SCtxtTemp.ToString() &&
                    SCddlTemp.ToString() == SessionFacade.CampaignValue)
                {
                    if (SessionFacade.Update_Bool == true)
                    {
                        dt = BindSiteContactInfo().Tables[0];
                        SessionFacade.Update_Bool = false;
                    }
                    else
                    {
                        if (SCddlTemp.ToString() == SessionFacade.CampaignValue)
                        {
                            ds = GetDatafromXMLArrangeColumn();
                        }
                        else
                        {
                            ds = BindSiteContactInfo();
                            ds = ArrangeColumn(ds);
                        }
                    }

                    //if (dt.Rows.Count == 0)
                    //{
                    //    ReArrangeDs = null;
                    //    ClearContactInfo();
                    //}
                    //else
                    //{
                    //    ReArrangeDs.Tables.Add(dt);
                    //}

                    //Site Info
                    dsAcc.Tables.Add(GetDatafromXMLAccountName());
                    ShowSiteInfo(dsAcc, "");

                    //Contact Info
                    gridSiteContactInfo.DataSource = ds;
                    gridSiteContactInfo.DataBind();
                }
                else
                {
                    ds = BindSiteContactInfo();
                    dsAcc = BindAcc();

                    if (ds != null)
                    {
                        ShowSiteInfo(dsAcc, PathnameAccount);

                        //Contact Info
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //Writing XML
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            ReArrangeDs = ArrangeColumn(ds);

                            if (ReArrangeDs.Tables[0].Rows.Count == 0)
                                ClearContactInfo();

                            gridSiteContactInfo.DataSource = ReArrangeDs;
                            gridSiteContactInfo.DataBind();
                        }
                        else
                        {
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            ClearContactInfo();

                            ReArrangeDs = ArrangeColumn(ds);

                            gridSiteContactInfo.DataSource = null;
                            gridSiteContactInfo.DataBind();
                        }
                    }

                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gridSiteContactInfo', 'HeaderDiv');</script>");
                //dt = ReArrangeDs.Tables[0];

                ////Searching Contact
                //if (Request.Cookies["CNo"] != null)
                //{
                //    int Pageindex, index;
                //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //    {
                //        if (dt.Rows[i].ItemArray.GetValue(0).ToString() == Request.Cookies["CNo"].Value)
                //        {
                //            gridSiteContactInfo.DataSource = dt;
                //            Pageindex = (i / 4);
                //            if (Pageindex != 0)
                //            {
                //                gridSiteContactInfo.PageIndex = Pageindex;
                //                gridSiteContactInfo.DataBind();

                //                gridSiteContactInfo.DataSource = ReArrangeDs;
                //                gridSiteContactInfo.PageIndex = Pageindex;
                //                index = (i - (4 * Pageindex));
                //                gridSiteContactInfo.SelectedIndex = index;
                //                gridSiteContactInfo.DataBind();

                //                tempIndex.Value = Pageindex.ToString() + "," + index;

                //                gridSiteContactInfo_SelectedIndexChanged(null, null);
                //            }
                //            else
                //            {
                //                gridSiteContactInfo.PageIndex = 0;
                //                gridSiteContactInfo.DataBind();

                //                gridSiteContactInfo.DataSource = ReArrangeDs;
                //                gridSiteContactInfo.PageIndex = 0;
                //                gridSiteContactInfo.SelectedIndex = i;
                //                gridSiteContactInfo.DataBind();

                //                gridSiteContactInfo_SelectedIndexChanged(null, null);

                //                tempIndex.Value = "0," + i.ToString();
                //            }

                //            gridSiteContactInfo.DataBind();
                //            break;
                //        }
                //        else
                //            tempIndex.Value = "";
                //    }
                //}
                //else
                //    tempIndex.Value = "";

            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Site and Contact Info - Bind Gridview");
            }
        }

        private DataSet GetDatafromXMLArrangeColumn()
        {
            DataSet ds = new DataSet();
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoArrangeColumn" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(PathnameArrangeColumn, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }

        private DataTable GetDatafromXML(string contact = "", string name = "")
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            Query = "1=1 ";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            //To Copy the Schema.
            dtTemp = ds.Tables[0].Clone();

            if (name != "")
            {
                Query = Query + " and [First Name] like '%" + name +
                    "%' or [Last Name] like '%" + name + "%'";
            }
            else if (contact != "" && contact != "0")
            {
                Query = Query + " and [Contact Number] <> " + contact;
            }
            else if (contact == "0")
            {
                Query = Query + " and new_contact_key <> " + GetContactNumber(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text.ToString()).ToString().Trim()
                + " or new_contact_key is null ";
            }
            //else
            //{
            //    if (Request.Cookies["CNo"] != null)
            //    {
            //        SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
            //        Query = Query + " and contact = " + Request.Cookies["CNo"].Value;
            //    }
            //}

            results = ds.Tables[0].Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;
        }

        private DataTable GetDatafromXMLAccountName(string contact = "", string name = "")
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoAccount" + ".xml";
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

            if (name != "")
            {
                Query = Query + " and firstname like '%" + name +
                    "%' or surname like '%" + name + "%'";
            }
            else if (contact != "")
            {
                Query = Query + " and Contact <> " + contact;
            }
            //else
            //{
            //    if (Request.Cookies["CNo"] != null)
            //    {
            //        SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
            //        Query = Query + " and contact = " + Request.Cookies["CNo"].Value;
            //    }
            //}

            results = ds.Tables[0].Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;
        }

        private DataSet BindSiteContactInfo()
        {
            string[] TempAccount = new string[6];
            DataSet dsSite = new DataSet();
            cSiteAndContactInfo objSiteContactInfo = new cSiteAndContactInfo();
            try
            {
                objSiteContactInfo.SearchAccount = SCtxtTemp.ToString().Trim();

                objSiteContactInfo.SearchCampaign = SCddlTemp.ToString().Trim();

                dsSite = objSiteContactInfo.GetSiteAndContactInfo();

                TempAccount = SessionFacade.LastAccount;

                TempAccount[2] = SCtxtTemp.ToString();

                SessionFacade.LastAccount = TempAccount;

                //LastPurchasedDate = dsSite.Tables[0].Columns["Last Purchased Date"].Ordinal - 4;

                if (dsSite.Tables.Count > 0)
                    return dsSite;
                else
                    return null;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Site and Contact Info - BindingData");
                gridSiteContactInfo.DataSource = null;
                gridSiteContactInfo.DataBind();
                return null;
            }
        }

        private DataSet BindAcc()
        {
            DataSet dsAcc = new DataSet();
            cSiteAndContactInfo objSiteContactInfo = new cSiteAndContactInfo();
            try
            {
                objSiteContactInfo.SearchAccount = SCtxtTemp.ToString().Trim();

                objSiteContactInfo.SearchCampaign = SCddlTemp.ToString().Trim();

                dsAcc = objSiteContactInfo.GetAccInfo();

                if (dsAcc.Tables.Count > 0)
                    return dsAcc;
                else
                    return null;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Acct Info - BindingData");
                ClearControlsSite();
                return null;
            }
        }

        private void ClearControlsSite()
        {
            txtSiteName.Text = string.Empty;
            txtBuyerOrg.Text = string.Empty;
            txtEmployeeSize.Text = string.Empty;
            txtIndustry.Text = string.Empty;
            txtKeyAcctMng.Text = string.Empty;
            txtLast12MSales.Text = string.Empty;
            txtLifetimeOrders.Text = string.Empty;
            txtLifetimeSales.Text = string.Empty;
            txtOrganization.Text = string.Empty;
            txtSIC2.Text = string.Empty;
            txtSiteName.Text = string.Empty;
            txtLast12MOrder.Text = string.Empty;
            txtLastOrderDate.Text = string.Empty;
        }

        private void RefreshSiteContactInfo()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoArrangeColumn" + ".xml";
            DataSet ds = new DataSet(), ReArrangeDs = new DataSet();
            ds = BindSiteContactInfo();

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            ReArrangeDs = ds;

            if (ReArrangeDs != null)
            {
                cArrangeDataSet ADS = new cArrangeDataSet();
                ADS.CampaignName = SessionFacade.CampaignValue;
                ADS.UserName = SessionFacade.LoggedInUserName;
                ADS.Listview = "lvwContInfo";

                int IsReorder = ADS.ColumnReorderCount();
                if (IsReorder > 0)
                {
                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                }
            }


            //Writing XML
            xmlSW = new System.IO.StreamWriter(PathnameArrangeColumn);
            ReArrangeDs.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            gridSiteContactInfo.DataSource = ReArrangeDs;
            gridSiteContactInfo.DataBind();
        }

        private DataSet ArrangeColumn(DataSet ds)
        {
            DataSet ReArrangeDs = new DataSet();
            ReArrangeDs = ds;
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() +
                "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" +
                SessionFacade.LoggedInUserName + "-SiteAndContactInfoArrangeColumn" + ".xml";

            if (ReArrangeDs != null)
            {
                cArrangeDataSet ADS = new cArrangeDataSet();
                ADS.CampaignName = SessionFacade.CampaignValue;
                ADS.UserName = SessionFacade.LoggedInUserName;
                ADS.Listview = "lvwContInfo";

                int IsReorder = ADS.ColumnReorderCount();
                if (IsReorder > 0)
                {
                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
                }
                else
                {
                    string[] ColumnName = { "FSM", "FAE", "LBL", "FSM", "FAE", "OS", "PVM", "LO",
                                                              "PPE", "PI", "SFS","SP","SLS","SCP","SC","TAGS",
                                                          "TPS","TC","W","ETO","P_assign_updatedby","P_assign_updatedon",
                                                          "q4q3","q6","q7","q8","Repdata Budget","UPDATEDBY","VALID_FROM",
                                                         "VALID_TO","q91113","q101214","Createdby","Createdon","new_contact_key" };
                    for (int i = 0; i < ColumnName.Length; i++)
                    {
                        if (ReArrangeDs.Tables[0].Columns.Contains(ColumnName[i]) == true)
                            ReArrangeDs.Tables[0].Columns.Remove(ColumnName[i]);
                    }
                }
            }

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameArrangeColumn);
            ReArrangeDs.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            return ReArrangeDs;
        }

        #endregion

        #region PageChanging
        protected void PageChanging(object sender, GridViewPageEventArgs e)
        {
            gridSiteContactInfo.DataSource = GetDatafromXMLArrangeColumn();
            gridSiteContactInfo.PageIndex = e.NewPageIndex;
            gridSiteContactInfo.DataBind();
            if (tempIndex.Value != "")
            {
                string[] temp = tempIndex.Value.Split(',');

                gridSiteContactInfo.DataSource = GetDatafromXMLArrangeColumn();
                gridSiteContactInfo.PageIndex = e.NewPageIndex;
                gridSiteContactInfo.SelectedIndex = int.Parse(temp[1]);
                gridSiteContactInfo.DataBind();
            }

        }
        #endregion

        #region Sorting Site and Contact Info

        protected SortDirection SCGridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridSiteContactInfo_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (SCGridViewSortDirection == SortDirection.Ascending)
            {
                SCGridViewSortDirection = SortDirection.Descending;
                SortGridSiteContactInfo(sortExpression, "DESC");
            }
            else
            {
                SCGridViewSortDirection = SortDirection.Ascending;
                SortGridSiteContactInfo(sortExpression, "ASC");
            }
        }

        private void SortGridSiteContactInfo(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
                    string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoArrangeColumn" + ".xml";
                    if (File.Exists(Pathname))
                    {

                        DataSet ReArrangeDs = new DataSet();
                        DataTable dsAllColumns = GetDatafromXML();
                        DataSet ds = new DataSet();
                        DataView dv = new DataView(dsAllColumns);

                        dv.Sort = sortExpression + " " + direction;

                        //Copy Sorting Row to XML
                        ds.Tables.Add(dv.ToTable());

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridSiteContactInfo.DataSource = ArrangeColumn(ds);
                        gridSiteContactInfo.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Enable Disable Buttons on the page
        protected void SetButtonVisible()
        {
            if (SessionFacade.UserRole == "ADMIN")
            {
                BtnArrangeColumn.Visible = true;
            }
            else
            {
                BtnArrangeColumn.Visible = false;
            }
        }

        #endregion+

        protected void gridSiteContactInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool varColumnExist = false;
            int columnIndex;
            string[] list = { "Last Purchased Date", "Lifetime Sales","Last 12M Sales",
                                "Lifetime Orders","last 12M Orders","Recency","PC_update_on" };

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
                            if (list[i] == "Last Purchased Date" ||
                                list[i] == "PC_update_on")
                            {
                                DateTime temp;
                                if (DateTime.TryParse(e.Row.Cells[columnIndex].Text, out temp) == true)
                                    e.Row.Cells[columnIndex].Text = Convert.ToDateTime(e.Row.Cells[columnIndex].Text).ToString("MM/dd/yyyy");
                            }
                            else if (list[i] == "Lifetime Sales" ||
                                     list[i] == "Last 12M Sales")
                            {
                                e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;
                                decimal temp;
                                if (decimal.TryParse(e.Row.Cells[columnIndex].Text, out temp) == true)
                                    e.Row.Cells[columnIndex].Text = decimal.Parse(e.Row.Cells[columnIndex].Text).ToString("C2");
                            }
                            else if (list[i] == "Lifetime Orders" ||
                                     list[i] == "last 12M Orders" ||
                                     list[i] == "Recency")
                            {
                                e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;
                            }
                        }
                    }
                }
            }
        }

        protected void ddlFactor_SelectedChanged(object sender, EventArgs e)
        {
            if (ddlFactor.Text == "Other")
                txtOther.Visible = true;
            else
                txtOther.Visible = false;
        }

        protected void gridSiteContactInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gridSiteContactInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FunctionVb Function = new FunctionVb();
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            if (row != null)
            {
                string ContactNumber = row.Cells[GetColumnIndexByName(row, "Contact Number")].Text.ToString();
                txtFirstname.Text = CheckSpace(row.Cells[GetColumnIndexByName(row, "First Name")].Text.ToString());
                txtLastname.Text = CheckSpace(row.Cells[GetColumnIndexByName(row, "Last Name")].Text.ToString());

                DataTable dtContactInfo = GetData(CheckSpace(ContactNumber), txtFirstname.Text, txtLastname.Text);

                foreach (DataRow dr in dtContactInfo.Rows)
                {
                    //Contact Number
                    if (ContactNumber == "0")
                        ContactNumber = CheckSpace(dr["new_contact_key"].ToString());
                    txtContactType.Text = CheckSpace(dr["Contact Type"].ToString());
                    txtStatus.Text = CheckSpace(dr["Contact Status"].ToString());
                    txtRecency.Text = CheckSpace(dr["Recency"].ToString());
                    txtDepartment.Text = CheckSpace(dr["Department"].ToString());
                    txtfunction.Text = CheckSpace(dr["Job Function"].ToString());
                    txtTitle.Text = CheckSpace(dr["Title"].ToString());
                    txtDirectPhone.Text = CheckSpace(dr["Direct Phone"].ToString());
                    txtSitePhone.Text = CheckSpace(dr["Site Phone"].ToString());
                    txtEmailContact.Text = CheckSpace(dr["Email Address"].ToString());
                    txtDoNotMail.Text = CheckSpace(dr["Do Not Mail"].ToString());
                    txtDoNotFax.Text = CheckSpace(dr["Do Not Fax"].ToString());
                    txtDoNotEmail.Text = CheckSpace(dr["Do Not Email"].ToString());
                    txtDoNoCall.Text = CheckSpace(dr["Do Not Call"].ToString());
                    txtLifetimeSales2.Text = decimal.Parse(IsNumeric(CheckSpace(dr["Lifetime Sales"].ToString()))).ToString("C");
                    txtLast12MSalesContact.Text = decimal.Parse(IsNumeric(CheckSpace(dr["Last 12M Sales"].ToString()))).ToString("C");
                    txtLifetimeOrders2.Text = Function.ConvertNumeric(dr["Lifetime Orders"].ToString()).ToString();
                    txtLast12MOrderContact.Text = Function.ConvertNumeric(dr["Last 12M Orders"].ToString()).ToString();
                    if (CheckSpace(dr["Last Purchased Date"].ToString()) == "")
                        txtLastOrderDateContact.Text = string.Empty;
                    else
                        txtLastOrderDateContact.Text = String.Format("{0:MMM dd, yyyy}", DateTime.Parse(CheckSpace(dr["Last Purchased Date"].ToString())));
                }

                //Master Data Changes
                txtContactNameMasterChange.Text = txtLastname.Text + ", " + txtFirstname.Text;
                txtContactNumberMasterChange.Text = ContactNumber;
                txtAccountNameChanges.Text = txtSiteName.Text;
                txtFirstnameContactChanges.Text = txtFirstname.Text;
                txtLastnameContactChanges.Text = txtLastname.Text;

                //Qualifying Question
                if (SCddlTemp.ToString() != "PC")
                {
                    foreach (DataRow dr in dtContactInfo.Rows)
                    {
                        if (CheckSpace(dr["Repdata Contact Status"].ToString()) == "No Longer there / Deceased")
                            ddlContactStatus.Text = "No Longer there";
                        else
                            ddlContactStatus.Text = CheckSpace(dr["Repdata Contact Status"].ToString());
                        ddlContFunc.Text = CheckSpace(dr["Repdata Function"].ToString());
                        ddlContBudget.Text = CheckSpace(dr["Repdata Budget"].ToString());
                        lblUpdatedByWho.Text = CheckSpace(dr["UPDATEDBY"].ToString());

                        if (CheckSpace(dr["VALID_FROM"].ToString()) == "")
                            lblUpdatedDateWhen.Text = "";
                        else
                            lblUpdatedDateWhen.Text = String.Format("{0:MMM dd, yyyy}", DateTime.Parse(CheckSpace(dr["VALID_FROM"].ToString())));

                        ddlSp.SelectedIndex = int.Parse(IsNumeric(CheckSpace(dr["SP"].ToString())));
                        if (CheckSpace(dr["q6"].ToString()) == "Other")
                            txtOther.Visible = true;
                        else
                            txtOther.Visible = false;
                        ddlFactor.Text = CheckSpace(dr["q6"].ToString());
                        txtOther.Text = CheckSpace(dr["q7"].ToString());
                        ddlPurchasing.Text = CheckSpace(dr["q8"].ToString());
                        ddlSpVendor.SelectedIndex = Function.ConvertNumeric(dr["q91113"].ToString());
                    }

                    //Secondary Question
                    txtContactNameSecond.Text = txtLastname.Text + ", " + txtFirstname.Text;

                    foreach (DataRow dr in dtContactInfo.Rows)
                    {
                        ddlFacility.SelectedIndex = Function.ConvertNumeric(dr["FSM"].ToString());
                        ddlFirst.SelectedIndex = Function.ConvertNumeric(dr["FAE"].ToString());
                        ddlLabels.SelectedIndex = Function.ConvertNumeric(dr["LBL"].ToString());
                        ddlLockout.SelectedIndex = Function.ConvertNumeric(dr["LO"].ToString());
                        ddlOffice.SelectedIndex = Function.ConvertNumeric(dr["OS"].ToString());
                        ddlPipe.SelectedIndex = Function.ConvertNumeric(dr["PVM"].ToString());
                        ddlPPE.SelectedIndex = Function.ConvertNumeric(dr["PPE"].ToString());
                        ddlPropertyID.SelectedIndex = Function.ConvertNumeric(dr["PI"].ToString());
                        ddlSafety.SelectedIndex = Function.ConvertNumeric(dr["SFS"].ToString());
                        ddlSafetyProducts.SelectedIndex = Function.ConvertNumeric(dr["SP"].ToString());
                        ddlSeals.SelectedIndex = Function.ConvertNumeric(dr["SLS"].ToString());
                        ddlSecurity.SelectedIndex = Function.ConvertNumeric(dr["SCP"].ToString());
                        ddlSpillControl.SelectedIndex = Function.ConvertNumeric(dr["SC"].ToString());
                        ddlTags.SelectedIndex = Function.ConvertNumeric(dr["TAGS"].ToString());
                        ddlTraffic.SelectedIndex = Function.ConvertNumeric(dr["TPS"].ToString());
                        ddlTrafficControl.SelectedIndex = Function.ConvertNumeric(dr["TC"].ToString());
                        ddlWarehouse.SelectedIndex = Function.ConvertNumeric(dr["W"].ToString());
                        ddlETO.SelectedIndex = Function.ConvertNumeric(dr["ETO"].ToString());
                        lblUpdatedByProductsWho.Text = CheckSpace(dr["P_assign_updatedby"].ToString());
                        if (CheckSpace(dr["P_assign_updatedon"].ToString()) == "")
                            lblLastUpdateDateProductsWhen.Text = "";
                        else
                            lblLastUpdateDateProductsWhen.Text = String.Format("{0:MMM dd, yyyy}", DateTime.Parse(CheckSpace(dr["P_assign_updatedon"].ToString())));
                    }

                    //Others
                    txtContactNameOther.Text = txtLastname.Text + ", " + txtFirstname.Text;

                    //Projects
                    txtContactNameProjects.Text = txtLastname.Text + ", " + txtFirstname.Text;

                    GetSafetyProducts();

                }
                else
                {
                    foreach (DataRow dr in dtContactInfo.Rows)
                    {
                        ddlNewContactStatus.Text = CheckSpace(dr["Repdata Contact Status"].ToString());
                        ddlJobArea.Text = CheckSpace(dr["Repdata Function"].ToString());
                        ddlHealth.Text = CheckSpace(dr["OHIE"].ToString());
                        ddlSpanish.Text = CheckSpace(dr["HSE"].ToString());
                        ddlEmployeeSize.Text = CheckSpace(dr["ES"].ToString());
                        ddlAnnual.Text = CheckSpace(dr["Repdata Budget"].ToString());
                        lblUpdatedbyPCWho.Text = CheckSpace(dr["PC_update_by"].ToString());
                        if (CheckSpace(dr["PC_update_on"].ToString()).Trim() == "")
                            lblDateWho.Text = "";
                        else
                        {
                            lblDateWho.Text = String.Format("{0:MMM dd, yyyy}",
                            DateTime.Parse(CheckSpace(dr["PC_update_on"].ToString())));
                        }

                    }
                }

            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gridSiteContactInfo', 'HeaderDiv');</script>");
        }

        protected void gridSafetyProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlSafetyProductsWhoElse = (DropDownList)e.Row.FindControl("ddlSafetyProductsWhoElse");
                Label lblStatusProductsWhoElse = (Label)e.Row.FindControl("Label39");
                if (lblStatusProductsWhoElse != null)
                {
                    if (lblStatusProductsWhoElse.Text == "0")
                        lblStatusProductsWhoElse.Text = "";
                    else if (lblStatusProductsWhoElse.Text == "1")
                        lblStatusProductsWhoElse.Text = "YES";
                    else if (lblStatusProductsWhoElse.Text == "2")
                        lblStatusProductsWhoElse.Text = "NO";
                }
                else if (ddlSafetyProductsWhoElse != null)
                {
                    if (ddlSafetyProductsWhoElse.Text == "0")
                        ddlSafetyProductsWhoElse.Text = "";
                    else if (ddlSafetyProductsWhoElse.Text == "1")
                        ddlSafetyProductsWhoElse.Text = "YES";
                    else if (ddlSafetyProductsWhoElse.Text == "2")
                        ddlSafetyProductsWhoElse.Text = "NO";
                }
            }

        }

        protected void gridSafetyProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "0")
                    e.Row.Cells[4].Text = "";
                else if (e.Row.Cells[4].Text == "1")
                    e.Row.Cells[4].Text = "YES";
                else
                    e.Row.Cells[4].Text = "NO";
            }
        }


        protected void hyperProjects_Click(object sender, EventArgs e)
        {
            tbconSiteContactInfo.ActiveTab = tabProjectsOthers;
        }

        protected void hyperProduct_Click(object sender, EventArgs e)
        {
            tbconSiteContactInfo.ActiveTab = tabProductPurchase;
        }

        protected void HyperOthers_Click(object sender, EventArgs e)
        {
            tbconSiteContactInfo.ActiveTab = tabProjectsOthers;
        }

        protected void SubmitMaster_Click(object sender, EventArgs e)
        {
            ModalPopupExtender4.Show();
        }

        #region Qualifying Question
        protected void UpdateQQ(object sender, EventArgs e)
        {
            if (gridSiteContactInfo.SelectedValue != null)
            {
                StatusButtonQQ(false);
                StatusControlQQ(true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "InformationMessage('Select a contact to be update.')", true);
            }
        }
        protected void CancelQQ(object sender, EventArgs e)
        {
            StatusControlQQ(false);
            StatusButtonQQ(true);
        }
        protected void SaveQQ(object sender, EventArgs e)
        {
            cUpdateQQ objUpdateQQ = new cUpdateQQ();
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            if (row != null)
            {
                objUpdateQQ.Account = SessionFacade.AccountNo;
                objUpdateQQ.Campaign = SessionFacade.CampaignValue;
                objUpdateQQ.Contact = GetContactNumber(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text);
                objUpdateQQ.ContactStatus = ddlContactStatus.Text;
                objUpdateQQ.ContBudget = ddlContBudget.Text;
                objUpdateQQ.ContFunc = ddlContFunc.Text;
                objUpdateQQ.Factor = ddlFactor.Text;
                objUpdateQQ.Other = txtOther.Text;
                objUpdateQQ.Purchasing = ddlPurchasing.Text;
                objUpdateQQ.SP = ddlSp.SelectedIndex;
                objUpdateQQ.SpVendor = ddlSpVendor.SelectedIndex;
                objUpdateQQ.Username = SessionFacade.LoggedInUserName;

                if (objUpdateQQ.UpdateQQ() == true)
                {
                    lblUpdatedByWho.Text = SessionFacade.LoggedInUserName;
                    lblUpdatedDateWhen.Text = DateTime.Now.ToString("MMM dd, yyyy");
                    RefreshSiteContactInfo();
                    GetContactChanges();
                    StatusControlQQ(false);
                    StatusButtonQQ(true);
                }
            }
        }
        protected void StatusControlQQ(bool enabled = true)
        {
            ddlContactStatus.Enabled = enabled;
            ddlFactor.Enabled = enabled;
            ddlContFunc.Enabled = enabled;
            ddlContBudget.Enabled = enabled;
            ddlSp.Enabled = enabled;
            ddlSpVendor.Enabled = enabled;
            ddlContBudget.Enabled = enabled;
            ddlPurchasing.Enabled = enabled;
            btnView.Enabled = enabled;
            txtOther.Enabled = enabled;

            hyperOtherVendors.Enabled = !enabled;
            hyperProduct.Enabled = !enabled;
            hyperProjects.Enabled = !enabled;
        }
        protected void StatusButtonQQ(bool enabled = true)
        {
            btnCancelQQ.Enabled = !enabled;
            btnSaveQQ.Enabled = !enabled;
            btnUpdateQQ.Enabled = enabled;
        }


        protected void UpdatePC(object sender, EventArgs e)
        {
            if (gridSiteContactInfo.SelectedValue != null)
            {
                StatusButtonPC(false);
                StatusControlPC(true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "InformationMessage('Select a contact to be update.')", true);
            }
        }
        protected void CancelPC(object sender, EventArgs e)
        {
            StatusControlPC(false);
            StatusButtonPC(true);
        }
        protected void SavePC(object sender, EventArgs e)
        {
            cUpdateQQPC objUpdateQQPC = new cUpdateQQPC();
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            if (row != null)
            {
                objUpdateQQPC.Account = SessionFacade.AccountNo;
                objUpdateQQPC.Campaign = SCddlTemp.ToString();
                objUpdateQQPC.Contact = GetContactNumber(row.Cells[WebHelper.GetColumnIndexOf(gridSiteContactInfo, "Contact Number") + 1].Text);
                objUpdateQQPC.ContBudgets = ddlContactStatus.Text;
                objUpdateQQPC.Health = ddlHealth.Text;
                objUpdateQQPC.Spanish = ddlSpanish.Text;
                objUpdateQQPC.EmployeeSize = ddlEmployeeSize.Text;
                objUpdateQQPC.ContBudgets = ddlAnnual.Text;
                objUpdateQQPC.ContStats = ddlNewContactStatus.Text;
                objUpdateQQPC.ContFunction = ddlJobArea.Text;
                objUpdateQQPC.Username = SessionFacade.LoggedInUserName;
                objUpdateQQPC.Qx = "";

                if (objUpdateQQPC.UpdateQQPC() == true)
                {
                    lblUpdatedbyPCWho.Text = SessionFacade.LoggedInUserName;
                    lblDateWho.Text = DateTime.Now.ToString("MMM dd, yyyy");
                    StatusControlPC(false);
                    StatusButtonPC(true);
                    RefreshSiteContactInfo();
                }
            }
        }
        protected void StatusControlPC(bool enabled)
        {
            ddlAnnual.Enabled = enabled;
            ddlHealth.Enabled = enabled;
            ddlJobArea.Enabled = enabled;
            ddlNewContactStatus.Enabled = enabled;
            ddlEmployeeSize.Enabled = enabled;
            ddlSpanish.Enabled = enabled;
        }
        protected void StatusButtonPC(bool enabled)
        {
            btnCancelPC.Enabled = !enabled;
            btnSavePC.Enabled = !enabled;
            btnUpdatePC.Enabled = enabled;
        }
        #endregion

        #region Produtcts
        protected void UpdateProducts(object sender, EventArgs e)
        {
            if (gridSiteContactInfo.SelectedValue != null)
            {
                StatusButtonProducts(false);
                StatusControlProducts(true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "InformationMessage('Select a contact to be update.')", true);
            }
        }
        protected void SaveProducts(object sender, EventArgs e)
        {
            cUpdateProducts objUpdateProducts = new cUpdateProducts();
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            if (row != null)
            {
                objUpdateProducts.Account = SCtxtTemp.ToString();
                objUpdateProducts.Contact = int.Parse(IsNumeric(CheckSpace(GetContactNumber(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text))));
                objUpdateProducts.FSM = ddlFacility.SelectedIndex;
                objUpdateProducts.FAE = ddlFirst.SelectedIndex;
                objUpdateProducts.LBL = ddlLabels.SelectedIndex;
                objUpdateProducts.LO = ddlLockout.SelectedIndex;
                objUpdateProducts.OS = ddlOffice.SelectedIndex;
                objUpdateProducts.PVM = ddlPipe.SelectedIndex;
                objUpdateProducts.PPE = ddlPPE.SelectedIndex;
                objUpdateProducts.PI = ddlPropertyID.SelectedIndex;
                objUpdateProducts.SFS = ddlSafety.SelectedIndex;
                objUpdateProducts.SP = ddlSafetyProducts.SelectedIndex;
                objUpdateProducts.SLS = ddlSeals.SelectedIndex;
                objUpdateProducts.SCP = ddlSecurity.SelectedIndex;
                objUpdateProducts.SC = ddlSpillControl.SelectedIndex;
                objUpdateProducts.TAGS = ddlTags.SelectedIndex;
                objUpdateProducts.TPS = ddlTraffic.SelectedIndex;
                objUpdateProducts.TC = ddlTrafficControl.SelectedIndex;
                objUpdateProducts.W = ddlWarehouse.SelectedIndex;
                objUpdateProducts.ETO = ddlETO.SelectedIndex;
                objUpdateProducts.Username = SessionFacade.LoggedInUserName;
                if (objUpdateProducts.UpdateProducts() == true)
                {
                    lblUpdatedByProductsWho.Text = SessionFacade.LoggedInUserName;
                    lblLastUpdateDateProductsWhen.Text = DateTime.Now.ToString("MMM dd, yyyy");
                    StatusControlProducts(false);
                    StatusButtonProducts(true);
                    RefreshSiteContactInfo();
                }
            }
        }
        protected void CancelProducts(object sender, EventArgs e)
        {
            StatusControlProducts(false);
            StatusButtonProducts(true);
        }
        protected void StatusControlProducts(bool enabled)
        {
            ddlFacility.Enabled = enabled;
            ddlPPE.Enabled = enabled;
            ddlSpillControl.Enabled = enabled;
            ddlFirst.Enabled = enabled;
            ddlPropertyID.Enabled = enabled;
            ddlTags.Enabled = enabled;
            ddlLabels.Enabled = enabled;
            ddlSafety.Enabled = enabled;
            ddlTraffic.Enabled = enabled;
            ddlLockout.Enabled = enabled;
            ddlSafetyProducts.Enabled = enabled;
            ddlTrafficControl.Enabled = enabled;
            ddlOffice.Enabled = enabled;
            ddlSeals.Enabled = enabled;
            ddlWarehouse.Enabled = enabled;
            ddlPipe.Enabled = enabled;
            ddlSecurity.Enabled = enabled;
            ddlETO.Enabled = enabled;
        }
        protected void StatusButtonProducts(bool enabled)
        {
            btnCancelProducts.Enabled = !enabled;
            btnSaveProducts.Enabled = !enabled;
            btnUpdateProducts.Enabled = enabled;
        }
        #endregion

        #region Secondary Question
        protected void btnAddSecond_Click(object sender, EventArgs e)
        {
            GridViewRow row = gridSiteContactInfo.SelectedRow;
            cSecondaryQ objSecondaryQ = new cSecondaryQ();

            if (row != null)
            {
                objSecondaryQ.Account = SessionFacade.AccountNo;
                objSecondaryQ.Campaign = SCddlTemp.ToString();
                objSecondaryQ.Q1 = txtQ12QQ.Text;
                objSecondaryQ.Q2 = txtQ22QQ.Text;
                objSecondaryQ.Q3 = txtQ32QQ.Text;
                objSecondaryQ.Q4 = txtQ42QQ.Text;
                objSecondaryQ.Q5 = txtQ52QQ.Text;
                objSecondaryQ.Username = SessionFacade.LoggedInUserName;
                objSecondaryQ.Contact = int.Parse(IsNumeric(CheckSpace(GetContactNumber(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text))));
                if (objSecondaryQ.AddSecondaryQ() == true)
                {
                    GetSecondaryQuestion();
                }
            }

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtQ12QQ.Text = string.Empty;
            txtQ22QQ.Text = string.Empty;
            txtQ32QQ.Text = string.Empty;
            txtQ42QQ.Text = string.Empty;
            txtQ52QQ.Text = string.Empty;
        }
        private DataSet BindSecondaryQuestion()
        {
            string[] TempAccount = new string[6];
            DataSet dsSecondQ = new DataSet();
            cSecondaryQ objSecondQ = new cSecondaryQ();
            try
            {
                objSecondQ.Account = SCtxtTemp.ToString();
                objSecondQ.Campaign = SCddlTemp.ToString();
                dsSecondQ = objSecondQ.GetcSecondaryQ();
                return dsSecondQ;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Secondary Question - BindingData");
                gridSiteContactInfo.DataSource = null;
                gridSiteContactInfo.DataBind();
                return null;
            }
        }
        #region GetSecondary Questions
        private void GetSecondaryQuestion()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName +
                "-SiteAndContactInfoSecondQ" + ".xml";
            DataSet ds = new DataSet();

            try
            {
                //if (SessionFacade.LastAccount[2] == txtTemp.Text.ToString())
                //{
                ds = BindSecondaryQuestion();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                gridSecondQQ.DataSource = ds;
                gridSecondQQ.DataBind();
                //}
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Secondary Question - Bind Gridview");
            }

        }
        #endregion

        protected SortDirection GridViewSortDirectionSecondaryQ
        {
            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridSecondaryQ_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirectionSecondaryQ == SortDirection.Ascending)
            {
                GridViewSortDirectionSecondaryQ = SortDirection.Descending;
                SortgridSecondaryQ(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirectionSecondaryQ = SortDirection.Ascending;
                SortgridSecondaryQ(sortExpression, "ASC");
            }
        }

        private void SortgridSecondaryQ(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
               Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName +
               "-SiteAndContactInfoSecondQ" + ".xml";
                    if (File.Exists(Pathname))
                    {
                        DataTable dt = new DataTable();
                        DataSet dsOthers = GetDatafromXMLSecondaryQ();
                        DataView dv = new DataView(dsOthers.Tables[0]);
                        DataSet ds = new DataSet();

                        dv.Sort = sortExpression + " " + direction;

                        //Copy Sorting Row to XML
                        dt = dv.ToTable();
                        ds.Tables.Add(dt);

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridSecondQQ.DataSource = dv;
                        gridSecondQQ.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private DataSet GetDatafromXMLSecondaryQ()
        {

            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                 Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName +
                 "-SiteAndContactInfoSecondQ" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }
        #endregion

        #region Other Vendors
        protected void btnOkOther_Click(object sender, EventArgs e)
        {
            cAddVendors objAddVendors = new cAddVendors();
            GridViewRow row = gridSiteContactInfo.SelectedRow;
            DataTable dt = new DataTable();
            if (row != null)
            {
                objAddVendors.Account = SessionFacade.AccountNo;
                objAddVendors.Campaign = SCddlTemp.ToString();
                objAddVendors.Contact = int.Parse(IsNumeric(CheckSpace(GetContactNumber(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text))));
                objAddVendors.Comments = txtCommentOther.Text;
                objAddVendors.VendorName = txtVendorName.Text;
                objAddVendors.Username = SessionFacade.LoggedInUserName;

                if (objAddVendors.AddVendors() == true)
                {
                    GetOtherVendors();
                }
            }
        }

        protected void btnClearOther_Click(object sender, EventArgs e)
        {
            txtVendorName.Text = string.Empty;
            txtCommentOther.Text = string.Empty;
            modalOther.Show();
        }

        private DataSet BindOtherVendors()
        {
            string[] TempAccount = new string[6];
            DataSet dsVendors = new DataSet();
            cAddVendors objVendors = new cAddVendors();
            try
            {
                objVendors.Account = SCtxtTemp.ToString();
                objVendors.Campaign = SCddlTemp.ToString();
                dsVendors = objVendors.GetVendors();
                return dsVendors;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Other Vendors - BindingData");
                return null;
            }
        }

        private void GetOtherVendors()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoOtherVendors" + ".xml";
            DataSet ds = new DataSet();
            try
            {
                ds = BindOtherVendors();

                //Write XML
                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                gridOtherVendors.DataSource = ds;
                gridOtherVendors.DataBind();
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Other Vendors - Bind Gridview");
            }
        }

        protected SortDirection GridViewSortDirectionOthers
        {
            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridOtherVendors_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirectionOthers == SortDirection.Ascending)
            {
                GridViewSortDirectionOthers = SortDirection.Descending;
                SortgridOtherVendors(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirectionOthers = SortDirection.Ascending;
                SortgridOtherVendors(sortExpression, "ASC");
            }
        }

        private void SortgridOtherVendors(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                    Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoOtherVendors" + ".xml";
                    if (File.Exists(Pathname))
                    {
                        DataTable dt = new DataTable();
                        DataSet dsOthers = GetDatafromXMLOtherVendors();
                        DataView dv = new DataView(dsOthers.Tables[0]);
                        DataSet ds = new DataSet();

                        dv.Sort = sortExpression + " " + direction;

                        //Copy Sorting Row to XML
                        dt = dv.ToTable();
                        ds.Tables.Add(dt);

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridOtherVendors.DataSource = dv;
                        gridOtherVendors.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private DataSet GetDatafromXMLOtherVendors()
        {

            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoOtherVendors" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }

        #endregion

        #region Projects
        protected void btnOk_Click1(object sender, EventArgs e)
        {
            cProjects objProjects = new cProjects();
            GridViewRow row = gridSiteContactInfo.SelectedRow;
            DataSet ds = new DataSet();
            if (row != null)
            {
                objProjects.Account = SessionFacade.AccountNo;
                objProjects.Campaign = SCddlTemp.ToString();
                objProjects.Contact = int.Parse(IsNumeric(CheckSpace(GetContactNumber(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text))));
                objProjects.Chance = ddlChance.Text;
                objProjects.EstimatedAmt = int.Parse(IsNumeric(CheckSpace(txtEstimatedAmt.Text)).ToString().Replace(",", ""));
                DateTime temp;
                if (DateTime.TryParse(txtDateProject.Text.ToString(), out temp) == true)
                    objProjects.ProjectDate = txtDateProject.Text.ToString();
                else
                    objProjects.ProjectDate = string.Empty;
                objProjects.ProjectType = txtProjectType.Text.ToString();
                objProjects.Username = SessionFacade.LoggedInUserName;
                if (objProjects.AddProjects() == true)
                {
                    GetProjects();
                    ModalPopupExtender1.Hide();
                }
            }
        }

        protected void btnClearProjects_Click(object sender, EventArgs e)
        {
            txtDateProject.Text = string.Empty;
            txtProjectType.Text = string.Empty;
            ddlChance.Text = "";
            txtEstimatedAmt.Text = string.Empty;
            ModalPopupExtender1.Show();
        }

        private DataSet BindProjects()
        {
            string[] TempAccount = new string[6];
            DataSet dsProjects = new DataSet();
            cProjects objProjects = new cProjects();
            try
            {
                objProjects.Account = SCtxtTemp.ToString();
                objProjects.Campaign = SCddlTemp.ToString();
                dsProjects = objProjects.GetProjects();

                return dsProjects;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Other Vendors - BindingData");
                return null;
            }
        }

        private void GetProjects()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoProjects" + ".xml";
            DataSet ds = new DataSet();
            string[] TempAccount = new string[6];
            try
            {
                ds = BindProjects();

                gridProjects.DataSource = null;
                gridProjects.DataBind();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                gridProjects.DataSource = ds;
                gridProjects.DataBind();
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Projects - Bind Gridview");
            }
        }

        protected SortDirection GridViewSortDirectionProjects
        {
            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridProjects_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (SCGridViewSortDirection == SortDirection.Ascending)
            {
                SCGridViewSortDirection = SortDirection.Descending;
                SortgridProjects(sortExpression, "DESC");
            }
            else
            {
                SCGridViewSortDirection = SortDirection.Ascending;
                SortgridProjects(sortExpression, "ASC");
            }
        }

        private void SortgridProjects(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                    Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoProjects" + ".xml";
                    if (File.Exists(Pathname))
                    {
                        DataTable dt = new DataTable();
                        DataSet dsProjects = GetDatafromXMLProjects();
                        DataView dv = new DataView(dsProjects.Tables[0]);
                        DataSet ds = new DataSet();

                        dv.Sort = sortExpression + " " + direction;

                        //Copy Sorting Row to XML
                        dt = dv.ToTable();
                        ds.Tables.Add(dt);

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridProjects.DataSource = dv;
                        gridProjects.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private DataSet GetDatafromXMLProjects()
        {

            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar
            + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoProjects" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }

        #endregion

        #region New Contact
        protected void btnOkNew_Click(object sender, EventArgs e)
        {
            cAddNewContact objAddNewContact = new cAddNewContact();

            objAddNewContact.Account = SessionFacade.AccountNo;
            objAddNewContact.Campaign = SCddlTemp.ToString();
            objAddNewContact.Email = txtEmailNewContact.Text;
            objAddNewContact.Firstname = txtFirstnameNewContact.Text;
            objAddNewContact.Name = txtSiteName.Text;
            objAddNewContact.Notes = txtOtherNewContact.Text;
            objAddNewContact.Phone = txtPhoneNewContact.Text;
            objAddNewContact.Surname = txtLastanameNewContact.Text;
            objAddNewContact.Title = ddlFunctionNewContact.Text;
            objAddNewContact.Username = SessionFacade.LoggedInUserName;
            if (objAddNewContact.AddNewContacts() == true)
            {
                RefreshSiteContactInfo();
                ClearContacts();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "Information()", true);
                SCModalPopupExtender2.Show();
            }
        }

        protected void Show_NewContact(object sender, EventArgs e)
        {
            string[] x;

            if (HiddenField1.Value != "0")
            {
                x = HiddenField1.Value.ToString().Split(';');
                SCModalPopupExtender2.X = int.Parse(x[0]);
                SCModalPopupExtender2.Y = int.Parse(x[1]);
            }
            SCModalPopupExtender2.Show();
        }
        #endregion

        #region View
        protected void btnOkView_Click(object sender, EventArgs e)
        {

        }

        protected void gridSafetyProducts_SelectedChanged(object sender, EventArgs e)
        {

        }

        private void SCOpenPopup()
        {
            string[] x;
            pnlSafety.Visible = true;
            if (tempSafety.Value != "0")
            {
                x = tempSafety.Value.ToString().Split(';');
                ModalPopupExtender3.X = int.Parse(x[0]);
                ModalPopupExtender3.Y = int.Parse(x[1]);
            }
            ModalPopupExtender3.Show();
        }

        protected void Search_Product(object sender, EventArgs e)
        {
            gridSafetyProducts.DataSource = GetDatafromXMLData(txtSearchContactSafetyProducts.Text);
            gridSafetyProducts.DataBind();
            SCOpenPopup();
        }


        private DataSet BindSafetyProducts()
        {
            string[] TempAccount = new string[6];
            DataSet dsSafetyProducts = new DataSet();
            cSafetyProducts objSafetyProducts = new cSafetyProducts();
            try
            {
                objSafetyProducts.Account = SCtxtTemp.ToString();
                objSafetyProducts.Campaign = SCddlTemp.ToString();
                dsSafetyProducts = objSafetyProducts.GetSafetyProducts();
                return dsSafetyProducts;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Safety Products - BindingData");
                return null;
            }
        }

        private void GetSafetyProducts()
        {
            GridViewRow row = gridSiteContactInfo.SelectedRow;
            try
            {
                if (row != null)
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                        Path.DirectorySeparatorChar + "XMLFiles\\" +
                        SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
                    if (File.Exists(Pathname))
                    {
                        if (row != null)
                        {
                            gridSafetyProducts.DataSource = GetDatafromXML(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text.ToString());
                            gridSafetyProducts.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Projects - Bind Gridview");
            }
        }

        protected void rdoContactFirstname_Checked(object sender, EventArgs e)
        {
            SessionFacade.NameOrContacts = "firstname";
            SCOpenPopup();
        }

        protected void rdoContactSurname_Checked(object sender, EventArgs e)
        {
            SessionFacade.NameOrContacts = "surname";
            SCOpenPopup();
        }

        #region AutoComplete
        public DataTable GetDatafromXMLData(string txtSearchContactSafetyProducts)
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                        Path.DirectorySeparatorChar + "XMLFiles\\" +
                        SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
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
            if (SessionFacade.NameOrContacts == "firstname")
            {
                Query = Query + " and [First Name] like '%" +
                        txtSearchContactSafetyProducts + "%'";
            }
            else if (SessionFacade.NameOrContacts == "surname")
            {
                Query = Query + " and [Last Name] like '%" +
                        txtSearchContactSafetyProducts + "%'";
            }

            results = ds.Tables[0].Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;
        }

        public string CheckRadioButton()
        {
            if (SessionFacade.NameOrContacts == "firstname")
                return "firstname";
            else if (SessionFacade.NameOrContacts == "surname")
                return "surname";
            else
                return string.Empty;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count)
        {
            DataTable dt;

            Multiview m = new Multiview();

            dt = m.GetDatafromXMLData(prefixText);

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                if (m.CheckRadioButton() == "firstname")
                {
                    dbValues = row["First Name"].ToString();
                    dbValues = dbValues.ToLower();
                    txtItems.Add(dbValues);
                }
                else if (m.CheckRadioButton() == "surname")
                {
                    dbValues = row["Last Name"].ToString();
                    dbValues = dbValues.ToLower();
                    txtItems.Add(dbValues);
                }
            }

            return txtItems.ToArray();
        }

        #endregion

        protected void View_Click(object sender, EventArgs e)
        {
            SCOpenPopup();
        }

        #endregion

        #region Preferences
        protected void btnSavePreferences_Click(object sender, EventArgs e)
        {
            cAddPreferences objAddPreferences = new cAddPreferences();

            if (rdoMail.Text != "")
                objAddPreferences.Mail = rdoMail.Text;
            else
                objAddPreferences.Mail = "";

            if (rdoFax.Text != "")
                objAddPreferences.Fax = rdoFax.Text;
            else
                objAddPreferences.Fax = "";

            if (rdoEmail.Text != "")
                objAddPreferences.Email = rdoEmail.Text;
            else
                objAddPreferences.Email = "";

            if (rdoPhone.Text != "")
                objAddPreferences.Phone = rdoPhone.Text;
            else
                objAddPreferences.Phone = "";


            objAddPreferences.Account = SessionFacade.AccountNo;
            objAddPreferences.Campaign = SCddlTemp.ToString();
            objAddPreferences.Username = SessionFacade.LoggedInUserName;
            objAddPreferences.Contact = int.Parse(IsNumeric(CheckSpace(txtContactNumberMasterChange.Text)));
            objAddPreferences.ContactName = txtContactNameMasterChange.Text;

            if (objAddPreferences.AddPreferences() == true)
            {
                GetPreferences();
                ModalPopupExtender4.Show();
            }

        }

        protected void btnCancelPreferences_Click(object sender, EventArgs e)
        {

        }

        private DataSet BindPreferences()
        {
            DataSet dsPreferences = new DataSet();
            cAddPreferences objPreferences = new cAddPreferences();
            try
            {
                objPreferences.Account = SCtxtTemp.ToString();
                objPreferences.Campaign = SCddlTemp.ToString();
                dsPreferences = objPreferences.SelectPreferences();
                return dsPreferences;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Preferences - BindingData");
                return null;
            }
        }

        private void GetPreferences()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoPreferences" + ".xml";
            DataSet ds = new DataSet();

            try
            {
                ds = BindPreferences();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                gridContactPreferences.DataSource = ds;
                gridContactPreferences.DataBind();
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Preferences - Bind Gridview");
            }
        }

        //Sorting
        protected SortDirection GridViewSortDirectionPreferences
        {
            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridPreferences_Sorting(object sender, GridViewSortEventArgs e)
        {
            int x, y;
            string sortExpression = e.SortExpression;
            if (GridViewSortDirectionPreferences == SortDirection.Ascending)
            {
                SCGridViewSortDirection = SortDirection.Descending;
                SortgridPreferences(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirectionPreferences = SortDirection.Ascending;
                SortgridPreferences(sortExpression, "ASC");
            }
            x = ModalPopupExtender4.X;
            y = ModalPopupExtender4.Y;
            ModalPopupExtender4.X = x;
            ModalPopupExtender4.Y = y;
            ModalPopupExtender4.Show();
        }

        private void SortgridPreferences(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                    Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoPreferences" + ".xml";
                    if (File.Exists(Pathname))
                    {
                        DataTable dt = new DataTable();
                        DataSet dsProjects = GetDatafromXMLPreferences();
                        DataView dv = new DataView(dsProjects.Tables[0]);
                        DataSet ds = new DataSet();

                        dv.Sort = sortExpression + " " + direction;

                        //Copy Sorting Row to XML
                        dt = dv.ToTable();
                        ds.Tables.Add(dt);

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridContactPreferences.DataSource = dv;
                        gridContactPreferences.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        private DataSet GetDatafromXMLPreferences()
        {

            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoPreferences" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }
        #endregion

        #region Account Changes
        protected void SaveAccountChanges_Click(object sender, EventArgs e)
        {
            cAddAccountChanges objAddAccountChanges = new cAddAccountChanges();

            objAddAccountChanges.Account = SessionFacade.AccountNo;

            if (txtAccountNameChanges.Text.ToString().Trim() !=
                txtAccountNameMasterChange.Text.ToString().Trim())
            {
                objAddAccountChanges.AccountName = txtAccountNameChanges.Text;
            }
            else
                objAddAccountChanges.AccountName = string.Empty;

            objAddAccountChanges.Address1 = txtAddress1AccountChanges.Text;
            objAddAccountChanges.Address2 = txtAddress2AccountChanges.Text;
            objAddAccountChanges.City = txtCityAccountChanges.Text;
            objAddAccountChanges.Comment = txtCommentAccountChanges.Text;
            objAddAccountChanges.Country = ddlCountry.Text;
            objAddAccountChanges.Fax = txtFaxNumberAccountChanges.Text;
            objAddAccountChanges.Phone = txtPhoneNumberAccountChanges.Text;
            objAddAccountChanges.State = txtStateAccountChanges.Text;
            objAddAccountChanges.Username = SessionFacade.LoggedInUserName;
            objAddAccountChanges.Zip = txtZipAccountChanges.Text;

            if (objAddAccountChanges.AddAccountChanges() == true)
            {
                GetAccountChanges();
                ModalPopupExtender4.Show();
            }
        }
        protected void CancelAccountChanges_Click(object sender, EventArgs e)
        {

        }
        private DataSet BindAccountChanges()
        {
            DataSet dsAccountChanges = new DataSet();
            cAddAccountChanges objAccountChanges = new cAddAccountChanges();
            try
            {
                objAccountChanges.Account = SCtxtTemp.ToString();
                dsAccountChanges = objAccountChanges.SelectAccountChanges();
                return dsAccountChanges;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Account Changes - BindingData");
                return null;
            }
        }
        private void GetAccountChanges()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoAccountChanges" + ".xml";
            DataSet ds = new DataSet();
            try
            {
                ds = BindAccountChanges();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                gridAccountChanges.DataSource = ds;
                gridAccountChanges.DataBind();
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Account Changes - Bind Gridview");
            }
        }
        //Sorting
        protected SortDirection GridViewSortDirectionAccountChanges
        {
            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }
        protected void gridAccountChanges_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirectionAccountChanges == SortDirection.Ascending)
            {
                GridViewSortDirectionAccountChanges = SortDirection.Descending;
                SortgridAccountChanges(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirectionAccountChanges = SortDirection.Ascending;
                SortgridAccountChanges(sortExpression, "ASC");
            }
            ModalPopupExtender4.Show();
        }
        private void SortgridAccountChanges(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
             Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoAccountChanges" + ".xml";
                    if (File.Exists(Pathname))
                    {
                        DataTable dt = new DataTable();
                        DataSet dsProjects = GetDatafromXMLAccountChanges();
                        DataView dv = new DataView(dsProjects.Tables[0]);
                        DataSet ds = new DataSet();

                        dv.Sort = sortExpression + " " + direction;

                        //Copy Sorting Row to XML
                        dt = dv.ToTable();
                        ds.Tables.Add(dt);

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridAccountChanges.DataSource = dv;
                        gridAccountChanges.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        private DataSet GetDatafromXMLAccountChanges()
        {

            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoAccountChanges" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }
        #endregion

        #region Contact Changes
        protected void SaveContactChanges_Click(object sender, EventArgs e)
        {
            cAddContactChanges objAddContactChanges = new cAddContactChanges();
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            //Firstname
            if (txtFirstname.Text != txtFirstnameContactChanges.Text.ToString().Trim())
                objAddContactChanges.Firstname = txtFirstnameContactChanges.Text;
            else
                objAddContactChanges.Firstname = string.Empty;

            //Lastname
            if (txtLastname.Text != txtLastnameContactChanges.Text.ToString().Trim())
                objAddContactChanges.Lastname = txtLastnameContactChanges.Text;
            else
                objAddContactChanges.Lastname = string.Empty;

            objAddContactChanges.Account = SessionFacade.AccountNo;
            objAddContactChanges.Comment = txtCommentContactChanges.Text;
            objAddContactChanges.Contact = int.Parse(IsNumeric(CheckSpace(GetContactNumber(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text))));
            objAddContactChanges.Department = txtDepartmentContactChanges.Text;
            objAddContactChanges.Email = txtEmailAddressContactChanges.Text;
            objAddContactChanges.Function = ddlFunctionContactChanges.Text;
            objAddContactChanges.Phone = txtPhoneContactChanges.Text;
            objAddContactChanges.PhoneExt = txtPhoneExtContactChanges.Text;
            objAddContactChanges.Status = ddlStatusContactChanges.Text;
            objAddContactChanges.Title = txtTitleContactChanges.Text;
            objAddContactChanges.Username = SessionFacade.LoggedInUserName;

            if (objAddContactChanges.AddContactChanges() == true)
            {
                GetContactChanges();
                ModalPopupExtender4.Show();
            }
        }
        protected void CancelContactChanges_Click(object sender, EventArgs e)
        {

        }
        private DataSet BindContactChanges()
        {
            DataSet dsContactChanges = new DataSet();
            cAddContactChanges objContactChanges = new cAddContactChanges();
            try
            {
                objContactChanges.Account = SCtxtTemp.ToString();
                dsContactChanges = objContactChanges.SelectContactChanges();
                return dsContactChanges;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Contact Changes - BindingData");
                return null;
            }
        }
        private void GetContactChanges()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName +
            "-SiteAndContactInfoContactChanges" + ".xml";
            DataSet ds = new DataSet();
            try
            {
                ds = BindContactChanges();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                gridContactChanges.DataSource = ds;
                gridContactChanges.DataBind();
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Contact Changes - Bind Gridview");
            }
        }
        //Sorting
        protected SortDirection GridViewSortDirectionContactChanges
        {
            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }
        protected void gridContactChanges_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirectionContactChanges == SortDirection.Ascending)
            {
                GridViewSortDirectionContactChanges = SortDirection.Descending;
                SortgridContactChanges(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirectionContactChanges = SortDirection.Ascending;
                SortgridContactChanges(sortExpression, "ASC");
            }
            ModalPopupExtender4.Show();
        }
        private void SortgridContactChanges(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                    Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName +
                    "-SiteAndContactInfoContactChanges" + ".xml";
                    if (File.Exists(Pathname))
                    {
                        DataTable dt = new DataTable();
                        DataSet dsProjects = GetDatafromXMLContactChanges();
                        DataView dv = new DataView(dsProjects.Tables[0]);
                        DataSet ds = new DataSet();

                        dv.Sort = sortExpression + " " + direction;

                        //Copy Sorting Row to XML
                        dt = dv.ToTable();
                        ds.Tables.Add(dt);

                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        gridContactChanges.DataSource = dv;
                        gridContactChanges.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        private DataSet GetDatafromXMLContactChanges()
        {

            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
           Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName +
           "-SiteAndContactInfoContactChanges" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }
        #endregion

        #region Row Edit
        protected void gridSafetyProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            DataSet ds = BindSiteContactInfo();
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            gridSafetyProducts.EditIndex = e.NewEditIndex;

            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            gridSafetyProducts.DataSource = GetDatafromXML(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text.ToString());
            gridSafetyProducts.DataBind();

            SCOpenPopup();
        }
        #endregion

        #region Row Updating
        protected void gridSafetyProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            DropDownList ddlSPSafety = (DropDownList)gridSafetyProducts.Rows[e.RowIndex].FindControl("ddlSafetyProductsWhoElse");
            Label lblContact = (Label)gridSafetyProducts.Rows[e.RowIndex].FindControl("lblContactSafety");
            DataSet ds = new DataSet();
            GridViewRow row = gridSiteContactInfo.SelectedRow;
            int Contact = int.Parse(GetContactNumber(lblContact.Text.ToString().Trim()));

            cSafetyProducts objSafetyProducts = new cSafetyProducts();
            objSafetyProducts.Account = SessionFacade.AccountNo;
            objSafetyProducts.Contact = Contact;
            objSafetyProducts.Sp = ddlSPSafety.SelectedIndex;
            objSafetyProducts.Username = SessionFacade.LoggedInUserName;

            if (objSafetyProducts.UpdateSafetyProducts() == true)
            {
                ds = BindSiteContactInfo();
                gridSiteContactInfo.DataSource = ds;
                gridSiteContactInfo.DataBind();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                gridSafetyProducts.EditIndex = -1;
                gridSafetyProducts.DataSource = GetDatafromXML(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text.ToString());
                gridSafetyProducts.DataBind();

            }
            SCOpenPopup();
        }
        #endregion

        #region Cancel Edit Click on Grid
        protected void gridSafetyProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Label lblContact = (Label)gridSafetyProducts.Rows[e.RowIndex].FindControl("lblContactSafety");
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            gridSafetyProducts.EditIndex = -1;

            gridSafetyProducts.DataSource = GetDatafromXML(row.Cells[GetColumnIndexByName(row, "Contact Number")].Text.ToString());
            gridSafetyProducts.DataBind();

            SCOpenPopup();
        }
        #endregion

        protected void btnShowSiteContactInfo_Click(object sender, EventArgs e)
        {
            if (lblTitleSiteContactInfo.Text == "-")
            {
                this.cpeSiteContactInfo.Collapsed = true;
                this.cpeSiteContactInfo.ClientState = "true";
                lblTitleSiteContactInfo.Text = "+";
                if (lblTitleContactInfo.Text == "+")
                    SCDataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 588px;");
                else
                    SCDataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 386px;");

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gridSiteContactInfo', 'HeaderDiv');</script>");
            }
            else
            {
                this.cpeSiteContactInfo.Collapsed = false;
                this.cpeSiteContactInfo.ClientState = "false";
                lblTitleSiteContactInfo.Text = "-";
                if (lblTitleContactInfo.Text == "+")
                    SCDataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 422px;");
                else
                    SCDataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 220px;");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gridSiteContactInfo', 'HeaderDiv');</script>");
            }
        }

        protected void btnTitleContactInfo_Click(object sender, EventArgs e)
        {
            if (lblTitleContactInfo.Text == "-")
            {
                this.cpeTabsSiteContact.Collapsed = true;
                this.cpeTabsSiteContact.ClientState = "true";
                lblTitleContactInfo.Text = "+";
                if (lblTitleSiteContactInfo.Text == "+")
                    SCDataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 588px;");
                else
                    SCDataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 422px;");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gridSiteContactInfo', 'HeaderDiv');</script>");
            }
            else
            {
                this.cpeTabsSiteContact.Collapsed = false;
                this.cpeTabsSiteContact.ClientState = "false";
                lblTitleContactInfo.Text = "-";
                if (lblTitleSiteContactInfo.Text == "+")
                    SCDataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 386px;");
                else
                    SCDataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 220px;");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gridSiteContactInfo', 'HeaderDiv');</script>");
            }
        }
        #endregion

        #region Show Order History Function
        protected void ShowOrderHistoryPage()
        {
           // OHSetButtonVisible();
            if (!IsPostBack)
            {
                if (OHtxtTemp.ToString().Trim() != "")
                    ShowOrder();
                else
                {

                }

                StatusSearchDateRange(false);
                StatusSearchYear(false);
                InsertCalendarYearToDropDown();
            }
            else
            {
                if (OHtxtTemp.ToString().Trim() != "")
                    ShowOrder();
                else
                {
                    gridOrderHistory.DataSource = null;
                    gridOrderHistory.DataBind();

                }
            }

        }

        #region Enable Disable Buttons on the page
        protected void OHSetButtonVisible()
        {
            if (SessionFacade.UserRole == "ADMIN")
            {
                btnExportToExcel.Visible = true;
                BtnArrangeColumn.Visible = true;
            }
            else
            {
                btnExportToExcel.Visible = false;
                BtnArrangeColumn.Visible = false;
            }
        }
        #endregion

        protected void btnShowOrders_Click(object sender, EventArgs e)
        {

            if (ByDate.Checked == true)
            {
                if (txtStartDate.Text == "")
                    litErrorinGrid.Text = "Select Start Date.";
                else if (txtEndDate.Text == "")
                    litErrorinGrid.Text = "Select End Date.";
                else
                    ShowOrder();
            }
            else
            {
                ShowOrder();
            }
        }

        protected void ByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ByDate.Checked == true)
            {
                ByCal.Checked = false;
                StatusSearchDateRange(true);
                StatusSearchYear(false);
            }
            else
                StatusSearchDateRange(false);
        }

        protected void ByCal_CheckedChanged(object sender, EventArgs e)
        {
            if (ByCal.Checked == true)
            {
                ByDate.Checked = false;
                txtStartDate.Text = string.Empty;
                txtEndDate.Text = string.Empty;
                StatusSearchDateRange(false);
                StatusSearchYear(true);
            }
            else
            {
                StatusSearchYear(false);
            }
        }

        protected void gridOrderHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool varColumnExist = false;
            int columnIndex;
            string[] list = { "Unit Price", "Ext Price","QTY",
                                "Line" };

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].CssClass = "locked";
                //e.Row.Cells[1].CssClass = "locked";
                //e.Row.Cells[2].CssClass = "locked";

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


                if (OHDateOrdinal != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Order Date"].ToString(), out temp) == true)
                        e.Row.Cells[OHDateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Order Date"]).ToString("MM/dd/yyyy");
                }
                if (OHUnitPrice != 1000)
                {
                    decimal temp;
                    if (decimal.TryParse(((DataRowView)e.Row.DataItem)["Unit Price"].ToString(), out temp) == true)
                        e.Row.Cells[OHUnitPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["Unit Price"].ToString()).ToString("C2");
                }
                if (OHExtPrice != 1000)
                {
                    decimal temp;
                    if (decimal.TryParse(((DataRowView)e.Row.DataItem)["Ext Price"].ToString(), out temp) == true)
                        e.Row.Cells[OHExtPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["Ext Price"].ToString()).ToString("C2");
                }
                if (OHConvertedDate != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Converted Date"].ToString(), out temp) == true)
                        e.Row.Cells[OHConvertedDate].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Converted Date"]).ToString("MM/dd/yyyy");
                }
            }
        }

        protected void ddiYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOrder();
        }

        #region Function



        private void GetTotalOrdersSales(DataTable dt)
        {
            FunctionVb Function = new FunctionVb();
            decimal sum = 0;
            DataView view;

            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            //Check Standard Order Only
            Query = "[Order Type]='Standard Order' and [Reason Rejection] Is Null and uvals='C'";

            dtTemp = dt.Clone();

            results = dt.Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            //Total Sales
            foreach (DataRow dr in dtTemp.Rows)
                sum += Function.ConvertDecimal(dr[6].ToString().Trim());

            txtbTatalSales.Text = decimal.Parse(sum.ToString()).ToString("C2");

            //Total Orders
            view = new DataView(dtTemp);

            dtTemp = view.ToTable(true, "Order Number");

            txtbTotalOrders.Text = dtTemp.Rows.Count.ToString();

        }

        public DataSet BindOrderHistory()
        {
            string[] TempAccount = new string[6];
            DataSet drExisting;
            try
            {
                cOrderHistory objOrderHistory = new cOrderHistory();

                //Campaign
                objOrderHistory.SearchOrderCampaignName = OHddlTemp.ToString().Trim();

                //Search by Account
                objOrderHistory.SearchOrderAccount = OHtxtTemp.ToString().Trim();

                drExisting = objOrderHistory.GetOrderHistory();
                DataSet ReturnDs = null;
                if (drExisting.Tables.Count > 0)
                {
                    TempAccount = SessionFacade.LastAccount;

                    TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();

                    SessionFacade.LastAccount = TempAccount;

                    ReturnDs = drExisting;

                    OHDateOrdinal = ReturnDs.Tables[0].Columns["Order Date"].Ordinal;

                    OHUnitPrice = ReturnDs.Tables[0].Columns["Unit Price"].Ordinal;

                    OHExtPrice = ReturnDs.Tables[0].Columns["Ext Price"].Ordinal;

                    OHConvertedDate = ReturnDs.Tables[0].Columns["Converted Date"].Ordinal;

                    OHuval = ReturnDs.Tables[0].Columns["uvals"].Ordinal;

                }
                else
                {
                    ReturnDs = null;
                }



                return ReturnDs;

            }
            catch (Exception ex)
            {
                OHDateOrdinal = 1000;
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Order History - BindingData");
                litErrorinGrid.Text = "There was a error while retrieving Data";
                gridOrderHistory.DataSource = null;
                gridOrderHistory.DataBind();
                return null;
            }
        }


        protected System.Data.DataTable DtOrders
        {
            get // a DataTable filled in any way, for example:
            {

                CustomerLookup cCustLookup = new CustomerLookup();
                DataSet ds;
                DataTable dtTemp = new DataTable();

                var datable = new System.Data.DataTable();


                cCustLookup.CampaignName = SessionFacade.CampaignName.ToString().Trim();

                try
                {
                    datable = Orders();//cCustLookup.GetCustomerLookUp().Tables[0];

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
                        if (datable.Columns[i].Caption.Trim() == "Order Date" ||
                        datable.Columns[i].Caption.Trim() == "Converted Date")
                        {
                            if (DateTime.TryParse(dr[i].ToString(), out temp) == false)
                            {
                                array[i] = default(DateTime);

                                //string.Format(String.Format("{0:MM/dd/yy}",array[i]);
                            }
                            else
                                array[i] = dr[i];
                        }
                        else
                            array[i] = dr[i];
                    }
                    dt.Rows.Add(array);
                }


                return dt;
            }
        }

        public DataTable Orders()
        {
            DataTable dtOrders = new DataTable();

            SessionFacade.CustomerLookUp = "";
            SessionFacade.OrderLookUp = "";
            DataTable dtTemp = new DataTable();
            string[] TempAccount = new string[6];
            DataSet ds = new DataSet();
            DataSet dsTotalOrderSales = new DataSet();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";

            if ((SessionFacade.LastAccount[0] == OHtxtTemp.ToString())
                &&
               (OHddlTemp.ToString() == SessionFacade.CampaignValue))
            {
                dtTemp = GetDatafromXML();

                if (dtTemp == null)
                {
                    ds = BindOrderHistory();

                    //if (ds != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    dtTemp = GetDatafromXML();
                    //}
                }

                if (dtTemp.Rows.Count > 0)
                {
                    DataSet ReArrangeDs = new DataSet();
                    ReArrangeDs.Tables.Add(dtTemp);

                    GetTotalOrdersSales(dtTemp);

                    //Working
                    if (ReArrangeDs.Tables[0].Columns.Contains("uvals") == true)
                        ReArrangeDs.Tables[0].Columns.Remove("uvals");

                    gridOrderHistory.DataSource = OHArrangeColumn(ReArrangeDs);
                    gridOrderHistory.DataBind();
                    dtOrders = OHArrangeColumn(ReArrangeDs).Tables[0];
                    litErrorinGrid.Text = "";
                    if (ReArrangeDs.Tables[0].Rows.Count > 0)
                    {

                        litErrorinGrid.Text = "";
                    }
                    else
                    {

                        litErrorinGrid.Text = "No Record Found";
                    }
                }
                else
                {
                    gridOrderHistory.DataSource = null;
                    gridOrderHistory.DataBind();
                }

            }
            else
            {
                ds = BindOrderHistory();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtTemp = GetDatafromXML();

                    GetTotalOrdersSales(dtTemp);

                    DataSet ReArrangeDs = new DataSet();
                    ReArrangeDs.Tables.Add(dtTemp);
                    if (OHuval != 1000)
                    {
                        ReArrangeDs.Tables[0].Columns.Remove("uvals");
                    }

                    ReArrangeDs = OHArrangeColumn(ReArrangeDs);

                    //Working

                    ReArrangeDs.AcceptChanges();

                    gridOrderHistory.DataSource = ReArrangeDs;

                    gridOrderHistory.DataBind();

                    //passing datatable to return value
                    dtOrders = ReArrangeDs.Tables[0];

                    if (ReArrangeDs.Tables[0].Rows.Count > 0)
                    {
                        litErrorinGrid.Text = "";
                    }
                    else
                    {
                        litErrorinGrid.Text = "No Record Found";
                    }

                    try
                    {
                        string strAccntNo = ds.Tables[0].Rows[0]["Account Number"].ToString().Trim();

                        if (strAccntNo == SessionFacade.AccountNo.ToString().Trim())
                        {
                            SessionFacade.AccountNo = strAccntNo;
                            TempAccount = SessionFacade.LastAccount;
                            TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();
                            SessionFacade.LastAccount = TempAccount;
                        }
                        else
                        {
                            SessionFacade.AccountNo = "";
                            TempAccount = SessionFacade.LastAccount;
                            TempAccount[0] = "";
                            SessionFacade.LastAccount = TempAccount;
                        }
                    }
                    catch
                    {
                        SessionFacade.AccountNo = "";
                        TempAccount = SessionFacade.LastAccount;
                        TempAccount[0] = "";
                        SessionFacade.LastAccount = TempAccount;
                    }



                }


                else
                {
                    //   SessionFacade.AccountNo = "";

                    gridOrderHistory.DataSource = null;
                    gridOrderHistory.DataBind();
                    txtbTotalOrders.Text = "0";
                    txtbTatalSales.Text = "$0.00";
                    litErrorinGrid.Text = "No Record Found";

                    TempAccount = SessionFacade.LastAccount;
                    TempAccount[0] = "";
                    SessionFacade.LastAccount = TempAccount;
                }


            }

            return dtOrders;
        }



    public void ShowOrder()
        {
            try
            {
                SessionFacade.CustomerLookUp = "";
                SessionFacade.OrderLookUp = "";
                DataTable dtTemp = new DataTable();
                string[] TempAccount = new string[6];
                DataSet ds = new DataSet();
                DataSet dsTotalOrderSales = new DataSet();

                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";
                string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" +
                    Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                    + "-OrderHistoryArrangeColumn" + ".xml";

                if ((SessionFacade.LastAccount[0] == OHtxtTemp.ToString())
                    &&
                   (OHddlTemp.ToString() == SessionFacade.CampaignValue))
                {
                    dtTemp = GetDatafromXML();

                    if (dtTemp == null)
                    {
                        ds = BindOrderHistory();

                        //if (ds != null && ds.Tables[0].Rows.Count > 0)
                        //{
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        dtTemp = GetDatafromXML();
                        //}
                    }

                    if (dtTemp.Rows.Count > 0)
                    {
                        DataSet ReArrangeDs = new DataSet();
                        ReArrangeDs.Tables.Add(dtTemp);

                        GetTotalOrdersSales(dtTemp);

                        //Working
                        if (ReArrangeDs.Tables[0].Columns.Contains("uvals") == true)
                            ReArrangeDs.Tables[0].Columns.Remove("uvals");

                        gridOrderHistory.DataSource = OHArrangeColumn(ReArrangeDs);
                        gridOrderHistory.DataBind();

                        litErrorinGrid.Text = "";
                        if (ReArrangeDs.Tables[0].Rows.Count > 0)
                        {
                            litErrorinGrid.Text = "";
                        }
                        else
                        {
                            litErrorinGrid.Text = "No Record Found";
                        }
                    }
                    else
                    {
                        gridOrderHistory.DataSource = null;
                        gridOrderHistory.DataBind();
                    }


                }
                else
                {
                    ds = BindOrderHistory();

                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtTemp = GetDatafromXML();

                        GetTotalOrdersSales(dtTemp);

                        DataSet ReArrangeDs = new DataSet();
                        ReArrangeDs.Tables.Add(dtTemp);
                        if (OHuval != 1000)
                        {
                            ReArrangeDs.Tables[0].Columns.Remove("uvals");
                        }

                        ReArrangeDs = OHArrangeColumn(ReArrangeDs);

                        //Working

                        ReArrangeDs.AcceptChanges();

                        gridOrderHistory.DataSource = ReArrangeDs;

                        gridOrderHistory.DataBind();

                        if (ReArrangeDs.Tables[0].Rows.Count > 0)
                        {

                            litErrorinGrid.Text = "";
                        }
                        else
                        {

                            litErrorinGrid.Text = "No Record Found";
                        }

                        try
                        {
                            string strAccntNo = ds.Tables[0].Rows[0]["Account Number"].ToString().Trim();

                            if (strAccntNo == SessionFacade.AccountNo.ToString().Trim())
                            {
                                SessionFacade.AccountNo = strAccntNo;
                                TempAccount = SessionFacade.LastAccount;
                                TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();
                                SessionFacade.LastAccount = TempAccount;
                            }
                            else
                            {
                                SessionFacade.AccountNo = "";
                                TempAccount = SessionFacade.LastAccount;
                                TempAccount[0] = "";
                                SessionFacade.LastAccount = TempAccount;
                            }
                        }
                        catch
                        {
                            SessionFacade.AccountNo = "";
                            TempAccount = SessionFacade.LastAccount;
                            TempAccount[0] = "";
                            SessionFacade.LastAccount = TempAccount;
                        }



                    }


                    else
                    {
                        //   SessionFacade.AccountNo = "";
                        gridOrderHistory.DataSource = null;
                        gridOrderHistory.DataBind();
                        txtbTotalOrders.Text = "0";
                        txtbTatalSales.Text = "$0.00";
                        litErrorinGrid.Text = "No Record Found";
                        TempAccount = SessionFacade.LastAccount;
                        TempAccount[0] = "";
                        SessionFacade.LastAccount = TempAccount;
                    }



                }
            }
            catch (Exception err)
            {
            }

        }

        #region write to XML File //Accepts DataSet

        private void WriteXmlToFile(DataSet thisDataSet)
        {
            string filename = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + Environment.UserName + "-OrderHistory" + ".xml";
            if (thisDataSet == null) { return; }
            // Create a file name to write to.

            // Create the FileStream to write with.
            System.IO.FileStream myFileStream = new System.IO.FileStream
               (filename, System.IO.FileMode.Create);
            // Create an XmlTextWriter with the fileStream.
            System.Xml.XmlTextWriter myXmlWriter =
               new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode);
            // Write to the file with the WriteXml method.
            thisDataSet.WriteXml(myXmlWriter);
            myXmlWriter.Close();
        }
        #endregion

        private void AssignTotalOrders(OrderSales c)
        {
            txtbTotalOrders.Text = c.Orders;
            txtbTatalSales.Text = c.Sales;
        }

        public DataTable GetDatafromXML()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();

            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";
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
                    if (Request.Cookies["CNo"] != null)
                    {
                        SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                        Query = Query + " and [Contact Number] = " + Request.Cookies["CNo"].Value;
                    }

                    //Search by Date
                    if (ByDate.Checked == true)
                    {
                        if ((txtStartDate.Text.ToString().Trim() != "") && (txtEndDate.Text.ToString().Trim() != ""))
                        {
                            Query = Query + " and [Order Date] >= '" + txtStartDate.Text + "' and [Order Date] <=  '" +
                            txtEndDate.Text + "'";
                        }
                    }
                    if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                    {
                        if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                        {
                            Query = Query + " and [Order Number] = '" + SessionFacade.OrderLookUp + "'";
                        }

                    }

                    if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                    {
                        if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                        {
                            Query = Query + " and [Customer PO] = '" + SessionFacade.CustomerLookUp + "'";
                        }
                    }
                    else if (ByCal.Checked == true)
                    {
                        if (rdoCalender.Checked == true)
                        {
                            Query = Query + " and [Order Date] >= '" + "1/1/" + ddlBycal.Text +
                                "' and [Order Date] <= '" + "12/31/" + ddlBycal.Text + "'";
                        }
                        else
                        {
                            Query = Query + " and ([Order Date] >= '" + "8/1/" + (int.Parse(ddlBycal.Text) - 1).ToString() +
                                "' and [Order Date] <= '" + "7/31/" + ddlBycal.Text + "')";
                        }
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

        public DataSet OHGetDatafromXMLArrangeColumn()
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }

        protected DataTable GetDatafromXMLTotalOrdersAndSales()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-OrderHistoryTotalOrdersSales" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = " 1=1";

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
                Query = Query + " and buyerct = " + Request.Cookies["CNo"].Value;
            }

            //Search by Date
            if (ByDate.Checked == true)
            {
                if ((txtStartDate.Text.ToString().Trim() != "") && (txtEndDate.Text.ToString().Trim() != ""))
                {
                    Query = Query + " and itemcreatedon >= '" + txtStartDate.Text + "' and itemcreatedon <=  '" +
                    txtEndDate.Text + "'";
                }
            }
            if (SessionFacade.OrderLookUp.ToString().Trim() != "")
            {
                if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                {
                    Query = Query + " and DOC_NUMBER = '" + SessionFacade.OrderLookUp + "'";
                }

            }

            if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
            {
                if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                {
                    Query = Query + " and BSTKD = '" + SessionFacade.CustomerLookUp + "'";
                }
            }
            else if (ByCal.Checked == true)
            {
                if (rdoCalender.Checked == true)
                {
                    Query = Query + " and itemcreatedon >= '" + "1/1/" + ddlBycal.Text +
                        "' and itemcreatedon <= '" + "12/31/" + ddlBycal.Text + "'";
                }
                else
                {
                    Query = Query + " and itemcreatedon >= '" + "8/1/" + (int.Parse(ddlBycal.Text) - 1).ToString() +
                        "' and itemcreatedon <= '" + "7/31/" + ddlBycal.Text + "'";
                }
            }

            results = ds.Tables[0].Select(Query);



            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;

        }

        protected void StatusSearchDateRange(bool value)
        {
            imgstartCal.Enabled = value;
            imgEndCal.Enabled = value;
        }

        protected void StatusSearchYear(bool value)
        {
            rdoCalender.Enabled = value;
            rdoFiscalYear.Enabled = value;
            ddlBycal.Enabled = value;
        }

        protected void InsertCalendarYearToDropDown()
        {
            int Year;
            ddlBycal.Items.Clear();
            if (DateTime.Now.Month >= 8)
            {
                for (int index = 3; index >= -1; index--)
                {
                    Year = DateTime.Now.Year - index;
                    ddlBycal.Items.Add(Year.ToString());
                }
            }
            else
            {
                for (int index = 4; index >= 0; index--)
                {
                    Year = DateTime.Now.Year - index;
                    ddlBycal.Items.Add(Year.ToString());
                }
            }
            ddlBycal.Text = DateTime.Now.Year.ToString();
        }

        protected DataSet OHArrangeColumn(DataSet ReArrangeDs)
        {
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";
            if (ReArrangeDs != null)
            {
                cArrangeDataSet ADS = new cArrangeDataSet();
                ADS.CampaignName = SessionFacade.CampaignValue;
                ADS.UserName = SessionFacade.LoggedInUserName;
                ADS.Listview = "lvwData";

                int IsReorder = ADS.ColumnReorderCount();
                if (IsReorder > 0)
                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
            }

            if (ReArrangeDs.Tables[0].Columns.Contains("Order Date"))
                OHDateOrdinal = ReArrangeDs.Tables[0].Columns["Order Date"].Ordinal;
            else
                OHDateOrdinal = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Unit Price"))
                OHUnitPrice = ReArrangeDs.Tables[0].Columns["Unit Price"].Ordinal;
            else
                OHUnitPrice = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Ext Price"))
                OHExtPrice = ReArrangeDs.Tables[0].Columns["Ext Price"].Ordinal;
            else
                OHExtPrice = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Converted Date"))
                OHConvertedDate = ReArrangeDs.Tables[0].Columns["Converted Date"].Ordinal;
            else
                OHConvertedDate = 1000;

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameArrangeColumn);
            ReArrangeDs.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            return ReArrangeDs;
        }

        #endregion

        #region PageChanging
        protected void gridOrderHistoryPageChanging(object sender, GridViewPageEventArgs e)
        {
            gridOrderHistory.DataSource = OHGetDatafromXMLArrangeColumn();
            gridOrderHistory.PageIndex = e.NewPageIndex;
            gridOrderHistory.DataBind();
        }
        #endregion

        #region Sorting Order History

        protected SortDirection OHGridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridOrderHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (OHGridViewSortDirection == SortDirection.Ascending)
            {
                OHGridViewSortDirection = SortDirection.Descending;
                SortGridOrderHistory(sortExpression, "DESC");
            }
            else
            {
                OHGridViewSortDirection = SortDirection.Ascending;
                SortGridOrderHistory(sortExpression, "ASC");
            }
        }

        private void SortGridOrderHistory(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                    "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";

                    if (File.Exists(Pathname))
                    {

                        DataSet ReArrangeDs = new DataSet();
                        DataTable dtAllColumns = GetDatafromXML();
                        DataSet ds = new DataSet();
                        DataSet Sortedds = new DataSet();
                        DataView dv = new DataView(dtAllColumns);

                        dv.Sort = sortExpression + " " + direction;

                        ds.Tables.Add(dv.ToTable());

                        //WriteXmlToFile(ds);
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();
                        Sortedds = OHArrangeColumn(ds);

                        //Working
                        //Sortedds.Tables[0].Columns.Remove("uvals");

                        if (ds.Tables[0].Columns.Contains("uvals") == true)
                            ds.Tables[0].Columns.Remove("uvals");

                        gridOrderHistory.DataSource = Sortedds;
                        gridOrderHistory.DataBind();

                        if (direction == "DESC")
                        {
                            gridOrderHistory.HeaderRow.Cells[Sortedds.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortdesc-header";

                        }
                        else
                        {
                            gridOrderHistory.HeaderRow.Cells[Sortedds.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortasc-header";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Export to Excel Function
        protected void OHbtn_Export2ExcelClick(object sender, EventArgs e)
        {
            OHExportExcelFunction();
        }
        public void OHExportExcelFunction()
        {
            try
            {
                DataSet ds = new DataSet();
                string UserFileName = SessionFacade.LoggedInUserName + "OrderHistory" + ".xls";
                if (BindOrderHistory() != null)
                {
                    if (BindOrderHistory().Tables[0].Rows.Count > 0)
                    {
                        ds = BindOrderHistory();

                    }

                    ds.Tables[0].Columns.Remove("uvals");
                    ds.AcceptChanges();

                    if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName)))
                        File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

                    File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/Ordertemp.xls"), Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));


                    string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName);


                    bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

                    //true means Excel File has been written
                    if (exportToEx == true)
                    {
                        if (Request.Browser.Type == "Desktop") //For chrome
                        {
                            ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=OrderHistory&FilePath=" + UserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=OrderHistory&FilePath=" + UserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
                        }
                    }
                    else
                    {
                        litErrorinGrid.Text = "Un able to export the data. Please contact Administartor";
                        // Response.Write("Data not Exported to Excel File");
                    }
                }



            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Order History - Error in Export to Excel" + err.ToString());
            }
        }
        #endregion

        protected void btn_ArrangeColumns(object sender, EventArgs e)
        {
            OpenNewWindow("../Home/ArrangeColumns.aspx?Data=lvwData");
        }

        public void OpenNewWindow(string url)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
        }

        protected void btn_POLOOKUPClick(object sender, EventArgs e)
        {
            //Jonmar, Please read the Notes in orde rhistory page 
            POLookUpFunction();
        }

        public void POLookUpFunction()
        {
            OHbtnOk.Visible = true;
            btnUpdate.Visible = false;
            OHOpenPopup();
        }

        private void OHOpenPopup()
        {
            OHPanel66.Visible = true;
            OHModalPopupExtender1.Show();
            upOuter.Update();
        }

        protected void OHbtnOk_Click1(object sender, EventArgs e)
        {
            try
            {
                string MyLookUpText = txtCustomerLookUp.Text.ToString().Trim();
                if (MyLookUpText.Length > 0)
                {
                    MyLookUpText = MyLookUpText.Replace(",", "");
                }

                if (MyLookUpText.Length > 0)
                {
                    if (rdoOrderNumber.Checked == true && rdoCustomerLookUp.Checked == false)
                    {
                        SessionFacade.OrderLookUp = txtCustomerLookUp.Text.ToString().Trim().PadLeft(10, '0');
                        SessionFacade.CustomerLookUp = "";
                    }
                    if (rdoOrderNumber.Checked == false && rdoCustomerLookUp.Checked == true)
                    {
                        SessionFacade.OrderLookUp = "";
                        SessionFacade.CustomerLookUp = txtCustomerLookUp.Text.ToString();
                    }
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Order History- Error in Customer PO LookUp" + err.ToString());
            }

            ClosePopup();

            DataTable dt = GetDatafromXML();
            GetTotalOrdersSales(dt);

            //Working
            if (dt.Columns.Contains("uvals") == true)
                dt.Columns.Remove("uvals");

            gridOrderHistory.DataSource = dt;
            gridOrderHistory.DataBind();
            litErrorinGrid.Text = "";

            upOuter.Update();
        }

        private void ClosePopup()
        {
            OHPanel66.Visible = false;
            OHModalPopupExtender1.Hide();
            upOuter.Update();
        }

        #endregion

        #region Product Summary Page Functions

        protected void ShowproductSummaryPage()
        {

            PSSetButtonVisible();

            if (txtTemp.ToString().Trim() != "")
            {


                SessionFacade.CampaignValue = ddlTemp.ToString().Trim();
                SessionFacade.CampaignName = ddlTemp.ToString().Trim();



                if (SessionFacade.CampaignName.ToString().Trim() != "PC")
                {
                    pnlOterCampaign.Visible = true;
                    pnlPCCampaign.Visible = false;
                    ArrnageColumnstring = "lvwSKUSummary";

                }
                if (SessionFacade.CampaignName.ToString().Trim() == "PC")
                {
                    pnlOterCampaign.Visible = false;
                    pnlPCCampaign.Visible = true;
                    ArrnageColumnstring = "lvwPCSKUSummary";
                }



                LoadProductSummary();
                SessionFacade.SKUCategory = null;
                LoadSKUSummary();

            }
            else
            {
                trBlank.Visible = true;
            }
        }
        #region Enable Disable Buttons on the page
        protected void PSSetButtonVisible()
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
                BradyCorp.Log.LoggerHelper.LogException(ex, "Product Summary Page - Load ProductSummary Data");
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
                    objProductSummary.CampaignName = ddlTemp.ToString().Trim();
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;


                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsProductLineSummary = objProductSummary.GetProductSummary();

                    if (dsProductLineSummary.Tables[0].Rows.Count > 0)
                    {
                        grdProductLineSummary.DataSource = null;
                        pnlGridIndex.Visible = true;
                        Panel2.Visible = true;
                        if (ddlTemp.ToString().Trim() != "PC")
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
                        if (ddlTemp.ToString().Trim() != "PC")
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
                BradyCorp.Log.LoggerHelper.LogException(ex, "Product Summary Page - Load ProductSummary Data");
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
        private void LoadSKUSummary()
        {
            cProductSummary objProductSummary = new cProductSummary();
            DataSet dsSKUSummary = null;
            try
            {

                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    objProductSummary.CampaignName = ddlTemp.ToString().Trim();
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsSKUSummary = objProductSummary.GetAllSKUSummary();
                    DataSet ReArrangeDs = null;

                    if (dsSKUSummary.Tables[0].Rows.Count > 0)
                    {
                        // Panel1.Visible = true;
                        // Panel4.Visible = true;
                        grdProductLineSummary.DataSource = null;
                        if (ddlTemp.ToString().Trim() != "PC")
                        {
                            dsSKUSummary.Tables[0].Columns.Remove("sku_category");
                            grdSkuSummary.DataSource = null;
                            pnlOterCampaign.Visible = true;
                            pnlPCCampaign.Visible = false;
                            grdSkuSummary.Visible = true;

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
                            grdSkuSummary.DataBind();
                            lblErrorSkuSummary.Text = "";
                        }
                        else
                        {

                            grdPCSKUSummary.DataSource = null;
                            pnlOterCampaign.Visible = false;
                            pnlPCCampaign.Visible = true;
                            grdPCSKUSummary.Visible = true;

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
                            grdPCSKUSummary.DataSource = ReArrangeDs;
                            grdPCSKUSummary.DataBind();
                            Panel4.Visible = true;
                            // lblErrorSkuSummary.Text = "";
                        }
                        Second = true;

                    }
                    else
                    {
                        if (ddlTemp.ToString().Trim() != "PC")
                        {
                            grdSkuSummary.Visible = false;
                        }
                        else
                        {
                            grdPCSKUSummary.Visible = false;
                        }
                        //Panel1.Visible = false;
                        //Panel4.Visible = false;
                        Second = false;
                    }

                }

            }
            catch (Exception ex)
            {
                lblErrorProductLineSummary.Text = "Error in Loading SKU Summary table";
                BradyCorp.Log.LoggerHelper.LogException(ex, "SKU Summary Page - Load ProductSummary Data");
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
                    objProductSummary.CampaignName = ddlTemp.ToString().Trim();
                    objProductSummary.SoldTo = SessionFacade.AccountNo;
                    objProductSummary.SKUCategory = SessionFacade.SKUCategory;
                    objProductSummary.BuyerCt = SessionFacade.BuyerCt;

                    dsSKUSummary = objProductSummary.GetAllSKUSummary();


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

        #endregion

        #region RowBound for Both Grids
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

            //            If e.Row.Cells("GoodsDesc").Text.Length > MaxChar Then
            //e.Row.Cells("GoodsDesc").Text = e.Row.Cells(6).Text.Substring(0, MaxChar)
            //End If
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

                dsProductLineSummary = objProductSummary.GetProductSummary();


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

                dsSKUSummary = objProductSummary.GetAllSKUSummary();

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


                PCDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;
                PCReviousDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Revision Date"].Ordinal;

                grdPCSKUSummary.DataSource = ReArrangeDs;
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
                SessionFacade.SKUCategory = null;
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
                BradyCorp.Log.LoggerHelper.LogException(ex, "Product Summary Page - Load ProductSummary Data");
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

                PCDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Ordered Date"].Ordinal;
                PCReviousDateOrdinal = ReArrangeDs.Tables[0].Columns["Last Revision Date"].Ordinal;

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
            ModalPopupExtender1.Show();
        }

        protected void SCbtnOk_Click1(object sender, EventArgs e)
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
            ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=" + vPageName + "&FilePath=" + vFilePath + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);

        }
        #endregion
        #endregion

        #region Show Notes&CommHistory
        protected void ShowNotesCommPage()
        {

            string tempCampaign = NCddlTemp.ToString().Trim();
            if (!IsPostBack)
            {
                //tempCampaign = ddlTemp.SelectedValue.ToString().Trim();
                NCStatusSearchDateRange(false);
                string acTUser = SessionFacade.LoggedInUserName;
                btnAllNotes.Text = "Show All " + acTUser.ToUpper() + "'s Notes";


              
                txtNoteStartDate.Attributes.Add("readonly", "true");
                txtNoteStartDate.Attributes.Add("readonly", "true");
                txtNoteStartDate.Attributes.Add("readonly", "true");
                ShowDialerData();
                ShowNotesHistory();

            }
            else
            {

                if (NCddlTemp.ToString() != SessionFacade.CampaignValue)
                {
                    ShowNotesHistory();
                    ShowDialerData();
                }


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
                BradyCorp.Log.LoggerHelper.LogException(ex, "Nottes & Com History Page - Button Login Click Error");
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
                // page.ClientScript.RegisterStartupScript(page.GetType(), "alert", script, true /* addScriptTags */);


            }
        }



        protected void btnRefreshNotes_Click(object sender, EventArgs e)
        {

            ShowNotesHistory(true);

            //NCgrdNotesHistory.DataSource = BindNotesCommHistory();
            //NCgrdNotesHistory.DataBind();
            grdDialerHistory.DataSource = BindDialerData();
            grdDialerHistory.DataBind();

        }


        #region NotesComHist

        public DataSet BindNotesCommHistory(bool UserNotes = false)
        {
            DataSet drExisting;
            cNotesCommHistory objNotesCommHistory = new cNotesCommHistory();

            //Campaign
            objNotesCommHistory.CampaignName = NCddlTemp.ToString().Trim();
            objNotesCommHistory.AccountNum = SessionFacade.AccountNo.ToString().Trim();
            objNotesCommHistory.CreatedBy = SessionFacade.LoggedInUserName.ToUpper();

            if (UserNotes == false)
            {
                string[] TempAccount = new string[6];

                drExisting = objNotesCommHistory.GetNotesCommHistory();
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
            string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";

            if (SessionFacade.UserTrig == "1")
            {
                xmlName = "-UserNotesHistory";
            }
            else
            {
                xmlName = "-NotesHistory";
            }


            NCgrdNotesHistory.DataSource = GetDatafromXML(xmlName);
            NCgrdNotesHistory.PageIndex = e.NewPageIndex;
            NCgrdNotesHistory.DataBind();
        }


        protected SortDirection NCGridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void NCgrdNotesHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (NCGridViewSortDirection == SortDirection.Ascending)
            {

                NCGridViewSortDirection = SortDirection.Descending;
                NCSortGridOrderHistory(sortExpression, "DESC");
            }
            else
            {
                NCGridViewSortDirection = SortDirection.Ascending;
                NCSortGridOrderHistory(sortExpression, "ASC");


            }
        }




        private void NCSortGridOrderHistory(string sortExpression, string direction)
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

                        NCgrdNotesHistory.DataSource = dv;
                        NCgrdNotesHistory.DataBind();

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

                    ////DataRow rowOrders;

                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-NotesHistory" + ".xml";
                    string Pathname2 = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-UserNotesHistory" + ".xml";

                    if (SessionFacade.LastAccount[3] == SessionFacade.AccountNo.ToString().Trim())
                    {
                        string xmlName = "-UserNotesHistory";
                        if (GetDatafromXML(xmlName).Rows.Count > 0)
                        {
                            NCgrdNotesHistory.DataSource = GetDatafromXML(xmlName);
                            NCgrdNotesHistory.DataBind();

                        }
                        else
                        {
                            //NCgrdNotesHistory.Visible = false;
                            NCgrdNotesHistory.DataSource = null;
                            NCgrdNotesHistory.DataBind();

                        }
                    }
                    else
                    {


                        //if new account number, get data from database and write to xml


                        ds = BindNotesCommHistory();
                        DataSet ReArrangeDs = new DataSet();
                        ReArrangeDs = ds;

                        if (ds.Tables[0].Rows.Count > 0)
                        {


                            if (ReArrangeDs != null)
                            {
                                cArrangeDataSet ADS = new cArrangeDataSet();
                                ADS.CampaignName = SessionFacade.CampaignValue;
                                ADS.UserName = SessionFacade.LoggedInUserName;
                                ADS.Listview = "lvwNotesData";

                                int IsReorder = ADS.ColumnReorderCount();
                                if (IsReorder > 0)
                                {
                                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);

                                }
                            }


                            ds = ReArrangeDs;
                            NCgrdNotesHistory.Visible = true;
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname2);
                            ds.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
                            xmlSW2.Close();

                            if (GetDatafromXML().Rows.Count > 0)
                            {
                                NCgrdNotesHistory.DataSource = GetDatafromXML();
                                NCgrdNotesHistory.DataBind();
                            }
                            else
                            {
                                //Writing XML
                                NCgrdNotesHistory.Visible = false;
                            }
                        }

                        else
                        {
                            NCgrdNotesHistory.DataSource = null;
                            NCgrdNotesHistory.DataBind();

                        }



                    }
                    SessionFacade.UserTrig = "0";
                }
                else
                {
                    string xmlName = "-UserNotesHistory";
                    string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + xmlName + ".xml";

                    ds = BindNotesCommHistory(true);
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    SessionFacade.UserTrig = "1";
                    NCgrdNotesHistory.DataSource = GetDatafromXML(xmlName);
                    NCgrdNotesHistory.DataBind();

                }
            }
            catch (Exception e)
            {
                new ArgumentNullException();
                NCgrdNotesHistory.Visible = false;

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

                dtTemp = ds.Tables[0].Clone();
                // dtTemp.Columns["Order Date"].DataType = System.Type.GetType("System.Date");
                if (Request.Cookies["CNo"] != null)
                {
                    SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                    Query = Query + " and [Contact_Number] = " + Request.Cookies["CNo"].Value;
                }


                //Search by Date
                if (ByDate.Checked == true)
                {
                    if ((NCtxtStartDate.Text != "") || (NCtxtEndDate.Text != ""))
                    {
                        Query = Query + " and Created_on >= '" + NCtxtStartDate.Text + "' and Created_on <=  '" +
                        NCtxtEndDate.Text + "'";
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
            DataSet drExisting;
            cDialerData objDialerData = new cDialerData();
            string Pathname = WebHelper.GetApplicationPath() + @"App_Data\XMLFiles" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-DialerData" + ".xml";
            objDialerData.SearchOrderCampaignName = NCddlTemp.ToString().Trim();


            drExisting = objDialerData.GetDialerData();

            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            drExisting.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();
            SessionFacade.UserTrig = "1";
            if (drExisting.Tables[0].Rows.Count > 0)
            {
                grdDialerHistory.Visible = true;


            }
            else
            {
                //grdDialerHistory.Visible = false;


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

                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();
                            grdDialerHistory.DataSource = GetXMLDialerData();
                            grdDialerHistory.DataBind();
                        }
                        else
                        {
                            //Writing XML
                            //grdDialerHistory.Visible = false;

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
                    grdDialerHistory.DataSource = GetXMLDialerData();
                    grdDialerHistory.DataBind();

                }
            }
            catch (Exception ex)
            {
                new ArgumentNullException();
                grdDialerHistory.Visible = false;

            }

        }


        protected SortDirection NCGridViewSortDirectionDialer
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
            if (NCGridViewSortDirectionDialer == SortDirection.Ascending)
            {

                NCGridViewSortDirectionDialer = SortDirection.Descending;
                SortGridDialerData(sortExpression, "DESC");
            }
            else
            {
                NCGridViewSortDirectionDialer = SortDirection.Ascending;
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


        protected void ddlNoteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowNotesHistory();
        }
        protected void NCStatusSearchDateRange(bool value)
        {
            NCimgstartCal.Enabled = value;
            NCimgEndCal.Enabled = value;
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
        protected void NCByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ByDate.Checked == true)
            {
                StatusSearchDateRange(true);

            }
            else
                StatusSearchDateRange(false);
        }


        protected void btnAllNotes_Click(object sender, EventArgs e)
        {
            ShowNotesHistory(true);
        }

        protected void btnAdfsfdNote_Click(object sender, EventArgs e)
        {
            Panel67.Visible = true;
            NCModalPopupExtender2.Show();
            //Response.Write(@"<script language='javascript'>alert('Update is successful.')</script>");
        }



        #region Export to Excel Function


        protected void NCbtn_Export2ExcelClick(object sender, EventArgs e)
        {
            //Jonmar, Please read the Notes in orde rhistory page 
            NCExportExcelFunction();

        }

        public void NCExportExcelFunction()
        {

            NCbtnOk.Visible = true;
            NCbtnUpdate.Visible = false;
            NCOpenPopup();
        }

        private void NCOpenPopup()
        {
            NCPanel66.Visible = true;
            NCModalPopupExtender1.Show();
        }

        protected void NCbtnOk_Click1(object sender, EventArgs e)
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
                    ds = BindNotesCommHistory();


                }
                if (rdoNotesHistory.Checked == false && rdoDialerData.Checked == true)
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

                NCClosePopup(PageName, DestinationUserFileName);


            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Notes and ComHist - Error in Export to Excel" + err.ToString());
            }
        }
        private void NCClosePopup(string vPageName, string vFilePath)
        {
            NCModalPopupExtender1.Hide();
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

        protected void NCgrdNotesHistory_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void NCgrdDialerHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void NCgrdNotesHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void NCgrdDialerHistory_RowDataBound(object sender, GridViewRowEventArgs e)
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
        #endregion
    }
}