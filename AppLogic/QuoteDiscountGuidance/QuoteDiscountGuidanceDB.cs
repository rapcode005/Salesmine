using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BradyCorp.Log;
using DataAccess;
namespace AppLogic
{
    public class cQuoteDiscountGuidanceDB
    {
        #region GetQuoteDiscountGuidance // used in Quote Page
        public SqlDataReader GetQuoteDiscountGuidance(cQuoteDiscountGuidance objQuoteDiscountGuidance)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlDataReader sqlQuoteDiscountGuidance = null;

                SqlParameter[] Parameter = new SqlParameter[2];
                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[0].Value = objQuoteDiscountGuidance.Campaign;

                Parameter[1] = new SqlParameter("@AccountNum", SqlDbType.VarChar, 10);
                Parameter[1].Value = objQuoteDiscountGuidance.Account;

                sqlQuoteDiscountGuidance = DBHelper.ExecuteSqlDataReader("GetQuoteDiscount", Parameter);

                return sqlQuoteDiscountGuidance;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region AddQuoteDiscountGuidance // used in Quote Page
        public bool AddQuoteDiscountGuidance(cQuoteDiscountGuidance objQuoteDiscountGuidance)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());

                SqlParameter[] Parameter = new SqlParameter[5];

                Parameter[0] = new SqlParameter("@AccountNum", SqlDbType.VarChar,10);
                Parameter[0].Value = objQuoteDiscountGuidance.Account;

                Parameter[1] = new SqlParameter("@Reseller", SqlDbType.Bit);
                if (objQuoteDiscountGuidance.Reseller == "Yes")
                    Parameter[1].Value = true;
                else if (objQuoteDiscountGuidance.Reseller == "No")
                    Parameter[1].Value = false;
                else
                    Parameter[1].Value = null;

                Parameter[2] = new SqlParameter("@ETO", SqlDbType.Bit);
                if (objQuoteDiscountGuidance.ETO == "Yes")
                    Parameter[2].Value = true;
                else if (objQuoteDiscountGuidance.ETO == "No")
                    Parameter[2].Value = false;
                else
                    Parameter[2].Value = null;

                Parameter[3] = new SqlParameter("@Government", SqlDbType.Bit);
                if (objQuoteDiscountGuidance.Government == "Yes")
                    Parameter[3].Value = true;
                else if (objQuoteDiscountGuidance.Government == "No")
                    Parameter[3].Value = false;
                else
                    Parameter[3].Value = null;

                Parameter[4] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
                Parameter[4].Value = objQuoteDiscountGuidance.Campaign;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("CQuoteDiscountGuidance", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion
    }
}