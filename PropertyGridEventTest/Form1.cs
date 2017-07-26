using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;


namespace PropertyGridEventTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TestMethod();
        }

        private void TestMethod()
        {
            propertyGrid.SelectedObject = new Class1();

            Control propertyGridView = propertyGrid.Controls[2];
            Type propertyGridViewType = propertyGridView.GetType();
            FieldInfo info = propertyGridViewType.GetField("edit ",
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.NonPublic |
            BindingFlags.DeclaredOnly |
            BindingFlags.Public);
            if (info == null)
                return;
            System.Windows.Forms.TextBox txtBox = info.GetValue(propertyGridView) as System.Windows.Forms.TextBox;

            txtBox.KeyDown += txtBox_KeyDown;
        }

        void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("TextBox keyDown");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = new Class1();

            Control propertyGridView = propertyGrid.Controls[2];
            Type propertyGridViewType = propertyGridView.GetType();
            FieldInfo info = propertyGridViewType.GetField("edit ",
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.NonPublic |
            BindingFlags.DeclaredOnly |
            BindingFlags.Public);
            if (info == null)
                return;
            System.Windows.Forms.TextBox txtBox = info.GetValue(propertyGridView) as System.Windows.Forms.TextBox;

            txtBox.KeyDown += txtBox_KeyDown;
        }

        private void propertyGrid_DoubleClick(object sender, EventArgs e)
        {
            ;
        }

        static int i = 0;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = Cursor.Position;
            Point pp = this.PointToClient(p);
            Control control = this.GetChildAtPoint(pp);
            if (control != null)
                this.Text = (i+++control.ToString() + "\t" + control.Name);
            else
                this.Text = (i+++"Click null");
        }

        private void propertyGrid_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = Cursor.Position;
            Point pp = this.PointToClient(p);
            Control control = this.GetChildAtPoint(pp);
            if (control != null)
                this.Text = (i++ + control.ToString() + "    " + control.Name);
            else
                this.Text = (i++ + "Click null");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control s in propertyGrid.Controls[2].Controls)
            {
                MessageBox.Show(s.ToString() + "\t  h              " + s.Name+"\t h"+s.Size+"\t h"+s.Location);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control s in this.Controls)
            {
                MessageBox.Show(s.ToString() + "\t" + s.Name + "\t" + s.Size + "\t" + s.Location);
            }
        }

        private void propertyGrid_Click(object sender, EventArgs e)
        {
            ;
        }
    }
}
