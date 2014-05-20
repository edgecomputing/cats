using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class LocalPurchaseDetailMap:EntityTypeConfiguration<LocalPurchaseDetail>
   {
       public LocalPurchaseDetailMap()
       {
           // Primary Key
           this.HasKey(t => t.LocalPurchaseDetailID);

           // Properties
           // Table & Column Mappings
           this.ToTable("LocalPurchaseDetail");
           this.Property(t => t.LocalPurchaseDetailID).HasColumnName("LocalPurchaseDetailID");
           this.Property(t => t.LocalPurchaseID).HasColumnName("LocalPurchaseID");
           this.Property(t => t.HubID).HasColumnName("HubID");
           this.Property(t => t.AllocatedAmount).HasColumnName("AllocatedAmount");
           this.Property(t => t.RecievedAmount).HasColumnName("RecievedAmount")
           this.Property(t => t.PartitionId).HasColumnName("PartitionId");
           // Relationships
           this.HasRequired(t => t.LocalPurchase)
               .WithMany(t => t.LocalPurchaseDetails)
               .HasForeignKey(d => d.LocalPurchaseID);

           this.HasRequired(t => t.Hub)
               .WithMany(t => t.LocalPurchaseDetails)
               .HasForeignKey(t => t.HubID);
       }
    }
}
