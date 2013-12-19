using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransportBidQuotationMap : EntityTypeConfiguration<TransportBidQuotation>
    {
        public TransportBidQuotationMap()
        {
            this.ToTable("Procurement.TransportBidQuotation");
           // this.ToTable("Procurement.Transporter");
            this.Property(t => t.TransportBidQuotationID).HasColumnName("TransportBidQuotationID");
            this.Property(t => t.TransportBidQuotationHeaddrID).HasColumnName("TransportBidQuotationHeaddrID");

            this.HasRequired(t => t.Bid)
                    .WithMany(t => t.TransportBidQuotations)
                    .HasForeignKey(d => d.BidID);
            
            //this.HasOptional(t => t.Region)
            //    .WithMany(t => t.TransportBidQuotations)
            //    .HasForeignKey(d => d.RegionID);

            this.HasRequired(t => t.Transporter)
                    .WithMany(t => t.TransportBidQuotations)
                    .HasForeignKey(d => d.TransporterID);
            
            this.HasRequired(t => t.Source)
                    .WithMany(t => t.TransportBidQuotations)
                    .HasForeignKey(d => d.SourceID);
            
            this.HasRequired(t => t.Destination)
                    .WithMany(t => t.TransportBidQuotations)
                    .HasForeignKey(d => d.DestinationID);
            
            this.Property(t => t.Tariff).HasColumnName("Tariff");

            this.Property(t => t.IsWinner).HasColumnName("IsWinner");

            this.Property(t => t.Position).HasColumnName("Position");

            this.Property(t => t.Remark).HasColumnName("Remark");

            //this.Property(t => t.RegionID).HasColumnName("RegionID");

            // Relationships
            this.HasOptional(t => t.TransportBidQuotationHeader)
                .WithMany(t => t.TransportBidQuotations)
                .HasForeignKey(d => d.TransportBidQuotationHeaddrID);
        }
    }
}