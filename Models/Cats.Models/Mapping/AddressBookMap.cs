using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class AddressBookMap : EntityTypeConfiguration<AddressBook>
    {

        public AddressBookMap()
        {
            // Primary Key
            this.HasKey(t => t.ContactID);

            // Properties
            this.Property(t => t.ContactName).IsRequired()
                .HasMaxLength(50);
            this.Property(t => t.FDPID).IsRequired();
            this.Property(t => t.phone).IsRequired()
                .HasMaxLength(50);

            //Table & column mapping
            this.ToTable("AddressBook");
            this.Property(t => t.ContactID).HasColumnName("ContactID");
            this.Property(t => t.FDPID).HasColumnName("FDPID");
            this.Property(t => t.phone).HasColumnName("phone");


            //// Relationships
            //this.HasRequired(t => t.Fdp)
            //    .WithMany(t => t.ContactPerson)
            //    .HasForeignKey(d => d.FDPID);



        }

    }


}
