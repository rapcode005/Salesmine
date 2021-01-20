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
using DataAccess;

namespace AppLogic
{

    public class cQuoteOver1KDB
    {
        #region GetQuoteOver1K // used in Quote Pipeline Page
        public DataSet GetQuoteOver1K(cQuoteOver1K objQuoteOver1K)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drQuoteOver1K = null;

                SqlParameter[] Parameter = new SqlParameter[1];
                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[0].Value = objQuoteOver1K.Campaign;

                drQuoteOver1K = DBHelper.ExecuteQueryToDataSet("SelectQuoteOver1KWithEditQuoteValue", Parameter);

                return drQuoteOver1K;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        
        }

        public DataSet GetQuoteOver1K_New(cQuoteOver1K objQuoteOver1K)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drQuoteOver1K = null;
                SqlParameter[] Parameter;

                if (objQuoteOver1K.StartDate != "" && objQuoteOver1K.StartDate != null)
                {
                    if (objQuoteOver1K.RepName != "" && objQuoteOver1K.RepName != null)
                    {
                        Parameter = new SqlParameter[6];
                        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                        Parameter[0].Value = SessionFacade.CampaignName;

                        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                        Parameter[1].Value = SessionFacade.LoggedInUserName;

                        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                        Parameter[2].Value = "lvwQuotePipeline";

                        Parameter[3] = new SqlParameter("@Startdate", SqlDbType.VarChar, 17);
                        Parameter[3].Value = objQuoteOver1K.StartDate;

                        Parameter[4] = new SqlParameter("@Enddate", SqlDbType.VarChar, 17);
                        Parameter[4].Value = objQuoteOver1K.EndDate;

                        Parameter[5] = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                        Parameter[5].Value = objQuoteOver1K.RepName;
                    }
                    else
                    {
                        Parameter = new SqlParameter[5];
                        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                        Parameter[0].Value = SessionFacade.CampaignName;

                        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                        Parameter[1].Value = SessionFacade.LoggedInUserName;

                        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                        Parameter[2].Value = "lvwQuotePipeline";

                        Parameter[3] = new SqlParameter("@Startdate", SqlDbType.VarChar, 17);
                        Parameter[3].Value = objQuoteOver1K.StartDate;

                        Parameter[4] = new SqlParameter("@Enddate", SqlDbType.VarChar, 17);
                        Parameter[4].Value = objQuoteOver1K.EndDate;
                    }
                }
                else
                {
                    if (objQuoteOver1K.RepName != "" && objQuoteOver1K.RepName != null)
                    {
                        Parameter = new SqlParameter[4];
                        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                        Parameter[0].Value = SessionFacade.CampaignName;

                        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                        Parameter[1].Value = SessionFacade.LoggedInUserName;

                        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                        Parameter[2].Value = "lvwQuotePipeline";

                        Parameter[3] = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                        Parameter[3].Value = objQuoteOver1K.RepName;
                    }
                    else
                    {
                        Parameter = new SqlParameter[3];
                        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                        Parameter[0].Value = SessionFacade.CampaignName;

                        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                        Parameter[1].Value = SessionFacade.LoggedInUserName;

                        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                        Parameter[2].Value = "lvwQuotePipeline";
                    }

                }

                drQuoteOver1K = DBHelper.ExecuteQueryToDataSet("[dbo].[SelectQuoteOver1KWithEditQuoteValueTest_COALESCE]", Parameter);
                return drQuoteOver1K;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        
        ////For Date Range
        //public DataSet GetQuoteOver1K_New_DateRange(cQuoteOver1K objQuoteOver1K)
        //{
        //    try
        //    {
        //        SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
        //        DataSet drQuoteOver1K = null;
        //        SqlParameter[] Parameter = new SqlParameter[5];
        //        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
        //        Parameter[0].Value = SessionFacade.CampaignName;

        //        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
        //        Parameter[1].Value = SessionFacade.LoggedInUserName;

        //        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
        //        Parameter[2].Value = "lvwQuotePipeline";

        //        Parameter[3] = new SqlParameter("@Startdate", SqlDbType.VarChar, 17);
        //        Parameter[3].Value = objQuoteOver1K.StartDate;

        //        Parameter[4] = new SqlParameter("@Enddate", SqlDbType.VarChar, 17);
        //        Parameter[4].Value = objQuoteOver1K.EndDate;

        //        drQuoteOver1K = DBHelper.ExecuteQueryToDataSet("[dbo].[SelectQuoteOver1KWithEditQuoteValueTest_COALESCE]", Parameter);
        //        return drQuoteOver1K;
        //    }
        //    catch (Exception ex)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
        //        throw new Exception(ex.Message, ex);
        //    }

        //}

        ////For Reps
        //public DataSet GetQuoteOver1K_New_Reps(cQuoteOver1K objQuoteOver1K)
        //{
        //    try
        //    {
        //        SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
        //        DataSet drQuoteOver1K = null;
        //        SqlParameter[] Parameter = new SqlParameter[4];
        //        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
        //        Parameter[0].Value = SessionFacade.CampaignName;

        //        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
        //        Parameter[1].Value = SessionFacade.LoggedInUserName;

        //        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
        //        Parameter[2].Value = "lvwQuotePipeline";

        //        Parameter[3] = new SqlParameter("@Name", SqlDbType.VarChar, 100);
        //        Parameter[3].Value = objQuoteOver1K.RepName;

        //        drQuoteOver1K = DBHelper.ExecuteQueryToDataSet("[dbo].[SelectQuoteOver1KWithEditQuoteValueTest_COALESCE]", Parameter);
        //        return drQuoteOver1K;
        //    }
        //    catch (Exception ex)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
        //        throw new Exception(ex.Message, ex);
        //    }

        //}
        #endregion

        #region GetQuoteOver1KWonorLoss() // used in Quote Pipeline Page
        public DataSet GetQuoteOver1KWonorLoss(cQuoteOver1K objQuoteOver1K)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drQuoteOver1K = null;

                SqlParameter[] Parameter = new SqlParameter[1];
                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[0].Value = objQuoteOver1K.Campaign;

                drQuoteOver1K = DBHelper.ExecuteQueryToDataSet("SelectQuotePipelineWonorLossEditQuoteValue", Parameter);
                //drQuoteOver1K.Tables[0].Columns["QUOTE DAY"].Convert(val => DateTime.Parse(val.ToString()).ToString("MM/dd/yyyy"));
                return drQuoteOver1K;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }

        public DataSet GetQuoteOver1KWonorLoss_New(cQuoteOver1K objQuoteOver1K)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drQuoteOver1K = null;
                SqlParameter[] Parameter;

                if (objQuoteOver1K.StartDate != "" && objQuoteOver1K.StartDate != null)
                {
                    if (objQuoteOver1K.RepName != "" && objQuoteOver1K.RepName != null)
                    {
                        Parameter = new SqlParameter[6];
                        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                        Parameter[0].Value = SessionFacade.CampaignName;

                        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                        Parameter[1].Value = SessionFacade.LoggedInUserName;

                        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                        Parameter[2].Value = "lvwQuotePipeline";

                        Parameter[3] = new SqlParameter("@Startdate", SqlDbType.VarChar, 17);
                        Parameter[3].Value = objQuoteOver1K.StartDate;

                        Parameter[4] = new SqlParameter("@Enddate", SqlDbType.VarChar, 17);
                        Parameter[4].Value = objQuoteOver1K.EndDate;

                        Parameter[5] = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                        Parameter[5].Value = objQuoteOver1K.RepName;
                    }
                    else
                    {
                        Parameter = new SqlParameter[5];
                        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                        Parameter[0].Value = SessionFacade.CampaignName;

                        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                        Parameter[1].Value = SessionFacade.LoggedInUserName;

                        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                        Parameter[2].Value = "lvwQuotePipeline";

                        Parameter[3] = new SqlParameter("@Startdate", SqlDbType.VarChar, 17);
                        Parameter[3].Value = objQuoteOver1K.StartDate;

                        Parameter[4] = new SqlParameter("@Enddate", SqlDbType.VarChar, 17);
                        Parameter[4].Value = objQuoteOver1K.EndDate;
                    }
                }
                else
                {
                    if (objQuoteOver1K.RepName != "" && objQuoteOver1K.RepName != null)
                    {
                        Parameter = new SqlParameter[4];
                        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                        Parameter[0].Value = SessionFacade.CampaignName;

                        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                        Parameter[1].Value = SessionFacade.LoggedInUserName;

                        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                        Parameter[2].Value = "lvwQuotePipeline";

                        Parameter[3] = new SqlParameter("@Name", SqlDbType.VarChar, 100);
                        Parameter[3].Value = objQuoteOver1K.RepName;
                    }
                    else
                    {
                        Parameter = new SqlParameter[3];
                        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                        Parameter[0].Value = SessionFacade.CampaignName;

                        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                        Parameter[1].Value = SessionFacade.LoggedInUserName;

                        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                        Parameter[2].Value = "lvwQuotePipeline";
                    }

                }

                //SqlParameter[] Parameter = new SqlParameter[3];
                //Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                //Parameter[0].Value = SessionFacade.CampaignName;

                //Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
                //Parameter[1].Value = SessionFacade.LoggedInUserName;

                //Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
                //Parameter[2].Value = "lvwQuotePipeline";


                drQuoteOver1K = DBHelper.ExecuteQueryToDataSet("[dbo].[SelectQuotePipelineWonorLossEditQuoteValue_COALESCE]", Parameter);

                return drQuoteOver1K;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }

        //public DataSet GetQuoteOver1KWonorLoss_New_DataRange(cQuoteOver1K objQuoteOver1K)
        //{
        //    try
        //    {
        //        SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
        //        DataSet drQuoteOver1K = null;

        //        SqlParameter[] Parameter = new SqlParameter[5];
        //        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
        //        Parameter[0].Value = SessionFacade.CampaignName;

        //        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
        //        Parameter[1].Value = SessionFacade.LoggedInUserName;

        //        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
        //        Parameter[2].Value = "lvwQuotePipeline";

        //        Parameter[3] = new SqlParameter("@Startdate", SqlDbType.VarChar, 17);
        //        Parameter[3].Value = objQuoteOver1K.StartDate;

        //        Parameter[4] = new SqlParameter("@Enddate", SqlDbType.VarChar, 17);
        //        Parameter[4].Value = objQuoteOver1K.EndDate;

        //        drQuoteOver1K = DBHelper.ExecuteQueryToDataSet("[dbo].[SelectQuotePipelineWonorLossEditQuoteValue_COALESCE]", Parameter);

        //        return drQuoteOver1K;
        //    }
        //    catch (Exception ex)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
        //        throw new Exception(ex.Message, ex);
        //    }

        //}

        //public DataSet GetQuoteOver1KWonorLoss_New_Reps(cQuoteOver1K objQuoteOver1K)
        //{
        //    try
        //    {
        //        SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
        //        DataSet drQuoteOver1K = null;

        //        SqlParameter[] Parameter = new SqlParameter[4];
        //        Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
        //        Parameter[0].Value = SessionFacade.CampaignName;

        //        Parameter[1] = new SqlParameter("@Username", SqlDbType.Char, 10);
        //        Parameter[1].Value = SessionFacade.LoggedInUserName;

        //        Parameter[2] = new SqlParameter("@Listview", SqlDbType.Char, 30);
        //        Parameter[2].Value = "lvwQuotePipeline";

        //        Parameter[3] = new SqlParameter("@Name", SqlDbType.VarChar, 100);
        //        Parameter[3].Value = objQuoteOver1K.RepName;

        //        drQuoteOver1K = DBHelper.ExecuteQueryToDataSet("[dbo].[SelectQuotePipelineWonorLossEditQuoteValue_COALESCE]", Parameter);

        //        return drQuoteOver1K;
        //    }
        //    catch (Exception ex)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
        //        throw new Exception(ex.Message, ex);
        //    }

        //}

        #endregion

        #region AddQuoteComputing // used in Quote Pipeline Page
        public bool AddQuoteComputing(cQuoteOver1K objQuoteOver1K)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());

                SqlParameter[] Parameter = new SqlParameter[22];

                Parameter[0] = new SqlParameter("@Probablilty", SqlDbType.Float);
                Parameter[0].Value = CheckIfValidNumber(objQuoteOver1K.Probablilty.Replace("%", "").Replace(" ", ""));

                Parameter[1] = new SqlParameter("@Weighted", SqlDbType.Float);
                Parameter[1].Value =  CheckIfValidNumber(objQuoteOver1K.Weighted);

                Parameter[2] = new SqlParameter("@ProposedDate", SqlDbType.Date);

                DateTime temp1;
                if (DateTime.TryParse(objQuoteOver1K.ProposedDate, out temp1))
                {
                    Parameter[2].Value = objQuoteOver1K.ProposedDate;
                }
                else
                    Parameter[2].Value = DBNull.Value;

                Parameter[3] = new SqlParameter("@Competition", SqlDbType.VarChar,50);
                Parameter[3].Value = objQuoteOver1K.Competition;
                
                Parameter[4] = new SqlParameter("@Notes", SqlDbType.VarChar,300);
                Parameter[4].Value = objQuoteOver1K.Notes;
                
                Parameter[5] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[5].Value = objQuoteOver1K.Campaign;

                Parameter[6] = new SqlParameter("@Quote_Doc", SqlDbType.VarChar, 15);
                Parameter[6].Value = objQuoteOver1K.Quote_Doc;

                Parameter[7] = new SqlParameter("@AccountNum", SqlDbType.VarChar,15);
                Parameter[7].Value = objQuoteOver1K.AccountNum;

                Parameter[8] = new SqlParameter("@Createdby", SqlDbType.VarChar,16);
                Parameter[8].Value = objQuoteOver1K.Createdby;

                Parameter[9] = new SqlParameter("@QuoteDay", SqlDbType.Date);
                if (string.IsNullOrEmpty(objQuoteOver1K.QuoteDay))
                    Parameter[9].Value = DBNull.Value;
                else
                    Parameter[9].Value = objQuoteOver1K.QuoteDay;

                Parameter[10] = new SqlParameter("@AccountName", SqlDbType.VarChar, 35);
                if (objQuoteOver1K.AccountName == null)
                    Parameter[10].Value = "";
                else
                    Parameter[10].Value = objQuoteOver1K.AccountName;
                
                Parameter[11] = new SqlParameter("@QSTCurrent", SqlDbType.VarChar, 100);
                if (objQuoteOver1K.QSTCurrent == null)
                    Parameter[11].Value = "";
                else
                    Parameter[11].Value = objQuoteOver1K.QSTCurrent;

                Parameter[12] = new SqlParameter("@QSTIn", SqlDbType.VarChar, 100);
                if (objQuoteOver1K.QSTIn == null)
                    Parameter[12].Value = "";
                else
                    Parameter[12].Value = objQuoteOver1K.QSTIn;
             
                Parameter[13] = new SqlParameter("@QuoteValue", SqlDbType.Float);
                if (objQuoteOver1K.QuoteValue == null ||
                    objQuoteOver1K.QuoteValue.Replace("&nbsp;", "0") == "")
                    Parameter[13].Value = 0.0f;
                else
                    Parameter[13].Value = objQuoteOver1K.QuoteValue;

                Parameter[14] = new SqlParameter("@WinorLoss", SqlDbType.VarChar, 5);
                Parameter[14].Value = objQuoteOver1K.WinorLoss;

                Parameter[15] = new SqlParameter("@Status", SqlDbType.VarChar, 15);
                Parameter[15].Value = objQuoteOver1K.Status;

                Parameter[16] = new SqlParameter("@QuoteCost", SqlDbType.Float);
                if (objQuoteOver1K.QuoteCost == null || objQuoteOver1K.QuoteCost.Replace("&nbsp;", "0") == "" || objQuoteOver1K.QuoteCost.Trim() == "")
                    Parameter[16].Value = 0.0f;
                else
                    Parameter[16].Value = objQuoteOver1K.QuoteCost.Replace("&nbsp;", "0");
                 
                Parameter[17] = new SqlParameter("@QuoteGMPerc", SqlDbType.Float);
                Parameter[17].Value = CheckIfValidNumber(objQuoteOver1K.QuoteGMPerc.Replace("%", "").Replace(" ", ""));

                DateTime temp;
                if (DateTime.TryParse(objQuoteOver1K.ScheDate, out temp))
                {
                    Parameter[18] = new SqlParameter("@sched_followup", SqlDbType.Date);
                    Parameter[18].Value = DateTime.Parse(objQuoteOver1K.ScheDate);
                }
                else
                {
                    Parameter[18] = new SqlParameter("@sched_followup", SqlDbType.Date);
                    Parameter[18].Value = DBNull.Value;
                }

                Parameter[19] = new SqlParameter("@product_line", SqlDbType.VarChar, 300);
                Parameter[19].Value = objQuoteOver1K.ProductLine;

                Parameter[20] = new SqlParameter("@construction", SqlDbType.VarChar, 4);
                Parameter[20].Value = objQuoteOver1K.Construction;

                Parameter[21] = new SqlParameter("@mining", SqlDbType.VarChar, 4);
                Parameter[21].Value = objQuoteOver1K.Mining;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddQuoteComputing", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }

        //Check if the string is valid Number
        private float CheckIfValidNumber(string value)
        {
            float temp;
            if (float.TryParse(value, out temp))
                return float.Parse(value);
            else
                return 0.0f;
        }

        //Check if the string is valid Date
        private DateTime CheckIfValidDate(string value)
        {
            DateTime temp;
            if (DateTime.TryParse(value, out temp))
                return DateTime.Parse(value);
            else
                return DateTime.Now;
        }
        #endregion

        #region AddQuoteComputingWinOrLoss // used in Quote Pipeline Page
        public bool AddQuoteComputingWinOrLoss(cQuoteOver1K objQuoteOver1K)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());

                SqlParameter[] Parameter = new SqlParameter[22];

                Parameter[0] = new SqlParameter("@Probablilty", SqlDbType.Float);
                Parameter[0].Value = CheckIfValidNumber(objQuoteOver1K.Probablilty.Replace("%", ""));

                Parameter[1] = new SqlParameter("@Weighted", SqlDbType.Float);
                Parameter[1].Value = CheckIfValidNumber(objQuoteOver1K.Weighted);

                Parameter[2] = new SqlParameter("@ProposedDate", SqlDbType.Date);

                DateTime temp1;
                if (DateTime.TryParse(objQuoteOver1K.ProposedDate, out temp1))
                {
                    Parameter[2].Value = objQuoteOver1K.ProposedDate;
                }
                else
                    Parameter[2].Value = DBNull.Value;

                Parameter[3] = new SqlParameter("@Competition", SqlDbType.VarChar, 50);
                Parameter[3].Value = objQuoteOver1K.Competition;

                Parameter[4] = new SqlParameter("@Notes", SqlDbType.VarChar, 300);
                Parameter[4].Value = objQuoteOver1K.Notes;

                Parameter[5] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[5].Value = objQuoteOver1K.Campaign;

                Parameter[6] = new SqlParameter("@Quote_Doc", SqlDbType.VarChar, 15);
                Parameter[6].Value = objQuoteOver1K.Quote_Doc;

                Parameter[7] = new SqlParameter("@AccountNum", SqlDbType.VarChar, 15);
                Parameter[7].Value = objQuoteOver1K.AccountNum;

                Parameter[8] = new SqlParameter("@Createdby", SqlDbType.VarChar, 16);
                Parameter[8].Value = objQuoteOver1K.Createdby;

                Parameter[9] = new SqlParameter("@QuoteDay", SqlDbType.Date);
                if (objQuoteOver1K.QuoteDay == null)
                    Parameter[9].Value = DBNull.Value;
                else
                    Parameter[9].Value = objQuoteOver1K.QuoteDay;

                Parameter[10] = new SqlParameter("@AccountName", SqlDbType.VarChar, 35);
                if (objQuoteOver1K.AccountName == null)
                    Parameter[10].Value = "";
                else
                    Parameter[10].Value = objQuoteOver1K.AccountName;

                Parameter[11] = new SqlParameter("@QSTCurrent", SqlDbType.VarChar, 100);
                if (objQuoteOver1K.QSTCurrent == null)
                    Parameter[11].Value = "";
                else
                    Parameter[11].Value = objQuoteOver1K.QSTCurrent;

                Parameter[12] = new SqlParameter("@QSTIn", SqlDbType.VarChar, 100);
                if (objQuoteOver1K.QSTIn == null)
                    Parameter[12].Value = "";
                else
                    Parameter[12].Value = objQuoteOver1K.QSTIn;

                Parameter[13] = new SqlParameter("@QuoteValue", SqlDbType.Float);
                if (objQuoteOver1K.QuoteValue == null)
                    Parameter[13].Value = 0.0f;
                else
                    Parameter[13].Value = objQuoteOver1K.QuoteValue;

                Parameter[14] = new SqlParameter("@WinorLoss", SqlDbType.VarChar, 5);
                Parameter[14].Value = objQuoteOver1K.WinorLoss;

                Parameter[15] = new SqlParameter("@Status", SqlDbType.VarChar, 15);
                Parameter[15].Value = objQuoteOver1K.Status;

                Parameter[16] = new SqlParameter("@QuoteCost", SqlDbType.Float);
                if (objQuoteOver1K.QuoteCost == null ||
                    objQuoteOver1K.QuoteCost.Replace("&nbsp;", "0") == "")
                    Parameter[16].Value = 0.0f;
                else
                    Parameter[16].Value = objQuoteOver1K.QuoteCost.Replace("&nbsp;", "0");

                Parameter[17] = new SqlParameter("@QuoteGMPerc", SqlDbType.Float);
                if (objQuoteOver1K.QuoteGMPerc == null ||
                    objQuoteOver1K.QuoteGMPerc.Replace(" %", "") == "")
                    Parameter[17].Value = 0.0f;
                else
                    Parameter[17].Value = objQuoteOver1K.QuoteGMPerc.Replace(" %", "");

                DateTime temp;
                if (DateTime.TryParse(objQuoteOver1K.ScheDate, out temp))
                {
                    Parameter[18] = new SqlParameter("@sched_followup", SqlDbType.Date);
                    Parameter[18].Value = DateTime.Parse(objQuoteOver1K.ScheDate);
                }
                else
                {
                    Parameter[18] = new SqlParameter("@sched_followup", SqlDbType.Date);
                    Parameter[18].Value = DBNull.Value;
                }

                Parameter[19] = new SqlParameter("@product_line", SqlDbType.VarChar, 300);
                Parameter[19].Value = objQuoteOver1K.ProductLine;

                Parameter[20] = new SqlParameter("@construction", SqlDbType.VarChar, 4);
                Parameter[20].Value = objQuoteOver1K.Construction;

                Parameter[21] = new SqlParameter("@mining", SqlDbType.VarChar, 4);
                Parameter[21].Value = objQuoteOver1K.Mining;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddQuoteComputingWinOrLoss", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region AddNewQuote // Adding New Quotes
        public bool AddNewQuote(cQuoteOver1K objNewQuote)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());

                SqlParameter[] Parameter = new SqlParameter[9];

                Parameter[0] = new SqlParameter("@QuoteDate", SqlDbType.Date);
                Parameter[0].Value = CheckIfValidDate(objNewQuote.QuoteDay);

                Parameter[1] = new SqlParameter("@Account", SqlDbType.VarChar,10);
                Parameter[1].Value = objNewQuote.AccountNum;

                Parameter[2] = new SqlParameter("@CompanyName", SqlDbType.VarChar, 35);
                Parameter[2].Value = objNewQuote.AccountName;

                Parameter[3] = new SqlParameter("@QuoteDocNo", SqlDbType.VarChar, 15);
                Parameter[3].Value = objNewQuote.Quote_Doc;

                Parameter[4] = new SqlParameter("@SlsTeamIN", SqlDbType.VarChar, 10);
                Parameter[4].Value = objNewQuote.QSTIn;

                Parameter[5] = new SqlParameter("@Sales_Rep", SqlDbType.VarChar, 10);
                Parameter[5].Value = objNewQuote.QSTCurrent;

                Parameter[6] = new SqlParameter("@quote_value", SqlDbType.Float);
                Parameter[6].Value = CheckIfValidNumber(objNewQuote.QuoteValue);

                Parameter[7] = new SqlParameter("@username", SqlDbType.VarChar, 10);
                Parameter[7].Value = SessionFacade.LoggedInUserName;

                Parameter[8] = new SqlParameter("@campaign", SqlDbType.VarChar, 10);
                Parameter[8].Value = objNewQuote.Campaign;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddNewQuotes", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region EditQuoteValue  // Editing Quote Value
        public bool EditQuoteValue(cQuoteOver1K objEditQuoteValue)
        {
            try
            {

                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());

                SqlParameter[] Parameter = new SqlParameter[7];

                Parameter[0] = new SqlParameter("@QuoteValue", SqlDbType.Float);
                float temp;
                if (float.TryParse(objEditQuoteValue.QuoteValue, out temp) == false)
                    Parameter[0].Value = 0.0f;
                else
                    Parameter[0].Value = float.Parse(objEditQuoteValue.QuoteValue);

                Parameter[1] = new SqlParameter("@Cusmerge", SqlDbType.VarChar, 15);
                Parameter[1].Value = objEditQuoteValue.AccountNum;

                Parameter[2] = new SqlParameter("@QuoteDoc", SqlDbType.VarChar, 15);
                Parameter[2].Value = objEditQuoteValue.Quote_Doc;

                Parameter[3] = new SqlParameter("@Campagin", SqlDbType.VarChar, 15);
                Parameter[3].Value = objEditQuoteValue.Campaign;

                Parameter[4] = new SqlParameter("@Createdby", SqlDbType.VarChar, 10);
                Parameter[4].Value = SessionFacade.LoggedInUserName;

                Parameter[5] = new SqlParameter("@WeightedValue", SqlDbType.Float);
                Parameter[5].Value = CheckIfValidNumber(objEditQuoteValue.Weighted);

                Parameter[6] = new SqlParameter("@Quote_Assignment", SqlDbType.VarChar, 40);
                Parameter[6].Value = objEditQuoteValue.QSTIn;

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("UpdateQuoteValue", Parameter);

                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}