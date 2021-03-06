using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.COM.ModelManager;

namespace SDPCRL.COM.Constant
{
    public partial class DataContext
    {
    }

    public class FieldObj
    {
        private string _fieldName = string.Empty;
        private LibFieldType _fieldType;
        private string _tableName;
        /// <summary>字段名</summary>
        public string FieldName { get { return _fieldName; } set { _fieldName = value; } }
        /// <summary>字段类型 </summary>
        public LibFieldType FieldType { get { return _fieldType; } set { _fieldType = value; } }
        /// <summary>字段值 </summary>
        public object FieldValue { get; set; }
        /// <summary>所属表名</summary>
        public string TableName { get { return _tableName; } set { _tableName = value; } }

        public FieldObj(string fieldName, int fieldType, string tableName)
        {
            this._fieldName = fieldName;
            this._fieldType =(LibFieldType) fieldType;
            this._tableName = tableName;
        }

    }

    #region 表实例对象
    /*DataSourceObject*/
    
    /*begin studentinfo*/
    /// <summary>学生信息</summary>
    public class studentinfo
    {
        /// <summary>自定义数据表</summary>
        public class defineTable1
        {
            /// <summary>学生姓名</summary>
            public FieldObj StudentName {get{ return new FieldObj("StudentName",0,"studentinfotable"); } set { }}
            /// <summary>学生性别</summary>
            public FieldObj stu_sex {get{ return new FieldObj("stu_sex",1,"studentinfotable"); } set { }}
            /// <summary>年龄</summary>
            public FieldObj age {get{ return new FieldObj("age",1,"studentinfotable"); } set { }}
            /// <summary>地址</summary>
            public FieldObj address {get{ return new FieldObj("address",0,"studentinfotable"); } set { }}
            /// <summary>户籍</summary>
            public FieldObj hj {get{ return new FieldObj("hj",0,"studentinfotable"); } set { }}
            /// <summary>入学日期</summary>
            public FieldObj ruxueriqi {get{ return new FieldObj("ruxueriqi",4,"studentinfotable"); } set { }}
            /// <summary>所在班级</summary>
            public FieldObj GRADE {get{ return new FieldObj("GRADE",0,"studentinfotable"); } set { }}
        }
    }
    /*end studentinfo*/
    #endregion
}
