using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using DataAccess;

namespace AppLogic
{
    public class cKamWindowDB
    {
        #region Get All SKU Summary
        //public DataSet GetKamData(cKamWindow objKamWindow)
        //{
        //    try
        //    {
        //        DataSet drReturnKamData = null;

        //        string SQLkamQuery = string.Empty;


        //        if (objKamWindow.KamSearchBy == "" || objKamWindow.KamSearchBy.Length == 0)
        //        {
        //            SQLkamQuery = "select SOLD_TO,NAME,MG_NAME,CONVERT(VARCHAR(20), CAST(LTSALES AS MONEY), 1) as LTSALES,LPDCUST from emed.SITEINFO where SOLD_TO <> '' AND salesteam='" + objKamWindow.KamSearchBySalesTeam + "'";

        //            if (objKamWindow.KamSearchByKamName.Length > 0)
        //            {
        //                SQLkamQuery = SQLkamQuery + " AND NAME like '%" + objKamWindow.KamSearchByKamName + " %'";
        //            }
        //            if (objKamWindow.KamSearchByMGName.Length > 0)
        //            {
        //                SQLkamQuery = SQLkamQuery + " AND MG_NAME like '%" + objKamWindow.KamSearchByMGName + " %'";
        //            }
        //        }


        //        string test = SQLkamQuery;
        //        drReturnKamData = DBHelper.ExecuteQueryToDataSet(SQLkamQuery);

        //        return drReturnKamData;
        //    }
        //    catch (Exception ex)
        //    {
        //        BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
        //        throw new Exception(ex.Message, ex);
        //    }

        //}

        public DataSet GetAutocompleteResult(cKamWindow objKamWindow)
        {
            try
            {
                DataSet drReturnKamData =  new DataSet();

                SqlParameter[] Parameter = new SqlParameter[3];

                Parameter[0] = new SqlParameter("@Campaign", SqlDbType.VarChar, 15);
                Parameter[0].Value = objKamWindow.KamCampaign.Replace("'", "");

                Parameter[1] = new SqlParameter("@SalesTeam", SqlDbType.VarChar, 100);
                Parameter[1].Value = objKamWindow.KamID;

                Parameter[2] = new SqlParameter("@Search", SqlDbType.VarChar, 100);
                Parameter[2].Value = objKamWindow.KamSearch;

                drReturnKamData = DBHelper.ExecuteQueryToDataSet("SelectKAMAutoComplete", Parameter);

                return drReturnKamData;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

        }
        #endregion
    }
}