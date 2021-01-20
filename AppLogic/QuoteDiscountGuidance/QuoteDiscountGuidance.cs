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
    public class cQuoteDiscountGuidance
    {
        public string Campaign { get; set; }
        public string Account { get; set; }
        public string Reseller { get; set; }
        public string ETO { get; set; }
        public string Government { get; set; }

        #region GetQuoteDiscountGuidance
        public SqlDataReader GetQuoteDiscountGuidance()
        {
            cQuoteDiscountGuidanceDB objQuoteDiscountGuidanceDB = new cQuoteDiscountGuidanceDB();
            try
            {
                return objQuoteDiscountGuidanceDB.GetQuoteDiscountGuidance(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region AddQuoteDiscountGuidance
        public bool AddQuoteDiscountGuidance()
        {
            cQuoteDiscountGuidanceDB objQuoteDiscountGuidanceDB = new cQuoteDiscountGuidanceDB();
            try
            {
                return objQuoteDiscountGuidanceDB.AddQuoteDiscountGuidance(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}