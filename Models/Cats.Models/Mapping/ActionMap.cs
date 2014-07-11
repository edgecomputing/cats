using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ActionTypesMap : EntityTypeConfiguration<ActionTypes>
    {
        public ActionTypesMap()
        {
            // Primary Key
            this.HasKey(t => t.ActionId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Actions");
            this.Property(t => t.ActionId).HasColumnName("ActionId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
