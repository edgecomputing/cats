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
            this.ToTable("LoasReason");
            this.Property(t => t.LossReasonId).HasColumnName("FDPID");
            this.Property(t => t.LossReasonEg).HasColumnName("Name");
            this.Property(t => t.LossReasonAm).HasColumnName("NameAM");
            this.Property(t => t.LossReasonCodeEg).HasColumnName("AdminUnitID");
            this.Property(t => t.LossReasonCodeAm).HasColumnName("Latitude");
        

        }
    }
}
