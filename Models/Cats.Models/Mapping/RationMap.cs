using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{

    public class RationMap : EntityTypeConfiguration<Ration>
    {
        public RationMap()
        {
            // Primary Key
            this.HasKey(t => t.RationID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Ration");
            this.Property(t => t.RationID).HasColumnName("RationID");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");

            // Relationships
            //this.HasOptional(t => t.UserProfile)
               // .WithMany(t => t.Rations)
               // .HasForeignKey(d => d.CreatedBy);

        }
    }
}
