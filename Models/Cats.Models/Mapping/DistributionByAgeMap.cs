using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class DistributionByAgeMap : EntityTypeConfiguration<DistributionByAge>
    {
        public DistributionByAgeMap()
        {
            // Primary Key
            this.HasKey(t => t.DistributionByAgeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DistributionByAge");
            this.Property(t => t.DistributionByAgeID).HasColumnName("DistributionByAgeID");
            this.Property(t => t.RegionalRequestID).HasColumnName("RegionalRequestID");
            this.Property(t => t.PlanID).HasColumnName("PlanID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
                
            //Relationships
                 this.HasRequired(t => t.RegionalRequest)
                .WithMany(t => t.DistributionByAges)
                .HasForeignKey(d => d.RegionalRequestID);

                 this.HasRequired(t => t.Program)
                     .WithMany(t => t.DistributionByAges)
                     .HasForeignKey(d => d.ProgramID);

                 this.HasRequired(t => t.Plan)
                         .WithMany(t => t.DistributionByAges)
                         .HasForeignKey(d => d.PlanID);
                 this.HasRequired(t => t.Program)
                         .WithMany(t => t.DistributionByAges)
                         .HasForeignKey(t => t.ProgramID);

        }

    }
}
