using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using AppLogic;
using BradyCorp.Log;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Security;
using System.Xml;

namespace WebSalesMine.WebPages.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected bool loginsucess = false;
        protected string sAMAccountName = string.Empty;
        protected DataTable ldapReturnTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            SetSessions();

            //txtPassword.Attributes.Add("onfocus", "this.type='text';");
            //txtPassword.Attributes.Add("onblur", "this.type='password';");

            txtPassword.Attributes.Add("onfocus", "textboxfunctionPass();");
            txtPassword.Attributes.Add("onblur", "textboxfunctionPass();");
            txtPassword.Attributes.Add("onkeydown", "textboxkeydownPass();");
            txtPassword.Attributes.Add("onkeyup", "textboxkeydownPass();");

            txtUserName.Attributes.Add("onfocus", "textboxfunctionUser();");
            txtUserName.Attributes.Add("onkeyup", "textboxfunctionUser();");
            txtUserName.Attributes.Add("onblur", "textboxfunctionUser();");//onclick
            txtUserName.Attributes.Add("onclick", "textboxfunctionUser();");
            txtUserName.Attributes.Add("onkeydown", "textboxfunctionUser();");
            txtUserName.Attributes.Add("onchange", "textboxfunctionUser();");
            txtUserName.Attributes.Add("autocomplete", "off");
            this.SetFocus(txtUserName);

            if (!IsPostBack)
            {
                // For resetting the login url so that it doesn't have a return value in the URL
                if (Request.QueryString["ReturnURL"] != null)
                {
                    Response.Redirect("~/login/login.aspx", true);
                }

                //if (Request.IsAuthenticated)
                //{
                //Response.Redirect("../Home/Main.aspx");
                if (Request.Cookies["loginCookie"] != null)
                {
                    HttpCookie loginCookie = Request.Cookies["loginCookie"];
                    SessionFacade.CampaignName = loginCookie.Values["CampaignName"].ToString();
                    SessionFacade.CampaignValue = loginCookie.Values["CampaignValue"].ToString();
                    SessionFacade.UserRole = loginCookie.Values["UserRole"].ToString();
                    SessionFacade.LoggedInUserName = loginCookie.Values["LoggedInUserName"].ToString();
                    SessionFacade.KamName = loginCookie.Values["KamName"].ToString();
                    if (loginCookie.Values["KamID"] != null)
                        SessionFacade.KamId = loginCookie.Values["KamID"].ToString();

                    //Clear Cookies
                    HttpCookie aCookie1 = new HttpCookie("ORNo");
                    aCookie1.Value = "";
                    aCookie1.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie1);

                    HttpCookie aCookie = new HttpCookie("PONo");
                    aCookie.Value = "";
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);

                    HttpCookie myCookies = new HttpCookie("QNo");
                    myCookies.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookies);

                    myCookies = new HttpCookie("ANo");
                    myCookies.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookies);

                    HttpCookie CNo = new HttpCookie("QuotePipiline");
                    CNo.Value = "";
                    CNo.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(CNo);


                    //Response.Redirect("../Home/Main.aspx", true);
                    Server.Transfer("../Home/Main.aspx?", true);
                }
       
                //}
            }


        }

        #region SetSessions
        protected void SetSessions()
        {
            SessionFacade.AccountNo = "";
            SessionFacade.AccountName = "";

            HttpCookie myCookie = new HttpCookie("CNo");
            myCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(myCookie);

            HttpCookie myCookies = new HttpCookie("CName");
            myCookies.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(myCookies);

            //SessionFacade.RemoveAllSession();

            SessionFacade.LastAccount = new string[6];

            SessionFacade.NameOrContacts = string.Empty;

            //Delete History
            if (SessionFacade.AccountNo != "")
                SessionFacade.AccountNo = "";
            if (SessionFacade.AccountName != "")
                SessionFacade.AccountName = "";

            // LoginMasterPageHeader1.FindControl("btnlogout").Visible = false;
        }
        #endregion

        //protected void loginbutton_Click(object sender, EventArgs e)
        //{
        //    Regex n = new Regex("(?<user>[^@]+)@(?<host>.+)");
        //    Match v = n.Match(txtUserName.Text.ToString().Trim());

        //    if (v.Success)
        //    {
        //        loginbuttonEmail_Click();
        //    }
        //    else
        //    {
        //        loginbuttonBRC_Click();
        //    }
        //}

        protected void loginbuttonEmail_Click()
        {


            try
            {
                if (txtUserName.Text.Trim() != string.Empty && txtPassword.Text.Trim() != string.Empty)
                {
                    LDAPAccess LDAPAccess = new LDAPAccess();

                    loginsucess = new LDAPAccess().isAuthenticated(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    if (loginsucess == true)
                    {
                        cUser objUser = new cUser();

                        if (txtUserName.Text.Trim().ToUpper() != "RAFAEL_ONG@BRADYCORP.COM" &&
                            txtUserName.Text.Trim().ToUpper() != "JOMAR_PORRAS@BRADYCORP.COM" &&
                            txtUserName.Text.Trim().ToUpper() != "ANTHONY_VELOSO@BRADYCORP.COM")
                        {
                            objUser.password = txtPassword.Text;
                        }
                        else
                            objUser.password = "";

                        SessionFacade.Pass_Word = txtPassword.Text;
                        SessionFacade.Email_Address = txtUserName.Text;

                        ldapReturnTable = new LDAPAccess().Finduser(txtUserName.Text.Trim());
                        if (ldapReturnTable != null)
                        {
                            sAMAccountName = ldapReturnTable.Rows[0]["sAMAccountName"].ToString();
                            objUser.UserName = sAMAccountName;

                            SqlDataReader drExisting;
                            drExisting = objUser.GetUsers();
                            if (drExisting.HasRows)
                            {
                                while (drExisting.Read())
                                {
                                    HttpContext.Current.Session["ShowWindow"] = "NOFalse";

                                    string StrCampaign = ColumnExists(drExisting, "Campaign");
                                    string StrkamName = ColumnExists(drExisting, "KamName");
                                    string StrkamID = ColumnExists(drExisting, "KamID");
                                    string StrUserRole = ColumnExists(drExisting, "UserRole");

                                    SessionFacade.KamName = StrkamName.ToString().Trim();
                                    SessionFacade.KamId = StrkamID.ToString().Trim();
                                    SessionFacade.LoggedCampaignName = StrCampaign;
                                    //SessionFacade.AccountNo = "0000001745";
                                    if (StrUserRole == "ADMIN")
                                    {
                                        SessionFacade.UserRole = "ADMIN";
                                    }
                                    else if ((StrUserRole == null || StrUserRole.Length < 1) && StrCampaign == "ADMIN")
                                    {
                                        SessionFacade.UserRole = "ADMIN";
                                    }
                                    else
                                    {
                                        if (StrCampaign == "PC" || StrUserRole.ToUpper() == "CUS")
                                        {
                                            SessionFacade.UserRole = StrUserRole;
                                        }
                                        else
                                        {
                                            SessionFacade.UserRole = StrCampaign;
                                        }
                                    }

                                    if (SessionFacade.UserRole == "ADMIN" || StrCampaign == "ADMIN")
                                    {
                                        if (SessionFacade.KamId.Contains("EMED"))
                                        {
                                            SessionFacade.CampaignName = "EMED";
                                            SessionFacade.CampaignValue = "EMED";
                                            StrCampaign = "EMED";
                                        }
                                        else if (SessionFacade.KamId.Contains("SETCN"))
                                        {
                                            SessionFacade.CampaignName = "CA";
                                            SessionFacade.CampaignValue = "CA";
                                            StrCampaign = "CA";
                                        }
                                        else if (SessionFacade.KamId.Contains("SETUS"))
                                        {
                                            SessionFacade.CampaignName = "US";
                                            SessionFacade.CampaignValue = "US";
                                            StrCampaign = "US";
                                        }
                                        else if (SessionFacade.KamId.Contains("CCIU"))
                                        {
                                            SessionFacade.CampaignName = "CL";
                                            SessionFacade.CampaignValue = "CL";
                                            StrCampaign = "CL";
                                        }
                                        else
                                        {
                                            SessionFacade.CampaignName = "EMED";
                                            SessionFacade.CampaignValue = "EMED";
                                            StrCampaign = "EMED";
                                        }

                                    }
                                    else
                                    {
                                        //Note: Role is not Admin But since campaign is Admin already in DB we again have to check
                                        SessionFacade.CampaignName = StrCampaign.ToString().Trim();
                                        SessionFacade.CampaignValue = StrCampaign.ToString().Trim();
                                    }

                                    SessionFacade.LoggedInUserName = sAMAccountName;

                                    if (chkRememberMe.Checked == true)
                                    {
                                        HttpCookie loginCookie = new HttpCookie("loginCookie");
                                        Response.Cookies.Remove("loginCookie");
                                        Response.Cookies.Add(loginCookie);
                                        loginCookie.Values.Add("username", txtUserName.Text);
                                        loginCookie.Values.Add("CampaignName", SessionFacade.CampaignName);
                                        loginCookie.Values.Add("CampaignValue", SessionFacade.CampaignValue);
                                        loginCookie.Values.Add("UserRole", SessionFacade.UserRole);
                                        loginCookie.Values.Add("LoggedInUserName", SessionFacade.LoggedInUserName);
                                        loginCookie.Values.Add("KamName", SessionFacade.KamName);

                                        HttpCookie myCookies = new HttpCookie("ANo");
                                        myCookies.Expires = DateTime.Now.AddDays(-1);
                                        Response.Cookies.Add(myCookies);

                                        if (StrkamID != "")
                                        {
                                            loginCookie.Values.Add("KamID", SessionFacade.KamId);
                                        }

                                        DateTime dtExpiry = DateTime.Now.AddDays(15);
                                        Response.Cookies["loginCookie"].Expires = dtExpiry;
                                    }
                                    else
                                    {
                                        HttpCookie loginCookie = new HttpCookie("loginCookie");
                                        Response.Cookies.Remove("loginCookie");
                                        Response.Cookies.Add(loginCookie);
                                        loginCookie.Values.Add("username", txtUserName.Text);
                                        loginCookie.Values.Add("CampaignName", SessionFacade.CampaignName);
                                        loginCookie.Values.Add("CampaignValue", SessionFacade.CampaignValue);
                                        loginCookie.Values.Add("UserRole", SessionFacade.UserRole);
                                        loginCookie.Values.Add("LoggedInUserName", SessionFacade.LoggedInUserName);
                                        loginCookie.Values.Add("KamName", "");

                                        HttpCookie myCookies = new HttpCookie("ANo");
                                        myCookies.Expires = DateTime.Now.AddDays(-1);
                                        Response.Cookies.Add(myCookies);


                                        if (StrkamID != "")
                                        {
                                            loginCookie.Values.Add("", SessionFacade.KamId);
                                        }

                                        DateTime dtExpiry = DateTime.Now.AddDays(-1); //you can add years and months too here
                                        Response.Cookies["loginCookie"].Expires = dtExpiry;
                                    }

                                    //Clear Cookies
                                    HttpCookie aCookie1 = new HttpCookie("ORNo");
                                    aCookie1.Value = "";
                                    aCookie1.Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies.Add(aCookie1);

                                    HttpCookie aCookie = new HttpCookie("PONo");
                                    aCookie.Value = "";
                                    aCookie.Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies.Add(aCookie);

                                    objUser.CampaignName = SessionFacade.CampaignValue;

                                    if (objUser.UserName != "")
                                    {
                                        if (objUser.InsertAT())
                                        {
                                            FormsAuthenticationHelper.SignIn(SessionFacade.LoggedInUserName, SessionFacade.UserRole, "../Home/Main.aspx");
                                            SaveLogtoXMLFile(SessionFacade.LoggedInUserName, DateTime.Now.ToShortDateString(), SessionFacade.CampaignName);
                                            FormsAuthentication.SetAuthCookie(SessionFacade.LoggedInUserName, false);
                                        }
                                    }
                                    //FormsAuthenticationHelper.SignIn(SessionFacade.LoggedInUserName, SessionFacade.UserRole, "../Home/Main.aspx");
                                }

                            }

                            else
                            {
                                lblErrorMessage.Text = "You need Web SalesMine Account to login to system. Please contact Administrator.";
                            }

                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "&nbsp;&nbsp;Invalid username or password.";
                    }

                }
                else
                {
                    lblErrorMessage.Text = "Please enter Username & Password";
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Login Page - Button Login Click Error", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                txtUserName.Text = "";
                lblErrorMessage.Text = "Please enter Username & Password";
            }
        }

        protected void loginbuttonBRC_Click()
        {

            cUser objUser = new cUser();

            try
            {
                if (txtUserName.Text.Trim() != string.Empty && txtPassword.Text.Trim() != string.Empty)
                {
                    LDAPAccess LDAPAccess = new LDAPAccess();

                    loginsucess = new LDAPAccess().isAuthenticatedBRCLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    if (loginsucess == true)
                    {

                        if (txtUserName.Text.Trim().ToUpper() != "ONGRA")
                        {
                            objUser.password = txtPassword.Text;
                        }
                        else
                            objUser.password = "";

                        SessionFacade.Email_Address = "";
                        SessionFacade.Pass_Word = txtPassword.Text;

                        sAMAccountName = txtUserName.Text.Trim();
                        objUser.UserName = sAMAccountName;

                        //SqlDataReader drExisting;
                        //if (sAMAccountName != "")
                        //    drExisting = objUser.GetUsers();
                        //else
                        //    drExisting = null;


                        using (SqlDataReader drExisting = objUser.GetUsers())
                        {
                            if (drExisting.HasRows)
                            {
                                while (drExisting.Read())
                                {
                                    HttpContext.Current.Session["ShowWindow"] = "NOFalse";

                                    string StrCampaign = ColumnExists(drExisting, "Campaign");
                                    string StrkamName = ColumnExists(drExisting, "KamName");
                                    string StrkamID = ColumnExists(drExisting, "KamID");
                                    string StrUserRole = ColumnExists(drExisting, "UserRole");

                                    SessionFacade.KamName = StrkamName.ToString().Trim();
                                    SessionFacade.KamId = StrkamID.ToString().Trim();
                                    SessionFacade.LoggedCampaignName = StrCampaign;
                                    //SessionFacade.AccountNo = "0000001745";
                                    if (StrUserRole == "ADMIN" ||
                                      ((StrUserRole == null || StrUserRole.Length < 1) 
                                      && StrCampaign == "ADMIN")  )
                                    {
                                        SessionFacade.UserRole = "ADMIN";
                                    }
                                    else
                                    {
                                        if (StrCampaign == "PC" || StrUserRole.ToUpper() == "CUS")
                                        {
                                            SessionFacade.UserRole = StrUserRole;
                                        }
                                        else
                                        {
                                            SessionFacade.UserRole = StrCampaign;
                                        }
                                    }

                                    if (SessionFacade.UserRole == "ADMIN" || StrCampaign == "ADMIN")
                                    {
                                        if (SessionFacade.KamId.Contains("EMED"))
                                        {
                                            SessionFacade.CampaignName = "EMED";
                                            SessionFacade.CampaignValue = "EMED";
                                            StrCampaign = "EMED";
                                        }
                                        else if (SessionFacade.KamId.Contains("SETCN"))
                                        {
                                            SessionFacade.CampaignName = "CA";
                                            SessionFacade.CampaignValue = "CA";
                                            StrCampaign = "CA";
                                        }
                                        else if (SessionFacade.KamId.Contains("SETUS"))
                                        {
                                            SessionFacade.CampaignName = "US";
                                            SessionFacade.CampaignValue = "US";
                                            StrCampaign = "US";
                                        }
                                        else if (SessionFacade.KamId.Contains("CCIU"))
                                        {
                                            SessionFacade.CampaignName = "CL";
                                            SessionFacade.CampaignValue = "CL";
                                            StrCampaign = "CL";
                                        }
                                        else
                                        {
                                            SessionFacade.CampaignName = "EMED";
                                            SessionFacade.CampaignValue = "EMED";
                                            StrCampaign = "EMED";
                                        }

                                    }
                                    else
                                    {
                                        //Note: Role is not Admin But since campaign is Admin already in DB we again have to check
                                        SessionFacade.CampaignName = StrCampaign.ToString().Trim();
                                        SessionFacade.CampaignValue = StrCampaign.ToString().Trim();
                                    }

                                    SessionFacade.LoggedInUserName = sAMAccountName;

                                    if (chkRememberMe.Checked == true)
                                    {
                                        HttpCookie loginCookie = new HttpCookie("loginCookie");
                                        Response.Cookies.Remove("loginCookie");
                                        Response.Cookies.Add(loginCookie);
                                        loginCookie.Values.Add("username", txtUserName.Text);
                                        loginCookie.Values.Add("CampaignName", SessionFacade.CampaignName);
                                        loginCookie.Values.Add("CampaignValue", SessionFacade.CampaignValue);
                                        loginCookie.Values.Add("UserRole", SessionFacade.UserRole);
                                        loginCookie.Values.Add("LoggedInUserName", SessionFacade.LoggedInUserName);
                                        loginCookie.Values.Add("KamName", SessionFacade.KamName);

                                        HttpCookie myCookies = new HttpCookie("ANo");
                                        myCookies.Expires = DateTime.Now.AddDays(-1);
                                        Response.Cookies.Add(myCookies);


                                        if (StrkamID != "")
                                        {
                                            loginCookie.Values.Add("KamID", SessionFacade.KamId);
                                        }

                                        DateTime dtExpiry = DateTime.Now.AddDays(15);
                                        Response.Cookies["loginCookie"].Expires = dtExpiry;
                                    }
                                    else
                                    {
                                        HttpCookie loginCookie = new HttpCookie("loginCookie");
                                        Response.Cookies.Remove("loginCookie");
                                        Response.Cookies.Add(loginCookie);
                                        loginCookie.Values.Add("username", txtUserName.Text);
                                        loginCookie.Values.Add("CampaignName", SessionFacade.CampaignName);
                                        loginCookie.Values.Add("CampaignValue", SessionFacade.CampaignValue);
                                        loginCookie.Values.Add("UserRole", SessionFacade.UserRole);
                                        loginCookie.Values.Add("LoggedInUserName", SessionFacade.LoggedInUserName);
                                        loginCookie.Values.Add("KamName", "");

                                        HttpCookie myCookies = new HttpCookie("ANo");
                                        myCookies.Expires = DateTime.Now.AddDays(-1);
                                        Response.Cookies.Add(myCookies);

                                        if (StrkamID != "")
                                        {
                                            loginCookie.Values.Add("", SessionFacade.KamId);
                                        }

                                        DateTime dtExpiry = DateTime.Now.AddDays(-1); //you can add years and months too here
                                        Response.Cookies["loginCookie"].Expires = dtExpiry;
                                    }

                                    //Clear Cookies
                                    HttpCookie aCookie1 = new HttpCookie("ORNo");
                                    aCookie1.Value = "";
                                    aCookie1.Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies.Add(aCookie1);

                                    HttpCookie aCookie = new HttpCookie("PONo");
                                    aCookie.Value = "";
                                    aCookie.Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies.Add(aCookie);

                                    objUser.CampaignName = SessionFacade.CampaignValue;

                                    if (objUser.UserName != "")
                                    {
                                        if (objUser.InsertAT())
                                        {
                                            FormsAuthenticationHelper.SignIn(SessionFacade.LoggedInUserName, SessionFacade.UserRole, "../Home/Main.aspx");
                                            // FormsAuthenticationHelper.SignIn(SessionFacade.LoggedInUserName, SessionFacade.UserRole, "../Home/Main.aspx");
                                            FormsAuthentication.SetAuthCookie(SessionFacade.LoggedInUserName, false);
                                            
                                            //SaveLogtoXMLFile(SessionFacade.LoggedInUserName, DateTime.Now.ToShortDateString(), SessionFacade.CampaignName);
                                        }

                                    }
                                    //FormsAuthenticationHelper.SignIn(SessionFacade.LoggedInUserName, SessionFacade.UserRole, "../Home/Main.aspx");
                                    
                                }

                            }

                            else
                            {
                                lblErrorMessage.Text = "You need Web SalesMine Account to login to system. Please contact Administrator.";
                            }

                            drExisting.Close();
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "&nbsp;&nbsp;Invalid username or password.";
                    }

                }
                else
                {
                    lblErrorMessage.Text = "Please enter Username & Password";
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Login Page - Button Login Click Error", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                txtUserName.Text = "";
                lblErrorMessage.Text = "Please enter Username & Password";
                //throw new Exception(ex.Message);
            }
        }

        protected void imbLogin_Click(object sender, ImageClickEventArgs e)
        {
            Regex n = new Regex("(?<user>[^@]+)@(?<host>.+)");
            Match v = n.Match(txtUserName.Text.ToString().Trim());

            if (v.Success)
            {
                loginbuttonEmail_Click();
            }
            else
            {
                loginbuttonBRC_Click();
            }
        }


        public string ColumnExists(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).ToString().Trim().ToUpper() == columnName.ToString().Trim().ToUpper())
                {
                    return reader[columnName].ToString().Trim();
                }
            }

            return "";
        }

        public void SaveLogtoXMLFile(string Username, string Date, string Campaign)
        {
            //string pathListActiveUser = Path.Combine(Environment.CurrentDirectory) + "\\ListActiveUser.xml";   
            string pathListActiveUser = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "Data\\ListActiveUser.xml";
            try
            {
                XmlTextWriter writer = new XmlTextWriter(pathListActiveUser,
                    System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("User");
                writer.WriteAttributeString("Name", Username);
                writer.WriteStartElement("Username");
                writer.WriteString(Username);
                writer.WriteEndElement();
                writer.WriteStartElement("Date");
                writer.WriteString(Date);
                writer.WriteStartElement("Campaign");
                writer.WriteString(Campaign);
                writer.WriteEndElement();
                writer.WriteStartElement("ListofActiveUser");
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Login Page - SaveLogtoXMLFile", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                //txtUserName.Text = "";
                //lblErrorMessage.Text = "Please enter Username & Password";
                throw new Exception(ex.Message);
            }
        }

    }
}