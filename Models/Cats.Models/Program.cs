using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class Program
    {
        public Program()
        {
           
        }
        [Key]
        public int ProgramID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LongName { get; set; }
        public virtual ICollection<RegionalRequest> RegionalRequests { get; set; }
        public ICollection<ReliefRequisition> ReliefRequisitions { get; set; }
        public ICollection<TransportBidPlan> TransportBidPlans { get; set; }
    }
}
