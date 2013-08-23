using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class RegionalRequestMap : EntityTypeConfiguration<RegionalRequest>
    {
        public RegionalRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.RegionalRequestID);

            // Properties
            this.Property(t => t.RequistionDate)
                .IsRequired();
            this.Property(t => t.Remark)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("EarlyWarning.RegionalRequest");
            this.Property(t => t.RegionalRequestID).HasColumnName("RegionalRequestID");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.RequistionDate).HasColumnName("RequestDate");
            this.Property(t => t.ProgramId).HasColumnName("ProgramID");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.ReferenceNumber).HasColumnName("RequestNumber");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.RationID).HasColumnName("RationID");
           

            // Relationships
            this.HasRequired(t => t.Program)
                .WithMany(t => t.RegionalRequests)
                .HasForeignKey(d => d.ProgramId);

            this.HasRequired(t => t.AdminUnit)
                .WithMany(t => t.RegionalRequests)
                .HasForeignKey(d => d.RegionID);


        }
    }
}
