using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCats.ViewModels;

namespace Cats.Services.Dashboard
{
    public class RegionalDashboard : IRegionalDashboard
    {
        public List<Object> GetRecentRequests(int regionID)
        {
            //regionID = 5;
            var requests = new RegionalRequest();
            //var result = requests.All( columns:"RequestNumber",where:"WHERE RegionID=@0",args:regionID);
            var limResult = requests.Query("SELECT TOP 5 RegionalRequestID,RequestNumber,Month,RequestDate,Status FROM EarlyWarning.RegionalRequest WHERE RegionID=@0 ORDER BY RequestDate", args: regionID);
            return limResult.ToList();

            //dynamic table = new RegionalRequest();
            //var re = table.Find(Categor)
        }

        public void Dispose()
        {
         
        }
    }
}