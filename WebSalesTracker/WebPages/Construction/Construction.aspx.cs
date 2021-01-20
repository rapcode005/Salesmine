using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AppLogic;
using System.IO;
using BradyCorp.Log;

using AppLogic;
using System.ComponentModel;


namespace WebSalesMine.WebPages.Construction
{
    public partial class Construction : System.Web.UI.Page
    {
        //public delegate void BindDL();
        protected void Page_Load(object sender, EventArgs e)
        {

           // init();

            TextBox txtTemp = Master.FindControl("txtProjectID") as TextBox;
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            SessionFacade.CampaignName = ddlTemp.SelectedItem.Value;

            
         
            if (Request.Cookies["CProjID"] != null)
            {
                if (Request.Cookies["CProjID"].Value == "")
                {
                    SessionFacade.PROJECTID = txtTemp.Text.Trim();
                }
            }

            System.Web.HttpCookie cookie = new HttpCookie("CProjID");
            cookie.Value = SessionFacade.PROJECTID;
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);

            if (! IsPostBack)
            {
                loadReedTerritories();
            }

            if (IsPostBack)
            {
                if (SessionFacade.PROJECTID != "" )
                {
                    string ControlId = string.Empty;
                    ControlId = getPostBackControlID();
                    //if (ControlId == "ddlCampaign")
                    //{
                        
                    //}
                    if (ControlId == null || ControlId == "ddlCampaign" || ControlId == "imbSearchProjID" || ControlId == "txtProjectID")
                    {
                        //loadProjectInfo();
                        //loadGeneralContractor();                        
                        //loadSubContractor();
                        //LoadCallInfo();
                        GetConstructionDetails();
                       
                    }
                    else if (ControlId == "btnAddCon")
                    {
                        loadSubContractor();
                    }
                }

                else
                {
                    ClearControls();
                }

                
            }
            TexboxesState();

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        protected void GetConstructionDetails()
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            TextBox txtProjectID = Master.FindControl("txtProjectID") as TextBox;
            cConstruction objProjectInfo = new cConstruction();
            DataSet dsProjectInfo = null;
            DataSet ds = new DataSet();

            string varCalledNotes = "";
            string varCustomerYN = "";
            string varKAM = "";
         
            bool varProjectStat = false;


            if (Request.Cookies["CProjID"] != null)
            {

                objProjectInfo.varProjectID = SessionFacade.PROJECTID;
                txtProjectID.Text = SessionFacade.PROJECTID;
            }
            else
            {
                objProjectInfo.varProjectID = "";
            }

            try
            {
           

                dsProjectInfo = objProjectInfo.GetConstructionDetails();
                if (dsProjectInfo != null)
                {
                    if (dsProjectInfo.Tables.Count > 0 && dsProjectInfo.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsProjectInfo.Tables[0].Rows)
                        {

                            txtTitle.Text = dr["Title"].ToString();
                            txtStage.Text = dr["stage"].ToString();
                            txtCountry.Text = dr["Country"].ToString();
                            txtCity.Text = dr["City"].ToString();
                            TxtStateProv.Text = dr["State/Province"].ToString();
                            txtValue.Text = dr["Value"].ToString();
                            txtBidDate.Text = String.Format("{0:MM/dd/yyyy}", dr["Bid Date"]);
                            txtMatchingDoc.Text = dr["# of Matching Docs"].ToString();

                            varProjectStat = rdoActive.Checked;

                            if (dr["Status"].ToString() == "Active")
                            {
                                rdoActive.Checked = true;
                            }
                            else { rdoActive.Checked = false; }

                            if (dr["Status"].ToString() == "Inactive")
                            {
                                rdoInActive.Checked = true;
                            }
                            else { rdoInActive.Checked = false; }


                            if (dr["DoNotCall"].ToString() == "1")
                            {
                                chkDonotCall.Checked = true;
                            }
                            else { chkDonotCall.Checked = false; }

                            if (dr["Managed"].ToString() == "1")
                            {
                                chkKAMManaged.Checked = true;
                            }
                            else { chkKAMManaged.Checked = false; }

                        }
                    }

                    if (dsProjectInfo.Tables.Count > 1 && dsProjectInfo.Tables[1].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsProjectInfo.Tables[1].Rows)
                        {

                            SessionFacade.GeneralConID = dr["ID"].ToString();

                            if (dr["Name"].ToString().Trim() != "")
                            {
                                txtName.Text = dr["Name"].ToString().Trim();
                            }
                            else
                                txtName.Text = "";

                            if (dr["Phone"].ToString().Trim() != "")
                            {
                                txtPhoneNum.Text = dr["Phone"].ToString().Trim();
                            }
                            else
                                txtPhoneNum.Text = "";

                            if (dr["Email"].ToString().Trim() != "")
                            {
                                txtEmail.Text = dr["Email"].ToString().Trim();
                            }
                            else
                                txtEmail.Text = "";

                            if (dr["Account"].ToString().Trim() != "")
                            {
                                txtAccountNum.Text = dr["Account"].ToString().Trim();
                            }
                            else
                                txtAccountNum.Text = "";

                            if (dr["Username"].ToString().Trim() != "")
                            {
                                txtlastupdatedby.Text = dr["Username"].ToString().Trim();
                            }
                            else
                                txtlastupdatedby.Text = "";

                            if (dr["Valid_From"].ToString().Trim() != "")
                            {
                                txtLastUpdatedDate.Text = dr["Valid_From"].ToString().Trim();
                            }
                            else
                                txtLastUpdatedDate.Text = "";

                            //------------CmbCustomer------------------
                            varCustomerYN = cmbCustomer.Text;

                            if (dr["Customer"].ToString() == "0")
                            {
                                cmbCustomer.Text = "Yes";
                            }
                            else
                            {
                                cmbCustomer.Text = "No";
                            }


                            //--------------CmbKAM---------------
                            varKAM = cmbKAM.Text;

                            if (dr["KAM"].ToString() == "0")
                            {
                                cmbKAM.Text = "Yes";
                            }
                            else
                            {
                                cmbKAM.Text = "No";
                            }



                            //----------Called Notes--------------------
                            varCalledNotes = cmbCalled.Text;

                            if (dr["calledNotes"].ToString() == "1")
                            {
                                cmbCalled.Text = "Yes";
                            }
                            else if (dr["calledNotes"].ToString() == "2")
                            {
                                cmbCalled.Text = "No";
                            }
                            else
                            {
                                cmbCalled.Text = "";
                            }


                        }
                    }

                    if (dsProjectInfo.Tables.Count > 2 && dsProjectInfo.Tables[2].Rows.Count > 0)
                    {
                        gwSubCon.DataSource = null;

                        gwSubCon.DataSource = dsProjectInfo.Tables[2];
                        gwSubCon.DataBind();


                        hdnSubConGWCount.Value = dsProjectInfo.Tables[2].Rows.Count.ToString();


                        if (dsProjectInfo.Tables[2].Rows.Count > 0)
                        {
                            txtCompanyName.Text = string.Empty;
                            txtTitle2.Text = string.Empty;
                            txtName2.Text = string.Empty;
                            txtPhone2.Text = string.Empty;
                            txtEmail2.Text = string.Empty;
                            txtAccountNum2.Text = string.Empty;
                            txtNotes2.Text = string.Empty;
                            dlCustomerYN.SelectedIndex = 0;
                            dlKANYN.SelectedIndex = 0;
                        }
                    }



                    if (dsProjectInfo.Tables.Count > 3 && dsProjectInfo.Tables[3].Rows.Count > 0)
                    {
                        txtCallNotes.Text = dsProjectInfo.Tables[3].Rows[0]["CallNotes"].ToString();
                        txtNextFollowupContact.Text = dsProjectInfo.Tables[3].Rows[0]["NextFollowupContact"].ToString();
                        txtFollowupDate.Text = dsProjectInfo.Tables[3].Rows[0]["NextFollowupDate"].ToString();
                        txtNumCall.Text = dsProjectInfo.Tables[3].Rows[0]["NumberOfCall"].ToString();
                        txtlastUpdatedBy3.Text = dsProjectInfo.Tables[3].Rows[0]["Username"].ToString();
                        txtLastUpdatedDate3.Text = dsProjectInfo.Tables[3].Rows[0]["Valid_from"].ToString();
                        txtfollowupNotes.Text = dsProjectInfo.Tables[3].Rows[0]["NextFollowupNotes"].ToString();
                        hdnCallInfo.Value = dsProjectInfo.Tables[3].Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        txtCallNotes.Text = string.Empty;
                        txtNextFollowupContact.Text = string.Empty;
                        txtFollowupDate.Text = string.Empty;
                        txtNumCall.Text = string.Empty;
                        txtlastUpdatedBy3.Text = string.Empty;
                        txtLastUpdatedDate3.Text = string.Empty;
                        txtfollowupNotes.Text = string.Empty;
                        hdnCallInfo.Value = string.Empty;
                    }

                }

            }
            catch (Exception errr)
            {

                BradyCorp.Log.LoggerHelper.LogException(errr, "Construction Page - Load Project Info Data-Construction-IsNullOrEmpty", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
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


        private void TexboxesState()
        {

            txtCompanyName.Enabled = false;
            txtTitle2.Enabled = false;
            txtName2.Enabled = false;
            txtPhone2.Enabled = false;
            txtEmail2.Enabled = false;
            txtNotes2.Enabled = false;
            dlCustomerYN.Enabled = false;
            dlKANYN.Enabled = false;
            txtAccountNum2.Enabled = false;


            //General Con Buttons
            btnGenCancel.Enabled = false;
            btnGenSave.Enabled = false;
            txtName.Enabled = false;
            txtPhoneNum.Enabled = false;
            txtEmail.Enabled = false;
            txtAccountNum.Enabled = false;
            cmbCustomer.Enabled = false;
            cmbKAM.Enabled = false;
            cmbCalled.Enabled = false;

            //Call Info Buttons
            btnCallCancel.Enabled = false;
            btnCallSave.Enabled = false;
        }
        private void ClearControls()
        {

            //SubCon
            txtCompanyName.Text = string.Empty;
            txtTitle2.Text = string.Empty;
            txtName2.Text = string.Empty;
            txtPhone2.Text = string.Empty;
            txtEmail2.Text = string.Empty;
            txtNotes2.Text = string.Empty;
            dlCustomerYN.SelectedIndex = 0;
            dlKANYN.SelectedIndex = 0;           
            txtAccountNum2.Text = string.Empty;
            lblLastUpdatedBy2.Text = string.Empty;
            lblLastUpdatedDate2.Text = string.Empty;

            //Call Notes

            txtCallNotes.Text = string.Empty;
            txtfollowupNotes.Text = string.Empty;
            txtNextFollowupContact.Text = string.Empty;
            txtFollowupDate.Text = string.Empty;
            txtNumCall.Text = string.Empty;
            txtlastUpdatedBy3.Text = string.Empty;
            txtLastUpdatedDate3.Text = string.Empty;

            //General Con
            txtName.Text = string.Empty;
            txtPhoneNum.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAccountNum.Text = string.Empty;
            cmbCustomer.Text = string.Empty;
            cmbKAM.Text = string.Empty;
            cmbCalled.Text = string.Empty;
            txtlastupdatedby.Text = string.Empty;
            txtlastupdatedby.Text = string.Empty;
            txtLastUpdatedDate.Text = string.Empty;

            //Project Info
            txtTitle.Text = string.Empty;
            txtStage.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtCity.Text = string.Empty;
            TxtStateProv.Text = string.Empty;
            txtValue.Text = string.Empty;
            txtBidDate.Text = string.Empty;
            txtMatchingDoc.Text = string.Empty;
            rdoActive.Checked = false;
            rdoInActive.Checked = false;
            chkDonotCall.Checked = false;
            chkKAMManaged.Checked = false;

            gwSubCon.DataSource = null;
            gwSubCon.DataBind();

        }


        private void init()
        {
            gwSubCon.DataSource = null;
            gwSubCon.DataBind();

            gwReedTerritories.DataSource = null;
            gwReedTerritories.DataBind();

        }

        #region LoadCallInfo

        private void LoadCallInfo()
        {
            cConstruction objSubContractor = new cConstruction();
            DataSet dsCallNotes = null;

            TextBox txtProjectID = Master.FindControl("txtProjectID") as TextBox;
            if (Request.Cookies["CProjID"] != null)
            {

                objSubContractor.varProjectID = SessionFacade.PROJECTID;
            }
            else if (txtProjectID.Text.Trim() != "")
            {
                SessionFacade.PROJECTID = txtProjectID.Text.Trim();
                objSubContractor.varProjectID = txtProjectID.Text.Trim();
            }
            else
            {
                objSubContractor.varProjectID = "";
            }

            try
            {
             
                dsCallNotes = objSubContractor.GetCallNotes();

                if (dsCallNotes.Tables.Count > 0 && dsCallNotes.Tables[0].Rows.Count > 0)
                {
                    txtCallNotes.Text = dsCallNotes.Tables[0].Rows[0]["CallNotes"].ToString();
                    txtNextFollowupContact.Text = dsCallNotes.Tables[0].Rows[0]["NextFollowupContact"].ToString();
                    txtFollowupDate.Text = dsCallNotes.Tables[0].Rows[0]["NextFollowupDate"].ToString();
                    txtNumCall.Text = dsCallNotes.Tables[0].Rows[0]["NumberOfCall"].ToString();
                    txtlastUpdatedBy3.Text = dsCallNotes.Tables[0].Rows[0]["Username"].ToString();
                    txtLastUpdatedDate3.Text = dsCallNotes.Tables[0].Rows[0]["Valid_from"].ToString();
                    txtfollowupNotes.Text = dsCallNotes.Tables[0].Rows[0]["NextFollowupNotes"].ToString();
                    hdnCallInfo.Value = dsCallNotes.Tables[0].Rows[0]["ID"].ToString();
                }
                else
                {
                    txtCallNotes.Text = string.Empty;
                    txtNextFollowupContact.Text = string.Empty;
                    txtFollowupDate.Text = string.Empty;
                    txtNumCall.Text = string.Empty;
                    txtlastUpdatedBy3.Text = string.Empty;
                    txtLastUpdatedDate3.Text = string.Empty;
                    txtfollowupNotes.Text = string.Empty;
                    hdnCallInfo.Value = string.Empty;
                }
               

            }
            catch (Exception errr)
            {                
                BradyCorp.Log.LoggerHelper.LogException(errr, "CallNotes Page - Load CallNotes Data-Construction-IsNullOrEmpty", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }
        }
        #endregion


        #region loadGeneralContractor

        private void loadGeneralContractor()
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            cConstruction objGeneralContractor = new cConstruction();
            DataSet dsGeneralContractor = null;
            DataSet ds = new DataSet();
            string varCalledNotes = "";
            string varCustomerYN = "";
            string varKAM = "";

            //string varName = "";
            //string varPhone = "";
            //string varEmail = "";
            //string varAccountNum = "";
            //string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-GeneralContractor" + ".xml";

            //General Con
            txtName.Text = string.Empty;
            txtPhoneNum.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAccountNum.Text = string.Empty;
            cmbCustomer.Text = string.Empty;
            cmbKAM.Text = string.Empty;
            cmbCalled.Text = string.Empty;
            txtlastupdatedby.Text = string.Empty;
            txtlastupdatedby.Text = string.Empty;
            txtLastUpdatedDate.Text = string.Empty;

            TextBox txtProjectID = Master.FindControl("txtProjectID") as TextBox;
            if (Request.Cookies["CProjID"] != null)
            {

                objGeneralContractor.varProjectID = SessionFacade.PROJECTID;
            }
            else if (txtProjectID.Text.Trim() != "")
            {
                SessionFacade.PROJECTID = txtProjectID.Text.Trim();
                objGeneralContractor.varProjectID = txtProjectID.Text.Trim();
            }
            else
            {
                objGeneralContractor.varProjectID = "";
            }

            try
            {

                dsGeneralContractor = objGeneralContractor.GetGeneralContractor();

                //System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                //dsGeneralContractor.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                //xmlSW.Close();


                //System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                //ds.ReadXml(fsReadXml);
                //fsReadXml.Close();

                if (dsGeneralContractor.Tables.Count > 0)
                {
                    foreach (DataRow dr in dsGeneralContractor.Tables[0].Rows)
                    {

                        SessionFacade.GeneralConID = dr["ID"].ToString();

                        if (dr["Name"].ToString().Trim() != "")
                        {
                            txtName.Text = dr["Name"].ToString().Trim();
                        }

                        if (dr["Phone"].ToString().Trim() != "")
                        {
                            txtPhoneNum.Text = dr["Phone"].ToString().Trim();
                        }

                        if (dr["Email"].ToString().Trim() != "")
                        {
                            txtEmail.Text = dr["Email"].ToString().Trim();
                        }

                        if (dr["Account"].ToString().Trim()!="")
                        {
                            txtAccountNum.Text = dr["Account"].ToString().Trim();
                        }

                        if (dr["Username"].ToString().Trim()!="")
                        {
                            txtlastupdatedby.Text= dr["Username"].ToString().Trim();
                        }

                        if (dr["Valid_From"].ToString().Trim() != "")
                        {
                            txtLastUpdatedDate.Text = dr["Valid_From"].ToString().Trim();
                        }


                        //varName = txtName.Text;                      
                        //txtName.Text = dr["Name"].ToString();
                        //if (varName != txtName.Text)
                        //{
                        //    txtName.Text = varName;
                        //}

                        //varPhone = txtPhoneNum.Text;
                        //txtPhoneNum.Text = dr["Phone"].ToString();
                        //if (varPhone != txtPhoneNum.Text)
                        //{
                        //    txtPhoneNum.Text = varPhone;
                        //}

                        //varEmail = txtEmail.Text;
                        //txtEmail.Text = dr["Email"].ToString();
                        //if(varEmail !=txtEmail.Text)
                        //{
                        //    txtEmail.Text = varEmail;
                        //}

                        //varAccountNum = txtAccountNum.Text;
                        //txtAccountNum.Text = dr["Account"].ToString();
                        //if(varAccountNum != txtAccountNum.Text)
                        //{
                        //    txtAccountNum.Text = varAccountNum;
                        //}

                        //------------CmbCustomer------------------
                        varCustomerYN = cmbCustomer.Text;

                        if (dr["Customer"].ToString() == "0")
                        {
                            cmbCustomer.Text = "Yes";
                        }
                        else
                        {
                            cmbCustomer.Text = "No";
                        }

                        //if (varCustomerYN != cmbCustomer.Text)
                        //{
                        //    cmbCustomer.Text = varCustomerYN;
                        //}
                        //---------------------------------

                        //--------------CmbKAM---------------
                        varKAM = cmbKAM.Text;
                        
                        if (dr["KAM"].ToString() == "0")
                        {
                            cmbKAM.Text = "Yes";
                        }
                        else
                        {
                            cmbKAM.Text = "No";
                        }

                        //if (varKAM != cmbKAM.Text)
                        //{
                        //    cmbKAM.Text = varKAM;
                        //}
                        //----------------------------------

                        //----------Called Notes--------------------
                        varCalledNotes = cmbCalled.Text;
  
                        if (dr["calledNotes"].ToString() == "1")
                        {
                            cmbCalled.Text = "Yes";
                        }
                        else if (dr["calledNotes"].ToString() == "2")
                        {
                            cmbCalled.Text = "No";
                        }
                        else
                        {
                            cmbCalled.Text = "";
                        }


                        //if (varCalledNotes != cmbCalled.Text)
                        //{
                        //    cmbCalled.Text = varCalledNotes;
                        //}
                        //---------------------------------------------------

                        ////GeneralContractor Variables
                        //objGeneralContractor.varContractorName = txtName.Text.Trim();
                        //objGeneralContractor.varContractorEmail = txtEmail.Text.Trim();
                        //objGeneralContractor.varContractorPhone = txtPhoneNum.Text.Trim();

                        //objGeneralContractor.varContractorAccount = txtAccountNum.Text.Trim();

                        //if (cmbCustomer.Text == "Yes")
                        //{
                        //    objGeneralContractor.varContractorCustomer = 0;
                        //}
                        //else
                        //{
                        //    objGeneralContractor.varContractorCustomer = 1;
                        //}

                        //if (cmbKAM.Text == "Yes")
                        //{
                        //    objGeneralContractor.varKAM = 0;
                        //}
                        //else
                        //{
                        //    objGeneralContractor.varKAM = 1;
                        //}

                        //if (cmbCalled.Text == "Yes")
                        //{
                        //    objGeneralContractor.varCalledNotes = 1;
                        //}
                        //else if (cmbCalled.Text == "No")
                        //{
                        //    objGeneralContractor.varCalledNotes = 2;
                        //}

                        //else if (cmbCalled.Text == "")
                        //{
                        //    objGeneralContractor.varCalledNotes = 0;
                        //}
                    }
                }

            }
            catch (Exception errr)
            {

                BradyCorp.Log.LoggerHelper.LogException(errr, "Construction Page - Load GeneralContractor Data-Construction-IsNullOrEmpty", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        #endregion

        #region loadProjectInfo

        private void loadProjectInfo()
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            TextBox txtProjectID = Master.FindControl("txtProjectID") as TextBox;
            cConstruction objProjectInfo = new cConstruction();
            DataSet dsProjectInfo = null;
            DataSet ds = new DataSet();
           // string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ProjectInfo" + ".xml";
            bool varProjectStat = false;


            if (Request.Cookies["CProjID"] != null)
            {

                objProjectInfo.varProjectID = SessionFacade.PROJECTID;
                txtProjectID.Text = SessionFacade.PROJECTID; 
            }
            else
            {
                objProjectInfo.varProjectID = "";
            }

            try
            {
                //DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                //dc.AutoIncrement = true;
                //dc.AutoIncrementSeed = 1;
                //dc.AutoIncrementStep = 1;

                dsProjectInfo = objProjectInfo.GetProjectInfo();

               // dsProjectInfo.Tables[0].Columns.Add(dc);

                foreach (DataRow dr in dsProjectInfo.Tables[0].Rows)
                {
                    
                    txtTitle.Text = dr["Title"].ToString();
                    txtStage.Text = dr["stage"].ToString();
                    txtCountry.Text = dr["Country"].ToString();
                    txtCity.Text = dr["City"].ToString();
                    TxtStateProv.Text = dr["State/Province"].ToString();
                    txtValue.Text = dr["Value"].ToString();
                    txtBidDate.Text = String.Format("{0:MM/dd/yyyy}", dr["Bid Date"]);
                    txtMatchingDoc.Text = dr["# of Matching Docs"].ToString();

                    varProjectStat = rdoActive.Checked;

                    if (dr["Status"].ToString() == "Active")
                    {
                        rdoActive.Checked = true;
                    }
                    else { rdoActive.Checked = false; }

                    if (dr["Status"].ToString() == "Inactive")
                    {
                        rdoInActive.Checked = true;
                    }
                    else { rdoInActive.Checked = false; }


                    //if (varProjectStat != rdoActive.Checked)
                    //{
                    //    rdoActive.Checked = varProjectStat;
                    //}

                    if (dr["DoNotCall"].ToString() == "1")
                    {
                        chkDonotCall.Checked = true;
                    }
                    else {chkDonotCall.Checked = false;}

                    if (dr["Managed"].ToString() == "1")
                    {
                        chkKAMManaged.Checked = true;
                    }
                    else { chkKAMManaged.Checked = false; }

                }

                //System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                //dsProjectInfo.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                //xmlSW.Close();


            }
            catch (Exception errr)
            {

                BradyCorp.Log.LoggerHelper.LogException(errr, "Construction Page - Load Project Info Data-Construction-IsNullOrEmpty", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        #endregion

        #region loadSubContractor
        private void loadSubContractor()
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            cConstruction objSubContractor = new cConstruction();
            DataSet dsSubContractor = null;
           // string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SubContractor" + ".xml";

       
            TextBox txtProjectID = Master.FindControl("txtProjectID") as TextBox;
            if (Request.Cookies["CProjID"] != null)
            {

                objSubContractor.varProjectID = SessionFacade.PROJECTID;
            }
            else if (txtProjectID.Text.Trim() != "")
            {
                SessionFacade.PROJECTID = txtProjectID.Text.Trim();
                objSubContractor.varProjectID = txtProjectID.Text.Trim();
            }
            else
            {
                objSubContractor.varProjectID = "";
            }

            try
            {
                //DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                //dc.AutoIncrement = true;
                //dc.AutoIncrementSeed = 1;
                //dc.AutoIncrementStep = 1;

                dsSubContractor = objSubContractor.GetSubContractor();

                //dsSubContractor.Tables[0].Columns.CollectionChanged += new CollectionChangeEventHandler(Columns_CollectionChanged);

                //dsSubContractor.Tables[0].Columns.Add(dc);

                //System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                //dsSubContractor.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                //xmlSW.Close();

                gwSubCon.DataSource = null;
                //gwSubCon.DataSource = GetXMLSubCon();
                gwSubCon.DataSource = dsSubContractor.Tables[0];
                gwSubCon.DataBind();


               // hdnSubConGWCount.Value = GetXMLSubCon().Tables[0].Rows.Count.ToString();
                hdnSubConGWCount.Value = dsSubContractor.Tables[0].Rows.Count.ToString();

                ////if ( GetXMLSubCon().Tables.Count == 0)
                if (dsSubContractor.Tables.Count == 0)
                {
                    txtCompanyName.Text = string.Empty;
                    txtTitle2.Text = string.Empty;
                    txtName2.Text = string.Empty;
                    txtPhone2.Text = string.Empty;
                    txtEmail2.Text = string.Empty;
                    txtAccountNum2.Text = string.Empty;
                    txtNotes2.Text = string.Empty;
                    dlCustomerYN.SelectedIndex = 0;
                    dlKANYN.SelectedIndex = 0;
                }

            }
            catch (Exception errr)
            {
                //lblErrorProductLineSummary.Text = "Error in Loading Product Summary table";
                BradyCorp.Log.LoggerHelper.LogException(errr, "SubContractor Page - Load SubContractor Data-Construction-IsNullOrEmpty", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        #endregion

        #region loadReedTerritories

        private void loadReedTerritories()
        {
            DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
            cConstruction objReedTerritories = new cConstruction();
             DataSet dsReedTerritories = null;
            //string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ReedTerritories" + ".xml";
            try
            {
                //DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                //dc.AutoIncrement = true;
                //dc.AutoIncrementSeed = 1;
                //dc.AutoIncrementStep = 1;

                dsReedTerritories = GetXMLReed();

                //dsReedTerritories.Tables[0].Columns.Add(dc);

                //System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                //dsReedTerritories.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                //xmlSW.Close();

                if (dsReedTerritories.Tables.Count > 0 && dsReedTerritories.Tables[0].Rows.Count > 0)
                {
                    gwReedTerritories.DataSource = dsReedTerritories.Tables[0];
                    gwReedTerritories.DataBind();
                }
                else
                {
                    gwReedTerritories.DataSource = null;
                    gwReedTerritories.DataBind();
                }

                if (gwReedTerritories.Rows.Count > 0)
                {

                    gwReedTerritories.UseAccessibleHeader = true;
                    gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

                }

            }
            catch (Exception errr)
            {
                //lblErrorProductLineSummary.Text = "Error in Loading Product Summary table";
                BradyCorp.Log.LoggerHelper.LogException(errr, "Construction Page - Load Construction Data-Construction-IsNullOrEmpty", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            }

        }

        #endregion

        //#region GetDatafromXMLCallNotes

        ////private void loadCallNotes()
        //[System.Web.Services.WebMethod]
        //public static DataTable GetDatafromXMLCallNotes(string varProjectID)
        //{
        //    //DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
        //    cConstruction objSubContractor = new cConstruction();
        //    DataSet dsCallNotes = null;
        //    DataSet ds = new DataSet();


        //    DataRow[] results;
        //    DataTable dtTemp = new DataTable();
        //    string Query;

        //    Query = " 1=1";

        //    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-CallNotes" + ".xml";

        //    if (SessionFacade.PROJECTID != null)
        //    {
        //        objSubContractor.varProjectID = SessionFacade.PROJECTID;
        //    }
        //    else
        //    {
        //        objSubContractor.varProjectID = "";
        //    }

        //    try
        //    {
        //        DataColumn dc = new DataColumn("Row", typeof(System.Int32));
        //        dc.AutoIncrement = true;
        //        dc.AutoIncrementSeed = 1;
        //        dc.AutoIncrementStep = 1;

        //        dsCallNotes = objSubContractor.GetCallNotes();
                
        //        dsCallNotes.Tables[0].Columns.Add(dc);

        //        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
        //        dsCallNotes.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //        xmlSW.Close();

        //        //System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
        //        //ds.ReadXml(fsReadXml);
        //        //fsReadXml.Close();



        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtTemp = ds.Tables[0].Clone();

        //            //Search by Date

        //            if (SessionFacade.PROJECTID != null)
        //            {

        //                Query = Query + " and [Project ID] = '" + SessionFacade.PROJECTID + "'";

        //            }

        //            results = ds.Tables[0].Select(Query);

        //            foreach (DataRow dr in results)
        //                dtTemp.ImportRow(dr);
        //        }
        //        else
        //            dtTemp = null;

        //        return dtTemp;
        //    }
        //    catch (Exception errr)
        //    {
        //        return null;
        //        //lblErrorProductLineSummary.Text = "Error in Loading Product Summary table";
        //        BradyCorp.Log.LoggerHelper.LogException(errr, "CallNotes Page - Load CallNotes Data-Construction-IsNullOrEmpty", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
        //    }

        //}

        //#endregion

        //#region GetDatafromXMLGeneralCon
        
        //[System.Web.Services.WebMethod]
        //public static DataTable GetDatafromXMLGeneralCon(string varProjectID)
        //{
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-GeneralContractor" + ".xml";
        //        DataRow[] results;
        //        DataTable dtTemp = new DataTable();
        //        string Query;

        //        Query = " 1=1";

        //        //Reading XML
        //        System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
        //        ds.ReadXml(fsReadXml);
        //        fsReadXml.Close();

        //        //To Copy the Schema.
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtTemp = ds.Tables[0].Clone();

        //            //Search by Date


        //            if (SessionFacade.PROJECTID != null)
        //            {

        //                Query = Query + " and [Project ID] = '" + SessionFacade.PROJECTID + "'";

        //            }

        //            results = ds.Tables[0].Select(Query);

        //            foreach (DataRow dr in results)
        //                dtTemp.ImportRow(dr);
        //        }
        //        else
        //            dtTemp = null;

        //        return dtTemp;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //#endregion

        //#region GetDatafromXMLSubCon

        //[System.Web.Services.WebMethod]
        //public static DataTable GetDatafromXMLSubCon(string RowID)
        //{
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SubContractor" + ".xml";
        //        DataRow[] results;
        //        DataTable dtTemp = new DataTable();
        //        string Query;

        //        Query = " 1=1";

        //        //Reading XML
        //        System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
        //        ds.ReadXml(fsReadXml);
        //        fsReadXml.Close();

        //        //To Copy the Schema.
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtTemp = ds.Tables[0].Clone();

        //            //Search by Date


        //            if (SessionFacade.PROJECTID != null)
        //            {

        //                Query = Query + " and [Row] = '" + RowID + "'";

        //            }

        //            results = ds.Tables[0].Select(Query);

        //            foreach (DataRow dr in results)
        //                dtTemp.ImportRow(dr);
        //        }
        //        else
        //            dtTemp = null;

        //        return dtTemp;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //#endregion

        //#region GetDatafromXMLProjectInfo
        //[System.Web.Services.WebMethod]
        //public static DataTable GetDatafromXMLProjectInfo(string ProjectID)
        //{
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ProjectInfo" + ".xml";
        //        DataRow[] results;
        //        DataTable dtTemp = new DataTable();
        //        string Query;
        //        DataSet dsProjectInfo = null;
        //        cConstruction objProjectInfo = new cConstruction();
        //        Query = " 1=1";

        //        //SessionFacade.PROJECTID = ProjectID;
        //        objProjectInfo.varProjectID = SessionFacade.PROJECTID;


        //        DataColumn dc = new DataColumn("Row", typeof(System.Int32));
        //        dc.AutoIncrement = true;
        //        dc.AutoIncrementSeed = 1;
        //        dc.AutoIncrementStep = 1;

        //        dsProjectInfo = objProjectInfo.GetProjectInfo();



        //        dsProjectInfo.Tables[0].Columns.Add(dc);

        //        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
        //        dsProjectInfo.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //        xmlSW.Close();


        //        //Reading XML
        //        System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
        //        ds.ReadXml(fsReadXml);
        //        fsReadXml.Close();

        //        //To Copy the Schema.

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtTemp = ds.Tables[0].Clone();

        //            //Search by Date


        //            if (SessionFacade.PROJECTID != null)
        //            {

        //                Query = Query + " and [Project ID] = '" + ProjectID + "'";

        //            }

        //            results = ds.Tables[0].Select(Query);

        //            foreach (DataRow dr in results)
        //                dtTemp.ImportRow(dr);
        //        }
        //        else
        //            dtTemp = null;

        //        return dtTemp;

        //    }


        //    catch
        //    {
        //        return null;
        //    }
        //}

        //#endregion

        #region GetXMLReed
        public DataSet GetXMLReed()
        {
            DataSet dsReed = new DataSet();
            DataTable dtTemp = new DataTable();
            string Query;
            DataRow[] results;
            Query = " 1=1";
            cConstruction objReedTerritories = new cConstruction();
            DataSet dsReedTerritories = new DataSet();
            try
            {

                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ReedTerritories" + ".xml";


                //DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                //dc.AutoIncrement = true;
                //dc.AutoIncrementSeed = 1;
                //dc.AutoIncrementStep = 1;

                

                if (rbActive.Checked == true)
                {
                    objReedTerritories.iStatus = 0; 
                    //Query = Query + " and [Status] = 'ACTIVE'";
                }
                if (rbInActive.Checked == true)
                {
                    objReedTerritories.iStatus = 1;
                    //Query = Query + " and [Status] = 'INACTIVE'";
                }
                if (txtFilter.Text != "" && rbActive.Checked == true && rdoFilterTitle.Checked == true)
                {
                    objReedTerritories.iStatus = 0;
                    objReedTerritories.VarTitle = txtFilter.Text.Trim();
                    //Query = Query + " and [Status] = 'ACTIVE' and [Title] like %" + txtFilter.Text.Trim() + "%";
                }
                else if (rbActiveInActive.Checked == true)
                {
                    objReedTerritories.iStatus = 2;                   
                    //Query = Query + " and [Status] = 'ACTIVE' and [Title] like %" + txtFilter.Text.Trim() + "%";
                }
               

                dsReedTerritories = objReedTerritories.GetReedTerritories();

                //dsReedTerritories.Tables[0].Columns.CollectionChanged += new CollectionChangeEventHandler(Columns_CollectionChanged);
                //dsReedTerritories.Tables[0].Columns.Add(dc);

               
                //dtTemp = dsReedTerritories.Tables[0].Clone();
                //dtTemp.Columns.Add(dc);

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                dsReedTerritories.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();


                //System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                //dsReed.ReadXml(fsReadXml);
                //fsReadXml.Close();

                //if (dsReedTerritories.Tables.Count > 0)
                //{
                    //if (rbActive.Checked == true)
                    //{
                    //    Query = Query + " and [Status] = 'ACTIVE'";
                    //}
                    //if (rbInActive.Checked == true)
                    //{
                    //    Query = Query + " and [Status] = 'INACTIVE'";
                    //}
                    //if (txtFilter.Text != "" && rbActive.Checked == true && rdoFilterTitle.Checked == true)
                    //{
                    //    Query = Query + " and [Status] = 'ACTIVE' and [Title] like %" + txtFilter.Text.Trim() + "%";
                    //}
                //}

                //results = dsReed.Tables[0].Select(Query);

                //foreach (DataRow dr in results)
                //    dtTemp.ImportRow(dr);

                //dsReed.Tables.Clear();
                //dsReed.Tables.Add(dtTemp);



                if (gwReedTerritories.Rows.Count > 0)
                {

                    gwReedTerritories.UseAccessibleHeader = true;
                    gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                return dsReedTerritories;
                // return dtTemp;
            }

            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Construction Page - Construction -grdReedTerritoriesDataPageEventHandler()", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return null;
            }
        }

        #endregion

        protected void gwReedTerritories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ReedTerritories" + ".xml";
            DataSet dsReedTerritories = new DataSet();
            if (System.IO.File.Exists(Pathname))
            {
                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                dsReedTerritories.ReadXml(fsReadXml, XmlReadMode.Auto);
                fsReadXml.Close();

                gwReedTerritories.DataSource = dsReedTerritories;
                gwReedTerritories.PageIndex = e.NewPageIndex;
                gwReedTerritories.DataBind();
            }


        }

        protected void gwReedTerritories_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortgwReedTerritories(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortgwReedTerritories(sortExpression, "ASC");
            }
        }

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

        private void SortgwReedTerritories(string sortExpression, string direction)
        {
            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ReedTerritories" + ".xml";
                DataSet ds = new DataSet();
                DataTable dtAllColumns = new DataTable();
                DataView dv = new DataView();
                DataSet dsReedTerritories = new DataSet();
                if (System.IO.File.Exists(Pathname))
                {
                    //Reading XML
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    dsReedTerritories.ReadXml(fsReadXml,XmlReadMode.Auto);
                    fsReadXml.Close();

                    if (dsReedTerritories.Tables[0].Rows.Count > 0)
                    {
                        dv = new DataView(dsReedTerritories.Tables[0]);
                        dv.Sort = sortExpression + " " + direction;
                        ds.Tables.Add(dv.ToTable());

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            gwReedTerritories.DataSource = ds;
                            gwReedTerritories.DataBind();
                        }
                        else
                        {
                            gwReedTerritories.DataSource = null;
                            gwReedTerritories.DataBind();
                        }

                    }


                }

            }
            catch (Exception ex)
            {
            }
        }

        #region GetXMLSubCon
        public DataSet GetXMLSubCon()
        {
            DataSet dsSubCon = new DataSet();
            DataTable dtTemp = new DataTable();
            string Query;
            DataRow[] results;
            Query = " 1=1";
            try
            {

                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SubContractor" + ".xml";

                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                dsSubCon.ReadXml(fsReadXml);
                fsReadXml.Close();

                dtTemp = dsSubCon.Tables[0].Clone();
                results = dsSubCon.Tables[0].Select(Query);

                foreach (DataRow dr in results)
                    dtTemp.ImportRow(dr);

                dsSubCon.Tables.Clear();
                dsSubCon.Tables.Add(dtTemp);


                return dsSubCon;
                // return dtTemp;
            }

            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Construction Page - Construction -grdReedTerritoriesDataPageEventHandler()", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return null;
            }
        }
        #endregion

        //#region LoadGenConst
        
        //public void LoadGenConst()
        //{
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-GeneralContractor" + ".xml";
        //        DataRow[] results;
        //        DataTable dtTemp = new DataTable();
        //        string Query;

        //        Query = " 1=1";

        //        Reading XML
        //        System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
        //        ds.ReadXml(fsReadXml);
        //        fsReadXml.Close();

        //        To Copy the Schema.
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtTemp = ds.Tables[0].Clone();

        //            Search by Date


        //            if (SessionFacade.PROJECTID != null)
        //            {

        //                Query = Query + " and [Project ID] = '" + SessionFacade.PROJECTID + "'";

        //            }

        //            results = ds.Tables[0].Select(Query);

        //            foreach (DataRow dr in results)
        //                dtTemp.ImportRow(dr);

        //            foreach (DataRow row in dtTemp.Rows)
        //            {

        //                txtName.Text = row["Name"].ToString();
        //                txtPhoneNum.Text = row["Phone"].ToString();
        //                txtEmail.Text = row["Email"].ToString();
        //                txtAccountNum.Text = row["Account"].ToString();

        //                cmbCustomer.Text = row["Customer"].ToString();


        //                if (row["Customer"].ToString() == "0")
        //                {
        //                    cmbCustomer.Text = "Yes";
        //                }
        //                else if (row["Customer"].ToString() == "1")
        //                {

        //                    cmbCustomer.Text = "No";
        //                }
        //                else if (row["Customer"].ToString() == "2")
        //                {
        //                    cmbCustomer.Text = "";
        //                }
        //                else
        //                {
        //                    cmbCustomer.Text = "";
        //                }

        //                if (row["KAM"].ToString() == "0")
        //                {
        //                    cmbKAM.Text = "Yes";
        //                }
        //                else if (row["KAM"].ToString() == "1")
        //                {

        //                    cmbKAM.Text = "No";
        //                }
        //                else if (row["KAM"].ToString() == "2")
        //                {
        //                    cmbKAM.Text = "Yes";
        //                }
        //                else
        //                {
        //                    cmbKAM.Text = "";
        //                }

        //                if (row["calledNotes"].ToString() == "0")
        //                {
        //                    cmbCalled.Text = "Yes";
        //                }
        //                else if (row["calledNotes"].ToString() == "1")
        //                {

        //                    cmbCalled.Text = "No";
        //                }
        //                else if (row["calledNotes"].ToString() == "2")
        //                {
        //                    cmbCalled.Text = "";
        //                }
        //                else
        //                {
        //                    cmbCalled.Text = "";
        //                }

        //            }
        //        }
        //        else
        //        {
        //            dtTemp = null;
        //        }

        //          return dtTemp;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //#endregion

        #region updateprojectstat

        //[System.Web.Services.WebMethod]
        //public static bool updateprojectstat()
        //{
        //    cConstruction objProjectInfo = new cConstruction();

        //    Page page = HttpContext.Current.CurrentHandler as Page;



        //    if (objProjectInfo.UpdateProjectStatus())
        //    {
        //        return true;
        //    }
        //    else

        //        return false;

        //}
        #endregion

        #region updateGenCon

        //[System.Web.Services.WebMethod]
        //public static bool updateGenCon(string varContractorName, string varContractorEmail, string varContractorPhone, int varContractorCustomer, string varContractorAccount, int varKAM, int varCalled)
        //{
        //    //string varContractorPhone, int varContractorCustomer, string varContractorAccount, int varKAM,
        //    cConstruction objGeneralCon = new cConstruction();

        //    objGeneralCon.varContractorName = varContractorName;
        //    objGeneralCon.varContractorEmail = varContractorEmail;
        //    objGeneralCon.varContractorPhone = varContractorPhone;
        //    objGeneralCon.varContractorCustomer = varContractorCustomer;
        //    objGeneralCon.varContractorAccount = varContractorAccount;
        //    objGeneralCon.varKAM = varKAM;
        //    objGeneralCon.varCalledNotes = varCalled;


        //    if (objGeneralCon.UpdateGeneralContractor())
        //    {
        //        return true;

        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        #endregion

        //#region updateSubCon

        //[System.Web.Services.WebMethod]
        //public static bool updateSubCon(string Company, string Title, string Name, string Phone, string Email, int Customer, int KAM, string Account, string Notes, int ID)
        //{
        //    //string varContractorPhone, int varContractorCustomer, string varContractorAccount, int varKAM,
        //    cConstruction objSubCon = new cConstruction();

        //    objSubCon.varSubCompanyName = Company;
        //    objSubCon.varSubTitle = Title;
        //    objSubCon.varSubName = Name;
        //    objSubCon.varSubPhone = Phone;
        //    objSubCon.varSubEmail = Email;
        //    objSubCon.varSubCustomer = Customer;
        //    objSubCon.varSubKAM = KAM;
        //    objSubCon.varSubAccount = Account;
        //    objSubCon.varSubNotes = Notes;
        //    objSubCon.varSubID = ID;

        //    if (objSubCon.UpdateSubContractor())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }


        //}

        //#endregion

        #region AddSubCon

        [System.Web.Services.WebMethod]
        public static bool AddSubCon(string Company, string Title, string Name, string Phone, string Email, int Customer, int KAM, string Account, string Notes, int ID)
        {
            //string varContractorPhone, int varContractorCustomer, string varContractorAccount, int varKAM,
            cConstruction objSubCon = new cConstruction();

            objSubCon.varSubCompanyName = Company;
            objSubCon.varSubTitle = Title;
            objSubCon.varSubName = Name;
            objSubCon.varSubPhone = Phone;
            objSubCon.varSubEmail = Email;
            objSubCon.varSubCustomer = Customer;
            objSubCon.varSubKAM = KAM;
            objSubCon.varSubAccount = Account;
            objSubCon.varSubNotes = Notes;
            objSubCon.varSubID = ID;

            if (objSubCon.AddSubContractor())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        //#region DeleteSubCon

        //[System.Web.Services.WebMethod]
        //public static bool DeleteSubCon(int ID)
        //{
        //    //string varContractorPhone, int varContractorCustomer, string varContractorAccount, int varKAM,
        //    cConstruction objSubCon = new cConstruction();


        //    objSubCon.varSubID = ID;

        //    if (objSubCon.DeleteSubContractor())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
        
        //#endregion

        //#region updateCallInfo
     
        //[System.Web.Services.WebMethod]
        //public static bool updateCallInfo(string CallNotes, string followupCont, string FollowupNotes, string FollowupDate, int NumberOfCall, int ID)
        //{

        //    //updateCallInfo(string CallNotes, string followupCont, string FollowupNotes, string FollowupDate, int NumberOfCall, int ID)


        //    //string varContractorPhone, int varContractorCustomer, string varContractorAccount, int varKAM,
        //    cConstruction objCallInfo = new cConstruction();

        //    objCallInfo.varCallNotes = CallNotes;
        //    objCallInfo.varFollowupCont = followupCont;
        //    objCallInfo.varFollowupNotes = FollowupNotes;
        //    objCallInfo.varNextfollowupDate = FollowupDate;
        //    objCallInfo.varNumberOfCall = NumberOfCall;
        //    objCallInfo.varCallID = ID;


        //    if (objCallInfo.UpdateCallInfo())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        //#endregion

        #region GetDataGeneralCon

        //[System.Web.Services.WebMethod]
        //public static DataTable GetDataGeneralCon(string ProjectID)
        //{

        //    DataSet ds = new DataSet();

        //    //try
        //    //{
        //    //string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ProjectInfo" + ".xml";
        //    DataRow[] results;
        //    DataTable dtTemp = new DataTable();
        //    string Query;
        //    DataSet dsGeneralContractor = null;
        //    cConstruction objProjectInfo = new cConstruction();
        //    Query = " 1=1";

        //    //SessionFacade.PROJECTID = ProjectID;
        //    //objProjectInfo.varProjectID = SessionFacade.PROJECTID;

        //    //return dtTemp;



        //    cConstruction objGeneralContractor = new cConstruction();


        //    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-GeneralContractor" + ".xml";

        //    try
        //    {
        //        SessionFacade.PROJECTID = ProjectID;
        //        objGeneralContractor.varProjectID = SessionFacade.PROJECTID;


        //        //----------------------------

        //        DataColumn dc = new DataColumn("Row", typeof(System.Int32));
        //        dc.AutoIncrement = true;
        //        dc.AutoIncrementSeed = 1;
        //        dc.AutoIncrementStep = 1;

        //        dsGeneralContractor = objGeneralContractor.GetGeneralContractor();

        //        dsGeneralContractor.Tables[0].Columns.Add(dc);

        //        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
        //        dsGeneralContractor.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //        xmlSW.Close();

        //        //Reading XML
        //        System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
        //        ds.ReadXml(fsReadXml);
        //        fsReadXml.Close();

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtTemp = ds.Tables[0].Clone();

        //            //Search by Date


        //            if (SessionFacade.PROJECTID != null)
        //            {

        //                Query = Query + " and [Project ID] = '" + ProjectID + "'";

        //            }

        //            results = ds.Tables[0].Select(Query);


        //            foreach (DataRow dr in results)
        //            {
        //                dtTemp.ImportRow(dr);
        //                SessionFacade.GeneralConID = dr["ID"].ToString();
        //            }
        //        }
        //        else
        //            dtTemp = null;

        //        return dtTemp;

        //    }

        //    catch
        //    {
        //        return null;
        //    }

        //}
        #endregion

        protected void grdReedTerritoriesDataPageEventHandler(object sender, GridViewPageEventArgs e)
        {

            try
            {
                gwReedTerritories.DataSource = GetXMLReed();
                gwReedTerritories.PageIndex = e.NewPageIndex;
                gwReedTerritories.DataBind();

                if (gwReedTerritories.Rows.Count > 0)
                {

                    gwReedTerritories.UseAccessibleHeader = true;
                    gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

                }
                
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "Construction Page - Construction -grdReedTerritoriesDataPageEventHandler()", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());

            }
        }

       
        protected void gwSubCon_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gwSubCon, "SELECT$" + e.Row.RowIndex.ToString());
            //}
        }


        protected void GetXMLReed(object sender, EventArgs e)
        {
            gwReedTerritories.DataSource = GetXMLReed();
            gwReedTerritories.DataBind();

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            gwReedTerritories.DataSource = GetXMLReed();
            gwReedTerritories.DataBind();

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        protected void btnGenUpdate_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true;
            btnGenCancel.Enabled = true;
            btnGenSave.Enabled = true;

            //Textboxes and CMB
            txtPhoneNum.Enabled = true;
            txtEmail.Enabled = true;
            txtAccountNum.Enabled = true;
            cmbCustomer.Enabled = true;
            cmbKAM.Enabled = true;
            cmbCalled.Enabled = true;
            btnGenUpdate.Enabled = false;

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }


            //--------------------------
        }


        protected void btnSubUpdate_Click(object sender, EventArgs e)
        {
            btnSubConCancel.Enabled = true;
            btnSubConSave.Enabled = true;

            //Textboxes and CMB
            txtCompanyName.Enabled = true;
            txtTitle2.Enabled = true;
            txtName2.Enabled = true;
            txtPhone2.Enabled = true;
            txtEmail2.Enabled = true;
            txtNotes2.Enabled = true;
            dlCustomerYN.Enabled = true;
            dlKANYN.Enabled = true;
            txtAccountNum2.Enabled = true;
            //---------------------------

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        protected void btnSubConCancel_Click(object sender, EventArgs e)
        {
            btnSubConCancel.Enabled = false;
            btnSubConSave.Enabled = false;

            //Textboxes and CMB
            txtCompanyName.Enabled = false;
            txtTitle2.Enabled = false;
            txtName2.Enabled = false;
            txtPhone2.Enabled = false;
            txtEmail2.Enabled = false;
            txtNotes2.Enabled = false;
            dlCustomerYN.Enabled = false;
            dlKANYN.Enabled = false;
            txtAccountNum2.Enabled = false;
            //---------------------------

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }




        //protected void gwReedTerritories_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-GeneralContractor" + ".xml";
        //        DataRow[] results;
        //        DataTable dtTemp = new DataTable();
        //        string Query;

        //        Query = " 1=1";

        //        //Reading XML
        //        System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
        //        ds.ReadXml(fsReadXml);
        //        fsReadXml.Close();

        //        //To Copy the Schema.
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            dtTemp = ds.Tables[0].Clone();

        //            //Search by Date


        //            if (SessionFacade.PROJECTID != null)
        //            {

        //                Query = Query + " and [Project ID] = '" + SessionFacade.PROJECTID + "'";

        //            }

        //            results = ds.Tables[0].Select(Query);

        //            foreach (DataRow dr in results)
        //                dtTemp.ImportRow(dr);
        //        }
        //        else
        //            dtTemp = null;

        //        if (gwReedTerritories.Rows.Count > 0)
        //        {

        //            gwReedTerritories.UseAccessibleHeader = true;
        //            gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

        //        }
        //    }
        //    catch
        //    {
        //        //return null;
        //    }
        //}

        protected void gwSubCon_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rows = null;
            rows = gwSubCon.SelectedRow;

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        protected void btnUpdateStat_Click(object sender, EventArgs e)
        {

            cConstruction objProjectInfo = new cConstruction();

            Page page = HttpContext.Current.CurrentHandler as Page;
            objProjectInfo.varProjectID = SessionFacade.PROJECTID;



            if (rdoActive.Checked == true)
            {
                SessionFacade.PROJECTSTATUS = "false";
            }
            else
            {
                SessionFacade.PROJECTSTATUS = "true";
            }


            objProjectInfo.UpdateProjectStatus();


            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }

             // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "Confirmation()", true);
        }



        protected void btnGenSave_Click(object sender, EventArgs e)
        {
            cConstruction objGeneralCon = new cConstruction();
            Page page = HttpContext.Current.CurrentHandler as Page;
            bool bStatus = false;
            objGeneralCon.varProjectID = SessionFacade.PROJECTID;
            objGeneralCon.varContractorName = txtName.Text.Trim();
            objGeneralCon.varContractorEmail = txtEmail.Text.Trim();
            objGeneralCon.varContractorPhone = txtPhoneNum.Text.Trim();           
            if (cmbCustomer.Text == "Yes")
            {
                objGeneralCon.varContractorCustomer = 0;
            }
            else
            {
                objGeneralCon.varContractorCustomer = 1;
            }
            objGeneralCon.varContractorAccount = txtAccountNum.Text.Trim();
         

            if (cmbKAM.Text == "Yes")
            {
                objGeneralCon.varKAM = 0;
            }
            else
            {
                objGeneralCon.varKAM = 1;
            }

           
            if (cmbCalled.Text == "Yes")
            {
                objGeneralCon.varCalledNotes = 1;
            }
            else if (cmbCalled.Text == "No")
            {                
                objGeneralCon.varCalledNotes = 2;
            }

            bStatus = objGeneralCon.UpdateGeneralContractor();
          
            if (bStatus == true)
            {
                loadGeneralContractor();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "onUpdateSuccess()", true);
            }
            

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }


        

        protected void btnSubConSave_Click(object sender, EventArgs e)
        {
            cConstruction objSubCon = new cConstruction();
            bool bStatus = false;
            objSubCon.varProjectID = SessionFacade.PROJECTID;

            objSubCon.varSubCompanyName = txtCompanyName.Text.Trim();
            objSubCon.varSubTitle = txtTitle2.Text.Trim();
            objSubCon.varSubName = txtName2.Text.Trim();
            objSubCon.varSubPhone = txtPhone2.Text.Trim();
            objSubCon.varSubEmail = txtEmail2.Text.Trim();
            objSubCon.varSubCustomer = Convert.ToInt32(dlCustomerYN.SelectedItem.Value);
            objSubCon.varSubKAM = Convert.ToInt32(dlKANYN.SelectedItem.Value);
            objSubCon.varSubAccount = txtAccountNum2.Text.Trim();
            objSubCon.varSubNotes = txtNotes2.Text.Trim();
            if (hdnSubConID.Value.Trim() != "")
            {
                objSubCon.varSubID = Convert.ToInt32(hdnSubConID.Value);
            }

            bStatus = objSubCon.UpdateSubContractor();

            if (bStatus == true)
            {
                loadSubContractor();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ConfirmationSubCon()", true);
            }

            

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }

        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            cConstruction objSubCon = new cConstruction();
            bool bStatus = false;
            if (hdnSubConID.Value.Trim() != "")
            {
                objSubCon.varSubID = Convert.ToInt32(hdnSubConID.Value);
            }
            bStatus = objSubCon.DeleteSubContractor();

            if (bStatus==true)
            {
             
                    txtCompanyName.Text = string.Empty;
                    txtTitle2.Text = string.Empty;
                    txtName2.Text = string.Empty;
                    txtPhone2.Text = string.Empty;
                    txtEmail2.Text = string.Empty;
                    txtAccountNum2.Text = string.Empty;
                    txtNotes2.Text = string.Empty;
                    dlCustomerYN.SelectedIndex = 0;
                    dlKANYN.SelectedIndex = 0;

                    loadSubContractor();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "onDeleteSuccess()", true);

            }


            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            
        }

        protected void btnGenCancel_Click(object sender, EventArgs e)
        {
            btnGenSave.Enabled = false;
            btnGenCancel.Enabled = false;
            btnGenUpdate.Enabled = true;
            txtName.Enabled = true;
            txtPhoneNum.Enabled = true;
            txtEmail.Enabled = true;
            txtAccountNum.Enabled = true;
            cmbCustomer.Enabled = true;
            cmbCalled.Enabled = true;
            cmbKAM.Enabled = true;
            loadGeneralContractor();    
            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }

        }

        protected void btnCallSave_Click(object sender, EventArgs e)
        {
            cConstruction objCallInfo = new cConstruction();

            bool bStatus = false;

            objCallInfo.varProjectID = SessionFacade.PROJECTID;
          
            objCallInfo.varCallNotes = txtCallNotes.Text;
            objCallInfo.varFollowupCont = txtNextFollowupContact.Text;
            objCallInfo.varFollowupNotes = txtfollowupNotes.Text;
            objCallInfo.varNextfollowupDate = txtFollowupDate.Text;
            if (txtNumCall.Text.Trim() != "")
            {
                objCallInfo.varNumberOfCall = Convert.ToInt32(txtNumCall.Text);
            }
            if (hdnCallInfo.Value.Trim() != "")
            {
                objCallInfo.varCallID = Convert.ToInt32(hdnCallInfo.Value);
            }

            bStatus = objCallInfo.UpdateCallInfo();

            if (bStatus == true)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ConfirmationSubCon()", true);
            }

            if (gwReedTerritories.Rows.Count > 0)
            {

                gwReedTerritories.UseAccessibleHeader = true;
                gwReedTerritories.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }



       // protected void gwSubCon_Sorting(object sender, GridViewSortEventArgs e)
        //{
            //string sortExpression = e.SortExpression;
            //if (GridViewSortDirection == SortDirection.Ascending)
            //{
            //    GridViewSortDirection = SortDirection.Descending;
            //    SortGridSubCon(sortExpression, "DESC");
            //}
            //else
            //{
            //    GridViewSortDirection = SortDirection.Ascending;
            //    SortGridSubCon(sortExpression, "ASC");
            //}
       // }


        //private void SortGridSubCon(string sortExpression, string direction)
        //{
        //    // You can cache the DataTable for improving performance
        //    try
        //    {
        //        DataSet ds = new DataSet();

        //        //string xmlName;

        //        //xmlName = "-SubContractor";

        //        DataTable dt = GetXMLSubCon().Tables[0];
        //        DataView dv = new DataView(dt);

        //        string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-SubContractor" + ".xml";

        //        // string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + xmlName + ".xml";

        //        dv.Sort = sortExpression + " " + direction;
        //        dt = dv.ToTable();

        //        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
        //        dt.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
        //        xmlSW.Close();

        //        gwSubCon.DataSource = dv;
        //        gwSubCon.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        new ArgumentNullException();

        //    }
        //}





        //protected SortDirection GridViewSortDirection
        //{

        //    get
        //    {

        //        if (ViewState["sortDirection"] == null)

        //            ViewState["sortDirection"] = SortDirection.Ascending;

        //        return (SortDirection)ViewState["sortDirection"];

        //    }

        //    set { ViewState["sortDirection"] = value; }

        //}


        //protected void gwReedTerritories_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    init();


        //    HttpCookie myCookie = new HttpCookie("CNo");
        //    myCookie.Expires = DateTime.Now.AddDays(-1);
        //    Response.Cookies.Add(myCookie);


        //    hdnSubConGWCount.Value = "0";
        //    string sortExpression = e.SortExpression;
        //    if (GridViewSortDirection == SortDirection.Ascending)
        //    {
        //        GridViewSortDirection = SortDirection.Descending;
        //        SortGridTeri(sortExpression, "DESC");
        //    }
        //    else
        //    {
        //        GridViewSortDirection = SortDirection.Ascending;
        //        SortGridTeri(sortExpression, "ASC");
        //    }
        //}



        //private void SortGridTeri(string sortExpression, string direction)
        //{

        //    DropDownList ddlTemp = Master.FindControl("ddlCampaign") as DropDownList;
        //    cConstruction objReedTerritories = new cConstruction();
        //  //  DataSet dsReedTerritories = null;
        //    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-ReedTerritories" + ".xml";


        //    try
        //    {

        //        DataTable dt = GetXMLReed().Tables[0];
        //       // dt.Columns.Add(dc);
        //        DataView dv = new DataView(dt);

        //        dv.Sort = sortExpression + " " + direction;
        //        dt = dv.ToTable();



        //        System.IO.StreamWriter xmlSW2 = new System.IO.StreamWriter(Pathname);
        //        dt.WriteXml(xmlSW2, XmlWriteMode.WriteSchema);
        //        xmlSW2.Close();

        //        gwReedTerritories.DataSource = dv;
        //        gwReedTerritories.DataBind();

        //    }
        //    catch (Exception ex)
        //    {
        //        new ArgumentNullException();

        //    }

        //}

        //void Columns_CollectionChanged(object sender, CollectionChangeEventArgs e)
        //{
        //    DataColumn dc = (e.Element as DataColumn);
        //    if (dc != null && dc.AutoIncrement)
        //    {
        //        long i = dc.AutoIncrementSeed;
        //        foreach (DataRow drow in dc.Table.Rows)
        //        {
        //            drow[dc] = i;
        //            i++;
        //        }
        //    }
        //}
    }    
}