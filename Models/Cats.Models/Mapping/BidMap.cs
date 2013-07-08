using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class BidMap:EntityTypeConfiguration<Bid>
    {

        public BidMap()
        {
            // Primary Key
            this.HasKey(t => t.BidID);

            // Properties
            this.Property(t => t.BidNumber)
                .IsRequired();
          
            // Table & Column Mappings
            this.ToTable("Bid","dbo");
            this.Property(t => t.BidNumber).HasColumnName("BidNumber");
            this.Property(t => t.BidID).HasColumnName("BidID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.OpeningDate).HasColumnName("OpeningDate");
            this.Property(t => t.StatusID).HasColumnName("StatusID");

            // Relationships
            this.HasRequired(t => t.Status)
                .WithMany(t => t.Bids)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
