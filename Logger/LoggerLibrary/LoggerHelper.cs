#region Using Statements
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Net;


#endregion // Using Statements

namespace BradyCorp.Log
{
    /// <summary>
    /// This class supports logging exceptions and messages via
    /// Enterprise Library Application Logging block.
    /// </summary>
    public static class LoggerHelper
    {
        #region Fields
        private static bool doLogAllInputParameters;
        #endregion // Fields

        #region Properties
        /// <summary>
        /// Gets a value which determines if all input parameters
        /// to web pages and HTTP handlers should be logged.
        /// </summary>
        public static bool DoLogAllInputParameters
        {
            get { return doLogAllInputParameters; }
            private set { doLogAllInputParameters = value; }
        }
        #endregion // Properties

        #region Constructors
        // Static constructor
        static LoggerHelper()
        {
            GetDebugSettingsFromWebConfig();
        }
        #endregion // Constructors

        #region Public Methods
        /// <summary>
        /// Logs an exception message to the configured logging destination.
        /// </summary>
        /// <param name="ex">The exception to be logged.</param>
        public static void LogException(Exception ex)
        {
            LogException(ex, null, "", "", "", "");
        }

        /// <summary>
        /// Logs an exeption message, along with an additional string,
        /// to the configured logging destination.
        /// </summary>
        /// <param name="ex">The exception to be logged.</param>
        /// <param name="msg">An additional message to be logged.</param>
        public static void LogException(Exception ex, string msg, string campign, string userlogin, string accntnum, string kamid)
        {

            /*      StringBuilder sb = new StringBuilder();
                  string indent = String.Empty;
                  Exception exArgument = ex;

                  // Loop through the exception and any inner exceptions.
                  while (exArgument != null)
                  {
                      sb.AppendFormat("{0}Exception type: {1} -- Exception message: {2}{3}", indent, ex.GetType(), exArgument.StackTrace, Environment.NewLine);

                      exArgument = exArgument.InnerException;
                      indent += "\t";
                  }

                  sb.AppendFormat("Additional Info: {0}", msg);

                  LogEntry logEntry = new LogEntry();
                  logEntry.Categories.Add("General");
                  logEntry.Message = sb.ToString();
                  logEntry.Severity = TraceEventType.Error;
                  Logger.Write(logEntry);*/


            LogEntry logEntry = new LogEntry();
            StringBuilder sb = new StringBuilder();
            string indent = String.Empty;
            Exception exArgument = ex;

            sb.AppendLine("Message Source	: " + msg.ToString().Trim());
            sb.AppendLine("Source		    : " + ex.Source.ToString().Trim());
            sb.AppendLine("Date		        : " + DateTime.Now.ToLongTimeString());
            sb.AppendLine("Time		        : " + DateTime.Now.ToShortDateString());
            sb.AppendLine("Computer		    : " + Dns.GetHostName().ToString());
            sb.AppendLine("Error		    : " + ex.Message.ToString().Trim());
            sb.AppendLine("Campaign		    : " + campign.ToString().Trim());
            sb.AppendLine("User Login		: " + userlogin.ToString().Trim());
            sb.AppendLine("Account Num		: " + accntnum.ToString().Trim());
            sb.AppendLine("KAMID                :" + kamid.ToString().Trim());
            sb.AppendLine("Stack Trace		: " + ex.StackTrace.ToString().Trim());
            sb.AppendLine("^^-------------------------------------------------------------------^^");
            logEntry.Message = sb.ToString();
            logEntry.Severity = TraceEventType.Error;
            Logger.Write(logEntry);
        }


        public static void LogExceptionOrderHistory(Exception ex, string msg, string campign, string userlogin,
            string accntnum, string kamid, string sdate, string edate, string Ornum, int Year,
            string PNo, int yrtype, string ContactNum)
        {

            LogEntry logEntry = new LogEntry();
            StringBuilder sb = new StringBuilder();
            string indent = String.Empty;
            Exception exArgument = ex;

            //Check if Null
            if (Ornum == null)
                Ornum = "";
            if (Year == null)
                Year = 0;
            if (PNo == null)
                PNo = "";
            if (yrtype == null)
                yrtype = 0;
            if (ContactNum == null)
                ContactNum = "";

            sb.AppendLine("Message Source	: " + msg.ToString().Trim());
            sb.AppendLine("Source		    : " + ex.Source.ToString().Trim());
            sb.AppendLine("Date		        : " + DateTime.Now.ToLongTimeString());
            sb.AppendLine("Time		        : " + DateTime.Now.ToShortDateString());
            sb.AppendLine("Computer		    : " + Dns.GetHostName().ToString());
            sb.AppendLine("Error		    : " + ex.Message.ToString().Trim());
            sb.AppendLine("Campaign		    : " + campign.ToString().Trim());
            sb.AppendLine("User Login		: " + userlogin.ToString().Trim());
            sb.AppendLine("Account Num		: " + accntnum.ToString().Trim());
            sb.AppendLine("KAMID                  :" + kamid.ToString().Trim());
            sb.AppendLine("Start Date           :" + sdate.ToString().Trim());
            sb.AppendLine("End Date             :" + edate.ToString().Trim());
            sb.AppendLine("Order Number          :" + Ornum.ToString().Trim());
            sb.AppendLine("Year                :" + Year.ToString().Trim());
            sb.AppendLine("PO Number                :" + PNo.ToString().Trim());
            sb.AppendLine("YrType                :" + yrtype.ToString().Trim());
            sb.AppendLine("Contact Number                :" + ContactNum.ToString().Trim());
            sb.AppendLine("Stack Trace		   : " + ex.StackTrace.ToString().Trim());
            sb.AppendLine("^^-------------------------------------------------------------------^^");
            logEntry.Message = sb.ToString();
            logEntry.Severity = TraceEventType.Error;
            Logger.Write(logEntry);
        }

        /// <summary>
        /// Logs a message to the configured logging destination as
        /// an information message type.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public static void LogMessage(string message)
        {
            LogMessage(message, null, LogSeverity.Information);
        }

        /// <summary>
        /// Logs a message to the configured logging destination as
        /// an information message type.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="className">The name of the class.</param>
        public static void LogMessage(string message, string className)
        {
            LogMessage(message, className, LogSeverity.Information);
        }




        /// <summary>
        /// Logs a message to the configured logging destination
        /// using the specified message type.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="className">The name of the class.</param>
        /// <param name="eventType">The type of the message to log.</param>
        public static void LogMessage(string message, string className, LogSeverity eventType)
        {
            // Create LogEntry
            LogEntry logEntry = new LogEntry();
            logEntry.Categories.Add("General");
            StringBuilder logMessage = new StringBuilder();
            if (className != null)
            {
                logMessage.Append(className).Append(" : ").Append(message);

            }
            else
            {
                logMessage.Append(message);
            }

            logEntry.Message = logMessage.ToString();
            logEntry.Severity = (TraceEventType)Enum.ToObject(typeof(TraceEventType), (int)eventType);

            Logger.Write(logEntry);

        }
        /*
		/// <summary>
		/// Formats a key/value collection for logging.
		/// </summary>
		/// <param name="prefix">The string to show in front of the name/value pairs.</param>
		/// <param name="collection">The name/value pairs to be formatted for logging.</param>
		/// <returns>The key/value collection formatted for logging.</returns>
        public static string GetNameValueCollectionStringForLog(string prefix, NameValueCollection collection)
        {
            return GetNameValueCollectionStringForLog(prefix, collection, false);
        }

		/// <summary>
		/// Formats a key/value collection for logging.
		/// </summary>
		/// <param name="prefix">The string to show in front of the name/value pairs.</param>
		/// <param name="collection">The name/value pairs to be formatted for logging.</param>
		/// <param name="urlDecodeCollection">Pass True if the values in the collection
		/// should be URL decoded, else pass False.</param>
		/// <returns>The key/value collection formatted for logging.</returns>
        public static string GetNameValueCollectionStringForLog(string prefix, NameValueCollection collection, bool urlDecodeValues)
        {
            StringBuilder sb = new StringBuilder(prefix + "\n");

            if (collection.Count == 0)
            {
                sb.AppendFormat("\t<collection is empty>\n");
            }
            else
            {
                // Loop through all of the name/value pairs and write them out.
                for (int i = 0; i < collection.Count; i++)
                {
                    sb.AppendFormat("\t{0}: {1}\n", collection.Keys[i], (urlDecodeValues ? HttpUtility.UrlDecode(collection[i]) : collection[i]));
                }
            }

            return sb.ToString();
        }*/
        #endregion // Public Methods

        #region Private Methods
        private static void GetDebugSettingsFromWebConfig()
        {
            try
            {
                Configuration webConfiguration = WebConfigurationManager.OpenWebConfiguration("~/");
                KeyValueConfigurationElement configValue = webConfiguration.AppSettings.Settings["LogAllInputParameters"];

                if (configValue != null)
                    DoLogAllInputParameters = bool.Parse(configValue.Value);
            }
            catch (Exception ex)
            {
                LogException(ex, "Exception occurred in LoggerHelper.GetDebugSettingsFromWebConfig()", "", "", "", "");
            }
        }
        #endregion // Private Methods


    }
}