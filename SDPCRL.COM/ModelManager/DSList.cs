using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;

namespace SDPCRL.COM.ModelManager
{
    [Serializable]
    public class DSList
    {
        public LibCollection<DSInfo> DSInfoCollection { get; set; }
    }

    [Serializable]
    public class DSInfo
    {
        [LibAttribute("DSID")]
        public int DSID { get; set; }
        [LibAttribute("DSName")]
        public string Name { get; set; }
        [LibAttribute("DSDisplay")]
        public string DISPLAYTEXT { get; set; }
        [LibAttribute("DSPackage")]
        public string PACKAGE { get; set; }
    }
}
