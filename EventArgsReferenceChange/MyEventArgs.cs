using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventArgsReferenceChange
{
    public class MyEventArgs:EventArgs
    {
        public MyEventArgs(int message) 
        {
            this.message = message;
        }

        private readonly int message = 0;
        public  int Message
        {
            get { return message; }
        }



        private  int message_readWrite = 0;
        public int Message_ReadWrite
        {
            get { return message_readWrite; }
            set { this.message_readWrite = value; }
        }
    }
}
