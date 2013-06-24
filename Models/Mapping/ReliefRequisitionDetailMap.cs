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
            this.Property(t => t.Beneficiaries)
                .IsRequired();



            // Table & Column Mappings
            this.ToTable("EarlyWarning.ReliefRequisitionDetail");
            this.Property(t => t.ReliefRequisitionDetailId).HasColumnName("ReliefRequisitionDetailID");
            this.Property(t => t.ReliefRequistionId).HasColumnName("ReliefRequistionID");
            this.Property(t => t.Fdpid).HasColumnName("FDPID");
            this.Property(t => t.Beneficiaries).HasColumnName("Beneficiaries");
            

            // Relationships
            this.HasRequired(t => t.ReliefRequistion)
                .WithMany(t => t.ReliefRequisitionDetails)
                .HasForeignKey(d => d.ReliefRequistionId);
            
        }
    }
}
