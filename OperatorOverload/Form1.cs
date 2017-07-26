using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OperatorOverload
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            MessageBox.Show(("ss" == "ss").ToString());

            Person i = new Person("Lee",23);
            Person j = new Person("Jay",24);
            string ss = i + j;
            int aa = i - j;
            MessageBox.Show(ss+aa);

        }

        

    }

    public class Person 
    {
        
        
        public static int operator -(Person i, Person j) 
        {
            return i.age - j.age;
        }
        
        
        
        public Person(string name , int age) 
        {
            this.name = name;
            this.age = age;
        }

        public string name;
        public int age;

        public static string operator +(Person i, Person j)
        {
            return "" + i.age + j.age;
        }
    }
}
