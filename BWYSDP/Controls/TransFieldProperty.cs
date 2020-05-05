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
    public partial class TransFieldProperty : BaseUserControl<LibTransFieldMap>
    {
        private LibTreeNode _Node=null;
        private LibTreeNode  oldnode = null;
        public TransFieldProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        public TransFieldProperty(string name)
           : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibTransFieldMap entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }

        public override void TextAndBotton_Click(object sender, EventArgs e)
        {
            base.TextAndBotton_Click(sender, e);
            Control ctl = sender as Control;
            string ctrNm = ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "");
            if (_Node != null)
            {
                var pnode = _Node.Parent as TreeNode;
                LibTransSource transSource = pnode.Tag as LibTransSource;
                if (string.Compare(ctrNm, "tranfld_TargetFieldNm") == 0)
                {
                    if (string.IsNullOrEmpty(transSource.TargetProgId))
                    {
                        ExceptionManager.ThrowError("目标单progid 必填");
                        //MessageHandle.ShowMessage("来源单progid 必填",false);
                    }
                    var formpage = ModelDesignProject.GetFormSourceByFormId(transSource.TargetProgId);
                    LibDataSource ds = ModelDesignProject.GetDataSourceById(formpage.DSID);
                    Panel p = new Panel();
                    p.Dock = DockStyle.Fill;
                    p.Name = "pfieldcollection";
                    p.AutoScroll = true;
                    TreeView tree = new TreeView();
                    tree.AfterCheck += new TreeViewEventHandler(tree_AfterCheck);
                    tree.CheckBoxes = true;
                    tree.Dock = DockStyle.Fill;
                    p.Controls.Add(tree);
                    //LibTreeNode _node;
                    if (ds.DefTables != null)
                    {
                        List<ExistField> exists = new List<ExistField>();
                        exists.Add(new ExistField { Name = this.entity .TargetFieldNm, FromTableNm = this.entity .TargetTableNm});
                        DSUtiles.GetAllFieldsByDS(ds, tree, exists);
                    }

                    FieldCollectionForm fielsform = new FieldCollectionForm(p);
                    DialogResult dialog = fielsform.ShowDialog(this);
                    if (dialog == DialogResult.OK)
                    {
                        foreach (LibTreeNode deftb in tree.Nodes)
                        {
                            foreach (LibTreeNode tbstruct in deftb.Nodes)
                            {
                                foreach (LibTreeNode f in tbstruct.Nodes)
                                {
                                    if (!f.Checked) continue;
                                    this.entity.TargetFieldNm = f.Name;
                                    this.entity.TargetFieldDisplay = f.Text;
                                    this.entity .TargetTableNm = (bool)f.Tag ? tbstruct.Name : string.Empty;
                                    this.entity .TargetTableIndex = Convert.ToInt32(tbstruct.NodeId);
                                    break;
                                }
                            }
                        }
                        this.SetPropertyValue(this.entity, _Node);
                        UpdateTabPageText();
                    }
                }
            }
        }

        public void tree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            LibTreeNode node = (LibTreeNode)e.Node;
            if (oldnode != null && oldnode .Checked) oldnode.Checked = false;
            oldnode = node;
        }

        /// <summary>
        /// 设置tabpage的标题后都一个*。表示已被修改
        /// </summary>
        private void UpdateTabPageText()
        {
            TabPage page = (TabPage)this.Parent.Parent .Parent.Parent;
            if (!page.Text.Contains(SysConstManage.Asterisk))
            {
                page.Text += SysConstManage.Asterisk;
            }
        }
    }
}
