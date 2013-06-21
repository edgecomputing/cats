using FluentMigrator;

namespace Cats.Migration
{
    [Migration(201306211026)]
    public class ReliefRequistion :MigrationBase
    {
        public override void Up()
        {
            Create.Table("EarlyWarning.ReliefRequistion")
                  .WithColumn("ReliefRequistionID").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("RequisitionDate").AsDateTime().NotNullable()
                  .WithColumn("ProgramID").AsInt32().NotNullable()
                  .WithColumn("RoundID").AsInt32().NotNullable()
                  .WithColumn("RequestedByUserID").AsInt32().NotNullable()
                  .WithColumn("CertifiedByUserID").AsInt32().NotNullable()
                  .WithColumn("AuthorisedByUserID").AsInt32().NotNullable();
            Create.ForeignKey("FK_ReliefRequistion_Round")
                  .FromTable("ReliefRequistion").ForeignColumn("RoundID")
                  .ToTable("Round").PrimaryColumn("RoundID");

            Create.Table("EarlyWarning.ReliefRequisitionDetail")
                  .WithColumn("ReliefRequisitionDetailID").AsInt32().PrimaryKey().Identity()
                  .WithColumn("ReliefRequistionID").AsInt32().NotNullable()
                  .WithColumn("CommodityID").AsInt32().NotNullable()
                  .WithColumn("DonorID").AsInt32().NotNullable()
                  .WithColumn("NoOfBeneficiaries").AsInt32().NotNullable()
                  .WithColumn("Amount").AsDecimal().NotNullable()
                  .WithColumn("FDPID").AsInt32().NotNullable();
            Create.ForeignKey("FK_ReliefRequisitionDetail_ReliefRequistion")
                  .FromTable("ReliefRequisitionDetail").ForeignColumn("ReliefRequistionID")
                  .ToTable("ReliefRequistion").PrimaryColumn("ReliefRequistionID");

        }

        public override void Down()
        {
            Delete.Table("ReliefRequistion");
            Delete.Table("ReliefRequisitionDetail");
        }
    }
}