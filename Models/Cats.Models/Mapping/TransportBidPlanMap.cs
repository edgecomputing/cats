using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransportBidPlanMap : EntityTypeConfiguration<TransportBidPlan>
    {
        public TransportBidPlanMap()
        {
            this.ToTable("Procurement.TransportBidPlan");
            this.Property(t => t.TransportBidPlanID).HasColumnName("TransportBidPlanID");

            this.Property(t => t.Year).HasColumnName("Year");

            this.Property(t => t.YearHalf).HasColumnName("YearHalf");

            //this.Property(t => t.RegionID).HasColumnName("RegionID");

            this.Property(t => t.ProgramID).HasColumnName("ProgramID");

            // Relationships
            this.HasRequired(t => t.Program)
                .WithMany(t => t.TransportBidPlans)
                .HasForeignKey(d => d.ProgramID);

          /*  this.HasRequired(t => t.Region)
                .WithMany(t => t.TransportBidPlans)
                .HasForeignKey(d => d.RegionID);*/
           // this.Property(t => t.Region).HasColumnName("Region");

          //  this.Property(t => t.Program).HasColumnName("Program");

        }
    }
}
