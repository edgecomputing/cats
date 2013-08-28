using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class AccountTransactionMap : EntityTypeConfiguration<AccountTransaction>
    {
        public AccountTransactionMap()
        {
            this.ToTable("AccountTransaction");
            this.Property(t => t.AccountTransactionID).HasColumnName("AccountTransactionID");

            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");

            this.Property(t => t.PartitionID).HasColumnName("PartitionID");

            this.Property(t => t.LedgerID).HasColumnName("LedgerID");

            this.Property(t => t.HubOwnerID).HasColumnName("HubOwnerID");

            this.Property(t => t.AccountID).HasColumnName("AccountID");

            this.Property(t => t.HubID).HasColumnName("HubID");

            this.Property(t => t.StoreID).HasColumnName("StoreID");

            this.Property(t => t.Stack).HasColumnName("Stack");

            this.Property(t => t.ProjectCodeID).HasColumnName("ProjectCodeID");

            this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");

            this.Property(t => t.ProgramID).HasColumnName("ProgramID");

            this.Property(t => t.ParentCommodityID).HasColumnName("ParentCommodityID");

            this.Property(t => t.CommodityID).HasColumnName("CommodityID");

            this.Property(t => t.CommodityGradeID).HasColumnName("CommodityGradeID");

            this.Property(t => t.QuantityInMT).HasColumnName("QuantityInMT");

            this.Property(t => t.QuantityInUnit).HasColumnName("QuantityInUnit");

            this.Property(t => t.UnitID).HasColumnName("UnitID");

            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");

            this.Property(t => t.RegionID).HasColumnName("RegionID");

            this.Property(t => t.Month).HasColumnName("Month");

            this.Property(t => t.Round).HasColumnName("Round");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.CommoditySourceID).HasColumnName("CommoditySourceID");
            this.Property(t => t.GiftTypeID).HasColumnName("GiftTypeID");

        }
    }
}