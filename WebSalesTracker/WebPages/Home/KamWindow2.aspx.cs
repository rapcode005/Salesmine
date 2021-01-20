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
using System.Data.SqlClient;
using System.Drawing; ////This namespace has to be used while accessing  SQL Database

namespace WebSalesMine.WebPages.Home
{
    public partial class KamWindow2 : System.Web.UI.Page
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

                if (GridView1.Rows.Count > 0)
                {
                    GridView1.UseAccessibleHeader = true;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

            }

        }


        public int selectedroindex
        {
            get
            {
                if (ViewState["selectedroindex"] != null)
                    return (int)ViewState["selectedroindex"];
                else
                    return -1;
            }
            set
            {
                ViewState["selectedroindex"] = value;
            }
        }

        protected void gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int myOldSelectedRowIndex = selectedroindex;
            if (myOldSelectedRowIndex != -1)
            {
                GridViewRow selectedPreviousRow = GridView1.Rows[myOldSelectedRowIndex];
                selectedPreviousRow.BackColor = System.Drawing.Color.White;       
            }

            GridViewRow mySelectedRow =
               ((GridViewRow)(((DataControlFieldCell)(((WebControl)(e.CommandSource)).Parent)).Parent));

            int myNewSelectedRowIndex = mySelectedRow.RowIndex;
            ViewState["selectedroindex"] = myNewSelectedRowIndex;

            GridViewRow currentSelectedRow = GridView1.Rows[myNewSelectedRowIndex];
            currentSelectedRow.BackColor = System.Drawing.Color.Aqua;

            switch (e.CommandName)
            {

                case "Click":
                    {
                        SessionFacade.AccountNo = e.CommandArgument.ToString();
                       break;
                    }
                default: //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "freezeHeader()", true);
                    break;

            }


            if (this.GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "freezeHeader()", true);
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
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
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

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "freezeHeader()", true);
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
                strResult = "select SOLD_TO as [Account Number],NAME as [Account Name],MG_NAME as [Managed Group],CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as [Life Time Sales],LPDCUST as [Last Ordered Date] from " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "'";
                //strResult = "select SOLD_TO,NAME,MG_NAME,CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as LTSALES,LPDCUST from " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND salesteam='" + SessionFacade.KamId + "'";
            }
            if (str == "AccountName")
            {
                strResult = "select SOLD_TO as [Account Number],NAME as [Account Name],MG_NAME as [Managed Group],CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as [Life Time Sales],LPDCUST as [Last Ordered Date] from  " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND NAME like '%" + txtbSearchKam.Text.Trim() + " %' AND salesteam='" + SessionFacade.KamId + "'";
                //strResult = "select SOLD_TO,NAME,MG_NAME,CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as LTSALES,LPDCUST from  " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND NAME like '%" + txtbSearchKam.Text.Trim() + " %' AND salesteam='" + SessionFacade.KamId + "'";
            }
            if (str == "Group")
            {
                strResult = "select SOLD_TO as [Account Number],NAME as [Account Name],MG_NAME as [Managed Group],CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as [Life Time Sales],LPDCUST as [Last Ordered Date] from  " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND MG_NAME like '%" + txtbSearchKam.Text.Trim() + " %' AND salesteam='" + SessionFacade.KamId + "'";
                //strResult = "select SOLD_TO,NAME,MG_NAME,CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as LTSALES,LPDCUST from  " + SessionFacade.CampaignName + ".SITEINFO where SOLD_TO <> '' AND MG_NAME like '%" + txtbSearchKam.Text.Trim() + " %' AND salesteam='" + SessionFacade.KamId + "'";
            }


            ds = SqlHelper.SqlDataExcuteDataSet(OnlineConstre, CommandType.Text, strResult);
            return ds;
        }



        void BindGrid(DataSet ds)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                   "call me", "freezeHeader()", true);
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
                      ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "freezeHeader()", true);
                }
                else
                {
                    litErrorinGrid.Text = "No Records Found";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "freezeHeader()", true);
                }
            }
            else
            {
                litErrorinGrid.Text = "Please choose filter creteria"; ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "freezeHeader()", true);
            }


        }







    }
}