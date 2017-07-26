using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventArgsReferenceChange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            subClass sub = new subClass();
            sub.deleg += new MyEventHandler(sub_deleg);
            sub.action();
            

        }

        void sub_deleg(object sender, MyEventArgs e)
        {
            //MessageBox.Show(""+e.Message);
            e.Message_ReadWrite = 965;
        }
        private void Acc(int a) 
        {

        }
    }
}
