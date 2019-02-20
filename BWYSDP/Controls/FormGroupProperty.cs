using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager.FormTemplate;

namespace BWYSDP.Controls
{
    public partial class FormGroupProperty : BaseUserControl
    {
        public FormGroupProperty()
        {
            InitializeComponent();
            InitializeControls <LibFormGroup>();
        }

        public FormGroupProperty(string name)
            :this()
        {
            this.Name = name;
        }
    }
}
