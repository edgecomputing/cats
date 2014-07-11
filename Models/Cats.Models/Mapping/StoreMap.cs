using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
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
            //this.HasRequired(t => t.Hub)
            //    .WithMany(t => t.Stores)
            //    .HasForeignKey(d => d.HubID);

        }
    }
}
