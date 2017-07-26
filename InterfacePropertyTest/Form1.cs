using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InterfacePropertyTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student stu = new Student();
            stu.Name = "ABS";
            MessageBox.Show(stu.Name);
            IStudent Istu = stu as IStudent;
            Istu.Name = "ABS Plus";
            MessageBox.Show(stu.Name);
        }
    }
}
