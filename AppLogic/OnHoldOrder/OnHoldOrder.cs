using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AppLogic
{
    public class cOnHoldOrder
    {

        public string Account { get; set; }
        public string Campaign { get; set; }
        public string Salesteam { get; set; }

        public DataSet GetOrderOnHold()
        {
            cOnHoldOrderDB objOrderOnHold = new cOnHoldOrderDB();
            try
            {
                return objOrderOnHold.GetOrderOnHold(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetOrderOnHoldByKamID()
        {
            cOnHoldOrderDB objOrderOnHold = new cOnHoldOrderDB();
            try
            {
                return objOrderOnHold.GetOrderOnHoldByKamID(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetOrderOnHoldAll()
        {
            cOnHoldOrderDB objOrderOnHold = new cOnHoldOrderDB();
            try
            {
                return objOrderOnHold.GetOrderOnHoldAll(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}