using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWYResFactory
{
    class ResManager : Res, IResManager
    {
        public string GetResByKey(string key)
        {
            return ResourceManager.GetString(key);
        }

        public new string SysDBNm
        {
            get
            {
                return Res.SysDBNm;
            }
        }
    }

    public interface IResManager
    {
        string GetResByKey(string key);
        string SysDBNm { get; }
    }
}
