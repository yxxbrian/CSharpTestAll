using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Dispose_Close_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Form2 form = new Form2()) 
            {
                form.Show();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormCollection formCollection = Application.OpenForms;
            foreach (Form form in formCollection) 
            {
                if (form.Text == "Form2")
                {
                    ((Form2)form).closeAction();
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormCollection formCollection = Application.OpenForms;
            foreach (Form form in formCollection)
            {
                if (form.Text == "Form2")
                {
                    ((Form2)form).DisposeAction();
                    return;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCollection formCollection = Application.OpenForms;
            foreach (Form form in formCollection)
            {
                if (form.Text == "Form2")
                {
                    Panel myPanel = ((Form2)form).panel1;
                    myPanel.BackColor = Color.Black;
                }
            }
        }
    }
}
