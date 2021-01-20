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
    public class cQuoteOver1K
    {
        public string Campaign { get; set; }
        public string Probablilty { get; set; }
        public string Weighted { get; set; }
        public string ProposedDate { get; set; }
        public string Competition { get; set; }
        public string Notes { get; set; }
        public string Quote_Doc { get; set; }
        public string AccountNum { get; set; }
        public string Createdby { get; set; }
        public string WinorLoss { get; set; }
        public string Status { get; set; }

        public string QuoteDay { get; set; }
        public string AccountName { get; set; }
        public string QSTCurrent { get; set; }
        public string QSTIn { get; set; }
        public string QuoteValue { get; set; }
        public string QuoteCost { get; set; }
        public string QuoteGMPerc { get; set; }

        public string ScheDate { get; set; }
        public string Mining { get; set; }
        public string Construction { get; set; }
        public string ProductLine { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string RepName { get; set; }

        #region GetQuoteOver1K
        public DataSet GetQuoteOver1K()
        {
            cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
            try
            {
                return objQuoteOver1KDB.GetQuoteOver1K(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetQuoteOver1K_New()
        {
            cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
            try
            {
                return objQuoteOver1KDB.GetQuoteOver1K_New(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //public DataSet GetQuoteOver1K_New_DateRange()
        //{
        //    cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
        //    try
        //    {
        //        return objQuoteOver1KDB.GetQuoteOver1K_New_DateRange(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        //public DataSet GetQuoteOver1K_New_Reps()
        //{
        //    cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
        //    try
        //    {
        //        return objQuoteOver1KDB.GetQuoteOver1K_New_Reps(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        #endregion

        #region GetQuoteOver1KWonorLoss()
        public DataSet GetQuoteOver1KWonorLoss()
        {
            cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
            try
            {
                return objQuoteOver1KDB.GetQuoteOver1KWonorLoss(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetQuoteOver1KWonorLoss_New()
        {
            cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
            try
            {
                return objQuoteOver1KDB.GetQuoteOver1KWonorLoss_New(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //public DataSet GetQuoteOver1KWonorLoss_New_DateRange()
        //{
        //    cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
        //    try
        //    {
        //        return objQuoteOver1KDB.GetQuoteOver1KWonorLoss_New_DataRange(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        //public DataSet GetQuoteOver1KWonorLoss_New_Reps()
        //{
        //    cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
        //    try
        //    {
        //        return objQuoteOver1KDB.GetQuoteOver1KWonorLoss_New_Reps(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}
        #endregion

        #region AddQuoteComputing
        public bool AddQuoteComputing()
        {
            cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
            try
            {
                return objQuoteOver1KDB.AddQuoteComputing(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region AddQuoteComputingWinOrLoss
        public bool AddQuoteComputingWinOrLoss()
        {
            cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
            try
            {
                return objQuoteOver1KDB.AddQuoteComputingWinOrLoss(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region AddNewQuote
        public bool AddNewQuote()
        {
            cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
            try
            {
                return objQuoteOver1KDB.AddNewQuote(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region EditQuoteValue
        public bool EditQuoteValue()
        {
            cQuoteOver1KDB objQuoteOver1KDB = new cQuoteOver1KDB();
            try
            {
                return objQuoteOver1KDB.EditQuoteValue(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}