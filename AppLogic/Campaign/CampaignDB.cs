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

namespace AppLogic
{
    public class cCampaignDB
    {
        string strQuery;

        //public SqlDataReader GetCampaignList(cCampaign objCampaign)
        //{
        //    try
        //    {
        //        SqlDataReader drCampaign = null;
        //        try
        //        {

        //            drCampaign = DBHelper.ExecuteSqlDataReaderWithoutParameter("usp_USER_PROFILESelectCampaign");
                    
        //            return drCampaign;
        //        }
        //        catch (Exception ex)
        //        {
        //            drCampaign.Close();
        //            throw new Exception(ex.Message.ToString(), ex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //    finally
        //    {
                 
        //    }

        //}

        public DataSet GetCampaignList(cCampaign objCampaign)
        {
            try
            {
                DataSet drCampaign = null;
                try
                {

                   // drCampaign = DBHelper.ExecuteSqlDataReaderWithoutParameter("usp_USER_PROFILESelectCampaign");
                    drCampaign = DBHelper.ExecuteQueryToDataSet("usp_USER_PROFILESelectCampaign");
                    return drCampaign;
                }
                catch (Exception ex)
                {
                    //drCampaign.Close();
                    throw new Exception(ex.Message.ToString(), ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {

            }

        }
        public DataSet GetCampaignList1(cCampaign objCampaign)
        {
            try
            {
                DataSet drCampaign1 = null;
                try
                {

                    drCampaign1 = DBHelper.ExecuteSPWithoutParameter("usp_USER_PROFILESelectCampaign");
                    return drCampaign1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString(), ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetAllCampaignList(cCampaign objCampaign)
        {
            try
            {
                DataSet drCampaign = null;
                try
                {

                    // drCampaign = DBHelper.ExecuteSqlDataReaderWithoutParameter("usp_USER_PROFILESelectCampaign");
                    drCampaign = DBHelper.ExecuteQueryToDataSet("usp_CampaignLisT");
                    return drCampaign;
                }
                catch (Exception ex)
                {
                    //drCampaign.Close();
                    throw new Exception(ex.Message.ToString(), ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {

            }

        }

    }
}