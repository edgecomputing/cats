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
        public List<object> GetRecentRequests()
        {
            var table = new UserProfile();
            var products = table.All();
            return products.ToList();
        }

        public void Dispose()
        {
         
        }
    }
}