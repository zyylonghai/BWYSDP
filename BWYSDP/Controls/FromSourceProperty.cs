using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager;
using BWYSDP.com;
using SDPCRL.CORE;

namespace BWYSDP.Controls
{
    public partial class FromSourceProperty : BaseUserControl<LibFromSourceField>
    {
        public FromSourceProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
        public FromSourceProperty(string name)
            : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibFromSourceField entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
        }

        public override void TextAndBotton_Click(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            string ctrNm = ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "");
            if (string.Compare(ctrNm, "fsfield_FromDataSource") == 0)//来源数据源
            {
                string[] dsarray = ModelManager.GetAllDataSourceNm(string.Empty);
                Panel p = new Panel();
                p.Dock = DockStyle.Fill;
                p.Name = "pfieldcollection";
                p.AutoScroll = true;
                ListBox listBox = new ListBox();
                listBox.Dock = DockStyle.Fill;
                p.Controls.Add(listBox);
                if (dsarray != null && dsarray.Length > 0)
                {
                    foreach (string item in dsarray)
                    {
                        listBox.Items.Add(item);
                    }
                }
                FieldCollectionForm fielsform = new FieldCollectionForm(p);
                DialogResult dialog = fielsform.ShowDialog(this);
                if (dialog == DialogResult.OK)
                {

                }
            }
            else if (string.Compare(ctrNm, "fsfield_FromDefindTableNm") == 0)//来源自定义表名
            {

            }
            else if (string.Compare(ctrNm, "fsfield_FromStructTableNm") == 0)//来源数据表名
            {

            }
            else if (string.Compare(ctrNm, "fsfield_FromFieldNm") == 0)//来源字段
            {

            }
        }
    }
}
