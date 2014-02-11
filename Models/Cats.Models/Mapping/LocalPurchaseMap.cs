using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class LocalPurchaseMap:EntityTypeConfiguration<LocalPurchase>
    {
       public LocalPurchaseMap()
       {
           // Primary Key
           this.HasKey(t => t.LocalPurchaseID);

           // Properties
           // Table & Column Mappings
           this.ToTable("LocalPurchase");
           this.Property(t => t.LocalPurchaseID).HasColumnName("LocalPurchaseID");
           this.Property(t => t.GiftCertificateID).HasColumnName("GiftCertificateID");
           this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
           this.Property(t => t.ProjectCode).HasColumnName("ProjectCode");
           this.Property(t => t.CommodityID).HasColumnName("CommodityID");
           this.Property(t => t.DonorID).HasColumnName("DonorID");
           this.Property(t => t.ProgramID).HasColumnName("ProgramID");
           this.Property(t => t.Quantity).HasColumnName("Quantity");
           this.Property(t => t.DateCreated).HasColumnName("DateCreated");
           this.Property(t => t.PurchaseOrder).HasColumnName("PurchaseOrder");
           this.Property(t => t.SupplierName).HasColumnName("SupplierName");
           this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
           this.Property(t => t.StatusID).HasColumnName("StatusID");
           this.Property(t => t.Remark).HasColumnName("Remark");
           // Relationships
           this.HasRequired(t => t.GiftCertificate)
               .WithMany(t => t.LocalPurchases)
               .HasForeignKey(d => d.GiftCertificateID);
           this.HasRequired(t => t.Commodity)
               .WithMany(t => t.LocalPurchases)
               .HasForeignKey(t => t.CommodityID);
           this.HasRequired(t => t.Donor)
             .WithMany(t => t.LocalPurchases)
             .HasForeignKey(t => t.DonorID);
           this.HasRequired(t => t.Program)
             .WithMany(t => t.LocalPurchases)
             .HasForeignKey(t => t.ProgramID);
       }
    }
}
