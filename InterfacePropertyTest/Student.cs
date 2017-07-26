using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacePropertyTest
{
    class Student:IStudent
    {
        string IStudent.Name 
        {
            get 
            {
                return this.name;
            }
            set 
            {
                this.name = "I" + value;
            }
        }

        public string Name 
        {
            get 
            {
                return this.name;
            }
            set 
            {
                this.name = value;
            }
        }

        string name = string.Empty;

    }
}
