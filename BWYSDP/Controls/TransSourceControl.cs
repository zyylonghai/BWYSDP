using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.COM.ModelManager.Trans;
using SDPCRL.CORE;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class TransSourceControl : UserControl
    {
        private LibTreeNode _funNode;
        private LibTransSource _Trans;
        private List<TransFieldProperty> _transFieldlst = null;

        TransProperty _tranproperty = null;
        public TransSourceControl()
        {
            InitializeComponent();
        }

        public TransSourceControl(LibTreeNode funcNode)
            : this()
        {
            this._funNode = funcNode;
            _tranproperty = new TransProperty();
            _transFieldlst = new List<TransFieldProperty>();
            _tranproperty.Dock = DockStyle.Fill;

            this.splitContainer1.Panel2.Controls.Add(_tranproperty);
        }

        public void GetControlValueBindToRpt()
        {
            _tranproperty.GetControlsValue();
            foreach (TransFieldProperty tranfield in _transFieldlst)
            {
                tranfield.GetControlsValue();
            }
        }

        private void TransSourceControl_Load(object sender, EventArgs e)
        {
            _Trans = ModelDesignProject.GetLibTransSourceById(_funNode.Name);
            //_tranproperty = new TransProperty();

            LibTreeNode transNode = new LibTreeNode();
            transNode.Name = _funNode.Name;
            transNode.Text = ReSourceManage.GetResource(NodeType.TransSetting);
            transNode.NodeType = NodeType.TransSetting;
            transNode.Tag = _Trans;
            if (_Trans.TransFields != null)
            {
                foreach (LibTransFieldMap field in _Trans.TransFields)
                {
                    #region 创建转单字段节点
                    LibTreeNode transfield = new LibTreeNode();
                    transfield.NodeType = NodeType.TransField;
                    transfield.NodeId = field.ID;
                    transfield.Name = field.SrcFieldNm;
                    transfield.Text = field.SrcFieldNm;
                    transNode.Nodes.Add(transfield);
                    #endregion
                }
            }
            this.treeView1.Nodes.Add(transNode);
            this.treeView1.SelectedNode = transNode;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LibTreeNode libnode = (LibTreeNode)e.Node;
            if (libnode == null) return;
            bool exists = false;
            switch (libnode.NodeType)
            {
                case NodeType.TransSetting:
                    if (this._tranproperty == null)
                    {
                        _tranproperty = new TransProperty();
                        this.splitContainer1.Panel2.Controls.Add(_tranproperty);
                    }
                    SetPanel2ControlsVisible(_tranproperty);

                    _Trans.TransName = string.IsNullOrEmpty(_Trans.TransName) ? _funNode.Text : _Trans.TransName;
                    _Trans.Package = string.IsNullOrEmpty(_Trans.Package) ? _funNode.Package : _Trans.Package;
                    //if (ModelDesignProject.ExitsDataSource(_fm.FormId, _fm.Package))
                    //{
                    //    _fm.DSID = _fm.FormId;
                    //}
                    _tranproperty.SetPropertyValue(_Trans, libnode);
                    break;
                case NodeType.TransField:
                    if (_transFieldlst != null)
                    {
                        foreach (TransFieldProperty item in _transFieldlst)
                        {
                            if (string.Compare(item.Name, libnode.NodeId) == 0)
                            {
                                SetPanel2ControlsVisible(item);
                                exists = true;
                                break;
                            }
                        }
                        if (!exists) //还未创建对应的控件
                        {
                            TransFieldProperty transfdp = new TransFieldProperty(libnode.NodeId);
                            transfdp.Dock = DockStyle.Fill;
                            this._transFieldlst.Add(transfdp);
                            this.splitContainer1.Panel2.Controls.Add(transfdp);
                            //LibReportGrid librptgd = _rpt.GridGroups.FindFirst("GridGroupID", ((LibTreeNode)libnode.Parent).NodeId);
                            transfdp.SetPropertyValue(_Trans.TransFields .FindFirst("ID", libnode.NodeId), libnode);

                            SetPanel2ControlsVisible(transfdp);
                        }
                    }
                    break;
            }
        }


        private void SetPanel2ControlsVisible(Control ctl)
        {
            foreach (Control item in this.splitContainer1.Panel2.Controls)
            {
                item.Visible = item == ctl ? true : false;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                LibTreeNode libnode = (LibTreeNode)e.Node;
                if (libnode.NodeType == NodeType.TransSetting)
                {
                    libnode.ContextMenuStrip = this.contextMenuStrip1;
                }
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            LibTreeNode curentNode = (LibTreeNode)this.treeView1.SelectedNode;
            switch (e.ClickedItem.Name)
            {
                case "andTransField": //添加转单字段
                    if (this._Trans.TransFields == null) _Trans.TransFields = new LibCollection<LibTransFieldMap>();
                    var existfields = _Trans.TransFields.Tolist();
                    if (string.IsNullOrEmpty(_Trans.SrcProgId))
                    {
                        ExceptionManager.ThrowError("来源单progid 必填");
                        //MessageHandle.ShowMessage("来源单progid 必填",false);
                    }
                    var formpage= ModelDesignProject.GetFormSourceByFormId(_Trans.SrcProgId);
                    LibDataSource ds = ModelDesignProject.GetDataSourceById(formpage.DSID);
                    Panel p = new Panel();
                    p.Dock = DockStyle.Fill;
                    p.Name = "pfieldcollection";
                    p.AutoScroll = true;
                    TreeView tree = new TreeView();
                    //tree.AfterCheck += new TreeViewEventHandler(Gridtree_AfterCheck);
                    tree.CheckBoxes = true;
                    tree.Dock = DockStyle.Fill;
                    p.Controls.Add(tree);
                    LibTreeNode _node;
                    if (ds.DefTables != null)
                    {
                        List<ExistField> exists = new List<ExistField>();
                        foreach (var item in existfields)
                        {
                            exists.Add(new ExistField { Name = item.SrcFieldNm, FromTableNm = item.SrcTableNm });
                        }
                        DSUtiles.GetAllFieldsByDS(ds, tree, exists);
                    }
                    FieldCollectionForm fielsform = new FieldCollectionForm(p);
                    DialogResult dialog = fielsform.ShowDialog(this);
                    if (dialog == DialogResult.OK)
                    {
                        curentNode.Nodes.Clear();
                        if (_Trans.TransFields != null && _Trans.TransFields.Count > 0)
                            _Trans.TransFields.RemoveAll();
                        foreach (LibTreeNode deftb in tree.Nodes)
                        {
                            foreach (LibTreeNode tbstruct in deftb.Nodes)
                            {
                                foreach (LibTreeNode f in tbstruct.Nodes)
                                {

                                    if (!f.Checked) continue;
                                    #region 添加节点
                                    //树节点

                                    LibTreeNode fieldNode = new LibTreeNode();
                                    fieldNode.NodeId = Guid.NewGuid().ToString();
                                    fieldNode.NodeType = NodeType.TransField;
                                    fieldNode.Name = f.Name;
                                    fieldNode.Text = fieldNode.Name;
                                    curentNode.Nodes.Add(fieldNode);

                                    //控件属性
                                    TransFieldProperty fieldProperty = new TransFieldProperty(fieldNode.NodeId);
                                    _transFieldlst.Add(fieldProperty);
                                    fieldProperty.Dock = DockStyle.Fill;
                                    this.splitContainer1.Panel2.Controls.Add(fieldProperty);

                                    //对应实体
                                    LibTransFieldMap libtransfield = new LibTransFieldMap();
                                    libtransfield.ID = fieldNode.NodeId;
                                    libtransfield.SrcFieldNm  = f.Name;
                                    libtransfield.SrcTableNm = (bool)f.Tag ? tbstruct.Name : string.Empty;
                                    //libtransfield.FromDefTableNm = (bool)f.Tag ? deftb.Name : string.Empty;
                                    libtransfield.SrcTableIndex = Convert.ToInt32(tbstruct.NodeId);
                                    libtransfield.SrcFieldDisplay  = f.Text;
                                    //libtransfield.Isdefine = !(bool)f.Tag;

                                    _Trans.TransFields.Add(libtransfield);

                                    fieldProperty.SetPropertyValue(libtransfield, fieldNode);

                                    #endregion
                                }
                            }
                        }
                        UpdateTabPageText();
                    }
                    break;
            }
        }
        /// <summary>
        /// 设置tabpage的标题后都一个*。表示已被修改
        /// </summary>
        private void UpdateTabPageText()
        {
            TabPage page = (TabPage)this.Parent;
            if (!page.Text.Contains(SysConstManage.Asterisk))
            {
                page.Text += SysConstManage.Asterisk;
            }
        }
    }
}
