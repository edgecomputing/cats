
using FluentMigrator;

namespace Cats.DatabaseMigrations
{
    [Migration(201307051652)]
    public class BidPlanMigration_201307051652 : Migration
    {
        public override void Up()
        {
            //Create.Schema("Procurement");


            Create.Table("TransportBidPlanDetail").InSchema("Procurement")
                .WithColumn("TransportBidPlanDetailID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ProgramID").AsInt32().NotNullable()
                .WithColumn("DestinationID").AsInt32().NotNullable()
                .WithColumn("SourceID").AsInt32().NotNullable()
                .WithColumn("Quantity").AsDecimal().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("TransportBidPlanDetail").InSchema("Procurement");
        }
    }
}


