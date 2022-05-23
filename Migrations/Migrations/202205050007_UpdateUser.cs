using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.Migrations
{
    [Migration(202205050007)]
    public class _202205050007_UpdateUser : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Alter.Column("Phone").OnTable("User").AsString().NotNullable().Unique();

        }
    }
}
