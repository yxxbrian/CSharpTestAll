using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONConvertTest
{
    class Dog:IAnimal
    {
        public string strain 
        {
            get 
            {
                return "Dog";
            }
            set 
            {

            }
        }

        public void Bark() 
        {
            Console.WriteLine("Wawa!");
            System.Windows.Forms.MessageBox.Show("Wawa");
        }

    }
}
