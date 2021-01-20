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
    public class cCampaign
    {
        private string mstrCampaignId;
        private string mstrCampaignName;

        public string CampaignID
        {
            get { return mstrCampaignId; }
            set { mstrCampaignId = value; }
        }
        public string CampaignName
        {
            get { return mstrCampaignName; }
            set { mstrCampaignName = value; }
        }

        //public SqlDataReader GetCampaignList()
        //{
        //    cCampaignDB objCampaignDB = new cCampaignDB();
        //    try
        //    {
        //        return objCampaignDB.GetCampaignList(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        public DataSet GetCampaignList()
        {
            cCampaignDB objCampaignDB = new cCampaignDB();
            try
            {
                return objCampaignDB.GetCampaignList(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }



        public DataSet GetCampaignList1()
        {
            cCampaignDB objCampaignDB = new cCampaignDB();
            try
            {
                return objCampaignDB.GetCampaignList1(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetAllCampaignList()
        {
            cCampaignDB objCampaignDB = new cCampaignDB();
            try
            {
                return objCampaignDB.GetAllCampaignList(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        
    }
}