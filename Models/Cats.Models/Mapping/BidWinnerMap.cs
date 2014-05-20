using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class BidWinnerMap : EntityTypeConfiguration<BidWinner>
    {
        public BidWinnerMap()
        {
            // Primary Key
            this.HasKey(t => t.BidWinnerID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BidWinner", "Procurement");
            this.Property(t => t.BidWinnerID).HasColumnName("BidWinnerID");
            this.Property(t => t.BidID).HasColumnName("BidID");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.DestinationID).HasColumnName("DestinationID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Tariff).HasColumnName("Tariff");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ExpiryDate).HasColumnName("expiryDate");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            // Relationships
            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.DestinationID);
            this.HasOptional(t => t.Commodity)
                .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.SourceID);
            this.HasRequired(t => t.Bid)
                .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.BidID);
            this.HasRequired(t => t.Transporter)
                .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.TransporterID);

            this.HasRequired(t => t.BusinessProcess)
                         .WithMany(t => t.BidWinners)
                         .HasForeignKey(d => d.BusinessProcessID);
        }
    }
}
