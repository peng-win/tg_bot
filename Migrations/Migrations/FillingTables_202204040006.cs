using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202204040006)]
    public class FillingTables_202204040006 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Delete.Column("ProductId").FromTable("Menu");
            Create.Column("Product").OnTable("Menu").AsString(50).NotNullable().ForeignKey("Products", "Product");
            
            Alter.Column("Size").OnTable("SizeProduct").AsDouble().NotNullable().Unique();

            Delete.Column("Size").FromTable("Menu");
            Create.Column("Size").OnTable("Menu").AsDouble().Nullable().ForeignKey("SizeProduct", "Size");


            Insert.IntoTable("Menu")
                .Row(new {Id = Guid.NewGuid(), Product = "Пепперони", Size = 25, WeightInGrams = 390, Price = 440})
                .Row(new {Id = Guid.NewGuid(), Product = "Пепперони", Size = 30, WeightInGrams = 580, Price = 670})
                .Row(new {Id = Guid.NewGuid(), Product = "Пепперони", Size = 35, WeightInGrams = 780, Price = 830})
                .Row(new {Id = Guid.NewGuid(), Product = "4 сыра", Size = 25, WeightInGrams = 380, Price = 490})
                .Row(new {Id = Guid.NewGuid(), Product = "4 сыра", Size = 30, WeightInGrams = 570, Price = 740})
                .Row(new {Id = Guid.NewGuid(), Product = "4 сыра", Size = 35, WeightInGrams = 760, Price = 890})
                .Row(new {Id = Guid.NewGuid(), Product = "Жюльен", Size = 25, WeightInGrams = 410, Price = 390})
                .Row(new {Id = Guid.NewGuid(), Product = "Жюльен", Size = 30, WeightInGrams = 620, Price = 600})
                .Row(new {Id = Guid.NewGuid(), Product = "Жюльен", Size = 35, WeightInGrams = 830, Price = 730})
                .Row(new {Id = Guid.NewGuid(), Product = "Шоколадный милкшейк", Size = 0.3, Price = 175})
                .Row(new {Id = Guid.NewGuid(), Product = "Ванильный милкшейк", Size = 0.3, Price = 175})
                .Row(new {Id = Guid.NewGuid(), Product = "Клубничный милкшейк", Size = 0.3,  Price = 175})
                .Row(new {Id = Guid.NewGuid(), Product = "Coca-Cola", Size = 0.5,  Price = 110})
                .Row(new {Id = Guid.NewGuid(), Product = "Кофе Латте", Size = 0.4,  Price = 150})
                .Row(new {Id = Guid.NewGuid(), Product = "Салат Цезарь", WeightInGrams = "210", Price = 210})
                ;
        }
    }
}
