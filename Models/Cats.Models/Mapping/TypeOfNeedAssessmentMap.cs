using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class TypeOfNeedAssessmentMap : EntityTypeConfiguration<TypeOfNeedAssessment>
    {
        public TypeOfNeedAssessmentMap()
        {
            // Primary Key
            this.HasKey(t => t.TypeOfNeedAssessmentID);

            // Properties
            this.Property(t => t.TypeOfNeedAssessment1)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("TypeOfNeedAssessment", "EarlyWarning");
            this.Property(t => t.TypeOfNeedAssessmentID).HasColumnName("TypeOfNeedAssessmentID");
            this.Property(t => t.TypeOfNeedAssessment1).HasColumnName("TypeOfNeedAssessment");
            this.Property(t => t.Remark).HasColumnName("Remark");
        }
    }
}
