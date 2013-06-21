using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ReliefRequisitionDetailMap:EntityTypeConfiguration<ReliefRequisitionDetail>
    {
        public ReliefRequisitionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ReliefRequisitionDetailID);

            // Properties
            this.Property(t => t.NoOfBeneficiaries)
                .IsRequired();

            this.Property(t => t.Amount)
                .IsRequired();


            // Table & Column Mappings
            this.ToTable("ReliefRequisitionDetail");
            this.Property(t => t.ReliefRequisitionDetailID).HasColumnName("ReliefRequisitionDetailID");
            this.Property(t => t.ReliefRequistionID).HasColumnName("ReliefRequistionID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.NoOfBeneficiaries).HasColumnName("NoOfBeneficiaries");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.FDPID).HasColumnName("FDPID");

            // Relationships
            this.HasRequired(t => t.ReliefRequistion)
                .WithMany(t => t.ReliefRequisitionDetails)
                .HasForeignKey(d => d.ReliefRequistionID);
            
        }
    }
}
