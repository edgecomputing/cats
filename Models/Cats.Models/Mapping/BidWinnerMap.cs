using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class BidWinnerMap:EntityTypeConfiguration<BidWinner>
    {
        public BidWinnerMap()
        {
            // Primary Key
            this.HasKey(t => t.BidWinnerID);

            // Properties
            this.Property(t => t.TransporterID).IsRequired();
            this.Property(t => t.BidID).IsRequired();
            this.Property(t => t.DestinationID).IsRequired();
            this.Property(t => t.SourceID).IsRequired();

            // Table & Column Mappings
            this.ToTable("BidWinner", "Procurement");
            this.Property(t => t.BidWinnerID).HasColumnName("BidWinnerID");
            this.Property(t => t.BidID).HasColumnName("BidID");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.DestinationID).HasColumnName("DestinationID");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Tariff).HasColumnName("Tariff");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");

            // Relationships
            this.HasRequired(t => t.Bid)
                .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.BidID);

            this.HasRequired(t => t.Hub)
                .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.SourceID);

            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.DestinationID);

            this.HasRequired(t => t.Transporter)
               .WithMany(t => t.BidWinners)
                .HasForeignKey(d => d.TransporterID);
        }
    }
}
