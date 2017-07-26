using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ref_Variables_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            int refVariable = 0;
            MessageBox.Show("Value of variable 'refVariable' is " + refVariable);
            InitializeComponent();
            AddValue(ref refVariable);
            MessageBox.Show("Value of variable 'refVariable' is "+refVariable);
        }

        private void AddValue(ref int value)
        {
            value++;
        }

    }
}
