using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Models
{
    public partial class NeedAssement
    {
        public int NAId { get; set; }
        public Nullable<int> VPoorNoOfM { get; set; }
        public Nullable<int> VPoorNoOfB { get; set; }
        public Nullable<int> PoorNoOfM { get; set; }
        public Nullable<int> PoorNoOfB { get; set; }
        public Nullable<int> MiddleNoOfM { get; set; }
        public Nullable<int> MiddleNoOfB { get; set; }
        public Nullable<int> BOffNoOfM { get; set; }
        public Nullable<int> BOffNoOfB { get; set; }
        public Nullable<int> Zone { get; set; }
        public Nullable<int> District { get; set; }
        //public virtual AdminUnit AdminUnit { get; set; }
        //public virtual AdminUnit AdminUnit1 { get; set; }
    }
}