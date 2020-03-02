using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.CORE
{
    public class LibProgAttribute : Attribute
    {
        string _progid = string.Empty;

        public string ProgId { get { return this._progid; } }
        public LibProgAttribute(string progid)
        {
            this._progid = progid;
        }
    }
}
