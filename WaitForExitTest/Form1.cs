using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace WaitForExitTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"C:\Program Files (x86)\Emotiv EPOC Control Panel v2.0.0.21\Applications\ConsumerControlPanel.exe";
            ProcessStartInfo psi = new ProcessStartInfo(path);
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Maximized;

            if (Program.IsAdministrator())
            {
                //
            }
            else 
            {
                psi.Verb = "runas";
                MessageBox.Show("runas");
            }
            Process emotivProcess = Process.Start(psi);
            emotivProcess.WaitForExit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Form1");
            log.Fatal(new Exception("TUMES")); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            f2.Close();
            f2.ShowDialog();
        }

        
    }
}
