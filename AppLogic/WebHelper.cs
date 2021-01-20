
#region Imports Section

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

#endregion


namespace AppLogic
{
    public class WebHelper
    {
        //This function returns the index of column of Grid. Its helpful when you want to access a column by its name and not by index
        //It accepts Gridview and Column name and returns the index of that column

        public static int GetColumnIndexOf(GridView gv, string ColumnName)
        {
            try
            {
                int columnID = 0;
                // Loop all the columns
                var DataControlFieldsList = gv.Columns.OfType<DataControlField>().ToList().
                                            Where(t => t.HeaderText.ToUpper().Trim() == ColumnName.ToUpper().Trim() ||
                                            t.AccessibleHeaderText.ToUpper().Trim() == ColumnName.ToUpper().Trim()).ToList();
                if (DataControlFieldsList != null)
                {
                    if (DataControlFieldsList.Count > 0)
                    {
                        columnID = gv.Columns.IndexOf(DataControlFieldsList.First());
                    }
                }
                return columnID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Version ()
        {
            return ConfigurationManager.AppSettings["Version"].ToString();
        }

        public string CopyRight()
        {
            return ConfigurationManager.AppSettings["Copyright"].ToString();
        }

        public static string GetApplicationPath()
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            string physicalPath = HttpContext.Current.Request.MapPath(appPath);
            return physicalPath;
        }

        public static void CreateFolder(string folderPath, string folderName)
        {
            folderPath = folderPath + Path.DirectorySeparatorChar + folderName;

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }



        public static string GetSourceExcelFilePath()
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            string folderPath = HttpContext.Current.Request.MapPath(appPath + Path.DirectorySeparatorChar + "App_Data" + Path.DirectorySeparatorChar);
            if (!Directory.Exists(folderPath + "Export2ExcelTemplate"))
            {
                Directory.CreateDirectory(folderPath + "Export2ExcelTemplate");
            }
            return folderPath + "Export2ExcelTemplate" + Path.DirectorySeparatorChar;

        }

        public static string GetExportExcelFilesPath()
        {
            string appPath = HttpContext.Current.Request.ApplicationPath;
            string folderPath = HttpContext.Current.Request.MapPath(appPath + Path.DirectorySeparatorChar + "App_Data" + Path.DirectorySeparatorChar);
            if (!Directory.Exists(folderPath + "ExportExcelFiles"))
            {
                Directory.CreateDirectory(folderPath + "ExportExcelFiles");
            }
            return folderPath + "ExportExcelFiles" + Path.DirectorySeparatorChar;

        }
  
    }
}