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
    public partial class DataSourceProperty : UserControl
    {
        private LibDataSource _dataSource;
         
        public DataSourceProperty()
        {
            InitializeComponent();
        }
        /// <summary>设置属性值</summary>
        /// <param name="Field"></param>
        /// <param name="node"></param>
        public void SetPropertyValue(LibDataSource datasource, LibTreeNode node)
        {
            this._dataSource = datasource;
            //this._fieldNode = node;
            ModelDesignProject.DoSetPropertyValue<LibDataSource>(this.Controls, datasource);

        }
    }
}
