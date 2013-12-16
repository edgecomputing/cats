using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class WoredaHubMap : EntityTypeConfiguration<WoredaHub>
    {
        public WoredaHubMap()
        {
            // Primary Key
            this.HasKey(t => t.WoredaHubID);

            // Properties
            // Table & Column Mappings
            this.ToTable("WoredaHub");
            this.Property(t => t.WoredaHubID).HasColumnName("WoredaHubID");
            this.Property(t => t.HRDID).HasColumnName("HRDID");
            this.Property(t => t.PlanID).HasColumnName("PlanID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
