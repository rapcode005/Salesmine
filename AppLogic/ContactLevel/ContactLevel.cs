using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using System.IO;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using BradyCorp.Log;

namespace AppLogic
{
    public class cContactLevel
    {
        public string SearchAccount { get; set; }
        public string SearchCampaign { get; set; }
        public string Page { get; set; }

        public DataSet GetListContactLevel()
        {
            cContactLevelDB objContactLevel = new cContactLevelDB();
            try
            {
                return objContactLevel.GetListContactLevel(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}