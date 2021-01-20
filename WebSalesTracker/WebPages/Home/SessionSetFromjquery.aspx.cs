using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebSalesMine.WebPages.Home
{
    public partial class SessionSetFromjquery : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        [System.Web.Services.WebMethod]
        public static object SaveValueInSession(string var1)
        {
            //window = 0 - true . display window
                      //1 - false. DOnt display window
   
            // Status - 1 - Minimized
            // status - 2 - Normal 
            HttpContext.Current.Session["ShowWindow"] = var1.ToString();
            //callfun();
           // HttpContext.Current.Session["ShowWindowStatus"] = status;
            return "success";
        }

        //Button clicl 
        //{ Call fun}

        //void callfun ()
    
    }
}