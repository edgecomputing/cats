using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransportBidQuotationMap : EntityTypeConfiguration<TransportBidQuotation>
    {
        public TransportBidQuotationMap()
        {
            // Primary Key
            this.HasKey(t => t.TransportBidQuotationID);

            // Properties
            this.Property(t => t.Remark)
                .HasMaxLength(50);
            
            // Table & Column Mappings
            this.ToTable("TransportBidQuotation", "Procurement");
            this.Property(t => t.TransportBidQuotationID).HasColumnName("TransportBidQuotationID");
            this.Property(t => t.TransportBidQuotationHeaderID).HasColumnName("TransportBidQuotationHeaderID");
            this.Property(t => t.BidID).HasColumnName("BidID");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.DestinationID).HasColumnName("DestinationID");
            this.Property(t => t.Tariff).HasColumnName("Tariff");
            this.Property(t => t.IsWinner).HasColumnName("IsWinner");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            // Relationships
            this.HasRequired(t => t.TransportBidQuotationHeader)
                .WithMany(t => t.TransportBidQuotations)
                .HasForeignKey(d => d.TransportBidQuotationHeaderID);

            this.HasRequired(t => t.AdminUnit)
               .WithMany(t => t.TransportBidQuotations)
               .HasForeignKey(d => d.DestinationID);

            this.HasRequired(t => t.Hub)
                .WithMany(t => t.TransportBidQuotations)
                .HasForeignKey(d => d.SourceID);

            
          

           
        }
    }
}