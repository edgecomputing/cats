using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            // Primary Key
            this.HasKey(t => t.ContactID);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(350);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(350);

            this.Property(t => t.PhoneNo)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Contact");
            this.Property(t => t.ContactID).HasColumnName("ContactID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            this.Property(t => t.FDPID).HasColumnName("FDPID");

            // Relationships
            this.HasRequired(t => t.FDP)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.FDPID);
        }
    }
}
