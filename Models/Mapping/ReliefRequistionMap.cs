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
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.RequistionDate).HasColumnName("RequisitionDate");
            this.Property(t => t.ProgramId).HasColumnName("ProgramID");
            this.Property(t => t.RoundId).HasColumnName("RoundID");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasRequired(t => t.Round)
                .WithMany(t => t.ReliefRequistions)
                .HasForeignKey(d => d.RoundId);
           
           
        }
    }
}
