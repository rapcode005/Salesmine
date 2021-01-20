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
using System.IO;
using System.Text;
using ClassLibrary1;
using System.Globalization;
using System.Collections;
using System.Data.SqlTypes;
using WebSalesMine.WebPages.UserControl;

namespace WebSalesMine.WebPages.Site_ContactInfo 
{
    public partial class SiteContactInfo : System.Web.UI.Page
    {
        public string Clement = "false";
        public string Contpcman = "false";
        public DropDownList ddlCampaignCurrencyCI = new DropDownList();
        public DropDownList ddlCampaignCI = new DropDownList();
        public string userRule = string.Empty,
                     AccountCI = string.Empty;

        #region Function

        [System.Web.Services.WebMethod]
        public static DataSet GetData(int Row)
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            
            Query = "Row = " + Row;
      
            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            //To Copy the Schema.
            dtTemp = ds.Tables[0].Clone();

            results = ds.Tables[0].Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            ds = new DataSet();
            ds.Tables.Add(dtTemp);

            return ds;
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
         
        public DataSet UppersCaseColumnHeader(DataSet sample)
        {
            //DataSet ReDataset = sample;
            bool OkSelect = false;
            string ColumnRemove = "";

            if (sample != null)
            {
                string DefaultSite_ContactColumns = "Contact Number|First Name|Last Name|Contact Type|Contact Status|Repdata Contact Status|Repdata Function|Recency|Department|Job Function|Title|Direct Phone|Site Phone|Email Address|Do Not Mail|Do Not Email|Do Not Fax|Do Not Call|Lifetime Sales|Last 12M Sales|Lifetime Orders|last 12M Orders|Last Purchased Date|Phone Extension";
                string[] SiteInfo = DefaultSite_ContactColumns.Split('|');
                for (int index = 0; index < sample.Tables[0].Columns.Count; index++)
                {
                    for (int s = 0; s < SiteInfo.Length; s++)
                    {
                        OkSelect = false;
                        if (sample.Tables[0].Columns[index].ColumnName == SiteInfo[s])
                        {
                            OkSelect = true;
                            break;
                        }
                        else
                            OkSelect = false;
                    }
                    if (OkSelect)
                    {
                        if (sample.Tables[0].Columns[index].ColumnName == "Contact Status")
                            sample.Tables[0].Columns[index].ColumnName = "SAP STATUS";
                        //else if (sample.Tables[0].Columns[index].ColumnName == "Repdata Contact Status")
                        //    sample.Tables[0].Columns[index].ColumnName = "CONTACT STATUS";
                        //else if (sample.Tables[0].Columns[index].ColumnName == "Repdata Function")
                        //    sample.Tables[0].Columns[index].ColumnName = "JOB AREA";
                        else
                        {
                            sample.Tables[0].Columns[index].ColumnName =
                                sample.Tables[0].Columns[index].ColumnName.ToString().ToUpper();
                        }
                    }
                    else
                    {
                        if (sample.Tables[0].Columns[index].ColumnName != "Row")
                            ColumnRemove += sample.Tables[0].Columns[index].ColumnName.ToString().ToUpper() + "|";
                    }
                }

                if (ColumnRemove != "")
                {
                    ColumnRemove = ColumnRemove.Remove(ColumnRemove.Length - 1);

                    string[] ColumnRemoveArray = ColumnRemove.Split('|');

                    //Remove Column
                    for (int x = 0; x < ColumnRemoveArray.Length; x++)
                    {
                        if (sample.Tables[0].Columns.Contains(ColumnRemoveArray[x]))
                        {
                            sample.Tables[0].Columns.Remove(ColumnRemoveArray[x]);
                        }
                    }
                }

            }
            return sample;
        }

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

            //txtFirstname.Text = string.Empty;
            //txtLastname.Text = string.Empty;
            ////txtDepartment.Text = string.Empty;
            //txtDoNotMail.Text = string.Empty;
            //txtLifetimeSales2.Text = string.Empty;
            //txtfunction.Text = string.Empty;
            //txtDoNotFax.Text = string.Empty;
            //txtLast12MSalesContact.Text = string.Empty;
            //txtContactType.Text = string.Empty;
            //txtTitle.Text = string.Empty;
            //txtDoNotEmail.Text = string.Empty;
            //txtLifetimeOrders2.Text = string.Empty;
            //txtStatus.Text = string.Empty;
            //txtDirectPhone.Text = string.Empty;
            //txtDoNoCall.Text = string.Empty;
            //txtLast12MOrderContact.Text = string.Empty;
            //txtRecency.Text = string.Empty;
            //txtSitePhone.Text = string.Empty;
            //txtLastOrderDateContact.Text = string.Empty;
            //txtEmailContact.Text = string.Empty;
        }

        //Get ContactNumber
        protected string GetContactNumber(string value)
        {
            gridSiteContactInfo.SelectedIndex = Int16.Parse(hdnEmailID.Value) - 1;
            GridViewRow row = gridSiteContactInfo.SelectedRow;
            if (value == "0")
            {
                //DataTable dtContactInfo = new DataTable();
                DataSet dtContactInfo = GetData(int.Parse(hdnEmailID.Value));
                if (dtContactInfo.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dtContactInfo.Tables[0].Rows[0];
                    return CheckSpace(dr["new_contact_key"].ToString());
                }
                else
                    return value;
            }
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

            foreach (CultureInfo culture in cultures)
            {
                CultureTypes ct = culture.CultureTypes;
                String s = ct.ToString();
                if (!s.Contains("NeutralCultures"))
                {
                    // check if it's not a invariant culture
                    if (culture.LCID != 127)
                    {
                        RegionInfo region = new RegionInfo(culture.LCID);
                        // add countries that are not in the list
                        if (!(list.Contains(region.EnglishName)))
                        {
                            list.Add(region.EnglishName);
                        }
                    }
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
        private void Visibility(bool visible)
        {
            //tabQualifyingQuestion.Visible = visible;
            //tabSecondQualifying.Visible = visible;
            //tabProductPurchase.Visible = visible;
            //tabProjectsOthers.Visible = visible;
            //tabPCQQ.Visible = !visible;
            //ContactLevelNotPC.Visible = visible;
            //ContactLevelPC.Visible = !visible;
            //SiteLevelPC.Visible = !visible;
        }

        private void ShowSiteInfo(DataSet dsAcc, string PathnameAccount)
        {
            string ControlID = string.Empty;
            NewMasterPage MasterPage = Master as NewMasterPage;
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoAccount" + ".xml";

            //Load Campaign with currency
       

            //Site Info
            if (dsAcc != null)
            {
                if (dsAcc.Tables[0].Rows.Count > 0)
                {
                    if (PathnameAccount != "")
                    {
                        //Writing XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameAccount);
                        dsAcc.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();
                    }

                    DateTime tempdate;
                    //Site Name
                    //if (ddlCampaignCI.SelectedValue == "DE")
                    //{
                    //    byte[] data = Encoding.Default.GetBytes(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim());
                    //    string output = Encoding.UTF8.GetString(data);

                    //    txtSiteName.Text = output;
                    //}
                    //else
                        txtSiteName.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim();

                    txtEmployeeSize.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(5).ToString().Trim();
                    txtBuyerOrg.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(6).ToString().Trim();
                    txtIndustry.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();
                    txtKeyAcctMng.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(7).ToString().Trim();
                    decimal result;

                    //Last 12M Sales
                    if (dsAcc.Tables[0].Rows[0].ItemArray.GetValue(9).ToString().Trim() == "" ||
                        decimal.TryParse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(9).ToString().Trim(), out result)
                        == false)
                    {
                        decimal temp = 0;
                        txtLast12MSales.Text = temp.FormatMoney(ddlCampaignCurrencyCI.SelectedItem.Text);
                        //txtLast12MSales.Text = objGrid.FormatCurrency(0, ddlCampaignCurrencyCI.SelectedItem.Text);
                    }
                    else
                    {
                        txtLast12MSales.Text = decimal.Parse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(9).ToString().Trim()).FormatMoney(ddlCampaignCurrencyCI.SelectedItem.Text);
                        //txtLast12MSales.Text = objGrid.FormatCurrency(decimal.Parse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(9).ToString().Trim()), ddlCampaignCurrencyCI.SelectedItem.Text);
                    }

                    txtLifetimeOrders.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(10).ToString().Trim();

                    //Lifetime Sales
                    if (dsAcc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString().Trim() == "" ||
                        decimal.TryParse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString().Trim(), out result) == false)
                    {
                         decimal temp = 0;
                        //txtLifetimeSales.Text = objGrid.FormatCurrency(0, ddlCampaignCurrencyCI.SelectedItem.Text);
                         txtLifetimeSales.Text = temp.FormatMoney(ddlCampaignCurrencyCI.SelectedItem.Text);
                    }
                    else
                    {
                        txtLifetimeSales.Text = decimal.Parse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString().Trim()).FormatMoney(ddlCampaignCurrencyCI.SelectedItem.Text);
                        //txtLifetimeSales.Text = objGrid.FormatCurrency(decimal.Parse(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString().Trim()), ddlCampaignCurrencyCI.SelectedItem.Text);
                    }
                    txtOrganization.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
                    txtSIC2.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(4).ToString().Trim();
                    txtLast12MOrder.Text = dsAcc.Tables[0].Rows[0].ItemArray.GetValue(11).ToString().Trim();
                    if (CheckSpace(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(12).ToString().Trim()) == "" ||
                        DateTime.TryParse(CheckSpace(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(12).ToString().Trim()),out tempdate) == false)
                        txtLastOrderDate.Text = "";
                    else
                        txtLastOrderDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(CheckSpace(dsAcc.Tables[0].Rows[0].ItemArray.GetValue(12).ToString().Trim())));
                }
                else
                    ClearControlsSite();
            }
            else
                ClearControlsSite();

        }

        private void ShowSiteContactInfo()
        {
            DataColumn dc = new DataColumn("Row", typeof(System.Int32));
            string[] TempAccount = new string[6];
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoArrangeColumn" + ".xml";
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            string PathnameAccount = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoAccount" + ".xml";
            DataSet ds = new DataSet();
            DataSet dsAcc = new DataSet();
            DataTable dt = new DataTable();
            DataSet ReArrangeDs = new DataSet();
            string ControlID = string.Empty;
            NewMasterPage MasterPage = Master as NewMasterPage;
            try
            {
                //Check If current and previous account number is diffrent.
                if (SessionFacade.LastAccount[2] == AccountCI &&
                    ddlCampaignCI.SelectedValue == SessionFacade.CampaignValue)
                {
                    //Used this code to refresh if Arrange Column uses.
                    if (SessionFacade.Update_Bool == true)
                    {
                        ds = BindSiteContactInfo();
                        ds = ArrangeColumn(ds);
                        SessionFacade.Update_Bool = false;
                    }
                    else
                    {
                        if (ddlCampaignCI.SelectedValue == SessionFacade.CampaignValue)
                        {
                            if (File.Exists(PathnameArrangeColumn))
                                ds = GetDatafromXMLArrangeColumn();
                            else
                                ds = new DataSet();
                        }
   
                        else
                        {
                            ds = BindSiteContactInfo();
                            ds = ArrangeColumn(ds);
                        }
                    }

                    if (File.Exists(PathnameAccount))
                    {
                        //Site Info
                        dsAcc.Tables.Add(GetDatafromXMLAccountName());
                        ShowSiteInfo(dsAcc, "");
                    }
                    else
                        ShowSiteInfo(null, "");
                   

                    gridSiteContactInfo.DataSource = (ds != null && ds.Tables.Count > 0) ?
                        UppersCaseColumnHeader(ds) : null;
                    gridSiteContactInfo.DataBind();
                }
                else
                {
                    ds = BindSiteContactInfo();
                    dsAcc = BindAcc();

                    if (dsAcc != null && dsAcc.Tables.Count > 0)
                    {
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameAccount);
                        dsAcc.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();
                    }

                    if (ds != null)
                    {
                        ShowSiteInfo(dsAcc, PathnameAccount);

                        //Contact Info
                        if (ds.Tables.Count > 0)
                        {

                            ds.Tables[0].Columns.Add(dc);
                            ds.Tables[0].Columns["Row"].AutoIncrement = true;
                            ds.Tables[0].Columns["Row"].AutoIncrementSeed = 1;
                            ds.Tables[0].Columns["Row"].AutoIncrementStep = 1;

                            //Writing XML
                            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                            xmlSW.Close();

                            ds = new DataSet();
                            ds.Tables.Add(GetDatafromXML());

                            ReArrangeDs = ArrangeColumn(ds);

                            if (ReArrangeDs.Tables.Count == 0)
                                ClearContactInfo();

                            gridSiteContactInfo.DataSource = UppersCaseColumnHeader(ReArrangeDs);
                            gridSiteContactInfo.DataBind();

                            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                            //"call me", "HideColumn()", true);
                        }
                        else
                        {

                            //if (File.Exists(Pathname))
                            //    Array.ForEach(Directory.GetFiles(WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles"), File.Delete);          

                            if (File.Exists(Pathname))
                                File.Delete(Pathname);

                            if (File.Exists(PathnameArrangeColumn))
                                File.Delete(PathnameArrangeColumn);

                            if (File.Exists(PathnameAccount))
                                File.Delete(PathnameAccount);

                            ClearContactInfo();

                            gridSiteContactInfo.DataSource = null;
                            gridSiteContactInfo.DataBind();
                        }
                    }
                    else
                    {

                        if(File.Exists(Pathname))
                            File.Delete(Pathname);

                        if(File.Exists(PathnameArrangeColumn))
                            File.Delete(PathnameArrangeColumn);

                        if (File.Exists(PathnameAccount))
                            File.Delete(PathnameAccount);

                        ClearContactInfo();

                        gridSiteContactInfo.DataSource = null;
                        gridSiteContactInfo.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Site and Contact Info - Bind Gridview", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
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
            GridViewRow row = null;
            if (gridSiteContactInfo.SelectedIndex > 0)
            {
                if (gridSiteContactInfo.SelectedValue != null)
                    row = gridSiteContactInfo.SelectedRow;
            }


            Query = "1=1 ";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            //To Copy the Schema.
            if (ds.Tables.Count > 0)
            {
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
                    if (gridSiteContactInfo.SelectedIndex > 0)
                    {
                        if (row != null)
                        {
                            Query = Query + " and new_contact_key <> " + GetContactNumber(row.Cells[GetColumnIndexByName(row, "CONTACT NUMBER")].Text.ToString()).ToString().Trim()
                            + " or new_contact_key is null ";
                        }
                    }
                }

                results = ds.Tables[0].Select(Query);

                foreach (DataRow dr in results)
                    dtTemp.ImportRow(dr);

                return dtTemp;
            }
            else
                return null;
            
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
         

            results = ds.Tables[0].Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;
        }

        private DataSet BindSiteContactInfo()
        {
            string ControlID = string.Empty;
            string[] TempAccount = new string[6];
            DataSet dsSite = new DataSet();
            cSiteAndContactInfo objSiteContactInfo = new cSiteAndContactInfo();
            NewMasterPage MasterPage = Master as NewMasterPage;
            try
            {

                objSiteContactInfo.SearchAccount = AccountCI;

                objSiteContactInfo.SearchCampaign = ddlCampaignCI.Text.ToString().Trim();

                dsSite = objSiteContactInfo.GetSiteAndContactInfo();

                TempAccount = SessionFacade.LastAccount;

                TempAccount[2] = AccountCI;

                SessionFacade.LastAccount = TempAccount;

                if (dsSite != null)
                {
                    if (dsSite.Tables.Count > 0)
                        return dsSite;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Site and Contact Info - BindingData", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                gridSiteContactInfo.DataSource = null;
                gridSiteContactInfo.DataBind();
                return null;
            }
        }

        private DataSet BindAcc()
        {
            NewMasterPage MasterPage = Master as NewMasterPage;
            DataSet dsAcc = new DataSet();
            cSiteAndContactInfo objSiteContactInfo = new cSiteAndContactInfo();
            try
            {

                objSiteContactInfo.SearchAccount = AccountCI.Replace(",", "");

                objSiteContactInfo.SearchCampaign = ddlCampaignCI.Text.ToString().Trim();

                dsAcc = objSiteContactInfo.GetAccInfo();

                if (dsAcc != null)
                {
                    if (dsAcc.Tables.Count > 0)
                        return dsAcc;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Acct Info - BindingData", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
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
            DataColumn dc = new DataColumn("Row", typeof(System.Int32));
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfo" + ".xml";
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoArrangeColumn" + ".xml";
            DataSet ds = new DataSet();
            ds = BindSiteContactInfo();

            ds.Tables[0].Columns.Add(dc);
            ds.Tables[0].Columns["Row"].AutoIncrement = true;
            ds.Tables[0].Columns["Row"].AutoIncrementSeed = 1;
            ds.Tables[0].Columns["Row"].AutoIncrementStep = 1;

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            DataSet ReArrangeDs = new DataSet();

            ReArrangeDs.Tables.Add(GetDatafromXML());

            ds = new DataSet();

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

            if (ReArrangeDs.Tables[0].Columns.Contains("Row") == false)
            {
                dc = new DataColumn("Row", typeof(System.Int32));
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dc.AutoIncrementStep = 1;
                ReArrangeDs.Tables[0].Columns.Add(dc);
            }

            //Writing XML
            xmlSW = new System.IO.StreamWriter(PathnameArrangeColumn);
            ReArrangeDs.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            gridSiteContactInfo.DataSource = UppersCaseColumnHeader(GetDatafromXMLArrangeColumn());
            gridSiteContactInfo.DataBind();
        }

        private DataSet ArrangeColumn(DataSet ReArrangeDs)
        {
            DataColumn dc = new DataColumn("Row", typeof(System.Int32));
            //DataSet ReArrangeDs = new DataSet();
            //ReArrangeDs = ds;
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() +
                "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" +
                SessionFacade.LoggedInUserName + "-SiteAndContactInfoArrangeColumn" + ".xml";

            if (ReArrangeDs != null && ReArrangeDs.Tables.Count > 0)
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

            if (ReArrangeDs.Tables[0].Columns.Contains("Row") == false)
            {
                ReArrangeDs.Tables[0].Columns.Add(dc);
                ReArrangeDs.Tables[0].Columns["Row"].AutoIncrement = true;
                ReArrangeDs.Tables[0].Columns["Row"].AutoIncrementSeed = 1;
                ReArrangeDs.Tables[0].Columns["Row"].AutoIncrementStep = 1;
            }

        
            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameArrangeColumn);
            ReArrangeDs.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            return GetDatafromXMLArrangeColumn();
        }

        private void AssignValue(string Firstname, string Lastname)
        {
            txtPopFirstName.Text = Firstname;
            txtPopLastName.Text = Lastname;
        }
        #endregion

        #region Sorting Site and Contact Info

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

        protected void gridSiteContactInfo_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (sortExpression == "SAP STATUS")
                sortExpression = "Contact Status";
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridSiteContactInfo(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
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

                        //DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                        //ds.Tables[0].Columns.Remove("Row");
                        //dc.AutoIncrement = true;
                        //dc.AutoIncrementSeed = 1;
                        //dc.AutoIncrementStep = 1;
                        //ds.Tables[0].Columns.Add(dc);


                        //Write XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();




                        gridSiteContactInfo.DataSource = UppersCaseColumnHeader(ArrangeColumn(ds));
                        gridSiteContactInfo.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        protected void gridSiteContactInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string rowID = String.Empty;
            bool varColumnExist = false;
            int columnIndex;
            string[] list = { "LAST PURCHASED DATE", "LIFETIME SALES","LAST 12M SALES",
                                "LIFETIME ORDERS","LAST 12M ORDERS","RECENCY","PC UPDATE ON","FIRST NAME",
                            "LAST NAME"};
            //Use to Check what Currency Code going to provide.
            //FunctionNum objGrid = new FunctionNum();
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            DropDownList ddlTempValue = Master.FindControl("ddlCampaignValue") as DropDownList;

            if (!string.IsNullOrEmpty(ddlTemp.SelectedValue))
            {
                ddlTempValue.ClearSelection();

                if (ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue) != null)
                {
                    ddlTempValue.Items.FindByValue(ddlTemp.SelectedValue).Selected = true;
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
                            if (list[i] == "LAST PURCHASED DATE" ||
                                list[i] == "PC UPDATE ON")
                            {
                                DateTime temp;
                                if (DateTime.TryParse(e.Row.Cells[columnIndex].Text, out temp) == true)
                                    e.Row.Cells[columnIndex].Text = Convert.ToDateTime(e.Row.Cells[columnIndex].Text).ToString("MM/dd/yyyy");
                                 e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;
                            }
                            else if (list[i] == "LIFETIME SALES" ||
                                     list[i] == "LAST 12M SALES")
                            {
                                e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;
                                decimal temp;
                                if (decimal.TryParse(e.Row.Cells[columnIndex].Text, out temp) == true)
                                {
                                    e.Row.Cells[columnIndex].Text = decimal.Parse(e.Row.Cells[columnIndex].Text).FormatMoney(ddlTempValue.SelectedItem.Text);
                                    //e.Row.Cells[columnIndex].Text =  objGrid.FormatCurrency(decimal.Parse(e.Row.Cells[columnIndex].Text), ddlTempValue.SelectedItem.Text);
                                }
                            }
                            else if (list[i] == "LIFETIME ORDERS" ||
                                     list[i] == "LAST 12M ORDERS" ||
                                     list[i] == "RECENCY")
                            {
                                e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;
                            }
                        }
                    }
                }
            }
        }

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            NewMasterPage MasterPage = Master as NewMasterPage;
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + 
                "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SiteAndContactInfoAccount" + ".xml";
            
            GetAccountInfo(MasterPage);

            CheckForButtonVisibility();

            if (!IsPostBack && 
                SessionFacade.AccountNo.ToString().Trim() != "")
            {
                ShowSiteContactInfo();
                SessionFacade.NameOrContacts = "";

                // Master Data Changes
                txtAccountNameMasterChange.Text = txtSiteName.Text;
                txtAccountNumberMasterChange.Text = AccountCI;

                LoadCountry();
            }
            else
            {
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

            if (SessionFacade.AccountNo.ToString().Trim() != "")
            {
                GetPreferences();
                GetAccountChanges();
                GetContactChanges();
            }

            //Check if there is Account searched
            if (File.Exists(Pathname))
                PnlOrderHistory.Visible = true;
            else
                PnlOrderHistory.Visible = false;

        }
        private void LoadCountry()
        {
            ddlCountry.DataSource = GetCountryList();
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "");
            SortDDL(ref this.ddlCountry);
        }
        private void CheckForButtonVisibility()
        {
            Contpcman = (ddlCampaignCI.SelectedValue == "PC") ? "true" : "false";
            Clement = (ddlCampaignCI.SelectedValue == "CL") ? "true" : "false";
        }
        private void GetAccountInfo(NewMasterPage MasterPage)
        {

            if (MasterPage != null)
            {
                AccountCI =  MasterPage.AccountNumberMaster.FormatAccountNumber();
                SessionFacade.AccountNo = MasterPage.AccountNumberMaster.FormatAccountNumber();
                SessionFacade.CampaignValue = MasterPage.CampaignMaster.SelectedValue;
                ddlCampaignCurrencyCI = MasterPage.CampaignCurrencyMaster;
                ddlCampaignCI = MasterPage.CampaignMaster;
            }

            if (!string.IsNullOrEmpty(ddlCampaignCI.SelectedValue))
            {
                if (!string.IsNullOrEmpty(ddlCampaignCurrencyCI.SelectedValue))
                {
                    ddlCampaignCurrencyCI.ClearSelection();
                }
                else
                {
                    ddlCampaignCurrencyCI.Items.Clear();

                    ddlCampaignCurrencyCI.Items.AddRange(ddlCampaignCI.Items.OfType<ListItem>().ToArray());
                }


                if (ddlCampaignCurrencyCI.Items.FindByValue(ddlCampaignCI.SelectedValue) != null)
                {
                    ddlCampaignCurrencyCI.Items.FindByValue(ddlCampaignCI.SelectedValue).Selected = true;
                }
                CurrencyCode.Value = ddlCampaignCurrencyCI.SelectedItem.Text;
            }
        }
        #endregion

        #region Qualifying Question
        protected void UpdateQQ(object sender, EventArgs e)
        {
            if (hdnEmailID.Value != "0")
            {
                gridSiteContactInfo.SelectedIndex = Int16.Parse(hdnEmailID.Value) - 1;
                if (gridSiteContactInfo.SelectedValue != null)
                {
                    //StatusButtonQQ(false);
                    StatusControlQQ(true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                        "call me", "  $(document).ready(function () {var mydiv = $('#pnlPopupCustInfo');mydiv.dialog({ autoOpen: false," +
                         "title: 'Contact Details'," +
                         "resizable: false," +
                         "dialogClass: 'FixedPostion'," +
                         "closeOnEscape: true," +
                         "width: 700," +
                         "open: function (type, data) {" +
                            " $(this).parent().appendTo('form'); " +
                         "}" +
                     "  }); mydiv.dialog('open');return false;}); ", true);
                }
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
            //StatusButtonQQ(true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                        "call me", "  $(document).ready(function () {var mydiv = $('#pnlPopupCustInfo');mydiv.dialog({ autoOpen: false," +
                         "title: 'Contact Details'," +
                         "resizable: false," +
                         "dialogClass: 'FixedPostion'," +
                         "closeOnEscape: true," +
                         "width: 700," +
                         "open: function (type, data) {" +
                            " $(this).parent().appendTo('form'); " +
                         "}" +
                     "  }); mydiv.dialog('open');return false;}); ", true);
        }
        protected void SaveQQ(object sender, EventArgs e)
        {
            if (hdnEmailID.Value != "0")
            {
                gridSiteContactInfo.SelectedIndex = Int16.Parse(hdnEmailID.Value) - 1;
                cUpdateQQ objUpdateQQ = new cUpdateQQ();
                GridViewRow row = gridSiteContactInfo.SelectedRow;

                if (row != null)
                {
                    AssignValue(row.Cells[GetColumnIndexByName(row, "FIRST NAME")].Text, row.Cells[GetColumnIndexByName(row, "LAST NAME")].Text);
                    objUpdateQQ.Account = SessionFacade.AccountNo;
                    objUpdateQQ.Campaign = SessionFacade.CampaignValue;
                    objUpdateQQ.Contact = GetContactNumber(row.Cells[GetColumnIndexByName(row, "CONTACT NUMBER")].Text);
                    objUpdateQQ.ContactStatus = ddlContactStatus.Text;
                    objUpdateQQ.ContBudget = "";
                    objUpdateQQ.ContFunc = ddlContFunc.Text;
                    objUpdateQQ.Factor = "";
                    objUpdateQQ.Other = "";
                    objUpdateQQ.Purchasing = "";
                    objUpdateQQ.SP = 0;
                    objUpdateQQ.SpVendor = 0;
                    objUpdateQQ.Username = SessionFacade.LoggedInUserName;

                    if (objUpdateQQ.UpdateQQ() == true)
                    {
                        lblUpdatedByWho.Text = SessionFacade.LoggedInUserName;
                        lblUpdatedDateWhen.Text = DateTime.Now.ToString("MMM dd, yyyy");
                        RefreshSiteContactInfo();
                        GetContactChanges();
                       // StatusControlQQ(false);
                        //StatusButtonQQ(true);

                      ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess()", true);


                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                        //   "call me", "  $(document).ready(function () {var mydiv = $('#pnlPopupCustInfo');mydiv.dialog({ autoOpen: false," +
                        //    "title: 'Contact Details'," +
                        //    "resizable: false," +
                        //    "dialogClass: 'FixedPostion'," +
                        //    "closeOnEscape: true," +
                        //    "width: 700," +
                        //    "open: function (type, data) {" +
                        //       " $(this).parent().appendTo('form'); " +
                        //    "}" +
                        //"  }); mydiv.dialog('open');return false;}); ", true);


                        

                    }
                }
            }

        }


        protected void StatusControlQQ(bool enabled = true)
        {
            ddlContactStatus.Enabled = enabled;
            ddlContFunc.Enabled = enabled;
        }
        //protected void StatusButtonQQ(bool enabled = true)
        //{
        //    //btnCancelQQ.Enabled = !enabled;
        //   // btnSaveQQ.Enabled = !enabled;
        //   // btnUpdateQQ.Enabled = enabled;
        //}

        protected void UpdatePC(object sender, EventArgs e)
        {
            if (hdnEmailID.Value != "0")
            {
                gridSiteContactInfo.SelectedIndex = Int16.Parse(hdnEmailID.Value) - 1;
                if (gridSiteContactInfo.SelectedValue != null)
                {
                    //StatusButtonPC(false);
                    StatusControlPC(true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                       "call me", "  $(document).ready(function () {var mydiv = $('#pnlPopupCustInfo');mydiv.dialog({ autoOpen: false," +
                        "title: 'Contact Details'," +
                        "resizable: false," +
                        "dialogClass: 'FixedPostion'," +
                        "closeOnEscape: true," +
                        "width: 700," +
                        "open: function (type, data) {" +
                           " $(this).parent().appendTo('form'); " +
                        "}" +
                    "  }); mydiv.dialog('open');return false;}); ", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                        "call me", "InformationMessage('Select a contact to be update.')", true);
                }
            }

        }
        protected void CancelPC(object sender, EventArgs e)
        {
            StatusControlPC(false);
            StatusButtonPC(true);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                       "call me", "  $(document).ready(function () {var mydiv = $('#pnlPopupCustInfo');mydiv.dialog({ autoOpen: false," +
                        "title: 'Contact Details'," +
                        "resizable: false," +
                        "dialogClass: 'FixedPostion'," +
                        "closeOnEscape: true," +
                        "width: 700," +
                        "open: function (type, data) {" +
                           " $(this).parent().appendTo('form'); " +
                        "}" +
                    "  }); mydiv.dialog('open');return false;}); ", true);
        }
        protected void SavePC(object sender, EventArgs e)
        {
            if (hdnEmailID.Value != "0")
            {
                gridSiteContactInfo.SelectedIndex = Int16.Parse(hdnEmailID.Value) - 1;
                //DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                cUpdateQQPC objUpdateQQPC = new cUpdateQQPC();
                GridViewRow row = gridSiteContactInfo.SelectedRow;

                if (row != null)
                {
                    AssignValue(row.Cells[GetColumnIndexByName(row, "FIRST NAME")].Text, row.Cells[GetColumnIndexByName(row, "LAST NAME")].Text);
                    objUpdateQQPC.Account = SessionFacade.AccountNo;
                    objUpdateQQPC.Campaign = SessionFacade.CampaignValue;
                    objUpdateQQPC.Contact = GetContactNumber(row.Cells[GetColumnIndexByName(row, "CONTACT NUMBER")].Text);
                    objUpdateQQPC.ContBudgets = ddlContactStatus.Text;
                    objUpdateQQPC.Health = ddlHealth.Text;
                    objUpdateQQPC.Spanish = ddlSpanish.Text;
                    objUpdateQQPC.EmployeeSize = txtEmployeeSizeValue.Text;
                    objUpdateQQPC.ContBudgets = "";
                    objUpdateQQPC.ContStats = ddlNewContactStatus.Text;
                    objUpdateQQPC.ContFunction = ddlJobArea.Text;
                    objUpdateQQPC.Username = SessionFacade.LoggedInUserName;
                    objUpdateQQPC.Qx = "";

                    if (objUpdateQQPC.UpdateQQPC() == true)
                    {
                        lblUpdatedbyPCWho.Text = SessionFacade.LoggedInUserName;
                        lblDateWho.Text = DateTime.Now.ToString("MMM dd, yyyy");
                       // StatusControlPC(false);
                       // StatusButtonPC(true);
                        RefreshSiteContactInfo();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SaveSuccess()", true);
                    }
                }
            }

        }
        protected void StatusControlPC(bool enabled)
        {
            //ddlAnnual.Enabled = enabled;
            ddlHealth.Enabled = enabled;
            ddlJobArea.Enabled = enabled;
            ddlNewContactStatus.Enabled = enabled;
            txtEmployeeSizeValue.Enabled = enabled;
            ddlSpanish.Enabled = enabled;
        }
        protected void StatusButtonPC(bool enabled)
        {
            btnSavePC.Enabled = !enabled;
        }
        #endregion

        #region New Contact
        protected void btnOkNew_Click(object sender, EventArgs e)
        {

            //DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            cAddNewContact objAddNewContact = new cAddNewContact();

            objAddNewContact.Account = SessionFacade.AccountNo;
            objAddNewContact.Campaign = SessionFacade.CampaignValue;
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


                //ModalPopupExtender2.Show();
            }
        }

        //protected void Show_NewContact(object sender, EventArgs e)
        //{
        //    //ModalPopupExtender2.Show();
        //}

        //protected void Reset_Click(object sender, EventArgs e)
        //{
        //    txtEmailNewContact.Text = string.Empty;
        //    txtFirstnameNewContact.Text = string.Empty;
        //    txtOtherNewContact.Text = string.Empty;
        //    txtPhoneNewContact.Text = string.Empty;
        //    txtLastanameNewContact.Text = string.Empty;
        //    ddlFunctionNewContact.Text = " ";
        //}
        #endregion

        #region Preferences
        protected void btnSavePreferences_Click(object sender, EventArgs e)
        {
            cAddPreferences objAddPreferences = new cAddPreferences();
            //DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;

            if (rdoMail.Text != "")
                objAddPreferences.Mail = rdoMail.Text.ToString().Trim();
            else
                objAddPreferences.Mail = "";

            if (rdoFax.Text != "")
                objAddPreferences.Fax = rdoFax.Text.ToString().Trim();
            else
                objAddPreferences.Fax = "";

            if (rdoEmail.Text != "")
                objAddPreferences.Email = rdoEmail.Text.ToString().Trim();
            else
                objAddPreferences.Email = "";

            if (rdoPhone.Text != "")
                objAddPreferences.Phone = rdoPhone.Text.ToString().Trim();
            else
                objAddPreferences.Phone = "";

            gridSiteContactInfo.SelectedIndex = Int16.Parse(hdnEmailID.Value) - 1;
            GridViewRow row = gridSiteContactInfo.SelectedRow;

            if (row == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                   "call me", "InformationMessage('Please select contact.')", true);
            }
            else
            {
                AssignValue(row.Cells[GetColumnIndexByName(row, "FIRST NAME")].Text, row.Cells[GetColumnIndexByName(row, "LAST NAME")].Text);

                objAddPreferences.Account = SessionFacade.AccountNo;
                objAddPreferences.Campaign = SessionFacade.CampaignValue;
                objAddPreferences.Username = SessionFacade.LoggedInUserName;
                objAddPreferences.Contact = int.Parse(IsNumeric(CheckSpace(GetContactNumber(row.Cells[GetColumnIndexByName(row, "CONTACT NUMBER")].Text))));
                objAddPreferences.ContactName = row.Cells[GetColumnIndexByName(row, "LAST NAME")].Text + ", " + row.Cells[GetColumnIndexByName(row, "FIRST NAME")].Text;

                if (objAddPreferences.AddPreferences() == true)
                {
                    GetPreferences();
                    //ModalPopupExtender4.Show();
                    //Retain Dialog
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                           "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                            "title: 'Master Data Change'," +
                            "width: 'auto'," +
                            "open: function (type, data) {" +
                               " $(this).parent().appendTo('form'); " +
                            "}" +
                        "  }); mydiv.dialog('open');return false;}); ", true);
                }
            }
        }

        protected void btnCancelPreferences_Click(object sender, EventArgs e)
        {
            rdoEmail.ClearSelection();
            rdoFax.ClearSelection();
            rdoMail.ClearSelection();
            rdoPhone.ClearSelection();
            //Retain Dialog
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                   "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                    "title: 'Master Data Change'," +
                    "width: 'auto'," +
                    "open: function (type, data) {" +
                       " $(this).parent().appendTo('form'); " +
                    "}" +
                "  }); mydiv.dialog('open');return false;}); ", true);
        }

        private DataSet BindPreferences()
        {
            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            DataSet dsPreferences = new DataSet();
            cAddPreferences objPreferences = new cAddPreferences();
            try
            {
                objPreferences.Account = txtTemp.Text;
                objPreferences.Campaign = ddlTemp.Text;
                dsPreferences = objPreferences.SelectPreferences();
                return dsPreferences;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Preferences - BindingData", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return null;
            }
        }

        private void GetPreferences()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
            Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + 
             "-SiteAndContactInfoPreferences" + ".xml";
            DataSet dsPreferences = new DataSet();

            try
            {
                dsPreferences = BindPreferences();

                if (dsPreferences != null)
                {
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    dsPreferences.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    //Check if Table Exists
                    if (dsPreferences != null)
                    {
                        if (dsPreferences.Tables.Count > 0)
                        {
                            //Check if Column Exists
                            if (dsPreferences.Tables[0].Columns.Contains("sold_to") &&
                                dsPreferences.Tables[0].Columns.Contains("createdby") &&
                                dsPreferences.Tables[0].Columns.Contains("createdon") &&
                                dsPreferences.Tables[0].Columns.Contains("Buyerct") &&
                                dsPreferences.Tables[0].Columns.Contains("ContactName") &&
                                dsPreferences.Tables[0].Columns.Contains("Mail") &&
                                dsPreferences.Tables[0].Columns.Contains("Fax") &&
                                dsPreferences.Tables[0].Columns.Contains("Email") &&
                                dsPreferences.Tables[0].Columns.Contains("Phone")
                                )
                            {
                                gridContactPreferences.DataSource = dsPreferences;
                                gridContactPreferences.DataBind();
                            }
                            else
                            {
                                gridContactPreferences.DataSource = null;
                                gridContactPreferences.DataBind();
                            }
                        }
                        else
                        {
                            gridContactPreferences.DataSource = null;
                            gridContactPreferences.DataBind();
                        }
                    }
                    else
                    {
                        gridContactPreferences.DataSource = null;
                        gridContactPreferences.DataBind();
                    }
                }
                else
                {
                    gridContactPreferences.DataSource = null;
                    gridContactPreferences.DataBind();
                }
                    
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Preferences - Bind Gridview", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
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
                GridViewSortDirection = SortDirection.Descending;
                SortgridPreferences(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirectionPreferences = SortDirection.Ascending;
                SortgridPreferences(sortExpression, "ASC");
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                 "title: 'Master Data Change'," +
                 "width: 'auto'," +
                 "open: function (type, data) {" +
                    " $(this).parent().appendTo('form'); " +
                 "}" +
             "  }); mydiv.dialog('open');return false;}); ", true);
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
            //DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;

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
            objAddAccountChanges.Campaign = SessionFacade.CampaignValue;

            if (objAddAccountChanges.AddAccountChanges() == true)
            {
                GetAccountChanges();
                //ModalPopupExtender4.Show();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                 "title: 'Master Data Change'," +
                 "width: 'auto'," +
                 "open: function (type, data) {" +
                    " $(this).parent().appendTo('form'); " +
                 "}" +
             "  }); mydiv.dialog('open');return false;}); ", true);
                txtAccountNameChanges.Text = txtSiteName.Text;
            }
        }
        protected void CancelAccountChanges_Click(object sender, EventArgs e)
        {
            txtAccountNameChanges.Text = string.Empty;
            txtAddress1AccountChanges.Text = string.Empty;
            txtAddress2AccountChanges.Text = string.Empty;
            txtCityAccountChanges.Text = string.Empty;
            txtCommentAccountChanges.Text = string.Empty;
            ddlCountry.Text = "";
            txtFaxNumberAccountChanges.Text = string.Empty;
            txtPhoneNumberAccountChanges.Text = string.Empty;
            txtStateAccountChanges.Text = string.Empty;
            txtZipAccountChanges.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                 "title: 'Master Data Change'," +
                 "width: 'auto'," +
                 "open: function (type, data) {" +
                    " $(this).parent().appendTo('form'); " +
                 "}" +
             "  }); mydiv.dialog('open');return false;}); ", true);
        }
        private DataSet BindAccountChanges()
        {
            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            DataSet dsAccountChanges = new DataSet();
            cAddAccountChanges objAccountChanges = new cAddAccountChanges();
            try
            {
                objAccountChanges.Account = txtTemp.Text;
                dsAccountChanges = objAccountChanges.SelectAccountChanges();
                return dsAccountChanges;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Account Changes - BindingData", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
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


                //Check if table exists.
                if (ds != null)
                {
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    if (ds.Tables.Count > 0)
                    {
                        //Check if column exists.
                        if (ds.Tables[0].Columns.Contains("sold_to") &&
                            ds.Tables[0].Columns.Contains("createdby") &&
                            ds.Tables[0].Columns.Contains("createdon") &&
                            ds.Tables[0].Columns.Contains("AccountName") &&
                            ds.Tables[0].Columns.Contains("Phone") &&
                            ds.Tables[0].Columns.Contains("Fax") &&
                            ds.Tables[0].Columns.Contains("Address1") &&
                            ds.Tables[0].Columns.Contains("Address2") &&
                            ds.Tables[0].Columns.Contains("City") &&
                            ds.Tables[0].Columns.Contains("State") &&
                            ds.Tables[0].Columns.Contains("Zip") &&
                            ds.Tables[0].Columns.Contains("Country") &&
                            ds.Tables[0].Columns.Contains("Comment"))
                        {
                            gridAccountChanges.DataSource = ds;
                            gridAccountChanges.DataBind();
                        }
                        else
                        {
                            gridAccountChanges.DataSource = null;
                            gridAccountChanges.DataBind();
                        }
                    }
                    else
                    {
                        gridAccountChanges.DataSource = null;
                        gridAccountChanges.DataBind();
                    }

                }
                else
                {
                    gridAccountChanges.DataSource = null;
                    gridAccountChanges.DataBind();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Account Changes - Bind Gridview", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
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
            //ModalPopupExtender4.Show();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                 "title: 'Master Data Change'," +
                 "width: 'auto'," +
                 "open: function (type, data) {" +
                    " $(this).parent().appendTo('form'); " +
                 "}" +
             "  }); mydiv.dialog('open');return false;}); ", true);
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
             //DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            if (hdnEmailID.Value != "0")
            {
                gridSiteContactInfo.SelectedIndex = Int16.Parse(hdnEmailID.Value) - 1;
                cAddContactChanges objAddContactChanges = new cAddContactChanges();
                GridViewRow row = gridSiteContactInfo.SelectedRow;

                AssignValue(row.Cells[GetColumnIndexByName(row, "FIRST NAME")].Text, row.Cells[GetColumnIndexByName(row, "LAST NAME")].Text);

                //Firstname
                //if (txtFirstname.Text != txtFirstnameContactChanges.Text.ToString().Trim())
               // if (txtPopFirstName.Text != txtFirstnameContactChanges.Text.ToString().Trim())
                    objAddContactChanges.Firstname = txtFirstnameContactChanges.Text;
                //else
                    //objAddContactChanges.Firstname = string.Empty;

                //Lastname
                //if (txtPopLastName.Text != txtLastnameContactChanges.Text.ToString().Trim())
                    objAddContactChanges.Lastname = txtLastnameContactChanges.Text;
                //else
                   // objAddContactChanges.Lastname = string.Empty;

                objAddContactChanges.Account = SessionFacade.AccountNo;
                objAddContactChanges.Comment = txtCommentContactChanges.Text;
                objAddContactChanges.Contact = int.Parse(IsNumeric(CheckSpace(GetContactNumber(row.Cells[GetColumnIndexByName(row, "CONTACT NUMBER")].Text))));
                objAddContactChanges.Department = txtDepartmentContactChanges.Text;
                objAddContactChanges.Email = txtEmailAddressContactChanges.Text;
                objAddContactChanges.Function = ddlFunctionContactChanges.Text;
                objAddContactChanges.Phone = txtPhoneContactChanges.Text;
                objAddContactChanges.PhoneExt = txtPhoneExtContactChanges.Text;
                objAddContactChanges.Status = ddlStatusContactChanges.Text;
                objAddContactChanges.Title = txtTitleContactChanges.Text;
                objAddContactChanges.Username = SessionFacade.LoggedInUserName;
                objAddContactChanges.Campaign = SessionFacade.CampaignValue;

                if (objAddContactChanges.AddContactChanges() == true)
                {
                    GetContactChanges();
                    //ModalPopupExtender4.Show();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                 "title: 'Master Data Change'," +
                 "width: 'auto'," +
                 "open: function (type, data) {" +
                    " $(this).parent().appendTo('form'); " +
                 "}" +
             "  }); mydiv.dialog('open');return false;}); ", true);
                }
            }
        }
        protected void CancelContactChanges_Click(object sender, EventArgs e)
        {
            txtCommentContactChanges.Text = string.Empty;
            txtFirstnameContactChanges.Text = string.Empty;
            txtLastnameContactChanges.Text = string.Empty;
            txtDepartmentContactChanges.Text = string.Empty;
            txtEmailAddressContactChanges.Text = string.Empty;
            ddlFunctionContactChanges.Text = "";
            txtPhoneContactChanges.Text = string.Empty;
            txtPhoneExtContactChanges.Text = string.Empty;
            ddlStatusContactChanges.Text = "";
            txtTitleContactChanges.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                 "title: 'Master Data Change'," +
                 "width: 'auto'," +
                 "open: function (type, data) {" +
                    " $(this).parent().appendTo('form'); " +
                 "}" +
             "  }); mydiv.dialog('open');return false;}); ", true);
        }
        private DataSet BindContactChanges()
        {
            TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
            DataSet dsContactChanges = new DataSet();
            cAddContactChanges objContactChanges = new cAddContactChanges();
            try
            {
                objContactChanges.Account = txtTemp.Text;
                dsContactChanges = objContactChanges.SelectContactChanges();
                return dsContactChanges;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Contact Changes - BindingData", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
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

                //check if table exists
                if (ds != null)
                {
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();
                    if (ds.Tables.Count > 0)
                    {
                        //check if column exists
                        if (ds.Tables[0].Columns.Contains("Sold_to") &&
                            ds.Tables[0].Columns.Contains("Createdby") &&
                            ds.Tables[0].Columns.Contains("Createdon") &&
                            ds.Tables[0].Columns.Contains("Buyerct") &&
                            ds.Tables[0].Columns.Contains("Firstname") &&
                            ds.Tables[0].Columns.Contains("Lastname") &&
                            ds.Tables[0].Columns.Contains("Status") &&
                            ds.Tables[0].Columns.Contains("Function") &&
                            ds.Tables[0].Columns.Contains("Title") &&
                            ds.Tables[0].Columns.Contains("Phone") &&
                            ds.Tables[0].Columns.Contains("PhoneExt") &&
                            ds.Tables[0].Columns.Contains("Department") &&
                            ds.Tables[0].Columns.Contains("Email_Address") &&
                            ds.Tables[0].Columns.Contains("comment"))
                        {
                            gridContactChanges.DataSource = ds;
                            gridContactChanges.DataBind();
                        }
                        else
                        {
                            gridContactChanges.DataSource = null;
                            gridContactChanges.DataBind();
                        }
                    }
                    else
                    {
                        gridContactChanges.DataSource = null;
                        gridContactChanges.DataBind();
                    }
                }
                else
                {
                    gridContactChanges.DataSource = null;
                    gridContactChanges.DataBind();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Contact Changes - Bind Gridview", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
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
            //ModalPopupExtender4.Show();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                "call me", "  $(document).ready(function () {var mydiv = $('#Panel4');mydiv.dialog({ autoOpen: false," +
                 "title: 'Master Data Change'," +
                 "width: 'auto'," +
                 "open: function (type, data) {" +
                    " $(this).parent().appendTo('form'); " +
                 "}" +
             "  }); mydiv.dialog('open');return false;}); ", true);
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

        protected void ArrangeColumn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                   "call me", "window.open('../Home/ArrangeColumns.aspx?Data=lvwContInfo','mywindow','width=700,height=400,scrollbars=yes')", true);
        }

        protected void btnShowSiteContactInfo_Click(object sender, EventArgs e)
        {
            //if (lblTitleSiteContactInfo.Text == "- Site Information")
            //{
                //this.cpeSiteContactInfo.Collapsed = true;
                //this.cpeSiteContactInfo.ClientState = "true";
              //  lblTitleSiteContactInfo.Text = "+ Site Information";
                //if (lblTitleContactInfo.Text == "+")
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 615px;");
                //else
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 413px;");
            //}
            //else
            //{
                //this.cpeSiteContactInfo.Collapsed = false;
                //this.cpeSiteContactInfo.ClientState = "false";
             //   lblTitleSiteContactInfo.Text = "- Site Information";
                //if (lblTitleContactInfo.Text == "+")
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 422px;");
                //else
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 220px;");
           // }
        }

        protected void btnTitleContactInfo_Click(object sender, EventArgs e)
        {
            //if (lblTitleContactInfo.Text == "-")
            //{
                //this.cpeTabsSiteContact.Collapsed = true;
                //this.cpeTabsSiteContact.ClientState = "true";
                //lblTitleContactInfo.Text = "+";
                //if (lblTitleSiteContactInfo.Text == "+")
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 615px;");
                //else
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 422px;");
            //}
            //else
            //{
                //this.cpeTabsSiteContact.Collapsed = false;
                //this.cpeTabsSiteContact.ClientState = "false";
                //lblTitleContactInfo.Text = "-";
                //if (lblTitleSiteContactInfo.Text == "+")
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 413px;");
                //else
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 220px;");
            //}
        }

        protected void btnSearchContactInfo_Click(object sender, EventArgs e)
        {
            //if (lblSearchContactInfo.Text == "-")
            //{
            //this.cpeSearchContact.Collapsed = true;
                //this.cpeSearchContact.ClientState = "true";
              //  lblSearchContactInfo.Text = "+";
                //if (lblTitleSiteContactInfo.Text == "+")
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 615px;");
                //else
                //    DataDiv.Style.Add("CollapsedTrue", "overflow: auto; border: 1px solid olive; height: 422px;");
            //}
            //else
            //{
            //    this.cpeSearchContact.Collapsed = false;
            //    this.cpeSearchContact.ClientState = "false";
            //    lblSearchContactInfo.Text = "-";
            //}
        }
    }
}