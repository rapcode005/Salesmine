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
using System.Web.UI;


namespace AppLogic
{

    public class cAddNOteDB
    {

        #region AddNote //Add Note
        public bool ExecAddNote(cAddNote objAddNOte)
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());

            try
            {

                SqlParameter[] Parameter = new SqlParameter[8];
                Parameter[0] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 10);
                Parameter[0].Value = objAddNOte.CreatedBy;

                Parameter[1] = new SqlParameter("@NoteType", SqlDbType.VarChar, 17);
                Parameter[1].Value = objAddNOte.NoteType;

                Parameter[2] = new SqlParameter("@Note", SqlDbType.VarChar, 500);
                Parameter[2].Value = objAddNOte.Note;

                Parameter[3] = new SqlParameter("@AccountNum", SqlDbType.VarChar, 10);
                Parameter[3].Value = objAddNOte.AccountNum;

                Parameter[4] = new SqlParameter("@ContactNum", SqlDbType.VarChar, 10);
                Parameter[4].Value = objAddNOte.ContactNum;

                Parameter[5] = new SqlParameter("@NoteDate", SqlDbType.VarChar, 20);
                Parameter[5].Value = objAddNOte.NoteDate;

                Parameter[6] = new SqlParameter("@Campaign", SqlDbType.VarChar, 6);
                Parameter[6].Value = objAddNOte.Campaign;

                Parameter[7] = new SqlParameter("@Createdon", SqlDbType.VarChar, 35);
                Parameter[7].Value = objAddNOte.Createdon;


                if (objAddNOte.Campaign == "")
                    return false;
                else
                {
                    int NoOfInsertedRecords = DBHelper.ExecuteNonQuery("NotesCommHist_AddNote", Parameter);
                    return NoOfInsertedRecords == 1 ? true : false;
                }
         
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message.ToString(), ex);
            }

        }
        #endregion

        #region UpdateNote //Update Note
        public bool ExecUpdateNote(cAddNote objAddNOte)
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());

            try
            {

                SqlParameter[] Parameter = new SqlParameter[5];

                Parameter[0] = new SqlParameter("@NoteType", SqlDbType.VarChar, 17);
                Parameter[0].Value = objAddNOte.NoteType;

                Parameter[1] = new SqlParameter("@Note", SqlDbType.VarChar, 500);
                Parameter[1].Value = objAddNOte.Note;

                Parameter[2] = new SqlParameter("@AccountNum", SqlDbType.VarChar, 10);
                Parameter[2].Value = objAddNOte.AccountNum;

                Parameter[3] = new SqlParameter("@Campaign", SqlDbType.VarChar, 6);
                Parameter[3].Value = objAddNOte.Campaign;

                Parameter[4] = new SqlParameter("@Createdon", SqlDbType.VarChar, 50);
                Parameter[4].Value = objAddNOte.Createdon;


                if (objAddNOte.Campaign == "")
                    return false;
                else
                {
                    int NoOfInsertedRecords = DBHelper.ExecuteNonQuery("NotesCommHist_UpdateNotev2", Parameter);
                    return NoOfInsertedRecords == 1 ? true : false;
                }

            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message.ToString(), ex);
            }

        }
        #endregion
    }

    public class cNotesCommHistoryDB
    {
        #region GetNotesCommHistory //Following functions will be used in Comm & History Page

        public DataSet GetNotesCommHistory(cNotesCommHistory objNotesCommHistory) //cOrderHistory objOrderHistory
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drNotesCommHistory = null;

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Account", SqlDbType.NVarChar, 10);
                Parameter[0].Value = objNotesCommHistory.AccountNum;

                Parameter[1] = new SqlParameter("@UNIT", SqlDbType.NVarChar, 10);
                Parameter[1].Value =objNotesCommHistory.CampaignName;

                if (objNotesCommHistory.Contact == "")
                {
                    objNotesCommHistory.Contact = null;
                }
                             
                drNotesCommHistory = DBHelper.ExecuteQueryToDataSet("sp_NotesDialer", Parameter);
               
                return drNotesCommHistory;
            }

            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetGoldMineNotesCommHistory(cNotesCommHistory objNotesCommHistory) //cOrderHistory objOrderHistory
        {
            try 
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["GoldMineDialerDBConnection"].ToString());
                DataSet drNotesCommHistory = null;

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 50);
                Parameter[0].Value = "Merged";

                Parameter[1] = new SqlParameter("@Account", SqlDbType.NVarChar, 10);
                Parameter[1].Value = objNotesCommHistory.AccountNum;

                //Parameter[2] = new SqlParameter("@CONTACT", SqlDbType.NVarChar, 10);
                //Parameter[2].Value = objNotesCommHistory.Contact;

                //Parameter[3] = new SqlParameter("@StartDate", SqlDbType.NVarChar, 15);
                //Parameter[3].Value = objNotesCommHistory.SearchOrderStartDate;

                //Parameter[4] = new SqlParameter("@EndDate", SqlDbType.NVarChar, 15);
                //Parameter[4].Value = objNotesCommHistory.SearchOrderEndDate;

                //Parameter[5] = new SqlParameter("@NoteType", SqlDbType.NVarChar, 50);
                //Parameter[5].Value = objNotesCommHistory.NotesKAMID;

                //if (objNotesCommHistory.Contact == "")
                //{
                //    objNotesCommHistory.Contact = null;
                //}
                //Parameter[2] = new SqlParameter("@CONTACT", SqlDbType.NVarChar, 10);
                //Parameter[2].Value = objNotesCommHistory.Contact;

                // drNotesCommHistory = DBHelper.ExecuteQueryToDataSet("NotesCommHist", Parameter);
               // drNotesCommHistory = DBHelper.ExecuteGoldmineQueryToDataSet("[dbo].[CLCallNotes]", Parameter);
                drNotesCommHistory = DBHelper.ExecuteGoldmineQueryToDataSet("[dbo].[CLCallNotes]", Parameter);

                return drNotesCommHistory;
            }

            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        
        #endregion

        #region GetUserNotes //Get all User Notes
        public DataSet GetUserNotes(cNotesCommHistory objNotesCommHistory) //cOrderHistory objOrderHistory
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drNotesCommHistory = null;

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@CREATEDBY", SqlDbType.NVarChar, 10);
                Parameter[0].Value = objNotesCommHistory.CreatedBy;

                Parameter[1] = new SqlParameter("@UNIT", SqlDbType.NVarChar, 10);
                Parameter[1].Value = objNotesCommHistory.CampaignName;
         

                drNotesCommHistory = DBHelper.ExecuteQueryToDataSet("NotesCommHist_User", Parameter);


                return drNotesCommHistory;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetNoteType //Get all Note Type
        public DataSet GetNoteType(cNotesCommHistory objNotesCommHistory) //cOrderHistory objOrderHistory
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drNotesCommHistory = null;

                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.NVarChar, 10);
                Parameter[0].Value = objNotesCommHistory.CampaignName;

                drNotesCommHistory = DBHelper.ExecuteQueryToDataSet("SelectNoteType", Parameter);


                return drNotesCommHistory;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetScheduleNotes //Get all User Schedule Notes
        public DataSet GetScheduleNotes(cNotesCommHistory objNotesCommHistory) //cOrderHistory objOrderHistory
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drNotesCommHistory = null;

                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
                Parameter[0].Value = objNotesCommHistory.CreatedBy;

                Parameter[1] = new SqlParameter("@UNIT", SqlDbType.VarChar, 10);
                Parameter[1].Value = objNotesCommHistory.CampaignName;

                Parameter[2] = new SqlParameter("@CurrentDate", SqlDbType.VarChar, 12);
                Parameter[2].Value = objNotesCommHistory.CurentDate;

                drNotesCommHistory = DBHelper.ExecuteQueryToDataSet("SelectNotesScheduleDate", Parameter);

                return drNotesCommHistory;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        //Will be use in Territory Page
        public DataSet GetTerritoryNotesCommHistory(cNotesCommHistory objNotesCommHistory) //cOrderHistory objOrderHistory
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drNotesCommHistory = null;

                SqlParameter[] Parameter = new SqlParameter[2];

                //Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                //Parameter[0].Value = objNotesCommHistory.AccountNum;

                Parameter[0] = new SqlParameter("@UNIT", SqlDbType.VarChar, 10);
                Parameter[0].Value = objNotesCommHistory.CampaignName;

                Parameter[1] = new SqlParameter("@KAMID", SqlDbType.VarChar, 10);
                Parameter[1].Value = objNotesCommHistory.NotesKAMID;
                
               // drNotesCommHistory = DBHelper.ExecuteQueryToDataSet("NotesCommHistTerritory", Parameter);
                drNotesCommHistory = DBHelper.ExecuteQueryToDataSet("sp_NotesDialerT", Parameter);

                return drNotesCommHistory;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
    }
    public class cDialerDataDB
    {
        #region GetDialerData //Following functions will be used in Comm & History Page -Dialer data
        public DataSet GetDialerData(cDialerData objDialerData) //cOrderHistory objOrderHistory
        {
            try
            {
                
                //SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["DialerDBConnection"].ToString());
                //DataSet drDialerData = null;

                //SqlParameter[] Parameter = new SqlParameter[2];

                //Parameter[0] = new SqlParameter("@ACCOUNT", SqlDbType.NVarChar, 10);
                //Parameter[0].Value = objDialerData.SearchOrderAccount;

                //Parameter[1] = new SqlParameter("@UNIT", SqlDbType.NVarChar, 10);
                //Parameter[1].Value = objDialerData.SearchOrderCampaignName;

                //drDialerData = DBHelper.ExecuteQueryToDataSetAIOP("sp_CallHist", Parameter);

                //return drDialerData;


                //using (SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["DialerDBConnection"].ToString()))
                //{
                    DataSet drDialerData = null;

                    SqlParameter[] Parameter = new SqlParameter[2];

                    Parameter[0] = new SqlParameter("@ACCOUNT", SqlDbType.NVarChar, 10);
                    Parameter[0].Value = objDialerData.SearchOrderAccount;

                    Parameter[1] = new SqlParameter("@UNIT", SqlDbType.NVarChar, 10);
                    Parameter[1].Value = objDialerData.SearchOrderCampaignName;

                    drDialerData = DBHelper.ExecuteQueryToDataSetAIOP("sp_CallHist", Parameter);

                    return drDialerData;
                //}
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GetDialerDataTerritory //Following functions will be used in Comm & History Page -Dialer data Territory
        public DataSet GetDialerDataTerritory(cDialerData objDialerData) //cOrderHistory objOrderHistory
        {
            try
            { 


                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["DialerDBConnection"].ToString());
                SqlConnection Constre2 = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                DataSet drDialerData = null;
                DataSet drdialerTerritory = null;
                string TerAccount = "";
                //drDialerData = DBHelper.ExecuteQueryToDataSet("Select  a.accountnum as [Account Number],a.listname as [List Name],a.contactdate as [Contact Date],a.phonenumber as [Phone Number],a.disposition as [Disposition],case when disposition='VM1' THEN 'Left Voice Mail' when disposition='VM2' THEN 'Voice Mail Follow Up' when disposition='VM3' THEN 'Left a Voice Mail Message' when disposition='UNK' THEN 'Unknown' when disposition='AOEX' THEN 'Account Owned Records Expired' Else b.DESCNAME End AS DescName,a.[agent_login] as [Agent Login],a.agent_name as [Agent Name] from sasdata_callhist_EMED a  left join disposition_desc b on a.disposition=b.status_code where accountnum = '" + SessionFacade.AccountNo +"' and accountnum is not null and accountnum <> '' order by contactdate desc", Constre);
                //drDialerData = DBHelper.ExecuteQueryToDataSet("Select  a.accountnum as [ACCOUNT NUMBER] " + 
                //                                                       ",a.listname as [LIST NAME] " +
                //                                                       ",a.contactdate as [CONTACT DATE] " +
                //                                                       ",a.phonenumber as [PHONE NUMBER] " +
                //                                                       ",a.disposition as [DISPOSITION] " +
                //                                                       ",case when disposition='VM1' THEN 'Left Voice Mail' " + 
                //                                                              "when disposition='VM2' THEN 'Voice Mail Follow Up' " +
                //                                                              "when disposition='VM3' THEN 'Left a Voice Mail Message' " +
                //                                                              "when disposition='UNK' THEN 'Unknown' " + 
                //                                                              "when disposition='AOEX' THEN 'Account Owned Records Expired' " +
                //                                                              "Else b.DESCNAME End AS DESCNAME " +
                //                                                       ",a.[agent_login] as [AGENT LOGIN] " +
                //                                                       ",a.agent_name as [AGENT NAME] " +
                //                                                       "from sasdata_callhist_" + objDialerData.SearchOrderCampaignName + " a  " +
                //                                               "left join disposition_desc b on a.disposition=b.status_code where accountnum = '" + SessionFacade.AccountNo + "' and accountnum is not null and accountnum <> '' " + 
                //                                               "order by contactdate desc", Constre);

                string sqlterritoryscrpt = "Select SOLD_TO from " + objDialerData.SearchOrderCampaignName + ".SITEINFO WHERE SOLD_TO <> '' AND salesteam= '" + objDialerData.DialerKAMID + "'";

                drdialerTerritory = DBHelper.ExecuteQueryToDataSet(sqlterritoryscrpt, Constre2);

                int i = 1;
                foreach (DataRow row in drdialerTerritory.Tables[0].Rows)
                {
                   
                    if (i == 1)
                    {
                        TerAccount = "'" + row["SOLD_TO"].ToString() + "'";
                    }
                    else
                    {
                        TerAccount = TerAccount + ",'"  + row["SOLD_TO"].ToString() + "'" ;
                    }

                    i += 1;
                }


                TerAccount = "(" + TerAccount + ")";


                //string sqlstr = "Select  a.accountnum as [ACCOUNT NUMBER] " + "\n" +
                //                                                      ",a.listname as [LIST NAME] " + "\n" +
                //                                                      ",a.contactdate as [CONTACT DATE] " + "\n" +
                //                                                      ",a.phonenumber as [PHONE NUMBER] " + "\n" +
                //                                                      ",a.disposition as [DISPOSITION] " + "\n" +
                //                                                      ",case  when disposition='VM1' THEN 'Left Voice Mail' " + "\n" +
                //                                                             "when disposition='VM2' THEN 'Voice Mail Follow Up' " + "\n" +
                //                                                             "when disposition='VM3' THEN 'Left a Voice Mail Message' " + "\n" +
                //                                                             "when disposition='UNK' THEN 'Unknown' " + "\n" + "\n" +
                //                                                             "when disposition='AOEX' THEN 'Account Owned Records Expired' " + "\n" +
                //                                                             "Else b.DESCNAME End AS DESCNAME " + "\n" +
                //                                                      ",a.[agent_login] as [AGENT LOGIN] " + "\n" +
                //                                                      ",a.agent_name as [AGENT NAME] " + "\n" +
                //                                                      "from sasdata_callhist_" + objDialerData.SearchOrderCampaignName + " a  " + "\n" +
                //                                              "left join disposition_desc b on a.disposition=b.status_code where accountnum in (Select SOLD_TO from ' " + objDialerData.SearchOrderCampaignName + ".SITEINFO WHERE SOLD_TO <> '' AND salesteam= '" + objDialerData.DialerKAMID + "' )  and accountnum is not null and accountnum <> '' " + "\n" +
                //                                              "order by contactdate desc";


                string sqlstr = "Select  a.accountnum as [ACCOUNT NUMBER] " + "\n" +
                                                                      ",a.listname as [LIST NAME] " + "\n" +
                                                                      ",a.contactdate as [CONTACT DATE] " + "\n" +
                                                                      ",a.phonenumber as [PHONE NUMBER] " + "\n" +
                                                                      ",a.disposition as [DISPOSITION] " + "\n" +
                                                                      ",case  when disposition='VM1' THEN 'Left Voice Mail' " + "\n" +
                                                                             "when disposition='VM2' THEN 'Voice Mail Follow Up' " + "\n" +
                                                                             "when disposition='VM3' THEN 'Left a Voice Mail Message' " + "\n" +
                                                                             "when disposition='UNK' THEN 'Unknown' " + "\n" + "\n" +
                                                                             "when disposition='AOEX' THEN 'Account Owned Records Expired' " + "\n" +
                                                                             "Else b.DESCNAME End AS DESCNAME " + "\n" +
                                                                      ",a.[agent_login] as [AGENT LOGIN] " + "\n" +
                                                                      ",a.agent_name as [AGENT NAME] " + "\n" +
                                                                      "from sasdata_callhist_" + objDialerData.SearchOrderCampaignName + " a  " + "\n" +
                                                              "left join disposition_desc b on a.disposition=b.status_code where accountnum in " + TerAccount + " and accountnum is not null and accountnum <> '' " + "\n" +
                                                              "order by contactdate desc";




                drDialerData = DBHelper.ExecuteQueryToDataSet(sqlstr, Constre);



                


















                //SqlParameter[] Parameter = new SqlParameter[5];

                //Parameter[0] = new SqlParameter("@Account", SqlDbType.Float);
                //Parameter[0].Value = float.Parse(objOrderHistory.SearchOrderAccount);

                //Parameter[1] = new SqlParameter("@Contact", SqlDbType.Float);
                //Parameter[1].Value = float.Parse(objOrderHistory.SearchOrderContact);

                //Parameter[2] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                //Parameter[2].Value = objOrderHistory.SearchOrderCampaignName;

                //Parameter[3] = new SqlParameter("@StartDate", SqlDbType.VarChar, 12);
                //Parameter[3].Value = objOrderHistory.SearchOrderStartDate;

                //Parameter[4] = new SqlParameter("@EndDate", SqlDbType.VarChar, 12);
                //Parameter[4].Value = objOrderHistory.SearchOrderEndDate;

                //drOrderHistory = DBHelper.ExecuteQueryToDataSet("spOrders", Parameter);

                return drDialerData;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }

    
}