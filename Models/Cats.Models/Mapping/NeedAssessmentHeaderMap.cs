
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
            this.Property(t => t.NeedACreatedDate).HasColumnName("NeedACreatedDate");
            this.Property(t => t.NeddACreatedBy).HasColumnName("NeddACreatedBy");
            this.Property(t => t.NeedAApproved).HasColumnName("NeedAApproved");
            this.Property(t => t.Remark).HasColumnName("Remark");


            // Relationships
            this.HasOptional(t => t.UserProfile)
                .WithMany(t => t.NeedAssessmentHeaders)
                .HasForeignKey(d => d.NeddACreatedBy);
        }
    }
}
