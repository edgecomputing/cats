using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransportBidPlanDetailMap : EntityTypeConfiguration<TransportBidPlanDetail>
    {
        public TransportBidPlanDetailMap()
        {
            this.ToTable("Procurement.TransportBidPlanDetail");
            this.Property(t => t.TransportBidPlanDetailID).HasColumnName("TransportBidPlanDetailID");

            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.PartitionID).HasColumnName("PartitionId");

            // Relationships
            this.HasRequired(t => t.BidPlan)
                .WithMany(t => t.TransportBidPlanDetails)
                .HasForeignKey(d => d.BidPlanID);

            this.HasRequired(t => t.Source)
                .WithMany(t => t.TransportBidPlanSources)
                .HasForeignKey(d => d.SourceID);

            this.HasRequired(t => t.Destination)
                .WithMany(t => t.TransportBidPlanDestinations)
                .HasForeignKey(d => d.DestinationID);

            
        }
    }
}