using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWYResFactory
{
   internal class ResManager : Res, IResManager
    {
        public string GetResByKey(string key)
        {
            return ResourceManager.GetString(key);
        }

        public new string SysDBNm
        {
            get
            {
                return Res.SysDBNm;
            }
        }
        public new string SQLSelect
        {
            get 
            {
                return Res.SQLSelect;
            }
        }
        public new string SQLFrom
        {
            get { return Res.SQLFrom; }
        }
        public new string SQLWhere
        {
            get { return Res.SQLWhere; }
        }
        public new string SQLAnd
        {
            get { return Res.SQLAnd; }
        }

        public new string SQLLeftJoin
        {
            get { return Res.SQLLeftJoin; }
        }

        public new string SQLOn
        {
            get { return Res.SQLOn; }
        }
        public new string SQLOr
        {
            get { return Res.SQLOr; }
        }
        public new string SQLUpdate
        {
            get { return Res.SQLUpdate; }
        }
        public new string LogDBNm
        {
            get { return Res.LogDBNm; }
        }

    }

    public interface IResManager
    {
        string GetResByKey(string key);
        string SysDBNm { get; }
        string SQLSelect { get; }
        string SQLFrom { get; }
        string SQLWhere { get; }
        string SQLAnd { get; }
        string SQLLeftJoin { get; }
        string SQLOn { get; }
        string SQLOr { get; }
        string SQLUpdate { get; }
        string LogDBNm { get; }

    }
}
