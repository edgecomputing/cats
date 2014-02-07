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
           this.Property(t => t.LocalPurchaseDetailID).HasColumnName("LocalPurchaseID");
           this.Property(t => t.LocalPurchseID).HasColumnName("GiftCertificateID");
           this.Property(t => t.HubID).HasColumnName("DateCreated");
           this.Property(t => t.AllocatedAmount).HasColumnName("PurchaseOrder");
           this.Property(t => t.RecievedAmount).HasColumnName("SupplierName");
           // Relationships
           this.HasRequired(t => t.LocalPurchase)
               .WithMany(t => t.LocalPurchaseDetails)
               .HasForeignKey(d => d.LocalPurchseID);

           this.HasRequired(t => t.Hub)
               .WithMany(t => t.LocalPurchaseDetails)
               .HasForeignKey(t => t.HubID);
       }
    }
}
