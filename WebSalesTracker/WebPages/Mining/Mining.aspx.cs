using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data.SqlClient;

using AppLogic;

namespace WebSalesMine.WebPages.Mining
{
    public partial class Mining : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox txtTemp = Master.FindControl("txtMiningID") as TextBox;
            RadioButton rbSiteTemp = Master.FindControl("rdoSiteNumber") as RadioButton;
            RadioButton rbPhoneTemp = Master.FindControl("rdoBestPhone") as RadioButton;
            //if (Request.Cookies["CMiningID"] != null )
            //{
            SessionFacade.MiningID = txtTemp.Text;
            SessionFacade.ContactID = hdnMiningContact.Value;

            if (SessionFacade.ContactID == "0")
            {
                SessionFacade.ContactID = "1";
            }

            //} 
            if (rbSiteTemp.Checked == true)
            {
                SessionFacade.SelectedOption = "SiteNum";
            }

            if (rbPhoneTemp.Checked == true)
            {
                SessionFacade.SelectedOption = "BestPhone";
            }

            //cmbKAM.Text = string.Empty;         
            GetMiningDetails();
            GetMiningDetailsDset();

        }

        private void GetMiningDetails()
        {
            try
            {

                cMining objMining = new cMining();

                objMining.varOptions = SessionFacade.SelectedOption;
                objMining.varValue = SessionFacade.MiningID;

                
                SqlDataReader drCampaign = objMining.drGetMiningInfo();
                if (drCampaign.HasRows)
                {
                    while (drCampaign.Read())
                    {
                        txtCompName.Text = (string)drCampaign["Company_Name"]; //drCampaign.GetString(0);
                        txtCompanyPhone.Text = (string)drCampaign["Company_Phone"];
                        txtSIC.Text = (string)drCampaign["Sic"];
                        txtEmployeeSize.Text = (string)drCampaign["Employee_Size"];
                        txtAddress.Text = (string)drCampaign["address"];
                        txtCity.Text = (string)drCampaign["city"];
                        txtState.Text = (string)drCampaign["state"];
                        txtPostalCode.Text = (string)drCampaign["Postal_Code"];
                        txtSalutation.Text = (string)drCampaign["salutation"];
                        txtFirstname.Text = (string)drCampaign["first_name"];
                        txtSurname.Text = (string)drCampaign["Last_Name"];
                        txtFunction.Text = (string)drCampaign["Function_Code"];
                        txtFunctionDesc.Text = (string)drCampaign["Function_Description"];
                        txtSource.Text = (string)drCampaign["source"];
                    }

                }
                else
                {
                    txtCompName.Text = string.Empty;
                    txtCompanyPhone.Text = string.Empty;
                    txtSIC.Text = string.Empty;
                    txtEmployeeSize.Text = string.Empty;
                    txtAddress.Text = string.Empty;
                    txtCity.Text = string.Empty;
                    txtState.Text = string.Empty;
                    txtPostalCode.Text = string.Empty;
                    txtSalutation.Text = string.Empty;
                    txtFirstname.Text = string.Empty;
                    txtSurname.Text = string.Empty;
                    txtFunction.Text = string.Empty;
                    txtFunctionDesc.Text = string.Empty;
                    txtSource.Text = string.Empty;
                }
                drCampaign.Close();
                
            }
            catch (Exception err)
            {

            }
        }


        private void GetMiningDetailsDset()
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningDetails" + ".xml";
            string PathnameDept = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningDeptDetails" + ".xml";
            string PathnameSCO = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningSCODetails" + ".xml";
            string PathnameNotes = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningNotesDetails" + ".xml";

            try
            {

                cMining objMining = new cMining();
                DataSet dsMiningNotes = null;
                DataSet dsMining = null;
                DataSet dsMiningDept = null;
                DataSet dsMiningSCO = null;
                
                DataSet ds = new DataSet();
                DataSet dsNotes = new DataSet(); 

                objMining.varOptions = SessionFacade.SelectedOption;
                objMining.varValue = SessionFacade.MiningID;
                objMining.varContactID = SessionFacade.ContactID;
       

                DataColumn dc = new DataColumn("Row", typeof(System.Int32));
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dc.AutoIncrementStep = 1;

                dsMining = objMining.dsGetMiningInfo();
                dsMiningDept = objMining.dsGetMiningDept();
            //    dsMiningSCO = objMining.dsGetMiningSCO();
                dsMiningNotes = objMining.dsGetMiningNotes();
               // dsGeneralContractor = objGeneralContractor.GetGeneralContractor();

                dsMining.Tables[0].Columns.Add(dc);


                gwMining.DataSource = null;

                if (dsMining.Tables.Count > 0)
                {
                    
                     

                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    dsMining.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    //System.IO.StreamWriter xmlSWDept = new System.IO.StreamWriter(PathnameDept);
                    //dsMiningDept.WriteXml(xmlSWDept, XmlWriteMode.WriteSchema);
                    //xmlSWDept.Close();

                    //System.IO.StreamWriter xmlSWSCO = new System.IO.StreamWriter(PathnameSCO);
                    //dsMiningSCO.WriteXml(xmlSWSCO, XmlWriteMode.WriteSchema);
                    //xmlSWSCO.Close();

                    System.IO.StreamWriter xmlSWNotes = new System.IO.StreamWriter(PathnameNotes);
                    dsMiningNotes.WriteXml(xmlSWNotes, XmlWriteMode.WriteSchema);
                    xmlSWNotes.Close();

                    //Reading XML
                    System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                    ds.ReadXml(fsReadXml);
                    fsReadXml.Close();

                    //Reading XML
                    System.IO.FileStream fsReadXmlNotes = new System.IO.FileStream(PathnameNotes, System.IO.FileMode.Open);
                    dsNotes.ReadXml(fsReadXmlNotes);
                    fsReadXmlNotes.Close();

                    gwMining.DataSource = ds;
                    gwMining.DataBind();

                    gvNotes.DataSource = dsNotes;
                    gvNotes.DataBind();
                }
                
                

            }
            catch (Exception err)
            {

            }
        }

        [System.Web.Services.WebMethod]
        public static DataTable GetMiningQA(string ContactID)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningQA" + ".xml";
          
            DataSet dsMining = new DataSet();
            cMining objMining = new cMining();
            DataSet ds = new DataSet(); 
            DataTable dtTemp = new DataTable();
            DataRow[] results;
            string Query="1=1";

            objMining.varOptions = SessionFacade.SelectedOption;
            objMining.varValue = SessionFacade.MiningID;
            objMining.varContactID = ContactID;

            dsMining = objMining.dsGetMiningQA();

            DataColumn dc = new DataColumn("Row", typeof(System.Int32));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;

            dsMining.Tables[0].Columns.Add(dc);

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            dsMining.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtTemp = ds.Tables[0].Clone();

                //Search by Date


                if (SessionFacade.PROJECTID != null)
                {

                    Query = Query + " and [Contact_number] = '" + ContactID + "'";

                }

                results = ds.Tables[0].Select(Query);


                foreach (DataRow dr in results)
                {
                    dtTemp.ImportRow(dr);
                    //SessionFacade.GeneralConID = dr["ID"].ToString();
                  
                }
            }
            //else
            //    dtTemp = null;


            return dtTemp;
        }


        [System.Web.Services.WebMethod]
        public static DataTable GetMiningQASCO(string ContactID)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningQASCO" + ".xml";

            DataSet dsMining = new DataSet(); 
            cMining objMining = new cMining();
            DataSet ds = new DataSet();
            DataTable dtTemp = new DataTable();
            DataRow[] results;
            string Query = "1=1";

            objMining.varOptions = SessionFacade.SelectedOption;
            objMining.varValue = SessionFacade.MiningID;
            objMining.varContactID = ContactID;

            dsMining = objMining.dsGetMiningQA();

            DataColumn dc = new DataColumn("Row", typeof(System.Int32));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;

            dsMining.Tables[0].Columns.Add(dc);

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            dsMining.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtTemp = ds.Tables[0].Clone();

                //Search by Date


                if (SessionFacade.PROJECTID != null)
                {

                    Query = Query + " and [Contact_number] = '" + ContactID + "'";

                }

                results = ds.Tables[0].Select(Query);


                foreach (DataRow dr in results)
                {
                    dtTemp.ImportRow(dr);
                    //SessionFacade.GeneralConID = dr["ID"].ToString();

                }
            }
            //else
            //    dtTemp = null;


            return dtTemp;
        }

        [System.Web.Services.WebMethod]
        public static DataTable GetMiningDetails(string ContactID)
        {
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningDetails" + ".xml";

            DataSet dsMining = new DataSet();
            cMining objMining = new cMining();
            DataSet ds = new DataSet();
            DataTable dtTemp = new DataTable();
            DataRow[] results;
            string Query = "1=1"; 

            objMining.varOptions = SessionFacade.SelectedOption;
            objMining.varValue = SessionFacade.MiningID;
            objMining.varContactID = ContactID;

            dsMining = objMining.dsGetMiningInfo();

            DataColumn dc = new DataColumn("Row", typeof(System.Int32));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;

            dsMining.Tables[0].Columns.Add(dc);

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
            dsMining.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtTemp = ds.Tables[0].Clone();

                //Search by Date


                if (SessionFacade.PROJECTID != null)
                {

                    Query = Query + " and [Contact_number] = '" + ContactID + "'";

                }

                results = ds.Tables[0].Select(Query);


                foreach (DataRow dr in results)
                {
                    dtTemp.ImportRow(dr);
                    //SessionFacade.GeneralConID = dr["ID"].ToString();

                }
            }
            //else
            //    dtTemp = null;


            return dtTemp;
        }


        [System.Web.Services.WebMethod]
        public static DataTable GetMiningDeptXML(string SiteID,string ContactID)
        {  
            string PathnameDept = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningDeptDetails" + ".xml";
            cMining objMining = new cMining();

            DataSet dsMiningDept = null;
            DataSet ds = new DataSet();
            DataTable dtTemp = new DataTable();
            DataRow[] results; 
            string Query = "1=1";

            objMining.varOptions = SessionFacade.SelectedOption;
            objMining.varValue = SessionFacade.MiningID;
            objMining.varContactID = ContactID;

            dsMiningDept = objMining.dsGetMiningDept();

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameDept);
            dsMiningDept.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(PathnameDept, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtTemp = ds.Tables[0].Clone();

                //Search by Date


                if (SessionFacade.MiningID != null)
                {

                    Query = Query + " and [ProspectAccNum] = '" + SiteID + "' and [Contact_number] = '" + ContactID + "'";

                }

                results = ds.Tables[0].Select(Query);


                foreach (DataRow dr in results)
                {
                    dtTemp.ImportRow(dr);
                    //SessionFacade.GeneralConID = dr["ID"].ToString();
                }
            }
            else
                dtTemp = null;


            return dtTemp;
        }


        [System.Web.Services.WebMethod]
        public static DataTable GetMiningSCOXML(string SiteID, string ContactID)
        {
            string PathnameSCO = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningSCODetails" + ".xml";

            cMining objMining = new cMining();

            DataSet dsMiningSCO = null;

            DataSet ds = new DataSet();
            DataTable dtTemp = new DataTable();
            DataRow[] results;
            string Query = "1=1";

            objMining.varOptions = SessionFacade.SelectedOption;
            objMining.varValue = SessionFacade.MiningID;
            objMining.varContactID = ContactID;

            dsMiningSCO = objMining.dsGetMiningSCO();

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameSCO);
            dsMiningSCO.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(PathnameSCO, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtTemp = ds.Tables[0].Clone();

                //Search by Date


                if (SessionFacade.MiningID != null)
                {

                    Query = Query + " and [ProspectAccNum] = '" + SiteID + "' and [Contact_number] = '" + ContactID + "'";

                }

                results = ds.Tables[0].Select(Query);


                foreach (DataRow dr in results)
                {
                    dtTemp.ImportRow(dr);
                    //SessionFacade.GeneralConID = dr["ID"].ToString();
                }
            }
            else
                dtTemp = null;


            return dtTemp;
        }


        [System.Web.Services.WebMethod]
        public static DataTable GetMiningNotesXML()
        {
            string PathnameNotes = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningNotesDetails" + ".xml";

            cMining objMining = new cMining();
            DataSet dsMiningNotes = null;

            DataSet ds = new DataSet();
            DataTable dtTemp = new DataTable();
            DataRow[] results;
            string Query = "1=1";

            objMining.varValue = SessionFacade.MiningID;

            dsMiningNotes = objMining.dsGetMiningNotes();

            //Writing XML
            System.IO.StreamWriter xmlSWNotes = new System.IO.StreamWriter(PathnameNotes);
            dsMiningNotes.WriteXml(xmlSWNotes, XmlWriteMode.WriteSchema);
            xmlSWNotes.Close();


            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(PathnameNotes, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtTemp = ds.Tables[0].Clone();

                //Search by Date


                if (SessionFacade.MiningID != null)
                {

                    Query = Query + " and [Site_Number] = '" + objMining.varValue + "'";

                }

                results = ds.Tables[0].Select(Query);
                

                foreach (DataRow dr in results)
                {
                    dtTemp.ImportRow(dr);
                    //SessionFacade.GeneralConID = dr["ID"].ToString();
                }
            }
            else
                dtTemp = null;

            
            return dtTemp;

        }

    

        protected void btnSaveQA_Click(object sender, EventArgs e)
        {
            //cMining objMining = new cMining();

            //objMining.varActiveMineSite = cmbKAM.SelectedItem.Text; 
            //objMining.varAddressOfficeWare = cmbOfficeWarehouse.SelectedItem.Text;
            //objMining.varNoQuestion = cmbNoQuestion.SelectedItem.Text;
            //objMining.varOtherNoQuestion = txtIfOthers.Text.Trim();
            //objMining.varSafetySignsFacility = dlSafetySigns.SelectedItem.Text;
            //objMining.varCreatedBy = SessionFacade.LoggedInUserName;
            //objMining.varYesCorporateOfficeSiteLevel = dlHandled.SelectedItem.Text;
            //objMining.varYesCorporateOfficeSiteLevelOther = txtQOther.Text.Trim();
            //objMining.varDisposition = dlDisposition.SelectedItem.Text;

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ConfirmationQA()", true);
           
        }

        [System.Web.Services.WebMethod]
        public static bool updateQA(string ActiveMineSite, string ContactID, string AddressOfficeWare, string NoQuestion, string OtherNoQuestion, string SafetySignsFacility, string YesCorporateOfficeSiteLevel, string YesCorporateOfficeSiteLevelOther, string Disposition)
        {
            //string varContractorPhone,  int varContractorCustomer, string varContractorAccount, int varKAM,
            
            cMining objMining = new cMining();

            objMining.varValue = SessionFacade.MiningID;
            objMining.varActiveMineSite = ActiveMineSite;
            objMining.varAddressOfficeWare = AddressOfficeWare;
            objMining.varNoQuestion = NoQuestion;
            objMining.varOtherNoQuestion = OtherNoQuestion;
            objMining.varSafetySignsFacility = SafetySignsFacility;
            objMining.varYesCorporateOfficeSiteLevel = YesCorporateOfficeSiteLevel;
            objMining.varYesCorporateOfficeSiteLevelOther = YesCorporateOfficeSiteLevelOther;
            objMining.varDisposition = Disposition;
            objMining.varContactID = ContactID;
            

            if (objMining.UpdateQAMining())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        [System.Web.Services.WebMethod]
        public static bool updateDept(string ContactID, string Department, string Counter)
        {  
            //string varContractorPhone,  int varContractorCustomer, string varContractorAccount, int varKAM,

            cMining objMining = new cMining();

            objMining.varValue = SessionFacade.MiningID;
            objMining.varDeptValue = Department;
            objMining.varContactID = ContactID;
            objMining.varCounterID = Counter;

            if (objMining.UpdateDeptMining())
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        [System.Web.Services.WebMethod]
        public static bool updateSCO(string ContactID, string MailAddress, string FirstName, string LastName, string Title, string PhoneNumber, string ExtNumber, string EmailAddress)
        {
            //string varContractorPhone,  int varContractorCustomer, string varContractorAccount, int varKAM,

            cMining objMining = new cMining();

            objMining.varValue = SessionFacade.MiningID;
            objMining.varContactID = ContactID;
            objMining.varMailAddress = MailAddress;
            objMining.varFirstName = FirstName;
            objMining.varLastName = LastName;
            objMining.varTitle = Title;
            objMining.varPhoneNumber = PhoneNumber;
            objMining.varExtNumber = ExtNumber;
            objMining.varEmailAddress = EmailAddress;


            if (objMining.UpdateSCOMining())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        [System.Web.Services.WebMethod]
        public static bool InserNotes(string Notes)
        {
            //string varContractorPhone,  int varContractorCustomer, string varContractorAccount, int varKAM,

            cMining objMining = new cMining();

            objMining.varValue = SessionFacade.MiningID;
            objMining.varNotes = Notes;
           
                        
            if (objMining.InsertMiningNotes())
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        protected void btnSaveNotes_Click(object sender, EventArgs e)
        {
           

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "ConfirmationNotes()", true);
            loadNotes();

        }
    

        private void loadNotes()
        {
             string PathnameNotes = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-MiningNotesDetails" + ".xml";

            cMining objMining = new cMining();
            DataSet dsMiningNotes = null;

            DataSet ds = new DataSet();
            DataTable dtTemp = new DataTable();
            DataRow[] results;
            string Query = "1=1";

            objMining.varValue = SessionFacade.MiningID;

            dsMiningNotes = objMining.dsGetMiningNotes();

            //Writing XML
            System.IO.StreamWriter xmlSWNotes = new System.IO.StreamWriter(PathnameNotes);
            dsMiningNotes.WriteXml(xmlSWNotes, XmlWriteMode.WriteSchema);
            xmlSWNotes.Close();


            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(PathnameNotes, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtTemp = ds.Tables[0].Clone();

                //Search by Date


                if (SessionFacade.MiningID != null)
                {

                    Query = Query + " and [Site_Number] = '" + objMining.varValue + "'";

                }

                results = ds.Tables[0].Select(Query);


                foreach (DataRow dr in results)
                {
                    dtTemp.ImportRow(dr);
                    //SessionFacade.GeneralConID = dr["ID"].ToString();
                }
            }
            else
            {
                dtTemp = null;
            }


            gvNotes.DataSource = dtTemp;
            gvNotes.DataBind();

            
        }

        protected void btnCancelNotes_Click(object sender, EventArgs e)
        {
            txtNotes2.Text=string.Empty;
            
            //Retain Dialog
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
            //       "call me", "  $(document).ready(function () {var mydiv = $('#pnlAllNotes');mydiv.dialog({ autoOpen: false," +
            //        "title: 'MINING NOTES'," +
            //        "width: 'auto'," +
            //        "open: function (type, data) {" +
            //           " $(this).parent().appendTo('form'); " +
            //        "}" +
            //    "  }); mydiv.dialog('open');return false;}); ", true);
        }

        //protected void lnkAllNotes_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "Messagessssss()", true);
        //}

        

    }
}