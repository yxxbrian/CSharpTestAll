using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text;

namespace SmallTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //TestProgram();
            //TestProgram1();
            //TestProgram2();
            //TestProgram3();
        }

        private string[] ParseAppendingCHLB(string str) 
        {
            //string str = "{\"channels_label\":[\"SPI1:MIC0\",\"SPI2:MIC1\",\"SPI3:MIC2\",\"SPI4:MIC3\",\"SPI5:AECREFL\",\"SPI6:AECREFR\",\"SPI7:Lin\",\"SPI8:Lout\",\"SPI9:Spkout\",\"SPI10:VADs\"]}..";
            //MessageBox.Show(str);
            List<string> splitedStrings = new List<string>();
            foreach (string str0 in str.Split(new char[] { '[', ']' })[1].Split(new char[] { '"', ',' })) 
            {
                if (!string.IsNullOrEmpty(str0))
                {
                    splitedStrings.Add(str0.Split(new char[] { ':' })[1]);
                }
            }
            return splitedStrings.ToArray();
        }

        private void TestProgram2() 
        {
            char[] MyChunk = new char[] { 'C','H','L','B'};
            string str = new string(MyChunk);
            if (str.Equals("CHLB"))
            {
                MessageBox.Show("Same");
            }
        }

        private void TestProgram() 
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 1)
                        continue;
                    Console.WriteLine(""+i+"\t"+j);
                    MessageBox.Show("Brilliant");
                }
            }
        }

        private void TestProgram1()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 1)
                        break;
                    Console.WriteLine("" + i + "\t" + j);
                }
            }
        }

    }
}
