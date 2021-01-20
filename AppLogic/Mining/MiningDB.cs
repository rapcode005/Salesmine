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
    public class cMiningDB
    {
        public DataSet GetMiningDetailsDset(cMining objMining)
        { 
            try 
            {
                DataSet drMiningDetails = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Options", SqlDbType.VarChar, 2000);
                Parameter[0].Value = objMining.varOptions;

                Parameter[1] = new SqlParameter("@Value", SqlDbType.VarChar, 15);
                Parameter[1].Value = objMining.varValue;

                drMiningDetails = DBHelper.ExecuteQueryToDataSet("sp_LoadMiningInfo", Parameter);

                return drMiningDetails;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet GetMiningQA(cMining objMining)
        {
            try 
            {
                DataSet drMiningDetails = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Value", SqlDbType.VarChar, 15);
                Parameter[0].Value = objMining.varValue;

                Parameter[1] = new SqlParameter("@Contact_number", SqlDbType.Int);
                Parameter[1].Value = objMining.varContactID;

                //drMiningDetails = DBHelper.ExecuteQueryToDataSet("sp_LoadMiningQA", Parameter);
                drMiningDetails = DBHelper.ExecuteQueryToDataSet("sp_LoadMiningQASCO", Parameter);
                
                return drMiningDetails;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetMiningDeptsDset(cMining objMining)
        {
            try
            {
                DataSet drMiningDetails = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];
     
                Parameter[0] = new SqlParameter("@Value", SqlDbType.VarChar, 15);
                Parameter[0].Value = objMining.varValue;

                Parameter[1] = new SqlParameter("@Contact_number", SqlDbType.Int);
                Parameter[1].Value = objMining.varContactID;

                drMiningDetails = DBHelper.ExecuteQueryToDataSet("sp_LoadMiningDept", Parameter);

                return drMiningDetails;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetMiningSCODset(cMining objMining)
        {
            try 
            {
                DataSet drMiningDetails = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Value", SqlDbType.VarChar, 15);
                Parameter[0].Value = objMining.varValue;

                Parameter[1] = new SqlParameter("@Contact_number", SqlDbType.Int);
                Parameter[1].Value = objMining.varContactID;

                drMiningDetails = DBHelper.ExecuteQueryToDataSet("sp_LoadMiningSCO", Parameter);

                return drMiningDetails;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet GetMiningNotesDset(cMining objMining)
        {
            try
            {
                DataSet drMiningDetails = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@Value", SqlDbType.VarChar, 15);
                Parameter[0].Value = objMining.varValue;

                //Parameter[1] = new SqlParameter("@Contactnum", SqlDbType.Float);
                //Parameter[1].Value = objMining.varContactID;

                drMiningDetails = DBHelper.ExecuteQueryToDataSet("sp_LoadMiningNotes", Parameter);

                return drMiningDetails;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }



        public SqlDataReader GetMiningInfoDReader(cMining objMining)
        {
            SqlDataReader drMiningDetails = null;
            try 
            { 
                         
                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@Options", SqlDbType.VarChar, 2000);
                Parameter[0].Value = objMining.varOptions;

                Parameter[1] = new SqlParameter("@Value", SqlDbType.VarChar, 15);
                Parameter[1].Value = objMining.varValue;

                drMiningDetails = DBHelper.ExecuteSqlDataReader("sp_LoadMiningInfo", Parameter);

                return drMiningDetails; 
            }
            catch (Exception ex)
            {
                drMiningDetails.Close();
                throw new Exception(ex.Message, ex);
            }

        }


        public bool InsertMiningQA(cMining objMining)
        {
            try 
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[11];

                Parameter[0] = new SqlParameter("@Value", SqlDbType.VarChar, 10);
                Parameter[0].Value = objMining.varValue;

                Parameter[1] = new SqlParameter("@ActiveMineSite", SqlDbType.VarChar, 50);
                Parameter[1].Value = objMining.varActiveMineSite;

                Parameter[2] = new SqlParameter("@AddressOfficeWare", SqlDbType.VarChar, 50);
                Parameter[2].Value = objMining.varAddressOfficeWare;

                Parameter[3] = new SqlParameter("@NoQuestion", SqlDbType.VarChar, 50);
                Parameter[3].Value = objMining.varNoQuestion;

                Parameter[4] = new SqlParameter("@OtherNoQuestion", SqlDbType.VarChar, 50);
                Parameter[4].Value = objMining.varOtherNoQuestion;

                Parameter[5] = new SqlParameter("@SafetySignsFacility", SqlDbType.VarChar, 4);
                Parameter[5].Value = objMining.varSafetySignsFacility;

                Parameter[6] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
                Parameter[6].Value = SessionFacade.LoggedInUserName;

                Parameter[7] = new SqlParameter("@YesCorporateOfficeSiteLevel", SqlDbType.VarChar, 50);
                Parameter[7].Value = objMining.varYesCorporateOfficeSiteLevel;

                Parameter[8] = new SqlParameter("@YesCorporateOfficeSiteLevelOther", SqlDbType.VarChar, 50);
                Parameter[8].Value = objMining.varYesCorporateOfficeSiteLevelOther;

                Parameter[9] = new SqlParameter("@Disposition", SqlDbType.VarChar, 100);
                Parameter[9].Value = objMining.varDisposition;

                Parameter[10] = new SqlParameter("@Contact_number", SqlDbType.Float);
                Parameter[10].Value = objMining.varContactID;


                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_InsertMiningQ&A", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }


        public bool InsertMiningDept(cMining objMining)
        {
            try
            { 
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[4];

                Parameter[0] = new SqlParameter("@Value", SqlDbType.VarChar, 10);
                Parameter[0].Value = objMining.varValue;

                Parameter[1] = new SqlParameter("@Contact_number", SqlDbType.Float);
                Parameter[1].Value = objMining.varContactID;

                Parameter[2] = new SqlParameter("@Department", SqlDbType.VarChar, 100);
                Parameter[2].Value = objMining.varDeptValue;

                Parameter[3] = new SqlParameter("@Counter", SqlDbType.Int);
                Parameter[3].Value = objMining.varCounterID;


                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_InsertMiningDep", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public bool InsertMiningSCO(cMining objMining)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[9];

                Parameter[0] = new SqlParameter("@Value", SqlDbType.VarChar, 10);
                Parameter[0].Value = objMining.varValue;

                Parameter[1] = new SqlParameter("@Contact_number", SqlDbType.Float);
                Parameter[1].Value = objMining.varContactID;

                Parameter[2] = new SqlParameter("@MailAddress", SqlDbType.VarChar, 200);
                Parameter[2].Value = objMining.varMailAddress;

                Parameter[3] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                Parameter[3].Value = objMining.varFirstName;

                Parameter[4] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                Parameter[4].Value = objMining.varLastName;

                Parameter[5] = new SqlParameter("@Title", SqlDbType.VarChar, 50);
                Parameter[5].Value = objMining.varTitle;

                Parameter[6] = new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 30);
                Parameter[6].Value = objMining.varPhoneNumber;

                Parameter[7] = new SqlParameter("@ExtNumber", SqlDbType.VarChar, 30);
                Parameter[7].Value = objMining.varExtNumber;

                Parameter[8] = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 50);
                Parameter[8].Value = objMining.varEmailAddress;


                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_InsertMiningSCO", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }


        }


        public bool InsertMiningNotes(cMining objMining)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@SiteNumber", SqlDbType.VarChar, 10);
                Parameter[0].Value = objMining.varValue;

                Parameter[1] = new SqlParameter("@Notes", SqlDbType.NVarChar,500);
                Parameter[1].Value = objMining.varNotes;

                Parameter[2] = new SqlParameter("@Createdby", SqlDbType.NChar, 10);
                Parameter[2].Value = SessionFacade.LoggedInUserName;



                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_InsertMiningNotes", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }


        }
    }
}