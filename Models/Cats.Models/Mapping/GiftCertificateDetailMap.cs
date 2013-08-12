using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
namespace Cats.Models.Mapping
{
    public class GiftCertificateDetailMap : EntityTypeConfiguration<GiftCertificateDetail>
    {
        public GiftCertificateDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.GiftCertificateDetailID);

            // Properties
            this.Property(t => t.BillOfLoading)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GiftCertificateDetail");
            this.Property(t => t.GiftCertificateDetailID).HasColumnName("GiftCertificateDetailID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            this.Property(t => t.GiftCertificateID).HasColumnName("GiftCertificateID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.WeightInMT).HasColumnName("WeightInMT");
            this.Property(t => t.BillOfLoading).HasColumnName("BillOfLoading");
            this.Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            this.Property(t => t.EstimatedPrice).HasColumnName("EstimatedPrice");
            this.Property(t => t.EstimatedTax).HasColumnName("EstimatedTax");
            this.Property(t => t.YearPurchased).HasColumnName("YearPurchased");
            this.Property(t => t.DFundSourceID).HasColumnName("DFundSourceID");
            this.Property(t => t.DCurrencyID).HasColumnName("DCurrencyID");
            this.Property(t => t.DBudgetTypeID).HasColumnName("DBudgetTypeID");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.GiftCertificateDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasRequired(t => t.Detail)
                .WithMany(t => t.GiftCertificateDetails)
                .HasForeignKey(d => d.DFundSourceID);
            this.HasRequired(t => t.Detail1)
                .WithMany(t => t.GiftCertificateDetails1)
                .HasForeignKey(d => d.DCurrencyID);
            this.HasRequired(t => t.Detail2)
                .WithMany(t => t.GiftCertificateDetails2)
                .HasForeignKey(d => d.DBudgetTypeID);
            this.HasRequired(t => t.GiftCertificate)
                .WithMany(t => t.GiftCertificateDetails)
                .HasForeignKey(d => d.GiftCertificateID);

        }
    }
}
