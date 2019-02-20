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
using System.Reflection;
using SDPCRL.CORE;

namespace BWYSDP.Controls
{
    /// <summary>表数据结构控件对象</summary>
    public partial class TBStructProperty : BaseUserControl
    {
        //private int _dsId;
        private LibDataTableStruct _tableStruct;
        private TreeNode _tableStructNode;
        public TBStructProperty()
        {
            InitializeComponent();
            InitializeControls<LibDataTableStruct >();
            //this.tbStruct_txtTableName.LostFocus += new EventHandler(tbStruct_PropertyTextBox_LostFocus);
            //this.tbStruct_txtTableDisplayName.LostFocus += new EventHandler(tbStruct_PropertyTextBox_LostFocus);
            //this.tbStruct_combcreateTBStruct.LostFocus += new EventHandler(tbStruct_PropertyTextBox_LostFocus);

        }
        public TBStructProperty(string  name)
            : this()
        {
            this.Name = name;
            //this._dsId = dsId;
        }

        public override void SetPropertyValue<TEntity>(TEntity entity, LibTreeNode node)
        {
            base.SetPropertyValue<TEntity>(entity, node);
            _tableStruct = entity as LibDataTableStruct;
            _tableStructNode = node;
        }

        //private void InitializeControls()
        //{
        //    ModelDesignProject.InternalBindControls<LibDataTableStruct>(this);
        //}

        //void tbStruct_PropertyTextBox_LostFocus(object sender, EventArgs e)
        //{
        //    SetPropertyValue(sender);
        //}

        ///// <summary>设置属性值</summary>
        ///// <param name="tableStruct"></param>
        //public void SetPropertyValue(LibDataTableStruct tableStruct, TreeNode tableStructNode)
        //{
        //    _tableStruct = tableStruct;
        //    _tableStructNode = tableStructNode;
        //    ModelDesignProject.DoSetPropertyValue<LibDataTableStruct>(this.Controls, tableStruct);
        //}

        public void GetControlsValue()
        {
            ModelDesignProject.DoGetControlsValue(this.Controls, _tableStruct);
        }

        //public void CreateTableStruct()
        //{
        //    ModelDesignProject.CreateTableStruct(_tableStruct);
        //}


        private void TBStructProperty_Load(object sender, EventArgs e)
        {
            //foreach (Control item in this.Controls)
            //{
            //    if (item.Name.Contains(SysConstManage.BtnCtrlNmPrefix))
            //    {
            //        item.Click += new EventHandler(item_Click);
            //    }
            //}
        }

        public override void TextAndBotton_Click(object sender, EventArgs e)
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Fill;
            p.Name = "pfieldcollection";
            p.AutoScroll = true;
            CheckBox chkb;
            List<string> target = null;
            PropertyInfo targetobj = null;
            Control ctl = sender as Control;
            PropertyInfo[] propertis = this._tableStruct.GetType().GetProperties();
            foreach (PropertyInfo info in propertis)
            {
                object[] attrArray = info.GetCustomAttributes(typeof(LibAttributeAttribute), true);
                if (attrArray.Length > 0)
                {
                    LibAttributeAttribute attr = attrArray[0] as LibAttributeAttribute;
                    if (string.Compare(attr.ControlNm, ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "")) == 0)
                    {
                        targetobj = info;
                        target = (List<string>)info.GetValue(this._tableStruct, null);
                    }
                }
            }
            foreach (LibField f in this._tableStruct.Fields)
            {
                chkb = new CheckBox();
                chkb.Checked = target.Contains(f.Name);
                chkb.Name = f.Name;
                chkb.Size = new System.Drawing.Size(100, 21); ;
                chkb.Text = string.Format("{0}({1})", f.Name, f.DisplayName);
                chkb.Dock = DockStyle.Top;
                chkb.Location = new System.Drawing.Point(0, 20);
                p.Controls.Add(chkb);
            }
            FieldCollectionForm fielsform = new FieldCollectionForm(p);
            DialogResult dialog = fielsform.ShowDialog(this);

            if (dialog == DialogResult.OK)
            {
                StringBuilder val = new StringBuilder();
                target.Clear();
                foreach (CheckBox item in p.Controls)
                {
                    if (item.Checked)
                    {
                        //if (!target.Contains(item.Name))
                        target.Add(item.Name);
                        if (val.Length > 0)
                        {
                            val.Append(SysConstManage.Comma);
                        }
                        val.Append(item.Name);
                    }
                }
                if (targetobj != null && targetobj.GetGetMethod() != null)
                    targetobj.SetValue(this._tableStruct, target, null);
                #region 控件赋值
                this.Controls[ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "")].Text = val.ToString();
                #endregion
            }
            fielsform.Dispose();
        }

        //void item_Click(object sender, EventArgs e)
        //{
        //    Panel p = new Panel();
        //    p.Dock = DockStyle.Fill;
        //    p.Name = "pfieldcollection";
        //    p.AutoScroll = true;
        //    CheckBox chkb;
        //    List<string> target=null;
        //    PropertyInfo targetobj=null;
        //    Control ctl = sender as Control;
        //    PropertyInfo[] propertis = this._tableStruct.GetType().GetProperties();
        //    foreach (PropertyInfo info in propertis)
        //    {
        //        object[] attrArray = info.GetCustomAttributes(typeof(LibAttributeAttribute), true);
        //        if (attrArray.Length > 0)
        //        {
        //            LibAttributeAttribute attr = attrArray[0] as LibAttributeAttribute;
        //            if (string.Compare(attr.ControlNm, ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "")) == 0)
        //            {
        //                targetobj = info;
        //                target =(List<string>) info.GetValue(this._tableStruct, null);
        //            }
        //        }
        //    }
        //    foreach (LibField f in this._tableStruct.Fields)
        //    {
        //        chkb = new CheckBox();
        //        chkb.Checked = target.Contains(f.Name);
        //        chkb.Name = f.Name;
        //        chkb.Size = new System.Drawing.Size(100, 21); ;
        //        chkb.Text = string.Format("{0}({1})",f.Name ,f.DisplayName);
        //        chkb.Dock = DockStyle.Top;
        //        chkb.Location = new System.Drawing.Point(0, 20);
        //        p.Controls.Add(chkb); 
        //    }
        //    FieldCollectionForm fielsform = new FieldCollectionForm(p);
        //    DialogResult dialog= fielsform.ShowDialog(this);

        //    if (dialog == DialogResult.OK)
        //    {
        //        StringBuilder val = new StringBuilder();
        //        target.Clear();
        //        foreach (CheckBox item in p.Controls)
        //        {
        //            if (item.Checked)
        //            {
        //                //if (!target.Contains(item.Name))
        //                target.Add(item.Name);
        //                if (val.Length > 0)
        //                {
        //                    val.Append(SysConstManage.Comma);
        //                }
        //                val.Append(item.Name);
        //            }
        //        }
        //        if (targetobj != null && targetobj.GetGetMethod() != null)
        //            targetobj.SetValue(this._tableStruct, target, null);
        //        #region 控件赋值
        //        this.Controls[ctl.Name.Replace(SysConstManage.BtnCtrlNmPrefix, "")].Text = val.ToString();
        //        #endregion
        //    }
        //    fielsform.Dispose();
        //}

        ///// <summary></summary>
        ///// <param name="valueObj"></param>
        //private void SetPropertyValue(object valueObj)
        //{
        //    string propertyName = string.Empty;
        //    object propertyValue = null;
        //    switch (valueObj.GetType().Name)
        //    {
        //        case "TextBox":
        //            TextBox obj = (TextBox)valueObj;
        //            propertyName = obj.Name;
        //            propertyValue = obj.Text;
        //            break;
        //        case "ComboBox":
        //            ComboBox combobj = (ComboBox)valueObj;
        //            if (string.Compare(combobj.Name, "comb_createTBStruct", true) == 0)
        //            {
        //                propertyName = combobj.Name;
        //                //if(combobj.SelectedText
        //                propertyValue = LibSysUtils .ToBooLean(combobj.Text);
        //            }
        //            break;
        //    }
        //    if (_tableStruct != null)
        //    {
        //        PropertyInfo[] propertis = _tableStruct.GetType().GetProperties();
        //        foreach (PropertyInfo p in propertis)
        //        {
        //            object[] attrArray = p.GetCustomAttributes(typeof(LibXmlAttributeAttribute), true);
        //            if (attrArray.Length > 0)
        //            {
        //                LibXmlAttributeAttribute attr = attrArray[0] as LibXmlAttributeAttribute;
        //                if (string.Compare(attr.ControlNm, propertyName, false) == 0)
        //                {
        //                    p.SetValue(_tableStruct, propertyValue, null);
        //                    if (string.Compare(propertyName, "tbStruct_txtTableName", true) == 0)
        //                        _tableStructNode.Name = propertyValue.ToString();//更新数节点的名称
        //                    else if (string.Compare(propertyName, "tbStruct_txtTableDisplayName", true) == 0)
        //                        _tableStructNode.Text = propertyValue.ToString(); //更新数节点的显示名称
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
