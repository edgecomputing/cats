using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class LedgerMap : EntityTypeConfiguration<Ledger>
    {
        public LedgerMap()
        {
            // Primary Key
            this.HasKey(t => t.LedgerID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Ledger");
            this.Property(t => t.LedgerID).HasColumnName("LedgerID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LedgerTypeID).HasColumnName("LedgerTypeID");

            // Relationships
            this.HasRequired(t => t.LedgerType)
                .WithMany(t => t.Ledgers)
                .HasForeignKey(d => d.LedgerTypeID);

        }
    }
}
