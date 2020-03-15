using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDPCRL.DAL.DBHelp;
using SDPCRL.CORE.FileUtils;
using SDPCRL.CORE;
using System.Configuration;
using SDPCRL.DAL.IDBHelp;
using SDPCRL.DAL.COM;
using BWYResFactory;

namespace InitialTool
{
    public partial class DBConfig : Form
    {
        string filepath;
        public DBConfig()
        {
            InitializeComponent();
            this.combConType.SelectedItem = "TCP";

            this.combDataBaseType.SelectedItem = "SQL SERVER";
            this.txtServerAddr.Text = @".\ZYY";
            this.txtUserId.Text = "sa";
            this.txtpwd.Text = "152625";
            this.txtDataBase.Text = "BWYDB_SYS";

            //this.combDataBaseType.SelectedItem = "ORACLE";
            //this.txtServerAddr.Text = "127.0.0.1";
            //this.txtUserId.Text = "zyy";
            //this.txtpwd.Text = "152625zyy";
            //this.txtDataBase.Text = "ORCLZYY";
            //filepath= string.Format(@"{0}\{1}", Environment.CurrentDirectory, ConfigurationManager.AppSettings["DBFilePath"].ToString());
        }
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string ex = string.Empty;
            ILibDBHelp help=null;
            string connectStr = string.Empty;
            switch (this.combDataBaseType.Text.Trim())
            {
                case "SQL SERVER":
                    help = new DBHelpFactory().GetDBHelp(LibProviderType.SqlServer);
                    connectStr = string.Format("server={0};database={1};uid={2};password={3}", this.txtServerAddr.Text, this.txtDataBase.Text, this.txtUserId.Text, this.txtpwd.Text);
                    break;
                case "ORACLE":
                    help = new DBHelpFactory().GetDBHelp(LibProviderType.Oracle);
                    ex = string.Empty;
                    connectStr = string.Format("Provider=OraOLEDB.Oracle.1;User ID={0};Password={1};" +
                       "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = {2})(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = {3})))",
                       this.txtUserId.Text.Trim(), this.txtpwd.Text.Trim(), this.txtServerAddr.Text.Trim(), this.txtDataBase.Text.Trim());
                    //connectStr = string.Format("User ID={0};Password={1};Data Source={2}",this.txtUserId.Text .Trim (),this.txtpwd .Text .Trim (),this.txtDataBase.Text .Trim ());
                    break;
            }
            if (help != null)
            {
                help.TestConnect(connectStr, out ex);
                MessageBox.Show(ex);
            }
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DBInfoHelp help = new DBInfoHelp();
            DBInfo dbinfo = new DBInfo();
            dbinfo.Key = DesCryptFactory.GenerateKey();
            dbinfo.DataBase = this.txtDataBase.Text.Trim();
            dbinfo.ServerAddr = this.txtServerAddr.Text.Trim();
            dbinfo.UserId = this.txtUserId.Text.Trim();
            dbinfo.Password = this.txtpwd.Text.Trim();
            dbinfo.Guid = dbinfo.DataBase == ResFactory.ResManager.LogDBNm ? dbinfo.DataBase : Guid.NewGuid().ToString();
            switch (this.combConType.SelectedText.Trim())
            {
                case "TCP":
                    dbinfo.ConnectType = LibConnectType.TCP;
                    break;
                case "HTTP":
                    dbinfo.ConnectType = LibConnectType.HTTP;
                    break;
            }
            switch (this.combDataBaseType.Text.Trim())
            {
                case "SQL SERVER":
                    dbinfo.ProviderType = LibProviderType.SqlServer;
                    break;
                case "ORACLE":
                    dbinfo.ProviderType = LibProviderType.Oracle;
                    break;
            }

            help.BinaryWriteDBInfo(dbinfo);
            //MessageBox.Show(help.ExceptionMessage);
            ILibDBHelp dbhelp = new DBHelpFactory().GetDBHelp(dbinfo.ProviderType);
            if (dbhelp.SaveAccout(dbinfo))
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存出错");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ILibDBHelp dbhelp = new DBHelpFactory().GetDBHelp();
            //FileOperation file = new FileOperation();
            //file.FilePath = SysConstManage.DBFilePath;
            //string info= file.BinaryReadDBConnectStr();
            object obj= dbhelp.ExecuteScalar("select nickname from ACCOUNT");
        }

        private void DBConfig_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("DBServerInfo");
            DataColumn col = null;
            col = new DataColumn("Guid");
            col.Caption = "账套Guid";
            dt.Columns.Add(col);

            col = new DataColumn("DataBase");
            col.Caption = "账套";
            dt.Columns.Add(col);

            col = new DataColumn("ServerAddr");
            col.Caption = "服务地址";
            dt.Columns.Add(col);

            col = new DataColumn("ConnectType");
            col.Caption = "数据库链接方式";
            dt.Columns.Add(col);

            col = new DataColumn("ProviderType");
            col.Caption = "数据库驱动类型";
            dt.Columns.Add(col);

            col = new DataColumn("UserId");
            col.Caption = "用户名";
            dt.Columns.Add(col);

            this.dataGridView1.DataSource = dt;
            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
            {
                c.HeaderText = dt.Columns[c.Name].Caption;
            }

            DBInfoHelp help = new DBInfoHelp();
            if (!string.IsNullOrEmpty(help.ReadSysDBConnect()))
            {
                List<DBInfo> dBInfos = help.GetAccoutSetting();

                ILibDBHelp dbhelp = new DBHelpFactory().GetDBHelp();
                DataTable accoutDT= dbhelp.GetAccout();
                if (accoutDT != null && accoutDT.Rows != null)
                {
                    DataRow row = null;
                    foreach (DBInfo info in dBInfos)
                    {
                        DataRow[] rows = accoutDT.Select(string.Format("ID='{0}'", info.Guid));
                        if (rows == null || rows.Length == 0)
                        {
                            continue;
                        }
                        help.Guid = rows[0]["ID"].ToString();
                        help.Key = rows[0]["key"].ToString();
                        help.ReadDBConnect();
                        row = dt.NewRow();
                        row["Guid"] = rows[0]["ID"];
                        row["DataBase"] = rows[0]["AccoutNm"];
                        row["ServerAddr"] = rows[0]["IPAddress"];
                        row["ProviderType"] = help.ProviderType;
                        dt.Rows.Add(row);
                    }
                    //foreach (DataRow dr in accoutDT.Rows)
                    //{
                    //    if (dr["AccoutNm"].ToString() == ResFactory.ResManager.SysDBNm)
                    //    {
                    //        help.ReadSysDBConnect();
                    //    }
                    //    else
                    //    {
                    //        help.Guid = dr["ID"].ToString();
                    //        help.Key = dr["key"].ToString();
                    //        help.ReadDBConnect();
                    //    }
                    //    row = dt.NewRow();
                    //    row["Guid"] = dr["ID"];
                    //    row["DataBase"] = dr["AccoutNm"];
                    //    row["ServerAddr"] = dr["IPAddress"];
                    //    row["ProviderType"] = help.ProviderType;
                    //    dt.Rows.Add(row);
                    //}
                }
            }

        }
    }
}
