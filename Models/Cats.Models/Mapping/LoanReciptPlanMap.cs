using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class LoanReciptPlanMap:EntityTypeConfiguration<LoanReciptPlan>
   {
       public LoanReciptPlanMap()
       {
           // Primary Key
           this.HasKey(t => t.LoanReciptPlanID);


           // Table & Column Mappings
           this.ToTable("LoanReciptPlan");
           this.Property(t => t.LoanReciptPlanID).HasColumnName("LoanReciptPlanID");
           this.Property(t => t.ShippingInstructionID).HasColumnName("ShippingInstructionID");
           this.Property(t => t.LoanSource).HasColumnName("LoanSource");
           this.Property(t => t.ProgramID).HasColumnName("ProgramID");
           this.Property(t => t.ProjectCode).HasColumnName("ProjectCode");
           this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
           this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
           this.Property(t => t.CommoditySourceID).HasColumnName("CommoditySourceID");
           this.Property(t => t.CommodityID).HasColumnName("CommodityID");
          // this.Property(t => t.HubID).HasColumnName("HubID");
           this.Property(t => t.Quantity).HasColumnName("Quantity");
           this.Property(t => t.StatusID).HasColumnName("StatusID");

           // Relationships
           this.HasRequired(t => t.ShippingInstruction)
               .WithMany(t => t.LoanReciptPlans)
               .HasForeignKey(d => d.ShippingInstructionID);

           //this.HasRequired(t => t.Hub)
           //.WithMany(t => t.LoanReciptPlans)
           //.HasForeignKey(d => d.SourceHubID);

           this.HasRequired(t => t.Program)
           .WithMany(t => t.LoanReciptPlans)
           .HasForeignKey(d => d.ProgramID);

           this.HasRequired(t => t.CommoditySource)
           .WithMany(t => t.LoanReciptPlans)
           .HasForeignKey(d => d.CommoditySourceID);

           this.HasRequired(t => t.Commodity)
           .WithMany(t => t.LoanReciptPlans)
           .HasForeignKey(d => d.CommodityID);

           //this.HasRequired(t => t.Hub)
           //.WithMany(t => t.LoanReciptPlans)
           //.HasForeignKey(d => d.SourceHubID);
       }

    }
}
