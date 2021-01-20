using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Configuration;
using System.Data;
using System.Threading;

namespace WebSalesMine.WebPages
{
    public class SqlHelper
    {
        #region Public Methods
        /// <summary>
        /// It defines calling parameter and reads the data
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandType"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader SqlDataReaderRequest(
            SqlConnection connection,
            CommandType commandType,
            string storedProcedureName,
            SqlParameter[] parameters = null)
        {
            using (SqlCommand command = CreateSqlCommand(connection, commandType, storedProcedureName, parameters))
            {
                var iAsyncResult = command.BeginExecuteReader();
                WaitHandle wh = iAsyncResult.AsyncWaitHandle;
                wh.WaitOne();
                SqlDataReader sqlDataReader = command.EndExecuteReader(iAsyncResult);
                return sqlDataReader;
            }
        }

        /// <summary>
        /// it return single values
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="commandType"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static int SqlDataExcuteScalar(
            SqlConnection sqlConnection,
            CommandType commandType,
            string storedProcedureName,
            SqlParameter[] sqlParameters = null)
        {
            using (var command = CreateSqlCommand(sqlConnection, commandType, storedProcedureName, sqlParameters))
            {
                var result = (int)command.ExecuteScalar();
                return result;
            }
        }



        /// <summary>
        /// it insert values with parameter
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="commandType"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="sqlParameters"></param>
        public static void SqlDataExcuteNonQuery(
            SqlConnection sqlConnection,
            CommandType commandType,
            string storedProcedureName,
            SqlParameter[] sqlParameters = null)
        {
            using (var command = CreateSqlCommand(sqlConnection, commandType, storedProcedureName, sqlParameters))
            {
                var iAsyncResult = command.BeginExecuteNonQuery();
                WaitHandle waitHandle = iAsyncResult.AsyncWaitHandle;
                waitHandle.WaitOne();
                command.EndExecuteNonQuery(iAsyncResult);
            }
        }

        /// <summary>
        /// it retuns list of value
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="commandType"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet SqlDataExcuteDataSet(
            SqlConnection sqlConnection,
            CommandType commandType,
            string storedProcedureName,
            SqlParameter[] parameters = null)
        {
            using (var command = CreateSqlCommand(sqlConnection, commandType, storedProcedureName, parameters))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                return dataSet;
            }
        }
        #endregion // End Public Methods

        #region Private Methods
        /// <summary>
        /// Encasuplates logic for creating a sql command
        /// </summary>
        /// <param name="sqlConnection">sql connection</param>
        /// <param name="commandType">command type</param>
        /// <param name="sqlQueryOrStoredProcedureName">stored procedure or sql query</param>
        /// <param name="sqlParameters">sql parameters</param>
        /// <returns>sql command</returns>
        private static SqlCommand CreateSqlCommand(
            SqlConnection sqlConnection,
            CommandType commandType,
            string sqlQueryOrStoredProcedureName,
            IEnumerable<SqlParameter> sqlParameters = null)
        {
            SqlCommand command = new SqlCommand(sqlQueryOrStoredProcedureName, sqlConnection);
            command.CommandType = commandType;

            if (sqlParameters != null)
            {
                command.Parameters.AddRange(sqlParameters.ToArray());
            }

            return command;
        }
        #endregion // End Private Methods
    }
}