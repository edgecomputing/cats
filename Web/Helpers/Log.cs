using System;
using log4net;
namespace Cats.Helpers
{
    public  class Logger
    {
        public void  LogAllErrorsMesseges(Exception ex,ILog log)
        {
            //assign the current exception as first object and then loop through its
            //inner exceptions till they are null

            for (var eCurrent = ex; eCurrent != null; eCurrent = eCurrent.InnerException)
            {
                log.Error(eCurrent.Message);
            }
        }
    }
}