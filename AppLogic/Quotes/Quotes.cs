using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using BradyCorp.Log;

namespace AppLogic
{
    public class cQuotes
    {
        public string SearchQuoteByAccount { get; set; }
        public string SearchQuoteByCampaign { get; set; }

        public DataTable GetListQuotes()
        {
            cQuotesDB objQuotesDB = new cQuotesDB();
            try
            {
                return objQuotesDB.GetListQuotes(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}