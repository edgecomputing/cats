using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
                .HasMaxLength(100);

            this.Property(t => t.PageDescription)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("dbo.Page");
            this.Property(t => t.PageId).HasColumnName("PageId");
            this.Property(t => t.PageKey).HasColumnName("PageKey");
            this.Property(t => t.PageDescription).HasColumnName("PageDescription");
        }
    }
}
