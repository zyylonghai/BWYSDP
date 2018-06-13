using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.CORE
{
   public  class LibExceptionBase: Exception
    {
        public LibExceptionBase()
            : base()
        {
 
        }

        public LibExceptionBase(string message)
            : base(message)
        {
 
        }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
    }
}
