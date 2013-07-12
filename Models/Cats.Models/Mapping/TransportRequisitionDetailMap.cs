using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class TransportRequisitionDetailMap : EntityTypeConfiguration<TransportRequisitionDetail>
    {
        public TransportRequisitionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.TransportRequisitionDetailID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransportRequisitionDetail", "Logistics");
            this.Property(t => t.TransportRequisitionDetailID).HasColumnName("TransportRequisitionDetailID");
            this.Property(t => t.TransportRequisitionID).HasColumnName("TransportRequisitionID");
            this.Property(t => t.RequisitionID).HasColumnName("RequisitionID");

            // Relationships
            this.HasRequired(t => t.ReliefRequisition)
                .WithMany(t => t.TransportRequisitionDetails)
                .HasForeignKey(d => d.RequisitionID);
            this.HasRequired(t => t.TransportRequisition)
                .WithMany(t => t.TransportRequisitionDetails)
                .HasForeignKey(d => d.TransportRequisitionID);

        }
    }
}
