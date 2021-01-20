using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLogic;
using System.Web.Security;

namespace WebSalesMine.WebPages.Home
{
    public partial class Chatting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = (string)Application["msg"];

            txtMessageList.Text = msg;
        }

        protected void onMessageClick(object sender, EventArgs e)
        {
            string name = SessionFacade.LoggedInUserName;
            string message = txtMessage.Text;
            string my = name + ":" + message;

            Application["msg"] = Application["msg"] + my + Environment.NewLine;

            txtMessageList.Text = Application["msg"].ToString();

            txtMessage.Text = "";
        }
    }
}