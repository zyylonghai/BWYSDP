using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SDPCRL.CORE
{
    public sealed class LibSysUtils
    {
        public static Int32 ToInt32(object obj)
        {
            Int32 result = 0;

            if (Int32.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            else
            {
                if (IsNULLOrEmpty(obj))
                {
                    return 0;
                }
                else
                {
                    return Int32.MinValue;
                }
            }
            return result;


        }

        public static bool IsNULLOrEmpty(object obj)
        {
            if (obj == null ||obj ==DBNull .Value)
            {
                return true;
            }
            else
            {
                if (string.Compare(obj.GetType().Name, typeof(string).Name, false) == 0 && obj.ToString() == string.Empty)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// 将文本“是”或“否” 转为bool
        /// </summary>
        /// <param name="value">“是”或“否”</param>
        /// <returns></returns>
        public static bool ToBooLean(string value)
        {
            string trueValue = ReSourceManage.GetResource(BooleanValue.True);
            if (string.Compare(trueValue, value, true) == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 将bool值 转为文本“是”或“否”
        /// </summary>
        /// <param name="value">true，false</param>
        /// <returns></returns>
        public static string BooleanToText(bool value)
        {
            if (value)
            {
                return ReSourceManage.GetResource(BooleanValue.True);
            }
            return ReSourceManage.GetResource(BooleanValue.False);
        }

        public static string ToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
        /// <summary>
        /// 将枚举成员的字符串，转为对应枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumNm">枚举成员的字符串</param>
        /// <returns></returns>
        public static T ConvertToEnumType<T>(string enumNm)
        {
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if (string.Compare(item.ToString(), enumNm) == 0)
                {
                    return item;
                }
            }
            return default(T);
        }

        /// <summary>
        /// 是否数字型，转为并输出Int32的值
        /// </summary>
        /// <param name="val"></param>
        /// <param name="result">是数字则转为Int32，否则为-1</param>
        /// <returns></returns>
        public static bool IsNumberic(string val, out Int32 result)
        {
            //System.Text.RegularExpressions.Regex rex =
            //new System.Text.RegularExpressions.Regex(@"^\d+$");
            result = -1;
            //if (rex.IsMatch(val))
            //{
            //    result = Convert.ToInt32(val);
            //    return true;
            //}
            //else
            //    return false;
            if (IsNumberic(val))
            {
                result = Convert.ToInt32(val);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否数字型，转为并输出Int64的值
        /// </summary>
        /// <param name="val"></param>
        /// <param name="result">是数字则转为Int64，否则为-1</param>
        /// <returns></returns>
        public static bool IsNumberic(string val, out Int64 result)
        {
            result = -1;
            if (IsNumberic(val))
            {
                result = Convert.ToInt64(val);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断参数val 是否为纯数字
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNumberic(string val)
        {
            System.Text.RegularExpressions.Regex rex =
           new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(val))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>特殊函数，只针对表索引转为对应字符</summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static char ToCharByTableIndex(int val)
        {
            return (char)(val + 65);
        }

        public static bool Compare(object a, object b, bool ignore = false)
        {
            Type atype = a.GetType();
            Type btype = b.GetType();
            if (atype != btype) {
                if (atype.Equals(typeof(System.DBNull)) && btype.Equals(typeof(byte[])))
                {
                    return ((byte[])b).Length == 0;
                }
                else if (btype.Equals(typeof(System.DBNull)) && atype.Equals(typeof(byte[])))
                {
                    return ((byte[])a).Length == 0;
                }
                if (atype.Equals(typeof(System.DBNull)) && btype.Equals(typeof(string)))
                {
                    return string.IsNullOrEmpty(b.ToString());
                }
                else if (atype.Equals(typeof(string)) && btype.Equals(typeof(System.DBNull)))
                {
                    return string.IsNullOrEmpty(a.ToString());
                }
                else
                    throw new LibExceptionBase("请确保比较的双方类型一致");
            }
            if (atype.Equals(typeof(int)))
            {
                return (int)a == (int)b;
            }
            else if (atype.Equals(typeof(decimal)))
            {
                return (decimal)a == (decimal)b;
            }
            else if (atype.Equals(typeof(long)))
            {
                return (long)a == (long)b;
            }
            else if (atype.Equals(typeof(float)))
            {
                return (float)a == (float)b;
            }
            else if (atype.Equals(typeof(double)))
            {
                return (double)a == (double)b;
            }
            else if (atype.Equals(typeof(byte)))
            {
                return (byte)a == (byte)b;
            }
            else if (atype.Equals(typeof(byte[])))
            {
                return string.Compare(Convert.ToBase64String((byte[])a), Convert.ToBase64String((byte[])b), ignore) == 0;
            }
            else
            {
                return string.Compare(a.ToString(), b.ToString(), ignore) == 0;
            }
        }

        public static T Copy<T>(T src)
        {
            if (src == null) return default(T);
            T entity = Activator.CreateInstance<T>();
            Type srctype = src.GetType();
            PropertyInfo[] properties = entity.GetType().GetProperties();
            PropertyInfo info = null;
            foreach (PropertyInfo p in properties)
            {
                info = srctype.GetProperty(p.Name);
                if (info.GetSetMethod() != null)
                {
                    p.SetValue(entity, info.GetValue(src, null), null);
                }
            }
            return entity;
        }

        //public static object ConvertToEnumType(string enumNm,Type enumType)
        //{

        //}

        private enum BooleanValue
        {
            [LibReSource("是")]
            True = 1,
            [LibReSource("否")]
            False = 0
        }
    }

    public sealed class ReSourceManage
    {
        public static string GetResource(object enumObj)
        {
            Type t = enumObj.GetType();
            FieldInfo field = t.GetField(t.GetEnumName(enumObj));
            object[] attrArray = field.GetCustomAttributes(typeof(LibReSourceAttribute), true);
            LibReSourceAttribute resource = attrArray[0] as LibReSourceAttribute;
            return resource.Resource;
        }
    }
}
