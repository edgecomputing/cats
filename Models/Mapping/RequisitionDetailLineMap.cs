using System.Data.Entity.ModelConfiguration;


namespace Cats.Models.Mapping
{
    class RequisitionDetailLineMap:EntityTypeConfiguration<RequisitionDetailLine>
    {
        public RequisitionDetailLineMap()
        {
            // primary Key
            this.HasKey(t => t.RequisitionDetailLineID);

            //properties


            //Table and Column Mapping
            this.ToTable("EarlyWarning.AllocationDetail");
            this.Property(t => t.RequisitionDetailLineID).HasColumnName("RequisitionDetailLineID");
            this.Property(t => t.ReliefRequisitionDetailID).HasColumnName("ReliefRequisitionDetailID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.Amount).HasColumnName("Amount");

            //Relationships
            this.HasRequired(t => t.ReliefRequisitionDetail)
                .WithMany(t => t.RequisitionDetailLines)
                .HasForeignKey(d => d.ReliefRequisitionDetailID);
        }
    }
}
