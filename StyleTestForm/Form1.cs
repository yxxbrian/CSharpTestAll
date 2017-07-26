using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Skins;


namespace StyleTestForm
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            XtraForm1 d = new XtraForm1();
            //d.Show();
            //this.LookAndFeel.SkinName = 

            uint s = 0xFFFFFFF0;
            int e = (int)s;


        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void barEditItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            string s = (string)(barEditItem2.EditValue);
            this.LookAndFeel.SkinName = s;
            //((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)barEditItem2)
            DevExpress.XtraEditors.Controls.ComboBoxItemCollection collection = ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)barEditItem2.Edit).Items;
            foreach (string str in collection) 
            {
                this.Text += str;
            }

        }
    }
}
