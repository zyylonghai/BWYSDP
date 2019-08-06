using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    [Serializable ]
    public class ErrorMessage
    {
        public string Message { get; set; }
        public string Stack { get; set; }
    }
    [Serializable]
    public class LibMessage
    {
        public string Message { get; set; }
        public LibMessageType MsgType { get; set; }
    }
}
