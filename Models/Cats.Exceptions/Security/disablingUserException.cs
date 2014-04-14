using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Cats.Models.Exceptions
{
    public class DisablingUserException : Exception, ISerializable
    {
        private const int exceptionCode = 4;
        private string exceptionDetail = "An error occured while disabling ";
        private const string exceptionTip = "";

        public DisablingUserException() { }
        public DisablingUserException(string message) { }
        public DisablingUserException(params object[] args)
        {
            exceptionDetail += args[0];
        }
        public DisablingUserException(Exception innerExeption) { }
        public DisablingUserException(string message, Exception innerException) { }

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