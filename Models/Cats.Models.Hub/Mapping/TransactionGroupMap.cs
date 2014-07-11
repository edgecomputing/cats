using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class TransactionGroupMap : EntityTypeConfiguration<TransactionGroup>
    {
        public TransactionGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionGroupID);

            // Properties
            // Table & Column Mappings
            this.ToTable("TransactionGroup");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
        }
    }
}
