using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using DataAccess;
using BradyCorp.Log;
using System.Data;
using System.Configuration;

namespace AppLogic
{
    public class cOrderHistoryDB
    {
        //#region GetOrderHistory //Following functions will be used in Order History Page
        //    public DataSet GetOrderHistory(cOrderHistory objOrderHistory)
        //    {
        //        try
        //        {
        //            DataSet drOrderHistory = new DataSet();

        //            SqlParameter[] Parameter = new SqlParameter[2];

        //            Parameter[0] = new SqlParameter("@Account",SqlDbType.VarChar, 10);
        //            Parameter[0].Value = objOrderHistory.SearchOrderAccount;

        //            Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
        //            Parameter[1].Value = objOrderHistory.SearchOrderCampaignName;

        //            drOrderHistory = DBHelper.ExecuteQueryToDataSet("OrderHistory", Parameter);

        //            return drOrderHistory;
        //        }
        //        catch (Exception ex)
        //        {
        //            LoggerHelper.LogMessage(ex.ToString());
        //            throw new Exception(ex.Message, ex);
        //        }
        //    }

        //    public DataSet GetOrderHistory_New(cOrderHistory objOrderHistory)
        //    {
        //        try
        //        {
        //            DataSet drOrderHistory = new DataSet();

        //            SqlParameter[] Parameter = new SqlParameter[4];

        //            Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
        //            Parameter[0].Value = objOrderHistory.SearchOrderAccount;

        //            Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
        //            Parameter[1].Value = objOrderHistory.SearchOrderCampaignName;

        //            Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
        //            Parameter[2].Value = "lvwData";

        //            Parameter[3] = new SqlParameter("@Username", SqlDbType.Char, 10);
        //            Parameter[3].Value = SessionFacade.LoggedInUserName;

        //            drOrderHistory = DBHelper.ExecuteQueryToDataSet("OrderHistory_Coalesce", Parameter);

        //            return drOrderHistory;
        //        }
        //        catch (Exception ex)
        //        {
        //            LoggerHelper.LogMessage(ex.ToString());
        //            throw new Exception(ex.Message, ex);
        //        }
        //    }
        //#endregion

        //#region GetTotalOrderTotalSales //Following function will be used in Order History Page
        //    public DataSet GetTotalOrderTotalSales(cOrderHistory objOrderHistory)
        //    {
        //        try
        //        {
        //            DataSet drTotalOrderTotalSales;

        //            //drTotalOrderTotalSales = DBHelper.ExecuteQueryToDataSet("select count(distinct doc_number) as total_orders,ISNULL(SUM(SUBTOT_OC4),0) as total_sales From EMED.Orders " +
        //            //objOrderHistory.Query + " and uvals='C' and REASON_REJ Is Null and doc_type_txt = 'Standard Order'");

        //            SqlParameter[] Parameter = new SqlParameter[5];

        //            Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
        //            Parameter[0].Value = objOrderHistory.SearchOrderAccount;

        //            Parameter[1] = new SqlParameter("@Contact", SqlDbType.VarChar, 10);
        //            float temp;
        //            if (float.TryParse(objOrderHistory.SearchOrderContact, out temp))
        //                Parameter[1].Value = float.Parse(objOrderHistory.SearchOrderContact);
        //            else
        //                Parameter[1].Value = DBNull.Value;

        //            Parameter[2] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
        //            Parameter[2].Value = objOrderHistory.SearchOrderCampaignName;

        //            Parameter[3] = new SqlParameter("@StartDate", SqlDbType.VarChar, 12);
        //            Parameter[3].Value = objOrderHistory.SearchOrderStartDate;

        //            Parameter[4] = new SqlParameter("@EndDate", SqlDbType.VarChar, 12);
        //            Parameter[4].Value = objOrderHistory.SearchOrderEndDate;

        //            drTotalOrderTotalSales = DBHelper.ExecuteQueryToDataSet("spTotalOrdersSales", Parameter);

        //            return drTotalOrderTotalSales;
        //        }
        //        catch (Exception ex)
        //        {
        //            BradyCorp.Log.LoggerHelper.LogException(ex, "GetTotalOrderTotalSales", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
        //            //LoggerHelper.LogMessage(ex.ToString());
        //            throw new Exception(ex.Message, ex);
        //        }
        //    }
        //#endregion

        #region GetOrderHistory //Following functions will be used in Order History Page to search
            public DataSet GetOrderHistory_Search(string SAccount, string SCampaignName, string Sdate, 
                string Edate, string ORdNum, int Year, string PONo, int yrtype)
            {
                try
                {
                    DataSet drOrderHistory = new DataSet();

                    SqlParameter[] Parameter = new SqlParameter[11];

                    Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 15);
                    Parameter[0].Value = SAccount.Replace("'", "");

                    Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                    Parameter[1].Value = SCampaignName;

                    Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                    Parameter[2].Value = "lvwData";

                    Parameter[3] = new SqlParameter("@Username", SqlDbType.Char, 10);
                    Parameter[3].Value = SessionFacade.LoggedInUserName;

                    Parameter[4] = new SqlParameter("@Cno", SqlDbType.VarChar, 15);
                    if (SessionFacade.BuyerCt != "")
                    {
                        float temp;
                        if (float.TryParse(SessionFacade.BuyerCt, out temp))
                            Parameter[4].Value = SessionFacade.BuyerCt;
                        else
                        {
                            //int vnull;
                            Parameter[4].Value = "";

                        }
                    }
                    else
                    {
                        Parameter[4].Value = "";
                    }
                    //Parameter[4].Value = SessionFacade.BuyerCt;

                    Parameter[5] = new SqlParameter("@Sdate", SqlDbType.VarChar,12);
                    Parameter[5].Value = Sdate;

                    Parameter[6] = new SqlParameter("@Edate", SqlDbType.VarChar,12);
                    Parameter[6].Value = Edate;

                    Parameter[7] = new SqlParameter("@ORdNum", SqlDbType.VarChar, 15);
                    Parameter[7].Value = ORdNum;

                    Parameter[8] = new SqlParameter("@Year", SqlDbType.Int);
                    Parameter[8].Value = Year;

                    Parameter[9] = new SqlParameter("@PONo", SqlDbType.VarChar, 60);
                    Parameter[9].Value = PONo;

                    Parameter[10] = new SqlParameter("@yrType", SqlDbType.Int);
                    Parameter[10].Value = yrtype;

                    //drOrderHistory = DBHelper.ExecuteQueryToDataSet("OrderHistory_Coalesce_Filter", Parameter);
                    drOrderHistory = DBHelper.ExecuteQueryToDataSet("OrderHistory_Coalesce_FilterNew", Parameter);

                    return drOrderHistory;
                }
                
                catch (Exception ex)
                {
                    BradyCorp.Log.LoggerHelper.LogExceptionOrderHistory(ex, "OrderHistory_Coalesce_Filter", 
                        SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, 
                        SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString(),Sdate,Edate,ORdNum,
                        Year, PONo, yrtype, SessionFacade.BuyerCt);
                    throw new Exception(ex.Message, ex);
                }
            }
        #endregion

        //#region GetOrderHistoryChart //Following function will be used in Order History Page
        //public DataSet GetOrderHistoryChart(cOrderHistory objOrderHistory)
        //{
        //    try
        //    {
        //        DataSet drOrderHistoryChart;

        //        //drTotalOrderTotalSales = DBHelper.ExecuteQueryToDataSet("select count(distinct doc_number) as total_orders,ISNULL(SUM(SUBTOT_OC4),0) as total_sales From EMED.Orders " +
        //        //objOrderHistory.Query + " and uvals='C' and REASON_REJ Is Null and doc_type_txt = 'Standard Order'");

        //        SqlParameter[] Parameter = new SqlParameter[2];

        //        Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
        //        Parameter[0].Value = objOrderHistory.SearchOrderAccount;

        //        Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
        //        Parameter[1].Value = objOrderHistory.SearchOrderCampaignName;

        //        drOrderHistoryChart = DBHelper.ExecuteQueryToDataSet("OrderhistoryChart", Parameter);

        //        return drOrderHistoryChart;
        //    }
        //    catch (Exception ex)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogException(ex, "GetOrderHistoryChart", 
        //            SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, 
        //            SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
        //        //LoggerHelper.LogMessage(ex.ToString());
        //        throw new Exception(ex.Message, ex);
        //    }
        //}
        //#endregion

    }
}