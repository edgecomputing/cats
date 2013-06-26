using FluentMigrator;

namespace Cats.Migration
{
    [Migration(201306241648)]
    public class AllocationDetailLineMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("AllocationDetailLine").InSchema("EarlyWarning")
                 .WithColumn("AllocationDetailLineID").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("ReliefRequisitionDetailID").AsInt32().NotNullable()
                 .WithColumn("CommodityID").AsInt32().NotNullable()
                 .WithColumn("DonorID").AsInt32().NotNullable()
                 .WithColumn("Amount").AsDecimal().NotNullable();


            Create.ForeignKey("FK_AllocationDetailLine_RequisitionDetailLineID")
                  .FromTable("AllocationDetailLine").InSchema("EarlyWarning").ForeignColumn("ReliefRequisitionDetailID")
                  .ToTable("ReliefRequisitionDetail").InSchema("EarlyWarning").PrimaryColumn("ReliefRequisitionDetailID");
            
        }

        public override void Down()
        {
            
        }
    }
}