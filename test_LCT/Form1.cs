using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test_LCT
{
      delegate void functionDelegate(int i); 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Controller controller = new Controller();
            //functionDelegate Delegate = new functionDelegate(controller.show);
           // Delegate(5);
        }
        DataTable table;
        private void Form1_Load(object sender, EventArgs e)
        {
            BindDataSource();
            
        }

        private void BindDataSource() 
        {
            table = new DataTable();
            table.Columns.Add("relationName", typeof(string));
            table.Columns.Add("relationValue",typeof(string));
            table.Rows.Add(new string[]{"one","one_plus"});
            table.Rows.Add(new string[]{"two","two_plus"});
            this.dataGridView1.DataSource = table;

        }


        Random random = new Random(5);
        private void button1_Click(object sender, EventArgs e)
        {
            
            double value = random.NextDouble();
            this.Text = " " + value;
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }
    }
}
