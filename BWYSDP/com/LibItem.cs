using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWYSDP.com
{
    public class LibItem
    {
        private int _key = 0;
        private string _value = string.Empty;
        public LibItem(int pKey, string pValue)
        {
            _key = pKey;
            _value = pValue;

        }
        public override string ToString()
        {
            return this._value;
        }

        public int Key
        {
            get
            {
                return this._key;
            }
            set
            {
                this._key = value;
            }
        }

        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }

        }

    }
}
