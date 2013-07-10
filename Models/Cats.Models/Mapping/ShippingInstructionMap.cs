using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    class ShippingInstructionMap : EntityTypeConfiguration<ShippingInstruction>
    {
        public ShippingInstructionMap()
        {
            // Primary Key
            this.HasKey(t => t.ShippingInstructionID);

            // Properties
            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ShippingInstruction");
            this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}
