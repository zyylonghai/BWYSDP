using BWYSDP.com;
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
    public partial class CreateFunc : LibFormBase
    {
        private bool isok = false;
        public CreateFunc()
        {
            InitializeComponent();
        }

        //protected override void DoSetParam(string tag, params object[] param)
        //{
        //    base.DoSetParam(tag, param);
        //    if (string.Compare(tag, "CreateFunc") == 0)
        //    {

        //    }
        //}
        protected override void ReturnParam(ref string tag, Dictionary<object, object> param)
        {
            base.ReturnParam(ref tag, param);
            if (isok)
            {
                tag = "NewFunc";
                LibTreeNode node = new LibTreeNode();
                foreach (Control ctr in this.panel1.Controls)
                {
                    RadioButton radiobt = (RadioButton)ctr;
                    if (radiobt != null && radiobt.Checked)
                    {
                        node.NodeType = (NodeType)(Convert.ToInt16(radiobt.Tag));
                    }
                }
                node.Name = this.txtbFuncID.Text.Trim();
                node.OriginalName = node.Name;
                node.Text = this.txtbFuncNm.Text.Trim();
                node.Package = this.txtbPackage.Text.Trim();
                param.Add("funcNode", node);
            }
        }

        private void btnsure_Click(object sender, EventArgs e)
        {
            isok = true;
            this.Close();
        }
    }
}
