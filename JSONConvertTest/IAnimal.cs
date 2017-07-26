using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONConvertTest
{
    interface IAnimal
    {
        string strain
        {
            get;
            set;
        }
        void Bark();
    }
}
