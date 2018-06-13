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

namespace ServiceTest
{
    public partial class Form1 : Form
    {
        IDBHelpFactory dbHelpFactory;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, false);
            dbHelpFactory = (IDBHelpFactory)Activator.GetObject(typeof(IDBHelpFactory), "tcp://localhost:8085/DBService");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dbHelpFactory != null)
            {
                ILibDBHelp dbhelp = dbHelpFactory.GetDBHelp("b32d655e-161c-477e-b892-a5eba98cc28b");
                object obj = dbhelp.ExecuteScalar("select nickname from ACCOUNT");
                DataTable dt = dbhelp.GetDataTable("select * from ACCOUNT");
            }
        }
    }
}
