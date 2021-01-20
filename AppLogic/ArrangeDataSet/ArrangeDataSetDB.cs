using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using BradyCorp.Log;

using DataAccess;


namespace AppLogic
{
    public class cArrangeDataSetDB
    {

        #region ColumnReorderCount 
        //This Function will returns the count fromt the column-reorder table depending on username 
        public int ColumnReorderCount(cArrangeDataSet objADS)
        {
            int NoOFRows = 0; 
            try
            {
                NoOFRows = DBHelper.ExecuteNonQueryWithoutParams("select count('X') from  " + objADS.CampaignName.Trim() + ".Column_Reorder where username='" + objADS.UserName + "' and Listview='"+ objADS.Listview + "'");
                return NoOFRows;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public int DeleteColumns(cArrangeDataSet objADS)
        {
            int NoOFRows = 0;
            try
            {
               // string DeleteSQl = "Delete from " + objADS.CampaignName.Trim() + ".Column_Reorder where username = ' " + objADS.UserName +"'";

                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 20);
                Parameter[0].Value = objADS.CampaignName;

                Parameter[1] = new SqlParameter("@USERNAME", SqlDbType.VarChar, 20);
                Parameter[1].Value = objADS.UserName;

                Parameter[2] = new SqlParameter("@LISTVIEW", SqlDbType.VarChar, 20);
                Parameter[2].Value = objADS.Listview;


                NoOFRows = DBHelper.ExecuteNonQuery("DeleteColumnReorder", Parameter);


              //  NoOFRows = DBHelper.ExecuteNonQuery(DeleteSQl);
                return NoOFRows;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public int InsertColumns(cArrangeDataSet objADS)
        {
            int NoOFRows = 0; 
            try
            {
                string InsertSQl = "Insert into " + objADS.CampaignName.Trim() + ".Column_Reorder (username,ListView,ColumnName,ColumnIndex,Position) values ('" + objADS.UserName + "','" + objADS.Listview + "','" + objADS.InsertColumnList + "',0,'" + objADS.InsertPosition + "')";
                NoOFRows = DBHelper.ExecuteNonQueryWithoutParams(InsertSQl);
                return NoOFRows;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }
        
        #endregion

        // Bellow Gets Column name and its position frm the Column Reorder Table. 
        //Params to send are - Campaign Name, UserName,which data
        #region ColumnOrderList
        public DataSet GetColumnOrderList(cArrangeDataSet objADS)
        {
            try
            {
                DataSet drUsers = null;

                SqlParameter[] Parameter = new SqlParameter[3];
              
                Parameter[0] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 20);
                Parameter[0].Value = objADS.CampaignName;

                Parameter[1] = new SqlParameter("@USERNAME", SqlDbType.VarChar, 20);
                Parameter[1].Value = objADS.UserName;

                Parameter[2] = new SqlParameter("@LISTVIEW", SqlDbType.VarChar, 20);
                Parameter[2].Value = objADS.Listview;

                drUsers = DBHelper.ExecuteQueryToDataSet("SelectColumnReorder", Parameter);

              

                return drUsers;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in ColumnOrderList Retrival");
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion

        #region ColumnDetails
        //Select column to be Included
        public DataSet GetColumnDetails(cArrangeDataSet objADS)
        {
            try
            {
                DataSet drUsers = null;

                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 10);
                Parameter[0].Value = objADS.CampaignName;

                Parameter[1] = new SqlParameter("@Page", SqlDbType.VarChar, 30);
                Parameter[1].Value = objADS.Listview;

                Parameter[2] = new SqlParameter("@Createdby", SqlDbType.VarChar, 10);
                Parameter[2].Value = SessionFacade.LoggedInUserName;

                drUsers = DBHelper.ExecuteQueryToDataSet("SelectColumnDetails", Parameter);

                return drUsers;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in Column Retrival");
                throw new Exception(ex.Message, ex);
            }

        }

        //Select to be not Included
        public DataSet GetColumnNotIncluded(cArrangeDataSet objADS)
        {
            try
            {
                DataSet drUsers = null;

                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@CAMPAIGN", SqlDbType.VarChar, 10);
                Parameter[0].Value = objADS.CampaignName;

                Parameter[1] = new SqlParameter("@Createdby", SqlDbType.VarChar, 10);
                Parameter[1].Value = SessionFacade.LoggedInUserName;

                Parameter[2] = new SqlParameter("@Page", SqlDbType.VarChar, 30);
                Parameter[2].Value = objADS.Listview;


                drUsers = DBHelper.ExecuteQueryToDataSet("SelectColumnNotIncluded", Parameter);

                return drUsers;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in Column Retrival");
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion
    }
}