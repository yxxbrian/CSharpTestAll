using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace String.EmptyIsNUllOrNot
{
    class Program
    {
        static void Main(string[] args)
        {
            test1_string_Empty_is_Null_Or_Not();
            Console.ReadKey();
        }

        static void test1_string_Empty_is_Null_Or_Not() 
        {
            if (string.Empty == null)
                Console.WriteLine("string.Empty is null");
            else
                Console.WriteLine("string.Empty is not null");
            //not null
        }




        

    }
}
