using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class RoundMap:EntityTypeConfiguration<Round>
    {
        public RoundMap()
        {
            // primary Key
            this.HasKey(t => t.RoundID);

            //properties
            this.Property(t => t.RoundNumber)
                .IsRequired();
            this.Property(t => t.StartDate)
                .IsRequired();
            this.Property(t => t.RoundNumber)
                .IsRequired();

            //Table and Column Mapping
            this.ToTable("EarlyWarning.Round");
            this.Property(t => t.RoundID).HasColumnName("RoundID");
            this.Property(t => t.RoundNumber).HasColumnName("RoundNumber");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");

            //Relationships


        }
    }
}
