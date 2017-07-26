using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SameNameVariablesTest
{
    
    public partial class Form1 : Form
    {
        int i = 0;

        public Form1()
        {
            int i = 1;
            InitializeComponent();
            MessageBox.Show(i.ToString());
        }
    }
}
