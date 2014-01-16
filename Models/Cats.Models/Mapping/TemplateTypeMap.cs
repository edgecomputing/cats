using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TemplateTypeMap : EntityTypeConfiguration<TemplateType>
    {
        public TemplateTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.TemplateTypeId);

            // Properties
            this.Property(t => t.TemplateObject)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TemplateType");
            this.Property(t => t.TemplateTypeId).HasColumnName("TemplateTypeId");
            this.Property(t => t.TemplateObject).HasColumnName("TemplateObject");
            this.Property(t => t.Remark).HasColumnName("Remark");
        }
    }
}
