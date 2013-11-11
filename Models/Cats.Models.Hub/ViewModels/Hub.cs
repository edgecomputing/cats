using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs
{
    public partial class Hub
    {
         [NotMapped]
        public string HubNameWithOwner {
            get { return this.Name + " : " + this.HubOwner.Name; } 
        }
    }
}
