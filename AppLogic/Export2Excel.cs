using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace AppLogic
{
    public class Export2Excel
    {
        public static bool Export(DataTable tbl, string fileName, bool createTable, bool overwriteFile)
        {
            string tableName = tbl.TableName.Replace(',', '_');
            tableName = String.IsNullOrEmpty(tableName) ? "Sheet1" : "Sheet1";
            try
            {
                System.Globalization.CultureInfo _infoEn = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
                string sql = "";



                //if (overwriteFile)
                //    if (File.Exists(fileName))
                //        File.Delete(fileName);

                using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HDR=Yes'"))
                {
                    con.Open();
                    OleDbCommand cmdInsert;

                  
                   
                    foreach (DataRow row in tbl.Rows)
                    {
                       
                        string values = "(";
                        for (int i = 0; i < tbl.Columns.Count; i++)
                        {
                            if (i + 1 == tbl.Columns.Count)
                            {
                                if (tbl.Columns[i].DataType == System.Type.GetType("System.Decimal") ||
                                     tbl.Columns[i].DataType == System.Type.GetType("System.Int64") ||
                                     tbl.Columns[i].DataType == System.Type.GetType("System.Double"))
                                    values += String.IsNullOrEmpty(row[i].ToString()) ? "0)" : Convert.ToDecimal(row[i]).ToString("#0.00", _infoEn) + ")";
                                else
                                    values += "'" + System.Security.SecurityElement.Escape(row[i].ToString()) + "')";
                            }
                            else
                            {
                                if (tbl.Columns[i].DataType == System.Type.GetType("System.Decimal") ||
                                     tbl.Columns[i].DataType == System.Type.GetType("System.Int64") ||
                                     tbl.Columns[i].DataType == System.Type.GetType("System.Double"))
                                    values += String.IsNullOrEmpty(row[i].ToString()) ? "0," : Convert.ToDecimal(row[i]).ToString("#0.00", _infoEn) + ",";
                                else
                                // values += "'" + System.Security.SecurityElement.Escape(row[i].ToString()) + "',";
                                {
                                    string strValue = row[i].ToString();
                                    strValue = strValue.Replace(";", string.Empty);//Replace ;
                                    strValue = strValue.Replace("'", "''"); //repalce single Quot
                                    strValue = strValue.Replace("\"", "");
                                    strValue = strValue.Replace("\\\"", "\"");    //repalce Double Quot
                                    strValue = strValue.Replace(",", string.Empty); //repalce single Quot
                                    strValue = strValue.Replace("*", string.Empty);//Replace *;

                                    values += "'" + strValue + "',";
                                }
                            }
                        }


                        string sqlInsert = String.Format("Insert into [{0}$] VALUES {1}", tableName, values);
                        cmdInsert = new OleDbCommand(sqlInsert, con);

                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exce)
            {
                BradyCorp.Log.LoggerHelper.LogMessage("Error in Export to Excel" + exce.ToString());
                string test = exce.ToString();
                return false;
            }
            return true;
        }
        private static string GetColumnType(DataColumn dataColumn)
        {
            string t;
            if (dataColumn.DataType == System.Type.GetType("System.Decimal"))
                t = " decimal";
            else if (dataColumn.DataType == System.Type.GetType("System.Int64"))
                t = " INT";
            else if (dataColumn.DataType == System.Type.GetType("System.Double"))
                t = " double";
            else
                t = " VARCHAR(255)";
            return t;
        }
        public static bool Export(DataTable tbl, string fileName)
        {
            return Export(tbl, fileName, true, true);
        }
    }
}
