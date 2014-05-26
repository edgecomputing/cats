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
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.OpeningDate).HasColumnName("OpeningDate");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.TransportBidPlanID).HasColumnName("TransportBidPlanID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            
        }
    }
}
