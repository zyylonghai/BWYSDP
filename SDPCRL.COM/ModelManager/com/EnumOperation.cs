using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SDPCRL.CORE;

namespace SDPCRL.COM.ModelManager.com
{
    public class EnumOperation
    {
        public static string GetFieldTypeText(LibFieldType fieldtype)
        {
            return "";
        }

        /// <summary>根据枚举的resource获取枚举的对象，并转为相等的整数值</summary>
        /// <param name="fieldTypeText"></param>
        /// <returns></returns>
        public static int GetFieldTypeValue(string fieldTypeText)
        {
            int result = -1;
            Type type = typeof(LibFieldType);
            Array array = type.GetEnumValues();
            LibFieldType fieldType;
            for (int i = 0; i < array.Length; i++)
            {
                fieldType = (LibFieldType)array.GetValue(i);
                string resource = ReSourceManage.GetResource(fieldType);
                if (string.Compare(resource, fieldTypeText, true) == 0)
                {
                    result = (int)fieldType;
                    break;
                }
            }
            #region 旧代码
            //FieldInfo[] fieldinfos = type.GetFields();
            //foreach (FieldInfo field in fieldinfos)
            //{
            //    object[] attrArray = field.GetCustomAttributes(typeof(LibReSourceAttribute), true);
            //    if (attrArray.Length > 0)
            //    {
            //        LibReSourceAttribute resource = attrArray[0] as LibReSourceAttribute;
            //        if (string.Compare(resource.Resource, fieldTypeText, true) == 0)
            //        {
            //           Array array= type.GetEnumValues();
            //           LibFieldType fieldtype =(LibFieldType)array.GetValue(0);
            //        }
            //    }
            //}
            #endregion
            return result;
        }


    }
}
