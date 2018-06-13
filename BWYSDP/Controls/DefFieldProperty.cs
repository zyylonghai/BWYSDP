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
    public partial class DefFieldProperty : UserControl
    {
        private LibField _field;
        private TreeNode _fieldNode;
        public DefFieldProperty()
        {
            InitializeComponent();
            this.fd_txtFieldName.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            this.fd_combFieldType.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            this.fd_txtDisplayText.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            this.fd_txtAliasName.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            this.fd_combAllowNull.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
            this.fd_combActive.LostFocus += new EventHandler(fd_PropertyTextBox_LostFocus);
        }

        void fd_PropertyTextBox_LostFocus(object sender, EventArgs e)
        {
            SetPropertyValue(sender);
        }

        /// <summary>设置属性值</summary>
        /// <param name="Field"></param>
        /// <param name="node"></param>
        public void SetPropertyValue(LibField field, TreeNode node)
        {
            this._field = field;
            this._fieldNode = node;
            ModelDesignProject.DoSetPropertyValue<LibField>(this.Controls, field);

        }

        /// <summary>用于控件失去焦点后，进行对应对象的赋值</summary>
        /// <param name="valueObj"></param>
        private void SetPropertyValue(object valueObj)
        {
            string propertyName = string.Empty;
            object propertyValue = null;
            switch (valueObj.GetType().Name)
            {
                case "TextBox":
                    TextBox obj = (TextBox)valueObj;
                    propertyName = obj.Name;
                    propertyValue = obj.Text;
                    break;
                case "ComboBox":
                    ComboBox combobj = (ComboBox)valueObj;
                    propertyName = combobj.Name;
                    if (string.Compare(propertyName, "fd_combFieldType", true) == 0)//字段类型
                    {
                        propertyValue = EnumOperation.GetFieldTypeValue(combobj.Text);
                    }
                    else if (string.Compare(propertyName, "fd_combAllowNull", true) == 0 ||
                             string.Compare(propertyName, "fd_combActive", true) == 0)//允许为null,是否虚字段
                    {
                        propertyValue = LibSysUtils.ToBooLean(combobj.Text);
                    }
                    break;
            }
            if (_field != null)
            {
                PropertyInfo[] propertis = _field.GetType().GetProperties();
                foreach (PropertyInfo p in propertis)
                {
                    object[] attrArray = p.GetCustomAttributes(typeof(LibXmlAttributeAttribute), true);
                    if (attrArray.Length > 0)
                    {
                        LibXmlAttributeAttribute attr = attrArray[0] as LibXmlAttributeAttribute;
                        if (string.Compare(attr.ControlNm, propertyName, false) == 0)
                        {
                            p.SetValue(_field, propertyValue, null);
                            if (string.Compare(propertyName, "fd_txtFieldName", true) == 0)
                                _fieldNode.Name = propertyValue.ToString();//更新数节点的名称
                            else if (string.Compare(propertyName, "fd_txtDisplayText", true) == 0)
                                _fieldNode.Text = propertyValue.ToString(); //更新数节点的显示名称
                            break;
                        }
                    }
                }
            }
        }
    }
}
