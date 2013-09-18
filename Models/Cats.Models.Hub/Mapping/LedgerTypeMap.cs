using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class LedgerTypeMap : EntityTypeConfiguration<LedgerType>
    {
        public LedgerTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.LedgerTypeID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Direction)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("LedgerType");
            this.Property(t => t.LedgerTypeID).HasColumnName("LedgerTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Direction).HasColumnName("Direction");
        }
    }
}
