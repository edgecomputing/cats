using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models.Mapping
{
    public class LocalizedPhraseMap : EntityTypeConfiguration<LocalizedPhrase>
    {
        public LocalizedPhraseMap()
        {
            // Primary Key
            this.HasKey(t => t.LocalizationId);

            // Properties
            this.Property(t => t.LanguageCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TranslatedText)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("LocalizedPhrase", "Localization");
            this.Property(t => t.LocalizationId).HasColumnName("LocalizationId");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.PhraseId).HasColumnName("PhraseId");
            this.Property(t => t.TranslatedText).HasColumnName("TranslatedText");

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.LocalizedPhrases)
                .HasForeignKey(d => d.LanguageCode);
            this.HasRequired(t => t.Phrase)
                .WithMany(t => t.LocalizedPhrases)
                .HasForeignKey(d => d.PhraseId);

        }
    }
}
