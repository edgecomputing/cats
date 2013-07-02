using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ReliefRequisitionMap : EntityTypeConfiguration<ReliefRequisition>
    {
        public ReliefRequisitionMap()
        {
            // Primary Key
            this.HasKey(t => t.ReliefRequisitionID);

            //// Properties
            //this.Property(t => t.RequistionDate)
            //    .IsRequired();
            //this.Property(t => t.Remark)
            //    .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("EarlyWarning.ReliefRequisition");
            this.Property(t => t.ReliefRequisitionID).HasColumnName("ReliefRequisitionID");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.RequisitionDate).HasColumnName("RequisitionDate");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.Round).HasColumnName("Round");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.RequestedBy).HasColumnName("RequestedBy");
            this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
            this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            this.Property(t => t.RequisitionDate).HasColumnName("RequisitionDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t=>t.ZoneID).HasColumnName("ZoneID");
            // Relationships
            //this.HasRequired(t => t.AdminUnit)
            //    .WithMany(t => t.ReliefRequisitions)
            //    .HasForeignKey(d => d.RegionID);

            //this.HasRequired(t => t.AdminUnit1)
            //    .WithMany(t => t.ReliefRequisitions)
            //    .HasForeignKey(d => d.ZoneID);
            //this.HasRequired(t => t.Program)
            //   .WithMany(t => t.ReliefRequisitions)
            //   .HasForeignKey(d => d.ProgramID);
            //this.HasRequired(t => t.UserProfile)
            //   .WithMany(t => t.ReliefRequisitions)
            //   .HasForeignKey(d => d.RequestedBy);
            //this.HasRequired(t => t.UserProfile1)
            //   .WithMany(t => t.ReliefRequisitions)
            //   .HasForeignKey(d => d.ApprovedBy);


        }
    }
}
