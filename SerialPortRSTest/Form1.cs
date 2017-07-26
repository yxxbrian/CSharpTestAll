using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SerialPortRSTest
{
    public partial class Form1 : Form
    {
        public delegate void  ReceiveMessageDelegate(IntPtr intPtr , string message);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public void receiveMessageAction(IntPtr intPtr, string message) 
        {
            

        }

        private void sendSerialMessage() 
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReceiveMessageDelegate delegateObject = new ReceiveMessageDelegate(receiveMessageAction);
            IntPtr pt = Marshal.GetFunctionPointerForDelegate(delegateObject);
            Trace.Write("\r\ndelegate pointer is " + pt);
            Trace.Write("\r\n Delegate is actually a IntPtr(pointer)");

            MessageBox.Show("\r\ndelegate pointer is " + pt);
            MessageBox.Show("\r\n Delegate is actually a IntPtr(pointer)");

        }
        Process cmd;
        private void button4_Click(object sender, EventArgs e)
        {
            cmd = new Process();
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = false;
            cmd.StartInfo = info;
            
            cmd.Start();
            
            //cmd.BeginOutputReadLine();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd.StandardInput.Write("\r\n ddsdsd &exit");
            cmd.StandardInput.Close();
            //cmd.BeginOutputReadLine();
            
            //Console.Write("sdsdsdsdds\r\n");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmd = new Process();
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            info.RedirectStandardInput = false;
            info.RedirectStandardOutput = false;

            //info.RedirectStandardError = true;
            info.UseShellExecute = true;
            info.CreateNoWindow = false;

            info.Arguments = "/k sd";
            
            cmd.StartInfo = info;
            
            cmd.Start();
            Console.Write("asdasdsad");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cmd.StartInfo.Arguments = "/k sdsds";
           // Console.ReadKey();
           // Console.Write("sdsdsd");
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process p = new Process(); 
            p.StartInfo.FileName = "cmd.exe"; 
            p.StartInfo.UseShellExecute = false; 
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true; 
            p.StartInfo.RedirectStandardError = true; 
            p.StartInfo.CreateNoWindow = true; 
            p.Start(); 
            p.StandardInput.WriteLine("/k ipconfig/all");
            string d = p.StandardOutput.ToString();
            p.Close();
            Console.Write(p.StandardOutput.ReadToEnd()); 
            //p.StandardInput.WriteLine("exit");
        }

        private void button8_Click(object sender, EventArgs e)
        {

            cmd = new Process();
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = false;
            
            
            cmd.StartInfo = info;
            cmd.Start();

            cmd.StandardInput.WriteLine("ver");
            cmd.StandardInput.WriteLine("exit");
            string result = cmd.StandardOutput.ReadToEnd();
            Console.Write(result);
            Console.ReadKey();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }





    }
}
