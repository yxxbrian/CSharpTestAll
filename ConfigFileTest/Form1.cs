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
using System.Xml;


namespace ConfigFileTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string file = Application.ExecutablePath;
            DirectoryInfo dirInfo = new DirectoryInfo(file);
            string f = dirInfo.Parent.Parent.Parent.FullName+"\\MyApp.config";
            
           // System.IO.Directory dir = 
            Configuration config = ConfigurationManager.OpenExeConfiguration(f);
            
            //config.AppSettings.Settings["Bat"].Value = "BatMan";
            config.Save();
        }


        private void GetConfigData(string key)
        {
            string file = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            MessageBox.Show(config.AppSettings.Settings[key].Value.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetConfigData("Bata");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string file = Application.ExecutablePath;
            DirectoryInfo dirInfo = new DirectoryInfo(file);
            string f = dirInfo.Parent.Parent.Parent.FullName + "\\Note.txt";
            Configuration config = ConfigurationManager.OpenExeConfiguration(f);
            config.AppSettings.Settings.Add("MyKey", "MyValue");
            config.Save();
            MessageBox.Show(config.AppSettings.Settings["MyKey"].Value.ToString());
            
            
            
           

        }
    }
}
