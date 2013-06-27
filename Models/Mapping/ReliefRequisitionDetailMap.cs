using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ReliefRequisitionDetailMap:EntityTypeConfiguration<ReliefRequisitionDetail>
    {
        public ReliefRequisitionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ReliefRequisitionDetialID);

            // Properties
            this.Property(t => t.Beneficiaries)
                .IsRequired();



            // Table & Column Mappings
            this.ToTable("EarlyWarning.ReliefRequisitionDetail");
            this.Property(t => t.ReliefRequisitionDetialID).HasColumnName("ReliefRequisitionDetialID");
            this.Property(t => t.RequisitionID).HasColumnName("RequisitionID");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.Beneficiaries).HasColumnName("Beneficiaries");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.DonorID).HasColumnName("DonorID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            
            

            // Relationships
            this.HasRequired(t => t.ReliefRequisition)
                .WithMany(t => t.ReliefRequisitionDetials)
                .HasForeignKey(d => d.RequisitionID);

            this.HasRequired(t => t.Fdps)
                .WithMany(t => t.ReliefRequisitionDetials)
                .HasForeignKey(d => d.FDPID);
            this.HasRequired(t => t.Commodity)
               .WithMany(t => t.ReliefRequisitionDetials)
               .HasForeignKey(d => d.CommodityID);


        }
    }
}
