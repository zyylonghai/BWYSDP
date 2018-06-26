using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;
using SDPCRL.CORE;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class DataSourceControl : UserControl
    {
        //public DefTBProperty _defTBProperty;
        private DataSourceProperty _dsProperty;
        private DefTBProperty _defTBProperty;
        private LibTreeNode _funNode;
        public DataSourceControl(LibTreeNode funcNode)
        {
            this._funNode = funcNode;
            InitializeComponent();
            _dsProperty = new DataSourceProperty();
            this.splitContainer1.Panel2.Controls.Add(_dsProperty);
        }

        private void DataSourceControl_Load(object sender, EventArgs e)
        {
            //数据集节点
            LibTreeNode dsNode = new LibTreeNode();
            dsNode.Name =_funNode.Name;
            dsNode.Text = ReSourceManage.GetResource(NodeType.DefDataSet);
            dsNode.NodeType = NodeType.DefDataSet;

            //自定义表节点
            LibTreeNode defTB = new LibTreeNode();
            defTB.Name = string.Format("deftb_{0}", _funNode.Name);
            defTB.Text = _funNode.Text;
            defTB.NodeType = NodeType.DefindTable;
            dsNode.Nodes.Add(defTB);

            //数据结构表
            LibTreeNode tablestruc = new LibTreeNode();
            tablestruc.Name = string.Format("tablestruct_{0}", _funNode.Name);
            tablestruc.Text = _funNode.Name;
            tablestruc.NodeType = NodeType.TableStruct;
            defTB.Nodes.Add(tablestruc);

            this.treeView1.Nodes.Add(dsNode);
            this.treeView1.SelectedNode = dsNode;

 
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LibTreeNode libnode = (LibTreeNode)e.Node;
            if (libnode == null) return;
            switch (libnode.NodeType)
            {
                case NodeType.DefDataSet :
                    if (this._dsProperty == null)
                    {
                        _dsProperty = new DataSourceProperty();
                        this.splitContainer1.Panel2.Controls.Add(_dsProperty);
                    }
                    SetPanel2ControlsVisible(_dsProperty);
                    LibDataSource ds = ModelDesignProject.GetDataSourceById(_funNode.Name);
                    ds.DataSourceName = string.IsNullOrEmpty(ds.DataSourceName) ? _funNode.Text : ds.DataSourceName;
                    ds.Package = string.IsNullOrEmpty(ds.Package) ? _funNode.Package : ds.Package;
                    _dsProperty.SetPropertyValue(ds, libnode);
                    break;
                case NodeType.DefindTable :
                    break;
                case NodeType.TableStruct :
                    break;
                case NodeType.Field :
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


    }
}
