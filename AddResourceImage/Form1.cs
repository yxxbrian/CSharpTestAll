using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace AddResourceImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Filter = "ICO Files(*.ico)|*.ico|PNG Files(*.PNG)|*.PNG|JPEG Files(*.JPEG)|*.JPEG|JPEG Files(*.JPEG)|*.JPEG|BMP Files(*.BMP)|*.BMP|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            DialogResult result = dialog.ShowDialog();
            if (!(result == System.Windows.Forms.DialogResult.OK))
                return;
            string fileName = dialog.FileName;
            MessageBox.Show(fileName);
        }


        private Image _image;
    }
}
