using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ReliefRequistionMap : EntityTypeConfiguration<ReliefRequistion>
    {
        public ReliefRequistionMap()
        {
            // Primary Key
            this.HasKey(t => t.ReliefRequistionId);

            // Properties
            this.Property(t => t.RequistionDate)
                .IsRequired();
            this.Property(t => t.Remark)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("Commodity");
            this.Property(t => t.ReliefRequistionId).HasColumnName("ReliefRequistionID");
            this.Property(t => t.RequistionDate).HasColumnName("RequisitionDate");
            this.Property(t => t.ProgramId).HasColumnName("ProgramID");
            this.Property(t => t.RoundId).HasColumnName("RoundID");
            this.Property(t => t.RequestedByUserId).HasColumnName("RequestedByUserID");
            this.Property(t => t.CertifiedByUserId).HasColumnName("CertifiedByUserID");
            this.Property(t => t.AuthorisedByUserId).HasColumnName("AuthorisedByUserId");

            // Relationships
           
           
        }
    }
}
