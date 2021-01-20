using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using BradyCorp.Log;

namespace AppLogic
{
    public class cOnHoldOrderDB
    {
        #region GetOrderOnHold //Following functions will be used in On Hold Order Page to search
        public DataSet GetOrderOnHold(cOnHoldOrder objOnHoldOrder)
        {
            try
            {
                DataSet drOrderHistory = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@SoldTo", SqlDbType.VarChar, 15);
                Parameter[0].Value = objOnHoldOrder.Account;

                Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                Parameter[1].Value = objOnHoldOrder.Campaign;

                drOrderHistory = DBHelper.ExecuteQueryToDataSet("SelectOrdersOnHold", Parameter);

                return drOrderHistory;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "SelectOrdersOnHold", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetOrderOnHoldByKamID //Following functions will be used in On Hold Order Page to search
        public DataSet GetOrderOnHoldByKamID(cOnHoldOrder objOnHoldOrder)
        {
            try
            {
                DataSet drOrderHistory = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@SalesTeam", SqlDbType.VarChar, 100);
                Parameter[0].Value = objOnHoldOrder.Salesteam;

                Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                Parameter[1].Value = objOnHoldOrder.Campaign;

                drOrderHistory = DBHelper.ExecuteQueryToDataSet("SelectOrdersOnHoldKamID", Parameter);

                return drOrderHistory;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "SelectOrdersOnHoldKamID", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetOrderOnHoldAll //Following functions will be used in On Hold Order Page to search
        public DataSet GetOrderOnHoldAll(cOnHoldOrder objOnHoldOrder)
        {
            try
            {
                DataSet drOrderHistory = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                Parameter[0].Value = objOnHoldOrder.Campaign;

                drOrderHistory = DBHelper.ExecuteQueryToDataSet("SelectOrdersOnHoldAll", Parameter);

                return drOrderHistory;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "SelectOrdersOnHoldAll", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}