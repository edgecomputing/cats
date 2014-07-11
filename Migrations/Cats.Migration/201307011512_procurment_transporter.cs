using FluentMigrator;

namespace Cats.DatabaseMigrations
{
    [Migration(201307011512)]
    public class ProcurementSchemaMigration : Migration
    {
        public override void Up()
        {
            Create.Schema("Procurement");
            Create.Table("Transporter").InSchema("Procurement")
                  .WithColumn("TransporterID").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Region").AsString().Nullable()
                  .WithColumn("SubCity").AsString().Nullable()
                  .WithColumn("Zone").AsString().Nullable()
                  .WithColumn("Woreda").AsString().Nullable()
                  .WithColumn("Kebele").AsString().Nullable()
                  .WithColumn("HouseNo").AsString().Nullable()
                  .WithColumn("TelephoneNo").AsString().Nullable()
                  .WithColumn("MobileNo").AsString().Nullable()
                  .WithColumn("Email").AsString().Nullable()

                  .WithColumn("Ownership").AsString().Nullable()
                  .WithColumn("VehicleCount").AsInt32().Nullable()
                  .WithColumn("LiftCapacityFrom").AsDecimal().Nullable()
                  .WithColumn("LiftCapacityTo").AsDecimal().Nullable()
                  .WithColumn("LiftCapacityTotal").AsDecimal().Nullable()
                  .WithColumn("Capital").AsDecimal().Nullable()
                  .WithColumn("EmployeeCountMale").AsInt32().Nullable()
                  .WithColumn("EmployeeCountFemale").AsInt32().Nullable()

                .WithColumn("OwnerName").AsString().Nullable()
                .WithColumn("OwnerMobile").AsString().Nullable()
                .WithColumn("ManagerName").AsString().Nullable()
                .WithColumn("ManagerMobile").AsString().Nullable()

                .WithColumn("ExperienceFrom").AsDateTime().Nullable()
                .WithColumn("ExperienceTo").AsDateTime().Nullable();
                 
        }

        public override void Down()
        {
            Delete.Table("Transporter").InSchema("Procurement");
            Delete.Schema("Procurement");
        }
    }
}

