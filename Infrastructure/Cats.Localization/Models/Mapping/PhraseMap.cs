using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models.Mapping
{
    public class PhraseMap : EntityTypeConfiguration<Phrase>
    {
        public PhraseMap()
        {
            // Primary Key
            this.HasKey(t => t.PhraseId);

            // Properties
            this.Property(t => t.PhraseText)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Phrase", "Localization");
            this.Property(t => t.PhraseId).HasColumnName("PhraseId");
            this.Property(t => t.PhraseText).HasColumnName("PhraseText");
        }
    }
}
