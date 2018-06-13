using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWYSDP.com;

namespace BWYSDP
{
    public partial class PromptForm : Form
    {
        private ListBox _messageList;
        public ListBox MessageList
        {
            get
            {
                if(_messageList==null )
                _messageList =new ListBox();
                return _messageList;
            }
        }
        public PromptForm()
        {
            InitializeComponent();
            MessageList.Dock = DockStyle.Fill ;
            this.panel1.Controls.Add(MessageList);
        }

        private void PromptForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(SystemInformation.WorkingArea.Width - this.Width,
                SystemInformation.WorkingArea.Height - this.Height);
        }

        private void PromptForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageHandel.DisposePromptForm();
        }
    }
}
