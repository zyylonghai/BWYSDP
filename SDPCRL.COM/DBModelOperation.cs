using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SDPCRL.COM.ModelManager;
using SDPCRL.COM.Constant;
using SDPCRL.CORE.FileUtils;
using SDPCRL.CORE;

namespace SDPCRL.COM
{
    /// <summary>数据模型操作类</summary>
    public class DBModelOperation
    {
        #region 私有属性
        private string _constantPath = @"F:\zyyinfo\projects\BWYSDP\SDPCRL.COM\Constant\DataContext.cs";
        private  string _tbspan = "        ";
        private string _dsSpan = "    ";
        private string _fieldSpan = "            ";
        private string _summary = "/// <summary>{0}</summary>";
        private string _beginObjStr = "/*begin {0}*/";
        private string _endObjStr = "/*end {0}*/";
        private string _classStr = "public class {0}";
        #endregion

        #region 私有方法

         private void AppendDS(StringBuilder strBuilder, string dataSourceName,string displayName)
         {
             strBuilder.AppendFormat(_dsSpan + _beginObjStr, dataSourceName);
             strBuilder.Append(Environment.NewLine);
             strBuilder.AppendFormat(_dsSpan + _summary, displayName);
             strBuilder.Append(Environment.NewLine);
             strBuilder.AppendFormat(_dsSpan + _classStr, dataSourceName);
             strBuilder.Append(Environment.NewLine);
             strBuilder.Append(_dsSpan + "{");
             strBuilder.Append(Environment.NewLine);
         }
         private void AppendTB(StringBuilder strBuilder, string defineTableName, string displayName)
         {
             strBuilder.AppendFormat(_tbspan + _summary, displayName);
             strBuilder.Append(Environment.NewLine);
             strBuilder.AppendFormat(_tbspan + _classStr, defineTableName);
             strBuilder.Append(Environment.NewLine);
             strBuilder.Append(_tbspan + "{");
             strBuilder.Append(Environment.NewLine);
         }
         private void AppendField(StringBuilder strBuilder, string fieldName, string displayName,string tableName,int fieldType)
         {
             strBuilder.AppendFormat(_fieldSpan + _summary, displayName);
             strBuilder.Append(Environment.NewLine);
             strBuilder.Append(_fieldSpan + "public FieldObj " + fieldName + " {get{" + string.Format(" return new FieldObj({0},{1},{2});", GetDoubleQuotation(fieldName), fieldType, GetDoubleQuotation(tableName)) + " } set { }}");
             strBuilder.Append(Environment.NewLine);
         }
         private void EndDSAppend(StringBuilder strBuilder,string dataSourceName)
         {
             strBuilder.Append(_dsSpan +"}");
             strBuilder.Append(Environment.NewLine);
             strBuilder.AppendFormat(_dsSpan + _endObjStr, dataSourceName);
         }
         private void EndTBAppend(StringBuilder strBuilder)
         {
             strBuilder.Append(_tbspan + "}");
         }
         
         private void DoCreateDataSourceObject(DataSource dataSource)
         {
             StringBuilder context = new StringBuilder();
             if (dataSource != null)
             {
                 context.Append(Environment.NewLine);
                 //AppendDS(context, dataSource.DataSourceName, dataSource.DSDisplayText);
                 foreach (DefineTable defTB in dataSource.DefTables)
                 {
                     AppendTB(context, defTB.TableName, defTB.DisplayName);
                     foreach (DataTableStruct dt in defTB.TableStruct)
                     {
                         foreach (LibField field in dt.Fields)
                         {
                             if (!LibSysUtils.IsNULLOrEmpty(field.AliasName))
                             {
                                 AppendField(context, field.AliasName, field.DisplayName,dt.Name,field.FieldType);
                             }
                             else
                                 AppendField(context, field.Name, field.DisplayName,dt.Name ,field.FieldType);
                         }
                     }
                 }
                 EndTBAppend(context);
                 context.Append(Environment.NewLine);
                 EndDSAppend(context,dataSource .DataSourceName);
             }
             FileOperation fileOperation = new FileOperation();
             fileOperation.FilePath = _constantPath;
             fileOperation.Encoding = LibEncoding.Unicode;
             string fileContex=fileOperation.ReadFile();
             string flag = "/*DataSourceObject*/";
             string beginobjStr = string.Format(_beginObjStr, dataSource.DataSourceName);
             string endobjStr = string.Format(_endObjStr, dataSource.DataSourceName);
             if (fileContex.Contains(beginobjStr))
             {
                 int beginObjIndex = fileContex.IndexOf(beginobjStr);
                 int endObjIndex=fileContex .IndexOf (endobjStr );
                 string oldObjStr = fileContex.Substring(beginObjIndex, (endObjIndex + endobjStr.Length)- beginObjIndex);
                 fileContex =fileContex.Replace(oldObjStr,context.ToString());
             }
             else
             {
                 int beginIndex = fileContex.IndexOf(flag);
                 fileContex = fileContex.Insert(beginIndex + flag.Length, context.ToString());
             }
             fileOperation.WritText(fileContex);
         }

         private string GetDoubleQuotation(object val)
         {
             return string.Format("\"{0}\"", val);
         }
        #endregion
        #region 公开方法
        /// <summary>创建数据对象</summary>
        /// <param name="dataSource"></param>
         public void CreateDataSourceObj(DataSource dataSource)
         {
             DoCreateDataSourceObject(dataSource);
         }
        #endregion
    }
}
