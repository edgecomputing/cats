using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class DonorMap:EntityTypeConfiguration<Donor>
    {
       public DonorMap()
       {
           // Primary Key
           this.HasKey(t => t.DonorID);

           // Properties
           this.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(50);

           this.Property(t => t.DonorCode)
               .HasMaxLength(3);

           this.Property(t => t.LongName)
               .HasMaxLength(500);

           // Table & Column Mappings
           this.ToTable("Donor");
           this.Property(t => t.DonorID).HasColumnName("DonorID");
           this.Property(t => t.Name).HasColumnName("Name");
           this.Property(t => t.DonorCode).HasColumnName("DonorCode");
           this.Property(t => t.IsResponsibleDonor).HasColumnName("IsResponsibleDonor");
           this.Property(t => t.IsSourceDonor).HasColumnName("IsSourceDonor");
           this.Property(t => t.LongName).HasColumnName("LongName");
       }
    }
}
