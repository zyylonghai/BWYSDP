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
    /// <summary>自定义表属性控件对象</summary>
    public partial class DefTBProperty : BaseUserControl
    {
        //private int _dsId;
        private LibDefineTable _defineTable;
        private LibTreeNode _defTBNode;
        public DefTBProperty()
        {
            InitializeComponent();
            InitializeControls<LibDefineTable >();
            //this.DefTB_txtTableName.LostFocus += new EventHandler(DefTB_PropertyTextBox_LostFocus);
            //this.DefTB_txtDisplayName.LostFocus += new EventHandler(DefTB_PropertyTextBox_LostFocus);
        }
        public DefTBProperty(string name)
            : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue<TEntity>(TEntity entity, LibTreeNode node)
        {
            base.SetPropertyValue<TEntity>(entity, node);
            _defineTable = entity as LibDefineTable;
            _defTBNode = node;
        }

        //private void InitializeControls()
        //{
        //    ModelDesignProject.InternalBindControls<LibDefineTable>(this);
        //}

        //void DefTB_PropertyTextBox_LostFocus(object sender, EventArgs e)
        //{
        //    //SetPropertyValue(sender);

        //}


        //public void SetPropertyValue(LibDefineTable defTB, LibTreeNode defTBNode)
        //{
        //    _defineTable = defTB;
        //    _defTBNode = defTBNode;
        //    ModelDesignProject.DoSetPropertyValue<LibDefineTable>(this.Controls, defTB);
        //}

        public void GetControlsValue()
        {
            ModelDesignProject.DoGetControlsValue(this.Controls, _defineTable);
        }

        //private void SetPropertyValue(object valueObj)
        //{
        //    string propertyName = string.Empty;
        //    object propertyValue=null ;
        //    switch (valueObj.GetType().Name)
        //    {
        //        case "TextBox":
        //            TextBox obj = (TextBox)valueObj;
        //            propertyName = obj.Name;
        //            propertyValue=obj .Text;
        //            break;
        //    }
        //    if (_defineTable != null)
        //    {
        //        PropertyInfo[] propertis = _defineTable.GetType().GetProperties();
        //        foreach (PropertyInfo p in propertis)
        //        {
        //            object[] attrArray = p.GetCustomAttributes(typeof(LibXmlAttributeAttribute), true);
        //            if (attrArray.Length > 0)
        //            {
        //                LibXmlAttributeAttribute attr = attrArray[0] as LibXmlAttributeAttribute;
        //                if (string.Compare(attr.ControlNm, propertyName, false) == 0)
        //                {
        //                    p.SetValue(_defineTable, propertyValue, null);
        //                    if (string.Compare(propertyName, "DefTB_txtTableName", true) == 0)
        //                        _defTBNode.Name = propertyValue.ToString();//更新数节点的名称
        //                    else if (string.Compare(propertyName, "DefTB_txtDisplayName", true) == 0)
        //                        _defTBNode.Text = propertyValue.ToString(); //更新数节点的显示名称
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
