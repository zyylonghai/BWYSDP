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
    public partial class TBStructProperty : UserControl
    {
        private int _dsId;
        private DataTableStruct _tableStruct;
        private TreeNode _tableStructNode;
        public TBStructProperty()
        {
            InitializeComponent();
            this.tbStruct_txtTableName.LostFocus += new EventHandler(tbStruct_PropertyTextBox_LostFocus);
            this.tbStruct_txtTableDisplayName.LostFocus += new EventHandler(tbStruct_PropertyTextBox_LostFocus);
            this.tbStruct_combcreateTBStruct.LostFocus += new EventHandler(tbStruct_PropertyTextBox_LostFocus);

        }

        void tbStruct_PropertyTextBox_LostFocus(object sender, EventArgs e)
        {
            SetPropertyValue(sender);
        }
        public TBStructProperty(int dsId)
            :this()
        {
            this._dsId = dsId;
        }

        /// <summary>设置属性值</summary>
        /// <param name="tableStruct"></param>
        public void SetPropertyValue(DataTableStruct tableStruct,TreeNode tableStructNode)
        {
            _tableStruct = tableStruct;
            _tableStructNode = tableStructNode;
            ModelDesignProject.DoSetPropertyValue<DataTableStruct>(this.Controls, tableStruct);
        }

        /// <summary></summary>
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
                    if (string.Compare(combobj.Name, "comb_createTBStruct", true) == 0)
                    {
                        propertyName = combobj.Name;
                        //if(combobj.SelectedText
                        propertyValue = LibSysUtils .ToBooLean(combobj.Text);
                    }
                    break;
            }
            if (_tableStruct != null)
            {
                PropertyInfo[] propertis = _tableStruct.GetType().GetProperties();
                foreach (PropertyInfo p in propertis)
                {
                    object[] attrArray = p.GetCustomAttributes(typeof(LibXmlAttributeAttribute), true);
                    if (attrArray.Length > 0)
                    {
                        LibXmlAttributeAttribute attr = attrArray[0] as LibXmlAttributeAttribute;
                        if (string.Compare(attr.ControlNm, propertyName, false) == 0)
                        {
                            p.SetValue(_tableStruct, propertyValue, null);
                            if (string.Compare(propertyName, "tbStruct_txtTableName", true) == 0)
                                _tableStructNode.Name = propertyValue.ToString();//更新数节点的名称
                            else if (string.Compare(propertyName, "tbStruct_txtTableDisplayName", true) == 0)
                                _tableStructNode.Text = propertyValue.ToString(); //更新数节点的显示名称
                            break;
                        }
                    }
                }
            }
        }
    }
}
