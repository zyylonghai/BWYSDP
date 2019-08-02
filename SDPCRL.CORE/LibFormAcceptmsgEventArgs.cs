using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.CORE
{
   public class LibFormAcceptmsgEventArgs:LibEventArgs 
    {
       public string Tag { get; set; }
       public Dictionary<object, object> ArgsDic { get; set; }
    }

    public class LibSqlExceptionEventArgs : LibEventArgs
    {
        public Exception Exception { get; set; }
    }
}
