using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SDPCRL.CORE.FileUtils
{
    /// <summary> </summary>
    public class FileOperation
    {
        #region 私有变量
        private string _filePath;
        private LibEncoding _Encoding;
        #endregion

        #region 共有属性
        /// <summary>文件路径</summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public LibEncoding Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }
        public string ExceptionMessage
        {
            get; set;
        }
        #endregion

        #region 共有方法

        /// <summary>读取文件</summary>
        /// <returns></returns>
        public string ReadFile()
        {
            string _context = string.Empty;

            try
            {
                _context = File.ReadAllText(_filePath, getEncoding());
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
            }
            return _context;
        }

        public bool WritText(string context)
        {
            try
            {
                File.WriteAllText(_filePath, context, getEncoding());
                return true;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
                return false;
            }
        }
        public bool ExistsFile()
        {
            return File.Exists(_filePath);
        }
        #endregion

        #region 私有方法
        private System.Text.Encoding getEncoding()
        {
            System.Text.Encoding encode = System.Text.Encoding.UTF8;
            switch (_Encoding)
            {
                case LibEncoding.ASCII:
                    encode = System.Text.Encoding.ASCII;
                    break;
                case LibEncoding.Unicode:
                    encode = System.Text.Encoding.Unicode;
                    break;
                case LibEncoding.UTF32:
                    encode = System.Text.Encoding.UTF32;
                    break;
                case LibEncoding.UTF7:
                    encode = System.Text.Encoding.UTF7;
                    break;
                case LibEncoding.UTF8:
                    encode = System.Text.Encoding.UTF8;
                    break;
                case LibEncoding.Default:
                    encode = System.Text.Encoding.Default;
                    break;
            }
            return encode;
        }

        #endregion

    }

    public enum LibEncoding
    {
        Default = 0,
        UTF8 = 1,
        UTF7 = 2,
        UTF32 = 3,
        Unicode = 4,
        ASCII = 5
    }
}
