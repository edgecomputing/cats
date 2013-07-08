using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class Commodity
    {
        public Commodity()
        {
            this.DispatchAllocations = new List<DispatchAllocation>();
            this.Commodity1 = new List<Commodity>();
           
            this.ReliefRequisitionDetails = new List<ReliefRequisitionDetail>();
          
        }

        public int CommodityID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string NameAM { get; set; }
        public string CommodityCode { get; set; }
        public int CommodityTypeID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
        public virtual ICollection<Commodity> Commodity1 { get; set; }
        public virtual Commodity Commodity2 { get; set; }
      
        public virtual CommodityType CommodityType { get; set; }
     
        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public ICollection<ReliefRequisition> ReliefRequisitions { get; set; }
      
    }
}
