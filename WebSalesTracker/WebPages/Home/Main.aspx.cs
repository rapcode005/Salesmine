using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLogic;
namespace WebSalesMine.WebPages.Home
{
    public partial class Main : System.Web.UI.Page
    {
        public string campaign = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            HttpCookie aCookie2 = new HttpCookie("CSS");
            aCookie2.Value = "";
            aCookie2.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(aCookie2);

            HttpCookie aCookie3 = new HttpCookie("CProjID");
            aCookie3.Value = "";
            aCookie3.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(aCookie3);

            campaign = SessionFacade.UserRole.Trim();

            //Label lblTemp = Master.FindControl("lblAccountName") as Label;

            ////Hide Account Name
            //lblTemp.Visible = true;

            //if (ddlTemp.Text == "PC")
            //{
            //    liCustomerSearch.Visible = false;
            //    liQuotes.Visible = false;
            //}
            //else
            //{
            //    liCustomerSearch.Visible = true;
            //    liQuotes.Visible = true;
            //}

        }
    }
}