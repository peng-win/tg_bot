using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202206240012)]
    public class _202206240012 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("Cart")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("UserName").AsString(30).NotNullable()
                .WithColumn("ProductIds").AsString().NotNullable();
        }
    }
}
