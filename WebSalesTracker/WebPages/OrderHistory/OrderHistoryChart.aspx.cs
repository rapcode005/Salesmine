using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AppLogic;
using System.Collections;
using System.IO;
using ClassLibrary1;
using System.Globalization;
using System.Text;

namespace WebSalesMine.WebPages.OrderHistory
{
    public partial class OrderHistoryChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                LoadChartData();
        }

        private void LoadChartData()
        {
            try
            {
                DataSet ds = new DataSet();
                cOrderHistory objOrderHistory = new cOrderHistory();

                objOrderHistory.SearchOrderCampaignName = SessionFacade.CampaignName;
                objOrderHistory.SearchOrderAccount = SessionFacade.AccountNo;

                ds = objOrderHistory.GetOrderHistoryChart();

                if (ds != null && 
                    ds.Tables.Count > 0)
                {
                    chtOrderHistory.DataSource = ds;

                    chtOrderHistory.Series["Series 1"].XValueMember = "Yearly";
                    chtOrderHistory.Series["Series 1"].YValueMembers = "Price";

                    chtOrderHistory.DataBind();
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "Function: LoadChartData", 
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, 
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
    }
}