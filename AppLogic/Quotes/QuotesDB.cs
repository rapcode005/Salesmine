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
    public class cQuotesDB
    {
        #region GetListQuotes //Following functions will be used in Quotes Page
        public DataTable GetListQuotes(cQuotes objQuotes)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataTable drQuotes = null;

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                Parameter[0].Value = objQuotes.SearchQuoteByAccount;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar,100);
                Parameter[1].Value = objQuotes.SearchQuoteByCampaign;

                drQuotes = DBHelper.ExecuteQueryToDataTable("spNewQuotes", Parameter);
                
                return drQuotes;
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