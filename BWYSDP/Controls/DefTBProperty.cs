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
    public partial class DefTBProperty : BaseUserControl<LibDefineTable >
    {
        //private int _dsId;
        //private LibDefineTable _defineTable;
        private LibTreeNode _defTBNode;
        public DefTBProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
        public DefTBProperty(string name)
            : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibDefineTable entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _defTBNode = node;
            
        }
    }
}
