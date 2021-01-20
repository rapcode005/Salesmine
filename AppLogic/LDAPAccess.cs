using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Security.Permissions;
//Note : add referance (from system Tab) to add below namespaces
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using DataAccess;

namespace AppLogic
{
    public class LDAPAccess
    {
        private DirectoryEntry RootEntry;
        private DirectorySearcher RootSearcher;
        private SearchResult Search;
        private string Ldapconnection = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;
        private DataTable dtUser;


        //public DataTable FindDetails(string BrcLogin)
        //{
        //    Ldapconnection = ConfigurationManager.AppSettings["LDAPConnectionstring"].ToString();
        //    username = ConfigurationManager.AppSettings["username"].ToString();
        //    password = ConfigurationManager.AppSettings["password"].ToString();

        //    string sMAccountname = string.Empty;
        //    RootEntry = new DirectoryEntry(Ldapconnection, username, password);

        //    RootSearcher = new DirectorySearcher(RootEntry);
        //    RootSearcher.Filter = string.Format("(&(objectCategory=Person)(anr={0}))", BrcLogin);
        //    RootSearcher.SearchScope = SearchScope.Subtree;
        //    Search = RootSearcher.FindOne();
        //}

        //Brc Login
        public bool isAuthenticatedBRCLogin(string Brclogin, string passwordOriginal)
        {
            try
            {
                Ldapconnection = ConfigurationManager.AppSettings["LDAPConnectionstring"].ToString();
                username = ConfigurationManager.AppSettings["username"].ToString();
                password = ConfigurationManager.AppSettings["password"].ToString();

                RootEntry = new DirectoryEntry(Ldapconnection, username, password);
                RootSearcher = new DirectorySearcher(RootEntry);
                //RootSearcher.Filter = "(&(objectClass=user)(userid=" + Brclogin + "))";

                RootSearcher.Filter = string.Format("(&(objectCategory=Person)(anr={0}))", Brclogin);

                RootSearcher.PageSize = 1;
                RootSearcher.SearchScope = SearchScope.OneLevel;
                RootSearcher.PropertiesToLoad.Add("sAMAccountName");
                Search = RootSearcher.FindOne();

                string sAMAccountName = GetProperty(Search, "sAMAccountName");

                if(Brclogin != sAMAccountName)
                    sAMAccountName = Brclogin;

                //if(Brclogin=="UyCh" ||  Brclogin=="HillMa" || Brclogin=="ChanNy")
                //    sAMAccountName = Brclogin;

                RootEntry = new DirectoryEntry(Ldapconnection, sAMAccountName, passwordOriginal);
                RootSearcher = new DirectorySearcher(RootEntry);
                RootSearcher.PageSize = 1;
                RootSearcher.SearchScope = SearchScope.OneLevel;
                try
                {
                    Search = RootSearcher.FindOne();
                    if (Search != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

            catch (Exception err)
            {
                //BradyCorp.Log.LoggerHelper.LogMessage("Error in LDAP Authentication for the user " + mail.ToString() + err.ToString());
                BradyCorp.Log.LoggerHelper.LogMessage("Error in LDAP Authentication for the user ");
                return false;
            }

        }

        public DataTable Finduser(string mail)
        {
            try
            {
                Ldapconnection = ConfigurationManager.AppSettings["LDAPConnectionstring"].ToString();
                username = ConfigurationManager.AppSettings["username"].ToString();
                password = ConfigurationManager.AppSettings["password"].ToString();


                string sMAccountname = string.Empty;
                RootEntry = new DirectoryEntry(Ldapconnection, username, password);

                RootSearcher = new DirectorySearcher(RootEntry);
                RootSearcher.Filter = "(&(objectClass=user)(objectCategory=person)(mail=" + mail + "))";
                RootSearcher.PageSize = 1;
                RootSearcher.SearchScope = SearchScope.OneLevel;
                RootSearcher.PropertiesToLoad.Add("mail");
                RootSearcher.PropertiesToLoad.Add("sAMAccountName");
                RootSearcher.PropertiesToLoad.Add("displayName");
                Search = RootSearcher.FindOne();
                if (Search != null)
                {
                    dtUser = UserConstructed();
                    DataRow drUser = dtUser.NewRow();
                    drUser["mail"] = GetProperty(Search, "mail");
                    drUser["sAMAccountName"] = GetProperty(Search, "sAMAccountName");
                    drUser["givenName"] = GetProperty(Search, "displayName");
                    dtUser.Rows.Add(drUser);
                }
                return dtUser;
            }

            catch
            {
                return null;
            }
        }

        public DataTable Findmail(string user)
        {
            try
            {
                Ldapconnection = ConfigurationManager.AppSettings["LDAPConnectionstring"].ToString();
                username = ConfigurationManager.AppSettings["username"].ToString();
                password = ConfigurationManager.AppSettings["password"].ToString();

                string sMAccountname = string.Empty;
                RootEntry = new DirectoryEntry(Ldapconnection, username, password);

                RootSearcher = new DirectorySearcher(RootEntry);
                //RootSearcher.Filter = "(&(objectClass=user)(objectCategory=person)(mail=" + mail + "))";
                RootSearcher.Filter = string.Format("(&(objectCategory=Person)(anr={0}))", user);
                RootSearcher.PageSize = 1;
                RootSearcher.SearchScope = SearchScope.OneLevel;
                RootSearcher.PropertiesToLoad.Add("mail");
                RootSearcher.PropertiesToLoad.Add("sAMAccountName");
                RootSearcher.PropertiesToLoad.Add("displayName");
                Search = RootSearcher.FindOne();
                if (Search != null)
                {
                    dtUser = UserConstructed();
                    DataRow drUser = dtUser.NewRow();
                    drUser["mail"] = GetProperty(Search, "mail");
                    drUser["sAMAccountName"] = GetProperty(Search, "sAMAccountName");
                    drUser["givenName"] = GetProperty(Search, "displayName");
                    dtUser.Rows.Add(drUser);
                }
                return dtUser;
            }

            catch
            {
                return null;
            }
        }

        private string ToReverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public string FindName(string user)
        {
            try
            {
                Ldapconnection = ConfigurationManager.AppSettings["LDAPConnectionstring"].ToString();
                username = ConfigurationManager.AppSettings["username"].ToString();
                password = ConfigurationManager.AppSettings["password"].ToString();

                string sMAccountname = string.Empty;
                RootEntry = new DirectoryEntry(Ldapconnection, username, password);
                RootSearcher = new DirectorySearcher(RootEntry);
                RootSearcher.Filter = string.Format("(&(objectCategory=Person)(anr={0}))", user);
                RootSearcher.PageSize = 1;
                RootSearcher.SearchScope = SearchScope.OneLevel;
                RootSearcher.PropertiesToLoad.Add("displayName");
                Search = RootSearcher.FindOne();
                if (Search != null)
                {
                    string Name = GetProperty(Search, "displayName").ToString().Trim();
                    int x = Name.Length - 1;
                    string WholeName = string.Empty;
                    
                    //First Name
                    while (Name.ToString().Trim().Substring(x,1) != " ")
                    {
                        WholeName += Name.ToString().Trim().Substring(x,1);
                        x--;
                    }

                    WholeName = ToReverse(WholeName);

                    WholeName += " ";

                    x = 0;
                    //Last Name
                    while (Name.ToString().Trim().Substring(x,1) != ",")
                    {
                        WholeName += Name.ToString().Trim().Substring(x,1);
                        x++;
                    }

                    return WholeName;
                }
                return "";
            }

            catch
            {
                return null;
            }
        }


        public bool isAuthenticated(string mail, string passwordOriginal)
        {
            try
            {
                Ldapconnection = ConfigurationManager.AppSettings["LDAPConnectionstring"].ToString();
                username = ConfigurationManager.AppSettings["username"].ToString();
                password = ConfigurationManager.AppSettings["password"].ToString();

                RootEntry = new DirectoryEntry(Ldapconnection, username, password);
                RootSearcher = new DirectorySearcher(RootEntry);
                RootSearcher.Filter = "(&(objectClass=user)(objectCategory=person)(mail=" + mail + "))";

                RootSearcher.PageSize = 1;
                RootSearcher.SearchScope = SearchScope.OneLevel;
                RootSearcher.PropertiesToLoad.Add("sAMAccountName");
                Search = RootSearcher.FindOne();

                string sAMAccountName = GetProperty(Search, "sAMAccountName");

                RootEntry = new DirectoryEntry(Ldapconnection, sAMAccountName, passwordOriginal);
                RootSearcher = new DirectorySearcher(RootEntry);
                RootSearcher.PageSize = 1;
                RootSearcher.SearchScope = SearchScope.OneLevel;
                try
                {
                    Search = RootSearcher.FindOne();
                    if (Search != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in LDAP Authentication for the user " + mail.ToString() + err.ToString());
                return false;
            }

        }

        public static string GetProperty(SearchResult search, string PropertyName)
        {
            if (search.Properties.Contains(PropertyName))
            {
                return search.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public DataTable UserConstructed()
        {
            dtUser = new DataTable();
            dtUser.Columns.Add("mail", Type.GetType("System.String"));
            dtUser.Columns.Add("sAMAccountName", Type.GetType("System.String"));
            dtUser.Columns.Add("givenname", Type.GetType("System.String"));
            return dtUser;
        }

        public DataTable FindMore(string mail)
        {
            try
            {
                Ldapconnection = ConfigurationManager.AppSettings["LDAPConnectionstring"].ToString();
                username = ConfigurationManager.AppSettings["username"].ToString();
                password = ConfigurationManager.AppSettings["password"].ToString();


                string sMAccountname = string.Empty;
                RootEntry = new DirectoryEntry(Ldapconnection, username, password);

                RootSearcher = new DirectorySearcher(RootEntry);
                RootSearcher.Filter = "(&(objectClass=user)(objectCategory=person)(mail like '" + mail + "'))";
                RootSearcher.PageSize = 1;
                RootSearcher.SearchScope = SearchScope.OneLevel;
                RootSearcher.PropertiesToLoad.Add("mail");
                RootSearcher.PropertiesToLoad.Add("sAMAccountName");
                Search = RootSearcher.FindOne();
                if (Search != null)
                {
                    dtUser = UserConstructed();
                    DataRow drUser = dtUser.NewRow();
                    drUser["mail"] = GetProperty(Search, "mail");
                    drUser["sAMAccountName"] = GetProperty(Search, "sAMAccountName");
                    dtUser.Rows.Add(drUser);
                }
                return dtUser;
            }

            catch
            {
                return null;
            }
        }

    }
}