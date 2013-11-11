using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class NotificationMap : EntityTypeConfiguration<Notification>
    {
        public NotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.NotificationId);

         

            // Table & Column Mappings
            this.ToTable("Notification");
            this.Property(t => t.NotificationId).HasColumnName("NotificationId");
            this.Property(t => t.ReqID).HasColumnName("ReqID");
            this.Property(t => t.TransportOrderID).HasColumnName("TransportOrderID");
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.NotifiedUser).HasColumnName("NotifiedUser");
            this.Property(t => t.NotificationDate).HasColumnName("NotificationDate");
        }
    }
}
