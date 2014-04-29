using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cats.Models.Hubs
{
    public partial class UserHub
    {
        
        // [Key ,Column(Order=0)]
        public int UserProfileID { get; set; }

    // [Key, Column(Order = 1)]
        public int HubID { get; set; }
        public int UserHubID { get; set; }
        public string IsDefault { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}