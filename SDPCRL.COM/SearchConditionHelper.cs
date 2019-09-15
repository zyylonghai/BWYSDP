using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.COM
{
    public class SearchConditionHelper
    {
        public static void AnalyzeSearchCondition(List<LibSearchCondition> conds, StringBuilder whereformat, ref object[] values)
        {
            int n = 0;
            LibSearchCondition precond = null;
            int len = 0;
            foreach (LibSearchCondition item in conds)
            {
                if (whereformat.Length > 0)
                {
                    if (precond != null)
                        whereformat.AppendFormat(" {0} ", precond.Logic.ToString());
                }
                switch (item.Symbol)
                {
                    case SmodalSymbol.Equal:
                        whereformat.Append("" + item.FieldNm + "={" + n + "}");
                        len = 1;
                        break;
                    case SmodalSymbol.MoreThan:
                        whereformat.Append("" + item.FieldNm + ">{" + n + "}");
                        len = 1;
                        break;
                    case SmodalSymbol.LessThan:
                        whereformat.Append("" + item.FieldNm + "<{" + n + "}");
                        len = 1;
                        break;
                    case SmodalSymbol.Contains:
                        whereformat.Append("" + item.FieldNm + " like {" + n + "}");
                        item.Values[0] = string.Format("%{0}%", item.Values[0]);
                        len = 1;
                        break;
                    case SmodalSymbol.Between:
                        whereformat.Append("" + item.FieldNm + " between {" + n + "} and {" + (n = n + 1) + "}");
                        len = 2;
                        break;
                    case SmodalSymbol.NoEqual:
                        whereformat.Append("" + item.FieldNm + "!={" + n + "}");
                        len = 1;
                        break;
                }
                n++;
                if (item.Values != null)
                {
                    for (int i = 0; i < len; i++)
                    {
                        //if (LibSysUtils.IsNULLOrEmpty(o)) continue;
                        Array.Resize(ref values, values.Length + 1);
                        values[values.Length - 1] = item.Values[i];
                    }
                }
                precond = item;
            }
        }
    }
}
