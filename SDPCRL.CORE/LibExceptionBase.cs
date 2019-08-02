using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.CORE
{
    public class LibExceptionBase : Exception
    {
        private string _innerStackTrace = string.Empty;
        public LibExceptionBase()
            : base()
        {

        }

        public LibExceptionBase(string message)
            : base(message)
        {
           
        }

        public LibExceptionBase(string message, string innerStackTrace)
            : base(message)
        {
            this._innerStackTrace = innerStackTrace;
        }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        public override string StackTrace {
            get {
                return string.Format("{0}\\n Old StackTrace \\n {1}", base.StackTrace, this._innerStackTrace);
            }
        }

        #region 公开函数

        public void AddErrorMessage(string msg)
        {

        }
        #endregion
    }

    public interface IlibException
    {
        void BeforeThrow();
    }
}
