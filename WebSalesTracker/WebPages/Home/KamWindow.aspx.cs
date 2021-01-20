using System;
using System.Data; // Name space for Dataset, Dataadapter etc
using System.Configuration; //name space needed to access the Web Config file
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AppLogic;

using System.IO; //This namespace has to be used if you are doing any file operation ( For example  read or write text file,excel,)
using System.Text;
using System.Collections; //This namespace has to be used while using arrays etc
using System.Xml; //This will be used if you want to read Or use xml files
using System.Data.SqlClient; ////This namespace has to be used while accessing  SQL Database




namespace WebSalesMine.WebPages.Home
{
    public partial class KamWindow : System.Web.UI.Page
    {
        public static string MainAccountNo = "";
        public static string MainAccountName = "";

        public static SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
        public static SqlConnection OnlineConstre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaOnlineCONNECTON"].ToString());

        protected DataSet KamDataSet = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (SessionFacade.KamId != null)
                {
                    if (SessionFacade.KamId.Length > 0)
                    {
                        pnlShowKamWindow.Visible = true;
                        litMessage.Visible = false;
                        LoadKamAllData();
                    }
                    else
                    {
                        pnlShowKamWindow.Visible = false;
                        litMessage.Visible = true;
                    }
                }

            }

        }



        public void LoadKamAllData()
        {
            DataSet ds = new DataSet();
            ds = ReturnKamData("");

            if (ds.Tables[0].Rows.Count > 0)
            {
                litErrorinGrid.Text = "";
                BindGrid(ds);
            }
            else
            {
                litErrorinGrid.Text = "No Records Found";
            }
        }

        protected void btnShowPage(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                if (b.ID == "OrderHistory")
                {
                    ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../OrderHistory/OrderHistory.aspx'; ", true);
                }
                if (b.ID == "ProductSummary")
                {
                    ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../ProductSummary/ProductSummary.aspx'; ", true);
                }

                if (b.ID == "NotesCommHistory")
                {
                    ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../NotesCommHistory/NotesCommHistory.aspx'; ", true);
                }
                if (b.ID == "Quotes")
                {
                    ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../Quotes/Quotes.aspx'; ", true);
                }

                if (b.ID == "SiteAndContactInfo")
                {
                    ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../SiteAndContactInfo/SiteAndContactInfo.aspx'; ", true);
                }
                if (b.ID == "CustomerLookUp")
                {
                    ScriptManager.RegisterStartupScript(this.KamGridUpdatePanel, typeof(string), "callme", "window.opener.location='../CustomerLookUp/CustomerLookUp.aspx'; ", true);
                }

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),"call me", "freezeHeader()", true);
            }
            
        }
        

        protected void BtnClearSearchKam_Click(object sender, EventArgs e)
        {
            txtbSearchKam.Text = "";
            rdoAccountName.Checked = false;
            rdoManagedGroup.Checked = false;
            LoadKamAllData();
        }

        //load = 0 = Show all records
        //load = 1 = Show only searachded accout name
        //load = 2 = Show only searachded managed account


        public DataSet ReturnKamData(string str)
        {
            DataSet ds = new DataSet();
            string strResult = string.Empty;
            if (str.Length == 0)
            {
                strResult = "select SOLD_TO,NAME,MG_NAME,CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as LTSALES,LPDCUST from " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "'";
            }
            if (str == "AccountName")
            {
                strResult = "select SOLD_TO,NAME,MG_NAME,CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as LTSALES,LPDCUST from  " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND NAME like '%" + txtbSearchKam.Text.Trim() + " %' AND salesteam='" + SessionFacade.KamId + "'";
            }
            if (str == "Group")
            {
                strResult = "select SOLD_TO,NAME,MG_NAME,CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as LTSALES,LPDCUST from  " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND MG_NAME like '%" + txtbSearchKam.Text.Trim() + " %' AND salesteam='" + SessionFacade.KamId + "'";
            }


            ds = SqlHelper.SqlDataExcuteDataSet(OnlineConstre, CommandType.Text, strResult);
            return ds;
        }



        void BindGrid(DataSet ds)
        {
            gridShowAllUserData.DataSource = ds;
            gridShowAllUserData.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "freezeHeader()", true);
        }

        protected void gridShowAllUserData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView _gridView = (GridView)sender;

            // Get the selected index and the command name

            int _selectedIndex = 0;
            try
            {
                _selectedIndex = int.Parse(e.CommandArgument.ToString());
            }
            catch
            {
                _selectedIndex = 0;
            }
            //if (_selectedIndex != 0)
            //{
                string _commandName = e.CommandName;

                switch (_commandName)
                {
                    case ("SingleClick"):
                        _gridView.SelectedIndex = _selectedIndex;
                        GridViewRow row = _gridView.SelectedRow;

                        SessionFacade.AccountNo = row.Cells[1].Text.ToString().Trim();
                        SessionFacade.AccountName = row.Cells[2].Text.ToString().Trim();

                        break;

                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                       "call me", "freezeHeader()", true);
           // }
        }

        protected void gridShowAllUserData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the LinkButton control in the first cell

                LinkButton _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
                // Get the javascript which is assigned to this LinkButton

                string _jsSingle =
                ClientScript.GetPostBackClientHyperlink(_singleClickButton, "");

                e.Row.Attributes["onclick"] = _jsSingle;
            }
        }

        protected void gridShowAllUserDataPageEventHandler(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (rdoAccountName.Checked == true && txtbSearchKam.Text.Length > 0)
            {
                ds = ReturnKamData("AccountName");
            }
            if (rdoManagedGroup.Checked == true && txtbSearchKam.Text.Length > 0)
            {
                ds = ReturnKamData("Group");
            }

            if (txtbSearchKam.Text.Length == 0)
            {
                ds = ReturnKamData("");
            }

            gridShowAllUserData.DataSource = ds;
            gridShowAllUserData.PageIndex = e.NewPageIndex;
            gridShowAllUserData.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                       "call me", "freezeHeader()", true);

        }

        protected void gridShowAllUserData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gridShowAllUserData.SelectedRow;

        }
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in gridShowAllUserData.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    Page.ClientScript.RegisterForEventValidation
                            (r.UniqueID + "$ctl00");

                }
            }

            base.Render(writer);
        }

        protected void btnSearchKam_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            if (rdoAccountName.Checked)
            {
                ds = ReturnKamData("AccountName");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    litErrorinGrid.Text = "";
                    BindGrid(ds);
                }
                else
                {
                    litErrorinGrid.Text = "No Records Found";

                }
            }
            else if (rdoManagedGroup.Checked)
            {
                ds = ReturnKamData("Group");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    litErrorinGrid.Text = "";
                    BindGrid(ds);
                }
                else
                {
                    litErrorinGrid.Text = "No Records Found";
                }
            }
            else { litErrorinGrid.Text = "Please choose filter creteria"; }


        }

        public SortDirection GridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {

                GridViewSortDirection = SortDirection.Descending;

                SortGridView(sortExpression, "DESC");

            }

            else
            {

                GridViewSortDirection = SortDirection.Ascending;

                SortGridView(sortExpression, "ASC");

            }

        }

        private void SortGridView(string sortExpression, string direction)
        {

            // You can cache the DataTable for improving performance

            DataSet ds = new DataSet();
            if (rdoAccountName.Checked == true && txtbSearchKam.Text.Length > 0)
            {
                ds = ReturnKamData("AccountName");
            }
            if (rdoManagedGroup.Checked == true && txtbSearchKam.Text.Length > 0)
            {
                ds = ReturnKamData("Group");
            }

            if (txtbSearchKam.Text.Length == 0)
            {
                ds = ReturnKamData("");
            }


            DataTable dt = ds.Tables[0];

            DataView dv = new DataView(dt);

            dv.Sort = sortExpression + " " + direction;



            gridShowAllUserData.DataSource = dv;
            gridShowAllUserData.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "freezeHeader()", true);
            
        }


        #region Delegates and Events

        public delegate void KamBtn_Click(object sender, EventArgs e);

        public event KamBtn_Click KamBtn_Click_Event;

        #endregion

     

    }
}