using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    class AllocationDetailLineMap:EntityTypeConfiguration<AllocationDetailLine>
    {
        public  AllocationDetailLineMap()
        {
           // primary Key
            this.HasKey(t => t.AllocationDetailLineID);

            //properties
            

            //Table and Column Mapping
            this.ToTable("EarlyWarning.AllocationDetail");
            this.Property(t => t.AllocationDetailLineID).HasColumnName("AllocationDetailLineID");
            this.Property(t => t.ReliefRequisitionDetailID).HasColumnName("ReliefRequisitionDetailID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Amount).HasColumnName("Amount");

            //Relationships
            this.HasRequired(t => t.ReliefRequisitionDetail)
                .WithMany(t => t.AllocationDetailLines)
                .HasForeignKey(d => d.ReliefRequisitionDetailID);
        }
    }
}
