using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDPCRL.DAL.IDBHelp;
using SDPCRL.IBussiness;

namespace ServiceTest
{
    public partial class Form1 : Form
    {
        IDBHelpFactory dbHelpFactory;
        IDALBus DALBus;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, false);
            dbHelpFactory = (IDBHelpFactory)Activator.GetObject(typeof(IDBHelpFactory), "tcp://192.168.1.3:8085/DBService");
            DALBus = (IDALBus)Activator.GetObject(typeof(IDALBus), "tcp://192.168.1.3:8085/DALServer");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dbHelpFactory != null)
            {
                ILibDBHelp dbhelp = dbHelpFactory.GetDBHelp();
                object obj = dbhelp.ExecuteScalar("declare @id varchar(10);set @id=1; select AccoutNm,@id from ACCOUT");
                ILibDBHelp dbhelp2 = dbHelpFactory.GetDBHelp("14f5d469-a774-484e-b467-2f80db09a5d3");
                DataTable dt = dbhelp2.GetDataTable("EXEC sp_executesql N'select * from INFO_lanmu where lanmu_ID=@id and lanmu_categorie_ID=@cid',N'@id nchar(3),@cid nchar(3)',@id='001',@cid='001'");
            }
            if (DALBus != null)
            {
                object obj = DALBus.ExecuteDalUpdate("TestFunc");
            }
        }
    }
}
