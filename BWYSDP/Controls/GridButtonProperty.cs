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
    public partial class GridButtonProperty : BaseUserControl<LibGridButton>
    {
        public GridButtonProperty()
        {
            InitializeComponent();
            InitializeControls();
            ModelDesignProject.DoModelEdit += ModelDesignProject_DoModelEdit;
        }
        private void ModelDesignProject_DoModelEdit(object sender, bool ischange)
        {
            if (ischange)
            {
                if (this.entity != null)
                {
                    this.entity.GridButtonName = this.Controls["gridbtn_GridButtonName"].Text;
                    this.entity.GridButtonDisplayNm = this.Controls["gridbtn_GridButtonDisplayNm"].Text;
                    this.entity.GridButtonEvent = this.Controls["gridbtn_GridButtonEvent"].Text;
                }
            }
        }
        public override void SetPropertyValue(LibGridButton entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
        }
    }
}
