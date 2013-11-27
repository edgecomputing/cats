using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Hubs.Mapping
{
    public class VWTransferredStockMap : EntityTypeConfiguration<VWTransferredStock>
    {
        public VWTransferredStockMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProgramID, t.DonorAll, t.Received, t.Expected, t.Commited, t.Dispatched });

            // Properties
            this.Property(t => t.ProgramID);

            this.Property(t => t.Program)
                .HasMaxLength(50);

            this.Property(t => t.Hub)
                .HasMaxLength(50);

            this.Property(t => t.Commodity)
                .HasMaxLength(50);

            this.Property(t => t.ShippingInstruction)
                .HasMaxLength(50);

            this.Property(t => t.ProjectCode)
                .HasMaxLength(50);


            this.Property(t => t.DonorAll);

            this.Property(t => t.Received);

            this.Property(t => t.Expected);

            this.Property(t => t.Commited);

            this.Property(t => t.Dispatched);

            // Table & Column Mappings
            this.ToTable("VWTransferredStock");
            this.Property(t => t.ProgramID).HasColumnName("ProgramID");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.ProjectCodeID).HasColumnName("ProjectCodeID");
            this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
            this.Property(t => t.Program).HasColumnName("Program");
            this.Property(t => t.Hub).HasColumnName("Hub");
            this.Property(t => t.Commodity).HasColumnName("Commodity");
            this.Property(t => t.ShippingInstruction).HasColumnName("ShippingInstruction");
            this.Property(t => t.ProjectCode).HasColumnName("ProjectCode");
            this.Property(t => t.DonorAll).HasColumnName("DonorAll");
            this.Property(t => t.Received).HasColumnName("Received");
            this.Property(t => t.Expected).HasColumnName("Expected");
            this.Property(t => t.Commited).HasColumnName("Commited");
            this.Property(t => t.Dispatched).HasColumnName("Dispatched");
        }
    }
}
