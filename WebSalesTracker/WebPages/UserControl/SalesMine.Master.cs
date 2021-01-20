using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data.SqlClient;
using AppLogic;


namespace WebSalesMine.WebPages.UserControl
{
    public partial class SalesMine : System.Web.UI.MasterPage
    {
        public string KamWindowName = "KamWindow";
        public string KamWindowStatus = "NOFalse";
        public string pcman = "false";

        protected void SetButtonVisible()
        {
            //Hide kam Button & Quotes if Campaign is PC
            //lblVersion.Text = new WebHelper().Version().ToString();
            //lblCopyRights.Text = new WebHelper().CopyRight().ToString();
            if (ddlCampaign.Text == "PC")
            {
                LiBtnKam.Visible = false;
                LiQuotes.Visible = false;
            }
            else
            {
                LiBtnKam.Visible = true;
                LiQuotes.Visible = true;
            }

            //Hide Custoemr LookUp Button & also bring Site & Contact Info page as first if Campaign is PC and role is PC-Man
            if ((ddlCampaign.Text == "PC") && SessionFacade.UserRole.Trim() == "PC-MAN")
            {
                LiCustomerLookUp.Visible = false;
                pcman = "true";
            }
            else
            {
                LiCustomerLookUp.Visible = true;
                pcman = "false";
            }

            //Hide kam Button if Kam id is not there
            if (string.IsNullOrEmpty(SessionFacade.KamId))
            {
                LiBtnKam.Visible = false;
            }
            else
            {
                LiBtnKam.Visible = true;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            
            //if (string.IsNullOrEmpty(SessionFacade.KamName))
            //{
            //    Response.Redirect(FormsAuthenticationHelper.LoginURL, true);
            //}

            if (string.IsNullOrEmpty(SessionFacade.LoggedInUserName))
            {
                Response.Redirect(FormsAuthenticationHelper.LoginURL, true);
            }


            LoadCampaign();
            SetCampaignLogo();
            SetAccountInformation();
            uppanelMain.Update();
        }

        public void CloseMyWindow()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "callme", "closeChildWindows();", true);
            //ScriptManager.RegisterStartupScript(this.uppanelMain, typeof(string), "callme", "closeChildWindows();", true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtAccountNumber.Attributes.Add("onfocus()", "FocusValueAccount(this);");
                
                if (string.IsNullOrEmpty(SessionFacade.CampaignName))
                {
                    SetCampaignInSession();
                }
                SetCampaignLogo();
                SetNavigateUrlForLogo();
                UserInformation();
              //  KamWindowName = SessionFacade.KamId + " - " + SessionFacade.KamName;
                // masterBody.Attributes.Add("onLoad", "javascript:dock('right');javascript:createWindowWithRemotingUrl('" + SessionFacade.KamId + "','" + SessionFacade.KamName + "');");

                //if (HttpContext.Current.Session["ShowWindow"] != null)
                //{
                //    KamWindowStatus = HttpContext.Current.Session["ShowWindow"].ToString();
                //}

              //  masterBody.Attributes.Add("onLoad", "javascript:test2('" + KamWindowStatus + "','" + KamWindowName +"');");
                //Tag When you Click Cancel in Contact Level


               

                if (CheckIfCancelOK.Value != "Cancel")
                {
                    if (ContactLevelNumber.Value != "" && ContactLevelNumber.Value != "OK")
                    {
                        if (ContactLevelNumber.Value != txtContactNumber.Text)
                        {
                            HttpCookie aCookie = new HttpCookie("CName");
                            aCookie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(aCookie);

                            HttpCookie myCookie = new HttpCookie("CNo");
                            myCookie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(myCookie);

                            ContactLevelNumber.Value = "OK";
                            RefeshPageDontDeleteThread();
                        }
                    }
                    else if (ContactLevelNumber.Value == "OK")
                    {
                        SetContactInformation();
                        ContactLevelNumber.Value = "";
                    }
                    else
                        SetContactInformation();
                }
                if (AccountNumber.Value != "")
                {
                    if (AccountNumber.Value != txtAccountNumber.Text)
                    {
                        SessionFacade.AccountNo = txtAccountNumber.Text;
                        SetAccountInformation();
                        AccountNumber.Value = "";
                        if (SessionFacade.AccountNo == "")
                        {
                            if (txtContactNumber.Text.ToString().Trim() != "")
                            {
                                HttpCookie aCookie = new HttpCookie("CName");
                                aCookie.Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies.Add(aCookie);

                                HttpCookie myCookie = new HttpCookie("CNo");
                                myCookie.Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies.Add(myCookie);

                                ContactLevelNumber.Value = "OK";
                                RefeshPageDontDeleteThread();
                            }
                        }
                    }
                    else
                        SetAccountInformation();
                }
                else
                    SetAccountInformation();
                if (txtContactNumber.Text == "")
                {
                    if (Request.Cookies["CNo"] != null)
                    {
                        HttpCookie aCookie = new HttpCookie("CName");
                        aCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(aCookie);

                        HttpCookie myCookie = new HttpCookie("CNo");
                        myCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(myCookie);

                        lblContactName.Text = string.Empty;
                    }
                }

                DisplayIconsAsPerRole(SessionFacade.UserRole, true);
                SetButtonVisible();
            }
            catch (Exception ex)
            {
                string test = ex.ToString();
                Response.Redirect("../login/login.aspx");
            }
        }

        private void RedirerctPage()
        {
            if (Session["MasterButton"] != null)
            {
                if (Session["MasterButton"].ToString() != "")
                {
                    Response.Redirect("../ProductSummary/ProductSummary.aspx", true);
                }
            }
        }

        public void KamPage_KamWindow_Click_Event(object sender, EventArgs e)
        {
            Response.Redirect("../Admin/ManageUsers.aspx");
        }

        #region DisplayIconsAsPerRole
        private void DisplayIconsAsPerRole(string Role, bool visibility)
        {
            LiOrderHistory.Visible = visibility;
            LiProductSummar.Visible = visibility;
            LiSiteContactInfo.Visible = visibility;
            LiNotesComHist.Visible = visibility;
            LiCustomerLookUp.Visible = visibility;
            LiBtnKam.Visible = visibility;

            if (Role == "PC-ONT" || Role == "PC-MAN")
            {
                LiQuotes.Visible = false;
            }
            else
            {
                LiQuotes.Visible = visibility;
            }
            if (Role == "ADMIN" || Role == "MANAGERS")
            {
                LiManageUsers.Visible = visibility;
            }
            else
            {
                LiManageUsers.Visible = false;
            }

        }
        #endregion

        #region Set User & Account No Information
        private void UserInformation()
        {

            //ucHeader.UserName = "Welcome " + SessionFacade.KamName;
            lblUser.Text = "Welcome " + SessionFacade.KamName;
        }

        private void SetAccountInformation()
        {
            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
            {
                txtAccountNumber.Text = "";
                txtAccountNumber.Text = SessionFacade.AccountNo;
            }
            //ucHeader.UserName = "Welcome " + SessionFacade.KamName;
            lblUser.Text = "Welcome " + SessionFacade.KamName;
            SetAccountName();
        }

        private void SetAccountName()
        {
            try
            {
                //lblAccountName.Text = "";
                //cAccount objAccountName = new cAccount();

                //objAccountName.CampaignName = ddlCampaign.SelectedValue.ToString().Trim();
                //objAccountName.AccountName = SessionFacade.AccountNo;

                //SqlDataReader drCampaign = objAccountName.GetAccountNameOnly();
                //if (drCampaign.HasRows)
                //{
                //    while (drCampaign.Read())
                //    {
                //        lblAccountName.Text = (string)drCampaign["name"]; //drCampaign.GetString(0);
                //    }


                //}
                //drCampaign.Close();
            }
            catch (Exception err)
            {

            }
        }

        #endregion

        #region Set Contact No and Name
        private void SetContactInformation()
        {
            if (Request.Cookies["CNo"] != null)
            {
                txtContactNumber.Text = Request.Cookies["CNo"].Value;
                //  SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
            }
            else
            {
                txtContactNumber.Text = string.Empty;
                // SessionFacade.BuyerCt = string.Empty;
            }

            if (Request.Cookies["CName"] != null)
            {
                if (Request.Cookies["CName"].Value.ToString().Contains("%"))
                {
                    string CName = Request.Cookies["CName"].Value;

                    CName = CName.Replace("%2C", ",");
                    CName = CName.Replace("%20", " ");
            
                    HttpCookie myCookies = new HttpCookie("CName");
                    myCookies.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookies);

                    myCookies = new HttpCookie("CName");
                    myCookies.Value = CName;
                    myCookies.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(myCookies);

                    lblContactName.Text = CName;
                    SessionFacade.ContactName = CName;
                }
                else
                {
                    lblContactName.Text = Request.Cookies["CName"].Value;
                    SessionFacade.ContactName = Request.Cookies["CName"].Value;
                }
            }
            else
            {
                lblContactName.Text = string.Empty;
                SessionFacade.ContactName = string.Empty;
            }
        }
        #endregion

        #region Set Campaign in/from session, CampaignLogo ,NavigateUrlForLogo
        private void SetCampaignInSession()
        {
            SessionFacade.CampaignName = ddlCampaign.SelectedItem.Value;
            SessionFacade.CampaignValue = ddlCampaign.SelectedItem.Value;
        }

        private void SetCampaignFromSession()
        {
            if (!string.IsNullOrEmpty(SessionFacade.CampaignName))
            {
                ddlCampaign.ClearSelection();

                ddlCampaign.Items.FindByValue(SessionFacade.CampaignValue).Selected = true;

            }
        }

        private void SetCampaignLogo()
        {
            string imagename = SessionFacade.CampaignValue + ".gif";
            try
            {
                //ucHeader.LogoUrl = "~/App_Themes/Images/" + imagename;
                //imgbradylogo.ImageUrl = "~/App_Themes/Images/" + imagename;
            }
            catch
            {
                //ucHeader.LogoUrl = "../App_Themes/Images/brady-logo.gif";
                //imgbradylogo.ImageUrl = "../App_Themes/Images/brady-logo.gif";
            }

        }

        private void SetNavigateUrlForLogo()
        {
            //ucHeader.LogoNavigateUrl = "~/WebPages/Home/Main.aspx";

        }
        #endregion

        private void LoadCampaign()
        {
            if (!string.IsNullOrEmpty(SessionFacade.UserRole))
            {
                //if role is admin then show all campaign in dropdown
                if (SessionFacade.UserRole == "ADMIN")
                {
                    cCampaign objCampaign = new cCampaign();
                    //SqlDataReader drCampaign = objCampaign.GetCampaignList();

                    //if (drCampaign.HasRows)
                    //{
                    //    ddlCampaign.DataSource = drCampaign;
                    //    ddlCampaign.DataTextField = "campaignName";
                    //    ddlCampaign.DataValueField = "campaignValue";
                    //    ddlCampaign.DataBind();
                    //    SetCampaignFromSession();
                    //}
                    //drCampaign.Close();


                }
                else
                {

                    if (SessionFacade.LoggedCampaignName == "ADMIN")
                    {
                        //if role is not there and kam id is there and campagin is there
                        if (!string.IsNullOrEmpty(SessionFacade.KamId))
                        {
                            if (SessionFacade.KamId.Contains("EMED"))
                            {
                                ListItem L1;

                                L1 = new ListItem("EMED ", "EMED");
                                ddlCampaign.Items.Insert(0, L1);
                            }
                            else if (SessionFacade.KamId.Contains("SETCN"))
                            {
                                ListItem L1;

                                L1 = new ListItem("CA ", "CA");
                                ddlCampaign.Items.Insert(0, L1);
                                SessionFacade.CampaignName = "CA";
                                SessionFacade.CampaignValue = "CA";

                            }
                            else
                            {
                                ListItem L1;

                                L1 = new ListItem("EMED ", "EMED");
                                ddlCampaign.Items.Insert(0, L1);
                                SessionFacade.CampaignName = "EMED";
                                SessionFacade.CampaignValue = "EMED";
                            }
                        }
                    }
                    else
                    {
                        // KaM ID IS NULL
                        //SCENIRIO : Userrole is PC-Man,Campaign = PC and kamid is null
                        if (SessionFacade.UserRole == "PC-MAN" || SessionFacade.UserRole == "PC-ONT")
                        {
                            ListItem L1;

                            L1 = new ListItem("PC", "PC");
                            ddlCampaign.Items.Insert(0, L1);
                            SessionFacade.CampaignName = "PC";
                            SessionFacade.CampaignValue = "PC";
                        }
                        else if (SessionFacade.UserRole == "MANAGERS")
                        {
                            ListItem L1;

                            L1 = new ListItem(SessionFacade.LoggedCampaignName, SessionFacade.LoggedCampaignName);
                            ddlCampaign.Items.Insert(0, L1);
                            SessionFacade.CampaignName = SessionFacade.LoggedCampaignName;
                            SessionFacade.CampaignValue = SessionFacade.LoggedCampaignName;
                        }
                        // KaM ID IS NULL
                        //SCENIRIO : Userrole is some other campaiign then PC and kamid is null and Role is null
                        else
                        {
                            ListItem L1;

                            L1 = new ListItem(SessionFacade.UserRole, SessionFacade.UserRole);
                            ddlCampaign.Items.Insert(0, L1);
                            SessionFacade.CampaignName = SessionFacade.UserRole;
                            SessionFacade.CampaignValue = SessionFacade.UserRole;
                        }
                    }

                }
            }


        }

        protected void ddlCampaign_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                SetCampaignInSession();
                SetCampaignLogo();
                SetAccountInformation();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "closeChildWindows()", true);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "RefreshWinodw()", true);
                uppanelMain.Update();
                SetButtonVisible();
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaing Change", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),SessionFacade.KamId.ToString());
            }

        }

        protected void txtContactNumber_TextChanged(object sender, EventArgs e)
        {
            //if (txtContactNumber.Text.Length < 0)
            //{
            //    SessionFacade.BuyerCt = string.Empty;
            //}

        }

        protected void RefeshPage()
        {
            SessionFacade.BuyerCt = "";
            if (Path.GetFileName(Request.PhysicalPath) == "OrderHistory.aspx")
                Response.Redirect("../OrderHistory/OrderHistory.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "ProductSummary.aspx")
                Response.Redirect("../ProductSummary/ProductSummary.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "NotesCommHistory.aspx")
                Response.Redirect("../NotesCommHistory/NotesCommHistory.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "Quotes.aspx")
                Response.Redirect("../Quotes/Quotes.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "Main.aspx")
                Response.Redirect("../Home/Main.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "SiteAndContactInfo.aspx")
                Response.Redirect("../SiteAndContactInfo/SiteAndContactInfo.aspx");
        }

        protected void RefeshPageDontDeleteThread()
        {
            if (txtContactNumber.Text.Length > 0)
            {

            }
            else
            {
                SessionFacade.BuyerCt = "";
            }
            if (Path.GetFileName(Request.PhysicalPath) == "OrderHistory.aspx")
                Response.Redirect("../OrderHistory/OrderHistory.aspx", false);
            else if (Path.GetFileName(Request.PhysicalPath) == "ProductSummary.aspx")
                Response.Redirect("../ProductSummary/ProductSummary.aspx", false);
            else if (Path.GetFileName(Request.PhysicalPath) == "NotesCommHistory.aspx")
                Response.Redirect("../NotesCommHistory/NotesCommHistory.aspx", false);
            else if (Path.GetFileName(Request.PhysicalPath) == "Quotes.aspx")
                Response.Redirect("../Quotes/Quotes.aspx", false);
            else if (Path.GetFileName(Request.PhysicalPath) == "Main.aspx")
                Response.Redirect("../Home/Main.aspx", false);
            else if (Path.GetFileName(Request.PhysicalPath) == "SiteAndContactInfo.aspx")
                Response.Redirect("../SiteAndContactInfo/SiteAndContactInfo.aspx", false);
        }

        protected void AccountNoRefeshPage()
        {
            if (Path.GetFileName(Request.PhysicalPath) == "OrderHistory.aspx")
                Response.Redirect("../OrderHistory/OrderHistory.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "ProductSummary.aspx")
                Response.Redirect("../ProductSummary/ProductSummary.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "NotesCommHistory.aspx")
                Response.Redirect("../NotesCommHistory/NotesCommHistory.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "Quotes.aspx")
                Response.Redirect("../Quotes/Quotes.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "Main.aspx")
                Response.Redirect("../Home/Main.aspx");
            else if (Path.GetFileName(Request.PhysicalPath) == "SiteAndContactInfo.aspx")
                Response.Redirect("../SiteAndContactInfo/SiteAndContactInfo.aspx");
        }

        protected void imbContactLevel_Click(object sender, ImageClickEventArgs e)
        {
            RefeshPage();
        }

        protected void imbSearchAcntNumber_Click1(object sender, EventArgs e)
        {
            try
            {
                string regexString = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexString);

                if (txtAccountNumber.Text != "" && txtAccountNumber.Text.Length > 0)
                {
                    SessionFacade.BuyerCt = string.Empty;
                    SessionFacade.ContactName = string.Empty;
                    HttpCookie aCookie = new HttpCookie("CName");
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);

                    HttpCookie myCookie = new HttpCookie("CNo");
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookie);

                    if (regex.IsMatch(txtAccountNumber.Text.Trim()))
                    {
                        SessionFacade.AccountNo = txtAccountNumber.Text;
                    }

                }
                SetAccountInformation();
            }
            catch (Exception err)
            {
                SessionFacade.AccountNo = "123123";
                BradyCorp.Log.LoggerHelper.LogException(err, "Account no - Search Button Click", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }


            RefeshPage();
        }

        protected void txtContactNumber_TextChanged1(object sender, EventArgs e)
        {
           
        }

        protected void imbContactLevel_Click1(object sender, EventArgs e)
        {
            RefeshPage();
        }

        protected void txtAccountNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string regexString = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexString);

                if (txtAccountNumber.Text != "" && txtAccountNumber.Text.Length > 0)
                {
                    HttpCookie aCookie = new HttpCookie("CName");
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);

                    HttpCookie myCookie = new HttpCookie("CNo");
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookie);

                    SessionFacade.BuyerCt = string.Empty;
                    SessionFacade.ContactName = string.Empty;

                    if (regex.IsMatch(txtAccountNumber.Text.Trim()))
                    {
                        SessionFacade.AccountNo = txtAccountNumber.Text;
                    }
                }

                SetAccountInformation();
            }
            catch (Exception err)
            {
                SessionFacade.AccountNo = "123123";
                BradyCorp.Log.LoggerHelper.LogException(err, "Main Page - Error in Account No change ", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void Hypelogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            
            string nextpage = "login.aspx";

            FormsAuthenticationHelper.SignOut();

            SalesMine MyMasterPage = (SalesMine)Page.Master;

            MyMasterPage.CloseMyWindow();

            //ScriptManager.RegisterStartupScript(this.tblHeaderTable, typeof(string), "callme", " window.open('../Home/KamWindow.aspx','mywindow','width=910,height=300,scrollbars=no');  window.close();", true);


            //HttpCookie loginCookie = new HttpCookie("loginCookie", Session.SessionID);
            //loginCookie.Domain = "mydomain.com";

            HttpCookie loginCookie = new HttpCookie("loginCookie");
         
            Response.Cookies.Remove("loginCookie");
            Response.Cookies.Add(loginCookie);
            loginCookie.Values.Add("username", "s");
            loginCookie.Values.Add("CampaignName", "");
            loginCookie.Values.Add("CampaignValue", "");
            loginCookie.Values.Add("UserRole", "");
            loginCookie.Values.Add("LoggedInUserName", "");
            loginCookie.Values.Add("KamName", "");
            DateTime dtExpiry = DateTime.Now.AddDays(-1); //you can add years and months too here
            Response.Cookies["loginCookie"].Expires = dtExpiry;
            Response.Redirect("../Login/login.aspx");
        }

    }
}