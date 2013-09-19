using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Cats.Models.Hub
{
     [Authorize]
    public class UserHubsModel
    {
        public List<UserHubModel> UserHubs = new List<UserHubModel>(); 
    }
}
