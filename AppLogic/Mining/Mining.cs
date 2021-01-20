using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using System.IO;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using System.Web.UI.WebControls;

using BradyCorp.Log;

namespace AppLogic
{
    public class cMining
    {
        public string varOptions { get; set; }
        public string varValue { get; set; }
        public string varActiveMineSite { get; set; }
        public string varAddressOfficeWare { get; set; }
        public string varNoQuestion { get; set; }
        public string varOtherNoQuestion { get; set; }
        public string varSafetySignsFacility { get; set; }
        public string varSafetySignsOffice { get; set; }
        public string varSafetySignsSite { get; set; }
        public string varPurchasingDeparment { get; set; }
        public string varCreatedBy { get; set; }
        public string varYesCorporateOfficeSiteLevel { get; set; }
        public string varYesCorporateOfficeSiteLevelOther { get; set; }
        public string varDisposition { get; set; }
        public string varDeptValue { get; set; }
        public string varContactID { get; set; }
        public string varCounterID { get; set; }

        public string varMailAddress { get; set; }
        public string varFirstName { get; set; }
        public string varLastName { get; set; }
        public string varTitle { get; set; }
        public string varPhoneNumber { get; set; }
        public string varExtNumber { get; set; }
        public string varEmailAddress { get; set; }

        public string varNotes { get; set; }
        
        public SqlDataReader drGetMiningInfo()
        {
            cMiningDB objMiningDB = new cMiningDB();
            try 
            {
                return objMiningDB.GetMiningInfoDReader(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet dsGetMiningInfo()
        { 
            cMiningDB objMiningDB = new cMiningDB();
            try
            {
                return objMiningDB.GetMiningDetailsDset(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet dsGetMiningQA()
        {
            cMiningDB objMiningDB = new cMiningDB();
            try
            { 
                return objMiningDB.GetMiningQA(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet dsGetMiningDept()
        {
            cMiningDB objMiningDB = new cMiningDB();
            try
            {
                return objMiningDB.GetMiningDeptsDset(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet dsGetMiningSCO() 
        {
            cMiningDB objMiningDB = new cMiningDB();
            try
            {
                return objMiningDB.GetMiningSCODset(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet dsGetMiningNotes()
        {
            cMiningDB objMiningDB = new cMiningDB();
            try
            {
                return objMiningDB.GetMiningNotesDset(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool UpdateQAMining()
        {
            cMiningDB objMiningDB = new cMiningDB();


            try
            {
                return objMiningDB.InsertMiningQA(this);
                
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool UpdateDeptMining()
        { 
            cMiningDB objMiningDB = new cMiningDB();


            try
            {
                return objMiningDB.InsertMiningDept(this);

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool UpdateSCOMining()
        { 
            cMiningDB objMiningDB = new cMiningDB();


            try
            {
                return objMiningDB.InsertMiningSCO(this);

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool InsertMiningNotes()
        {
            cMiningDB objMiningDB = new cMiningDB();


            try
            {
                return objMiningDB.InsertMiningNotes(this);

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}