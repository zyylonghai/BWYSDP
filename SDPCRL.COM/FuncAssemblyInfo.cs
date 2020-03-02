using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    [Serializable]
    public class FuncAssemblyInfo
    {
        public string FuncID { get; set; }
        public string AssemblyName { get; set; }
        public string TypeFullName { get; set; }
    }
}
