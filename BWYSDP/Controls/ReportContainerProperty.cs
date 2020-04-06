﻿using System;
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
    public partial class ReportContainerProperty : BaseUserControl<LibReportContainer>
    {
        private LibTreeNode _Node;
        public ReportContainerProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        public ReportContainerProperty(string name)
              : this()
        {
            this.Name = name;
        }

        public override void SetPropertyValue(LibReportContainer entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
            _Node = node;
        }
    }
}
