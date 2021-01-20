using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using DataAccess;
using BradyCorp.Log;


namespace AppLogic
{
    public class CustomerLookupDB
    {
       
        public DataSet GetCustomerLookUp(CustomerLookup  vCampaignName)
        {
            try
            {

                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaOnlineCONNECTON"].ToString());
                DataSet drCustomerLookUp = null;

                SqlParameter[] Parameter = new SqlParameter[5];
                Parameter[0] = new SqlParameter("@filterby", SqlDbType.VarChar, 20);
                Parameter[0].Value = vCampaignName.CampaignName;
                Parameter[1] = new SqlParameter("@filtertype", SqlDbType.VarChar, 20);
                Parameter[1].Value = vCampaignName.SearchType;
                Parameter[2] = new SqlParameter("@filtertxt", SqlDbType.VarChar, 20);
                Parameter[2].Value = vCampaignName.TextSearch ;
                Parameter[3] = new SqlParameter("@Username", SqlDbType.VarChar, 20);
                Parameter[3].Value = SessionFacade.LoggedInUserName;
                Parameter[4] = new SqlParameter("@Listview", SqlDbType.VarChar, 30);
                Parameter[4].Value = "lvwLookupData";

                drCustomerLookUp = DBHelper.ExecuteQueryToDataSet("sp_CustomerLookup_Coalesce", Parameter);

                return drCustomerLookUp;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }

        public DataSet GetNewBuyer(NewBuyerSince NewBuyerSince)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaOnlineCONNECTON"].ToString());
                DataSet drCustomerLookUp = null;

                SqlParameter[] Parameter = new SqlParameter[5];
                Parameter[0] = new SqlParameter("@fpdcont", SqlDbType.VarChar, 15);
                Parameter[0].Value = NewBuyerSince.fpdcont;
                Parameter[1] = new SqlParameter("@salesteam", SqlDbType.VarChar, 20);
                Parameter[1].Value = NewBuyerSince.SalesTeam;
                Parameter[2] = new SqlParameter("@Campaign", SqlDbType.VarChar, 20);
                Parameter[2].Value = NewBuyerSince.CampaignName;
                Parameter[3] = new SqlParameter("@Username", SqlDbType.VarChar, 20);
                Parameter[3].Value = SessionFacade.LoggedInUserName;
                Parameter[4] = new SqlParameter("@Listview", SqlDbType.VarChar, 30);
                Parameter[4].Value = "lvwLookupData";

                drCustomerLookUp = DBHelper.ExecuteQueryToDataSet("[sp_NewBuyerSince_Coalesce]", Parameter);

                return drCustomerLookUp;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
    }
}