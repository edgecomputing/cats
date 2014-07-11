using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class DashboardWidgetMap : EntityTypeConfiguration<DashboardWidget>
    {
        public DashboardWidgetMap()
        {
            // Primary Key
            this.HasKey(t => t.DashboardWidgetID);

            // Propertie
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Icon)
                .HasMaxLength(255);

            this.Property(t => t.Source)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("DashboardWidget");
            this.Property(t => t.DashboardWidgetID).HasColumnName("DashboardWidgetID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Icon).HasColumnName("Icon");
            this.Property(t => t.Source).HasColumnName("Source");
        }
    }
}
