using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202206240011)]

    public class _202206240011 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("Menu")
                .Row(new { Id = Guid.NewGuid(), Product = "Кофе Капучино", Size = 0.4, Price = 150 });
        }
    }
}
