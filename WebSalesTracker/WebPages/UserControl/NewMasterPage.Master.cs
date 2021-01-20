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
using System.Collections.Specialized;
using System.Collections.Generic;

namespace WebSalesMine.WebPages.UserControl
{
    public partial class NewMasterPage : System.Web.UI.MasterPage
    {
        #region Properties
        public string AccountNumberMaster
        {
            get { return txtAccountNumber.Text; }
            set { this.txtAccountNumber.Text = value; }
        }

        public string AccountNameMaster
        {
            get { return lblAccountName.Text; }
            set { this.lblAccountName.Text = value; }
        }

        public DropDownList CampaignMaster
        {
            get { return ddlCampaign; }
            set { this.ddlCampaign = value; }
        }

        public DropDownList CampaignCurrencyMaster
        {
            get { return ddlCampaignValue; }
            set { this.ddlCampaignValue = value; }
        }

        public string CampaignValueListPro
        {
            get { return CampaignValueList.Value; }
            set { this.CampaignValueList.Value = value; }
        }

        public string CampaignNameListPro
        {
            get { return CampaignNameList.Value; }
            set { this.CampaignNameList.Value = value; }
        }
        #endregion

        public string pcman = "false";
        public string varConst = string.Empty;
        public string varHideAccountSearch = "false";
        public string varMining = "false";
        public string CA = "false";
        public string EMED = "false";
        public string ViewOnHoldOrder = "false";
        public string querystring = "";
        public string ViewQuotePipelineDG = "true";

        protected void ClearCSS_Click(object sender, EventArgs e)
        {
            HttpCookie aCookie2 = new HttpCookie("CSS");
            aCookie2.Value = "None";
            aCookie2.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(aCookie2);
        }

        #region SetButtonVisible
        protected void SetButtonVisible()
        {
            //UserRole is not Customized
            if (SessionFacade.UserRole != "Cus")
            {
                //Hide KAM Window
                LiBtnKam.Visible = (CheckForKAM()) ?
                    false : true;
                KAMDivider.Visible = (CheckForDividerKAM()) ?
                    false : true;

                //Hide Quote Pages
                LiQuotes.Visible = (ddlCampaign.SelectedValue.In("PC", "CL")) ?
                    false : true;

                //Hide Quote Pipeline and Discount
                LiQuotesOver1K.Visible = (CheckForQuotesPipelineGuidedance()) ?
                    false : true;
                LiQuoteGuidance.Visible = (CheckForQuotesPipelineGuidedance()) ?
                    false : true;

                //Hide Custoemr LookUp Button 
                LiCustomerLookUp.Visible = CheckForCustomerLookup() ?
                    false : true;
                pcman = (CheckForCustomerLookup()) ?
                    "true" : "false";

                //Hide Manage Message
                messagedivider.Visible = (CheckManageMessage()) ?
                    true : false;
                LiMessage.Visible = (CheckManageMessage()) ?
                    true : false;

                //Hide or unhide Search Box
                searchbox.Visible = (CheckAccountSearch()) ? false : true;
                lblAccountName.Visible = (CheckAccountSearch()) ? false : true;

                //switch (Path.GetFileName(Request.PhysicalPath))
                //{
                //    case "CustomerLookUp.aspx":
                //    case "ManageUsers.aspx":
                //    case "ManageMessage.aspx":
                //    case "QuotesOver1K.aspx":
                //    case "QuotesGuidance.aspx":
                //        {
                //            searchbox.Visible = false;
                //            lblAccountName.Visible = false;
                //            break;
                //        }
                //    default:
                //        {
                //            searchbox.Visible = true;
                //            lblAccountName.Visible = true;
                //            break;
                //        }
                //}

                //To hide text search in account number.
                //if (Request.Cookies["CSS"] != null)
                //{
                //    if (Request.Cookies["CSS"].Value == "Construction")
                //    {
                //        varConst = "Construction";
                //        varHideAccountSearch = "true";
                //    }
                //    else if (Request.Cookies["CSS"].Value == "Mining")
                //    {
                //        varMining = "Mining";
                //        lblAccountName.Text = string.Empty;
                //        varHideAccountSearch = "true";
                //    }
                //    else if (Request.Cookies["CSS"].Value == "Quotes1K")
                //    {
                //        varHideAccountSearch = "true";
                //    }
                //    else if (Request.Cookies["CSS"].Value == "QuoteGuidance")
                //    {
                //        varHideAccountSearch = "true";
                //    }
                //}

                //Hide Construction
                switch (SessionFacade.UserRole.Trim())
                {
                    case "ADMIN":
                    case "CA(CONS)":
                    case "US(CONS)":
                        {
                            LiConstruction.Visible = true;
                            switch (ddlCampaign.SelectedValue)
                            {
                                case "CA":
                                case "US":
                                    {
                                        CA = "true";
                                        break;
                                    }
                                default:
                                    {
                                        CA = "false";
                                        break;
                                    }
                            }
                            break;
                        }
                    default:
                        LiConstruction.Visible = false;
                        break;
                }

                EMED = CheckforMining() ?
                "true" : "false";
            }
            else
            {
                cUser objGetCusPage = new cUser();
                objGetCusPage.createdby = SessionFacade.LoggedInUserName;
                DataSet dsCusPage = objGetCusPage.SelectCutomizedScreen();
                if (dsCusPage.CheckDataRecords())
                {
                    DataRow row = dsCusPage.Tables[0].Rows[0];

                    string Page = row["PageName"].ToString();
                    string[] PageList = Page.ToString().ToUpper().Split(',');

                    InvisibleAllPage();

                    foreach (string value in PageList)
                    {
                        switch (value)
                        {
                            case "ORDER HISTORY":
                                LiOrderHistory.Visible = true;
                                break;
                            case "PRODUCT SUMMARY":
                                LiProductSummar.Visible = true;
                                break;
                            case "QUOTES":
                                {
                                    if (ddlCampaign.SelectedValue != "CL")
                                        LiQuotes.Visible = true;
                                    break;
                                }
                            case "CUSTOMER INFO":
                                LiSiteContactInfo.Visible = true;
                                break;
                            case "CUSTOMER SEARCH":
                                LiCustomerLookUp.Visible = true;
                                break;
                            case "NOTES":
                                LiNotesComHist.Visible = true;
                                break;
                            case "CONSTRUCTION":
                                CA = "true";
                                break;
                            case "MINING":
                                EMED = "true";
                                break;
                            case "USER":
                                {
                                    LiManageUsers.Visible = true;
                                    admindivider.Visible = true;
                                    break;
                                }
                            case "MESSAGE":
                                {
                                    LiMessage.Visible = true;
                                    messagedivider.Visible = true;
                                    break;
                                }
                            case "QUOTE PIPELINE":
                                LiQuotesOver1K.Visible = true;
                                break;
                            case "QUOTE DG":
                                LiQuoteGuidance.Visible = true;
                                break;
                        }
                    }

                }

                //if (Request.Cookies["CSS"] != null &&
                //    Request.Cookies["CSS"].Value == "Construction")
                //{
                //    varConst = "Construction";
                //}
                //else if (Request.Cookies["CSS"] != null &&
                //    Request.Cookies["CSS"].Value == "Mining")
                //{
                //    varMining = "Mining";
                //    lblAccountName.Text = string.Empty;
                //}

                //if (Request.Cookies["CSS"] != null)
                //{
                //    varConst = (Request.Cookies["CSS"].Value == "Construction") ?
                //        "Construction" : string.Empty;
                //    varMining = (Request.Cookies["CSS"].Value == "Mining") ?
                //        "Mining" : string.Empty;
                //    lblAccountName.Text = (Request.Cookies["CSS"].Value == "Mining") ?
                //        string.Empty : lblAccountName.Text;
                //}

            }

            //To hide text search in account number.
            if (Request.Cookies["CSS"] != null)
            {
                varConst = (Request.Cookies["CSS"].Value == "Construction") ?
                    "Construction" : string.Empty;
                varMining = (Request.Cookies["CSS"].Value == "Mining") ?
                    "Mining" : string.Empty;
                lblAccountName.Text = (Request.Cookies["CSS"].Value == "Mining") ?
                    string.Empty : lblAccountName.Text;
            }
        }

        private bool CheckAccountSearch()
        {
            string Page = Path.GetFileName(Request.PhysicalPath);
            return Page.In("CustomerLookUp.aspx", "ManageUsers.aspx", "ManageMessage.aspx",
                                "QuotesOver1K.aspx", "QuotesGuidance.aspx");
        }

        private bool CheckForQuotesPipelineGuidedance()
        {
            return ddlCampaign.SelectedValue.In("PC", "CL", "BE", "FR", "IT", "NL");
        }

        private static bool CheckManageMessage()
        {
            return SessionFacade.UserRole.Trim() == "ADMIN" || SessionFacade.UserRole.Trim() == "PC(ADMIN)";
        }

        private bool CheckForCustomerLookup()
        {
            return ((ddlCampaign.Text == "PC") && SessionFacade.UserRole.Trim() == "PC-MAN");
        }

        private bool CheckForDividerKAM()
        {
            return (ddlCampaign.SelectedValue == "PC") ||
                                string.IsNullOrEmpty(SessionFacade.KamId);
        }

        private bool CheckForKAM()
        {
            return (ddlCampaign.SelectedValue.In("PC", "CL"))
                                && string.IsNullOrEmpty(SessionFacade.KamId);
        }

        private bool CheckforMining()
        {
            return (ddlCampaign.Text == "EMED" || SessionFacade.UserRole.Trim() == "PC-MC");
        }
        #endregion

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

        protected void Page_Init(object sender, EventArgs e)
        {
            MasterPageFunc URAd = new UserRoleADMIN();
            MasterPageFunc LCNAd = new LoggedCampaignNameADMIN();
            MasterPageFunc URC = new UserRoleCus();
            MasterPageFunc URD = new UserRoleDefault();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
            "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageMessage" + ".xml";

            if (string.IsNullOrEmpty(SessionFacade.LoggedInUserName))
            {
                Response.Redirect(FormsAuthenticationHelper.LoginURL, true);
            }

            URAd.SetSuccessor(LCNAd);
            LCNAd.SetSuccessor(URC);
            URC.SetSuccessor(URD);

            URAd.LoadCampaign(this);

            //LoadCampaign();

            if (CheckToLoadMessage(Pathname))
                LoadMessage();

            if (Request.Cookies["CProjID"] != null)
            {
                SessionFacade.PROJECTID = Request.Cookies["CProjID"].Value;//txtProjectID.Text.Trim();

                if (Request.Cookies["CProjID"].Value == "")
                {
                    SetProjectID();
                }
            }


            SetAccountInformation();
        }

        private static bool CheckToLoadMessage(string Pathname)
        {
            return (File.Exists(Pathname) == false) ||
                            (File.GetCreationTime(Pathname).ToShortDateString() !=
                            DateTime.Now.ToShortDateString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //For getting the value and use in javascript code.
                txtAccountNumber.Attributes.Add("onfocus()", "FocusValueAccount(this);");
                txtProjectID.Attributes.Add("onfocus()", "FocusValueAccount(this);");

                //Assign link
                order_history.HRef = "../OrderHistory/OrderHistory.aspx";
                product.HRef = "../ProductSummary/ProductSummary.aspx";
                Customer.HRef = "../SiteAndContactInfo/SiteAndContactInfo.aspx";
                Notes.HRef = "../NotesCommHistory/NotesCommHistory.aspx";
                Quotes.HRef = "../Quotes/Quotes.aspx";
                CustomerMan.HRef = "../SiteAndContactInfo/SiteAndContactInfo.aspx";
                OnHoldOrder.HRef = "../OnHoldOrder/OnHoldOrder.aspx";
                btnHome.PostBackUrl = "../Home/Main.aspx";
                QuoteGuidance.HRef = "../QuotesGuidance/QuotesGuidance.aspx";
                QuotesPipeline.HRef = "../QuotesOver1K/QuotesOver1K.aspx";
                CustomerSearch.HRef = "../CustomerLookUp/CustomerLookUp.aspx";

                if (string.IsNullOrEmpty(SessionFacade.CampaignName))
                {
                    SetCampaignInSession();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script language=\"javascript\">");
                sb.Append("if (window.location.href.indexOf('OrderHistory.aspx') > -1)" +
                " document.getElementById('nav').className = 'order_history'; " +
                " else if(window.location.href.indexOf('Quotes.aspx') > -1) document.getElementById('nav').className = 'Quotes'; " +
                " else if(window.location.href.indexOf('ProductSummary.aspx') > -1) document.getElementById('nav').className = 'product';" +
                " else if(window.location.href.indexOf('ProductSummary.aspx') > -1) document.getElementById('nav').className = 'product';" +
                " else if(window.location.href.indexOf('NotesCommHistory.aspx') > -1) document.getElementById('nav').className = 'Notes'; " +
                " else if(window.location.href.indexOf('NotesCommHistory.aspx') > -1) document.getElementById('nav').className = 'Notes'; " +
                " else if(window.location.href.indexOf('SiteAndContactInfo.aspx') > -1) document.getElementById('nav').className = 'Customer'; " +
                " else if(window.location.href.indexOf('SiteAndContactInfo.aspx') > -1) document.getElementById('nav').className = 'CustomerMan'; " +
                " else if(window.location.href.indexOf('QuotesOver1K.aspx') > -1) document.getElementById('nav').className = 'QuotesPipeline'; " +
                " else if(window.location.href.indexOf('QuotesGuidance.aspx') > -1) document.getElementById('nav').className = 'QuoteGuidance'; " +
                " else if(window.location.href.indexOf('Construction.aspx') > -1) document.getElementById('nav').className = 'Construction'; " +
                " else if(window.location.href.indexOf('Mining.aspx') > -1) document.getElementById('nav').className = 'Mining'; " +
                " else if(window.location.href.indexOf('OnHoldOrder.aspx') > -1) document.getElementById('nav').className = 'OnHoldOrder'; " +
                " else document.getElementById('nav').className = '' ;");
                sb.Append("</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", sb.ToString());

                if (SessionFacade.UserRole.ToString().ToUpper() != "CUS")
                {
                    DisplayIconsAsPerRole(SessionFacade.UserRole, true);
                    SetButtonVisible();
                }
                else
                    DisplayIconsAsPerRole(SessionFacade.UserRole, true, true);

                //Visibility of Show Today Note Box
                if (Path.GetFileName(Request.PhysicalPath) == "NotesCommHistory.aspx")
                {
                    Page.ClientScript.RegisterStartupScript(typeof(ScriptManager), "CallShowDialogNote",
                        "onSuccessGetNote()", true);
                }

                //Visibility of On Hold Order Page
                ViewOnHoldOrder = ((ddlCampaign.SelectedValue == "DE" || ddlCampaign.SelectedValue == "FR"
                    || ddlCampaign.SelectedValue == "UK") && !string.IsNullOrEmpty(SessionFacade.KamId.ToString()))
                    ? "true" : "false";

                //Visibility of QuotePipeline Page
                ViewQuotePipelineDG = (ddlCampaign.SelectedValue == "IT" || ddlCampaign.SelectedValue == "PC" ||
                    ddlCampaign.SelectedValue == "CL") ? "false" : "true";

                //To hide text search in account number.
                StatusTextSearch();

                //This code used in Linkbuton in Quote Pipeline Page
                if (Request.Cookies["A1No"] != null)
                {
                    txtAccountNumber.Text = Request.Cookies["A1No"].Value;

                    HttpCookie aCookie = new HttpCookie("CName");
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);

                    HttpCookie myCookie = new HttpCookie("CNo");
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookie);

                    SessionFacade.BuyerCt = string.Empty;
                    SessionFacade.ContactName = string.Empty;

                    SessionFacade.AccountNo = txtAccountNumber.Text;

                    SetAccountInformation();

                    HttpCookie myCookies = new HttpCookie("A1No");
                    myCookies.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookies);

                    RefeshPage();

                    myCookies = new HttpCookie("QNoTemp");
                    myCookies.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookies);

                }

                //This code used in Linkbuton in Schedule Note Window in Notes Page
                if (Request.Cookies["ANoNote"] != null)
                {
                    txtAccountNumber.Text = Request.Cookies["ANoNote"].Value;

                    HttpCookie aCookie = new HttpCookie("CName");
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);

                    HttpCookie myCookie = new HttpCookie("CNo");
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookie);

                    SessionFacade.BuyerCt = string.Empty;
                    SessionFacade.ContactName = string.Empty;

                    SessionFacade.AccountNo = txtAccountNumber.Text;

                    SetAccountInformation();

                    HttpCookie myCookies = new HttpCookie("ANoNote");
                    myCookies.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookies);

                    RefeshPage();

                }

                //This code used in Construction Page
                try
                {
                    if (Request.Cookies["CProjID"] != null &&
                        Request.Cookies["CProjID"].Value != "")
                    {
                        txtProjectID.Text =
                             (Request.Cookies["CProjID"].Value != ProjectID.Value) ?
                             Request.Cookies["CProjID"].Value : txtProjectID.Text;

                        ProjectID.Value = txtProjectID.Text;

                        HttpCookie projectID2 = new HttpCookie("CProjID");
                        Response.Cookies["CProjID"].Value = ProjectID.Value;
                        Response.Cookies["CProjID"].Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(projectID2);

                        SessionFacade.PROJECTID = ProjectID.Value;
                    }
                    else
                        txtProjectID.Text = SessionFacade.PROJECTID;
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../login/login.aspx");
            }
        }

        private void StatusTextSearch()
        {
            if (Path.GetFileName(Request.PhysicalPath).IndexOf("Construction.aspx") > -1)
            {
                varConst = "Construction";
                varHideAccountSearch = "true";
            }
            else if (Path.GetFileName(Request.PhysicalPath).IndexOf("Mining.aspx") > -1)
            {
                varMining = "Mining";
                lblAccountName.Text = string.Empty;
                varHideAccountSearch = "true";
            }
            else if (Path.GetFileName(Request.PhysicalPath).IndexOf("QuotesOver1K.aspx") > -1 ||
                Path.GetFileName(Request.PhysicalPath).IndexOf("QuotesGuidance.aspx") > -1 ||
                Path.GetFileName(Request.PhysicalPath).IndexOf("CustomerLookUp.aspx") > -1)
            {
                varHideAccountSearch = "true";
            }
            else
            {
                varHideAccountSearch = "false";
            }
        }

        #region DisplayIconsAsPerRole
        private void DisplayIconsAsPerRole(string Role, bool visibility, bool Customized = false)
        {
            if (Customized == false)
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
                    admindivider.Visible = false;
                }
                if (Role == "PC-MC")
                {
                    LiMining.Visible = true;

                }
                else
                {
                    LiMining.Visible = false;
                }

                if (Role == "FR" || Role == "BE" ||
                    Role == "NL" || Role == "PC-ONT" ||
                    Role == "PC-MAN" || Role == "CL" ||
                    Role == "IT")
                {
                    LiQuotesOver1K.Visible = false;
                    LiQuoteGuidance.Visible = false;
                }
                else
                {
                    LiQuotesOver1K.Visible = true;
                    LiQuoteGuidance.Visible = true;
                }
            }
            else
            {
                cUser objGetCusPage = new cUser();
                objGetCusPage.createdby = SessionFacade.LoggedInUserName;
                DataSet dsCusPage = objGetCusPage.SelectCutomizedScreen();
                if (dsCusPage != null)
                {
                    if (dsCusPage.Tables.Count > 0)
                    {
                        DataRow row = dsCusPage.Tables[0].Rows[0];

                        string Page = row["PageName"].ToString();
                        string[] PageList = Page.ToString().ToUpper().Split(',');

                        InvisibleAllPage();

                        for (int i = 0; i < PageList.Length; i++)
                        {
                            switch (PageList[i])
                            {
                                case "ORDER HISTORY":
                                    LiOrderHistory.Visible = true;
                                    break;
                                case "PRODUCT SUMMARY":
                                    LiProductSummar.Visible = true;
                                    break;
                                case "QUOTES":
                                    if (ddlCampaign.SelectedValue != "CL")
                                        LiQuotes.Visible = true;
                                    break;
                                case "CUSTOMER INFO":
                                    LiSiteContactInfo.Visible = true;
                                    break;
                                case "CUSTOMER SEARCH":
                                    LiCustomerLookUp.Visible = true;
                                    break;
                                case "NOTES":
                                    LiNotesComHist.Visible = true;
                                    break;
                                case "CONSTRUCTION":
                                    CA = "true";
                                    break;
                                case "MINING":
                                    EMED = "true";
                                    break;
                                case "USER":
                                    LiManageUsers.Visible = true;
                                    admindivider.Visible = true;
                                    break;
                                case "MESSAGE":
                                    LiMessage.Visible = true;
                                    messagedivider.Visible = true;
                                    break;
                                case "QUOTE PIPELINE":
                                    LiQuotesOver1K.Visible = true;
                                    break;
                                case "QUOTE DG":
                                    LiQuoteGuidance.Visible = true;
                                    break;
                            }
                            //if (PageList[i] == "ORDER HISTORY")
                            //    LiOrderHistory.Visible = true;
                            //else if (PageList[i] == "PRODUCT SUMMARY")
                            //    LiProductSummar.Visible = true;
                            //else if (PageList[i] == "QUOTES")
                            //{
                            //    if(ddlCampaign.SelectedValue != "CL")
                            //        LiQuotes.Visible = true;
                            //}
                            //else if (PageList[i] == "CUSTOMER INFO")
                            //    LiSiteContactInfo.Visible = true;
                            //else if (PageList[i] == "CUSTOMER SEARCH")
                            //    LiCustomerLookUp.Visible = true;
                            //else if (PageList[i] == "NOTES")
                            //    LiNotesComHist.Visible = true;
                            //else if (PageList[i] == "CONSTRUCTION")
                            //{
                            //    CA = "true";
                            //}
                            //else if (PageList[i] == "MINING")
                            //{
                            //    EMED = "true";
                            //}
                            //else if (PageList[i] == "USER")
                            //{
                            //    LiManageUsers.Visible = true;
                            //    admindivider.Visible = true;
                            //}
                            //else if (PageList[i] == "MESSAGE")
                            //{
                            //    LiMessage.Visible = true;
                            //    messagedivider.Visible = true;
                            //}
                            //else if (PageList[i] == "QUOTE PIPELINE")
                            //    LiQuotesOver1K.Visible = true;
                            //else if (PageList[i] == "QUOTE DG")
                            //    LiQuoteGuidance.Visible = true;
                        }

                        if (Request.Cookies["CSS"] != null)
                        {
                            if (Request.Cookies["CSS"].Value == "Construction")
                            {
                                varConst = "Construction";
                            }
                        }

                        if (Request.Cookies["CSS"] != null)
                        {
                            if (Request.Cookies["CSS"].Value == "Mining")
                            {
                                varMining = "Mining";
                                lblAccountName.Text = string.Empty;
                            }
                        }
                    }
                }
            }
        }

        private void InvisibleAllPage()
        {
            LiOrderHistory.Visible = false;
            LiProductSummar.Visible = false;
            LiQuotes.Visible = false;
            LiSiteContactInfo.Visible = false;
            LiCustomerLookUp.Visible = false;
            LiNotesComHist.Visible = false;
            EMED = "";
            CA = "";
            LiManageUsers.Visible = false;
            admindivider.Visible = false;
            LiMessage.Visible = false;
            messagedivider.Visible = false;
            LiQuotesOver1K.Visible = false;
            LiQuoteGuidance.Visible = false;
        }
        #endregion

        #region Account Information

        //public bool ColumnExists(SqlDataReader reader, string columnName)
        //{
        //    for (int i = 0; i < reader.FieldCount; i++)
        //    {
        //        if (reader.GetName(i).ToUpper().Trim() == columnName)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        private void SetAccountName()
        {
            cAccount objAccountName = new cAccount();
            try
            {
                objAccountName.CampaignName = ddlCampaign.SelectedValue.ToString().Trim();

                DataSet drCampaignValue = new DataSet();
                lblAccountName.Text = "";

                objAccountName.AccountName = SessionFacade.AccountNo.FormatAccountNumber();

                using (drCampaignValue = objAccountName.GetAccountNameOnly())
                {


                    try
                    {

                        if (drCampaignValue.CheckDataRecords())
                        {
                            try
                            {
                                lblAccountName.Text = drCampaignValue.Tables[0].Rows[0]["NAME"].ToString();
                                AccountName.Value = drCampaignValue.Tables[0].Rows[0]["NAME"].ToString();
                                Region.Value = drCampaignValue.Tables[0].Rows[0]["REGION"].ToString();
                            }
                            catch (Exception ex)
                            {
                                BradyCorp.Log.LoggerHelper.LogException(ex, "Error During binding drCampaignValue NAME & REGION", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                            }


                            try
                            {
                                //Check State
                                if (Request.Cookies["CSS"] != null)
                                {
                                    if (Path.GetFileName(Request.PhysicalPath) == "OrderHistory.aspx")
                                    {
                                        GetMessageVoid(Region.Value);
                                    }
                                    else if (Request.Cookies["CSS"].Value != "Customer" ||
                                        Request.Cookies["CSS"].Value != "CustomerMan"
                                         )
                                    {
                                        if (GetMessage(Region.Value))
                                        {
                                            ScriptManager.RegisterStartupScript(this, typeof(ScriptManager), "CallShowDialog", "onSuccessGet()", true);
                                            //Page.ClientScript.RegisterStartupScript(typeof(ScriptManager), "CallShowDialog", "onSuccessGet()", true);
                                        }
                                    }
                                }
                                else if (Path.GetFileName(Request.PhysicalPath) == "Main.aspx")
                                {
                                    if (GetMessage(Region.Value))
                                    {
                                        ScriptManager.RegisterStartupScript(this, typeof(ScriptManager), "CallShowDialog", "onSuccessGet()", true);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                BradyCorp.Log.LoggerHelper.LogException(ex, "Error During Check State", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                            }

                        }
                    }
                    catch (Exception err)
                    {
                        BradyCorp.Log.LoggerHelper.LogException(err, "Error During binding drCampaignValue dataset", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                    }
                }
                //}
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Displaying Site Info", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private void SetAccountInformation()
        {
            if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
            {
                txtAccountNumber.Text = "";
                if (SessionFacade.AccountNo != "0000000000")
                    txtAccountNumber.Text = SessionFacade.AccountNo;
            }
            SetAccountName();
        }

        private void SetProjectID()
        {
            if (string.IsNullOrEmpty(SessionFacade.PROJECTID))
            {
                //txtProjectID.Text = "";
                //if (SessionFacade.PROJECTID != "0000000000")
                // txtAccountNumber.Text = SessionFacade.AccountNo;
                SessionFacade.PROJECTID = txtProjectID.Text.Trim();
            }
        }

        protected void txtAccountNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                string[] separateURL = url.Split('?');
                NameValueCollection queryString = new NameValueCollection();
                string regexString = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexString);

                if (txtAccountNumber.Text != "" && txtAccountNumber.Text.Length > 0)
                {

                    if (regex.IsMatch(txtAccountNumber.Text.Trim()))
                    {
                        HttpCookie aCookie = new HttpCookie("CName");
                        aCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(aCookie);

                        HttpCookie myCookie = new HttpCookie("CNo");
                        myCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(myCookie);

                        SessionFacade.BuyerCt = string.Empty;
                        SessionFacade.ContactName = string.Empty;

                        SessionFacade.AccountNo = txtAccountNumber.Text;

                        SetAccountInformation();
                    }
                }
            }
            catch (Exception err)
            {
                SessionFacade.AccountNo = "123123";
                BradyCorp.Log.LoggerHelper.LogException(err, "Main Page - Error in Account No change ", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void txtMiningID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string regexString = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexString);

                if (txtMiningID.Text != "" && txtMiningID.Text.Length > 0)
                {

                    if (regex.IsMatch(txtMiningID.Text.Trim()))
                    {

                        SessionFacade.MiningID = txtMiningID.Text;

                        if (rdoSiteNumber.Checked == true)
                        {
                            SessionFacade.SelectedOption = "SiteNum";
                        }
                        if (rdoBestPhone.Checked == true)
                        {
                            SessionFacade.SelectedOption = "BestPhone";
                        }
                        // SetAccountInformation();
                    }

                    //RefeshPage();

                }


            }
            catch (Exception err)
            {
                SessionFacade.AccountNo = "123123";
                BradyCorp.Log.LoggerHelper.LogException(err, "Main Page - Error in Account No change ", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void RefeshPage()
        {
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

        protected void RefeshPageAccount()
        {
            if (Path.GetFileName(Request.PhysicalPath).IndexOf("OrderHistory.aspx") > -1)
                Response.Redirect("../OrderHistory/OrderHistory.aspx?AccountNumber=" + txtAccountNumber.Text, false);
            else if (Path.GetFileName(Request.PhysicalPath).IndexOf("ProductSummary.aspx") > -1)
                Response.Redirect("../ProductSummary/ProductSummary.aspx?AccountNumber=" + txtAccountNumber.Text, false);
            else if (Path.GetFileName(Request.PhysicalPath).IndexOf("NotesCommHistory.aspx") > -1)
                Response.Redirect("../NotesCommHistory/NotesCommHistory.aspx?AccountNumber=" + txtAccountNumber.Text, false);
            else if (Path.GetFileName(Request.PhysicalPath).IndexOf("Quotes.aspx") > -1)
                Response.Redirect("../Quotes/Quotes.aspx?AccountNumber=" + txtAccountNumber.Text, false);
            else if (Path.GetFileName(Request.PhysicalPath).IndexOf("Main.aspx") > -1)
                Response.Redirect("../Home/Main.aspx?AccountNumber=" + txtAccountNumber.Text, false);
            else if (Path.GetFileName(Request.PhysicalPath).IndexOf("SiteAndContactInfo.aspx") > -1)
                Response.Redirect("../SiteAndContactInfo/SiteAndContactInfo.aspx?AccountNumber=" + txtAccountNumber.Text, false);
        }

        protected void imbSearchAcntNumber_Click1(object sender, EventArgs e)
        {
            try
            {
                string regexString = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexString);

                if (txtAccountNumber.Text != "" && txtAccountNumber.Text.Length > 0)
                {


                    if (regex.IsMatch(txtAccountNumber.Text.Trim()))
                    {
                        SessionFacade.BuyerCt = string.Empty;
                        SessionFacade.ContactName = string.Empty;
                        HttpCookie aCookie = new HttpCookie("CName");
                        aCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(aCookie);

                        HttpCookie myCookie = new HttpCookie("CNo");
                        myCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(myCookie);
                        SessionFacade.AccountNo = txtAccountNumber.Text;

                        SetAccountInformation();
                    }

                    //RefeshPageAccount();

                }

            }
            catch (Exception err)
            {
                SessionFacade.AccountNo = "123123";
                BradyCorp.Log.LoggerHelper.LogException(err, "Account no - Search Button Click", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

            //RefeshPage();
        }
        #endregion

        #region Campaign Information
        protected void ddlCampaign_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                SetCampaignInSession();
                SetAccountInformation();
                //ScriptManager.RegisterStartupScript(this,GetType(),"call me", "closeChildWindows();",true);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "RefreshWinodw()", true);
                //uppanelMain.Update();
                if (SessionFacade.UserRole.ToString().ToUpper() != "CUS")
                    SetButtonVisible();
                else
                    DisplayIconsAsPerRole(SessionFacade.UserRole, true, true);
                //CloseMyWindow();
                //RefeshPageAccount();
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaing Change", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        public void SetCampaignInSession()
        {
            try
            {
                if (ddlCampaign.Items.Count > 0)
                {
                    SessionFacade.CampaignName = ddlCampaign.SelectedItem.Value;
                    SessionFacade.CampaignValue = ddlCampaign.SelectedItem.Value;
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "SetCampaignInSession-New Master Page", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        public void SetCampaignFromSession()
        {
            if (!string.IsNullOrEmpty(SessionFacade.CampaignName))
            {
                ddlCampaign.ClearSelection();

                if (ddlCampaign.Items.FindByValue(SessionFacade.CampaignValue) != null)
                {
                    ddlCampaign.Items.FindByValue(SessionFacade.CampaignValue).Selected = true;
                }

            }
        }

        #endregion

        #region State Message
        public bool GetMessage(string Region)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
          "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageMessage" + ".xml";
            try
            {
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query = string.Empty;
                DataSet dsMessage = new DataSet();

                if (File.Exists(Pathname) == false)
                {
                    LoadMessage();
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    dsMessage.ReadXml(fsReadXml);
                    fsReadXml.Close();
                }
                else
                {
                    //Reading XML
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    dsMessage.ReadXml(fsReadXml);
                    fsReadXml.Close();
                }

                if (dsMessage.Tables.Count > 0)
                {
                    if (dsMessage.Tables[0].Columns.Contains("State") == true)
                    {
                        dtTemp = dsMessage.Tables[0].Clone();

                        Query += "State='" + Region + "' and Campaign='" +
                        ddlCampaign.SelectedValue + "'";

                        results = dsMessage.Tables[0].Select(Query);

                        foreach (DataRow dr in results)
                            dtTemp.ImportRow(dr);

                        if (dtTemp.Rows.Count != 0)
                        {
                            SessionFacade.PopupMessage = dtTemp.Rows[0]["Message"].ToString();
                            return true;
                        }
                    }
                    else
                        return false;

                }

                return false;
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Get State Popup Message", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;
            }
        }

        public void GetMessageVoid(string Region)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
          "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageMessage" + ".xml";
            try
            {
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query = string.Empty;
                DataSet dsMessage = new DataSet();

                if (File.Exists(Pathname) == false)
                {
                    LoadMessage();
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    dsMessage.ReadXml(fsReadXml);
                    fsReadXml.Close();
                }
                else
                {
                    //Reading XML
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    dsMessage.ReadXml(fsReadXml);
                    fsReadXml.Close();
                }

                if (dsMessage.Tables.Count > 0)
                {
                    if (dsMessage.Tables[0].Columns.Contains("State") == true)
                    {
                        dtTemp = dsMessage.Tables[0].Clone();

                        Query += "State='" + Region + "' and Campaign='" +
                        ddlCampaign.SelectedValue + "'";

                        results = dsMessage.Tables[0].Select(Query);

                        foreach (DataRow dr in results)
                            dtTemp.ImportRow(dr);

                        if (dtTemp.Rows.Count != 0)
                        {
                            SessionFacade.PopupMessage = dtTemp.Rows[0]["Message"].ToString();
                        }
                    }

                }

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Get State Popup Message", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        public void LoadMessage()
        {
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
               "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageMessage" + ".xml";

                DataSet drMessage = new DataSet();
                cManageMessage objManageMessage = new cManageMessage();

                drMessage = objManageMessage.GetMessage();

                if (drMessage != null)
                {
                    if (drMessage.Tables.Count > 0)
                    {
                        //Writing XML
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        drMessage.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();
                    }
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaing Change", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
        #endregion

        public void CloseMyWindow()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "callme", "closeChildWindows();", true);
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "callme", "javascript:closeChildWindows();", true);
            // ScriptManager.RegisterStartupScript(this.Page, typeof(string), "callme", "javascript:closeChildWindows();", true);

            // ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "QuoteClick();window.opener.location='../Quotes/Quotes.aspx';", true);
        }

        protected void Hypelogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            string nextpage = "login.aspx";

            FormsAuthenticationHelper.SignOut();

            NewMasterPage MyMasterPage = (NewMasterPage)Page.Master;

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
            loginCookie.Values.Add("KamID", "");
            DateTime dtExpiry = DateTime.Now.AddDays(-1); //you can add years and months too here
            Response.Cookies["loginCookie"].Expires = dtExpiry;
            Response.Redirect("../Login/login.aspx");
        }

        protected void txtProjectID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string regexString = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexString);

                if (txtProjectID.Text != "" && txtProjectID.Text.Length > 0)
                {

                    if (regex.IsMatch(txtProjectID.Text.Trim()))
                    {
                        //SessionFacade.PROJECTID = Request.Cookies["CProjID"].Value;

                        //HttpCookie aCookie = new HttpCookie("CName");
                        //aCookie.Expires = DateTime.Now.AddDays(-1);
                        //Response.Cookies.Add(aCookie);

                        //HttpCookie myCookie = new HttpCookie("CNo");
                        //myCookie.Expires = DateTime.Now.AddDays(-1);
                        //Response.Cookies.Add(myCookie);

                        //SessionFacade.BuyerCt = string.Empty;
                        //SessionFacade.ContactName = string.Empty;

                        //SessionFacade.AccountNo = txtAccountNumber.Text;

                        //SetAccountInformation();
                    }

                    //RefeshPage();

                }


            }
            catch (Exception err)
            {
                SessionFacade.AccountNo = "123123";
                BradyCorp.Log.LoggerHelper.LogException(err, "Main Page - Error in Account No change ", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void imbSearchProjID_Click(object sender, EventArgs e)
        {
            try
            {
                string regexString = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexString);

                if (txtProjectID.Text != "" && txtProjectID.Text.Length > 0)
                {


                    if (regex.IsMatch(txtProjectID.Text.Trim()))
                    {
                        //SessionFacade.BuyerCt = string.Empty;
                        //SessionFacade.ContactName = string.Empty;
                        //HttpCookie aCookie = new HttpCookie("CName");
                        //aCookie.Expires = DateTime.Now.AddDays(-1);
                        //Response.Cookies.Add(aCookie);

                        //HttpCookie myCookie = new HttpCookie("CNo");
                        //myCookie.Expires = DateTime.Now.AddDays(-1);
                        //Response.Cookies.Add(myCookie);
                        //SessionFacade.AccountNo = txtAccountNumber.Text;

                        //SetAccountInformation();

                        //HttpCookie loginCookie = new HttpCookie("CProjID");
                        //loginCookie.Values.Add("CProjID", txtProjectID.Text.Trim());


                        //Response.Cookies["CProjID"].Value = txtProjectID.Text.Trim();
                        //Response.Cookies["CProjID"].Expires = DateTime.Now.AddDays(1);

                        // Response.Cookies.Add(loginCookie);
                    }

                }

            }
            catch (Exception err)
            {
                //SessionFacade.AccountNo = "123123";
                BradyCorp.Log.LoggerHelper.LogException(err, "Account no - Search Button Click", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }


            //RefeshPage();
        }

        protected void imbSearchMining_Click(object sender, EventArgs e)
        {
            try
            {
                string regexString = @"\d+";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexString);

                if (txtMiningID.Text != "" && txtMiningID.Text.Length > 0)
                {


                    if (regex.IsMatch(txtMiningID.Text.Trim()))
                    {

                        //Response.Cookies["CMiningID"].Value = txtMiningID.Text.Trim();
                        //Response.Cookies["CMiningID"].Expires = DateTime.Now.AddDays(1);

                        // Response.Cookies.Add(loginCookie);
                    }

                }

            }
            catch (Exception err)
            {
                //SessionFacade.AccountNo = "123123";
                BradyCorp.Log.LoggerHelper.LogException(err, "Account no - Search Button Click", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void btnHome_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Home/Main.aspx");
        }

    }

    #region LoadCampaign
    public abstract class MasterPageFunc
    {
        protected MasterPageFunc UserRole;

        public void SetSuccessor(MasterPageFunc MasterPage)
        {
            this.UserRole = MasterPage;
        }

        public abstract void LoadCampaign(NewMasterPage MasterPage);
    }

    public class UserRoleADMIN : MasterPageFunc
    {
        public override void LoadCampaign(NewMasterPage MasterPage)
        {
            try
            {
                if (SessionFacade.UserRole == "ADMIN")
                {
                    string PathnameCampaign = WebHelper.GetApplicationPath() + "App_Data" +
                        Path.DirectorySeparatorChar +
                    "XMLFiles\\" + SessionFacade.LoggedInUserName + "-Campaign" + ".xml";

                    cCampaign objCampaign = new cCampaign();
                    DataSet drCampaign = objCampaign.GetCampaignList();

                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameCampaign);
                    drCampaign.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    if (drCampaign.CheckDataRecords())
                    {
                        LoadDropDownList(MasterPage.CampaignCurrencyMaster, "Currency",
                            "campaignValue", drCampaign);

                        LoadDropDownList(MasterPage.CampaignMaster, "campaignName", "campaignValue",
                            drCampaign);

                        MasterPage.SetCampaignFromSession();
                    }
                }
                else
                    UserRole.LoadCampaign(MasterPage);
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaign LoadCampaign Admin",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private void LoadDropDownList(DropDownList ddl, string TextField, string ValueField, DataSet ds)
        {
            try
            {
                ddl.DataSource = ds;
                ddl.DataTextField = TextField;
                ddl.DataValueField = ValueField;
                ddl.DataBind();
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaign LoadDropDownList Admin",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
    }

    public class LoggedCampaignNameADMIN : MasterPageFunc
    {
        public override void LoadCampaign(NewMasterPage MasterPage)
        {
            try
            {
                if (SessionFacade.LoggedCampaignName.ToUpper() == "ADMIN"
                    && !string.IsNullOrEmpty(SessionFacade.KamId))
                {
                    //if role is not there and kam id is there and campagin is there
                    if (SessionFacade.KamId.Contains("EMED"))
                    {
                        ListItem L1;

                        L1 = new ListItem("EMED ", "EMED");
                        MasterPage.CampaignMaster.Items.Insert(0, L1);
                    }
                    else if (SessionFacade.KamId.Contains("SETCN"))
                    {
                        ListItem L1;

                        L1 = new ListItem("CA ", "CA");
                        MasterPage.CampaignMaster.Items.Insert(0, L1);
                        SessionFacade.CampaignName = "CA";
                        SessionFacade.CampaignValue = "CA";
                    }
                    else
                    {
                        ListItem L1;

                        L1 = new ListItem("EMED ", "EMED");
                        MasterPage.CampaignMaster.Items.Insert(0, L1);
                        SessionFacade.CampaignName = "EMED";
                        SessionFacade.CampaignValue = "EMED";
                    }
                    //For Currency
                    ListItem L2;

                    L2 = new ListItem("USD ", "EMED");
                    MasterPage.CampaignCurrencyMaster.Items.Insert(0, L2);
                }
                else
                    UserRole.LoadCampaign(MasterPage);
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaing LoadCampaign LoggedCampaignNameADMIN",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
    }

    public class UserRoleCus : MasterPageFunc
    {
        public override void LoadCampaign(NewMasterPage MasterPage)
        {
            try
            {
                if (SessionFacade.UserRole.ToString().ToUpper() == "CUS")
                {
                    cUser objGetCusPage = new cUser();
                    objGetCusPage.createdby = SessionFacade.LoggedInUserName;
                    DataSet dsCusPage = objGetCusPage.SelectCutomizedScreen();
                    DataSet dsCode = new DataSet();
                    ListItem L1, L2;

                    if (dsCusPage.CheckDataRecords())
                    {
                        DataRow row = dsCusPage.Tables[0].Rows[0];

                        MasterPage.CampaignValueListPro = (dsCusPage.Tables[0].Columns.Contains("CampaignValue")) ?
                            row["CampaignValue"].ToString() : string.Empty;

                        MasterPage.CampaignNameListPro = (dsCusPage.Tables[0].Columns.Contains("CampaignName")) ?
                            row["CampaignName"].ToString() : string.Empty;

                        string[] ListOfCampaign = MasterPage.CampaignValueListPro.ToString().Split(',');
                        string[] ListOfCampaignName = MasterPage.CampaignNameListPro.ToString().Split(',');

                        for (int i = 0; i < ListOfCampaign.Length; i++)
                        {
                            cUser objGetCurrencyCode = new cUser();
                            L1 = new ListItem(ListOfCampaignName[i].ToString(), ListOfCampaign[i].ToString());
                            MasterPage.CampaignMaster.Items.Insert(i, L1);

                            //Get Currency Code
                            objGetCurrencyCode.campaignUnit = ListOfCampaign[i].ToString();
                            dsCode = objGetCurrencyCode.GetCurrencyCode();

                            if (dsCode != null && dsCode.Tables.Count > 0)
                            {
                                DataRow rowCurrency = dsCode.Tables[0].Rows[0];
                                if (dsCode.Tables[0].Columns.Contains("Currency"))
                                {
                                    L2 = new ListItem(rowCurrency["Currency"].ToString(), ListOfCampaign[i].ToString());
                                    MasterPage.CampaignCurrencyMaster.Items.Insert(i, L2);
                                }
                            }
                        }
                        MasterPage.SetCampaignFromSession();
                    }
                }
                else
                    UserRole.LoadCampaign(MasterPage);
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaing LoadCampaign UserRoleCus",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
    }

    public class UserRoleDefault : MasterPageFunc
    {
        public override void LoadCampaign(NewMasterPage MasterPage)
        {
            try
            {
                // KaM ID IS NULL                
                //SCENIRIO : Userrole is PC-Man,Campaign = PC and kamid is null
                if (SessionFacade.UserRole == "PC-MAN" ||
                    SessionFacade.UserRole == "PC-ONT" ||
                    SessionFacade.UserRole == "PC(ADMIN)" ||
                    SessionFacade.UserRole == "PC-MC")
                {
                    ListItem L1, L2;

                    L1 = new ListItem("Personnel Concepts", "PC");
                    L2 = new ListItem("USD", "PC");
                    MasterPage.CampaignMaster.Items.Insert(0, L1);
                    MasterPage.CampaignCurrencyMaster.Items.Insert(0, L2);
                    SessionFacade.CampaignName = "PC";
                    SessionFacade.CampaignValue = "PC";
                }
                else if (SessionFacade.UserRole == "MANAGERS")
                {
                    ListItem L1;

                    L1 = new ListItem(SessionFacade.LoggedCampaignName, SessionFacade.LoggedCampaignName);
                    MasterPage.CampaignMaster.Items.Insert(0, L1);
                    SessionFacade.CampaignName = SessionFacade.LoggedCampaignName;
                    SessionFacade.CampaignValue = SessionFacade.LoggedCampaignName;
                }
                // KaM ID IS NULL
                //SCENIRIO : Userrole is some other campaiign then PC and kamid is null and Role is null
                else
                {
                    ListItem L1 = new ListItem(), L2 = new ListItem();

                    DataSet dsCode = new DataSet();

                    //Get Currency Code
                    dsCode = GetCampaignWithCurrencyCode(dsCode);

                    if (dsCode.CheckDataRecords())
                    {
                        DataRow rowCurrency = dsCode.Tables[0].Rows[0];
                        if (dsCode.Tables[0].Columns.Contains("Currency"))
                        {
                            L1 = new ListItem(rowCurrency["CampaignName"].ToString(), SessionFacade.UserRole);
                            L2 = new ListItem(rowCurrency["Currency"].ToString(), SessionFacade.UserRole);
                            MasterPage.CampaignCurrencyMaster.Items.Insert(0, L2);
                            MasterPage.CampaignMaster.Items.Insert(0, L1);
                        }
                    }
                    SessionFacade.CampaignName = SessionFacade.UserRole;
                    SessionFacade.CampaignValue = SessionFacade.UserRole;
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaign LoadCampaign UserRoleDefault",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        private static DataSet GetCampaignWithCurrencyCode(DataSet dsCode)
        {
            cUser objGetCurrencyCode = new cUser();
            try
            {
                objGetCurrencyCode.campaignUnit = SessionFacade.UserRole;
                dsCode = objGetCurrencyCode.GetCurrencyCode();
                return dsCode;
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "GetCampaignWithCurrencyCode-New Master Page", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return null;
            }
        }
    }
    #endregion

    #region AccountName

    #endregion

}