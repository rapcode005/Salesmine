using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AppLogic;
using System.Collections;
using System.IO;
using ClassLibrary1;
using System.Globalization;

using System.Text;
using System.Data.SqlTypes;


using System.Text.RegularExpressions;

namespace WebSalesMine.WebPages.Home
{

    public partial class test : System.Web.UI.Page
    {
        private int OHDateOrdinal = 1000;
        private int OHUnitPrice = 1000;
        private int OHExtPrice = 1000;
        private int OHConvertedDate = 1000;
        private int OHuval = 1000;
        public string OHtxtTemp = "0000001447";
        public string OHddlCampaign = "EMED";
        public string OHddlTemp = "EMED";

        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                ShowOrderHistoryPage();
            }
            catch (Exception err)
            {

            }

        }

        #region Show Order History Function
        protected void ShowOrderHistoryPage()
        {
            OHSetButtonVisible();
            if (!IsPostBack)
            {
                if (OHtxtTemp.ToString().Trim() != "")
                    ShowOrder();
                else
                {

                }

                StatusSearchDateRange(false);
                StatusSearchYear(false);
                InsertCalendarYearToDropDown();
            }
            else
            {
                if (OHtxtTemp.ToString().Trim() != "")
                    ShowOrder();
                else
                {
                    OHgridOrderHistory.DataSource = null;
                    OHgridOrderHistory.DataBind();

                }
            }

        }

        #region Enable Disable Buttons on the page
        protected void OHSetButtonVisible()
        {
            if (SessionFacade.UserRole == "ADMIN")
            {
                OHbtnExportToExcel.Visible = true;
                OHBtnArrangeColumn.Visible = true;
            }
            else
            {
                OHbtnExportToExcel.Visible = false;
                OHBtnArrangeColumn.Visible = false;
            }
        }
        #endregion

        protected void btnShowOrders_Click(object sender, EventArgs e)
        {

            if (OHByDate.Checked == true)
            {
                if (OHtxtStartDate.Text == "")
                    OHlitErrorinGrid.Text = "Select Start Date.";
                else if (OHtxtEndDate.Text == "")
                    OHlitErrorinGrid.Text = "Select End Date.";
                else
                    ShowOrder();
            }
            else
            {
                ShowOrder();
            }
        }

        protected void ByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (OHByDate.Checked == true)
            {
                OHByCal.Checked = false;
                StatusSearchDateRange(true);
                StatusSearchYear(false);
            }
            else
                StatusSearchDateRange(false);
        }

        protected void ByCal_CheckedChanged(object sender, EventArgs e)
        {
            if (OHByCal.Checked == true)
            {
                OHByDate.Checked = false;
                OHtxtStartDate.Text = string.Empty;
                OHtxtEndDate.Text = string.Empty;
                StatusSearchDateRange(false);
                StatusSearchYear(true);
            }
            else
            {
                StatusSearchYear(false);
            }
        }

        protected void gridOrderHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool varColumnExist = false;
            int columnIndex;
            string[] list = { "Unit Price", "Ext Price","QTY",
                                "Line" };

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].CssClass = "locked";
                //e.Row.Cells[1].CssClass = "locked";
                //e.Row.Cells[2].CssClass = "locked";

                //Right Align of Currency Column
                for (int i = 0; i < list.Length; i++)
                {
                    columnIndex = 0;
                    foreach (DataControlFieldCell cell in e.Row.Cells)
                    {
                        if (cell.ContainingField is BoundField)
                        {
                            if (((BoundField)cell.ContainingField).DataField.Equals(list[i]))
                            {
                                varColumnExist = true;
                                break;
                            }
                            else
                                varColumnExist = false;
                        }
                        columnIndex++;
                    }

                    if (varColumnExist == true)
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                            e.Row.Cells[columnIndex].HorizontalAlign = HorizontalAlign.Right;

                    }
                }


                if (OHDateOrdinal != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Order Date"].ToString(), out temp) == true)
                        e.Row.Cells[OHDateOrdinal].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Order Date"]).ToString("MM/dd/yyyy");
                }
                if (OHUnitPrice != 1000)
                {
                    decimal temp;
                    if (decimal.TryParse(((DataRowView)e.Row.DataItem)["Unit Price"].ToString(), out temp) == true)
                        e.Row.Cells[OHUnitPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["Unit Price"].ToString()).ToString("C2");
                }
                if (OHExtPrice != 1000)
                {
                    decimal temp;
                    if (decimal.TryParse(((DataRowView)e.Row.DataItem)["Ext Price"].ToString(), out temp) == true)
                        e.Row.Cells[OHExtPrice].Text = decimal.Parse(((DataRowView)e.Row.DataItem)["Ext Price"].ToString()).ToString("C2");
                }
                if (OHConvertedDate != 1000)
                {
                    DateTime temp;
                    if (DateTime.TryParse(((DataRowView)e.Row.DataItem)["Converted Date"].ToString(), out temp) == true)
                        e.Row.Cells[OHConvertedDate].Text = Convert.ToDateTime(((DataRowView)e.Row.DataItem)["Converted Date"]).ToString("MM/dd/yyyy");
                }
            }
        }

        protected void ddiYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOrder();
        }

        #region Function



        private void GetTotalOrdersSales(DataTable dt)
        {
            FunctionVb Function = new FunctionVb();
            decimal sum = 0;
            DataView view;

            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            //Check Standard Order Only
            Query = "[Order Type]='Standard Order' and [Reason Rejection] Is Null and uvals='C'";

            dtTemp = dt.Clone();

            results = dt.Select(Query);

            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            //Total Sales
            foreach (DataRow dr in dtTemp.Rows)
                sum += Function.ConvertDecimal(dr[6].ToString().Trim());

            OHtxtbTatalSales.Text = decimal.Parse(sum.ToString()).ToString("C2");

            //Total Orders
            view = new DataView(dtTemp);

            dtTemp = view.ToTable(true, "Order Number");

            OHtxtbTotalOrders.Text = dtTemp.Rows.Count.ToString();

        }

        public DataSet BindOrderHistory()
        {
            string[] TempAccount = new string[6];
            DataSet drExisting;
            try
            {
                cOrderHistory objOrderHistory = new cOrderHistory();

                //Campaign
                objOrderHistory.SearchOrderCampaignName = OHddlTemp.ToString().Trim();

                //Search by Account
                objOrderHistory.SearchOrderAccount = OHtxtTemp.ToString().Trim();

                drExisting = objOrderHistory.GetOrderHistory();
                DataSet ReturnDs = null;
                if (drExisting.Tables.Count > 0)
                {
                    TempAccount = SessionFacade.LastAccount;

                    TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();

                    SessionFacade.LastAccount = TempAccount;

                    ReturnDs = drExisting;

                    OHDateOrdinal = ReturnDs.Tables[0].Columns["Order Date"].Ordinal;

                    OHUnitPrice = ReturnDs.Tables[0].Columns["Unit Price"].Ordinal;

                    OHExtPrice = ReturnDs.Tables[0].Columns["Ext Price"].Ordinal;

                    OHConvertedDate = ReturnDs.Tables[0].Columns["Converted Date"].Ordinal;

                    OHuval = ReturnDs.Tables[0].Columns["uvals"].Ordinal;

                }
                else
                {
                    ReturnDs = null;
                }



                return ReturnDs;

            }
            catch (Exception ex)
            {
                OHDateOrdinal = 1000;
                BradyCorp.Log.LoggerHelper.LogException(ex, "Showing Order History - BindingData");
                OHlitErrorinGrid.Text = "There was a error while retrieving Data";
                OHgridOrderHistory.DataSource = null;
                OHgridOrderHistory.DataBind();
                return null;
            }
        }


        protected System.Data.DataTable DtOrders
        {
            get // a DataTable filled in any way, for example:
            {

                CustomerLookup cCustLookup = new CustomerLookup();
                DataSet ds;
                DataTable dtTemp = new DataTable();

                var datable = new System.Data.DataTable();


                cCustLookup.CampaignName = SessionFacade.CampaignName.ToString().Trim();

                try
                {
                    datable = Orders();//cCustLookup.GetCustomerLookUp().Tables[0];

                }
                catch
                {

                }


                var dt = new System.Data.DataTable();

                foreach (DataColumn dc in datable.Columns)
                {
                    dt.Columns.Add(dc.ColumnName.ToString(), dc.DataType).Caption = dc.ColumnName.ToString();

                }

                DataRow[] results;

                string Query;

                Query = " 1=1";
                results = datable.Select(Query);


                DateTime temp;
                foreach (DataRow dr in datable.Rows)
                {
                    object[] array = new object[datable.Columns.Count];
                    for (int i = 0; i < datable.Columns.Count; i++)
                    {
                        if (datable.Columns[i].Caption.Trim() == "Order Date" ||
                        datable.Columns[i].Caption.Trim() == "Converted Date")
                        {
                            if (DateTime.TryParse(dr[i].ToString(), out temp) == false)
                            {
                                array[i] = default(DateTime);

                                //string.Format(String.Format("{0:MM/dd/yy}",array[i]);
                            }
                            else
                                array[i] = dr[i];
                        }
                        else
                            array[i] = dr[i];
                    }
                    dt.Rows.Add(array);
                }


                return dt;
            }
        }

        public DataTable Orders()
        {
            DataTable dtOrders = new DataTable();

            SessionFacade.CustomerLookUp = "";
            SessionFacade.OrderLookUp = "";
            DataTable dtTemp = new DataTable();
            string[] TempAccount = new string[6];
            DataSet ds = new DataSet();
            DataSet dsTotalOrderSales = new DataSet();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";

            if ((SessionFacade.LastAccount[0] == OHtxtTemp.ToString())
                &&
               (OHddlTemp.ToString() == SessionFacade.CampaignValue))
            {
                dtTemp = GetDatafromXML();

                if (dtTemp == null)
                {
                    ds = BindOrderHistory();

                    //if (ds != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    dtTemp = GetDatafromXML();
                    //}
                }

                if (dtTemp.Rows.Count > 0)
                {
                    DataSet ReArrangeDs = new DataSet();
                    ReArrangeDs.Tables.Add(dtTemp);

                    GetTotalOrdersSales(dtTemp);

                    //Working
                    if (ReArrangeDs.Tables[0].Columns.Contains("uvals") == true)
                        ReArrangeDs.Tables[0].Columns.Remove("uvals");

                    OHgridOrderHistory.DataSource = OHArrangeColumn(ReArrangeDs);
                    OHgridOrderHistory.DataBind();
                    dtOrders = OHArrangeColumn(ReArrangeDs).Tables[0];
                    OHlitErrorinGrid.Text = "";
                    if (ReArrangeDs.Tables[0].Rows.Count > 0)
                    {

                        OHlitErrorinGrid.Text = "";
                    }
                    else
                    {

                        OHlitErrorinGrid.Text = "No Record Found";
                    }
                }
                else
                {
                    OHgridOrderHistory.DataSource = null;
                    OHgridOrderHistory.DataBind();
                }

            }
            else
            {
                ds = BindOrderHistory();

                System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtTemp = GetDatafromXML();

                    GetTotalOrdersSales(dtTemp);

                    DataSet ReArrangeDs = new DataSet();
                    ReArrangeDs.Tables.Add(dtTemp);
                    if (OHuval != 1000)
                    {
                        ReArrangeDs.Tables[0].Columns.Remove("uvals");
                    }

                    ReArrangeDs = OHArrangeColumn(ReArrangeDs);

                    //Working

                    ReArrangeDs.AcceptChanges();

                    OHgridOrderHistory.DataSource = ReArrangeDs;

                    OHgridOrderHistory.DataBind();

                    //passing datatable to return value
                    dtOrders = ReArrangeDs.Tables[0];

                    if (ReArrangeDs.Tables[0].Rows.Count > 0)
                    {
                        OHlitErrorinGrid.Text = "";
                    }
                    else
                    {
                        OHlitErrorinGrid.Text = "No Record Found";
                    }

                    try
                    {
                        string strAccntNo = ds.Tables[0].Rows[0]["Account Number"].ToString().Trim();

                        if (strAccntNo == SessionFacade.AccountNo.ToString().Trim())
                        {
                            SessionFacade.AccountNo = strAccntNo;
                            TempAccount = SessionFacade.LastAccount;
                            TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();
                            SessionFacade.LastAccount = TempAccount;
                        }
                        else
                        {
                            SessionFacade.AccountNo = "";
                            TempAccount = SessionFacade.LastAccount;
                            TempAccount[0] = "";
                            SessionFacade.LastAccount = TempAccount;
                        }
                    }
                    catch
                    {
                        SessionFacade.AccountNo = "";
                        TempAccount = SessionFacade.LastAccount;
                        TempAccount[0] = "";
                        SessionFacade.LastAccount = TempAccount;
                    }



                }


                else
                {
                    //   SessionFacade.AccountNo = "";

                    OHgridOrderHistory.DataSource = null;
                    OHgridOrderHistory.DataBind();
                    OHtxtbTotalOrders.Text = "0";
                    OHtxtbTatalSales.Text = "$0.00";
                    OHlitErrorinGrid.Text = "No Record Found";

                    TempAccount = SessionFacade.LastAccount;
                    TempAccount[0] = "";
                    SessionFacade.LastAccount = TempAccount;
                }


            }

            return dtOrders;
        }



        public void ShowOrder()
        {
            try
            {
                SessionFacade.CustomerLookUp = "";
                SessionFacade.OrderLookUp = "";
                DataTable dtTemp = new DataTable();
                string[] TempAccount = new string[6];
                DataSet ds = new DataSet();
                DataSet dsTotalOrderSales = new DataSet();

                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";
                string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" +
                    Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                    + "-OrderHistoryArrangeColumn" + ".xml";

                if ((SessionFacade.LastAccount[0] == OHtxtTemp.ToString())
                    &&
                   (OHddlTemp.ToString() == SessionFacade.CampaignValue))
                {
                    dtTemp = GetDatafromXML();

                    if (dtTemp == null)
                    {
                        ds = BindOrderHistory();

                        //if (ds != null && ds.Tables[0].Rows.Count > 0)
                        //{
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();

                        dtTemp = GetDatafromXML();
                        //}
                    }

                    if (dtTemp.Rows.Count > 0)
                    {
                        DataSet ReArrangeDs = new DataSet();
                        ReArrangeDs.Tables.Add(dtTemp);

                        GetTotalOrdersSales(dtTemp);

                        //Working
                        if (ReArrangeDs.Tables[0].Columns.Contains("uvals") == true)
                            ReArrangeDs.Tables[0].Columns.Remove("uvals");

                        OHgridOrderHistory.DataSource = OHArrangeColumn(ReArrangeDs);
                        OHgridOrderHistory.DataBind();

                        OHlitErrorinGrid.Text = "";
                        if (ReArrangeDs.Tables[0].Rows.Count > 0)
                        {
                            OHlitErrorinGrid.Text = "";
                        }
                        else
                        {
                            OHlitErrorinGrid.Text = "No Record Found";
                        }
                    }
                    else
                    {
                        OHgridOrderHistory.DataSource = null;
                        OHgridOrderHistory.DataBind();
                    }


                }
                else
                {
                    ds = BindOrderHistory();

                    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                    xmlSW.Close();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtTemp = GetDatafromXML();

                        GetTotalOrdersSales(dtTemp);

                        DataSet ReArrangeDs = new DataSet();
                        ReArrangeDs.Tables.Add(dtTemp);
                        if (OHuval != 1000)
                        {
                            ReArrangeDs.Tables[0].Columns.Remove("uvals");
                        }

                        ReArrangeDs = OHArrangeColumn(ReArrangeDs);

                        //Working

                        ReArrangeDs.AcceptChanges();

                        OHgridOrderHistory.DataSource = ReArrangeDs;

                        OHgridOrderHistory.DataBind();

                        if (ReArrangeDs.Tables[0].Rows.Count > 0)
                        {

                            OHlitErrorinGrid.Text = "";
                        }
                        else
                        {

                            OHlitErrorinGrid.Text = "No Record Found";
                        }

                        try
                        {
                            string strAccntNo = ds.Tables[0].Rows[0]["Account Number"].ToString().Trim();

                            if (strAccntNo == SessionFacade.AccountNo.ToString().Trim())
                            {
                                SessionFacade.AccountNo = strAccntNo;
                                TempAccount = SessionFacade.LastAccount;
                                TempAccount[0] = SessionFacade.AccountNo.ToString().Trim();
                                SessionFacade.LastAccount = TempAccount;
                            }
                            else
                            {
                                SessionFacade.AccountNo = "";
                                TempAccount = SessionFacade.LastAccount;
                                TempAccount[0] = "";
                                SessionFacade.LastAccount = TempAccount;
                            }
                        }
                        catch
                        {
                            SessionFacade.AccountNo = "";
                            TempAccount = SessionFacade.LastAccount;
                            TempAccount[0] = "";
                            SessionFacade.LastAccount = TempAccount;
                        }



                    }


                    else
                    {
                        //   SessionFacade.AccountNo = "";
                        OHgridOrderHistory.DataSource = null;
                        OHgridOrderHistory.DataBind();
                        OHtxtbTotalOrders.Text = "0";
                        OHtxtbTatalSales.Text = "$0.00";
                        OHlitErrorinGrid.Text = "No Record Found";
                        TempAccount = SessionFacade.LastAccount;
                        TempAccount[0] = "";
                        SessionFacade.LastAccount = TempAccount;
                    }



                }
            }
            catch (Exception err)
            {
            }

        }

        #region write to XML File //Accepts DataSet

        private void WriteXmlToFile(DataSet thisDataSet)
        {
            string filename = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + Environment.UserName + "-OrderHistory" + ".xml";
            if (thisDataSet == null) { return; }
            // Create a file name to write to.

            // Create the FileStream to write with.
            System.IO.FileStream myFileStream = new System.IO.FileStream
               (filename, System.IO.FileMode.Create);
            // Create an XmlTextWriter with the fileStream.
            System.Xml.XmlTextWriter myXmlWriter =
               new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode);
            // Write to the file with the WriteXml method.
            thisDataSet.WriteXml(myXmlWriter);
            myXmlWriter.Close();
        }
        #endregion

        private void AssignTotalOrders(OrderSales c)
        {
            OHtxtbTotalOrders.Text = c.Orders;
            OHtxtbTatalSales.Text = c.Sales;
        }

        public DataTable GetDatafromXML()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();

            try
            {
                string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";
                DataRow[] results;
                DataTable dtTemp = new DataTable();
                string Query;

                Query = " 1=1";

                //Reading XML
                System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
                ds.ReadXml(fsReadXml);
                fsReadXml.Close();

                //To Copy the Schema.
                if (ds.Tables.Count > 0)
                {
                    dtTemp = ds.Tables[0].Clone();
                    if (Request.Cookies["CNo"] != null)
                    {
                        SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                        Query = Query + " and [Contact Number] = " + Request.Cookies["CNo"].Value;
                    }

                    //Search by Date
                    if (OHByDate.Checked == true)
                    {
                        if ((OHtxtStartDate.Text.ToString().Trim() != "") && (OHtxtEndDate.Text.ToString().Trim() != ""))
                        {
                            Query = Query + " and [Order Date] >= '" + OHtxtStartDate.Text + "' and [Order Date] <=  '" +
                            OHtxtEndDate.Text + "'";
                        }
                    }
                    if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                    {
                        if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                        {
                            Query = Query + " and [Order Number] = '" + SessionFacade.OrderLookUp + "'";
                        }

                    }

                    if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                    {
                        if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                        {
                            Query = Query + " and [Customer PO] = '" + SessionFacade.CustomerLookUp + "'";
                        }
                    }
                    else if (OHByCal.Checked == true)
                    {
                        if (OHrdoCalender.Checked == true)
                        {
                            Query = Query + " and [Order Date] >= '" + "1/1/" + OHddlBycal.Text +
                                "' and [Order Date] <= '" + "12/31/" + OHddlBycal.Text + "'";
                        }
                        else
                        {
                            Query = Query + " and ([Order Date] >= '" + "8/1/" + (int.Parse(OHddlBycal.Text) - 1).ToString() +
                                "' and [Order Date] <= '" + "7/31/" + OHddlBycal.Text + "')";
                        }
                    }

                    results = ds.Tables[0].Select(Query);

                    foreach (DataRow dr in results)
                        dtTemp.ImportRow(dr);
                }
                else
                    dtTemp = null;

                return dtTemp;
            }
            catch
            {
                return null;
            }
        }

        public DataSet OHGetDatafromXMLArrangeColumn()
        {
            DataSet ds = new DataSet();
            string Pathname = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            return ds;
        }

        protected DataTable GetDatafromXMLTotalOrdersAndSales()
        {
            //DataRow rowOrders;
            DataSet ds = new DataSet();

            string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar + SessionFacade.LoggedInUserName + "-OrderHistoryTotalOrdersSales" + ".xml";
            DataRow[] results;
            DataTable dtTemp = new DataTable();
            string Query;

            Query = " 1=1";

            //Reading XML
            System.IO.FileStream fsReadXml = new System.IO.FileStream(Pathname, System.IO.FileMode.Open);
            ds.ReadXml(fsReadXml);
            fsReadXml.Close();

            //To Copy the Schema.

            dtTemp = ds.Tables[0].Clone();
            // dtTemp.Columns["Order Date"].DataType = System.Type.GetType("System.Date");
            if (Request.Cookies["CNo"] != null)
            {
                SessionFacade.BuyerCt = Request.Cookies["CNo"].Value;
                Query = Query + " and buyerct = " + Request.Cookies["CNo"].Value;
            }

            //Search by Date
            if (OHByDate.Checked == true)
            {
                if ((OHtxtStartDate.Text.ToString().Trim() != "") && (OHtxtEndDate.Text.ToString().Trim() != ""))
                {
                    Query = Query + " and itemcreatedon >= '" + OHtxtStartDate.Text + "' and itemcreatedon <=  '" +
                    OHtxtEndDate.Text + "'";
                }
            }
            if (SessionFacade.OrderLookUp.ToString().Trim() != "")
            {
                if (SessionFacade.OrderLookUp.ToString().Trim() != "")
                {
                    Query = Query + " and DOC_NUMBER = '" + SessionFacade.OrderLookUp + "'";
                }

            }

            if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
            {
                if (SessionFacade.CustomerLookUp.ToString().Trim() != "")
                {
                    Query = Query + " and BSTKD = '" + SessionFacade.CustomerLookUp + "'";
                }
            }
            else if (OHByCal.Checked == true)
            {
                if (OHrdoCalender.Checked == true)
                {
                    Query = Query + " and itemcreatedon >= '" + "1/1/" + OHddlBycal.Text +
                        "' and itemcreatedon <= '" + "12/31/" + OHddlBycal.Text + "'";
                }
                else
                {
                    Query = Query + " and itemcreatedon >= '" + "8/1/" + (int.Parse(OHddlBycal.Text) - 1).ToString() +
                        "' and itemcreatedon <= '" + "7/31/" + OHddlBycal.Text + "'";
                }
            }

            results = ds.Tables[0].Select(Query);



            foreach (DataRow dr in results)
                dtTemp.ImportRow(dr);

            return dtTemp;

        }

        protected void StatusSearchDateRange(bool value)
        {
            OHimgstartCal.Enabled = value;
            OHimgEndCal.Enabled = value;
        }

        protected void StatusSearchYear(bool value)
        {
            OHrdoCalender.Enabled = value;
            OHrdoFiscalYear.Enabled = value;
            OHddlBycal.Enabled = value;
        }

        protected void InsertCalendarYearToDropDown()
        {
            int Year;
            OHddlBycal.Items.Clear();
            if (DateTime.Now.Month >= 8)
            {
                for (int index = 3; index >= -1; index--)
                {
                    Year = DateTime.Now.Year - index;
                    OHddlBycal.Items.Add(Year.ToString());
                }
            }
            else
            {
                for (int index = 4; index >= 0; index--)
                {
                    Year = DateTime.Now.Year - index;
                    OHddlBycal.Items.Add(Year.ToString());
                }
            }
            OHddlBycal.Text = DateTime.Now.Year.ToString();
        }

        protected DataSet OHArrangeColumn(DataSet ReArrangeDs)
        {
            string PathnameArrangeColumn = WebHelper.GetApplicationPath() + "App_Data" +
                Path.DirectorySeparatorChar + "XMLFiles\\" + SessionFacade.LoggedInUserName
                + "-OrderHistoryArrangeColumn" + ".xml";
            if (ReArrangeDs != null)
            {
                cArrangeDataSet ADS = new cArrangeDataSet();
                ADS.CampaignName = SessionFacade.CampaignValue;
                ADS.UserName = SessionFacade.LoggedInUserName;
                ADS.Listview = "lvwData";

                int IsReorder = ADS.ColumnReorderCount();
                if (IsReorder > 0)
                    ReArrangeDs = ADS.RearangeDS(ReArrangeDs);
            }

            if (ReArrangeDs.Tables[0].Columns.Contains("Order Date"))
                OHDateOrdinal = ReArrangeDs.Tables[0].Columns["Order Date"].Ordinal;
            else
                OHDateOrdinal = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Unit Price"))
                OHUnitPrice = ReArrangeDs.Tables[0].Columns["Unit Price"].Ordinal;
            else
                OHUnitPrice = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Ext Price"))
                OHExtPrice = ReArrangeDs.Tables[0].Columns["Ext Price"].Ordinal;
            else
                OHExtPrice = 1000;

            if (ReArrangeDs.Tables[0].Columns.Contains("Converted Date"))
                OHConvertedDate = ReArrangeDs.Tables[0].Columns["Converted Date"].Ordinal;
            else
                OHConvertedDate = 1000;

            //Writing XML
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(PathnameArrangeColumn);
            ReArrangeDs.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
            xmlSW.Close();

            return ReArrangeDs;
        }

        #endregion

        #region PageChanging
        protected void OHgridOrderHistoryPageChanging(object sender, GridViewPageEventArgs e)
        {
            OHgridOrderHistory.DataSource = OHGetDatafromXMLArrangeColumn();
            OHgridOrderHistory.PageIndex = e.NewPageIndex;
            OHgridOrderHistory.DataBind();
        }
        #endregion

        #region Sorting Order History

        protected SortDirection OHGridViewSortDirection
        {

            get
            {

                if (ViewState["sortDirection"] == null)

                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];

            }

            set { ViewState["sortDirection"] = value; }

        }

        protected void gridOrderHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (OHGridViewSortDirection == SortDirection.Ascending)
            {
                OHGridViewSortDirection = SortDirection.Descending;
                OHSortGridOrderHistory(sortExpression, "DESC");
            }
            else
            {
                OHGridViewSortDirection = SortDirection.Ascending;
                OHSortGridOrderHistory(sortExpression, "ASC");
            }
        }

        private void OHSortGridOrderHistory(string sortExpression, string direction)
        {
            try
            {
                if (!string.IsNullOrEmpty(SessionFacade.AccountNo))
                {
                    string Pathname = WebHelper.GetApplicationPath() + "App_Data" + Path.DirectorySeparatorChar +
                    "XMLFiles\\" + SessionFacade.LoggedInUserName + "-OrderHistory" + ".xml";

                    if (File.Exists(Pathname))
                    {

                        DataSet ReArrangeDs = new DataSet();
                        DataTable dtAllColumns = GetDatafromXML();
                        DataSet ds = new DataSet();
                        DataSet Sortedds = new DataSet();
                        DataView dv = new DataView(dtAllColumns);

                        dv.Sort = sortExpression + " " + direction;

                        ds.Tables.Add(dv.ToTable());

                        //WriteXmlToFile(ds);
                        System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(Pathname);
                        ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                        xmlSW.Close();
                        Sortedds = OHArrangeColumn(ds);

                        //Working
                        //Sortedds.Tables[0].Columns.Remove("uvals");

                        if (ds.Tables[0].Columns.Contains("uvals") == true)
                            ds.Tables[0].Columns.Remove("uvals");

                        OHgridOrderHistory.DataSource = Sortedds;
                        OHgridOrderHistory.DataBind();

                        if (direction == "DESC")
                        {
                            OHgridOrderHistory.HeaderRow.Cells[Sortedds.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortdesc-header";

                        }
                        else
                        {
                            OHgridOrderHistory.HeaderRow.Cells[Sortedds.Tables[0].Columns[sortExpression].Ordinal].CssClass = "sortasc-header";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Export to Excel Function
        protected void OHbtn_Export2ExcelClick(object sender, EventArgs e)
        {
            OHExportExcelFunction();
        }
        public void OHExportExcelFunction()
        {
            try
            {
                DataSet ds = new DataSet();
                string UserFileName = SessionFacade.LoggedInUserName + "OrderHistory" + ".xls";
                if (BindOrderHistory() != null)
                {
                    if (BindOrderHistory().Tables[0].Rows.Count > 0)
                    {
                        ds = BindOrderHistory();

                    }

                    ds.Tables[0].Columns.Remove("uvals");
                    ds.AcceptChanges();

                    if (File.Exists(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName)))
                        File.Delete(Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));

                    File.Copy(Server.MapPath("../../App_Data/Export2ExcelTemplate/Ordertemp.xls"), Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName));


                    string filename = Server.MapPath("../../App_Data/ExportExcelFiles/" + UserFileName);


                    bool exportToEx = Export2Excel.Export(ds.Tables[0], filename);

                    //true means Excel File has been written
                    if (exportToEx == true)
                    {
                        if (Request.Browser.Type == "Desktop") //For chrome
                        {
                            ////I am passing 2 varible to the page. Hard code the page name as quotes/orderhistory/ etc..It will be Excel file name

                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=OrderHistory&FilePath=" + UserFileName + "','_blank', 'resizable=yes, scrollbars=yes, titlebar=yes, width=400, height=50, top=10, left=10,status=1');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "updatepanel3", "winref = window.open('../Home/Export2ExcelPage.aspx?PageName=OrderHistory&FilePath=" + UserFileName + "','_parent', 'toolbar=0,statusbar=0,height=400,width=500');", true);
                        }
                    }
                    else
                    {
                        OHlitErrorinGrid.Text = "Un able to export the data. Please contact Administartor";
                        // Response.Write("Data not Exported to Excel File");
                    }
                }



            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Order History - Error in Export to Excel" + err.ToString());
            }
        }
        #endregion

        protected void btn_ArrangeColumns(object sender, EventArgs e)
        {
            OpenNewWindow("../Home/ArrangeColumns.aspx?Data=lvwData");
        }

        public void OpenNewWindow(string url)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
        }

        protected void btn_POLOOKUPClick(object sender, EventArgs e)
        {
            //Jonmar, Please read the Notes in orde rhistory page 
            POLookUpFunction();
        }

        public void POLookUpFunction()
        {
            OHbtnOk.Visible = true;
            OHbtnUpdate.Visible = false;
            OHOpenPopup();
        }

        private void OHOpenPopup()
        {
            OHPanel66.Visible = true;
            OHModalPopupExtender1.Show();
            upOuter.Update();
        }

        protected void OHbtnOk_Click1(object sender, EventArgs e)
        {
            try
            {
                string MyLookUpText = OHtxtCustomerLookUp.Text.ToString().Trim();
                if (MyLookUpText.Length > 0)
                {
                    MyLookUpText = MyLookUpText.Replace(",", "");
                }

                if (MyLookUpText.Length > 0)
                {
                    if (OHrdoOrderNumber.Checked == true && OHrdoCustomerLookUp.Checked == false)
                    {
                        SessionFacade.OrderLookUp = OHtxtCustomerLookUp.Text.ToString().Trim().PadLeft(10, '0');
                        SessionFacade.CustomerLookUp = "";
                    }
                    if (OHrdoOrderNumber.Checked == false && OHrdoCustomerLookUp.Checked == true)
                    {
                        SessionFacade.OrderLookUp = "";
                        SessionFacade.CustomerLookUp = OHtxtCustomerLookUp.Text.ToString();
                    }
                }
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Order History- Error in Customer PO LookUp" + err.ToString());
            }

            OHClosePopup();

            DataTable dt = GetDatafromXML();
            GetTotalOrdersSales(dt);

            //Working
            if (dt.Columns.Contains("uvals") == true)
                dt.Columns.Remove("uvals");

            OHgridOrderHistory.DataSource = dt;
            OHgridOrderHistory.DataBind();
            OHlitErrorinGrid.Text = "";

            upOuter.Update();
        }

        private void OHClosePopup()
        {
            OHPanel66.Visible = false;
            OHModalPopupExtender1.Hide();
            upOuter.Update();
        }

        #endregion


       
    }
}