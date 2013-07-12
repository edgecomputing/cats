using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ReliefRequisitionMap : EntityTypeConfiguration<ReliefRequisition>
    {
        public ReliefRequisitionMap()
        {
            // Primary Key
            this.HasKey(t => t.RequisitionID);

            // Properties
            this.Property(t => t.RequisitionNo)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ReliefRequisition", "EarlyWarning");
            this.Property(t => t.RequisitionID).HasColumnName("RequisitionID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.ZoneID).HasColumnName("ZoneID");
            this.Property(t => t.Round).HasColumnName("Round");
            this.Property(t => t.RequisitionNo).HasColumnName("RequisitionNo");
            this.Property(t => t.RequestedBy).HasColumnName("RequestedBy");
            this.Property(t => t.RequestedDate).HasColumnName("RequestedDate");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.RegionalRequestID).HasColumnName("RegionalRequestID");

            // Relationships
            this.HasOptional(t => t.AdminUnit)
                .WithMany(t => t.ReliefRequisitions)
                .HasForeignKey(d => d.RegionID);
            this.HasOptional(t => t.AdminUnit1)
                .WithMany(t => t.ReliefRequisitions1)
                .HasForeignKey(d => d.ZoneID);
            this.HasOptional(t => t.Commodity)
                .WithMany(t => t.ReliefRequisitions)
                .HasForeignKey(d => d.CommodityID);
            this.HasOptional(t => t.Program)
                .WithMany(t => t.ReliefRequisitions)
                .HasForeignKey(d => d.ProgramID);
           
                
            this.HasOptional(t => t.RegionalRequest)
                .WithMany(t => t.ReliefRequisitions)
                .HasForeignKey(d => d.RegionalRequestID);

        }
    }
}
