using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    public class Hub
    {
        public int HubId { get; set; }
        public string Name { get; set; }
        public int HubOwnerId { get; set; }
    }
}