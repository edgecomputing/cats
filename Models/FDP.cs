using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public partial class FDP
    {
        public FDP()
        {
            //this.Contacts = new List<Contact>();
            //this.Dispatches = new List<Dispatch>();
            //this.DispatchAllocations = new List<DispatchAllocation>();
        }
        [Key]
        public int FDPID { get; set; }
        public string Name { get; set; }
        public string NameAM { get; set; }
        public int AdminUnitID { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        //public virtual ICollection<Contact> Contacts { get; set; }
        //public virtual ICollection<Dispatch> Dispatches { get; set; }
        //public virtual ICollection<DispatchAllocation> DispatchAllocations { get; set; }
    }
}
