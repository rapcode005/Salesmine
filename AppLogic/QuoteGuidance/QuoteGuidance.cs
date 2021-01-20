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

namespace AppLogic
{
    public class cQuoteGuidance
    {
        public string ProdLine { get; set; }
        public string QuoteType { get; set; }
        public string QuoteBucket { get; set; }
        public string CustType { get; set; }
        public string Account { get; set; }
        public string Contact { get; set; }
        public string QuoteNumber { get; set; }
        public string Campaign { get; set; }
        public string MaterialEntrd { get; set; }

        #region GetCustomerType
        public DataSet GetCustomerType()
        {
            cQuoteGuidanceDB objQuoteGuidanceDB = new cQuoteGuidanceDB ();
            try
            {
                return objQuoteGuidanceDB.GetCustomerType(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetProductLine
        public DataSet GetProductLine()
        {
            cQuoteGuidanceDB objQuoteGuidanceDB = new cQuoteGuidanceDB();
            try
            {
                return objQuoteGuidanceDB.GetProductLine(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetQuoteBucket
        public DataSet GetQuoteBucket()
        {
            cQuoteGuidanceDB objQuoteGuidanceDB = new cQuoteGuidanceDB();
            try
            {
                return objQuoteGuidanceDB.GetQuoteBucket(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetQuoteType
        public DataSet GetQuoteType()
        {
            cQuoteGuidanceDB objQuoteGuidanceDB = new cQuoteGuidanceDB();
            try
            {
                return objQuoteGuidanceDB.GetQuoteType(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region SelectQuoteDiscountGuidance
        public DataSet SelectQuoteDiscountGuidance()
        {
            cQuoteGuidanceDB objQuoteGuidanceDB = new cQuoteGuidanceDB();
            try
            {
                return objQuoteGuidanceDB.SelectQuoteDiscountGuidance(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

    }
}