using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models.ViewModels.Dashboard;

namespace Cats.Services.Dashboard
{
    public interface IRegionalDashboard:IDisposable
    {
        List<RecentRequests> GetRecentRequests(int regionID);
        List<RecentRequisitions> GetRecentRequisitions(int regionID);
        List<Object> RequisitionsPercentage(int regionID);
        List<Object> GetRecentDispatches(int regionID);

    }
}