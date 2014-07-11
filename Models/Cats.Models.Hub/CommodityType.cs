using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class CommodityType
    {
        public CommodityType()
        {
            this.Commodities = new List<Commodity>();
            this.Receives = new List<Receive>();
        }
        [Key]
        public int CommodityTypeID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
        public virtual ICollection<Receive> Receives { get; set; }
    }
}
