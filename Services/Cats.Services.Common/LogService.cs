using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Cats.Services.Common
{
    public class Log : ILog
    {
        log4net.ILog _Log;

       
        public Log(log4net.ILog log)
            : this()
        {
            _Log = log;
        }

      
        public Log()
        {
            _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            log4net.Config.XmlConfigurator.Configure();
        }

       
        public void informational(string message)
        {
            _Log.Info(message);
        }

       
        public void informational(string message, Exception innerException)
        {
            _Log.Info(message, innerException);
        }

      
        public void debug(string message)
        {
            _Log.Debug(message);
        }

        
        public void debug(string message, Exception innerException)
        {
            _Log.Debug(message, innerException);
        }

      
        public void error(string message)
        {
            _Log.Error(message);
        }

      
        public void error(string message, Exception innerException)
        {
            _Log.Error(message, innerException);
        }

    
        public void warning(string message)
        {
            _Log.Warn(message);
        }

       
        public void warning(string message, Exception innerException)
        {
            _Log.Warn(message, innerException);
        }
    }
}
