using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Cats.Models.Exceptions
{
    public class UserNotFoundException : Exception, ISerializable
    {
        private const int exceptionCode = 2;
        private const string exceptionDetail = "User with the given credential is not found on our system";
        private const string exceptionTip = "Please make sure you have typed both your username and password correctly";

        public UserNotFoundException() { }
        public UserNotFoundException(string message)  {   }
        public UserNotFoundException(Exception innerException){   } 
        public UserNotFoundException(string message, Exception innerException) { }

        public string detail { get { return exceptionDetail; } }
        public string tip { get { return exceptionTip; } }
        
        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.Append(detail);
            message.Append(Environment.NewLine);
            message.Append(tip);
            return message.ToString();
        }
    }
}