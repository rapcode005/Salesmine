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
    public class cChatMessage
    {
        public string Message { get; set; }


        public bool NewMessage()
        {
            cChatMessageDB objMessage = new cChatMessageDB();
            try
            {
                return objMessage.WriteMessage(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataTable GetMessage()
        {
            cChatMessageDB objMessage = new cChatMessageDB();
            try
            {
                return objMessage.GetMessage(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}