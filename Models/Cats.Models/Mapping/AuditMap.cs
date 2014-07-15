using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
namespace Cats.Models.Mapping
{
    public class AuditMap : EntityTypeConfiguration<Audit>
    {
        public AuditMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditID);

            // Properties
            this.Property(t => t.Action)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TableName)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.PrimaryKey)
                .HasMaxLength(50);

            this.Property(t => t.ColumnName)
                .HasMaxLength(3000);

            // Table & Column Mappings
            this.ToTable("Audit");
            this.Property(t => t.AuditID).HasColumnName("AuditID");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.DateTime).HasColumnName("DateTime");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.PrimaryKey).HasColumnName("PrimaryKey");
            this.Property(t => t.ColumnName).HasColumnName("ColumnName");
            this.Property(t => t.NewValue).HasColumnName("NewValue");
            this.Property(t => t.OldValue).HasColumnName("OldValue");
        }
    }
}
