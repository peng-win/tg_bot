using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202205270010)]
    public class _202205270010 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("Menu")
                .Row(new { Id = Guid.NewGuid(), Product = "Деревенская", Size = 25, WeightInGrams = 390, Price = 440 })
                .Row(new { Id = Guid.NewGuid(), Product = "Деревенская", Size = 30, WeightInGrams = 550, Price = 680 })
                .Row(new { Id = Guid.NewGuid(), Product = "Деревенская", Size = 35, WeightInGrams = 760, Price = 820 })
                .Row(new { Id = Guid.NewGuid(), Product = "Гавайская", Size = 25, WeightInGrams = 400, Price = 460 })
                .Row(new { Id = Guid.NewGuid(), Product = "Гавайская", Size = 30, WeightInGrams = 560, Price = 700 })
                .Row(new { Id = Guid.NewGuid(), Product = "Гавайская", Size = 35, WeightInGrams = 740, Price = 850 })
                .Row(new { Id = Guid.NewGuid(), Product = "Coca-Cola Zero", Size = 0.5,  Price = 110 })
                .Row(new { Id = Guid.NewGuid(), Product = "Coca-Cola Vanilla", Size = 0.5,  Price = 110 })
                .Row(new { Id = Guid.NewGuid(), Product = "Sprite", Size = 0.5,  Price = 110 })
                .Row(new { Id = Guid.NewGuid(), Product = "Fanta", Size = 0.5,  Price = 110 })
                .Row(new { Id = Guid.NewGuid(), Product = "Картофель Фри",  WeightInGrams = 150, Price = 200 })
                .Row(new { Id = Guid.NewGuid(), Product = "Картофель Фри",  WeightInGrams = 200, Price = 290 })
                .Row(new { Id = Guid.NewGuid(), Product = "Картофель по-деревенски", WeightInGrams = 150, Price = 220 })
                .Row(new { Id = Guid.NewGuid(), Product = "Картофель по-деревенски", WeightInGrams = 200, Price = 290 })
                .Row(new { Id = Guid.NewGuid(), Product = "Наггетсы", WeightInGrams = 180, Price = 280 })
                .Row(new { Id = Guid.NewGuid(), Product = "Наггетсы", WeightInGrams = 260, Price = 370 })
                ;
        }
    }
}
