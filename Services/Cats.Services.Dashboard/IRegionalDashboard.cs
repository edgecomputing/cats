using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Services.Dashboard
{
    public interface IRegionalDashboard:IDisposable
    {
        List<Object> GetRecentRequests(int regionID);
    }
}