using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202204030005)]
    public class UpdateTables_202204030005 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("Products")
                .Row(new { Id = Guid.NewGuid(), Product = "4 сыра", TypeProduct = "Пицца", Picture = "https://cdpiz1.pizzasoft.ru/rs/280x280/pizzafab/items/3/4-syra-bolshaya-main_image-3416-31557.jpg", Description = "Тесто дрожжевое, сыр Моцарелла, соус томатный, сыр гауда, сыр пармезан, сыр с голубой плесенью, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Деревенская", TypeProduct = "Пицца", Picture = "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/3/derevenskaya-bolshaya-main_image-3506-42148.jpg", Description = "Тесто дрожжевое, сыр Моцарелла, ветчина, соус фирменный (лечо, майонез), колбаса полукопченая, томаты свежие, перец болгарский, шампиньоны свежие, огурцы маринованные, кукуруза консервированная, лук репчатый красный, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Жюльен", TypeProduct = "Пицца", Picture = "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/3/zhyulen-bolshaya-main_image-3540-75129.jpg", Description = "Тесто дрожжевое, сыр Моцарелла, куриное филе, соус цезарь, помидоры свежие, сыр пармезан, шампиньоны свежие, сливки, микс салат, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Пепперони", TypeProduct = "Пицца", Picture = "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/3/pepperoni-bolshaya-main_image-3612-77066.jpg", Description = "Пицца, которую мы запустили в космос! Тесто дрожжевое, сыр Моцарелла, колбаса пепперони (острая), соус томатный, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Гавайская", TypeProduct = "Пицца", Picture = "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/3/gavayskaya-bolshaya-main_image-3495-54466.jpg", Description = "Тесто дрожжевое, сыр моцарелла, ветчина, соус томатный, ананас консервированный, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Шоколадный милкшейк", TypeProduct = "Десерты", Picture = "https://carlsjr-norilsk.ru/wp-content/uploads/2019/12/chocolate-cocktail-1.jpg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Ванильный милкшейк", TypeProduct = "Десерты", Picture = "https://carlsjr-norilsk.ru/wp-content/uploads/2019/12/vanilla-cocktail-1.jpg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Клубничный милкшейк", TypeProduct = "Десерты", Picture = "https://carlsjr-norilsk.ru/wp-content/uploads/2019/12/strawberry-cocktail-1.jpg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Кофе Латте", TypeProduct = "Напитки", Picture = "https://dodopizza-a.akamaihd.net/static/Img/Products/870e47d7c0c6409eb3208d1e1f39d7fc_292x292.jpeg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Кофе Капучино", TypeProduct = "Напитки", Picture = "https://dodopizza-a.akamaihd.net/static/Img/Products/5972d1b78fec44b4a3fae17019c269cf_292x292.jpeg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Coca-Cola", TypeProduct = "Напитки", Picture = "https://dodopizza-a.akamaihd.net/static/Img/Products/5a945ed86ef943ac9583c4a6413d9ad0_292x292.jpeg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Coca-Cola Zero", TypeProduct = "Напитки", Picture = "https://dodopizza-a.akamaihd.net/static/Img/Products/d30242be31454f698db2028aed954e40_292x292.jpeg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Coca-Cola Vanilla", TypeProduct = "Напитки", Picture = "https://dodopizza-a.akamaihd.net/static/Img/Products/4dde423fc98f4c01a3862917ef7bcb25_292x292.jpeg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Sprite", TypeProduct = "Напитки", Picture = "https://dodopizza-a.akamaihd.net/static/Img/Products/73eb242273e0477e9544104ca9b1d42f_292x292.jpeg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Fanta", TypeProduct = "Напитки", Picture = "https://dodopizza-a.akamaihd.net/static/Img/Products/c5781875bf694dbc97bc327455cd87d9_292x292.jpeg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Салат Цезарь", TypeProduct = "Закуски", Picture = "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/1/salat-cezar-s-krevetkami-main_image-1278-77921.jpg", Description = "Салат айсберг, помидоры черри, сыр пармезан, гренки, соус цезарь." })
                .Row(new { Id = Guid.NewGuid(), Product = "Картофель Фри", TypeProduct = "Закуски", Picture = "https://cdpiz1.pizzasoft.ru/rs/280x280/pizzafab/items/7/kartofel-fri--bolshoy-main_image-7257-99040.jpg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Картофель по-деревенски", TypeProduct = "Закуски", Picture = "https://cdpiz1.pizzasoft.ru/rs/280x280/pizzafab/items/7/kartofel-po-derevenski--bolshoy-main_image-7256-62731.jpg", Description = "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Наггетсы", TypeProduct = "Закуски", Picture = "https://cdpiz1.pizzasoft.ru/rs/280x280/pizzafab/items/7/naggetsy--bolshie-main_image-7258-89313.jpg", Description = "" })
                ;

            Delete.FromTable("SizeProduct")
                .Row(new { Id = Guid.NewGuid(), Size = 25, Unit = "см" })
                .Row(new { Id = Guid.NewGuid(), Size = 30, Unit = "см" })
                .Row(new { Id = Guid.NewGuid(), Size = 35, Unit = "см" })
                .Row(new { Id = Guid.NewGuid(), Size = 0.3, Unit = "л" })
                .Row(new { Id = Guid.NewGuid(), Size = 0.4, Unit = "л" })
                .Row(new { Id = Guid.NewGuid(), Size = 0.5, Unit = "л" })
                .Row(new { Id = Guid.NewGuid(), Size = 0.33, Unit = "л" })
                .Row(new { Id = Guid.NewGuid(), Size = 1, Unit = "л" })
                ;
        }

        public override void Up()
        {
            Insert.IntoTable("Products")
                .Row(new { Id = Guid.NewGuid(), Product = "4 сыра", TypeProduct = "Пицца", Picture= "https://cdpiz1.pizzasoft.ru/rs/280x280/pizzafab/items/3/4-syra-bolshaya-main_image-3416-31557.jpg", Description= "Тесто дрожжевое, сыр Моцарелла, соус томатный, сыр гауда, сыр пармезан, сыр с голубой плесенью, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Деревенская", TypeProduct = "Пицца", Picture= "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/3/derevenskaya-bolshaya-main_image-3506-42148.jpg", Description= "Тесто дрожжевое, сыр Моцарелла, ветчина, соус фирменный (лечо, майонез), колбаса полукопченая, томаты свежие, перец болгарский, шампиньоны свежие, огурцы маринованные, кукуруза консервированная, лук репчатый красный, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Жюльен", TypeProduct = "Пицца", Picture= "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/3/zhyulen-bolshaya-main_image-3540-75129.jpg", Description= "Тесто дрожжевое, сыр Моцарелла, куриное филе, соус цезарь, помидоры свежие, сыр пармезан, шампиньоны свежие, сливки, микс салат, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Пепперони", TypeProduct = "Пицца", Picture= "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/3/pepperoni-bolshaya-main_image-3612-77066.jpg", Description= "Пицца, которую мы запустили в космос! Тесто дрожжевое, сыр Моцарелла, колбаса пепперони (острая), соус томатный, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Гавайская", TypeProduct = "Пицца", Picture= "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/3/gavayskaya-bolshaya-main_image-3495-54466.jpg", Description= "Тесто дрожжевое, сыр моцарелла, ветчина, соус томатный, ананас консервированный, сушеный базилик." })
                .Row(new { Id = Guid.NewGuid(), Product = "Шоколадный милкшейк", TypeProduct = "Десерты", Picture= "https://carlsjr-norilsk.ru/wp-content/uploads/2019/12/chocolate-cocktail-1.jpg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Ванильный милкшейк", TypeProduct = "Десерты", Picture= "https://carlsjr-norilsk.ru/wp-content/uploads/2019/12/vanilla-cocktail-1.jpg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Клубничный милкшейк", TypeProduct = "Десерты", Picture= "https://carlsjr-norilsk.ru/wp-content/uploads/2019/12/strawberry-cocktail-1.jpg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Кофе Латте", TypeProduct = "Напитки", Picture= "https://dodopizza-a.akamaihd.net/static/Img/Products/870e47d7c0c6409eb3208d1e1f39d7fc_292x292.jpeg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Кофе Капучино", TypeProduct = "Напитки", Picture= "https://dodopizza-a.akamaihd.net/static/Img/Products/5972d1b78fec44b4a3fae17019c269cf_292x292.jpeg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Coca-Cola", TypeProduct = "Напитки", Picture= "https://dodopizza-a.akamaihd.net/static/Img/Products/5a945ed86ef943ac9583c4a6413d9ad0_292x292.jpeg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Coca-Cola Zero", TypeProduct = "Напитки", Picture= "https://dodopizza-a.akamaihd.net/static/Img/Products/d30242be31454f698db2028aed954e40_292x292.jpeg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Coca-Cola Vanilla", TypeProduct = "Напитки", Picture= "https://dodopizza-a.akamaihd.net/static/Img/Products/4dde423fc98f4c01a3862917ef7bcb25_292x292.jpeg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Sprite", TypeProduct = "Напитки", Picture= "https://dodopizza-a.akamaihd.net/static/Img/Products/73eb242273e0477e9544104ca9b1d42f_292x292.jpeg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Fanta", TypeProduct = "Напитки", Picture= "https://dodopizza-a.akamaihd.net/static/Img/Products/c5781875bf694dbc97bc327455cd87d9_292x292.jpeg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Салат Цезарь", TypeProduct = "Закуски", Picture= "https://cdpiz1.pizzasoft.ru/rs/600x600/pizzafab/items/1/salat-cezar-s-krevetkami-main_image-1278-77921.jpg", Description= "Салат айсберг, помидоры черри, сыр пармезан, гренки, соус цезарь." })
                .Row(new { Id = Guid.NewGuid(), Product = "Картофель Фри", TypeProduct = "Закуски", Picture= "https://cdpiz1.pizzasoft.ru/rs/280x280/pizzafab/items/7/kartofel-fri--bolshoy-main_image-7257-99040.jpg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Картофель по-деревенски", TypeProduct = "Закуски", Picture= "https://cdpiz1.pizzasoft.ru/rs/280x280/pizzafab/items/7/kartofel-po-derevenski--bolshoy-main_image-7256-62731.jpg", Description= "" })
                .Row(new { Id = Guid.NewGuid(), Product = "Наггетсы", TypeProduct = "Закуски", Picture= "https://cdpiz1.pizzasoft.ru/rs/280x280/pizzafab/items/7/naggetsy--bolshie-main_image-7258-89313.jpg", Description= "" })
                ;

            Insert.IntoTable("SizeProduct")
                .Row(new { Id = Guid.NewGuid(), Size = 25, Unit = "см" })
                .Row(new { Id = Guid.NewGuid(), Size = 30, Unit = "см" })
                .Row(new { Id = Guid.NewGuid(), Size = 35, Unit = "см" })
                .Row(new { Id = Guid.NewGuid(), Size = 0.3, Unit = "л" })
                .Row(new { Id = Guid.NewGuid(), Size = 0.4, Unit = "л" })
                .Row(new { Id = Guid.NewGuid(), Size = 0.5, Unit = "л" })
                .Row(new { Id = Guid.NewGuid(), Size = 0.33, Unit = "л" })
                .Row(new { Id = Guid.NewGuid(), Size = 1, Unit = "л" })
                ;
        }
    }
}
