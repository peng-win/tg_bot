using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.Migrations
{
    [Migration(202204010004)]
    public class FillingTables_202204010004 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Column("Description").OnTable("Menu").AsString(1000).Nullable();
            Insert.IntoTable("Ingridients")
                .Row(new {Id = Guid.NewGuid(), Name = "Перец халапеньо", WeightInGrams = 20, Price = 50})
                .Row(new {Id = Guid.NewGuid(), Name = "Маслины", WeightInGrams = 20, Price = 30})
                .Row(new {Id = Guid.NewGuid(), Name = "Лук красный", WeightInGrams = 10, Price = 20})
                .Row(new {Id = Guid.NewGuid(), Name = "Бекон", WeightInGrams = 20, Price = 45})
                .Row(new {Id = Guid.NewGuid(), Name = "Помидоры", WeightInGrams = 35, Price = 50})
                .Row(new {Id = Guid.NewGuid(), Name = "Шампиньоны", WeightInGrams = 20, Price = 45})
                .Row(new {Id = Guid.NewGuid(), Name = "Ветчина", WeightInGrams = 30, Price = 65})
                .Row(new {Id = Guid.NewGuid(), Name = "Маринованные огурчики", WeightInGrams = 15, Price = 25})
                .Row(new {Id = Guid.NewGuid(), Name = "Сыр пармезан", WeightInGrams = 25, Price = 70})
                .Row(new {Id = Guid.NewGuid(), Name = "Чеснок", WeightInGrams = 10, Price = 15})
                .Row(new {Id = Guid.NewGuid(), Name = "Салями", WeightInGrams = 35, Price = 65})
                .Row(new {Id = Guid.NewGuid(), Name = "Пепперони", WeightInGrams = 15, Price = 60})
                .Row(new {Id = Guid.NewGuid(), Name = "Курица", WeightInGrams = 30, Price = 50})
                .Row(new {Id = Guid.NewGuid(), Name = "Помидорки черри", WeightInGrams = 55, Price = 80})
                .Row(new {Id = Guid.NewGuid(), Name = "Ананасы", WeightInGrams = 45, Price = 65});
        }
    }
}
