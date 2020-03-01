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
                AddNodes(fileinfos, datasourceNode);
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
                AddNodes(fileinfos, formsourceNode);


                TreeNode permissionNode = new TreeNode("权限模型");
                permissionNode.Name = SysConstManage.PermissionSourceNm;
                this.treeView1.Nodes.Add(permissionNode);
                fileOperation.FilePath = string.Format(@"{0}\{1}", SysConstManage.ModelPath, SysConstManage.PermissionSourceNm);
                fileinfos = fileOperation.SearchAllFileInfo();
                AddNodes(fileinfos, permissionNode);
            }
        }

        private void AddNodes(List<LibFileInfo> fileinfos, TreeNode pnode)
        {
            TreeNode node = null;
            foreach (var item in fileinfos)
            {
                if (pnode.Nodes.ContainsKey(item.Folder))
                {
                    node = new TreeNode(item.FileName);
                    node.Name = item.FileName;
                    pnode.Nodes[item.Folder].Nodes.Add(node);
                }
                else
                {
                    node = new TreeNode(item.Folder);
                    node.Name = item.Folder;
                    pnode.Nodes.Add(node);

                    node = new TreeNode(item.FileName);
                    node.Name = item.FileName;
                    pnode.Nodes[item.Folder].Nodes.Add(node);
                }
            }
        }
    }
}
