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
    
    
    public class cUser
    {
        public static SqlDataReader dr1;

        private string mstrCampaignId;
        private string mstrCampaignName;
        private string mstrUserId;
        private string mstrUserName;
        private string mstrCreatedEditedBy;
        private string mstrDeleteUserName;
        private string mstrUserRole;
        private string mstrValidFrom;
        private string mstrValidTo;
        private string mstrKamId;
        private string mstrKamName;
        private string mstrAccountStatus;
        private string mstrSearchManageUserBy;
        private string mstrSearchManageUserByUserName;
        private string mstrSearchManageUserByCampaign;

        public string password { get; set; }
        
        public string CampaignID
        {
            get { return mstrCampaignId; }
            set { mstrCampaignId = value; }
        }
        public string CampaignName
        {
            get { return mstrCampaignName; }
            set { mstrCampaignName = value; }
        }
        public string UserId
        {
            get { return mstrUserId; }
            set { mstrUserId = value; }
        }
        
        public string UserName
        {
            get { return mstrUserName; }
            set { mstrUserName = value; }
        }

        public string CreatedEditedBy
        {
            get { return mstrCreatedEditedBy; }
            set { mstrCreatedEditedBy = value; }
        }
        


        public string DeleteUserName
        {
            get { return mstrDeleteUserName; }
            set { mstrDeleteUserName = value; }
        }
        
         public string UserRole
        {
            get { return mstrUserRole; }
            set { mstrUserRole = value; }
        }
        
        public string ValidFrom
        {
            get { return mstrValidFrom; }
            set { mstrValidFrom = value; }
        }
        public string ValidTo
        {
            get { return mstrValidTo; }
            set { mstrValidTo = value; }
        }
        public string KamId
        {
            get { return mstrKamId; }
            set { mstrKamId = value; }
        }
        public string KamName
        {
            get { return mstrKamName; }
            set { mstrKamName = value; }
        }

        public string AccountStatus
        {
            get { return mstrAccountStatus; }
            set { mstrAccountStatus = value; }
        }
        public string SearchManageUserBy
        {
            get { return mstrSearchManageUserBy; }
            set { mstrSearchManageUserBy = value; }
        }
        public string SearchManageUserByUserName
        {
            get { return mstrSearchManageUserByUserName; }
            set { mstrSearchManageUserByUserName = value; }
        }
        public string SearchManageUserByCampaign
        {
            get { return mstrSearchManageUserByCampaign; }
            set { mstrSearchManageUserByCampaign = value; }
        }

        //This following Class used in Customerized User Profile
        public string createdby { get; set; }
        public string campaignUnit { get; set; }
        public string campaignFullName { get; set; }
        public string PageNameList { get; set; }
        public bool order_history { get; set; }
        public bool product_summary { get; set; }
        public bool quotes { get; set; }
        public bool customer_info { get; set; }
        public bool customer_search { get; set; }
        public bool notes { get; set; }
        public bool construcion { get; set; }
        public bool mining { get; set; }
        public bool user { get; set; }
        public bool message { get; set; }
        public bool quotepipeline { get; set; }
        public bool quotedg { get; set; }

        public SqlDataReader GetUsers()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.GetUsers(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        //This is used in Audit Trail in SalesMine
        public string CreatedbyAT { get; set; }
        public string DescriptionAT { get; set; }
        public string TaskAT { get; set; }
        public string PageAT { get; set; }
        public string CampaignAT { get; set; }

        public bool InsetAuditTrailSalesMine()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.InsetAuditTrailSalesMine(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //This is used in Editing User Email Message
        public string EmailMessage { get; set; }
        public string EmailSubject { get; set; }
        public bool EditMessageEmail()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.EditMessageEmail(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public DataSet ShowEmailMessage()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.ShowMessageEmail();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        //This is used in Manage Users
        public DataSet GetUsersList()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.GetUserList(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public bool InsertUser()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                ReplaceQuotes();
                return objUsersDB.InsertUser(this);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in Insert User");
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error in Insert User", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;
                //throw new Exception(ex.Message.ToString(), ex);
            }

        }



        public bool InsertAT()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                ReplaceQuotes();
                return objUsersDB.InsertAT(this);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in Insert User");
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error in Insert User","","","","");
                return false;
                //throw new Exception(ex.Message.ToString(), ex);
            }

        }


        //Delete User
        public bool DeleteUser()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                ReplaceQuotes();
                return objUsersDB.DeleteUser(this);
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in Deleting User");
                BradyCorp.Log.LoggerHelper.LogException(ex, "Error in Deleting User", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;
                //throw new Exception(ex.Message.ToString(), ex);
            }

        }


        public bool UpdateUsers()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                ReplaceQuotes();
                return objUsersDB.UpdateUsers(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        }

        public void ReplaceQuotes()
        {
            try
            {
                //if ((UserId != "") || (UserId != null))
                //    UserId = UserId.Replace("'", "''");
                if ((CampaignName != "") && (CampaignName != null))
                    CampaignName = CampaignName.Replace("'", "''");
                if ((UserName != "") && (UserName != null))
                    UserName = UserName.Replace("'", "''");
                if ((KamId != "") && (KamId != null))
                    KamId = KamId.Replace("'", "''");
                if ((KamName != "") && (KamName != null))
                    KamName = KamName.Replace("'", "''");
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //This  function is used in Manage Users. 
        //Function will get Roles through Stored PRocedure and fills dropdown during edit mode
        public DataSet GetRoleList()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.GetRoleList(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool AddCutomizedScreen()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.AddCutomizedScreen(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet SelectCutomizedScreen()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.SelectCustomizedScreen(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet GetAllPageList()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.GetAllPageList(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet GetCurrencyCode()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.GetCurrencyCode(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet AllGetCurrencyCode()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.AllGetCurrencyCode();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet ShowAllNoteType()
        {
            cUserDB objUsersDB = new cUserDB();
            try
            {
                return objUsersDB.ShowAllNoteType();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }   

    }
    
}