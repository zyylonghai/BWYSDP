using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.COM.Constant;
using System.Data;
using System.Runtime.InteropServices;

namespace SDPCRL.COM
{
    //public class EntityBuilder<T> :CommandTextBuilder 
    //    where T : class
    //{
    //    #region 私有变量
    //    //private FieldObj[] _selectField;
    //    //private List<ConditionObj> _onCondition;
    //    private WhereConditionObj _whereCond;
    //    private T _a;

    //    #endregion

    //    #region 公有变量

    //    public T A { get { return _a; } }
    //    /// <summary></summary>
    //    //public FieldObj[] SelectField { get { return _selectField; } }
    //    //public List<ConditionObj> OnCondition { get { return _onCondition; } }

    //    #endregion

    //    #region 构造函数
    //    public EntityBuilder()
    //    {
    //        //_onCondition = new List<ConditionObj>();
    //        this.OnCondition = new List<ConditionObj>();
    //    }
    //    #endregion

    //    #region 公有函数

    //    public EntityBuilder<T> Select(Func<T, FieldObj[]> fields)
    //    {
    //        T obj = Activator.CreateInstance<T>();
    //        this.SelectFields = fields(obj);
    //        this.SelectTable = obj.GetType().FullName;
    //        //_selectField = fields(obj);
    //        return this;
    //    }

    //    public EntityBuilder<T> Join<BeJoinTB, JoinTB>(Func<BeJoinTB, FieldObj> fieldA, Func<JoinTB, FieldObj> fieldB)
    //    {
    //        BeJoinTB beJoin = Activator.CreateInstance<BeJoinTB>();
    //        JoinTB jointb = Activator.CreateInstance<JoinTB>();
    //        AddRelateTable<JoinTB>();
    //        AddCondition(fieldA(beJoin), fieldB(jointb), JoinType.Join);
    //        return this;
    //    }

    //    public EntityBuilder<T> LeftJoin<BeJoinTB, JoinTB>(Func<BeJoinTB, FieldObj> fieldA, Func<JoinTB, FieldObj> fieldB)
    //    {
    //        BeJoinTB beJoin = Activator.CreateInstance<BeJoinTB>();
    //        JoinTB jointb = Activator.CreateInstance<JoinTB>();
    //        AddRelateTable<JoinTB>();
    //        AddCondition(fieldA(beJoin), fieldB(jointb), JoinType.LeftJoin);
    //        return this;
    //    }
    //    public EntityBuilder<T> RightJoin<BeJoinTB, JoinTB>(Func<BeJoinTB, FieldObj> fieldA, Func<JoinTB, FieldObj> fieldB)
    //    {
    //        BeJoinTB beJoin = Activator.CreateInstance<BeJoinTB>();
    //        JoinTB jointb = Activator.CreateInstance<JoinTB>();
    //        AddRelateTable<JoinTB>();
    //        AddCondition(fieldA(beJoin), fieldB(jointb), JoinType.RightJoin);
    //        return this;
    //    }

    //    public WhereConditionObj Where<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value)
    //    {
    //        if (_whereCond == null)
    //            _whereCond = new WhereConditionObj(this);
    //        _whereCond.AddCondition<TB>(field, symbol, value);
    //        return _whereCond;
    //    }

    //    #endregion

    //    #region 私有函数
    //    private void AddCondition(FieldObj fieldA, FieldObj fieldB, JoinType joinType)
    //    {
    //        ConditionObj conditionObj = new ConditionObj();
    //        conditionObj.FieldA = fieldA;
    //        conditionObj.FieldB = fieldB;
    //        conditionObj.Symbol = SQLSymbolSign.Equal;
    //        conditionObj.JoinType = joinType;
    //        this.OnCondition.Add(conditionObj);
    //        //_onCondition.Add(conditionObj);
    //    }
    //    private void AddRelateTable<TBNM>()
    //    {
    //        Type type = typeof(TBNM);
    //        int len = this.RelateTables.Length;
    //        Array.Resize(ref this.RelateTables, len + 1);
    //        string tableNm = type.FullName;
    //        if (!this.RelateTables.Contains(tableNm))
    //        {
    //            this.RelateTables[len] = tableNm;
    //        }
    //    }
    //    #endregion
    //    public class WhereConditionObj
    //    {
    //        private List<ConditionObj> _whereCondition;
    //        private EntityBuilder<T> _dmlOperation;

    //        public List<ConditionObj> WhereCondition { get { return _whereCondition; } set { _whereCondition = value; } }

    //        public WhereConditionObj(EntityBuilder<T> dmlOperation)
    //        {
    //            this._dmlOperation = dmlOperation;
    //            _whereCondition = new List<ConditionObj>();
    //        }

    //        public WhereConditionObj And<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value)
    //        {
    //            AddCondition<TB>(field, symbol, value);
    //            return this;
    //        }

    //        public WhereConditionObj Or<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value)
    //        {
    //            AddCondition<TB>(field, symbol, value); 
    //            return this;
    //        }

    //        //public DataTable ToDataTable()
    //        //{

    //        //}

    //        public void AddCondition<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value)
    //        {
    //            TB obj = Activator.CreateInstance<TB>();
    //            FieldObj fieldObj = field(obj);
    //            fieldObj.FieldValue = value;
    //            ConditionObj cond = new ConditionObj();
    //            cond.FieldA = fieldObj;
    //            cond.Symbol = symbol;
    //            _whereCondition.Add(cond);
    //        }
    //    }
    //}

    public class EntityBuilder<T> : CommandTextBuilder
        where T : class
    {
        private FieldObj[] _selectField;
        public EntityBuilder()
        {
        }

        public EntityBuilder<T> Select<TB>(Func<TB, FieldObj[]> fields)
        {
            TB obj = Activator.CreateInstance<TB>();
            this.SelectFields = fields(obj);
            return this;
        }
        public EntityBuilder<T> Where(Func<WhereConditionObj, ConditionObj[]> whereCondition)
        {
            WhereConditionObj cond = new WhereConditionObj();
            this.WhereCondition = whereCondition(cond);
            return this;
        }

        #region 私有函数
        //private void AddCondition<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value)
        //{
        //    TB obj = Activator.CreateInstance<TB>();
        //    FieldObj fieldObj = field(obj);
        //    fieldObj.FieldValue = value;
        //    ConditionObj cond = new ConditionObj();
        //    cond.FieldA = fieldObj;
        //    cond.Symbol = symbol;
        //    _whereCondition.Add(cond);
        //}
        #endregion

        public class WhereConditionObj
        {
            public ConditionObj And<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value)
            {

                return CreateConditionObj<TB>(field, symbol, value, ConditonModel.And);
            }
            public ConditionObj Or<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value)
            {
                return CreateConditionObj<TB>(field, symbol, value, ConditonModel.Or);
            }

            public ConditionObj In<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value)
            {
                return CreateConditionObj<TB>(field, symbol, value, ConditonModel.In);
            }

            #region 私有函数
            private ConditionObj CreateConditionObj<TB>(Func<TB, FieldObj> field, SQLSymbolSign symbol, object value,ConditonModel model)
            {
                TB obj = Activator.CreateInstance<TB>();
                FieldObj fieldObj = field(obj);
                fieldObj.FieldValue = value;
                ConditionObj cond = new ConditionObj();
                cond.FieldA = fieldObj;
                cond.Symbol = symbol;
                cond.Model = model;
                return cond;
            }
            #endregion
        }
    }
    public class ConditionObj
    {
        public FieldObj FieldA { get; set; }
        public FieldObj FieldB { get; set; }
        public SQLSymbolSign Symbol { get; set; }
        //public JoinType JoinType { get; set; }
        public ConditonModel Model { get; set; }
    }
    //public enum JoinType
    //{
    //    Join,
    //    LeftJoin,
    //    RightJoin
    //}

    public enum ConditonModel
    {
        And=0,
        Or=1,
        In=2
    }
    public enum SQLSymbolSign
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,
        /// <summary>
        /// 大于
        /// </summary>
        Morn,
        /// <summary>
        /// 小于
        /// </summary>
        Less,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,
        /// <summary>
        /// 大于等于
        /// </summary>
        MornEqual,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessEqual,
        /// <summary>
        /// like
        /// </summary>
        Like,
        /// <summary>
        /// In
        /// </summary>
        In
    }
}
