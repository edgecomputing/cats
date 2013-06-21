using FluentMigrator;


namespace Cats.Migration
{
    [Migration(201306211220)]
    public class Round : MigrationBase
    {
        public override void Up()
        {
            Create.Table("EarlyWarning.Round")
                  .WithColumn("RoundId").AsInt32().PrimaryKey().Identity()
                  .WithColumn("RoundNumber").AsInt32().NotNullable()
                  .WithColumn("StartDate").AsDateTime().NotNullable()
                  .WithColumn("EndDate").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("EarlyWarning.Round");
        }
    }
}