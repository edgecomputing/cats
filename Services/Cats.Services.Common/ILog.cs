using System;


namespace Cats.Services.Common
{
    public interface ILog
    {
        
        void informational(string message);
        void informational(string message, Exception innerException);
        void debug(string message);
        void debug(string message, Exception innerException);
        void error(string message);
        void error(string message, Exception innerException);
        void warning(string message);
        void warning(string message, Exception innerException);
    }
}
