using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Exceptions
{
    public class PageNotFoundException:Exception
    {               
        public PageNotFoundException(string message):base(message){}       
        public PageNotFoundException(string message, Exception innerException):base(message,innerException){}

        public override string ToString()
        {
            return string.Format("The requested page cannot be found in the translations database");
        }
    }
}
