using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class CurrencyMap:EntityTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            // Primary Key
            this.HasKey(t => t.CurrencyID);

            // Properties
            this.Property(t => t.Code)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Currency", "dbo");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            // Relationships
        }
    }
}
