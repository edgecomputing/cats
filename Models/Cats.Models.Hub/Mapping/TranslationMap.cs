using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class TranslationMap : EntityTypeConfiguration<Translation>
    {
        public TranslationMap()
        {
            // Primary Key
            this.HasKey(t => t.TranslationID);

            // Properties
            this.Property(t => t.LanguageCode)
                .IsRequired()
                .HasMaxLength(4);

            this.Property(t => t.Phrase)
                .IsRequired()
                .HasMaxLength(4000);

            this.Property(t => t.TranslatedText)
                .IsRequired()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("Translation");
            this.Property(t => t.TranslationID).HasColumnName("TranslationID");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.Phrase).HasColumnName("Phrase");
            this.Property(t => t.TranslatedText).HasColumnName("TranslatedText");
        }
    }
}
