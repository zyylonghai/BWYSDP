using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using SDPCRL.CORE;

namespace BWYSDP.com
{
    public class LibTreeNode : TreeNode
    {

        #region 公开属性
        /// <summary> </summary>
        public string NodeId { get; set; }
        /// <summary>原始名称</summary>
        public string OriginalName { get; set; }
        /// <summary>所属包</summary>
        public string Package { get; set; }

        /// <summary>节点类型</summary>
        public NodeType NodeType { get; set; }
        #endregion

        #region 构造函数
        public LibTreeNode()
            : base()
        {

        }
        public LibTreeNode(string text)
            : base(text)
        {

        }
        #endregion

        public void CopyTo(LibTreeNode newNode)
        {
            if (newNode != null)
            {
                PropertyInfo[] propertys = newNode.GetType().GetProperties();
                foreach (PropertyInfo p in propertys)
                {
                    PropertyInfo info = this.GetType().GetProperty(p.Name);
                    if (info.GetSetMethod() != null)
                        p.SetValue(newNode, info.GetValue(this, null), null);
                }
                //newNode.NodeType = this.NodeType;
                //newNode.Name = this.Name;
                //newNode.Text = this.Text;
                //newNode.OriginalName = this.OriginalName;
                //newNode.Package = this.Package;
            }
            else
            {
                throw new LibExceptionBase("参数newNode不允许为null");
            }
        }
    }
}
