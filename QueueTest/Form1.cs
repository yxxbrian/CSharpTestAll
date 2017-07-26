using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace QueueTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            QueueTest();
        }

        public void QueueTest() 
        {
            Queue<object> queue = new Queue<object>(50);
            queue.Enqueue("One");
            queue.Enqueue("Two");
            queue.Enqueue("Three");
            queue.Enqueue(null);
            queue.Enqueue("over");

            MessageBox.Show("count of queue:"+queue.Count);

            foreach (object obj in queue)
            {
                if (obj != null)
                    MessageBox.Show("" + obj.ToString());
                else
                    MessageBox.Show("NULL");
            }
                
            MessageBox.Show(""+queue.Dequeue());
            if (queue.Contains("Two"))
                MessageBox.Show("Contains Two");

            Array array = queue.ToArray();
            string[] stringArray = array as string[];
            

        }

    }
}
