using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.Micro;
using Cats.Data.Micro.Models;


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
        
        public List<object> GetRecentRequisitions(int regionID)
        {
            var requisitions = new RecentRequisition();
            var limResult =
                requisitions.Query(
                    "SELECT TOP 5 * FROM Dashborad_Regional_Requisitions WHERE RegionID=@0 ORDER BY RequestedDate",
                    args: regionID);
            return limResult.ToList();
        }

        public List<object> RequisitionsPercentage(int regionID)
        {
            dynamic requisitions = new RequisitionPercentage();
            var status =
                requisitions.Query("SELECT * FROM Dashboard_regional_ReqPercentage WHERE RegionID=@0",args:regionID);

            foreach (var statu in status)
            {
                //statu.
            }
            
            return status.ToList();
        }

        public List<object> GetRecentDispatches(int regionID)
        {
            var requisitions = new RecentDispatches();
            var limResult =
                requisitions.Query(
                    "SELECT TOP 5 * FROM Dashboard_Regional_Dispatches WHERE RegionID=@0 ORDER BY DispatchDate",
                    args: regionID);
            return limResult.ToList();
        }
        public void Dispose()
        {
         
        }
    }
}