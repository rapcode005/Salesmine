#region Import Section

using System;
#endregion
namespace WebSalesMine
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("WebPages/Login/Login.aspx", true);
        }
    }
}
