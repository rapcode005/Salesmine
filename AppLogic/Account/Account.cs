using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace AppLogic
{
    public class cAccount
    {
        private string mstrSoldTo; // This is Account Number
        private string mstrAccountName;
        private string mstrMGName;
        private string mstrCampaignName;

        public string CampaignName
        {
            get { return mstrCampaignName; }
            set { mstrCampaignName = value; }
        }

        public string SoldTo
        {
            get { return mstrSoldTo; }
            set { mstrSoldTo = value; }
        }
        public string AccountName
        {
            get { return mstrAccountName; }
            set { mstrAccountName = value; }
        }

        //public SqlDataReader GetAccountNameOnly()
        //{
        //    cAccountDB objAccountDB = new cAccountDB();
        //    try
        //    {
        //        return objAccountDB.GetAccountNameOnly(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        public DataSet GetAccountNameOnly()
        {
            cAccountDB objAccountDB = new cAccountDB();
            try
            {
                return objAccountDB.GetAccountNameOnly(this);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex, "GetAccountNameOnly-Account", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                throw new Exception(ex.Message, ex);
            }
        } 
    }
}