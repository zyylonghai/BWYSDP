using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.CORE
{
    public abstract class LibLock
    {
        public string Key { get; set; }

        public LibLockStatus Status { get; set; }

        public abstract void Lock();
        public abstract void UnLock();
    }
    public enum LibLockStatus
    {
        UnLock=0,
        Lock=1

    }
}
