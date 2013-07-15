
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Transaction");
            this.Property(t => t.TransactionID).HasColumnName("TransactionID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
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

            // Relationships
            //this.HasRequired(t => t.Account)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.AccountID);
            //this.HasRequired(t => t.Commodity)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.ParentCommodityID);
            //this.HasRequired(t => t.Commodity1)
            //    .WithMany(t => t.Transactions1)
            //    .HasForeignKey(d => d.CommodityID);
            //this.HasOptional(t => t.CommodityGrade)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.CommodityGradeID);
            //this.HasRequired(t => t.Hub)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.HubID);
            //this.HasRequired(t => t.HubOwner)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.HubOwnerID);
            //this.HasRequired(t => t.Ledger)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.LedgerID);
            //this.HasRequired(t => t.Program)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.ProgramID);
            //this.HasRequired(t => t.ProjectCode)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.ProjectCodeID);
            //this.HasRequired(t => t.ShippingInstruction)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.ShippingInstructionID);
            //this.HasRequired(t => t.Store)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.StoreID);
            //this.HasOptional(t => t.TransactionGroup)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.TransactionGroupID);
            //this.HasRequired(t => t.Unit)
            //    .WithMany(t => t.Transactions)
            //    .HasForeignKey(d => d.UnitID);
        }
    }
}
