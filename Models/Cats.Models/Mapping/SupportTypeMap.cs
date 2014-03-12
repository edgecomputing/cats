using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class SupportTypeMap:EntityTypeConfiguration<SupportType>
   {
       public SupportTypeMap()
       {
           // Primary Key
           this.HasKey(t => t.SupportTypeID);

         

           // Table & Column Mappings
           this.ToTable("SupportType");
           this.Property(t => t.SupportTypeID).HasColumnName("SupportTypeID");
           this.Property(t => t.Description).HasColumnName("Description");
       }
    }
}
