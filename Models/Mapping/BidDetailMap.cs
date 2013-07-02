using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    class BidDetailMap:EntityTypeConfiguration<BidDetail>
    {
        public BidDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.BidDetailID);

            // Properties
            this.Property(t => t.BidDocumentPrice)
                .IsRequired();
            this.Property(t => t.CBO).IsRequired();
          
            // Table & Column Mappings
            this.ToTable("Procurement.BidDetail");
            this.Property(t => t.BidDetailID).HasColumnName("BidDetailID");
            this.Property(t => t.BidID).HasColumnName("BidID");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.AmountForReliefProgram).HasColumnName("AmountForReliefProgram");
            this.Property(t => t.AmountForPSNPProgram).HasColumnName("AmountForPSNPProgram");
            this.Property(t => t.BidDocumentPrice).HasColumnName("BidDocumentPrice");
            this.Property(t => t.CBO).HasColumnName("CBO");

            // Relationships
            this.HasRequired(t => t.Bid)
                .WithMany(t => t.BidDetails)
                .HasForeignKey(d => d.BidID);

            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.BidDetails)
                .HasForeignKey(d => d.RegionID);
        }
    }
}
