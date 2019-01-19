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
using BWYSDP.BLL;
using BWYSDP.Controls;
using SDPCRL.CORE;

namespace BWYSDP
{
    public partial class DefineTableControl : UserControl
    {
        private int _dsId;
        private DataSource _currentDS;
        private static int _number = 1;
        public DefineTableControl(int dsId)
        {
            this._dsId = dsId;
            InitializeComponent();
            InitializeControl();
        }

        private void InitializeControl()
        {
            _currentDS = ModelDesignProject.GetDataSource(_dsId);
            if (_currentDS.DefTables != null)
            {
                foreach (DefineTable defTB in _currentDS.DefTables)
                {
                    TreeNode nodeDefTable = new TreeNode();
                    SetDefineTableNode(defTB, nodeDefTable);
                    TreeNode nodeDTStruct;
                    TreeNode nodeField;
                    foreach (DataTableStruct dtStruct in defTB.TableStruct)
                    {
                        nodeDTStruct = new TreeNode();
                        SetTableStructNode(dtStruct, nodeDTStruct);
                        foreach (LibField field in dtStruct.Fields)
                        {
                            nodeField = new TreeNode();
                            SetFieldNode(field, nodeField);
                            nodeDTStruct.Nodes.Add(nodeField);
                        }
                        nodeDefTable.Nodes.Add(nodeDTStruct);
                    }
                    this.treeView1.Nodes["defineTableCollection"].Nodes.Add(nodeDefTable);
                }
            }
            else
            {
                _currentDS.DefTables = new SDPCRL.CORE.LibCollection<DefineTable>();
                TreeNode nodeDefTable = new TreeNode();

                DefineTable defTable = new DefineTable();
                defTable.ID = DataSourceInfoBLL.GetMaxDefTBID(_dsId) + 1;
                defTable.TableName = "defineTable1";
                defTable.DisplayName = "自定义数据表";
                SetDefineTableNode(defTable, nodeDefTable);
                _currentDS.DefTables.Add(defTable);

                defTable.TableStruct = new SDPCRL.CORE.LibCollection<DataTableStruct>();
                TreeNode tableStruct = new TreeNode();
                nodeDefTable.Nodes.Add(tableStruct);

                DataTableStruct dbStruct = new DataTableStruct();
                dbStruct.Name = "dataTableStruct1";
                dbStruct.DisplayName = "数据表结构";
                SetTableStructNode(dbStruct, tableStruct);
                defTable.TableStruct.Add(dbStruct);
                this.treeView1.Nodes["defineTableCollection"].Nodes.Add(nodeDefTable);
                this.treeView1.SelectedNode = tableStruct; ;
                nodeDefTable.Expand();
            }
            ModelDesignProject.CreateDS(_currentDS);
        }

        /// <summary>设置自定义数据表节点属性</summary>
        /// <param name="defTable"></param>
        /// <param name="node"></param>
        private void SetDefineTableNode(DefineTable defTable, TreeNode node)
        {
            node.Name = defTable.TableName;
            node.Text = defTable.DisplayName;
            node.ContextMenuStrip = this.contextMenuStrip_defTB;
        }

        /// <summary>设置数据表结构节点属性</summary>
        /// <param name="dtStruct"></param>
        /// <param name="node"></param>
        private void SetTableStructNode(DataTableStruct dtStruct, TreeNode node)
        {
            node.Name = dtStruct.Name;
            node.Text = dtStruct.DisplayName;
            node.ContextMenuStrip = this.contextMenuStrip_TBStruct;
        }

        private void SetFieldNode(LibField field, TreeNode node)
        {
            node.Name = field.Name;
            node.Text = field.DisplayName;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNodeSelected(e.Node);
        }

        private DataTableStruct GetDTStruct(string name)
        {
            DataTableStruct dtStruct = null;
            bool isFind = false;
            foreach (DefineTable deftb in _currentDS.DefTables)
            {
                foreach (DataTableStruct item in deftb.TableStruct)
                {
                    if (string.Compare(item.Name, name, true) == 0)
                    {
                        dtStruct = item;
                        isFind = true;
                        break;
                    }
                }
                if (isFind)
                    break;
            }
            return dtStruct;
        }

        /// <summary>获取字段实体对象</summary>
        /// <param name="tableStructName">表名</param>
        ///  <param name="fieldName">字段名</param>
        /// <returns></returns>
        private LibField GetField(string tableStructName, string fieldName)
        {
            LibField field = null;
            DataTableStruct tableStruct = GetDTStruct(tableStructName);
            if (tableStruct != null)
            {
                foreach (LibField item in tableStruct.Fields)
                {
                    if (string.Compare(item.Name, fieldName, true) == 0)
                    {
                        field = item;
                    }
                }
            }
            #region
            //foreach (DefineTable deftb in _currentDS.DefTables)
            //{
            //    foreach (DataTableStruct tablestruct in deftb.TableStruct)
            //    {
            //        foreach (LibField item in tablestruct.Fields)
            //        {
            //            if (string.Compare(item.Name, fieldName, true) == 0)
            //            {
            //                field = item;
            //                isFind = true;
            //                break;
            //            }
            //        }
            //        if (isFind)
            //            break;
            //    }
            //    if (isFind)
            //        break;
            //}
            #endregion
            return field;
        }

        private DefineTable GetDefTabale(string name)
        {
            DefineTable deftable = null;
            foreach (DefineTable deftb in _currentDS.DefTables)
            {
                if (string.Compare(deftb.TableName, name, true) == 0)
                {
                    deftable = deftb;
                }
            }
            return deftable;
        }

        /// <summary>新建字段</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_fieldAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            _number++;
            TreeNode fieldNode = new TreeNode("字段" + _number.ToString());
            fieldNode.Name = "field" + _number.ToString();
            node.Nodes.Add(fieldNode);
            fieldNode.ContextMenuStrip = contextMenuStrip_field;
            this.treeView1.SelectedNode = fieldNode;

            LibField field = new LibField();
            field.Name = fieldNode.Name;
            field.DisplayName = "";
            field.AliasName = string.Empty;
            DataTableStruct tableStruct = GetDTStruct(node.Name);
            if (tableStruct.Fields == null)
                tableStruct.Fields = new LibCollection<LibField>();
            tableStruct.Fields.Add(field);

            TreeNodeSelected(fieldNode);

        }

        /// <summary></summary>
        /// <param name="node"></param>
        private void TreeNodeSelected(TreeNode node)
        {
            if (node.Level == 1) //根节点 即自定义表节点
            {
                this.tbStructProperty1.Visible = false;
                this.defFieldProperty1.Visible = false;
                this.defTBProperty1.Visible = true;
                //this.defTBProperty1.SetPropertyValue(GetDefTabale(node.Name), node);
            }
            else if (node.Level == 2) //第二级节点 即表结构 节点
            {
                this.defTBProperty1.Visible = false;
                this.defFieldProperty1.Visible = false;
                this.tbStructProperty1.Visible = true;
                //this.tbStructProperty1.SetPropertyValue(GetDTStruct(node.Name), node);

            }
            else if (node.Level == 3)//第三级节点，即字段属性
            {
                this.tbStructProperty1.Visible = false;
                this.defTBProperty1.Visible = false;
                this.defFieldProperty1.Visible = true;
                string tableStructName = node.Parent.Name;
                this.defFieldProperty1.SetPropertyValue(GetField(tableStructName, node.Name),(LibTreeNode)node);

            }
        }
    }
}
