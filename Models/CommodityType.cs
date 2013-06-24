using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class CommodityType
    {
        public CommodityType()
        {
            this.Commodities = new List<Commodity>();
           
        }
        [Key]
        public int CommodityTypeID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
       
    }
}
