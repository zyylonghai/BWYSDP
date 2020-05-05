using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager.Trans;
using BWYSDP.com;
using SDPCRL.CORE;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class TransProperty : BaseUserControl<LibTransSource>
    {
        private LibTreeNode _Node;
        public TransProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
        public TransProperty(string name)
      : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibTransSource entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }

        public override void TextAndBotton_Click(object sender, EventArgs e)
        {
            base.TextAndBotton_Click(sender, e);
            Control ctl = sender as Control;
            string ctrNm = ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "");
            Panel p = new Panel();
            p.Dock = DockStyle.Fill;
            p.Name = "pfieldcollection";
            p.AutoScroll = true;
            ListBox listBox = new ListBox();
            listBox.Dock = DockStyle.Fill;
            p.Controls.Add(listBox);
            if (string.Compare(ctrNm, "tran_SrcProgId") == 0 || string.Compare(ctrNm, "tran_TargetProgId") == 0)//来源单ProgId
            {
                string[] progarray = ModelManager.GetAllProgId(string.Empty);
                if (progarray != null && progarray.Length > 0)
                {
                    foreach (string item in progarray)
                    {
                        listBox.Items.Add(item);
                    }
                }
                FieldCollectionForm fielsform = new FieldCollectionForm(p);
                DialogResult dialog = fielsform.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {
                    if (listBox.SelectedItem == null) return;
                    if (this.Controls[ctrNm].Text.Trim() != listBox.SelectedItem.ToString())
                    {
                        this.Controls[ctrNm].Text = listBox.SelectedItem.ToString();
                        if (ctrNm == "tran_SrcProgId")
                            this.entity.SrcProgId = listBox.SelectedItem.ToString();
                        else 
                            this.entity .TargetProgId = listBox.SelectedItem.ToString();
                    }
                }
            }
        }
    }
}
