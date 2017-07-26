using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace EventArgsReferenceChange
{

    
    
    class subClass
    {
        
        
        public subClass() 
        {
            
        }

        public void action() 
        {
            MyEventArgs myE = new MyEventArgs(888);
            OnMyEventHandler(myE);
            MessageBox.Show("Changed?:"+myE.Message_ReadWrite);
        }

        public event MyEventHandler deleg = null;
        private void OnMyEventHandler(MyEventArgs e) 
        {
            if (deleg != null) 
            {
                deleg(this, e);
            }
        }

       
    }
}
