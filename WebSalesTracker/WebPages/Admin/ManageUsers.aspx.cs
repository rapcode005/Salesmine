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
//using System.Web.Mail;
using System.Net.Mail;

namespace WebSalesMine.WebPages.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        public static SqlConnection OnlineConstre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaOnlineCONNECTON"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showgrid();
                GetAllCampaignList();
                GetAllPageList();
                DisableStatusControl();
                GetEmailMessage();
                GetAllNoteType();
                if (SessionFacade.Email_Address == "" || SessionFacade.Email_Address == null)
                {
                    //Email
                    DataTable responseldape = new DataTable();
                    responseldape = new LDAPAccess().Findmail(SessionFacade.LoggedInUserName);
                    if (responseldape != null)
                    {
                        if (responseldape.Rows.Count > 0)
                        {
                            txtFromEmail.Text = responseldape.Rows[0].ItemArray[0].ToString();
                        }
                    }
                }
                else
                    txtFromEmail.Text = SessionFacade.Email_Address;
            }
        }

        #region Display in Grid
        public void showgrid()
        {
            cUser objUser = new cUser();
            string PathnameAll = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageUser" + ".xml";
            string PathnameSearchByLetter = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageUserByLetter" + ".xml";
            try
            {
                objUser.SearchManageUserByCampaign = "";
                objUser.SearchManageUserByUserName = "";
                DataSet drExisting;
                drExisting = objUser.GetUsersList();
                DataView dv = new DataView();

                if (drExisting.Tables[0].Rows.Count > 0)
                {
                    //Writing XML All unit 
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameAll);
                    drExisting.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    dv = GetById(SessionFacade.SearchLetter);

                    if (dv.Count > 0)
                    {
                        drExisting = new DataSet();
                        drExisting.Tables.Add(dv.ToTable());

                        //Writing XML Search by Letter
                        xmlSW = new System.IO.StreamWriter(PathnameSearchByLetter);
                        drExisting.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        grdError.DataSource = drExisting;
                        grdError.DataBind();
                    }
                    else
                    {
                        drExisting = new DataSet();
                        drExisting.Tables.Add(dv.ToTable());

                        DataRow dr = drExisting.Tables[0].NewRow();
                        dr[0] = "test";
                        drExisting.Tables[0].Rows.Add(dr);

                        // Define all of the columns you are binding in your GridView

                        grdError.DataSource = drExisting;
                        grdError.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Login Page - Button Login Click Error", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }
        #endregion

        void BindGrid(DataSet ds)
        {
            grdError.DataSource = ds;
            grdError.DataBind();
            grdError.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void ShowAllAdmin(object sender, EventArgs e)
        {
            //txtbSearchUser.Text = "";
            litErrorinGrid.Text = "";
            //rdoAccountName.Checked = false;
            //rdoCampaignGroup.Checked = false;

            showgrid();
        }

        #region Search user
        //protected void ShowAdminBySearch(object sender, EventArgs e)
        //{
        //    cUser objMember = new cUser();
        //    if (rdoAccountName.Checked)
        //    {
        //        objMember.SearchManageUserByCampaign = "";
        //        objMember.SearchManageUserByUserName = txtbSearchUser.Text.ToString();
        //        DataSet drExisting;
        //        drExisting = objMember.GetUsersList();

        //        if (drExisting.Tables[0].Rows.Count > 0)
        //        {

        //            litErrorinGrid.Text = "";
        //            BindGrid(drExisting);

        //        }

        //        else
        //        {

        //            litErrorinGrid.Text = "No Records Found";
        //        }

        //    }
        //    else if (rdoCampaignGroup.Checked)
        //    {
        //        objMember.SearchManageUserByCampaign = txtbSearchUser.Text.ToString();
        //        objMember.SearchManageUserByUserName = "";
        //        DataSet drExisting;
        //        drExisting = objMember.GetUsersList();

        //        if (drExisting.Tables[0].Rows.Count > 0)
        //        {
        //            litErrorinGrid.Text = "";
        //            BindGrid(drExisting);

        //        }
        //        else
        //        {
        //            litErrorinGrid.Text = "No Records Found";
        //        }
        //    }

        //    else { litErrorinGrid.Text = "Please choose filter creteria"; }


        //}


        #endregion

        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            foreach (GridViewRow row in grdError.Rows)
            {
                DropDownList dpRoles = null;
                dpRoles = row.FindControl("DropDownListUserRole") as DropDownList;
                if (dpRoles != null)
                {
                    try
                    {
                        // cUser objUser = new cUser();
                        //DataSet drRoleExisting = null;
                        DataTable table = new DataTable();
                        if (ddl.SelectedValue.ToString().ToUpper() == "PC")
                        {


                            table.Columns.Add("RoleName", typeof(string));
                            table.Rows.Add("PC-ONT");
                            table.Rows.Add("PC-MAN");
                            table.Rows.Add("MANAGERS");
                            table.Rows.Add("ADMIN");
                            table.Rows.Add("PC(ADMIN)");
                            table.Rows.Add("CUS");


                            dpRoles.DataSource = table;
                            dpRoles.DataTextField = "RoleName";
                            dpRoles.DataValueField = "RoleName";
                            dpRoles.DataBind();


                            break;
                        }
                        else if (ddl.SelectedValue.ToString().ToUpper() == "US")
                        {

                            table.Columns.Add("RoleName", typeof(string));
                            table.Rows.Add(ddl.SelectedValue);
                            table.Rows.Add("MANAGERS");
                            table.Rows.Add("ADMIN");
                            table.Rows.Add("US(CONS)");
                            table.Rows.Add("CUS");

                            dpRoles.DataSource = table;
                            dpRoles.DataTextField = "RoleName";
                            dpRoles.DataValueField = "RoleName";
                            dpRoles.DataBind();
                        }
                        else if (ddl.SelectedValue.ToString().ToUpper() == "CA")
                        {
                            table.Columns.Add("RoleName", typeof(string));
                            table.Rows.Add(ddl.SelectedValue);
                            table.Rows.Add("MANAGERS");
                            table.Rows.Add("ADMIN");
                            table.Rows.Add("CA(CONS)");
                            table.Rows.Add("CUS");

                            dpRoles.DataSource = table;
                            dpRoles.DataTextField = "RoleName";
                            dpRoles.DataValueField = "RoleName";
                            dpRoles.DataBind();
                        }
                        else
                        {
                            table.Columns.Add("RoleName", typeof(string));
                            table.Rows.Add(ddl.SelectedValue);
                            table.Rows.Add("MANAGERS");
                            table.Rows.Add("ADMIN");
                            table.Rows.Add("CUS");

                            dpRoles.DataSource = table;
                            dpRoles.DataTextField = "RoleName";
                            dpRoles.DataValueField = "RoleName";
                            dpRoles.DataBind();


                            break;
                        }

                    }
                    catch (Exception Err)
                    {
                        litErrorinGrid.Text = Err.ToString();
                    }
                }
            }
        }

        protected void DropDownListUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                foreach (GridViewRow row in grdError.Rows)
                {
                    DropDownList ddl1 = row.FindControl("DropDownList1") as DropDownList;
                    LinkButton lnkButton = row.FindControl("DropDownListChoose") as LinkButton;
                    if (ddl.SelectedValue.ToString() == "CUS")
                    {
                        if (lnkButton != null)
                        {
                            if (ddl1 != null)
                            {
                                SessionFacade.Campaign_Name_Value_Unit = ddl1.SelectedValue.ToString();
                                SessionFacade.Campaign_Name_Unit = ddl1.SelectedItem.Text;
                                lnkButton.CssClass = "";
                            }
                        }
                    }
                    else
                    {
                        if (lnkButton != null)
                            lnkButton.CssClass = "DisplayNone";
                    }
                }

            }
            catch (Exception Err)
            {
                litErrorinGrid.Text = Err.ToString();
            }
        }

        public DataTable GetDatafromXML()
        {
            DataSet ds = new DataSet();
            string PathnameAll = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageUser" + ".xml";
            try
            {
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(PathnameAll, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);

                    return dtTemp;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetDatafromXMLByLetter()
        {
            DataSet ds = new DataSet();

            string PathnameSearchByLetter = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageUserByLetter" + ".xml";
            try
            {
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(PathnameSearchByLetter, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);

                    return dtTemp;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public DataView GetById(string id)
        {
            if (id == "")
            {
                SessionFacade.SearchLetter = "A";
                id = "A";
            }
            else
                SessionFacade.SearchLetter = id.Replace("%", "");

            string PathnameSearchByLetter = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageUserByLetter" + ".xml";
            //Fetch record from database using like operator.
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt = GetDatafromXML();
            DataView dv = dt.DefaultView;

            dv.RowFilter = "USERNAME LIKE '" + SessionFacade.SearchLetter + "%'";

            ds = new DataSet();
            ds.Tables.Add(dv.ToTable());

            //Writing XML Search by Letter
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameSearchByLetter);
            ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();



            return dv;
        }

        private void ShowNoResultFound(DataTable source, GridView gv)
        {

            for (int i = 65; i <= (65 + 25); i++)
            {
                LinkButton lb = new LinkButton();

                lb.Text = Char.ConvertFromUtf32(i) + " ";

                lb.CommandArgument = "%" + Char.ConvertFromUtf32(i) + "%";
                lb.CommandName = "AlphaPaging";


            }
        }

        #region RetrunLDAP UserName Through Email

        protected void BtnGetLdapUser_Click(object sender, EventArgs e)
        {
            //Jonmar, Please read the Notes in orde rhistory page 
            GetLdapUserFunction();
        }

        public void GetLdapUserFunction()
        {

            btnOk.Visible = true;
            OpenPopup();
        }

        private void OpenPopup()
        {
            ratePanel.Visible = true;
            ModalPopupExtender1.Show();
        }

        private void ClosePopup()
        {
            ratePanel.Visible = false;
            ModalPopupExtender1.Hide();
        }

        private void ClearPopup()
        {
            txtbLdapEmail.Text = string.Empty;
            txtbLdapFirstName.Text = string.Empty;
            txtbLdapuserName.Text = string.Empty;
            litLdapError.Text = string.Empty;
        }


        protected void lnkFetch_Click(object sender, EventArgs e)
        {
            GetLdapInfo(txtbLdapEmail.Text);
        }

        protected void btnOk_Click1(object sender, EventArgs e)
        {
            ClearPopup();
            ClosePopup();
        }


        protected void GetLdapInfo(string strLDAPEMAIL)
        {
            try
            {
                DataTable responseldape = null;
                responseldape = new LDAPAccess().Finduser(strLDAPEMAIL);

                if (responseldape != null)
                {
                    txtbLdapEmail.Text = responseldape.Rows[0][0].ToString();
                    txtbLdapFirstName.Text = responseldape.Rows[0][2].ToString();

                    txtbLdapuserName.Text = responseldape.Rows[0][1].ToString();
                    litLdapError.Text = "";
                }
                else
                {
                    litLdapError.Text = "User Detail doesn't exist.Please check the entered user email id is correct.";
                }

                OpenPopup();
            }
            catch
            {
                litLdapError.Text = "User Detail doesn't exist.Please check the entered user email id is correct.";
                OpenPopup();
            }
        }

        #endregion

        protected void btnSaveShow_Click(object sender, EventArgs e)
        {
            try
            {
                string CampaignValueList = string.Empty;
                string CampaignNameList = string.Empty;
                string PageNameList = string.Empty;
                //int Count = chkCampaignList.Items.Cast<ListItem>().Count(li => li.Selected);

                //For CampaignList
                foreach (ListItem item in chkCampaignList.Items)
                {
                    if (item.Selected)
                    {
                        CampaignValueList = CampaignValueList + item.Value.ToString().Trim() + ",";
                        CampaignNameList = CampaignNameList + item.Text.ToString().Trim() + ",";
                    }
                }

                CampaignValueList = CampaignValueList.Remove(CampaignValueList.Length - 1);
                CampaignNameList = CampaignNameList.Remove(CampaignNameList.Length - 1);

                cUser objInsertPageName = new cUser();


                if (Username.Value != "M")
                {
                    objInsertPageName.createdby = Username.Value;
                    objInsertPageName.campaignUnit = CampaignValueList;
                    objInsertPageName.campaignFullName = CampaignNameList;

                    //For PageList
                    foreach (ListItem item in chkPageList.Items)
                    {
                        if (item.Selected)
                        {
                            PageNameList = PageNameList + item.Value.ToString().Trim() + ",";
                        }
                    }

                    PageNameList = PageNameList.Remove(PageNameList.Length - 1);

                    objInsertPageName.PageNameList = PageNameList;

                    if (objInsertPageName.AddCutomizedScreen())
                    {

                    }
                }
                else
                {
                    for (int index = 0; index <= lbNtLoginMutiple.Items.Count - 1; index++)
                    {
                        objInsertPageName.createdby = lbNtLoginMutiple.Items[index].Text;
                        objInsertPageName.campaignUnit = CampaignValueList;
                        objInsertPageName.campaignFullName = CampaignNameList;

                        //For PageList
                        foreach (ListItem item in chkPageList.Items)
                        {
                            if (item.Selected)
                            {
                                PageNameList = PageNameList + item.Value.ToString().Trim() + ",";
                            }
                        }

                        PageNameList = PageNameList.Remove(PageNameList.Length - 1);

                        objInsertPageName.PageNameList = PageNameList;

                        if (objInsertPageName.AddCutomizedScreen())
                        {

                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                   "call me", "SaveScreen()", true);
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Saving Customized Page", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        [System.Web.Services.WebMethod]
        public static DataSet GetAllCustomizedPage()
        {
            try
            {
                cUser objGetCusPage = new cUser();
                objGetCusPage.createdby = SessionFacade.Selected_Username;
                DataSet dsCusPage = objGetCusPage.SelectCutomizedScreen();
                return dsCusPage;
            }
            catch
            {
                return null;
            }
        }

        protected void GetAllCampaignList()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            cCampaign objCampaign = new cCampaign();
            try
            {
                ds = objCampaign.GetAllCampaignList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    chkCampaignList.DataSource = ds;
                    chkCampaignList.DataValueField = "CampaignValue";
                    chkCampaignList.DataTextField = "CampaignName";
                    chkCampaignList.DataBind();

                    //Mutiple User
                    ddlCampaignMutiple.DataSource = ds;
                    ddlCampaignMutiple.DataValueField = "CampaignValue";
                    ddlCampaignMutiple.DataTextField = "CampaignName";
                    ddlCampaignMutiple.DataBind();

                    //Mutiple User For User Role
                    table.Columns.Add("RoleName", typeof(string));
                    table.Rows.Add("CL");
                    table.Rows.Add("CUS");
                    table.Rows.Add("MANAGERS");
                    table.Rows.Add("ADMIN");

                    ddlUserRoleMutiple.DataSource = table;
                    ddlUserRoleMutiple.DataTextField = "RoleName";
                    ddlUserRoleMutiple.DataValueField = "RoleName";
                    ddlUserRoleMutiple.DataBind();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Binding in Checkboxlist for Manage User", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        protected void GetAllPageList()
        {
            DataSet ds = new DataSet();
            cUser objPage = new cUser();
            try
            {
                ds = objPage.GetAllPageList();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        chkPageList.DataSource = ds;
                        chkPageList.DataValueField = "PageName";
                        chkPageList.DataTextField = "PageName";
                        chkPageList.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Binding in Checkboxlist for Manage User", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        protected void GetEmailMessage()
        {
            DataSet ds = new DataSet();
            cUser objEmail = new cUser();

            try
            {
                ds = objEmail.ShowEmailMessage();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                        txtMessageEmail.Text = ds.Tables[0].Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Binding in Showing Email Message for Manage User", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void GetAllCampaignData()
        {
            DataSet ds = new DataSet();
            cUser objEmail = new cUser();

            try
            {
                ds = objEmail.ShowEmailMessage();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                        txtMessageEmail.Text = ds.Tables[0].Rows[0]["Message"].ToString();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Binding in Showing Email Message for Manage User", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void GetAllNoteType()
        {
            DataSet ds = new DataSet();
            cUser objNoteType = new cUser();
            try
            {
                ds = objNoteType.ShowAllNoteType();

                if (ds != null && ds.Tables.Count > 0)
                {
                    grdNoteTypeList.DataSource = ds;
                    grdNoteTypeList.DataBind();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error:GetAllNoteType - Manage Users Page",
                   SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                   SessionFacade.KamId.ToString());
            }
        }

        //Use for binding
        public DataSet GetNoteType()
        {
            DataSet ds = new DataSet();
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            cNotesCommHistory objNoteType = new cNotesCommHistory();
            try
            {

                objNoteType.CampaignName = ddlTemp.SelectedValue;

                ds = objNoteType.GetNoteType();

                return ds;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error:GetNoteType - Manage Users Page",
                   SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                   SessionFacade.KamId.ToString());
                return null;
            }
        }

        //[System.Web.Services.WebMethod]
        protected void btnSaveMutiple_Click(object sender, EventArgs e)
        {
            try
            {
                string FullName = string.Empty;
                string SalesTeamID = string.Empty;
                string Username = string.Empty;

                //With NTlogin, Full Name and KAM ID
                if (lbNtLoginMutiple.Items.Count > 0 && lbSalesTeamMutiple.Items.Count > 0 &&
                    lbFullNameMutiple.Items.Count > 0)
                {

                    for (int index = 0; index <= lbNtLoginMutiple.Items.Count - 1; index++)
                    {
                        cUser objMember = new cUser();

                        objMember.UserName = lbNtLoginMutiple.Items[index].Text;
                        objMember.CampaignName = ddlCampaignMutiple.SelectedValue;
                        objMember.KamId = lbSalesTeamMutiple.Items[index].Text;
                        objMember.KamName = lbFullNameMutiple.Items[index].Text;
                        objMember.UserRole = ddlUserRoleMutiple.SelectedValue;
                        objMember.CreatedEditedBy = SessionFacade.LoggedInUserName;

                        if (objMember.InsertUser() == true)
                        {

                        }
                    }
                }

                //With NTlogin and Full Name
                else if (lbNtLoginMutiple.Items.Count > 0 && lbSalesTeamMutiple.Items.Count == 0 &&
                    lbFullNameMutiple.Items.Count > 0)
                {
                    for (int index = 0; index <= lbNtLoginMutiple.Items.Count - 1; index++)
                    {
                        cUser objMember = new cUser();

                        objMember.UserName = lbNtLoginMutiple.Items[index].Text;
                        objMember.CampaignName = ddlCampaignMutiple.SelectedValue;
                        objMember.KamId = "";
                        objMember.KamName = lbFullNameMutiple.Items[index].Text;
                        objMember.UserRole = ddlUserRoleMutiple.SelectedValue;
                        objMember.CreatedEditedBy = SessionFacade.LoggedInUserName;

                        if (objMember.InsertUser() == true)
                        {

                        }
                    }
                }

                 //With NTlogin and KAM ID
                else if (lbNtLoginMutiple.Items.Count > 0 && lbSalesTeamMutiple.Items.Count > 0 &&
                    lbFullNameMutiple.Items.Count == 0)
                {
                    for (int index = 0; index <= lbNtLoginMutiple.Items.Count - 1; index++)
                    {
                        cUser objMember = new cUser();

                        objMember.UserName = lbNtLoginMutiple.Items[index].Text;
                        objMember.CampaignName = ddlCampaignMutiple.SelectedValue;
                        objMember.KamId = lbSalesTeamMutiple.Items[index].Text;
                        objMember.KamName = "";
                        objMember.UserRole = ddlUserRoleMutiple.SelectedValue;
                        objMember.CreatedEditedBy = SessionFacade.LoggedInUserName;

                        if (objMember.InsertUser() == true)
                        {

                        }
                    }
                }

                //With NTlogin 
                else if (lbNtLoginMutiple.Items.Count > 0 && lbSalesTeamMutiple.Items.Count == 0 &&
                    lbFullNameMutiple.Items.Count == 0)
                {
                    for (int index = 0; index <= lbNtLoginMutiple.Items.Count - 1; index++)
                    {
                        cUser objMember = new cUser();

                        objMember.UserName = lbNtLoginMutiple.Items[index].Text;
                        objMember.CampaignName = ddlCampaignMutiple.SelectedValue;
                        objMember.KamId = "";
                        objMember.KamName = "";
                        objMember.UserRole = ddlUserRoleMutiple.SelectedValue;
                        objMember.CreatedEditedBy = SessionFacade.LoggedInUserName;

                        if (objMember.InsertUser() == true)
                        {

                        }
                    }
                }

                //To Refresh
                showgrid();

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                   "call me", "SaveMutiple()", true);
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Saving Mutiple User", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void ddlCampaignMutiple_SelectIndexChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            if (ddlCampaignMutiple.SelectedValue == "PC")
            {

                table.Columns.Add("RoleName", typeof(string));
                table.Rows.Add("PC-ONT");
                table.Rows.Add("PC-MAN");
                table.Rows.Add("PC(ADMIN)");
            }

            else if (ddlCampaignMutiple.SelectedValue == "CA" || ddlCampaignMutiple.SelectedValue == "US")
            {

                table.Columns.Add("RoleName", typeof(string));
                table.Rows.Add(ddlCampaignMutiple.SelectedValue);
                table.Rows.Add(ddlCampaignMutiple.SelectedValue + "(CONS)");
            }

            else
            {
                table.Columns.Add("RoleName", typeof(string));
                table.Rows.Add(ddlCampaignMutiple.SelectedValue);
            }

            table.Rows.Add("ADMIN");
            table.Rows.Add("MANAGER");
            table.Rows.Add("CUS");

            ddlUserRoleMutiple.DataSource = table;
            ddlUserRoleMutiple.DataMember = "RoleName";
            ddlUserRoleMutiple.DataBind();

            if (ddlUserRoleMutiple.SelectedValue == "CUS")
                lnkCampaignChoose.CssClass = "";
            else
                lnkCampaignChoose.CssClass = "DisplayNone";

        }

        protected void ddlUserRoleMutiple_SelectIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserRoleMutiple.SelectedValue == "CUS")
                lnkCampaignChoose.CssClass = "";
            else
                lnkCampaignChoose.CssClass = "DisplayNone";
        }

        protected void btnAddNtLoginMutiple_Click(object sender, EventArgs e)
        {
            lbNtLoginMutiple.Items.Add(txtNtLoginMutiple.Text);

            txtNtLoginMutiple.Text = "";
        }

        protected void btnAddSalesTeamMutiple_Click(object sender, EventArgs e)
        {
            lbSalesTeamMutiple.Items.Add(txtSalesTeamMutiple.Text);

            txtSalesTeamMutiple.Text = "";
        }

        protected void txtToEmail_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddFullNameMutiple_Click(object sender, EventArgs e)
        {
            lbFullNameMutiple.Items.Add(txtFullNameMutiple.Text);

            txtFullNameMutiple.Text = "";
        }

        protected void ClearListBox(object sender, EventArgs e)
        {
            lbSalesTeamMutiple.Items.Clear();
            lbFullNameMutiple.Items.Clear();
            lbNtLoginMutiple.Items.Clear();
        }

        protected void SendEmail(object sender, EventArgs e)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                MailAddress fromAddress = new MailAddress(txtFromEmail.Text.ToString().Replace(",", ""));
                message.From = fromAddress;

                //Add Recipient
                string[] To = txtToEmail.Text.ToString().Replace(", ", ",").Split(',');
                for (int i = 0; i < To.Length; i++)
                    message.To.Add(To[i]);

                //Add CC
                if (txtCCEMail.Text.ToString().Trim() != "")
                {
                    string[] CC = txtCCEMail.Text.ToString().Replace(", ", ",").Split(',');
                    for (int i = 0; i < CC.Length; i++)
                        message.CC.Add(CC[i]);
                }

                message.Subject = txtSubjectEmail.Text.ToString().Replace(",", "");
                message.IsBodyHtml = true;
                message.Body = txtMessageEmail.Text.Replace("\n", "<br />");
                smtpClient.Host = "smtp.gmail.com";   // We use gmail as our smtp client
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials =
                    new System.Net.NetworkCredential(txtFromEmail.Text, SessionFacade.Pass_Word);

                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                //ex.Message;
            }
        }

        protected void bntEditEmail_Click(object sender, EventArgs e)
        {
            if (bntEditEmail.Text == "Edit Message")
            {
                EnableStatusControl();
                bntEditEmail.Text = "Update";
                btnCancelEmail.CssClass = "button";

            }
            else
            {
                cUser objEmail = new cUser();
                objEmail.EmailMessage = txtMessageEmail.Text;
                if (objEmail.EditMessageEmail())
                {

                }
                bntEditEmail.Text = "Edit Message";
                DisableStatusControl();
                btnCancelEmail.CssClass = "DisplayNone";
            }
        }

        protected void btnCancelEmail_Click(object sender, EventArgs e)
        {
            bntEditEmail.Text = "Edit Message";
            DisableStatusControl();
            btnCancelEmail.CssClass = "DisplayNone";
        }

        protected void AddCC(object sender, EventArgs e)
        {
            lblCCEmail.CssClass = "LabelFont";
            txtCCEMail.CssClass = "textbox curved";
        }

        private void DisableStatusControl()
        {
            txtMessageEmail.ReadOnly = true;
            txtMessageEmail.BackColor = System.Drawing.Color.LightGray;
        }

        private void EnableStatusControl()
        {
            txtMessageEmail.ReadOnly = false;
            txtMessageEmail.BackColor = System.Drawing.Color.White;
        }

        #region Manage NoteType
        protected void grdNoteTypeList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                string[] NoteTypeTest = NoteTypeValues.Value.ToString().Split('|');

                grdNoteTypeList.EditIndex = e.NewEditIndex;
                GetAllNoteType();
                
                //DropDownList DropDownListCampaign = ((DropDownList)grdError.Rows[e.NewEditIndex].FindControl("DropDownList1"));
                //DropDownList DropDownListUserRole = ((DropDownList)grdError.Rows[e.NewEditIndex].FindControl("DropDownListUserRole"));
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddCA")), NoteTypeTest[0]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddEMED")), NoteTypeTest[1]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddUS")), NoteTypeTest[2]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddPC")), NoteTypeTest[3]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddCL")), NoteTypeTest[4]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddUK")), NoteTypeTest[5]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddSUK")), NoteTypeTest[6]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddAT")), NoteTypeTest[7]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddDE")), NoteTypeTest[8]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddCH")), NoteTypeTest[9]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddBE")), NoteTypeTest[10]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddFR")), NoteTypeTest[11]);
                AssignDropDownList(((DropDownList)grdNoteTypeList.Rows[e.NewEditIndex].FindControl("ddlddNL")), NoteTypeTest[12]);

                ScriptManager.RegisterStartupScript(this, GetType(), "call me", "ShowManageNoteType();", true);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error:grdNoteTypeList_RowEditing - Manage Users Page",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                    SessionFacade.KamId.ToString());
            }
        }

        protected void grdNoteTypeList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grdNoteTypeList.EditIndex = -1;
                GetAllNoteType();
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error:grdNoteTypeList_RowCancelingEdit - Manage Users Page",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                    SessionFacade.KamId.ToString());
            }

        }

        //Function use for assigning previous value in Manage Note Type 
        private void AssignDropDownList(DropDownList DropDownListCampaign,string value)
        {
            try
            {
                DropDownListCampaign.SelectedValue = value;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error:AssignDropDownList - Manage Users Page",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                    SessionFacade.KamId.ToString());
            }
        }

        #endregion

        #region GrdError
        protected void grdError_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region Paging for the Grid
        //protected void grdErrorDataPageEventHandler(object sender, GridViewPageEventArgs e)
        //{
        //    litErrorinGrid.Text = "";
        //    DataSet drExisting;
        //    cUser objMember = new cUser();

        //    if (rdoAccountName.Checked == true && txtbSearchUser.Text.Length > 0)
        //    {
        //        objMember.SearchManageUserByCampaign = "";
        //        objMember.SearchManageUserByUserName = txtbSearchUser.Text.ToString();
        //    }
        //    if (rdoCampaignGroup.Checked == true && txtbSearchUser.Text.Length > 0)
        //    {
        //        objMember.SearchManageUserByCampaign = txtbSearchUser.Text.ToString();
        //        objMember.SearchManageUserByUserName = "";
        //    }

        //    if (txtbSearchUser.Text.Length == 0)
        //    {
        //        objMember.SearchManageUserByCampaign = "";
        //        objMember.SearchManageUserByUserName = "";
        //    }

        //    drExisting = objMember.GetUsersList();
        //    grdError.DataSource = drExisting;
        //    grdError.PageIndex = e.NewPageIndex;
        //    grdError.DataBind();

        //}
        #endregion

        #region Sorting For the Grid

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

        protected void grdError_Sorting(object sender, GridViewSortEventArgs e)
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

        private void SortGridView(string sortExpression, string direction)
        {

            // You can cache the DataTable for improving performance

            DataSet drExisting = new DataSet();
            cUser objMember = new cUser();

            DataTable dt = drExisting.Tables[0];
            DataView dv = new DataView(dt);

            dv.Sort = sortExpression + " " + direction;
            grdError.DataSource = dv;
            grdError.DataBind();


        }
        #endregion

        #region Cancel Edit Click on Grid
        protected void grdError_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdError.EditIndex = -1;
            grdError.DataSource = GetDatafromXMLByLetter();
            grdError.DataBind();

        }
        #endregion

        protected void grdError_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdError.EditIndex = e.NewEditIndex;
                showgrid();
                DataSet drExisting = new DataSet();
                drExisting.Tables.Add(GetDatafromXMLByLetter());

                if (drExisting.Tables[0].Rows.Count > 0)
                {
                    litErrorinGrid.Text = "";
                    SessionFacade.Selected_Username = drExisting.Tables[0].Rows[e.NewEditIndex].ItemArray[0].ToString();
                    BindGrid(drExisting);

                    DropDownList DropDownListCampaign = ((DropDownList)grdError.Rows[e.NewEditIndex].FindControl("DropDownList1"));
                    DropDownList DropDownListUserRole = ((DropDownList)grdError.Rows[e.NewEditIndex].FindControl("DropDownListUserRole"));
                    //TextBox txtKamID = ((TextBox)grdError.Rows[e.NewEditIndex].FindControl("txtbKamId"));
                    //TextBox txtFullName = ((TextBox)grdError.Rows[e.NewEditIndex].FindControl("txtbKamName"));

                    DropDownListCampaign.SelectedValue = CampaignValuePrevious.Value.Trim();
                    if (UserRolePrevious.Value.Trim() == "")
                        UserRolePrevious.Value = DropDownListCampaign.SelectedValue.ToString().ToUpper();
                    DropDownListUserRole.SelectedValue = UserRolePrevious.Value.Trim();

                    //Check if equal
                    if (DropDownListUserRole.SelectedValue != UserRolePrevious.Value.Trim())
                    {
                        DropDownListUserRole.Items.Clear();
                        DataTable table = new DataTable();
                        if (DropDownListCampaign.SelectedValue.ToString().ToUpper() == "PC")
                        {


                            table.Columns.Add("RoleName", typeof(string));
                            table.Rows.Add("PC-ONT");
                            table.Rows.Add("PC-MAN");
                            table.Rows.Add("MANAGERS");
                            table.Rows.Add("ADMIN");
                            table.Rows.Add("PC(ADMIN)");
                            table.Rows.Add("CUS");


                            DropDownListUserRole.DataSource = table;
                            DropDownListUserRole.DataTextField = "RoleName";
                            DropDownListUserRole.DataValueField = "RoleName";
                            DropDownListUserRole.DataBind();

                        }
                        else if (DropDownListCampaign.SelectedValue.ToString().ToUpper() == "US")
                        {

                            table.Columns.Add("RoleName", typeof(string));
                            table.Rows.Add(DropDownListCampaign.SelectedValue);
                            table.Rows.Add("MANAGERS");
                            table.Rows.Add("ADMIN");
                            table.Rows.Add("US(CONS)");
                            table.Rows.Add("CUS");

                            DropDownListUserRole.DataSource = table;
                            DropDownListUserRole.DataTextField = "RoleName";
                            DropDownListUserRole.DataValueField = "RoleName";
                            DropDownListUserRole.DataBind();
                        }
                        else if (DropDownListCampaign.SelectedValue.ToString().ToUpper() == "CA")
                        {
                            table.Columns.Add("RoleName", typeof(string));
                            table.Rows.Add(DropDownListCampaign.SelectedValue);
                            table.Rows.Add("MANAGERS");
                            table.Rows.Add("ADMIN");
                            table.Rows.Add("CA(CONS)");
                            table.Rows.Add("CUS");

                            DropDownListUserRole.DataSource = table;
                            DropDownListUserRole.DataTextField = "RoleName";
                            DropDownListUserRole.DataValueField = "RoleName";
                            DropDownListUserRole.DataBind();
                        }
                        else
                        {
                            table.Columns.Add("RoleName", typeof(string));
                            table.Rows.Add(DropDownListCampaign.SelectedValue);
                            table.Rows.Add("MANAGERS");
                            table.Rows.Add("ADMIN");
                            table.Rows.Add("CUS");

                            DropDownListUserRole.DataSource = table;
                            DropDownListUserRole.DataTextField = "RoleName";
                            DropDownListUserRole.DataValueField = "RoleName";
                            DropDownListUserRole.DataBind();

                        }

                        DropDownListUserRole.SelectedValue = UserRolePrevious.Value.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error:grdError_RowEditing - Manage Users Page",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                    SessionFacade.KamId.ToString());
            }
        }

        #region Delete Row from Grid
        protected void grdError_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lb = (Label)grdError.Rows[e.RowIndex].FindControl("Label6");
                string dUserName = lb.Text.Trim();
                DeleteRecord(dUserName);
                showgrid();
            }
            catch
            {
                litErrorinGrid.Text = "Error in Deleting Record";
                showgrid();
            }

        }


        private void DeleteRecord(string dUserName)
        {
            try
            {
                cUser objMember = new cUser();
                objMember.DeleteUserName = dUserName;
                if (objMember.DeleteUser())
                {
                    litErrorinGrid.Text = "User - " + dUserName + " Sucessfully deleted from system.";
                }

                else
                {
                    litErrorinGrid.Text = "User - " + dUserName + " Sucessfully deleted from system.";
                }
            }

            catch (SqlException ee)
            {

            }

            finally
            {

            }
        }
        #endregion

        #region Add Record from Grid
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            litErrorinGrid.Text = "";
            //string strResult = @"SELECT USERNAME,CAMPAIGN as 'CampaignName',admPass,KamId,KamName,USERROLE,'Active' as Status FROM USER_PROFILE where valid_to > getdate() order by username asc ";

            ////SqlDataAdapter da = new SqlDataAdapter("select em.ErrorID, em.ErrorCategoryID, em.ErrorMessage,ec.Errorcategoryname from ErrorMessage  em, errorcategory ec where em.ErrorCategoryID = ec.ErrorCategoryID", con);
            //SqlDataAdapter da = new SqlDataAdapter(strResult, OnlineConstre);

            DataTable dt = new DataTable();
            //da.Fill(dt);

            dt = GetDatafromXMLByLetter();

            // Here we'll add a blank row to the returned DataTable
            DataRow dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 0);
            //Creating the first row of GridView to be Editable
            grdError.EditIndex = 0;
            grdError.DataSource = dt;
            grdError.DataBind();
            //Changing the Text for Inserting a New Record
            ((LinkButton)grdError.Rows[0].Cells[0].Controls[0]).Text = "Insert";

        }

        #endregion

        #region Update in Grid
        protected void grdError_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            TextBox ReadOnlyTextBox = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtUSERNAME"));
            if (((LinkButton)grdError.Rows[0].Cells[0].Controls[0]).Text == "Insert")
            {

                cUser objMember = new cUser();

                ReadOnlyTextBox.ReadOnly = false;

                DropDownList ddl = (DropDownList)grdError.Rows[e.RowIndex].FindControl("DropDownList1");
                DropDownList ddlRoles = (DropDownList)grdError.Rows[e.RowIndex].FindControl("DropDownListUserRole");
                //DropDownList ddl1 = (DropDownList)grdError.Rows[e.RowIndex].FindControl("ddlErrIdentifier");

                string str = ddl.SelectedItem.Text.ToString();
                // string str1 = ddl1.SelectedItem.Text.ToString();

                SessionFacade.Selected_Username = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtUSERNAME")).Text.Trim();

                objMember.UserName = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtUSERNAME")).Text.Trim();
                objMember.CampaignName = ddl.SelectedValue.ToString().Trim();
                objMember.KamId = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtbKamId")).Text.Trim();
                objMember.KamName = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtbKamName")).Text.Trim();
                objMember.UserRole = ddlRoles.SelectedValue.ToString().Trim();
                objMember.CreatedEditedBy = SessionFacade.LoggedInUserName;

                try
                {
                    if (objMember.InsertUser() == true)
                    {
                        litErrorinGrid.Text = "User sucessfully added";

                    }
                }
                catch (Exception ex)
                {
                    litErrorinGrid.Text = "Error in adding new user";
                    BradyCorp.Log.LoggerHelper.LogMessage("Manage Users - Error in Adding New user");

                }
                grdError.EditIndex = -1;
                showgrid();
            }
            else
            {
                //TextBox txtUpdateErrorIdentifier = (TextBox)grdError.Rows[e.RowIndex].FindControl("txtErrorIdentifier");
                //txtUpdateErrorIdentifier.ReadOnly = true;
                ReadOnlyTextBox.ReadOnly = true;
                DropDownList ddl = (DropDownList)grdError.Rows[e.RowIndex].FindControl("DropDownList1");
                DropDownList ddlRoles = (DropDownList)grdError.Rows[e.RowIndex].FindControl("DropDownListUserRole");

                Label lb = (Label)grdError.Rows[e.RowIndex].FindControl("Label6");

                string str = ddl.SelectedItem.Text.ToString();
                string str1 = ddl.SelectedItem.Value.ToString();


                SessionFacade.Selected_Username = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtUSERNAME")).Text.Trim();

                cUser objMember = new cUser();

                objMember.UserName = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtUSERNAME")).Text.Trim();
                objMember.CampaignName = ddl.SelectedValue.ToString().Trim();
                objMember.KamId = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtbKamId")).Text.Trim();
                objMember.KamName = ((TextBox)grdError.Rows[e.RowIndex].FindControl("txtbKamName")).Text.Trim();
                objMember.UserRole = ddlRoles.SelectedValue.ToString().Trim();
                objMember.CreatedEditedBy = SessionFacade.LoggedInUserName;

                if (objMember.UpdateUsers())
                {
                    grdError.EditIndex = -1;
                    showgrid();
                }
                grdError.EditIndex = -1;
                showgrid();

            }

        }
        #endregion

        protected void grdError_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            litErrorinGrid.Text = "";
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    //Load Error Category to DropDown
                    DropDownList dp = (DropDownList)e.Row.FindControl("DropDownList1");
                    cCampaign objCampaign = new cCampaign();
                    DataSet drExisting = null;

                    try
                    {
                        drExisting = objCampaign.GetCampaignList1();

                        if (drExisting.Tables[0].Rows.Count > 0)
                        {

                            dp.DataSource = drExisting.Tables[0];
                            dp.DataTextField = "campaignName";
                            dp.DataValueField = "campaignValue";
                            dp.DataBind();
                        }

                    }
                    catch (Exception err)
                    {
                        litErrorinGrid.Text = err.ToString();
                    }
                    finally
                    {


                    }
                    //Load User Roles in DropDown\
                    DropDownList dpRoles = (DropDownList)e.Row.FindControl("DropDownListUserRole");
                    DataTable table = new DataTable();
                    if (dp.SelectedValue.ToString().ToUpper() == "PC")
                    {
                        table.Columns.Add("RoleName", typeof(string));
                        table.Rows.Add("PC-ONT");
                        table.Rows.Add("PC-MAN");
                        table.Rows.Add("CUS");
                        table.Rows.Add("MANAGERS");
                        table.Rows.Add("ADMIN");
                        table.Rows.Add("PC(ADMIN)");

                        dpRoles.DataSource = table;
                        dpRoles.DataTextField = "RoleName";
                        dpRoles.DataValueField = "RoleName";
                        dpRoles.DataBind();

                    }
                    else
                    {
                        table.Columns.Add("RoleName", typeof(string));
                        table.Rows.Add(dp.SelectedValue);
                        table.Rows.Add("CUS");
                        table.Rows.Add("MANAGERS");
                        table.Rows.Add("ADMIN");


                        dpRoles.DataSource = table;
                        dpRoles.DataTextField = "RoleName";
                        dpRoles.DataValueField = "RoleName";
                        dpRoles.DataBind();

                    }

                }
            }
        }

        protected void grdError_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                TableCell cell = e.Row.Cells[0];
                cell.ColumnSpan = 2;

                for (int i = 65; i <= (65 + 25); i++)
                {
                    LinkButton lb = new LinkButton();

                    lb.Text = Char.ConvertFromUtf32(i) + " ";

                    lb.CommandArgument = "%" + Char.ConvertFromUtf32(i) + "%";
                    lb.CommandName = "AlphaPaging";

                    cell.Controls.Add(lb);

                }
            }
        }

        protected void grdError_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //At first I check that if the CommandName is “AlphaPaging”
            if (e.CommandName.Equals("AlphaPaging"))
            {
                DataTable dt = new DataTable();
                DataView dv = GetById(e.CommandArgument.ToString());
                dt = dv.ToTable().Clone();
                if (dv.Count > 0)
                {
                    dt = dv.ToTable();

                    grdError.DataSource = dt;
                    grdError.DataBind();

                    grdError.Rows[0].Visible = true;
                }
                else
                {
                    dt = dv.ToTable();

                    DataRow dr = dt.NewRow();
                    dr[0] = "";
                    dt.Rows.Add(dr);

                    grdError.DataSource = dt;
                    grdError.DataBind();

                    grdError.Rows[0].Visible = false;

                }


            }
        }
        #endregion
    }
}