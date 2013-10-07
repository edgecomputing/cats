using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class ErrorLogMap : EntityTypeConfiguration<ErrorLog>
    {
        public ErrorLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ErrorLogID);

            // Properties
            this.Property(t => t.Application)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Host)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Source)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Message)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.User)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Sequence);
                //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.AllXml)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ErrorLog");
            this.Property(t => t.ErrorLogID).HasColumnName("ErrorLogID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.Application).HasColumnName("Application");
            this.Property(t => t.Host).HasColumnName("Host");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.User).HasColumnName("User");
            this.Property(t => t.StatusCode).HasColumnName("StatusCode");
            this.Property(t => t.TimeUtc).HasColumnName("TimeUtc");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
            this.Property(t => t.AllXml).HasColumnName("AllXml");
        }
    }
}
