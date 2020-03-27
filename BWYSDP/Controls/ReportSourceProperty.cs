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

namespace BWYSDP.Controls
{
    public partial class ReportSourceProperty :  BaseUserControl<LibReportsSource>
    {
        public ReportSourceProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
    }
}
