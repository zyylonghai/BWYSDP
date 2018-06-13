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
        [LibXmlAttribute("DSID")]
        public int DSID { get; set; }
        [LibXmlAttribute("DSName")]
        public string Name { get; set; }
        [LibXmlAttribute("DSDisplay")]
        public string DISPLAYTEXT { get; set; }
        [LibXmlAttribute("DSPackage")]
        public string PACKAGE { get; set; }
    }
}
