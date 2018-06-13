using System;
using System.Collections;
using System.Collections.Generic;

namespace SDPCRL.CORE
{
    /// <summary> 事件管理者</summary>
    public class LibEventManager
    {
        private static Hashtable _listenerList = new Hashtable();
        private static LibEventListener _eventListener;

        #region 私有函数
        /// <summary>注册监听器</summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private static LibEventListener GetListener(object sender)
        {
            if (!_listenerList.ContainsKey(sender))
            {
                _eventListener = new LibEventListener(sender);
                _listenerList.Add(sender, _eventListener);
            }
            return _listenerList[sender] as LibEventListener ;
        }
        #endregion

        #region 公开函数
        /// <summary>订阅事件</summary>
        /// <param name="sender"></param>
        /// <param name="eventType"></param>
        public static void SubscribeEvent(object sender, LibEventType eventType)
        {
            switch (eventType)
            {
                case LibEventType.FormCommunitation:
                    GetListener(sender).DoSubscribeEvent(eventType);
                    break;
            }
        }
        /// <summary> 触发事件 </summary>
        /// <param name="sender"></param>
        /// <param name="eventType"></param>
        /// <param name="avgs"></param>
        public static void TouchEvent(object sender, LibEventType eventType, params object[] avgs)
        {
            switch (eventType)
            {
                case LibEventType.FormCommunitation:
                    Dictionary<object, object> param = null;
                    string tag = string.Empty;
                    if (avgs != null && avgs.Length > 1)
                    {
                        tag = avgs[0].ToString();
                        param = avgs[1] as Dictionary<object, object>;
                    }
                    GetListener(sender).eventSource.OnFormAcceptMsg(tag, param);
                    break;
            }
        }

        /// <summary>注销监听器 </summary>
        /// <param name="sender"></param>
        public static void LogOutListener(object sender)
        {
            if (_listenerList.ContainsKey(sender))
                _listenerList.Remove(sender);
        }
        #endregion
        /// <summary> 事件源</summary>
        class LibEventSource
        {
            public delegate void FormCommunitionEventHandle(string tag, Dictionary<object, object> param);
            public event FormCommunitionEventHandle DoFormAcceptMsg;

            public void OnFormAcceptMsg(string tag, Dictionary<object, object> param)
            {
                if (DoFormAcceptMsg != null)
                    DoFormAcceptMsg(tag, param);
            }
            public LibEventSource(object sender)
            {

            }
            public LibEventSource()
            {

            }
        }
        /// <summary>事件监听器</summary>
        class LibEventListener
        {
            #region 私有变量
            private LibEventSource _eventSource;
            private object _obj;
            #endregion

            #region 构造函数
            public LibEventListener(object obj)
            {
                _obj = obj;
            }
            #endregion
            public LibEventSource eventSource
            {
                get
                {
                    if (_eventSource == null)
                    {
                        _eventSource = new LibEventSource();
                    }
                    return _eventSource;
                }
            }
            /// <summary>订阅事件</summary>
            public void DoSubscribeEvent(LibEventType eventType)
            {
                switch (eventType)
                {
                    case LibEventType.FormCommunitation:
                        eventSource.DoFormAcceptMsg += new LibEventSource.FormCommunitionEventHandle(eventSource_DoFormAcceptMsg);
                        break;
                }
            }

            public void DoUnSubscribeEvent(LibEventType eventType)
            {
                switch (eventType)
                {
                    case LibEventType.FormCommunitation:
                        eventSource.DoFormAcceptMsg -= new LibEventSource.FormCommunitionEventHandle(eventSource_DoFormAcceptMsg);
                        break;
                }
            }

            private void eventSource_DoFormAcceptMsg(string tag, Dictionary<object, object> agrs)
            {
                ILibEventListener eventListener = _obj as ILibEventListener;
                eventListener.DoFormAcceptMsg(tag, agrs);
            }
        }
    }


    public interface ILibEventListener
    {
        void DoFormAcceptMsg(string tag, Dictionary<object, object> agrs);
    }

    public class LibParamEventArgs : EventArgs
    {
        private object[] _param;
        public object Param
        {
            get;
            set;
        }
        public LibParamEventArgs(params object[] param)
        {
            this._param = param;
        }
    }

    public enum LibEventType
    {
        /// <summary>窗体之间通讯事件 </summary>
        FormCommunitation = 0,
        /// <summary>点击事件</summary>
        OnClick = 1
    }
}
