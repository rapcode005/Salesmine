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
    public class NewBuyerSince
    {
        public string CampaignName { get; set; }
        public string fpdcont { get; set; }
        public string SalesTeam { get; set; }

        public DataSet GetNewBuyer()
        {
            CustomerLookupDB objCustomerLookupDB = new CustomerLookupDB();
            try
            {
                return objCustomerLookupDB.GetNewBuyer(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }

    public class CustomerLookup
    {
        private string CLCampaignName;
        private string CLTxtSearch;
        private string CLSearchType;
        private string Buyerct;


        public string CampaignName
        {
            get { return CLCampaignName; }
            set { CLCampaignName = value; }
        }

        public string TextSearch
        {
            get { return CLTxtSearch; }
            set { CLTxtSearch = value; }
        }

        public string SearchType
        {
            get { return CLSearchType; }
            set { CLSearchType = value; }
        }


        public string BuyerCT
        {
            get { return Buyerct; }
            set { Buyerct = value; }
        }

        public  string valContactLevel
        {
            get { return valContactLevel; }
            set { valContactLevel = value; }
        }

        public Boolean chkContacLevel
        {
            get { return chkContacLevel; }
            set { chkContacLevel = value; }
        }

        public DataSet GetCustomerLookUp()
        {

            CustomerLookupDB objCustomerLookupDB = new CustomerLookupDB();
            try
            {
                return objCustomerLookupDB.GetCustomerLookUp(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


    }
}