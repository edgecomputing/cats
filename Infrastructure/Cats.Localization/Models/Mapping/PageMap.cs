using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models.Mapping
{
    public class PageMap : EntityTypeConfiguration<Page>
    {
        public PageMap()
        {
            // Primary Key
            this.HasKey(t => t.PageId);

            // Properties
            this.Property(t => t.PageKey)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Page", "Localization");
            this.Property(t => t.PageId).HasColumnName("PageId");
            this.Property(t => t.PageKey).HasColumnName("PageKey");

            // Relationships
            this.HasMany(t => t.Phrases)
                .WithMany(t => t.Pages)
                .Map(m =>
                {
                    m.ToTable("PagePhrase", "Localization");
                    m.MapLeftKey("PageId");
                    m.MapRightKey("PhraseId");
                });


        }
    }
}
