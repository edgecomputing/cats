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

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TypeOfNotification)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Notification");
            this.Property(t => t.NotificationId).HasColumnName("NotificationId");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.RecordId).HasColumnName("RecordId");
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.TypeOfNotification).HasColumnName("TypeOfNotification");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Application).HasColumnName("Application");

           
        }
    }
}
