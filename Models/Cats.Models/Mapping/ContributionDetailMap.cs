using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Mapping
{
   public class ContributionDetailMap:EntityTypeConfiguration<ContributionDetail>
    {
       public ContributionDetailMap()
       {
           // Primary Key
           this.HasKey(t => t.ContributionDetailID);

           // Properties
           this.Property(t => t.PledgeReferenceNo)
               .HasMaxLength(50);

           // Table & Column Mappings
           this.ToTable("ContributionDetail");
           this.Property(t => t.ContributionDetailID).HasColumnName("ContributionDetailID");
           this.Property(t => t.ContributionID).HasColumnName("ContributionID");
           this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
           this.Property(t => t.PledgeReferenceNo).HasColumnName("PledgeReferenceNo");
           this.Property(t => t.PledgeDate).HasColumnName("PledgeDate");
           this.Property(t => t.Amount).HasColumnName("Amount");

           // Relationships
           this.HasRequired(t => t.Currency)
               .WithMany(t => t.ContributionDetails)
               .HasForeignKey(d => d.CurrencyID);
           this.HasRequired(t => t.Contribution)
               .WithMany(t => t.ContributionDetails)
               .HasForeignKey(d => d.ContributionID);

       }
    }
}
