using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class PeriodMap : EntityTypeConfiguration<Period>
    {
        public PeriodMap()
        {
            // Primary Key
            this.HasKey(t => t.PeriodID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Period");
            this.Property(t => t.PeriodID).HasColumnName("PeriodID");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");
        }
    }
}
