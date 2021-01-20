using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BradyCorp.Log;
using AppLogic;
using System.Data;
using System.IO;
using WebSalesMine.WebPages.UserControl;

namespace WebSalesMine.WebPages.OnHoldOrder
{
    public partial class OnHoldOrder : System.Web.UI.Page
    {
        public DropDownList ddlCampaignOHO = new DropDownList();
        public string AccountOHO = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string PostBack = getPostBackControlID();

            if (!IsPostBack)
            {
                ShowOnHoldOrder();
            }
            else
            {
                if (PostBack == "ddlCampaign" || PostBack == "txtAccountNumber" ||
                         PostBack == "imbSearchAcntNumber")
                    ShowOnHoldOrder();
            }
        }

        #region Sorting On Hold Order
        protected SortDirection GridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridOnHoldOrder_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridOrderHistory(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridOrderHistory(sortExpression, "ASC");
            }
        }

        private void SortGridOrderHistory(string sortExpression, string direction)
        {
            try
            {
                DataSet dsOnHold = new DataSet();
                DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
                TextBox txtTemp = Master.FindControl("txtAccountNumber") as TextBox;
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
         "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OnHoldOrder" + ".xml";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                dsOnHold.ReadXml(fsReadXml);
                fsReadXml.Close();

                if (dsOnHold != null &&
                    dsOnHold.Tables.Count > 0 &&
                    dsOnHold.Tables[0].Rows.Count > 0)
                {

                    DataView dv = new DataView(dsOnHold.Tables[0]);

                    dv.Sort = sortExpression + " " + direction;

                    gridOnHoldOrder.DataSource = dv;
                    gridOnHoldOrder.DataBind();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error During Sorting On Hold Order",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
        #endregion

        #region function
        public void ShowOnHoldOrder()
        {
            NewMasterPage MasterPage = Master as NewMasterPage;
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
         "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OnHoldOrder" + ".xml";
            DataSet dsOnHold = new DataSet();
            cOnHoldOrder objOnHoldOrder = new cOnHoldOrder();
            if (MasterPage != null)
            {
                AccountOHO = MasterPage.AccountNumberMaster.FormatAccountNumber();
                ddlCampaignOHO = MasterPage.CampaignMaster;
            }
            try
            {
                
                if (ddlCampaignOHO.SelectedValue == "DE" ||
                    ddlCampaignOHO.SelectedValue == "UK" ||
                    ddlCampaignOHO.SelectedValue == "FR")
                {
                    objOnHoldOrder.Account = AccountOHO;
                    objOnHoldOrder.Campaign = ddlCampaignOHO.SelectedValue;

                    dsOnHold = objOnHoldOrder.GetOrderOnHold();

                    if (dsOnHold != null && dsOnHold.Tables.Count > 0)
                    {
                        gridOnHoldOrder.DataSource = dsOnHold;
                        gridOnHoldOrder.DataBind();

                        //Writing XML All unit 
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        dsOnHold.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        Label2.Visible = true;
                        llblFilterByOnHoldOrder.Text = "Account Number";
                    }
                    else
                    {
                        gridOnHoldOrder.DataSource = null;
                        gridOnHoldOrder.DataBind();

                        Label2.Visible = false;
                        llblFilterByOnHoldOrder.Text = "";
                    }
                }
                else
                {
                    gridOnHoldOrder.DataSource = null;
                    gridOnHoldOrder.DataBind();

                    Label2.Visible = false;
                    llblFilterByOnHoldOrder.Text = "";
                }

            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogException(err, "ShowOnHoldOrder", ddlCampaignOHO.SelectedValue,
                    SessionFacade.LoggedInUserName, AccountOHO,
                    SessionFacade.KamId.ToString());
                LoggerHelper.LogMessage(err.ToString());
            }
        }

        private string getPostBackControlID()
        {
            Control control = null;
            //first we will check the "__EVENTTARGET" because if post back made by       the controls
            //which used "_doPostBack" function also available in Request.Form collection.
            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = Page.FindControl(ctrlname);
            }
            // if __EVENTTARGET is null, the control is a button type and we need to
            // iterate over the form collection to find it
            else
            {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in Page.Request.Form)
                {
                    //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                    //mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        ctrlStr = ctl.Substring(0, ctl.Length - 2);
                        c = Page.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = Page.FindControl(ctl);
                    }
                    if (c is System.Web.UI.WebControls.Button ||
                             c is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }

            if (control == null)
            {
                return null;
            }
            else
            {
                return control.ID;
            }
        }
        #endregion

        #region PageChanging
        protected void gridOnHoldOrderPageChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataSet dsOnHold = new DataSet();
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OnHoldOrder" + ".xml";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                dsOnHold.ReadXml(fsReadXml);
                fsReadXml.Close();

                if (dsOnHold != null &&
                   dsOnHold.Tables.Count > 0 &&
                   dsOnHold.Tables[0].Rows.Count > 0)
                {

                    gridOnHoldOrder.PageIndex = e.NewPageIndex;
                    gridOnHoldOrder.DataSource = dsOnHold;
                    gridOnHoldOrder.DataBind();
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error During Pagining OnHoldOrder",
                   SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                   SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }
        #endregion

        protected void btnShowAllOnHoldOrder_Click(object sender, EventArgs e)
        {
            try
            {
                NewMasterPage MasterPage = Master as NewMasterPage;
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OnHoldOrder" + ".xml";
                DataSet dsOrderOnHold = new DataSet();
                cOnHoldOrder objOnHoldOrder = new cOnHoldOrder();

                if (MasterPage != null)
                {
                    ddlCampaignOHO = MasterPage.CampaignMaster;
                }

                if (ddlCampaignOHO.SelectedValue == "DE" ||
                    ddlCampaignOHO.SelectedValue == "UK" ||
                    ddlCampaignOHO.SelectedValue == "FR")
                {
                    objOnHoldOrder.Campaign = ddlCampaignOHO.SelectedValue;
                    dsOrderOnHold = objOnHoldOrder.GetOrderOnHoldAll();

                    if (dsOrderOnHold != null &&
                        dsOrderOnHold.Tables.Count > 0)
                    {
                        gridOnHoldOrder.DataSource = dsOrderOnHold;
                        gridOnHoldOrder.DataBind();

                        //Writing XML All unit 
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        dsOrderOnHold.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        Label2.Visible = true;
                        llblFilterByOnHoldOrder.Text = "All";

                        MasterPage.AccountNumberMaster = "";
                        MasterPage.AccountNameMaster = "";
                    }
                    else
                    {
                        gridOnHoldOrder.DataSource = null;
                        gridOnHoldOrder.DataBind();

                        Label2.Visible = false;
                        llblFilterByOnHoldOrder.Text = "";
                        MasterPage.AccountNumberMaster = "";
                        MasterPage.AccountNameMaster = "";
                    }
                }
                else
                {
                    gridOnHoldOrder.DataSource = null;
                    gridOnHoldOrder.DataBind();

                    Label2.Visible = false;
                    llblFilterByOnHoldOrder.Text = "";
                    MasterPage.AccountNumberMaster = "";
                    MasterPage.AccountNameMaster = "";
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error:btnShowAllOnHoldOrder_Click",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

        protected void btnShowMyOnHoldOrder_Click(object sender, EventArgs e)
        {
            try
            {
                NewMasterPage MasterPage = Master as NewMasterPage;
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
         "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OnHoldOrder" + ".xml";
                DataSet dsOrderOnHold = new DataSet();
                cOnHoldOrder objOnHoldOrder = new cOnHoldOrder();

                if (MasterPage != null)
                {
                    ddlCampaignOHO = MasterPage.CampaignMaster;
                }

                if (ddlCampaignOHO.SelectedValue == "DE" ||
                    ddlCampaignOHO.SelectedValue == "UK" ||
                    ddlCampaignOHO.SelectedValue == "FR")
                {
                    objOnHoldOrder.Campaign = ddlCampaignOHO.SelectedValue;
                    objOnHoldOrder.Salesteam = SessionFacade.KamId;
                    dsOrderOnHold = objOnHoldOrder.GetOrderOnHoldByKamID();

                    if (dsOrderOnHold != null &&
                        dsOrderOnHold.Tables.Count > 0)
                    {
                        gridOnHoldOrder.DataSource = dsOrderOnHold;
                        gridOnHoldOrder.DataBind();

                        //Writing XML All unit 
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        dsOrderOnHold.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        Label2.Visible = true;
                        llblFilterByOnHoldOrder.Text = "Salesteam ID";

                        MasterPage.AccountNumberMaster = "";
                        MasterPage.AccountNameMaster = "";
                    }
                    else
                    {
                        gridOnHoldOrder.DataSource = null;
                        gridOnHoldOrder.DataBind();

                        Label2.Visible = false;
                        llblFilterByOnHoldOrder.Text = "";
                        MasterPage.AccountNumberMaster = "";
                        MasterPage.AccountNameMaster = "";
                    }
                }
                else
                {
                    gridOnHoldOrder.DataSource = null;
                    gridOnHoldOrder.DataBind();

                    Label2.Visible = false;
                    llblFilterByOnHoldOrder.Text = "";
                    MasterPage.AccountNumberMaster = "";
                    MasterPage.AccountNameMaster = "";
                }
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error:btnShowAllOnHoldOrder_Click",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
    }
}