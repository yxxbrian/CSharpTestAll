using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook_Singleton
{
     internal class USBDetector
    {
        static private readonly object synObject = new object();
        private USBDetector() 
        {
            //
        }

        static private USBDetector _instance = null;

        static public USBDetector Instance 
        {
            get 
            {
                if (_instance == null)
                {
                    lock (synObject)
                    {
                        if (_instance == null)
                            _instance = new USBDetector();
                    }
                }
                return _instance;
            }
        }

        public void MsgShow() 
        {
            System.Windows.Forms.MessageBox.Show("Ruh-Huh");
            actionMethod();
            System.Windows.Forms.MessageBox.Show(funcMethod(true));
            System.Windows.Forms.MessageBox.Show(funcMethod1());
        }


        Func<bool, string> funcMethod = delegate(bool param) { return param == true ? "true" : "false"; };
        Action actionMethod = new Action(delegate { System.Windows.Forms.MessageBox.Show("Good"); });
        Func< string> funcMethod1 = () => { return "true" ; };

    }
}
