using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models.Mapping
{
    public class LanguageMap : EntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            // Primary Key
            this.HasKey(t => t.LanguageCode);

            // Properties
            this.Property(t => t.LanguageCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.LanguageName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            //this.ToTable("Languages", "Localization");
            this.ToTable("dbo.Languages");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.LanguageName).HasColumnName("LanguageName");
        }
    }
}