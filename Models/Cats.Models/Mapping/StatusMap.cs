using System.Data.Entity.ModelConfiguration;
namespace Cats.Models.Mapping
{
    public class StatusMap:EntityTypeConfiguration<Status>
    {
        public StatusMap()
        {
            // Primary Key
            this.HasKey(t => t.StatusID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Status","Procurement");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.Name).HasColumnName("Name");

            //RelationShips
            
        }
    }
}
