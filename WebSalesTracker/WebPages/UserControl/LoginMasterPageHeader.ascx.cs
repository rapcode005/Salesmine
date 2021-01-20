using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSalesMine.WebPages.UserControl
{
    public partial class LoginMasterPageHeader : System.Web.UI.UserControl
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

        
          public string LogoUrl
          {
            set {imgbradylogo.ImageUrl = value; }
          }

          public string LogoNavigateUrl
          {
            set { imgbradylogo.NavigateUrl = value; }
          }
  #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

     
    }
}



