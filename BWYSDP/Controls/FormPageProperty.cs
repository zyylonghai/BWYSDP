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
using SDPCRL.CORE;

namespace BWYSDP.Controls
{
    public partial class FormPageProperty : BaseUserControl<LibFormPage >
    {
        //private LibFormPage _formSource;
        public FormPageProperty()
        {
            InitializeComponent();
            InitializeControls();
        }

        ///// <summary>设置属性值</summary>
        ///// <param name="Field"></param>
        ///// <param name="node"></param>
        //public void SetPropertyValue(LibFormPage formSource, LibTreeNode node)
        //{
        //    this._formSource = formSource;
        //    //this._fieldNode = node;
        //    ModelDesignProject.DoSetPropertyValue<LibFormPage>(this.Controls, formSource);

        //}


        //public override void SetPropertyValue<TEntity>(TEntity entity, LibTreeNode node)
        //{
        //    base.SetPropertyValue<TEntity>(entity, node);
        //    this._formSource = entity as LibFormPage;
        //}

        //private void InitializeControls()
        //{
        //    ModelDesignProject.InternalBindControls<LibFormPage>(this);
        //}

        public override void TextAndBotton_Click(object sender, EventArgs e)
        {
            base.TextAndBotton_Click(sender, e);
        }

        //private void FormPageProperty_Load(object sender, EventArgs e)
        //{
        //    foreach (Control item in this.Controls)
        //    {
        //        if (item.Name.Contains(SysConstManage.BtnCtrlNmPrefix))
        //        {
        //            item.Click += new EventHandler(item_Click);
        //        }
        //    }
        //}

        //void item_Click(object sender, EventArgs e)
        //{
 
        //}
    }
}
