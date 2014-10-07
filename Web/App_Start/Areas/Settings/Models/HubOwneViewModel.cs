using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Settings.Models
{
    public class HubOwnerViewModel
    {
        public int HubOwnerID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string Hub { get; set; }
        public int HubId { get; set; }
       
    }
}