﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class TransferMap:EntityTypeConfiguration<Transfer>
   {
       public TransferMap()
       {
           // Primary Key
           this.HasKey(t => t.TransferID);

          

           // Table & Column Mappings
           this.ToTable("Transfer");
           this.Property(t => t.TransferID).HasColumnName("TransferID");
           this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
           this.Property(t => t.SourceHubID).HasColumnName("SourceHubID");
           this.Property(t => t.ProgramID).HasColumnName("ProgramID");
           this.Property(t => t.CommoditySourceID).HasColumnName("CommoditySourceID");
           this.Property(t => t.CommodityID).HasColumnName("CommodityID");

           this.Property(t => t.DestinationHubID).HasColumnName("DestinationHubID");
           this.Property(t => t.ProjectCode).HasColumnName("ProjectCode");
           this.Property(t => t.Quantity).HasColumnName("Quantity");

           this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
           this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
           this.Property(t => t.StatusID).HasColumnName("StatusID");

           this.Property(t => t.Remark).HasColumnName("Remark");

           // Relationships
           this.HasRequired(t => t.ShippingInstruction)
               .WithMany(t => t.Transfers)
               .HasForeignKey(d => d.ShippingInstructionID);

           this.HasRequired(t => t.SourceHub)
               .WithMany(t => t.SourceTransfers)
               .HasForeignKey(t => t.SourceHubID);

           this.HasRequired(t => t.Program)
              .WithMany(t => t.Transfers)
              .HasForeignKey(t => t.ProgramID);

           this.HasRequired(t => t.SourceHub)
              .WithMany(t => t.SourceTransfers)
              .HasForeignKey(t => t.SourceHubID);

           this.HasRequired(t => t.CommoditySource)
              .WithMany(t => t.Transfers)
              .HasForeignKey(t => t.CommoditySourceID);

           this.HasRequired(t => t.Commodity)
              .WithMany(t => t.Transfers)
              .HasForeignKey(t => t.CommodityID);

           this.HasRequired(t => t.DestinationHub)
              .WithMany(t => t.DestinationTransfers)
              .HasForeignKey(t => t.DestinationHubID);

       }
    }
}
