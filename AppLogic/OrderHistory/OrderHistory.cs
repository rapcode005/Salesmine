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

    public class cOrderHistory : System.Web.UI.Page
    {

        private ShowOrders TypeSearch { get; set; }

        public string AccountHis { get; set; }

        private string campaignhis;
        public string CampaignHis
        {
            get
            {
                return campaignhis;
            }
            set
            {
                TypeSearch = new NoAddFilter();
                campaignhis = value;
            }
        }

        public string Sdate { get; set; }

        private string edate;
        public string Edate 
        {
            get
            {
                return edate;
            }
            set
            {
                TypeSearch = (value != null) ? new ByDate() : TypeSearch;
                edate = value;
            }
        }

        public int year { get; set; }

        private int IntyrType;
        public int intyrtype 
        { 
            get
            {
                return IntyrType;
            }
            set
            {
                TypeSearch = (value == 1 || value == 0) ? new ByCal() : TypeSearch;
                IntyrType = value;
            }
        }

        private string ordnum;
        public string OrdNum 
        {
            get
            {
                return ordnum;
            }
            set
            {
                TypeSearch = (value != string.Empty) ? new OrderNumber() : TypeSearch;
                ordnum = value;
            }
        }

        private string ponum;
        public string PONum 
        {
            get
            {
                return ponum;
            }
            set
            {
                TypeSearch = (value != string.Empty) ? new PONumber() : TypeSearch;
                ponum = value;
            }
        }

        public DataSet GetOrderHistory_Search(string SAccount, string SCampaignName, string Sdate, string Edate, 
            string ORdNum, int Year, string PONo, int yrtype)
        {
            cOrderHistoryDB objOrderHistory = new cOrderHistoryDB();
            try
            {
                return objOrderHistory.GetOrderHistory_Search(SAccount, SCampaignName, Sdate, Edate, ORdNum, Year, PONo, yrtype);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool StatusExcelButton()
        {
            string[] Unit = { "PC-ONT", "ADMIN", "CUS" };
            bool Result = false;
            try
            {
                //while (index <= 2 && SessionFacade.UserRole.Trim() != Unit[index])
                //{
                //    Condi = SessionFacade.UserRole.Trim() == Unit[index];
                //    index++;
                //}

                //Result = (SessionFacade.CampaignName == "PC" && Condi == true) ? true : false;

                //return Result;
                for (int index = 0; index <= 2; index++)
                {
                    Result = (SessionFacade.UserRole.Trim() == Unit[index]);
                    if (Result) break;
                }

                return (SessionFacade.CampaignName == "PC" && Result == true) ? true : false;

            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogException(ex,
                    "Error During StatusExcelButton()", 
                    SessionFacade.CampaignValue, SessionFacade.LoggedInUserName,
                    SessionFacade.AccountNo.ToString(), SessionFacade.KamId.ToString());
                return false;
            }
        }

        public DataSet LoadOrders()
        {
            return TypeSearch.LoadOrders(this);
        }
        
    }

    public abstract class ShowOrders
    {
        public abstract DataSet LoadOrders(cOrderHistory objOrderHis);
    }

    public class NoAddFilter : ShowOrders
    {
        public override DataSet LoadOrders(cOrderHistory objOrderHis)
        {
            cOrderHistoryDB objOrderHistory = new cOrderHistoryDB();
            return objOrderHistory.GetOrderHistory_Search(objOrderHis.AccountHis, objOrderHis.CampaignHis,
                "1/1/1700", "1/1/1700", null, -9999, null, -1);
        }
    }

    public class ByDate : ShowOrders
    {
        public override DataSet LoadOrders(cOrderHistory objOrderHis)
        {
            cOrderHistoryDB objOrderHistory = new cOrderHistoryDB();
            return objOrderHistory.GetOrderHistory_Search(objOrderHis.AccountHis, objOrderHis.CampaignHis,
                objOrderHis.Sdate, objOrderHis.Edate, null, -9999, null, -1);
        }
    }

    public class ByCal : ShowOrders
    {
        public override DataSet LoadOrders(cOrderHistory objOrderHis)
        {
            cOrderHistoryDB objOrderHistory = new cOrderHistoryDB();
            return objOrderHistory.GetOrderHistory_Search(objOrderHis.AccountHis, objOrderHis.CampaignHis,
                "1/1/1700", "1/1/1700", null, objOrderHis.year, null, objOrderHis.intyrtype);
        }
    }

    public class OrderNumber : ShowOrders
    {
        public override DataSet LoadOrders(cOrderHistory objOrderHis)
        {
            cOrderHistoryDB objOrderHistory = new cOrderHistoryDB();
            return objOrderHistory.GetOrderHistory_Search(objOrderHis.AccountHis, objOrderHis.CampaignHis,
                objOrderHis.Sdate, objOrderHis.Edate, objOrderHis.OrdNum,
                objOrderHis.year, null, objOrderHis.intyrtype);
        }
    }

    public class PONumber : ShowOrders
    {
        public override DataSet LoadOrders(cOrderHistory objOrderHis)
        {
            cOrderHistoryDB objOrderHistory = new cOrderHistoryDB();
            return objOrderHistory.GetOrderHistory_Search(objOrderHis.AccountHis, objOrderHis.CampaignHis,
                 objOrderHis.Sdate, objOrderHis.Edate, null, objOrderHis.year,
                 objOrderHis.PONum, objOrderHis.intyrtype);
        }
    }

    //public class OrderSales
    //{
    //    public string Orders { get; set; }
    //    public string Sales { get; set; }
    //}
}