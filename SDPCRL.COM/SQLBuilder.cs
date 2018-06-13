using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.COM.Constant;
using System.Data;

namespace SDPCRL.COM
{
    public class SQLBuilder
    {
        #region 私有变量
        private StringBuilder _sql;
        private string _tableName;
        private int _dsId;
        private FieldObj[] _fields;
        #endregion


        #region 公有变量
        public string TableName { get { return _tableName; } set { _tableName = value; } }
        public int DSID { get { return _dsId; } set { _dsId = value; } }
        #endregion

        #region 构造函数
        public SQLBuilder()
        {
            _sql = new StringBuilder();
        }
        public SQLBuilder(string tableName)
            : base()
        {
            this._tableName = tableName;
        }
        public SQLBuilder(int dsId)
            : base()
        {
            this._dsId = dsId;
        }
        #endregion

        #region 私有函数

        public SQLBuilder Select(params FieldObj[] fields)
        {
            Func<string, string, string> te = testecondition;
            //this._fields = fields; Join(new string[] { },);
            //List<studentinfo.defineTable1> lis = new List<studentinfo.defineTable1>();lis.Select (i=>i.

            return this;
        }
        public static string testecondition(string a, string b)
        {
            return a + b;
        }

        public SQLBuilder Join(string[] tableNames, Func<string> onCondition)
        {
            return this;
        }

        public SQLBuilder LeftJoin()
        {
            return this;
        }

        public SQLBuilder RightJoin()
        {
            return this;
        }

        public SQLBuilder Where()
        {
            EntityBuilder<studentinfo.defineTable1> def = new EntityBuilder<studentinfo.defineTable1>();
            //def.Select(i => new FieldObj[] { i.address, i.age, i.GRADE, i.ruxueriqi, i.StudentName });
            return this;
        }

        #endregion

        #region 公有函数

        #endregion

        //class SelectObj
        //{
        //    private FieldObj[] _fieldObjs;
        //    private OnObj _onObj;
        //    private string[] _tableArray;


        //    public FieldObj[] FieldObjs { get { return _fieldObjs; } }
        //    public string[] TableNameArray { get { return _tableArray; } }
        //    public OnObj OnObj { get { return _onObj; } }
        //    public SelectObj(FieldObj[] fields)
        //    {
        //        this._fieldObjs = fields;
        //    }

        //    public OnObj LeftJoin(params string[] tableNames)
        //    {
        //        _tableArray = tableNames;
        //        return _onObj = new OnObj();
        //    }
        //}

        //class OnObj
        //{
        //    private  FieldObj[,] _fields;

        //    public FieldObj[,] OnCondition { get { return _fields; } }
        //    public WhereObj On(FieldObj [,] fields)
        //    {

        //    }
        //}
        //class WhereObj
        //{
        //    public void where<T>() {}
        //    public void test()
        //    {

        //    }
        //}


    }

    //public class ceshi
    //{
    //    public void test()
    //    {
    //        SQLBuilder builder = new SQLBuilder();
    //    }
    //}

}
