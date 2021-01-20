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
    public class cConstructionDB
    {
        #region ReedTerritories //Following functions will be used in ReedTerritories Page
        public DataSet GetReedTerritories(cConstruction objConstruction)
        {
            try
            {   
                DataSet drReedTerritories = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@Status", SqlDbType.Int,32);
                Parameter[0].Value = objConstruction.iStatus;

                Parameter[1] = new SqlParameter("@Title", SqlDbType.VarChar, 200);
                if (objConstruction.VarTitle == null)
                {
                    Parameter[1].Value = "";
                }
                else
                {
                    Parameter[1].Value = objConstruction.VarTitle;
                }

                //Parameter[2] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                //Parameter[2].Value = SessionFacade.CampaignValue;

                Parameter[2] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[2].Value = "US";

                drReedTerritories.Tables.Add(DBHelper.ExecuteQueryToDataTable("sp_LoadReedTerritories", Parameter));

                return drReedTerritories;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet GetGeneralContractor(cConstruction objConstruction)
        {
            try
            {
                DataSet dsGeneralContractor = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];
                 
                Parameter[0] = new SqlParameter("@ProjectID", SqlDbType.VarChar, 15);
                Parameter[0].Value = objConstruction.varProjectID;

                //Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                //Parameter[1].Value = SessionFacade.CampaignValue;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[1].Value = "US";

                //Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                //Parameter[1].Value = objConstruction.SearchOrderCampaignName;

                dsGeneralContractor.Tables.Add(DBHelper.ExecuteQueryToDataTable("sp_LoadGeneralContractorNew", Parameter));

                return dsGeneralContractor;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }



        public DataSet GetSubContractor(cConstruction objConstruction)
        {
            try 
            {
                DataSet dsSubContractor = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@ProjectID", SqlDbType.VarChar, 10);
                Parameter[0].Value = objConstruction.varProjectID;

                //Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                //Parameter[1].Value = SessionFacade.CampaignValue;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[1].Value = "US";

                //Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                //Parameter[1].Value = objConstruction.SearchOrderCampaignName;

                dsSubContractor.Tables.Add(DBHelper.ExecuteQueryToDataTable("sp_LoadSubContractor", Parameter));

                return dsSubContractor;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }



        public DataSet GetCallNotes(cConstruction objConstruction)
        {
            try
            { 
                DataSet dsSubContractor = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@ProjectID", SqlDbType.VarChar, 10);
                Parameter[0].Value = objConstruction.varProjectID;

                //Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                //Parameter[1].Value = SessionFacade.CampaignValue;

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[1].Value = "US";

                //Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                //Parameter[1].Value = objConstruction.SearchOrderCampaignName;

                dsSubContractor.Tables.Add(DBHelper.ExecuteQueryToDataTable("sp_LoadCallNotes", Parameter));

                return dsSubContractor;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetConstructionDetails(cConstruction objConstruction)
        {
            try
            {
                DataSet dsSubContractor = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[2];

                Parameter[0] = new SqlParameter("@ProjectID", SqlDbType.VarChar, 15);
                Parameter[0].Value = objConstruction.varProjectID;               

                Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[1].Value = "US";

                dsSubContractor = DBHelper.ExecuteQueryToDataSet("GetConstructionDetailsNew", Parameter);
               

                return dsSubContractor;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet GetProjectInfo(cConstruction objConstruction)
        {
            try
            {
                DataSet dsSubContractor = new DataSet();

                SqlParameter[] Parameter = new SqlParameter[1];

                Parameter[0] = new SqlParameter("@Project_ID", SqlDbType.VarChar, 10);
                Parameter[0].Value = objConstruction.varProjectID;

                //Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                //Parameter[1].Value = objConstruction.SearchOrderCampaignName;

                dsSubContractor.Tables.Add(DBHelper.ExecuteQueryToDataTable("sp_GetProjectInfo", Parameter));

                return dsSubContractor;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        } 


        //public DataSet UpdateProjectStatus(cConstruction objConstruction)
        //{
        //    try
        //    { 
        //        DataSet dsSubContractor = new DataSet();

        //        SqlParameter[] Parameter = new SqlParameter[1];

        //        Parameter[0] = new SqlParameter("@ProjectID", SqlDbType.VarChar, 10);
        //        Parameter[0].Value = objConstruction.varProjectID;

        //        //Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
        //        //Parameter[1].Value = objConstruction.SearchOrderCampaignName;

        //        dsSubContractor.Tables.Add(DBHelper.ExecuteQueryToDataTable("sp_LoadCallNotes", Parameter));

        //        return dsSubContractor;
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerHelper.LogMessage(ex.ToString());
        //        throw new Exception(ex.Message, ex);
        //    }
        //}



        public bool UpdateProjectStatus(cConstruction objProjectStatus)
        {
            try 
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@ProjectID", SqlDbType.Float, 10);
                Parameter[0].Value = SessionFacade.PROJECTID;//objProjectStatus.varProjectID;
                 
                Parameter[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 10);
                Parameter[1].Value = SessionFacade.LoggedInUserName;

                Parameter[2] = new SqlParameter("@Status", SqlDbType.Bit);
                Parameter[2].Value = SessionFacade.PROJECTSTATUS; //objProjectStatus.varProjectStatus;

               

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_UpdateStatus", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }


        public bool UpdateGeneralCon(cConstruction objGeneralCon)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[11];

                Parameter[0] = new SqlParameter("@ProjectID", SqlDbType.Float, 10);
                Parameter[0].Value = SessionFacade.PROJECTID;//objProjectStatus.varProjectID;

                Parameter[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 10);
                Parameter[1].Value = SessionFacade.LoggedInUserName;

                Parameter[2] = new SqlParameter("@ContractorName", SqlDbType.VarChar, 100);
                Parameter[2].Value = objGeneralCon.varContractorName;

                Parameter[3] = new SqlParameter("@ContractorEmail", SqlDbType.VarChar, 50);
                Parameter[3].Value = objGeneralCon.varContractorEmail;

                Parameter[4] = new SqlParameter("@ContractorPhone", SqlDbType.VarChar, 30);
                Parameter[4].Value = objGeneralCon.varContractorPhone;

                Parameter[5] = new SqlParameter("@ContractorCustomer", SqlDbType.Int, 64);
                Parameter[5].Value = objGeneralCon.varContractorCustomer;

                Parameter[6] = new SqlParameter("@ContractorAccount", SqlDbType.VarChar, 20);
                Parameter[6].Value = objGeneralCon.varContractorAccount;

                Parameter[7] = new SqlParameter("@KAM", SqlDbType.Int, 64);
                Parameter[7].Value = objGeneralCon.varKAM;

                Parameter[8] = new SqlParameter("@CalledNotes", SqlDbType.Int, 32);
                Parameter[8].Value = objGeneralCon.varCalledNotes;

                Parameter[9] = new SqlParameter("@ID", SqlDbType.Int, 64);
                if (SessionFacade.GeneralConID != "")
                {
                    Parameter[9].Value = SessionFacade.GeneralConID;
                }
                else
                {
                    Parameter[9].Value = 0;
                }

                //Parameter[10] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                //Parameter[10].Value = SessionFacade.CampaignValue;

                Parameter[10] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[10].Value = "US";


                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_UpdateGeneralCon", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;

            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }


        public bool UpdateSubCon(cConstruction objSubCon)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[12];

                Parameter[0] = new SqlParameter("@SubCompanyName", SqlDbType.VarChar, 255);
                Parameter[0].Value = objSubCon.varSubCompanyName;

                Parameter[1] = new SqlParameter("@SubTitle", SqlDbType.VarChar, 255);
                Parameter[1].Value = objSubCon.varSubTitle;

                Parameter[2] = new SqlParameter("@SubName", SqlDbType.VarChar, 255);
                Parameter[2].Value = objSubCon.varSubName;

                Parameter[3] = new SqlParameter("@SubPhone", SqlDbType.VarChar, 255);
                Parameter[3].Value = objSubCon.varSubPhone;

                Parameter[4] = new SqlParameter("@SubEmail", SqlDbType.VarChar, 255);
                Parameter[4].Value = objSubCon.varSubEmail;

                Parameter[5] = new SqlParameter("@SubCustomer", SqlDbType.Int, 64);
                Parameter[5].Value = objSubCon.varSubCustomer;

                Parameter[6] = new SqlParameter("@SubKAM", SqlDbType.Int, 64);
                Parameter[6].Value = objSubCon.varSubKAM;

                Parameter[7] = new SqlParameter("@SubAccount", SqlDbType.VarChar, 255);
                Parameter[7].Value = objSubCon.varSubAccount;
                
                Parameter[8] = new SqlParameter("@SubNotes", SqlDbType.VarChar, 100);
                Parameter[8].Value = objSubCon.varSubNotes;

                Parameter[9] = new SqlParameter("@SubProjectID", SqlDbType.Float, 10);
                Parameter[9].Value = SessionFacade.PROJECTID;

                Parameter[10] = new SqlParameter("@SubUserName", SqlDbType.VarChar, 10);
                Parameter[10].Value = SessionFacade.LoggedInUserName;

                Parameter[11] = new SqlParameter("@ID", SqlDbType.Int, 64);
                Parameter[11].Value = objSubCon.varSubID;


                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_UpdateSubCon", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;

            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }




        public bool UpdateCallNotes(cConstruction objCallNotes)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[8];

                Parameter[0] = new SqlParameter("@varCallNotes", SqlDbType.VarChar, 300);
                Parameter[0].Value = objCallNotes.varCallNotes;

                Parameter[1] = new SqlParameter("@varFollowupCont", SqlDbType.VarChar, 150);
                Parameter[1].Value = objCallNotes.varFollowupCont;

                Parameter[2] = new SqlParameter("@varFollowupNotes", SqlDbType.VarChar, 150);
                Parameter[2].Value = objCallNotes.varFollowupNotes;

                Parameter[3] = new SqlParameter("@varNextfollowupDate", SqlDbType.VarChar, 150);
                Parameter[3].Value = objCallNotes.varNextfollowupDate;

                Parameter[4] = new SqlParameter("@varNumberOfCall", SqlDbType.Int, 64);
                Parameter[4].Value = objCallNotes.varNumberOfCall;

                Parameter[5] = new SqlParameter("@varUserName", SqlDbType.VarChar, 10);
                Parameter[5].Value = SessionFacade.LoggedInUserName;

                Parameter[6] = new SqlParameter("@varProjectID", SqlDbType.Float, 10);
                Parameter[6].Value = SessionFacade.PROJECTID;

                Parameter[7] = new SqlParameter("@varID", SqlDbType.Int, 64);
                Parameter[7].Value = objCallNotes.varCallID;



                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_UpdateCallNotes", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;

            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }



        public bool AddSubCon(cConstruction objSubCon)
        {
            try
            { 
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[12];

                Parameter[0] = new SqlParameter("@CompanyName", SqlDbType.VarChar, 255);
                Parameter[0].Value = objSubCon.varSubCompanyName;

                Parameter[1] = new SqlParameter("@Title", SqlDbType.VarChar, 255);
                Parameter[1].Value = objSubCon.varSubTitle;

                Parameter[2] = new SqlParameter("@SubName", SqlDbType.VarChar, 255);
                Parameter[2].Value = objSubCon.varSubName;

                Parameter[3] = new SqlParameter("@SubPhone", SqlDbType.VarChar, 255);
                Parameter[3].Value = objSubCon.varSubPhone;

                Parameter[4] = new SqlParameter("@SubEmail", SqlDbType.VarChar, 255);
                Parameter[4].Value = objSubCon.varSubEmail;

                Parameter[5] = new SqlParameter("@SubCustomer", SqlDbType.Int, 64);
                Parameter[5].Value = objSubCon.varSubCustomer;

                Parameter[6] = new SqlParameter("@SubKAM", SqlDbType.Int, 64);
                Parameter[6].Value = objSubCon.varSubKAM;

                Parameter[7] = new SqlParameter("@Account", SqlDbType.VarChar, 255);
                Parameter[7].Value = objSubCon.varSubAccount;

                Parameter[8] = new SqlParameter("@Notes", SqlDbType.VarChar, 100);
                Parameter[8].Value = objSubCon.varSubNotes;

                Parameter[9] = new SqlParameter("@ProjectID", SqlDbType.Float, 10);
                Parameter[9].Value = SessionFacade.PROJECTID;

                Parameter[10] = new SqlParameter("@UserName", SqlDbType.VarChar, 10);
                Parameter[10].Value = SessionFacade.LoggedInUserName;

                //Parameter[11] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                //Parameter[11].Value = SessionFacade.CampaignValue;

                Parameter[11] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[11].Value = "US";

                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_AddSubCon", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;

            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }




        public bool DeleteSubCon(cConstruction objSubCon)
        {
            try
            { 
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@varUID", SqlDbType.Int, 64);
                Parameter[0].Value = objSubCon.varSubID;

                Parameter[1] = new SqlParameter("@ProjectID", SqlDbType.Float, 10);
                Parameter[1].Value = SessionFacade.PROJECTID;

                //Parameter[2] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                //Parameter[2].Value = SessionFacade.CampaignValue;

                Parameter[2] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
                Parameter[2].Value = "US";


                int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("sp_DeleteSubCon", Parameter);
                return NoOfUpdatedRecords > 0 ? true : false;

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