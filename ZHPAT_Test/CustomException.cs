using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZHPAT_Test
{
    [Serializable]
    class CustomException:Exception
    {
        public CustomException():base()
        {
        }
        
        private CustomException(string message):base(message) 
        {   
        }

        public CustomException(string message, Exception innerException):base(message,innerException) 
        {
        }
    }
}
