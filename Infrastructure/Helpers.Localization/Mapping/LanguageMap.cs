using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageHelpers.Localization.Models
{
    public class LanguageMap : EntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            // Primary Key
            this.HasKey(t => t.LanguageID);

            // Properties
            this.Property(t => t.LanguageCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);
            this.Property(t => t.LanguageName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Languages");
            this.Property(t => t.LanguageID).HasColumnName("LanguageID");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.LanguageName).HasColumnName("LanguageName");
        }
    }
}