using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Master
    {
        public Master()
        {
            this.Details = new List<Detail>();
        }
        [Key]
        public int MasterID { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
    }
}
