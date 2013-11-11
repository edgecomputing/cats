using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class LetterTemplateMap : EntityTypeConfiguration<LetterTemplate>
    {
        public LetterTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.LetterTemplateID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Template)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("LetterTemplate");
            this.Property(t => t.LetterTemplateID).HasColumnName("LetterTemplateID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Parameters).HasColumnName("Parameters");
            this.Property(t => t.Template).HasColumnName("Template");
        }
    }
}
