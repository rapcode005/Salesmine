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
using System.Web.UI.WebControls;


namespace AppLogic
{
    public class cProductSummary 
    {

        public static SqlDataReader drProductSummary;

        //private string mstrCampaignName;
        //private string mstrSKUCategory;
        //private string mstrSoldTo;
        //private string mstrBuyerCt;


        //public string CampaignName
        //{
        //    get { return mstrCampaignName; }
        //    set { mstrCampaignName = value; }
        //}

        public string CampaignName { get; set; }

        //public string SKUCategory
        //{
        //    get { return mstrSKUCategory; }
        //    set { mstrSKUCategory = value; }
        //}

        public string SKUCategory { get; set; }

        //public string SoldTo
        //{
        //    get { return mstrSoldTo; }
        //    set { mstrSoldTo = value; }
        //}

        public string SoldTo { get; set; }

        //public string BuyerCt
        //{
        //    get { return mstrBuyerCt; }
        //    set { mstrBuyerCt = value; }
        //}

        public string BuyerCt { get; set; }

        #region GetProductSummary
        public DataSet GetProductSummary()
        {
            cProductSummaryDB objProductSummaryDB = new cProductSummaryDB();
            try
            {
                return objProductSummaryDB.GetProductSummary(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
        //
        #region Get All SKU Summary

        public DataSet GetAllSKUSummary()
        {
            cProductSummaryDB objProductSummaryDB = new cProductSummaryDB();
            try
            {
                return objProductSummaryDB.GetAllSKUSummary(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        //These 2 bellow will be used in Teritory Page

        #region GetTeritoryProductSummary
        public DataSet GetTeritoryProductSummary()
        {
            cProductSummaryDB objProductSummaryDB = new cProductSummaryDB();
            try
            {
                return objProductSummaryDB.GetTeritoryProductSummary(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
        //
        #region Get Teritory All SKU Summary

        public DataSet GetAllTeritorySKUSummary()
        {
            cProductSummaryDB objProductSummaryDB = new cProductSummaryDB();
            try
            {
                return objProductSummaryDB.GetAllTeritorySKUSummary(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        //For Excel Buttton
        public bool StatusExcelButton()
        {
            string[] Unit = { "PC-ONT", "ADMIN", "CUS" };
            bool Result = false;
            try
            {
                for (int index = 0; index <= 2; index++)
                {
                    Result = (SessionFacade.UserRole.Trim() == Unit[index]);
                    if (Result) break;
                }
                return (SessionFacade.CampaignName == "PC" && Result == true) ? true : false;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex,
                    "Error During StatusExcelButton()",
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;
            }
        }
    }
}