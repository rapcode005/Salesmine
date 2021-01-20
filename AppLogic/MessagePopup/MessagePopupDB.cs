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
    public class cMessagePopupDB
    {
        #region GetMessage //Following functions will be used in State Pop-up Message
        public SqlDataReader GetMessage(cMessagePopup objMessagePopup)
        {
            SqlConnection Constre = new SqlConnection(ConfigurationManager.ConnectionStrings["SASManilaCONNECTON"].ToString());
            SqlDataReader drManageMessage = null;
            try
            {
                drManageMessage = DBHelper.ExecuteSqlReaderWithoutParameter("Select [Campaign],[State],[Message] from ManageMessage " +
                                                                            "where Campaign='" + objMessagePopup.Campaign + "' and " +
                                                                            "State='" + objMessagePopup.Region + "' and valid_to = '9999-12-31'");
                return drManageMessage;
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