using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class Unit
    {
        public Unit()
        {
            this.RationDetails = new List<RationDetail>();
        }
        [Key]
        public int UnitID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RationDetail> RationDetails { get; set; }

    }
}
