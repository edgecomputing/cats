using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Cats.Models.Exceptions
{
    public class PasswordChangeException : Exception, ISerializable
    {
        private const int exceptionCode = 4;
        private const string exceptionDetail = "An error occured while changing password";
        private const string exceptionTip = "";

        public PasswordChangeException() { }
        public PasswordChangeException(string message) { }
        public PasswordChangeException(Exception innerExeption) { }
        public PasswordChangeException(string message, Exception innerException) { }

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