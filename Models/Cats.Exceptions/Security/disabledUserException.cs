using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Cats.Models.Exceptions
{
    public class DisabledUserException : Exception, ISerializable
    {
        private const int exceptionCode = 3;
        private const string exceptionDetail = "The user account is currently disabled";
        private const string exceptionTip = "Please contact your administrator";

        public DisabledUserException() { }
        public DisabledUserException(string message) { }
        public DisabledUserException(Exception innerExeption) { }
        public DisabledUserException(string message, Exception innerException) { }

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