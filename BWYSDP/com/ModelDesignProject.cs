using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.COM.ModelManager;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using SDPCRL.CORE;
using SDPCRL.COM;
using SDPCRL.CORE.FileUtils;
using System.Data;
using SDPCRL.COM.ModelManager.FormTemplate;
using BWYSDP.Controls;
using static System.Windows.Forms.Control;

namespace BWYSDP.com
{
    /// <summary>模型设计工厂类</summary>
    public class ModelDesignProject
    {
        private static Hashtable _dataSourceContain = Hashtable.Synchronized(new Hashtable());
        private static Hashtable _formSourceContain = new Hashtable();
        private static Hashtable _permissionSourceContain = new Hashtable();
        private static Hashtable _keyvaluesContain = new Hashtable();
        private static DSList _dsList = new DSList();
        private static bool initialvale = false;

        public delegate void ModelEditEventHandle(object sender, bool ischange);
        public static event ModelEditEventHandle DoModelEdit;

        #region 公开函数

        #region 旧代码
        /// <summary>创建数据源</summary>
        /// <param name="dsId"></param>
        public static void CreateDS(DataSource dataSource)
        {
            if (!_dataSourceContain.ContainsValue(dataSource))
            {
                //DataSource ds = new DataSource();
                //ds.DSID = dsId;
                _dataSourceContain.Add(dataSource.DSID, dataSource);
            }
        }

        /// <summary>根据DSName获取数据源</summary>
        /// <param name="dsName">数据源名称</param>
        /// <returns></returns>
        public static DataSource GetDataSource(string dsName)
        {
            foreach (DataSource item in _dataSourceContain.Values)
            {
                if (string.Compare(item.DataSourceName, dsName, true) == 0)
                {
                    return item;
                }
            }
            return DoGetDSFromXML(dsName);
        }

        /// <summary>当前DSID是否存在</summary>
        /// <param name="dsId"></param>
        /// <returns></returns>
        public static bool ExistId(int dsId)
        {
            foreach (int key in _dataSourceContain.Keys)
            {
                if (key == dsId)
                    return true;
            }
            return DoExistId(dsId);
        }

        /// <summary>当前数据源名称是否存在</summary>
        /// <param name="dataSourceName"></param>
        /// <returns></returns>
        public static bool ExistDSName(string dataSourceName)
        {
            foreach (DataSource item in _dataSourceContain.Values)
            {
                if (string.Compare(item.DataSourceName, dataSourceName, true) == 0)
                {
                    return true;
                }
            }
            return DoExistDSName(dataSourceName);
        }

        /// <summary>获取数据源信息列表</summary>
        /// <returns></returns>
        public static List<DataSource> GetDataSourceList()
        {
            return DoGetDataSourceList();
        }

        /// <summary>根据DSID获取数据源</summary>
        /// <param name="dsId"></param>
        /// <returns></returns>
        public static DataSource GetDataSource(int dsId)
        {
            return _dataSourceContain.ContainsKey(dsId) ? _dataSourceContain[dsId] as DataSource : DoGetDSFromXML(dsId);
        }

        public static void DoSaveDS()
        {

        }
        public static void DoSaveDS(string dataSourceName)
        {
            foreach (DataSource item in _dataSourceContain.Values)
            {
                if (string.Compare(item.DataSourceName, dataSourceName, true) == 0)
                {
                    SaveDataSource(item);
                }
            }
        }

        /// <summary>保存某个数据源</summary>
        /// <param name="dsId"></param>
        public static void DoSaveDS(int dsId)
        {
            if (_dataSourceContain.ContainsKey(dsId))
            {
                DataSource ds = (DataSource)_dataSourceContain[dsId];
                SaveDataSource(ds);
                //SerializerUtils.xmlserialzaition(ds, string.Format("{0}\\{1}\\{2}.xml", SysConstManage.DSFileRootPath, ds.Package, ds.DataSourceName));
            }
        }
        /// <summary>创建数据表对象</summary>
        /// <param name="ds"></param>
        public static void CreateTableObj(DataSource ds)
        {
            DBModelOperation dbModelOperation = new DBModelOperation();
            dbModelOperation.CreateDataSourceObj(ds);
        }

        /// <summary>创建数据表对象</summary>
        /// <param name="dsId"></param>
        public static void CreateTableObj(int dsId)
        {

        }
        #endregion


        /// <summary>根据控件类型，设置控件的值</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controls"></param>
        /// <param name="valueType"></param>
        public static void DoSetPropertyValue<T>(Control.ControlCollection controls, T valueType)
        {
            initialvale = true;
            PropertyInfo[] propertis = valueType.GetType().GetProperties();
            foreach (Control item in controls)
            {
                foreach (PropertyInfo info in propertis)
                {
                    object[] attrArray = info.GetCustomAttributes(typeof(LibAttributeAttribute), true);
                    if (attrArray.Length > 0)
                    {
                        LibAttributeAttribute attr = attrArray[0] as LibAttributeAttribute;
                        if (string.Compare(attr.ControlNm, item.Name, false) == 0)
                        {
                            switch (attr.ControlType)
                            {
                                case LibControlType.Combox:
                                    if (info.PropertyType.Equals(typeof(bool)))
                                    {
                                        item.Text = LibSysUtils.BooleanToText((bool)info.GetValue(valueType, null));
                                    }
                                    else
                                    {
                                        item.Text = ReSourceManage.GetResource(info.GetValue(valueType, null));
                                    }
                                    break;
                                case LibControlType.TextAndBotton:
                                    if (info.PropertyType.Equals(typeof(string)))
                                    {
                                        item.Text = LibSysUtils.ToString(info.GetValue(valueType, null));
                                        break;
                                    }
                                    object obj = info.GetValue(valueType, null);
                                    ICollection lst = obj as ICollection;
                                    if (lst != null)
                                    {
                                        foreach (var n in lst)
                                        {
                                            if (!string.IsNullOrEmpty(item.Text))
                                            {
                                                item.Text += SysConstManage.Comma;
                                            }
                                            item.Text += n.ToString();
                                        }
                                    }
                                    else
                                    {
                                        if (obj != null)
                                            item.Text = info.GetValue(valueType, null).ToString();
                                    }
                                    break;
                                case LibControlType.TextBox:
                                    item.Text = LibSysUtils.ToString(info.GetValue(valueType, null));
                                    break;
                            }
                        }
                    }
                }
            }
            initialvale = false;
        }

        /// <summary>获取控件值，并赋值到相应对象（T）</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controls"></param>
        /// <param name="obj"></param>
        public static void DoGetControlsValue<T>(Control.ControlCollection controls, T obj)
        {
            PropertyInfo[] propertis = obj.GetType().GetProperties();
            Control ctrl;
            foreach (PropertyInfo p in propertis)
            {
                object[] attrArray = p.GetCustomAttributes(typeof(LibAttributeAttribute), true);
                if (attrArray.Length > 0)
                {
                    LibAttributeAttribute attr = attrArray[0] as LibAttributeAttribute;
                    if (controls.ContainsKey(attr.ControlNm))
                    {
                        ctrl = controls[attr.ControlNm];
                        switch (attr.ControlType)
                        {
                            case LibControlType.TextBox:
                                if (p.GetSetMethod() != null)
                                {
                                    if (p.PropertyType.Equals(typeof(int)) || p.PropertyType.Equals(typeof(decimal)))
                                    {
                                        int valu;
                                        if (LibSysUtils.isNumberic(ctrl.Text.Trim(), out valu))
                                            p.SetValue(obj, valu, null);
                                        else
                                        {
                                            ExceptionManager.ThrowError(string.Format("{0}只能输入数字", attr.DisplayText));
                                        }
                                    }
                                    //else if (p.PropertyType.Equals(typeof(decimal)))
                                    //{
                                    //    p.SetValue(obj, ctrl.Text.Trim(), null);
                                    //}
                                    else
                                        p.SetValue(obj, ctrl.Text.Trim(), null);
                                }
                                break;
                            case LibControlType.Combox:
                                if (p.PropertyType.Equals(typeof(bool)))
                                {
                                    p.SetValue(obj, LibSysUtils.ToBooLean(ctrl.Text.Trim()), null);
                                }
                                else
                                {
                                    LibItem itm = ((ComboBox)ctrl).SelectedItem as LibItem;
                                    p.SetValue(obj, itm.Key, null);
                                }
                                break;
                            case LibControlType.TextAndBotton:

                                break;
                        }
                    }
                }
            }
        }

        /// <summary>创建或修改数据库表结构</summary>
        public static void UpdateTableStruct(LibDataTableStruct obj)
        {
            if (obj != null && obj.Ignore)
            {
                StringBuilder builder = new StringBuilder();
                BLL.BllDataBase bll = new BLL.BllDataBase();
                DataTable result = (DataTable)bll.GetTableSchema(obj.Name);
                if (result != null && result.Rows.Count > 0)//表已存在
                {
                    foreach (LibField field in obj.Fields)
                    {
                        if (field.IsActive)
                        {
                            DataRow[] rows = result.Select(string.Format("column_name='{0}'", string.IsNullOrEmpty(field.OrignNm) ? field.Name : field.OrignNm));
                            if (rows.Length > 0)
                            {
                                if (string.Compare(rows[0]["column_name"].ToString(), field.OrignNm) == 0)//列名称改变。
                                {
                                    builder.AppendFormat(" exec sp_rename '{0}.{1}','{2}'", obj.Name, field.OrignNm, field.Name);

                                }
                                builder.AppendFormat(" alter table {0}  ALTER COLUMN {1} ", obj.Name, field.Name);

                            }
                            else
                            {
                                builder.AppendFormat(" alter table {0} add {1} ", obj.Name, field.Name);
                            }
                            switch (field.FieldType)
                            {
                                case LibFieldType.Byte:
                                    builder.AppendFormat("[{0}] ", "bit");
                                    break;
                                case LibFieldType.Date:
                                    builder.AppendFormat("[{0}] ", "date");
                                    break;
                                case LibFieldType.DateTime:
                                    builder.AppendFormat("[{0}] ", "datetime");
                                    break;
                                case LibFieldType.Decimal:
                                    builder.AppendFormat("[{0}]", "decimal");
                                    builder.AppendFormat("({0}, {1}) ", field.FieldLength, field.Decimalpoint);
                                    break;
                                case LibFieldType.Interger:
                                    builder.AppendFormat("[{0}] ", "int");
                                    break;
                                case LibFieldType.Long:
                                    builder.AppendFormat("[{0}] ", "bigint");
                                    break;
                                case LibFieldType.String:
                                    builder.AppendFormat("[{0}]", "nvarchar");
                                    builder.AppendFormat("({0}) ", field.FieldLength);
                                    break;
                                case LibFieldType.Text:
                                    builder.AppendFormat("[{0}]", "ntext");
                                    break;
                            }
                            builder.AppendFormat("{0} ", field.AllowNull ? "NULL" : "NOT NULL");
                        }
                    }
                    bll.BuilderTableStruct(builder.ToString());
                    return;
                }
                //else
                //{
                builder.AppendFormat("CREATE TABLE [dbo].[{0}]( ", obj.Name);
                int initlen = builder.Length;
                foreach (LibField field in obj.Fields)
                {
                    if (field.IsActive)
                    {
                        if (builder.Length != initlen)
                        {
                            builder.Append(", ");
                        }
                        builder.AppendFormat("[{0}] ", field.Name);
                        switch (field.FieldType)
                        {
                            case LibFieldType.Byte:
                                builder.AppendFormat("[{0}] ", "bit");
                                break;
                            case LibFieldType.Date:
                                builder.AppendFormat("[{0}] ", "date");
                                break;
                            case LibFieldType.DateTime:
                                builder.AppendFormat("[{0}] ", "datetime");
                                break;
                            case LibFieldType.Decimal:
                                builder.AppendFormat("[{0}]", "decimal");
                                builder.AppendFormat("({0}, {1}) ", field.FieldLength, field.Decimalpoint);
                                break;
                            case LibFieldType.Interger:
                                builder.AppendFormat("[{0}] ", "int");
                                break;
                            case LibFieldType.Long:
                                builder.AppendFormat("[{0}] ", "bigint");
                                break;
                            case LibFieldType.String:
                                builder.AppendFormat("[{0}]", "nvarchar");
                                builder.AppendFormat("({0}) ", field.FieldLength);
                                break;
                            case LibFieldType.Text:
                                builder.AppendFormat("[{0}]", "ntext");
                                break;
                        }
                        builder.AppendFormat("{0} ", field.AllowNull ? "NULL" : "NOT NULL");
                    }
                }
                //}
                if (obj.PrimaryKey != null && obj.PrimaryKey.Count > 0)
                {
                    builder.Append(", ");
                    builder.AppendFormat(" CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED (", obj.Name);
                    for (int i = 0; i < obj.PrimaryKey.Count; i++)
                    {
                        builder.AppendFormat("[{0}] ASC", obj.PrimaryKey[i]);
                        if (i != obj.PrimaryKey.Count - 1)
                        {
                            builder.Append(",");
                        }
                    }
                    builder.Append(")");
                    builder.Append("WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");
                }
                builder.Append(") ON [PRIMARY]");
                bll.BuilderTableStruct(builder.ToString());
            }
        }

        /// <summary>
        /// 根据T的属性 创建相应控件并绑定控件到容器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="UCtr"></param>
        public static void InternalBindControls<T>(UserControl UCtr)
        {
            PropertyInfo[] propertis = typeof(T).GetProperties();
            Label lb;
            TextBox tb;
            ComboBox comb;
            Button btn;
            ToolTip tip = new ToolTip();
            int x = 25;
            int y = 25;
            int interval = 30;
            int index = 0;
            int w = 300;
            int h = 21;
            int btnW = 35;
            System.Drawing.Font font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            foreach (PropertyInfo p in propertis)
            {
                object[] attrArray = p.GetCustomAttributes(typeof(LibAttributeAttribute), true);
                if (attrArray.Length > 0)
                {
                    LibAttributeAttribute attr = attrArray[0] as LibAttributeAttribute;
                    if (!attr.IsHidden)
                    {
                        lb = new Label();
                        lb.Font = font;
                        lb.Name = string.Format("lb_{0}", attr.ControlNm);
                        lb.Size = new System.Drawing.Size(85, 12);
                        lb.Text = string.Format("{0}:", attr.DisplayText);
                        lb.Location = new System.Drawing.Point(x, y + index * (interval + lb.Height));
                        UCtr.Controls.Add(lb);
                        switch (attr.ControlType)
                        {
                            case LibControlType.TextBox:
                                #region 文本输入框控件
                                tb = new TextBox();
                                tb.Multiline = true;
                                tb.Font = font;
                                tb.Name = attr.ControlNm;
                                tb.ReadOnly = attr.IsReadOnly;
                                tb.Size = new System.Drawing.Size(w, h);
                                tb.TabIndex = index;
                                tb.Multiline = false;
                                tb.Location = new System.Drawing.Point(lb.Location.X + lb.Width, y + index * (interval + lb.Height));
                                tb.TextChanged += new EventHandler(tb_TextChanged);
                                UCtr.Controls.Add(tb);
                                break;
                            #endregion
                            case LibControlType.Combox:
                                #region 下拉选项列表控件
                                comb = new ComboBox();
                                comb.Font = font;
                                comb.Name = attr.ControlNm;
                                comb.DropDownStyle = ComboBoxStyle.DropDownList;
                                comb.Size = new System.Drawing.Size(w, h);
                                comb.Enabled = !attr.IsReadOnly;
                                comb.Location = new System.Drawing.Point(lb.Location.X + lb.Width, y + index * (interval + lb.Height));
                                if (p.PropertyType.Equals(typeof(bool)))
                                {
                                    comb.Items.Add(new LibItem(1, LibSysUtils.BooleanToText(true)));
                                    comb.Items.Add(new LibItem(0, LibSysUtils.BooleanToText(false)));
                                    //comb.Items.Add(LibSysUtils.BooleanToText(true));
                                    //comb.Items.Add(LibSysUtils.BooleanToText(false));
                                }
                                else
                                {
                                    foreach (var item in Enum.GetValues(p.PropertyType))
                                    {
                                        comb.Items.Add(new LibItem((int)item, ReSourceManage.GetResource(item)));
                                    }
                                }
                                comb.SelectedValueChanged += new EventHandler(tb_TextChanged);
                                UCtr.Controls.Add(comb);
                                break;
                            #endregion
                            case LibControlType.TextAndBotton:
                                #region 浏览输入框控件
                                tb = new TextBox();
                                tb.Multiline = true;
                                tb.Font = font;
                                tb.Name = attr.ControlNm;
                                tb.ReadOnly = true;
                                tb.Size = new System.Drawing.Size(w, h);
                                tb.Location = new System.Drawing.Point(lb.Location.X + lb.Width, y + index * (interval + lb.Height));
                                tb.TextChanged += new EventHandler(tb_TextChanged);
                                btn = new Button();
                                btn.Font = font;
                                btn.Name = string.Format("{0}{1}", SysConstManage.BtnCtrlNmPrefix, attr.ControlNm);
                                btn.Text = SysConstManage.BtnCtrlDefaultText;
                                btn.Size = new System.Drawing.Size(btnW, h);
                                btn.Location = new System.Drawing.Point(lb.Location.X + lb.Width + tb.Width, y + index * (interval + lb.Height));
                                btn.Click += new EventHandler(((BaseUserControl<T>)UCtr).TextAndBotton_Click);

                                UCtr.Controls.Add(tb);
                                UCtr.Controls.Add(btn);
                                break;
                                #endregion
                        }
                        index++;
                    }
                }
            }
        }

        static void tb_TextChanged(object sender, EventArgs e)
        {
            if (DoModelEdit != null)
            {
                DoModelEdit(sender , !initialvale);
            }
        }

        public static void SetControlVisible(Control ctl, ControlCollection controlCollection)
        {
            foreach (Control item in controlCollection)
            {
                item.Visible = item == ctl ? true : false;
            }
        }

        #region 2018.5.24

        /// <summary>
        /// 初始化 模型菜单列表
        /// </summary>
        public static void InitialModelTree(TreeView tree)
        {
            if (tree != null && tree.Nodes.Count > 0)
                tree.Nodes.Clear();
            LibTreeNode node;
            List<NodeInfo> infolist = DoReadNodes("/Root");
            foreach (NodeInfo item in infolist)
            {
                node = new LibTreeNode();

                if (string.Compare(item.NodeName, SysConstManage.ClassNodeNm) == 0)//分类节点
                {
                    node.Text = item.Attributions[SysConstManage.AtrrName];
                    node.Name = node.Text;
                    node.NodeType = NodeType.Class;
                }
                else if (string.Compare(item.NodeName, SysConstManage.FuncNodeNm) == 0)//功能节点
                {
                    node.Text = item.InnerText;
                    node.Name = item.Attributions[SysConstManage.AtrrName];
                    node.NodeType = NodeType.Func;
                    node.Package = item.Attributions[SysConstManage.AtrrPackage];
                }
                //else if (string.Compare(item.NodeName, SysConstManage.KeyValues) == 0)
                //{
                //    node.Text = item.InnerText;
                //    node.Name = item.Attributions[SysConstManage.AtrrName];
                //    node.NodeType = NodeType.KeyValues;
                //    node.Package = item.Attributions[SysConstManage.AtrrPackage];
                //}
                node.OriginalName = node.Name;
                LibTreeNode child = new LibTreeNode(string.Empty);
                child.Name = "-1";
                node.Nodes.Add(child);
                tree.Nodes.Add(node);
            }
        }
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="parent"></param>
        public static void GetChildNode(LibTreeNode parent)
        {
            string express = string.Empty;
            if (parent != null)
            {
                List<NodeInfo> childs = null;
                LibTreeNode node;
                LibTreeNode preParent = (LibTreeNode)parent.Parent;
                bool needchild = true;
                parent.Nodes.RemoveByKey("-1");
                if (parent.NodeType == NodeType.Class)
                {
                    express = string.Format("{0}[@{1}='{2}']", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, parent.Name);
                    //else if((int)parent.Tag == 1)
                    //    express = string.Format("{0}[@name='{1}']", SysConstManage.FuncNodeNm, parent.Name);
                    #region 组织express的值
                    while (preParent != null)
                    {
                        if (preParent.NodeType == NodeType.Class)
                        {
                            express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                        }
                        else if (preParent.NodeType == NodeType.Func)
                        {
                            express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.FuncNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                        }

                        preParent = (LibTreeNode)preParent.Parent;
                    }
                    #endregion
                    childs = DoReadNodes(string.Format("/Root/{0}", express));
                    foreach (NodeInfo item in childs)
                    {
                        if (parent.Nodes.Find(item.Attributions[SysConstManage.AtrrName], false).Count() > 0)
                        {
                            continue;
                        }
                        node = new LibTreeNode();
                        //node.Name = item.Attributions[SysConstManage.AtrrName];
                        if (string.Compare(item.NodeName, SysConstManage.ClassNodeNm) == 0)//分类节点
                        {
                            node.Text = item.Attributions[SysConstManage.AtrrName];
                            node.Name = node.Text;
                            node.NodeType = NodeType.Class;
                        }
                        else if (string.Compare(item.NodeName, SysConstManage.FuncNodeNm) == 0)//功能节点
                        {
                            node.Text = item.InnerText;
                            node.Name = item.Attributions[SysConstManage.AtrrName];
                            node.NodeType = NodeType.Func;
                            node.Package = item.Attributions[SysConstManage.AtrrPackage];
                        }
                        else if (string.Compare(item.NodeName, SysConstManage.KeyValues) == 0)//
                        {
                            node.Text = item.InnerText;
                            node.Name = item.Attributions[SysConstManage.AtrrName];
                            node.NodeType = NodeType.KeyValues;
                            node.Package = item.Attributions[SysConstManage.AtrrPackage];
                            needchild = false;
                        }
                        node.OriginalName = node.Name;
                        if (needchild)
                        {
                            LibTreeNode child = new LibTreeNode(string.Empty);
                            child.Name = "-1";
                            node.Nodes.Add(child);
                        }
                        parent.Nodes.Add(node);
                    }
                }
                else
                {
                    FileOperation fileoperation = new FileOperation();
                    fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.DataSourceNm, parent.Package, parent.Name);
                    if (fileoperation.ExistsFile())
                    {
                        AddFuncNode(parent, NodeType.DataModel);
                    }
                    fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.FormSourceNm, parent.Package, parent.Name);
                    if (fileoperation.ExistsFile())
                    {
                        AddFuncNode(parent, NodeType.FormModel);
                    }
                    fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.PermissionSourceNm, parent.Package, parent.Name);
                    if (fileoperation.ExistsFile())
                    {
                        AddFuncNode(parent, NodeType.PermissionModel);
                    }
                }
            }
        }
        /// <summary>添加ModelTreeTemp文件的xml节点</summary>
        /// <param name="newNode"></param>
        public static void AddXmlNode(LibTreeNode newNode)
        {
            if (newNode != null)
            {
                LibTreeNode preParent = (LibTreeNode)newNode.Parent;
                string express = string.Empty;
                #region 组织express的值
                express = string.Format("{0}[@{1}='{2}']", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name);
                preParent = (LibTreeNode)preParent.Parent;
                while (preParent != null)
                {
                    if (preParent.NodeType == NodeType.Class)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }
                    else if (preParent.NodeType == NodeType.Func)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.FuncNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }

                    preParent = (LibTreeNode)preParent.Parent;
                }
                #endregion

                XMLOperation xmlOperation = new XMLOperation(SysConstManage.ModelTemp);
                NodeInfo nodeinfo = new NodeInfo();
                LibXMLAttributCollection attributcollection = new LibXMLAttributCollection();
                switch (newNode.NodeType)
                {
                    case NodeType.Class:
                        nodeinfo.NodeName = SysConstManage.ClassNodeNm;
                        break;
                    case NodeType.Func:
                        nodeinfo.NodeName = SysConstManage.FuncNodeNm;
                        attributcollection.Add(SysConstManage.AtrrPackage, newNode.Package);
                        break;
                    case NodeType.KeyValues:
                        nodeinfo.NodeName = SysConstManage.KeyValues;
                        attributcollection.Add(SysConstManage.AtrrPackage, newNode.Package);
                        break;
                }
                //nodeinfo.NodeName =newNode.NodeType==NodeType.Class SysConstManage.ClassNodeNm;
                nodeinfo.InnerText = newNode.Text;
                attributcollection.Add(SysConstManage.AtrrName, newNode.Name);
                nodeinfo.Attributions = attributcollection;
                xmlOperation.AddNode(nodeinfo, string.Format("/Root/{0}", express));
            }
        }
        /// <summary>
        /// 修改ModelTreeTemp文件的xml节点
        /// </summary>
        /// <param name="currentNode"></param>
        public static void UpdateXmlNode(LibTreeNode currentNode)
        {
            if (currentNode != null)
            {
                LibTreeNode preParent = (LibTreeNode)currentNode.Parent;
                string express = string.Empty;
                #region 组织express的值
                express = string.Format("{0}[@{1}='{2}']", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, currentNode.OriginalName);
                while (preParent != null)
                {
                    if (preParent.NodeType == NodeType.Class)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }
                    else if (preParent.NodeType == NodeType.Func)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.FuncNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }

                    preParent = (LibTreeNode)preParent.Parent;
                }
                #endregion
                XMLOperation xmlOperation = new XMLOperation(SysConstManage.ModelTemp);
                NodeInfo nodeinfo = new NodeInfo();
                LibXMLAttributCollection attributcollection = new LibXMLAttributCollection();
                switch (currentNode.NodeType)
                {
                    case NodeType.Class:
                        nodeinfo.NodeName = SysConstManage.ClassNodeNm;
                        break;
                    case NodeType.Func:
                        nodeinfo.NodeName = SysConstManage.FuncNodeNm;
                        attributcollection.Add(SysConstManage.AtrrPackage, currentNode.Package);
                        break;
                }
                nodeinfo.InnerText = currentNode.Text;
                attributcollection.Add(SysConstManage.AtrrName, currentNode.Name);
                nodeinfo.Attributions = attributcollection;
                if (xmlOperation.UpdateNode(nodeinfo, string.Format("/Root/{0}", express)))
                {
                    currentNode.OriginalName = currentNode.Name;
                }
            }
        }

        /// <summary>删除ModelTreeTemp文件的xml节点</summary>
        /// <param name="currentNode"></param>
        public static void DeleteXmlNode(LibTreeNode currentNode)
        {
            if (currentNode != null)
            {
                LibTreeNode preParent = (LibTreeNode)currentNode.Parent;
                string express = string.Empty;
                #region 组织express的值
                express = string.Format("{0}[@{1}='{2}']", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, currentNode.OriginalName);
                while (preParent != null)
                {
                    if (preParent.NodeType == NodeType.Class)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.ClassNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }
                    else if (preParent.NodeType == NodeType.Func)
                    {
                        express = string.Format("{0}[@{1}='{2}']/{3}", SysConstManage.FuncNodeNm, SysConstManage.AtrrName, preParent.Name, express);
                    }

                    preParent = (LibTreeNode)preParent.Parent;
                }
                #endregion
                XMLOperation xmlOperation = new XMLOperation(SysConstManage.ModelTemp);
                xmlOperation.DeletNode(string.Format("/Root/{0}", express));
            }
        }

        /// <summary> 创建模型文件</summary>
        /// <param name="treeNode"></param>
        public static void CreatModelFile(LibTreeNode treeNode)
        {
            FileOperation fileoperation = new FileOperation();
            string dir = string.Empty;
            switch (treeNode.NodeType)
            {
                case NodeType.DataModel:
                    dir = SysConstManage.DataSourceNm;
                    break;
                case NodeType.FormModel:
                    dir = SysConstManage.FormSourceNm;
                    break;
                case NodeType.PermissionModel:
                    dir = SysConstManage.PermissionSourceNm;
                    break;
                case NodeType.KeyValues:
                    dir = SysConstManage.KeyValues;
                    break;
            }
            fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, dir, treeNode.Package, treeNode.Name);
            fileoperation.CreateFile(true);
        }

        public static void DeleteModelFile(LibTreeNode treeNode)
        {

        }

        public static bool ExitsDataSource(string dsid, string package)
        {
            FileOperation fileoperation = new FileOperation();
            fileoperation.FilePath = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.DataSourceNm, package, dsid);
            return fileoperation.ExistsFile();
        }

        private static List<NodeInfo> DoReadNodes(string express)
        {
            XMLOperation xmlOperation = new XMLOperation(SysConstManage.ModelTemp);
            ILibXMLNodeRead noderead = xmlOperation.NodeRead(express);
            List<NodeInfo> result = new List<NodeInfo>();
            while (!noderead.EOF)
            {
                if (noderead.HasChildNode)
                {
                    NodeInfo childinfo = null;
                    for (int index = 0; (childinfo = noderead.ReadChild(index)) != null; index++)
                    {
                        result.Add(childinfo);
                    }
                }
                noderead.ReadNext();
            }
            return result;
        }

        private static void AddFuncNode(LibTreeNode parent, NodeType type)
        {
            foreach (LibTreeNode item in parent.Nodes)
            {
                if (string.Compare(item.Name, parent.Name) == 0 && item.NodeType == type)
                {
                    return;
                }
            }
            LibTreeNode node = new LibTreeNode();
            node.Text = parent.Text;
            node.Name = parent.Name;
            node.OriginalName = node.Name;
            node.NodeType = type;
            node.Package = parent.Package;
            parent.Nodes.Add(node);
        }

        #region 模型对象操作
        public static LibDataSource GetDataSourceById(string dataSourceNm)
        {
            if (_dataSourceContain.ContainsKey(dataSourceNm))
            {
                return (LibDataSource)_dataSourceContain[dataSourceNm];
            }
            else
            {
                LibDataSource ds = ModelManager.GetDataSource(dataSourceNm);
                _dataSourceContain.Add(dataSourceNm, ds);
                return ds;
            }

        }

        public static LibFormPage GetFormSourceByFormId(string formid)
        {
            if (_formSourceContain.ContainsKey(formid))
            {
                return (LibFormPage)_formSourceContain[formid];
            }
            else
            {
                LibFormPage fm = ModelManager.GetFormSource(formid);
                _formSourceContain.Add(formid, fm);
                return fm;
            }
        }
        public static LibKeyValueCollection GetKeyvaluesByid(string id)
        {
            if (_keyvaluesContain.ContainsKey(id))
            {
                return (LibKeyValueCollection)_keyvaluesContain[id];
            }
            else
            {
                LibKeyValueCollection keyvalus = ModelManager.GetKeyValues(id);
                _keyvaluesContain.Add(id, keyvalus);
                return keyvalus;
            }
        }

        public static void RemoveDataSource(string dataSourceNm)
        {
            if (_dataSourceContain.ContainsKey(dataSourceNm))
            {
                _dataSourceContain[dataSourceNm] = null;
                _dataSourceContain.Remove(dataSourceNm);
            }
        }
        /// <summary>保存模型设计</summary>
        /// <param name="modelNm"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool SaveModel(string modelNm, NodeType type)
        {
            string path = string.Empty;
            switch (type)
            {
                case NodeType.DataModel:
                    if (_dataSourceContain.ContainsKey(modelNm))
                    {
                        LibDataSource ds = (LibDataSource)_dataSourceContain[modelNm];
                        path = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.DataSourceNm, ds.Package, ds.DSID);
                        return InternalSaveModel(ds, path);
                    }
                    else
                    {
                        return false;
                    }
                case NodeType.FormModel:
                    if (_formSourceContain.ContainsKey(modelNm))
                    {
                        LibFormPage fm = (LibFormPage)_formSourceContain[modelNm];
                        path = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.FormSourceNm, fm.Package, fm.FormId);
                        return InternalSaveModel(fm, path);
                    }
                    else
                    {
                        return false;
                    }
                case NodeType.PermissionModel:
                    if (_permissionSourceContain.ContainsKey(modelNm))
                    {
                        LibPermissionSource pm = (LibPermissionSource)_permissionSourceContain[modelNm];
                        return InternalSaveModel(pm, path);
                    }
                    else
                    {
                        return false;
                    }
                case NodeType.KeyValues:
                    if (_keyvaluesContain.ContainsKey(modelNm))
                    {
                        LibKeyValueCollection keyvalus = (LibKeyValueCollection)_keyvaluesContain[modelNm];
                        path = string.Format(@"{0}\{1}\{2}\{3}.xml", SysConstManage.ModelPath, SysConstManage.KeyValues, keyvalus.Package, keyvalus.ID);
                        return InternalSaveModel(keyvalus, path);
                    }
                    else
                        return false;
                default:
                    return false;
            }
        }
        #endregion

        #endregion

        #endregion

        #region 私有函数

        #region 旧代码
        private static void SaveDataSource(DataSource ds)
        {
            DSInfo dsinfo = new DSInfo();
            dsinfo.DSID = ds.DSID;
            dsinfo.Name = ds.DataSourceName;
            //dsinfo.DISPLAYTEXT = ds.DSDisplayText;
            dsinfo.PACKAGE = ds.Package;
            if (_dsList.DSInfoCollection == null)
                _dsList.DSInfoCollection = new LibCollection<DSInfo>();
            if (!CheckDSList(dsinfo))
            {
                _dsList.DSInfoCollection.Add(dsinfo);
            }
            SerializerUtils.xmlserialzaition(_dsList, SysConstManage.DSListFile);
            SerializerUtils.xmlserialzaition(ds, string.Format("{0}\\{1}\\{2}.xml", SysConstManage.DSFileRootPath, ds.Package, ds.DataSourceName));
        }

        /// <summary>检查数据源列表是否已经存在对应数据</summary>
        /// <param name="dsInfo"></param>
        /// <returns></returns>
        private static bool CheckDSList(DSInfo dsInfo)
        {
            foreach (DSInfo item in _dsList.DSInfoCollection)
            {
                if (item.DSID == dsInfo.DSID)
                {
                    return true;
                }
            }
            return false;
        }

        private static DataSource DoGetDSFromXML(int dsId)
        {
            return ModelManager.DoGetDataSource(dsId);
        }

        private static DataSource DoGetDSFromXML(string dsName)
        {
            return ModelManager.DoGetDataSource(dsName);
        }

        private static bool DoExistId(int dsId)
        {
            return ModelManager.DoIsExistId(dsId);
        }


        private static bool DoExistDSName(string dataSourceName)
        {
            return ModelManager.DoIsExistDSName(dataSourceName);
        }

        private static List<DataSource> DoGetDataSourceList()
        {
            return ModelManager.DoGetDataSourceListEx(ref _dsList);
        }

        /// <summary>检查具体制定 的dropdowlist控件</summary>
        /// <param name="control"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private static bool CheckEnumFields(Control control, PropertyInfo info, object valueType)
        {
            bool result = false;
            if (string.Compare(control.Name, "fd_combFieldType", true) == 0)
            {
                control.Text = ReSourceManage.GetResource((LibFieldType)info.GetValue(valueType, null));
                result = true;
            }
            return result;
        }
        #endregion

        private static bool InternalSaveModel<T>(T entity, string path)
        {
            SerializerUtils.xmlserialzaition(entity, path);
            return true;
        }
        #endregion

    }
    /// <summary>节点类型</summary>
    public enum NodeType
    {
        /// <summary>分类节点</summary>
        [LibReSource("分类节点")]
        Class = -1,
        /// <summary>功能节点</summary>
        [LibReSource("功能节点")]
        Func = 1,
        /// <summary>数据源模型节点</summary>
        [LibReSource("数据源模型节点")]
        DataModel = 2,
        /// <summary>排版模型节点</summary>
        [LibReSource("排版模型节点")]
        FormModel = 3,
        /// <summary>权限模型节点</summary>
        [LibReSource("权限模型节点")]
        PermissionModel = 4,
        /// <summary>特殊功能</summary>
        [LibReSource("特殊功能节点")]
        SpectFunc = 5,
        /// <summary>报表功能</summary>
        [LibReSource("报表功能节点")]
        ReportFunc = 6,
        /// <summary>字典模型</summary>
        [LibReSource("字典模型节点")]
        KeyValues =16,


        /// <summary>数据集</summary>
        [LibReSource("数据集")]
        DefDataSet = 7,
        /// <summary>自定义表</summary>
        [LibReSource("自定义表")]
        DefindTable = 8,
        /// <summary>数据结构表</summary>
        [LibReSource("数据结构表")]
        TableStruct = 9,
        /// <summary>字段</summary>
        [LibReSource("字段")]
        Field = 10,

        /// <summary>排版页面容器</summary>
        [LibReSource("页面容器")]
        FormPanel = 11,
        /// <summary>页面信息组 </summary>
        [LibReSource("页面信息组")]
        FormGroup = 12,
        /// <summary> 页面表格组</summary>
        [LibReSource("页面表格组")]
        GridGroup = 13,
        /// <summary> 信息组字段</summary>
        [LibReSource("信息组字段")]
        FormGroup_Field = 14,
        /// <summary> 信息组字段</summary>
        [LibReSource("表格组字段")]
        GridGroup_Field = 15

    }
}
