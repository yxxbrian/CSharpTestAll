using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace XMLWriterTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            XMLTestNew();
            XMLTestNEW();
            //testAction(@"lan\testXml.xml");
        }

        XmlWriter lanWriter;
        XmlWriterSettings lanWriterSettings;


        private void XMLTestNEW() 
        {
            XmlWriter xmlWriter;
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriter = XmlWriter.Create("myXML_1.xml", xmlWriterSettings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("BookShops");



            xmlWriter.WriteStartElement("Book");
            xmlWriter.WriteAttributeString("Number", "product-001");
            /*
            xmlWriter.WriteStartAttribute("Number");
            xmlWriter.WriteValue("001");
            xmlWriter.WriteEndAttribute();
            */
            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteValue("西游记");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("OtherName");
            xmlWriter.WriteValue( "大唐西游转");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Author");
            xmlWriter.WriteValue("吴承恩");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Money");
            xmlWriter.WriteValue("220");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Details");
            xmlWriter.WriteValue("\n东胜神州的傲来国花果山的一块巨石孕育出了一只明灵石猴，石猴后来拜须菩提为师后习得了七十二变，具有了通天本领，于是占山为王，自称齐天大圣。");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();


            xmlWriter.WriteStartElement("Book");
            xmlWriter.WriteAttributeString("Number", "product-002");
            /*
            xmlWriter.WriteStartAttribute("Number");
            xmlWriter.WriteValue("001");
            xmlWriter.WriteEndAttribute();
            */
            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteValue("红楼梦");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("OtherName");
            xmlWriter.WriteValue("石头记");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Author");
            xmlWriter.WriteValue("曹雪芹");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Money");
            xmlWriter.WriteValue("320");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Details");
            xmlWriter.WriteValue("\n女娲炼石补天，所炼之石剩一块未用，弃在大荒山无稽崖青埂峰下。此石“自经煅炼之后灵性已通”，因未被选中补天常悲伤自怨。一日，和尚茫茫大士、道士渺渺真人经过此地。");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            
            
            
            
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Close();

        }


        private void XMLTestNew() 
        {
            XmlWriter xmlWriter;
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriter = XmlWriter.Create("myXML.xml", xmlWriterSettings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("School");
            
            xmlWriter.WriteStartElement("NUAA");
            xmlWriter.WriteStartAttribute("Name");
            xmlWriter.WriteValue("Name_NUAA");
            xmlWriter.WriteValue(1994);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteAttributeString("Position", "Nanjing");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("qtech");
            xmlWriter.WriteAttributeString("Name", "QTECH");
            xmlWriter.WriteAttributeString("Position","Qingtao");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Pecking");
            xmlWriter.WriteStartElement("SmallPecking");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();


            xmlWriter.WriteEndElement();


            xmlWriter.WriteEndDocument();

            xmlWriter.Close();
        }


        private void testAction(string path)
        {
            if (!Directory.Exists("lan"))
                Directory.CreateDirectory("lan");

            lanWriterSettings = new XmlWriterSettings();
            lanWriterSettings.Indent = true;
            lanWriter = XmlWriter.Create(path, lanWriterSettings);
            lanWriter.WriteStartDocument();

            lanWriter.WriteStartElement("Zhpat");
            {
                lanWriter.WriteStartElement("native-Lan");
                lanWriter.WriteAttributeString("name", "Zhapt");
                lanWriter.WriteAttributeString("time", System.DateTime.Now.ToString("HH:mm:ss"));
                lanWriter.WriteEndElement();
                lanWriter.WriteStartElement("native-Lan");
                lanWriter.WriteAttributeString("name", "Zhapt");
                lanWriter.WriteAttributeString("time", System.DateTime.Now.ToString("HH:mm:ss"));
                lanWriter.WriteEndElement();
            }

            lanWriter.WriteStartElement("Zhpat2");
            {
                lanWriter.WriteStartElement("native-Lan2");
                lanWriter.WriteAttributeString("name", "Zhapt2");
                lanWriter.WriteAttributeString("time", System.DateTime.Now.ToString("HH:mm:ss"));
                lanWriter.WriteEndElement();
            }
            lanWriter.WriteEndElement();
            

            lanWriter.WriteEndElement();
            
            
            lanWriter.WriteEndDocument();
            lanWriter.Close();
        }


    }
}
