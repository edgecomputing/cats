using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransportBidQuotationHeaderMap : EntityTypeConfiguration<TransportBidQuotationHeader>
    {
        public TransportBidQuotationHeaderMap()
        {
            // Primary Key
            this.HasKey(t => t.TransportBidQuotationHeaddrID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransportBidQuotationHeader", "Procurement");
            this.Property(t => t.TransportBidQuotationHeaddrID).HasColumnName("TransportBidQuotationHeaddrID");
            this.Property(t => t.BidQuotationDate).HasColumnName("BidQuotationDate");
            this.Property(t => t.BidBondAmount).HasColumnName("BidBondAmount");
        }
    }
}
