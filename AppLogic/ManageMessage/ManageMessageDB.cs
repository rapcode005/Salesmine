using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using DataAccess;
using BradyCorp.Log;
using System.Data;
using System.Configuration;
namespace AppLogic
{
    public class cManageMessageDB
    {
        #region GetMessage //Following functions will be used in Manage Message Page
        public DataSet GetMessage()
        {
            try
            {
                DataSet drManageMessage = null;

                drManageMessage = DBHelper.ExecuteSPWithoutParameter("GetMessage");

                return drManageMessage;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "GetMessage", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region AddNewStateMessage //Following functions will be used in Manage Message Page
        public bool AddNewStateMessage(cManageMessage objManageMessage)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[5];

                Parameter[0] = new SqlParameter("@Message", SqlDbType.VarChar, 300);
                Parameter[0].Value = objManageMessage.Message;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 12);
                Parameter[1].Value = objManageMessage.Campaign;

                Parameter[2] = new SqlParameter("@EffDate", SqlDbType.Date);
                Parameter[2].Value = DateTime.Parse(objManageMessage.Date);

                Parameter[3] = new SqlParameter("@State", SqlDbType.VarChar, 10);
                Parameter[3].Value = objManageMessage.State;

                Parameter[4] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
                Parameter[4].Value = objManageMessage.Username;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddStateMessage", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region EditNewStateMessage //Following functions will be used in Manage Message Page
        public bool EditNewStateMessage(cManageMessage objManageMessage)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[7];

                Parameter[0] = new SqlParameter("@Message", SqlDbType.VarChar, 300);
                Parameter[0].Value = objManageMessage.Message;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 12);
                Parameter[1].Value = objManageMessage.Campaign;

                Parameter[2] = new SqlParameter("@EffDate", SqlDbType.Date);
                Parameter[2].Value = DateTime.Parse(objManageMessage.Date);

                Parameter[3] = new SqlParameter("@State", SqlDbType.VarChar, 10);
                Parameter[3].Value = objManageMessage.State;

                Parameter[4] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
                Parameter[4].Value = objManageMessage.Username;

                Parameter[5] = new SqlParameter("@PreviousState", SqlDbType.VarChar, 10);
                Parameter[5].Value = objManageMessage.PreState;

                Parameter[6] = new SqlParameter("@PreviousCampaign", SqlDbType.VarChar, 12);
                Parameter[6].Value = objManageMessage.PreCampaign;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("EditStateMessage", Parameter);
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