using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SDPCRL.DAL.COM
{
    public class LibDbParameter 
    {
        public string ParameterNm { get; set; }
        public DbType DbType { get; set; }

        public int Size { get; set; }

        public ParameterDirection Direction { get; set; }

        public object Value { get; set; }
    }
}
