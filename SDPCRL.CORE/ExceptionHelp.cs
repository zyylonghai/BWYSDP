﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDPCRL.CORE
{
    public class ExceptionHelp
    {
        //IlibException ilibException = null;
        public ExceptionHelp()
        {

        }

        public void ThrowError<T>(T obj, string msg)
        {
            IlibException exception = obj as IlibException;
            if (exception != null)
            {
                exception.BeforeThrow();
            }
            throw new LibExceptionBase(msg);
        }
        public void ThrowError<T>(T obj, string msg, string StackTrace)
        {
            IlibException exception = obj as IlibException;
            if (exception != null)
            {
                exception.BeforeThrow();
            }
            throw new LibExceptionBase(msg,StackTrace);
        }
        //public void AddMessage<T>(T obj, string msg, LibMessageType messageType)
        //{

        //}
            
    }
    public enum LibMessageType
    {
        /// <summary>错误信息</summary>
        Error = 1,
        /// <summary>警告信息</summary>
        Warning = 2,
        /// <summary>提示信息</summary>
        Prompt = 3
    }
}
