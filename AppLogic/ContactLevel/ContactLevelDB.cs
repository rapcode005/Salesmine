using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BradyCorp.Log;

namespace AppLogic
{
    public class cContactLevelDB
    {
        #region GetListContactLevel //Following functions will be used in Order History Page
        public DataSet GetListContactLevel(cContactLevel objContactLevel)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drContactLevel = null;

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objContactLevel.SearchAccount;

                Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                Parameter[1].Value = objContactLevel.SearchCampaign;

                switch (objContactLevel.Page)
                {
                    case "Quotes":
                        drContactLevel = DBHelper.ExecuteQueryToDataSet("ContactLevelQuote", Parameter);
                        break;
                    case "Orders":
                        drContactLevel = DBHelper.ExecuteQueryToDataSet("ContactLevelOrder", Parameter);
                        break;
                    case "Product" :
                        drContactLevel = DBHelper.ExecuteQueryToDataSet("ContactLevelProduct", Parameter);
                        break;
                    default:
                        drContactLevel = DBHelper.ExecuteQueryToDataSet("ContactLevelNotes", Parameter);
                        break;
                }

                return drContactLevel;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "GetListContactLevel Function in ContactLevelDB", 
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                    SessionFacade.KamId.ToString());
                //LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetListContactLevelNeW(cContactLevel objContactLevel)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                //DataSet drContactLevel = null;

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objContactLevel.SearchAccount;

                Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                Parameter[1].Value = objContactLevel.SearchCampaign;

                //switch (objContactLevel.Page)
                //{
                //    case "Quotes":
                //        drContactLevel = DBHelper.ExecuteQueryToDataSet("ContactLevelQuote", Parameter);
                //        break;
                //    case "Orders":
                //        drContactLevel = DBHelper.ExecuteQueryToDataSet("ContactLevelOrder", Parameter);
                //        break;
                //    case "Product":
                //        drContactLevel = DBHelper.ExecuteQueryToDataSet("ContactLevelProduct", Parameter);
                //        break;
                //    default:
                //        drContactLevel = DBHelper.ExecuteQueryToDataSet("ContactLevelNotes", Parameter);
                //        break;
                //}

                //drContactLevel = DBHelper.ExecuteQueryToDataSet(ContactLevelFunc, Parameter);

                return DBHelper.ExecuteQueryToDataSet("ContactLevel" + objContactLevel.Page, Parameter);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "GetListContactLevel Function in ContactLevelDB",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(),
                    SessionFacade.KamId.ToString());
                //LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

    }
}