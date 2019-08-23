using SDPCRL.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    public enum Language
    {
        /// <summary>中文（简体）</summary>
        [LibReSource("中文（简体）")]
        CHS = 0,
        /// <summary>中文（繁体）</summary>
        [LibReSource("中文（繁体）")]
        CHS_F =1,
        /// <summary>英文</summary>
        [LibReSource("英文")]
        ENG = 2
    }
}
