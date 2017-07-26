using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace ThreadMessageBoxTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(Test));
            thread.Start();
        }

        void Test() 
        {
            MessageBox.Show("OK");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Person person = new Person();
            person.Name = "Bruce Lee";
            MessageBox.Show(person.Name);
            //person.age = 
        }
    }

    public class Person 
    {
        public string Name
        {
            get;
            set;
        }

        

        private uint baseAge;

        public uint age 
        {
            get 
            {
                return baseAge + 1;
            }
        }

    }

}
