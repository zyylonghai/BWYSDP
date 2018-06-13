using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDPCRL.DAL.Service;

namespace InitialTool
{
    public partial class ServiceTool : Form
    {
        public ServiceTool()
        {
            InitializeComponent();
            string s = System.DateTime.Now.ToString();
        }

        /// <summary>
        /// 账套配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DBConfig dbconfig = new DBConfig();
            dbconfig.Show();
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServerStar_Click(object sender, EventArgs e)
        {
            ServiceFactory.DBService.Star();
            this.listBox1.Items.Add("数据服务已经启动。");
        }
    }
}
