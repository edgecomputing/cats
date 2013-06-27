using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Cats.Migration
{
    [Migration(201306201648)]
    public class EarlyWarningSchemaMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Schema("EarlyWarning");           
            Create.Schema("Logistics");
        }

        public override void Down()
        {
            Delete.Schema("EarlyWarning");
            Delete.Schema("Logistics");
        }
    }
}
