using FluentMigrator;

namespace Cats.DatabaseMigrations
{
    [Migration(201307021436)]
    public class BidMigration : Migration
    {
        public override void Up()
        {
            //Migration to create Table Bid
            Create.Table("Bid").InSchema("Procurement")
                  .WithColumn("BidID").AsInt32().PrimaryKey().Identity()
                  .WithColumn("StartDate").AsDateTime().NotNullable()
                  .WithColumn("EndDate").AsDateTime().NotNullable()
                  .WithColumn("BidNumber").AsString().NotNullable();

            //Migration to create Table BidDetail
            Create.Table("BidDetail").InSchema("Procurement")
                  .WithColumn("BidDetailID").AsInt32().PrimaryKey().Identity()
                  .WithColumn("BidID").AsInt32().NotNullable()
                  .WithColumn("RegionID").AsInt32().NotNullable()
                  .WithColumn("AmountForReliefProgram").AsDecimal().Nullable()
                  .WithColumn("AmountForPSNPProgram").AsDecimal().Nullable()
                  .WithColumn("BidDocumentPrice").AsFloat().NotNullable()
                  .WithColumn("CBO").AsFloat().NotNullable();

            //Relationship
            Create.ForeignKey("FK_BidDetail_Bid")
              .FromTable("BidDetail").InSchema("Procurement").ForeignColumn("BidID")
              .ToTable("Bid").InSchema("dbo").PrimaryColumn("BidID");

            Create.ForeignKey("FK_BidDetail_AdminUnit")
             .FromTable("BidDetail").InSchema("Procurement").ForeignColumn("RegionID")
             .ToTable("AdminUnit").InSchema("dbo").PrimaryColumn("AdminUnitID");


        }

        public override void Down()
        {

            Delete.Table("Bid").InSchema("dbo");
            Delete.Table("BidDetail").InSchema("Procurement");
        }
    }
}