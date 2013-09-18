using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class StoreMap : EntityTypeConfiguration<Store>
    {
        public StoreMap()
        {
            // Primary Key
            this.HasKey(t => t.StoreID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StoreManName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Store");
            this.Property(t => t.StoreID).HasColumnName("StoreID");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.IsTemporary).HasColumnName("IsTemporary");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.StackCount).HasColumnName("StackCount");
            this.Property(t => t.StoreManName).HasColumnName("StoreManName");

            // Relationships
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.Stores)
                .HasForeignKey(d => d.HubID);

        }
    }
}
