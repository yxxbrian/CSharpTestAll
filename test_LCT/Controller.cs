using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test_LCT
{
    class Controller
    {
        protected Controller(IRelation receptor) 
        {
            this.receptor = receptor;
        }

        EventHandler eventHandler;
        public Controller() 
        {
            if (eventHandler != null)
                eventHandler = null;
            eventHandler += function;
            eventHandler( new object(),new EventArgs());
        }

        public void function(object sender, EventArgs e) 
        {
            MessageBox.Show("Event");
        }

        protected IRelation receptor;

        public void show(int i)
        {
            MessageBox.Show("number:"+i);
        }


    }
}
