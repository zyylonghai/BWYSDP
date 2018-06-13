using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.COM.Constant;

namespace SDPCRL.COM
{
    public class CommandTextBuilder
    {
        #region 私有变量
        private   FieldObj[] _selectField;
        private  ConditionObj[] _whereCondition;
        #endregion

        #region 公开，受保护的变量

        /// <summary>查询的字段</summary>
        protected FieldObj[] SelectFields { get { return _selectField; } set { _selectField = value; } }

        /// <summary>Where条件集合</summary>
        protected ConditionObj[] WhereCondition { get { return _whereCondition; } set { _whereCondition = value; } }

        /// <summary>要查找的表</summary>
        protected string SelectTable { get; set; }

        /// <summary>关联的表集合</summary>
        protected string[] RelateTables = { };

        #endregion

        #region 公开的方法
        protected void DoCreateCommandTex(CommandModel commdModel)
        {
            switch (commdModel)
            {
                case CommandModel.Select:

                    break;
            }
        }
        #endregion

        //protected void CreateCommdParam( List<ConditionObj> whereCondition)
        //{
        //    RelateTables[
        //}

        protected enum CommandModel
        {
            Select=1,
            Update=2,
            Delete=3,
            Insert=4
        }
    }

}
