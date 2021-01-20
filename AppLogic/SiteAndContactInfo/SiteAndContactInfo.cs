using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BradyCorp.Log;
using System.Text.RegularExpressions;

namespace AppLogic
{
    public class cSiteAndContactInfo
    {
        public string SearchAccount { get; set; }
        public string SearchCampaign { get; set; }

        public DataSet GetSiteAndContactInfo()
        {
            cSiteAndContactInfoDB objSiteAndContactInfoDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteAndContactInfoDB.GetListSiteContactInfo(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetAccInfo()
        {
            cSiteAndContactInfoDB objSiteAndContactInfoDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteAndContactInfoDB.GetAcctInfo(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //private void ReplaceQuotes()
        //{
        //    try
        //    {
        //        //if ((UserId != "") || (UserId != null))-
        //        //    UserId = UserId.Replace("'", "''");
        //        if ((CampaignName != "") && (CampaignName != null))
        //            CampaignName = CampaignName.Replace("'", "''");
        //        if ((UserName != "") && (UserName != null))
        //            UserName = UserName.Replace("'", "''");
        //        if ((KamId != "") && (KamId != null))
        //            KamId = KamId.Replace("'", "''");
        //        if ((KamName != "") && (KamName != null))
        //            KamName = KamName.Replace("'", "''");


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}
    }

    public class cUpdateQQ
    {
        public string Account { get; set; }
        public string Campaign { get; set; }
        public string ContactStatus { get; set; }
        public string ContFunc { get; set; }
        public int SP { get; set; }
        public int SpVendor { get; set; }
        public string Factor { get; set; }
        public string ContBudget { get; set; }
        public string Purchasing { get; set; }
        public string Contact { get; set; }
        public string Username { get; set; }
        public string Other { get; set; }

        public bool UpdateQQ()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.UpdateQQ(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }

        }

    }

    public class cUpdateQQPC
    {
        public string Account { get; set; }
        public string Campaign { get; set; }
        public string Spanish { get; set; }
        public string EmployeeSize { get; set; }
        public string Username { get; set; }
        public string ContStats { get; set; }
        public string ContFunction { get; set; }
        public string ContBudgets { get; set; }
        public string Qx { get; set; }
        public string Health { get; set; }
        public string Contact { get; set; }
        public string OtherEmployeeSize { get; set; }


        public bool UpdateQQPC()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.UpdateQQPC(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }
    }

    public class cSecondaryQ
    {
        public string Account { get; set; }
        public string Campaign { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }
        public string Username { get; set; }
        public int Contact { get; set; }

        public bool AddSecondaryQ()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.AddSecondQ(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }

        public DataSet GetcSecondaryQ()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.GetSecondQ(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }
    }

    public class cUpdateProducts
    {
        public string Account { get; set; }
        public int Contact { get; set; }
        public int FSM { get; set; }
        public int FAE { get; set; }
        public int LBL { get; set; }
        public int LO { get; set; }
        public int OS { get; set; }
        public int PVM { get; set; }
        public int PPE { get; set; }
        public int PI { get; set; }
        public int SFS { get; set; }
        public int SP { get; set; }
        public int SLS { get; set; }
        public int SCP { get; set; }
        public int SC { get; set; }
        public int TAGS { get; set; }
        public int TPS { get; set; }
        public int TC { get; set; }
        public int W { get; set; }
        public int ETO { get; set; }
        public string Username { get; set; }

        public bool UpdateProducts()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.UpdateProducts(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }

    }

    public class cAddVendors
    {
        public string Account { get; set; }
        public string Campaign { get; set; }
        public int Contact { get; set; }
        public string VendorName { get; set; }
        public string Comments { get; set; }
        public string Username { get; set; }

        public bool AddVendors()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.AddVendors(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }

        public DataSet GetVendors()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.GetVendors(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }
    }

    public class cProjects
    {
        public string Account { get; set; }
        public string Campaign { get; set; }
        public int Contact { get; set; }
        public string ProjectDate { get; set; }
        public string ProjectType { get; set; }
        public string Chance { get; set; }
        public int EstimatedAmt { get; set; }
        public string Username { get; set; }

        public bool AddProjects()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.AddProjects(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }

        public DataSet GetProjects()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.GetProjects(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }
    }

    public class cAddNewContact
    {
        public string Account { get; set; }
        public string Campaign { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }

        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                bool isEmail = Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                _Email = (isEmail) ? value : _Email;
            }
        }

        public string Notes { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public bool AddNewContacts()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.AddNewContacts(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }
    }

    public class cSafetyProducts
    {
        public string Account { get; set; }
        public int Contact { get; set; }
        public string Campaign { get; set; }
        public int Sp { get; set; }
        public string Username { get; set; }

        public DataSet GetSafetyProducts()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.GetSafetyProducts(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }

        public bool UpdateSafetyProducts()
        {
            cSiteAndContactInfoDB objSiteContactDB = new cSiteAndContactInfoDB();
            try
            {
                return objSiteContactDB.UpdateSafetyProducts(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString(), ex);
            }
        }
    }
}