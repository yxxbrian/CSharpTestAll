using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace stringTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Test4();
        }

        //相同的字符串，引用会指向同一块内存区域，引用相同；修改字符串，会在内存分配另一块区域存储，指向另一块；
        private void Test() 
        {
            string s1 = "SAD";
            string s2 = new string(new char[] { 'S','A','D'});
            MessageBox.Show((s1 == s2).ToString());

        }

        private void Test1() 
        {
            string a = "ab";
            string b = a;
            a = "abc";
            MessageBox.Show("a:" + a + " b:" + b);//a == "abc";b == "ab"
        }
        private void Test2()
        {
            int a = 12;
            int b = 12;
            MessageBox.Show("a==b?" + object.ReferenceEquals(a,b));//false
        }
        private void Test3()
        {
            Boolean a = true;
            Boolean b = a;
            MessageBox.Show("a==b?" + object.ReferenceEquals(a, b));//false
        }


        private void Test4() 
        {
            int a = 4;
            int b = a++ * a++;
            MessageBox.Show("a:"+a+" b:"+b);

        }
    }
}
