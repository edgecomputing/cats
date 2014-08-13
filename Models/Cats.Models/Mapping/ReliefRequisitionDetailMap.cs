using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ReliefRequisitionDetailMap:EntityTypeConfiguration<ReliefRequisitionDetail>
    {
        public ReliefRequisitionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.RequisitionDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ReliefRequisitionDetail", "EarlyWarning");
            this.Property(t => t.RequisitionDetailID).HasColumnName("RequisitionDetailID");
            this.Property(t => t.RequisitionID).HasColumnName("RequisitionID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.BenficiaryNo).HasColumnName("BenficiaryNo");
            this.Property(t => t.Amount).HasColumnName("Amount").HasPrecision(18,4);
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.ReliefRequisitionDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasOptional(t => t.Donor)
                .WithMany(t => t.ReliefRequisitionDetails)
                .HasForeignKey(d => d.DonorID);
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.ReliefRequisitionDetails)
                .HasForeignKey(d => d.FDPID);
            this.HasRequired(t => t.ReliefRequisition)
                .WithMany(t => t.ReliefRequisitionDetails)
                .HasForeignKey(d => d.RequisitionID);

        }
    }
}
