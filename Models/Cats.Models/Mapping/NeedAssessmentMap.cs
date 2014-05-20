using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;


namespace Cats.Models.Mapping
{
    public class NeedAssessmentMap:EntityTypeConfiguration<NeedAssessment>
    {
        public NeedAssessmentMap()
        {
            // Primary Key
            this.HasKey(t => t.NeedAID);

            

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("NeedAssessment", "EarlyWarning");
            this.Property(t => t.NeedAID).HasColumnName("NeedAID");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.Season).HasColumnName("Season");
            this.Property(t => t.NeedADate).HasColumnName("NeedADate");
            this.Property(t => t.NeddACreatedBy).HasColumnName("NeddACreatedBy");
            this.Property(t => t.NeedAApproved).HasColumnName("NeedAApproved");
            this.Property(t => t.NeedAApprovedBy).HasColumnName("NeedAApprovedBy");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.PlanID).HasColumnName("PlanID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            // Relationships
            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.NeedAssessments)
                .HasForeignKey(d => d.Region);
            this.HasOptional(t => t.UserProfile)
                .WithMany(t => t.NeedAssessments)
                .HasForeignKey(d => d.NeddACreatedBy);
            this.HasOptional(t => t.UserProfile1)
                .WithMany(t => t.NeedAssessments1)
                .HasForeignKey(d => d.NeedAApprovedBy);
            this.HasRequired(t => t.Season1)
                .WithMany(t => t.NeedAssessments)
                .HasForeignKey(d => d.Season);
             this.HasOptional(t => t.TypeOfNeedAssessment1)
                .WithMany(t => t.NeedAssessments)
                .HasForeignKey(d => d.TypeOfNeedAssessment);
             this.HasRequired(t => t.Plan)
                .WithMany(t => t.NeedAssessments)
                .HasForeignKey(d => d.PlanID);

        }
    }
}
