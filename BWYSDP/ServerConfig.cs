using BWYSDP.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BWYSDP
{
    public partial class ServerConfig : LibFormBase
    {
        public ServerConfig()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            WakeUpForm<ServerAdd>("add");
        }

        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
            if (string.Compare("SetServer", tag) == 0)
            {
 
            }
        }

        public override void DoFormAcceptMsg(string tag, Dictionary<object, object> agrs)
        {
            base.DoFormAcceptMsg(tag, agrs);
            if (string.Compare(tag, "serverAdd") == 0)
            {
                if (agrs != null && agrs.Count > 0)
                {
                    object info;
                    if (agrs.TryGetValue("info", out info) &&info!=null)
                    {
                        this.listBox1.Items.Add(info);
                    }

                }
            }
        }

        private void ServerConfig_Load(object sender, EventArgs e)
        {
            SQLite sqlite = new SQLite();
            List<ServerInfo > servers= sqlite.SelectAllServer();
            foreach (ServerInfo item in servers)
            {
                this.listBox1.Items.Add(item);
                if (item.IsCurrentServer)
                    this.listBox1.SelectedItem = item;
            }
        }
    }
}
