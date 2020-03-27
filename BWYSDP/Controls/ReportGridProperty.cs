using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager.Reports;
using BWYSDP.com;
using SDPCRL.CORE;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class ReportGridProperty : BaseUserControl<LibReportGrid>
    {
        private LibTreeNode _Node;
        public ReportGridProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        public ReportGridProperty(string name)
             : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibReportGrid entity, LibTreeNode node)
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
            if (string.Compare(ctrNm, "rptGrid_DSID") == 0) //选择数据源ID
            {
                string[] dsarray = ModelManager.GetAllDataSourceNm(string.Empty);
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
                    if (this.Controls[ctrNm].Text.Trim() != listBox.SelectedItem.ToString())
                    {
                        this.Controls[ctrNm].Text = listBox.SelectedItem.ToString();
                        this.entity.DSID = listBox.SelectedItem.ToString();
                    }
                }
            }
        }
    }
}
