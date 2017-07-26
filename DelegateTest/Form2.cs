using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void form2_showMessage(string astring) 
        {
            this.label1.Text = astring;
        }

        private void john_Click(object sender, EventArgs e)
        {
            this.johnchange_event("Cabbage");
        }

        public delegate void johnchange_delegate(string _string);
        public event johnchange_delegate johnchange_event;
    }
}
