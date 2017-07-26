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
using System.IO;
using System.Collections;
using System.DirectoryServices;
using System.Net;
using System.Threading;

namespace sendMessageTestForm2
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }


        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int msg, uint wParam, uint lPram);

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg) 
            {
                case (Message.WM_ONE):
                    {
                        Trace.Write(""+m.Msg+m.WParam+"&&"+m.LParam);
                        this.Text = "" + m.Msg + m.WParam + "&&" + m.LParam;
                        break;
                    }
                case (Message.WM_TWO):
                    {
                        Trace.Write(""+m.Msg + m.WParam + "&&" + m.LParam);
                        this.Text = "" + m.Msg + m.WParam + "&&" + m.LParam;
                        break;
                    }
                default: 
                    {

                        base.DefWndProc(ref m);
                        break;
                    }


            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread SubThread_GetIPAddress = new Thread(new ThreadStart(GetIPAddress));
            SubThread_GetIPAddress.Start();

        }


        public static string ReadMac(string ip)//传递IP地址,即可返回MAC地址
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            
            string mac = "";
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            //p.StartInfo.FileName = @"C:\Windows\System32\nbtstat.exe";
            p.StartInfo.FileName = @"C:\\Windows\\sysnative\\nbtstat.exe";
            p.StartInfo.Arguments = "-a " + ip;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            int len = output.IndexOf("MAC Address = ");
            if (len > 0)
            {
                mac = output.Substring(len + 14, 17);
            }
            p.WaitForExit();
            return mac;
        }

        public delegate void myInvoke(TreeNode node);


        public void addNode(TreeNode node)
        {
            PCTree.Nodes.Add(node);
        }

        public void GetIPAddress()
        {
            DirectoryEntry entryPC = new DirectoryEntry("WinNT:");
            ArrayList arr = new ArrayList();
            foreach (DirectoryEntry child in entryPC.Children)
            {
                TreeNode node = new TreeNode();
                node.Text = child.SchemaClassName + ":" + child.Name;
                //PCTree.Nodes.Add(node);
                


                foreach (DirectoryEntry pc in child.Children)
                {
                    if (String.Compare(pc.SchemaClassName, "computer", true) == 0)
                    {
                        TreeNode son = new TreeNode();
                        //son.Text = pc.Name;
                        try
                        {
                            IPHostEntry hostent = Dns.GetHostByName(pc.Name);//Dns.GetHostByName(pc.Name); // 主机信息
                            Array addrs = hostent.AddressList; // IP地址数组
                            IEnumerator it = addrs.GetEnumerator(); // 迭代器
                            while (it.MoveNext())
                            {
                                // 循环到下一个IP 地址
                                IPAddress ip = (IPAddress)it.Current; // 获得 IP 地址
                                son.Text = ip.ToString(); // 显示 IP地址
                                arr.Add(ReadMac(ip.ToString()));
                            }
                        }
                        catch
                        {
                            son.Text = pc.Name;
                        }
                        node.Nodes.Add(son);
                    }
                }
                myInvoke mi = new myInvoke(addNode);
                BeginInvoke(mi, new object[] { node });
                Trace.Write(arr);
            }
            foreach (string s in arr)
            {
                this.listBox1.Items.Add(s);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            //p.StartInfo.FileName = @"nbtstat";
            p.StartInfo.FileName = @"C:\\Windows\\sysnative\\nbtstat.exe";
            //p.StartInfo.FileName = @"C:\Windows\System32\nbtstat.exe";
            p.StartInfo.Arguments = "-a ";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DirectoryEntry entryPC = new DirectoryEntry("WinNT:");
            ArrayList arr = new ArrayList();
            foreach (DirectoryEntry child in entryPC.Children)
            {
                TreeNode node = new TreeNode();
                node.Text = child.SchemaClassName + ":" + child.Name;
                //PCTree.Nodes.Add(node);



                foreach (DirectoryEntry pc in child.Children)
                {
                    if (String.Compare(pc.SchemaClassName, "computer", true) == 0)
                    {
                        TreeNode son = new TreeNode();
                        //son.Text = pc.Name;
                        try
                        {
                            IPHostEntry hostent = Dns.GetHostByName(pc.Name);//Dns.GetHostByName(pc.Name); // 主机信息
                            Array addrs = hostent.AddressList; // IP地址数组
                            foreach(IPAddress ip in addrs)
                                this.comboBox1.Items.Add(ip.ToString().Trim());
                            this.comboBox1.SelectedIndex = 0;
                        }
                        catch
                        {
                            son.Text = pc.Name;
                        }
                        node.Nodes.Add(son);
                    }
                }
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = ReadMac(this.comboBox1.SelectedIndex.ToString().Trim());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 15; i++)
            {
                Trace.Write("\r\nmain" + i);
                Thread.Sleep(100);
            }
            Thread subThread = new Thread(new ThreadStart(subAction));
            
            subThread.Start();
            subThread.Join();
            for (int i = 31; i < 45; i++)
            {
                Trace.Write("\r\nmain" + i);
                Thread.Sleep(100);
            }


        }

        public void subAction() 
        {
            for (int i = 15; i < 30; i++)
            {
                Trace.Write("\r\nsub" + i);
                Thread.Sleep(100);
            }

        }


    }
}
