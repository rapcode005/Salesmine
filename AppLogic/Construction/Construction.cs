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
    public class cConstruction
    {
          //public string Query { get; set; }
        private string mstrQuery;
        private string mstrQuery1;

        public string SearchConstructionCampaignName { get; set; }
        public string SearchConstructionAccount { get; set; }
        public string SearchConstructionContact { get; set; }
        public string SearchConstructionStartDate { get; set; }
        public string SearchConstructionEndDate { get; set; }
        public string varProjectID { get; set; }
        public bool varProjectStatus { get; set; } 

        //GeneralContractor
        public string varContractorName { get; set; }
        public string varContractorEmail { get; set; }
        public string varContractorPhone { get; set; }
        public int varContractorCustomer { get; set; }
        public string varContractorAccount { get; set; }
        public int varKAM { get; set; }
        public string varUserName { get; set; }
        public int varCalledNotes { get; set; }
       


        //SubContractor
        public string varSubCompanyName { get; set; }
        public string varSubTitle { get; set; }
        public string varSubName { get; set; }
        public string varSubPhone { get; set; }
        public string varSubEmail { get; set; }
        public int varSubCustomer { get; set; }
        public int varSubKAM { get; set; }
        public string varSubAccount { get; set; }
        public string varSubNotes { get; set; }
        public int varSubID { get; set; }


        //CallNotes

        public string varCallNotes { get; set; }
        public string varFollowupCont { get; set; }
        public string varFollowupNotes { get; set; }
        public string varNextfollowupDate { get; set; }
        public int varNumberOfCall { get; set; }
        public int varCallID { get; set; }

        //ReedTerritories
        public int iStatus { get; set; }
        public string VarTitle { get; set; }
        
        public string Query
        {
            get { return mstrQuery; }
            set { mstrQuery = value; }
        }

        public string Query1
        {
            get { return mstrQuery1; }
            set { mstrQuery1 = value; }
        }

        public DataSet GetReedTerritories()
        {
            cConstructionDB objReedTerritories = new cConstructionDB();
            try
            {
                return objReedTerritories.GetReedTerritories(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetGeneralContractor()
        {
            cConstructionDB objReedTerritories = new cConstructionDB();
            try
            {
                return objReedTerritories.GetGeneralContractor(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet GetSubContractor()
        {
            cConstructionDB objSubContractor = new cConstructionDB();
            try
            {
                return objSubContractor.GetSubContractor(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetCallNotes()
        {
            cConstructionDB objSubContractor = new cConstructionDB();
            try
            {
                return objSubContractor.GetCallNotes(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetConstructionDetails()
        {
            cConstructionDB objSubContractor = new cConstructionDB();
            try
            {
                return objSubContractor.GetConstructionDetails(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public DataSet GetProjectInfo()
        {
            cConstructionDB objProjectInfo = new cConstructionDB();
            cConstruction objProjectInfo2 = new cConstruction();
            try
            {
                objProjectInfo2.varProjectID = SessionFacade.PROJECTID;
                return objProjectInfo.GetProjectInfo(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public bool UpdateProjectStatus()
        {
            cConstructionDB objProjectInfo = new cConstructionDB();

            
            try
            {
                return objProjectInfo.UpdateProjectStatus(this);

               
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool UpdateGeneralContractor()
        {
            cConstructionDB objProjectInfo = new cConstructionDB();


            try
            {
                return objProjectInfo.UpdateGeneralCon(this);


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool UpdateSubContractor()
        {
            cConstructionDB objSubCon = new cConstructionDB();


            try
            {
                return objSubCon.UpdateSubCon(this);


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public bool UpdateCallInfo()
        {
            cConstructionDB objCallInfo = new cConstructionDB();


            try
            {
                return objCallInfo.UpdateCallNotes(this);


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public bool AddSubContractor()
        {
            cConstructionDB objSubCon = new cConstructionDB();

            try
            {
                return objSubCon.AddSubCon(this);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public bool DeleteSubContractor()
        {
            cConstructionDB objSubCon = new cConstructionDB();

            try
            {
                return objSubCon.DeleteSubCon(this);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //public DataSet GetTotalOrdersSales()
        //{
        //    cOrderHistoryDB objOrderHistory = new cOrderHistoryDB();
        //    try
        //    {
        //        return objOrderHistory.GetTotalOrderTotalSales(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}


    }

    //public class OrderSales
    //{
    //    public string Orders { get; set; }
    //    public string Sales { get; set; }
    //}
    
}