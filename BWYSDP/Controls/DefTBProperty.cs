using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager;
using System.Reflection;
using SDPCRL.CORE;
using BWYSDP.com;

namespace BWYSDP.Controls
{
    public partial class DefTBProperty : UserControl
    {
        private int _dsId;
        private DefineTable _defineTable;
        private TreeNode _defTBNode;
        public DefTBProperty()
        {
            InitializeComponent();
            this.DefTB_txtTableName.LostFocus += new EventHandler(DefTB_PropertyTextBox_LostFocus);
            this.DefTB_txtDisplayName.LostFocus += new EventHandler(DefTB_PropertyTextBox_LostFocus);
        }

        void DefTB_PropertyTextBox_LostFocus(object sender, EventArgs e)
        {
            SetPropertyValue(sender);

        }
        public DefTBProperty(int dsId)
            :this()
        {
            this._dsId = dsId;
        }

        public void SetPropertyValue(DefineTable defTB,TreeNode defTBNode)
        {
            _defineTable = defTB;
            _defTBNode = defTBNode;
            ModelDesignProject.DoSetPropertyValue<DefineTable>(this.Controls, defTB);
        }

        private void SetPropertyValue(object valueObj)
        {
            string propertyName = string.Empty;
            object propertyValue=null ;
            switch (valueObj.GetType().Name)
            {
                case "TextBox":
                    TextBox obj = (TextBox)valueObj;
                    propertyName = obj.Name;
                    propertyValue=obj .Text;
                    break;
            }
            if (_defineTable != null)
            {
                PropertyInfo[] propertis = _defineTable.GetType().GetProperties();
                foreach (PropertyInfo p in propertis)
                {
                    object[] attrArray = p.GetCustomAttributes(typeof(LibXmlAttributeAttribute), true);
                    if (attrArray.Length > 0)
                    {
                        LibXmlAttributeAttribute attr = attrArray[0] as LibXmlAttributeAttribute;
                        if (string.Compare(attr.ControlNm, propertyName, false) == 0)
                        {
                            p.SetValue(_defineTable, propertyValue, null);
                            if (string.Compare(propertyName, "DefTB_txtTableName", true) == 0)
                                _defTBNode.Name = propertyValue.ToString();//更新数节点的名称
                            else if (string.Compare(propertyName, "DefTB_txtDisplayName", true) == 0)
                                _defTBNode.Text = propertyValue.ToString(); //更新数节点的显示名称
                            break;
                        }
                    }
                }
            }
        }
    }
}
