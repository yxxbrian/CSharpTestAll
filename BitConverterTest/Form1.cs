using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace SendMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(threadAction));
            thread.Start();


        }

        private void threadAction() 
        {
            SendMessage(this.Handle,0X1001,IntPtr.Zero,"sd");

        }


        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
            int hWnd,      // handle to destination window
            int Msg,       // message
            int wParam,    // first message parameter
            int lParam     // second message parameter
            );
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            IntPtr wParam,
            string lParam
            );

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg) 
            {
                case (0X1001): 
                    {
                        MessageBox.Show("YES"); 
                        break; 
                    }
                default: 
                    {
                        base.DefWndProc(ref m); 
                        break;
                    } 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form subForm = new Form();
            subForm.Size = new Size(300, 200);
            subForm.Visible = true;
            subForm.MdiParent = this;
            subForm.Show();


        }    
    }
}
