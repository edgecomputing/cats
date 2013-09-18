using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Cats.Models.Exceptions
{
    public class disabledUserException : Exception, ISerializable
    {
        private const int exceptionCode = 3;
        private const string exceptionDetail = "The user account is currently disabled";
        private const string exceptionTip = "Please contact your administrator";

        public disabledUserException() { }
        public disabledUserException(string message) { }
        public disabledUserException(Exception innerExeption) { }
        public disabledUserException(string message, Exception innerException) { }

        public string detail { get { return exceptionDetail; } }
        public string tip { get { return exceptionTip; } }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.Append(detail);
            message.Append(".");
            message.Append(tip);
            message.Append(".");
            return message.ToString();
        }
    }
}