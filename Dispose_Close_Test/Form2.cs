using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dispose_Close_Test
{
    public partial class Form2 : Form,IDisposable
    {
        public Form2()
        {
            InitializeComponent();
        }
        public void closeAction() 
        {
            this.Close();
            MessageBox.Show("Text:" + this.Text);

        }

        public void DisposeAction() 
        {
            this.Dispose();
            MessageBox.Show("Text:"+this.Text);
        }

        public new void Dispose() 
        {
            MessageBox.Show("DISPOSING");
        }
        
    }
}
