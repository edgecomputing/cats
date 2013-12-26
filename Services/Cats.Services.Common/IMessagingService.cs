using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Services.Common
{
    public interface IMessagingService
    {
        bool SendMessage(string message, string address);
    }
}
