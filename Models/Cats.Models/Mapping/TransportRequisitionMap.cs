using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{

    public class TransportRequisitionMap : EntityTypeConfiguration<TransportRequisition>
    {
        public TransportRequisitionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransportRequisitionID);

            // Properties
            this.Property(t => t.TransportRequisitionNo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransportRequisition", "Logistics");
            this.Property(t => t.TransportRequisitionID).HasColumnName("TransportRequisitionID");
            this.Property(t => t.TransportRequisitionNo).HasColumnName("TransportRequisitionNo");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.RequestedBy).HasColumnName("RequestedBy");
            this.Property(t => t.RequestedDate).HasColumnName("RequestedDate");
            this.Property(t => t.CertifiedBy).HasColumnName("CertifiedBy");
            this.Property(t => t.CertifiedDate).HasColumnName("CertifiedDate");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            // Relationships
            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.TransportRequisitions)
                .HasForeignKey(d => d.RegionID);
            this.HasRequired(t => t.Program)
                .WithMany(t => t.TransportRequisitions)
                .HasForeignKey(d => d.ProgramID);

        }
    }
}
