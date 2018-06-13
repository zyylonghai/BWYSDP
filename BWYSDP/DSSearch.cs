using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BWYSDP
{
    public partial class DSSearch : LibFormBase
    {
        public DSSearch()
        {
            InitializeComponent();
        }

        protected override void DoSetParam(string tag, params object[] param)
        {
            base.DoSetParam(tag, param);
        }
        protected override void ReturnParam(ref string tag, Dictionary<object, object> param)
        {
            base.ReturnParam(ref tag, param);
            param.Add("a", "nimei");
            param.Add("b", "aaaa");
            tag = "returntest";
        }
    }
}
