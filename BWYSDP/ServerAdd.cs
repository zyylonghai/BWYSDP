using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.DAL;

namespace BWYSDP
{
    public partial class ServerAdd : LibFormBase
    {
        private string _tag = string.Empty;
        ServerInfo info = null;
        public ServerAdd()
        {
            InitializeComponent();
        }

        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
            this.txtIPAddress.Text = "192.168.1.3";
            this.txtpoint.Text = "8085";
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            info = new ServerInfo();
            info.connectype = this.combConnType.Text;
            info.accountid = this.combaccountId.SelectedValue.ToString();
            info.accountname = this.combaccountId.Text;
            info.ipAddress = this.txtIPAddress.Text.Trim();
            info.point = Convert.ToInt32(this.txtpoint.Text.Trim());
            info.serverNm = this.txtServerName.Text.Trim();
            info.IsCurrentServer = true;
            SQLite sqlite = new SQLite();
            sqlite.Insert(info);
            this.Close();
        }

        protected override void ReturnParam(ref string tag, Dictionary<object, object> param)
        {
            base.ReturnParam(ref tag, param);
            tag = "serverAdd";
            param.Add("info", info);
        }

        private void combaccountId_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtIPAddress.Text))
            {
                SDPCRL.BLL.BUS.ServerInfo.IPAddress = this.txtIPAddress.Text.Trim();
            }
            if (!string.IsNullOrEmpty(this.combConnType.Text))
            {
                SDPCRL.BLL.BUS.ServerInfo.ConnectType = this.combConnType.Text.Trim();
            }
            if (!string.IsNullOrEmpty(this.txtpoint.Text))
            {
                SDPCRL.BLL.BUS.ServerInfo.Point = Convert.ToInt32(this.txtpoint.Text.Trim());
            }
            this.combaccountId.DataSource = GetData();
            this.combaccountId.ValueMember = "accountid";
            this.combaccountId.DisplayMember = "accountname";
        }

        private void combaccountId_SelectedValueChanged(object sender, EventArgs e)
        {
            this.combaccountId.ValueMember = "accountid";
            this.combaccountId.DisplayMember = "accountname";
        }

        private List<ServerInfo> GetData()
        {
            List<ServerInfo> data = new List<ServerInfo>();
            Dictionary <string ,string > dic= this.BllData.GetAccount();
            ServerInfo info=null ;
            foreach (KeyValuePair<string, string> item in dic)
            {
                info = new ServerInfo();
                info.accountid = item.Key;
                info.accountname = item.Value;
                data.Add(info);
            }

            return data;
        }
    }
}
