using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace PropertyGridEventTest
{
    class Class1
    {

        public string DD = "DDDDD0";
        public string DD1 = "DDDDD1";
        public string DD2 = "DDDDD2";
        public int dsd = 1212;


        [Category("DD")]
        public string sd 
        {
            get 
            {
                return DD;
            }
            set 
            {
                this.DD = value;
            }
        }

        [Category("FF")]
        public string sdf
        {
            get
            {
                return DD1;
            }
            set
            {
                this.DD1 = value;
            }
        }

        [Category("GG")]
        public string sdfff
        {
            get
            {
                return DD2;
            }
            set
            {
                this.DD2 = value;
            }
        }

        [Category("DD")]
        public string df
        {
            get
            {
                return DD;
            }
            set
            {
                this.DD = value;
            }
        }

    }
}
