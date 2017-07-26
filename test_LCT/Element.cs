using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test_LCT
{   
    
    class Element:IRelation
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        public Element()
        {
            this.id = random.NextDouble();
        }

        double id;

        public string RelationName 
        {
            get 
            {
                return this.elementString!=null?this.elementString:"None";
            }
            set 
            {
                this.elementString = value;
            }

        }

        private string elementString;
    }
}
