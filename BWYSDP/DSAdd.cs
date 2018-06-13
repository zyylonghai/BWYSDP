using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.CORE;

namespace BWYSDP
{
    public partial class DSAdd : LibFormBase
    {
        private bool _isOk = false;
        public DSAdd()
        {
            InitializeComponent();
            InitialzeValue();
        }

        public void InitialzeValue()
        {
            this.txtDSPackage.Text = "com";
        }

        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
            if (string.Compare(tag, "DSAdd", true) == 0)
            {
 
            }
        }

        protected override void ReturnParam(ref string tag, Dictionary<object, object> param)
        {
            base.ReturnParam(ref tag, param);
            if (_isOk)
            {
                tag = "DSInfo";
                param.Add("DSID", this.txtDSID.Text.Trim());
                param.Add("DSName", this.txtDSName.Text.Trim());
                param.Add("DSDisplayText", this.txtDSDisplayText.Text.Trim());
                param.Add("DSPackage", this.txtDSPackage.Text.Trim());
            }

        }

        private void btComfirm_Click(object sender, EventArgs e)
        {
            string dsName = this.txtDSName.Text.Trim();
            string dsDisplaytext = this.txtDSDisplayText.Text.Trim();
            if (string.IsNullOrEmpty(dsName))
            {
                MessageBox.Show("请填写数据源名称");
                return;
            }
            if (string.IsNullOrEmpty(this.txtDSPackage.Text.Trim()))
            {
                DialogResult dialogres = MessageBox.Show("所属包未填，默认为com?", "温馨提示", MessageBoxButtons.OKCancel);
                if (dialogres != DialogResult.OK)
                {
                    return;
                }
            }
            InvalidateValue();
            _isOk = true;
            this.Close();
        }

        /// <summary>验证数据有效性</summary>
        private void InvalidateValue()
        {
            if (ModelDesignProject.ExistId(LibSysUtils.ToInt32(this.txtDSID.Text.Trim())))
            {
                ExceptionManager.ThrowError("该DSID已存在");
                //return;
            }
            if (ModelDesignProject.ExistDSName(this.txtDSName.Text.Trim()))
            {
                ExceptionManager.ThrowError("该数据源名称已经存在");
            }

        }

        private void btCance_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
