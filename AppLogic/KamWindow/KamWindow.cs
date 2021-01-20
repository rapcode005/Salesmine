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
    public class cKamWindow
    {
       
        //private string mstrKamID;
        //private string mstrKamSearchBy;
        //private string mstrKamSearchByKamName;
        //private string mstrKamSearchByKamMGName;
        //private string mstrKamSearchBySalesTeam;
        
        //private string mstrCampaignName;

        public string KamSearch
        {
            get;
            set;
        }
        public string KamID
        {
            get;
            set;
        }

        public string KamCampaign
        {
            get;
            set;
        }

        //public DataSet GetKamData()
        //{
        //    cKamWindowDB objProductSummaryDB = new cKamWindowDB();
        //    try
        //    {
        //        return objProductSummaryDB.GetKamData(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}
    }
}