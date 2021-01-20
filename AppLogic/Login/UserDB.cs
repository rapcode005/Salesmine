using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using DataAccess;
using BradyCorp.Log;


namespace AppLogic
{
    public class cUserDB
    {
        string strQuery;

        #region GetUsers
        public SqlDataReader GetUsers(cUser objUsers)
        {
            try
            {
                //SqlDataReader drUsers;
               
                SqlParameter[] Parameter = new SqlParameter[5];
                Parameter[0] = new SqlParameter("@username", SqlDbType.NVarChar, 100);
                Parameter[0].Value = objUsers.UserName;

                Parameter[1] = new SqlParameter("@CAMPAIGN", SqlDbType.NVarChar, 100);
                Parameter[1].Value = objUsers.CampaignName;

                              

                Parameter[2] = new SqlParameter("@VALID_TO", SqlDbType.DateTime);
                Parameter[2].Value = "9999-12-31 00:00:00.000";

                //if (objUsers.ValidFrom != null)
                //{
                //    DateTime dt = Convert.ToDateTime(objUsers.ValidTo);
                //    Parameter[2].Value = dt.ToLongDateString();
                //}
                //else
                //{
                //    Parameter[2].Value = DBNull.Value;

                //}

                Parameter[3] = new SqlParameter("@KamId", SqlDbType.NVarChar, 100);
                Parameter[3].Value = objUsers.KamId;

                Parameter[4] = new SqlParameter("@KamName", SqlDbType.NVarChar, 100);
                Parameter[4].Value = objUsers.KamName;

                
              

                //drUsers = DBHelper.ExecuteSqlDataReader("usp_USER_PROFILESelect", Parameter);

                //return DBHelper.ExecuteSqlDataReader("usp_USER_PROFILESelect", Parameter);

                return DBHelper.ExecuteSqlDataReader("usp_USER_PROFILESelect", Parameter);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex,"User Login - usp_USER_PROFILESelect", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                //BradyCorp.Log.LoggerHelper.LogMessage("User Login - usp_USER_PROFILESelect" + ex.ToString());
                throw new Exception(ex.Message, ex);
            }
           
        }
        #endregion

        #region DeleteUsers //Use this Method to delete a user from the system

        public bool DeleteUser(cUser objUsers)
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            try
            {
                SqlParameter[] Parameter = new SqlParameter[1];
                Parameter[0] = new SqlParameter("@username", SqlDbType.VarChar, 20);
                Parameter[0].Value = objUsers.DeleteUserName;
                int NoOfRecods = DBHelper.ExecuteNonQuery("usp_USER_PROFILEDelete", Parameter);
                //int NoOfRecods = DBHelper.ExecuteNonQuery("usp_USER_PROFILEDelete", Parameter); //SqlHelper.ExecuteNonQuery(Constre, CommandType.StoredProcedure, "usp_USER_PROFILEDelete", Parameter);
                return NoOfRecods == 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
            
        }
        #endregion

        #region  UpdateUsers    //Use this Method to Update a user from the system
            public bool UpdateUsers(cUser objUsers)
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            try
            {
                SqlParameter[] Parameter = new SqlParameter[6];
                Parameter[0] = new SqlParameter("@username", SqlDbType.VarChar, 20);
                Parameter[0].Value = objUsers.UserName; ;

                Parameter[1] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 20);
                Parameter[1].Value = objUsers.CampaignName;

                Parameter[2] = new SqlParameter("@KamId", SqlDbType.VarChar, 100);
                Parameter[2].Value = objUsers.KamId;

                Parameter[3] = new SqlParameter("@KamName", SqlDbType.VarChar, 64);
                Parameter[3].Value = objUsers.KamName;

                Parameter[4] = new SqlParameter("@USERROLE", SqlDbType.VarChar, 50);
                Parameter[4].Value = objUsers.UserRole;

                Parameter[5] = new SqlParameter("@CREATEDEDITEDBY", SqlDbType.VarChar, 20);
                Parameter[5].Value = objUsers.CreatedEditedBy;

                //int NoOfRecods = SqlHelper.ExecuteNonQuery(Constre, CommandType.StoredProcedure, "usp_USER_PROFILEUpdate", Parameter);
                //int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("usp_USER_PROFILEUpdate", Parameter);
                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("usp_USER_PROFILEUpdate", Parameter);
                return NoOfUpdatedRecords == 1 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
           
        }
        #endregion

        #region GetUserList //Following functions will be used in Manage User Page
        /// <summary>
        
        /// </summary>
        /// <param name="objUsers"></param>
        /// <returns></returns>

        public DataSet GetUserList(cUser objUsers)
        {
            try
            {
                DataSet drUsers = null;

                SqlParameter[] Parameter = new SqlParameter[2];
                Parameter[0] = new SqlParameter("@username", SqlDbType.VarChar, 20);
                Parameter[0].Value = objUsers.SearchManageUserByUserName;

                Parameter[1] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 20);
                Parameter[1].Value = objUsers.SearchManageUserByCampaign;
                //drUsers = DBHelper.ExecuteQueryToDataSet("sp_UserSelect", Parameter);
                drUsers = DBHelper.ExecuteQueryToDataSet("sp_UserSelect", Parameter);

                return drUsers;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "User Login - sp_UserSelect", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                //BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region InserUsers //Use this to Insert Users in to System
        public bool InsertUser(cUser objUsers)
        {
            
            try
            {
               SqlParameter[] Parameter = new SqlParameter[6];
                Parameter[0] = new SqlParameter("@username", SqlDbType.VarChar, 20);
                Parameter[0].Value = objUsers.UserName; ;

                Parameter[1] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 20);
                Parameter[1].Value = objUsers.CampaignName;

                Parameter[2] = new SqlParameter("@KamId", SqlDbType.VarChar, 100);
                Parameter[2].Value = objUsers.KamId;

                Parameter[3] = new SqlParameter("@KamName", SqlDbType.VarChar, 64);
                Parameter[3].Value = objUsers.KamName;

                Parameter[4] = new SqlParameter("@USERROLE", SqlDbType.VarChar, 50);
                Parameter[4].Value = objUsers.UserRole;

                Parameter[5] = new SqlParameter("@CREATEDEDITEDBY", SqlDbType.VarChar, 20);
                Parameter[5].Value = objUsers.CreatedEditedBy;

                

                //int NoOfInsertedRecords = DBHelper.ExecuteNonQuery("usp_USER_PROFILEInsert", Parameter);
                int NoOfInsertedRecords = DBHelper.ExecuteNonQuery("usp_USER_PROFILEInsert", Parameter);
                return NoOfInsertedRecords == 1 ? true : false;
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        }
        #endregion

        #region InserAT //Use this to Insert Audit Trail in to System
        public bool InsertAT(cUser objUsers)
        {

            try
            {
                SqlParameter[] Parameter = new SqlParameter[3];
                Parameter[0] = new SqlParameter("@username", SqlDbType.VarChar, 50);
                Parameter[0].Value = objUsers.UserName;

                Parameter[1] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 10);
                Parameter[1].Value = objUsers.CampaignName;

                Parameter[2] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
                Parameter[2].Value = objUsers.password;

                int NoOfInsertedRecords = DBHelper.ExecuteNonQuery("InsertAT", Parameter);
                return NoOfInsertedRecords > 0 ? true : false;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        }
        #endregion

        #region Get Roles //Used in Manage Users Page
        public DataSet GetRoleList(cUser objUsers)
        {
            try
            {
                DataSet drUserRole = null;
                try
                {
                    drUserRole = DBHelper.ExecuteSPWithoutParameter("usp_USER_PROFILESelectRole");
                    return drUserRole;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString(), ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region AddCutomizedScreen //Use this to Insert what page you only want to view in to System
        public bool AddCutomizedScreen(cUser objPageName)
        {
             
            try
            {
                SqlParameter[] Parameter = new SqlParameter[4];

                Parameter[0] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
                Parameter[0].Value = objPageName.createdby;

                Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 250);
                Parameter[1].Value = objPageName.campaignFullName;

                Parameter[2] = new SqlParameter("@CampaignValue", SqlDbType.VarChar, 200);
                Parameter[2].Value = objPageName.campaignUnit;

                Parameter[3] = new SqlParameter("@PageName", SqlDbType.VarChar, 250);
                Parameter[3].Value = objPageName.PageNameList;

                int NoOfInsertedRecords = DBHelper.ExecuteNonQuery("AddCustomizedPageShow", Parameter);
                return NoOfInsertedRecords == 1 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        }
        #endregion

        #region SelectCustomizedScreen // Use this to get the list of page you want to view in to System
        public DataSet SelectCustomizedScreen(cUser objSelect)
        {
            try
            {
                DataSet dsSelect = new DataSet();
                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@Createdby", SqlDbType.VarChar, 10);
                Parameter[0].Value = objSelect.createdby;

                dsSelect = DBHelper.ExecuteQueryToDataSet("GetCustomizedPageList", Parameter);

                return dsSelect;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message.ToString(), ex);
            }
        }
        #endregion

        #region GetAllPageList //Used in Manage Users Page
        public DataSet GetAllPageList(cUser objUsers)
        {
            try
            {
                DataSet drPageList = null;
                try
                {
                    drPageList = DBHelper.ExecuteSPWithoutParameter("usp_USER_PROFILESelectPage");
                    return drPageList;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString(), ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region GetCurrencyCode //Used in Master Page to get  Currency Code from Campaign Value
        public DataSet GetCurrencyCode(cUser objCode)
        {
            try
            {
                DataSet dsSelect = new DataSet();
                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@campaign", SqlDbType.VarChar, 10);
                Parameter[0].Value = objCode.campaignUnit;

                dsSelect = DBHelper.ExecuteQueryToDataSet("GetCurrencyCode", Parameter);

                return dsSelect;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public DataSet AllGetCurrencyCode()
        {
            try
            {
                DataSet dsSelect = new DataSet();

                dsSelect = DBHelper.ExecuteSPWithoutParameter("AllGetCurrencyCode");

                return dsSelect;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region InsetAuditTrailSalesMine //Use this to Insert Audit Trail in to SalesMine Database
        public bool InsetAuditTrailSalesMine(cUser objUsers)
        {

            try
            {
                SqlParameter[] Parameter = new SqlParameter[5];

                Parameter[0] = new SqlParameter("@Page", SqlDbType.VarChar, 50);
                Parameter[0].Value = objUsers.PageAT;

                Parameter[1] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 20);
                Parameter[1].Value = objUsers.CampaignAT;

                Parameter[2] = new SqlParameter("@Task", SqlDbType.VarChar, 100);
                Parameter[2].Value = objUsers.TaskAT;

                Parameter[3] = new SqlParameter("@Createdby", SqlDbType.VarChar, 10);
                Parameter[3].Value = objUsers.CreatedbyAT;

                Parameter[4] = new SqlParameter("@Description", SqlDbType.VarChar, 250);
                Parameter[4].Value = objUsers.DescriptionAT;

                int NoOfInsertedRecords = DBHelper.ExecuteNonQuery("InsertAuidiTrailSalesMine", Parameter);
                return NoOfInsertedRecords == 1 ? true : false;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        }
        #endregion

        #region EditMessageEmail //Use this to Edit Email Body
        public bool EditMessageEmail(cUser objEmail)
        {

            try
            {
                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Message", SqlDbType.VarChar, 250);
                Parameter[0].Value = objEmail.EmailMessage;

                Parameter[1] = new SqlParameter("@Createdby", SqlDbType.VarChar, 10);
                Parameter[1].Value = SessionFacade.LoggedInUserName;

                int NoOfInsertedRecords = DBHelper.ExecuteNonQuery("EditMessageEmail", Parameter);
                return NoOfInsertedRecords == 1 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        } 
        #endregion

        #region ShowMessageEmail //Use this to Show Email Body
        public DataSet ShowMessageEmail()
        {

            try
            {
                DataSet ds = new DataSet();

                ds = DBHelper.ExecuteSPWithoutParameter("SelectUserEmail");
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        }
        #endregion

        #region ShowAllNoteType //Use this to Manage Note Type
        public DataSet ShowAllNoteType()
        {

            try
            {
                DataSet ds = new DataSet();

                ds = DBHelper.ExecuteSPWithoutParameter("SelectAllNoteType");
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        }
        #endregion
    }
}