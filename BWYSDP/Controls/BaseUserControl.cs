using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;

namespace BWYSDP.Controls
{
    public class BaseUserControl : UserControl
    {
        public BaseUserControl()
        {
 
        }

        public void InitializeControls<T>()
        {
            ModelDesignProject.InternalBindControls<T>(this);
        }

        public virtual void SetPropertyValue<TEntity>(TEntity entity, LibTreeNode node)
        {
            ModelDesignProject.DoSetPropertyValue<TEntity>(this.Controls, entity);
        }

        public virtual void TextAndBotton_Click(object sender, EventArgs e)
        {
 
        }
    }
}
