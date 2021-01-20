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
    public class cQuoteGuidanceDB
    {
        #region GetCustomerType // used in Quote DG Page
        public DataSet GetCustomerType(cQuoteGuidance objQuoteGuidance)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drCustomerType = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
                Parameter[0].Value = objQuoteGuidance.Campaign;

                drCustomerType = DBHelper.ExecuteQueryToDataSet("usp_SelectCustomerType",Parameter);

                return drCustomerType;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region GetProductLine // used in Quote DG Page
        public DataSet GetProductLine(cQuoteGuidance objQuoteGuidance)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drProductLine = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[1];
                
                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
                Parameter[0].Value = objQuoteGuidance.Campaign;

                drProductLine = DBHelper.ExecuteQueryToDataSet("usp_SelectProductLine",Parameter);

                return drProductLine;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region GetQuoteBucket // used in Quote DG Page
        public DataSet GetQuoteBucket(cQuoteGuidance objQuoteGuidance)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drQuoteBucket = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
                Parameter[0].Value = objQuoteGuidance.Campaign;

                drQuoteBucket = DBHelper.ExecuteQueryToDataSet("usp_SelectQuoteBucket",Parameter);

                return drQuoteBucket;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region GetQuoteType // used in Quote DG Page
        public DataSet GetQuoteType(cQuoteGuidance objQuoteGuidance)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drQuoteType = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
                Parameter[0].Value = objQuoteGuidance.Campaign;

                drQuoteType = DBHelper.ExecuteQueryToDataSet("usp_SelectQuoteType", Parameter);

                return drQuoteType;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region SelectQuoteDiscountGuidancee // used in Quote DG Page
        public DataSet SelectQuoteDiscountGuidance(cQuoteGuidance objQuoteGuidance)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet dsQuoteDiscountGuidance = new DataSet();

                SqlParameter[] Parameter = null;

                if (objQuoteGuidance.Campaign != "EMED" &&
                        objQuoteGuidance.Campaign != "CA" &&
                        objQuoteGuidance.Campaign != "US" &&
                        objQuoteGuidance.Campaign != "CL")
                {
                    Parameter = new SqlParameter[9];
                }
                else
                    Parameter = new SqlParameter[8];

                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
                Parameter[0].Value = objQuoteGuidance.Campaign;

                Parameter[1] = new SqlParameter("@CustomerType", SqlDbType.VarChar, 20);
                Parameter[1].Value = objQuoteGuidance.CustType;

                Parameter[2] = new SqlParameter("@ProductLine", SqlDbType.VarChar, 100);
                Parameter[2].Value = objQuoteGuidance.ProdLine;

                Parameter[3] = new SqlParameter("@Quote_Bucket", SqlDbType.VarChar, 16);
                Parameter[3].Value = objQuoteGuidance.QuoteBucket;

                Parameter[4] = new SqlParameter("@QuoteType", SqlDbType.VarChar, 40);
                Parameter[4].Value = objQuoteGuidance.QuoteType;

                Parameter[5] = new SqlParameter("@Account" , SqlDbType.VarChar, 15);
                Parameter[5].Value = objQuoteGuidance.Account;

                Parameter[6] = new SqlParameter("@Contact" , SqlDbType.Int);
                Parameter[6].Value = objQuoteGuidance.Contact;

                Parameter[7] = new SqlParameter("@Doc_Number", SqlDbType.VarChar, 15);
                Parameter[7].Value = objQuoteGuidance.QuoteNumber;

                 if (objQuoteGuidance.Campaign != "EMED" &&
                        objQuoteGuidance.Campaign != "CA" &&
                        objQuoteGuidance.Campaign != "US" &&
                        objQuoteGuidance.Campaign != "CL")
                {
                    Parameter[8] = new SqlParameter("@Material_Entrd", SqlDbType.VarChar, 100);
                    Parameter[8].Value = objQuoteGuidance.MaterialEntrd.ToString().ToUpper().Trim();
                }

                dsQuoteDiscountGuidance = DBHelper.ExecuteQueryToDataSet("SelectNewQuoteDiscountGuidancev3", Parameter);

                return dsQuoteDiscountGuidance;
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