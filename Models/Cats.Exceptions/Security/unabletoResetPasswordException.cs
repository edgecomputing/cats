using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Cats.Models.Exceptions
{
    public class UnabletoResetPasswordException : Exception, ISerializable
    {
        private const int exceptionCode = 1;
        private string exceptionDetail = "Uable to reset user password for ";
        private  string exceptionTip = "";

        public UnabletoResetPasswordException() { }
        public UnabletoResetPasswordException(string message) { }
        public UnabletoResetPasswordException(Exception innerExeption) { }
        public UnabletoResetPasswordException(string message, Exception innerException) { }
        
        // user name as an argumnet 
        public UnabletoResetPasswordException(params object[] args) {
            exceptionDetail += exceptionDetail + args[0].ToString();
        }
        
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