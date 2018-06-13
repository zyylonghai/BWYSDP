using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDPCRL.CORE;
using System.Threading;
using System.Diagnostics;

namespace InitialTool
{
    public class ExceptionManager
    {
        public static void ThrowError(string message)
        {
            //try { 
            throw new LibExceptionBase(message);
            //}
            //catch (LibExceptionBase ex) 
            //{
            //    PromptForm prompt = new PromptForm();
            //    prompt.MessageList.Items.Add(ex.Message);
            //    prompt.Show();
            //    throw;

            //}
        }
    }
    public class MessageHandel
    {
        private static PromptForm _promPt;
        public static PromptForm PromPt
        {
            get
            {
                if (_promPt == null)
                {
                    _promPt = new PromptForm();
                }
                return _promPt;
            }
        }

        static void ShowMessage(string message)
        {
            PromPt.MessageList.Items.Add(message);
            PromPt.Show();
        }
        public static void ShowMessage(string message, bool clear)
        {
            if (clear)
                PromPt.MessageList.Items.Clear();
            ShowMessage(message);
        }

        public static void DisposePromptForm()
        {
            _promPt = null;
        }

    }
}
