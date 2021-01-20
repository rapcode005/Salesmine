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
    public class cChatMessageDB
    {
        #region ChatMessage //Following functions will be used in Chat Page
        public bool WriteMessage(cChatMessage objMessage)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
                Parameter[0].Value = SessionFacade.LoggedInUserName;

                Parameter[1] = new SqlParameter("@Message", SqlDbType.VarChar, 250);
                Parameter[1].Value = objMessage.Message;

                Parameter[2] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
                Parameter[2].Value = SessionFacade.CampaignName; //objProjectStatus.varProjectStatus;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("WriteMessage", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public DataTable GetMessage(cChatMessage objMessage)
        {
            try
            {
                DataTable dtMessage = new DataTable();

                //SqlParameter[] Parameter = new SqlParameter[2];
                //Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                //Parameter[1].Value = objConstruction.SearchOrderCampaignName;

                dtMessage = DBHelper.ExecuteSPWithoutParameter("GetChatMessage").Tables[0];

                return dtMessage;
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