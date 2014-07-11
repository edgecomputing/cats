using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Role
    {
        public Role()
        {
            this.SessionAttempts = new List<SessionAttempt>();
            this.SessionHistories = new List<SessionHistory>();
            this.UserRoles = new List<UserRole>();
        }
        [Key]
        public int RoleID { get; set; }
        public int SortOrder { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SessionAttempt> SessionAttempts { get; set; }
        public virtual ICollection<SessionHistory> SessionHistories { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
