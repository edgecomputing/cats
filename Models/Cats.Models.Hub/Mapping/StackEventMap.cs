using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class StackEventMap : EntityTypeConfiguration<StackEvent>
    {
        public StackEventMap()
        {
            // Primary Key
            this.HasKey(t => t.StackEventID);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(4000);

            this.Property(t => t.Recommendation)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("StackEvent");
            this.Property(t => t.StackEventID).HasColumnName("StackEventID");
            this.Property(t => t.EventDate).HasColumnName("EventDate");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.StoreID).HasColumnName("StoreID");
            this.Property(t => t.StackEventTypeID).HasColumnName("StackEventTypeID");
            this.Property(t => t.StackNumber).HasColumnName("StackNumber");
            this.Property(t => t.FollowUpDate).HasColumnName("FollowUpDate");
            this.Property(t => t.FollowUpPerformed).HasColumnName("FollowUpPerformed");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Recommendation).HasColumnName("Recommendation");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");

            // Relationships
            this.HasRequired(t => t.StackEventType)
                .WithMany(t => t.StackEvents)
                .HasForeignKey(d => d.StackEventTypeID);

        }
    }
}
