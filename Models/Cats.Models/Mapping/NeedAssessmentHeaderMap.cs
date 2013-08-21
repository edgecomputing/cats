
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class NeedAssessmentHeaderMap : EntityTypeConfiguration<NeedAssessmentHeader>
    {
        public NeedAssessmentHeaderMap()
        {
            // Primary Key
            this.HasKey(t => t.NAHeaderId);

            // Properties
            this.Property(t => t.Remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NeedAssessmentHeader", "EarlyWarning");
            this.Property(t => t.NAHeaderId).HasColumnName("NAHeaderId");
            this.Property(t => t.NeedAID).HasColumnName("NeedAID");
            this.Property(t => t.Zone).HasColumnName("Zone");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasOptional(t => t.AdminUnitType)
                .WithMany(t => t.NeedAssessmentHeaders)
                .HasForeignKey(d => d.Zone);
            this.HasOptional(t => t.NeedAssessment)
                .WithMany(t => t.NeedAssessmentHeaders)
                .HasForeignKey(d => d.NeedAID);

        }
    }
}
