using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class NeedAssessmentDao
    {
        public int NAHeaderId { get; set; }
        public int NeedAID { get; set; }
        public int ZoneId { get; set; }
        public int RegionId { get; set; }
        public string Zone { get; set; }
        public string Region { get; set; }
    }
}
