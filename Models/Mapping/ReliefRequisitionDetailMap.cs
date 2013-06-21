using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ReliefRequisitionDetailMap:EntityTypeConfiguration<ReliefRequisitionDetail>
    {
        public ReliefRequisitionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ReliefRequisitionDetailId);

            // Properties
            this.Property(t => t.NoOfBeneficiaries)
                .IsRequired();

            this.Property(t => t.Amount)
                .IsRequired();


            // Table & Column Mappings
            this.ToTable("ReliefRequisitionDetail");
            this.Property(t => t.ReliefRequisitionDetailId).HasColumnName("ReliefRequisitionDetailID");
            this.Property(t => t.ReliefRequistionId).HasColumnName("ReliefRequistionID");
            this.Property(t => t.CommodityId).HasColumnName("CommodityID");
            this.Property(t => t.DonorId).HasColumnName("DonorID");
            this.Property(t => t.NoOfBeneficiaries).HasColumnName("NoOfBeneficiaries");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Fdpid).HasColumnName("FDPID");

            // Relationships
            this.HasRequired(t => t.ReliefRequistion)
                .WithMany(t => t.ReliefRequisitionDetails)
                .HasForeignKey(d => d.ReliefRequistionId);
            
        }
    }
}
