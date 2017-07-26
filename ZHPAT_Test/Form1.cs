using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;


namespace ZHPAT_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++) 
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[1].Value != null) 
                {
                    try
                    {
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString());
                        dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) / Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
                    }
                    catch(Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                    finally 
                    {

                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime currentTime = System.DateTime.Now;
            string timeString = currentTime.Year + "年" + currentTime.Month + "月" + currentTime.Day + "日" + currentTime.Hour + "时" + currentTime.Minute + "分" + currentTime.Second + "秒";
            Properties.Settings.Default.testName = timeString;
            Properties.Settings.Default.Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Text = Properties.Settings.Default.testName;
        }
        string txtFilePath = "D:\\temp.txt";
        FileInfo fileInfo;
        static int i = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            fileInfo = new FileInfo(txtFilePath);

            if (!fileExist(txtFilePath))
            {
                FileInfo txtFile = new FileInfo(txtFilePath);
                StreamWriter streamWriter = txtFile.CreateText();
                streamWriter.WriteLine("OK"+i++);
                streamWriter.Close();
            }
            



        }

        private bool fileExist(string path) 
        {
            return Directory.Exists(path);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FileInfo fileInfo = new FileInfo("D:\\temp.txt");
            StreamReader streamReader = fileInfo.OpenText();
            this.Text = streamReader.ReadLine();
            streamReader.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cmdPing cmdPing = new cmdPing();
             string text = cmdPing.cmdIPPing("www.baidu.com");
             MessageBox.Show(text);
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            System.IO.Directory.GetCurrentDirectory();
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            }
            catch (Exception ex) 
            {
                Console.Write(ex.Message);
                MessageBox.Show(ex.Message);
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Users\Administrator\Desktop\question.txt";
            
            startInfo.UseShellExecute = true;
            process.StartInfo = startInfo;
            process.Start();
            this.textBox1.AppendText("\r\nProcessName: "+process.ProcessName);
            this.textBox1.AppendText("\r\nmachineName"+process.MachineName);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Process process = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"D:\Program Files (x86)\SPlayer\splayer.exe";
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;

            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
            this.textBox1.AppendText("\r\nProcessName: " + process.ProcessName);
            this.textBox1.AppendText("\r\nmachineName" + process.MachineName);
        }

    }
}
