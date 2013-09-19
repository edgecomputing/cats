using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Cats.Models.Exceptions
{
    public class unabletoResetPasswordException : Exception, ISerializable
    {
        private const int exceptionCode = 1;
        private string exceptionDetail = "Uable to reset user password for ";
        private  string exceptionTip = "";

        public unabletoResetPasswordException() { }
        public unabletoResetPasswordException(string message) { }
        public unabletoResetPasswordException(Exception innerExeption) { }
        public unabletoResetPasswordException(string message, Exception innerException) { }
        
        // user name as an argumnet 
        public unabletoResetPasswordException(params object[] args) {
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