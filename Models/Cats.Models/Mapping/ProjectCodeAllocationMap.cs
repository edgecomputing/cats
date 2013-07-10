using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ProjectCodeAllocationMap : EntityTypeConfiguration<ProjectCodeAllocation>
    {
        public ProjectCodeAllocationMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectCodeAllocationID);

            // Properties
            this.Property(t => t.ProjectCodeAllocationID);
                //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProjectCodeAllocation", "Logistics");
            this.Property(t => t.ProjectCodeAllocationID).HasColumnName("ProjectCodeAllocationID");
            this.Property(t => t.HubAllocationID).HasColumnName("HubAllocationID");
            this.Property(t => t.ProjectCodeID).HasColumnName("ProjectCodeID");
            this.Property(t => t.SINumberID).HasColumnName("SINumberID");
            this.Property(t => t.AllocatedBy).HasColumnName("AllocatedBy");
            this.Property(t => t.AlloccationDate).HasColumnName("AlloccationDate");
            this.Property(t => t.Amount_Project).HasColumnName("Amount_FromProject");
            this.Property(t => t.Amount_SI).HasColumnName("Amount_FromSI");
            // Relationships
            //this.HasOptional(t => t.ProjectCode)
            //    .WithMany(t => t.ProjectCodeAllocations)
            //    .HasForeignKey(d => d.ProjectCodeID);
            //this.HasOptional(t => t.ShippingInstruction)
            //    .WithMany(t => t.ProjectCodeAllocations)
            //    .HasForeignKey(d => d.SINumberID);
            //this.HasRequired(t => t.UserProfile)
            //    .WithMany(t => t.ProjectCodeAllocations)
            //    .HasForeignKey(d => d.AllocatedBy);
            //this.HasRequired(t => t.HubAllocation)
            //    .WithMany(t => t.ProjectCodeAllocations)
            //    .HasForeignKey(d => d.HubAllocationID);

        }
    }
}
