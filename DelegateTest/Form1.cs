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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.form2_showMessage("LabelXXX");
            form2.johnchange_event +=new Form2.johnchange_delegate(this.change);
            form2.Show();
            
        }
        private void change(string _string) 
        {
            this.Text = _string;
        }
    }
}
