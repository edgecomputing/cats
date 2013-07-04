using FluentMigrator;

namespace Cats.DatabaseMigrations
{
    [Migration(201307031756)]
    public class AddStatusToRegionalRequest : Migration
    {
        public override void Up()
        {
            Alter.Table("RegionalRequest").InSchema("EarlyWarning")
               .AddColumn("Status").AsInt32().Nullable();
        }

        public override void Down()
        {
            Delete.Column("Status").FromTable("RegionalRequest").InSchema("EarlyWarning");
        }
    }
}