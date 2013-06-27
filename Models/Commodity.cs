using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class Commodity
    {
        public Commodity()
        {
            
            this.Commodity1 = new List<Commodity>();
           
        }
        [Key]
        public int CommodityID { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string NameAM { get; set; }
        public string CommodityCode { get; set; }
        public int CommodityTypeID { get; set; }
        public Nullable<int> ParentID { get; set; }
  
        public virtual ICollection<Commodity> Commodity1 { get; set; }
        public virtual Commodity Commodity2 { get; set; }

        public virtual CommodityType CommodityType { get; set; }
        public ICollection<ReliefRequisition> ReliefRequisitions { get; set; }
        public ICollection<ReliefRequisitionDetial> ReliefRequisitionDetials { get; set; } 
      
    }
}
