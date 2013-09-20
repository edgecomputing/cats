using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageHelpers.Localization.Models
{
    public class LocalizedTextMap : EntityTypeConfiguration<LocalizedText>
    {
        public LocalizedTextMap()
        {
            this.HasKey(t => t.LocalizedTextId);

            this.Property(t => t.LanguageCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TextKey)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TranslatedText)
                .IsRequired()
                .HasMaxLength(100);

            this.ToTable("LocalizedTexts");
            this.Property(t => t.LocalizedTextId).HasColumnName("LocalizedTextId");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.TextKey).HasColumnName("TextKey");
            this.Property(t => t.TranslatedText).HasColumnName("TranslatedText");
        }
    }
}