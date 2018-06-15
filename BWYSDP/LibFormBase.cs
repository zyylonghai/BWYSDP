using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDPCRL.CORE;

namespace BWYSDP
{
    public class LibFormBase : Form, ILibEventListener
    {
        #region 
        private LibFormBase _caller;
        private ParamContainer _paramContain
        {
            get { return ParamContainer.GetInstance(); }
        }

        #endregion
        public LibFormBase()
        {

        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            ReturnParam(ref _paramContain.RefTag, _paramContain.FormCommunitationAgrs);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoSetParam(_paramContain.Tag, _paramContain.FormParam);
        }

        protected virtual void ReturnParam(ref string tag, Dictionary<object, object> param)
        {

        }

        protected virtual void DoSetParam(string tag, params object[] param)
        {

        }

        public void WakeUpForm<T>(string tag, params object[] param)
        {

            _paramContain.Tag = tag;
            _paramContain.FormParam = param;
            T called = Activator.CreateInstance<T>();
            FormCommunicationLine communication = new FormCommunicationLine(this, called, _paramContain);
            Form entity = called as Form;
            entity.Show();
        }

        #region LibEventListener 接口实现
        public virtual void DoFormAcceptMsg(string tag, Dictionary<object, object> agrs)
        {

        }
        #endregion

    }

    /// <summary>窗体之间，呼叫者和被叫着的通讯类</summary>
    public class FormCommunicationLine
    {
        private LibFormBase _caller;
        private LibFormBase _called;
        private ParamContainer _paramContain;
        public FormCommunicationLine(object caller, object called, ParamContainer paramcontainer)
        {
            this._called = called as LibFormBase;
            this._caller = caller as LibFormBase;
            this._paramContain = paramcontainer;
            if (called != null)
            {
                this._called.FormClosing += new FormClosingEventHandler(called_FormClosing);
                this._called.Load += new EventHandler(called_Load);
            }
        }

        void called_Load(object sender, EventArgs e)
        {
            LibEventManager.SubscribeEvent(_caller, LibEventType.FormCommunitation);
        }

        void called_FormClosing(object sender, FormClosingEventArgs e)
        {
            LibEventManager.TouchEvent(_caller, LibEventType.FormCommunitation, _paramContain.RefTag, _paramContain.FormCommunitationAgrs);
            LibEventManager.LogOutListener(_caller);
            _paramContain.FormCommunitationAgrs.Clear();
        }
    }

    /// <summary>参数容器</summary>
    public class ParamContainer
    {
        private static Dictionary<object, object> _formCommunitationAgrs;
        private static ParamContainer _paramContainer;
        private static readonly object locker = new object();
        public string RefTag = string.Empty;
        public string Tag
        {
            get;
            set;
        }
        public object[] FormParam
        {
            get;
            set;
        }

        public Dictionary<object, object> FormCommunitationAgrs
        {
            get
            {
                if (_formCommunitationAgrs == null)
                    _formCommunitationAgrs = new Dictionary<object, object>();
                return _formCommunitationAgrs;
            }
            set { _formCommunitationAgrs = value; }
        }

        private ParamContainer() { }
        public static ParamContainer GetInstance()
        {
            if (_paramContainer == null)
            {
                lock (locker)
                {
                    if (_paramContainer == null)
                        _paramContainer = new ParamContainer();
                }
            }
            return _paramContainer;
        }

    }
}
