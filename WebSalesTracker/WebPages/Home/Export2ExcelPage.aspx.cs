using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.ComponentModel;

namespace WebSalesMine.WebPages.Home
{
    public partial class Export2ExcelPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string downloadFilePath = Request.QueryString["FilePath"];
                string downloadFileName = Request.QueryString["PageName"];
                DownLoadMyFiles(downloadFilePath, downloadFileName);
            }
            catch
            {

            }
        }

        protected void DownLoadMyFiles(string filepath, string strPageName)
        {

           
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                filepath = filepath.Replace(c.ToString(), "");
            }


            string strFileName = strPageName + ".xls";
        
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=" + strFileName);
            Response.TransmitFile(Server.MapPath("../../App_Data/ExportExcelFiles/" + filepath));
          
            Response.Flush();
        }

       
        protected void DownLoadFiles(string filepath)
        {
            // The file name used to save the file to the client's system..

            string filename = Path.GetFileName(filepath);
            System.IO.Stream stream = null;
            try
            {
                // Open the file into a stream. 
                stream = new FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                // Total bytes to read: 
                long bytesToRead = stream.Length;



                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.MapPath("~/App_Data/ExportExcelFiles/Order.xls"));
                // Read the bytes from the stream in small portions. 
                while (bytesToRead > 0)
                {
                    // Make sure the client is still connected. 
                    if (Response.IsClientConnected)
                    {
                        // Read the data into the buffer and write into the 
                        // output stream. 
                        byte[] buffer = new Byte[10000];
                        int length = stream.Read(buffer, 0, 10000);
                        Response.OutputStream.Write(buffer, 0, length);
                        Response.Flush();

                        bytesToRead = bytesToRead - length;
                    }
                    else
                    {
                        // Get out of the loop, if user is not connected anymore.. 
                        bytesToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                // An error occurred.. 
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
    }
}