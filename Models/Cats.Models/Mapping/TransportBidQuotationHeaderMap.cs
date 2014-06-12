using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransportBidQuotationHeaderMap : EntityTypeConfiguration<TransportBidQuotationHeader>
    {
        public TransportBidQuotationHeaderMap()
        {
            // Primary Key
            this.HasKey(t => t.TransportBidQuotationHeaderID);

            // Properties
            this.Property(t => t.EnteredBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransportBidQuotationHeader", "Procurement");
            this.Property(t => t.TransportBidQuotationHeaderID).HasColumnName("TransportBidQuotationHeaderID");
            this.Property(t => t.BidQuotationDate).HasColumnName("BidQuotationDate");
            //this.Property(t => t.BidBondAmount).HasColumnName("BidBondAmount");
            this.Property(t => t.TransporterId).HasColumnName("TransporterId");
            this.Property(t => t.BidId).HasColumnName("BidId");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.EnteredBy).HasColumnName("EnteredBy");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            // Relationships
            this.HasOptional(t => t.AdminUnit)
                .WithMany(t => t.TransportBidQuotationHeaders)
                .HasForeignKey(d => d.RegionID);
            this.HasOptional(t => t.Bid)
                .WithMany(t => t.TransportBidQuotationHeaders)
                .HasForeignKey(d => d.BidId);
            this.HasOptional(t => t.Transporter)
                .WithMany(t => t.TransportBidQuotationHeaders)
                .HasForeignKey(d => d.TransporterId);
        }
    }
}