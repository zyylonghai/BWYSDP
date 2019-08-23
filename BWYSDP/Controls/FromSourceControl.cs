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
    public partial class FromSourceControl : UserControl
    {
        //public List<FromSourceProperty> fromSourcesProplist = null;
        public FromSourceControl()
        {
            InitializeComponent();
            //fromSourcesProplist = new List<FromSourceProperty>();
        }
        public FromSourceControl(LibFromSourceField[] sourceFields)
            :this()
        {
            foreach (LibFromSourceField item in sourceFields)
            {
                this.listBox1.Items.Add(item);
                FromSourceProperty sourceProperty = new FromSourceProperty(item.ID);
                sourceProperty.Dock = DockStyle.Fill;
                this.splitContainer1.Panel2.Controls.Add(sourceProperty);
                sourceProperty.SetPropertyValue(item, null);
                ModelDesignProject.SetControlVisible(sourceProperty, this.splitContainer1.Panel2.Controls);
            }
        }
        /// <summary>
        /// 新增按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            LibFromSourceField fromSourceField = new LibFromSourceField();
            fromSourceField.ID = Guid.NewGuid().ToString();
            this.listBox1.Items.Add(fromSourceField);
            FromSourceProperty sourceProperty = new FromSourceProperty(fromSourceField.ID);
            sourceProperty.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(sourceProperty);
            sourceProperty.SetPropertyValue(fromSourceField, null);
            ModelDesignProject.SetControlVisible(sourceProperty, this.splitContainer1.Panel2.Controls);
            //SetPanel2ControlsVisible(sourceProperty);
        }

        /// <summary>
        /// 移除按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox box = sender as ListBox;
            LibFromSourceField selectfield = box.SelectedItem as LibFromSourceField;
            foreach (Control item in this.splitContainer1.Panel2.Controls)
            {
                if (item.Name == selectfield.ID)
                {
                    ModelDesignProject.SetControlVisible(item, this.splitContainer1.Panel2.Controls);
                    break;
                }
            }
        }

        private void FillControls(LibFromSourceField[] sourceFields)
        {

        }
    }
}
