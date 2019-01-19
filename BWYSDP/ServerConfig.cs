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
            else if (string.Compare(tag, "serverEdit") == 0)
            {
                RefreshAllServerInfo();
            }
        }

        private void ServerConfig_Load(object sender, EventArgs e)
        {
            RefreshAllServerInfo();
        }

        /// <summary>链接</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ServerInfo info;
            SQLite sqlite = new SQLite();
            foreach (object item in this.listBox1.Items)
            {
                info = (ServerInfo)item;
                if (info.IsCurrentServer)
                {
                    info.IsCurrentServer = false;
                    sqlite.Update(info);
                }
            }
            info = (ServerInfo)this.listBox1.SelectedItem;
            info.IsCurrentServer = true;
            sqlite.Update(info);
            RefreshAllServerInfo();

        }
        /// <summary>编辑</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ServerInfo info = (ServerInfo)this.listBox1.SelectedItem;
            if (info != null)
            {
                WakeUpForm<ServerAdd>("edit", info);
            }
        }
        /// <summary>删除</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelet_Click(object sender, EventArgs e)
        {
            ServerInfo info = (ServerInfo)this.listBox1.SelectedItem;
            SQLite sqlite = new SQLite();
            sqlite.Delete(info);
            RefreshAllServerInfo();
        }

        private void RefreshAllServerInfo()
        {
            SQLite sqlite = new SQLite();
            List<ServerInfo> servers = sqlite.SelectAllServer();
            this.listBox1.Items.Clear();
            foreach (ServerInfo item in servers)
            {
                this.listBox1.Items.Add(item);
                if (item.IsCurrentServer)
                    this.listBox1.SelectedItem = item;
            }
        }
    }
}
