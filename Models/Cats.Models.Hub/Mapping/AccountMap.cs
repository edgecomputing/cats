using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountID);

            // Properties
            this.Property(t => t.EntityType)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Account");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.EntityType).HasColumnName("EntityType");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
        }
    }
}
