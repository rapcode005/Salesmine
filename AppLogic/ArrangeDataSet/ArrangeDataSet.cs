using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace AppLogic
{


    public class cArrangeDataSet
    {
        public string mstrCampaignName { get; set; }
        public string mstrUserName { get; set; }
        public string mstrlistview { get; set; }
        public string mstrUserColumnList { get; set; }
        public string mstrInsertColumnList { get; set; }
        public int mstrInsertPosition { get; set; }


        public string CampaignName
        {
            get { return mstrCampaignName; }
            set { mstrCampaignName = value; }
        }

        public string UserName
        {
            get { return mstrUserName; }
            set { mstrUserName = value; }
        }

        public string Listview
        {
            get { return mstrlistview; }
            set { mstrlistview = value; }
        }

        public string UserColumnList
        {
            get { return mstrUserColumnList; }
            set { mstrUserColumnList = value; }
        }

        public string InsertColumnList
        {
            get { return mstrInsertColumnList; }
            set { mstrInsertColumnList = value; }
        }

        public int InsertPosition
        {
            get { return mstrInsertPosition; }
            set { mstrInsertPosition = value; }
        }

        public int ColumnReorderCount()
        {
            cArrangeDataSetDB objADDB = new cArrangeDataSetDB();
            try
            {
                return objADDB.ColumnReorderCount(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet ReturnColumnOrder()
        {
            cArrangeDataSetDB objADDB = new cArrangeDataSetDB();
            try
            {
                return objADDB.GetColumnOrderList(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetColumnDetails()
        {
            cArrangeDataSetDB objADDB = new cArrangeDataSetDB();
            try
            {
                return objADDB.GetColumnDetails(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet GetColumnNotIncluded()
        {
            cArrangeDataSetDB objADDB = new cArrangeDataSetDB();
            try
            {
                return objADDB.GetColumnNotIncluded(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DataSet RearangeDS(DataSet dsCurrenData)
         {
            try
            {
                cArrangeDataSetDB objADDB = new cArrangeDataSetDB();
                DataSet dsColInfo = new DataSet();
                dsColInfo = objADDB.GetColumnOrderList(this);
                string gridviewName = string.Empty;
                DataSet ds = new DataSet();

                ds = dsCurrenData;
                
                if (ds.Tables[0] != null)
                {
                    DataTable dt = ds.Tables[0];

                    //Note : Sometime it might happens like ColumnReorder row name might have been differant from the DataSet Name. 
                    //Here ColumnReorder name is 'Shipped_To Name' and OrderHistory Dataset contaions column name as 'Ship Name'

                    if (dsColInfo.Tables.Count > 0 && dsColInfo.Tables[0].Rows.Count > 0)
                    {

                        if (dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwContInfo")
                        {
                            gridviewName = "lvwContInfo";

                            foreach (DataRow dr in dsColInfo.Tables[0].Rows)
                            {
                                string RowName = dr["ColumnName"].ToString().Trim();
                                if (RowName == "Lfetime Sales")
                                    dr["ColumnName"] = "Lifetime Sales";
                                if (RowName == "CreatedBy")
                                    dr["ColumnName"] = "Createdby";
                                if (RowName == "createdon")
                                    dr["ColumnName"] = "Createdon";
                                if (RowName == "supressdetails")
                                    dr["ColumnName"] = "SuppressDetails";
                                if (RowName == "Dapartment")
                                    dr["ColumnName"] = "Department";
                                if (RowName == "Last 12M Orders")
                                    dr["ColumnName"] = "last 12M Orders";
                                if (RowName == "Last Purchase Date")
                                    dr["ColumnName"] = "Last Purchased Date";
                            }
                        }
                        if (dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwData")
                        {
                            gridviewName = "lvwData";
                            foreach (DataRow dr in dsColInfo.Tables[0].Rows)
                            {
                                string RowName = dr["ColumnName"].ToString().Trim();
                                if (RowName == "Shipped_To Name")
                                {
                                    dr["ColumnName"] = "Ship Name";
                                }

                            }
                        }

                        if (dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwQuoteitem")
                        {

                        }

                        if (dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwPCSKUSummary")
                        {
                            gridviewName = "lvwPCSKUSummary";
                        }
                        if (dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwPCSKUSummaryT")
                        {
                            gridviewName = "lvwPCSKUSummaryT";
                        }

                        if (dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwNotesDataTer" || dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwDialerData")
                        {
                            DataRow anyRow = dsColInfo.Tables[0].NewRow();
                            anyRow["Username"] = dsColInfo.Tables[0].Rows[0]["Username"];
                            anyRow["listview"] = dsColInfo.Tables[0].Rows[0]["listview"];
                            anyRow["ColumnName"] = "Row";
                            anyRow["ColumnIndex"] = dsColInfo.Tables[0].Rows[0]["ColumnIndex"];
                            anyRow["Position"] = dsColInfo.Tables[0].Rows.Count + 1;
                            dsColInfo.Tables[0].Rows.Add(anyRow);
                        }
                        if (dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwNotesData")
                        {
                            DataRow anyRow = dsColInfo.Tables[0].NewRow();
                            anyRow["Username"] = dsColInfo.Tables[0].Rows[0]["Username"];
                            anyRow["listview"] = dsColInfo.Tables[0].Rows[0]["listview"];                           
                            anyRow["ColumnIndex"] = dsColInfo.Tables[0].Rows[0]["ColumnIndex"];
                            anyRow["Position"] = dsColInfo.Tables[0].Rows.Count + 1;
                            dsColInfo.Tables[0].Rows.Add(anyRow);
                        }

                        dsColInfo.AcceptChanges();

                        while (ds.Tables.Count > 0)
                        {
                            DataTable table = ds.Tables[0];
                            if (ds.Tables.CanRemove(table))
                            {
                                ds.Tables.Remove(table);
                            }
                        }

                        if (dsColInfo.Tables.Count > 0 && dsColInfo.Tables[0].Rows.Count > 0)
                        {
                            dt = SelectDataTableColumns(dt, dsColInfo.Tables[0]);

                            ds.Tables.Add(dt);
                        }
                        //if (dsColInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Trim() == "lvwSKUSummary")
                        //{
                        //    ds.Tables[0].Columns.Remove("PRODUCT CATEGORY");
                        //}

                        int RowPosition = 0;

                        // ds.Tables[0].Columns["Ship State"].SetOrdinal(1);
                        foreach (DataRow dr in dsColInfo.Tables[0].Rows)
                        {
                            string RowName = dr["ColumnName"].ToString().Trim();

                            if (dr["ColumnName"].ToString().Trim() != "" && dr["ColumnName"].ToString().Trim() != null)
                            {
                                if (SessionFacade.CampaignValue != "PC")
                                    RowPosition = Int32.Parse(dr["Position"].ToString().Trim());
                                else
                                {
                                    //if (gridviewName == "lvwPCSKUSummary" || gridviewName == "lvwPCSKUSummaryT")
                                    //{
                                    RowPosition = Int32.Parse(dr["Position"].ToString().Trim());
                                    //}
                                    //else
                                    //{
                                    //    if (Int32.Parse(dr["Position"].ToString().Trim()) >= 8)
                                    //    {
                                    //        RowPosition = Int32.Parse(dr["Position"].ToString().Trim()) - 5;
                                    //    }
                                    //    else
                                    //        RowPosition = Int32.Parse(dr["Position"].ToString().Trim());
                                    //}
                                }

                                if (ds.Tables[0].Columns.Count >= RowPosition)
                                    ds.Tables[0].Columns[RowName].SetOrdinal(RowPosition - 1);
                            }

                        }

                        ds.AcceptChanges();
                    }

                    return ds;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in ColumnOrderList Retrival");
                return null;
                throw new Exception(ex.Message, ex);

            }

        }


        //private DataSet CopyColumnRows(DataSet dsTemp,DataSet dsCurrentData, string ColumnName)
        //{
        //    dsTemp.Tables[0].Columns.Add(ColumnName);
        //    foreach (DataRow dr in dsCurrentData.Tables[0].Rows)
        //    {
        //        foreach (DataRow dr2 in dsTemp.Tables[0].Rows)
        //        {
        //            if (dr["Contact Number"].ToString() == dr2["Contact Number"].ToString()
        //                && dr["First Name"].ToString() == dr2["First Name"].ToString()
        //                && dr["Last Name"].ToString() == dr2["Last Name"].ToString())
        //            {
        //                if(dr[ColumnName].ToString() != "")
        //                    dr2[ColumnName] = dr[ColumnName].ToString(); 
        //                break;
        //            }
        //        }
        //    }

        //    return dsTemp;
        //}


        public static DataTable SelectDataTableColumns(DataTable dataTable, DataTable ColInfo)
        {
            DataTable cloneDataTable = dataTable.Copy();

            List<string> ColInfoColumnList = new List<string>();
            foreach (DataRow dr in ColInfo.Rows)
            {
                ColInfoColumnList.Add(dr["ColumnName"].ToString().Trim());
            }

            //foreach (DataRow dr in ColInfo.Rows)
            //{
            //    if (!cloneDataTable.Columns.Contains(dr["ColumnName"].ToString().Trim()))
            //        cloneDataTable.Columns.Remove(dr["ColumnName"].ToString().Trim());
            //}
            List<string> RemoveColumnList = new List<string>();

            foreach (DataColumn dc in cloneDataTable.Columns)
            {

                if (ColInfoColumnList.Contains(dc.ColumnName.ToString().Trim()))
                {
                   
                }
                else
                {
                    RemoveColumnList.Add(dc.ColumnName.ToString().Trim());
                }
                //cloneDataTable.Columns.Remove(dc.ColumnName.ToString().Trim());
            }
            string s = "s";
            foreach (string colstr in RemoveColumnList)
            {

                cloneDataTable.Columns.Remove(colstr);
            }
             cloneDataTable.AcceptChanges();


            return cloneDataTable;

        }

        public int InsertColumns()
        {
            cArrangeDataSetDB objADDB = new cArrangeDataSetDB();
            try
            {
                return objADDB.InsertColumns(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int DeleteColumns()
        {
            cArrangeDataSetDB objADDB = new cArrangeDataSetDB();
            try
            {
                return objADDB.DeleteColumns(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}