using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AppLogic;
using System.IO;

namespace WebSalesMine.WebPages.UserControl
{
    public partial class MasterPageMainHeader : System.Web.UI.UserControl
    {
        #region
        public String UserName
        {
            get { return lblUser.Text; }
            set { lblUser.Text = value; }
        }

        public String Role
        {
            get { return lblRole.Text; }
            set { lblRole.Text = value; }
        }

        public bool UserNameVisible
        {
            get { return lblUser.Visible; }
            set { lblUser.Visible = value; }
        }

        public bool RoleVisible
        {
            get { return lblRole.Visible; }
            set { lblRole.Visible = value; }
        }

        //public bool LoginOutButtonVisible
        //{
        //    get { return btnlogout.Visible; }
        //    set { btnlogout.Visible = value; }
        //}

        //public string LoginOutButton
        //{
        //    get { return btnlogout.UniqueID; }
        //}

        public string LogoUrl
        {
            set { imgbradylogo.ImageUrl = value; }
        }

        public string LogoNavigateUrl
        {
            set { imgbradylogo.NavigateUrl = value; }
        }

        #endregion


       
        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogoutButton.Attributes.Add("onmouseover", "this.style.cursor='pointer';");

            if (!IsPostBack)
            {

                imgbradylogo.ImageUrl = "~/App_Themes/Images/" +SessionFacade.CampaignValue+".gif";

            }
        }



        //protected void btnlogout_Click(object sender, ImageClickEventArgs e)
        //{
        //    Session.Abandon();
        //    Response.Redirect("../Login/login.aspx");
            
        //}

        protected void btnlogout_Click1(object sender, EventArgs e)
        {
            Session.Abandon();

            SalesMine MyMasterPage = (SalesMine)Page.Master;

            MyMasterPage.CloseMyWindow();

          // ScriptManager.RegisterStartupScript(this.tblHeaderTable, typeof(string), "callme", " window.open('../Home/KamWindow.aspx','mywindow','width=910,height=300,scrollbars=no');  window.close();", true);
           
            Response.Redirect("../Login/login.aspx");

        }

       



        protected void imbSearchAcntNumber_Click(object sender, ImageClickEventArgs e)
        {
           
        }
    }
}