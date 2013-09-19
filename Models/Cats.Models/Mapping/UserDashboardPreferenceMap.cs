using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class UserDashboardPreferenceMap : EntityTypeConfiguration<UserDashboardPreference>
    {
        public UserDashboardPreferenceMap()
        {
            // Primary Key
            this.HasKey(t => t.UserDashboardPreferenceID);

            // Properties
            this.Property(t => t.OrderNo);

            // Table & Column Mappings
            this.ToTable("UserDashboardPreference");
            this.Property(t => t.UserDashboardPreferenceID).HasColumnName("UserDashboardPreferenceID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.DashboardWidgetID).HasColumnName("DashboardWidgetID");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");

            // Relationships
            this.HasRequired(t => t.DashboardWidget)
                .WithMany(t => t.UserDashboardPreferences)
                .HasForeignKey(d => d.DashboardWidgetID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserDashboardPreferences)
                .HasForeignKey(d => d.UserID);

        }
    }
}
