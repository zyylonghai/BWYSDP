using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace SDPCRL.CORE
{
    [Serializable]
    public class LibCollection<T>:ICollection
    {
        #region 私有属性
        private ArrayList _entityArray;
        #endregion

        #region 公开属性
        public T this[int index]
        {
            get { return (T)_entityArray[index]; }
        }
        [XmlAttribute]
        public string Guid { get; set; }

        #endregion
        public void Add(T item)
        {
            if (_entityArray == null)
                _entityArray = new ArrayList();
            _entityArray.Add(item);
        }
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get 
            {
                if (_entityArray == null)
                    _entityArray = new ArrayList();
                return _entityArray.Count;
            }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator GetEnumerator()
        {
            if (_entityArray == null)
                _entityArray = new ArrayList();
            return _entityArray.GetEnumerator();
        }
    }
}
