using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;

namespace BWYSDP.Controls
{
    public class BaseUserControl<T> : UserControl
        //where T : class
    {
        public T entity;
        public BaseUserControl()
        {
           
        }

        public void InitializeControls()
        {
            ModelDesignProject.InternalBindControls<T>(this);
        }

        public virtual void SetPropertyValue(T entity, LibTreeNode node)
        {
            this.entity = entity;
            ModelDesignProject.DoSetPropertyValue(this.Controls, entity);
        }

        public void GetControlsValue()
        {
            ModelDesignProject.DoGetControlsValue(this.Controls, this.entity);
        }

        public virtual void TextAndBotton_Click(object sender, EventArgs e)
        {
 
        }
    }
}
