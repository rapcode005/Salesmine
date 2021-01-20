using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;


namespace DataAccess
{
    public class DBHelper
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SASManilaOnlineCONNECTON"].ConnectionString;
        private static string DialerconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DialerDBConnection"].ConnectionString;
        private static string GoldMineconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["GoldMineDialerDBConnection"].ConnectionString;
        private static SqlConnection sqlConn;
        /// <summary> 
        /// This returns an open connection for VitalAxis.
        /// By Bineesh Raghavan
        /// </summary>
        /// <returns>SqlConnection</returns>
        public static SqlConnection GetOpenConnection()
        {
            try
            {
                sqlConn = new SqlConnection(connectionString);
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
            }
            catch (Exception)
            {
                if (sqlConn.State != ConnectionState.Closed)
                {
                    sqlConn.Close();
                }
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

            }
            return sqlConn;
        }


        public static SqlConnection GetOpenConnectionAIOP()
        {
            try
            {
                sqlConn = new SqlConnection(DialerconnectionString);
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();
            }
            catch (Exception)
            {
                if (sqlConn.State != ConnectionState.Closed)
                {
                    sqlConn.Close();
                }
                sqlConn = new SqlConnection(DialerconnectionString);
                sqlConn.Open();
            }
            return sqlConn;
        }

        public static SqlConnection GetGoldMineOpenConnection()
        {
            try
            {
                sqlConn = new SqlConnection(GoldMineconnectionString);
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();
            }
            catch (Exception)
            {
                if (sqlConn.State != ConnectionState.Closed)
                {
                    sqlConn.Close();
                }
                sqlConn = new SqlConnection(GoldMineconnectionString);
                sqlConn.Open();
            }
            return sqlConn;
        }

        public static Object ExecuteScalar(string sqlText, SqlConnection sqlConn)
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlText;
            comm.CommandType = CommandType.Text;
            comm.Connection = sqlConn;
            DBHelper.AssignTimeOut(comm);
            Object r = comm.ExecuteScalar();
            //comm.Connection = null;
            comm.Dispose();
            comm = null;
            return r;
        }
        public static Object ExecuteScalar(string sqlText)
        {
            Object r = null;
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                try
                {
                    r = ExecuteScalar(sqlText, sqlConn);
                }
                catch { }
                finally
                {
                    if (null != sqlConn)
                        sqlConn.Close();
                }
            }
            return r;
        }
        public static DataTable ExecuteQueryToTable(string sqlText, SqlConnection sqlConn)
        {
            DataTable dt = new DataTable();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlText;
            comm.CommandType = CommandType.Text;
            comm.Connection = sqlConn;
            DBHelper.AssignTimeOut(comm);
            SqlDataAdapter sda = new SqlDataAdapter(comm);
            sda.Fill(dt);
            comm.Connection = null;
            sda.Dispose();
            return dt;
        }
        public static DataSet ExecuteQueryToDataSet(string sqlText, SqlConnection sqlConn)
        {
            DataSet dset = new DataSet();
            SqlCommand comm = new SqlCommand(sqlText, sqlConn);
            comm.CommandType = CommandType.Text;
            DBHelper.AssignTimeOut(comm);
            SqlDataAdapter sda = new SqlDataAdapter(comm);
            sda.Fill(dset);
            comm.Connection = null;
            sda.Dispose();
            return dset;
        }

        public static DataSet ExecuteQueryToDataSet(string sqlText)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dset = new DataSet();
                SqlCommand comm = new SqlCommand(sqlText, connection);
                comm.CommandType = CommandType.Text;
                DBHelper.AssignTimeOut(comm);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dset);
                comm.Connection = null;
                sda.Dispose();
                return dset;
            }
        }


        public static DataSet ExecuteQueryToDataSetAIOP(string sqlText)
        {
            using (SqlConnection connection = new SqlConnection(DialerconnectionString))
            {
                DataSet dset = new DataSet();
                SqlCommand comm = new SqlCommand(sqlText, connection);
                comm.CommandType = CommandType.Text;
                DBHelper.AssignTimeOut(comm);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dset);
                comm.Connection = null;
                sda.Dispose();
                return dset;
            }
        }

        public static DataSet ExecuteSPWithoutParameter_old(string storedProcedureName)
        {
            DataSet dset = new DataSet();

            using (SqlConnection connection = GetOpenConnection())
            {

                SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                comm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dset);
                sda.Dispose();
            }
            return dset;
        }

        public static DataSet ExecuteSPWithoutParameter(string storedProcedureName)
        {
            DataSet dset = new DataSet();
            // sqlConn = new SqlConnection(connectionString);
            // if (sqlConn.State == ConnectionState.Closed)
            //        sqlConn.Open();
            //}
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                    comm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter sda = new SqlDataAdapter(comm);
                    sda.Fill(dset);
                    sda.Dispose();

                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return dset;
            }

            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(err.ToString());
                throw new Exception(err.Message);
            }

        }

        /// <summary>
        /// This method is used to execute SQL stored procedre with any number of input parameters and returns DataSet
        /// by Bineesh Raghavan
        /// </summary>
        /// <param name="StoredProcedureName">Name of the stored procedure</param>
        /// <param name="parameterList">SqlParameter collection </param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteQueryToDataSet(string storedProcedureName, SqlParameter[] parameterList)
        {
            try
            {
                DataSet dset = new DataSet();

                //SqlConnection connection = new SqlConnection(connectionString);

                //using (SqlConnection connection = GetOpenConnection())
                //{
                    
                //    SqlCommand comm = new SqlCommand(storedProcedureName, connection);

                //    comm.CommandType = CommandType.StoredProcedure;

                //    foreach (SqlParameter prm in parameterList)
                //    {

                //        if ((prm.Value == null) || (prm.Value.ToString() == ""))
                //        {
                //            prm.SqlValue = DBNull.Value;
                //        }

                //        comm.Parameters.Add(prm);
                //    }
                //    SqlDataAdapter sda = new SqlDataAdapter(comm);
                //    sda.Fill(dset);
                //    sda.Dispose();

                //    if (connection.State == ConnectionState.Open)
                //        connection.Close();
                //}

                //return dset;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand comm = new SqlCommand(storedProcedureName, connection);

                    comm.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter prm in parameterList)
                    {

                        if ((prm.Value == null) || (prm.Value.ToString() == ""))
                        {
                            prm.SqlValue = DBNull.Value;
                        }

                        comm.Parameters.Add(prm);
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(comm);
                    sda.Fill(dset);
                    sda.Dispose();

                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

                return dset;
            }
            catch (Exception err)
            {
                BradyCorp.Log.LoggerHelper.LogMessage(err.ToString());
                throw new Exception(err.Message);
            }
        }

        public static DataSet ExecuteQueryToDataSetAIOP(string storedProcedureName, SqlParameter[] parameterList)
        {
            DataSet dset = new DataSet();
            using (SqlConnection connection = GetOpenConnectionAIOP())
            {

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                comm.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter prm in parameterList)
                {

                    if ((prm.Value == null) || (prm.Value.ToString() == ""))
                    {
                        prm.SqlValue = DBNull.Value;
                    }

                    comm.Parameters.Add(prm);
                }
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dset);
                sda.Dispose();


                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
            return dset;
        }


        public static DataSet ExecuteGoldmineQueryToDataSet(string storedProcedureName, SqlParameter[] parameterList)
        {
            DataSet dset = new DataSet();
            using (SqlConnection connection = GetGoldMineOpenConnection())
            {
                SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                comm.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter prm in parameterList)
                {

                    if ((prm.Value == null) || (prm.Value.ToString() == ""))
                    {
                        prm.SqlValue = DBNull.Value;
                    }

                    comm.Parameters.Add(prm);
                }
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dset);
                sda.Dispose();
            }
            return dset;
        }
        /// <summary>
        /// This method is used to execute SQL stored procedre with any number of input parameters and returns DataTable
        /// Bineesh Raghavan
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterList"></param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteQueryToDataTable(string storedProcedureName, SqlParameter[] parameterList)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = GetOpenConnection())
            {
                SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter prm in parameterList)
                {
                    comm.Parameters.Add(prm);
                }
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dt);
                sda.Dispose();
            }
            return dt;
        }
        /// <summary>
        /// Method accepts stored procedure name and parameterList and return the value
        /// By Bineesh Raghavan
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterList"></param>
        /// <returns>Object</returns>
        public static Object ExecuteScalar(string storedProcedureName, SqlParameter[] parameterList)
        {
            Object retValue = null;
            using (SqlConnection connection = GetOpenConnection())
            {
                SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                comm.CommandType = CommandType.StoredProcedure;
                AssignTimeOut(comm);
                foreach (SqlParameter prm in parameterList)
                {
                    comm.Parameters.Add(prm);
                }
                retValue = comm.ExecuteScalar();
            }
            return retValue;
        }
        public static DataTable ExecuteQueryToTable(string sqlText)
        {
            DataTable r = null;
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                try
                {
                    r = ExecuteQueryToTable(sqlText, sqlConn);
                }
                catch { }
                finally
                {
                    if (null != sqlConn)//varaible name was changed to conn, so again changed to sqlConn
                        sqlConn.Close();
                }
            }
            return r;
        }
        //revision_1
        public static void ExecuteNonQuery(string insertSql)
        {
            string strSql = insertSql;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    cmd.CommandText = strSql;
                    DBHelper.AssignTimeOut(cmd);
                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    if (null != conn)
                        conn.Close();
                }
            }
        }

        public static int ExecuteNonQueryWithoutParams(string SelectSQL)
        {
            int _rowsAffected = -1;
            string strSql = SelectSQL;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    cmd.CommandText = strSql;
                    DBHelper.AssignTimeOut(cmd);
                    _rowsAffected = (Int32)cmd.ExecuteScalar();
                }
                catch { }
                finally
                {
                    if (null != conn)
                        conn.Close();
                }
            }
            return _rowsAffected;
        }
        /// <summary>
        /// To insert data into database table with storedProcedureName and SqlParameter array
        /// Bineesh Raghavan on 20-Jan-09
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterList"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery_old(string storedProcedureName, SqlParameter[] parameterList)
        {
            int _rowsAffected = -1;
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand comm = new SqlCommand(storedProcedureName, conn);
                AssignTimeOut(comm);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter prm in parameterList)
                {
                    comm.Parameters.Add(prm);
                }
                _rowsAffected = comm.ExecuteNonQuery();
            }
            return _rowsAffected;
        }

        public static int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameterList)
        {
            int _rowsAffected = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand comm = new SqlCommand(storedProcedureName, connection);
                AssignTimeOut(comm);
                comm.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter prm in parameterList)
                {
                    comm.Parameters.Add(prm);
                }
                _rowsAffected = comm.ExecuteNonQuery();
            }
            return _rowsAffected;
        }



        public static int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameterList, SqlConnection conn)
        {
            int _rowsAffected = -1;
            SqlCommand comm = new SqlCommand(storedProcedureName, conn);
            AssignTimeOut(comm);
            comm.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter prm in parameterList)
            {
                comm.Parameters.Add(prm);
            }
            _rowsAffected = comm.ExecuteNonQuery();
            return _rowsAffected;
        }
        public static void ExecuteNonQuery(string insertSql, SqlConnection connection)
        {
            string strSql = insertSql;
            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand cmd = new SqlCommand(strSql, connection);
                cmd.CommandText = strSql;
                DBHelper.AssignTimeOut(cmd);
                cmd.ExecuteNonQuery();
                scope.Complete();
            }
        }
        public static void ExecuteNonQuery(string insertSql, ref SqlConnection oConnection, ref SqlTransaction oTransaction)
        {
            try
            {
                using (oConnection)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = insertSql;
                    DBHelper.AssignTimeOut(cmd);
                    cmd.Connection = oConnection;
                    cmd.Transaction = oTransaction;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //revision_2
        /// <summary>
        /// This method will be used to get output parameter values in hash table collection
        /// </summary>
        /// <param name="StoredProcedureName">Name of the stored procedure</param>
        /// <param name="ParameterValues">Parameter collection </param>
        /// <returns></returns>
        public Hashtable ExecuteStoredProcedureGetOutputParameters(string StoredProcedureName, params object[] ParameterValues)
        {
            Hashtable htOutputParameters = null;
            SqlConnection Connection = null;
            if (connectionString != string.Empty && connectionString != null)
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Create Command
                        SqlCommand Command = Connection.CreateCommand();
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandText = StoredProcedureName;
                        // Open Connection
                        Connection.Open();
                        // Discover Parameters for Stored Procedure
                        // Populate command.Parameters Collection.
                        // Causes Round trip to Database.
                        SqlCommandBuilder.DeriveParameters(Command);
                        // Initialize Index of parameterValues Array
                        int index = 0;
                        // Populate the Input Parameters With Values Provided  
                        foreach (SqlParameter Parameter in Command.Parameters)
                        {
                            if (Parameter.Direction == ParameterDirection.Input || Parameter.Direction == ParameterDirection.InputOutput)
                            {
                                Parameter.Value = ParameterValues[index];
                                index++;
                            }
                        }
                        Command.ExecuteReader(CommandBehavior.CloseConnection);
                        htOutputParameters = new Hashtable();
                        // Populate the Input Parameters With Values Provided        
                        foreach (SqlParameter Parameter in Command.Parameters)
                        {
                            if (Parameter.Direction == ParameterDirection.Output || Parameter.Direction == ParameterDirection.InputOutput)
                            {
                                htOutputParameters.Add(Parameter.ParameterName, Parameter.Value);
                            }
                        }
                    }
                    catch (SqlException sqlException)
                    {
                        throw sqlException;
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    finally
                    {
                        if (Connection.State == ConnectionState.Open)
                            Connection.Close();
                    }
                }
            }
            return htOutputParameters;
        }
        public static void AssignTimeOut(System.Data.Common.DbCommand Command)
        {
            Command.CommandTimeout = DBHelper.GetTimeOutFromConfig();
        }
        public static void AssignTimeOut(System.Data.Common.DbDataAdapter Adapter)
        {
            if (Adapter.SelectCommand != null) Adapter.SelectCommand.CommandTimeout = DBHelper.GetTimeOutFromConfig();
            if (Adapter.InsertCommand != null) Adapter.InsertCommand.CommandTimeout = DBHelper.GetTimeOutFromConfig();
            if (Adapter.UpdateCommand != null) Adapter.UpdateCommand.CommandTimeout = DBHelper.GetTimeOutFromConfig();
            if (Adapter.DeleteCommand != null) Adapter.DeleteCommand.CommandTimeout = DBHelper.GetTimeOutFromConfig();
        }
        //SHRI ADDED TIMEOUT
        public static int GetTimeOutFromConfig()
        {
            int timeout = 400;
            try { timeout = 400; }
            catch { timeout = 500; }
            return timeout;
        }
        /// <summary>
        /// This method is used to get a dataset from the result set
        /// </summary>
        /// <param name="StoredProcedureName"></param>
        /// <param name="ParameterValues"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string StoredProcedureName, params object[] ParameterValues)
        {
            DataSet dsResults = null;
            SqlConnection Connection = null;
            if (connectionString != string.Empty && connectionString != null)
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Create Command
                        SqlCommand Command = Connection.CreateCommand();
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandText = StoredProcedureName;
                        // Open Connection
                        Connection.Open();
                        // Discover Parameters for Stored Procedure
                        // Populate command.Parameters Collection.
                        // Causes Round trip to Database.
                        SqlCommandBuilder.DeriveParameters(Command);
                        // Initialize Index of parameterValues Array
                        int index = 0;
                        // Populate the Input Parameters With Values Provided        
                        foreach (SqlParameter Parameter in Command.Parameters)
                        {
                            if (Parameter.Direction == ParameterDirection.Input || Parameter.Direction == ParameterDirection.InputOutput)
                            {
                                Parameter.Value = ParameterValues[index];
                                index++;
                            }
                        }
                        IDataReader DataReader = Command.ExecuteReader(CommandBehavior.CloseConnection);
                        dsResults = new DataSet();
                        dsResults.Tables.Add(GetTable(DataReader));
                    }
                    catch (SqlException sqlException)
                    {
                        throw sqlException;
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    finally
                    {
                        if (Connection.State == ConnectionState.Open)
                            Connection.Close();
                    }
                }
            }
            return dsResults;
        }
        /// <summary>
        /// This method will be used to get SPID from SQL Server
        /// Bineesh Raghavan
        /// </summary>
        /// <returns></returns>
        public static int GetSPID()
        {
            string sql = "SELECT @@SPID";
            int SPID = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                DBHelper.AssignTimeOut(command);
                connection.Open();
                command.CommandType = CommandType.Text;
                SPID = Convert.ToInt32(command.ExecuteScalar());
            }
            return SPID;
        }
        /// <summary>
        /// This method will be used to get SPID from SQL Server using SqlConnection which is in transaction
        /// Bineesh Raghavan
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static int GetSPID(SqlConnection conn)
        {
            string sql = "SELECT @@SPID";
            int SPID = -1;
            SqlCommand command = new SqlCommand(sql, conn);
            DBHelper.AssignTimeOut(command);
            if (conn.State != ConnectionState.Open)
                conn.Open();
            command.CommandType = CommandType.Text;
            SPID = Convert.ToInt32(command.ExecuteScalar());
            return SPID;
        }
        /// <summary>
        /// method to return SqlDataReader
        /// </summary>
        /// <param name="StoredProcedureName"></param>
        /// <param name="parameterList"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteSqlDataReader(string StoredProcedureName, SqlParameter[] parameterList)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = GetOpenConnection();
                SqlDataReader reader;
                SqlCommand comm = new SqlCommand(StoredProcedureName, conn);
                comm.CommandType = CommandType.StoredProcedure;
                AssignTimeOut(comm);
                foreach (SqlParameter prm in parameterList)
                {
                    if ((prm.Value == null) || (prm.Value.ToString() == ""))
                    {
                        prm.SqlValue = DBNull.Value;
                    }
                    comm.Parameters.Add(prm);
                }

                reader = comm.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                BradyCorp.Log.LoggerHelper.LogMessage("ExecuteSqlDataReader " + ex.ToString());
                //BradyCorp.Log.LoggerHelper.LogMessage(ex.ToString());
                //                conn.Close();
                throw ex;
            }
            
        }

        public static SqlDataReader ExecuteSqlDataReaderWithoutParameter(string StoredProcedureName)
        {
            SqlDataReader reader;
            SqlConnection conn = GetOpenConnection();
            SqlCommand comm = new SqlCommand(StoredProcedureName, conn);
            comm.CommandType = CommandType.StoredProcedure;
            AssignTimeOut(comm);

            try
            {
                reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
            return reader;
        }




        public static SqlDataReader ExecuteSqlReaderWithoutParameter(string SQLName)
        {
            SqlDataReader reader;
            SqlConnection conn = GetOpenConnection();
            SqlCommand comm = new SqlCommand(SQLName, conn);
            comm.CommandType = CommandType.Text;
            AssignTimeOut(comm);

            try
            {
                reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
            return reader;
        }

        /// <summary>
        /// This will create a DataTable from the SqlDataReader
        /// </summary>
        /// <param name="Reader"></param>
        /// <returns></returns>
        private DataTable GetTable(IDataReader Reader)
        {
            DataTable _SchemaTable = Reader.GetSchemaTable();
            DataTable _Table = new DataTable();
            DataColumn _DataColumn;
            DataRow _Row;
            System.Collections.ArrayList _ArrayList = new System.Collections.ArrayList();
            for (int RowCount = 0; RowCount < _SchemaTable.Rows.Count; RowCount++)
            {
                _DataColumn = new DataColumn();
                if (!_Table.Columns.Contains(_SchemaTable.Rows[RowCount]["ColumnName"].ToString()))
                {
                    _DataColumn.ColumnName = _SchemaTable.Rows[RowCount]["ColumnName"].ToString();
                    if (_DataColumn.ColumnName == "StartDate" || _DataColumn.ColumnName == "EndDate")
                    {
                        _DataColumn.DataType = System.Type.GetType("System.DateTime");
                    }
                    _DataColumn.Unique = Convert.ToBoolean(_SchemaTable.Rows[RowCount]["IsUnique"]);
                    _DataColumn.AllowDBNull = Convert.ToBoolean(_SchemaTable.Rows[RowCount]["AllowDBNull"]);
                    _DataColumn.ReadOnly = Convert.ToBoolean(_SchemaTable.Rows[RowCount]["IsReadOnly"]);
                    _ArrayList.Add(_DataColumn.ColumnName);
                    _Table.Columns.Add(_DataColumn);
                }
            }
            while (Reader.Read())
            {
                _Row = _Table.NewRow();
                for (int RowCount = 0; RowCount < _ArrayList.Count; RowCount++)
                {
                    _Row[((System.String)_ArrayList[RowCount])] = Reader[(System.String)_ArrayList[RowCount]];
                }
                _Table.Rows.Add(_Row);
            }
            return _Table;
        }
    }
}