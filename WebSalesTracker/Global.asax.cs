using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using AppLogic;
using System.IO;
using System.Xml;

namespace WebSalesMine.WebSalesMine
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
           // DeleteLogfromXML(SessionFacade.LoggedInUserName, DateTime.Now.ToShortDateString(), SessionFacade.CampaignName);
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            //SaveLogtoXMLFile(SessionFacade.LoggedInUserName, DateTime.Now.ToShortDateString(), SessionFacade.CampaignName);
           // DeleteLogfromXML(SessionFacade.LoggedInUserName, DateTime.Now.ToShortDateString(), SessionFacade.CampaignName);
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
           // Response.Redirect("~/WebPages/Login/Login.aspx");
           //FormsAuthenticationHelper.SignOut();
           //SaveLogtoXMLFile(SessionFacade.LoggedInUserName, DateTime.Now.ToShortDateString(), SessionFacade.CampaignName);
           //DeleteLogfromXML(SessionFacade.LoggedInUserName, DateTime.Now.ToShortDateString(), SessionFacade.CampaignName);
        }

        //public void DeleteLogfromXML(string Username, string Date, string Campaign)
        //{
        //    //string pathListActiveUser = Path.Combine(Environment.CurrentDirectory) + "\\ListActiveUser.xml";   
        //    string pathListActiveUser = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "Data\\ListActiveUser.xml";
        //    try
        //    {
        //        // create the XML, load the contents
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(pathListActiveUser);

        //        // find a node - here the one with name='abc'
        //        XmlNode node = doc.SelectSingleNode("/User[@name='" + Username + "']");
        //        ;
        //        // if found....
        //        if (node != null)
        //        {
        //            // get its parent node
        //            XmlNode parent = node.ParentNode;

        //            // remove the child node
        //            parent.RemoveChild(node);

        //            // verify the new XML structure
        //            string newXML = doc.OuterXml;

        //            // save to file or whatever....
        //            doc.Save(pathListActiveUser);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogException(ex, "Login Page - SaveLogtoXMLFile", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
        //        //txtUserName.Text = "";
        //        //lblErrorMessage.Text = "Please enter Username & Password";
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
