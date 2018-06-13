using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SDPCRL.CORE
{
    public class LibXmlAttributeAttribute : XmlAttributeAttribute
    {
        private string _controlNm;
        public LibXmlAttributeAttribute(string controlNm)
            :base()
        {
            this._controlNm = controlNm;
        }
        public string ControlNm
        {
            get { return _controlNm; }
        }
    }

    public class LibReSourceAttribute : Attribute
    {
        private string _reSource;
        public LibReSourceAttribute(string reSource)
            : base()
        {
            this._reSource = reSource;
        }
        public string Resource
        {
            get { return _reSource; }
        }
    }
}
