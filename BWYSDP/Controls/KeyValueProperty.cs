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
    public partial class KeyValueProperty : BaseUserControl<LibKeyValue>
    {
        public KeyValueProperty()
        {
            InitializeComponent();
            InitializeControls();
            ModelDesignProject.DoModelEdit += ModelDesignProject_DoModelEdit; ;
        }

        private void ModelDesignProject_DoModelEdit(bool ischange)
        {
            if (ischange)
            {
                if (this.entity != null)
                {
                    this.entity.Key = this.Controls["keyval_Key"].Text;
                    this.entity.Value = this.Controls["keyval_Value"].Text;
                    this.entity.Remark = this.Controls["keyval_Remark"].Text;
                }
            }
        }

        public override void SetPropertyValue(LibKeyValue entity, LibTreeNode node)
        {
            base.SetPropertyValue(entity, node);
        }
    }
}
