using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Exceptions.EarlyWarning
{
    class AlreadyExistingHrdExeception :Exception, ISerializable
    {
        private const int exceptionCode = 3;
        private const string exceptionDetail = "HRD For this Season and Year is already exist ";
        private const string exceptionTip = "Please try another season";

        public AlreadyExistingHrdExeception() { }
        public AlreadyExistingHrdExeception(string message) { }
        public AlreadyExistingHrdExeception(Exception innerExeption) { }
        public AlreadyExistingHrdExeception(string message, Exception innerException) { }

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
