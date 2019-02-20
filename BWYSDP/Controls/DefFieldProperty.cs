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
using System.Reflection;
using SDPCRL.COM.ModelManager.com;

namespace BWYSDP.Controls
{
    public partial class DefFieldProperty : BaseUserControl
    {
        private LibField _field;
        private LibTreeNode _fieldNode;
        public DefFieldProperty()
        {
            InitializeComponent();
            InitializeControls<LibField >();
            //this.fd_txtFieldName.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_combFieldType.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_txtDisplayText.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_txtAliasName.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_combAllowNull.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            //this.fd_combActive.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
        }
        public DefFieldProperty(string name)
            :this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue<TEntity>(TEntity entity, LibTreeNode node)
        {
            base.SetPropertyValue<TEntity>(entity, node);
            this._field = entity as LibField;
            this._fieldNode = node;
        }
        //private void InitializeControls()
        //{
        //    ModelDesignProject.InternalBindControls<LibField>(this);
        //}

        //void fd_PropertyTextBox_LostFocus(object sender, EventArgs e)
        //{
        //    SetPropertyValue(sender);
        //}

        ///// <summary>设置属性值</summary>
        ///// <param name="Field"></param>
        ///// <param name="node"></param>
        //public void SetPropertyValue(LibField field, LibTreeNode node)
        //{
        //    this._field = field;
        //    this._fieldNode = node;
        //    ModelDesignProject.DoSetPropertyValue<LibField>(this.Controls, field);

        //}

        public void GetControlsValue()
        {
            ModelDesignProject.DoGetControlsValue(this.Controls, _field);
        }

        private void DefFieldProperty_Load(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (string .Compare ("fd_txtFieldName",item.Name )==0 ||string .Compare ("fd_txtDisplayText",item.Name )==0)//字段名称控件
                {
                    item.KeyUp += new KeyEventHandler(item_KeyUp);
                }
            }
        }

        void item_KeyUp(object sender, KeyEventArgs e)
        {
            Control ctl=sender as Control ;
            if (e.KeyCode == Keys.Enter)
            {
                foreach (LibTreeNode item in this._fieldNode.Parent.Nodes)
                {
                    if (string.Compare(ctl.Text .Trim (), item.Name,true) == 0)
                    {
                        throw new LibExceptionBase("不能有重复字段名");
                    }
                }
                this._fieldNode.Name = string.Compare("fd_txtFieldName", ctl.Name) == 0 ? ctl.Text.Trim() : this._fieldNode.Name;
                this._field.Name = string.Compare("fd_txtFieldName", ctl.Name) == 0 ? ctl.Text.Trim() : this._field.Name;
                this._fieldNode.Text = string.Format("{0}({1})", _field.Name, string.Compare("fd_txtFieldName", ctl.Name) == 0 ?_field.DisplayName:ctl.Text);
            }
        }

        ///// <summary>用于控件失去焦点后，进行对应对象的赋值</summary>
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
        //            propertyName = combobj.Name;
        //            if (string.Compare(propertyName, "fd_combFieldType", true) == 0)//字段类型
        //            {
        //                propertyValue = EnumOperation.GetFieldTypeValue(combobj.Text);
        //            }
        //            else if (string.Compare(propertyName, "fd_combAllowNull", true) == 0 ||
        //                     string.Compare(propertyName, "fd_combActive", true) == 0)//允许为null,是否虚字段
        //            {
        //                propertyValue = LibSysUtils.ToBooLean(combobj.Text);
        //            }
        //            break;
        //    }
        //    if (_field != null)
        //    {
        //        PropertyInfo[] propertis = _field.GetType().GetProperties();
        //        foreach (PropertyInfo p in propertis)
        //        {
        //            object[] attrArray = p.GetCustomAttributes(typeof(LibXmlAttributeAttribute), true);
        //            if (attrArray.Length > 0)
        //            {
        //                LibXmlAttributeAttribute attr = attrArray[0] as LibXmlAttributeAttribute;
        //                if (string.Compare(attr.ControlNm, propertyName, false) == 0)
        //                {
        //                    p.SetValue(_field, propertyValue, null);
        //                    if (string.Compare(propertyName, "fd_txtFieldName", true) == 0)
        //                        _fieldNode.Name = propertyValue.ToString();//更新数节点的名称
        //                    else if (string.Compare(propertyName, "fd_txtDisplayText", true) == 0)
        //                        _fieldNode.Text = propertyValue.ToString(); //更新数节点的显示名称
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
