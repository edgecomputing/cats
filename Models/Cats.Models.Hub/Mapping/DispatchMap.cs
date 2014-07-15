using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class DispatchMap : EntityTypeConfiguration<Dispatch>
    {
        public DispatchMap()
        {
            // Primary Key
            this.HasKey(t => t.DispatchID);

            // Properties
            this.Property(t => t.GIN)
                .IsRequired()
                .HasMaxLength(7);

            this.Property(t => t.WeighBridgeTicketNumber)
                .HasMaxLength(50);

            this.Property(t => t.RequisitionNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BidNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DriverName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PlateNo_Prime)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PlateNo_Trailer)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(4000);

            this.Property(t => t.DispatchedByStoreMan)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Dispatch");
            this.Property(t => t.DispatchID).HasColumnName("DispatchID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.GIN).HasColumnName("GIN");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.WeighBridgeTicketNumber).HasColumnName("WeighBridgeTicketNumber");
            this.Property(t => t.RequisitionNo).HasColumnName("RequisitionNo");
            this.Property(t => t.BidNumber).HasColumnName("BidNumber");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.DriverName).HasColumnName("DriverName");
            this.Property(t => t.PlateNo_Prime).HasColumnName("PlateNo_Prime");
            this.Property(t => t.PlateNo_Trailer).HasColumnName("PlateNo_Trailer");
            this.Property(t => t.PeriodYear).HasColumnName("PeriodYear");
            this.Property(t => t.PeriodMonth).HasColumnName("PeriodMonth");
            this.Property(t => t.Round).HasColumnName("Round");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.DispatchDate).HasColumnName("DispatchDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.DispatchedByStoreMan).HasColumnName("DispatchedByStoreMan");
            this.Property(t => t.DispatchAllocationID).HasColumnName("DispatchAllocationID");
            this.Property(t => t.OtherDispatchAllocationID).HasColumnName("OtherDispatchAllocationID");

            // Relationships
            this.HasOptional(t => t.DispatchAllocation)
                .WithMany(t => t.Dispatches)
                .HasForeignKey(d => d.DispatchAllocationID);
            this.HasOptional(t => t.FDP)
                .WithMany(t => t.Dispatches)
                .HasForeignKey(d => d.FDPID);
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.Dispatches)
                .HasForeignKey(d => d.HubID);
            this.HasOptional(t => t.OtherDispatchAllocation)
                .WithMany(t => t.Dispatches)
                .HasForeignKey(d => d.OtherDispatchAllocationID);
            this.HasRequired(t => t.Transporter)
                .WithMany(t => t.Dispatches)
                .HasForeignKey(d => d.TransporterID);

        }
    }
}
