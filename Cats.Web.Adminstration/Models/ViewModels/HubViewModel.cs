using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Web.Adminstration.Models.ViewModels
{
    public class HubViewModel
    {
        public int HubID { get; set; }
        public string HubName { get; set; }
        public int HubOwnerID { get; set; }
    }
}