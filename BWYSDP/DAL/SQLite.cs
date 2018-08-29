using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

namespace BWYSDP.DAL
{
    public class SQLite
    {
        public SQLite()
        {
            using (SQLiteConnection cn = new SQLiteConnection("Data Source=ServerInfo.db;Pooling=true;FailIfMissing=false"))
            {
                //在打开数据库时，会判断数据库是否存在，如果不存在，则在当前目录下创建一个 
                cn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cn;
                    try
                    {
                        //建立表，如果表已经存在，则报错 
                        cmd.CommandText = "CREATE TABLE [ServerInfo] (" +
                            " ipAddress nvarchar(50)," +
                            "connectype nvarchar(15)," +
                            "serverNm nvarchar(50)," +
                            "point int," +
                            "accountid nvarchar(36)," +
                            "accountname nvarchar(30)," +
                            "IsCurrentServer bit)";
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public bool Insert(ServerInfo info)
        {
            using (SQLiteConnection cn = new SQLiteConnection("Data Source=ServerInfo.db;Pooling=true;FailIfMissing=false"))
            {
                //在打开数据库时，会判断数据库是否存在，如果不存在，则在当前目录下创建一个 
                cn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cn;
                    try
                    {
                        cmd.CommandText = string.Format("insert into ServerInfo values({0},{1},{2},{3},{4},{5},{6}",
                            info.ipAddress,
                            info.connectype,
                            info.serverNm,
                            info.point,
                            info.accountid,
                            info.accountname,
                            info.IsCurrentServer);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public List<ServerInfo> Select()
        {
            List<ServerInfo> result = new List<ServerInfo>();


            return result;
        }

        public ServerInfo GetCurrentServer()
        {
            using (SQLiteConnection cn = new SQLiteConnection("Data Source=ServerInfo.db;Pooling=true;FailIfMissing=false"))
            {
                //在打开数据库时，会判断数据库是否存在，如果不存在，则在当前目录下创建一个 
                cn.Open();
                ServerInfo info = new ServerInfo();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = cn;
                    try
                    {
                        cmd.CommandText = string.Format("Select *from ServerInfo where IsCurrentServer=1");
                        using (SQLiteDataReader read = cmd.ExecuteReader())
                        {
                            info.accountid = read["accountid"].ToString();
                            info.accountname = read["accountname"].ToString();
                            info.connectype = read["connectype"].ToString();
                            info.ipAddress = read["ipAddress"].ToString();
                            info.serverNm = read["serverNm"].ToString();
                            info.IsCurrentServer = (bool)read["IsCurrentServer"];
                            info.point =Convert.ToInt32(read["point"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                return info;
            }
        }

        public bool Update(ServerInfo info)
        {
            return true;
        }

        public bool Delete(ServerInfo info)
        {
            return true;
        }
    }

    //public class DBServer
    //{
    //    //数据库连接
    //    static SQLiteConnection m_dbConnection;
    //    static bool isBegintrans = false;
    //    static DbTransaction trans = null;
    //    //创建一个空的数据库
    //    private static void createNewDatabase()
    //    {
    //        SQLiteConnection.CreateFile(System.Environment.CurrentDirectory + @"\ServerInfo.db");
    //    }

    //    //创建一个连接到指定数据库
    //    private static void connectToDatabase()
    //    {
    //        if (m_dbConnection == null)
    //            m_dbConnection = new SQLiteConnection("Data Source=" + System.Environment.CurrentDirectory + @"\SysDB.db;Pooling=True;Journal Mode=Off");
    //        if (m_dbConnection.State == ConnectionState.Closed || m_dbConnection.State == ConnectionState.Broken)
    //            m_dbConnection.Open();
    //    }

    //    private static void CreateTable()
    //    {
    //        try
    //        {
    //            connectToDatabase();
    //            SQLiteCommand command = new SQLiteCommand(m_dbConnection);
    //            //建立表，如果表已经存在，则报错 
    //            command.CommandText = "CREATE TABLE [ServerInfo] (" +
    //                " ipAddress nvarchar(50)," +
    //                "connectype nvarchar(15)," +
    //                "serverNm nvarchar(50)," +
    //                "point int," +
    //                "accountid nvarchar(36)," +
    //                "accountname nvarchar(30)," +
    //                "IsCurrentServer bit)";
    //            command.ExecuteNonQuery();
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }

    //    public static void Initialdb()
    //    {
    //        createNewDatabase();
    //        CreateTable();
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="sql"></param>
    //    public static void ExecuteNonQuery(string sql)
    //    {
    //        connectToDatabase();
    //        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
    //        command.ExecuteNonQuery();
    //    }
    //    public static object ExecuteScalar(string sql)
    //    {
    //        connectToDatabase();
    //        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
    //        return command.ExecuteScalar();
    //    }
    //    public static void BeginTrans()
    //    {
    //        connectToDatabase();
    //        trans = m_dbConnection.BeginTransaction();

    //    }

    //    public static void StorageTemp(int id, byte[] bts)
    //    {
    //        //string sql = string.Format("insert into Temp(id,conten) values({0},{1})", id, bts);
    //        string sqlStr = "insert into Temp(id,conten) values(@id,@data)";// Id, data
    //        try
    //        {
    //            SQLiteCommand cmd = new SQLiteCommand(sqlStr, m_dbConnection);
    //            cmd.Parameters.Add("@id", System.Data.DbType.String).Value = id;
    //            cmd.Parameters.Add("@data", System.Data.DbType.Binary).Value = bts;
    //            cmd.ExecuteNonQuery();
    //        }
    //        catch (Exception ex)
    //        {
    //            trans.Rollback();
    //        }
    //        //ExecuteNonQuery(sql);
    //    }
    //    public static void CommitTrans()
    //    {
    //        if (isBegintrans && trans != null)
    //            trans.Commit();
    //    }

    //    public static bool Insert(ServerInfo info)
    //    {
    //        using (SQLiteConnection cn = new SQLiteConnection("Data Source=ServerInfo.db;Pooling=true;FailIfMissing=false"))
    //        {
    //            //在打开数据库时，会判断数据库是否存在，如果不存在，则在当前目录下创建一个 
    //            cn.Open();
    //            using (SQLiteCommand cmd = new SQLiteCommand())
    //            {
    //                cmd.Connection = cn;
    //                try
    //                {
    //                    cmd.CommandText = string.Format("insert into ServerInfo values({0},{1},{2},{3},{4},{5},{6}",
    //                        info.ipAddress,
    //                        info.connectype,
    //                        info.serverNm,
    //                        info.point,
    //                        info.accountid,
    //                        info.accountname,
    //                        info.IsCurrentServer);
    //                    cmd.ExecuteNonQuery();
    //                    return true;
    //                }
    //                catch (Exception ex)
    //                {
    //                    return false;
    //                }
    //            }
    //        }
    //    }
    //}

    public class ServerInfo
    {
        public string ipAddress { get; set; }
        public string connectype { get; set; }
        public string serverNm { get; set; }
        public int point { get; set; }
        public string accountid { get; set; }
        public string accountname { get; set; }
        public bool IsCurrentServer { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",serverNm ,ipAddress ,point,accountname);
        }
    }
}
