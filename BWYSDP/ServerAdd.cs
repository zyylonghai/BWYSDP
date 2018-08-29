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
            //if (string.Compare(tag, "SetServer") == 0)
            //{
                
            //}
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
            info.IsCurrentServer = false;
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
            //this.combaccountId.Items.Clear();
            //ServerInfo info = new ServerInfo();
            //info.accountid = "test1";
            //info.accountname = "测试账套";
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
            for (int i = 1; i < 3; i++)
            {
                ServerInfo info = new ServerInfo();
                info.accountid = "test"+i.ToString ();
                info.accountname = "测试账套" + i.ToString();

                data.Add(info);
            }
            return data;
        }
    }
}
