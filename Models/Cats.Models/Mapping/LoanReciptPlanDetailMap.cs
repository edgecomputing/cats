using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class LoanReciptPlanDetailMap:EntityTypeConfiguration<LoanReciptPlanDetail>
   {
       public LoanReciptPlanDetailMap()
       {
           // Primary Key
           this.HasKey(t => t.LoanReciptPlanDetailID);


           // Table & Column Mappings
           this.ToTable("LoanReciptPlanDetail");
           this.Property(t => t.LoanReciptPlanDetailID).HasColumnName("LoanReciptPlanDetailID");
           this.Property(t => t.LoanReciptPlanID).HasColumnName("LoanReciptPlanID");
           this.Property(t => t.HubID).HasColumnName("HubID");
           this.Property(t => t.MemoReferenceNumber).HasColumnName("MemoReferenceNumber");
           this.Property(t => t.RecievedQuantity).HasColumnName("RecievedQuantity");
           this.Property(t => t.ApprovedBy).HasColumnName("ApprovedBy");
           this.Property(t => t.RecievedDate).HasColumnName("RecievedDate");
           
           // Relationships
           this.HasRequired(t => t.LoanReciptPlan)
               .WithMany(t => t.LoanReciptPlanDetails)
               .HasForeignKey(d => d.LoanReciptPlanID);

           this.HasRequired(t => t.Hub)
           .WithMany(t => t.LoanReciptPlanDetails)
           .HasForeignKey(d => d.HubID);

           this.HasRequired(t => t.UserProfile)
          .WithMany(t => t.LoanReciptPlanDetails)
          .HasForeignKey(d => d.ApprovedBy);
       }
    }
}
