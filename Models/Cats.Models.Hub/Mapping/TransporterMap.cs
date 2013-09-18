using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class TransporterMap : EntityTypeConfiguration<Transporter>
    {
        public TransporterMap()
        {
            // Primary Key
            this.HasKey(t => t.TransporterID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NameAM)
                .HasMaxLength(50);

            this.Property(t => t.LongName)
                .HasMaxLength(50);

            this.Property(t => t.BiddingSystemID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Transporter");
            this.Property(t => t.TransporterID).HasColumnName("TransporterID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAM).HasColumnName("NameAM");
            this.Property(t => t.LongName).HasColumnName("LongName");
            this.Property(t => t.BiddingSystemID).HasColumnName("BiddingSystemID");
        }
    }
}
