using FluentMigrator;

namespace Cats.Migration
{
    [Migration(201306211026)]
    public class ReliefRequistionMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("ReliefRequistion").InSchema("EarlyWarning")
                  .WithColumn("ReliefRequistionID").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("RequisitionDate").AsDateTime().NotNullable()
                  .WithColumn("ProgramID").AsInt32().NotNullable()
                  .WithColumn("RoundID").AsInt32().NotNullable()
                  .WithColumn("RegionID").AsInt32().NotNullable()
                  .WithColumn("ReferenceNumber").AsString().NotNullable()
                  .WithColumn("Year").AsInt32().NotNullable()
                  .WithColumn("Remark").AsString().Nullable();
            Create.Table("ReliefRequisitionDetail").InSchema("EarlyWarning")
               .WithColumn("ReliefRequisitionDetailID").AsInt32().PrimaryKey().Identity()
               .WithColumn("ReliefRequistionID").AsInt32().NotNullable()
               .WithColumn("FDPID").AsInt32().NotNullable()
               .WithColumn("Beneficiaries").AsString().NotNullable();

            Create.Table("RequsitionDetailLine").InSchema("EarlyWarning")
                .WithColumn("RequisitionDetailLineID").AsInt32().PrimaryKey().Identity()
                .WithColumn("ReliefRequisitionDetailID").AsInt32().NotNullable()
                .WithColumn("CommodityID").AsInt32().NotNullable()
                .WithColumn("Amount").AsDecimal().NotNullable();

            Create.Table("Round").InSchema("EarlyWarning")
      .WithColumn("RoundId").AsInt32().PrimaryKey().Identity()
      .WithColumn("RoundNumber").AsInt32().NotNullable()
      .WithColumn("StartDate").AsDateTime().NotNullable()
      .WithColumn("EndDate").AsDateTime().NotNullable();

            Create.ForeignKey("FK_RequisitionDetailLine_ReliefRequisitionDetail")
               .FromTable("RequsitionDetailLine").InSchema("EarlyWarning").ForeignColumn("RequisitionDetailLineID")
               .ToTable("ReliefRequisitionDetail").InSchema("EarlyWarning").PrimaryColumn("ReliefRequisitionDetailID");

            Create.ForeignKey("FK_RequisitionDetailLine_Commodity")
             .FromTable("RequsitionDetailLine").InSchema("EarlyWarning").ForeignColumn("RequisitionDetailLineID")
             .ToTable("Commodity").PrimaryColumn("CommodityID");

            Create.ForeignKey("FK_ReliefRequistion_Round")
                  .FromTable("ReliefRequistion").InSchema("EarlyWarning").ForeignColumn("RoundID")
                  .ToTable("Round").InSchema("EarlyWarning").PrimaryColumn("RoundID");

            Create.ForeignKey("Fk_ReliefRequisition_Program")
                  .FromTable("ReliefRequistion").InSchema("EarlyWarning").ForeignColumn("ProgramID")
                  .ToTable("Program").PrimaryColumn("ProgramId");

            Create.ForeignKey("FK_ReliefRequistion.AdminUnit")
                  .FromTable("ReliefRequistion").InSchema("EarlyWarning").ForeignColumn("RegionID")
                  .ToTable("AdminUnit").PrimaryColumn("AdminUnitID");

            Create.ForeignKey("FK_ReliefRequisitionDetail_ReliefRequistion")
                  .FromTable("ReliefRequisitionDetail").InSchema("EarlyWarning").ForeignColumn("ReliefRequistionID")
                  .ToTable("ReliefRequistion").InSchema("EarlyWarning").PrimaryColumn("ReliefRequistionID");
            Create.ForeignKey("FK_ReliefRequisitionDetail_FDP")
                  .FromTable("ReliefRequisitionDetail").InSchema("EarlyWarning").ForeignColumn("ReliefRequistionID")
                  .ToTable("FDP").PrimaryColumn("FDPID");


        }

        public override void Down()
        {
            Delete.Table("RequsitionDetailLine").InSchema("EarlyWarning");
            Delete.Table("ReliefRequisitionDetail").InSchema("EarlyWarning");
            Delete.Table("Round").InSchema("EarlyWarning");
            Delete.Table("ReliefRequistion").InSchema("EarlyWarning");
        }
    }
}