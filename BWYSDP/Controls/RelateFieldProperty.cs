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

namespace BWYSDP.Controls
{
    public partial class RelateFieldProperty : BaseUserControl<LibRelateField>
    {
        public RelateFieldProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        //public override void SetPropertyValue(LibRelateField entity, LibTreeNode node)
        //{
        //    base.SetPropertyValue(entity, node);
        //}
    }
}
