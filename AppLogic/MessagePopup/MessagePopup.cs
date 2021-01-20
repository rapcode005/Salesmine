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
    public class cMessagePopup
    {
        public string Region { get; set; }
        public string Campaign { get; set; }

        public SqlDataReader GetMessage()
        {
            cMessagePopupDB objMessagePopup = new cMessagePopupDB();
            try
            {
                return objMessagePopup.GetMessage(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}