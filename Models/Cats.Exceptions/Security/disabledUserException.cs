
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Cats.Exceptions.Security
{
    public class disabledUserException: Exception, ISerializable
    {
         public disabledUserException() {
            ExceptionMessage = "Incorrect Password";
        }
    
        public disabledUserException(string message) { }
    
        public disabledUserException(string message, Exception innerException) { }
        
        public string ExceptionMessage { get; set; }
        
        public override string ToString()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(ExceptionMessage);
            msg.Append(Environment.NewLine);
            return ExceptionMessage;
        }
    }
}
