using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace XMLDocumentTest
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void DelElementTest() 
        {
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader xmlreader = XmlReader.Create("Book.xml", settings);
            doc.Load(xmlreader);
            xmlreader.Close();


            if (dataGridView1.SelectedRows.Count <= 0)
                return;
            string ISBN = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string path = string.Format("/bookstore/book[@ISBN=\"{0}\"]", ISBN);
            XmlElement element = (XmlElement)doc.SelectSingleNode(path);
            if (element != null)
            {
                element.ParentNode.RemoveChild(element);
            }

        }

        XmlDocument doc = new XmlDocument();
        private new void  Load(object sender, EventArgs e) 
        {
            List<BookModel> bookmodelList = new List<BookModel>(); ;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader xmlreader = XmlReader.Create("Book.xml", settings);
            doc.Load(xmlreader);
            xmlreader.Close();
            XmlNode root = doc.SelectSingleNode("bookstore");
            XmlNodeList nodelist = root.ChildNodes;
            foreach (XmlNode node in nodelist) 
            {
                BookModel bookmodel = new BookModel();
                XmlElement element = (XmlElement)node;
                bookmodel.BookType = element.GetAttribute("Type");
                bookmodel.BookISBN = element.GetAttribute("ISBN");
                XmlNodeList children = element.ChildNodes;
                bookmodel.BookName = children[0].InnerText;
                bookmodel.BookAuthor = children[1].InnerText;
                bookmodel.BookPrice = Convert.ToDouble(children[2].InnerText);

                bookmodelList.Add(bookmodel);

            }
            dataGridView1.DataSource = bookmodelList;
            
        }

        private void AddBook(object sender, EventArgs e) 
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader xmlreader = XmlReader.Create("Book.xml", settings);
            doc.Load(xmlreader);
            xmlreader.Close();
            XmlNode root = doc.SelectSingleNode("bookstore");

            XmlElement element = doc.CreateElement("book");
            XmlAttribute attribute = doc.CreateAttribute("Type");
            attribute.InnerText = "必修课";
            element.SetAttributeNode(attribute);
            attribute = doc.CreateAttribute("ISBN");
            attribute.InnerText = "7-111-19149-4";
            element.SetAttributeNode(attribute);
            
            XmlElement childElement = doc.CreateElement("Title");
            childElement.InnerText = "英雄崛起论";
            element.AppendChild(childElement);
            childElement = doc.CreateElement("Author");
            childElement.InnerText = "岳新新";
            element.AppendChild(childElement);
            childElement = doc.CreateElement("Price");
            childElement.InnerText = "250";
            element.AppendChild(childElement);

            root.AppendChild(element);
            doc.Save("Book.xml");
            Load(null, null);
            dataGridView1.ClearSelection();
        }

        private void Del(object sender, EventArgs e)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader xmlreader = XmlReader.Create("Book.xml", settings);
            doc.Load(xmlreader);
            xmlreader.Close();


            if (dataGridView1.SelectedCells.Count <= 0)
                return;
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            string ISBN = dataGridView1.Rows[selectedRowIndex].Cells[1].Value.ToString();
            string path = string.Format("/bookstore/book[@ISBN=\"{0}\"]",ISBN);
            XmlElement element = (XmlElement)doc.SelectSingleNode(path);
            if (element != null)
            {
                element.ParentNode.RemoveChild(element); 
            }
            doc.Save("Book.xml");
            Load(null, null);
            dataGridView1.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
        }

       

        

    }
}
