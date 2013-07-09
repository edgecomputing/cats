using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.DatabaseMigrations
{
    /// <summary>
    /// Migration for User and Profile tables.
    /// TODO: This schema assumens that you have alread run .NetSqlAzMan database script to create all required
    ///       database scripts for .NetSqlAzMan store
    /// </summary>
    [Migration(201307041045)]
    class UserProfileMigration : Migration
    {
        public override void Up()
        {
            // Create User table
            Create.Table("User").InSchema("dbo")
                .WithColumn("UserId").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("UserName").AsString(200).NotNullable()
                .WithColumn("FullName").AsString(200).NotNullable()
                .WithColumn("Email").AsString(50).Nullable()
                .WithColumn("Password").AsString(-1).NotNullable()
                .WithColumn("Disabled").AsBoolean().NotNullable();

            // Create Profile table
            Create.Table("Profile").InSchema("dbo")
                .WithColumn("UserId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("UILanguage").AsInt32().Nullable()
                .WithColumn("KeyboardLanguage").AsInt32().Nullable()
                .WithColumn("Calendar").AsInt32().Nullable();

            Create.ForeignKey("FK_Profile_User")
                .FromTable("Profile").InSchema("dbo").ForeignColumn("UserId")
                .ToTable("User").InSchema("dbo").PrimaryColumn("UserId");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Profile_User").OnTable("Profile").InSchema("dbo");

            Delete.Table("User").InSchema("dbo");
            Delete.Table("UsersDemo").InSchema("dbo");
        }
    }
}
