using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XML_Test
{
    public partial class Form1 : Form
    {
       
        
        public Form1()
        {
            InitializeComponent();
            InitGraphics();
            InitBrush();
            InitPen();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Xml接口-构造函数
        /// <summary>
/// Xml接口
/// </summary>
/// <param name="node"></param>
        protected  Form1(XmlNode node) 
        {
            this.textBox1.Text = ((XmlElement)node).GetAttribute("ID");
            this.textBox2.Text = ((XmlElement)node).GetAttribute("Name");

            XmlElement element = (XmlElement)node.SelectSingleNode("Age");
            this.textBox3.Text = element.InnerText;

            element = (XmlElement)node.SelectSingleNode("Height");
            this.textBox4.Text = element.InnerText;

        }
        #endregion



        #region XmlDocument接口
        protected  Form1(XmlDocument document) 
        {
            XmlNode node = document.SelectSingleNode("userXinxin");
            this.textBox1.Text = ((XmlElement)node).GetAttribute("ID");
            this.textBox2.Text = ((XmlElement)node).GetAttribute("Name");

            XmlElement element = (XmlElement)node.SelectSingleNode("Age");
            this.textBox3.Text = element.InnerText;

            element = (XmlElement)node.SelectSingleNode("Height");
            this.textBox4.Text = element.InnerText;
        }


        #endregion


        #region newFileName 属性
        private string fileName = "Xinxin";
        //protected string newFileName;
        protected string NewFileName 
        {
            get 
            {
                string tem;
                if (fileName.Trim() == "" || fileName == null) 
                    throw new Exception("fileName empty!");
                if (fileName.ToLower().Contains(".xml"))
                {
                    tem = fileName;
                }
                else
                {
                    tem = fileName + ".xml";
                }
                return tem;
            }

            set 
            {
                fileName = value;
            }


        }
        #endregion


        #region 创建并生成user节点、返回XmlDocument文件

        protected XmlDocument document;

        protected XmlDocument creatDocument() 
        {
            XmlDocument tempDoc = new XmlDocument();
            //XmlDeclaration declaration = new XmlDeclaration();
            //declaration = tempDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            //tempDoc.AppendChild(declaration);
            return tempDoc;
        }

     
        protected void creatRootElement(XmlDocument doc) 
        {
            XmlElement element = doc.CreateElement("user");
            doc.AppendChild(element);
     
            doc.Save(NewFileName);
        }

        protected void updateDocument() 
        {
            this.document = creatDocument();
            creatRootElement(this.document);
        }
#endregion


//        #region 保存node入doc
//        protected void saveNodeToDoc(XmlNode node, XmlDocument doc) 
//        {
//            XmlElement element = (XmlElement)doc.SelectSingleNode("user");
//            if (element != null) 
//            {
//                element.AppendChild(node);
//                doc.Save(NewFileName);
//            }

//        } 

//#endregion


        #region 本地数据转换成XmlNode并返回


        protected XmlNode BuildUpNode(XmlNode node)
        {
            XmlNode childNode = node;
            if (childNode != null)
            {
                XmlAttribute tempAttribute = childNode.Attributes.Append(document.CreateAttribute("ID"));
                tempAttribute.InnerText = Convert.ToString(this.textBox1.Text);

                tempAttribute = childNode.Attributes.Append(document.CreateAttribute("Name"));
                tempAttribute.InnerText = Convert.ToString(this.textBox2.Text);

                XmlNode tempNode = childNode.AppendChild(document.CreateElement("Age"));
                tempNode.InnerText = Convert.ToString(this.textBox3.Text);

                tempNode = childNode.AppendChild(document.CreateElement("Height"));
                tempNode.InnerText = Convert.ToString(this.textBox4.Text);

                return node;
            }
            else 
            {
                throw new Exception("user node unsearched!!");
                //return node;
            }
        }


        #endregion


        #region

        protected XmlDocument updateDataToDocument() 
        {
            updateDocument();
            XmlNode node = document.SelectSingleNode("user") ;
            XmlElement element = document.CreateElement("userXinxin");
            XmlNode childNode = node.AppendChild(element);
            node.AppendChild(BuildUpNode(childNode));
            document.Save(NewFileName);
            return document;
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            updateDataToDocument();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadForm1();
        }

        private void startWatch ()
        {
            stopwatch.Reset();
            stopwatch.Start();
            
        }

        private void stopWatch(string msg) 
        {
            stopwatch.Stop();
            MessageBox.Show(msg + stopwatch.ElapsedMilliseconds + " milliseconds");

        }

        protected void loadForm1()
        {

            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(NewFileName);
            XmlNode node = doc.SelectSingleNode("user");
            node = node.SelectSingleNode("userXinxin");
            if (node == null)
                throw new Exception("userXinxin node unfinded in the document");
            this.textBox1.Text = ((XmlElement)node).GetAttribute("ID");
            this.textBox2.Text = ((XmlElement)node).GetAttribute("Name");

            XmlElement element = (XmlElement)node.SelectSingleNode("Age");
            this.textBox3.Text = element.InnerText;

            element = (XmlElement)node.SelectSingleNode("Height");
            this.textBox4.Text = element.InnerText;

            

        }

        private void textBox9_MouseClick(object sender, MouseEventArgs e)
        {
            


        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            openFileDialog1.Filter = "All files (*.*)|*.*|txt files (*.txt)|*.txt|Image Files(*.BMP)|*.BMP";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;

            }
        }

       private System.Timers.Timer timer;

       private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = "number:" + Convert.ToString(number);

            timer = new System.Timers.Timer(1000);
            timer.BeginInit();
            timer.AutoReset = true;
            timer.SynchronizingObject = this;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.EndInit();

            timer.Start();
            
        }


        private void timer_Elapsed(object sender , System.Timers.ElapsedEventArgs e) 
        {
            Process();

        }

        static int number = 0;

        private void Process() 
        {
            this.textBox3.Text = "number:"+Convert.ToString(number);
            number++;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (timer != null) 
            {
                timer.Stop();
                timer.BeginInit();
                timer.Interval *= 2;
                timer.EndInit();
                timer.Start();

            }

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            



        }

        private void simpleButtonOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog2.FilterIndex = 1;
            openFileDialog2.RestoreDirectory = true;

            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = openFileDialog2.FileName;
                proc.Start();

            }
        }

        private void simpleButtonSelectColor_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true;
            colorDialog1.Color = this.BackColor;
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                this.BackColor = colorDialog1.Color;

            }

        }


        #region graphics的读写与初始化

        private Graphics myGraphics;

        public Graphics graphics 
        {
            get 
            {
                return this.myGraphics;
            }
            set 
            {
                this.myGraphics = value;

            }

        }

        private void InitGraphics() 
        {
            graphics = this.CreateGraphics();

        }
#endregion

        #region pen的读写与初始化操作
        
        private Pen myPen;

        public Pen pen 
        {
            get 
            {
                return this.myPen;

            }
            set 
            {
                if(value is Pen)
                    this.myPen = value;
            }

        }

        private void InitPen() 
        {
            pen = new Pen(brush,3);


        }

        #endregion

        #region brush的读写与初始化操作
        
        private Brush myBrush;

        public Brush brush 
        {
            get 
            {
                return this.myBrush;
            }
            set 
            {

            }

        }

        private void InitBrush() 
        {
            myBrush = new SolidBrush(Color.Blue);

        }

        #endregion

        static int num = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == 0) 
            {
                Pen myPen = new Pen(Color.Red);
               
                Rectangle rect = new Rectangle(new Point(700,20),new Size(50+num*10,50+num*10));
                graphics.DrawRectangle(myPen,rect);
                num++;
            }
            else if (this.comboBox1.SelectedIndex == 1) 
            {
                //Brush myBrush = new SolidBrush(Color.Blue);
                //Pen myPen = new Pen(myBrush,3);
                graphics.DrawEllipse(pen,new Rectangle(700,100,80,80));


            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            startWatch();
            for (int i = 0; i < 5000; i++) 
            {
                for (int j = 0; j < 5000; j++) ;
            }
            stopWatch("nothing last ");
        }

    }
}
