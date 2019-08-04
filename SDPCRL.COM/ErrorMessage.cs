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
    public enum LibMessageType
    {
        /// <summary>错误信息</summary>
        Error=1,
        /// <summary>警告信息</summary>
        Warning=2,
        /// <summary>提示信息</summary>
        Prompt = 3
    }
}
