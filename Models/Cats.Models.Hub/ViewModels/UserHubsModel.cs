using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Cats.Models.Hubs
{
     [Authorize]
    public class UserHubsModel
    {
        public List<UserHubModel> UserHubs = new List<UserHubModel>(); 
    }
}
