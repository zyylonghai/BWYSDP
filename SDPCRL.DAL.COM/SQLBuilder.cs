using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWYResFactory;
using SDPCRL.CORE;
using System.Text.RegularExpressions;

namespace SDPCRL.DAL.COM
{
    public class SQLBuilder
    {
        public string GetSQL(string tableNm,string[] fields,string whereStr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ResFactory.ResManager.SQLSelect);
            foreach (string field in fields)
            {
                if(builder .Length !=ResFactory.ResManager .SQLSelect .Length)
                {
                    builder.Append(SysConstManage.Comma);
                }
                builder.AppendFormat(" {0}", field);
            }

            builder.AppendFormat(" {0}",ResFactory.ResManager.SQLFrom);
            builder.AppendFormat(" {0}", tableNm);
            builder.AppendFormat(" {0} {1}", ResFactory.ResManager.SQLWhere, whereStr);
            return builder.ToString();
        }


        public string GetSQL(string tableNm, string[] fields, WhereObject where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append( ResFactory.ResManager.SQLSelect);
            foreach (string field in fields)
            {
                if (builder.Length != ResFactory.ResManager.SQLSelect.Length)
                {
                    builder.Append(SysConstManage.Comma);
                }
                builder.AppendFormat(" {0}", field);
            }
            builder.AppendFormat(" {0}", ResFactory.ResManager.SQLFrom);
            builder.AppendFormat(" {0}", tableNm);
            if (!string.IsNullOrEmpty(where.WhereFormat))
            {
                return string.Format("EXEC sp_executesql N'{0} where {1}',{2}",builder .ToString (),where.WhereFormat ,where .ValueTostring);
            }
            return string.Format("EXEC sp_executesql N'{0}'", builder.ToString());
        }

        public WhereObject Where(string format, params object[] values)
        {
            WhereObject wobj = new WhereObject();
            wobj.WhereFormat = format;
            wobj.Values = values;
            return wobj;
        }
    }
   public class WhereObject
    {
        private string _whereformat = string.Empty;
        private string[] _params;
        public string WhereFormat
        {
            get
            {
                MatchCollection matchs = Regex.Matches(_whereformat, @"{\w*}+");
                int index = 0;
                if (matchs.Count > 0)
                    _params = new string[matchs.Count];
                foreach (Match item in matchs)
                {
                    index = Convert.ToInt32(item.Value.Replace("{", "").Replace("}", ""));
                    _params[index] = string.Format("@V{0}", index);
                    _whereformat = _whereformat.Replace(item.Value, _params[index]);
                }
                return _whereformat;
            }
            set
            {
                _whereformat = value;
            }
        }

        public object[] Values
        {
            get;
            set;
        }

        public string ValueTostring
        {
            get
            {
                if (_params !=null && _params.Length > 0)
                {
                    StringBuilder partype = new StringBuilder();
                    StringBuilder val = new StringBuilder();
                    for (int n = 0; n < _params.Length; n++)
                    {
                        if (partype.Length > 0)
                        {
                            partype.Append(",");
                            val.Append(",");
                        }
                        switch (Values[n].GetType().Name)
                        {
                            case "String":
                                partype.AppendFormat("{0} nvarchar({1})", _params[n], Values[Convert.ToInt32(_params[n].Substring(2))].ToString().Length);
                                val.AppendFormat("{0}='{1}'", _params[n], Values[Convert.ToInt32(_params[n].Substring(2))]);
                                break;
                            case "Int32":
                                partype.AppendFormat("{0} int)", _params[n]);
                                val.AppendFormat("{0}={1}", _params[n], Values[Convert.ToInt32(_params[n].Substring(2))]);
                                break;
                            case "DateTime":
                                break;
                        }
                    }
                    return string.Format("N'{0}',{1}", partype.ToString(), val.ToString());
                }
                else
                    return string.Empty;
            }
        }
    }

}
