
using FluentMigrator;

namespace Cats.DatabaseMigrations
{
    [Migration(201307051652)]
    public class BidPlanMigration : Migration
    {
        public override void Up()
        {
            //Create.Schema("Procurement");


            Create.Table("TransportBidPlan").InSchema("Procurement")
                .WithColumn("TransportBidPlanID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Year").AsInt32().NotNullable()
                .WithColumn("YearHalf").AsInt32().NotNullable()
                .WithColumn("ProgramID").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("TransportBidPlan").InSchema("Procurement");
        }
    }
}


