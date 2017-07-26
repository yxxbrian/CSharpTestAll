using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StringMatchCollectionTest
{
    //委托(delegate、Func<>、Action)、匿名函数、Lambda
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Timers.Timer timer_weituo = new System.Timers.Timer(1000);
            timer_weituo.AutoReset = false;
            timer_weituo.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer_weituo.Start();
            
        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(new Func<bool>(delegate() { MessageBox.Show(""); return true; }));
            testMain();
        }
        


        //调用函数声明
        public void testMain()
        {
            MessageBox.Show("this is a demo which uses -- delegate  Func<>  Action<> delegate(Type param){} (Type param)=>()");
            Process(new Calculate(Tool_string), 2, 5);
            Process(new Func<double, double, bool>(Tool_bool), 2, 5);
            Process(new Action(Tool_void));

            //Func匿名
            Process(new Func<double, double, bool>(delegate(double x, double y) { return x > y; }), 2, 5);
            Process(delegate(double x, double y) { return x > y; }, 2, 5);
            

            //Calculate匿名
            Process(new Calculate(delegate(double x, double y) { return x > y ? "true" : "false"; }), 2, 5);
            Process(delegate(double x,double y){return x > y ? "true" : "false";},2,5);

            //Action匿名
            Process(new Action(Tool_void));
            Process(delegate() { MessageBox.Show("Got it"); });

            //Lambda
            Process((x, y) => x > y, 2, 5);
            Process((x, y) => x > y ? "true" : "false", 2, 5);
        }

        public string Tool_string(double x, double y)
        {
            return x > y ? "bigger" : "smaller";
        }
        public bool Tool_bool(double x, double y)
        {
            return x > y;
        }
        public void Tool_void() 
        {
            MessageBox.Show("Got it");
        }
        //————————————————————————————————————————————————————————————
        //NO.1--- delegate

        public delegate string Calculate(double param1, double param2);
        public void Process(Calculate cal, double x, double y)
        {
            string result1 = cal(x, y);
            string result2 = cal(y, x);
            MessageBox.Show("param1/param2 equals " + result1 + "\r\nand param2/param1 equals " + result2);
        }
        

        //—————————————————————————————————————————————————————————————
        //NO.2--- Func<>
        public void Process(Func<double, double, bool> func, double x, double y) 
        {
            bool result1 = func(x, y);
            bool result2 = func(y, x);
            MessageBox.Show("param1/param2 equals " + result1 + "\r\nand param2/param1 equals " + result2);
        }

        public void Process(Func<bool> func) 
        {
            MessageBox.Show(""+func());
        }

        //—————————————————————————————————————————————————————————————
        //NO.3--- Action<>
        public void Process(Action action) 
        {
            action();
            //MessageBox.Show(""action);
        }





        

        void handleMethod() 
        {
            MessageBox.Show("OK");
        }


    }
}
