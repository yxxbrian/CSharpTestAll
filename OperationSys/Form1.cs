using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OperationSys
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            GetSYSInfo();
        }

        private void GetSYSInfo() 
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            MessageBox.Show(osInfo.VersionString);

        }
    }
}
