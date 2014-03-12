using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TemplateMap : EntityTypeConfiguration<Template>
    {
        public TemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.TemplateId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Template");
            this.Property(t => t.TemplateId).HasColumnName("TemplateId");
            this.Property(t => t.TemplateType).HasColumnName("TemplateType");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Remark).HasColumnName("Remark");

            // Relationships
            this.HasRequired(t => t.TemplateType1)
                .WithMany(t => t.Templates)
                .HasForeignKey(d => d.TemplateType);

        }
    }
}
