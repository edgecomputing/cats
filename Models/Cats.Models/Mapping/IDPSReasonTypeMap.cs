using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class IDPSReasonTypeMap : EntityTypeConfiguration<IDPSReasonType>
    {
        public IDPSReasonTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.IDPSId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("IDPSReasonType");
            this.Property(t => t.IDPSId).HasColumnName("IDPSId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
