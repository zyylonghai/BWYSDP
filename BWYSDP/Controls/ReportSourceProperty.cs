using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDPCRL.COM.ModelManager.Reports;
using BWYSDP.com;

namespace BWYSDP.Controls
{
    public partial class ReportSourceProperty :  BaseUserControl<LibReportsSource>
    {
        public ReportSourceProperty()
        {
            InitializeComponent();
            InitializeControls();
            ModelDesignProject.DoModelEdit += ModelDesignProject_DoModelEdit;
        }

        private void ModelDesignProject_DoModelEdit(object sender, bool ischange)
        {
            if (ischange)
            {
                Control ctr=(Control)sender;
                if (ctr.Name == "rpt_Layoutmode")
                {
                    ComboBox layoutmode = (ComboBox)sender;
                    if (layoutmode != null)
                    {
                        this.entity.Layoutmode = (LayoutMode)((LibItem)layoutmode.SelectedItem).Key;
                    }
                }
            }
        }
    }
}
