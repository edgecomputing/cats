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
            this.ToTable("Bid","procurement");
            this.Property(t => t.BidNumber).HasColumnName("BidNumber");
            this.Property(t => t.BidID).HasColumnName("BidID");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.OpeningDate).HasColumnName("OpeningDate");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.TransportBidPlanID).HasColumnName("TransportBidPlanID");
            this.Property(t => t.BidBondAmount).HasColumnName("BidBondAmount");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            //this.Property(t => t.BidBond).HasColumnName("BidBond");

            this.HasRequired(t => t.AdminUnit)
               .WithMany(t => t.Bids)
               .HasForeignKey(d => d.RegionID);
            
        }
    }
}
