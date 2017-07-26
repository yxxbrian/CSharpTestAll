using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DragCopyPaste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //setClipboardTest();
        }

        private void setClipboardTest() 
        {
            Clipboard.Clear();
            Clipboard.SetText("Boom Shakalaka!", TextDataFormat.UnicodeText);

            IDataObject idata = Clipboard.GetDataObject();
            string text = "";
            for(int i=0;i < idata.GetFormats().Length;i++)
            {
                foreach(string str in idata.GetFormats())
                    text += str+"\r\n";
            }
            MessageBox.Show(text);

            text = "";
            if(idata.GetDataPresent(DataFormats.UnicodeText))
                text += idata.GetData(DataFormats.UnicodeText);
            MessageBox.Show("Text:"+text);
        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    e.Effect = DragDropEffects.Move;
            //}
            //else
            //    e.Effect = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            //Array filesArray = (Array)e.Data.GetData(DataFormats.FileDrop);
            //foreach (object obj in filesArray) 
            //{
            //    this.listBox2.Items.Add(obj.ToString());
            //}
            if(e.Data.GetDataPresent(DataFormats.Text))
            {
                this.listBox2.Items.Add(e.Data.GetData(DataFormats.Text));
            }
            

        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.listBox1.DoDragDrop(this.listBox1.SelectedItem, DragDropEffects.Move);
        }

        private void listBox3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBox3_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
            {
               this.listBox3.Items.Add((e.Data.GetData(DataFormats.FileDrop) as Array).GetValue(0).ToString());
            }

        }
    }
}
