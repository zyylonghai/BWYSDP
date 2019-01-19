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
        string _statu = string.Empty;
        public ServerAdd()
        {
            InitializeComponent();
        }

        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
            this._statu = tag;
            if (string.Compare(tag, "add") == 0)
            {
                //info = new ServerInfo();
                this.combConnType.SelectedText = "TCP";
                this.combConnType.Text = "TCP";
                this.txtIPAddress.Text = "192.168.1.5";
                this.txtpoint.Text = "8085";
            }
            else if (string.Compare(tag, "edit") == 0)
            {
                if (param.Length > 0)
                {
                    info = (ServerInfo)param[0];
                    this.txtServerName.Enabled = false;
                    this.txtServerName.Text = info.serverNm;
                    this.combConnType.SelectedText = info.connectype;
                    this.combConnType.Text = info.connectype;
                    this.txtIPAddress.Text = info.ipAddress;
                    this.txtpoint.Text = info.point.ToString();
                    this.combaccountId.DataSource = new List<ServerInfo> { info };
                    this.combaccountId.ValueMember = "accountid";
                    this.combaccountId.DisplayMember = "accountname";
                    this.combaccountId.SelectedValue = info.accountid;
                    this.combaccountId.SelectedText = info.accountname;

                }
            }
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
            SQLite sqlite = new SQLite();
            switch (this._statu)
            {
                case "add":
                    sqlite.Insert(info);
                    break;
                case "edit":
                    sqlite.Update(info);
                    break;
            }
            this.Close();
        }

        protected override void ReturnParam(ref string tag, Dictionary<object, object> param)
        {
            base.ReturnParam(ref tag, param);
            switch (this._statu)
            {
                case "add":
                    tag = "serverAdd";
                    if (info != null)
                    {
                        param.Add("info", info);
                    }
                    break;
                case "edit":
                    tag = "serverEdit";
                    break;
            }
        }

        private void combaccountId_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtIPAddress.Text))
            {
                SDPCRL.BLL.BUS.ServerInfo.IPAddress = this.txtIPAddress.Text.Trim();
            }
            else
            {
                return;
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
            BLL.BllDataBase bll = new BLL.BllDataBase(false);
            Dictionary<string, string> dic = bll.GetAccount();
            ServerInfo info = null;
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
