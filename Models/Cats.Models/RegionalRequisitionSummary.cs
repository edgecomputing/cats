using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class RegionalRequisitionsSummary
    {
        public int? RegionID { get; set; }
        public string RegionName { get; set; }
        public int? NumberOfHubAssignedRequisitions { get; set; }
        public int? NumberOfTotalRequisitions { get; set; }
        public decimal? Percentage { get; set; }
        public DateTime? DateLastModified { get; set; }
        public string ProgramType { get; set; }
    }
}