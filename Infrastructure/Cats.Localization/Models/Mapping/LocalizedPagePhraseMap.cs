using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models.Mapping
{
    public class LocalizedPagePhraseMap : EntityTypeConfiguration<LocalizedPagePhrase>
    {
        public LocalizedPagePhraseMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PageId, t.PageKey, t.PhraseId });

            // Properties
            this.Property(t => t.PageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PageKey)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.PhraseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LanguageCode)
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("LocalizedPagePhrases", "Localization");
            this.Property(t => t.PageId).HasColumnName("PageId");
            this.Property(t => t.PageKey).HasColumnName("PageKey");
            this.Property(t => t.PhraseId).HasColumnName("PhraseId");
            this.Property(t => t.PhraseText).HasColumnName("PhraseText");
            this.Property(t => t.TranslatedText).HasColumnName("TranslatedText");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
        }
    }
}
