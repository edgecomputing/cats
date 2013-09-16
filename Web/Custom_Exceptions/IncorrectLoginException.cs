using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Custom_Exceptions
{
    public class IncorrectLoginException: Exception
    {
        public IncorrectLoginException() : base() { }
    
        public IncorrectLoginException(string message)
            : base(message) { }
    
        public IncorrectLoginException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    
        public IncorrectLoginException(string message, Exception innerException)
            : base(message, innerException) { }
    
        public IncorrectLoginException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
        
    }
}