using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DataAccess;
using System.Configuration;
using BradyCorp.Log;
using AppLogic;

public class cSubmitMasterDataChangeDB
{
    #region AddPreferences //Following functions will be used in Submit Master Data Change
    public bool AddPreferences(cAddPreferences objAddPreferences)
    {
        try
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            SqlParameter[] Parameter = new SqlParameter[9];

            Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
            Parameter[0].Value = objAddPreferences.Account;

            Parameter[1] = new SqlParameter("@Contact", SqlDbType.Int);
            Parameter[1].Value = objAddPreferences.Contact;

            Parameter[2] = new SqlParameter("@ContactName", SqlDbType.VarChar, 100);
            Parameter[2].Value = objAddPreferences.ContactName;

            Parameter[3] = new SqlParameter("@Phone", SqlDbType.VarChar, 100);
            Parameter[3].Value = objAddPreferences.Phone;

            Parameter[4] = new SqlParameter("@Username", SqlDbType.VarChar, 16);
            Parameter[4].Value = objAddPreferences.Username;

            Parameter[5] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
            Parameter[5].Value = objAddPreferences.Campaign;

            Parameter[6] = new SqlParameter("@Mail", SqlDbType.VarChar, 100);
            Parameter[6].Value = objAddPreferences.Mail;

            Parameter[7] = new SqlParameter("@Fax", SqlDbType.VarChar, 100);
            Parameter[7].Value = objAddPreferences.Fax;

            Parameter[8] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
            Parameter[8].Value = objAddPreferences.Email;

            int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddPreferences", Parameter);
            return NoOfUpdatedRecords > 0 ? true : false;
        }
        catch (Exception ex)
        {
            BradyCorp.Log.LoggerHelper.LogException(ex, "AddPreferences", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            //LoggerHelper.LogMessage(ex.ToString());
            throw new Exception(ex.Message, ex);
        }
    }
    #endregion

    #region SelectPreferences //Following functions will be used in Submit Master Data Change
    public DataSet SelectPreferences(cAddPreferences objAddPreferences)
    {
        try
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            SqlParameter[] Parameter = new SqlParameter[2];
            DataSet dsPreferences = new DataSet();

            Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
            Parameter[0].Value = objAddPreferences.Account.Replace("'", "''");

            Parameter[1] = new SqlParameter("@Campaign", SqlDbType.VarChar, 16);
            Parameter[1].Value = objAddPreferences.Campaign;

            dsPreferences = DBHelper.ExecuteQueryToDataSet("SelectPreferences", Parameter);
            return dsPreferences;
        }
        catch (Exception ex)
        {
            BradyCorp.Log.LoggerHelper.LogException(ex, "SelectPreferences", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            //LoggerHelper.LogMessage(ex.ToString());
            throw new Exception(ex.Message, ex);
        }
    }
    #endregion

    #region AddAccountChanges //Following functions will be used in Submit Master Data Change
    public bool AddAccountChanges(cAddAccountChanges objAddAccountChanges)
    {
        try
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            SqlParameter[] Parameter = new SqlParameter[13];

            Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
            Parameter[0].Value = objAddAccountChanges.Account;

            Parameter[1] = new SqlParameter("@Phone", SqlDbType.VarChar, 16);
            Parameter[1].Value = objAddAccountChanges.Phone;

            Parameter[2] = new SqlParameter("@Fax", SqlDbType.VarChar, 50);
            Parameter[2].Value = objAddAccountChanges.Fax;

            Parameter[3] = new SqlParameter("@City", SqlDbType.VarChar, 100);
            Parameter[3].Value = objAddAccountChanges.City;

            Parameter[4] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
            Parameter[4].Value = objAddAccountChanges.Username;

            Parameter[5] = new SqlParameter("@State", SqlDbType.VarChar, 100);
            Parameter[5].Value = objAddAccountChanges.State;

            Parameter[6] = new SqlParameter("@Zip", SqlDbType.VarChar, 20);
            Parameter[6].Value = objAddAccountChanges.Zip;

            Parameter[7] = new SqlParameter("@Country", SqlDbType.VarChar, 100);
            Parameter[7].Value = objAddAccountChanges.Country;

            Parameter[8] = new SqlParameter("@Address1", SqlDbType.VarChar, 500);
            Parameter[8].Value = objAddAccountChanges.Address1;

            Parameter[9] = new SqlParameter("@Address2", SqlDbType.VarChar, 500);
            Parameter[9].Value = objAddAccountChanges.Address2;

            Parameter[10] = new SqlParameter("@AccountName", SqlDbType.VarChar, 100);
            Parameter[10].Value = objAddAccountChanges.AccountName;

            Parameter[11] = new SqlParameter("@Comment", SqlDbType.VarChar, 256);
            Parameter[11].Value = objAddAccountChanges.Comment;

            Parameter[12] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
            Parameter[12].Value = objAddAccountChanges.Campaign;

            int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddAccountChanges", Parameter);
            return NoOfUpdatedRecords > 0 ? true : false;
        }
        catch (Exception ex)
        {
            BradyCorp.Log.LoggerHelper.LogException(ex, "AddAccountChanges(", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            //LoggerHelper.LogMessage(ex.ToString());
            throw new Exception(ex.Message, ex);
        }
    }
    #endregion

    #region SelectAccountChanges //Following functions will be used in Submit Master Data Change
    public DataSet SelectAccountChanges(cAddAccountChanges objAddAccountChanges)
    {
        try
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            SqlParameter[] Parameter = new SqlParameter[1];
            DataSet dsAccountChanges = new DataSet();

            Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
            Parameter[0].Value = objAddAccountChanges.Account.Replace("'", "''");

            dsAccountChanges = DBHelper.ExecuteQueryToDataSet("SelectAccountChanges", Parameter);
            return dsAccountChanges;
        }
        catch (Exception ex)
        {
            BradyCorp.Log.LoggerHelper.LogException(ex, "SelectAccountChanges", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            //LoggerHelper.LogMessage(ex.ToString());
            throw new Exception(ex.Message, ex);
        }
    }
    #endregion

    #region AddContactChanges //Following functions will be used in Submit Master Data Change
    public bool AddContactChanges(cAddContactChanges objAddContactChanges)
    {
        try
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            SqlParameter[] Parameter = new SqlParameter[14];

            Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
            Parameter[0].Value = objAddContactChanges.Account;

            Parameter[1] = new SqlParameter("@Firstname", SqlDbType.VarChar, 100);
            Parameter[1].Value = objAddContactChanges.Firstname;

            Parameter[2] = new SqlParameter("@Lastname", SqlDbType.VarChar, 100);
            Parameter[2].Value = objAddContactChanges.Lastname;

            Parameter[3] = new SqlParameter("@Status", SqlDbType.VarChar, 30);
            Parameter[3].Value = objAddContactChanges.Status;

            Parameter[4] = new SqlParameter("@Function", SqlDbType.VarChar, 25);
            Parameter[4].Value = objAddContactChanges.Function;

            Parameter[5] = new SqlParameter("@Title", SqlDbType.VarChar, 100);
            Parameter[5].Value = objAddContactChanges.Title;

            Parameter[6] = new SqlParameter("@Phone", SqlDbType.VarChar, 20);
            Parameter[6].Value = objAddContactChanges.Phone;

            Parameter[7] = new SqlParameter("@PhoneExt", SqlDbType.VarChar, 30);
            Parameter[7].Value = objAddContactChanges.PhoneExt;

            Parameter[8] = new SqlParameter("@Department", SqlDbType.VarChar, 50);
            Parameter[8].Value = objAddContactChanges.Department;

            Parameter[9] = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            Parameter[9].Value = objAddContactChanges.Email;

            Parameter[10] = new SqlParameter("@Username", SqlDbType.VarChar, 10);
            Parameter[10].Value = objAddContactChanges.Username;

            Parameter[11] = new SqlParameter("@Contact", SqlDbType.Int);
            Parameter[11].Value = objAddContactChanges.Contact;

            Parameter[12] = new SqlParameter("@Comment", SqlDbType.VarChar, 256);
            Parameter[12].Value = objAddContactChanges.Comment;

            Parameter[13] = new SqlParameter("@Campaign", SqlDbType.VarChar, 10);
            Parameter[13].Value = objAddContactChanges.Campaign;

            int NoOfUpdatedRecords = DBHelper.ExecuteNonQuery("AddContactChanges", Parameter);
            return NoOfUpdatedRecords > 0 ? true : false;
        }
        catch (Exception ex)
        {
            BradyCorp.Log.LoggerHelper.LogException(ex, "AddContactChanges", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            //LoggerHelper.LogMessage(ex.ToString());
            throw new Exception(ex.Message, ex);
        }
    }
    #endregion

    #region SelectContactChanges //Following functions will be used in Submit Master Data Change
    public DataSet SelectContactChanges(cAddContactChanges objAddContactChanges)
    {
        try
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            SqlParameter[] Parameter = new SqlParameter[1];
            DataSet dsContactChanges = new DataSet();

            Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
            Parameter[0].Value = objAddContactChanges.Account.Replace("'","''");

            dsContactChanges = DBHelper.ExecuteQueryToDataSet("SelectContactChanges", Parameter);
            return dsContactChanges;
        }
        catch (Exception ex)
        {
            BradyCorp.Log.LoggerHelper.LogException(ex, "SelectContactChanges", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
            //LoggerHelper.LogMessage(ex.ToString());
            throw new Exception(ex.Message, ex);
        }
    }
    #endregion
}
