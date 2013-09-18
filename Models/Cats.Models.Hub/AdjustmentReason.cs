using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class AdjustmentReason
    {
        public AdjustmentReason()
        {
            this.Adjustments = new List<Adjustment>();
        }
        [Key]
        public int AdjustmentReasonID { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public virtual ICollection<Adjustment> Adjustments { get; set; }
    }
}
