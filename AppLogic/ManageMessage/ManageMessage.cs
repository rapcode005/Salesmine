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
    public class cManageMessage
    {
        public string Message { get; set; }
        public string Campaign { get; set; }
        public string State { get; set; }
        public string Username { get; set; }
        public string Date { get; set; }
        public string PreState { get; set; }
        public string PreCampaign { get; set; }

        public DataSet GetMessage()
        {
            cManageMessageDB objManageMessage = new cManageMessageDB();
            try
            {
                return objManageMessage.GetMessage();
            }
            catch (Exception ex)
            {
                LoggerHelper.LogMessage(ex.ToString());
                throw new Exception(ex.Message, ex);
            }
        }

        public bool AddStateMessage()
        {
            cManageMessageDB objManageMessage = new cManageMessageDB();
            try
            {
                return objManageMessage.AddNewStateMessage(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool EditStateMessage()
        {
            cManageMessageDB objManageMessage = new cManageMessageDB();
            try
            {
                return objManageMessage.EditNewStateMessage(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}