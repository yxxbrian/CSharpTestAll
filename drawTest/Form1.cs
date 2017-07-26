using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace drawTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            action();
            Form2 f2 = new Form2();
            f2.FormClosing+=new FormClosingEventHandler(f2_F);
            f2.Show();
            
        }
        private void f2_F(object sender, FormClosingEventArgs e) 
        {
            MessageBox.Show("mine");
        }

        private void action() 
        {
            double x = 1;
            double y = Math.Sqrt(3);
            double angle1 = Math.Atan2(y, x);
            double angle2 = Math.Atan2(y, x)/Math.PI*180;
            MessageBox.Show("angle1: "+angle1 +"   angle2: "+angle2);

            double delta1 = Math.Cos(angle1);
            double delta2 = Math.Cos(angle2);
            MessageBox.Show("delta1: "+delta1+"  delta2: "+delta2);



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
