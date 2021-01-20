using System;
using AppLogic;

namespace WebSalesMine.WebPages.UserControl
{
    public partial class MasterPageMainFooter : System.Web.UI.UserControl
    {
        #region  Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigureFooter();
        }

        #endregion

        #region Private Methods

        private void ConfigureFooter()
        {
            lblVersion.Text = new WebHelper().Version().ToString();
            //lblCopyRights.Text = ConfigurationFacade.CopyRights;
        }

        #endregion
    }
}