using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLogic;

namespace WebSalesMine.WebPages.Home
{
    public partial class Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMessage.Text = SessionFacade.PopupMessage;
        }
    }
}