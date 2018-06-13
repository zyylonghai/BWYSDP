using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SDPCRL.CORE.FileUtils;
using SDPCRL.CORE;
namespace SDPCRL.DAL.Service
{
    class DBServiceBase
    {
        #region 私有变量，属性
        private static SqlConnection _connection;
        private string connectionStr
        {
            get
            {
                return _connection == null ? string.Empty : _connection.ConnectionString;
                
            }
        }

        protected  SqlConnection Connection
        {
            get 
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(connectionStr);
                    SetConnectionStr(_connection);
                    _connection.Open();
                }
                else if (_connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Open();
                }
                else if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }
        #endregion

        #region 构造函数

        public DBServiceBase()
        {
            
        }
        #endregion

        #region 私有函数

        /// <summary>获取连接字符串 </summary>
        private void SetConnectionStr(SqlConnection connection)
        {
            XMLOperation xmldoc = new XMLOperation(SysConstManage.DBInfoFilePath);
             string  service = xmldoc.NodeRead("/INFO/SERVICE").InnerText.Trim();
            string  database = xmldoc.NodeRead("/INFO/DATABASE").InnerText.Trim();
            string uid = xmldoc.NodeRead("/INFO/DBACCOUNT").InnerText.Trim();
            string pwd = xmldoc.NodeRead("/INFO/PASSWORD").InnerText.Trim();
            connection .ConnectionString=  string.Format("server={0};database={1};uid={2};password={3}", service, database,uid,pwd);

        }
        #endregion
        #region 公开方法

        protected  int DoExecuteNonQuery(string sql)
        {
            SqlCommand comd = new SqlCommand(sql, Connection);
            try
            {
                return comd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _connection.Close();
                return -1;
            }
        }

        protected  DataSet DoExecuteQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            SqlDataAdapter dataadapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                dataadapter.SelectCommand = cmd;
                dataadapter.Fill(ds);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        protected object DoExecuteScalar(string sql)
        {
            object obj = null;
            SqlCommand cmd = new SqlCommand(sql, Connection);
            try
            {
                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            return obj;
        }

        public  void test()
        {
            SqlCommand comd = new SqlCommand("insert into DSINFO values(@v0,@v1,@v2)", Connection);
            comd.Parameters.Add("@v0", SqlDbType.Int);
            comd.Parameters.Add("@v1", SqlDbType.VarChar);
            comd.Parameters.Add("@v2", SqlDbType.VarChar);
            comd.Parameters["@v0"].Value ="223455";
            comd.Parameters["@v1"].Value = 25362;
            comd.Parameters["@v2"].Value = "testzyy";
            comd.ExecuteNonQuery();
        }

        #endregion
    }
}
