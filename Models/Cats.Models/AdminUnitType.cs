using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class AdminUnitType
    {
        public AdminUnitType()
        {
            this.AdminUnits = new List<AdminUnit>();
            
        }

        [Key]
        public int AdminUnitTypeID { get; set; }
        public string Name { get; set; }
        public string NameAM { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<AdminUnit> AdminUnits { get; set; }
        
    }
}
