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
    public partial class ReportRowProperty : BaseUserControl<LibReportRow>
    {
        private LibTreeNode _Node;
        public ReportRowProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        public ReportRowProperty(string name)
           : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibReportRow entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }
    }
}
