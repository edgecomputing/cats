using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models.Mapping
{
    public class LocalizedTextMap : EntityTypeConfiguration<LocalizedText>
    {
        public LocalizedTextMap()
        {
            // Primary Key
            this.HasKey(t => t.LocalizedTextId);

            // Properties
            this.Property(t => t.LanguageCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.TextKey)
                .HasMaxLength(200);

            this.Property(t => t.TranslatedText)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("dbo.LocalizedTexts");
            this.Property(t => t.LocalizedTextId).HasColumnName("LocalizedTextId");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.PageId).HasColumnName("PageId");
            this.Property(t => t.TextKey).HasColumnName("TextKey");
            this.Property(t => t.TranslatedText).HasColumnName("TranslatedText");

            // Relationships
            this.HasOptional(t => t.Page)
                .WithMany(t => t.LocalizedTexts)
                .HasForeignKey(d => d.PageId);

        }
    }
}
