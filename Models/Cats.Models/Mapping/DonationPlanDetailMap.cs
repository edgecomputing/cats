using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class DonationPlanDetailMap : EntityTypeConfiguration<DonationPlanDetail>
    {
        public DonationPlanDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DonationDetailPlanID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DonationPlanDetail");
            this.Property(t => t.DonationDetailPlanID).HasColumnName("DonationDetailPlanID");
            this.Property(t => t.DonationHeaderPlanID).HasColumnName("DonationHeaderPlanID");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.AllocatedAmount).HasColumnName("AllocatedAmount");
            this.Property(t => t.ReceivedAmount).HasColumnName("ReceivedAmount");
            this.Property(t => t.Balance).HasColumnName("Balance");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            // Relationships
            this.HasRequired(t => t.DonationPlanHeader)
                .WithMany(t => t.DonationPlanDetails)
                .HasForeignKey(d => d.DonationHeaderPlanID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.DonationPlanDetails)
                .HasForeignKey(d => d.HubID);

        }
    }
}
