using FluentMigrator;

namespace Cats.DatabaseMigrations
{
    [Migration(201306201648)]
    public class EarlyWarningSchemaMigration : Migration
    {
        public override void Up()
        {
            Create.Schema("EarlyWarning");           
            Create.Schema("Logistics");
            Create.Schema("Procurement");
           
        }

        public override void Down()
        {
            Delete.Schema("EarlyWarning");
            Delete.Schema("Logistics");
            Delete.Schema("Procurement");
           
        }
    }
}
