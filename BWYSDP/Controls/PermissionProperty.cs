using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager;

namespace BWYSDP.Controls
{
    public partial class PermissionProperty : BaseUserControl<LibPermissionSource>
    {
        public PermissionProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        public PermissionProperty(string permissionId)
            :this ()
        {
            this.Name = permissionId;
        }
    }
}
