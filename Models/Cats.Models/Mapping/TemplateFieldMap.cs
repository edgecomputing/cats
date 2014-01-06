using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TemplateFieldMap : EntityTypeConfiguration<TemplateField>
    {
        public TemplateFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.TemplateFieldId);

            // Properties
            this.Property(t => t.Field)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TemplateFields");
            this.Property(t => t.TemplateFieldId).HasColumnName("TemplateFieldId");
            this.Property(t => t.TemplateId).HasColumnName("TemplateId");
            this.Property(t => t.Field).HasColumnName("Field");
        }
    }
}
