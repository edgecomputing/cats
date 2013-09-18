using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class Period
    {
        [Key]
        public int PeriodID { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
    }
}
