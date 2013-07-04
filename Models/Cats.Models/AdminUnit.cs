using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class AdminUnit
    {
        public AdminUnit()
        {
            this.AdminUnit1 = new List<AdminUnit>();
            this.BidDetails = new List<BidDetail>();
            this.FDPs = new List<FDP>();
            this.RegionalRequests = new List<RegionalRequest>();
            this.ReliefRequisitions = new List<ReliefRequisition>();
            this.ReliefRequisitions1 = new List<ReliefRequisition>();
           
        }

        public int AdminUnitID { get; set; }
        public string Name { get; set; }
        public string NameAM { get; set; }
        public Nullable<int> AdminUnitTypeID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public virtual ICollection<AdminUnit> AdminUnit1 { get; set; }
        public virtual AdminUnit AdminUnit2 { get; set; }
        public virtual AdminUnitType AdminUnitType { get; set; }
        public virtual ICollection<BidDetail> BidDetails { get; set; }
        public virtual ICollection<FDP> FDPs { get; set; }
        public ICollection<RegionalRequest> RegionalRequests { get; set; }
        public ICollection<ReliefRequisition> ReliefRequisitions { get; set; }
        public virtual ICollection<ReliefRequisition> ReliefRequisitions1 { get; set; }    
    }
}
