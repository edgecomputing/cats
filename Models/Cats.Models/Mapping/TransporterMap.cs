using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class TransporterMap : EntityTypeConfiguration<Transporter>
    {
        public TransporterMap()
        {
            // Primary Key
            this.HasKey(t => t.TransporterID);

            // Properties
           /* this.Property(t => t.Region)
                .HasMaxLength(50);

            this.Property(t => t.SubCity)
                .HasMaxLength(50);*/

            // Table & Column Mappings
  
            this.ToTable("Procurement.Transporter");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.SubCity).HasColumnName("SubCity");
            this.Property(t => t.Zone).HasColumnName("Zone");
            this.Property(t => t.Woreda).HasColumnName("Woreda");
            this.Property(t => t.Kebele).HasColumnName("Kebele");
            this.Property(t => t.HouseNo).HasColumnName("HouseNo");
            this.Property(t => t.TelephoneNo).HasColumnName("TelephoneNo");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Ownership).HasColumnName("Ownership");
            this.Property(t => t.VehicleCount).HasColumnName("VehicleCount");
            this.Property(t => t.LiftCapacityFrom).HasColumnName("LiftCapacityFrom");
            this.Property(t => t.LiftCapacityTo).HasColumnName("LiftCapacityTo");
            this.Property(t => t.LiftCapacityTotal).HasColumnName("LiftCapacityTotal");

            this.Property(t => t.EmployeeCountMale).HasColumnName("EmployeeCountMale");
            this.Property(t => t.EmployeeCountFemale).HasColumnName("EmployeeCountFemale");
            this.Property(t => t.OwnerName).HasColumnName("OwnerName");
            this.Property(t => t.OwnerMobile).HasColumnName("OwnerMobile");
            this.Property(t => t.ManagerName).HasColumnName("ManagerName");
            this.Property(t => t.ManagerMobile).HasColumnName("ManagerMobile");
            this.Property(t => t.ExperienceFrom).HasColumnName("ExperienceFrom");
            this.Property(t => t.ExperienceTo).HasColumnName("ExperienceTo");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");

            // Relationships
            /*this.HasOptional(t => t.AdminUnit2)
                .WithMany(t => t.AdminUnit1)
                .HasForeignKey(d => d.ParentID);
            this.HasOptional(t => t.AdminUnitType)
                .WithMany(t => t.AdminUnits)
                .HasForeignKey(d => d.AdminUnitTypeID);
            */
        }
    }
}
