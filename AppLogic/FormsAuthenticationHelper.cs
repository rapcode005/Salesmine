using System;
using System.Web.Security;
using System.Web;

namespace AppLogic
{
    public class FormsAuthenticationHelper
    {
        private enum TicketItems
        {
            UserName,
            Role
        }

        #region Properties
        public string UserName
        {
            get
            {
                return GetUserName(TicketItems.UserName);
            }
        }

        public string UserRole
        {
            get
            {
                return GetUserName(TicketItems.Role);
            }
        }

        public static string LoginURL
        {
            get
            {
                return FormsAuthentication.LoginUrl;
            }
        }

        public static string DefaultUrl
        {
            get
            {
                return FormsAuthentication.DefaultUrl;
            }
        }

        #endregion

        #region Public Methods

        public static void SignIn(string userName, string role)
        {
            double timeOut = 240;
            FormsAuthentication.Initialize();
            FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(timeOut), false, role, FormsAuthentication.FormsCookiePath);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));
            FormsAuthentication.RedirectFromLoginPage(userName, true);
        }

        public static void SignIn(string userName, string role, string redirectToPageURL)
        {
            double timeOut = 240;
            FormsAuthentication.Initialize();
            FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(timeOut), false, role, FormsAuthentication.FormsCookiePath);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));
            HttpContext.Current.Response.Redirect(redirectToPageURL, false);//, true);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        public static void RedirectToLoginPage()
        {
            FormsAuthentication.RedirectToLoginPage();
        }
        #endregion

        private static string GetUserName(TicketItems ticketItems)
        {
            string userData = string.Empty;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    if (ticketItems == TicketItems.UserName)
                    {
                        userData = ticket.Name;
                    }
                    else if (ticketItems == TicketItems.Role)
                    {
                        userData = ticket.UserData;
                    }
                }
            }
            return userData;
        }
    }
}