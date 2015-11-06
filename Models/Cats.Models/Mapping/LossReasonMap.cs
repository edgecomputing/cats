using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class LossReasonMap:EntityTypeConfiguration<LossReason>
    {
       public LossReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.LossReasonId);

           
            // Table & Column Mappings
            this.ToTable("LossReason");
            this.Property(t => t.LossReasonId).HasColumnName("LossReasonId");
            this.Property(t => t.LossReasonEg).HasColumnName("LossReasonEg");
            this.Property(t => t.LossReasonAm).HasColumnName("LossReasonAm");
            this.Property(t => t.LossReasonCodeEg).HasColumnName("LossReasonCodeEg");
            this.Property(t => t.LossReasonCodeAm).HasColumnName("LossReasonCodeAm");
            this.Property(t => t.Description).HasColumnName("Description");
        

        }
    }
}
