using SDPCRL.CORE;
using SDPCRL.CORE.FileUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWYSDP
{
    public partial class FileUpload : LibFormBase
    {
        public FileUpload()
        {
            InitializeComponent();
            this.txthost.Text = "192.168.123.24";
            this.txtpoint.Text = "21";
            this.txtusernm.Text = "erp-zhengyy";
            this.txtpwd.Text = "ITpwd2018";
        }
        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
            if (tag == "ftpupload")
            {
                //读取根目录下的模型文件
                FileOperation fileOperation = new FileOperation();
                fileOperation.FilePath = string.Format(@"{0}\{1}", SysConstManage.ModelPath,SysConstManage.DataSourceNm);
                fileOperation.Encoding = LibEncoding.UTF8;
                TreeNode datasourceNode = new TreeNode("数据源模型");
                datasourceNode.Name = SysConstManage.DataSourceNm;
                TreeNode node = null;
                this.treeView1.Nodes.Add(datasourceNode);
                List<LibFileInfo > fileinfos= fileOperation.SearchAllFileInfo();
                AddNodes(fileinfos, datasourceNode,SysConstManage.DataSourceNm);
                //foreach (var item in fileinfos)
                //{
                //    if (datasourceNode.Nodes.ContainsKey(item.Folder))
                //    {
                //        node = new TreeNode(item.FileName);
                //        node.Name = item.FileName;
                //        datasourceNode.Nodes[item.Folder].Nodes.Add(node);
                //    }
                //    else
                //    {
                //        node = new TreeNode(item.Folder);
                //        node.Name = item.Folder;
                //        datasourceNode.Nodes.Add(node);

                //        node = new TreeNode(item.FileName);
                //        node.Name = item.FileName;
                //        datasourceNode.Nodes[item.Folder].Nodes.Add(node);
                //    }
                //}

                TreeNode formsourceNode = new TreeNode("排版模型");
                formsourceNode.Name = SysConstManage.FormSourceNm;
                this.treeView1.Nodes.Add(formsourceNode);
                fileOperation.FilePath = string.Format(@"{0}\{1}", SysConstManage.ModelPath, SysConstManage.FormSourceNm);
                fileinfos = fileOperation.SearchAllFileInfo();
                AddNodes(fileinfos, formsourceNode,SysConstManage.FormSourceNm);


                TreeNode permissionNode = new TreeNode("权限模型");
                permissionNode.Name = SysConstManage.PermissionSourceNm;
                this.treeView1.Nodes.Add(permissionNode);
                fileOperation.FilePath = string.Format(@"{0}\{1}", SysConstManage.ModelPath, SysConstManage.PermissionSourceNm);
                fileinfos = fileOperation.SearchAllFileInfo();
                AddNodes(fileinfos, permissionNode,SysConstManage.PermissionSourceNm);
            }
        }

        private void AddNodes(List<LibFileInfo> fileinfos, TreeNode pnode,string modelsource)
        {
            TreeNode node = null;
            ModelInfo info = null;
            foreach (var item in fileinfos)
            {
                if (pnode.Nodes.ContainsKey(item.Folder))
                {
                    node = new TreeNode(item.FileName);
                    node.Name = item.FileName;
                    //node.Tag = item;
                    info = new ModelInfo
                    {
                        FilePath = item.Path,
                        Name = item.FullFileName,
                        package = item.Folder,
                        ModelSource = modelsource
                    };
                    node.Tag = info;
                    pnode.Nodes[item.Folder].Nodes.Add(node);
                }
                else
                {
                    node = new TreeNode(item.Folder);
                    node.Name = item.Folder;
                    pnode.Nodes.Add(node);

                    node = new TreeNode(item.FileName);
                    node.Name = item.FileName;
                    //node.Tag = item;
                    info = new ModelInfo
                    {
                        FilePath = item.Path,
                        Name = item.FullFileName,
                        package = item.Folder,
                        ModelSource = modelsource
                    };
                    node.Tag = info;
                    pnode.Nodes[item.Folder].Nodes.Add(node);
                }
            }
        }
        /// <summary>
        /// >> 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            TreeNode selectnode = this.treeView1.SelectedNode;
            ModelInfo model = (ModelInfo)selectnode.Tag;
            this.listBox1.Items.Add(model);
        }
        /// <summary>
        /// 登陆事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string host = this.txthost.Text.Trim();
            string point = this.txtpoint.Text.Trim();
            string usernm = this.txtusernm.Text.Trim();
            string pwd = this.txtpwd.Text.Trim();
            if (!host.Contains(":"))
            {
                host = string.Format("{0}:{1}", host, point);
            }
            if (!host.Contains("ftp"))
            {
                host = string.Format("{0}{1}", "ftp://", host);
            }
            FTPHelp ftp = new FTPHelp(host, usernm, pwd);
            if (ftp.RemoteFtpDirExists(host))
            {
                MessageBox.Show("链接成功");
            }
        }
        /// <summary>
        /// 上传按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            string host = this.txthost.Text.Trim();
            string point = this.txtpoint.Text.Trim();
            string usernm = this.txtusernm.Text.Trim();
            string pwd = this.txtpwd.Text.Trim();
            if (!host.Contains(":"))
            {
                host = string.Format("{0}:{1}", host, point);
            }
            if (!host.Contains("ftp"))
            {
                host = string.Format("{0}{1}", "ftp://", host);
            }
            FTPHelp ftp = new FTPHelp(host, usernm, pwd);
        
            foreach (ModelInfo o in this.listBox1.Items)
            {
                ftp.UploadFile(string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", 
                    host, "Barcode","Public","Views","Models",o.ModelSource,o.package), o.FilePath);
            }
            if (ftp.ErrorMsg.Count > 0)
            {
                string msg = string.Empty;
                foreach (string s in ftp.ErrorMsg)
                {
                    msg += s;
                }
                MessageBox.Show(msg);
            }
        }
    }

    public class ModelInfo
    {
        public string Name { get; set; }
        public string package { get; set; }
        public string FilePath { get; set; }

        public string ModelSource { get; set; }

        public override string ToString()
        {
            return this.FilePath;
        }
    }
}
