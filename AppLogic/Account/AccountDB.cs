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
    public class cAccountDB
    {
        public DataSet GetAccountNameOnly(cAccount objAccount)
        {
            try
            {
                SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
                try
                {
                    SqlParameter[] Parameter = new SqlParameter[2];

                    Parameter[0] = new SqlParameter("@Account", SqlDbType.VarChar, 10);
                    int temp;
                    if (int.TryParse(objAccount.AccountName.Trim().ToString(), out temp))
                        Parameter[0].Value = objAccount.AccountName;
                    else
                        Parameter[0].Value = SessionFacade.AccountNo;

                    Parameter[1] = new SqlParameter("@CampaignName", SqlDbType.VarChar, 100);
                    if (objAccount.CampaignName.ToString().Trim() != "")
                        Parameter[1].Value = objAccount.CampaignName;
                    else
                        Parameter[1].Value = SessionFacade.CampaignValue;
                    
                    //Select = "select Name,REGION from  " + objAccount.CampaignName.Trim() + ".SITEINFO where sold_to='" + objAccount.AccountName + "'";
                   // SqlDataReader drCampaign = DBHelper.ExecuteSqlDataReader("Search_Acct_Info", Parameter);
                    int temp1 = 0;

                    if (Parameter[1].Value != "" && int.TryParse(Parameter[0].Value.ToString(), out temp1) == true)
                    {
                        DataSet drCampaign = new DataSet();

                        drCampaign = DBHelper.ExecuteQueryToDataSet("Search_Acct_Info", Parameter);
                        return drCampaign;
                    }
                    else
                        return null;
                   
                }
                catch (Exception ex)
                {
                    
                    BradyCorp.Log.LoggerHelper.LogException(ex, "AccoountDB-GetAccountNameOnly", SessionFacade.CampaignValue, SessionFacade.LoggedInUserName, SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                    return null;
                    throw new Exception(ex.Message.ToString(), ex);

                }
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message, ex);
            }

        }
    }
}