using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
    public enum REGIONAL_REQUEST_STATUS
    {
        Draft=1,
        Submitted=2,
        Closed=3
    }

    public static class RegionalRequestStatuses
{
        public class RegionalRequestStatus
        {
            public int Value { get; set; }
            public string Name { get; set; }
        }
        public static List<RegionalRequestStatus> GetAllStatus()
    {
        return new List<RegionalRequestStatus>()
                   {
                       new RegionalRequestStatus{Name=REGIONAL_REQUEST_STATUS.Draft.ToString(),Value=(int)REGIONAL_REQUEST_STATUS.Draft },
                         new RegionalRequestStatus{Name=REGIONAL_REQUEST_STATUS.Submitted.ToString(),Value=(int)REGIONAL_REQUEST_STATUS.Submitted },
                           new RegionalRequestStatus{Name=REGIONAL_REQUEST_STATUS.Closed.ToString(),Value=(int)REGIONAL_REQUEST_STATUS.Closed }

                   };
    }  
}
}
