using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager.FormTemplate;
using BWYSDP.com;

namespace BWYSDP.Controls
{
    public partial class ButtonProperty :BaseUserControl<LibButton>
    {
        private LibTreeNode _Node;
        public ButtonProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
        public ButtonProperty(string name)
       : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibButton entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }
    }
}
