
//----------------------------------
/*
 * 
    File Name       :   DbHelper.cs
    Author Name     :   
    Creation Date   :   20 Nov 2010
    Desciption      :   It contains db connection with some useful method used during data access    
 
 Modification History   : 
 * 
 * Modified By  |   Modified Date   |   Description
 * ---------------------------------------------------------------------------------
 * Name         |   20 Nov 2010     |   create db helper methods

*/
//----------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;


namespace Erp.Data.Common
{
    public class DbHelper
    {
        DbHelper dbHelper;
        int TimeOut = 0;
        #region----------variable declaration

        static SqlCommand dbSqlCommand;
        static SqlDataAdapter dbSqlDataAdapter;

        public static string constring;//= "WebEBSConnectionstring";//Closing


        #endregion


        #region----------method declaration

        /// <summary>
        /// get sql db connection
        /// </summary>
        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["WindowsConfigurationManager"].ConnectionString);
                //return new SqlConnection(WebConfigurationManager.ConnectionStrings[HttpContext.Current.Session["constring"].ToString()].ToString());
            }
        }

        /// <summary>
        /// get sql db connection
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["WindowsConfigurationManager"].ConnectionString);
           
        }
    
        public SqlCommand getSqlCommand()
        {
            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = GetSqlConnection();

            return sqlCommand;
        }
       


        public DataTable GetTable(string storeProcedureName)
        {
            SqlCommand sqlCommand = null;

            try
            {
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                using (SqlConnection conn = GetSqlConnection())
                {
                    conn.Open();

                    sqlCommand = new SqlCommand();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = storeProcedureName;
                    sqlCommand.CommandTimeout = TimeOut;
                    sqlAdapter.SelectCommand = sqlCommand;

                    DataTable dt = new DataTable();
                    sqlAdapter.Fill(dt);
                    return dt;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();

            }
        }


        /// <summary>
        /// to get data table used in drop down list
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="firtsColumn">pass id column name</param>
        /// <param name="secondColunName">pass display column name</param>
        /// <returns>data table</returns>
        public DataTable GetTableForDropDown(string storeProcedureName, string firtsColumn, string secondColunName)
        {
            SqlCommand sqlCommand = null;

            try
            {
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();

                sqlCommand = getSqlCommand();
                sqlCommand.CommandText = storeProcedureName;

                sqlAdapter.SelectCommand = sqlCommand;
                DataTable dt = new DataTable();

                dt.Columns.Add(firtsColumn);
                dt.Columns.Add(secondColunName);
                sqlAdapter.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// to get data table used in drop down list
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="firtsColumn">pass id column name</param>
        /// <param name="secondColunName">pass display column name</param>
        /// <returns>data table</returns>
        public DataTable GetTableForDropDown(string storeProcedureName, SqlParameter[] arrParams, string firtsColumn, string secondColunName)
        {
            SqlCommand sqlCommand = null;

            try
            {
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();

                sqlCommand = getSqlCommand();
                sqlCommand.CommandText = storeProcedureName;
                sqlCommand.CommandTimeout = 0;

                if (arrParams != null)
                {
                    foreach (SqlParameter param in arrParams)
                        sqlCommand.Parameters.Add(param);
                }
                sqlAdapter.SelectCommand = sqlCommand;
                DataTable dt = new DataTable();

                dt.Columns.Add(firtsColumn);
                dt.Columns.Add(secondColunName);
                sqlAdapter.Fill(dt);

                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }
        /// <summary>
        /// to get filtered data table from store procedure 
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="arrParams">pass filter parameters</param>
        /// <returns>data table</returns>
        public DataTable GetTable(string storeProcedureName, SqlParameter[] arrParams)
        {
            SqlCommand sqlCommand = null;
            try
            {
                SqlDataAdapter m_da = new SqlDataAdapter();
                // sqlCommand = getSqlCommand();

                //using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["WebEBSConnectionstring"].ToString()))
                using (SqlConnection conn = GetSqlConnection())
                {
                    conn.Open();
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = storeProcedureName;
                    sqlCommand.CommandTimeout = TimeOut;
                    if (arrParams != null)
                    {
                        foreach (SqlParameter param in arrParams)
                            sqlCommand.Parameters.Add(param);
                    }
                    m_da.SelectCommand = sqlCommand;
                    DataTable dt = new DataTable();
                    m_da.Fill(dt);
                    return dt;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// to get data set
        /// </summary>
        /// <param name="spName">pass stored procedure name</param>
        /// <returns>data set</returns>
        public DataSet GetDataSet(string spName)
        {
            try
            {
                SqlDataAdapter m_da = new SqlDataAdapter();
                //SqlCommand m_cmd = getSqlCommand();

                //using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["WebEBSConnectionstring"].ToString()))
                using (SqlConnection conn = GetSqlConnection())
                {
                    conn.Open();
                    SqlCommand m_cmd = new SqlCommand();
                    m_cmd.CommandType = CommandType.StoredProcedure;
                    m_cmd.Connection = conn;
                    m_cmd.CommandType = CommandType.StoredProcedure;
                    m_cmd.CommandTimeout = TimeOut;
                    m_cmd.CommandText = spName;

                    m_da.SelectCommand = m_cmd;

                    DataSet ds = new DataSet();
                    m_da.Fill(ds);

                    return ds;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// to add parameters in stored procedure 
        /// </summary>
        /// <param name="cmd">sql command</param>
        /// <param name="parameters">pass stored procedure parameters</param>
        private static void AddParameters(ref SqlCommand cmd, SqlParameter[] parameters)
        {
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = (object)DBNull.Value;
                }
                cmd.Parameters.Add(para);
            }

        }

        /// <summary>
        /// to execute sql query
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="parameterValues">pass stored procedure parameters value</param>
        /// <returns>int</returns>
        public Int32 ExecuteNonQuery(string storeProcedureName, SqlParameter[] parameterValues)
        {
            SqlCommand sqlCommand = null;


            try
            {

                //sqlCommand = getSqlCommand();
                //using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["WebEBSConnectionstring"].ToString()))
                using (SqlConnection conn = GetSqlConnection())
                {
                    conn.Open();
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = storeProcedureName;
                    AddParameters(ref sqlCommand, parameterValues);
                    sqlCommand.CommandTimeout = TimeOut;
                    return sqlCommand.ExecuteNonQuery();
                }
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// to execute scaler
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="parameterValues">pass stored procedure parameters value</param>
        /// <returns>string</returns>
        public string ExecuteScalar(string storeProcedureName, SqlParameter[] parameterValues)
        {
            SqlCommand sqlCommand = null;

            try
            {
                //using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["WebEBSConnectionstring"].ToString()))
                using (SqlConnection conn = GetSqlConnection())
                {
                    conn.Open();
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = storeProcedureName;
                    sqlCommand.CommandTimeout = TimeOut;
                    AddParameters(ref sqlCommand, parameterValues);
                    return Convert.ToString(sqlCommand.ExecuteScalar());
                }
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }


        /// <summary>
        /// to execute scaler
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <returns>string</returns>
        public string ExecuteScalar(string storeProcedureName)
        {
            SqlCommand sqlCommand = null;

            try
            {
                //using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["WebEBSConnectionstring"].ToString()))
                using (SqlConnection conn = GetSqlConnection())
                {
                    conn.Open();
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandText = storeProcedureName;
                    sqlCommand.CommandTimeout = TimeOut;
                    return Convert.ToString(sqlCommand.ExecuteScalar());
                }
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// to get data set
        /// </summary>
        /// <param name="spName">pass stored procedure name</param>
        /// <param name="parameters">pass parameters</param>
        /// <returns>data set</returns>
        public DataSet GetDataSet(string spName, SqlParameter[] parameters)
        {
            try
            {
                SqlDataAdapter m_da = new SqlDataAdapter();
                //using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["WebEBSConnectionstring"].ToString()))
                using (SqlConnection conn = GetSqlConnection())
                {
                    conn.Open();
                    SqlCommand m_cmd = new SqlCommand();
                    m_cmd.CommandType = CommandType.StoredProcedure;
                    m_cmd.Connection = conn;
                    m_cmd.CommandType = CommandType.StoredProcedure;
                    m_cmd.CommandTimeout = TimeOut;
                    m_cmd.CommandText = spName;
                    AddParameters(ref m_cmd, parameters);
                    m_da.SelectCommand = m_cmd;
                    DataSet ds = new DataSet();
                    m_da.Fill(ds);
                    return ds;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// to execute reader
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="dbDataSet">pass data set</param>
        public void ExecuteReader(string storeProcedureName, ref DataSet dbDataSet)
        {
            try
            {
                dbSqlCommand = new SqlCommand();
                dbSqlCommand.CommandText = storeProcedureName;
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.Connection = Connection;
                dbSqlCommand.CommandTimeout = TimeOut;
                dbSqlDataAdapter = new SqlDataAdapter(dbSqlCommand);
                dbSqlDataAdapter.Fill(dbDataSet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dbSqlCommand.Dispose();
            }

        }

        /// <summary>
        /// to execute reader
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="dbTable">pass parameters</param>
        /// <param name="parameterValues">pass parameters value</param>
        public void ExecuteReader(string storeProcedureName, ref DataTable dbTable, SqlParameter[] parameterValues)
        {
            try
            {
                dbSqlCommand = new SqlCommand();
                dbSqlDataAdapter = new SqlDataAdapter();
                dbSqlCommand.CommandText = storeProcedureName;
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.Connection = Connection;
                dbSqlCommand.CommandTimeout = TimeOut;
                dbSqlDataAdapter.SelectCommand = dbSqlCommand;
                AddParameters(ref dbSqlCommand, parameterValues);
                dbSqlDataAdapter.Fill(dbTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dbSqlCommand.Dispose();
            }
        }

        /// <summary>
        /// to get data reader
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="parameterValues">pass stored procedure parameters value</param>
        /// <returns></returns>
        public IDataReader ExecuteDataReader(string storeProcedureName, SqlParameter[] parameterValues)
        {
            try
            {
                dbSqlCommand = new SqlCommand();
                dbSqlCommand.CommandText = storeProcedureName;
                dbSqlCommand.Connection = Connection;
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandTimeout = TimeOut;
                dbSqlCommand.Connection.Open();
                AddParameters(ref dbSqlCommand, parameterValues);
                dbSqlCommand.Parameters.Add(new SqlParameter("@returnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
                return dbSqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dbSqlCommand.Connection.Close();
                dbSqlCommand.Connection.Dispose();
                dbSqlCommand.Dispose();
            }
        }

        /// <summary>
        /// to execute reader
        /// </summary>
        /// <param name="storeProcedureName">pass stored procedure name</param>
        /// <param name="dbDataSet">pass data set</param>
        /// <param name="parameterValues">pass parameters</param>
        public static void ExecuteReader(string storeProcedureName, ref DataSet dbDataSet, SqlParameter[] parameterValues)
        {
            try
            {
                dbSqlCommand = new SqlCommand();
                dbSqlDataAdapter = new SqlDataAdapter();
                dbSqlCommand.CommandText = storeProcedureName;
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.Connection = Connection;
                dbSqlDataAdapter.SelectCommand = dbSqlCommand;
                AddParameters(ref dbSqlCommand, parameterValues);
                dbSqlDataAdapter.Fill(dbDataSet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dbSqlCommand.Dispose();
            }

        }

      

        public static DataTable GetData(string sql, SqlConnection con)
        {
            // write code to fill datatable.
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Connection.Open();
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dtab = new DataTable();
            ada.Fill(dtab);
            cmd.Dispose();
            return dtab;
        }

        #endregion
    }
}

