using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class StackEventType
    {
        public StackEventType()
        {
            this.StackEvents = new List<StackEvent>();
        }
        [Key]
        public int StackEventTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Periodic { get; set; }
        public Nullable<int> DefaultFollowUpDuration { get; set; }
        public virtual ICollection<StackEvent> StackEvents { get; set; }
    }
}
