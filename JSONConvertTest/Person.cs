using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JSONConvertTest
{
    
    class Person:IComparable<Person>
    {

        public const int test = 0;

        public Person(string name, uint age,SEX sex)
        {
            this.name = name;
            this.age = age;
        }

        [JsonProperty]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        string name = string.Empty;

        [JsonProperty]
        public uint Age 
        {
            get 
            {
                return age;
            }
            set 
            {
                this.age = value;
            }
        }

        uint age = 0;

        public int CompareTo(Person personB)
        {
            if (this.age > personB.age)
                return 1;
            else if (this.age == personB.age)
                return 0;
            else return -1;
        }

    }
}
