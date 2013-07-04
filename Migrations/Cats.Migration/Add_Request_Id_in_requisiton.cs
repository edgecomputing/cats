using FluentMigrator;

namespace Cats.DatabaseMigrations
{
    [Migration(201307030509)]
    public class AddRequest_Id_In_Requisiton : Migration
    {
        public override void Up()
        {
            Alter.Table("ReliefRequisition").InSchema("EarlyWarning")
                .AddColumn("RegionalRequestID").AsInt32().Nullable();
            Create.ForeignKey("FK_ReliefRequisition_RegionalRequest").FromTable("ReliefRequisition").InSchema(
                "EarlyWarning")
                .ForeignColumn("RegionalRequestID").ToTable("RegionalRequest").InSchema("EarlyWarning").PrimaryColumn(
                    "RegionalRequestID");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_ReliefRequisition_RegionalRequest");
            Delete.Column("RegionalRequestID").FromTable("ReliefRequisition").InSchema("EarlyWarning");
        }
    }
}