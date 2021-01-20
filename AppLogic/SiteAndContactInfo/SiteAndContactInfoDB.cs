using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BradyCorp.Log;
using DataAccess;
using System.Configuration;
using System.Data.SqlTypes;

namespace AppLogic
{
    public class cSiteAndContactInfoDB
    {
        #region GetSiteAndContactInfo //Following functions will be used in Site and Contact Info
        public DataSet GetListSiteContactInfo(cSiteAndContactInfo objSiteContact)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drContactSite = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objSiteContact.SearchAccount.Replace("'", "''");

                Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                Parameter[1].Value = objSiteContact.SearchCampaign;

                //Parameter[2] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
                //Parameter[2].Value = SessionFacade.LoggedInUserName;

                //Parameter[3] = new SqlParameter("@Listview", SqlDbType.VarChar, 30);
                //Parameter[3].Value = "lvwContInfo";

                drContactSite = DBHelper.ExecuteQueryToDataSet("SiteContactInfo", Parameter);

                return drContactSite;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "GetListSiteContactInfo", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                //LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetAccInfo //Following functions will be used in Site and Contact Info
        public DataSet GetAcctInfo(cSiteAndContactInfo objSiteContact)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drContactAcc = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objSiteContact.SearchAccount.Replace("'", "''");

                Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                Parameter[1].Value = objSiteContact.SearchCampaign;

                drContactAcc = DBHelper.ExecuteQueryToDataSet("Acct_Info", Parameter);

                return drContactAcc;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region UpdateQQ //Following functions will be used in Site and Contact Info
        public bool UpdateQQ(cUpdateQQ objUpdateQQ)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[12];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objUpdateQQ.Account;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 100);
                Parameter[1].Value = objUpdateQQ.Campaign;

                Parameter[2] = new SqlParameter("@Contact", SqlDbType.Int);
                Parameter[2].Value = int.Parse(objUpdateQQ.Contact);

                Parameter[3] = new SqlParameter("@Function", SqlDbType.VarChar, 25);
                Parameter[3].Value = objUpdateQQ.ContFunc;

                Parameter[4] = new SqlParameter("@ContactStatus", SqlDbType.VarChar, 25);
                Parameter[4].Value = objUpdateQQ.ContactStatus;

                Parameter[5] = new SqlParameter("@Purchasing", SqlDbType.VarChar, 10);
                Parameter[5].Value = objUpdateQQ.Purchasing;

                Parameter[6] = new SqlParameter("@ContBudget", SqlDbType.VarChar, 20);
                Parameter[6].Value = objUpdateQQ.ContBudget;

                Parameter[7] = new SqlParameter("@Factor", SqlDbType.VarChar, 10);
                Parameter[7].Value = objUpdateQQ.Factor;

                Parameter[8] = new SqlParameter("@SpVendor", SqlDbType.Int);
                Parameter[8].Value = objUpdateQQ.SpVendor;

                Parameter[9] = new SqlParameter("@Sp", SqlDbType.Int);
                Parameter[9].Value = objUpdateQQ.SP;

                Parameter[10] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
                Parameter[10].Value = objUpdateQQ.Username;

                Parameter[11] = new SqlParameter("@Other", SqlDbType.VarChar, 300);
                Parameter[11].Value = objUpdateQQ.Other;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("UpdateQQ", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region UpdateQQPC //Following functions will be used in Site and Contact Info
        public bool UpdateQQPC(cUpdateQQPC objUpdateQQPC)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[11];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objUpdateQQPC.Account;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 100);
                Parameter[1].Value = objUpdateQQPC.Campaign;

                Parameter[2] = new SqlParameter("@Spanish", SqlDbType.VarChar, 32);
                Parameter[2].Value = objUpdateQQPC.Spanish;

                Parameter[3] = new SqlParameter("@EmployeeSize", SqlDbType.VarChar, 32);
                Parameter[3].Value = objUpdateQQPC.EmployeeSize;

                Parameter[4] = new SqlParameter("@health", SqlDbType.VarChar, 32);
                Parameter[4].Value = objUpdateQQPC.Health;

                Parameter[5] = new SqlParameter("@Username", SqlDbType.VarChar, 20);
                Parameter[5].Value = objUpdateQQPC.Username;

                Parameter[6] = new SqlParameter("@ContStats", SqlDbType.VarChar, 64);
                Parameter[6].Value = objUpdateQQPC.ContStats;

                Parameter[7] = new SqlParameter("@Qx", SqlDbType.VarChar, 64);
                Parameter[7].Value = objUpdateQQPC.Qx;

                Parameter[8] = new SqlParameter("@ContFunction", SqlDbType.VarChar, 64);
                Parameter[8].Value = objUpdateQQPC.ContFunction;

                Parameter[9] = new SqlParameter("@ContBudgets", SqlDbType.VarChar, 64);
                Parameter[9].Value = objUpdateQQPC.ContBudgets;

                Parameter[10] = new SqlParameter("@Contact", SqlDbType.Int);
                Parameter[10].Value = int.Parse(objUpdateQQPC.Contact.ToString().Trim());

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("UpdateQQPC", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region AddSecondQ //Following functions will be used in Site and Contact Info
        public bool AddSecondQ(cSecondaryQ objAddQ)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[9];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objAddQ.Account;

                Parameter[1] = new SqlParameter("@Contact", SqlDbType.Int);
                Parameter[1].Value = objAddQ.Contact;

                Parameter[2] = new SqlParameter("@Campaign", SqlDbType.VarChar, 12);
                Parameter[2].Value = objAddQ.Campaign;

                Parameter[3] = new SqlParameter("@Q1", SqlDbType.VarChar, 128);
                Parameter[3].Value = objAddQ.Q1;

                Parameter[4] = new SqlParameter("@Q2", SqlDbType.VarChar, 128);
                Parameter[4].Value = objAddQ.Q2;

                Parameter[5] = new SqlParameter("@Q3", SqlDbType.VarChar, 128);
                Parameter[5].Value = objAddQ.Q3;

                Parameter[6] = new SqlParameter("@Q4", SqlDbType.VarChar, 128);
                Parameter[6].Value = objAddQ.Q4;

                Parameter[7] = new SqlParameter("@Q5", SqlDbType.VarChar, 128);
                Parameter[7].Value = objAddQ.Q5;

                Parameter[8] = new SqlParameter("@Username", SqlDbType.VarChar, 16);
                Parameter[8].Value = objAddQ.Username;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddSecondaryQuestion", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetSecondQ //Following functions will be used in Site and Contact Info
        public DataSet GetSecondQ(cSecondaryQ objSecondQ)
        {
            try
            {
                DataSet drSecondQ = new DataSet();
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objSecondQ.Account;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 100);
                Parameter[1].Value = objSecondQ.Campaign;

                drSecondQ = DBHelper.ExecuteQueryToDataSet("SecondaryQuestion", Parameter);

                return drSecondQ;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region UpdateProducts //Following functions will be used in Site and Contact Info
        public bool UpdateProducts(cUpdateProducts objUpdateProducts)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[21];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objUpdateProducts.Account;

                Parameter[1] = new SqlParameter("@Contact", SqlDbType.Int);
                Parameter[1].Value = objUpdateProducts.Contact;

                Parameter[2] = new SqlParameter("@FSM", SqlDbType.Int);
                Parameter[2].Value = objUpdateProducts.FSM;

                Parameter[3] = new SqlParameter("@FAE", SqlDbType.Int);
                Parameter[3].Value = objUpdateProducts.FAE;

                Parameter[4] = new SqlParameter("@LBL", SqlDbType.Int);
                Parameter[4].Value = objUpdateProducts.LBL;

                Parameter[5] = new SqlParameter("@LO", SqlDbType.Int);
                Parameter[5].Value = objUpdateProducts.LO;

                Parameter[6] = new SqlParameter("@OS", SqlDbType.Int);
                Parameter[6].Value = objUpdateProducts.OS;

                Parameter[7] = new SqlParameter("@PVM", SqlDbType.Int);
                Parameter[7].Value = objUpdateProducts.PVM;

                Parameter[8] = new SqlParameter("@PPE", SqlDbType.Int);
                Parameter[8].Value = objUpdateProducts.PPE;

                Parameter[9] = new SqlParameter("@PI", SqlDbType.Int);
                Parameter[9].Value = objUpdateProducts.PI;

                Parameter[10] = new SqlParameter("@SFS", SqlDbType.Int);
                Parameter[10].Value = objUpdateProducts.SFS;

                Parameter[11] = new SqlParameter("@SP", SqlDbType.Int);
                Parameter[11].Value = objUpdateProducts.SP;

                Parameter[12] = new SqlParameter("@SLS", SqlDbType.Int);
                Parameter[12].Value = objUpdateProducts.SLS;

                Parameter[13] = new SqlParameter("@SCP", SqlDbType.Int);
                Parameter[13].Value = objUpdateProducts.SCP;

                Parameter[14] = new SqlParameter("@SC", SqlDbType.Int);
                Parameter[14].Value = objUpdateProducts.SC;

                Parameter[15] = new SqlParameter("@TAGS", SqlDbType.Int);
                Parameter[15].Value = objUpdateProducts.TAGS;

                Parameter[16] = new SqlParameter("@TPS", SqlDbType.Int);
                Parameter[16].Value = objUpdateProducts.TPS;

                Parameter[17] = new SqlParameter("@TC", SqlDbType.Int);
                Parameter[17].Value = objUpdateProducts.TC;

                Parameter[18] = new SqlParameter("@W", SqlDbType.Int);
                Parameter[18].Value = objUpdateProducts.W;

                Parameter[19] = new SqlParameter("@ETO", SqlDbType.Int);
                Parameter[19].Value = objUpdateProducts.ETO;

                Parameter[20] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
                Parameter[20].Value = objUpdateProducts.Username;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("UpdateProducts", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region AddVendors //Following functions will be used in Site and Contact Info
        public bool AddVendors(cAddVendors objAddVendors)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[5];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objAddVendors.Account;

                Parameter[1] = new SqlParameter("@Contact", SqlDbType.Int);
                Parameter[1].Value = objAddVendors.Contact;

                Parameter[2] = new SqlParameter("@Name", SqlDbType.VarChar, 64);
                Parameter[2].Value = objAddVendors.VendorName;

                Parameter[3] = new SqlParameter("@Other", SqlDbType.VarChar, 128);
                Parameter[3].Value = objAddVendors.Comments;

                Parameter[4] = new SqlParameter("@Username", SqlDbType.VarChar, 16);
                Parameter[4].Value = objAddVendors.Username;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddOtherVendors", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetVendors //Following functions will be used in Site and Contact Info
        public DataSet GetVendors(cAddVendors objAddVendors)
        {
            try
            {
                DataSet drVendors = new DataSet();
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objAddVendors.Account;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 100);
                Parameter[1].Value = objAddVendors.Campaign;

                drVendors = DBHelper.ExecuteQueryToDataSet("SelectOtherVendors", Parameter);

                return drVendors;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region AddProjects //Following functions will be used in Site and Contact Info
        public bool AddProjects(cProjects objProjects)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[7];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objProjects.Account;

                Parameter[1] = new SqlParameter("@Contact", SqlDbType.Int);
                Parameter[1].Value = objProjects.Contact;

                Parameter[2] = new SqlParameter("@Date", SqlDbType.DateTime);
                if (objProjects.ProjectDate == string.Empty)
                    Parameter[2].Value = SqlDateTime.Null;
                else
                    Parameter[2].Value = objProjects.ProjectDate;

                Parameter[3] = new SqlParameter("@ProjectType", SqlDbType.VarChar, 128);
                Parameter[3].Value = objProjects.ProjectType;

                Parameter[4] = new SqlParameter("@Chance", SqlDbType.VarChar, 12);
                Parameter[4].Value = objProjects.Chance;

                Parameter[5] = new SqlParameter("@EstimatedAmt", SqlDbType.Int);
                Parameter[5].Value = objProjects.EstimatedAmt;

                Parameter[6] = new SqlParameter("@Username", SqlDbType.VarChar, 16);
                Parameter[6].Value = objProjects.Username;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddProjects", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetProjects //Following functions will be used in Site and Contact Info
        public DataSet GetProjects(cProjects objProjects)
        {
            try
            {
                DataSet drVendors = new DataSet();
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objProjects.Account;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 100);
                Parameter[1].Value = objProjects.Campaign;

                drVendors = DBHelper.ExecuteQueryToDataSet("SelectProjects", Parameter);

                return drVendors;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region AddNewContacts //Following functions will be used in Site and Contact Info
        public bool AddNewContacts(cAddNewContact objAddNewContact)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[10];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objAddNewContact.Account;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[1].Value = objAddNewContact.Campaign;

                Parameter[2] = new SqlParameter("@Firstname", SqlDbType.VarChar,35);
                Parameter[2].Value = objAddNewContact.Firstname;

                Parameter[3] = new SqlParameter("@Surname", SqlDbType.VarChar, 35);
                Parameter[3].Value = objAddNewContact.Surname;

                Parameter[4] = new SqlParameter("@Title", SqlDbType.VarChar, 35);
                Parameter[4].Value = objAddNewContact.Title;

                Parameter[5] = new SqlParameter("@Email", SqlDbType.VarChar,60);
                Parameter[5].Value = objAddNewContact.Email;

                Parameter[6] = new SqlParameter("@Notes", SqlDbType.VarChar, 256);
                Parameter[6].Value = objAddNewContact.Notes;

                Parameter[7] = new SqlParameter("@Createdby", SqlDbType.VarChar, 12);
                Parameter[7].Value = objAddNewContact.Username;

                Parameter[8] = new SqlParameter("@Name", SqlDbType.VarChar, 35);
                Parameter[8].Value = objAddNewContact.Name;

                Parameter[9] = new SqlParameter("@Phone", SqlDbType.VarChar, 12);
                Parameter[9].Value = objAddNewContact.Phone;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddNewContact", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetSafetyProducts //Following functions will be used in Site and Contact Info
        public DataSet GetSafetyProducts(cSafetyProducts objSatefyProducts)
        {
            try
            {
                DataSet drVendors = new DataSet();
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objSatefyProducts.Account;

                Parameter[1] = new SqlParameter("@Contact", SqlDbType.Int);
                Parameter[1].Value = objSatefyProducts.Contact;

                Parameter[2] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[2].Value = objSatefyProducts.Campaign;

                drVendors = DBHelper.ExecuteQueryToDataSet("SelectProjects", Parameter);

                return drVendors;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region UpdateSafetyProducts //Following functions will be used in Site and Contact Info
        public bool UpdateSafetyProducts(cSafetyProducts objSafetyProducts)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[4];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objSafetyProducts.Account;

                Parameter[1] = new SqlParameter("@Contact", SqlDbType.Int);
                Parameter[1].Value = objSafetyProducts.Contact;

                Parameter[2] = new SqlParameter("@SP", SqlDbType.Int);
                Parameter[2].Value = objSafetyProducts.Sp;

                Parameter[3] = new SqlParameter("@Username", SqlDbType.VarChar, 16);
                Parameter[3].Value = objSafetyProducts.Username;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("UpdateSafetyProducts", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}