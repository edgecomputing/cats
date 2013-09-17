using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace Cats.Models.Exceptions
{
    public class unmatchingUsernameAndPasswordException : Exception, ISerializable
    {
        private string exceptionDetail = "User password and username don't match";
        private string exceptionTip = "Check your spelling, notice capitalization";

        public unmatchingUsernameAndPasswordException() { }

        public unmatchingUsernameAndPasswordException(string message) {
            
        }

        public unmatchingUsernameAndPasswordException(string message, Exception innerException) { }

        public string detail {get{return exceptionDetail;}}

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