using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
 public class BusinessProcessStateMap : EntityTypeConfiguration <BusinessProcessState>
    {
      public BusinessProcessStateMap()
      { 
        
         //this.ToTable("Cats.BusinessProcessState");
         this.ToTable("BusinessProcessState");
         this.HasKey(t => t.BusinessProcessStateID);
        
         this.Property(t => t.ParentBusinessProcessID).HasColumnName("ParentBusinessProcessID");
         
         this.Property(t => t.StateID).HasColumnName("StateID");  
         this.Property(t => t.PerformedBy).HasColumnName("PerformedBy");  
         this.Property(t => t.DatePerformed).HasColumnName("DatePerformed");  
         this.Property(t => t.Comment).HasColumnName("Comment");

         this.HasRequired(t => t.BaseStateTemplate)
                .WithMany(t => t.DerivedBusinessProcessStates)
                .HasForeignKey(d => d.StateID);
        
         this.HasRequired(t => t.ParentBusinessProcess)
                .WithMany(t => t.BusinessProcessStates)
                .HasForeignKey(d => d.ParentBusinessProcessID);
                         
      }
   }
}