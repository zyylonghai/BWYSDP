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
    public partial class DataSourceProperty : BaseUserControl<LibDataSource >
    {
        //private LibDataSource _dataSource;
         
        public DataSourceProperty()
        {
            InitializeComponent();
            InitializeControls();
        }
    }
}
