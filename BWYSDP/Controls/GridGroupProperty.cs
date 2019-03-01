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
    public partial class GridGroupProperty : BaseUserControl<LibGridGroup>
    {
        private LibTreeNode _Node;
        public GridGroupProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
        public GridGroupProperty(string name)
            :this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibGridGroup entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }
    }
}
