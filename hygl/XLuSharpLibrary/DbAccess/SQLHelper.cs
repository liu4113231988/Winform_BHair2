using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLuSharpLibrary.DbAccess
{
    public class SQLHelper
    {
        public SQLHelper()
        {
            if (DbConfig.DbConnection == "" || DbConfig.DbConnection == null)
            {
                throw new Exception("链接字符串不能为空！");
            }
            switch (DbConfig.DbType)
            {
                case DBType.SQLServer:
                    _DbCommand = new SqlCommand();
                    _DbCommand.Connection = new SqlConnection(DbConfig.DbConnection);
                    _DbDataAdapter = new SqlDataAdapter();
                    break;
                case DBType.Access:
                    _DbCommand = new OleDbCommand();
                    _DbCommand.Connection = new OleDbConnection(DbConfig.DbConnection);
                    _DbDataAdapter = new OleDbDataAdapter();
                    break;
                case DBType.Mysql:
                    _DbCommand = new MySqlCommand();
                    _DbCommand.Connection = new MySqlConnection(DbConfig.DbConnection);
                    _DbDataAdapter = new MySqlDataAdapter();
                    break;
            }
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002108 File Offset: 0x00000308
        public SQLHelper(string strConn, DBType dbtype)
        {
            switch (dbtype)
            {
                case DBType.SQLServer:
                    this._DbCommand = new SqlCommand();
                    this._DbCommand.Connection = new SqlConnection(strConn);
                    this._DbDataAdapter = new SqlDataAdapter();
                    break;
                case DBType.Access:
                    this._DbCommand = new OleDbCommand();
                    this._DbCommand.Connection = new OleDbConnection(strConn);
                    this._DbDataAdapter = new OleDbDataAdapter();
                    break;
                case DBType.Mysql:
                    _DbCommand = new MySqlCommand();
                    _DbCommand.Connection = new MySqlConnection(strConn);
                    _DbDataAdapter = new MySqlDataAdapter();
                    break;
            }
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002184 File Offset: 0x00000384
        private void OpenConnection()
        {
            try
            {
                if (this._DbCommand.Connection.State == ConnectionState.Closed)
                {
                    this._DbCommand.Connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000021E4 File Offset: 0x000003E4
        private void CloseConnection()
        {
            if (this._DbCommand.Connection.State == ConnectionState.Open)
            {
                this._DbCommand.Connection.Close();
            }
            if (this._DbCommand != null)
            {
                this._DbCommand.Dispose();
            }
        }

        // Token: 0x06000005 RID: 5 RVA: 0x0000223C File Offset: 0x0000043C
        public int ExecuteSql(string cmdText)
        {
            int result;
            try
            {
                this._DbCommand.CommandText = cmdText;
                this.OpenConnection();
                result = this._DbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw new Exception(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000022AC File Offset: 0x000004AC
        public int ExecuteSql(string cmdText, IDataParameter[] cmdParameters)
        {
            int result;
            try
            {
                this._DbCommand.CommandText = cmdText;
                foreach (IDataParameter value in cmdParameters)
                {
                    this._DbCommand.Parameters.Add(value);
                }
                this.OpenConnection();
                result = this._DbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw new Exception(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        // Token: 0x06000007 RID: 7 RVA: 0x00002350 File Offset: 0x00000550
        public void ExecuteSql(string cmdText, out DataTable dt)
        {
            try
            {
                this._DbCommand.CommandText = cmdText;
                this._DbDataAdapter.SelectCommand = this._DbCommand;
                DataSet dataSet = new DataSet();
                this._DbDataAdapter.Fill(dataSet);
                dt = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw new Exception(ex.Message);
            }
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000023C8 File Offset: 0x000005C8
        public void ExecuteSql(string cmdText, IDataParameter[] cmdParameters, out DataTable dt)
        {
            try
            {
                this._DbCommand.CommandText = cmdText;
                foreach (IDataParameter value in cmdParameters)
                {
                    this._DbCommand.Parameters.Add(value);
                }
                this._DbDataAdapter.SelectCommand = this._DbCommand;
                DataSet dataSet = new DataSet();
                this._DbDataAdapter.Fill(dataSet);
                dt = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw new Exception(ex.Message);
            }
        }

        // Token: 0x06000009 RID: 9 RVA: 0x00002474 File Offset: 0x00000674
        public int ExecuteProc(string procName)
        {
            int result;
            try
            {
                this._DbCommand.CommandText = procName;
                this._DbCommand.CommandType = CommandType.StoredProcedure;
                this.OpenConnection();
                result = this._DbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw new Exception(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        // Token: 0x0600000A RID: 10 RVA: 0x000024F0 File Offset: 0x000006F0
        public int ExecuteProc(string procName, IDataParameter[] cmdParameters)
        {
            int result;
            try
            {
                this._DbCommand.CommandText = procName;
                this._DbCommand.CommandType = CommandType.StoredProcedure;
                foreach (IDataParameter value in cmdParameters)
                {
                    this._DbCommand.Parameters.Add(value);
                }
                this.OpenConnection();
                result = this._DbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw new Exception(ex.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return result;
        }

        // Token: 0x0600000B RID: 11 RVA: 0x000025A0 File Offset: 0x000007A0
        public void ExecuteProc(string procName, out DataTable dt)
        {
            try
            {
                this._DbCommand.CommandText = procName;
                this._DbCommand.CommandType = CommandType.StoredProcedure;
                this._DbDataAdapter.SelectCommand = this._DbCommand;
                DataSet dataSet = new DataSet();
                this._DbDataAdapter.Fill(dataSet);
                dt = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw new Exception(ex.Message);
            }
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002628 File Offset: 0x00000828
        public void ExecuteProc(string procName, IDataParameter[] cmdParameters, out DataTable dt)
        {
            try
            {
                this._DbCommand.CommandText = procName;
                this._DbCommand.CommandType = CommandType.StoredProcedure;
                foreach (IDataParameter value in cmdParameters)
                {
                    this._DbCommand.Parameters.Add(value);
                }
                this._DbDataAdapter.SelectCommand = this._DbCommand;
                DataSet dataSet = new DataSet();
                this._DbDataAdapter.Fill(dataSet);
                dt = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                this.CloseConnection();
                throw new Exception(ex.Message);
            }
        }

        // Token: 0x0600000D RID: 13 RVA: 0x000026E4 File Offset: 0x000008E4
        public bool ExecuteTransaction(string[] cmdTexts)
        {
            try
            {
                this.OpenConnection();
                this._DbTransaction = this._DbCommand.Connection.BeginTransaction();
                this._DbCommand.Transaction = this._DbTransaction;
                foreach (string commandText in cmdTexts)
                {
                    this._DbCommand.CommandText = commandText;
                    this._DbCommand.ExecuteNonQuery();
                }
                this._DbTransaction.Commit();
            }
            catch
            {
                this._DbTransaction.Rollback();
                this.CloseConnection();
                return false;
            }
            return true;
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002798 File Offset: 0x00000998
        public bool ExecuteTransaction(string[] cmdTexts, List<IDataParameter[]> lstParameter, int count)
        {
            try
            {
                this.OpenConnection();
                this._DbTransaction = this._DbCommand.Connection.BeginTransaction();
                this._DbCommand.Transaction = this._DbTransaction;
                for (int i = 0; i < count; i++)
                {
                    this._DbCommand.CommandText = cmdTexts[i];
                    foreach (IDataParameter value in lstParameter[i])
                    {
                        this._DbCommand.Parameters.Add(value);
                    }
                    this._DbCommand.ExecuteNonQuery();
                }
                this._DbTransaction.Commit();
            }
            catch
            {
                this._DbTransaction.Rollback();
                this.CloseConnection();
                return false;
            }
            return true;
        }

        // Token: 0x04000001 RID: 1
        private IDbCommand _DbCommand;

        // Token: 0x04000002 RID: 2
        private IDbDataAdapter _DbDataAdapter;

        // Token: 0x04000003 RID: 3
        private IDbTransaction _DbTransaction;
    }
}
