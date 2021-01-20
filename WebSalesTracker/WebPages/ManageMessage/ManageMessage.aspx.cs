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
using System.Windows.Forms;

namespace WebSalesMine.WebPages.ManageMessage
{
    public partial class ManageMessage : System.Web.UI.Page
    {
        public string AddValidTo = "false";
        protected void Page_init(object sender, EventArgs e)
        {
            LoadCampaign();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["Refresh"] != null)
                {
                    ShowMessage();
                    HttpCookie myCookie = new HttpCookie("Refresh");
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookie);
                }
                //txtAddValidTo.Attributes.Add("readonly", "true");
                if (!IsPostBack)
                {
                    ShowMessage();
                }
                //txtAddValidTo.Visible = false;
                //lblAddValidTo.Visible = false;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Manage Message", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }

      
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dr = GetDatafromXMLDetails(txtAddState.Text, ddlAddCampaign.SelectedValue);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                            "Call Save", "Confirm()", true);
        }

        protected void ddlAddCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    SetCampaignInSession();
            //}
            //catch (Exception err)
            //{
            //    BradyCorp.Log.LoggerHelper.LogException(err, "Error During Campaing Change", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            //}

        }

        #region Function
        protected void ShowMessage()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
           "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageMessage" + ".xml";

            DataSet drMessage = new DataSet();
            cManageMessage objManageMessage = new cManageMessage();

            drMessage = objManageMessage.GetMessage();

            if (drMessage.Tables[0].Rows.Count > 0)
            {
                //Writing XML
                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                drMessage.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                grdMessage.DataSource = drMessage;
                grdMessage.DataBind();
            }
            else
            {
                grdMessage.DataSource = null;
                grdMessage.DataBind();
            }
        }

        //private void SetCampaignInSession()
        //{
        //    SessionFacade.CampaignName = ddlAddCampaign.SelectedItem.Value;
        //    SessionFacade.CampaignValue = ddlAddCampaign.SelectedItem.Value;
        //}

        //private void SetCampaignFromSession()
        //{
        //    if (!string.IsNullOrEmpty(SessionFacade.CampaignName))
        //    {
        //        ddlAddCampaign.ClearSelection();

        //        ddlAddCampaign.Items.FindByValue(SessionFacade.CampaignValue).Selected = true;
        //    }
        //}

        private void LoadCampaign()
        {

            try
            {
                ListItem L1 = new ListItem();

                cUser objGetCurrencyCode = new cUser();
                DataSet dsCode = new DataSet();

                //Get Currency Code
                dsCode = objGetCurrencyCode.AllGetCurrencyCode();

                if (dsCode != null && dsCode.Tables.Count > 0)
                {
                    L1 = new ListItem("", "");
                    ddlAddCampaign.Items.Insert(0, L1);

                    ddlAddCampaign.DataSource = dsCode;
                    ddlAddCampaign.DataTextField = "CampaignName";
                    ddlAddCampaign.DataValueField = "CampaignValue";
                    ddlAddCampaign.DataBind();
                }
            }

            catch (Exception ex)
            {

            }

        }

        [System.Web.Services.WebMethod]
        public static DataTable GetDatafromXMLDetails(string State, string Campaign)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
           "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageMessage" + ".xml";
            try
            {
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;
                DataSet ds = new DataSet();

                Query = "1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();

                    Query = Query + " and State = '" + State + "' and Campaign='" +
                        Campaign + "'";

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        public static void AddMessage(string State, string Message, string Validto,
            string Campaign)
        {
            DataSet drMessage = new DataSet();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
            "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageMessage" + ".xml";

            cManageMessage objManageMessage = new cManageMessage();

            objManageMessage.State = State.ToUpper();
            objManageMessage.Message = Message;
            objManageMessage.Username = SessionFacade.LoggedInUserName;
            objManageMessage.Date = Validto;
            objManageMessage.Campaign = Campaign;
            if (objManageMessage.AddStateMessage())
            {
                objManageMessage = new cManageMessage();

                drMessage = objManageMessage.GetMessage();

                if (drMessage.Tables.Count > 0)
                {
                    //Writing XML
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    drMessage.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();
                }
            }


        }

        [System.Web.Services.WebMethod]
        public static void Update(string State, string PreState, string Campaign, string PreCampaign
            ,string Message, string ValidTo)
        {

            DataSet drMessage = new DataSet();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
            "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ManageMessage" + ".xml";

            cManageMessage objManageMessage = new cManageMessage();

            objManageMessage.Campaign = Campaign;
            objManageMessage.State = State;
            objManageMessage.Message = Message;
            objManageMessage.Date = ValidTo;
            objManageMessage.Username = SessionFacade.LoggedInUserName;
            objManageMessage.PreState = PreState;
            objManageMessage.PreCampaign = PreCampaign;
            if (objManageMessage.EditStateMessage())
            {
                objManageMessage = new cManageMessage();

                drMessage = objManageMessage.GetMessage();

                if (drMessage.Tables.Count > 0)
                {
                    //Writing XML
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    drMessage.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();
                }
            }
        }
        #endregion

    }
}