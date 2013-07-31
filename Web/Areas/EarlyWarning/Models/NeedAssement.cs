using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.EarlyWarning.Models
{
    public class NeedAssement
    {
        public int NAId { get; set; }
        public int VPoorNoOfM { get; set; }
        public int VPoorNoOfB { get; set; }
        public int PoorNoOfM { get; set; }
        public int PoorNoOfB { get; set; }
        public int MiddleNoOfM { get; set; }
        public int MiddleNoOfB { get; set; }
        public int BOffNoOfM { get; set; }
        public int BOffNoOfB { get; set; }
        public int Zone { get; set; }
        public int District { get; set; }

    }
}