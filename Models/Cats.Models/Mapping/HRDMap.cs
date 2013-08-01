using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
    public class HRDMap : EntityTypeConfiguration<HRD>
    {
        public HRDMap()
        {
            // Primary Key
            this.HasKey(t => t.HRDID);

            // Properties
            // Table & Column Mappings
            this.ToTable("HRD");
            this.Property(t => t.HRDID).HasColumnName("HRDID");
            this.Property(t => t.Year).HasColumnName("Year");
            //this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            //this.Property(t => t.RevisionNumber).HasColumnName("RevisionNumber");
            this.Property(t => t.CreatedDate).HasColumnName("DateCreated");
            this.Property(t => t.Month).HasColumnName("Month");
            //this.Property(t => t.IsWorkingVersion).HasColumnName("IsWorkingVersion");
            //this.Property(t => t.IsPublished).HasColumnName("IsPublished");
            this.Property(t => t.PublishedDate).HasColumnName("PublishedDate");
           // this.Property(t => t.RationID).HasColumnName("RationID");

            // Relationships
            //this.HasOptional(t => t.Ration)
            //    .WithMany(t => t.HRDs)
            //    .HasForeignKey(d => d.RationID);
            //this.HasOptional(t => t.Season)
            //    .WithMany(t => t.HRDs)
            //    .HasForeignKey(d => d.SeasonID);
            //this.HasOptional(t => t.UserProfile)
            //    .WithMany(t => t.HRDs)
            //    .HasForeignKey(d => d.CreatedBy);

        }
    }
}
