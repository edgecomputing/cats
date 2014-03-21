using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Settings.Models.ViewModels
{
    public class HubUserViewModel
    {
        public int UserProfileID { get; set; }
        public string User { get; set; }
        public string Hub { get; set; }
        public int HubID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public int UserHubID { get; set; }
        public string IsDefault { get; set; }
      
    }
}